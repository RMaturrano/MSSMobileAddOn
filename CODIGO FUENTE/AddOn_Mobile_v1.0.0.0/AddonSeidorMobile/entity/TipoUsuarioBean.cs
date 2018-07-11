using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.entity
{
    public class TipoUsuarioBean
    {
        public int docEntry { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string activo { get; set; }
        public string supervisor { get; set; }
        public string cobrador { get; set; }
    }
}
