using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace UninstallService
{
    class Program
    {
        #region Constantes
        private const String SERVICE_UNINSTALL_PREFIX = " -u ";
        private const String SERVICE_NAME = "InstallUtil.exe\"";
        private const String SERVICE_INSTALLER_NAME = "ZambaInsertService.exe\"";
        #endregion

        static void Main(string[] args)
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