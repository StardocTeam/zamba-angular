using MimeKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MailKit.Security;
using System.Net;

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
            public string? ImapServer { get; set; }
            public int? ImapPort { get; set; }
            public string? secureSocketOptions { get; set; }
            public string? ImapUsername { get; set; }
            public string? ImapPassword { get; set; }
            public string? FolderName { get; set; }
            public string? ExportedFolderPath { get; set; }

        }
        public class imapConfig// : IImapConfig
        {
            public string? ImapServer { get; set; }
            public int? ImapPort { get; set; }
            public string? secureSocketOptions { get; set; }
            public string? ImapUsername { get; set; }
            public string? ImapPassword { get; set; }
            public string? FolderName { get; set; }
            public string? ExportFolderPath { get; set; }


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

        public bool AreEqual(imapConfig lastConfig, imapConfig other)
        {
            return lastConfig != null && lastConfig.ImapServer == other.ImapServer &&
                   lastConfig.ImapPort == other.ImapPort &&
                   lastConfig.ImapUsername == other.ImapUsername &&
                   lastConfig.ImapPassword == other.ImapPassword &&
                   lastConfig.FolderName == other.FolderName &&
                   lastConfig.ExportFolderPath == other.ExportFolderPath;
        }

        static imapConfig lastConfig;
        public List<ZMessage> RetrieveEmails(imapConfig config, string tracefile)
        {
            List<string> log = new List<string>();
            List<ZMessage> messages = new List<ZMessage>();

            Boolean ConfigIsEquals = AreEqual(EmailService.lastConfig,config);

            EmailService.lastConfig = config;
            try
            {
                // Configure the certificate validation callback to trust the certificate
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

                using (var client = new ImapClient())
                {
                    // Ignore certificate validation for the IMAP connection
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    log.Add("Connecting...: " + config.ImapUsername + " - " + config.FolderName);
                    SecureSocketOptions secureSocketOptions = SecureSocketOptions.SslOnConnect;
                    switch (config.secureSocketOptions)
                    {
                        case "Auto":
                            secureSocketOptions = SecureSocketOptions.Auto;
                            if (!ConfigIsEquals) log.Add("secureSocketOptions: Auto");
                            break;
                        case "SslOnConnect":
                            secureSocketOptions = SecureSocketOptions.SslOnConnect;
                            if (!ConfigIsEquals) log.Add("secureSocketOptions: SslOnConnect");
                            break;
                        case "None":
                            secureSocketOptions = SecureSocketOptions.None;
                            if (!ConfigIsEquals) log.Add("secureSocketOptions: None");
                            break;
                    }

                    if (!ConfigIsEquals) log.Add("config.ImapServer: " + config.ImapServer);
                    if (!ConfigIsEquals) log.Add("config.ImapPort.Value: " + config.ImapPort.Value);
                    client.Connect(config.ImapServer, config.ImapPort.Value, secureSocketOptions);

                    if (!ConfigIsEquals) log.Add("Authenticate user/mail: " + config.ImapUsername);
                    
                    int count = Math.Max(0, config.ImapPassword.Length - 3); // Ensure count is non-negative
                    string xs = new string('x', count);

                    if (!ConfigIsEquals) log.Add("Authenticate password: " + config.ImapPassword.Substring(0,3) + xs);
                    client.Authenticate(config.ImapUsername, config.ImapPassword);

                    if (!ConfigIsEquals) log.Add("Source Folder: " + config.FolderName);
                    
                    var folder = client.GetFolder(config.FolderName);
                    folder.Open(FolderAccess.ReadWrite);

                    if (!ConfigIsEquals) log.Add("Destination Folder: " + config.ExportFolderPath);

                    var uids = folder.Search(SearchQuery.Not(SearchQuery.Deleted));

                    if (!Directory.Exists("Log\\" + config.ExportFolderPath))
                        Directory.CreateDirectory("Log\\" + config.ExportFolderPath);

                    Int64 messageCount = 0;
                    log.Add("Found " + uids.Count + " emails, exporting the first 10");

                    foreach (var uid in uids)
                    {
                        try
                        {
                            messageCount++;
                            if (messageCount > 10)
                            {
                                break;
                            }

                            var message = folder.GetMessage(uid);

                            // Check if the email has been previously retrieved
                            var isPreviouslyRetrieved = IsPreviouslyRetrieved(uid.ToString());
                            if (isPreviouslyRetrieved)
                                continue;

                            // Create a local email file with .eml extension
                            var filePath = Path.Combine("Log\\" + config.ExportFolderPath, $"{uid}.eml");
                            message.WriteTo(FormatOptions.Default, filePath);

                            //                            ConvertEmlToMsg(filePath, filePath.Replace(".eml",".msg"));
                            //                            FileStream msgFileStream = File.Open(filePath.Replace(".eml", ".msg"), FileMode.Open);

                            FileStream msgFileStream = File.Open(filePath, FileMode.Open);
                            var msgmemoryStream = new MemoryStream();
                            msgFileStream.CopyTo(msgmemoryStream);
                            msgFileStream.Close();

                            File.Delete(filePath);

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
                            currentMessage.From = string.Join(char.Parse(";"), message.From.Mailboxes.Select(x => x.Address).ToArray());
                            currentMessage.To = string.Join(char.Parse(";"), message.To.Mailboxes.Select(x => x.Address).ToArray());
                            currentMessage.Cc = string.Join(char.Parse(";"), message.Cc.Mailboxes.Select(x => x.Address).ToArray());
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
                            DirectoryInfo exceptionDir = new DirectoryInfo("Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\Exceptions\\");
                            if (!exceptionDir.Exists)
                                exceptionDir.Create();
                            string errorfile = exceptionDir.FullName + "\\Exception ZImapAPI - " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt";
                            log.Add($"ZIMAP Mail ERROR : {uid} - {ex.ToString()}");
                            StreamWriter sw = new StreamWriter(errorfile);
                            sw.WriteLine($"ZIMAP Mail ERROR : {uid} - {ex.ToString()}");
                            sw.Flush();
                            sw.Close();
                            sw.Dispose();
                        }
                    }

                    client.Disconnect(true);
                }

                return messages;
            }
            catch (System.Net.WebException ex)
            {
                DirectoryInfo dir = new DirectoryInfo("Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\Exceptions\\");
                if (!dir.Exists)
                    dir.Create();
                string errorfile = dir.FullName + "\\Exception ZImapAPI - " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt";
                log.Add($"ZIMAP WebException: {ex.ToString()}");
                StreamWriter sw = new StreamWriter(errorfile);
                sw.WriteLine(string.Join(System.Environment.NewLine, log.ToArray()));
                sw.Flush();
                sw.Close();
                sw.Dispose();
                throw new Exception(string.Join(System.Environment.NewLine, log.ToArray()), ex);
            }
            catch (Exception ex)
            {

                DirectoryInfo dir = new DirectoryInfo("Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\Exceptions\\");
                if (!dir.Exists)
                    dir.Create();
                string errorfile = dir.FullName + "\\Exception ZImapAPI - " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt";
                log.Add($"ZIMAP ERROR : {ex.ToString()}");
                StreamWriter sw = new StreamWriter(errorfile);
                sw.WriteLine(string.Join(System.Environment.NewLine, log.ToArray()));
                sw.Flush();
                sw.Close();
                sw.Dispose();
                throw new Exception(string.Join(System.Environment.NewLine, log.ToArray()), ex);
            }
            finally
            {
                log.Add("----------EXPORT IMAP END-------------");
                StreamWriter sw = new StreamWriter(tracefile,true);
                sw.WriteLine(string.Join(System.Environment.NewLine, log.ToArray()));
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void CleanTraceAndExceptions(DirectoryInfo dir, int days)
        {
            try
            {

                foreach (FileInfo fi in dir.GetFiles())
                {
                    TimeSpan age = DateTime.Now - fi.CreationTime;
                    if (age.TotalDays > days)
                    {
                        try
                        {
                            fi.Delete();
                        }
                        catch (Exception)
                        {
                            // Handle deletion exception here if needed
                        }
                    }
                }

                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    CleanTraceAndExceptions(di, days);
                }

                if (dir.GetFiles().Length == 0 && dir.GetDirectories().Length == 0 && dir.FullName.EndsWith("Log\\") == false) {
                    dir.Delete();
                }


            }
            catch (Exception)
            {
                // Handle exception here if needed
            }
        }

        private bool IsPreviouslyRetrieved(string uid)
        {
            // Check if the email with the given UID has been previously retrieved
            // Implement your logic here (e.g., check a database or a file)

            return false;
        }
              

    }
}
