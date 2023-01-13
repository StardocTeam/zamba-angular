using ChatJsMvcSample.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace Zamba.Collaboration.Code
{
    public class Mail
    {
        public Mail()
        {
            InitZambaDB();
        }

        private static void InitZambaDB()
        {
            try
            {
                if (Zamba.Servers.Server.ConInitialized == false)
                {
                    Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                    ZC.InitializeSystem("Zamba.Collaboration");
                    // IUser currentUser = UserBusiness.Rights.ValidateLogIn(22357, ClientType.Web);
                }
            }
            catch (Exception ex)
            {
                AppBlock.ZException.Log(ex);
            }
        }

        public static void Send(string email, string name, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress("demo@stardoc.com.ar", "Zamba Collaboration");
                var toAddress = new MailAddress(email, name);
                var smtp = smptClient(fromAddress);
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                AppBlock.ZException.Log(ex);
            }
        }

        private static SmtpClient smptClient(MailAddress fromAddress)
        {
            return new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "Stardoc2017")
            };
        }

        public static void SendInvitation(ChatUser thisUser, ChatUser invUser, bool mailCopy, string encURL)
        {
            try
            {


                InitZambaDB();
                var td = Zamba.Core.ZOptBusiness.GetValueOrDefault("ThisDomain", "http://www.zamba.com.ar/zambastardoc");
                var url = td + "Account/CreateAccountEnc?enc=" + encURL;
                string assemblyFile = Path.GetDirectoryName((
                                new System.Uri(Assembly.GetExecutingAssembly().CodeBase)
                            ).AbsolutePath).ToString();
                var msg = File.ReadAllText(assemblyFile + "\\Mail\\MailInvitation.html")
                     .Replace("[*Link]", url)
                     .Replace("[*UserName]", thisUser.Name)
                     .Replace("[*UserMail]", thisUser.Email)
                     .Replace("[*InvitedUserName]", invUser.Name);

                Send(invUser.Email, invUser.Name, "Invitacion a Zamba Collaboration", msg);
                if (mailCopy)
                {
                    var copyMsg = File.ReadAllText(assemblyFile + "\\Mail\\MailNotification.html")
                    .Replace("[*Link]", url)
                    .Replace("[*UserName]", thisUser.Name)
                    .Replace("[*UserMail]", thisUser.Email)
                    .Replace("[*InvitedUserName]", invUser.Name);

                    Send(thisUser.Email, thisUser.Name, "Invitacion a Zamba Collaboration", copyMsg);
                }
            }
            catch (Exception ex)
            {
                AppBlock.ZException.Log(ex);
            }
        }

        public static void Dismiss(ChatUser thisUser, ChatUser invUser)
        {
            var td = Zamba.Core.ZOptBusiness.GetValueOrDefault("ThisDomain", "http://www.zamba.com.ar/zambastardoc");
            string assemblyFile = Path.GetDirectoryName((
                            new System.Uri(Assembly.GetExecutingAssembly().CodeBase)
                        ).AbsolutePath).ToString();
            var msg = File.ReadAllText(assemblyFile + "\\Mail\\MailDismiss.html")
                 .Replace("[*Link]", td)
                 .Replace("[*InvitedUserMail]", invUser.Email)
                 .Replace("[*InvitedUserName]", invUser.Name);

            Send(thisUser.Email, thisUser.Name, "Zamba Collaboration", msg);
        }
        public static string ThisDomain()
        {
            InitZambaDB();
            var td = Zamba.Core.ZOptBusiness.GetValueOrDefault("ThisDomain", "http://www.zamba.com.ar/zambastardoc");
            return td;
        }
        public static void CreatedAccountNotification(ChatUser thisUser, string password)
        {


            SendGenericMail(ThisDomain(), "Accede ahora", "Bienvenido <b>" + thisUser.Name + "</b> a Zamba Collaboration",
                "Se ha creado una nueva cuenta en Zamba Collaboration para que puedas acceder a " +
                  " Zamba Link desde cualquier dispositivo y lugar e interactuar con cualquier persona.", "Tu nueva contraseña es: <b>" + password +
                  "</b> por favor no la comentes ni divulgues, para poder cambiarla, accede a Zamba Collaboration desde el siguiente botón y modificalo desde el menú"
                   + " 'Mi Cuenta'", thisUser);
        }
        public static void ForgotPassword(ChatUser thisUser, string password)
        {
            SendGenericMail(ThisDomain(), "Accede ahora", "<b>" + thisUser.Name + "</b> se ha restaurado su contraseña",
                "Se genero una nueva contraseña en Zamba Collaboration para que puedas acceder a " +
                  " Zamba Link desde cualquier dispositivo y lugar e interactuar con cualquier persona.", "Tu nueva contraseña es: <b>" + password +
                  "</b> por favor no la comentes ni divulgues. Para poder cambiarla, accede a Zamba Collaboration desde el siguiente botón y modificalo desde el menú"
                   + " 'Mi Cuenta'", thisUser);
        }
        public static void SendGenericMail(string lnk, string linkDesc, string title, string message, string message2, ChatUser cu)
        {
            string assemblyFile = Path.GetDirectoryName((
                            new System.Uri(Assembly.GetExecutingAssembly().CodeBase)
                        ).AbsolutePath).ToString();
            var msg = File.ReadAllText(assemblyFile + "\\Mail\\MailGeneric.html")
                 .Replace("[*Link]", lnk == string.Empty ? ThisDomain() : lnk)
                 .Replace("[*LinkDesc]", linkDesc)
                 .Replace("[*Title]", title)
                 .Replace("[*Message]", message)
                  .Replace("[*Message2]", message2);

            Send(cu.Email, cu.Name, "Zamba Collaboration", msg);
        }
    }
}