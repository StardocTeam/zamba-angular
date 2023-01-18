using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Zamba.CoverPage
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
            SingleInstance.SingleApplication.Run((new MainForm()));
        }
    }
}
