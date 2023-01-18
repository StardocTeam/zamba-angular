using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using Zamba.Services;
using Zamba.Core;
using System.Web.Security;

public partial class Views_Aysa_GeneralReportStandAlone : System.Web.UI.Page
{

    string FilteredQuery
    {
        get
        {
            return (string)Session["GR_FilteredQuery"];
        }
        set
        {
            Session["GR_FilteredQuery"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Actualiza el timemout
            if (Session["User"] != null)
            {
                SqlDataSource2.ConnectionString = Zamba.Servers.Server.get_Con(false, false, false).ConString;
                IUser user = (IUser)Session["User"];

                if (!Page.IsPostBack)
                {
                    txtMaxResults.Text = "1000";
                    ZOptBusiness zopt = new ZOptBusiness();
                    string title = zopt.GetValue("WebViewTitle");
                    zopt = null;
                    this.Title = (string.IsNullOrEmpty(title)) ? "Zamba Software" : title + " - Zamba Software";
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

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            List<string> l = GetIds();
            if (l == null || l.Count == 0)
            {
                SqlDataSource2.SelectCommand = string.Empty;
                SqlDataSource2.DataBind();
                RadGrid1.Rebind();
                SetVisibility(false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DoAlert", "$('#RadFilter1').hide(); alert('No se ha seleccionado ninguna entidad');", true);

                return;
            }

            RadFilter1.FireApplyCommand();
            if (e.CommandName == "OpenTask")
            {
                OpenTask();
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    protected String FormatStrQuery(List<string> Ids)
    {
        Dictionary<Int64, String> indexs = null;
        DataSet ds = null;
        long eId;
        String docTable;
        StringBuilder whereSLTS = null;

        try
        {
            if (Ids.Count > 0)
            {
                String Query = String.Empty;
                Query = "Select ";
                Query += "WFStep.Name AS Etapa, WFStepStates.Description AS Estado_de_Tarea,";
                String FirstDocTable = string.Empty;
                String FromQuery = String.Empty;
                indexs = new Dictionary<long, string>();

                foreach (String Id in Ids)
                {
                    if (Id != string.Empty)
                    {
                        eId = Int64.Parse(Id);
                        docTable = "Doc_I" + eId;

                        if (FirstDocTable == String.Empty) FirstDocTable = docTable;

                        whereSLTS = new StringBuilder();
                        ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(System.Data.CommandType.Text, "Select INDEX_R_DOC_TYPE.INDEX_ID, DOC_INDEX.INDEX_NAME, DOC_INDEX.DROPDOWN from index_r_doc_type INNER JOIN DOC_INDEX on index_r_doc_type.index_id = doc_index.index_id where doc_type_id = " + Id);

                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            if (indexs.ContainsKey(Int64.Parse(r["INDEX_ID"].ToString())) == false)
                            {
                                indexs.Add(Int64.Parse(r["INDEX_ID"].ToString()), r["INDEX_NAME"].ToString().Trim().Replace(" ", "_").Replace("/", "-"));
                                if (r["DROPDOWN"].ToString().Trim() == "2" || r["DROPDOWN"].ToString().Trim() == "4")
                                {
                                    Query += docTable + ".I" + r["INDEX_ID"] + " as [Codigo_" + r["INDEX_NAME"].ToString().Trim().Replace(" ", "_").Replace("/", "-") + "],";
                                    Query += "SLST_S" + r["INDEX_ID"] + ".Descripcion as [" + r["INDEX_NAME"].ToString().Trim().Replace(" ", "_").Replace("/", "-") + "],";
                                    whereSLTS.Append(" left join SLST_S");
                                    whereSLTS.Append(r["INDEX_ID"]);
                                    whereSLTS.Append(" on convert(varchar,SLST_S");
                                    whereSLTS.Append(r["INDEX_ID"]);
                                    whereSLTS.Append(".codigo)=");
                                    whereSLTS.Append(docTable);
                                    whereSLTS.Append(".I" + r["INDEX_ID"]);
                                }
                                else
                                {
                                    Query += docTable + ".I" + r["INDEX_ID"] + " as [" + r["INDEX_NAME"].ToString().Trim().Replace(" ", "_").Replace("/", "-") + "],";
                                }
                            }
                        }
                        Boolean InnerJoin = false;
                        if (FromQuery.Contains("from"))
                        {
                            if (InnerJoin) { FromQuery += " inner join " + docTable; } else { FromQuery += " left join " + docTable; }
                            FromQuery += " on " + docTable + ".i1217 = " + FirstDocTable + ".i1217" + whereSLTS.ToString();
                        }
                        else
                        {
                            FromQuery += " from " + docTable + whereSLTS.ToString();
                            FromQuery += " LEFT JOIN WFDocument ON " + docTable + ".DOC_ID = WFDocument.Doc_ID LEFT JOIN WFStep ON WFDocument.step_Id = WFStep.step_Id LEFT JOIN WFStepStates ON WFDocument.Do_State_ID = WFStepStates.Doc_State_ID ";
                        }
                    }
                }

                Query = Query.Remove(Query.Length - 1);

                return Query + " " + FromQuery;

            }
            return string.Empty;
        }
        finally
        {
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }
            if (indexs != null)
            {
                indexs.Clear();
                indexs = null;
            }
            if (whereSLTS != null)
            {
                whereSLTS.Length = 0;
                whereSLTS = null;
            }
        }
    }

    private List<string> GetIds()
    {
        List<string> Ids = new List<string>();
        CheckBox c;
        foreach (Control item in panelEntidades.Controls)
        {
            c = item as CheckBox;
            if (c != null && c.Checked) Ids.Add((c.ID.ToString().ToLower().Replace("chk", "")));
        }
        return Ids;
    }

    protected void Header1_SkinChanged(object sender, SkinChangedEventArgs e)
    {
        //Required for dynamic skin changing
        //RadGrid1.Rebind();
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
        String Query = string.Empty;
        try
        {
            Query = GetQuery(true);
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
            //SqlDataSource2.SelectCommand = FilteredQuery;
            //SqlDataSource2.DataBind();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    private String GetQuery(bool useTop)
    {
        String Query = string.Empty;
        try
        {
            List<string> Ids = GetIds();
            if (Ids.Count > 1 && chk1027.Checked == false)
            {
                chk1027.Checked = true;
                Ids.Add("1027");
            }

            int maxResults = (string.IsNullOrEmpty(txtMaxResults.Text)) ? 1000 : int.Parse(txtMaxResults.Text);

            string whereQuery = GetWhereForRegion();
            Query = FormatStrQuery(Ids);

            if (!(string.IsNullOrEmpty(Query) || Query.Contains("where")) && !string.IsNullOrEmpty(whereQuery))
            {
                Query += " where " + whereQuery;
            }

            if (useTop)
            {
                Query = "set rowcount " + maxResults + " " + Query + " set rowcount 0";
            }

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se genero la siguiente consulta para el reporte general : ");
            ZTrace.WriteLineIf(ZTrace.IsInfo, Query);
            ZTrace.WriteLineIf(ZTrace.IsInfo, "");

            return Query;
        }
        catch (Exception)
        {
            Zamba.Core.ZClass.raiseerror(new Exception("Query: " + Query.ToString()));
            return string.Empty;
        }
    }

    protected void btnApplyEntities_Click(object sender, EventArgs e)
    {
        try
        {
            RadGrid1.MasterTableView.FilterExpression = "";
            RadFilter1.RootGroup.Expressions.Clear();
            RadFilter1.RecreateControl();
            FilteredQuery = GetQuery(true);
            SqlDataSource2.SelectCommand = FilteredQuery;
            SqlDataSource2.DataBind();
            RadGrid1.Rebind();
            FormatColumnsView();
            SetVisibility((RadGrid1.MasterTableView.Items.Count > 0));
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    private void FormatColumnsView()
    {
        foreach (GridColumn col in RadGrid1.MasterTableView.RenderColumns)
        {
            if (col is GridDateTimeColumn)
            {
                GridDateTimeColumn column = (GridDateTimeColumn)col;

                column.EditDataFormatString = "{0:dd/MM/yyyy}";
                column.DataFormatString = "{0:dd/MM/yyyy}";
                column.EditFormHeaderTextFormat = "{0:dd/MM/yyyy}";

            }
        }
        RadGrid1.MasterTableView.Rebind();
    }

    private void SetVisibility(bool visible)
    {
        RadFilter1.Visible = visible;
        panelAcciones.Visible = visible;
        RadGrid1.Visible = visible;
        lblAviso.Visible = !visible;
    }

    protected void RadFilter1_PreRender(object sender, EventArgs e)
    {
        var menu = RadFilter1.FindControl("rfContextMenu") as RadContextMenu;
        menu.DefaultGroupSettings.Height = Unit.Pixel(400);
        menu.EnableAutoScroll = true;

        IEnumerable<RadDateTimePicker> pickers = ControlsHelper.FindControlsOfType<RadDateTimePicker>(RadFilter1);

        foreach (RadDateTimePicker picker in pickers)
        {
            picker.TimePopupButton.Style.Add("visibility", "hidden");
            picker.TimePopupButton.Style.Add("display", "none");
            picker.DateInput.DisplayDateFormat = "dd/MM/yyyy";
            picker.DateInput.DateFormat = "dd/MM/yyyy";
        }
        IEnumerable<RadNumericTextBox> controls = ControlsHelper.FindControlsOfType<RadNumericTextBox>(RadFilter1);
        foreach (RadNumericTextBox textBox in controls)
        {
            textBox.NumberFormat.GroupSeparator = string.Empty;
            textBox.NumberFormat.DecimalDigits = 5;
            textBox.NumberFormat.AllowRounding = true;
        }

    }

    protected String GetExpressionFormatedOf(Telerik.Web.UI.RadFilterGroupExpression rootExpression)
    {
        const String betweenGroupOperator = " AND ";
        const String orGroupOperator = " OR ";
        const String dateTimeType = "datetime";
        string expression = string.Empty;
        string groupOperator = rootExpression.GroupOperation.ToString();
        StringBuilder expressionBuilder = new StringBuilder();
        int i = 0;
        Telerik.Web.UI.RadFilterQueryProvider provider = new RadFilterSqlQueryProvider();
        RadFilterGroupExpression groupExpression = new RadFilterGroupExpression();

        foreach (Telerik.Web.UI.RadFilterExpression subExpression in rootExpression.Expressions)
        {
            switch (subExpression.FilterFunction)
            {
                case RadFilterFunction.Group:
                    groupExpression = (Telerik.Web.UI.RadFilterGroupExpression)subExpression;
                    expression = "(" +  GetExpressionFormatedOf(groupExpression) + ")";
                    break;
                default:

                    groupExpression.AddExpression(subExpression);
                    provider.ProcessGroup(groupExpression);
                    expression = provider.Result;
                    if (((Telerik.Web.UI.RadFilterNonGroupExpression)subExpression).FieldType.Name.ToLower().Contains(dateTimeType))
                    {
                        if (textContainsADate(expression))
                        {
                            if (expression.Contains(betweenGroupOperator))
                            {
                                foreach (string BetweenExpression in expression.Split(new string[] { betweenGroupOperator },
                                    StringSplitOptions.RemoveEmptyEntries))
                                {
                                    expression = expression.Replace(BetweenExpression, getDateFormated(BetweenExpression));
                                }
                            }
                            else if (expression.Contains(orGroupOperator))
                            {
                                foreach (string BetweenExpression in expression.Split(new string[] { orGroupOperator },
                                    StringSplitOptions.RemoveEmptyEntries))
                                {
                                    expression = expression.Replace(BetweenExpression, getDateFormated(BetweenExpression));
                                }
                            }
                            else
                            {
                               expression = expression.Replace(expression, getDateFormated(expression));
                            }
                        }
                    }
                    groupExpression.Expressions.Remove(subExpression);
                    break;

            }

            if (i > 0)
            {
                expressionBuilder.Append(groupOperator);
                expressionBuilder.Append(" ");
            }


            expressionBuilder.Append(expression);
            expressionBuilder.Append(" ");
            i++;
        }

        return expressionBuilder.ToString();
    }

    private static bool textContainsADate(string text)
    {
        string expressionToEvaluate;
        const string operatorSeparator = "'";
        const string dateTimeSeparator = " ";
        const string regularExpression = "^\\d{2}/\\d{2}/\\d{4}$";

        if (text.Contains(operatorSeparator))
        {
            expressionToEvaluate = text.Split(new string[] { operatorSeparator }, StringSplitOptions.RemoveEmptyEntries)[1];
            expressionToEvaluate = expressionToEvaluate.Split(new string[] { dateTimeSeparator }, StringSplitOptions.RemoveEmptyEntries)[0];
            return System.Text.RegularExpressions.Regex.IsMatch(expressionToEvaluate, regularExpression);
        }

        return false;
    }



    private static string getDateFormated(string expression)
    {
        string newDate, oldDate;

        oldDate = expression.Split(new string[] { "'" }, StringSplitOptions.RemoveEmptyEntries)[1].ToString();
        newDate = Zamba.Servers.Server.get_Con(false, true, false).ConvertDate(oldDate.Split(new string[] { " " },
            StringSplitOptions.RemoveEmptyEntries)[0].ToString().Replace("'", ""));
        expression = expression.Replace("'" + oldDate + "'", newDate);

        oldDate = null;
        newDate = null;

        return expression;
    }

    protected void ApplyExpressions(object sender, Telerik.Web.UI.RadFilterApplyExpressionsEventArgs e)
    {

        String s = GetExpressionFormatedOf(e.ExpressionRoot);

        int maxResults = (string.IsNullOrEmpty(txtMaxResults.Text)) ? 1000 : int.Parse(txtMaxResults.Text);
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("set rowcount ");
        sbQuery.AppendLine(maxResults.ToString());
        sbQuery.Append("select * from (");
        sbQuery.Append(GetQuery(false));

        if (!string.IsNullOrEmpty(s))
        {
            sbQuery.Append(") as Q ");
            sbQuery.Append(" where ");
            sbQuery.AppendLine(s);

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se aplico el siguiente filtro : ");
            ZTrace.WriteLineIf(ZTrace.IsInfo, s.Replace("(", "").Replace(")", ""));
            ZTrace.WriteLineIf(ZTrace.IsInfo, "");

        }
        else
            sbQuery.AppendLine(") as Q ");

        sbQuery.Append("set rowcount 0");
        FilteredQuery = sbQuery.ToString();

        ZTrace.WriteLineIf(ZTrace.IsInfo, "La consulta finalmente es : ");
        ZTrace.WriteLineIf(ZTrace.IsInfo, FilteredQuery);
        ZTrace.WriteLineIf(ZTrace.IsInfo, "");

        SqlDataSource2.SelectCommand = FilteredQuery;

        sbQuery.Length = 0;
        sbQuery = null;

        SqlDataSource2.DataBind();
        RadGrid1.Rebind();
        FormatColumnsView();
    }

    public void ConfigureExport()
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
    }

    public int GetTaskIdByIdIndustria(int IdIndustria)
    {
        DataSet ds = null;
        try
        {
            ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, "SELECT doc_id FROM doc_i1027 WHERE i1217 = " + IdIndustria);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0]["doc_id"].ToString());
            }
            return 0;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            return 0;
        }
        finally
        {
            ds.Dispose();
            ds = null;
        }
    }

    protected void OpenTask()
    {
        //TO-DO: llamar al opener, y validar que sea el correcto.
        try
        {
            String script = String.Empty;

            if (hdnIdIndustria != null && hdnIdIndustria.Value != null && hdnIdIndustria.Value != string.Empty)
            {
                int docId = GetTaskIdByIdIndustria(int.Parse(hdnIdIndustria.Value));
                string docName = new SResult().GetResultName(docId, 1027);
                if (docId != 0)
                {
                    script = "OpenTaskInOpener('" + "../WF/TaskSelector.ashx?DocId=" + docId + "&DocTypeId=1027'," + docId + ",'" +
                             docName + "');";
                }

                hdnIdIndustria.Value = string.Empty;
            }
            else
            {
                script = "alert('No se ha seleccionado ningun documento para visualizar');";
            }

            Page.ClientScript.RegisterStartupScript(typeof(Page), "DoOpenTaskScript", script, true);

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        OpenTask();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SFileTools sf = null;
        DataSet ds = null;
        try
        {
            sf = new SFileTools();

            bool res;

            if (CheckBox2.Checked == true)
            {
                ds = ServersBusiness.BuildExecuteDataSet(CommandType.Text, GetQuery(false));
            }
            else
            {
                ds = ServersBusiness.BuildExecuteDataSet(CommandType.Text, FilteredQuery);
            }

            String url = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/log/temp/exportGeneralReport" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "_" + ds.Tables[0].Rows.Count + ".xls";

            String path = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\exportGeneralReport" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "_" + ds.Tables[0].Rows.Count + ".xls";

            res = sf.ExportToXLS(ds.Tables[0], path);

            if (res)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("window.open('{0}');", url), true);
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally
        {
            if (sf != null)
            {
                sf = null;
            }
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        SFileTools sf = null;
        DataSet ds = null;

        try
        {
            sf = new SFileTools();

            bool res;

            if (CheckBox2.Checked == true)
            {
                ds = ServersBusiness.BuildExecuteDataSet(CommandType.Text, GetQuery(false));
            }
            else
            {
                ds = ServersBusiness.BuildExecuteDataSet(CommandType.Text, FilteredQuery);
            }

            String url = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/log/temp/exportGeneralReport" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "_" + ds.Tables[0].Rows.Count + ".csv";

            String path = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\exportGeneralReport" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "_" + ds.Tables[0].Rows.Count + ".csv";

            res = sf.ExportToCSV(ds.Tables[0], path);

            if (res)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("window.open('{0}');", url), true);
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally
        {
            if (sf != null)
            {
                sf = null;
            }
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }
        }
    }

    private string GetWhereForRegion()
    {
        DataSet ds = null;
        StringBuilder sbQuery = null;
        try
        {
            sbQuery = new StringBuilder();
            sbQuery.Append("select g.name as GroupName ");
            sbQuery.Append("from USR_R_GROUP usrG ");
            sbQuery.Append("left outer join USRGROUP G on G.id = usrG.GROUPID ");
            sbQuery.Append("where Usrid = ");
            sbQuery.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString());

            ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sbQuery.ToString());

            if (ds.Tables.Count > 0)
            {
                DataTable dtRegiones = ds.Tables[0];

                List<string> regions = new List<string>();

                foreach (DataRow row in dtRegiones.Rows)
                {
                    if (row["GroupName"] != null)
                    {
                        string groupName = row["GroupName"].ToString();

                        if (chk1027.Checked == true)
                        {
                            switch (groupName)
                            {
                                case "DRCF":
                                    regions.Add("Doc_I1027.I1181 = 1");
                                    break;
                                case "DRN":
                                    regions.Add("Doc_I1027.I1181 = 2");
                                    break;
                                case "DRO":
                                    regions.Add("Doc_I1027.I1181 = 5");
                                    break;
                                case "DRSE":
                                    regions.Add("Doc_I1027.I1181 = 3");
                                    break;
                                case "DRSO":
                                    regions.Add("Doc_I1027.I1181 = 4");
                                    break;
                            }
                        }
                        else
                        {
                            switch (groupName)
                            {
                                case "DRCF":
                                    regions.Add("I1181 = 1");
                                    break;
                                case "DRN":
                                    regions.Add("I1181 = 2");
                                    break;
                                case "DRO":
                                    regions.Add("I1181 = 5");
                                    break;
                                case "DRSE":
                                    regions.Add("I1181 = 3");
                                    break;
                                case "DRSO":
                                    regions.Add("I1181 = 4");
                                    break;
                            }
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

                    if (chk1027.Checked == true)
                        sbQuery.Append(" OR Doc_I1027.i1181 is null");
                    else
                        sbQuery.Append(" OR i1181 is null");
                }
                else
                {
                    if (regions.Count > 0)
                    {
                        sbQuery.Append(regions[0]);

                        if (chk1027.Checked == true)
                            sbQuery.Append(" OR Doc_I1027.i1181 is null");
                        else
                            sbQuery.Append(" OR i1181 is null");
                    }
                }
            }

            return sbQuery.ToString();
        }
        finally
        {
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }
            if (sbQuery != null)
            {
                sbQuery.Length = 0;
                sbQuery = null;
            }
        }
    }
}
