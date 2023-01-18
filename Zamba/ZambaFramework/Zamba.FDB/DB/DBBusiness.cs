using System;
using System.Data;
using Zamba.Servers;
using Zamba.Core;
using Zamba.AppBlock;
using System.Reflection;
using Zamba.Membership;
using Zamba;

/// <summary>
/// Esta clase se encarga de ejecutar consultas respecto a la base de datos
/// </summary>
/// <history>Marcelo 01/10/08 Created</history>
/// <remarks></remarks>
public class DBBusiness
{
    #region "System"

    public static Boolean InitializeSystem(ObjectTypes incomingModule, Assembly WorkflowRulesAssembly, bool IsService, ref string status, IErrorReportBusiness errorRB)
    {
        Zamba.Core.Cache.CacheBusiness.ClearAllCache();
        ZTrace.RemoveDBListener();
        if (InitializeDB(IsService, ref status))
        {
            if (errorRB != null && errorRB.DBWriter != null)
            {
                StartTrace(incomingModule.ToString(), errorRB.DBWriter);
            }

            if (errorRB != null)
            {
                if (bool.Parse(UserPreferences.getValueForMachine("LogErrorsInDB", UPSections.MonitorPreferences, false)))
                {
                    Zamba.AppBlock.ZException.LogToDB += errorRB.AddException;
                }
                if (bool.Parse(UserPreferences.getValueForMachine("LogPerformanceIssuesInDB", UPSections.MonitorPreferences, false)))
                {
                    Zamba.Servers.utilities.LogPerformanceIssue += errorRB.AddPerformanceIssue;
                }
                if (bool.Parse(UserPreferences.getValueForMachine("SendErrorsbyMail", UPSections.MonitorPreferences, false)))
                {
                    Zamba.AppBlock.ZException.LogToDB += errorRB.SendException;
                    Zamba.Servers.utilities.LogPerformanceIssue += errorRB.SendPerformanceIssue;
                }
            }
            // ToolsBusiness.loadGlobalVariables();

            UserPreferences.LoadAllMachineConfigValues();
            MembershipHelper.OptionalAppTempPath = ZOptBusiness.GetValue("AppTempPath");

            if ((WorkflowRulesAssembly != null))
                RulesAssembly = WorkflowRulesAssembly;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void StartTrace(string moduleName, IDBWriter DBWriter)
    {

        try
        {
            Int32 level = default(Int32);
            try
            {
                level = Int32.Parse(UserPreferences.getValueForMachine("TraceLevel", UPSections.UserPreferences, "4"));
            }
            catch
            {
                level = 4;
            }
            ZTrace.SetLevel(level, moduleName);
            AddDBListener(moduleName, DBWriter);

            if (level > 0)
                ZTrace.WriteLineIf(ZTrace.IsError, "Nivel de trace: " + level);

        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    public static void AddDBListener(string moduleName, IDBWriter DBWriter)
    {
        Boolean SaveTraceInDB = false;
        Boolean.TryParse(UserPreferences.getValueForMachine("SaveTraceInDB", UPSections.MonitorPreferences, false, false), out SaveTraceInDB);

        if (SaveTraceInDB)
            ZTrace.AddDBListener(moduleName, Environment.MachineName, DBWriter);
        else
            ZTrace.RemoveDBListener();
    }

    public static Assembly RulesAssembly;
    #endregion


    /// <summary>
    /// Return the columns names
    /// </summary>
    /// <param name="name">Nombre de la tabla</param>
    /// <returns></returns>
    /// <history>Marcelo 01/10/08 Created</history>
    /// <remarks></remarks>
    public static DataSet GetColumns(string strServer, string strDatabase, string strUser, string tableName)
    {
        return DBFactory.GetColumns(strServer, strDatabase, strUser, tableName);
    }

    /// <summary>
    /// Devuelve una lista con todas las tablas y vistas de la base de datos
    /// </summary>
    /// <returns></returns>
    /// <history>Marcelo 01/10/08 Created</history>
    /// <history>Marcelo 18/12/08 Modified</history>
    /// <remarks></remarks>
    public static DataSet GetTablesAndViews(string strServer, string strDatabase, string strUser)
    {
        return DBFactory.GetTablesAndViews(strServer, strDatabase, strUser);
    }


    public static Boolean InitializeDB(bool IsService, ref string status)
    {
        try
        {
            status = string.Empty;
            Zamba.Servers.Server.ConInitialized = false;
            Zamba.Servers.Server.ConInitializing = true;
            ZTrace.WriteLineIf(ZTrace.IsError, "InitializeDB");
            ZTrace.WriteLineIf(ZTrace.IsError, "server.currentfile:" + Server.currentfile(IsService));
            Server _server = new Server(Server.currentfile(IsService));
            if (Server.ServerType != DBTYPES.SinDefinir)
            {
                _server.MakeConnection();
                if (_server.TestConnection() == false) return false;
                _server.InitializeConnection(UserPreferences.getValue("DateConfig", UPSections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} 00:00:00', 102)"), UserPreferences.getValue("DateTimeConfig", UPSections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} {3}:{4}:{5}', 102)"));
                ZOptBusiness.GetAllValues();
                _server = null;
                Zamba.Servers.Server.ConInitializing = false;
                Zamba.Servers.Server.ConInitialized = true;
                return true;
            }
            else
            {
                Zamba.Servers.Server.ConInitializing = false;
                Zamba.Servers.Server.ConInitialized = false;
                return false;
            }
        }
        catch (Exception ex)
        {
            Zamba.Servers.Server.ConInitialized = false;
            Zamba.Servers.Server.ConInitializing = false;
            ZException.Log(ex);
            status = ex.ToString();
            return false;
        }
        finally
        {
            Zamba.Servers.Server.ConInitializing = false;
        }


    }



}