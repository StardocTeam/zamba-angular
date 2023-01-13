using System.Collections.Generic;
using System.ServiceProcess;
using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ZambaInsertService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[] { new ZambaInsert() }; ;
            ServiceBase.Run(ServicesToRun);
        }
    }
}
