using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.dao;
using AddonSeidorMobile.data_schema.tablas;
using AddonSeidorMobile.data_schema.database;

namespace AddonSeidorMobile.view
{
    public class MaestroMenuApp : FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;
        private SAPbouiCOM.Grid oMatrix;
        private SAPbouiCOM.EditText edtCodigo;
        private SAPbouiCOM.EditText edtDescripcion;

        //IDS
        private const string MATRIZ_PRINCIPAL = "mtxPrin";
        private const string BTN_ADD = "btnAdd";
        private const string BTN_UPD = "btnUpd";
        private const string BTN_DEL = "btnDel";
        private const string BTN_LIMPIAR = "btnClean";
        private const string EDT_CODIGO = "txtCode";
        private const string EDT_DESCRIPCION = "txtDes";


        public MaestroMenuApp(Dictionary<string, IForm> dictionary)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmMenu, FormName.MAESTRO_MENUAPP);
            if (mForm != null)
            {
                dictionary.Add(getFormUID(), this);
                mForm.Visible = true;

                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.MAESTRO_MENUAPP + " revise el log del sistema.");
        }

        #region INICIAR_FORM
        public void instanciarObjetosUI()
        {
            oMatrix = mForm.Items.Item(MATRIZ_PRINCIPAL).Specific;
            edtCodigo = mForm.Items.Item(EDT_CODIGO).Specific;
            edtDescripcion = mForm.Items.Item(EDT_DESCRIPCION).Specific;
        }

        public void iniciarValoresPorDefecto()
        {
            oMatrix.DataTable.ExecuteQuery(MenuAppDAO.QUERY_LIST_MENU);
            oMatrix.AutoResizeColumns();

            edtCodigo.Value = MenuAppDAO.obtenerUltimoId().ToString("D2");
            edtDescripcion.Value = string.Empty;
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
                    if (itemEvent.ItemUID.Equals(MATRIZ_PRINCIPAL) && itemEvent.Row >= 0)
                    {
                        if (!string.IsNullOrEmpty(oMatrix.DataTable.GetValue("Code", itemEvent.Row))) { 
                            edtCodigo.Value = oMatrix.DataTable.GetValue("Code", itemEvent.Row).ToString().Trim();
                            edtDescripcion.Value = oMatrix.DataTable.GetValue("Name", itemEvent.Row).ToString().Trim();
                        }
                    }
                    else { 
                        switch (mForm.Items.Item(itemEvent.ItemUID).Type)
                        {
                            case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                                switch (itemEvent.ItemUID)
                                {
                                    case BTN_ADD:
                                        RegistrarObjetos();
                                        break;
                                    case BTN_UPD:
                                        ActualizarObjetos();
                                        break;
                                    case BTN_DEL:
                                        EliminarObjetos();
                                        break;
                                    case BTN_LIMPIAR:
                                        iniciarValoresPorDefecto();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
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
            }
            catch (Exception ex)
            {
                result = false;
                StatusMessageError("HandleMenuDataEvents() > " + ex.Message);
            }

            return result;
        }

        public bool HandleRightClickEvent(SAPbouiCOM.ContextMenuInfo menuInfo)
        {
            var result = true;

            if (menuInfo.BeforeAction)
            {
                try
                {
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
        #endregion

        private bool EliminarObjetos()
        {
            var res = true;

            try
            {
                string codigo = edtCodigo.Value.Trim();
                
                if (MenuAppDAO.codigoExiste(codigo))
                {
                    if (PermisoDAO.eliminarMenu(int.Parse(MenuAppDAO.obtenerIdxCodigo(codigo)))) {
                        MenuAppDAO.eliminar(codigo);
                        StatusMessageSuccess("Registro eliminado con éxito.");
                        iniciarValoresPorDefecto();
                    }
                }
                else
                {
                    StatusMessageError("El código seleccionado no existe, si desea crearlo seleccione el botón \"+\".");
                }
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("EliminarObjetos() > " + ex.Message);
            }

            return res;
        }

        private bool RegistrarObjetos()
        {
            var res = true;

            try
            {
                if (validarControles())
                {
                    string codigo = edtCodigo.Value.Trim();
                    string descripcion = edtDescripcion.Value.Trim();

                    if (!MenuAppDAO.codigoExiste(codigo))
                    {
                        MenuAppDAO.registrar(edtCodigo.Value.ToString().Trim(), edtDescripcion.Value.ToString().Trim());
                        PermisoDAO.registrarMenuDetalleDefault(codigo, descripcion);
                        StatusMessageSuccess("Registro creado con éxito.");
                        iniciarValoresPorDefecto();
                    }
                    else
                    {
                        StatusMessageError("El código seleccionado ya existe, si desea actualizarlo seleccione el botón \"U\".");
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("RegistrarObjetos() > " + ex.Message);
            }

            return res;
        }

        private bool ActualizarObjetos()
        {
            var res = true;

            try
            {
                if (validarControles())
                {
                    string codigo = edtCodigo.Value.Trim();
                    string descripcion = edtDescripcion.Value.Trim();

                    if (MenuAppDAO.codigoExiste(codigo))
                    {
                        MenuAppDAO.actualizar(codigo, descripcion);
                        StatusMessageSuccess("Registro actualizado con éxito.");
                        iniciarValoresPorDefecto();
                    }else{
                        StatusMessageError("El código seleccionado no existe, si desea crearlo seleccione el botón \"+\".");
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("ActualizarObjetos() > " + ex.Message);
            }

            return res;
        }


        private bool validarControles()
        {
            bool res = true;

            try
            {
                if (string.IsNullOrEmpty(edtCodigo.Value))
                {
                    res = false;
                    StatusMessageError("Debe ingresar el código del menú");
                }
                else if (string.IsNullOrEmpty(edtDescripcion.Value))
                {
                    res = false;
                    StatusMessageError("Debe ingresar la descripción del menú");
                }
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("validarControles() > " + ex.Message);
            }

            return res;
        }
    }
}
