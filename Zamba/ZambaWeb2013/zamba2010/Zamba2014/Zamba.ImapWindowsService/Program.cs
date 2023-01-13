using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Zamba.ImapWindowsService
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            string fileName = @"H:\Temp\servicio imap.txt";

            using (StreamWriter writer = new StreamWriter(fileName, append: true))
            {
                writer.WriteLine("Main");
                writer.Close();
            }
            ServiceBase[] ServicesToRun;

            ServicesToRun = new ServiceBase[]
            {
                new IMAP()
            };
            ServiceBase.Run(ServicesToRun);

            
        }
    }
}
