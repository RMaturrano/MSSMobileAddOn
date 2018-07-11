using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.dao;
using AddonSeidorMobile.data_schema.tablas;
using AddonSeidorMobile.entity;

namespace AddonSeidorMobile.view
{
    public class ConfiguracionPermisos: FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;
        private int menu_counter = 1;
        private SAPbouiCOM.Grid mtxCab;
        private SAPbouiCOM.Grid mtxDet;
        private SAPbouiCOM.StaticText lblText;

        //IDS
        private const string MATRIZ_CAB = "mtxTUsr";
        private const string MATRIZ_DET = "mtxMenu";
        private const string COL_COD_TUSR = "Col_0";
        private const string BUTTON_UPDATE = "1";
        private const string LBL_TXT = "lblTxt";

        public ConfiguracionPermisos(Dictionary<string, IForm> dictionary)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmPermisos, FormName.CFG_PERMISOS_X_TIPO_USR);
            if (mForm != null)
            {
                dictionary.Add(getFormUID(), this);
                mForm.Visible = true;

                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.CFG_PERMISOS_X_TIPO_USR + " revise el log del sistema.");
        }

        #region INICIAR_FORM
        public void instanciarObjetosUI()
        {
            mtxCab = mForm.Items.Item(MATRIZ_CAB).Specific;
            mtxDet = mForm.Items.Item(MATRIZ_DET).Specific;
            lblText = mForm.Items.Item(LBL_TXT).Specific;
        }

        public void iniciarValoresPorDefecto()
        {
            //var mListPermisos = PermisoDAO.listarCabecera();

            //foreach (var item in mListPermisos)
            //{
            //    mtxCab.DataTable.Rows.Add();
            //    mtxCab.DataTable.SetValue("Código", mtxCab.DataTable.Rows.Count - 1, item.codigo);
            //    mtxCab.DataTable.SetValue("Descripción", mtxCab.DataTable.Rows.Count - 1, item.descripcion);
            //}

            mtxCab.DataTable.ExecuteQuery(PermisoDAO.getQueryForList());
            mtxCab.AutoResizeColumns();

            lblText.Item.ForeColor = ColorTranslator.ToOle(Color.Blue);
            lblText.Item.FontSize = 12;
        }
        #endregion

        #region EVENTOS_SAP
        public bool HandleItemEvents(SAPbouiCOM.ItemEvent itemEvent)
        {
            var result = true;

            try
            {
                switch (itemEvent.ItemUID)
                {
                    case MATRIZ_DET:
                        if (!itemEvent.BeforeAction)
                        {
                            if (itemEvent.ColUID.Equals("#") && itemEvent.Row == 0)
                            {
                                if (menu_counter < 6)
                                    menu_counter++;
                                else if (menu_counter == 6)
                                {
                                    StatusMessageInfo("Se mostrará el formulario de menús de la aplicación...");
                                    menu_counter++;
                                }
                                else if (menu_counter == 7)
                                {
                                    Conexion.application.Menus.Item(FormName.MAESTRO_MENUAPP).Enabled = true;
                                    Conexion.application.Forms.GetFormByTypeAndCount(169, 1).Update();
                                    Conexion.application.ActivateMenuItem(FormName.MAESTRO_MENUAPP);
                                    Conexion.application.Menus.Item(FormName.MAESTRO_MENUAPP).Enabled = false;
                                    Conexion.application.Forms.GetFormByTypeAndCount(169, 1).Update();
                                    menu_counter = 1;
                                }
                                else
                                    menu_counter = 1;
                            }
                            else
                                menu_counter = 1;
                        }
                        break;
                    case MATRIZ_CAB:
                        if (!itemEvent.BeforeAction)
                        {
                            if (itemEvent.ColUID.Equals("RowsHeader") && itemEvent.Row >= 0)
                            {
                                if (itemEvent.Row - 1 < mtxCab.DataTable.Rows.Count)
                                {
                                    mtxDet.DataTable.Rows.Clear();

                                    
                                    string codigo = mtxCab.DataTable.GetValue("Código", itemEvent.Row).Trim();
                                    string descripcion = mtxCab.DataTable.GetValue("Descripción", itemEvent.Row).Trim();

                                    if (!string.IsNullOrEmpty(codigo))
                                    {
                                        //var detalles = PermisoDAO.listarDetalle(codigo.Trim());
                                        bool isActive = PermisoDAO.verificarEstadoTU(codigo);

                                        //foreach (var item in detalles)
                                        //{
                                        //    mtxDet.DataTable.Rows.Add();
                                        //    mtxDet.DataTable.SetValue("Código", mtxDet.DataTable.Rows.Count - 1, item.codigoMenu);
                                        //    mtxDet.DataTable.SetValue("Descripción", mtxDet.DataTable.Rows.Count - 1, item.descripcionMenu);
                                        //    mtxDet.DataTable.SetValue("Accesa", mtxDet.DataTable.Rows.Count - 1, item.accesa);
                                        //    mtxDet.DataTable.SetValue("Crea", mtxDet.DataTable.Rows.Count - 1, item.crea);
                                        //    mtxDet.DataTable.SetValue("Edita", mtxDet.DataTable.Rows.Count - 1, item.edita);
                                        //    mtxDet.DataTable.SetValue("Aprueba", mtxDet.DataTable.Rows.Count - 1, item.aprueba);
                                        //    mtxDet.DataTable.SetValue("Rechaza", mtxDet.DataTable.Rows.Count - 1, item.rechaza);
                                        //    mtxDet.DataTable.SetValue("Sel. Lista precio", mtxDet.DataTable.Rows.Count - 1, item.escogePrecio);
                                        //}

                                        mtxDet.DataTable.ExecuteQuery(PermisoDAO.getQueryForListDetail(codigo));
                                        mtxDet.Columns.Item(2).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
                                        mtxDet.Columns.Item(3).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
                                        mtxDet.Columns.Item(4).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
                                        mtxDet.Columns.Item(5).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
                                        mtxDet.Columns.Item(6).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
                                        mtxDet.Columns.Item(7).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
                                        mtxDet.AutoResizeColumns();

                                        if (!isActive)
                                        {
                                            string caption = "El tipo de usuario " + descripcion + ", se encuentra deshabilitado.";
                                            lblText.Caption = caption;
                                            lblText.Item.ForeColor = ColorTranslator.ToOle(Color.Red);
                                            cambiarHabilitacionCeldas(false);
                                        }
                                        else
                                        {
                                            lblText.Caption = "Mostrando permisos del tipo de usuario " + descripcion;
                                            lblText.Item.ForeColor = ColorTranslator.ToOle(Color.Blue);
                                            cambiarHabilitacionCeldas(true);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case BUTTON_UPDATE:
                        if (itemEvent.BeforeAction && mForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                        {
                            string selected = string.Empty;

                            for (int i = 0; i < mtxCab.DataTable.Rows.Count; i++)
                            {
                                if (mtxCab.Rows.IsSelected(i))
                                {
                                    selected = mtxCab.DataTable.GetValue("Código", i);
                                    break;
                                }
                            }

                            if (string.IsNullOrEmpty(selected))
                            {
                                StatusMessageInfo("Debe seleccionar una fila en la lista de Tipo de usuario para actualizar.");
                                result = false;
                            }
                            else
                                result = actualizarPermiso(selected.Trim());
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("HandleItemEvents() > " + ex.Message);
            }

            return result;
        }

        public bool HandleFormDataEvents(SAPbouiCOM.BusinessObjectInfo oBusinessObjectInfo)
        {
            return true;
        }

        public bool HandleMenuDataEvents(SAPbouiCOM.MenuEvent menuEvent)
        {
            return true;
        }

        public bool HandleRightClickEvent(SAPbouiCOM.ContextMenuInfo menuInfo)
        {
            return true;
        }
        #endregion

        #region UTILS
        public string getFormUID()
        {
            return mForm.UniqueID;
        }

        private void cambiarHabilitacionCeldas(bool mostrar)
        {
            try
            {
                mForm.Freeze(true);

                for (int i = 0; i < mtxDet.DataTable.Rows.Count; i++)
                {
                    mtxDet.CommonSetting.SetCellEditable(i + 1, 3, mostrar);
                    mtxDet.CommonSetting.SetCellEditable(i + 1, 4, mostrar);
                    mtxDet.CommonSetting.SetCellEditable(i + 1, 5, mostrar);
                    mtxDet.CommonSetting.SetCellEditable(i + 1, 6, mostrar);
                    mtxDet.CommonSetting.SetCellEditable(i + 1, 7, mostrar);
                    mtxDet.CommonSetting.SetCellEditable(i + 1, 8, mostrar);
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("cambiarHabilitacionCeldas() > " + ex.Message);
            }
            finally
            {
                mForm.Freeze(false);
            }
        }
        #endregion

        private bool actualizarPermiso(string selected)
        {
            var res = true;

            try
            {
                PermisoBean tipoUserPermiso = new PermisoBean();
                tipoUserPermiso.codigo = selected.Trim();

                List<PermisoDetBean> permisos = new List<PermisoDetBean>();

                for (int i = 0; i < mtxDet.DataTable.Rows.Count; i++)
                {
                    permisos.Add(new PermisoDetBean()
                    {
                        codigoMenu = mtxDet.DataTable.GetValue("Código", i).ToString().Trim(),
                        accesa = mtxDet.DataTable.GetValue("Accesa", i).Trim(),
                        crea = mtxDet.DataTable.GetValue("Crea", i).Trim(),
                        edita = mtxDet.DataTable.GetValue("Edita", i).Trim(),
                        aprueba = mtxDet.DataTable.GetValue("Aprueba", i).Trim(),
                        rechaza = mtxDet.DataTable.GetValue("Rechaza", i).Trim(),
                        escogePrecio = mtxDet.DataTable.GetValue("Sel. Lista precio", i).Trim()
                    });
                }

                tipoUserPermiso.detalles = permisos;

                res = PermisoDAO.actualizar(tipoUserPermiso);
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("actualizarPermiso() > " + ex.Message);
            }

            return res;
        }

    }
}
