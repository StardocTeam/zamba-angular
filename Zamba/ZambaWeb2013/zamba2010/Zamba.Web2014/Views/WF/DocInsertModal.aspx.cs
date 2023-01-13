using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using System.IO;
using System.Web.Security;
using System.Collections;
using System.Web.Configuration;

using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Text;
using mshtml;
using Zamba.Services;
using Zamba.Membership;

public partial class Views_WF_DocInsertModal : System.Web.UI.Page
{
    //bool IsModal;
    private IUser user;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            FormsAuthentication.RedirectToLoginPage();
        }
        else
        {
            user = (IUser)Session["User"];
        }

        FormBrowser.IsShowing = false;
    }
}
