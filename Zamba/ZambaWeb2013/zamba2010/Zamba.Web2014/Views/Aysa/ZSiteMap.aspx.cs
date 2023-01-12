using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using Zamba.Core;
using Telerik.Web;
using System.Text;
using Telerik.Web.UI;

public partial class Views_Aysa_ZSiteMap : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadZSiteMap();
    }

    private void LoadZSiteMap()
    {
        try
        {
            RadSiteMap1.DataSource = ToolsBusiness.GetZiteMap();
           
            RadSiteMap1.DataFieldID = "ID";
            RadSiteMap1.DataTextField = "Title";
            RadSiteMap1.DataFieldParentID = "ParentID";
            RadSiteMap1.DataNavigateUrlField = "URL";            
            RadSiteMap1.ShowNodeLines = true;
            
            RadSiteMap1.DataBind();

            RadSiteMapNodeCollection siteMapNodes = RadSiteMap1.Nodes;
            if (siteMapNodes != null && siteMapNodes.Count > 0)
            {
                foreach (RadSiteMapNode item in siteMapNodes[0].Nodes)
                {
                    if (item.Text == "Alta de industria (Permite ingresar una nueva industria al sistema)") 
                    {
                        item.Visible = RightsBusiness.GetUserRights(ObjectTypes.DocTypes, RightsType.Create, 1027,0);
                    }
                }
            }
        }

        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }
}
 