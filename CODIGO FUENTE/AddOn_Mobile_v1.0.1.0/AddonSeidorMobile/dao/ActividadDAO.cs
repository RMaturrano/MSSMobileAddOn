using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.data_schema.database;

namespace AddonSeidorMobile.dao
{
    public class ActividadDAO
    {
        public static string getQForListBDM(int idEmpresa, string basedatos)
        {
            string qery = " select     " +
                      "  \"CLAVEMOVIL\"     as \"Clave móvil\",  " +
                      "  \"ORIGEN\"         as \"Origen incidencia\",  " +
                      "  \"CODIGOCLIENTE\"      as \"Código cliente\",  " +
                      "  T0.\"CardName\"      as \"Nombre cliente\",  " +
                      "  \"CODIGOCONTACTO\"     as \"Código contacto\",  " +
                      "  \"CODIGODIRECCION\"    as \"Código dirección\",  " +
                      "  T2.\"DESCRIPCION\"       as \"Motivo\",  " +
                      "  \"COMENTARIOS\"        as \"Comentarios\",  " +
                      "  \"VENDEDOR\"           as \"Código vendedor\",  " +
                      "  T1.\"SlpName\"           as \"Nombre vendedor\",  " +
                      "  \"LATITUD\"            as \"Latitud\",  " +
                      "  \"LONGITUD\"           as \"Longitud\",  " +
                      "  TO_VARCHAR(\"FECHACREACION\",'YYYY/MM/DD')      as \"Fecha creación\",  " +
                      "  \"HORACREACION\"       as \"Hora creación\",  " +
                      "  \"MODOOFFLINE\"        as \"¿Modo OffLine?\",  " +
                      "  \"CLAVEFACTURA\"       as \"Código factura\",  " +
                      "  \"SERIEFACTURA\"       as \"Serie\",  " +
                      "  \"CORRELATIVOFACTURA\" as \"Correlativo\",  " +
                      "  \"TIPOINCIDENCIA\"     as \"Tipo de incidencia\",  " +
                      "  TO_VARCHAR(\"FECHAPAGO\",'YYYY/MM/DD')          as \"Fecha de pago\",  " +
                      "  \"MIGRADO\"            as \"¿Migrado?\",  " +
                      //"  \"Actualizado\"        as \"\",  " +
                      //"  \"Finalizado\"         as \"\",  " +
                      "  \"CODIGOSAP\"          as \"Código SAP\",  " +
                      "  \"MENSAJE\"            as \"Mensaje\"  " +
                //"  \"EMPRESA\"            as \"\"  " +
                "  from \"" + BdMobile.BD_NAME + "\".\"" + BdMobile.TB_ACTIVIDADES + "\" X0 " +
                  " LEFT JOIN " + basedatos + ".OCRD T0 ON T0.\"CardCode\" = \"CODIGOCLIENTE\" " +
                  " LEFT JOIN " + basedatos + ".OSLP T1 ON T1.\"SlpCode\" = \"VENDEDOR\" " +
                  " LEFT JOIN " + BdMobile.BD_NAME + "." + BdMobile.TB_MOTIVOS_INCIDENCIAS + "  T2 " +
                  " ON \"CODIGOMOTIVO\" = T2.\"ID\" " +
                  " WHERE X0.\"EMPRESA\" = " + idEmpresa;

            return qery;
        }
    }
}
