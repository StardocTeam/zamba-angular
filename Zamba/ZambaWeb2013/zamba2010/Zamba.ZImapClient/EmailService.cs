using MimeKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MailKit.Security;
using System.Net;
using System.Buffers.Text;
using MimeKit.Encodings;
using MsgKit.Exceptions;
using MsgKit;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zamba.MailKit
{
    public class EmailService
    {

        public class DTOObjectImap
        {
            public int Id_proceso;
            public int Is_Active;
            public String Nombre_usuario;
            public String Domain;
            public String Nombre_proceso;
            public String Correo_electronico;
            public int Id_usuario;
            public String Password;
            public String Direccion_servidor;
            public int Puerto;
            public String Protocolo;
            public int Filtrado;
            public String Filtro_campo;
            public String Filtro_valor;
            public int Filtro_recientes;
            public int Filtro_noleidos;
            public String Exportar_adjunto_por_separado;
            public String Carpeta;
            public String CarpetaDest;
            public int Entidad;
            public int Enviado_por;
            public int Para;
            public int Cc;
            public String Cco;
            public int Asunto;
            public int Body;
            public int Fecha;
            public int Usuario_zamba;
            public int Codigo_mail;
            public int Tipo_exportacion;
            public int Autoincremento;
            public int GenericInbox;
        }

        public interface IImapConfig
        {
             string ImapServer { get; set; }
             int ImapPort { get; set; }
             string secureSocketOptions { get; set; }
             string ImapUsername { get; set; }
             string ImapPassword { get; set; }
             string FolderName { get; set; }
             string ExportedFolderPath { get; set; }

        }
        public class imapConfig// : IImapConfig
        {
            public string ImapServer { get; set; }
            public int ImapPort { get; set; }
            public string secureSocketOptions { get; set; }
            public string ImapUsername { get; set; }
            public string ImapPassword { get; set; }
            public string FolderName { get; set; }
            public string ExportFolderPath { get; set; }


        }
        public class ZMessage
        {
            public UniqueId uniqueId;
            public string Subject;
            public string From;
            public string To;
            public string Cc;
            public DateTimeOffset Date;
            public string Body;
            public List<ZMessageAttach> Attachs = new List<ZMessageAttach>();
            public string File;
            public string MessageId;
        }

        public class ZMessageAttach
        {
            public string File;
            public string Name;
        }
        public List<ZMessage> RetrieveEmails(imapConfig config)
        {

            List<string> log = new List<string>();
            List<ZMessage> messages = new List<ZMessage>();

            try
            {

                // Configure the certificate validation callback to trust the certificate
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

                using (var client = new ImapClient())
                {
                    // Ignore certificate validation for the IMAP connection
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    log.Add("Connect");
                    SecureSocketOptions secureSocketOptions = SecureSocketOptions.Auto;
                    switch (config.secureSocketOptions)
                    {
                        case "Auto":
                            secureSocketOptions = SecureSocketOptions.Auto;
                            log.Add("Auto");
                            break;
                        case "SslOnConnect":
                            secureSocketOptions = SecureSocketOptions.SslOnConnect;
                            log.Add("SslOnConnect");
                            break;
                        case "None":
                            secureSocketOptions = SecureSocketOptions.None;
                            log.Add("None");
                            break;
                    }

                    log.Add("config.ImapServer: " + config.ImapServer);
                    log.Add("config.ImapPort.Value: " + config.ImapPort);
                    client.Connect(config.ImapServer, config.ImapPort, secureSocketOptions);

                    log.Add("Authenticate" + config.ImapUsername);
                    client.Authenticate(config.ImapUsername, config.ImapPassword);

                    log.Add("GetFolder");
                    var folder = client.GetFolder(config.FolderName);
                    folder.Open(FolderAccess.ReadWrite);

                    var uids = folder.Search(SearchQuery.Not(SearchQuery.Deleted));

                    if (!Directory.Exists(config.ExportFolderPath))
                        Directory.CreateDirectory(config.ExportFolderPath);



                    foreach (var uid in uids)
                    {
                        try
                        {

                            var message = folder.GetMessage(uid);

                            // Check if the email has been previously retrieved
                            var isPreviouslyRetrieved = IsPreviouslyRetrieved(uid.ToString());
                            if (isPreviouslyRetrieved)
                                continue;

                            // Create a local email file with .eml extension
                            var filePath = Path.Combine(config.ExportFolderPath, $"{uid}.eml");
                            message.WriteTo(FormatOptions.Default, filePath);

                            //MemoryStream messageFileStream = new MemoryStream();
                            //message.WriteTo(FormatOptions.Default, messageFileStream);
                            //messageFileStream.Flush();

                            //                            ConvertEmlToMsg(filePath, filePath.Replace(".eml",".msg"));

//                            FileStream msgFileStream = File.Open(filePath.Replace(".eml", ".msg"), FileMode.Open);
                            FileStream msgFileStream = File.Open(filePath, FileMode.Open);
                            var msgmemoryStream = new MemoryStream();
                            msgFileStream.CopyTo(msgmemoryStream);
                            msgFileStream.Close();
                            
                            IMailFolder newfolder = folder;
                            try
                            {
                                newfolder = folder.GetSubfolder(config.ExportFolderPath);

                            }
                            catch (FolderNotFoundException)
                            {
                                folder.Create(config.ExportFolderPath, true);
                                newfolder = folder.GetSubfolder(config.ExportFolderPath);
                            }

                            // Move the email to the exported folder
                            folder.MoveTo(uid, newfolder);

                            log.Add($"Mail OK: {uid} - {message.Subject}");

                            ZMessage currentMessage = new ZMessage();
                            currentMessage.uniqueId = uid;
                            currentMessage.File = System.Convert.ToBase64String(msgmemoryStream.ToArray());
                            msgmemoryStream.Close();
                            currentMessage.Subject = message.Subject;
                            currentMessage.From = string.Join(";", message.From.Mailboxes.Select(x => x.Address).ToArray());
                            currentMessage.To = string.Join(";", message.To.Mailboxes.Select(x => x.Address).ToArray());
                            currentMessage.Cc = string.Join(";", message.Cc.Mailboxes.Select(x => x.Address).ToArray());
                            currentMessage.Date = message.Date;
                            currentMessage.Body = message.HtmlBody;
                            currentMessage.MessageId = message.MessageId;

                            foreach (MimeEntity attach in message.Attachments)
                            {
                                MemoryStream attachStream = new MemoryStream();

                                attach.WriteTo(FormatOptions.Default, attachStream);
                                currentMessage.Attachs.Add(new ZMessageAttach() { File = System.Convert.ToBase64String(attachStream.ToArray()), Name = attach.Headers["Content-Description"] });
                            }

                            messages.Add(currentMessage);

                        }
                        catch (Exception ex)
                        {
                            log.Add($"ZIMAP Mail ERROR : {uid} - {ex.ToString()}");
                            StreamWriter sw = new StreamWriter("Error.txt");
                            sw.WriteLine(string.Join(System.Environment.NewLine, log.ToArray()));
                            sw.Flush();
                            sw.Close();
                            sw.Dispose();
                        }
                    }

                    client.Disconnect(true);
                }

                if (messages.Count == 0)
                {
                    log.Add("No hay mensajes exportados");
                }
                return messages;
            }
            catch (System.Net.WebException ex)
            {
                log.Add($"ZIMAP WebException: {ex.ToString()}");
                StreamWriter sw = new StreamWriter("Error.txt");
                sw.WriteLine(string.Join(System.Environment.NewLine, log.ToArray()));
                sw.Flush();
                sw.Close();
                sw.Dispose();
                throw new Exception(string.Join(System.Environment.NewLine, log.ToArray()), ex);
            }
            catch (Exception ex)
            {
                log.Add($"ZIMAP ERROR : {ex.ToString()}");
                StreamWriter sw = new StreamWriter("Error.txt");
                sw.WriteLine(string.Join(System.Environment.NewLine, log.ToArray()));
                sw.Flush();
                sw.Close();
                sw.Dispose();
                throw new Exception(string.Join(System.Environment.NewLine, log.ToArray()), ex);
            }
        }

        private bool IsPreviouslyRetrieved(string uid)
        {
            // Check if the email with the given UID has been previously retrieved
            // Implement your logic here (e.g., check a database or a file)

            return false;
        }



        public static void ConvertEmlToMsg(Stream emlFile, Stream msgFile)
        {
            var eml = MimeMessage.Load(emlFile);
            var sender = new Sender(string.Empty, string.Empty);

            if (eml.From.Count > 0)
            {
                var mailAddress = (MailboxAddress)eml.From[0];
                sender = new Sender(mailAddress.Address, mailAddress.Name);
            }

            var representing = new Representing(string.Empty, string.Empty);
            if (eml.ResentSender != null)
                representing = new Representing(eml.ResentSender.Address, eml.ResentSender.Name);

            var msg = new Email(sender, representing, eml.Subject)
            {
                SentOn = eml.Date.UtcDateTime,
                InternetMessageId = eml.MessageId
            };

            using (var memoryStream = new MemoryStream())
            {
                eml.Headers.WriteTo(memoryStream);
                msg.TransportMessageHeadersText = Encoding.ASCII.GetString(memoryStream.ToArray());
            }

            switch (eml.Priority)
            {
                case MessagePriority.NonUrgent:
                    msg.Priority = MsgKit.Enums.MessagePriority.PRIO_NONURGENT;
                    break;
                case MessagePriority.Normal:
                    msg.Priority = MsgKit.Enums.MessagePriority.PRIO_NORMAL;
                    break;
                case MessagePriority.Urgent:
                    msg.Priority = MsgKit.Enums.MessagePriority.PRIO_URGENT;
                    break;
            }

            switch (eml.Importance)
            {
                case MessageImportance.Low:
                    msg.Importance = MsgKit.Enums.MessageImportance.IMPORTANCE_LOW;
                    break;
                case MessageImportance.Normal:
                    msg.Importance = MsgKit.Enums.MessageImportance.IMPORTANCE_NORMAL;
                    break;
                case MessageImportance.High:
                    msg.Importance = MsgKit.Enums.MessageImportance.IMPORTANCE_HIGH;
                    break;
            }

            foreach (var to in eml.To)
            {
                var mailAddress = (MailboxAddress)to;
                msg.Recipients.AddTo(mailAddress.Address, mailAddress.Name);
            }

            foreach (var cc in eml.Cc)
            {
                var mailAddress = (MailboxAddress)cc;
                msg.Recipients.AddCc(mailAddress.Address, mailAddress.Name);
            }

            foreach (var bcc in eml.Bcc)
            {
                var mailAddress = (MailboxAddress)bcc;
                msg.Recipients.AddBcc(mailAddress.Address, mailAddress.Name);
            }

            using (var headerStream = new MemoryStream())
            {
                eml.Headers.WriteTo(headerStream);
                headerStream.Position = 0;
                msg.TransportMessageHeadersText = Encoding.ASCII.GetString(headerStream.ToArray());
            }

            var namelessCount = 0;
            var index = 1;

            // This loops through the top-level parts (i.e. it doesn't open up attachments and continue to traverse).
            // As such, any included messages are just attachments here.
            foreach (var bodyPart in eml.BodyParts)
            {
                var handled = false;

                // The first text/plain part (that's not an attachment) is the body part.
                if (!bodyPart.IsAttachment && bodyPart is TextPart text)
                {
                    // Sets the first matching inline content type for body parts.

                    if (msg.BodyText == null && text.IsPlain)
                    {
                        msg.BodyText = text.Text;
                        handled = true;
                    }

                    if (msg.BodyHtml == null && text.IsHtml)
                    {
                        msg.BodyHtml = text.Text;
                        handled = true;
                    }

                    if (msg.BodyRtf == null && text.IsRichText)
                    {
                        msg.BodyRtf = text.Text;
                        handled = true;
                    }
                }

                // If the part hasn't previously been handled by "body" part handling
                if (!handled)
                {
                    var attachmentStream = new MemoryStream();
                    var fileName = bodyPart.ContentType.Name;
                    var extension = string.Empty;

                    if (bodyPart is MessagePart messagePart)
                    {
                        messagePart.Message.WriteTo(attachmentStream);
                        if (messagePart.Message != null)
                            fileName = messagePart.Message.Subject;

                        extension = ".eml";
                    }
                    else if (bodyPart is MessageDispositionNotification)
                    {
                        var part = (MessageDispositionNotification)bodyPart;
                        fileName = part.FileName;
                    }
                    else if (bodyPart is MessageDeliveryStatus)
                    {
                        var part = (MessageDeliveryStatus)bodyPart;
                        fileName = "details";
                        extension = ".txt";
                        part.WriteTo(FormatOptions.Default, attachmentStream, true);
                    }
                    else
                    {
                        var part = (MimePart)bodyPart;
                        part.Content.DecodeTo(attachmentStream);
                        fileName = part.FileName;
                    }

                    fileName = string.IsNullOrWhiteSpace(fileName)
                        ? $"part_{++namelessCount:00}"
                        : fileName;

                    if (!string.IsNullOrEmpty(extension))
                        fileName += extension;

                    var inline = bodyPart.ContentDisposition != null &&
                        bodyPart.ContentDisposition.Disposition.Equals("inline",
                            StringComparison.InvariantCultureIgnoreCase);

                    attachmentStream.Position = 0;

                    try
                    {
                        msg.Attachments.Add(attachmentStream, fileName, -1, inline, bodyPart.ContentId);
                    }
                    catch (MKAttachmentExists)
                    {
                        var tempFileName = Path.GetFileNameWithoutExtension(fileName);
                        var tempExtension = Path.GetExtension(fileName);
                        msg.Attachments.Add(attachmentStream, $"{tempFileName}({index}){tempExtension}", -1, inline, bodyPart.ContentId);
                        index += 1;
                    }
                }
            }

            msg.Save(msgFile);
        }

        public static void ConvertEmlToMsg(string emlFile, string msgFile)
        {
            var eml = MimeMessage.Load(emlFile);
            var sender = new Sender(string.Empty, string.Empty);

            if (eml.From.Count > 0)
            {
                var mailAddress = (MailboxAddress)eml.From[0];
                sender = new Sender(mailAddress.Address, mailAddress.Name);
            }

            var representing = new Representing(string.Empty, string.Empty);
            if (eml.ResentSender != null)
                representing = new Representing(eml.ResentSender.Address, eml.ResentSender.Name);

            var msg = new Email(sender, representing, eml.Subject)
            {
                SentOn = eml.Date.UtcDateTime,
                InternetMessageId = eml.MessageId
            };

            using (var memoryStream = new MemoryStream())
            {
                eml.Headers.WriteTo(memoryStream);
                msg.TransportMessageHeadersText = Encoding.ASCII.GetString(memoryStream.ToArray());
            }

            switch (eml.Priority)
            {
                case MessagePriority.NonUrgent:
                    msg.Priority = MsgKit.Enums.MessagePriority.PRIO_NONURGENT;
                    break;
                case MessagePriority.Normal:
                    msg.Priority = MsgKit.Enums.MessagePriority.PRIO_NORMAL;
                    break;
                case MessagePriority.Urgent:
                    msg.Priority = MsgKit.Enums.MessagePriority.PRIO_URGENT;
                    break;
            }

            switch (eml.Importance)
            {
                case MessageImportance.Low:
                    msg.Importance = MsgKit.Enums.MessageImportance.IMPORTANCE_LOW;
                    break;
                case MessageImportance.Normal:
                    msg.Importance = MsgKit.Enums.MessageImportance.IMPORTANCE_NORMAL;
                    break;
                case MessageImportance.High:
                    msg.Importance = MsgKit.Enums.MessageImportance.IMPORTANCE_HIGH;
                    break;
            }

            foreach (var to in eml.To)
            {
                var mailAddress = (MailboxAddress)to;
                msg.Recipients.AddTo(mailAddress.Address, mailAddress.Name);
            }

            foreach (var cc in eml.Cc)
            {
                var mailAddress = (MailboxAddress)cc;
                msg.Recipients.AddCc(mailAddress.Address, mailAddress.Name);
            }

            foreach (var bcc in eml.Bcc)
            {
                var mailAddress = (MailboxAddress)bcc;
                msg.Recipients.AddBcc(mailAddress.Address, mailAddress.Name);
            }

            using (var headerStream = new MemoryStream())
            {
                eml.Headers.WriteTo(headerStream);
                headerStream.Position = 0;
                msg.TransportMessageHeadersText = Encoding.ASCII.GetString(headerStream.ToArray());
            }

            var namelessCount = 0;
            var index = 1;

            // This loops through the top-level parts (i.e. it doesn't open up attachments and continue to traverse).
            // As such, any included messages are just attachments here.
            foreach (var bodyPart in eml.BodyParts)
            {
                var handled = false;

                // The first text/plain part (that's not an attachment) is the body part.
                if (!bodyPart.IsAttachment && bodyPart is TextPart text)
                {
                    // Sets the first matching inline content type for body parts.

                    if (msg.BodyText == null && text.IsPlain)
                    {
                        msg.BodyText = text.Text;
                        handled = true;
                    }

                    if (msg.BodyHtml == null && text.IsHtml)
                    {
                        msg.BodyHtml = text.Text;
                        handled = true;
                    }

                    if (msg.BodyRtf == null && text.IsRichText)
                    {
                        msg.BodyRtf = text.Text;
                        handled = true;
                    }
                }

                // If the part hasn't previously been handled by "body" part handling
                if (!handled)
                {
                    var attachmentStream = new MemoryStream();
                    var fileName = bodyPart.ContentType.Name;
                    var extension = string.Empty;

                    if (bodyPart is MessagePart messagePart)
                    {
                        messagePart.Message.WriteTo(attachmentStream);
                        if (messagePart.Message != null)
                            fileName = messagePart.Message.Subject;

                        extension = ".eml";
                    }
                    else if (bodyPart is MessageDispositionNotification)
                    {
                        var part = (MessageDispositionNotification)bodyPart;
                        fileName = part.FileName;
                    }
                    else if (bodyPart is MessageDeliveryStatus)
                    {
                        var part = (MessageDeliveryStatus)bodyPart;
                        fileName = "details";
                        extension = ".txt";
                        part.WriteTo(FormatOptions.Default, attachmentStream, true);
                    }
                    else
                    {
                        var part = (MimePart)bodyPart;
                        part.Content.DecodeTo(attachmentStream);
                        fileName = part.FileName;
                    }

                    fileName = string.IsNullOrWhiteSpace(fileName)
                        ? $"part_{++namelessCount:00}"
                        : fileName;

                    if (!string.IsNullOrEmpty(extension))
                        fileName += extension;

                    var inline = bodyPart.ContentDisposition != null &&
                        bodyPart.ContentDisposition.Disposition.Equals("inline",
                            StringComparison.InvariantCultureIgnoreCase);

                    attachmentStream.Position = 0;

                    try
                    {
                        msg.Attachments.Add(attachmentStream, fileName, -1, inline, bodyPart.ContentId);
                    }
                    catch (MKAttachmentExists)
                    {
                        var tempFileName = Path.GetFileNameWithoutExtension(fileName);
                        var tempExtension = Path.GetExtension(fileName);
                        msg.Attachments.Add(attachmentStream, $"{tempFileName}({index}){tempExtension}", -1, inline, bodyPart.ContentId);
                        index += 1;
                    }
                }
            }

            msg.Save(msgFile);
        }

    }
}
