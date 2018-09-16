using System.Collections.Generic;

namespace AddonSeidorMobile.data_schema.tablas
{
    public class Articulo
    {
        private const string TABLA_CABE = "OITM";

        #region _COLUMNAS

        public static List<CampoBean> getCamposTabla()
        {
            var myList = new List<CampoBean>();
            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSS_MUES",
                descrp_campo = "Articulo de muestraS",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "S", "N" },
                validDescription = new string[] { "SI", "NO" },
                //valorPorDef = "N"
            });

            return myList;
        }

        #endregion
    }
}
