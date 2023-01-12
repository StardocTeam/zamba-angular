using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace ServiceInstall
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Process MyProcess = new Process();

                MyProcess.StartInfo.UseShellExecute = true;
                MyProcess.StartInfo.FileName = "\"" + Application.StartupPath + "\\InstallUtil.exe\"";
                MyProcess.StartInfo.Arguments = "-i \"" + Application.StartupPath + "\\ZambaInsertService.exe\"";

                MyProcess.Start();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

            }
        }
    }
}

