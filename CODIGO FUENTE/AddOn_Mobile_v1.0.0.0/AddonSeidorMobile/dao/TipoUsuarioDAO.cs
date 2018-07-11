using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.data_schema.tablas;
using AddonSeidorMobile.entity;

namespace AddonSeidorMobile.dao
{
    public class TipoUsuarioDAO: FormCommon
    {
        public static List<TipoUsuarioBean> listar()
        {
            var res = new List<TipoUsuarioBean>();
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("SELECT \"DocEntry\", \"Code\", \"Name\", \"U_MSSM_HAB\", \"U_MSSM_SUP\", \"U_MSSM_COB\" from \"@" + TipoUsuario.getTabla().nombre + "\" order by \"DocEntry\"");

                if (mRS.RecordCount > 0)
                {
                    while (!mRS.EoF)
                    {
                        res.Add(new TipoUsuarioBean()
                        {
                            docEntry = mRS.Fields.Item("DocEntry").Value,
                            codigo = mRS.Fields.Item("Code").Value.ToString().Trim(),
                            descripcion = mRS.Fields.Item("Name").Value.ToString().Trim(),
                            activo = mRS.Fields.Item("U_MSSM_HAB").Value.ToString().Trim(),
                            supervisor = mRS.Fields.Item("U_MSSM_SUP").Value.ToString().Trim(),
                            cobrador = mRS.Fields.Item("U_MSSM_COB").Value.ToString().Trim()
                        });
                        mRS.MoveNext();
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("TipoUsuarioDAO > listar() > " + ex.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        //No usado
        public static bool eliminar(string codigo)
        {
            var res = true;

            SAPbobsCOM.GeneralService mService = null;
            SAPbobsCOM.GeneralDataParams searchParams = null;

            try
            {
                mService = Conexion.company.GetCompanyService().GetGeneralService(TipoUsuario.getTabla().nombre);
                searchParams = mService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                searchParams.SetProperty("Code", codigo);
                SAPbobsCOM.GeneralData fObj = mService.GetByParams(searchParams);
                mService.Delete(searchParams);
            }
            catch (Exception ex)
            {
                res = false;
                StatusMessageError("TipoUsuarioDAO > eliminar() > " + ex.Message);
            }
            finally
            {
                if (mService != null)
                    LiberarObjetoGenerico(mService);

                if (searchParams != null)
                    LiberarObjetoGenerico(searchParams);
            }

            return res;
        }

        public static bool actualizar(string codigo, string descripcion, string activo, string supervisor, string cobrador)
        {
            var res = true;

            SAPbobsCOM.GeneralService mService = null;
            SAPbobsCOM.GeneralDataParams searchParams = null;
            SAPbobsCOM.GeneralData mTipoUsuario = null;

            try
            {

                mService = Conexion.company.GetCompanyService().GetGeneralService(TipoUsuario.getTabla().nombre);
                mTipoUsuario = mService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                searchParams = mService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                searchParams.SetProperty("Code", codigo);

                mTipoUsuario = mService.GetByParams(searchParams);
                mTipoUsuario.SetProperty("Name", descripcion);
                mTipoUsuario.SetProperty("U_MSSM_HAB", activo);
                mTipoUsuario.SetProperty("U_MSSM_SUP", supervisor);
                mTipoUsuario.SetProperty("U_MSSM_COB", cobrador);

                mService.Update(mTipoUsuario);
            }
            catch (Exception e)
            {
                res = false;
                StatusMessageError("TipoUsuarioDAO > actualizar() > " + e.Message);
            }
            finally
            {
                if (mService != null)
                    LiberarObjetoGenerico(mService);

                if (searchParams != null)
                    LiberarObjetoGenerico(searchParams);

                if (mTipoUsuario != null)
                    LiberarObjetoGenerico(mTipoUsuario);
            }

            return res;
        }

        public static bool registrar(string codigo, string descripcion, string supervisor, string cobrador)
        {
            var res = true;

            SAPbobsCOM.GeneralService mService = null;
            SAPbobsCOM.GeneralData mTipoUsuario = null;

            try
            {
                mService = Conexion.company.GetCompanyService().GetGeneralService(TipoUsuario.getTabla().nombre);
                mTipoUsuario = mService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                mTipoUsuario.SetProperty("Code", codigo);
                mTipoUsuario.SetProperty("Name", descripcion);
                mTipoUsuario.SetProperty("U_MSSM_SUP", supervisor);
                mTipoUsuario.SetProperty("U_MSSM_COB", supervisor);
                mService.Add(mTipoUsuario);
            }
            catch (Exception e)
            {
                res = false;
                StatusMessageError("TipoUsuarioDAO > registrar() > " + e.Message);
            }
            finally
            {
                if (mService != null)
                    LiberarObjetoGenerico(mService);

                if (mTipoUsuario != null)
                    LiberarObjetoGenerico(mTipoUsuario);
            }

            return res;
        }
    }
}
