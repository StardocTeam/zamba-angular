using System;
using System.IO;
using Ionic.Zip;
using System.Net.Mail;

namespace Zamba.Framework
{
    public partial class SendZip
    {

        public static bool EnviarZip(Zipdata zipdata, string AppTempPath, string emailAddress, string emailPassWord, string emailSMTPProv)
        {
            ZipFile zip = new ZipFile();
            var isCorrectMailSend = false; 

            if (zipdata.DocidList.Count > 0)
            {
                zip.Comment = zipdata.ZipName;
                zip.Password = zipdata.ZipPassword;
                string zipname = "\\temp\\" + zip.Comment + ".zip";
                var pathToZip = AppTempPath + zipname;

                foreach (var result in zipdata.DocidList)
                {
                    if (File.Exists(result))
                    {
                        //var archive = Path.GetFileName(result);
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
                        using (System.Net.Mail.MailMessage MailSetup = new System.Net.Mail.MailMessage())
                        {
                            MailSetup.Subject = zipdata.Asunto;
                            MailSetup.DeliveryNotificationOptions = DeliveryNotificationOptions.Never;

                            foreach (string mail in zipdata.MailTo)
                            {
                                MailSetup.To.Add(mail);
                            }
                                                        
                            MailSetup.From = new System.Net.Mail.MailAddress(emailAddress); 
                            MailSetup.Body = zipdata.MessageBody;

                            if (zipdata.CC != null && zipdata.CC.Count > 0) {
                                foreach (string CC in zipdata.CC)
                                {
                                    MailSetup.CC.Add(CC);
                                }
                            }

                            System.Net.Mail.Attachment attachment;
                            attachment = new System.Net.Mail.Attachment(pathToZip);
                            MailSetup.Attachments.Add(attachment);
                            using (System.Net.Mail.SmtpClient SMTP = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
                            {
                                SMTP.Port = 587;
                                SMTP.EnableSsl = true;
                                SMTP.Credentials = new System.Net.NetworkCredential(MailSetup.From.ToString(), emailPassWord);
                                SMTP.Send(MailSetup);
                                isCorrectMailSend = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        isCorrectMailSend = false;
                    }
                }
            }  
            return isCorrectMailSend;
        }

    }
}
