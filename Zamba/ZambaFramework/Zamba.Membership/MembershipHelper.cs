﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Windows.Forms;

using System.IO;
using System.Collections;
using System.Diagnostics;
using System.ServiceModel;
using Zamba;

namespace Zamba.Membership
{
    public static class MembershipHelper
    {
        private static IUser _currentUser = null;
        private static ClientType _clientType = ClientType.Undefined;
        private static string _appUrl = string.Empty;
        private static string _taskWebLink = string.Empty;
        private static string _documentWebLink = string.Empty;



        private static string _optionalAppTempPath;
        private const string HTTP = "http://";
        private const string HTTPS = "https://";

        /// <summary>
        /// Obtiene un string del tipo http:// o https://
        /// </summary>
        public static string Protocol
        {
            get
            {
                if (HttpContext.Current.Request.IsSecureConnection)
                    return HTTPS;
                else
                    return HTTP;
            }
        }

        /// <summary>
        /// Obtiene la session actual, si no existe devuelve null
        /// </summary>
        /// <history>Javier 03/07/2012 Created</history>
        public static HttpSessionState CurrentSession
        {
            get 
            {
                try
                {
                    return HttpContext.Current.Session;
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }

        public static string OptionalAppTempPath
        {
            get
            {
                return _optionalAppTempPath;
            }
            set
            {
                _optionalAppTempPath = value;
            }
        }

        public static ClientType ClientType
        {
            get
            {
                return _clientType;
            }
            set
            {
                _clientType = value; 
            }
        }

        public static IUser CurrentUser
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    return (IUser)HttpContext.Current.Session["User"];
                }
                return _currentUser;
            }
        }

