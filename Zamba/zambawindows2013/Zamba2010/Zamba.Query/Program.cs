using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Zamba.Query
{
    class Program 
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>        
        
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string line= String.Empty;
            if (args.Length > 0)
            {
                foreach (string Item in args)
                {
                    line += Item + " ";
                }
            }
            else
                line = "";
            Application.Run(new FrmMain(line));
            
        }

    }
}