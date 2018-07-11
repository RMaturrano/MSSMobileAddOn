using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.dao;
using AddonSeidorMobile.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.view
{
    public class MaestroCliente: FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;
        private SAPbouiCOM.Grid mGrid;

        private const string GRID_PRINCIPAL = "grdPrin";
        private const string BTN_REFRESCAR = "btnUpd";

        public MaestroCliente(Dictionary<string, IForm> dictionary)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmClientes, FormName.MAESTRO_CLIENTES);
            if (mForm != null)
            {
                dictionary.Add(getFormUID(), this);
                mForm.Visible = true;

                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.MAESTRO_CLIENTES + " revise el log del sistema.");
        }

        public void instanciarObjetosUI()
        {
            try
            {
                mGrid = mForm.Items.Item(GRID_PRINCIPAL).Specific;
            }
            catch (Exception e)
            {
                StatusMessageError("instanciarObjetosUI() > " + e.Message);
            }
        }

        public void iniciarValoresPorDefecto()
        {
            EmpresaBean empresa = EmpresaDAO.obtenerEmpresa(Conexion.company.CompanyDB);
            mGrid.DataTable.ExecuteQuery(ClienteDAO.getQForListBDM(empresa.id, empresa.base_datos));

            mGrid.Columns.Item(1).Type = SAPbouiCOM.BoGridColumnType.gct_ComboBox;
            ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(1)).ValidValues.Add("TPJ", "Persona jurídica");
            ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(1)).ValidValues.Add("TPN", "Persona natural");
            ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(1)).DisplayType = SAPbouiCOM.BoComboDisplayType.cdt_Description;

            mGrid.Columns.Item(2).Type = SAPbouiCOM.BoGridColumnType.gct_ComboBox;
            ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(2)).ValidValues.Add("0", "Otros tipos de documento");
            ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(2)).ValidValues.Add("1", "Documento nacional de identidad");
            ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(2)).ValidValues.Add("4", "Carnet de extranjería");
            ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(2)).ValidValues.Add("6", "Registro único de contribuyentes");
            ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(2)).ValidValues.Add("7", "Pasaporte");
            ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(2)).DisplayType = SAPbouiCOM.BoComboDisplayType.cdt_Description;

            mGrid.Columns.Item(18).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
            mGrid.Columns.Item(19).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
            mGrid.Columns.Item(0).TitleObject.Sortable = true;
            mGrid.Columns.Item(3).TitleObject.Sortable = true;
            mGrid.Columns.Item(4).TitleObject.Sortable = true;
            mGrid.Columns.Item(5).TitleObject.Sortable = true;
            mGrid.Columns.Item(6).TitleObject.Sortable = true;
            mGrid.Columns.Item(7).TitleObject.Sortable = true;
            mGrid.Columns.Item(8).TitleObject.Sortable = true;
            mGrid.Columns.Item(9).TitleObject.Sortable = true;
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
