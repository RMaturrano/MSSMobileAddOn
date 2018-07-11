using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddonSeidorMobile.commons
{
    public interface IForm
    {
        void instanciarObjetosUI();
        void iniciarValoresPorDefecto();
        bool HandleItemEvents(SAPbouiCOM.ItemEvent itemEvent);
        bool HandleFormDataEvents(SAPbouiCOM.BusinessObjectInfo oBusinessObjectInfo);
        bool HandleMenuDataEvents(SAPbouiCOM.MenuEvent menuEvent);
        bool HandleRightClickEvent(SAPbouiCOM.ContextMenuInfo menuInfo);
        string getFormUID();
    }
}
