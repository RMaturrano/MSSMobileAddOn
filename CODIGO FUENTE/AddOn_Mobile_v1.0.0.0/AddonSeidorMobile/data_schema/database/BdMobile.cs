using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.data_schema.database
{
    public class BdMobile
    {
        public const string BD_NAME = "SBO_MSS_MOBILE";

        public const string TB_EMPRESAS = "EMPRESAS";
        public const string TB_MENUAPP = "MENU";
        public const string TB_PERMISOS = "PERMISOS";
        public const string TB_ORDEN_VENTA = "ORDR";
        public const string TB_ORDEN_VENTA_DETALLE = "RDR1";
        public const string TB_PAGO_RECIBIDO = "ORCT";
        public const string TB_PAGO_RECIBIDO_DETALLE = "RCT1";
        public const string TB_SOCIO_NEGOCIO = "OCRD";
        public const string TB_SOCIO_NEGOCIO_CONTACTOS = "CRD1";
        public const string TB_SOCIO_NEGOCIO_DIRECCIONES = "CRD2";
        public const string TB_MOTIVOS_INCIDENCIAS = "MOTIVOS";
        public const string TB_LOG_REGISTROS = "LOG_REGISTROS";
        public const string TB_ACTIVIDADES = "OCLG";
        public const string TB_DEVOLUCION = "ORDN";
        public const string TB_NOTA_CREDITO = "ORIN";


        //ALTER TABLE "SBO_MSS_MOBILE"."EMPRESAS" ADD ("EST_ORCT" VARCHAR(2) NULL)

        public static string getDataBaseSQL()
        {
            return "CREATE SCHEMA \""+BD_NAME+"\" OWNED BY system;";
        }

        public static List<string> getTablesSQL()
        {
            var res = new List<string>();
            res.Add(SQLTBEmpresas());
            res.Add(SQLTBMenu());
            res.Add(SQLTBPermisos());
            res.Add(SQLTBOrdenVenta());
            res.Add(SQLTBOrdenVentaDetalle());
            res.Add(SQLTBPagoRecibido());
            res.Add(SQLTBPagoRecibidoDetalle());
            res.Add(SQLTBSocioNegocio());
            res.Add(SQLTBSocioNegocioContacto());
            res.Add(SQLTBSocioNegocioDireccion());
            res.Add(SQLTBMotivosIncidencias());
            res.Add(SQLTBLogRegistros());
            res.AddRange(getInserts());

            return res;
        }

        private static string SQLTBEmpresas()
        {
            return "CREATE COLUMN TABLE \""+BD_NAME+"\".\""+TB_EMPRESAS+"\" (\"id\" INTEGER CS_INT NOT NULL , " +
	                " \"descripcion\" NVARCHAR(150) NOT NULL , " +
	                " \"base_datos\" NVARCHAR(150) NOT NULL ,  " +
	                " \"usuario\" NVARCHAR(150) NOT NULL , " +
	                " \"clave\" NVARCHAR(150) NOT NULL , " +
	                " \"estado\" CHAR(1) CS_FIXEDSTRING NOT NULL , " +
                    " \"LINEAS_ORDR\" SMALLINT NOT NULL , " +
                    " \"EST_ORDR\" CHAR(2) NOT NULL , " +
                    " \"EST_ORCT\" CHAR(2) NOT NULL , " +
	                " \"observacion\" NVARCHAR(200) NOT NULL )";
        }

        private static string SQLTBMenu()
        {
            return "CREATE COLUMN TABLE \""+BD_NAME+"\".\""+TB_MENUAPP+"\" (\"id\" INTEGER CS_INT NOT NULL , " +
	                " \"descripcion\" NVARCHAR(150) NOT NULL, " +
                    " \"CODIGO\" NVARCHAR(2) NOT NULL)";
        }

        private static string SQLTBPermisos()
        {
            return "CREATE COLUMN TABLE \""+BD_NAME+"\".\""+TB_PERMISOS+"\" (\"id\" INTEGER CS_INT NOT NULL , " +
										"	\"idEmpresa\" INTEGER CS_INT NOT NULL , " +
										"	\"idMenu\" INTEGER CS_INT NOT NULL , " +
										"	\"idPerfil\" NVARCHAR(50) NOT NULL , " +
						                "    \"accesa\" NVARCHAR(1) NOT NULL, " +
						                "    \"crea\" NVARCHAR(1) NOT NULL," +
						                "    \"edita\" NVARCHAR(1) NOT NULL," +
						                "    \"aprueba\" NVARCHAR(1) NOT NULL," +
						                "    \"rechaza\" NVARCHAR(1) NOT NULL," +
                                        "    \"escogePrecio\" NVARCHAR(1) NOT NULL) ";
        }

        private static string SQLTBOrdenVenta()
        {
            return "CREATE COLUMN TABLE \"" + BD_NAME + "\".\"" + TB_ORDEN_VENTA + "\"  " +
                            "    (\"ClaveMovil\" NVARCHAR(50) NOT NULL,   " +
                            "     \"TransaccionMovil\" NVARCHAR(2) NOT NULL,   " +
                            "     \"SocioNegocio\" NVARCHAR(50) NOT NULL,   " +
                            "     \"ListaPrecio\" NVARCHAR(50) NULL,   " +
                            "     \"CondicionPago\" NVARCHAR(50) NULL,   " +
                            "     \"Indicador\" NVARCHAR(50) NULL,   " +
                            "     \"Referencia\" NVARCHAR(100) NULL,   " +
                            "     \"FechaContable\" NVARCHAR(10) NOT NULL,   " +
                            "     \"FechaVencimiento\" NVARCHAR(10) NOT NULL,   " +
                            "     \"Moneda\" NVARCHAR(50) NOT NULL,   " +
                            "     \"EmpleadoVenta\" NVARCHAR(50) NOT NULL,   " +
                            "     \"DireccionFiscal\" NVARCHAR(50) NULL,   " +
                            "     \"DireccionEntrega\" NVARCHAR(50) NULL,   " +
                            "     \"Comentario\" NVARCHAR(254) NULL,   " +
                            "     \"Migrado\" NVARCHAR(50) NOT NULL,   " +
                            "     \"Actualizado\" NVARCHAR(50) NOT NULL,   " +
                            "     \"Finalizado\" NVARCHAR(50) NOT NULL,   " +
                            "     \"DocEntry\" NVARCHAR(50) NULL,   " +
                           "      \"Mensaje\" NVARCHAR(254) NULL," +
                           "      \"EMPRESA\" SMALLINT NOT NULL , " + 
                           "      \"RANGODIRECCION\" NVARCHAR(2), " +
                           "      \"LATITUD\" NVARCHAR(150), " +
                           "      \"LONGITUD\" NVARCHAR(150) )";
        }

        private static string SQLTBOrdenVentaDetalle()
        {
            return "CREATE COLUMN TABLE \"" + BD_NAME + "\".\"" + TB_ORDEN_VENTA_DETALLE + "\"  " +
                             "    (\"ClaveMovil\" NVARCHAR(50) NOT NULL,   " +
                             "     \"Linea\" NVARCHAR(11) NULL,     " +
                             "     \"Articulo\" NVARCHAR(50) NOT NULL,      " +
                             "     \"UnidadMedida\" NVARCHAR(20) NOT NULL,      " +
                             "     \"Almacen\" NVARCHAR(8) NOT NULL,   " +
                             "     \"Cantidad\" NVARCHAR(50) NOT NULL,      " +
                             "     \"ListaPrecio\" NVARCHAR(50) NULL,   " +
                             "     \"PrecioUnitario\" NVARCHAR(50) NOT NULL,    " +
                             "     \"PorcentajeDescuento\" NVARCHAR(50) NOT NULL,   " +
                             "     \"Impuesto\" NVARCHAR(50) NOT NULL)";
        }

        private static string SQLTBPagoRecibido()
        {
            return "CREATE COLUMN TABLE \"" + BD_NAME + "\".\"" + TB_PAGO_RECIBIDO + "\" " +
                        "    (\"ClaveMovil\" NVARCHAR(50) NOT NULL,  " +
	                    "       \"TransaccionMovil\" NVARCHAR(2) NOT NULL,    " +
                        "       \"SocioNegocio\" NVARCHAR(50) NOT NULL,   " +
                        "       \"EmpleadoVenta\" NVARCHAR(50) NOT NULL,  " +
                        "       \"Comentario\" NVARCHAR(254) NULL,  " +
                        "       \"Glosa\" NVARCHAR(50) NULL,  " +
                        "       \"FechaContable\" NVARCHAR(10) NOT NULL,   " +
                        "       \"TipoPago\" NVARCHAR(2) NOT NULL,   " +
                        "       \"Moneda\" NVARCHAR(3) NOT NULL,   " +
                        "       \"ChequeCuenta\" NVARCHAR(15) NULL,   " +
                        "       \"ChequeBanco\" NVARCHAR(30) NULL,      " +
                        "       \"ChequeVencimiento\" NVARCHAR(10) NULL,   " +
                        "       \"ChequeImporte\"  NUMERIC(19,6) NULL,  " +
                        "       \"ChequeNumero\"  INTEGER CS_INT NULL,     " +
                        "       \"TransferenciaCuenta\" NVARCHAR(15) NULL,   " +
                        "       \"TransferenciaReferencia\" NVARCHAR(27) NULL,  " +
                        "       \"TransferenciaImporte\"  NUMERIC(19,6) NULL,  " +
                        "       \"EfectivoCuenta\" NVARCHAR(15) NULL,  " +
                        "       \"EfectivoImporte\"  NUMERIC(19,6) NULL, " +
                        "       \"Migrado\" NVARCHAR(2) NOT NULL,  " +
                        "       \"Actualizado\" NVARCHAR(2) NOT NULL,  " +
                        "       \"Finalizado\" NVARCHAR(2) NOT NULL,   " +
                        "       \"DocEntry\" NVARCHAR(50) NULL, " +
                        "       \"Mensaje\" NVARCHAR(254) NULL, " +
                        "       \"EMPRESA\" SMALLINT NOT NULL " +
                        "   )";
        }

        private static string SQLTBPagoRecibidoDetalle()
        {
            return "CREATE COLUMN TABLE \"" + BD_NAME + "\".\"" + TB_PAGO_RECIBIDO_DETALLE + "\" " +
                            "    (\"ClaveMovil\" NVARCHAR(50) NOT NULL, " +
	                        "     \"Linea\" INTEGER NULL,   " +
	                        "     \"FacturaCliente\" INTEGER NOT NULL,   " +
                            "     \"Importe\" NUMERIC(19,6) NOT NULL)";
        }

        private static string SQLTBMotivosIncidencias()
        {
            return "CREATE COLUMN TABLE \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\"  (  " +
	                       "      id INTEGER CS_INT NOT NULL ,   " +
	                       "      descripcion NVARCHAR(250) NOT NULL,    " +
	                       "      hab_orden CHAR(2) NOT NULL, " +
	                       "      hab_entrega  CHAR(2) NOT NULL, " +
                           "      hab_factura char(2) NOT NULL " +
                           "  )";
        }

        private static string SQLTBLogRegistros()
        {
            return "CREATE COLUMN TABLE \"" + BD_NAME + "\".\"" + TB_LOG_REGISTROS + "\" ( " +
                            " ID INTEGER CS_INT NOT NULL , " +
                            " EMPRESAID INTEGER," +
                            " USUARIOID NVARCHAR(200)," +
                            " CLAVEDOC NVARCHAR(200)," +
                            " TIPODOC CHAR(2)," +
                            " FECHAREGISTRO DATE ," +
                            " MESSAGE NVARCHAR(254) , " +
                            " SOURCE CHAR(2) , " +
                            " TIPO CHAR(2) " +
                            " )";
        }

        private static string SQLTBSocioNegocio()
        {
            return "CREATE COLUMN TABLE  \"" + BD_NAME + "\".\"" + TB_SOCIO_NEGOCIO + "\" (   " + 
	                      "  \"ClaveMovil\" NVARCHAR(50) NOT NULL,   " + 
	                      "  \"TransaccionMovil\" NVARCHAR(2) NOT NULL,  " +
	                      "  \"TipoPersona\" NVARCHAR(10) NOT NULL, " +
	                      "  \"TipoDocumento\" NVARCHAR(10),	 " +
	                      "  \"NumeroDocumento\" NVARCHAR(32), " +
	                      "  \"NombreRazonSocial\" NVARCHAR(100), " +
	                      "  \"NombreComercial\" NVARCHAR(100), " +
	                      "  \"ApellidoPaterno\" NVARCHAR(25), " +
	                      "  \"ApellidoMaterno\" NVARCHAR(25), " +
	                      "  \"PrimerNombre\" NVARCHAR(25), " +
	                      "  \"SegundoNombre\" NVARCHAR(25), " +
	                      "  \"Telefono1\" NVARCHAR(20), " +
	                      "  \"Telefono2\" NVARCHAR(20), " +
	                      "  \"TelefonoMovil\" NVARCHAR(50), " +
	                      "  \"CorreoElectronico\" NVARCHAR(100), " +
	                      "  \"GrupoSocio\" INTEGER, " +
	                      "  \"ListaPrecio\" INTEGER, " +
	                      "  \"CondicionPago\" INTEGER, " +
	                      "  \"Indicador\" NVARCHAR(2), " +
	                      "  \"Zona\" NVARCHAR(32), " +
	                      "  \"Migrado\" CHAR(2), " +
	                      "  \"Actualizado\" CHAR(2), " +
                          "  \"Finalizado\" CHAR(2), " +
                          " \"EMPRESA\" SMALLINT NOT NULL, " +
                          " \"POSEEACTIVOS\" CHAR(2), " +
                          " \"VENDEDOR\" NVARCHAR(10), " +
                          " \"MENSAJE\" NVARCHAR(254), " +
                          " \"CARDCODE\" NVARCHAR(50) " +
                       " )";
        }

        private static string SQLTBSocioNegocioContacto()
        {
            return "CREATE COLUMN TABLE \"" + BD_NAME + "\".\"" + TB_SOCIO_NEGOCIO_CONTACTOS + "\" (   " + 
	                       " \"ClaveMovil\" NVARCHAR(50) NOT NULL, " +    
	                       " \"IdContacto\" NVARCHAR(50) NOT NULL,  " + 
	                       " \"PrimerNombre\" NVARCHAR(50), " + 
	                       " \"SegundoNombre\" NVARCHAR(50),	 " + 
	                       " \"Apellidos\" NVARCHAR(50), " + 
	                       " \"Posicion\" NVARCHAR(90), " + 
	                       " \"Direccion\" NVARCHAR(100), " + 
	                       " \"CorreoElectronico\" NVARCHAR(100), " + 
	                       " \"Telefono1\" NVARCHAR(20), " + 
	                       " \"Telefono2\" NVARCHAR(20), " +
                           " \"TelefonoMovil\" NVARCHAR(50) " + 
                        " )";
        }

        private static string SQLTBSocioNegocioDireccion()
        {
            return "CREATE COLUMN TABLE \"" + BD_NAME + "\".\"" + TB_SOCIO_NEGOCIO_DIRECCIONES + "\" ( " +
	                      "  \"ClaveMovil\" NVARCHAR(50) NOT NULL,    " +
	                      "  \"Tipo\" NVARCHAR(1) NOT NULL,  " +
	                      "  \"Codigo\" NVARCHAR(50),  " +
	                      "  \"Pais\" NVARCHAR(3), " +
	                      "  \"Departamento\" NVARCHAR(3),	 " +
	                      "  \"Provincia\" NVARCHAR(100), " +
	                      "  \"Distrito\" NVARCHAR(100), " +
	                      "  \"Calle\" NVARCHAR(100), " +
                          "  \"Referencia\" NVARCHAR(100),  " +
                          "  \"LATITUD\"        NVARCHAR(50),              " +
                          "  \"LONGITUD\"       NVARCHAR(50)             " +
                          " )";
        }

        private static string SQLTBActividades()
        {
            return "CREATE COLUMN TABLE \"" + BD_NAME + "\".\"" + TB_ACTIVIDADES + "\" " +
                   " (   \"ClaveMovil\"      NVARCHAR(50) NOT NULL,   " +
                   "     \"Origen\"         NVARCHAR(100),              " +
                   "     \"CodigoCliente\"      NVARCHAR(15),        " +
                   "     \"CodigoContacto\"     NVARCHAR(50),       " +
                   "     \"CodigoDireccion\"    NVARCHAR(50),      " +
                   "     \"CodigoMotivo\"       INTEGER,              " +
                   "     \"Comentarios\"         NVARCHAR(100),         " +
                   "     \"Vendedor\"       INTEGER,                  " +
                   "     \"Latitud\"        NVARCHAR(50),              " +
                   "     \"Longitud\"       NVARCHAR(50),             " +
                   "     \"FechaCreacion\"      NVARCHAR(10),        " +
                   "     \"HoraCreacion\"     NVARCHAR(5),          " +
                   "     \"ModoOffLine\"        char(1),               " +
                   "     \"ClaveFactura\"       INTEGER,              " +
                   "     \"SerieFactura\"       NVARCHAR(4),          " +
                   "     \"CorrelativoFactura\" INTEGER,        " +
                   "     \"TipoIncidencia\"     NVARCHAR(2),        " +
                   "     \"FechaPago\"          NVARCHAR(10),            " +
                   "     \"Migrado\"            NVARCHAR(2),               " +
                   "     \"Actualizado\"        NVARCHAR(2),           " +
                   "     \"Finalizado\"         NVARCHAR(2),            " +
                   "     \"CodigoSAP\"          integer,                 " +
                   "     \"Mensaje\"            NVARCHAR(254),             " +
                   "     \"EMPRESA\"            integer,  " +
                   "     \"RANGODIRECCION\"     NVARCHAR(2))";
        }

        private static string SQLTBDevolucion()
        {
            return "CREATE COLUMN TABLE \"" + BD_NAME + "\".\"" + TB_DEVOLUCION + "\"    " +
                    "  (   " +
                    "      ClaveMovil NVARCHAR(50) NOT NULL,   " +
                    "      ClaveBase NVARCHAR(50) NOT NULL,               " +
                    "      SocioNegocio NVARCHAR(50) NOT NULL,            " +
                    "      ListaPrecio NVARCHAR(50) NULL,                 " +
                    "      CondicionPago NVARCHAR(50) NULL,               " +
                    "      Indicador NVARCHAR(50) NULL,                   " +
                    "      Referencia NVARCHAR(100) NULL,                 " +
                    "      FechaContable NVARCHAR(10) NOT NULL,           " +
                    "      FechaVencimiento NVARCHAR(10) NOT NULL,        " +
                    "      Moneda NVARCHAR(50) NOT NULL,                  " +
                    "      EmpleadoVenta NVARCHAR(50) NOT NULL,           " +
                    "      DireccionFiscal NVARCHAR(50) NULL,             " +
                    "      DireccionEntrega NVARCHAR(50) NULL,            " +
                    "      Comentario NVARCHAR(254) NULL,                 " +
                    "      Migrado NVARCHAR(50) NOT NULL,                 " +
                    "      DocEntry NVARCHAR(50) NULL,                    " +
                    "      Mensaje NVARCHAR(254) NULL,                    " +
                    "      EMPRESA SMALLINT NOT NULL                      " +
                    "  )";
        }

        public static List<string> getInserts()
        {
            var list = new List<string>();

            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (1, 'Cliente con stock', 'Y', 'N', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (2, 'Cliente sin dinero', 'Y', 'Y', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (3, 'Local cerrado', 'Y', 'Y', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (4, 'Máquina malograda', 'Y', 'Y', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (5, 'Recojo de activo', 'Y', 'N', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (6, 'Ausencia de encargado', 'Y', 'Y', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (7, 'Sin servicio eléctrico', 'Y', 'Y', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (8, 'Falta de tiempo', 'Y', 'Y', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (9, 'Otros', 'Y', 'Y', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (10, 'Cliente no pidió', 'N', 'Y', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (11, 'Pedido errado', 'N', 'Y', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (12, 'Producto averiado', 'N', 'Y', 'N')");
            list.Add("insert into \"" + BD_NAME + "\".\"" + TB_MOTIVOS_INCIDENCIAS + "\" values (13, 'Camión malogrado', 'N', 'Y', 'N')");

            return list;
        }
           
         
    }
}
