using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Services;
using Zamba.Core;

public partial class Views_Client_Reintegros_Main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCmbFormTypes();
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
