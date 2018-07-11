using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.data_schema.tablas;

namespace AddonSeidorMobile.data_schema
{
    public class SchemaAddon
    {
        public static List<TablaBean> tablasADDON()
        {
            var tables = new List<TablaBean>();

            tables.Add(Movil.getTabla());
            tables.Add(TipoUsuario.getTabla());
            tables.Add(Vendedor.getTabla());
            tables.Add(Vendedor.getTablaDet1());
            //tables.Add(Vendedor.getTablaDet2());
            //tables.Add(Vendedor.getTablaDet3());

            return tables;
        }

        public static List<CampoBean> camposADDON()
        {
            var campos = new List<CampoBean>();

            campos.AddRange(Movil.getCamposTabla());
            campos.AddRange(TipoUsuario.getCamposTabla());
            campos.AddRange(Vendedor.getCamposCabe());
            campos.AddRange(Vendedor.getCamposDet1());
            //campos.AddRange(Vendedor.getCamposDet2());
            //campos.AddRange(Vendedor.getCamposDet3());
            campos.AddRange(OrdenVenta.getCamposTabla());
            campos.AddRange(PagosRecibidos.getCamposTabla());
            campos.AddRange(SocioNegocio.getCamposTabla());
            campos.AddRange(SocioNegocio.getCamposTablaDirecciones());
            campos.AddRange(ListaPrecio.getCamposTabla());
            campos.AddRange(Actividad.getCamposTabla());

            return campos;
        }

        public static List<ObjetoBean> objetosADDON()
        {
            var objects = new List<ObjetoBean>();

            objects.Add(Movil.getObjeto());
            objects.Add(TipoUsuario.getObjeto());
            objects.Add(Vendedor.getObjeto());

            return objects;
        }
    }
}
