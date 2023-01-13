using System;
using Microsoft.Office.Interop.Outlook;
using System.Collections.Generic;
using System.Text;
using Zamba.Outlook.Interfaces;
using System.IO;

namespace Zamba.Outlook.Outlook
{
    /// <summary>
    /// This class wraps the Mail Logic from the Outlook API
    /// </summary>
    public sealed class Mail
        : IMail
    {
        #region Constantes
        private const string ADDRESSES_SEPARATOR = ";";
        private const string MAPI_NAMESPACE = "MAPI";
        #endregion

        #region Constructores
        public Mail()
        { }
        #endregion

        #region Send Mail
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="isBodyHTML">Specifies wheather the body should be sent as HTML or plain text</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        public void SendMail(String body, Boolean isBodyHTML, String subject, List<String> toMailAdresses)
        {
            if (isBodyHTML)
                SendMailAsHTML(body, subject, toMailAdresses);
            else
                SendMailAsPlainText(body, subject, toMailAdresses);
        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="isBodyHTML">Specifies wheather the body should be sent as HTML or plain text</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        public void SendMail(String body, Boolean isBodyHTML, String subject, List<String> toMailAdresses, List<String> ccMailAddresses)
        {
            if (isBodyHTML)
                SendMailAsHTML(body, subject, toMailAdresses, ccMailAddresses);
            else
                SendMailAsPlainText(body, subject, toMailAdresses, ccMailAddresses);
        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="isBodyHTML">Specifies wheather the body should be sent as HTML or plain text</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        /// <param name="bccMailAddresses">A list of BCC Mail Address</param>
        public void SendMail(String body, Boolean isBodyHTML, String subject, List<String> toMailAdresses, List<String> ccMailAddresses, List<String> bccMailAddresses)
        {
            if (isBodyHTML)
                SendMailAsHTML(body, subject, toMailAdresses, ccMailAddresses, bccMailAddresses);
            else
                SendMailAsPlainText(body, subject, toMailAdresses, ccMailAddresses, bccMailAddresses);
        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="isBodyHTML">Specifies wheather the body should be sent as HTML or plain text</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        /// <param name="bccMailAddresses">A list of BCC Mail Address</param>
        /// <param name="AttachmentsFilePaths">A list of paths of attachments</param>
        public void SendMail(String body, Boolean isBodyHTML, String subject, List<String> toMailAdresses, List<String> ccMailAddresses, List<String> bccMailAddresses, List<String> AttachmentsFilePaths)
        {
            if (isBodyHTML)
                SendMailAsHTML(body, subject, toMailAdresses, ccMailAddresses, bccMailAddresses, AttachmentsFilePaths);
            else
                SendMailAsPlainText(body, subject, toMailAdresses, ccMailAddresses, bccMailAddresses, AttachmentsFilePaths);
        }

        #region Plain Text Body
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        private static void SendMailAsPlainText(String body, String subject, List<String> toMailAdresses)
        {
            SendMailAsPlainText(body, subject, toMailAdresses, null, null, null);
        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        private static void SendMailAsPlainText(String body, String subject, List<String> toMailAdresses, List<String> ccMailAddresses)
        {
            SendMailAsPlainText(body, subject, toMailAdresses, ccMailAddresses, null, null);
        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="body">The body of the Mail</param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        /// <param name="bccMailAddresses">A list of BCC Mail Address</param>
        private static void SendMailAsPlainText(String body, String subject, List<String> toMailAdresses, List<String> ccMailAddresses, List<String> bccMailAddresses)
        {
            SendMailAsPlainText(body, subject, toMailAdresses, ccMailAddresses, bccMailAddresses, null);
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
        private static void SendMailAsPlainText(String body, String subject, List<String> toMailAdresses, List<String> ccMailAddresses, List<String> bccMailAddresses, List<String> AttachmentsFilePaths)
        {
            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();
            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);

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
        }

        #endregion

        #region HTML Body
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="htmlBody">The HTML body </param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        private static void SendMailAsHTML(String htmlBody, String subject, List<String> toMailAdresses)
        {
            SendMailAsHTML(htmlBody, subject, toMailAdresses, null, null, null);

        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="htmlBody">The HTML body </param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        private static void SendMailAsHTML(String htmlBody, String subject, List<String> toMailAdresses, List<String> ccMailAddresses)
        {
            SendMailAsHTML(htmlBody, subject, toMailAdresses, ccMailAddresses, null, null);

        }
        /// <summary>
        /// Sends a Mail usign the current default Outlook Account
        /// </summary>
        /// <param name="htmlBody">The HTML body </param>
        /// <param name="subject">The subject of the Mail</param>
        /// <param name="toMailAdresses">A list of TO Mail Address</param>
        /// <param name="ccMailAddresses">A list of CC Mail Address</param>
        /// <param name="bccMailAddresses">A list of BCC Mail Address</param>
        private static void SendMailAsHTML(String htmlBody, String subject, List<String> toMailAdresses, List<String> ccMailAddresses, List<String> bccMailAddresses)
        {
            SendMailAsHTML(htmlBody, subject, toMailAdresses, ccMailAddresses, bccMailAddresses, null);

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
        private static void SendMailAsHTML(String htmlBody, String subject, List<String> toMailAdresses, List<String> ccMailAddresses, List<String> bccMailAddresses, List<String> AttachmentsFilePaths)
        {
            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();
            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);

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
            mail.BodyFormat = OlBodyFormat.olFormatHTML;
            mail.Subject = subject.Replace("\n","");
            if (!htmlBody.Contains("<") && !htmlBody.Contains(">"))
                mail.HTMLBody = "<HTML> <HEAD> <TITLE></TITLE> </HEAD> <BODY><P>" + htmlBody.Replace("\n", "<br>") + "</P></BODY></HTML>";
            else
                mail.HTMLBody = htmlBody;
            mail.Send();
            mail = null;
        }
        #endregion
        #endregion        
        }

    }