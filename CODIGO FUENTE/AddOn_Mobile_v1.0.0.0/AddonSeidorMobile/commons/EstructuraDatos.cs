using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Reflection;
using System.Text;
using AddonSeidorMobile.conexion;
using AddonSeidorMobile.data_access;
using AddonSeidorMobile.data_schema;

namespace AddonSeidorMobile.commons
{
    public class EstructuraDatos : FormCommon
    {
        #region _Attributes_

        int m_iErrCode = 0;
        string m_sErrMsg = "";
        private string m_sNombreAddon = AddonSeidorMobile.Properties.Resources.NombreAddon;
        private string m_sDescripcion = AddonSeidorMobile.Properties.Resources.Descripcion;
        #endregion

        #region _Constructor_

        public EstructuraDatos()
        {
            try
            {
                if (ValidaVersion(m_sNombreAddon, m_sVersion))
                {
                    RegistrarVersion(m_sNombreAddon, m_sVersion);
                    CrearTablasADDON();
                    CrearCamposADDON();
                    CrearObjetosADDON();
                    CrearStoresADDON();
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("Error: EstructuraDatos.cs > EstructuraDatos():" + ex.Message);
            }
        }

        #endregion

        #region _Methods_

        private void CrearTablasADDON()
        {
            foreach (var item in SchemaAddon.tablasADDON())
            {
                CreaTablaMD(item.nombre, item.descripcion, item.tipo);
            }
        }

        private void CrearCamposADDON()
        {
            foreach (var item in SchemaAddon.camposADDON())
            {
                CreaCampoMD(item.nombre_tabla, item.nombre_campo, item.descrp_campo,
                            item.tipo_campo, item.subtipo_campo, item.tamano, item.obligatorio,
                            item.validValues, item.validDescription, item.valorPorDef, item.tablaVinculada);
            }
        }

        private void CrearObjetosADDON()
        {
            foreach (var item in SchemaAddon.objetosADDON())
            {
                CreaUDOMD(item.code, item.name, item.tableName, item.findColumns, item.childTables,
                          item.canCancel, item.canClose, item.canDelete, item.canCreateDefaultForm,
                          item.formColumns, item.canFind, item.canLog, item.objectType, item.manageSeries,
                          item.enableEnhancedForm, item.rebuildEnhancedForm, item.childFormColumns);
            }
        }

        private void CrearStoresADDON()
        {
            foreach (var item in StoresAddon.storesADDON())
            {
                createSP(item.nombre, item.contenido);
            }
        }

        #endregion

        #region _Functions_

        private bool ValidaVersion(string NombreAddon, string Version)
        {
            bool bRetorno = false;
            SAPbobsCOM.UserTable oUT = null;
            SAPbobsCOM.Recordset oRS = null;
            string NombreTabla = "";
            try
            {
                NombreTabla = NombreAddon.ToUpper();
                try
                {
                    oUT = Conexion.company.UserTables.Item(NombreTabla);
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Contains("invalid field name")) oUT = null;
                    else throw ex;
                }

                if (oUT == null)
                {
                    CreaTablaMD(NombreTabla, m_sDescripcion, SAPbobsCOM.BoUTBTableType.bott_NoObject);
                    bRetorno = true;
                }
                else
                {
                    oRS = (SAPbobsCOM.Recordset)Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(Consultas.consultaTablaConfiguracion(Conexion.company.DbServerType, NombreAddon, "", true));
                    if (oRS.RecordCount == 0)
                    {
                        bRetorno = true;
                        StatusMessageInfo("Actualizará la estructura de datos");
                    }
                    else
                    {
                        if (int.Parse(Version.Replace(".", "").ToString()) > int.Parse(oRS.Fields.Item("code").Value.ToString().Replace(".", "")))
                        {
                            bRetorno = true;
                            StatusMessageInfo("Actualizará la estructura de datos");
                        }

                        if (int.Parse(Version.Replace(".", "").ToString()) < int.Parse(oRS.Fields.Item("code").Value.ToString().Replace(".", "")))
                            StatusMessageError("Detectó una version superior para este Addon");
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageError("Error: EstructuraDatos.cs > ValidaVersion(): " + ex.Message);
            }
            finally
            {
                LiberarObjetoGenerico(oUT);
                LiberarObjetoGenerico(oRS);
                oRS = null;
                oUT = null;
            }
            return bRetorno;
        }

        private void RegistrarVersion(string NombreAddon, string Version)
        {
            SAPbobsCOM.UserTable oUT = null;
            string NombreTabla = "";
            try
            {
                NombreTabla = NombreAddon.ToUpper();
                oUT = Conexion.company.UserTables.Item(NombreTabla);
                oUT.Code = Version;
                oUT.Name = NombreAddon + " V-" + Version;
                m_iErrCode = oUT.Add();
                if (m_iErrCode == 0)
                    StatusMessageSuccess("Se ingreso un nuevo registro a la BD ");
                else
                    StatusMessageError("Error ingresar el registro en la tabla: " + NombreTabla);
            }
            catch (Exception ex)
            {
                StatusMessageError("Error: EstructuraDatos.cs > RegistrarVersion():" + ex.Message);
            }
            finally
            {
                LiberarObjetoGenerico(oUT);
                oUT = null;
            }
        }

        private bool CreaTablaMD(string NombTabla, string DescTabla, SAPbobsCOM.BoUTBTableType tipoTabla)
        {
            SAPbobsCOM.UserTablesMD oUserTablesMD = null;
            try
            {
                oUserTablesMD = null;
                oUserTablesMD = (SAPbobsCOM.UserTablesMD)Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
                if (!oUserTablesMD.GetByKey(NombTabla))
                {
                    oUserTablesMD.TableName = NombTabla;
                    oUserTablesMD.TableDescription = DescTabla;
                    oUserTablesMD.TableType = tipoTabla;

                    m_iErrCode = oUserTablesMD.Add();
                    if (m_iErrCode != 0)
                    {
                        Conexion.company.GetLastError(out m_iErrCode, out m_sErrMsg);
                        StatusMessageError("Error al crear  tabla: " + NombTabla);
                        return false;
                    }
                    else
                        StatusMessageSuccess("Se ha creado la tabla: " + NombTabla);

                    LiberarObjetoGenerico(oUserTablesMD);
                    oUserTablesMD = null;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                StatusMessageError("Error: EstructuraDatos.cs > CreaTablaMD():" + ex.Message);
                return false;
            }
            finally
            {
                LiberarObjetoGenerico(oUserTablesMD);
                oUserTablesMD = null;
            }
        }

        private void CreaCampoMD(string NombreTabla, string NombreCampo, string DescCampo, SAPbobsCOM.BoFieldTypes TipoCampo, SAPbobsCOM.BoFldSubTypes SubTipo, int Tamano, SAPbobsCOM.BoYesNoEnum Obligatorio, string[] validValues, string[] validDescription, string valorPorDef, string tablaVinculada)
        {
            SAPbobsCOM.UserFieldsMD oUserFieldsMD = null;
            try
            {
                if (NombreTabla == null) NombreTabla = "";
                if (NombreCampo == null) NombreCampo = "";
                if (Tamano == 0) Tamano = 10;
                if (validValues == null) validValues = new string[0];
                if (validDescription == null) validDescription = new string[0];
                if (valorPorDef == null) valorPorDef = "";
                if (tablaVinculada == null) tablaVinculada = "";

                oUserFieldsMD = (SAPbobsCOM.UserFieldsMD)Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                oUserFieldsMD.TableName = NombreTabla;
                oUserFieldsMD.Name = NombreCampo;
                oUserFieldsMD.Description = DescCampo;
                oUserFieldsMD.Type = TipoCampo;
                if (TipoCampo != SAPbobsCOM.BoFieldTypes.db_Date) oUserFieldsMD.EditSize = Tamano;
                oUserFieldsMD.SubType = SubTipo;

                if (tablaVinculada != "") oUserFieldsMD.LinkedTable = tablaVinculada;
                else
                {
                    if (validValues.Length > 0)
                    {
                        for (int i = 0; i <= (validValues.Length - 1); i++)
                        {
                            oUserFieldsMD.ValidValues.Value = validValues[i];
                            if (validDescription.Length > 0) oUserFieldsMD.ValidValues.Description = validDescription[i];
                            else oUserFieldsMD.ValidValues.Description = validValues[i];
                            oUserFieldsMD.ValidValues.Add();
                        }
                    }
                    oUserFieldsMD.Mandatory = Obligatorio;
                    if (valorPorDef != "") oUserFieldsMD.DefaultValue = valorPorDef;
                }

                m_iErrCode = oUserFieldsMD.Add();
                if (m_iErrCode != 0)
                {
                    Conexion.company.GetLastError(out m_iErrCode, out m_sErrMsg);
                    if ((m_iErrCode != -5002) && (m_iErrCode != -2035))
                        StatusMessageError("Error al crear campo de usuario: " + NombreCampo
                            + "en la tabla: " + NombreTabla + " Error: " + m_sErrMsg);
                }
                else
                    StatusMessageSuccess("Se ha creado el campo de usuario: " + NombreCampo
                            + " en la tabla: " + NombreTabla);
            }
            catch (Exception ex)
            {
                StatusMessageError("Error: EstructuraDatos.cs > CreaCampoMD():" + ex.Message);
            }
            finally
            {
                LiberarObjetoGenerico(oUserFieldsMD);
                oUserFieldsMD = null;
            }
        }

        private bool CreaUDOMD(string sCode, string sName, string sTableName, string[] sFindColumns,
            string[] sChildTables, SAPbobsCOM.BoYesNoEnum eCanCancel, SAPbobsCOM.BoYesNoEnum eCanClose,
            SAPbobsCOM.BoYesNoEnum eCanDelete, SAPbobsCOM.BoYesNoEnum eCanCreateDefaultForm, string[] sFormColumns,
            SAPbobsCOM.BoYesNoEnum eCanFind, SAPbobsCOM.BoYesNoEnum eCanLog, SAPbobsCOM.BoUDOObjType eObjectType,
            SAPbobsCOM.BoYesNoEnum eManageSeries, SAPbobsCOM.BoYesNoEnum eEnableEnhancedForm,
            SAPbobsCOM.BoYesNoEnum eRebuildEnhancedForm, string[] sChildFormColumns)
        {
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;
            int i_Result = 0;
            string s_Result = "";

            try
            {
                oUserObjectMD = (SAPbobsCOM.UserObjectsMD)Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);

                if (!oUserObjectMD.GetByKey(sCode))
                {
                    oUserObjectMD.Code = sCode;
                    oUserObjectMD.Name = sName;
                    oUserObjectMD.ObjectType = eObjectType;
                    oUserObjectMD.TableName = sTableName;
                    oUserObjectMD.CanCancel = eCanCancel;
                    oUserObjectMD.CanClose = eCanClose;
                    oUserObjectMD.CanDelete = eCanDelete;
                    oUserObjectMD.CanCreateDefaultForm = eCanCreateDefaultForm;
                    oUserObjectMD.EnableEnhancedForm = eEnableEnhancedForm;
                    oUserObjectMD.RebuildEnhancedForm = eRebuildEnhancedForm;
                    oUserObjectMD.CanFind = eCanFind;
                    oUserObjectMD.CanLog = eCanLog;
                    oUserObjectMD.ManageSeries = eManageSeries;

                    if (sFindColumns != null)
                    {
                        for (int i = 0; i < sFindColumns.Length; i++)
                        {
                            oUserObjectMD.FindColumns.ColumnAlias = sFindColumns[i];
                            oUserObjectMD.FindColumns.Add();
                        }
                    }
                    if (sChildTables != null)
                    {
                        for (int i = 0; i < sChildTables.Length; i++)
                        {
                            oUserObjectMD.ChildTables.TableName = sChildTables[i];
                            oUserObjectMD.ChildTables.Add();
                        }
                    }
                    if (sFormColumns != null)
                    {
                        oUserObjectMD.UseUniqueFormType = SAPbobsCOM.BoYesNoEnum.tYES;

                        for (int i = 0; i < sFormColumns.Length; i++)
                        {
                            oUserObjectMD.FormColumns.FormColumnAlias = sFormColumns[i];
                            oUserObjectMD.FormColumns.Add();
                        }
                    }
                    if (sChildFormColumns != null)
                    {
                        if (sChildTables != null)
                        {
                            for (int i = 0; i < sChildFormColumns.Length; i++)
                            {
                                oUserObjectMD.FormColumns.SonNumber = 1;
                                oUserObjectMD.FormColumns.FormColumnAlias = sChildFormColumns[i];
                                oUserObjectMD.FormColumns.Add();
                            }
                        }
                    }

                    i_Result = oUserObjectMD.Add();

                    if (i_Result != 0)
                    {
                        Conexion.company.GetLastError(out i_Result, out s_Result);
                        StatusMessageError("Error: EstructuraDatos.cs > CreaUDOMD(): " + s_Result + ", creando el UDO " + sCode + ".");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                StatusMessageError("Error: EstructuraDatos.cs > CreaUDOMD():" + ex.Message);
                return false;
            }
            finally
            {
                LiberarObjetoGenerico(oUserObjectMD);
            }
        }


        private void createSP(string name, string store)
        {
            SAPbobsCOM.Recordset rs = Conexion.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            try
            {
                try
                {
                    rs.DoQuery("DROP PROCEDURE " + name);
                }
                catch (Exception)
                {
                }

                rs.DoQuery(store);
            }
            catch (Exception ex)
            {
                StatusMessageError("createSP() > " + ex.Message);
            }
            finally
            {
                if (rs != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(rs);
            }
        }

        public string m_sVersion
        {
            get
            {
                return ApplicationDeployment.IsNetworkDeployed
                       ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                       : Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        #endregion

    }
}
