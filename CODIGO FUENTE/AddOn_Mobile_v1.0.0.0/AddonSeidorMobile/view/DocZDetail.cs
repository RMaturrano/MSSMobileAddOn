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
    public class DocZDetail : FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;
        private SAPbouiCOM.Grid mGrid;

        private const string GRID_PRINCIPAL = "grdPrin";
        private string claveDocumento;

        public DocZDetail(string claveMovil)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmDetail, FormName.DOC_DETALLES);
            if (mForm != null)
            {
                claveDocumento = claveMovil;
                mForm.Visible = true;

                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.DOC_DETALLES + " revise el log del sistema.");
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
            try
            {
                EmpresaBean empresa = EmpresaDAO.obtenerEmpresa(Conexion.company.CompanyDB);
                mGrid.DataTable.ExecuteQuery(OrdenVentaDAO.getQForListDetailBDM(claveDocumento, empresa.base_datos));
                ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(0)).LinkedObjectType = Constantes.OBJ_TYPE_ITEM;
                ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(3)).LinkedObjectType = Constantes.OBJ_TYPE_WAREHOUSE;
                ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(8)).LinkedObjectType = Constantes.OBJ_TYPE_TAXES;
                mGrid.Columns.Item(0).TitleObject.Sortable = true;
                mGrid.Columns.Item(1).TitleObject.Sortable = true;
                mGrid.Columns.Item(2).TitleObject.Sortable = true;
                mGrid.Columns.Item(3).TitleObject.Sortable = true;
                mGrid.Columns.Item(4).TitleObject.Sortable = true;
                mGrid.Columns.Item(5).TitleObject.Sortable = true;
                mGrid.Columns.Item(6).TitleObject.Sortable = true;
                mGrid.AutoResizeColumns();

                mForm.Title = "Detalles del documento " + claveDocumento;
            }
            catch (Exception ex)
            {
                StatusMessageError("iniciarValoresPorDefecto() > " + ex.Message);
            }
        }

        public bool HandleItemEvents(SAPbouiCOM.ItemEvent itemEvent)
        {
            return true;
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
