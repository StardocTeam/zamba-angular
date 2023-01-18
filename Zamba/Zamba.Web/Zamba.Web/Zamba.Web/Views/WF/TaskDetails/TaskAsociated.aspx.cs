using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Services;
using Zamba.Web.Helpers;

public partial class Views_WF_TaskDetails_TaskAsociated : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Int64 docId = Convert.ToInt64(Request.QueryString["ResultId"]);
            Int64 docTypeId = Convert.ToInt64(Request.QueryString["DocTypeId"]);
            IResult result = new Results_Business().GetResult(docId, docTypeId, true);

            ucDocAssociatedGrid.loadAssociatedDocs(result);

            Zamba.Services.SZOptBusiness ZOptBusines = new Zamba.Services.SZOptBusiness();
            Page.Title = (string)ZOptBusines.GetValue("WebViewTitle") + " - Asociados";
            ZOptBusines = null;
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    //public static IHtmlString GetJqueryCoreScript()
    //{
    //    return Tools.GetJqueryCoreScript(HttpContext.Current.Request);
    //}
}
