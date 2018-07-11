using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.data_schema.tablas
{
    public class TipoUsuario
    {
        #region _TABLA
        public static TablaBean getTabla()
        {
            return new TablaBean()
            {
                nombre = "MSSM_MTU",
                descripcion = "MAESTRO TIPO USUARIO",
                tipo = SAPbobsCOM.BoUTBTableType.bott_MasterData
            };
        }
        #endregion

        public static List<CampoBean> getCamposTabla()
        {
            var myList = new List<CampoBean>();

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_HAB",
                descrp_campo = "Habilitado",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "Y"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_SUP",
                descrp_campo = "Supervisor",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_COB",
                descrp_campo = "Cobrador",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            return myList;
        }

        #region _OBJETO
        public static ObjetoBean getObjeto()
        {
            var myObj = new ObjetoBean();
            myObj.code = getTabla().nombre;
            myObj.name = "TIPO_USUARIO";
            myObj.tableName = getTabla().nombre;
            myObj.canCancel = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.canClose = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.canDelete = SAPbobsCOM.BoYesNoEnum.tYES;
            myObj.canCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.canFind = SAPbobsCOM.BoYesNoEnum.tYES;
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
