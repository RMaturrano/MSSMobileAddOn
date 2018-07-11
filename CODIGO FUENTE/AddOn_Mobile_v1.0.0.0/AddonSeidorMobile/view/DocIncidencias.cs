using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.dao;
using AddonSeidorMobile.entity;

namespace AddonSeidorMobile.view
{
    public class DocIncidencias: FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;
        private SAPbouiCOM.Grid mGrid;

        private const string GRID_PRINCIPAL = "grdPrin";
        private const string BTN_REFRESCAR = "btnUpd";

        public DocIncidencias(Dictionary<string, IForm> dictionary)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmIncidencia, FormName.DOC_INCIDENCIAS);
            if (mForm != null)
            {
                dictionary.Add(getFormUID(), this);
                mForm.Visible = true;

                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.DOC_INCIDENCIAS + " revise el log del sistema.");
        }

        public void instanciarObjetosUI()
        {
            try
            {
                mGrid = mForm.Items.Item(GRID_PRINCIPAL).Specific;
            }
            catch (Exception ex)
            {
                StatusMessageError("instanciarObjetosUI() > " + ex.Message);
            }
        }

        public void iniciarValoresPorDefecto()
        {
            EmpresaBean empresa = EmpresaDAO.obtenerEmpresa(Conexion.company.CompanyDB);
            mGrid.DataTable.ExecuteQuery(ActividadDAO.getQForListBDM(empresa.id, empresa.base_datos));
            ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(2)).LinkedObjectType = Constantes.OBJ_TYPE_SOCIOS_NEGOCIO;
            ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(8)).LinkedObjectType = Constantes.OBJ_TYPE_EMPLEADO_VENTAS;
            ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(15)).LinkedObjectType = Constantes.OBJ_TYPE_FACTURA;
            ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(21)).LinkedObjectType = Constantes.OBJ_TYPE_ACTIVIDAD;
            mGrid.Columns.Item(14).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
            mGrid.Columns.Item(20).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
            mGrid.Columns.Item(0).TitleObject.Sortable = true;
            mGrid.Columns.Item(1).TitleObject.Sortable = true;
            mGrid.Columns.Item(2).TitleObject.Sortable = true;
            mGrid.Columns.Item(3).TitleObject.Sortable = true;
            mGrid.Columns.Item(8).TitleObject.Sortable = true;
            mGrid.Columns.Item(9).TitleObject.Sortable = true;
            mGrid.Columns.Item(12).TitleObject.Sortable = true;
            mGrid.Columns.Item(13).TitleObject.Sortable = true;
            mGrid.AutoResizeColumns();
        }

        public bool HandleItemEvents(SAPbouiCOM.ItemEvent itemEvent)
        {
            var res = true;

            try
            {
                switch (itemEvent.ItemUID)
                {
                    case BTN_REFRESCAR:
                        if (itemEvent.BeforeAction)
                            iniciarValoresPorDefecto();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                res = false;
                StatusMessageError("HandleItemEvents() > " + e.Message);
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

        public string getFormUID()
        {
            return mForm.UniqueID;
        }
    }
}
