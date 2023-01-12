using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Web.Helpers;

public partial class Views_WF_TaskDetails_TaskMailhistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Zamba.Services.SZOptBusiness ZOptBusines = new Zamba.Services.SZOptBusiness();
            Page.Title = (string)ZOptBusines.GetValue("WebViewTitle") + " - Historial de Mails";

            Int64 docId = Int64.Parse(Request["ResultId"]);
            ucMails.LoadMailsHistoryByDocId(docId);
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    //public static IHtmlString GetJqueryCoreScript()
    //{
    //    return Tools.GetJqueryCoreScript(HttpContext.Current.Request);
    //}
}
