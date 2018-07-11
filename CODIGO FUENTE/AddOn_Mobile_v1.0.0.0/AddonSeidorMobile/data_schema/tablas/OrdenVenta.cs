using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.data_schema.tablas
{
    public class OrdenVenta
    {
        private const string TABLA_CABE = "ORDR";

        #region _COLUMNAS

        public static List<CampoBean> getCamposTabla()
        {
            var myList = new List<CampoBean>();

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_CRM",
                descrp_campo = "Creado móvil",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_CLM",
                descrp_campo = "Clave móvil",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 50
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_TRM",
                descrp_campo = "Transacción móvil",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 2,
                validValues = new string[] { "01", "02", "03", "04", "05" },
                validDescription = new string[] { "Ninguno", "Borrador creado", "Borrador actualizado",
                    "Borrador rechazado", "Transaccion creada" },
                valorPorDef = "01"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_MOL",
                descrp_campo = "Creado en modo OffLine",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_RAN",
                descrp_campo = "Rango dirección",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 2,
                validValues = new string[] { "01", "02", "03" },
                validDescription = new string[] { "Dentro del rango (20m)", "Fuera del rango", "No disponible" },
                valorPorDef = "03"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_LAT",
                descrp_campo = "Latitud",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 150
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_LON",
                descrp_campo = "Longitud",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 150
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_HOR",
                descrp_campo = "Hora de creación",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 20
            });

            return myList;
        }

        #endregion

    }
}
