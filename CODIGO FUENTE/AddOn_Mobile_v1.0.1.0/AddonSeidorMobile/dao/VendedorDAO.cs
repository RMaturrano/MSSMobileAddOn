using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;

namespace AddonSeidorMobile.dao
{
    public class VendedorDAO: FormCommon
    {
        public static bool existeUsuarioMovil(string usuario, string codVendedor)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("select COUNT(\"Code\") AS \"Response\" from \"@MSSM_CVE\" where \"U_MSSM_USR\" = '"
                    +usuario.Trim()+"' AND \"Code\" != '"+codVendedor+"'");

                if (mRS.RecordCount > 0)
                {
                    int counter = int.Parse(mRS.Fields.Item("Response").Value.ToString().Trim());
                    if (counter <= 0)
                        res = false;
                }
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("VendedorDAO > existeUsuarioMovil() > " + ex.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        public static bool existeEquipoAsignado(string codEquipo, string codVendedor)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("select COUNT(\"Code\") AS \"Response\" from \"@MSSM_CVE\" where \"U_MSSM_EQP\" = '" 
                    + codEquipo.Trim() + "' AND \"Code\" != '" + codVendedor + "'");

                if (mRS.RecordCount > 0)
                {
                    int counter = int.Parse(mRS.Fields.Item("Response").Value.ToString().Trim());
                    if (counter <= 0)
                        res = false;
                }
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("VendedorDAO > existeEquipoAsignado() > " + ex.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }
    }
}
