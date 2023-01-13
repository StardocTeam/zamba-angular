using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization.Formatters;
using Zamba.Services.Remoting;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.WFExecution;
using Zamba.WFActivity.Regular;


namespace Zamba.RemotingServer
{

    
    public partial class frmMain : Form 
    {
        
        public frmMain()
        {
            InitializeComponent();
        }
        private static TcpChannel _TcpServerChannel = null;

      

      
        private void frmMain_Load(object sender, EventArgs e)
        {
            ZCore.InitializeSystem(ObjectTypes.RemoteInsert, new RulesInstance().GetWFActivityRegularAssembly());

            ThreadStart _start = new ThreadStart(ZambaPublish);
            Thread th = new Thread(_start);
            th.Start();
            this.Visible = false;
        }

        private void ZambaPublish()
        {
            
            ZTrace.WriteLineIf(ZTrace.IsInfo,"--------------------------------------------------------");
            ZTrace.WriteLineIf(ZTrace.IsInfo,"Zamba.RemotingServer Trace -- "+ DateTime.Now.ToString());
         
        
            WellKnownServiceTypeEntry remObjZambaRemoteClass = null;
         
            BinaryClientFormatterSinkProvider clientProvider = null;

            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();

            serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            ZTrace.WriteLineIf(ZTrace.IsInfo,"Creating channel properties");
            IDictionary props = new Hashtable();

            props["port"] = 12360;
            ZTrace.WriteLineIf(ZTrace.IsInfo,"Port: " + props["port"].ToString());

            props["typeFilterLevel"] = TypeFilterLevel.Full;
            ZTrace.WriteLineIf(ZTrace.IsInfo,"TypeFilterLevel: " + ((TypeFilterLevel)props["typeFilterLevel"]).ToString());

            try
            {
                foreach (IChannel rChannel in ChannelServices.RegisteredChannels)
                {
                    ChannelServices.UnregisterChannel(rChannel);
                }

                _TcpServerChannel = new TcpChannel(props, clientProvider, serverProvider);
                ZTrace.WriteLineIf(ZTrace.IsInfo,"Registring Channel");
                ChannelServices.RegisterChannel(_TcpServerChannel, false);
                ZTrace.WriteLineIf(ZTrace.IsInfo,"Channel Registered");

                ZTrace.WriteLineIf(ZTrace.IsInfo,"Creating WellKnownServiceTypeEntries");
                   remObjZambaRemoteClass = new WellKnownServiceTypeEntry(typeof(ZambaRemoteClass), "IZambaRemoteClass.rem", WellKnownObjectMode.Singleton);
             
                RemotingConfiguration.RegisterWellKnownServiceType(remObjZambaRemoteClass );
                ZTrace.WriteLineIf(ZTrace.IsInfo,"WellKnownServiceType ZambaRemoteClass Registered");

                
                ZTrace.WriteLineIf(ZTrace.IsInfo,"---Set channel---");


            }
            catch (Exception ex)
            {

                ZTrace.WriteLineIf(ZTrace.IsInfo,ex.ToString());
            }

            Console.ReadLine();
       
            ZTrace.WriteLineIf(ZTrace.IsInfo,"--------------------------------------------------------");
            ZTrace.WriteLineIf(ZTrace.IsInfo,"Listening... Zamba.RemotingServer Trace -- " + DateTime.Now.ToString());
        }

       

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
