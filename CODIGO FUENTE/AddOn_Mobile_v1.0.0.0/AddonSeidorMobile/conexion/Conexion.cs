using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using AddonSeidorMobile.view;
using AddonSeidorMobile.commons;
using System.IO;
using AddonSeidorMobile.data_schema.database;

namespace AddonSeidorMobile.conexion
{
    public class Conexion
    {
        public static SAPbobsCOM.Company company;
        public static SAPbouiCOM.Application application;
        private static readonly Dictionary<string, IForm> formOpen;

        static Conexion()
        {
            formOpen = new Dictionary<string, IForm>();
        }

        public Conexion()
        {
            application = instanciarAplicacion();
            company = instanciarCompania();
            inicializarFiltros();
            application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(application_AppEvent);
            application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(application_MenuEvent);
            application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(application_ItemEvent);
            application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(application_FormDataEvent);
            application.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(application_RightClickEvent);
            crearMenu();
            //verificarEstadoBaseMovil();
        }

        private void verificarEstadoBaseMovil()
        {
            SAPbobsCOM.Recordset oRS = null;

            try
            {
                oRS = company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery("select count(*) as \"Result\" from \"SYS\".\"P_SCHEMAS_\" where \"NAME\" = '" + BdMobile.BD_NAME + "'");
                if (oRS.RecordCount > 0)
                {
                    int result = int.Parse(oRS.Fields.Item("Result").Value.ToString().Trim());
                    if (result == 0)
                    {
                        application.StatusBar.SetText("Base móvil no encontrada. Se registrará la base móvil.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        oRS.DoQuery(BdMobile.getDataBaseSQL());
                        foreach (var scriptTable in BdMobile.getTablesSQL())
                        {
                            oRS.DoQuery(scriptTable);
                        }
                        application.StatusBar.SetText("Base móvil registrada.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                application.StatusBar.SetText("verificarEstadoBaseMovil() > " + ex.Message);
            }
            finally
            {
                if (oRS != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRS);
                }
            }
        }

        private SAPbouiCOM.Application instanciarAplicacion()
        {
            var guiApi = new SAPbouiCOM.SboGuiApi();
            guiApi.Connect(Environment.GetCommandLineArgs().GetValue(1).ToString());
            return guiApi.GetApplication();
        }

        private SAPbobsCOM.Company instanciarCompania()
        {
            try
            {
                return application.Company.GetDICompany();
            }
            catch (Exception e)
            {
               application.MessageBox(e.Message);
            }

            return null;
        }

        private void inicializarFiltros()
        {
            SAPbouiCOM.EventFilters filtros = new SAPbouiCOM.EventFilters();
            SAPbouiCOM.EventFilter filtroMenu = filtros.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK);

            SAPbouiCOM.EventFilter filtroItem = filtros.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED);
            filtroItem.AddEx(FormName.MAESTRO_EQUIPOS);
            filtroItem.AddEx(FormName.MAESTRO_MENUAPP);
            filtroItem.AddEx(FormName.MAESTRO_TIPOUSR);
            filtroItem.AddEx(FormName.CFG_PERMISOS_X_TIPO_USR);
            filtroItem.AddEx(FormName.CFG_VENDEDOR);
            filtroItem.AddEx(FormName.AST_CONFIGURACION);
            filtroItem.AddEx(FormName.MAESTRO_CLIENTES);
            filtroItem.AddEx(FormName.DOC_ORDEN_VENTA);
            filtroItem.AddEx(FormName.DOC_PAGO_RECIBIDO);
            filtroItem.AddEx(FormName.DOC_INCIDENCIAS);
            filtroItem.AddEx(FormName.LOG_REGISTROS);
            filtroItem.AddEx(FormName.DOC_DEVOLUCION);
            filtroItem.AddEx(FormName.DOC_NOTA_CREDITO);

            SAPbouiCOM.EventFilter filtroLinkPressed = filtros.Add(SAPbouiCOM.BoEventTypes.et_MATRIX_LINK_PRESSED);
            filtroLinkPressed.AddEx(FormName.DOC_ORDEN_VENTA);
            filtroLinkPressed.AddEx(FormName.DOC_PAGO_RECIBIDO);
            filtroLinkPressed.AddEx(FormName.DOC_DEVOLUCION);
            filtroLinkPressed.AddEx(FormName.DOC_NOTA_CREDITO);

            SAPbouiCOM.EventFilter filtroDoubleClick = filtros.Add(SAPbouiCOM.BoEventTypes.et_DOUBLE_CLICK);
            filtroDoubleClick.AddEx(FormName.DOC_ORDEN_VENTA);

            SAPbouiCOM.EventFilter filtroCFL = filtros.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST);
            filtroCFL.AddEx(FormName.CFG_VENDEDOR);

            SAPbouiCOM.EventFilter filtroRightClick = filtros.Add(SAPbouiCOM.BoEventTypes.et_RIGHT_CLICK);
            filtroRightClick.AddEx(FormName.MAESTRO_EQUIPOS);

            application.SetFilter(filtros);
        }

        //Eventos de aplicación
        void application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = formOpen[FormUID].HandleItemEvents(pVal);
        }

