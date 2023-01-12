using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Data;
using Zamba.Services;
using Zamba.Core;
using Zamba.Core.WF.WF;

public partial class Views_UC_Task_TaskDetailUL : System.Web.UI.UserControl
{
    RightsBusiness RiB = new RightsBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bool historial = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.FrmDocHistory, Zamba.Core.RightsType.View, -1);
                if (historial)
                    CrearTabHistorial();
                bool foro = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.ForoWeb, Zamba.Core.RightsType.View, -1);
                if (foro)
                    CrearTabForum();
            }
            catch
            {
            }
        }
    }

    private void CrearTabForum()
    {
        string script = "$('#ulPrincipal').append('<li><a href=\"#tabForumUL\">Foro</a></li>');";
        string iframe = "<iframe  width=\"100%\" frameborder=\"0\" runat=\"server\" id=\"forumFrameUL\" style=\"height:550px\"></iframe>";
        string div1 = "$('#tabTaskDetailUl').append('<div ID=\"tabForumUL\">" + iframe + "</div>');";
        string js = "<script type=\"text/javascript\">";
        js += div1;
        js += script;
        js += " </script> ";
        Page.ClientScript.RegisterStartupScript(typeof(Page), "DoAddForumScript", js);
    }

    private void CrearTabHistorial()
    {
        string script = "$('#ulPrincipal').append('<li><a href=\"#tabHistoryUL\">Historial</a></li>');";
        string iframe = "<iframe  width=\"100%\" frameborder=\"0\" runat=\"server\" id=\"historyFrameUL\" style=\"height:550px\"></iframe>";
        string div1 = "$('#tabTaskDetailUl').append('<div ID=\"tabHistoryUL\">" + iframe + "</div>');";
        string js = "<script type=\"text/javascript\">";
        js += div1;
        js += script;
        js += " </script> ";
        Page.ClientScript.RegisterStartupScript(typeof(Page), "DoAddHistoryScript", js);
    }
}
