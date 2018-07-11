using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.commons;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.entity;

namespace AddonSeidorMobile.dao
{
    public class SeriesDAO : FormCommon
    {
        public static List<SerieBean> listar()
        {
            var res = new List<SerieBean>();
            SAPbobsCOM.Recordset mRS = null;

            try
            {
                mRS = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                mRS.DoQuery("select \"Series\",\"SeriesName\" from NNM1 where \"ObjectCode\" = '17'");

                if (mRS.RecordCount > 0)
                {
                    while (!mRS.EoF)
                    {
                        res.Add(new SerieBean()
                        {
                            codigo = mRS.Fields.Item("Series").Value.ToString().Trim(),
                            descripcion = mRS.Fields.Item("SeriesName").Value.ToString().Trim()
                        });
                        mRS.MoveNext();
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("SeriesDAO > listar() > " + ex.Message);
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
