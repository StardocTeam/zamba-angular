using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace DeleteTempFiles
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //    try
            //    {
            //        if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Logs"))
            //            System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Logs");

            //        Trace.Listeners.Add(new TextWriterTraceListener(System.Windows.Forms.Application.StartupPath + "\\Logs\\Trace DeleteTempFiles " + System.DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt"));
            //        Trace.AutoFlush = true;
            //    }
            //    catch
            //    {
            //    }

            //    System.Timers.Timer tmr = new System.Timers.Timer(DeleteTempFiles.Interval);
            //    tmr.Elapsed += new System.Timers.ElapsedEventHandler(DeleteTempFiles.DeleteFiles);

            //    //Rundeletempfiles
            //    if (DeleteTempFiles.LoadInitalValues())
            //        DeleteTempFiles.DeleteTemp();

            //    GC.KeepAlive(tmr);
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmDeleteTempFiles appDeleteTempFiles = new frmDeleteTempFiles();
            appDeleteTempFiles.Visible = false;
            Application.Run(appDeleteTempFiles);
        }
    }
}
