using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using System.Data;
using Zamba.Core;
using Zamba.Framework;
using Zamba;

namespace Zamba.Web.Test.SendMail
{
    public partial class TestSendMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Testmail(object sender, EventArgs e)
        {
            try
            {
                ISendMailConfig mail = new SendMailConfig();

                UserBusiness UB = new UserBusiness();
                UB.ValidateLogIn(3, ClientType.Web);

                mail.UserId = 3;
                mail.MailType = MailTypes.NetMail;
                mail.SaveHistory = false;
                mail.MailTo = "legnani@stardoc.com.ar;" + ZOptBusiness.GetValueOrDefault("ServiceReportEmails", "soporte@stardoc.com.ar");
                mail.Subject = "Zamba - Informe AFIP Obtencion de Legajos";
                mail.Body = string.Join("\n", "Test de Envio");
                mail.IsBodyHtml = true;
                mail.LinkToZamba = false;

                MessagesBusiness.SendQuickMail(mail);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
}