using MimeKit;
using MailKit.Net.Imap;
using System.IO;
using System;
using MailKit.Search;
using MailKit;
using MailKit.Security;
using System.Net;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace EmailRetrievalAPI.Controllers
{
    public class EmailService
    {
        public class imapConfig
        {
            public string ImapServer = "nasa1mail.mmc.com";
            public int ImapPort = 143;
            public SecureSocketOptions secureSocketOptions = SecureSocketOptions.Auto;
            public string ImapUsername = "mgd\\eseleme\\pedidoscaucion@marsh.com";
            public string ImapPassword = "Julio2023";
            public string FolderName = "INBOX";
            public string ExportFolderPath = "exported";

        }

        public string RetrieveEmails(imapConfig config)
        {

            List<string> log = new List<string>();

            try
            {

                // Configure the certificate validation callback to trust the certificate
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

                using (var client = new ImapClient())
                {
                    // Ignore certificate validation for the IMAP connection
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    log.Add("Connect");
                    client.Connect(config.ImapServer, config.ImapPort, config.secureSocketOptions);

                    log.Add("Authenticate");
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

                            // Create a local email file with .msg extension
                            var filePath = Path.Combine(config.ExportFolderPath, $"{uid}.eml");
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                message.WriteTo(FormatOptions.Default, stream);
                            }

                            IMailFolder newfolder = folder;
                            try
                            {
                                newfolder = folder.GetSubfolder("Exported");

                            }
                            catch (FolderNotFoundException)
                            {
                                folder.Create("Exported", true);
                                newfolder = folder.GetSubfolder("Exported");
                            }

                            // Move the email to the exported folder
                            folder.MoveTo(uid, newfolder);

                            log.Add($"Mail OK: {uid} - {message.Subject}");
                        }
                        catch (Exception ex)
                        {
                            log.Add($"Mail ERROR : {uid} - {ex.ToString()}");
                        }
                    }

                    client.Disconnect(true);
                }

            }
            catch (Exception ex)
            {
                log.Add($"ERROR : {ex.ToString()}");
            }

            return string.Join(System.Environment.NewLine, log);
        }

        private bool IsPreviouslyRetrieved(string uid)
        {
            // Check if the email with the given UID has been previously retrieved
            // Implement your logic here (e.g., check a database or a file)

            return false;
        }
    }
}
