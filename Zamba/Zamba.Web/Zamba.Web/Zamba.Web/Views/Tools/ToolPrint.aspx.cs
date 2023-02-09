using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Zamba.Core;
using Zamba.Membership;

public partial class Views_Tools_ToolPrint : System.Web.UI.Page
{
    Int64 _doctypeId;
    Int64 _docId;
    string _url;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Int64.TryParse(Request.QueryString["doctypeid"], out _doctypeId);
            Int64.TryParse(Request.QueryString["docid"], out _docId);
            String token = new ZssFactory().GetZss(Zamba.Membership.MembershipHelper.CurrentUser).Token;

            _url = string.Format(MembershipHelper.Protocol + "{0}{1}/Services/GetDocFile.ashx?DocTypeId={2}&DocId={3}&userid={4},token={5}", Request.ServerVariables["HTTP_HOST"], Request.ApplicationPath, _doctypeId, _docId, Zamba.Membership.MembershipHelper.CurrentUser.ID,token);

            if (!string.IsNullOrEmpty(_url))
            {
                // Source del formbrowser
                if (!_url.StartsWith("http"))
                    formBrowser.Attributes["src"] = MembershipHelper.Protocol + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/temp/" + new FileInfo(_url).Name;
                else
                    formBrowser.Attributes["src"] = _url;
            }

            try
            {
                ZOptBusiness zopt = new ZOptBusiness();
                string title = zopt.GetValue("WebViewTitle");
                zopt = null;
                this.Title = (string.IsNullOrEmpty(title)) ? "Zamba Software" : title + " - Zamba Software";
            }
            catch { }
        }
    }
}
