using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WebDavMail
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
            frmWebDavMailExample mainFrm = new frmWebDavMailExample();
            Application.Run(mainFrm);
        }
    }
}
