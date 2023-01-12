using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Zamba.Core;

namespace Zamba.ImapWindowsService
{
    public partial class IMAP : ServiceBase
    {
        public IMAP()
        {
            InitializeComponent();
        }
        private Timer _timer;
        protected override void OnStart(string[] args)
        {
            ConfigurarTimer();
            Trace("Servicio de windows IMAP iniciado");
        }
        private void ConfigurarTimer()
        {
            _timer = new Timer();
            _timer.AutoReset = true;
            _timer.Interval = 60000;
            _timer.Enabled = true;
            _timer.Elapsed += new ElapsedEventHandler(TimerTick);
        }
        private void TimerTick(object source, ElapsedEventArgs e)
        {
            EjecutarServicioIMAP();
        }
        private void Trace(string mensaje)
        {

        }
        private void EjecutarServicioIMAP()
        {
            try
            {
                Trace("Ejecutando servicio IMAP...");
                parametrosRestAPI parametrosRestAPI = new parametrosRestAPI();
                parametrosRestAPI.URLRestAPI = ZOptBusiness.GetValueOrDefault("WebRestApiURL", "Http://Localhost/ZambaWeb.RestApi/api") + "/eInbox/InsertEmailsInZamba";
                // ZOptBusiness.GetValueOrDefault("ImapServiceUserId", "14984")
                parametrosRestAPI.BodyContent = "";
                parametrosRestAPI.UserId = int.Parse(ZOptBusiness.GetValueOrDefault("ImapServiceUserId", "14984"));
                parametrosRestAPI.BearerToken = "";
                parametrosRestAPI.Headers.Add("", "");
                parametrosRestAPI.Method = "GET";
                string Response = CallRestAPI(parametrosRestAPI);
            }
            catch (Exception ex)
            {
                Trace("Ocurrio un error " + ex.Message);
            }
        }


        private class parametrosRestAPI
        {
            public string ToBase64(string Texto)
            {
                byte[] bytes;
                bytes = ASCIIEncoding.ASCII.GetBytes(Texto);
                return Convert.ToBase64String(bytes);
            }


            public string Base64ToString(string Texto)
            {
                byte[] bytes;
                bytes = Convert.FromBase64String(Texto);
                return Encoding.Default.GetString(bytes);
            }

            public string URLRestAPI;
            public string BodyContent;
            public string Method="POST";
            public Dictionary<string, string> Headers = new Dictionary<string, string>();
            public string BearerToken = "";
            public string Username;
            public string Password;
            public int UserId { get; set; }
            public string BasicAuthorization
            {
                get
                {
                    if (Username != "" & Password != "")
                        return "Basic " + ToBase64(Username + ":" + Password);
                    return "";
                }
                set
                {
                    string UserPassword;
                    UserPassword = Base64ToString(value);
                    Username = UserPassword.Split(':').First();
                    Password = UserPassword.Split(':').Last();
                }
            }

        }
        private string CallRestAPI(parametrosRestAPI RestAPIParameters)
        {
            HttpWebRequest Browser;
            WebResponse ResponseWeb;
            string ResponseContent="";
            Browser = (HttpWebRequest)WebRequest.Create(new Uri(RestAPIParameters.URLRestAPI));
            Browser.KeepAlive = true;
            foreach (KeyValuePair<string, string> Atributo in RestAPIParameters.Headers)
                Browser.Headers.Add(Atributo.Key, Atributo.Value);
            if (RestAPIParameters.BasicAuthorization != "")
                Browser.Headers.Add("Authorization", "Basic " +  RestAPIParameters.BasicAuthorization);
            if (RestAPIParameters.BearerToken != "")
                Browser.Headers.Add("Authorization", "Bearer " + RestAPIParameters.BearerToken);
            Browser.Accept = "application/json";
            Browser.ContentType = "application/json";
            Browser.Method = "GET";
            if (RestAPIParameters.Method == "POST")
            {
                Browser.Method = "POST";
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bytes = encoding.GetBytes(RestAPIParameters.BodyContent);
                Stream newStream = Browser.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();
            }
            ResponseWeb = Browser.GetResponse();
            var stream = ResponseWeb.GetResponseStream();
            var sr = new StreamReader(stream);
            ResponseContent = sr.ReadToEnd();
            return ResponseContent;
        }

        protected override void OnStop()
        {
            Trace("Servicio de windows IMAP finalizado");
        }        
    }
}
