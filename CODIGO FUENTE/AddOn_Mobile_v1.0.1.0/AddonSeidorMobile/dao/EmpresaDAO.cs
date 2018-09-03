using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.data_schema.database;
using AddonSeidorMobile.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.dao
{
    public class EmpresaDAO: FormCommon
    {
        public static bool registrar(EmpresaBean bean)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                string query = "INSERT INTO " + BdMobile.BD_NAME + "." + BdMobile.TB_EMPRESAS + " (\"id\", \"descripcion\", \"base_datos\", \"estado\",  " +
                            " \"usuario\", \"clave\", \"observacion\", \"LINEAS_ORDR\", \"EST_ORDR\", \"EST_ORCT\", \"MOTIVO\", \"PAIS\", \"LOCALIZACION\", " +
                            " \"CTA_TRANSFERENCIA\", \"CTA_EFECTIVO\", \"CTA_CHEQUE\")  " +
                            " VALUES(" + obtenerUltimoId() + ", '" + bean.descripcion + "', '" + bean.base_datos + "', '" +
                                bean.estado + "', '" + bean.usuario + "', '" + bean.password + "', '', " +
                                bean.maximoLineas + ", '" + bean.estadoOrden + "', '" + bean.estadoPago + "', '"+
                                bean.motivoTraslado+ "', '" + bean.pais + "'" + ", '" + bean.localizacion + "'," +
                                bean.ctaPagoTransferencia + "', '" + bean.ctaPagoEfectivo + "'" + ", '" + bean.ctaPagoCheque + "')";

                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery(query);
            }
            catch (Exception e)
            {
                res = false;
                StatusMessageError("EmpresaDAO > registrar() > " + e.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        public static bool actualizar(EmpresaBean bean)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("update " + BdMobile.BD_NAME + "." + BdMobile.TB_EMPRESAS + " SET \"descripcion\" = '" + bean.descripcion + "', " +
                                "	 \"estado\" = '" + bean.estado + "', \"usuario\" = '" + bean.usuario + "', \"clave\" = '" + bean.password + "', " +
                                "	 \"LINEAS_ORDR\" = " + bean.maximoLineas + ", \"EST_ORDR\" = '" + bean.estadoOrden + "', " + 
                                "    \"EST_ORCT\" = '" + bean.estadoPago + "', \"MOTIVO\" = '" + bean.motivoTraslado + "', " +
                                "    \"PAIS\" = '" + bean.pais + "', \"LOCALIZACION\" = '" + bean.localizacion + "', " +
                                "    \"CTA_TRANSFERENCIA\" = '" + bean.ctaPagoTransferencia + "', \"CTA_EFECTIVO\" = '" + bean.ctaPagoEfectivo + "', " +
                                "    \"CTA_CHEQUE\" = '" + bean.ctaPagoCheque + "' " +
                                "    where \"id\" = " + bean.id);
            }
            catch (Exception e)
            {
                res = false;
                StatusMessageError("EmpresaDAO > actualizar() > " + e.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        public static bool empresaExiste(string baseDatos)
        {
            bool existe = true;

            SAPbobsCOM.Recordset oRS = null;

            try
            {
                oRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery("select count(*)  as \"Result\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_EMPRESAS + " where \"base_datos\" = '" + baseDatos + "'");

                if (oRS.RecordCount > 0)
                {
                    int counter = int.Parse(oRS.Fields.Item("Result").Value.ToString().Trim());
                    if (counter == 0)
                        existe = false;
                }

            }
            catch (Exception)
            {
                existe = false;
            }
            finally
            {
                if (oRS != null)
                    LiberarObjetoGenerico(oRS);
            }

            return existe;
        }

        public static EmpresaBean obtenerEmpresa(string baseDatos)
        {
            EmpresaBean bean = null;
            SAPbobsCOM.Recordset oRS = null;

            try
            {
                oRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery("select * from " + BdMobile.BD_NAME + "." + BdMobile.TB_EMPRESAS + " where \"base_datos\" = '" + baseDatos + "'");

                if (oRS.RecordCount > 0)
                {
                    bean = new EmpresaBean();
                    bean.id = int.Parse(oRS.Fields.Item("id").Value.ToString().Trim());
                    bean.base_datos = oRS.Fields.Item("base_datos").Value.ToString().Trim();
                    bean.descripcion = oRS.Fields.Item("descripcion").Value.ToString().Trim();
                    bean.usuario = oRS.Fields.Item("usuario").Value.ToString().Trim();
                    bean.password = oRS.Fields.Item("clave").Value.ToString().Trim();
                    bean.estado = oRS.Fields.Item("estado").Value.ToString().Trim();
                    bean.maximoLineas = int.Parse(oRS.Fields.Item("LINEAS_ORDR").Value.ToString().Trim());
                    bean.estadoOrden = oRS.Fields.Item("EST_ORDR").Value.ToString().Trim();
                    bean.estadoPago = oRS.Fields.Item("EST_ORCT").Value.ToString().Trim();
                    bean.motivoTraslado = oRS.Fields.Item("MOTIVO").Value.ToString().Trim();
                    bean.pais = oRS.Fields.Item("PAIS").Value.ToString().Trim();
                    bean.localizacion = oRS.Fields.Item("LOCALIZACION").Value.ToString().Trim();
                    bean.ctaPagoTransferencia = oRS.Fields.Item("CTA_TRANSFERENCIA").Value.ToString().Trim();
                    bean.ctaPagoEfectivo = oRS.Fields.Item("CTA_EFECTIVO").Value.ToString().Trim();
                    bean.ctaPagoCheque = oRS.Fields.Item("CTA_CHEQUE").Value.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("obtenerEmpresa() > " + ex.Message);
                return null;
            }
            finally
            {
                if (oRS != null)
                    LiberarObjetoGenerico(oRS);
            }

            return bean;
        }

        public static int obtenerUltimoId()
        {
            int res = 1;
            SAPbobsCOM.Recordset oRS = null;

            try
            {
                oRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery("select IFNULL(max(\"id\"),0) + 1  as \"Result\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_EMPRESAS);

                if (oRS.RecordCount > 0)
                {
                    res = int.Parse(oRS.Fields.Item("Result").Value.ToString().Trim());
                }

            }
            catch (Exception)
            {
                res = 1;
            }
            finally
            {
                if (oRS != null)
                    LiberarObjetoGenerico(oRS);
            }


            return res;
        }

        public static int obtenerIdInterno()
        {
            int res = -1;
            SAPbobsCOM.Recordset oRS = null;

            try
            {
                oRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery("select \"id\" as \"Result\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_EMPRESAS + " where \"base_datos\" = '"+Conexion.company.CompanyDB+"'");

                if (oRS.RecordCount > 0)
                {
                    res = int.Parse(oRS.Fields.Item("Result").Value.ToString().Trim());
                }
            }
            catch (Exception)
            {
                res = -1;
            }
            finally
            {
                if (oRS != null)
                    LiberarObjetoGenerico(oRS);
            }


            return res;
        }
    }
}
