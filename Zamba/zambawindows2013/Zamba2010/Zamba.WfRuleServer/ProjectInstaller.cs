using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using Zamba.WfRuleServer ;
using System.Diagnostics;

namespace Zamba.WfRuleServer
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {

        #region Constantes
        private const String SERVICE_UNINSTALL_PREFIX = " -u ";
        private const String SERVICE_NAME = "InstallUtil.exe\"";
        private const String SERVICE_INSTALLER_NAME = "Zamba.wfruleserver.exe\"";
        #endregion

        private ServiceInstaller serviceInstaller = null;
        private ServiceProcessInstaller processInstaller = null;

        public ProjectInstaller()
        {
            InitializeComponent();
            //this.BeforeUninstall += new InstallEventHandler(ZambaServiceInstaller_BeforeUninstall);

            processInstaller = new ServiceProcessInstaller();

            serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;



            serviceInstaller.StartType = ServiceStartMode.Automatic;

            serviceInstaller.ServiceName = "Zamba.WfRuleServer";

            Installers.Add(serviceInstaller);

            Installers.Add(processInstaller);


           
                       
        }

        
    }
}
