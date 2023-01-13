using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {

        Int32 Value = Int32.Parse(DropDownList1.SelectedValue);

        switch (Value)
        {
            case 1:
                wpmMainManager.DisplayMode = WebPartManager.BrowseDisplayMode;
                break;
            case 2:
                wpmMainManager.DisplayMode = WebPartManager.DesignDisplayMode;
                break;
            case 3:
                wpmMainManager.DisplayMode = WebPartManager.EditDisplayMode;
                break;
            case 4:
                wpmMainManager.DisplayMode = WebPartManager.CatalogDisplayMode;
                break;
        }

    }
}
