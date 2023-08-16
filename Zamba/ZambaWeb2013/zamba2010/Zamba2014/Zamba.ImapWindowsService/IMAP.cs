using EmailRetrievalAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Zamba.Core;
using Zamba.FileTools;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Zamba.ImapWindowsService
{
    public partial class IMAP : ServiceBase
    {
        public IMAP()
        {
            InitializeComponent();
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.ImapWindowsService");
            }
        }
        private Timer _timer;
        private long userId;

        protected override void OnStart(string[] args)
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicio de windows IMAP iniciado");

            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando servicio IMAP...");
                userId = long.Parse(ZOptBusiness.GetValueOrDefault("ImapServiceUserId", "14984"));
                IUser user = GetUser(userId);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "User " + user.Name);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Membership.MembershipHelper.CurrentUser.ID " + Membership.MembershipHelper.CurrentUser.ID);
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ocurrio un error " + ex.Message);
            }

            ConfigurarTimer();
        }

        public Zamba.Core.IUser GetUser(long? userId)
        {
            try
            {
                UserBusiness UBR = new UserBusiness();
                var user = UBR.ValidateLogIn(userId.Value, ClientType.WebApi);
                UBR = null;
                return user;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        private void ConfigurarTimer()
        {
            _timer = new Timer();
            _timer.AutoReset = true;
            _timer.Interval = 60000;
            _timer.Enabled = true;
            _timer.Elapsed += new ElapsedEventHandler(TimerTick);
        }
        private bool Processing;
        private void TimerTick(object source, ElapsedEventArgs e)
        {
            try
            {
                if (!Processing)
                {
                    Processing = true;
                    EjecutarServicioIMAP();
                }
            }
            finally
            {
                Processing = false;
            }

            //ConsumeServiceRestApi RestClient = new ConsumeServiceRestApi();
            //var data = "{ \"ImapServer\": \"nasa1mail.mmc.com\", \"ImapPort\": 143, \"secureSocketOptions\": \"Auto\", \"ImapUsername\": \"MGD\\\\eseleme\\\\pedidoscaucion@marsh.com\", \"ImapPassword\": \"Octubre2023\", \"FolderName\": \"INBOX/F\", \"ExportFolderPath\": \"INBOX/A\" }";
            //RestClient.CallServiceRestApi("https://localhost:44365/swagger/api/imap/get","POST",data);
        }

        private void EjecutarServicioIMAP()
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha iniciado el proceso de insercion de correos.");
                userId = long.Parse(ZOptBusiness.GetValueOrDefault("ImapServiceUserId", "14984"));
                IUser user = GetUser(userId);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "User " + user.Name);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Membership.MembershipHelper.CurrentUser.ID " + Membership.MembershipHelper.CurrentUser.ID);

                //EL USUARIO LOGEADO EN LA APP DE ADMIN O EN EL SERVICIO SE DEBE ENVIAR

                ZImapClient e = new ZImapClient();

                //GetProcessInfo
                EmailBusiness EB = new EmailBusiness();
                List<IDTOObjectImap> imapProcessList = new List<IDTOObjectImap>();

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo Procesos");
                var processTable = EB.getAllImapProcesses();
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de Procesos " + processTable.Rows.Count);

                foreach (DataRow row in processTable.Rows)
                {
                    DTOObjectImap item = new DTOObjectImap(row);
                    imapProcessList.Add(item);
                }
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ejecutaran " + imapProcessList.Count + " proceso/s.");

                e.RequestEmailsAndInsertInZamba(imapProcessList, (Object)new Results_Business());

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw;
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
            public string Method = "POST";
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
            string ResponseContent = "";
            Browser = (HttpWebRequest)WebRequest.Create(new Uri(RestAPIParameters.URLRestAPI));
            Browser.KeepAlive = true;
            foreach (KeyValuePair<string, string> Atributo in RestAPIParameters.Headers)
                Browser.Headers.Add(Atributo.Key, Atributo.Value);
            if (RestAPIParameters.BasicAuthorization != "")
                Browser.Headers.Add("Authorization", "Basic " + RestAPIParameters.BasicAuthorization);
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
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicio de windows IMAP finalizado");
        }
    }
}
