using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba;
using System.Web;
using Zamba.Core;
using Zamba.Core.WF.WF;
using Zamba.Services;
using ZambaWeb.RestApi.Controllers.Class;
using static ZambaWeb.RestApi.Controllers.Class.EmailData;

namespace ZambaWeb.RestApi.Controllers.Web
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Email")]
    public class EmailController : ApiController
    {

        /// <summary>
        /// Gestiona el envio de un correo desde la Web.
        /// </summary>
        /// <param name="emailData">Datos del correo</param>
        /// <returns>Verdadero si fue enviado el correo, caso contrario, falso,</returns>
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [RestAPIAuthorize(OverrideAuthorization = true)]
        [AllowAnonymous]
        [Route("SendEmail")]
        public bool SendEmail(EmailData emailData)
        {
            string body = emailData.MessageBody;
            

            SMail sMail;
            ZOptBusiness ZOptB;
            WFTaskBusiness WFT;
            SendMailConfig mail = null;

            try
            {
                sMail = new SMail();
                ZOptB = new ZOptBusiness();
                WFT = new WFTaskBusiness();

                if (emailData.AddLink)
                {
                    bool Flag = false;
                    foreach (IdInfo item in emailData.Idinfo)
                    {
                        TaskResult task = (TaskResult)WFT.GetTaskByDocIdAndDocTypeId(item.DocId, item.DocTypeid);

                        if (task == null)
                        {
                            task = new TaskResult() { ID = item.DocId, DocTypeId = item.DocTypeid, TaskId = 0 };
                            var result = new Results_Business().GetResult(item.DocId, item.DocTypeid, false); ;

                            task.Name = result.Name;
                            task.Indexs = result.Indexs;
                        }

                        AllObjects.Tarea = task;

                        string PathButtonTemplates = ZOptBusiness.GetValueOrDefault("MailBodyButtonsTemplate", @"c:\Prueba\template.html");
                        String messageBodyLinkButton = AllObjects.FuncionesComunes.get_LeerArchivo(PathButtonTemplates);

                        if (String.IsNullOrEmpty(messageBodyLinkButton))
                        {
                            string LinkPublico = AllObjects.FuncionesComunes.get_ObtenerLinkPublicoWeb(task.Name);

                            if (!body.Contains("<br>Se te ha enviado el siguiente link al documento: <br><br>"))
                            {
                                messageBodyLinkButton = "<br>Se te ha enviado el siguiente link al documento: <br><br>" + LinkPublico;
                                body += messageBodyLinkButton;
                            }
                            else
                            {
                                messageBodyLinkButton = "<br><br>" + LinkPublico;
                                body += messageBodyLinkButton;
                                Flag = true;
                            }
                        }
                        else
                        {
                            messageBodyLinkButton = messageBodyLinkButton.Replace("{{Zamba.Body}}", body);
                            messageBodyLinkButton = TextoInteligente.GetValueFromZvarOrSmartText(messageBodyLinkButton, task);
                            body = messageBodyLinkButton;
                        }

                    }

                    if (Flag)
                        body = body.Replace("Se te ha enviado el siguiente link al documento: ", "Se te ha enviado los siguientes links al documento: ");

                }

                mail = NewSendMailConfig(emailData, body);
                mail.EnableSsl = mail.EnableSsl;

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "[SendEmail]: Lista Idinfo: " + emailData.Idinfo.Count.ToString());
                    foreach (IdInfo item in emailData.Idinfo)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "[SendEmail]: Iteracion: " + item.DocId.ToString() + " - " + item.DocTypeid.ToString());
                        mail.SourceDocId = item.DocId;
                        mail.SourceDocTypeId = item.DocTypeid;

                        if (emailData.AddLink != true)
                            mail.AttachFileNames = GetTaskDocument_ForEmail(emailData);

                    }
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "[SendEmail]: Revicion de archivos adjuntados:");
                    if (emailData.Base64StringArray != null)
                    {
                        foreach (Dictionary<string, string> item in emailData.Base64StringArray)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "[SendEmail]: Iteracion: " + item["Base64"].ToString() + " - " + item["FileName"].ToString());

                            var imageBytes = Convert.FromBase64String(item["Base64"]);
                            var stream = new MemoryStream(imageBytes);
                            var attachment = new System.Net.Mail.Attachment(stream, item["FileName"]);

                            mail.Attachments.Add(attachment);
                        }
                    }
                

                if (sMail.SendMail(mail, emailData.MailPathVariable))
                {
                    var TipoMail = MessagesBusiness.GetMessage(MailTypes.NetMail);
                    TipoMail.De = mail.From;
                    TipoMail.Subject = mail.Subject;
                    TipoMail.CC = mail.Cc;
                    TipoMail.CCO = mail.Cco;
                    TipoMail.Body = mail.Body;
                    TipoMail.MailTo = mail.MailTo;

                    MessagesBusiness.SaveHistory(TipoMail, emailData.Idinfo[0].DocId, emailData.Idinfo[0].DocTypeid);

                    return true;
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]:" + ex.Message);

                throw ex;
            }
            finally
            {
                if (mail != null)
                    mail.Dispose();
            }

            return false;
        }

        /// <summary>
        /// Configura los parametros del Mail a enviar
        /// </summary>
        /// <param name="emailData">Datos del correo</param>
        /// <param name="body">Cuerpo del mensaje</param>
        /// <returns>Un Correo parametrizado</returns>
        public SendMailConfig NewSendMailConfig(EmailData emailData, string body)
        {
            string user = string.Empty;
            string pass = string.Empty;
            string from = string.Empty;
            string port = string.Empty;
            string smtp = string.Empty;
            bool enableSsl = false;

            try
            {
                Zamba.Core.ZOptBusiness ZOptB = new Zamba.Core.ZOptBusiness();
                SendMailConfig ConfigMail = new SendMailConfig();

                if (ZOptB.GetValue("UseEmailConfigFromAD") != null && ZOptB.GetValue("UseEmailConfigFromAD").ToLower() == "true")
                {
                    ConfigMail.MailType = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Type;
                    ConfigMail.From = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Mail;
                    ConfigMail.UserName = Zamba.Membership.MembershipHelper.CurrentUser.eMail.UserName;
                    ConfigMail.Password = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Password;
                    ConfigMail.SMTPServer = Zamba.Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP;
                    ConfigMail.Port = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Puerto.ToString();
                }
                else
                {
                    if (ZOptB.GetValue("WebView_SendBySMTP") != null && ZOptB.GetValue("WebView_SendBySMTP").ToLower() == "true")
                    {
                        user = ZOptB.GetValue("WebView_UserSMTP");
                        pass = ZOptB.GetValue("WebView_PassSMTP");
                        from = ZOptB.GetValue("WebView_FromSMTP");
                        port = ZOptB.GetValue("WebView_PortSMTP");
                        smtp = ZOptB.GetValue("WebView_SMTP");
                        bool.TryParse(ZOptB.GetValue("WebView_SslSMTP"), out enableSsl);
                    }
                    else if (Zamba.Membership.MembershipHelper.CurrentUser.eMail.Type == MailTypes.NetMail)
                    {
                        user = Zamba.Membership.MembershipHelper.CurrentUser.eMail.UserName;
                        pass = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Password;
                        from = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Mail;
                        port = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Puerto.ToString();
                        smtp = Zamba.Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP;
                        enableSsl = Zamba.Membership.MembershipHelper.CurrentUser.eMail.EnableSsl;
                    }
                    else if (ConfigurationManager.AppSettings["WebView_SendBySMTP"] != string.Empty)
                    {
                        user = ConfigurationManager.AppSettings["WebView_UserSMTP"];
                        pass = ConfigurationManager.AppSettings["WebView_PassSMTP"];
                        from = ConfigurationManager.AppSettings["WebView_FromSMTP"];
                        port = ConfigurationManager.AppSettings["WebView_PortSMTP"];
                        smtp = ConfigurationManager.AppSettings["WebView_SMTP"];
                        bool.TryParse(ConfigurationManager.AppSettings["WebView_SslSMTP"], out enableSsl);
                    }

                    ConfigMail.MailType = MailTypes.NetMail;
                    ConfigMail.UserName = user;
                    ConfigMail.Password = pass;
                    ConfigMail.From = from;
                    ConfigMail.Port = port;
                    ConfigMail.SMTPServer = smtp;
                }

                ConfigMail.MailTo = emailData.MailTo;
                ConfigMail.Cc = emailData.CC;
                ConfigMail.Cco = emailData.CCO;
                ConfigMail.Subject = emailData.Subject;

                if (emailData.AddLink != true)
                    ConfigMail.AttachFileNames = GetTaskDocument_ForEmail(emailData);


                ConfigMail.EnableSsl = enableSsl;
                ConfigMail.UserId = emailData.userid;
                ConfigMail.IsBodyHtml = true;
                ConfigMail.SaveHistory = MessagesBusiness.IsEmailHistoryEnabled();

                #region ConfigMail.Body

                if (body != null)
                {
                    if (body.Contains("</") == false)
                        body = body.Replace(System.Environment.NewLine, "<br/>");
                    else
                        body = body.Replace(System.Environment.NewLine, "");

                    ConfigMail.Body = body;
                }
                else
                    ConfigMail.Body = "<br>";

                #endregion

                return ConfigMail;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]:" + ex.Message);

                throw ex;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]

        [RestAPIAuthorize(OverrideAuthorization = true)]
        [AllowAnonymous]
        [Route("SendZipByMail")]
        public bool SendZipByMail(EmailData zipdata, List<string> attachs, string AppTempPath)
        {
            ZipFile zip = new ZipFile();
            string user = string.Empty;
            string pass = string.Empty;
            string from = string.Empty;
            string port = string.Empty;
            string smtp = string.Empty;
            string to = zipdata.MailTo;
            string subject = zipdata.Subject;
            string cc = zipdata.CC;
            string cco = zipdata.CCO;
            string body = zipdata.MessageBody;
            bool enableSsl = false;
            bool isSender = false;
            SZOptBusiness ZOptB = new SZOptBusiness();
            string UseEmailConfigFromAD = ZOptB.GetValue("UseEmailConfigFromAD");

            try
            {


                if (UseEmailConfigFromAD != null && UseEmailConfigFromAD.ToLower() == "true")
                {
                    from = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Mail;
                    user = Zamba.Membership.MembershipHelper.CurrentUser.eMail.UserName;
                    pass = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Password;
                    smtp = Zamba.Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP;
                    port = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Puerto.ToString();
                }
                else
                {
                    if (ZOptB.GetValue("WebView_SendBySMTP") != null && ZOptB.GetValue("WebView_SendBySMTP").ToLower() == "true")
                    {
                        user = ZOptB.GetValue("WebView_UserSMTP");
                        pass = ZOptB.GetValue("WebView_PassSMTP");
                        from = ZOptB.GetValue("WebView_FromSMTP");
                        port = ZOptB.GetValue("WebView_PortSMTP");
                        smtp = ZOptB.GetValue("WebView_SMTP");
                        bool.TryParse(ZOptB.GetValue("WebView_SslSMTP"), out enableSsl);
                    }
                    else if (Zamba.Membership.MembershipHelper.CurrentUser.eMail.Type == MailTypes.NetMail)  // el usuario usa smtp?
                    {
                        user = Zamba.Membership.MembershipHelper.CurrentUser.eMail.UserName;
                        pass = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Password;
                        from = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Mail;
                        port = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Puerto.ToString();
                        smtp = Zamba.Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP;
                        enableSsl = Zamba.Membership.MembershipHelper.CurrentUser.eMail.EnableSsl;
                    }
                    else if (ConfigurationManager.AppSettings["WebView_SendBySMTP"] != string.Empty) // usar config smtp definida en la aplicacion?
                    {
                        user = ConfigurationManager.AppSettings["WebView_UserSMTP"];
                        pass = ConfigurationManager.AppSettings["WebView_PassSMTP"];
                        from = ConfigurationManager.AppSettings["WebView_FromSMTP"];
                        port = ConfigurationManager.AppSettings["WebView_PortSMTP"];
                        smtp = ConfigurationManager.AppSettings["WebView_SMTP"];
                        bool.TryParse(ConfigurationManager.AppSettings["WebView_SslSMTP"], out enableSsl);
                    }
                }

            if (attachs.Count > 0)
            {
                zip.Comment = zipdata.ZipName;

                if (zipdata.ZipInfo.Trim() != "" )
                    zip.Password = zipdata.ZipInfo ;

                string zipname = "\\temp\\" + zip.Comment + ".zip";
                var pathToZip = AppTempPath + zipname;

                    foreach (var result in attachs)
                    {
                        if (File.Exists(result))
                        {
                            zip.AddFile(result, "");
                        }
                    }

                    if (Directory.Exists(AppTempPath + "\\temp") == false)
                    {
                        Directory.CreateDirectory(AppTempPath + "\\temp");
                    }

                    zip.Save(pathToZip); //Guardo el zip temporalmente

                    if (zip.Count > 0)
                    {
                        //enviar email
                        try
                        {
                            using (var MailSetup = new SendMailConfig())
                            {
                                MailSetup.Subject = subject;
                                MailSetup.MailTo = to;
                                MailSetup.From = from;
                                MailSetup.Cc = cc;
                                MailSetup.Cco = cco;
                                MailSetup.Body = body;
                                MailSetup.MailType = MailTypes.NetMail;
                                MailSetup.UserName = user;
                                MailSetup.Password = pass;
                                MailSetup.SMTPServer = smtp;
                                MailSetup.Port = port;
                                MailSetup.IsBodyHtml = true;
                                MailSetup.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID; // emailData.userid;
                                MailSetup.EnableSsl = enableSsl;
                                MailSetup.SaveHistory = MessagesBusiness.IsEmailHistoryEnabled();
                                MailSetup.ZipAttaches = new System.Net.Mail.Attachment(pathToZip);

                                SMail sMail = new SMail();

                                if (sMail.SendMail(MailSetup))
                                {
                                    var TipoMail = MessagesBusiness.GetMessage(MailTypes.NetMail);
                                    TipoMail.De = MailSetup.From;
                                    TipoMail.Subject = MailSetup.Subject;
                                    TipoMail.CC = MailSetup.Cc;
                                    TipoMail.CCO = MailSetup.Cco;
                                    TipoMail.Body = MailSetup.Body;
                                    TipoMail.MailTo = MailSetup.MailTo;

                                    MessagesBusiness.SaveHistory(TipoMail, zipdata.Idinfo[0].DocId, zipdata.Idinfo[0].DocTypeid);
                                    return true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                            Console.WriteLine(ex.ToString());
                            throw ex;
                        }
                    }
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (zip != null)
                {
                    zip.Dispose();
                    zip = null;
                }
            }

            return false;
        }

        /// <summary>
        /// Prepara una cadena de caracteres con Fecha y Hora con el formato DDMMYYYY_HHMMSS. Destinado al nombramiento de archivos.
        /// </summary>
        /// <returns></returns>
        private string ArmDateTime_ToString()
        {
            return DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_" +
                DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [RestAPIAuthorize(OverrideAuthorization = true)]
        [AllowAnonymous]
        [Route("GetTaskDocument")]
        public List<string> GetTaskDocument(EmailData Data)
        {
            List<string> AllPaths = new List<string>();
            WFTaskBusiness WTB = new WFTaskBusiness();
            SResult SResult = new SResult();
            try
            {
                for (int i = 0; i < Data.Idinfo.Count(); i++)
                {


                    var localResult = SResult.GetResult(Data.Idinfo[i].DocId, Data.Idinfo[i].DocTypeid, false);

                    String path;
                    //ver cuando el volumen es -2
                    if (!string.IsNullOrEmpty(localResult.FullPath) && System.IO.Path.HasExtension(localResult.FullPath))
                    {
                        Results_Business RB = new Results_Business();
                        path = RB.GetTempFileFromResult(localResult, true);
                        RB = null;
                        AllPaths.Add(path);

                    }
                }

                return AllPaths;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetTaskDocument")]
        public List<string> GetTaskDocument_ForEmail(EmailData Data)
        {
            List<string> AllPaths = new List<string>();
            WFTaskBusiness WTB = new WFTaskBusiness();
            SResult SResult = new SResult();
            try
            {
                for (int i = 0; i < Data.Idinfo.Count(); i++)
                {
                    var localResult = SResult.GetResult(Data.Idinfo[i].DocId, Data.Idinfo[i].DocTypeid, false);

                    if (!string.IsNullOrEmpty(localResult.FullPath) && System.IO.Path.HasExtension(localResult.FullPath))
                    {
                        Results_Business RB = new Results_Business();
                        String path = RB.GetTempFileFromResult(localResult, false); //GetPreview en false
                        RB = null;
                        AllPaths.Add(path);
                    }
                }
                return AllPaths;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [RestAPIAuthorize(OverrideAuthorization = true)]
        [AllowAnonymous]
        [Route("SendZipMail")]
        public bool SendZipMail(EmailData zipData)
        {
            List<string> AllPaths = new List<string>();
            WFTaskBusiness WTB = new WFTaskBusiness();
            SResult SResult = new SResult();
            List<string> attachs = GetTaskDocument_ForEmail(zipData);

            if (attachs != null && attachs.Count > 0)
            {

                IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
                var isSentZipMail = SendZipByMail(zipData, attachs, Zamba.Membership.MembershipHelper.AppTempPath);
                return isSentZipMail;

            }
            return false;
        }

        public class EmailObject
        {
            public long DocId { get; set; }

            public long DocTypeId { get; set; }



        }




        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [RestAPIAuthorize(OverrideAuthorization = true)]
        [AllowAnonymous]
        [Route("getListOfLinks")]
        public List<string> getListOfLinks(List<EmailObject> emailobject)
        {
            ITaskResult TaskResult;
            STasks Stasks = new STasks();
            RightsBusiness RB = new RightsBusiness();
            List<string> AllPaths = new List<string>();
            DataSet dt = new DataSet();

            NewsBusiness NB = new NewsBusiness();
            try
            {
                foreach (var email in emailobject)
                {
                    string path = null;
                    string mode = null;
                    TaskResult = Stasks.GetTaskByDocId(email.DocId);


                    if (TaskResult != null)
                    {
                        //Int64 doctypeid = Stasks.GetDocTypeId(TaskResult.TaskId);
                        if (TaskResult.TaskId != 0)
                        {
                            if (RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Use, TaskResult.StepId))
                                path = "/Views/WF/TaskViewer.aspx?taskid=" + TaskResult.TaskId + "&docid=" + email.DocId + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID;
                            else
                                path = "/Views/Search/DocViewer.aspx?docid=" + email.DocId + "&doctype=" + email.DocTypeId + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID;
                        }
                        else
                        {
                            NB.SetRead(email.DocTypeId, email.DocId);
                            path = "/Views/Search/DocViewer.aspx?docid=" + email.DocId + "&doctype=" + email.DocTypeId + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID;
                        }

                    }
                    else
                    {
                        NB.SetRead(email.DocTypeId, email.DocId);
                        path = "/Views/Search/DocViewer.aspx?docid=" + email.DocId + "&doctype=" + email.DocTypeId + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID;

                    }

                    AllPaths.Add(path);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                NB = null;
            }
            return AllPaths;
        }



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [RestAPIAuthorize(OverrideAuthorization = true)]
        [AllowAnonymous]
        [Route("getIFAnyTaskHasFile")]
        public bool getIFAnyTaskHasFile(EmailData zipData)
        {
            List<string> partialAttachs = new List<string>();
            List<string> attachs = GetTaskDocument_ForEmail(zipData);
            try
            {
                foreach (var result in attachs)
                {
                    if (File.Exists(result))
                    {
                        partialAttachs.Add(result);
                    }
                }

                if (partialAttachs.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
