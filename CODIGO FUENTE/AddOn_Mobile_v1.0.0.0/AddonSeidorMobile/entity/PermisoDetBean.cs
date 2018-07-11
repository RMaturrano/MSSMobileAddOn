using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.entity
{
    public class PermisoDetBean
    {
        public int LineId { get; set; }
        public string codigoMenu { get; set; }
        public string descripcionMenu { get; set; }
        public string accesa { get; set; }
        public string crea { get; set; }
        public string edita { get; set; }
        public string aprueba { get; set; }
        public string rechaza { get; set; }
        public string escogePrecio { get; set; }
    }
}
