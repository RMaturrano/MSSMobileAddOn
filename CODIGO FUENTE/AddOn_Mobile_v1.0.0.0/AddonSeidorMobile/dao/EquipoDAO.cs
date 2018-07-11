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
    public class EquipoDAO : FormCommon
    {
        public static List<EquipoBean> listar()
        {
            var res = new List<EquipoBean>();
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("SELECT * from \"@" + Movil.getTabla().nombre + "\" order by \"DocEntry\"");

                if (mRS.RecordCount > 0)
                {
                    while (!mRS.EoF)
                    {
                        res.Add(new EquipoBean()
                        {
                            docEntry = mRS.Fields.Item("DocEntry").Value,
                            codigo = mRS.Fields.Item("Code").Value.ToString().Trim(),
                            descripcion = mRS.Fields.Item("Name").Value.ToString().Trim(),
                            modelo = mRS.Fields.Item("U_MSSM_MOD").Value.ToString().Trim(),
                            serie =  mRS.Fields.Item("U_MSSM_SER").Value.ToString().Trim(),
                            color =  mRS.Fields.Item("U_MSSM_COL").Value.ToString().Trim(),
                            codigoUnico = mRS.Fields.Item("U_MSSM_IDU").Value.ToString().Trim(),
                            verificarId = mRS.Fields.Item("U_MSSM_VAL").Value.ToString().Trim()
                        });
                        mRS.MoveNext();
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("EquipoDAO > listar() > " + ex.Message);
            }
            finally
            {
                if (mRS != null)
                    LiberarObjetoGenerico(mRS);
            }

            return res;
        }

        public static bool actualizar(EquipoBean bean)
        {
            var res = true;

            SAPbobsCOM.GeneralService mService = null;
            SAPbobsCOM.GeneralDataParams searchParams = null;
            SAPbobsCOM.GeneralData mEquipo = null;

            try
            {

                mService = Conexion.company.GetCompanyService().GetGeneralService(Movil.getTabla().nombre);
                mEquipo = mService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                searchParams = mService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                searchParams.SetProperty("Code", bean.codigo);

                mEquipo = mService.GetByParams(searchParams);
                mEquipo.SetProperty("Name", bean.descripcion);
                mEquipo.SetProperty("U_MSSM_MOD", bean.modelo);
                mEquipo.SetProperty("U_MSSM_SER", bean.serie);
                mEquipo.SetProperty("U_MSSM_COL", bean.color);
                mEquipo.SetProperty("U_MSSM_IDU", bean.codigoUnico);
                mEquipo.SetProperty("U_MSSM_VAL", bean.verificarId);

                mService.Update(mEquipo);
            }
            catch (Exception e)
            {
                res = false;
                StatusMessageError("EquipoDAO > actualizar() > " + e.Message);
            }
            finally
            {
                if (mService != null)
                    LiberarObjetoGenerico(mService);

                if (searchParams != null)
                    LiberarObjetoGenerico(searchParams);

                if (mEquipo != null)
                    LiberarObjetoGenerico(mEquipo);
            }

            return res;
        }

        public static bool registrar(EquipoBean bean)
        {
            var res = true;

            SAPbobsCOM.GeneralService mService = null;
            SAPbobsCOM.GeneralData mEquipo = null;

            try
            {
                mService = Conexion.company.GetCompanyService().GetGeneralService(Movil.getTabla().nombre);
                mEquipo = mService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                mEquipo.SetProperty("Code", bean.codigo);
                mEquipo.SetProperty("Name", bean.descripcion);
                mEquipo.SetProperty("U_MSSM_MOD", bean.modelo);
                mEquipo.SetProperty("U_MSSM_SER", bean.serie);
                mEquipo.SetProperty("U_MSSM_COL", bean.color);
                mEquipo.SetProperty("U_MSSM_IDU", bean.codigoUnico);
                mEquipo.SetProperty("U_MSSM_VAL", bean.verificarId);
                mService.Add(mEquipo);
            }
            catch (Exception e)
            {
                res = false;
                StatusMessageError("EquipoDAO > registrar() > " + e.Message);
            }
            finally
            {
                if (mService != null)
                    LiberarObjetoGenerico(mService);

                if (mEquipo != null)
                    LiberarObjetoGenerico(mEquipo);
            }

            return res;
        }
    }
}
