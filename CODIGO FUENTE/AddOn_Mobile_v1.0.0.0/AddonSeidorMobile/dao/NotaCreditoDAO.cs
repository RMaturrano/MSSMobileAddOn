using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.data_schema.database;

namespace AddonSeidorMobile.dao
{
    public class NotaCreditoDAO
    {
        public static string getQForListBDM(int idEmp, string database)
        {
            string qery = " select  " +
                       " \"CLAVEMOVIL\" as \"Clave móvil\",   " +
                       " \"CLAVEBASE\" as \"Clave Base\",   " +
                       " \"SOCIONEGOCIO\"   as \"Código cliente\",   " +
                       " T0.\"CardName\"   as \"Nombre cliente\",   " +
                       " T1.\"PymntGroup\"   as \"Condición de pago\",   " +
                       " T2.\"Name\"   as \"Indicador\",   " +
                       " \"REFERENCIA\"   as \"Referencia\",   " +
                       " TO_VARCHAR(\"FECHACONTABLE\",'YYYY/MM/DD')   as \"Fecha contable\",   " +
                       " TO_VARCHAR(\"FECHAVENCIMIENTO\",'YYYY/MM/DD')  as \"Fecha vencimiento\",   " +
                       " \"MONEDA\"  as \"Moneda\",   " +
                       " \"EMPLEADOVENTA\"  as \"Empleado\",   " +
                       " T3.\"SlpName\"  as \"Nombre empleado\",   " +
                       " \"DIRECCIONFISCAL\"   as \"Dirección fiscal\",   " +
                       " \"DIRECCIONENTREGA\"  as \"Dirección entrega\",   " +
                       " \"COMENTARIO\"  as \"Comentario\",   " +
                       " \"MIGRADO\"  as \"¿Migrado?\",   " +
                       " X0.\"DOCENTRY\"  as \"Código SAP\",   " +
                       " \"MENSAJE\"  as \"Mensaje\"   " +
                  "  from \"" + BdMobile.BD_NAME + "\".\"" + BdMobile.TB_NOTA_CREDITO + "\" X0 " +
                  " LEFT JOIN " + database + ".OCRD T0 ON T0.\"CardCode\" = \"SOCIONEGOCIO\" " +
                  " LEFT JOIN " + database + ".OCTG T1 ON T1.\"GroupNum\" = \"CONDICIONPAGO\" " +
                  " LEFT JOIN " + database + ".OIDC T2 ON T2.\"Code\" = \"INDICADOR\" " +
                  " LEFT JOIN " + database + ".OSLP T3 ON T3.\"SlpCode\" = \"EMPLEADOVENTA\" " +
                  " WHERE X0.\"EMPRESA\" = " + idEmp +
                  " ORDER BY X0.\"FECHACONTABLE\" DESC ";

            return qery;
        }
    }
}
