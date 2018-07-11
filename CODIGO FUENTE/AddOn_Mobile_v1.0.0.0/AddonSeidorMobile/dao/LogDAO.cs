using AddonSeidorMobile.data_schema.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.dao
{
    public class LogDAO
    {
        public static string getQForListBDM(int idEmp, string database)
        {
            string qry = " select " +
                        " T0.\"ID\" as \"#\", " +
                        //" T0.\"EMPRESAID\" as \"Cód. Empresa\", " +
                        //" T1.\"descripcion\" as \"Nombre\", " +
                        " T0.\"USUARIOID\" as \"Empleado\", " +
                        " T1.\"SlpName\" as \"Nombre empleado\", " +
                        " T0.\"CLAVEDOC\" as \"Clave móvil documento\", " +
                        " T0.\"TIPODOC\" as \"Tipo\", " +
                        " T0.\"FECHAREGISTRO\" as \"Fecha registro\", " +
                        " T0.\"MESSAGE\" as \"Mensaje\", " +
                        " T0.\"SOURCE\" as \"Origen\", " +
                        " T0.\"TIPO\" as \"Documento\" " +
                   "from \"" + BdMobile.BD_NAME + "\".\"" + BdMobile.TB_LOG_REGISTROS + "\" T0  " +
                   " LEFT JOIN " + database + ".OSLP T1 ON T1.\"SlpCode\" = \"USUARIOID\" " +
                   "  where T0.\"EMPRESAID\" = " + idEmp;

            return qry;
        }

    }
}
