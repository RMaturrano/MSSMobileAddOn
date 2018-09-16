using System.Collections.Generic;

namespace AddonSeidorMobile.data_schema.tablas
{

    public class Almacen
    {
        private const string TABLA_CABE = "OWHS";

        #region _COLUMNAS

        public static List<CampoBean> getCamposTabla()
        {
            var myList = new List<CampoBean>();

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSS_DSC",
                descrp_campo = "% Descuento",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Float,
                subtipo_campo = SAPbobsCOM.BoFldSubTypes.st_Percentage,
            });

            return myList;
        }

        #endregion
    }
}
