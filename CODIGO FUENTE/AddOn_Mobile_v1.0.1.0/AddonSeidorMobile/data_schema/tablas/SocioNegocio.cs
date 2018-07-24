using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.data_schema.tablas
{
    public class SocioNegocio
    {
        private const string TABLA_CABE = "OCRD";
        private const string TABLA_DIRECCIONES = "CRD1";

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
                nombre_campo = "MSSM_POA",
                descrp_campo = "Posee activos",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 2,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_CABE,
                nombre_campo = "MSSM_TRE",
                descrp_campo = "Tipo de registro",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 2,
                validValues = new string[] { "01", "02", "03" },
                validDescription = new string[] { "Lead", "Cliente final", "Cliente competencia" },
                valorPorDef = "02"
            });

            return myList;
        }

        public static List<CampoBean> getCamposTablaDirecciones()
        {
            var myList = new List<CampoBean>();

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSSM_LAT",
                descrp_campo = "Latitud",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 100
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSSM_LON",
                descrp_campo = "Longitud",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 100
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSSM_FIV",
                descrp_campo = "Fecha de inicio visitas",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Date
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_COVE",
                descrp_campo = "Código vendedor",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 20
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_DVLU",
                descrp_campo = "Visita Lunes",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_DVMA",
                descrp_campo = "Visita Martes",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_DVMI",
                descrp_campo = "Visita Miercoles",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_DVJU",
                descrp_campo = "Visita Jueves",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_DVVI",
                descrp_campo = "Visita Viernes",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_DVSA",
                descrp_campo = "Visita Sábado",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_DVDO",
                descrp_campo = "Visita Domingo",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 1,
                validValues = new string[] { "Y", "N" },
                validDescription = new string[] { "SI", "NO" },
                valorPorDef = "N"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_FREC",
                descrp_campo = "Frecuencia",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 3,
                validValues = new string[] { "000", "101", "102", "103" },
                validDescription = new string[] { "-", "Semanal", "Quincenal", "Mensual" },
                valorPorDef = "000"
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_CANA",
                descrp_campo = "Canal",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 50
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_RUTA",
                descrp_campo = "Ruta",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 20
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_ZONA",
                descrp_campo = "Zona",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 20
            });

            myList.Add(new CampoBean()
            {
                nombre_tabla = TABLA_DIRECCIONES,
                nombre_campo = "MSS_GIRO",
                descrp_campo = "Giro",
                tipo_campo = SAPbobsCOM.BoFieldTypes.db_Alpha,
                tamano = 50
            });

            return myList;
        }

        #endregion
    }
}
