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

public partial class Views_Tools_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userTools"] == null)
            Response.Redirect("../../Views/Security/Login_WebTools.aspx");
        if (!Page.IsPostBack)
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
    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        if (chkCleanWF.Checked)
        {
            Zamba.Core.WFStepBusiness.ClearHashTables();  //workflows
            Zamba.Core.CacheBusiness.ClearCaches();
            Zamba.Core.WFStepStatesComponent.ClearHashTables();
            Zamba.Core.WFBusiness.ClearHashTables();
            Zamba.Core.WFRulesBusiness.ClearHashTables();
        }
        if (chkCleanVolumes.Checked)
        {
            Zamba.Core.VolumesBusiness.ClearHashTables(); //volumenes
        }
        if (chkCleanForms.Checked)
        {
            Zamba.Core.FormBusiness.ClearHashTables(); //forms
        }
        if (chkCleanIndexs.Checked)
        {
            Zamba.Core.AutoSubstitutionBusiness.ClearHashTables(); //atributos
            Zamba.Core.IndexsBusiness.ClearHashTables();
        }
        if (chkCleanUsers.Checked)
        {
            Zamba.Core.UserGroupBusiness.ClearHashTables(); //usuarios y permisos           
            Zamba.Core.UserComponent.ClearHashTables();
            Zamba.Core.RightsBusiness.ClearHashTables();
            Zamba.Membership.MembershipHelper.ClearHashTables();
            Zamba.Core.UserBusiness.ClearHashTables();
        }
        if (chkCleanDocTypes.Checked)
        {
            Zamba.Core.DocTypesBusiness.ClearHashTables();
        }

        Session.Abandon();

        Response.Redirect(Request.Url.ToString());
    }

    protected void btnCleanAll_Click(object sender, EventArgs e)
    {
        Zamba.Core.WFStepBusiness.ClearHashTables();  //workflows
        Zamba.Core.CacheBusiness.ClearCaches();
        Zamba.Core.WFStepStatesComponent.ClearHashTables();
        Zamba.Core.WFBusiness.ClearHashTables();
        Zamba.Core.WFRulesBusiness.ClearHashTables();
        Zamba.Core.VolumesBusiness.ClearHashTables(); //volumenes
        Zamba.Core.FormBusiness.ClearHashTables(); //forms
        Zamba.Core.AutoSubstitutionBusiness.ClearHashTables(); //atributos
        Zamba.Core.IndexsBusiness.ClearHashTables();
        Zamba.Core.UserGroupBusiness.ClearHashTables(); //usuarios y permisos           
        Zamba.Core.UserComponent.ClearHashTables();
        Zamba.Core.RightsBusiness.ClearHashTables();
        Zamba.Membership.MembershipHelper.ClearHashTables();
        Zamba.Core.UserBusiness.ClearHashTables();
        Zamba.Core.DocTypesBusiness.ClearHashTables();
        Zamba.Services.SWFResultsConsume.ClearOptions();
        Session.Abandon();

        Response.Redirect(Request.Url.ToString());
    }
}
