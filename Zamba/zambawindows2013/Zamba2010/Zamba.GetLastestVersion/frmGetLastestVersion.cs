using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Zamba.Tools;
using System.Collections;

//using Zamba.Servers;

namespace Zamba.GetLastestVersion
{
    public partial class frmGetLastestVersion : Form
    {
        public frmGetLastestVersion()
        {
            InitializeComponent();
            try
            {
                if (!System.IO.Directory.Exists(sFolder + "\\Exceptions"))
                    System.IO.Directory.CreateDirectory(sFolder + "\\Exceptions");

                Trace.Listeners.Add(new TextWriterTraceListener(sFolder + "\\Exceptions\\Trace GetLastVersion " + System.DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt"));
                Trace.AutoFlush = true;
            }
            catch
            {
            }
            try
            {

                System.Collections.Generic.List<String> deleteShortcutNames = new System.Collections.Generic.List<String>();
                deleteShortcutNames.Add("Zamba Cliente");
                deleteShortcutNames.Add("Zamba Cliente.exe");
                deleteShortcutNames.Add("Cliente");
                deleteShortcutNames.Add("Cliente.exe");

                System.Collections.Generic.List<String> deleteShortcutTargets = new System.Collections.Generic.List<String>();
                deleteShortcutTargets.Add(sFolder + "\\Zamba Cliente.exe");

                Trace.WriteLine("");
                Trace.WriteLine("Asignacion de valores");
                Trace.WriteLine("");
                Trace.WriteLine("Desktop folder: " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                Trace.WriteLine("Startup Folder: " + sFolder);
                Trace.WriteLine("Comparación: " + ((Boolean)(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) == Application.StartupPath)).ToString());
                Trace.WriteLine("");
                Trace.WriteLine("Primer param: " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\");
                Trace.WriteLine("Segundo param: " + "C:\\Program Files\\Zamba Software\\Cliente.exe");
                Trace.WriteLine("Tercer param: " + "Zamba Cliente");
                Trace.WriteLine("Cuarto param: " + deleteShortcutNames);
                Trace.WriteLine("Quinto param: " + deleteShortcutTargets);

                ShortcutHandler.ValidateShortcutInFolder(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\", "C:\\Program Files\\Zamba Software\\Cliente.exe", "Zamba Cliente", deleteShortcutNames, deleteShortcutTargets);

            }
            catch (Exception ex)
            {
                Trace.WriteLine("");
                Trace.WriteLine("Error no controlado: " + ex.ToString());
                Trace.WriteLine("");
                
            }

        }

        private String sFolder
        {
            get
            {
                if (String.Compare(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),Application.StartupPath) == 0)
                    return "C:\\Program Files\\Zamba Software";
                else
                    return Application.StartupPath;
            }
        }

        private void frmGetLastestVersion_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            try
            {
                ProcessKiller.KillAllZambaProcess();
                GetLastestVersionBusiness.CopyFromBase();
                GetLastestVersionBusiness.ClearExecuteDotDat();
                GetLastestVersionBusiness.ShellUpdater(sFolder);
                //GetLastestVersionBusiness.GetAppIni();
                //GetLastestVersionBusiness.SetEstregOneDown();
                //GetLastestVersionBusiness.CloseCon();
                //GetLastestVersionBusiness.ShellCliente();
                this.Close();
            }
            catch
            {
                this.Close();
                this.Dispose();
            }
        }
    }
}
