using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.dao;
using AddonSeidorMobile.data_schema.tablas;

namespace AddonSeidorMobile.view
{
    public class MaestroTipoUsuario : FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;
        private SAPbouiCOM.DBDataSource dsTipoUsuario;
        private SAPbouiCOM.Matrix oMatrix;

        //IDS
        private string TB_TIPOUSR_APP = "@" + TipoUsuario.getTabla().nombre;
        private const string MATRIZ_PRINCIPAL = "mtxPrin";

        //Right Click
        private string ItemUIDRightClick;
        private int RowItemRightClick;
        public List<string> deletedEntries { get; set; }

        public MaestroTipoUsuario(Dictionary<string, IForm> dictionary)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmTipoUsuario, FormName.MAESTRO_TIPOUSR);
            if (mForm != null)
            {
                dictionary.Add(getFormUID(), this);
                mForm.Visible = true;

                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.MAESTRO_TIPOUSR + " revise el log del sistema.");
        }

        #region INICIAR_FORM
        public void instanciarObjetosUI()
        {
            deletedEntries = new List<string>();

            dsTipoUsuario = mForm.DataSources.DBDataSources.Item(TB_TIPOUSR_APP);
            oMatrix = mForm.Items.Item(MATRIZ_PRINCIPAL).Specific;
        }

        public void iniciarValoresPorDefecto()
        {
            //Conexion.application.Menus.Item(Constantes.Menu_Crear).Enabled = false;
            //Conexion.application.Menus.Item(Constantes.Menu_Buscar).Enabled = false;

            dsTipoUsuario.Clear();
            deletedEntries.Clear();

            var mListMenu = TipoUsuarioDAO.listar();

            foreach (var item in mListMenu)
            {
                dsTipoUsuario.InsertRecord(dsTipoUsuario.Size);
                insertToDS(dsTipoUsuario.Size - 1, item.docEntry.ToString(), item.codigo, item.descripcion, item.activo, item.supervisor, item.cobrador);
            }

            dsTipoUsuario.InsertRecord(dsTipoUsuario.Size);
            insertToDS(dsTipoUsuario.Size - 1, string.Empty, string.Empty, string.Empty, "Y", "N", "N");

            oMatrix.LoadFromDataSource();

            for (int i = 0; i < dsTipoUsuario.Size - 1; i++)
            {
                oMatrix.CommonSetting.SetCellEditable(i + 1, 1, false);
            }

            oMatrix.AutoResizeColumns();
            oMatrix.SetCellFocus(oMatrix.RowCount, 1);
        }
        #endregion

        #region EVENTOS_SAP
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

        public bool HandleFormDataEvents(SAPbouiCOM.BusinessObjectInfo oBusinessObjectInfo)
        {
            return true;
        }

        public bool HandleMenuDataEvents(SAPbouiCOM.MenuEvent menuEvent)
        {
            var result = true;

            try
            {
                switch (menuEvent.MenuUID)
                {
                    case Constantes.Menu_EliminarLinea:
                        if (menuEvent.BeforeAction)
                            DeleteRow(RowItemRightClick, ItemUIDRightClick);
                        break;
                }
            }
            catch (Exception ex)
            {
                result = false;
                StatusMessageError("HandleMenuDataEvents() > " + ex.Message);
            }

            return result;
        }

        private void DeleteRow(int row, string ItemUID)
        {
            try
            {
                if (ItemUID.Equals(MATRIZ_PRINCIPAL))
                {
                    oMatrix.FlushToDataSource();

                    if (dsTipoUsuario.GetValue("Code", row - 1) != null &&
                        !string.IsNullOrEmpty(dsTipoUsuario.GetValue("Code", row - 1).Trim()))
                    {
                        deletedEntries.Add(dsTipoUsuario.GetValue("Code", row - 1).ToString().Trim());
                        dsTipoUsuario.RemoveRecord(row - 1);

                        if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                            mForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;

                        oMatrix.LoadFromDataSource();
                        oMatrix.CommonSetting.SetCellEditable(dsTipoUsuario.Size, 1, true);
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("DeleteRow() > " + ex.Message);
            }
        }

        public bool HandleRightClickEvent(SAPbouiCOM.ContextMenuInfo menuInfo)
        {
            var result = true;

            SAPbouiCOM.MenuItem oMenuItem;
            SAPbouiCOM.Menus oMenus;

            if (menuInfo.BeforeAction)
            {
                try
                {
                    if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || mForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                        || mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                    {
                        SAPbouiCOM.MenuCreationParams oCreationPackage = null;
                        oCreationPackage = Conexion.application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);
                        oMenuItem = Conexion.application.Menus.Item("1280");
                        oMenus = oMenuItem.SubMenus;

                        ItemUIDRightClick = menuInfo.ItemUID;
                        RowItemRightClick = menuInfo.Row;

                        if (menuInfo.Row > 0 && !oMenus.Exists(Constantes.Menu_EliminarLinea))
                        {
                            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                            oCreationPackage.UniqueID = Constantes.Menu_EliminarLinea;
                            oCreationPackage.String = Constantes.Menu_EliminarLineaDescripcion;
                            oCreationPackage.Position = 101;
                            oCreationPackage.Enabled = true;
                            oMenus.AddEx(oCreationPackage);
                        }
                    }
                }
                catch (Exception e)
                {
                    StatusMessageError("HandleRightClickEvent > BeforeAction > " + e.Message);
                }
            }
            else if (!menuInfo.BeforeAction)
            {
                try
                {
                    if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || mForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                    {
                        if (menuInfo.Row > 0)
                            Conexion.application.Menus.RemoveEx(Constantes.Menu_EliminarLinea);
                    }
                }
                catch (Exception e)
                {
                    StatusMessageError("HandleRightClickEvent > NotBeforeAction > " + e.Message);
                }

            }

            return result;
        }
        #endregion

        #region UTILS
        public string getFormUID()
        {
            return mForm.UniqueID;
        }

        private void insertToDS(int index, string val0, string val1, string val2, string val3, string val4, string val5)
        {
            try
            {
                dsTipoUsuario.SetValue("DocEntry", dsTipoUsuario.Size - 1, val0);
                dsTipoUsuario.SetValue("Code", dsTipoUsuario.Size - 1, val1);
                dsTipoUsuario.SetValue("Name", dsTipoUsuario.Size - 1, val2);
                dsTipoUsuario.SetValue("U_MSSM_HAB", dsTipoUsuario.Size - 1, val3);
                dsTipoUsuario.SetValue("U_MSSM_SUP", dsTipoUsuario.Size - 1, val4);
                dsTipoUsuario.SetValue("U_MSSM_COB", dsTipoUsuario.Size - 1, val5);
            }
            catch(Exception e)
            {
                StatusMessageError("insertToDS() > " + e.Message);
            }
        }
        #endregion

        private void EliminarObjetos()
        {
            if (deletedEntries.Count > 0)
            {
                foreach (string code in deletedEntries)
                {
                    TipoUsuarioDAO.eliminar(code);
                }
            }
        }

        private bool RegistrarObjetos()
        {
            var res = true;

            try
            {
                Conexion.company.StartTransaction();

                EliminarObjetos();
                oMatrix.FlushToDataSource();


                //Recorrer el datasource principal para buscar nuevos registros y actualizar
                for (int i = dsTipoUsuario.Size - 1; i >= 0; i--)
                {
                    var docEntry = dsTipoUsuario.GetValue("DocEntry", i);
                    string codigo = dsTipoUsuario.GetValue("Code", i).Trim();
                    string descripcion = dsTipoUsuario.GetValue("Name", i).Trim();
                    string activo = dsTipoUsuario.GetValue("U_MSSM_HAB", i).Trim();
                    string supervisor = dsTipoUsuario.GetValue("U_MSSM_SUP", i).Trim();
                    string cobrador = dsTipoUsuario.GetValue("U_MSSM_COB", i).Trim();

                    if (docEntry != null && !string.IsNullOrEmpty(docEntry.ToString().Trim()))
                    {
                        res = TipoUsuarioDAO.actualizar(codigo, descripcion, activo, supervisor, cobrador);

                        if (!res)
                            break;
                    }
                    else if (!string.IsNullOrEmpty(codigo))
                    {
                        res = TipoUsuarioDAO.registrar(codigo, descripcion, supervisor, cobrador);

                        if(res)
                           res = PermisoDAO.registrarPerfilDefault(codigo, descripcion);

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
    }
}
