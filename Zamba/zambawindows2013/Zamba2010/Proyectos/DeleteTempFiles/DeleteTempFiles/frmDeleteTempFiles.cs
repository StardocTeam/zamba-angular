using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DeleteTempFiles
{
    public partial class frmDeleteTempFiles 
        : Form
    {

        public static Int32 Interval = 21600000;
        static String initialConfigPath = Path.Combine(Application.StartupPath, "DeleteTempFiles.ini");
        public static Int16 ValueDays;
        public static String ValuePath;
        private static String _exceptionPath;
        private TextWriterTraceListener txtTrace;

        public frmDeleteTempFiles()
        {
            InitializeComponent();
            try
            {
                if (LoadInitalValues())
                    StartTimer(Interval);
            }
            catch
            {
            }
        }

        public void StartTimer(Int32 interval)
        {
            System.Threading.Timer tmrC = new System.Threading.Timer(new TimerCallback(Execute));
            tmrC.Change(6000, interval);
        }
        public void Execute(Object o)
        {
            try
            {
                Boolean isTraceOk = false;
                isTraceOk = InitializeTrace();
                if (LoadInitalValues())
                {
                    DeleteTemp();
                    DeleteException();
                }
                   
                if (isTraceOk)
                    CloseTrace();
                GC.Collect();
            }
            catch
            {
            }
        }

        private Boolean InitializeTrace()
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + "\\Logs"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Logs");
                txtTrace = new TextWriterTraceListener(Application.StartupPath + "\\Logs\\Trace DeleteTempFiles " + System.DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt");
                Trace.Listeners.Add(txtTrace);
                Trace.AutoFlush = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void CloseTrace()
        {
            txtTrace.Close();
            Trace.Listeners.Remove(txtTrace);
            txtTrace.Dispose();
            txtTrace = null;
        }

        private Boolean LoadInitalValues()
        {
            Trace.WriteLine(System.DateTime.Now.ToString());
            Trace.WriteLine("---------------------------------------------------");
            Trace.WriteLine("       Asignacion de valores iniciales             ");
            Trace.WriteLine("");

            //Se valida que exista el .ini, caso contrario se devuelve false.
            if (!File.Exists(initialConfigPath))
            {
                Trace.WriteLine("El archivo de configuracion no existe en la ruta:");
                Trace.WriteLine(initialConfigPath);
                return false;
            }

            StreamReader iCstream = new StreamReader(initialConfigPath);
            String[] separators = { "=" };

            try
            {
                iCstream.ReadLine();
                String iCsValueDays = iCstream.ReadLine().Split(separators, StringSplitOptions.None)[1];
                ValueDays = Int16.Parse(iCsValueDays);
                Trace.WriteLine("Primer parámetro asignado. [Días :: " + iCsValueDays + "]");

                String iCsValuePath = iCstream.ReadLine().Split(separators, StringSplitOptions.None)[1];
                if (!Directory.Exists(iCsValuePath))
                {
                    Trace.WriteLine("No se encuentra el directorio en: ");
                    Trace.WriteLine(iCsValuePath);
                    return false;
                }

                Trace.WriteLine("Segundo parámetro asignado. [Ruta :: " + iCsValuePath + "]");
                ValuePath = iCsValuePath;


                String iCsExceptionPath = iCstream.ReadLine().Split(separators, StringSplitOptions.None)[1];
                if (!Directory.Exists(iCsExceptionPath))
                {
                    Trace.WriteLine("No se encuentra el directorio en: ");
                    Trace.WriteLine(iCsExceptionPath);
                    return false;
                }
                Trace.WriteLine("Tercer parámetro asignado. [Ruta Exceptions:: " + iCsExceptionPath + "]");
                _exceptionPath = iCsExceptionPath;

            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error asignando valores: " + ex.ToString());
                return false;
            }
            finally
            {
                if (iCstream != null)
                {
                    iCstream.Close();
                }
            }

            Trace.WriteLine("");
            Trace.WriteLine("---------------------------------------------------");
            Trace.WriteLine("");
            return true;
        }
        private void DeleteTemp()
        {
            Trace.WriteLine(System.DateTime.Now.ToString());
            Trace.WriteLine("---------------------------------------------------");
            Trace.WriteLine("                Borrado de archivos                ");
            Trace.WriteLine("");

            DirectoryInfo tempDirectory = new DirectoryInfo(ValuePath);
            Trace.WriteLine("Buscando archivos en:");
            Trace.WriteLine(tempDirectory.FullName);
            Trace.WriteLine("con fecha menor o igual a: " + DateTime.Today.AddDays(ValueDays * -1));

            FileInfo[] tmpFList = tempDirectory.GetFiles();
            try
            {
                foreach (FileInfo fi in tmpFList)
                {
                    if (fi.CreationTime <= DateTime.Today.AddDays(ValueDays * -1))
                    {
                        Trace.Write("[" + fi.CreationTime.ToString() + "] " + fi.Name + ": ");
                        try
                        {
                            fi.Delete();
                            Trace.WriteLine("Eliminado con éxito.");
                        }
                        catch
                        {
                            try
                            {
                                fi.Attributes = FileAttributes.Normal;
                                fi.Delete();
                                Trace.WriteLine("Eliminado con éxito.");
                            }
                            catch
                            {
                                try
                                {
                                    File.Delete(fi.FullName);
                                    Trace.WriteLine("Eliminado con éxito.");
                                }
                                catch (Exception ex)
                                {
                                    Trace.WriteLine("Imosible eliminar. Error: " + ex.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            finally
            {
                try
                {
                    Int16 i;
                    for (i = 0; i < tmpFList.Length; i++)
                    {
                        tmpFList[i] = null;
                    }
                }
                catch { }
                finally
                {
                    tempDirectory = null;
                    tmpFList = null;
                }
            }
            Trace.WriteLine("");
            Trace.WriteLine("---------------------------------------------------");
            Trace.WriteLine("");
        }
        private void DeleteException()
        {
            Trace.WriteLine(DateTime.Now.ToString());
            Trace.WriteLine("---------------------------------------------------");
            Trace.WriteLine("                Borrado de Exceptions                ");
            Trace.WriteLine("");

            if (!Directory.Exists(_exceptionPath))
            {
                Trace.WriteLine("Directorio inexistente: " + _exceptionPath);
                return;
            }

            DirectoryInfo CurrentDirectory = new DirectoryInfo(_exceptionPath);
            
            foreach (FileInfo CurrentFile in CurrentDirectory.GetFiles())
            {
                try
                {
                    if (CurrentFile.CreationTime <= DateTime.Today.AddDays(ValueDays * -1))
                    {
                        Trace.WriteLine("Borrando" + CurrentFile.Name);
                        CurrentFile.Delete();
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error borrando" + CurrentFile.Name);
                }
            }

            CurrentDirectory = null;
        }
    }
}