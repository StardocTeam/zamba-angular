using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Zamba.Core;

namespace Zamba.Framework
{
    public class AppIniManager
    {
        public void LoadAppIniFromServer()
        {
            try
            {
                string appPathUp = GetLastAppServerPathFormConfigFile();
                ZTrace.WriteLineIf(ZTrace.IsInfo, "appPathUp: " + appPathUp);

                if (appPathUp != string.Empty && appPathUp.Contains("\\"))
                {
                    //Por si llega con caracteres especiales 
                    appPathUp = Regex.Replace(appPathUp, @"\t|\n|\r", "");
                    if (Directory.Exists(appPathUp))
                    {
                        var pathsInisServer = Directory.GetFiles(appPathUp);
                        //Agarro el app.ini de la ruta
                        foreach (string currentFile in pathsInisServer)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "currentFile: " + currentFile);
                            FileInfo appIniServer = new FileInfo(currentFile);
                            appIniServer.CopyTo(Path.Combine(Membership.MembershipHelper.AppConfigPath, appIniServer.Name), true);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error: " + ex.Message);
            }
        }

        public string GetLastAppServerPathFormConfigFile()
        {
            try
            {
                FileInfo File = new FileInfo(Path.Combine(Membership.MembershipHelper.AppConfigPath, "AppIniServerPath.ini"));
                if ((File.Exists))
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "AppIniServerPath.ini: Existe");
                    StreamReader sr = new StreamReader(File.FullName);
                    string LastAppServerPath = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                    return LastAppServerPath;
                }
                //else
                //{
                //    ZTrace.WriteLineIf(ZTrace.IsInfo, "AppIniServerPath.ini: NO Existe");
                //    StreamWriter sr = new StreamWriter(File.FullName);
                //    string LastAppServerPath = @"\\arbue11as06v\zamba\Volumenes\appini";
                //    sr.WriteLine(LastAppServerPath);
                //    sr.Flush();
                //    sr.Close();
                //    sr.Dispose();
                //    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se creo AppIniServerPath.ini con: " + LastAppServerPath);

                //    return LastAppServerPath;
                //}
                return string.Empty;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error: " + ex.ToString());
                return string.Empty;
            }
        }

        public void SetLastAppServerPathFormConfigFile()
        {
            try
            {
                var ServerPath = ZOptBusiness.GetValue("AppIniServerPath");

                if (ServerPath != null && ServerPath != string.Empty)
                {
                    FileInfo File = new FileInfo(Path.Combine(Membership.MembershipHelper.AppConfigPath, "AppIniServerPath.ini"));
                    if (File.Exists)
                    {
                        File.Delete();
                    }
                    StreamWriter sr = new StreamWriter(File.FullName);
                    sr.Write(ServerPath);
                    sr.Flush();
                    sr.Close();
                    sr.Dispose();
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error: " + ex.ToString());
            }

        }

        public void ExecuteFilesFromServer()
        {
            try
            {
                string appPathUp = GetLastAppServerPathFormConfigFile();

                if (appPathUp != string.Empty && appPathUp.Contains("\\"))
                {
                    if (Directory.Exists(appPathUp))
                    {
                        var pathExesServer = Directory.GetFiles(appPathUp, "*.exe");

                        foreach (string currentFile in pathExesServer)
                        {
                            FileInfo appIniServer = new FileInfo(currentFile);
                            appIniServer.CopyTo(Path.Combine(Membership.MembershipHelper.AppConfigPath, appIniServer.Name), true);
                        }

                        var pathsBatsServer = Directory.GetFiles(appPathUp, "*.bat");

                        foreach (string currentFile in pathsBatsServer)
                        {
                            FileInfo appIniServer = new FileInfo(currentFile);
                            appIniServer.CopyTo(Path.Combine(Membership.MembershipHelper.AppConfigPath, appIniServer.Name), true);
                        }

                        foreach (string currentFile in pathExesServer)
                        {
                            System.Diagnostics.Process.Start(currentFile);
                        }

                        foreach (string currentFile in pathsBatsServer)
                        {
                            System.Diagnostics.Process.Start(currentFile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error: " + ex.Message);
            }
        }


    }
}
