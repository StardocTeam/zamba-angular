using System.ServiceProcess;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System;
using System.Windows.Forms;

namespace ZambaInsertService
{
    [RunInstaller(true)]
    public class ZambaServiceInstaller : Installer
    {

        #region Constantes
        private const String SERVICE_UNINSTALL_PREFIX = " -u ";
        private const String SERVICE_NAME = "InstallUtil.exe\"";
        private const String SERVICE_INSTALLER_NAME = "ZambaInsertService.exe\"";
        #endregion

        private ServiceInstaller _serviceInstaller = null;
        private ServiceProcessInstaller _processInstaller = null;

        public ZambaServiceInstaller()
        {

            InitializeComponent();

            this.BeforeUninstall += new InstallEventHandler(ZambaServiceInstaller_BeforeUninstall);

            _processInstaller = new ServiceProcessInstaller();
            _processInstaller.Account = ServiceAccount.LocalSystem;

            Installers.Add(_processInstaller);

            _serviceInstaller = new ServiceInstaller();
            _serviceInstaller.StartType = ServiceStartMode.Automatic;
            _serviceInstaller.ServiceName = "Zamba Insert";

            Installers.Add(_serviceInstaller);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if ((components != null))
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private System.ComponentModel.IContainer components;

        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
        }


        private void ZambaServiceInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            try
            {
                String InstallUtil = "\"" + Application.StartupPath + "\\" + SERVICE_NAME;
                String ZambaInsertService = "\"" + Application.StartupPath + "\\" + SERVICE_INSTALLER_NAME;

                Process MyProcess = new Process();

                MyProcess.StartInfo.UseShellExecute = true;
                MyProcess.StartInfo.FileName = InstallUtil;
                MyProcess.StartInfo.Arguments = SERVICE_UNINSTALL_PREFIX + ZambaInsertService;

                MyProcess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}