using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace Zamba.RemotingServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool isCreated = false ;
            foreach (Process proc in Process.GetProcesses() )
            {
                if ( Process.GetCurrentProcess().ProcessName   == proc.ProcessName && Process.GetCurrentProcess().Id != proc.Id )
                {
                    isCreated = true;
                }


            }

            if (!isCreated)
            {
                
                Application.Run(new frmMain()); 
            }
            
            
             
        }
    }
}
