using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Xml.Linq;
using Zamba.Core;

namespace Zamba.ImapWindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        //ServiceInstaller serviceInstaller1;
        //ServiceProcessInstaller processInstaller;
        public ProjectInstaller()
        {
            InitializeComponent();
            var status = default(string);

            // Add any initialization after the InitializeComponent() call
            // Instantiate installers for process and services.
            //this.serviceProcessInstaller1 = new ServiceProcessInstaller();
            //this.serviceInstaller1 = new ServiceInstaller();
            // The services will run under the system account.
            //serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;
            // The services will be started manually.
            //serviceInstaller1.StartType = ServiceStartMode.Automatic;
            // ServiceName must equal those on ServiceBase derived classes.
            string Name;
            try
            {
                Name = ReadIni(System.Windows.Forms.Application.StartupPath + "\\service.ini");
            }
            catch (Exception)
            {
                Name = "Zamba IMAP Windows Service3";
            }

                this.serviceInstaller1.Description = Name;
                this.serviceInstaller1.DisplayName = Name;
                this.serviceInstaller1.ServiceName = Name;

            // Add installers to collection. Order is not important.
            // Installers.Add(serviceInstaller1);
            // Installers.Add(serviceProcessInstaller1);

            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.serviceInstaller1});
        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        public static string ReadIni(string Filename)
        {

            try
            {
                if (!File.Exists(Filename))
                {
                    Trace.WriteLine("No se encuentra el archivo: " + Filename);
                    return string.Empty;
                }

                string Result = "";
                bool found = false;

                StreamReader r = new StreamReader(Filename);
                while (r.Peek() != -1 && found == false)
                {
                    Result = r.ReadLine();
                    if (Result.IndexOf("ServiceName") != -1)
                        found = true;
                }
                string ServiceName = Result.Split(char.Parse("="))[1];
                r.Close();
                r.Dispose();

                if (string.IsNullOrEmpty(ServiceName))
                {
                    return "Zamba IMAP Windows Service5";
                }
                return ServiceName;
            }
            catch
            {
                return "Zamba IMAP Windows Service6";
            }
        }

    }
}
