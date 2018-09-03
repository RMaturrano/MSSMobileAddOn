using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.dao;
using AddonSeidorMobile.entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AddonSeidorMobile.view
{
    public class AsistenteConfiguracion : FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;

        //PANEL 1;
        #region FORMOBJ_PANEL_1
        SAPbouiCOM.Button btnRegistrar;
        SAPbouiCOM.Button btnP1Siguiente;
        SAPbouiCOM.UserDataSource udsEstadoConfiguracion;
        SAPbouiCOM.UserDataSource udsEstadoSociedad;
        SAPbouiCOM.UserDataSource udsCondicionSociedad;
        SAPbouiCOM.UserDataSource udsEstadoOrden;
        SAPbouiCOM.UserDataSource udsEstadoPago;
        SAPbouiCOM.UserDataSource udsMotivoTraslado;
        SAPbouiCOM.UserDataSource udsActLocalizacion;
        SAPbouiCOM.UserDataSource udsCtaTransferencia;
        SAPbouiCOM.UserDataSource udsCtaEfectivo;
        SAPbouiCOM.UserDataSource udsCtaCheque;
        SAPbouiCOM.EditText edtSociedad;
        SAPbouiCOM.EditText edtDescripcion;
        SAPbouiCOM.EditText edtUsuario;
        SAPbouiCOM.EditText edtPassword;
        SAPbouiCOM.EditText edtIdInterno;
        SAPbouiCOM.EditText edtMaxLineasOrdn;
        SAPbouiCOM.EditText edtMotivoTraslado;
        SAPbouiCOM.EditText edtPais;
        SAPbouiCOM.ComboBox cboEstado;
        SAPbouiCOM.ComboBox cboCondicion;
        SAPbouiCOM.ComboBox cboEstOrden;
        SAPbouiCOM.ComboBox cboEstPago;
        #endregion

        #region IDS_PANEL_1
        private const string BTN_P1NEXT = "btnP1Next";
        private const string BTN_P1PREV = "Item_8";
        private const string BTN_REGISTRAR = "btnReg";
        private const string UDS_ESTADO_CONFIGURACION = "UD_CB1";
        private const string UDS_ESTADO_SOCIEDAD = "UD_SL1";
        private const string UDS_CONDICION_SOCIEDAD = "UD_SL2";
        private const string UDS_EST_ORDR = "UD_4";
        private const string UDS_EST_ORCT = "UD_5";
        private const string UDS_MOTIVO_TRASLADO = "UD_7";
        private const string UDS_ACT_LOCALIZACION = "UD_LOC";
        private const string UDS_CTA_TRANSFERENCIA = "UD_AC1";
        private const string UDS_CTA_EFECTIVO = "UD_AC2";
        private const string UDS_CTA_CHEQUE = "UD_AC3";
        private const string EDT_SOCIEDAD = "p1Emp";
        private const string EDT_DESCRIPCION = "p1Des";
        private const string EDT_USUARIO = "p1Usr";
        private const string EDT_PASSWORD = "p1Clv";
        private const string EDT_IDINTERNO = "p1Idi";
        private const string EDT_MAXLINEAS = "Item_7";
        private const string EDT_MOT_TRASLADO = "Item_22";
        private const string CBO_ESTADO = "p1Est";
        private const string CBO_CONDICION = "p1Cdc";
        private const string CBO_EST_ORDR = "Item_16";
        private const string CBO_EST_ORCT = "Item_21";
        private const string BASE_REGISTRADA = "02";
        private const string BASE_NO_REGISTRADA = "01";
        private const string ACTIVO = "A";
        private const string INACTIVO = "I";
        private const string EDT_PAIS = "Item_24";
        private const string EDT_CTA_TRANSFERENCIA = "Item_31";
        private const string EDT_CTA_EFECTIVO = "Item_32";
        private const string EDT_CTA_CHEQUE = "Item_33";
        #endregion

        //PANEL 2:
        #region FORMOBJ_PANEL_2
        SAPbouiCOM.EditText edtCodigoMenu;
        SAPbouiCOM.EditText edtDescrpMenu;
        SAPbouiCOM.Grid gridListaMenu;
        #endregion

        #region IDS_PANEL_2
        public const string EDT_CODIGO_MENU = "p2Cod";
        public const string EDT_DESCRIPCION_MENU = "p2Des";
        public const string BTN_AGREGAR_MENU = "p2BtnAdd";
        public const string BTN_ELIMINAR_MENU = "p2BtnDel";
        public const string BTN_ACTUALIZAR_MENU = "p2BtnUpd";
        public const string BTN_LIMPIAR = "p2BtnLim";
        public const string GRID_LISTA_MENU = "p2GrdMnu";
        #endregion

        public AsistenteConfiguracion(Dictionary<string, IForm> dictionary)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmAstCon, FormName.AST_CONFIGURACION);
            if (mForm != null)
            {
                dictionary.Add(getFormUID(), this);
                mForm.Visible = true;

                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.AST_CONFIGURACION + " revise el log del sistema.");
        }

        public void instanciarObjetosUI()
        {
            udsEstadoConfiguracion = mForm.DataSources.UserDataSources.Item(UDS_ESTADO_CONFIGURACION);
            udsEstadoSociedad = mForm.DataSources.UserDataSources.Item(UDS_ESTADO_SOCIEDAD);
            udsCondicionSociedad = mForm.DataSources.UserDataSources.Item(UDS_CONDICION_SOCIEDAD);
            udsEstadoOrden = mForm.DataSources.UserDataSources.Item(UDS_EST_ORDR);
            udsEstadoPago = mForm.DataSources.UserDataSources.Item(UDS_EST_ORCT);
            udsCondicionSociedad = mForm.DataSources.UserDataSources.Item(UDS_CONDICION_SOCIEDAD);
            udsMotivoTraslado = mForm.DataSources.UserDataSources.Item(UDS_MOTIVO_TRASLADO);
            edtSociedad = mForm.Items.Item(EDT_SOCIEDAD).Specific;
            edtDescripcion = mForm.Items.Item(EDT_DESCRIPCION).Specific;
            edtUsuario = mForm.Items.Item(EDT_USUARIO).Specific;
            edtPassword = mForm.Items.Item(EDT_PASSWORD).Specific;
            edtIdInterno = mForm.Items.Item(EDT_IDINTERNO).Specific;
            edtMaxLineasOrdn = mForm.Items.Item(EDT_MAXLINEAS).Specific;
            edtMotivoTraslado = mForm.Items.Item(EDT_MOT_TRASLADO).Specific;
            edtPais = mForm.Items.Item(EDT_PAIS).Specific;
            cboEstado = mForm.Items.Item(CBO_ESTADO).Specific;
            cboCondicion = mForm.Items.Item(CBO_CONDICION).Specific;
            cboEstOrden = mForm.Items.Item(CBO_EST_ORDR).Specific;
            cboEstPago = mForm.Items.Item(CBO_EST_ORCT).Specific;
            btnRegistrar = mForm.Items.Item(BTN_REGISTRAR).Specific;
            btnP1Siguiente = mForm.Items.Item(BTN_P1NEXT).Specific;
            udsActLocalizacion = mForm.DataSources.UserDataSources.Item(UDS_ACT_LOCALIZACION);
            udsCtaTransferencia = mForm.DataSources.UserDataSources.Item(UDS_CTA_TRANSFERENCIA);
            udsCtaEfectivo = mForm.DataSources.UserDataSources.Item(UDS_CTA_EFECTIVO);
            udsCtaCheque = mForm.DataSources.UserDataSources.Item(UDS_CTA_CHEQUE);

            edtCodigoMenu = mForm.Items.Item(EDT_CODIGO_MENU).Specific;
            edtDescrpMenu = mForm.Items.Item(EDT_DESCRIPCION_MENU).Specific;
            gridListaMenu = mForm.Items.Item(GRID_LISTA_MENU).Specific;
        }

        public void iniciarValoresPorDefecto()
        {
            string sociedad = Conexion.company.CompanyDB;

            if (EmpresaDAO.empresaExiste(sociedad))
            {
                EmpresaBean mEmpresa = EmpresaDAO.obtenerEmpresa(sociedad);
                if (mEmpresa != null)
                {
                    edtSociedad.Value = mEmpresa.base_datos;
                    edtDescripcion.Value = mEmpresa.descripcion;
                    edtUsuario.Value = mEmpresa.usuario;
                    edtPassword.Value = mEmpresa.password;
                    edtIdInterno.Value = mEmpresa.id.ToString();
                    edtMaxLineasOrdn.Value = mEmpresa.maximoLineas.ToString();
                    edtPais.Value = mEmpresa.pais;

                    udsCondicionSociedad.Value = BASE_REGISTRADA;
                    udsEstadoSociedad.Value = mEmpresa.estado;
                    udsEstadoOrden.Value = mEmpresa.estadoOrden;
                    udsEstadoPago.Value = mEmpresa.estadoPago;
                    udsMotivoTraslado.Value = mEmpresa.motivoTraslado;
                    udsActLocalizacion.Value = mEmpresa.localizacion;
                    udsCtaTransferencia.Value = mEmpresa.ctaPagoTransferencia;
                    udsCtaEfectivo.Value = mEmpresa.ctaPagoEfectivo;
                    udsCtaCheque.Value = mEmpresa.ctaPagoCheque;

                    btnRegistrar.Caption = "Actualizar";
                    udsEstadoConfiguracion.Value = "Y";

                    btnP1Siguiente.Item.Enabled = true;
                }
            }
            else
            {
                edtSociedad.Value = Conexion.company.CompanyDB;
                edtDescripcion.Value = Conexion.company.CompanyName;
                edtUsuario.Value = Conexion.company.UserName;
                edtPassword.Value = string.Empty;
                edtIdInterno.Value = string.Empty;
                edtMaxLineasOrdn.Value = "50";

                udsCondicionSociedad.Value = BASE_NO_REGISTRADA;
                udsEstadoSociedad.Value = ACTIVO;

                btnRegistrar.Caption = "Registrar";
                udsEstadoConfiguracion.Value = "N";
                udsEstadoOrden.Value = "02";
                udsEstadoPago.Value = "02";
                udsActLocalizacion.Value = "N";

                btnP1Siguiente.Item.Enabled = false;
            }
        }

        public bool HandleItemEvents(SAPbouiCOM.ItemEvent itemEvent)
        {
            var result = true;
            try
            {
                if (itemEvent.BeforeAction)
                {
                    if (itemEvent.ItemUID.Equals(GRID_LISTA_MENU) && itemEvent.Row >= 0)
                    {
                        if (!string.IsNullOrEmpty(gridListaMenu.DataTable.GetValue("Code", itemEvent.Row)))
                        {
                            edtCodigoMenu.Value = gridListaMenu.DataTable.GetValue("Code", itemEvent.Row).ToString().Trim();
                            edtDescripcion.Value = gridListaMenu.DataTable.GetValue("Name", itemEvent.Row).ToString().Trim();
                        }
                    }
                    else
                    {
                        switch (mForm.Items.Item(itemEvent.ItemUID).Type)
                        {
                            case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                                switch (itemEvent.ItemUID)
                                {
                                    case BTN_REGISTRAR:
                                        if (udsCondicionSociedad.Value.Equals(BASE_NO_REGISTRADA))
                                            registrarBase();
                                        else
                                            actualizarBase();
                                        break;
                                    case BTN_P1NEXT:
                                        if (mForm.PaneLevel == 1)
                                        {
                                            mForm.PaneLevel = 2;
                                            cargarMenus();
                                        }
                                        break;
                                    case BTN_P1PREV:
                                        if (mForm.PaneLevel == 2)
                                            mForm.PaneLevel = 1;
                                        break;
                                    case BTN_LIMPIAR:
                                        cargarMenus();
                                        break;
                                    case BTN_AGREGAR_MENU:
                                        registrarMenu();
                                        break;
                                    case BTN_ACTUALIZAR_MENU:
                                        actualizarMenu();
                                        break;
                                    case BTN_ELIMINAR_MENU:
                                        eliminarMenu();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                    }
                }
                else if (itemEvent.ItemUID.Equals(EDT_PAIS) && !itemEvent.BeforeAction &&
                       itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                {
                    var selectedObjects = ((SAPbouiCOM.ChooseFromListEvent)itemEvent).SelectedObjects;

                    if (selectedObjects != null)
                    {
                        mForm.DataSources.UserDataSources.Item("UD_9").Value = selectedObjects.GetValue("Code", 0).ToString().Trim();
                    }
                }
                else if (itemEvent.ItemUID.Equals(EDT_CTA_TRANSFERENCIA) && !itemEvent.BeforeAction &&
                       itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                {
                    var selectedObjects = ((SAPbouiCOM.ChooseFromListEvent)itemEvent).SelectedObjects;

                    if (selectedObjects != null)
                    {
                        udsCtaTransferencia.Value = selectedObjects.GetValue("AcctCode", 0).ToString().Trim();
                    }
                }
                else if (itemEvent.ItemUID.Equals(EDT_CTA_EFECTIVO) && !itemEvent.BeforeAction &&
                       itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                {
                    var selectedObjects = ((SAPbouiCOM.ChooseFromListEvent)itemEvent).SelectedObjects;

                    if (selectedObjects != null)
                    {
                        udsCtaEfectivo.Value = selectedObjects.GetValue("AcctCode", 0).ToString().Trim();
                    }
                }
                else if (itemEvent.ItemUID.Equals(EDT_CTA_CHEQUE) && !itemEvent.BeforeAction &&
                       itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                {
                    var selectedObjects = ((SAPbouiCOM.ChooseFromListEvent)itemEvent).SelectedObjects;

                    if (selectedObjects != null)
                    {
                        udsCtaCheque.Value = selectedObjects.GetValue("AcctCode", 0).ToString().Trim();
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
            return true;
        }

        public bool HandleRightClickEvent(SAPbouiCOM.ContextMenuInfo menuInfo)
        {
            return true;
        }

        public string getFormUID()
        {
            return mForm.UniqueID;
        }


        //Funciones
        private void registrarBase()
        {
            try
            {
                if (validarPanel1())
                {
                    string description = Regex.Replace(edtDescripcion.Value.Trim(), @"\t|\n|\r|'", "");

                    if (EmpresaDAO.registrar(new EmpresaBean() { base_datos = edtSociedad.Value.Trim(),
                        descripcion = description,
                        estado = cboEstado.Selected.Value.Trim(),
                        usuario = edtUsuario.Value.Trim(),
                        password = edtPassword.Value.Trim(),
                        maximoLineas = int.Parse(edtMaxLineasOrdn.Value.Trim()),
                        estadoOrden = cboEstOrden.Selected.Value.Trim(),
                        estadoPago = cboEstPago.Selected.Value.Trim(),
                        motivoTraslado = edtMotivoTraslado.Value.Trim(),
                        pais = edtPais.Value.Trim(),
                        localizacion = udsActLocalizacion.Value.Trim(),
                        ctaPagoTransferencia = udsCtaTransferencia.Value.Trim(),
                        ctaPagoEfectivo = udsCtaEfectivo.Value.Trim(),
                        ctaPagoCheque = udsCtaCheque.Value.Trim()}))
                    {
                        StatusMessageSuccess("Datos de la sociedad registrados con éxito");
                        iniciarValoresPorDefecto();
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("registrarBase() > " + ex.Message);
            }
        }

        private void actualizarBase()
        {
            try
            {
                if (validarPanel1())
                {
                    if (EmpresaDAO.actualizar(new EmpresaBean(){id = int.Parse(edtIdInterno.Value.Trim()),
                                                                descripcion = edtDescripcion.Value.Trim(),
                                                                estado = cboEstado.Selected.Value.Trim(),
                                                                usuario = edtUsuario.Value.Trim(),
                                                                password = edtPassword.Value.Trim(),
                                                                maximoLineas = int.Parse(edtMaxLineasOrdn.Value.Trim()),
                                                                estadoOrden = cboEstOrden.Selected.Value.Trim(),
                                                                estadoPago = cboEstPago.Selected.Value.Trim(),
                                                                motivoTraslado = edtMotivoTraslado.Value.Trim(),
                                                                pais = edtPais.Value.Trim(),
                                                                localizacion = udsActLocalizacion.Value.Trim(),
                                                                ctaPagoTransferencia = udsCtaTransferencia.Value.Trim(),
                                                                ctaPagoEfectivo = udsCtaEfectivo.Value.Trim(),
                                                                ctaPagoCheque = udsCtaCheque.Value.Trim()}))
                    {
                        StatusMessageSuccess("Datos de la sociedad actualizados con éxito");
                        iniciarValoresPorDefecto();
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("actualizarBase() > " + ex.Message);
            }
        }

        private bool validarPanel1()
        {
            var res = true;

            try
            {
                if (string.IsNullOrEmpty(edtDescripcion.Value))
                {
                    res = false;
                    StatusMessageError("Debe ingresar la descripción de la sociedad.");
                }else if(cboEstado.Selected == null){
                    res = false;
                    StatusMessageError("Debe seleccionar el estado de la sociedad.");
                }
                else if (string.IsNullOrEmpty(edtUsuario.Value))
                {
                    res = false;
                    StatusMessageError("Debe ingresar el usuario.");
                }
                else if (string.IsNullOrEmpty(edtPassword.Value))
                {
                    res = false;
                    StatusMessageError("Debe ingresar la clave del usuario.");
                }
                else if (string.IsNullOrEmpty(edtMaxLineasOrdn.Value))
                {
                    res = false;
                    StatusMessageError("Debe ingresar la cantidad máxima de líneas por pedido.");
                }
                else if (int.Parse(edtMaxLineasOrdn.Value) == 0)
                {
                    res = false;
                    StatusMessageError("La cantidad máxima de líneas por pedido no puede ser cero.");
                }
                else if (cboEstOrden.Selected == null)
                {
                    res = false;
                    StatusMessageError("Debe seleccionar el tipo de recepción de orden de venta desde la aplicación.");
                }
                else if (cboEstPago.Selected == null)
                {
                    res = false;
                    StatusMessageError("Debe seleccionar el tipo de recepción de pagos desde la aplicación.");
                }else if (string.IsNullOrEmpty(edtPais.Value))
                {
                    res = false;
                    StatusMessageError("Debe seleccionar un país.");
                }
                else if (string.IsNullOrEmpty(udsCtaTransferencia.Value))
                {
                    res = false;
                    StatusMessageError("Debe seleccionar una cuenta para el tipo de pago 'Transferencia/Depósito'.");
                }
                else if (string.IsNullOrEmpty(udsCtaEfectivo.Value))
                {
                    res = false;
                    StatusMessageError("Debe seleccionar una cuenta para el tipo de pago 'Efectivo'");
                }
                else if (string.IsNullOrEmpty(udsCtaCheque.Value))
                {
                    res = false;
                    StatusMessageError("Debe seleccionar una cuenta para el tipo de pago 'Cheque'");
                }
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("validarPanel1() > " + ex.Message);
            }

            return res;
        }

        private void cargarMenus()
        {
            try
            {
                gridListaMenu.DataTable.ExecuteQuery(MenuAppDAO.QUERY_LIST_MENU);
                gridListaMenu.AutoResizeColumns();

                edtCodigoMenu.Value = MenuAppDAO.obtenerUltimoId().ToString("D2");
                edtDescrpMenu.Value = string.Empty;
            }
            catch (Exception ex)
            {
                StatusMessageError("cargarMenus() > " + ex.Message);
            }
        }

        private void registrarMenu()
        {
            try
            {
                if (validarControles())
                {
                    string codigo = edtCodigoMenu.Value.Trim();
                    string descripcion = edtDescrpMenu.Value.Trim();

                    if (!MenuAppDAO.codigoExiste(codigo))
                    {
                        MenuAppDAO.registrar(edtCodigoMenu.Value.ToString().Trim(), edtDescrpMenu.Value.ToString().Trim());
                        PermisoDAO.registrarMenuDetalleDefault(codigo, descripcion);
                        StatusMessageSuccess("Registro creado con éxito.");
                        cargarMenus();
                    }
                    else
                    {
                        StatusMessageError("El código seleccionado ya existe, si desea actualizarlo seleccione el botón \"U\".");
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("registrarMenu() > " + ex.Message);
            }
        }

        private void actualizarMenu()
        {
            try
            {
                if (validarControles())
                {
                    string codigo = edtCodigoMenu.Value.Trim();
                    string descripcion = edtDescrpMenu.Value.Trim();

                    if (MenuAppDAO.codigoExiste(codigo))
                    {
                        MenuAppDAO.actualizar(codigo, descripcion);
                        StatusMessageSuccess("Registro actualizado con éxito.");
                        cargarMenus();
                    }
                    else
                    {
                        StatusMessageError("El código seleccionado no existe, si desea crearlo seleccione el botón \"+\".");
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("actualizarMenu() > " + ex.Message);
            }
        }

        private void eliminarMenu()
        {
            try
            {
                string codigo = edtCodigoMenu.Value.Trim();

                if (MenuAppDAO.codigoExiste(codigo))
                {
                    if (PermisoDAO.eliminarMenu(int.Parse(MenuAppDAO.obtenerIdxCodigo(codigo))))
                    {
                        MenuAppDAO.eliminar(codigo);
                        StatusMessageSuccess("Registro eliminado con éxito.");
                        cargarMenus();
                    }
                }
                else
                {
                    StatusMessageError("El código seleccionado no existe, si desea crearlo seleccione el botón \"+\".");
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("eliminarMenu() > " + ex.Message);
            }
        }

        private bool validarControles()
        {
            bool res = true;

            try
            {
                if (string.IsNullOrEmpty(edtCodigoMenu.Value))
                {
                    res = false;
                    StatusMessageError("Debe ingresar el código del menú");
                }
                else if (string.IsNullOrEmpty(edtDescrpMenu.Value))
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
