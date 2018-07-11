using AddonSeidorMobile.conexion;
using AddonSeidorMobile.data_schema.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.dao
{
    public class ClienteDAO
    {
        public static string getQForListBDM(int idEmpresa, string basedatos)
        {
            return "select \"ClaveMovil\" as \"Clave Movil\",     " +
                      "      \"TipoPersona\"   as \"Tipo persona\" ,   " +
                      "      \"TipoDocumento\"   as \"Tipo de documento\" ,   " +
                      "      \"NumeroDocumento\"   as \"Nro documento\" ,   " +
                      "      \"NombreRazonSocial\"  as \"Nombre o razón social\" ,   " +
                      "      \"NombreComercial\"   as \"Nombre comercial\"  ,  " +
                      "      \"ApellidoPaterno\"   as \"Apelido paterno\"  ,  " +
                      "      \"ApellidoMaterno\"   as \"Apellido materno\"  ,  " +
                      "      \"PrimerNombre\"   as \"Primer nombre\"  ,  " +
                      "      \"SegundoNombre\"   as \"Segundo nombre\"  ,  " +
                      "      \"Telefono1\"   as \"Tlf. 1\" ,   " +
                      "      \"Telefono2\"   as \"Tlf. 2\" ,   " +
                      "      \"TelefonoMovil\"   as \"Tlf. móvil\" ,   " +
                      "      \"CorreoElectronico\"  as \"Correo\" ,   " +
                      //"      \"GrupoSocio\"   as \"Grupo\"  ,  " +
                      "      T0.\"GroupName\"   as \"Grupo\"  ,  " +
                      //"      \"ListaPrecio\"   as \"Lista de precio\"  ,  " +
                      "      T1.\"ListName\"   as \"Lista de precio\"  ,  " +
                      //"      \"CondicionPago\"   as \"Condición de pago\" ,   " +
                      "      T2.\"PymntGroup\"   as \"Condición de pago\" ,   " +
                      //"      \"Indicador\"   as \"Indicador\"  ,  " +
                      "      T3.\"Name\"   as \"Indicador\"  ,  " +
                      //"      \"Zona\"   as \"Zona\"  ,  " +
                      "      IFNULL(\"Migrado\",'N')   as \"¿Migrado?\"  ,  " +
                      //"      \"Actualizado\"   as \"\" ,   " +
                      //"      \"Finalizado\"   as \"\" ,   " +
                      //"      \"TransaccionMovil\"  as \"Estado\" ,   " +
                      "      IFNULL(\"POSEEACTIVOS\",'N')  as \"¿Posee Activos?\",   " +
                      "      \"VENDEDOR\"  as \"Vendedor\",   " +
                      "      \"MENSAJE\"  as \"Mensaje validación\" ,  " +
                      "      \"CARDCODE\"  as \"Código SAP\"   " +
                     " FROM \"" + BdMobile.BD_NAME + "\".\"" + BdMobile.TB_SOCIO_NEGOCIO + "\"  " +
                     " LEFT JOIN " + basedatos + ".OCRG T0 ON T0.\"GroupCode\" = \"GrupoSocio\" " +
                     " LEFT JOIN " + basedatos + ".OPLN T1 ON T1.\"ListNum\" = \"ListaPrecio\" " +
                     " LEFT JOIN " + basedatos + ".OCTG T2 ON T2.\"GroupNum\" = \"CondicionPago\" " +
                     " LEFT JOIN " + basedatos + ".OIDC T3 ON T3.\"Code\" = \"Indicador\" " +
                     " WHERE EMPRESA = " + idEmpresa;
        
        }                  
    }                      
}                          
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           
                           