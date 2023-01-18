using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using Zamba.Servers;
using System.Text;
using Zamba.Core;
using System.IO;
using System.Threading;

namespace DocToZi
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                do
                {
                    // Ejecuta DocToZi continuamente. Cuando termina de 
                    // ejecutarse espera 30 minutos y vuelve a empezar.
                    using (Exporta a = new Exporta())
                    {
                        a.execute();
                    }
                    System.Threading.Thread.Sleep(1800000);
                } while (true);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            } 
        }
    }
}
