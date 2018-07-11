using AddonSeidorMobile.data_schema.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.dao
{
    public class PagoDAO
    {
        public static string getQForListBDM(int idEmp, string database)
        {
            string qery = "  select   " +
                              "  \"ClaveMovil\"       as \"Clave móvil\" , " +
                              //"    \"TransaccionMovil\"       as \"\" , " +
                              "    \"SocioNegocio\"       as \"Código cliente\" , " +
                              "    T0.\"CardName\"       as \"Nombre cliente\" , " +
                              "    \"EmpleadoVenta\"       as \"Empleado\" , " +
                              "    T1.\"SlpName\"       as \"Nombre empleado\" , " +
                              "    \"Comentario\"       as \"Comentario\" , " +
                              "    \"Glosa\"     as \"Comentario diario\" , " +
                              "    TO_VARCHAR(\"FechaContable\",'YYYY/MM/DD')      as \"Fecha contable\",  " +
                              "   CASE \"TipoPago\" WHEN 'C' THEN 'Cheque' " +
                                    " WHEN 'T' THEN 'Transferencia/Depósito' ELSE 'Efectivo' END      as \"Tipo pago\",  " +
                              "    \"Moneda\"      as \"Moneda\" , " +
                              "    \"ChequeCuenta\"      as \"Cheque cuenta\" , " +
                              "    \"ChequeBanco\"      as \"Cheque banco\" , " +
                              "    TO_VARCHAR(\"ChequeVencimiento\",'YYYY/MM/DD')     as \"Cheque vcto.\" , " +
                              "    \"ChequeImporte\"       as \"Cheque importe\" , " +
                              "    \"ChequeNumero\"       as \"Cheque número\",  " +
                              "    \"TransferenciaCuenta\"       as \"Transf. cuenta\",  " +
                              "    \"TransferenciaReferencia\"      as \"Transf. referencia\",  " +
                              "    \"TransferenciaImporte\"        as \"Transf. importe\" , " +
                              "    \"EfectivoCuenta\"       as \"Efectivo cuenta\" , " +
                              "    \"EfectivoImporte\"      as \"Efectivo importe\" , " +
                              "    \"Migrado\"       as \"¿Migrado?\" , " +
                              //"    \"Actualizado\"       as \"Actualizado\",  " +
                              //"    \"Finalizado\"       as \"\" , " +
                              "    X0.\"DocEntry\"       as \"Código SAP\" , " +
                              "    \"Mensaje\"       as \"Mensaje\"  " +
                              //"    \"EMPRESA\"       as \"\"  " +
                            "  from   \"" + BdMobile.BD_NAME + "\".\"" + BdMobile.TB_PAGO_RECIBIDO + "\" X0 " + 
                            " LEFT JOIN " + database + ".OCRD T0 ON T0.\"CardCode\" = \"SocioNegocio\" " +
                            " LEFT JOIN " + database + ".OSLP T1 ON T1.\"SlpCode\" = \"EmpleadoVenta\" " +
                            " WHERE X0.\"EMPRESA\" = " + idEmp +
                            " ORDER BY X0.\"FechaContable\" DESC ";

            return qery;
        }
    }
}
