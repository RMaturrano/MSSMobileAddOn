using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.dao;
using AddonSeidorMobile.data_schema.tablas;
using AddonSeidorMobile.entity;

namespace AddonSeidorMobile.view
{
    public class MaestroEquipo : FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;
        private SAPbouiCOM.DBDataSource dsEquipo;
        private SAPbouiCOM.Matrix mtxPrincipal;

        public MaestroEquipo(Dictionary<string, IForm> dictionary)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmEquipo, FormName.MAESTRO_EQUIPOS);
            if (mForm != null)
            {
                dictionary.Add(getFormUID(), this);

                mForm.Visible = true;
             
                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.MAESTRO_EQUIPOS + " revise el log del sistema.");
        }

        public void instanciarObjetosUI()
        {
            try
            {
                dsEquipo = mForm.DataSources.DBDataSources.Item("@" + Movil.getTabla().nombre);
                mtxPrincipal = mForm.Items.Item("Item_0").Specific;
            }
            catch (Exception ex)
            {
                StatusMessageError("instanciarObjetosUI() > " + ex.Message);
            }
        }

        public void iniciarValoresPorDefecto()
        {
            try
            {
                //Conexion.application.Menus.Item(Constantes.Menu_Crear).Enabled = false;
                //Conexion.application.Menus.Item(Constantes.Menu_Buscar).Enabled = false;

                dsEquipo.Clear();

                var mListEquipos = EquipoDAO.listar();

                foreach (var item in mListEquipos)
                {
                    dsEquipo.InsertRecord(dsEquipo.Size);
                    insertToDS(dsEquipo.Size - 1, item.docEntry.ToString(), item.codigo, item.descripcion, 
                        item.modelo, item.serie, item.color, item.codigoUnico, item.verificarId);
                }

                dsEquipo.InsertRecord(dsEquipo.Size);
                insertToDS(dsEquipo.Size - 1, string.Empty);

                mtxPrincipal.LoadFromDataSource();

                for (int i = 0; i < dsEquipo.Size - 1; i++)
                {
                    mtxPrincipal.CommonSetting.SetCellEditable(i + 1, 1, false);
                }

                mtxPrincipal.AutoResizeColumns();
                mtxPrincipal.SetCellFocus(mtxPrincipal.RowCount, 1);
            }
            catch (Exception ex)
            {
                StatusMessageError("iniciarValoresPorDefecto() > " + ex.Message);
            }
        }

        #region EVENTS_ITEM
        public bool HandleItemEvents(SAPbouiCOM.ItemEvent itemEvent)
        {
            var result = true;
            try
            {
                if (itemEvent.BeforeAction)
                {
                    switch (mForm.Items.Item(itemEvent.ItemUID).Type)
                    {
                        case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                            switch (itemEvent.ItemUID)
                            {
                                case "1":
                                    if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE ||
                                       mForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                                        result = RegistrarObjetos();
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                StatusMessageError("HandleItemEvents() > " + ex.Message);
            }

            return result;
        }
        #endregion

        #region EVENTS_FORM
        public bool HandleFormDataEvents(SAPbouiCOM.BusinessObjectInfo oBusinessObjectInfo)
        {
            return true;
        }
        #endregion

        #region EVENTS_MENU
        public bool HandleMenuDataEvents(SAPbouiCOM.MenuEvent menuEvent)
        {
            return true;
        }
        #endregion

        #region EVENTS_RIGHTCLICK
        public bool HandleRightClickEvent(SAPbouiCOM.ContextMenuInfo menuInfo)
        {
            return true;
        }
        #endregion

        public string getFormUID()
        {
            return mForm.UniqueID;
        }

        private bool RegistrarObjetos()
        {
            var res = true;

            try
            {
                Conexion.company.StartTransaction();

                mtxPrincipal.FlushToDataSource();


                //Recorrer el datasource principal para buscar nuevos registros y actualizar
                for (int i = dsEquipo.Size - 1; i >= 0; i--)
                {
                    var docEntry = dsEquipo.GetValue("DocEntry", i);
                    string codigo = dsEquipo.GetValue("Code", i).Trim();
                    string descripcion = dsEquipo.GetValue("Name", i).Trim();
                    string modelo = dsEquipo.GetValue("U_MSSM_MOD", i).Trim();
                    string serie = dsEquipo.GetValue("U_MSSM_SER", i).Trim();
                    string color = dsEquipo.GetValue("U_MSSM_COL", i).Trim();
                    string codigoUnico = dsEquipo.GetValue("U_MSSM_IDU", i).Trim();
                    string verificarID = dsEquipo.GetValue("U_MSSM_VAL", i).Trim();

                    EquipoBean bean = new EquipoBean()
                    {
                        codigo = codigo,
                        descripcion = descripcion,
                        modelo = modelo,
                        serie = serie,
                        color = color,
                        codigoUnico = codigoUnico,
                        verificarId = verificarID
                    };

                    if (docEntry != null && !string.IsNullOrEmpty(docEntry.ToString().Trim()))
                    {
                        res = EquipoDAO.actualizar(bean);

                        if (!res)
                            break;
                    }
                    else if (!string.IsNullOrEmpty(codigo))
                    {
                        res = EquipoDAO.registrar(bean);

                        if (!res)
                            break;
                    }
                }

                if (res)
                {
                    if (Conexion.company.InTransaction)
                        Conexion.company.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);

                    iniciarValoresPorDefecto();
                }
                else
                {
                    if (Conexion.company.InTransaction)
                        Conexion.company.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("RegistrarObjetos() > " + ex.Message);
            }

            return res;
        }

        private void insertToDS(int index, string val0, string val1 = null, string val2 = null, string val3 = null,
                                string val4 = null, string val5 = null, string val6 = null, string val7 = "N")
        {
            dsEquipo.SetValue("DocEntry", dsEquipo.Size - 1, val0);
            dsEquipo.SetValue("Code", dsEquipo.Size - 1, val1);
            dsEquipo.SetValue("Name", dsEquipo.Size - 1, val2);
            dsEquipo.SetValue("U_MSSM_MOD", dsEquipo.Size - 1, val3);
            dsEquipo.SetValue("U_MSSM_SER", dsEquipo.Size - 1, val4);
            dsEquipo.SetValue("U_MSSM_COL", dsEquipo.Size - 1, val5);
            dsEquipo.SetValue("U_MSSM_IDU", dsEquipo.Size - 1, val6);
            dsEquipo.SetValue("U_MSSM_VAL", dsEquipo.Size - 1, val7);
        }
    }
}
