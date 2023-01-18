using System;
using System.Collections.Generic;
using Zamba.Core;
using System.Collections;

namespace Zamba.Services
{
    public class SMail : IService, IDisposable
    {
        #region Attributes
        private MessagesBusiness mb;
        NotifyBusiness nb = null;
        private SSteps steps;
        #endregion

        #region Constructors

        public SMail()
        {
            mb = new MessagesBusiness();
            nb = new NotifyBusiness();
            steps = new SSteps();
        }
        #endregion

        #region IService Members
        ServicesTypes IService.ServiceType()
        {
            return ServicesTypes.Mail;
        }
        #endregion

        public List<IMailMessage> GetMessages(Int64 userId)
        {
            List<IMailMessage> MessagesList = null;
            ///TODO:
            return MessagesList;
        }
        public IMailMessage GetMessage(Int64 messageId)
        {
            IMailMessage Message = null;
            ///TODO:
            return Message;
        }
        public byte[] GetMessageFile(long id)
        {
            return MessagesBusiness.GetMessageFile(id);
        }
        public void SendMessage(IMailMessage message)
        {
            ///TODO
        }
        public void SaveMessageFile(byte[] file, long id)
        {
            MessagesBusiness.SaveMessageFile(file, id);
        }

        public bool SendMail(MailTypes eMailType, string from, string ProveedorSMTP, string Puerto, string UserName, string Password,
            string para, string cc, string cco, string asunto, string body, bool isBodyHtml, List<string> attachFileNames,
            long userId, ArrayList ArrayLinks, List<string> ImagesToEmbedPaths, string basemail, byte[] originalFile,
            string originalFileName, bool enableSsl, long docId, long docTypeId, List<BlobDocument> attaches, bool linkToZamba)
        {
            bool result;

            using (SendMailConfig mail = new SendMailConfig())
            {
                mail.ArrayLinks = ArrayLinks;
                mail.LoadAttaches(ref attaches);
                mail.AttachFileNames = attachFileNames;
                mail.Basemail = basemail;
                mail.Body = body;
                mail.Cc = cc;
                mail.Cco = cco;
                mail.EnableSsl = enableSsl;
                mail.From = from;
                mail.ImagesToEmbedPaths = ImagesToEmbedPaths;
                mail.IsBodyHtml = isBodyHtml;
                mail.MailTo = para;
                mail.MailType = eMailType;
                mail.OriginalDocument = originalFile;
                mail.OriginalDocumentFileName = originalFileName;
                mail.SaveHistory = MessagesBusiness.IsEmailHistoryEnabled();
                mail.Password = Password;
                mail.Port = Puerto;
                mail.SMTPServer = ProveedorSMTP;
                mail.UserName = UserName;
                mail.Subject = asunto;
                mail.UserId = userId;
                mail.UseWebService = CheckMailWsUsage();
                mail.SourceDocId = docId;
                mail.SourceDocTypeId = docTypeId;
                mail.LinkToZamba = linkToZamba;
                result = SendMail(mail);
            }

            return result;
        }

        public bool SendMail(SendMailConfig conf, string MailPathVariable = "")
        {
            return MessagesBusiness.SendMail(conf, MailPathVariable);
        }

        public bool SendMailbyZip(SendMailConfig conf, string MailPathVariable = "")
        {
            return MessagesBusiness.SendMailbyZip(conf, MailPathVariable);
        }

        public bool SendMailbyZipHistory(SendMailConfig conf, string MailPathVariable = "")
        {
            return MessagesBusiness.SendMailbyZipHistory(conf, MailPathVariable);
        }

        
        private bool CheckMailWsUsage()
        {
            ZOptBusiness zoptb = new ZOptBusiness();
            string sUseWS = zoptb.GetValue("UseWSSendMail");
            zoptb = null;
            bool useWS = (string.IsNullOrEmpty(sUseWS)) ? false : bool.Parse(sUseWS);
            return useWS;
        }

        /// <summary>
        /// Llama al ws para guardar el mail actual en historial
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cC"></param>
        /// <param name="cCO"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachs"></param>
        /// <param name="docId"></param>
        /// <param name="docTypeID"></param>
        /// <param name="userID"></param>
        /// <param name="exportPath"></param>
        /// <returns></returns>
        public bool SaveMailHistoryWS(string to, string cC, string cCO, string subject, string body, List<string> attachs, long docId, long docTypeID, long userID, string exportPath)
        {
            return mb.SaveMailHistoryWS(to, cC, cCO, subject, body, attachs, docId, docTypeID, userID, exportPath);
        }
        public bool SaveMailHistory(string to, string cC, string cCO, string subject, string body, List<string> attachs, long docId, long docTypeID, long userID, string exportPath)
        {
            return (bool)MessagesBusiness.SaveHistory(to, cC, cCO, subject, body, attachs, docId, docTypeID, userID, exportPath);
        }

        /// <summary>
        /// LLama al ws para obtener el archivo blob de un mail de historial
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public byte[] GetMailWS(long id, long userId)
        {
            return mb.GetMailWS(id, userId);
        }

        /// <summary>
        /// Obtiene el link que se incluye en el cuerpo de los mails para poder acceder a documentos y tareas
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="wfStepId"></param>
        /// <param name="docId"></param>
        /// <param name="docTypeId"></param>
        /// <returns></returns>
        public string MakeHtmlLink(Int64 taskId, Int64 wfStepId, Int64 docId, Int64 docTypeId)
        {
            return MessagesBusiness.MakeHtmlLink(taskId, wfStepId, docId, docTypeId,"");
        }

        public List<long> getAllSelectedUsers(GroupToNotifyTypes typeGroupToNotify, List<long> docIds)
        {
            return NotifyBusiness.getAllSelectedUsers(typeGroupToNotify, docIds);
        }
        
        public void Dispose()
        {
            mb = null;
            steps = null;
            nb = null;
        }
    }
}