using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.dao;
using AddonSeidorMobile.entity;

namespace AddonSeidorMobile.view
{
    public class DocDevolucion: FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;
        private SAPbouiCOM.Grid mGrid;

        private const string GRID_PRINCIPAL = "grdPrin";
        private const string BTN_REFRESCAR = "btnUpd";

        public DocDevolucion(Dictionary<string, IForm> dictionary)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmDevolucion, FormName.DOC_DEVOLUCION);
            if (mForm != null)
            {
                dictionary.Add(getFormUID(), this);
                mForm.Visible = true;

                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.DOC_DEVOLUCION + " revise el log del sistema.");
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
            mGrid.DataTable.ExecuteQuery(DevolucionDAO.getQForListBDM(empresa.id, empresa.base_datos));
            ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(1)).LinkedObjectType = Constantes.OBJ_TYPE_ENTREGA;
            ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(2)).LinkedObjectType = Constantes.OBJ_TYPE_SOCIOS_NEGOCIO;
            ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(16)).LinkedObjectType = Constantes.OBJ_TYPE_DEVOLUCION;
            mGrid.Columns.Item(15).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
            mGrid.Columns.Item(0).TitleObject.Sortable = true;
            mGrid.Columns.Item(1).TitleObject.Sortable = true;
            mGrid.Columns.Item(2).TitleObject.Sortable = true;
            mGrid.Columns.Item(3).TitleObject.Sortable = true;
            mGrid.Columns.Item(7).TitleObject.Sortable = true;
            mGrid.Columns.Item(8).TitleObject.Sortable = true;
            mGrid.Columns.Item(10).TitleObject.Sortable = true;
            mGrid.Columns.Item(11).TitleObject.Sortable = true;
                mGrid.AutoResizeColumns();
        }

        public bool HandleItemEvents(SAPbouiCOM.ItemEvent itemEvent)
        {
            var res = true;

            try
            {
                    if (itemEvent.EventType == SAPbouiCOM.BoEventTypes.et_MATRIX_LINK_PRESSED &&
                        itemEvent.ItemUID.Equals(GRID_PRINCIPAL))
                    {
                        string messageColumn = mGrid.DataTable.GetValue("Mensaje", itemEvent.Row);

                        if (itemEvent.BeforeAction)
                        {
                            if (messageColumn != null && !messageColumn.Trim().Equals(""))
                            {
                                if (messageColumn.Trim().ToUpper().Contains("BORRADOR"))
                                    ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(16)).LinkedObjectType = Constantes.OBJ_TYPE_DRAFTS;
                            }
                            else
                                res = false;

                        }
                        else
                            ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(16)).LinkedObjectType = Constantes.OBJ_TYPE_DEVOLUCION;
                    }
                    else {
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
