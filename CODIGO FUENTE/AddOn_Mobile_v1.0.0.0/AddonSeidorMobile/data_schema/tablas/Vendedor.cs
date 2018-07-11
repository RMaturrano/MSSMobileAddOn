using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.data_schema.tablas
{
    public class Vendedor
    {
        #region _TABLA

        public static TablaBean getTabla()
        {
            return new TablaBean()
            {
                nombre = "MSSM_CVE",
                descripcion = "CONFIG. VENDEDOR",
                tipo = SAPbobsCOM.BoUTBTableType.bott_MasterData
            };
        }

        public static TablaBean getTablaDet1()
        {
            return new TablaBean()
            {
                nombre = "MSSM_CV1",
                descripcion = "CONFIG. VENDEDOR ALMACEN",
                tipo = SAPbobsCOM.BoUTBTableType.bott_MasterDataLines
            };
        }

        /*
                public static TablaBean getTablaDet2()
                {
                    return new TablaBean()
                    {
                        nombre = "MSSM_CV2",
                        descripcion = "CONFIG. VENDEDOR GRPCLI_DET2",
                        tipo = SAPbobsCOM.BoUTBTableType.bott_MasterDataLines
                    };
                }

                public static TablaBean getTablaDet3()
                {
                    return new TablaBean()
                    {
                        nombre = "MSSM_CV3",
                        descripcion = "CONFIG. VENDEDOR ZONAS_DET3",
                        tipo = SAPbobsCOM.BoUTBTableType.bott_MasterDataLines
                    };
                }
        */
        #endregion

        #region _COLUMNAS

        public static List<CampoBean> getCamposCabe()
        {
            var myList = new List<CampoBean>();

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_PER",
                descrp_campo = "Perfil",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 50
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_USR",
                descrp_campo = "Usuario móvil",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 50
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_PWD",
                descrp_campo = "Password móvil",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Numeric,
                tamano = 10
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_EQP",
                descrp_campo = "Equipo asignado",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 50
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_TGA",
                descrp_campo = "Todos los grupos artículo",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_TGC",
                descrp_campo = "Todos los grupos cliente",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_TZO",
                descrp_campo = "Todas las zonas",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_ALM",
                descrp_campo = "Almacen",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 8
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_PRJ",
                descrp_campo = "Proyecto",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 50
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_SRI",
                descrp_campo = "Serie",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 50
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTabla().nombre,
                nombre_campo = "MSSM_VEH",
                descrp_campo = "Vehiculo",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 50
            });

            return myList;
        }

        public static List<CampoBean> getCamposDet1()
        {
            var myList = new List<CampoBean>();

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTablaDet1().nombre,
                nombre_campo = "MSSM_COD",
                descrp_campo = "Código Almacén",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 20
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = getTablaDet1().nombre,
                nombre_campo = "MSSM_NOM",
                descrp_campo = "Descripción",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 100
            });

            return myList;
        }

        //public static List<CampoBean> getCamposDet3()
        //{
        //    var myList = new List<CampoBean>();

        //    myList.Add(new CampoBean()
        //    {
        //        nombre_tabla = getTablaDet3().nombre,
        //        nombre_campo = "MSSM_COD",
        //        descrp_campo = "Código grupo",
        //        tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
        //        tamano = 10
        //    });

        //    myList.Add(new CampoBean()
        //    {
        //        nombre_tabla = getTablaDet3().nombre,
        //        nombre_campo = "MSSM_NOM",
        //        descrp_campo = "Descripción",
        //        tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
        //        tamano = 50
        //    });

        //    return myList;
        //}

        #endregion

        #region _OBJETO

        public static ObjetoBean getObjeto()
        {
            var myObj = new ObjetoBean();
            myObj.code = getTabla().nombre;
            myObj.name = "CONFIG_X_VENDEDOR";
            myObj.tableName = getTabla().nombre;
            myObj.childTables = new string[] { getTablaDet1().nombre };
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
