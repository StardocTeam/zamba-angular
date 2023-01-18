using System;
using System.Configuration;
using Zamba.Core;
using Zamba.Tools;
using System.IO;

namespace Marsh.Bussines
{
    public class EmailNotificacion
    {
        private int _user_id;
        private string _smtp;
        private string _user;
        private string _pass;
        private string _from;
        private string _port;

        private Byte[] _key = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6};
        private Byte[] _iv = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };

        public EmailNotificacion(int user_id)
        {
            try
            {
                _smtp = ConfigurationSettings.AppSettings["mail_smtp"].ToString();
                _user = ConfigurationSettings.AppSettings["mail_user"].ToString();
                _pass = ConfigurationSettings.AppSettings["mail_pass"].ToString();
                _from = ConfigurationSettings.AppSettings["mail_from"].ToString();
                _port = ConfigurationSettings.AppSettings["mail_port"].ToString();

                _pass = Encryption.DecryptString(_pass, _key, _iv);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            _user_id = user_id;
        }

        /// <summary>
        /// Envia un email
        /// </summary>
        /// <param name="to">Destinatario</param>
        /// <param name="subject">Asunto</param>
        /// <param name="body">Mensaje</param>
        /// <returns></returns>
        public bool EnviarNotificacion(string to, string subject, string body)
        {
            return MessagesBussines.SendMail(MailTypes.NetMail, _from, _smtp, _port, _user, _pass, to, string.Empty, string.Empty, subject, body, true, null, _user_id, null);
        }

        /// <summary>
        /// Lee el templare HTML para el envio de emails
        /// </summary>
        /// <returns></returns>
        public string LeerHTML(string filename)
        {
            string file;

            file = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"email\" + filename;

            using (StreamReader sr = File.OpenText(file))
            {
                return sr.ReadToEnd();
            }            
        }
    }
}
