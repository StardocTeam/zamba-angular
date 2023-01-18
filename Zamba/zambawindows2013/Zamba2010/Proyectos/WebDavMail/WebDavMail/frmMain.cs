using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;

namespace WebDavMail
{
    public partial class frmWebDavMailExample : Form
    {
        private Mail mail;
        public frmWebDavMailExample()
        {
            InitializeComponent();
        }

        #region General Code

        private void DisplayText(string strResult)
        {
            lblResult.Visible = true;
            tvResult.Visible = false;
            lblResult.Text = strResult;
        }
        
        private void DisplayXML(XmlDocument xmlDoc)
        {
            lblResult.Visible = false;
            tvResult.Visible = true;
            try
            {
                tvResult.Nodes.Clear();
                tvResult.Nodes.Add(new TreeNode(xmlDoc.DocumentElement.Name));
                TreeNode tNode = new TreeNode();
                tNode = tvResult.Nodes[0];

                AddNode(xmlDoc.DocumentElement, tNode);
                tvResult.ExpandAll();
            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayException(Exception ex)
        {
            lblResult.Visible = true;
            tvResult.Visible = false;
            lblResult.Text = ex.Message + Environment.NewLine + Environment.NewLine + ex.InnerException;
        }

        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i;

            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                inTreeNode.Text = (inXmlNode.OuterXml).Trim();
            }
        }     

        private void SetMailClass()
        {
            Properties.Settings.Default.Save();
            mail = new Mail();
            mail.p_strServer = Properties.Settings.Default["ExchangeServer"].ToString();
            mail.p_strUserName = Properties.Settings.Default["UserName"].ToString();
            mail.p_strAlias = Properties.Settings.Default["UserNameAlias"].ToString();
            mail.p_strPassword = Properties.Settings.Default["Password"].ToString();
            mail.p_strInboxURL = Properties.Settings.Default["InboxName"].ToString();
            mail.p_strDrafts = Properties.Settings.Default["DraftsName"].ToString();
        }
        #endregion

        private void btnGetAllUnread_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            try
            {
                SetMailClass();
                XmlDocument xmlDoc = mail.GetUnreadMailAll();
                DisplayXML(xmlDoc);
            }
            catch(Exception ex) {DisplayException(ex);}
        }

        private void btnGetAllUnreadAttachment_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            try
            {
                SetMailClass();
                XmlDocument xmlDoc = mail.GetUnreadMailWithAttachments();
                DisplayXML(xmlDoc);
            }
            catch(Exception ex) {DisplayException(ex);}
        }

        private void btnAllMailboxInfo_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            try
            {
                SetMailClass();
                XmlDocument xmlDoc = mail.GetAllMailboxInfo();
                DisplayXML(xmlDoc);
            }
            catch(Exception ex) {DisplayException(ex);}
        }

        private void btnGetMailboxSize_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            try
            {
                SetMailClass();
                String sResult = mail.GetMailboxSize().ToString();
                DisplayText("Total Mailbox size = " + sResult);
            }
            catch(Exception ex) {DisplayException(ex);}
        }

        private void btnGetAttachmentList_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            try
            {
                SetMailClass();
                XmlDocument xmlDoc = mail.GetAttachmentsListXML(txtUrlMail.Text);
                DisplayXML(xmlDoc);
            }
            catch(Exception ex) {DisplayException(ex);}
        }

        private void btnGetAttachment_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            try
            {
                SetMailClass();
                String sResult = mail.getAttachmentFromMail(txtUrlAttachment.Text);
                DisplayText("Attachment, represented as a string. Use a StreamWriter to recreate the file" + Environment.NewLine + Environment.NewLine + sResult);
            }
            catch(Exception ex) {DisplayException(ex);}
        }

        private void btnMarkAsRead_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            try
            {
                SetMailClass();
                String strResult = mail.MarkAsRead(txtUrlMailRead.Text);
                DisplayText("Mail marked as read. StatusCode:" + strResult);
            }
            catch (Exception ex) { DisplayException(ex); }
        }

        private void textbox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).Select(0, ((TextBox)sender).Text.Length);
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            try
            {
                SetMailClass();
                String strResult = mail.SendMail(txtMailTo.Text, txtSubject.Text, txtBody.Text);
                DisplayText("Mail Body send: " + Environment.NewLine + Environment.NewLine + strResult);
            }
            catch (Exception ex) { DisplayException(ex); }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            string strTag = ((Button)sender).Tag.ToString();
            string strHelpText = "";
            switch(strTag)
            {
                case "Exchange":
                    strHelpText = "Enter the URL of the Exchange server here. Always start with http(s)://, followed by an IP adress or DNS name (for example: https://webmail.company.com)";
                    break;
                case "UserName":
                    strHelpText = "Enter the username you use (for example in Outlook) to login to the exchange server";
                    break;
                case "Alias":
                    strHelpText = "The connection string to the Exchange server sometimes uses an alias instead of your username. To find out your alias, take a look at the url when you login to the exchange server with your browser.";
                    break;
                case "Password":
                    strHelpText = "";
                    break;
                case "Inbox":
                    strHelpText = "Most of the times the inbox name is actually inbox. However, you should check this by having a look at the url when you login to the exchange server with your browser.";
                    break;
                case "Drafts":
                    strHelpText = "Most of the times the drafts name is actually drafts. However, you should check this by having a look at the url when you go to your drafts folder in your browser.";
                    break;
                default:
                    strHelpText = "Help Not Found.";
                    break;
            }
            DisplayText("Help with " + strTag + ":" + Environment.NewLine + Environment.NewLine + strHelpText);
        }

        private void tvResult_Click(object sender, EventArgs e)
        {
            if(true)
            btnCopyClipboard.Enabled = true;
        }

        private void btnCopyClipboard_Click(object sender, EventArgs e)
        {
            String selectString = tvResult.SelectedNode.Text;
            Clipboard.SetDataObject(selectString, true);

        }

        private void btnGetContacts_Click(object sender, EventArgs e)
        {
            if (txtContactsLike.Text.Trim().Length > 0)
            {
                try
                {
                    SetMailClass();
                    String strResult = mail.PrintContactsUsingExchangeWebDAV(txtContactsLike.Text.Trim());
                    if (strResult.Length > 0) DisplayText("Contact(s): " + Environment.NewLine + Environment.NewLine + strResult);
                    else DisplayText("No contact(s) found");
                }
                catch (Exception ex) { DisplayException(ex); }
            }
            else DisplayText("Contact name has to contain at least one starting character.");
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            try
            {
                SetMailClass();
                XmlDocument xmlDoc = mail.DownloadAttachs(TxtDownloadAttach.Text);
                DisplayXML(xmlDoc);

                //String sResult = mail.DownloadAttachs(TxtDownloadAttach.Text);
                //DisplayText("Attachment, represented as a string. Use a StreamWriter to recreate the file" + Environment.NewLine + Environment.NewLine + sResult);
            }
            catch (Exception ex) { DisplayException(ex); }
        }
    }
}
