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
    public class LogRegistros: FormCommon, IForm
    {
        private SAPbouiCOM.Form mForm;
        private SAPbouiCOM.Grid mGrid;

        private const string GRID_PRINCIPAL = "grdPrin";

        public LogRegistros(Dictionary<string, IForm> dictionary)
        {
            mForm = createForm(Conexion.company, Conexion.application, AddonSeidorMobile.Properties.Resources.frmLog, FormName.LOG_REGISTROS);
            if (mForm != null)
            {
                dictionary.Add(getFormUID(), this);
                mForm.Visible = true;

                instanciarObjetosUI();
                iniciarValoresPorDefecto();
            }
            else
                StatusMessageError("Constructor() > No se pudo crear el formulario " + FormName.LOG_REGISTROS + " revise el log del sistema.");
        }

        public void instanciarObjetosUI()
        {
            try
            {
                mGrid = mForm.Items.Item(GRID_PRINCIPAL).Specific;
                EmpresaBean empresa = EmpresaDAO.obtenerEmpresa(Conexion.company.CompanyDB);
                mGrid.DataTable.ExecuteQuery(LogDAO.getQForListBDM(empresa.id, empresa.base_datos));
                ((SAPbouiCOM.EditTextColumn)mGrid.Columns.Item(1)).LinkedObjectType = Constantes.OBJ_TYPE_EMPLEADO_VENTAS;

                mGrid.Columns.Item(4).Type = SAPbouiCOM.BoGridColumnType.gct_ComboBox;
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(4)).ValidValues.Add("01", "ERROR");
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(4)).ValidValues.Add("02", "CORRECTO");
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(4)).DisplayType = SAPbouiCOM.BoComboDisplayType.cdt_Description;

                mGrid.Columns.Item(7).Type = SAPbouiCOM.BoGridColumnType.gct_ComboBox;
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(7)).ValidValues.Add("01", "APP MOBILE > BASE INTERMEDIA");
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(7)).ValidValues.Add("02", "BASE INTERMEDIA > SAP");
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(7)).DisplayType = SAPbouiCOM.BoComboDisplayType.cdt_Description;

                mGrid.Columns.Item(8).Type = SAPbouiCOM.BoGridColumnType.gct_ComboBox;
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(8)).ValidValues.Add("00", "SOCIO DE NEGOCIO");
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(8)).ValidValues.Add("01", "ORDEN DE VENTA");
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(8)).ValidValues.Add("02", "PAGO RECIBIDO");
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(8)).ValidValues.Add("03", "INCIDENCIA");
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(8)).ValidValues.Add("04", "NOTA DE CREDITO");
                ((SAPbouiCOM.ComboBoxColumn)mGrid.Columns.Item(8)).DisplayType = SAPbouiCOM.BoComboDisplayType.cdt_Description;


                mGrid.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                StatusMessageError("instanciarObjetosUI() > " + ex.Message);
            }
        }

        public void iniciarValoresPorDefecto()
        {
            
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
