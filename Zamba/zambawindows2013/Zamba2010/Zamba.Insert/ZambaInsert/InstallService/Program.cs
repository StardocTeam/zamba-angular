using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace InstallService
{
    class Program
    {
        #region Constantes
        private const String SERVICE_INSTALL_PREFIX = " -i ";
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
                MyProcess.StartInfo.Arguments = SERVICE_INSTALL_PREFIX + ZambaInsertService;

                MyProcess.Start();

                MessageBox.Show("Para finalizar la instalacion , reinicie la PC"); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}