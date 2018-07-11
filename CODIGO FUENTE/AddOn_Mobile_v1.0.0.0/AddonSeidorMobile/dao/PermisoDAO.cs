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
    public class PermisoDAO: FormCommon
    {
        public static string getQueryForList()
        {
            string query = "select distinct \"idPerfil\" as \"Código\", T1.\"Name\" as \"Descripción\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_PERMISOS +
                             "  T0 join " + Conexion.company.CompanyDB + ".\"@MSSM_MTU\" T1 on T0.\"idPerfil\" = T1.\"Code\" " +
                             "   where \"idEmpresa\" = " + EmpresaDAO.obtenerIdInterno() +
                             "   union " +
                             "   select \"Code\" as \"idPerfil\", \"Name\" from " + Conexion.company.CompanyDB + ".\"@MSSM_MTU\" " +
                             "   order by 1 ";

            return query;
        }

        public static List<PermisoBean> listarCabecera()
        {
            var res = new List<PermisoBean>();
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("select distinct \"idPerfil\", T1.\"Name\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_PERMISOS+ 
                             "  T0 join "+Conexion.company.CompanyDB+".\"@MSSM_MTU\" T1 on T0.\"idPerfil\" = T1.\"Code\" " +
                             "   where \"idEmpresa\" = " + EmpresaDAO.obtenerIdInterno() +
                             "   union " +
                             "   select \"Code\" as \"idPerfil\", \"Name\" from " + Conexion.company.CompanyDB + ".\"@MSSM_MTU\" " +
                             "   order by \"idPerfil\" ");

                if (mRS.RecordCount > 0)
                {
                    while (!mRS.EoF)
                    {
                        res.Add(new PermisoBean()
                        {
                            codigo = mRS.Fields.Item("idPerfil").Value.ToString().Trim(),
                            descripcion = mRS.Fields.Item("Name").Value.ToString().Trim()
                        });
                        mRS.MoveNext();
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("PermisoDAO > listarCabecera() > " + ex.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        public static string getQueryForListDetail(string codigo)
        {
            return "select T0.\"idMenu\" as \"Código\",T1.\"descripcion\" as \"Descripción\", " +
                                   " T0.\"accesa\" as \"Accesa\",T0.\"crea\" as \"Crea\", " +
                                   " T0.\"edita\" as \"Edita\",T0.\"aprueba\" as \"Aprueba\",T0.\"rechaza\" as \"Rechaza\", " +
                                   " T0.\"escogePrecio\" as \"Sel. Lista precio\" " +
                            " from " + BdMobile.BD_NAME + "." + BdMobile.TB_PERMISOS +
                            "  T0 JOIN " + BdMobile.BD_NAME + "." + BdMobile.TB_MENUAPP + " T1 " +
                            " ON T0.\"idMenu\" = T1.\"id\" " +
                            " where T0.\"idPerfil\" = '" + codigo +
                            "' and T0.\"idEmpresa\" = " + EmpresaDAO.obtenerIdInterno() + " order by T0.\"id\"";
        }

        public static List<PermisoDetBean> listarDetalle(string codigo)
        {
            var res = new List<PermisoDetBean>();
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("select T0.\"id\",T0.\"idMenu\",T1.\"descripcion\",T0.\"accesa\",T0.\"crea\", "+
		                           " T0.\"edita\",T0.\"aprueba\",T0.\"rechaza\",T0.\"escogePrecio\" " +
                            " from " + BdMobile.BD_NAME + "." + BdMobile.TB_PERMISOS + 
                            "  T0 JOIN " + BdMobile.BD_NAME + "." + BdMobile.TB_MENUAPP + " T1 "+
                            " ON T0.\"idMenu\" = T1.\"id\" " +
                            " where T0.\"idPerfil\" = '" + codigo +
                            "' and T0.\"idEmpresa\" = "+EmpresaDAO.obtenerIdInterno()+" order by T0.\"id\"");

                if (mRS.RecordCount > 0)
                {
                    while (!mRS.EoF)
                    {
                        res.Add(new PermisoDetBean()
                        {
                            LineId = int.Parse(mRS.Fields.Item("id").Value.ToString().Trim()),
                            codigoMenu = mRS.Fields.Item("idMenu").Value.ToString().Trim(),
                            descripcionMenu = mRS.Fields.Item("descripcion").Value.ToString().Trim(),
                            accesa = mRS.Fields.Item("accesa").Value.ToString().Trim(),
                            crea = mRS.Fields.Item("crea").Value.ToString().Trim(),
                            edita = mRS.Fields.Item("edita").Value.ToString().Trim(),
                            aprueba = mRS.Fields.Item("aprueba").Value.ToString().Trim(),
                            rechaza = mRS.Fields.Item("rechaza").Value.ToString().Trim(),
                            escogePrecio = mRS.Fields.Item("escogePrecio").Value.ToString().Trim()
                        });
                        mRS.MoveNext();
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("PermisoDAO > listarDetalle() > " + ex.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        public static bool registrarPerfilDefault(string codigo, string descripcion)
        {
            var res = true;

            //Cuando se registra un nuevo perfil, añadir por defecto los permisos de menu
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                var mLines = MenuAppDAO.listar();

                foreach (var l in mLines)
                {
                    mRS.DoQuery("insert into " + BdMobile.BD_NAME + "." + BdMobile.TB_PERMISOS +
                                " values (" + obtenerUltimoId() + ", " + EmpresaDAO.obtenerIdInterno() +
                                ", " + l.docEntry + ", '" + codigo + "', 'N', 'N', 'N', 'N', 'N', 'N')");
                }
            }
            catch (Exception e)
            {
                res = false;
                StatusMessageError("PermisoDAO > registrarPerfilDefault() > " + e.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        public static bool registrarMenuDetalleDefault(string codMenu, string descripcion)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;
            SAPbobsCOM.Recordset mRSInsert = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRSInsert = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("select distinct \"idPerfil\", \"idEmpresa\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_PERMISOS);

                string idMenu = MenuAppDAO.obtenerIdxCodigo(codMenu);

                if (mRS.RecordCount > 0)
                {
                    while (!mRS.EoF)
                    {
                        string idPerfil = mRS.Fields.Item("idPerfil").Value.ToString().Trim();
                        string idEmpresa = mRS.Fields.Item("idEmpresa").Value.ToString().Trim();

                        mRSInsert.DoQuery("insert into " + BdMobile.BD_NAME + "." + BdMobile.TB_PERMISOS +
                                    " values (" + obtenerUltimoId() + ", " + idEmpresa +
                                    ", "+idMenu+", '"+idPerfil+"', 'N', 'N', 'N', 'N', 'N', 'N')");

                        mRS.MoveNext();
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("PermisoDAO > registrarDetalleDefault() > " + ex.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);

                if (mRSInsert != null)
                    LiberarObjetoGenerico(mRSInsert);
            }

            return res;
        }

        public static bool actualizar(PermisoBean bean)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                foreach (var permiso in bean.detalles)
                {

                    string query = " UPDATE " + BdMobile.BD_NAME + "." + BdMobile.TB_PERMISOS +
                            "  set \"accesa\" = '" + permiso.accesa + "', \"crea\" = '" + permiso.crea + "',  " +
                                   " \"edita\" = '" + permiso.edita + "', \"aprueba\" = '" + permiso.aprueba + "',  " +
                                   " \"rechaza\" = '" + permiso.rechaza + "', \"escogePrecio\" = '" + permiso.escogePrecio + "'  " +
                            "  where \"idEmpresa\" = " + EmpresaDAO.obtenerIdInterno() + " AND \"idMenu\" = " +
                               permiso.codigoMenu + " AND \"idPerfil\" = '" + bean.codigo + "'";
                    mRS.DoQuery(query);
                }
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

        internal static bool verificarEstadoTU(string codigo)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("SELECT \"U_MSSM_HAB\" FROM \"@MSSM_MTU\" WHERE \"Code\" = '" + codigo + "'");

                if (mRS.RecordCount > 0)
                {
                    string value = mRS.Fields.Item("U_MSSM_HAB").Value.ToString().Trim();
                    if (string.IsNullOrEmpty(value))
                        res = false;
                    else if (value.Equals("N"))
                        res = false;
                }
                else
                    res = false;
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("PermisoDAO > verificarEstadoTU() > " + ex.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        internal static bool eliminarMenu(int idMenu)
        {
            var res = true;
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("delete from " + BdMobile.BD_NAME + "." + BdMobile.TB_PERMISOS + " where \"idMenu\" = " + idMenu);
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("PermisoDAO > eliminarMenu() > " + ex.Message);
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
                oRS.DoQuery("select IFNULL(max(\"id\"),0) + 1  as \"Result\" from " + BdMobile.BD_NAME + "." + BdMobile.TB_PERMISOS);

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
    }
}
