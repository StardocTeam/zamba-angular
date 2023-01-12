using System;
using Microsoft.Office.Interop.Outlook ;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Zamba.Outlook
{
    /// <summary>
    /// This class wraps the Outlook API
    /// </summary>
    public sealed class Outlook
    {
        private static Application GetOutlook()
        {
            //TODO: Validar si existe una sesion de Outlook y tomarla 
            return new Application();
        }

        #region Constantes
        private const String ADDRESSES_SEPARATOR = ";";
        private const String MAPI_NAMESPACE = "MAPI";
        #endregion

        #region Send Mail

        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        public static void SendMail(String body, String subject, List<String> toMailAdresses)
        {
            SendMail(body, subject, toMailAdresses, null, null, null);
        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        public static void SendMail(String body, String subject, List<String> toMailAdresses, List<String> ccMailAddresses)
        {
            SendMail(body, subject, toMailAdresses, ccMailAddresses, null, null);
        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        /// <param name="bccMailAddresses">A list of BCC Mail Address</param>
        public static void SendMail(String body, String subject, List<String> toMailAdresses, List<String> ccMailAddresses, List<String> bccMailAddresses)
        {
            SendMail(body, subject, toMailAdresses, ccMailAddresses, bccMailAddresses, null);
        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        /// <param name="bccMailAddresses">A list of BCC Mail Address</param>
        /// <param name="AttachmentsFilePaths">A list of paths of attachments</param>
        public static void SendMail(String body, String subject, List<String> toMailAdresses, List<String> ccMailAddresses, List<String> bccMailAddresses, List<String> AttachmentsFilePaths)
        {
            Application OutlookApplication = GetOutlook();
            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);
            MAPIFolder OutboxFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderOutbox);


            _MailItem mail = (_MailItem)OutlookApplication.CreateItem(OlItemType.olMailItem);

            mail.SaveSentMessageFolder = OutboxFolder;

            StringBuilder AddressesBuilder = new StringBuilder();
            #region To Address
            if (null != toMailAdresses && toMailAdresses.Count > 0)
            {
                foreach (String CurrentMailAddress in toMailAdresses)
                {
                    AddressesBuilder.Append(CurrentMailAddress);
                    AddressesBuilder.Append(ADDRESSES_SEPARATOR);
                }

                AddressesBuilder.Remove(AddressesBuilder.Length - ADDRESSES_SEPARATOR.Length, ADDRESSES_SEPARATOR.Length);

                mail.To = AddressesBuilder.ToString();

                AddressesBuilder.Remove(0, AddressesBuilder.Length);
            }
            #endregion

            #region CC Address
            if (null != ccMailAddresses && ccMailAddresses.Count > 0)
            {
                foreach (String CurrentMailAddress in ccMailAddresses)
                {
                    AddressesBuilder.Append(CurrentMailAddress);
                    AddressesBuilder.Append(ADDRESSES_SEPARATOR);
                }
                AddressesBuilder.Remove(AddressesBuilder.Length - ADDRESSES_SEPARATOR.Length, ADDRESSES_SEPARATOR.Length);

                mail.CC = AddressesBuilder.ToString();

                AddressesBuilder.Remove(0, AddressesBuilder.Length);
            }
            #endregion

            #region BCC Address
            if (null != bccMailAddresses && bccMailAddresses.Count > 0)
            {
                foreach (String CurrentMailAddress in bccMailAddresses)
                {
                    AddressesBuilder.Append(CurrentMailAddress);
                    AddressesBuilder.Append(ADDRESSES_SEPARATOR);
                }
                AddressesBuilder.Remove(AddressesBuilder.Length - ADDRESSES_SEPARATOR.Length, ADDRESSES_SEPARATOR.Length);

                mail.BCC = AddressesBuilder.ToString();

                AddressesBuilder.Remove(0, AddressesBuilder.Length);
            }
            #endregion

            #region Attachments
            if (null != AttachmentsFilePaths && AttachmentsFilePaths.Count > 0)
            {
                String CurrentAttachmentName = null;

                foreach (String CurrentAttachmentFilePath in AttachmentsFilePaths)
                {
                    if (File.Exists(CurrentAttachmentFilePath))
                    {
                        CurrentAttachmentName = Path.GetFileName(CurrentAttachmentFilePath);
                        mail.Attachments.Add(CurrentAttachmentFilePath, Type.Missing, Type.Missing, CurrentAttachmentFilePath);
                    }
                }
            }
            #endregion

            mail.Subject = subject;
            mail.Body = body;

            mail.Send();
            mail = null;

            //OutlookApplication.Quit();
        }

        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="htmlBody">The HTML body </param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        public static void SendMail(String htmlBody, List<String> toMailAdresses, String subject)
        {
            SendMail(htmlBody, toMailAdresses, null, null, null, subject);

        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="htmlBody">The HTML body </param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        public static void SendMail(String htmlBody, List<String> toMailAdresses, List<String> ccMailAddresses, String subject)
        {
            SendMail(htmlBody, toMailAdresses, ccMailAddresses, null, null, subject);

        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="htmlBody">The HTML body </param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        /// <param name="bccMailAddresses">A list of BCC Mail Address</param>
        public static void SendMail(String htmlBody, List<String> toMailAdresses, List<String> ccMailAddresses, List<String> bccMailAddresses, String subject)
        {
            SendMail(htmlBody, toMailAdresses, ccMailAddresses, bccMailAddresses, null, subject);

        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="htmlBody">The HTML body </param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        /// <param name="bccMailAddresses">A list of BCC Mail Address</param>
        /// <param name="AttachmentsFilePaths">A list of paths of attachments</param>
        public static void SendMail(String htmlBody, List<String> toMailAdresses, List<String> ccMailAddresses, List<String> bccMailAddresses, List<String> AttachmentsFilePaths, String subject)
        {
            Application OutlookApplication = GetOutlook();
            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);
            MAPIFolder outboxFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderOutbox);


            _MailItem mail = (_MailItem)OutlookApplication.CreateItem(OlItemType.olMailItem);

            StringBuilder AddressesBuilder = new StringBuilder();

            #region To Address
            if (null != toMailAdresses && toMailAdresses.Count > 0)
            {
                foreach (String CurrentMailAddress in toMailAdresses)
                {
                    AddressesBuilder.Append(CurrentMailAddress);
                    AddressesBuilder.Append(ADDRESSES_SEPARATOR);
                }

                AddressesBuilder.Remove(AddressesBuilder.Length - ADDRESSES_SEPARATOR.Length, ADDRESSES_SEPARATOR.Length);

                mail.To = AddressesBuilder.ToString();

                AddressesBuilder.Remove(0, AddressesBuilder.Length);
            }
            #endregion

            #region CC Address
            if (null != ccMailAddresses && ccMailAddresses.Count > 0)
            {
                foreach (String CurrentMailAddress in ccMailAddresses)
                {
                    AddressesBuilder.Append(CurrentMailAddress);
                    AddressesBuilder.Append(ADDRESSES_SEPARATOR);
                }
                AddressesBuilder.Remove(AddressesBuilder.Length - ADDRESSES_SEPARATOR.Length, ADDRESSES_SEPARATOR.Length);

                mail.CC = AddressesBuilder.ToString();

                AddressesBuilder.Remove(0, AddressesBuilder.Length);
            }
            #endregion

            #region BCC Address
            if (null != bccMailAddresses && bccMailAddresses.Count > 0)
            {
                foreach (String CurrentMailAddress in bccMailAddresses)
                {
                    AddressesBuilder.Append(CurrentMailAddress);
                    AddressesBuilder.Append(ADDRESSES_SEPARATOR);
                }
                AddressesBuilder.Remove(AddressesBuilder.Length - ADDRESSES_SEPARATOR.Length, ADDRESSES_SEPARATOR.Length);

                mail.BCC = AddressesBuilder.ToString();

                AddressesBuilder.Remove(0, AddressesBuilder.Length);
            }
            #endregion

            AddressesBuilder = null;

            #region Attachments
            if (null != AttachmentsFilePaths && AttachmentsFilePaths.Count > 0)
            {
                String CurrentAttachmentName = null;

                foreach (String CurrentAttachmentFilePath in AttachmentsFilePaths)
                {
                    if (File.Exists(CurrentAttachmentFilePath))
                    {
                        CurrentAttachmentName = Path.GetFileName(CurrentAttachmentFilePath);
                        mail.Attachments.Add(CurrentAttachmentFilePath, Type.Missing, Type.Missing, CurrentAttachmentName);
                    }
                }
            }
            #endregion

            mail.Subject = subject;
            mail.HTMLBody = htmlBody;

            mail.Send();
            mail = null;

            OutlookApplication.Quit();
        }

        #endregion



        public static List<OutlookContact> GetContactsNameAndEmail()
        {
            Application OutlookApplication = GetOutlook();

            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);
            MAPIFolder ContactsFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderContacts);

            List<OutlookContact> ContactList = new List<OutlookContact>(ContactsFolder.Items.Count);

            ContactItem CurrentContactItem = null;
            OutlookContact CurrentContact = null;

            foreach (Object CurrentItem in ContactsFolder.Items)
            {
                if (CurrentItem is ContactItem)
                {
                    CurrentContactItem = (ContactItem)CurrentItem;
                    CurrentContact = new OutlookContact(CurrentContactItem.FullName, CurrentContactItem.Email1Address);
                    ContactList.Add(CurrentContact);
                }
            }

            OutlookApplication.Quit();

            CurrentContactItem = null;
            CurrentContact = null;

            return ContactList;
        }

        [Obsolete("Usada para testeo")]
        public static void Main(String[] args)
        {
            try
            {
                String Body = "asd asd as da sd asd a sd asd a sd a sd asd a sd a sd asd a sda sd <laksdfañlsdjkfalñsdkfjahldkfjhas";
                String Subject = "Testeo envio de citas";
                List<String> To = new List<string>(2);
                To.Add("marcelo.ferrer@stardoc.com.ar");
                To.Add("legnani@stardoc.com.ar");
                //To.Add("andres.nagel@stardoc.com.ar");

                Appointment a = new Appointment(Body, To, Subject, DateTime.Now, DateTime.Now, "Florida y Lavalle", AppointmentImportance.High);
                
                a.Send();

            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                Console.Read();
            }
        }
    }
}