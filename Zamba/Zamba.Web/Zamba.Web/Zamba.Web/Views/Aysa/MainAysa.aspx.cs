using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Services;
using Zamba.Core;

public partial class Views_Client_Aysa_MainAysa : System.Web.UI.Page
{
    UserPreferences UP = new UserPreferences();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCmbFormTypes();

            if (Zamba.Membership.MembershipHelper.CurrentUser != null)
            {
                try
                {

                    IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
                    SRights rights = new SRights();
                    Int32 type = 0;
                    if (user.WFLic) type = 1;
                    if (user.ConnectionId > 0)
                    {
                        UserPreferences UserPreferences = new UserPreferences();
                        Ucm.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "30")), type);
                        UserPreferences = null;
                    }
                    else
                        Response.Redirect("~/Views/Security/LogIn.aspx");
                    rights = null;

                }
                catch (Exception ex)
                {
                    Zamba.AppBlock.ZException.Log(ex);
                }
            }
        }
    }

    public void FillCmbFormTypes()
    {
        try
        {
            SForms SForms = new SForms();
            var arrayforms = SForms.GetVirtualDocumentsByRightsOfCreate(FormTypes.WebInsert);
            ddltipodeform.DataSource = arrayforms;
            ddltipodeform.DataTextField = "Name";
            ddltipodeform.DataValueField = "ID";
            ddltipodeform.DataBind();
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }


}
