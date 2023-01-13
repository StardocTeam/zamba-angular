using InvocacionServWDigDepFiel;
using InvocacionServWDigDepFiel.wConsDepFiel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zamba.Core;

namespace ZambaAfipDepDigi
{
    public partial class ZambaAfipDepDigiService : ServiceBase
    {
        public ZambaAfipDepDigiService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            ExecuteService();
        }
        BackgroundWorker bg = null;
        private void ExecuteService()
        {
           

            bg = new BackgroundWorker();
            bg.DoWork += Bg_DoWork;
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
            bg.ProgressChanged += Bg_ProgressChanged;
            bg.RunWorkerAsync();
        }

        private void Bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Thread.Sleep(600000);
            ExecuteService();
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                int i = 1;
                while (i == 1)
                {
                    Debug.WriteLine("wait");
                }
                i = 2;

                if (Zamba.Servers.Server.ConInitialized == false)
                {
                    Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                    ZC.InitializeSystem("ZambaAfipDepDigiService");
                }

                int userId = 1048;
                SignPDFController SP = new SignPDFController();
                SP.GetLegajosAllService(userId);
                SP = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        protected override void OnStop()
        {
        }




    }
}
