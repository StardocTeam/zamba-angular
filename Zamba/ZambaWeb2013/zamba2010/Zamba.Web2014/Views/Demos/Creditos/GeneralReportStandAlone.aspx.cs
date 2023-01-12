using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using Zamba.Services;
using Zamba.Core;

public partial class Views_Aysa_GeneralReportStandAlone : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource2.ConnectionString = Zamba.Servers.Server.get_Con(false, true, false).ConString;

        //Actualiza el timemout
        if (!Page.IsPostBack && Session["User"] != null)
        {
            try
            {
                ZOptBusiness zopt = new ZOptBusiness();

                string title = zopt.GetValue("WebViewTitle");
                zopt = null;
                this.Title = (string.IsNullOrEmpty(title)) ? "Zamba Software" : title + " - Zamba Software";
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

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        String Query = string.Empty;
        try
        {
            Query = FormatStrQuery();

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

    protected String FormatStrQuery()
        {
            if (hdnIds.Value != null && hdnIds.Value != string.Empty)
            {
                var Ids = hdnIds.Value.Split(Char.Parse(","));

                String Query = String.Empty;
                Query = "Select ";

                String FirstDocTable = string.Empty;
                String FromQuery = String.Empty;

                    Dictionary<Int64, String> Indexs = new Dictionary<long, string>();

                    foreach (String Id in Ids)
                    {
                        if (Id != string.Empty)
                        {
                            Int64 EId = Int64.Parse(Id);
                            String DocTable = "Doc_I" + EId;

                            if (FirstDocTable == String.Empty) FirstDocTable = DocTable;

                            StringBuilder whereSLTS = new StringBuilder();
                            DataSet ds = Zamba.Servers.Server.get_Con(false,true,false).ExecuteDataset(System.Data.CommandType.Text, "Select INDEX_R_DOC_TYPE.INDEX_ID, DOC_INDEX.INDEX_NAME, DOC_INDEX.DROPDOWN from index_r_doc_type INNER JOIN DOC_INDEX on index_r_doc_type.index_id = doc_index.index_id where doc_type_id = " + Id);

                            foreach (DataRow r in ds.Tables[0].Rows)
                            {

                                if (Indexs.ContainsKey(Int64.Parse(r["INDEX_ID"].ToString())) == false)
                                {
                                    Indexs.Add(Int64.Parse(r["INDEX_ID"].ToString()), r["INDEX_NAME"].ToString().Trim());
                                    if (r["DROPDOWN"].ToString().Trim() == "2" || r["DROPDOWN"].ToString().Trim() == "4")
                                    {
                                        Query += DocTable + ".I" + r["INDEX_ID"] + " as [Codigo " + r["INDEX_NAME"].ToString().Trim() + "],";
                                        Query += "SLST_S" + r["INDEX_ID"] + ".Descripcion as [" + r["INDEX_NAME"].ToString().Trim() + "],";

                                        whereSLTS.Append(" left join SLST_S");
                                        whereSLTS.Append(r["INDEX_ID"]);
                                        whereSLTS.Append(" on convert(varchar,SLST_S");
                                        whereSLTS.Append(r["INDEX_ID"]);
                                        whereSLTS.Append(".codigo)=");
                                        whereSLTS.Append(DocTable);
                                        whereSLTS.Append(".I" + r["INDEX_ID"]);
                                    }
                                    else
                                    {
                                        Query += DocTable + ".I" + r["INDEX_ID"] + " as [" + r["INDEX_NAME"].ToString().Trim() + "],";
                                    }
                                }
                                else
                                {
                                    //                            Query += DocTable + ".I" + r["INDEX_ID"] + ",";
                                }
                            }

                            FromQuery += " from " + DocTable + whereSLTS.ToString();
                         }
                    }

                    Query = Query.Remove(Query.Length - 1);

                    return Query + " " + FromQuery;

            }
            return string.Empty;
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
        String Query=string.Empty;
        try
        {
            Query = FormatStrQuery();

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
        String Query=string.Empty;
        try
        {
            Query = FormatStrQuery();

            SqlDataSource2.SelectCommand = Query;
            SqlDataSource2.DataBind();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
            Zamba.Core.ZClass.raiseerror(new Exception("Query: " + Query.ToString()));
        }
    }

    protected void SetIds(int Id)
        {
            if (hdnIds.Value != null && hdnIds.Value != string.Empty)
            {
                //                var Ids = hdnIds.Value.Split(Char.Parse(","));

                String IdsString = hdnIds.Value;
                if (IdsString.Contains(Id.ToString()))
                {
                    hdnIds.Value = IdsString.Replace(Id.ToString(),String.Empty);
                    hdnIds.Value = hdnIds.Value.Replace(", ,", char.Parse(",").ToString());
                    hdnIds.Value = hdnIds.Value.Replace(",,", char.Parse(",").ToString());
                }
                else
                {
                    hdnIds.Value = IdsString + "," + Id.ToString();
                }

            }
            else
            {
                hdnIds.Value = Id.ToString();
            }
        }

    protected void btnApplyEntities_Click(object sender, EventArgs e)
    {
        String Query=string.Empty;
        try
        {
            Query = FormatStrQuery();

            SqlDataSource2.SelectCommand = Query;
            

            SqlDataSource2.DataBind();
            RadGrid1.Rebind();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
            Zamba.Core.ZClass.raiseerror(new Exception("Query: " + Query.ToString()));
        }
    }

    protected void chk1027_CheckedChanged(object sender, EventArgs e)
    {
        SetIds(1024);
    }

    protected void chk1040_CheckedChanged(object sender, EventArgs e)
    {
        SetIds(1023);

    }

    protected void chk1045_CheckedChanged(object sender, EventArgs e)
    {
        SetIds(1025);

    }

    protected void chk1038_CheckedChanged(object sender, EventArgs e)
    {
        SetIds(1029);

    }
    
    protected void RadFilter1_PreRender(object sender, EventArgs e)
    {
        var menu = RadFilter1.FindControl("rfContextMenu") as RadContextMenu;
        menu.DefaultGroupSettings.Height = Unit.Pixel(400);
        menu.EnableAutoScroll = true;
    }

    public void ConfigureExport()
    {
        RadGrid1.ExportSettings.ExportOnlyData = CheckBox1.Checked;
        RadGrid1.ExportSettings.IgnorePaging = CheckBox2.Checked;
        RadGrid1.ExportSettings.OpenInNewWindow = CheckBox3.Checked;
    }

    public int GetTaskIdByIdIndustria(int IdIndustria)
    {
        try
        {
        DataSet ds = Zamba.Servers.Server.get_Con(false,true,false).ExecuteDataset(System.Data.CommandType.Text, "Select task_id from wfdocument inner join doc_i1027 on wfdocument.doc_id = doc_i1027.doc_id where i1217 = " + IdIndustria);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            return int.Parse(ds.Tables[0].Rows[0]["Task_Id"].ToString());
        }
        return 0;
        }
        catch
        {            
            return 0;
        }
    }

    protected void OpenTask()
    {

        //TO-DO: llamar al opener, y validar que sea el correcto.
        try
        {
            if (hdnIdIndustria != null && hdnIdIndustria.Value != null && hdnIdIndustria.Value != string.Empty)
            {
                int TaskId = GetTaskIdByIdIndustria(int.Parse(hdnIdIndustria.Value));
                string TaskName = "Industria";
                String script = String.Empty;
                if (TaskId != 0)
                {
                    script = "OpenTaskInOpener('" + "../WF/TaskViewer.aspx?taskid=" + TaskId + "'," + TaskId + ",'" + TaskName + "');";
                }

                Page.ClientScript.RegisterStartupScript(typeof(Page), "DoOpenTaskScript", script, true);

                hdnIdIndustria.Value = string.Empty;
            }
        }
        catch
        {
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        OpenTask();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ConfigureExport();
        RadGrid1.MasterTableView.ExportToExcel();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        ConfigureExport();
        RadGrid1.MasterTableView.ExportToCSV();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ConfigureExport();
        RadGrid1.MasterTableView.ExportToWord();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        ConfigureExport();
        RadGrid1.MasterTableView.ExportToPdf();
    }
}