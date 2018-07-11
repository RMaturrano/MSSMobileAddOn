using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.commons
{
    public class Constantes
    {
        public static string PREFIX_MSG_ADDON = AddonSeidorMobile.Properties.Resources.PrefijoMensajes.ToString() + " ";
        public static string MENU_PRINCIPAL_ID = "mnuFdrSM";
        public static string DEFAULT_SELECT_MUDO = "select \"DocEntry\", \"Code\", \"Name\" from ";

        public static string OBJ_TYPE_SOCIOS_NEGOCIO = "2";
        public static string OBJ_TYPE_ORDEN_VENTA = "17";
        public static string OBJ_TYPE_EMPLEADO_VENTAS = "53";
        public static string OBJ_TYPE_FACTURA = "13";
        public static string OBJ_TYPE_ACTIVIDAD = "33";
        public static string OBJ_TYPE_DEVOLUCION = "16";
        public static string OBJ_TYPE_NOTA_CREDITO = "14";
        public static string OBJ_TYPE_ENTREGA = "15";
        public static string OBJ_TYPE_DRAFTS = "112";
        public static string OBJ_TYPE_PAYMENT_DRAFTS = "140";
        public static string OBJ_TYPE_INCOMING_PAYMENT = "24";
        public static string OBJ_TYPE_ITEM = "4";
        public static string OBJ_TYPE_WAREHOUSE = "64";
        public static string OBJ_TYPE_TAXES = "128";

        //Botones de la barra de herramientas
        public const string Menu_Buscar = "1281";
        public const string Menu_Crear = "1282";
        public const string Registro_Datos_Siguiente = "1288";
        public const string Registro_Datos_Anterior = "1289";
        public const string Primer_Registro_Datos = "1290";
        public const string Ultimo_Registro_Datos = "1291";
        public const string Actualizar_Registro = "1304";

        //Menú click derecho
        public const string Menu_AgregarLinea = "mnu_AgregarLinea";
        public const string Menu_EliminarLinea = "mnu_EliminarLinea";
        public const string Menu_AgregarLineaDescripcion = "Agregar fila";
        public const string Menu_EliminarLineaDescripcion = "Eliminar fila";
    }
}
