using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Windows.Forms;
using Zamba.Core;

namespace Zamba.Membership
{
    public static class MembershipHelper
    {
        private static IUser _currentUser = null;
        private static ClientType _clientType = ClientType.Undefined;
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
                catch
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

        /// <summary>
        /// Usuario actual de la aplicacion
        /// </summary>
        public static IUser CurrentUser
        {
            get
            {
                if (System.Web.HttpContext.Current != null)
                {
                    return (IUser)System.Web.HttpContext.Current.Session["User"];
                }
                return _currentUser;
            }
        }

        public static void SetCurrentUser(IUser user)
        {
            if (System.Web.HttpContext.Current == null)
            {
                _currentUser = user;
            }
            else
            {
                System.Web.HttpContext.Current.Session["User"] = user;
            }
        }

        public static Entorno CurrentEnvironment;

        public static String StartUpPath
        {
            get
            {
                if (System.Web.HttpContext.Current != null)
                {
                    Trace.WriteLine("System.Web.HttpContext.Current.Request.ApplicationPath: " + System.Web.HttpContext.Current.Request.ApplicationPath);
                    return System.Web.HttpContext.Current.Request.ApplicationPath;

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
        /// Valida si se esta en un entorno web
        /// </summary>
        public static Boolean isWeb
        {
            get
            {
                if (System.Web.HttpContext.Current != null)
                {
                    return true;
                }
                return false;
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

                            if (System.ServiceModel.OperationContext.Current != null)
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

        public static string GenerateRandomPassword(int passwordLength = 10)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-*";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
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
                    return Application.StartupPath;
                }
            }
        }

        public static string Module { get; set; }
    }
}