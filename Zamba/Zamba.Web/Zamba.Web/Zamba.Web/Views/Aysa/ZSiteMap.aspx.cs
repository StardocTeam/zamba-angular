using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using Zamba.Core;
using Telerik.Web;
using System.Text;
using Telerik.Web.UI;
using Zamba.Membership;
using System.Data;
using Zamba;

public partial class Views_Aysa_ZSiteMap : System.Web.UI.Page
{
    RightsBusiness RiB = new RightsBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadZSiteMap();
    }

    private void LoadZSiteMap()
    {
        try
        {
            LoadPrincipalMap();//SELECT [ID], [Title], [URL], [ParentID] FROM [ZSiteMap]
            LoadRSMHome();//Botones dinamicos configurados desde el Admin Zamba
            LoadRSMHeader();
        }

        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    private void LoadPrincipalMap()
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
                    item.Visible = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.Create, 1027);               
            }
        }
    }

    private void LoadRSMHome()
    {   
        var homeBtn=DynamicButtonBusiness.GetInstance().GetHomeButtons(Zamba.Membership.MembershipHelper.CurrentUser);
        if (homeBtn.Count==0) return;
        RSMHome.DataSource = DynamicButtonsDS(homeBtn, "Inicio");
   
        RSMHome.DataFieldID = "ID";
        RSMHome.DataTextField = "Title";
        RSMHome.DataFieldParentID = "ParentID";
        RSMHome.DataNavigateUrlField = "URL";
        RSMHome.ShowNodeLines = true;
        RSMHome.DataBind();

        RadSiteMapNodeCollection siteMapNodes = RSMHome.Nodes;
    }
    private void LoadRSMHeader()
    {
        var homeBtn = DynamicButtonBusiness.GetInstance().GetHeaderButtons(Zamba.Membership.MembershipHelper.CurrentUser);
        if (homeBtn.Count == 0) return;
        RSMHeader.DataSource = DynamicButtonsDS(homeBtn, "Principal");

        RSMHeader.DataFieldID = "ID";
        RSMHeader.DataTextField = "Title";
        RSMHeader.DataFieldParentID = "ParentID";
        RSMHeader.DataNavigateUrlField = "URL";
        RSMHeader.ShowNodeLines = true;
        RSMHeader.DataBind();

        RadSiteMapNodeCollection siteMapNodes = RSMHeader.Nodes;
    }
    private DataSet DynamicButtonsDS(List<IDynamicButton> dBtnList, string place)
    {
        var ds = new DataSet();         
        var dt = new DataTable();
        var dC = new List<DataColumn>();
        dC.Add(new DataColumn("ID", Type.GetType("System.Int32")));
        dC.Add(new DataColumn("Title", Type.GetType("System.String")));
        dC.Add(new DataColumn("URL", Type.GetType("System.String")));
        dC.Add(new DataColumn("ParentID", Type.GetType("System.Int32")));              
        dt.Columns.AddRange(dC.ToArray());

        //Cabecera de mapa
        var i = 1;
       var dr = dt.NewRow();
        dr["ID"] = i++;
        dr["Title"] =place;
        dr["URL"] = "#";        
        dt.Rows.Add(dr);

        foreach (ZDynamicButton dBtn in dBtnList)
        {
            dr = dt.NewRow();
            dr["ID"] = i++;
            dr["Title"] = dBtn.Caption;
            dr["URL"] = "../../ZDispatcher.aspx?Action=playrule&RuleId=" + dBtn.RuleId;
            dr["ParentID"] = 1;
            dt.Rows.Add(dr);
        }     
        
        ds.Tables.Add(dt);
      return ds;
    }

}
