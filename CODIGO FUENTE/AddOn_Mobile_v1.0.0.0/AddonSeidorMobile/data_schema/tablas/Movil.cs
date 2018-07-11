using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.data_schema.tablas
{
    public class Movil
    {
        #region _TABLA

        public static TablaBean getTabla()
        {
            return new TablaBean()
            {
                nombre = "MSSM_EQP",
                descripcion = "MAESTRO DE EQUIPOS",
                tipo = SAPbobsCOM.BoUTBTableType.bott_MasterData
            };
        }

        #endregion

        #region _COLUMNAS

        public static List<CampoBean> getCamposTabla()
        {
            var myList = new List<CampoBean>();

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_MOD",
                descrp_campo = "Modelo",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 100
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_SER",
                descrp_campo = "Serie",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 30
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_COL",
                descrp_campo = "Color",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 30
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_IDU",
                descrp_campo = "Código ID único",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 30
            });

            //myList.Add(new CampoBean()
            //{
            //    nombre_tabla = getTabla().nombre,
            //    nombre_campo = "MSSM_TIP",
            //    descrp_campo = "Tipo usuario",
            //    tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
            //    tamano = 2,
            //    validValues = new string [] {"01","02","03"},
            //    validDescription = new string [] {"Supervisor","Vendedor","Ninguno"},
            //    valorPorDef = "03",
            //    obligatorio = SAPbobsCOM.BoYesNoEnum.tYES
            //});

            //myList.Add(new CampoBean()
            //{
            //    nombre_tabla = getTabla().nombre,
            //    nombre_campo = "MSSM_USR",
            //    descrp_campo = "Usuario móvil",
            //    tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
            //    tamano = 10,
            //    obligatorio = SAPbobsCOM.BoYesNoEnum.tYES
            //});

            //myList.Add(new CampoBean()
            //{
            //    nombre_tabla = getTabla().nombre,
            //    nombre_campo = "MSSM_CLV",
            //    descrp_campo = "Clave móvil",
            //    tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
            //    tamano = 8,
            //    obligatorio = SAPbobsCOM.BoYesNoEnum.tYES
            //});

            //myList.Add(new CampoBean()
            //{
            //    nombre_tabla = getTabla().nombre,
            //    nombre_campo = "MSSM_ADD",
            //    descrp_campo = "Crear",
            //    tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
            //    tamano = 1,
            //    validValues = new string [] {"Y","N"},
            //    validDescription = new string [] {"SI", "NO"},
            //    valorPorDef = "N"
            //});

            //myList.Add(new CampoBean()
            //{
            //    nombre_tabla = getTabla().nombre,
            //    nombre_campo = "MSSM_EDT",
            //    descrp_campo = "Editar",
            //    tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
            //    tamano = 1,
            //    validValues = new string[] { "Y", "N" },
            //    validDescription = new string[] { "SI", "NO" },
            //    valorPorDef = "N"
            //});

            //myList.Add(new CampoBean()
            //{
            //    nombre_tabla = getTabla().nombre,
            //    nombre_campo = "MSSM_AUT",
            //    descrp_campo = "Aprobar",
            //    tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
            //    tamano = 1,
            //    validValues = new string[] { "Y", "N" },
            //    validDescription = new string[] { "SI", "NO" },
            //    valorPorDef = "N"
            //});

            //myList.Add(new CampoBean()
            //{
            //    nombre_tabla = getTabla().nombre,
            //    nombre_campo = "MSSM_DEN",
            //    descrp_campo = "Rechazar",
            //    tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
            //    tamano = 1,
            //    validValues = new string[] { "Y", "N" },
            //    validDescription = new string[] { "SI", "NO" },
            //    valorPorDef = "N"
            //});

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_VAL",
                descrp_campo = "Verificar ID",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            //myList.Add(new CampoBean()
            //{
            //    nombre_tabla = getTabla().nombre,
            //    nombre_campo = "MSSM_SEL",
            //    descrp_campo = "Escoger Precio",
            //    tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
            //    tamano = 1,
            //    validValues = new string[] { "Y", "N" },
            //    validDescription = new string[] { "SI", "NO" },
            //    valorPorDef = "Y"
            //});

            return myList;
        }

        #endregion

        #region _OBJETO

        public static ObjetoBean getObjeto()
        {
            var myObj = new ObjetoBean();
            myObj.code = getTabla().nombre;
            myObj.name = "EQUIPO_MOVIL";
            myObj.tableName = getTabla().nombre;
            myObj.canCancel = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.canClose = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.canDelete = SAPbobsCOM.BoYesNoEnum.tYES;
            myObj.canCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO;
            myObj.canFind = SAPbobsCOM.BoYesNoEnum.tYES;
            myObj.findColumns = new string[] { "U_MSSM_MOD" };
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
