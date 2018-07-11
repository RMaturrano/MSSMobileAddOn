using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.data_schema.tablas;
using AddonSeidorMobile.entity;
using AddonSeidorMobile.data_schema.database;

namespace AddonSeidorMobile.dao
{
    public class MenuAppDAO:FormCommon
    {
        public const string QUERY_LIST_MENU = "SELECT  \"id\" as \"DocEntry\", \"CODIGO\" as \"Code\", \"descripcion\" as \"Name\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_MENUAPP + " order by \"id\"";

        public static List<MenuAppBean> listar()
        {
            var res = new List<MenuAppBean>();
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery(QUERY_LIST_MENU);

                if (mRS.RecordCount > 0)
                {
                    while (!mRS.EoF)
                    {
                        res.Add(new MenuAppBean()
                        {
                            docEntry = mRS.Fields.Item("DocEntry").Value,
                            codigo = mRS.Fields.Item("Code").Value.ToString().Trim(),
                            descripcion = mRS.Fields.Item("Name").Value.ToString().Trim()
                        });
                        mRS.MoveNext();
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("MenuAppDAO > listar() > " + ex.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res; 
        }

        public static bool eliminar(string codigo)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("DELETE FROM " + BdMobile.BD_NAME + "." + BdMobile.TB_MENUAPP + " where \"CODIGO\" = '" + codigo + "'");
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("MenuAppDAO > eliminar() > " + ex.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        public static bool actualizar(string codigo, string descripcion)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery(" UPDATE " + BdMobile.BD_NAME + "." + BdMobile.TB_MENUAPP + 
                            " SET \"descripcion\" = '" + descripcion.Trim() + "' where \"CODIGO\" = '" + codigo+ "'");
            }
            catch (Exception e)
            {
                res = false;
                StatusMessageError("MenuAppDAO > actualizar() > " + e.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        public static bool registrar(string codigo, string descripcion)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery(" INSERT INTO " + BdMobile.BD_NAME + "." + BdMobile.TB_MENUAPP +
                            " (\"id\", \"CODIGO\", \"descripcion\") VALUES("+ obtenerUltimoId() +", '" +codigo+ "', '"+descripcion+"')");
            }
            catch (Exception e)
            {
                res = false;
                StatusMessageError("MenuAppDAO > registrar() > " + e.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        public static int obtenerUltimoId()
        {
            int res = 1;
            SAPbobsCOM.Recordset oRS = null;

            try
            {
                oRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery("select IFNULL(max(\"id\"),0) + 1  as \"Result\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_MENUAPP);

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
                if(oRS != null)
                    LiberarObjetoGenerico(oRS);
            }


            return res;
        }

        public static string obtenerIdxCodigo(string codigo)
        {
            string res = string.Empty;
            SAPbobsCOM.Recordset oRS = null;

            try
            {
                oRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery("select \"id\"  as \"Result\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_MENUAPP + " where \"CODIGO\" = '"+codigo+"'");

                if (oRS.RecordCount > 0)
                {
                    res = oRS.Fields.Item("Result").Value.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("obtenerIdXcodigo() > " + ex.Message);
                res = string.Empty;
            }
            finally
            {
                if (oRS != null)
                    LiberarObjetoGenerico(oRS);
            }


            return res;
        }

        public static bool codigoExiste(string codigo)
        {
            bool existe = true;

            SAPbobsCOM.Recordset oRS = null;

            try
            {
                oRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery("select count(*)  as \"Result\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_MENUAPP + " where \"CODIGO\" = '" + codigo + "'");

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
    }
}