        public static void SetCurrentUser(IUser user)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
                HttpContext.Current.Session["User"] = user;
            else
                _currentUser = user;
        }

        public static Entorno CurrentEnvironment;

        public static String StartUpPath
        {
            get
            {
                if (System.Web.HttpContext.Current != null)
                {
                    Trace.WriteLine("System.Web.HttpContext.Current.Request.ApplicationPath: " + System.Web.HttpContext.Current.Request.ApplicationPath);
					return HttpContext.Current.Request.MapPath(Path.Combine(HttpContext.Current.Request.ApplicationPath, "bin"));
                }
                else if (CurrentEnvironment == Entorno.Exporta)
                {
                    //ML: Todo ver la forma de obtener en espanol con funciones del framework
                    if (Thread.CurrentThread.CurrentUICulture.IetfLanguageTag.Contains("es"))
                    {
                        Trace.WriteLine("Environment.GetEnvironmentVariable(ProgramW6432).Replace(Program Files, Archivos de Programa) + \\Zamba Software\\Exporta Outlook: " + Environment.GetEnvironmentVariable("ProgramW6432").Replace("Program Files", "Archivos de Programa") + @"\Zamba Software\Exporta Outlook");
                        return Environment.GetEnvironmentVariable("ProgramW6432").Replace("Program Files", "Archivos de Programa") + @"\Zamba Software\Exporta Outlook";
                    }
                    else
                    {
                        Trace.WriteLine("Environment.GetEnvironmentVariable(ProgramW6432) + \\Zamba Software\\Exporta Outlook: " + Environment.GetEnvironmentVariable("ProgramW6432") + @"\Zamba Software\Exporta Outlook");
                        return Environment.GetEnvironmentVariable("ProgramW6432") + @"\Zamba Software\Exporta Outlook";
                    }


                }
                else
                {
                    Trace.WriteLine("System.Windows.Forms.Application.StartupPath: " + System.Windows.Forms.Application.StartupPath);
                    return System.Windows.Forms.Application.StartupPath;
                }
            }
        }


        /// <summary>
        /// Ruta de los archivos de configuracion (app.ini, datetime.config, date.config, smtp)
        /// </summary>
        /// <history>Marcelo    01/02/2012  Created</history>
        public static String AppConfigPath
        {
            get
            {
                String path;
                if (System.Web.Configuration.WebConfigurationManager.AppSettings["AppConfigPath"] != null)
                {
                    try
                    {
                        path = System.Web.Configuration.WebConfigurationManager.AppSettings["AppConfigPath"];

                        if (String.IsNullOrEmpty(path) == false)
                        {
                            if (HttpContext.Current != null) 
                                if (path.StartsWith("/"))
                                    path = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + path.Replace("/", "\\");

                            if (OperationContext.Current != null)
                            { 
                                if (path.StartsWith("/"))
                                    path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + path.Replace("/", "\\");
                                path = path.Replace("\\\\", "\\");
                            }

                            if (path.EndsWith("\\"))
                                path = path.Remove(path.Length - 1);

                            if (Directory.Exists(path) == false)
                                Directory.CreateDirectory(path);

                            return path;
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.ToString());
                        path = string.Empty;
                    }
                }

                if (System.Web.Configuration.WebConfigurationManager.AppSettings["ZambaSofwareAppDataPath"] != null)
                {
                    try
                    {
                        path = System.Web.Configuration.WebConfigurationManager.AppSettings["ZambaSofwareAppDataPath"];

                        if (String.IsNullOrEmpty(path) == false)
                        {
                            if (path.EndsWith("\\"))
                                path = path.Remove(path.Length - 1);

                            if (Directory.Exists(path) == false)
                                Directory.CreateDirectory(path);

                            return path;
                        }
                    }
                    catch
                    {
                        path = string.Empty;
                    }
                }


                try
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Zamba Software";

                    if (Directory.Exists(path) == false)
                        Directory.CreateDirectory(path);

                    return path;
                }
                catch
                {

                    if (HttpContext.Current == null)
                        return Application.StartupPath;

                    return HttpContext.Current.Request.MapPath(Path.Combine(HttpContext.Current.Request.ApplicationPath, "bin"));
                }
            }
        }

        /// <summary>
        /// Ruta de los archivos temporales (Exception, Trace, Temp, IndexerTemp)
        /// </summary>
        /// <history>Marcelo    01/02/2012  Created</history>
        public static String AppTempPath
        {
            get
            {
                String path;
                if (HttpContext.Current != null)
                {
                    if (System.Web.Configuration.WebConfigurationManager.AppSettings["AppTempPath"] != null)
                    {
                        try
                        {
                            path = System.Web.Configuration.WebConfigurationManager.AppSettings["AppTempPath"];

                            if (String.IsNullOrEmpty(path) == false)
                            {
                                if (path.StartsWith("/"))
                                    path = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + path.Replace("/", "\\");

                                if (path.EndsWith("\\"))
                                    path = path.Remove(path.Length - 1);

                                if (Directory.Exists(path) == false)
                                    Directory.CreateDirectory(path);

                                return path;
                            }
                        }
                        catch
                        {
                            path = string.Empty;
                        }
                    }

                    if (System.Web.Configuration.WebConfigurationManager.AppSettings["ZambaSofwareAppDataPath"] != null)
                    {
                        try
                        {
                            path = System.Web.Configuration.WebConfigurationManager.AppSettings["ZambaSofwareAppDataPath"];

                            if (String.IsNullOrEmpty(path) == false)
                            {
                                if (path.EndsWith("\\"))
                                    path = path.Remove(path.Length - 1);

                                if (Directory.Exists(path) == false)
                                    Directory.CreateDirectory(path);

                                return path;
                            }
                        }
                        catch
                        {
                            path = string.Empty;
                        }
                    }

                }

                try
                {
                    if (string.IsNullOrEmpty(_optionalAppTempPath))
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Zamba Software";
                        _optionalAppTempPath = path;
                    }
                    else
                    {
                        path = _optionalAppTempPath;
                    }

                    if (Directory.Exists(path) == false)
                        Directory.CreateDirectory(path);

                    return path;
                }
                catch
                {

                    if (HttpContext.Current == null)
                        return Application.StartupPath;

                    return HttpContext.Current.Request.MapPath(Path.Combine(HttpContext.Current.Request.ApplicationPath, "bin"));
                }
            }
        }

        public static string Module { get; set; }
        /// <summary>
        /// Obtiene la info del directorio temporal
        /// </summary>
        /// <param name="dirName">Nombre del directorio dentro del temporal</param>
        /// <history>Marcelo    01/02/2012  Created</history>
        /// <returns>DirectoryInfo del directorio dentro del temporal</returns>
        public static DirectoryInfo AppTempDir(String dirName)
        {
            if (dirName.StartsWith("\\") == false)
                dirName = dirName + "\\";

            if (Directory.Exists(AppTempPath + dirName)==false)
                Directory.CreateDirectory(AppTempPath  + dirName);

            return new DirectoryInfo(AppTempPath  + dirName);
        }

        /// <summary>
        /// Ruta de los formularios
        /// </summary>
        /// <history>Marcelo    01/02/2012  Created</history>
        public static String AppFormPath(String CurrentTheme)
        {

            String path;
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                        try
                        {
                            path = HttpContext.Current.Request.MapPath(Path.Combine(HttpContext.Current.Request.ApplicationPath, Path.Combine("forms",CurrentTheme))); 

                            if (String.IsNullOrEmpty(path) == false)
                            {
                                if (path.EndsWith("\\"))
                                    path = path.Remove(path.Length - 1);

                                if (Directory.Exists(path) == false)
                                    Directory.CreateDirectory(path);

                                return path;
                            }
                        }
                        catch
                        {
                            path = string.Empty;
                        }

                    if (System.Web.Configuration.WebConfigurationManager.AppSettings["ZambaSofwareAppDataPath"] != null)
                    {
                        try
                        {
                            path = System.Web.Configuration.WebConfigurationManager.AppSettings["ZambaSofwareAppDataPath"];

                            if (String.IsNullOrEmpty(path) == false)
                            {
                                if (path.EndsWith("\\"))
                                    path = path.Remove(path.Length - 1);

                                if (Directory.Exists(path) == false)
                                    Directory.CreateDirectory(path);

                                return path;
                            }
                        }
                        catch
                        {
                            path = string.Empty;
                        }
                    }

                }

                try
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Zamba Software\\forms";

                    if (Directory.Exists(path) == false)
                        Directory.CreateDirectory(path);

                    return path;
                }
                catch
                {

                    if (HttpContext.Current == null)
                        return Application.StartupPath;

                    return HttpContext.Current.Request.MapPath(Path.Combine(HttpContext.Current.Request.ApplicationPath, "forms"));
                }
            
        }

        public static Boolean isWeb
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Session != null)
                        return true;

                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Obtiene un objeto de la session, utilizado la key como la cache.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subtype"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetSessionItem(CacheType type, CacheSubType subtype, string key)
        {
            if (CurrentSession != null)
            {
                return CurrentSession[type + "§" + subtype + "§"+ key];
            }

            return null;
        }

        /// <summary>
        /// Setea un objeto de la session, utilizado la key como la cache.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subtype"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetSessionItem(CacheType type, CacheSubType subtype, string key, object value)
        {
            if (CurrentSession != null)
            {
                CurrentSession[type + "§" + subtype + "§"+ key] = value;
            }
        }

        public static void ClearHashTables()
        {
            HttpContext.Current.Session["tableIni"] = new Hashtable();
        }

        /// <summary>
        /// Obtiene el link que se incluye en el cuerpo de los mails para poder acceder a los documentos
        /// </summary>
        public static string DocumentWebLink
        {
            get
            {
                if (_documentWebLink.Length == 0)
                    _documentWebLink = "<br><br>Link Cliente Web: <a href=\"" + AppUrl + "/Views/Main/default.aspx?docid={0}&doctype={1}\">Acceder al documento</a><br>";
                return _documentWebLink;
            }
        }

        /// <summary>
        /// Obtiene el link que se incluye en el cuerpo de los mails para poder acceder a las tareas
        /// </summary>
        public static string TaskWebLink
        {
            get
            {
                if(_taskWebLink.Length == 0)
                    _taskWebLink = "<br><br>Link Cliente Web: <a href=\"" + AppUrl + "/Views/Main/default.aspx?taskid={0}&docid={1}&doctype={2}&wfstepid={3}\">Acceder al documento</a><br>";
                return _taskWebLink;
            }
        }

        /// <summary>
        /// Obtiene la ruta de la aplicación 
        /// </summary>
        public static string AppUrl
        {
            get
            {
                if (_appUrl.Length == 0)
                    _appUrl = Protocol + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.ApplicationPath;
                return _appUrl;
            }
        }
   

        public static Hashtable tableIni
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["tableIni"] == null)
                    {
                        HttpContext.Current.Session["tableIni"] = new Hashtable();
                    }
                    return (Hashtable)HttpContext.Current.Session["tableIni"];
                }
                else
                {
                    //Trace.WriteLine("Se devuelve hashtable vacio ya que se está consumiendo desde web service.");
                }

                return new Hashtable(); ;
            }
        }

        public static void setTableIni(String key, string value)
        {
            tableIni.Add(key, value);
        }

 }
}