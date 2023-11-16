﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using Zamba.Services;
using Zamba.Core;
using System.Web.Security;

public partial class Views_Aysa_ComplaintsReport : System.Web.UI.Page
{
    long lastDocIDSelected = 0;
    private const string PAGELOCATION = "Aysa/GDI/ComplaintsReport.aspx{0}";
    private const string TASKSELECTORURL = "WF/TaskSelector.ashx?DocId={0}&DocTypeId={1}";
    const string REPORTQUERY =
" SELECT     WFStep.Name AS Etapa, USRTABLE.NAME AS [Usuario_Asignado],  " +
" WFDocument.Name AS Tarea, DOC_I11075.I1187 AS [Codificacion_de_la_Industria],   " +
" DOC_I11075.I1188 AS [Razon_Social], DOC_I11075.I1192 AS Calle, DOC_I11075.I1193 AS Num, SLST_S11297.Descripcion AS [Motivo_de_la intimacion],   " +
" SLST_S11320.Descripcion AS [Tipo_Denuncia], DOC_I11075.I1229 AS Observaciones, SLST_S1181.Descripcion AS Region,   " +
" DOC_I11075.I11298 AS [Nro_Siseme], wfdocument.Doc_ID as [ID de Zamba], " +
" CONVERT(nvarchar(10), DOC_I11075.I11344, 103) AS [Fecha_Ingreso_Siseme],   " +
" CONVERT(nvarchar(10), DOC_I11075.I11318, 103) AS [Fecha_ultima_CN_CD],   " +
" CONVERT(nvarchar(10), DOC_I11075.I11355, 103) AS [Fecha_CD],   " +
" CONVERT(nvarchar(10), DOC_I11075.I11319, 103) AS [Fecha_de_Recepcion]  " +
" FROM           WFDocument  INNER JOIN " +
" WFStep ON dbo.WFDocument.step_Id = WFStep.step_Id INNER JOIN " +
" WFStepStates ON WFDocument.Do_State_ID = WFStepStates.Doc_State_ID INNER JOIN " +
" DOC_I11075 ON WFDocument.Doc_ID = DOC_I11075.DOC_ID LEFT OUTER JOIN    " +
" USRTABLE ON WFDocument.User_Asigned = USRTABLE.ID LEFT OUTER JOIN  " +
" SLST_S1181 ON DOC_I11075.I1181 = SLST_S1181.Codigo LEFT OUTER JOIN " +
" SLST_S11320 ON DOC_I11075.I11320 = SLST_S11320.Codigo LEFT OUTER JOIN " +
" slst_s11297 ON Doc_I11075.I11297 = slst_s11297.Codigo " ;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Actualiza el timemout
            if (Session["User"] != null)
            {
                SqlDataSource2.ConnectionString = Zamba.Servers.Server.get_Con(false, false, false).ConString;
				IUser user = (IUser)Session["User"];                
                SqlDataSource2.SelectCommand = getQuery(REPORTQUERY);
                
                if (!Page.IsPostBack)
                {
                    this.Title = "Reporte de Denuncias" + " - Zamba Software";
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
                        FormsAuthentication.RedirectToLoginPage();
                    rights = null;
                    RadGrid1.MasterTableView.Rebind();

                }
                else
                {
                    if (RadGrid1.SelectedItems.Count > 0)
                    {
                        long.TryParse(RadGrid1.SelectedItems[0].Cells[RadGrid1.SelectedItems[0].Cells.Count - 1].Text, out lastDocIDSelected);
                    }

                }


            }
            else
                FormsAuthentication.RedirectToLoginPage();
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    protected string getQuery(string InitialQuery)
    {
        string Query = InitialQuery;
        string whereQuery = GetWhereForRegion();

        if (!(string.IsNullOrEmpty(Query) || Query.Contains("where")) && !string.IsNullOrEmpty(whereQuery))
        {
            Query += " where " + whereQuery;
        }
        if (Query.Contains("where") && !string.IsNullOrEmpty(whereQuery)) { Query += " and " + whereQuery; }


        return Query;
    }

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
	String Query = String.Empty;
        try
        {
		Query = getQuery(REPORTQUERY);
            SqlDataSource2.SelectCommand = Query;
            SqlDataSource2.DataBind();
            RadGrid1.Rebind();

            if (e.CommandName == "FilterRadGrid")
            {
                RadFilter1.FireApplyCommand();
            }

            if (e.CommandName == "ExportToExcel")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToExcel();
            }

