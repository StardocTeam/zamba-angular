using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.ServiceModel.Web;
using System.Web;
using System.ServiceModel.Activation;
using System.Net.Mail;
using System.Threading;

namespace Zamba.AgentServer.WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UCMService" in code, svc and config file together.
    //[AspNetCompatibilityRequirements(RequirementsMode
    //  = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UCMService : IUCMService
    {
        public static Zamba.AgentServer.WS.UCMService UCM = null;

        public UCMDetail[] Details(string Client, string Year, string Month, string Day, string Hour)
        {
            List<UCMDetail> listofusers = new List<UCMDetail>();
            String query = " SELECT distinct CASE WHEN Type = 0 THEN 'Documental' WHEN  Type = 1 then 'Workflow' else 'Otro' END as [TipoLicencia], Client,Server, Base, UpdateDate,winuser as usuario  FROM UCMClientSset WHERE Client = '" + Client + "' and YEAR(UpdateDate) = '" + Year + "' and  DAY(UpdateDate) =  '" + Day + "'  and  MONTH(UpdateDate) =  '" + Month + "'  and  DATEPART(hour,UpdateDate) =  '" + Hour + "' order by winuser, updatedate";

            DataSet ds = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);

            Int32 count = 0;
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    count++;
                    listofusers.Add(new UCMDetail { Usuario = r["Usuario"].ToString(), Tipo = r["TipoLicencia"].ToString(), Count = count.ToString() });

                }

            }

            return listofusers.ToArray();
        }

        public void SendResume(String MailTo, String currentClient)
        {
            DataSet Clients;
            if (currentClient != null)
            {
                Clients = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, "select distinct Client from ucmclientsset where Client = '" + currentClient + "'");
            }
            else
            {
                Clients = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, "select distinct Client from ucmclientsset");
            }

            var smc = new Core.SendMailConfig();
            smc.From = "zamba@stardoc.com.ar";
            smc.SMTPServer = "smtp.gmail.com";
            smc.Port = "587";
            smc.UserName = "zamba@stardoc.com.ar";
            smc.Password = "StarDoc2014";
            smc.MailTo = "legnani@stardoc.com.ar;" + MailTo;
            var mf = new Zamba.Core.Message_Factory();
            
            foreach (DataRow r in Clients.Tables[0].Rows)
            {
                DataSet dsTemp;
                string details;
                Int64 usercount = 0;
                String newLine = Environment.NewLine;
                String mailBody;
                String client;
                String lastUpdate;
                String startTime;

                mailBody = "<div>Resumen de Uso de Licencias" + newLine + "</div><br />";
                client = r[0].ToString();
                object[] param = { client };
                mailBody += "<div><h2>" + "Cliente: " + client + newLine + "</h2></div><br />";

                lastUpdate = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, "select MAX(UPDATEDATE) from ucmclientsset where client = '" + client + "'").ToString();
                startTime = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, "select MIN(updatedate)  from ucmclientsset where client = '" + client + "'").ToString();
                details = "<div>" + "La ultima actualizacion de los registros ocurrio: " + lastUpdate + newLine + "</div><br />";
                dsTemp = Zamba.Servers.Server.get_Con().ExecuteDataset("ZSP_LICENSE_100_GetConnectionStatistics", param);
                details += "<div>" + "Los Registros analizados comprenden los horarios de 9hs a 18hs entre el: " + startTime + " y " + lastUpdate + " con el siguiente detalle: " + newLine + "</div><br />";
                foreach (DataRow R in dsTemp.Tables[0].Rows)
                {
                    details += "<div><b>" + "Año: " + R["Anio"].ToString() + newLine + "</b></div><br/>";
                    details += "<div>" + "Maximo de Conexiones Simultaneas: " + R["Maximo"].ToString() + newLine + "</div>";
                    details += "<div>" + "Minimo de Conexiones Simultaneas: " + R["Minimo"].ToString() + newLine + "</div>";
                    details += "<div>" + "Promedio de Conexiones Simultaneas: " + R["Promedio"].ToString() + newLine + "</div><br />";
                }
                mailBody += details + newLine;

                dsTemp = Zamba.Servers.Server.get_Con().ExecuteDataset("ZSP_LICENSE_100_GetMaxConcurrency", param);
                details = "<div>" + "Se detalla a continuacion los ultimos 100 registros de maxima concurrencia: " + newLine + "</div><br />";
                foreach (DataRow u in dsTemp.Tables[0].Rows)
                {
                    details += "<div> Fecha: " + u[0].ToString() + " Conexiones: " + u[1].ToString() + newLine + "</div>";
                }
                mailBody += details + newLine + newLine + newLine + "<br/><br/>";

                dsTemp = Zamba.Servers.Server.get_Con().ExecuteDataset("ZSP_LICENSE_100_GetLastConcurrency", param);
                details = "<div>" + "Se detalla a continuacion los ultimos 100 registros de concurrencia: " + newLine + "</div><br />";
                foreach (DataRow u in dsTemp.Tables[0].Rows)
                {
                    details += "<div> Fecha: " + u[0].ToString() + " Conexiones: " + u[1].ToString() + newLine + "</div>";
                }
                mailBody += details + newLine + newLine + newLine + "<br/><br/>";

                dsTemp = Zamba.Servers.Server.get_Con().ExecuteDataset("ZSP_LICENSE_100_GetLastUsers", param);
                details = "<div>" + "Usuarios registrados que han usado la aplicacion y su ultima fecha de conexion son: " + newLine + "</div><br />";
                foreach (DataRow u in dsTemp.Tables[0].Rows)
                {
                    usercount++;
                    details += "<div>" + usercount + ": " + u[0].ToString() + " - " + u[1].ToString() + newLine + "</div>";
                }
                mailBody += details + newLine + newLine + newLine + "<br/><br/>";

                dsTemp = Zamba.Servers.Server.get_Con().ExecuteDataset("ZSP_LICENSE_100_GetMaxConnectedUsers", param);
                if (dsTemp.Tables[0].Rows.Count > 0)
                    details = " (" + dsTemp.Tables[0].Rows[0][2].ToString() + ")";
                details = "<div>" + "Usuarios conectados en momento del pico máximo de licencias" + details + ": " + newLine + "</div><br />";
                usercount = 0;
                foreach (DataRow u in dsTemp.Tables[0].Rows)
                {
                    usercount++;
                    details += "<div>" + u[0].ToString() + " - " + u[1].ToString() + newLine + "</div>";
                }
                mailBody += details + newLine + newLine + newLine + "<br/><br/>";
  
                smc.Subject = "Informe de Licencias " + client;
                smc.Body = mailBody;
                mf.SendMailNet(smc);
            }

            mf = null;
            smc.Dispose();
            smc = null;
        }

        public void QuequeTasks()
        {
            // Create an object containing the information needed
            // for the task.
            TaskInfo ti = new TaskInfo("Reporte de licencias Diario", 42);

            // Queue the task and data.
            if (ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), ti))
            {
                Console.WriteLine("Main thread does some work, then sleeps.");

                // If you comment out the Sleep, the main thread exits before
                // the ThreadPool task has a chance to run.  ThreadPool uses 
                // background threads, which do not keep the application 
                // running.  (This is a simple example of a race condition.)
                Thread.Sleep(60000);

                Console.WriteLine("Main thread exits.");
                QuequeTasks();
            }
            else
            {
                Console.WriteLine("Unable to queue ThreadPool request.");
            }
        }

        // The thread procedure performs the independent task, in this case
        // formatting and printing a very simple report.
        //
        static void ThreadProc(Object stateInfo)
        {
            TaskInfo ti = (TaskInfo)stateInfo;
            Zamba.AgentServer.WS.UCMService serv = new WS.UCMService();
            serv.CheckForAutomaticResumeSend();

            Console.WriteLine(ti.Boilerplate, ti.Value);
        }

        public void CheckForAutomaticResumeSend()
        {
            DateTime? LastSendDate = GetLastSendDate();
            if (LastSendDate.HasValue == false || (DateTime.Now.AddDays(1) - LastSendDate.Value).Days > 1)
            {
                SendResume(string.Empty, null);
                SetLastSendDate();
            }
        }

        public DateTime? GetLastSendDate()
        {
            Zamba.Core.ZOptBusiness zopt = new Core.ZOptBusiness();
            string currentvalue = zopt.GetValue("LicenceResumeLastSendDate");

            if (currentvalue != null && currentvalue != "")
            {
                return DateTime.Parse(currentvalue);
            }
            else
            {
                return null;
            }
        }

        public void SetLastSendDate()
        {
            Zamba.Core.ZOptBusiness zopt = new Core.ZOptBusiness();
            string currentvalue = zopt.GetValue("LicenceResumeLastSendDate");

            if (currentvalue != null && currentvalue != "")
            {
                Zamba.Core.ZOptBusiness.Update("LicenceResumeLastSendDate", DateTime.Now.ToString());
            }
            else
            {
                Zamba.Core.ZOptBusiness.Insert("LicenceResumeLastSendDate", DateTime.Now.ToString());
            }
        }
    }

    [System.Runtime.Serialization.DataContract]
    public class UCMDetail
    {
        [System.Runtime.Serialization.DataMember]
        public string Usuario { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Tipo { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Count { get; set; }
    }

    // TaskInfo holds state information for a task that will be
    // executed by a ThreadPool thread.
    public class TaskInfo
    {
        // State information for the task.  These members
        // can be implemented as read-only properties, read/write
        // properties with validation, and so on, as required.
        public string Boilerplate;
        public int Value;

        // Public constructor provides an easy way to supply all
        // the information needed for the task.
        public TaskInfo(string text, int number)
        {
            Boilerplate = text;
            Value = number;
        }
    }
}
