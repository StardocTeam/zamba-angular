using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Runtime.Remoting;
using Zamba;
using Zamba.WfRemotingComponent;
using Zamba.Core;
using System.Threading;

namespace Zamba.WfRuleServer
{
    public partial class Server : ServiceBase
    {
        public WellKnownServiceTypeEntry remObj;
        private Int32 NewId = 0;
        bool flag = true;

        public Server()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //while (flag)
                //{

                //    System.Threading.Thread.Sleep(2000);

                //}

                
                string puerto = "9001"; //UserPreferences.getValue("Port", UserPreferences.Sections.Remoting, "6000");
                string Tipo = "IZRemoting";
                WfRemotingComponent.WfRemotingComponent.RemoveAllFromServers();

                WfRemotingComponent.WfRemotingComponent.AddTrace(Environment.CurrentDirectory);

                WfRemotingComponent.WfRemotingComponent.InstanciarUsuario();

                WfRemotingComponent.WfRemotingComponent.WritePort();

                WfRemotingComponent.WfRemotingComponent.ReadPort(ref  puerto, ref Tipo);

                WfRemotingComponent.WfRemotingComponent.AddToServers(Environment.MachineName, WfRemotingComponent.WfRemotingComponent.GetIp(), Convert.ToInt32(puerto), typeof(ZRuleServEngine), typeof(IZRemoting), Tipo, ref NewId);

                WfRemotingComponent.WfRemotingComponent.SetChannel(puerto.ToString(), Tipo, remObj, Environment.CurrentDirectory);
                
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

        protected override void OnStop()
        {
            CloseProcess();
        }

        protected override void OnPause()
        {
            CloseProcess();
        }

        protected override void OnContinue()
        {
            CloseProcess();
        }

        private void CloseProcess()
        {
            
            WfRemotingComponent.WfRemotingComponent.RemoveFromServers(NewId);
        }

    }
}
