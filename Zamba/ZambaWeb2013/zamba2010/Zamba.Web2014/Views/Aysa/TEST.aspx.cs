using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Services;
using Telerik.Web.UI;
using Zamba.Core;
using System.Web.Security;



public partial class Views_Aysa_TEST : System.Web.UI.Page
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

    public void Page_Load(object sender, EventArgs e)
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
        
        //Trae las entidades

       var ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, "select doctypeid,doc_type_name from enable_doc_types e inner join DOC_TYPE c on e.doctypeid = c.doc_type_id where DocTypeEnabled = 1");



        if (ds != null && ds.Tables.Count > 0)
          foreach (DataRow r in ds.Tables[0].Rows)
           {
                
              var EntityName = (r["doc_type_name"].ToString());
              var EntityId = (r["doctypeid"].ToString());  
              CheckBox chk = new CheckBox();
              chk.Text = EntityName;
              chk.ID = EntityId;
              panelEntidades.Controls.Add(chk);


            }
                

                    
    }

    
    //public void CheckBoxes(object sender, EventArgs e)
    //{

    //        foreach (Control box in panelEntidades.Controls)
    //        {

    //            if (box is CheckBox)
    //            {


    //                if (((CheckBox)box).Checked)
    //                {

    //                    //prueba !!!!

    //                    var ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, "select doctypeid,doc_type_name from enable_doc_types e inner join DOC_TYPE c on e.doctypeid = c.doc_type_id where DocTypeEnabled = 1");
                       
    //                    if (ds != null && ds.Tables.Count > 0)
    //                    {

    //                        foreach (DataRow r in ds.Tables[0].Rows)
    //                        {
    //                            var entiNames = (r["doc_type_name"].ToString());

    //                        }

    //                    }

    //                }


    //            }
    //        }
    //    }


    private List<string> GetIds()
    {
        List<string> Ids = new List<string>();
        foreach (Control item in panelEntidades.Controls)
        {
            
            if (item is CheckBox){

                var t = ((CheckBox)item);
                if (t.Checked) {

                    //Select + t.ID + from enable_ids inner join index_r_doc_type wher doc_type_id 

                    Ids.Add((t.ID.ToString().ToLower()));
                  
                    
                    }
            }
       }
                
        return Ids;
    }




    protected String FormatStrQuery(List<string> Ids)
    {

       
            if (Ids.Count > 0)
            {
               

                foreach (String Id in Ids)
                {
                    if (Id != string.Empty)
                    {
                        
                        var ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(System.Data.CommandType.Text, "Select INDEX_R_DOC_TYPE.INDEX_ID, DOC_INDEX.INDEX_NAME, DOC_INDEX.DROPDOWN from index_r_doc_type INNER JOIN DOC_INDEX on index_r_doc_type.index_id = doc_index.index_id where doc_type_id = " + Id);

                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            var par = (Int64.Parse(r["INDEX_ID"].ToString()));
                            var par2 = (r["INDEX_NAME"].ToString());
                            
                            
                            Response.Write(par);
                            Response.Write(par2);
                        }
                    }
                }
           
          }
            return string.Empty;
   }
      
                            
    


    private String GetQuery(bool useTop)
    {
        String Query = string.Empty;
        try
        {
            List<string> Ids = GetIds();
            //if (Ids.Count > 1)
            //{

            //    Ids.Add("1027");
            //}

            //int maxResults = (string.IsNullOrEmpty(txtMaxResults.Text)) ? 1000 : int.Parse(txtMaxResults.Text);

            string whereQuery = GetWhereForRegion();
            Query = FormatStrQuery(Ids);

            if (!(string.IsNullOrEmpty(Query) || Query.Contains("where")) && !string.IsNullOrEmpty(whereQuery))
            {
                Query += " where " + whereQuery;
            }

            if (useTop)
            {
                Query = "set rowcount " + " " + Query + " set rowcount 0";
            }

            STrace.WriteLineIf(ZTrace.IsVerbose, "Se genero la siguiente consulta para el reporte general : ");
            STrace.WriteLineIf(ZTrace.IsVerbose, Query);
            STrace.WriteLineIf(ZTrace.IsVerbose, "");

            return Query;
        }
        catch (Exception)
        {
            Zamba.Core.ZClass.raiseerror(new Exception("Query: " + Query.ToString()));
            return string.Empty;
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

                        //if (chk1027.Checked == true)
                        //{
                        //    switch (groupName)
                        //    {
                        //        case "DRCF":
                        //            regions.Add("Doc_I1027.I1181 = 1");
                        //            break;
                        //        case "DRN":
                        //            regions.Add("Doc_I1027.I1181 = 2");
                        //            break;
                        //        case "DRO":
                        //            regions.Add("Doc_I1027.I1181 = 5");
                        //            break;
                        //        case "DRSE":
                        //            regions.Add("Doc_I1027.I1181 = 3");
                        //            break;
                        //        case "DRSO":
                        //            regions.Add("Doc_I1027.I1181 = 4");
                        //            break;
                        //    }
                        //}
                        //else
                        //{
                        //    switch (groupName)
                        //    {
                        //        case "DRCF":
                        //            regions.Add("I1181 = 1");
                        //            break;
                        //        case "DRN":
                        //            regions.Add("I1181 = 2");
                        //            break;
                        //        case "DRO":
                        //            regions.Add("I1181 = 5");
                        //            break;
                        //        case "DRSE":
                        //            regions.Add("I1181 = 3");
                        //            break;
                        //        case "DRSO":
                        //            regions.Add("I1181 = 4");
                        //            break;
                        //    }
                        //}
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

                    //if (chk1027.Checked == true)
                    //    sbQuery.Append(" OR Doc_I1027.i1181 is null");
                    //else
                    //    sbQuery.Append(" OR i1181 is null");
                }
                else
                {
                    if (regions.Count > 0)
                    {
                        sbQuery.Append(regions[0]);

                        //if (chk1027.Checked == true)
                        //    sbQuery.Append(" OR Doc_I1027.i1181 is null");
                        //else
                        //    sbQuery.Append(" OR i1181 is null");
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

            //RadFilter1.FireApplyCommand();
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

    private void SetVisibility(bool visible)
    {
        RadFilter1.Visible = visible;
        panelAcciones.Visible = visible;
        RadGrid1.Visible = visible;
        lblAviso.Visible = !visible;
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

    protected void ApplyExpressions(object sender, Telerik.Web.UI.RadFilterApplyExpressionsEventArgs e)
    {

        String s = GetExpressionFormatedOf(e.ExpressionRoot);

        //int maxResults = (string.IsNullOrEmpty(txtMaxResults.Text)) ? 1000 : int.Parse(txtMaxResults.Text);
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("set rowcount ");
        //sbQuery.AppendLine(maxResults.ToString());
        sbQuery.Append("select * from (");
        sbQuery.Append(GetQuery(false));

        if (!string.IsNullOrEmpty(s))
        {
            sbQuery.Append(") as Q ");
            sbQuery.Append(" where ");
            sbQuery.AppendLine(s);

            STrace.WriteLineIf(ZTrace.IsVerbose, "Se aplico el siguiente filtro : ");
            STrace.WriteLineIf(ZTrace.IsVerbose, s.Replace("(", "").Replace(")", ""));
            STrace.WriteLineIf(ZTrace.IsVerbose, "");

        }
        else
            sbQuery.AppendLine(") as Q ");

        sbQuery.Append("set rowcount 0");
        FilteredQuery = sbQuery.ToString();

        STrace.WriteLineIf(ZTrace.IsVerbose, "La consulta finalmente es : ");
        STrace.WriteLineIf(ZTrace.IsVerbose, FilteredQuery);
        STrace.WriteLineIf(ZTrace.IsVerbose, "");

        SqlDataSource2.SelectCommand = FilteredQuery;

        sbQuery.Length = 0;
        sbQuery = null;

        SqlDataSource2.DataBind();
        RadGrid1.Rebind();
        FormatColumnsView();
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
                    expression = "(" + GetExpressionFormatedOf(groupExpression) + ")";
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
}
    


   



   




            




   


    

    
  