            if (e.CommandName == "ExportToCSV")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToCSV();
            }

            if (e.CommandName == "ExportToWord")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToWord();
            }

            if (e.CommandName == "ExportToPDF")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToPdf();
            }

            if (e.CommandName == "OpenTask")
            {
                OpenTask();
            }

            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
              e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
              e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
            {
                ConfigureExport();
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
            Zamba.Core.ZClass.raiseerror(new Exception("Query: " + Query.ToString()));
        }
    }

    protected void Header1_SkinChanged(object sender, SkinChangedEventArgs e)
    {
        //Required for dynamic skin changing
        RadGrid1.Rebind();
    }

    protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
    {
        //
    }

    protected string GetFilterIcon()
    {
        return RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Vista.Grid.Filter.gif");
    }

    protected string GetExcelIcon()
    {
        return "../../Content/images/icons/3.png";
    }

    protected string GetCSVIcon()
    {
        return "../../Content/images/icons/8.png";
    }

    protected string GetWordIcon()
    {
        return "../../Content/images/icons/2.png";
    }

    protected string GetPDFIcon()
    {
        return "../../Content/images/icons/4.png";
    }

    protected string GetOpenTaskIcon()
    {
        return "../../Content/images/icons/30.png";
    }

    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
	String Query = String.Empty;
        try
        {
		Query = getQuery(REPORTQUERY);
            SqlDataSource2.SelectCommand = Query; 
            SqlDataSource2.DataBind();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
            Zamba.Core.ZClass.raiseerror(new Exception("Query: " + Query.ToString()));
        }
    }

    protected void RadGrid1_Load(object sender, EventArgs e)
    {
        try
        {
        foreach (GridColumn column in RadGrid1.MasterTableView.RenderColumns)
        {
            if (column.UniqueName.Contains("Zamba"))
            {
                column.Visible = false;
            }
        }
        RadGrid1.Rebind();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    protected void RadFilter1_PreRender(object sender, EventArgs e)
    {
        try
        {
        var menu = RadFilter1.FindControl("rfContextMenu") as RadContextMenu;
        menu.DefaultGroupSettings.Height = Unit.Pixel(400);
        menu.EnableAutoScroll = true;
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    public void ConfigureExport()
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
    }

    protected void OpenTask()
    {
        //TO-DO: llamar al opener, y validar que sea el correcto.
        try
        {
            String script = String.Empty;
            if (lastDocIDSelected > 0)
            {
                string docName = new SResult().GetResultName(lastDocIDSelected, 11075);
                Uri currUrl = HttpContext.Current.Request.Url;

                string urlToOpen = currUrl.ToString().Replace(string.Format(PAGELOCATION, currUrl.Query), string.Format(TASKSELECTORURL, lastDocIDSelected, 11075));

                script = "OpenTaskInOpener('" + urlToOpen + "'," + lastDocIDSelected + ",'" + docName + "');";
            }
            else
            {
                script = "alert('No se ha seleccionado ningun documento para visualizar');";
            }

            Page.ClientScript.RegisterStartupScript(typeof(Page), "DoOpenTaskScript", script, true);
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            OpenTask();
        }
        catch (Exception ex)
        {
               Zamba.Core.ZClass.raiseerror(ex);
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckBox2.Checked == true)
            {
                SFileTools sf = new SFileTools();
                String Query = string.Empty;
                Query = getQuery(REPORTQUERY);

                String url = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/log/temp/exportComplaints" + Zamba.Membership.MembershipHelper.CurrentUser.ID + ".xls";

                String path = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\exportComplaints" + Zamba.Membership.MembershipHelper.CurrentUser.ID + ".xls";

                if (sf.ExportToXLS(ServersBusiness.BuildExecuteDataSet(CommandType.Text, Query).Tables[0], path))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("window.open('{0}');", url), true);
                }
            }
            else
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToExcel();
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }

    }

   protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckBox2.Checked == true)
            {
                SFileTools sf = new SFileTools();
                String Query = string.Empty;
				Query = getQuery(REPORTQUERY);


                String url = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/log/temp/exportComplaints" + Zamba.Membership.MembershipHelper.CurrentUser.ID + ".csv";

                String path = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\exportComplaints" + Zamba.Membership.MembershipHelper.CurrentUser.ID + ".csv";

                if (sf.ExportToCSV(ServersBusiness.BuildExecuteDataSet(CommandType.Text, Query).Tables[0], path))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("window.open('{0}');", url), true);
                }
            }
            else
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToCSV();
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }

    }

    private string GetWhereForRegion()
    {
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("select g.name as GroupName ");
        sbQuery.Append("from USR_R_GROUP usrG ");
        sbQuery.Append("left outer join USRGROUP G on G.id = usrG.GROUPID ");
        sbQuery.Append("where Usrid = ");
        sbQuery.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString());

        DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sbQuery.ToString());

        if (ds.Tables.Count > 0)
        {
            DataTable dtRegiones = ds.Tables[0];

            List<string> regions = new List<string>();

            foreach (DataRow row in dtRegiones.Rows)
            {
                if (row["GroupName"] != null)
                {
                    string groupName = row["GroupName"].ToString();

                    switch (groupName)
                    {
                        case "DRCF":
                            regions.Add("Doc_i11075.I1181 = 1");
                            break;
                        case "DRN":
                            regions.Add("Doc_i11075.I1181 = 2");
                            break;
                        case "DRO":
                            regions.Add("Doc_i11075.I1181 = 5");
                            break;
                        case "DRSE":
                            regions.Add("Doc_i11075.I1181 = 3");
                            break;
                        case "DRSO":
                            regions.Add("Doc_i11075.I1181 = 4");
                            break;
                    }
                }
            }
            sbQuery = new StringBuilder();
            if (regions.Count > 1)
            {
                sbQuery.Append("(");
                foreach (string item in regions)
                {
                    sbQuery.Append(item);
                    sbQuery.Append(" OR ");
                }
                if (sbQuery.ToString().Trim().EndsWith("OR"))
                {
                    sbQuery.Remove(sbQuery.ToString().LastIndexOf("OR "), 3);
                }

                sbQuery.Append(")");
                sbQuery.Append(" OR Doc_i11075.i1181 is null");
            }
            else
            {
                if (regions.Count > 0)
                {
                    sbQuery.Append(regions[0]);
                    sbQuery.Append(" OR Doc_i11075.i1181 is null");
                }
            }
        }

        return sbQuery.ToString();
    }
}