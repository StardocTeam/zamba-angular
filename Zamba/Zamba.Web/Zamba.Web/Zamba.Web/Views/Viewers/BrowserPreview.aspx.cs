using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Web.Helpers;

public partial class BrowserPreview : System.Web.UI.Page
{
    UserPreferences UP = new UserPreferences();
    protected void Page_PreInit(object sender, EventArgs e)
    {


        if (Zamba.Servers.Server.ConInitialized == false)
        {
            Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
            ZC.InitializeSystem("Zamba.Web");
        }

        Zamba.Membership.MembershipHelper.OptionalAppTempPath = UP.getValue("AppTempPath", Zamba.UPSections.UserPreferences, String.Empty);

        ZOptBusiness zoptb = new ZOptBusiness();
        Page.Theme = zoptb.GetValue("CurrentTheme");
        Session["CurrentTheme"] = Page.Theme;
        zoptb = null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //public static IHtmlString GetJqueryCoreScript()
    //{
    //    return Tools.GetJqueryCoreScript(HttpContext.Current.Request);
    //}

    public static IHtmlString RegisterThemeBundles()
    {
        return Tools.RegisterThemeBundles(HttpContext.Current.Request);
    }
   
}