        void application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    company.Disconnect();
                    Environment.Exit(0);
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    company.Disconnect();
                    Environment.Exit(0);
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    company.Disconnect();
                    Environment.Exit(0);
                    break;
            }
        }

        void application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            var result = true;

            if (pVal.BeforeAction)
            {
                switch (pVal.MenuUID)
                {
                    case FormName.MAESTRO_EQUIPOS:
                        MaestroEquipo mEquipo = new MaestroEquipo(formOpen);
                        break;
                    case FormName.MAESTRO_TIPOUSR:
                        MaestroTipoUsuario mTipoUsuario = new MaestroTipoUsuario(formOpen);
                        break;
                    case FormName.MAESTRO_MENUAPP:
                        MaestroMenuApp mMenuApp = new MaestroMenuApp(formOpen);
                        break;
                    case FormName.CFG_PERMISOS_X_TIPO_USR:
                        ConfiguracionPermisos mPermisos = new ConfiguracionPermisos(formOpen);
                        break;
                    case FormName.CFG_VENDEDOR:
                        ConfiguracionVendedor mVendedor = new ConfiguracionVendedor(formOpen);
                        break;
                    case FormName.AST_CONFIGURACION:
                        AsistenteConfiguracion mAsistente = new AsistenteConfiguracion(formOpen);
                        break;
                    case FormName.MAESTRO_CLIENTES:
                        MaestroCliente mCliente = new MaestroCliente(formOpen);
                        break;
                    case FormName.DOC_ORDEN_VENTA:
                        DocOrdenes mOrden = new DocOrdenes(formOpen);
                        break;
                    case FormName.DOC_PAGO_RECIBIDO:
                        DocPagos mPago = new DocPagos(formOpen);
                        break;
                    case FormName.DOC_INCIDENCIAS:
                        DocIncidencias mIncidencias = new DocIncidencias(formOpen);
                        break;
                    case FormName.LOG_REGISTROS:
                        LogRegistros mLog = new LogRegistros(formOpen);
                        break;
                    case FormName.DOC_DEVOLUCION:
                        DocDevolucion mDev = new DocDevolucion(formOpen);
                        break;
                    case FormName.DOC_NOTA_CREDITO:
                        DocNotaCredito mNot = new DocNotaCredito(formOpen);
                        break;
                    default:
                        break;
                }
            }

            // Control "Crear" de la barra de herramientas || Control "Buscar" de la barra de herramientas
            if (pVal.MenuUID == Constantes.Menu_Crear || pVal.MenuUID == Constantes.Menu_Buscar
                || pVal.MenuUID == Constantes.Registro_Datos_Anterior || pVal.MenuUID == Constantes.Registro_Datos_Siguiente
                || pVal.MenuUID == Constantes.Primer_Registro_Datos || pVal.MenuUID == Constantes.Ultimo_Registro_Datos)
            {
                var mForm = application.Forms.ActiveForm;
                if (formOpen.ContainsKey(mForm.UniqueID))
                    formOpen[mForm.UniqueID].HandleMenuDataEvents(pVal);
            }

            //Controles basados en el menu "Click derecho"
            if (pVal.MenuUID == Constantes.Menu_AgregarLinea || pVal.MenuUID == Constantes.Menu_EliminarLinea)
            {
                if (pVal.BeforeAction)
                {
                    var mForm = application.Forms.ActiveForm;
                    if (formOpen.ContainsKey(mForm.UniqueID))
                        formOpen[mForm.UniqueID].HandleMenuDataEvents(pVal);
                }
            }

            BubbleEvent = result;
        }

        void application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = formOpen[BusinessObjectInfo.FormUID].HandleFormDataEvents(BusinessObjectInfo);
        }

        void application_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = formOpen[eventInfo.FormUID].HandleRightClickEvent(eventInfo);
        }

        //Creación de menú
        private void crearMenu()
        {
            SAPbouiCOM.Form frmApe = application.Forms.GetFormByTypeAndCount(169, 1);
            frmApe.Freeze(true);

            try
            {
                application.StatusBar.SetText(Constantes.PREFIX_MSG_ADDON + "Cargando opciones de menú", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_None);

                if (application.Menus.Exists(Constantes.MENU_PRINCIPAL_ID))
                {
                    application.Menus.RemoveEx(Constantes.MENU_PRINCIPAL_ID);
                    frmApe.Update();
                }

                XmlDocument xmlMenu = new XmlDocument();
                xmlMenu.LoadXml(AddonSeidorMobile.Properties.Resources.Menu);
                application.LoadBatchActions(xmlMenu.InnerXml);

                SAPbouiCOM.Menus oMenus = application.Menus;
                SAPbouiCOM.MenuItem oMenuItem = application.Menus.Item(Constantes.MENU_PRINCIPAL_ID);

                string path = Path.GetTempPath() + "\\menu_logo.jpg";
                AddonSeidorMobile.Properties.Resources.menu_logo.Save(path);
                oMenuItem.Image = path;

                application.StatusBar.SetText(Constantes.PREFIX_MSG_ADDON + "Menú cargado con éxito", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch (Exception e)
            {
                application.StatusBar.SetText(Constantes.PREFIX_MSG_ADDON + e.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                frmApe.Freeze(false);
                frmApe.Update();
            }
        }

        public static void addForm(string UID, IForm newForm)
        {
            formOpen.Add(UID, newForm);
        }
    }
}
