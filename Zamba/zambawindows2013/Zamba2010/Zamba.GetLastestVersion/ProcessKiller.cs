using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
//using Zamba.Servers;
using System.Data;
using System.Windows.Forms;

namespace Zamba.GetLastestVersion
{


    class ProcessKiller
    {
        public const String ExecName = "Zamba.GetLastestVersion";
        public static void TryKillProcess(String processNameToKill)
        {
            if (!String.IsNullOrEmpty(processNameToKill))
            {
                Process[] processToKill = Process.GetProcessesByName(processNameToKill);
                if (null != processToKill && processToKill.Length > 0)
                {
                    foreach (Process pToKill in processToKill)
                    {
                        try
                        {
                            pToKill.Kill();
                            pToKill.Dispose();
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine("Error al matar el proceso: " + ex.ToString() + "." );
                        }

                    }
                }
            }
        }
        public static void KillAllZambaProcess()
        {
            Trace.WriteLine("------------------------------------------------------");
            Trace.WriteLine("Comienza la Destrucción de todos los Procesos de Zamba");
            //"D:\\Zamba2007\\Zamba.GetLastestVersion\\bin\\Debug"
            String myPath = Environment.CurrentDirectory;

            //Se comenta esta línea ya que al ejecutarse como Shell desde la carpeta
            //compilado, el CurrentDirectory es la carpeta compilado.
            //myPath = myPath.Remove(myPath.LastIndexOf("\\"));

            Trace.Write("Se buscan todos los ejecutables.");
            String[] executables = Directory.GetFiles(myPath, "*.exe", SearchOption.AllDirectories);
            Trace.WriteLine(" Cantidad: " + executables.Length);
            foreach (String exec in executables)
            {
                if (exec.Length > 0 && exec.Contains("\\"))
                {
                    String execName = exec.Remove(0, exec.LastIndexOf("\\"));
                    execName = execName.Replace("\\", String.Empty);
                    execName = execName.Replace(".exe", String.Empty);

                    if (String.Compare(execName, ExecName) != 0 && String.Compare(execName, ExecName + ".vshost") != 0)
                    {
                        Trace.WriteLine("Se trata de matar el proceso: " + execName);
                        TryKillProcess(execName);
                    }
                }
            }
        }
    }

