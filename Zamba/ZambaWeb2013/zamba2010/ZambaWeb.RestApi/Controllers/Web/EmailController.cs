using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba;
using Zamba.Core;
using Zamba.Core.WF.WF;
using Zamba.Data;
using Zamba.Services;
using ZambaWeb.RestApi.Controllers.Class;
using static System.Net.Mime.MediaTypeNames;
using static ZambaWeb.RestApi.Controllers.Class.EmailData;

namespace ZambaWeb.RestApi.Controllers.Web
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("Email")]
    public class EmailController : ApiController
    {

        /// <summary>
        /// Gestiona el envio de un correo desde la Web.
        /// </summary>
        /// <param name="emailData">Datos del correo</param>
        /// <returns>Verdadero si fue enviado el correo, caso contrario, falso,</returns>
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("SendEmail")]
        public bool SendEmail(EmailData emailData)
        {
            string body = emailData.MessageBody;
            SMail sMail;
            ZOptBusiness ZOptB;
            WFTaskBusiness WFT;
            SendMailConfig mail = null;
            bool RDO = false;

            try
            {
                sMail = new SMail();
                ZOptB = new ZOptBusiness();
                WFT = new WFTaskBusiness();

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "[SendEmail]: emailData.AddLink: " + emailData.AddLink.ToString());
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
                            //string LinkPublico = AllObjects.FuncionesComunes.get_ObtenerLinkPublicoWeb(task.Name);
                            string LinkPublico = MessagesBusiness.MakeHtmlLink(0, 0, item.DocId, item.DocTypeid, task.Name);
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

                //ZTrace.WriteLineIf(ZTrace.IsVerbose, "[SendEmail]: emailData.SendDocument: " + emailData.SendDocument);                

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

                mail.LinkToZamba = false;
                sMail.SendMail(mail, emailData.MailPathVariable);
                RDO = true;
                if (emailData.isDomail == "true" && VariablesInterReglas.ContainsKey("rutaDocumento"))
                {
                    string exportPath = string.Empty;
                    for (int i = 0; i < emailData.Idinfo.Count; i++)
                    {
                        Results_Business rb = new Results_Business();
                        SZOptBusiness ZO = new SZOptBusiness();

                        exportPath = Email_Factory.CreateExportFolder(Convert.ToInt32(emailData.Idinfo[i].DocId));
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Ruta exportacion: " + exportPath);

                        string MailTo = emailData.MailTo.ToString();
                        string cc = emailData.CC.ToString();
                        long DocID = emailData.Idinfo[i].DocId;

                        var from = ZO.GetValue("WebView_FromSMTP");
                        string MailPathVariable = "MailPathVariable";
                        Email_Factory.SaveMsgFromDomail(ref MailTo, ref cc, emailData.Subject, emailData.MessageBody, ref DocID, MailPathVariable, from, ref exportPath);


                        var GetMailPathVariable = VariablesInterReglas.get_Item(MailPathVariable);


                        var result =  rb.GetResult(Convert.ToInt64(emailData.Idinfo[i].DocId), emailData.Idinfo[i].DocTypeid, true);
                        rb.ReplaceDocument(ref result, GetMailPathVariable.ToString(), false, null);
                    }
                }
                  

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]:" + ex.Message);

                throw ;
            }
            finally
            {
                if (mail != null)
                    mail.Dispose();
            }

            return RDO;
        }

        /// <summary>
        /// Gestiona el envio de un correo desde la Web.
        /// </summary>
        /// <param name="emailData">Datos del correo</param>
        /// <returns>Verdadero si fue enviado el correo, caso contrario, falso,</returns>
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("SendEmailForSearch")]
        public bool SendEmailForSearch(EmailData emailData)
        {
            string body = emailData.MessageBody;
            SMail sMail;
            ZOptBusiness ZOptB;
            WFTaskBusiness WFT;
            SendMailConfig mail = null;
            bool RDO = false;

            try
            {
                sMail = new SMail();
                ZOptB = new ZOptBusiness();
                WFT = new WFTaskBusiness();

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "[SendEmail]: emailData.AddLink: " + emailData.AddLink.ToString());
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
                            //string LinkPublico = AllObjects.FuncionesComunes.get_ObtenerLinkPublicoWeb(task.Name);
                            string LinkPublico = MessagesBusiness.MakeHtmlLink(0, 0, item.DocId, item.DocTypeid, task.Name);
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

                //ZTrace.WriteLineIf(ZTrace.IsVerbose, "[SendEmail]: emailData.SendDocument: " + emailData.SendDocument);                

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
                mail.LinkToZamba = false;
                Int64 DocIdSaveHistory = mail.SourceDocId;
                sMail.SendMail(mail, emailData.MailPathVariable);
                RDO = true;
                List<String> Attachs = new List<string>();
                foreach(Dictionary<String,String> FileAttach in emailData.Base64StringArray)
                {
                    Attachs.Add(FileAttach["FileName"]);
                }
                if (emailData.Idinfo.Count > 1)
                {
                    for (int i = 0; i < emailData.Idinfo.Count; i++)
                    {
                        if (DocIdSaveHistory != emailData.Idinfo[i].DocId)
                        {
                            MessagesBusiness.SaveHistory(emailData.MailTo, emailData.CC, emailData.CCO, emailData.Subject, emailData.messageBody, Attachs, emailData.Idinfo[i].DocId, emailData.Idinfo[i].DocTypeid, emailData.Userid, String.Empty, "", emailData.MailTo);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]:" + ex.Message);

                throw ;
            }
            finally
            {
                if (mail != null)
                    mail.Dispose();
            }

            return RDO;
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
                    var smtpConfig = new EmailBusiness().GetSMPTConfig();
                    if (smtpConfig != null)
                    {
                        user = smtpConfig.User;
                        pass = smtpConfig.Pass;
                        from = smtpConfig.From;
                        port = smtpConfig.Port;
                        smtp = smtpConfig.MailServer;
                        enableSsl = smtpConfig.EnableSSL;
                    }
                    else if (ZOptB.GetValue("WebView_SendBySMTP") != null && ZOptB.GetValue("WebView_SendBySMTP").ToLower() == "true")
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

                ConfigMail.EnableSsl = enableSsl;
                ConfigMail.UserId = emailData.userid;
                ConfigMail.IsBodyHtml = true;
                ConfigMail.SaveHistory = MessagesBusiness.IsEmailHistoryEnabled();
                ConfigMail.LinkToZamba = emailData.AddLink;

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

                throw ;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
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
            bool RDO = false;

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
                    var smtpConfig = new EmailBusiness().GetSMPTConfig();
                    if (smtpConfig != null)
                    {
                        user = smtpConfig.User;
                        pass = smtpConfig.Pass;
                        from = smtpConfig.From;
                        port = smtpConfig.Port;
                        smtp = smtpConfig.MailServer;
                        enableSsl = smtpConfig.EnableSSL;
                    }
                    else if (ZOptB.GetValue("WebView_SendBySMTP") != null && ZOptB.GetValue("WebView_SendBySMTP").ToLower() == "true")
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

                //TO DO: Si hay diferentes ZIPs por cada tarea, hay que asignarles sus respectivos ZIPs
                if (attachs.Count > 0)
                {
                    zip.Comment = zipdata.ZipName + "_" + ArmDateTime_ToString();
                    zip.Password = zipdata.ZipPassword;
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

                    //TO DO: Si hay diferentes ZIPs por cada tarea, hay que asignarles sus respectivos ZIPs
                    if (zip.Count > 0)
                    {
                        try
                        {
                            using (SendMailConfig MailSetup = new SendMailConfig())
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

                                var SenMail = sMail.SendMailbyZip(MailSetup);
                                RDO = true;
                                if (SenMail)
                                {
                                    foreach (IdInfo item in zipdata.Idinfo)
                                    {
                                        MailSetup.SourceDocId = item.DocId;
                                        MailSetup.SourceDocTypeId = item.DocTypeid;

                                        sMail.SendMailbyZipHistory(MailSetup);

                                    }
                                }


                            }
                        }
                        catch (Exception ex)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                            Console.WriteLine(ex.ToString());
                            throw ;
                        }
                    }
                }

                return RDO;
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

            return RDO;
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
        [AllowAnonymous]
        [Route("GetTaskDocument")]
        public List<string> GetTaskDocument(EmailData Data)
        {
            List<string> AllPaths = new List<string>();
            WFTaskBusiness WTB = new WFTaskBusiness();
            SResult SResult = new SResult();
          
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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetTaskDocument")]
        public List<string> GetTaskDocument_ForEmail(EmailData Data)
        {
            List<string> AllPaths = new List<string>();
            WFTaskBusiness WTB = new WFTaskBusiness();
            SResult SResult = new SResult();
           
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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("DownloadZip")]
        public IHttpActionResult DownloadZip(EmailData zipData)
        {
            try
            {
                List<string> attachs = GetTaskDocument_ForEmail(zipData);
                byte[] zipBytes = null;


                if (attachs != null && attachs.Count > 0)
                {
                    var docTypeIds = zipData.Idinfo.Select(i => i.DocTypeid).Distinct();
                    DocTypesBusiness DTB = new DocTypesBusiness();

                    string zipNameEntities = String.Empty;
                    foreach (var id in docTypeIds)
                    {
                        zipNameEntities += DTB.GetDocTypeName(id) + "-";
                    }

                    ZipFile zip = new ZipFile();
                    zip.Comment = zipNameEntities + DateTime.Now.ToString("dd-MM-yyyy-H-mm-ss");
                    string zipname = zip.Comment + ".zip";
                    var pathToZip = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\" + zipname;

                    foreach (var result in attachs)
                    {
                        if (File.Exists(result))
                        {
                            zip.AddFile(result, "");
                        }
                    }

                    if (zip.Count() > 0)
                    {
                        if (Directory.Exists(Zamba.Membership.MembershipHelper.AppTempPath + "\\temp") == false)
                        {
                            Directory.CreateDirectory(Zamba.Membership.MembershipHelper.AppTempPath + "\\temp");
                        }

                        zip.Save(pathToZip);
                        zipBytes = FileEncode.Encode(pathToZip);
                        DownloadDataResponse DD = new DownloadDataResponse();
                        DD.fileName = zipname;
                        DD.data = zipBytes;
                        DD.missingAttachment = zipData.Idinfo.Count() > zip.Count();
                        var jsonDD = JsonConvert.SerializeObject(DD);
                        return Ok(jsonDD);
                    }
                    else
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se encuentran disponibles los archivos adjuntos")));
                    }
                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Las tareas no tienen archivos adjuntos")));
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]:" + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el recurso")));
            }

        }

        public class EmailObject
        {
            public long DocId { get; set; }

            public long DocTypeId { get; set; }



        }




        [System.Web.Http.AcceptVerbs("GET", "POST")]
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
                                path = "/Views/WF/TaskViewer.aspx?doctypeid=" + email.DocTypeId.ToString() + "&taskid=" + TaskResult.TaskId + "&docid=" + email.DocId + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID;
                            else
                                path = "/Views/Search/DocViewer.aspx?docid=" + email.DocId + "&doctype=" + email.DocTypeId + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID;
                        }
                        else
                        {
                            path = "/Views/Search/DocViewer.aspx?docid=" + email.DocId + "&doctype=" + email.DocTypeId + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID;
                        }

                    }
                    else
                    {

                        path = "/Views/Search/DocViewer.aspx?docid=" + email.DocId + "&doctype=" + email.DocTypeId + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID;

                    }

                    AllPaths.Add(path);
                }
            }
           
            finally
            {
                NB = null;
            }
            return AllPaths;
        }



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("getIFAnyTaskHasFile")]
        public bool getIFAnyTaskHasFile(EmailData zipData)
        {
            try
            {
                List<string> partialAttachs = new List<string>();
                //TO DO: Hay que devolver las rutas de los archivos existentes con sus respectivos DocIds y DocTypesIds, para el registro de multiples zip enviados.
                List<string> attachs = GetTaskDocument_ForEmail(zipData);

                if (attachs != null)
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
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]:" + ex.Message);
                return false;
            }
        }
    }
}
