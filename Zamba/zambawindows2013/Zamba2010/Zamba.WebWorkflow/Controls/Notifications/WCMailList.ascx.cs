using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Zamba.Core;
using System.Collections.Generic;
public partial class Controls_Notifications_WebUserControl : System.Web.UI.UserControl
{
    public List<string> MailsToSend;
    public delegate void GoBackToMail();
    public event GoBackToMail GoBackToMailEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {
            try
            {
                lstAddedMails.Items.Clear();
                DataSet Mails = UserBusiness.GetAllZambaUsersMail();
                lstMails.DataSource = Mails.Tables[0];
                lstMails.DataValueField = "correo";
                lstMails.DataTextField = "usuario";
                lstMails.DataBind();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lstAddedMails.Items.Add(lstMails.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }

    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            lstAddedMails.Items.Remove(lstAddedMails.SelectedItem);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        MailsToSend = new List<string>();

        try
        {
            if (lstAddedMails.Items.Count > 0)
            {
                foreach (ListItem email in lstAddedMails.Items)
                {
                    MailsToSend.Add(email.ToString());
                }
            }

            GoBackToMailEvent();

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            MailsToSend.Clear();
            MailsToSend = null;
        }

        finally
        {
            lstAddedMails.Items.Clear();
        }
    }
}