    class GetLastestVersionBusiness
    {
        #region Propiedades
        //Zamba.GetLastestVersion.exe +\\martin\d\compilado 2
        private static String EnvironmentLine
        {
            get
            {
                StringBuilder sBuilder = new StringBuilder();
                foreach (String line in Environment.GetCommandLineArgs())
                {
                    sBuilder.Append(line);
                    sBuilder.Append(" ");
                }
                return sBuilder.ToString();
            }
        }
        private static String LineServerPath
        {
            get
            {
                String[] auxString = EnvironmentLine.Split(Char.Parse("+"));
                if (auxString.Length > 0 && auxString[1] != null)
                {
                    return auxString[1].Trim();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private static String ParentPath
        {
            get
            {
                String myPath = Environment.CurrentDirectory;

                //Se comenta esta línea ya que al ejecutarse como Shell desde la carpeta
                //compilado, el CurrentDirectory es la carpeta compilado.
                //myPath = myPath.Remove(myPath.LastIndexOf(Char.Parse("\\")));

                return myPath;
            }
        }
        //private static IConnection auxConnection = Server.get_Con(false, true, false);
        //private static IConnection NewConnection
        //{
        //    get
        //    {
        //        return auxConnection;
        //    }
        //} 
        #endregion 

        #region Métodos Públicos
        public static void CopyFromBase()
        {
            Trace.WriteLine(" ");
            Trace.WriteLine("------------------------------------------------------");
            Trace.WriteLine("Comienza proceso de actualización");
            CopyProcess(LineServerPath, ParentPath);
        }

        public static void ShellUpdater( String sFolder)
        {
            Trace.WriteLine(" ");
            Trace.WriteLine("------------------------------------------------------");
            Trace.WriteLine("Comienza Shell del Updater");

            Process zambaUpdater = new Process();
            Trace.WriteLine("Proceso Zamba.Updater creado.");

            //CommandLine : +CopyFrom +CopyWhere +ExecuteThis +IsZambaInstalled
            String commandLine = " +" + LineServerPath + " +" + ParentPath + " +" + ParentPath + "\\Cliente.exe" + " +true";
            Trace.WriteLine("Linea de comandos creada:");
            Trace.WriteLine(commandLine);
            String applicationPath = sFolder + "\\Zamba.UpdateNGen.exe";
            Trace.WriteLine("Path del proceso creado:");
            Trace.WriteLine(applicationPath);
            
            zambaUpdater.StartInfo = new ProcessStartInfo(applicationPath , commandLine);
            zambaUpdater.StartInfo.CreateNoWindow = false;
            zambaUpdater.StartInfo.UseShellExecute = true;

            zambaUpdater.Start();
            Trace.WriteLine("Proceso comenzado (Start)");
        }

        public static void ClearExecuteDotDat()
        {
            Trace.WriteLine(" ");
            Trace.WriteLine("------------------------------------------------------");
            Trace.WriteLine("Comienza borrado del archivo Exectue.dat");

            if (File.Exists(ParentPath + "\\execute.dat"))
            {
            File.Delete(ParentPath + "\\execute.dat");
            File.Create(ParentPath + "\\execute.dat");
            }
            //StreamWriter sw = new StreamWriter(ParentPath + "\\execute.dat", false);
            //try
            //{

            //    sw.Write("");

            //    sw.Close();
            //}
            //catch
            //{
            //    sw.Close();
            //    sw.Dispose();
            //}
        }

        //public static void ShellCliente()
        //{
        //    String clientePath = ParentPath + "\\Cliente.exe";

        //    Process zambaCliente = new Process();
        //    zambaCliente.StartInfo = new ProcessStartInfo(clientePath);
        //    zambaCliente.StartInfo.FileName = clientePath;
        //    zambaCliente.StartInfo.CreateNoWindow = true;
        //    zambaCliente.StartInfo.UseShellExecute = true;

        //    zambaCliente.Start();
        //}

        ////public static void SetEstregOneDown()
        //{
        //    try
        //    {
        //        Int32 intLastVer = GetEstreg();

        //        if (intLastVer != -1)
        //        {
        //            UpdateEstreg(intLastVer - 1);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //public static void CloseCon()
        //{
        //    if (auxConnection != null)
        //    {
        //        auxConnection.Close();
        //    }
        //}

        //public static void GetAppIni()
        //{
        //    String appIniPath = ParentPath + "\\app.ini";

        //    if (File.Exists(appIniPath))
        //    {
        //        FileInfo fAppIni = new FileInfo(appIniPath);
        //        fAppIni.CopyTo(Environment.CurrentDirectory + "\\" + fAppIni.Name, true);
        //    }
        //}

        #endregion

        #region Métodos Privados
        //private static String GetPathFromVerreg()
        //{
        //    if (null != NewConnection)
        //    {
        //        return NewConnection.ExecuteScalar(CommandType.Text, "SELECT PATH FROM VERREG").ToString();
        //    }
        //    else
        //    {
        //        return String.Empty;
        //    }
        //}

        //private static Int32 GetEstreg()
        //{
        //    if (NewConnection != null)
        //    {

        //        String strVer = NewConnection.ExecuteScalar(CommandType.Text, "SELECT VER FROM ESTREG WHERE M_Name = '" + Environment.MachineName + "'").ToString();

        //        if (String.IsNullOrEmpty(strVer))
        //        {
        //            return -1;
        //        }
        //        else
        //        {
        //            return Convert.ToInt32(strVer);
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("Servidor Inaccesible. Verifique configuración de conexión.");
        //        return -1;
        //    }
        //}

        //private static void UpdateEstreg(Int32 intVer)
        //{
        //    if (NewConnection != null)
        //    {
        //        StringBuilder sBuilder = new StringBuilder();
        //        intVer = intVer - 1;

        //        sBuilder.Append("UPDATE ESTREG SET Ver = '");
        //        sBuilder.Append(intVer);
        //        sBuilder.Append("' WHERE M_Name = '");
        //        sBuilder.Append(Environment.MachineName);
        //        sBuilder.Append("'");

        //        NewConnection.ExecuteNonQuery(CommandType.Text, sBuilder.ToString());
        //    }
        //    else
        //    {
        //        throw new Exception("Servidor Inaccesible. Verifique configuración de conexión.");
        //    }
        //}

        private static void CopyProcess(String strDirOrigen, String strDirDestino)
        {
            if (!Directory.Exists(strDirDestino))
            {
                Directory.CreateDirectory(strDirDestino);
            }

            if (Directory.Exists(strDirOrigen))
            {

                DirectoryInfo dirDestino = new DirectoryInfo(strDirDestino);
                DirectoryInfo dirOrigen = new DirectoryInfo(strDirOrigen);

                FileInfo[] archivos = dirOrigen.GetFiles();

                Int32 i;
                Int32 QArchivos = archivos.Length;

                for (i = 0; i < QArchivos; i++)
                {
                    if (String.Compare(archivos[i].Name.ToLower(), "app.ini") != 0)
                    {
                        if (i == 214)
                        {
                            i = 315;
                            i = 214;
                        }

                        try
                        {
                            archivos[i].CopyTo(dirDestino.FullName + "\\" + archivos[i].Name, true);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            try
                            {

                                FileInfo fi = new FileInfo(dirDestino.FullName + "\\" + archivos[i].Name);
                                fi.Attributes = FileAttributes.Normal;
                                fi = null;
                                archivos[i].CopyTo(dirDestino.FullName + "\\" + archivos[i].Name, true);
                            }
                            catch
                            {
                                try
                                {
                                    archivos[i].CopyTo(dirDestino.FullName + "\\failed" + archivos[i].Name, true);
                                }
                                catch { }
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                archivos[i].CopyTo(dirDestino.FullName + "\\" + archivos[i].Name, true);
                            }
                            catch { }
                        }
                    }
                }
                try
                {
                    DirectoryInfo[] subDirs = dirOrigen.GetDirectories();
                    for (i = 0; i < subDirs.Length; i++)
                    {
                        String strSubDirDes = dirDestino.FullName + "\\" + subDirs[i].Name;
                        CopyProcess(subDirs[i].FullName, strSubDirDes);
                    }
                }
                catch { }
            }

        } 
        #endregion       
    }
}
