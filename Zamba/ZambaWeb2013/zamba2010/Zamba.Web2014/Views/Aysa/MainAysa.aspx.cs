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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCmbFormTypes();

            if (Session["User"] != null)
            {
                try
                {

                    IUser user = (IUser)Session["User"];
                    SRights rights = new SRights();
                    Int32 type = 0;
                    if (user.WFLic) type = 1;
                    if (user.ConnectionId > 0)
                    {
                        SUserPreferences SUserPreferences = new SUserPreferences();
                        rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
                        SUserPreferences = null;
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
