using System;
using System.Collections.Generic;

using System.Text;
using System.Diagnostics ;
using Zamba;
using Zamba.Core;
using System.IO ;

using System.Runtime.Remoting ;
using System.Runtime.Remoting.Channels.Tcp ;
using System.Runtime.Remoting.Channels.Http ;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Runtime.Serialization.Formatters;

namespace Zamba.WfRemotingComponent
{
    public static class WfRemotingComponent
    {
        private static TcpChannel _TcpServerChannel = null;
        private static HttpChannel _HttpServerChannel = null;
        
        public static void InstanciarUsuario()
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo,"Obteniendo los permisos de usuario");
            UserBusiness.Rights.ValidateLogIn(Int32.Parse(UserPreferences.getValue("UserId", Sections.UserPreferences, 0)), ClientType.Service);
            ZTrace.WriteLineIf(ZTrace.IsInfo,"Los permisos se obtuvieron correctamente");
        }

        public static void  AddTrace ( string startPath)
        {
            try 
            {	        
                     Trace.Listeners.Add( new TextWriterTraceListener(startPath + @"\Exceptions\Trace Zamba Import Servidor " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt"));
                     Trace.AutoFlush = true;
  
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

        public static void WritePort()
        {
            if ( File.Exists(@".\RemotingPort.txt") == false)
	        {
              StreamWriter sw = new  StreamWriter(@".\RemotingPort.txt", false);
            sw.WriteLine("9001");
            sw.WriteLine("TCP");
            sw.WriteLine("N");
            sw.WriteLine("0");
            sw.Close();

	        }

        }

        public static void ReadPort( ref string  puerto , ref string  Tipo )
        {
            StreamReader sr = new StreamReader(@".\RemotingPort.txt");
            puerto = sr.ReadLine();
            Tipo = sr.ReadLine();                    
            sr.Close();
        }
    
        public static void AddToServers(string NombreServidor ,string IP, Int32 Puerto , Type  Objeto , Type  Interfaz , string  Descripcion , ref Int32 NewId )
        {
            try 
	        {
                RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
        		
                //todo que se fije si ya existe y lo actualice
                NewId = Zamba.Data.CoreData.GetNewID(IdTypes.ZServidores);
                string sql = @"Insert into ZServidores (Id,NombreServidor,IP,Puerto,Objeto,Interfaz,Descripcion,svrType)values(" + NewId.ToString().Trim() + ",'" + NombreServidor.Trim() + "','" + IP.Trim() + "'," + Puerto.ToString().Trim() + ",'" + Objeto.Name + "','" + Interfaz.Name + "','" + Descripcion.Trim() + "', 2 )";
                Zamba.Servers.Server.get_Con(false,true,false ).ExecuteNonQuery(CommandType.Text, sql);
                
	        }
	        catch (Exception ex)
	        {
        		 Zamba.Core.ZClass.raiseerror(ex);
	        }
        }

        public static void GetFromServers(ref Int32 Id, ref string NombreServidor, ref string IP, ref Int32 Puerto, ref string Objeto, string Interfaz)
        {
            try
            {
                string sql = @"select * from zservidores where Interfaz like '" + Interfaz + "' and svrType = 2 ";
                DataSet ds=  Zamba.Servers.Server.get_Con(false,true, false).ExecuteDataset(CommandType.Text, sql);
                if (ds!= null )
                {
                    Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString().Trim());
                   NombreServidor = ds.Tables[0].Rows[0]["NombreServidor"].ToString().Trim();
                   IP=  ds.Tables[0].Rows[0]["IP"].ToString();
                   Puerto = Convert.ToInt32 (  ds.Tables[0].Rows[0]["Puerto"].ToString().Trim());
                   Objeto =  ds.Tables[0].Rows[0]["Objeto"].ToString().Trim(); 
                }
            }
            catch (Exception ex)
            {
               Zamba.Core.ZClass.raiseerror(ex);
            }

        }
        
        public static string  GetIp()
        {
            try 
	        {	        
		        ZTrace.WriteLineIf(ZTrace.IsInfo,"IP:");
                string Host = Dns.GetHostName();
                IPHostEntry IPs  = Dns.GetHostEntry(Host);
                IPAddress[] Direcciones  = IPs.AddressList;
                ZTrace.WriteLineIf(ZTrace.IsInfo,Direcciones[0].ToString());
                return Direcciones[0].ToString();
	        }
	        catch (Exception ex)
            {
        		return "0.0.0.0";
	        }
        }

        public static void SetEngine()
        {
            
        }

        public static void SetChannel(string puerto , string  tipo ,  WellKnownServiceTypeEntry remObj  , string StartUpPath)
        {
            
            ZTrace.WriteLineIf(ZTrace.IsInfo,"Ingreso con 0 parametro");

            BinaryClientFormatterSinkProvider clientProvider = null;

            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();

            serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            IDictionary props = new Hashtable();

            props["port"] = Convert.ToInt32(puerto) ;

            props["typeFilterLevel"] = TypeFilterLevel.Full;

            if (tipo.ToString().ToUpper() == "TCP")
	        {
                
                try 
                {
                    _TcpServerChannel = new TcpChannel(props, clientProvider, serverProvider); 
                    ChannelServices.RegisterChannel(_TcpServerChannel,false );
                    
                }
                catch (Exception ex)
                {
                    Zamba.Core.ZClass.raiseerror(ex);  		
                }
            }
            if (tipo.ToString().ToUpper() == "HTTP")
	        {
                
           
                try 
	            {
                    _HttpServerChannel = new HttpChannel(props, clientProvider, serverProvider);
                    ChannelServices.RegisterChannel(_HttpServerChannel, false);
	            }
	            catch (Exception ex)
	            {
                    Zamba.Core.ZClass.raiseerror(ex);
	            }
	        }


                
                remObj = new WellKnownServiceTypeEntry(typeof(ZRuleServEngine), "ZRuleServEngine.rem", WellKnownObjectMode.Singleton);
                RemotingConfiguration.RegisterWellKnownServiceType(remObj);
                ZTrace.WriteLineIf(ZTrace.IsInfo,@"Servidor Registrado en: TCP, Puerto " + puerto);
                ZTrace.WriteLineIf(ZTrace.IsInfo,remObj.ToString());
	       

        }

        public static void RemoveFromServers(Int32 NewId )
        {
            try 
	        {
                foreach (IChannel rChannel in ChannelServices.RegisteredChannels)
                {
                    ChannelServices.UnregisterChannel(rChannel);
                }
 
                Zamba.Servers.Server.get_Con(false,true,false ).ExecuteNonQuery(CommandType.Text, "Delete ZServidores Where Id =" + NewId.ToString() + " and svrType = 2"  );
	        }
	        catch (Exception ex)
	        {
                Zamba.Core.ZClass.raiseerror(ex);
	        }

        }

        public static void RemoveAllFromServers()
        {
            try
            {
                foreach (IChannel rChannel in ChannelServices.RegisteredChannels)
                {
                    ChannelServices.UnregisterChannel(rChannel);
                }

                Zamba.Servers.Server.get_Con(false,true, false).ExecuteNonQuery(CommandType.Text, "Delete ZServidores where svrType = 2");
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }

        }

        public static List<ITaskResult>  ExecuteRule(Int64 ruleId , Int64 stepId, List< ITaskResult> results ) 
        {
            Int32 Id = 0;
            string NombreServidor = " ";
            string serverip = " ";
            Int32 port = 0;
            string objecto =" " ;
            string Interfaz = typeof( IZRemoting).Name ;
            IZRemoting  _Remoto = null;
            List<ITaskResult> lista = new List<ITaskResult> ();

            try
            {
               
                GetFromServers(ref Id, ref NombreServidor, ref serverip, ref port, ref objecto, Interfaz);
                
                foreach (IChannel rChannel in ChannelServices.RegisteredChannels)
                {
                    ChannelServices.UnregisterChannel(rChannel);                                      
                }
                                
                TcpClientChannel channel = new TcpClientChannel();
                
                ChannelServices.RegisterChannel(channel, false);
                _Remoto =  ( IZRemoting )Activator.GetObject(typeof(ZRuleServEngine), @"tcp://" + serverip.Trim() + ":" + port.ToString() + @"/" + objecto + ".rem");
                lista= _Remoto.ExecuteRule( ruleId , stepId , results );
                ChannelServices.UnregisterChannel(channel);
 
            }
            catch (RemotingException ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }

            return lista;            
        }
    }

   
}
