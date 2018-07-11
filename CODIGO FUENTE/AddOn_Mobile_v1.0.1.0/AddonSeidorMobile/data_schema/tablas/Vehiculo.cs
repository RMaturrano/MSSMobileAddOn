using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.data_schema.tablas
{
    public class Vehiculo
    {
        #region _TABLA
        public static TablaBean getTabla()
        {
            return new TablaBean()
            {
                nombre = "MSS_VEHI",
                descripcion = "VEHICULO",
                tipo = SAPbobsCOM.BoUTBTableType.bott_MasterData
            };
        }
        #endregion

        #region _OBJETO
        public static ObjetoBean getObjeto()
        {
            var myObj = new ObjetoBean();
            myObj.code = getTabla().nombre;
            myObj.name = "VEHICULO";
            myObj.tableName = getTabla().nombre;
            myObj.canCancel = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.canClose = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.canDelete = SAPbobsCOM.BoYesNoEnum.tYES;
            myObj.canCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tYES;
            myObj.canFind = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.canLog = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.objectType = SAPbobsCOM.BoUDOObjType.boud_MasterData;
            myObj.manageSeries = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.enableEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.rebuildEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;
            return myObj;
        }
        #endregion

    }
}
