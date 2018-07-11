using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.data_access.bean;

namespace AddonSeidorMobile.data_access
{
    public class StoresAddon
    {
        public static List<StoreBean> storesADDON()
        {
            var stores = new List<StoreBean>();

            //  stores.AddRange(SPProvisionamiento.getStores());

            return stores;
        }
    }
}
