using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZambaLink
{
    static class Program
    {       
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Frm_ZLink());
        //}

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Solucion problema  de resolucion http://stackoverflow.com/questions/13228185/how-to-configure-an-app-to-run-correctly-on-a-machine-with-a-high-dpi-setting-e/13228495
            if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

          
            SingleInstanceController controller = new SingleInstanceController();
                        controller.Run(args);

            #region Mutex
            //bool createdNew = true;
            //string line = String.Empty;
            //if (args.Length > 0)
            //{
            //    foreach (string Item in args)
            //    {
            //        line += Item + " ";
            //    }
            //}
            //else
            //    line = "";
            //using (Mutex mutex = new Mutex(true, "Zamba.Link", out createdNew))
            //{
            //    if (createdNew)
            //    {
            //        Application.EnableVisualStyles();
            //        Application.SetCompatibleTextRenderingDefault(false);
            //        Application.Run(new Frm_ZLink(line));
            //    }
            //    else
            //    {
            //        Process current = Process.GetCurrentProcess();
            //        foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            //        {
            //            if (process.Id != current.Id)
            //            {
            //                SetForegroundWindow(process.MainWindowHandle);
            //                break;
            //            }
            //        }
            //    }
            //}
            #endregion
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }

    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        string lineUsr = string.Empty;
        public SingleInstanceController()
        {
            IsSingleInstance = true;

            StartupNextInstance += this_StartupNextInstance;

            Startup += this_StartUp;
        }

        void this_StartUp(object sender, StartupEventArgs e)
        {

            if (e.CommandLine.Count > 0)
            {
                lineUsr = e.CommandLine[0];
            Frm_ZLink form = new Frm_ZLink(e.CommandLine[0]);

            }
            else
            {
                Frm_ZLink form = new Frm_ZLink();
            }

        }

        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            Frm_ZLink form = MainForm as Frm_ZLink; //My derived form type
            //if (e.CommandLine.Count > 1)
            //    form.ExecuteCurrentClient(e.CommandLine[1]);
            form.Show();
        }

        protected override void OnCreateMainForm()
        {
            if (lineUsr != string.Empty)
            {
                MainForm = new Frm_ZLink(lineUsr);

            }
            else
            {
                MainForm = new Frm_ZLink();
            }
        }
    }
}
