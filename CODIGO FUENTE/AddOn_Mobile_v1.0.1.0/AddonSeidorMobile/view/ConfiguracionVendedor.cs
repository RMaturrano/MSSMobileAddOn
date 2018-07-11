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
    public class ConfiguracionVendedor : FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;
        private SAPbouiCOM.EditText edtCodVendedor;
        private SAPbouiCOM.EditText edtNomVendedor;
        private SAPbouiCOM.ComboBox cboPerfil;
        private SAPbouiCOM.ComboBox cboSeries;
        private SAPbouiCOM.EditText edtUsuarioMovil;
        private SAPbouiCOM.EditText edtPasswrdMovil;
        private SAPbouiCOM.EditText edtEquipoAsignado;
        private SAPbouiCOM.Matrix mtxAlmacenes;
        private SAPbouiCOM.DBDataSource dsCabecera;
        private SAPbouiCOM.DBDataSource dsDetAlmacenes;

        //IDS
        private const string EDT_COD_VENDEDOR = "edtVen";
        private const string EDT_NOM_VENDEDOR = "edtNom";
        private const string EDT_USUARIO_MOVIL = "edtUsr";
        private const string EDT_PASSWRD_MOVIL = "edtPass";
        private const string EDT_EQUIPO_ASIGNADO = "edtEqp";
        private const string EDT_PROYECTO = "txtPrj";
        private const string CBO_SERIE = "Item_9";
        private const string EDT_VEHICULO = "Item_11";
        private const string CBO_PERFIL = "cboPer";
        private const string MATRIZ_ALMACENES = "mtxAlm";
        private const string MATRIZ_GRUPOS_CLIENTES = "mtxCli";
        private const string MATRIZ_GRUPOS_ZONAS = "mtxZon";
        private const string DEFAULT_TAB = "Item_7";
        private string DS_CABECERA = "@" + Vendedor.getTabla().nombre;
        private string DS_DETALLE_1 = "@" + Vendedor.getTablaDet1().nombre;
        //private string DS_DETALLE_2 = "@" + Vendedor.getTablaDet2().nombre;
        //private string DS_DETALLE_3 = "@" + Vendedor.getTablaDet3().nombre;
        private const string BTN_ADD_ROW_P1 = "btnAdd1";
        private const string BTN_DEL_ROW_P1 = "btnDel1";
        private const string BTN_ADD_ROW_P2 = "btnAdd2";
        private const string BTN_DEL_ROW_P2 = "btnDel2";
        private const string BTN_ADD_ROW_P3 = "btnAdd3";
        private const string BTN_DEL_ROW_P3 = "btnDel3";
        private const string BTN_PRINCIPAL = "1";

        public ConfiguracionVendedor(Dictionary<string, IForm> dictionary)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmVendedor, FormName.CFG_VENDEDOR);
            if (mForm != null)
            {
                dictionary.Add(getFormUID(), this);
                mForm.Visible = true;

                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.CFG_VENDEDOR + " revise el log del sistema.");
        }

        #region FORM_INIT
        public void instanciarObjetosUI()
        {
            edtCodVendedor = mForm.Items.Item(EDT_COD_VENDEDOR).Specific;
            edtNomVendedor = mForm.Items.Item(EDT_NOM_VENDEDOR).Specific;
            edtEquipoAsignado = mForm.Items.Item(EDT_EQUIPO_ASIGNADO).Specific;
            edtUsuarioMovil = mForm.Items.Item(EDT_USUARIO_MOVIL).Specific;
            edtPasswrdMovil = mForm.Items.Item(EDT_PASSWRD_MOVIL).Specific;
            mtxAlmacenes = mForm.Items.Item(MATRIZ_ALMACENES).Specific;
            cboPerfil = mForm.Items.Item(CBO_PERFIL).Specific;
            cboSeries = mForm.Items.Item(CBO_SERIE).Specific;
            //mtxGruposArticulo = mForm.Items.Item(MATRIZ_GRUPOS_ARTICULOS).Specific;
            //mtxGruposCliente = mForm.Items.Item(MATRIZ_GRUPOS_CLIENTES).Specific;
            //mtxGruposZona = mForm.Items.Item(MATRIZ_GRUPOS_ZONAS).Specific;

            dsCabecera = mForm.DataSources.DBDataSources.Item(DS_CABECERA);
            dsDetAlmacenes = mForm.DataSources.DBDataSources.Item(DS_DETALLE_1);
            //dsDetalle2 = mForm.DataSources.DBDataSources.Item(DS_DETALLE_2);
            //dsDetalle3 = mForm.DataSources.DBDataSources.Item(DS_DETALLE_3);
        }

        public void iniciarValoresPorDefecto()
        {
            if (cboPerfil.ValidValues.Count == 0)
            {
                var perfiles = TipoUsuarioDAO.listar();

                foreach (var item in perfiles)
                {
                    cboPerfil.ValidValues.Add(item.codigo, item.descripcion);
                }
            }

            if (cboSeries.ValidValues.Count == 0)
            {
                var series = SeriesDAO.listar();

                foreach (var item in series)
                {
                    cboSeries.ValidValues.Add(item.codigo, item.descripcion);
                }
            }


            dsDetAlmacenes.Clear();
            dsDetAlmacenes.InsertRecord(dsDetAlmacenes.Size);
            dsDetAlmacenes.SetValue("U_MSSM_COD", dsDetAlmacenes.Size - 1, string.Empty);
            dsDetAlmacenes.SetValue("U_MSSM_NOM", dsDetAlmacenes.Size - 1, string.Empty);
            mtxAlmacenes.LoadFromDataSource();

            //dsDetalle2.Clear();
            //dsDetalle2.InsertRecord(dsDetalle2.Size);
            //dsDetalle2.SetValue("U_MSSM_COD", dsDetalle2.Size - 1, string.Empty);
            //dsDetalle2.SetValue("U_MSSM_NOM", dsDetalle2.Size - 1, string.Empty);
            //mtxGruposCliente.LoadFromDataSource();

            //dsDetalle3.Clear();
            //dsDetalle3.InsertRecord(dsDetalle3.Size);
            //dsDetalle3.SetValue("U_MSSM_COD", dsDetalle3.Size - 1, string.Empty);
            //dsDetalle3.SetValue("U_MSSM_NOM", dsDetalle3.Size - 1, string.Empty);
            //mtxGruposZona.LoadFromDataSource();

            //mForm.Items.Item(DEFAULT_TAB).Click();
            mForm.Items.Item(EDT_COD_VENDEDOR).Click();
        }
        #endregion

        #region SAP_EVENTS
        public bool HandleItemEvents(SAPbouiCOM.ItemEvent itemEvent)
        {
            var res = true;

            try
            {
                switch (itemEvent.ItemUID)
                {
                    case EDT_COD_VENDEDOR:
                        if (!itemEvent.BeforeAction && itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                        {
                            var selectedObjects = ((SAPbouiCOM.IChooseFromListEvent)itemEvent).SelectedObjects;

                            if (selectedObjects != null)
                            {
                                dsCabecera.SetValue("Code", 0, selectedObjects.GetValue("SlpCode", 0).ToString().Trim());
                                dsCabecera.SetValue("Name", 0, selectedObjects.GetValue("SlpName", 0).ToString().Trim());

                                if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                    mForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                            }
                        }
                        break;
                    case MATRIZ_ALMACENES:
                        if (!itemEvent.BeforeAction && itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                        {
                            var selectedObjects = ((SAPbouiCOM.IChooseFromListEvent)itemEvent).SelectedObjects;

                            if (selectedObjects != null)
                            {
                                dsDetAlmacenes.SetValue("U_MSSM_COD", itemEvent.Row - 1, selectedObjects.GetValue("WhsCode", 0).ToString().Trim());
                                dsDetAlmacenes.SetValue("U_MSSM_NOM", itemEvent.Row - 1, selectedObjects.GetValue("WhsName", 0).ToString().Trim());
                                mtxAlmacenes.LoadFromDataSource();

                                if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                    mForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                            }
                        }
                        break;
                    case EDT_EQUIPO_ASIGNADO:
                        if (!itemEvent.BeforeAction && itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                        {
                            var selectedObjects = ((SAPbouiCOM.IChooseFromListEvent)itemEvent).SelectedObjects;

                            if (selectedObjects != null)
                            {
                                dsCabecera.SetValue("U_MSSM_EQP", 0, selectedObjects.GetValue("Code", 0).ToString().Trim());

                                if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                    mForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                            }
                        }
                        break;
                    case EDT_PROYECTO:
                        if (!itemEvent.BeforeAction && itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                        {
                            var selectedObjects = ((SAPbouiCOM.IChooseFromListEvent)itemEvent).SelectedObjects;

                            if (selectedObjects != null)
                            {
                                dsCabecera.SetValue("U_MSSM_PRJ", 0, selectedObjects.GetValue("PrjCode", 0).ToString().Trim());

                                if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                    mForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                            }
                        }
                        break;
                    case EDT_VEHICULO:
                        if (!itemEvent.BeforeAction && itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                        {
                            var selectedObjects = ((SAPbouiCOM.IChooseFromListEvent)itemEvent).SelectedObjects;

                            if (selectedObjects != null)
                            {
                                dsCabecera.SetValue("U_MSSM_VEH", 0, selectedObjects.GetValue("Code", 0).ToString().Trim());

                                if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                    mForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                            }
                        }
                        break;
                    //case MATRIZ_GRUPOS_ARTICULOS:
                    //    if (!itemEvent.BeforeAction && itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                    //    {
                    //        var selectedObjects = ((SAPbouiCOM.IChooseFromListEvent)itemEvent).SelectedObjects;

                    //        if (selectedObjects != null)
                    //        {
                    //            dsDetalle1.SetValue("U_MSSM_COD", itemEvent.Row - 1, selectedObjects.GetValue("ItmsGrpCod", 0).ToString().Trim());
                    //            dsDetalle1.SetValue("U_MSSM_NOM", itemEvent.Row - 1, selectedObjects.GetValue("ItmsGrpNam", 0).ToString().Trim());
                    //            mtxGruposArticulo.LoadFromDataSource();

                    //            if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                    //                mForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                    //        }
                    //    }
                    //    break;
                    //case MATRIZ_GRUPOS_CLIENTES:
                    //    if (!itemEvent.BeforeAction && itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                    //    {
                    //        var selectedObjects = ((SAPbouiCOM.IChooseFromListEvent)itemEvent).SelectedObjects;

                    //        if (selectedObjects != null)
                    //        {
                    //            dsDetalle2.SetValue("U_MSSM_COD", itemEvent.Row - 1, selectedObjects.GetValue("GroupCode", 0).ToString().Trim());
                    //            dsDetalle2.SetValue("U_MSSM_NOM", itemEvent.Row - 1, selectedObjects.GetValue("GroupName", 0).ToString().Trim());
                    //            mtxGruposCliente.LoadFromDataSource();

                    //            if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                    //                mForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                    //        }
                    //    }
                    //    break;
                    case BTN_ADD_ROW_P1:
                        if (!itemEvent.BeforeAction)
                        {
                            dsDetAlmacenes.InsertRecord(dsDetAlmacenes.Size);
                            dsDetAlmacenes.SetValue("U_MSSM_COD", dsDetAlmacenes.Size - 1, string.Empty);
                            dsDetAlmacenes.SetValue("U_MSSM_NOM", dsDetAlmacenes.Size - 1, string.Empty);
                            mtxAlmacenes.LoadFromDataSource();
                        }
                        break;
                    //case BTN_ADD_ROW_P2:
                    //    if (!itemEvent.BeforeAction)
                    //    {
                    //        dsDetalle2.InsertRecord(dsDetalle2.Size);
                    //        dsDetalle2.SetValue("U_MSSM_COD", dsDetalle2.Size - 1, string.Empty);
                    //        dsDetalle2.SetValue("U_MSSM_NOM", dsDetalle2.Size - 1, string.Empty);
                    //        mtxGruposCliente.LoadFromDataSource();
                    //    }
                    //    break;
                    //case BTN_ADD_ROW_P3:
                    //    if (!itemEvent.BeforeAction)
                    //    {
                    //        dsDetalle3.InsertRecord(dsDetalle3.Size);
                    //        dsDetalle3.SetValue("U_MSSM_COD", dsDetalle3.Size - 1, string.Empty);
                    //        dsDetalle3.SetValue("U_MSSM_NOM", dsDetalle3.Size - 1, string.Empty);
                    //        mtxGruposZona.LoadFromDataSource();
                    //    }
                    //    break;
                    case BTN_DEL_ROW_P1:
                        if (!itemEvent.BeforeAction)
                        {
                            List<int> indexes = new List<int>();
                            for (int i = 1; i <= mtxAlmacenes.RowCount; i++)
                            {
                                if (mtxAlmacenes.IsRowSelected(i))
                                    indexes.Add(i);
                            }

                            if (indexes.Count > 0)
                            {
                                int counter = 0;
                                while (counter <= indexes.Count)
                                {
                                    for (int i = 1; i <= mtxAlmacenes.RowCount; i++)
                                    {
                                        if (mtxAlmacenes.IsRowSelected(i))
                                        {
                                            mtxAlmacenes.DeleteRow(i);
                                            break;
                                        }
                                    }

                                    counter++;
                                }

                                if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                    mForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;

                                mtxAlmacenes.FlushToDataSource();
                            }
                        }
                        break;
                    //case BTN_DEL_ROW_P2:
                    //    if (!itemEvent.BeforeAction)
                    //    {
                    //        List<int> indexes = new List<int>();
                    //        for (int i = 1; i <= mtxGruposCliente.RowCount; i++)
                    //        {
                    //            if (mtxGruposCliente.IsRowSelected(i))
                    //                indexes.Add(i);
                    //        }

                    //        if (indexes.Count > 0)
                    //        {
                    //            int counter = 0;
                    //            while (counter <= indexes.Count)
                    //            {
                    //                for (int i = 1; i <= mtxGruposCliente.RowCount; i++)
                    //                {
                    //                    if (mtxGruposCliente.IsRowSelected(i))
                    //                    {
                    //                        mtxGruposCliente.DeleteRow(i);
                    //                        break;
                    //                    }
                    //                }

                    //                counter++;
                    //            }

                    //            if (mForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                    //                mForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;

                    //            mtxGruposCliente.FlushToDataSource();
                    //        }
                    //    }
                    //    break;
                    //case BTN_DEL_ROW_P3:
                    //    if (!itemEvent.BeforeAction)
                    //    {

                    //    }
                    //    break;
                    case BTN_PRINCIPAL:
                        if (itemEvent.BeforeAction && (mForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE || mForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE))
                        {
                            res = validarObjeto();
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

            return res;
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

        public string getFormUID()
        {
            return mForm.UniqueID;
        }

        private bool validarObjeto()
        {
            var result = true;

            try
            {
                if (string.IsNullOrEmpty(edtCodVendedor.Value))
                {
                    result = false;
                    StatusMessageError("Debe ingresar el código del vendedor.");
                }
                else if (cboPerfil.Selected == null)
                {
                    result = false;
                    StatusMessageError("Debe seleccionar el perfil del vendedor.");
                }
                else if (!string.IsNullOrEmpty(edtUsuarioMovil.Value) && !string.IsNullOrEmpty(edtCodVendedor.Value)
                    && VendedorDAO.existeUsuarioMovil(edtUsuarioMovil.Value, edtCodVendedor.Value))
                {
                    result = false;
                    StatusMessageError("El usuario móvil ya existe en otro usuario.");
                }
                else if (!string.IsNullOrEmpty(edtEquipoAsignado.Value) && !string.IsNullOrEmpty(edtCodVendedor.Value)
                    && VendedorDAO.existeEquipoAsignado(edtEquipoAsignado.Value, edtCodVendedor.Value))
                {
                    result = false;
                    StatusMessageError("El equipo ya fue asignado a otro usuario.");
                }
            }
            catch (Exception ex)
            {
                result = false;
                StatusMessageError("validarObjeto() > " + ex.Message);
            }

            return result;
        }
    }
}
