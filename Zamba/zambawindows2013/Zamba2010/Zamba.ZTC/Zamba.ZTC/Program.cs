using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;

namespace Zamba.ZTC
{
    class Program
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
            if (args.Length >= 3 && 
                string.Compare(args[0].Trim(), String.Empty) != 0 && 
                string.Compare(args[1].Trim(), String.Empty) != 0 &&
                string.Compare(args[2].Trim(), String.Empty) != 0)
            {

                try
                {

                    Int64 objectType = Int64.Parse(args[0]);
                    long objectId = Int64.Parse(args[1]);
                    string user = args[2];
                    string pass = args.Length > 3 ? args[3] : string.Empty;

                    Application.Run(new FrmMain(objectType, objectId, user, pass,line));
 
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    Application.Run(new FrmMain(line));
                }
            }
            else
            {
                Application.Run(new FrmMain(line));
            }
        }
    }
}
