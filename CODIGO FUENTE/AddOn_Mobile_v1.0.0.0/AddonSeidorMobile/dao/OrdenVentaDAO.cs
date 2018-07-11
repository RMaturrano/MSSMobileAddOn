using AddonSeidorMobile.data_schema.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.dao
{
    public class OrdenVentaDAO
    {
        public static string getQForListBDM(int idEmp, string database)
        {
            string qery = " select  " +
                       " \"ClaveMovil\" as \"Clave móvil\",   " +
                       //" \"TransaccionMovil\"  as \"\",   " +
                       " \"SocioNegocio\"   as \"Código cliente\",   " +
                       " T0.\"CardName\"   as \"Nombre cliente\",   " +
                       //" \"ListaPrecio\"   as \"\",   " +
                       //" \"CondicionPago\"   as \"Condición de pago\",   " +
                       " T1.\"PymntGroup\"   as \"Condición de pago\",   " +
                       //" \"Indicador\"   as \"Indicador\",   " +
                       " T2.\"Name\"   as \"Indicador\",   " +
                       " \"Referencia\"   as \"Referencia\",   " +
                       " TO_VARCHAR(\"FechaContable\",'YYYY/MM/DD')   as \"Fecha contable\",   " +
                       " TO_VARCHAR(\"FechaVencimiento\",'YYYY/MM/DD')  as \"Fecha vencimiento\",   " +
                       " \"Moneda\"  as \"Moneda\",   " +
                       " \"EmpleadoVenta\"  as \"Empleado\",   " +
                       " T3.\"SlpName\"  as \"Nombre empleado\",   " +
                       " \"DireccionFiscal\"   as \"Dirección fiscal\",   " +
                       " \"DireccionEntrega\"  as \"Dirección entrega\",   " +
                       " \"Comentario\"  as \"Comentario\",   " +
                       " \"Migrado\"  as \"¿Migrado?\",   " +
                       //" \"Actualizado\"  as \"\",   " +
                       //" \"Finalizado\"  as \"\",   " +
                       " X0.\"DocEntry\"  as \"Código SAP\",   " +
                       " \"Mensaje\"  as \"Mensaje\"   " +
                       //" \"EMPRESA\"  as \"\"  " +
                  "  from \"" + BdMobile.BD_NAME + "\".\"" + BdMobile.TB_ORDEN_VENTA + "\" X0 " +
                  " LEFT JOIN " + database + ".OCRD T0 ON T0.\"CardCode\" = \"SocioNegocio\" " +
                  " LEFT JOIN " + database + ".OCTG T1 ON T1.\"GroupNum\" = \"CondicionPago\" " +
                  " LEFT JOIN " + database + ".OIDC T2 ON T2.\"Code\" = \"Indicador\" " +
                  " LEFT JOIN " + database + ".OSLP T3 ON T3.\"SlpCode\" = \"EmpleadoVenta\" " +
                  " WHERE X0.\"EMPRESA\" = " + idEmp  + 
                  " ORDER BY X0.\"FechaContable\" DESC ";

            return qery;
        }

        public static string getQForListDetailBDM(string claveMovil, string database)
        {
            string query = "select  " +
                           "     T0.\"Articulo\" as \"Artículo\", " +
                           "     T1.\"ItemName\" as \"Descripción\", " +
                           "     T3.\"UomCode\" as \"Unidad de medida\", " +
                           "     T0.\"Almacen\" as \"Almacén\", " +
                           "     T0.\"Cantidad\", " +
                           "     T2.\"ListName\" as \"Lista de precio\", " +
                           "     T0.\"PrecioUnitario\" as \"Precio unitario\", " +
                           "     T0.\"PorcentajeDescuento\" as \" % descuento\", " +
                           "     T0.\"Impuesto\" " +
                           " from \"" + BdMobile.BD_NAME + "\".\"" + BdMobile.TB_ORDEN_VENTA_DETALLE + "\" T0 JOIN " + database + ".\"OITM\" T1 " +
                           "     ON T0.\"Articulo\" = T1.\"ItemCode\" JOIN " + database + ".\"OPLN\" T2 " +
                           "     ON T0.\"ListaPrecio\" = T2.\"ListNum\" JOIN " + database + ".\"OUOM\" T3 " +
                           "     ON T0.\"UnidadMedida\" = T3.\"UomEntry\" " +
                           " where T0.\"ClaveMovil\" = '" + claveMovil + "' " +
                           " order by \"Articulo\" ";
            return query;
        }

    }
}
