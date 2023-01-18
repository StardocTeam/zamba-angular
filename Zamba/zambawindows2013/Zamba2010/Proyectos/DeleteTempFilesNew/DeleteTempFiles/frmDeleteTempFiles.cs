using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace DeleteTempFiles
{
    public partial class frmDeleteTempFiles : Form
    {

        public static Int32 Interval = 21600000;
        static String initialConfigPath = System.IO.Path.Combine(Application.StartupPath, "DeleteTempFiles.ini");
        public static Int16 ValueDays;
        public static String ValuePath;
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
            System.Threading.Timer tmrC = new System.Threading.Timer(new System.Threading.TimerCallback(Execute));
            tmrC.Change(60000, interval);
        }
        public void Execute(Object o)
        {
            try
            {
                Boolean isTraceOk = false;
                isTraceOk = InitializeTrace();
                if (LoadInitalValues())
                    DeleteTemp();
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
                if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Logs"))
                    System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Logs");
                txtTrace = new TextWriterTraceListener(System.Windows.Forms.Application.StartupPath + "\\Logs\\Trace DeleteTempFiles " + System.DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt");
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
            Trace.WriteLine("con fecha menor o igual a: " + System.DateTime.Today.AddDays(ValueDays * -1));

            FileInfo[] tmpFList = tempDirectory.GetFiles();
            try
            {
                foreach (FileInfo fi in tmpFList)
                {
                    if (fi.CreationTime <= System.DateTime.Today.AddDays(ValueDays * -1))
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
    }
}