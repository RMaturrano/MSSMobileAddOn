using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;

namespace AddonSeidorMobile
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Conexion oConexion = new Conexion();

                if ((Conexion.company != null) && (Conexion.company.Connected))
                {
                    Conexion.application.StatusBar.SetText("Verificando estructura de datos del AddOn...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_None);
                    EstructuraDatos oEstructuraDatos = new EstructuraDatos();
                    Conexion.application.StatusBar.SetText("Verificación completada.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_None);

                    GC.KeepAlive(oConexion);
                    Application.Run();
                }
                else {
                    MessageBox.Show("No se pudo obtener la compañía desde la sesión actual, reinicie la aplicación. ", "No se pudo iniciar AddOn Mobile");
                    Application.Exit();
                }

                Application.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error que impidió la ejecución de la aplicación: " + ex.Message, "No se pudo iniciar AddOn Mobile");
            }
        }
    }
}
