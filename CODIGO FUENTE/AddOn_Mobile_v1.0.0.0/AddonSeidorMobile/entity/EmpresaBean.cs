using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.entity
{
    public class EmpresaBean
    {
        public int id { get; set; }
        public string base_datos { get; set; }
        public string descripcion { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string estado { get; set; }
        public int maximoLineas { get; set; }
        public string estadoOrden { get; set; }
        public string estadoPago { get; set; }
        public string motivoTraslado { get; set; }
    }
}
