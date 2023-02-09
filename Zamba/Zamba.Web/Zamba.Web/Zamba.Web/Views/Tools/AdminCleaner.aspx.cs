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
        DocTypesBusiness DTB = new DocTypesBusiness();
        if (chkCleanWF.Checked)
        {
            Zamba.Core.WFStepBusiness.ClearHashTables();  //workflows
            Zamba.Core.CacheBusiness.ClearCaches();
            Zamba.Core.WFStepStatesComponent.ClearHashTables();
            new WFBusiness().ClearHashTables();
            Zamba.Core.WFRulesBusiness.ClearHashTables();
        }
        if (chkCleanForms.Checked)
        {
             new FormBusiness().ClearHashTables(); //forms
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
            new RightsBusiness().ClearHashTables();
            Zamba.Membership.MembershipHelper.ClearHashTables();
            UserBusiness UB = new UserBusiness();

            UB.ClearHashTables();
        }
        if (chkCleanDocTypes.Checked)
        {
            DocTypesBusiness.ClearHashTables();
        }

        Session.Abandon();

        Response.Redirect(Request.Url.ToString());
    }

    protected void btnCleanAll_Click(object sender, EventArgs e)
    {
        Zamba.Core.WFStepBusiness.ClearHashTables();  //workflows
        Zamba.Core.CacheBusiness.ClearCaches();
        Zamba.Core.WFStepStatesComponent.ClearHashTables();
        new WFBusiness().ClearHashTables();
        Zamba.Core.WFRulesBusiness.ClearHashTables();
            //volumenes
        new FormBusiness().ClearHashTables(); //forms
        Zamba.Core.AutoSubstitutionBusiness.ClearHashTables(); //atributos
        Zamba.Core.IndexsBusiness.ClearHashTables();
        Zamba.Core.UserGroupBusiness.ClearHashTables(); //usuarios y permisos           
        Zamba.Core.UserComponent.ClearHashTables();
        new RightsBusiness().ClearHashTables();
        Zamba.Membership.MembershipHelper.ClearHashTables();
        UserBusiness UB = new UserBusiness();

        UB.ClearHashTables();
        DocTypesBusiness.ClearHashTables();
        Zamba.Services.SWFResultsConsume.ClearOptions();
        Session.Abandon();

        Response.Redirect(Request.Url.ToString());
    }
}
