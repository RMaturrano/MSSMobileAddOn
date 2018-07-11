using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.entity
{
    public class PermisoBean
    {
        public int docEntry { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public List<PermisoDetBean> detalles { get; set; }
    }
}
