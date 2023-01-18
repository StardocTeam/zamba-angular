using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;

using System.IO;
using System.Web.Security;
using System.Collections;
using System.Web.Configuration;
using System.Text.RegularExpressions;

using System.Text;
using Zamba.Core;
using Zamba.Membership;

public partial class Views_Tools_Messages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && Zamba.Membership.MembershipHelper.CurrentUser != null)
        {
            try
            {
                ZOptBusiness zopt = new ZOptBusiness();
                string title = zopt.GetValue("WebViewTitle");
                zopt = null;
                this.Title = (string.IsNullOrEmpty(title)) ? "Zamba Software" : title + " - Zamba Software";
            }
            catch { }
        }

        Int32 msgCode;
        string message = String.Empty;
        
        if(Int32.TryParse(Request.QueryString["msg"], out msgCode))
        {
            switch(msgCode)
            {
                case 0:
                    {
                        message = "Usted no tiene permiso para acceder a la tarea en esta etapa";
                        break;
                    }
                case 1:
                    {
                        message = "Archivo no encontrado";
                        btnClose.Visible = false;
                        break;
                    }
            }
        }

        lblMessage.Text = message;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Int64 taskId;

        if (Int64.TryParse(Request.QueryString["taskid"], out taskId))
        {
            Response.Write("<script language='javascript'>{parent.CloseTask(" + taskId.ToString() + ",true);}</script>");
        }
    }
}
