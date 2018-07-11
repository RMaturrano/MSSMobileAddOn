using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddonSeidorMobile.conexion;

namespace AddonSeidorMobile.commons
{
    public class FormCommon
    {
        public SAPbouiCOM.Form createForm(SAPbobsCOM.Company company, SAPbouiCOM.Application application, string resource, string formName)
        {
            SAPbouiCOM.Form mForm = null;

            try
            {
                SAPbouiCOM.FormCreationParams fCreationParams = application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
                fCreationParams.XmlData = resource;
                fCreationParams.FormType = formName;
                fCreationParams.UniqueID = formName + DateTime.Now.ToString("hhmmss");

                mForm = application.Forms.AddEx(fCreationParams);
                mForm.Settings.Enabled = true;
            }
            catch (Exception ex)
            {
                StatusMessageError("Error creando formulario " + formName + ". Excepción :" + ex.Message);
            }

            return mForm;
        }

        internal static void StatusMessageError(string message)
        {
            Conexion.application.StatusBar.SetText(Constantes.PREFIX_MSG_ADDON + message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
        }

        internal static void StatusMessageInfo(string message)
        {
            try
            {
                Conexion.application.StatusBar.SetText(Constantes.PREFIX_MSG_ADDON + message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
            }
            catch (Exception ex)
            {
                Conexion.application.StatusBar.SetText("StatusMessageInfo() > " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            
        }

        internal static void StatusMessageSuccess(string message)
        {
            Conexion.application.StatusBar.SetText(Constantes.PREFIX_MSG_ADDON + message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
        }

        internal static void LiberarObjetoGenerico(Object objeto)
        {
            try
            {
                if (objeto != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objeto);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Constantes.PREFIX_MSG_ADDON + " Error Liberando Objeto: " + ex.Message);
            }
        }
    }
}
