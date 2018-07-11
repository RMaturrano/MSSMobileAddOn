using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.data_schema.tablas
{
    public class PagosRecibidos
    {
        private const string TABLA_CABE = "ORCT";

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

            return myList;
        }

        #endregion
    }
}
