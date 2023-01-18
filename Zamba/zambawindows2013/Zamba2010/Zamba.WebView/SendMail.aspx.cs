using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using Zamba.WebActiveX;
using Zamba.Core;

public partial class DocViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {
            string filename = Request.QueryString["fullpath"].Remove(0, Request.QueryString["fullpath"].LastIndexOf("\\") + 1);
            WCSendMail.ClearAttachments();
            WCSendMail.SetInitialState();
            WCSendMail.AddAttach(filename, Request.QueryString["fullpath"]);    
        }
    }
}