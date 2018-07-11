using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.data_schema.tablas
{
    public class Actividad
    {
        private const string TABLA_CABE = "OCLG";

        #region _COLUMNAS

        public static List<CampoBean> getCamposTabla()
        {
            var myList = new List<CampoBean>();
            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_CRM",
                descrp_campo = "Creado móvil",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_CLM",
                descrp_campo = "Clave móvil",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 50
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_TRM",
                descrp_campo = "Transacción móvil",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 2,
                validValues = new string[] { "01", "02", "03", "04", "05" },
                validDescription = new string[] { "Ninguno", "Borrador creado", "Borrador actualizado",
                    "Borrador rechazado", "Transaccion creada" },
                valorPorDef = "01"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_MOL",
                descrp_campo = "Creado en modo OffLine",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_LAT",
                descrp_campo = "Latitud",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 150
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_LON",
                descrp_campo = "Longitud",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 150
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_FEC",
                descrp_campo = "Fecha de creación",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Date,
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_HOR",
                descrp_campo = "Hora de creación",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 20
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_MOT",
                descrp_campo = "Motivo",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 150
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_SER",
                descrp_campo = "Serie de factura",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 20
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_COR",
                descrp_campo = "Correlativo",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Numeric,
                tamano = 11
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_TIP",
                descrp_campo = "Tipo de incidencia",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 100
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_FCP",
                descrp_campo = "Fecha de compromiso de pago",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Date
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_RAN",
                descrp_campo = "Rango dirección",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 2,
                validValues = new string[] { "01", "02", "03" },
                validDescription = new string[] { "Dentro del rango (20m)", "Fuera del rango", "No disponible" },
                valorPorDef = "03"
            });

            return myList;
        }

        #endregion

        public static void addActivityTypes(string newType)
        {
            SAPbobsCOM.ActivityTypes type = null;
            SAPbobsCOM.Recordset oRS = null;

            try
            {
                oRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery("SELECT COUNT(*) as \"Counter\" FROM OCLT WHERE \"Name\" like '" + newType + "'");

                if (int.Parse(oRS.Fields.Item("Counter").Value.ToString()) == 0)
                {
                    type = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oActivityTypes);
                    type.Name = newType;
                    type.Add();
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(Constantes.PREFIX_MSG_ADDON + 
                    " Error creando el tipo de actividad " + newType + " ! " + e.Message);
            }
            finally
            {
                if(type != null)
                    LiberarObjetoGenerico(type);
                LiberarObjetoGenerico(oRS);
            }
        }

        internal static void LiberarObjetoGenerico(Object objeto)
        {
            try
            {
                if (objeto != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objeto);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Constantes.PREFIX_MSG_ADDON + " Error Liberando Objeto: " + ex.Message);
            }
        }
    }
}
