using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Zamba.Diagrams
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string line = String.Empty;
            if (args.Length > 0)
            {
                foreach (string Item in args)
                {
                    line += Item + " ";
                }
            }
            else
                line = "";
            Application.Run(new FrmDiagramas(line));
        }
    }
}
