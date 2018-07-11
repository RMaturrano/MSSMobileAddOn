using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.data_schema
{
    public class TablaBean
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public SAPbobsCOM.BoUTBTableType tipo { get; set; }
    }
}
