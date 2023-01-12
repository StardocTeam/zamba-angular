using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.Services;
using System.Web;

namespace Views.Reports
{
    public partial class Reports : Page
    {
      
        //        private const string REPORTQUERY = @"select top 100 percent pr.i12344 'Procesos', p.i12393 'Cod.Familia', f.i12344 'Familia', p.i12334 'Cod.Producto',  
        //                                    p.i12335 'Descripcion', u.descripcion 'Medida' 
        //                                    from doc_i12090 p  
        //                                    inner join slst_s12343 u on u.codigo = p.i12343  
        //                                    inner join doc_i12125 pr on pr.i12436 = p.i12437 and pr.i12395 = 12027 
        //                                    inner join doc_i12120 f on f.i12338 = p.i12393  
        //                                    order by pr.i12344, f.i12344, p.i12335 ";

        #region GetIconsUrl

        protected string GetFilterIcon()
        {
            return RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Vista.Grid.Filter.gif");
        }

        protected string GetExcelIcon()
        {
            return "../../Content/images/icons/3.png";
        }

        protected string GetCsvIcon()
        {
            return "../../Content/images/icons/8.png";
        }

        protected string GetWordIcon()
        {
            return "../../Content/images/icons/2.png";
        }

        protected string GetPdfIcon()
        {
            return "../../Content/images/icons/4.png";
        }

        protected string GetOpenTaskIcon()
        {
            return "../../Content/images/icons/30.png";
        }

        #endregion
        string REPORTQUERY = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SqlDataSource2.ConnectionString = Zamba.Servers.Server.get_Con(false, true, false).ConString;
                SZOptBusiness zopt = new SZOptBusiness();
                string title = zopt.GetValue("WebViewTitle");
                zopt = null;
                
                String varValue = Request.QueryString[0].ToString();
                if (varValue == null || varValue == string.Empty)
                    varValue = "";
                
               REPORTQUERY = Zamba.Servers.Server.get_Con().ExecuteScalar( CommandType.Text, string.Format("Select query from reporte_general where id = {0}",varValue)).ToString();
                var ReportTitle = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("Select Name from reporte_general where id = {0}", varValue));

                Title = (string.IsNullOrEmpty(title)) ? "Zamba Software " : title + " " + ReportTitle;
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
        }

        protected void RadGrid1ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                BindGrid();
                RunCommand(e);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void RunCommand(GridCommandEventArgs e)
        {
            string command = e.CommandName;

            if (command == "FilterRadGrid")
            {
                RadFilter1.FireApplyCommand();
            }

        }

        protected void RadGrid1NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            BindGrid();
        }

        protected void RadGrid1Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                SqlDataSource2.SelectCommand = REPORTQUERY;
                SqlDataSource2.DataBind();
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        protected void RadFilter1PreRender(object sender, EventArgs e)
        {
            var menu = RadFilter1.FindControl("rfContextMenu") as RadContextMenu;
            if (menu == null) return;
            menu.DefaultGroupSettings.Height = Unit.Pixel(400);
            menu.EnableAutoScroll = true;
        }

        public void ConfigureExport()
        {
            RadGrid1.ExportSettings.IgnorePaging = CheckBox2.Checked;
        }

        protected void Button2Click(object sender, EventArgs e)
        {
            ConfigureExport();
            RadGrid1.MasterTableView.ExportToExcel();
        }

        protected void Button3Click(object sender, EventArgs e)
        {
            ConfigureExport();
            RadGrid1.MasterTableView.ExportToCSV();
        }

        protected void ApplyExpressions(object sender, Telerik.Web.UI.RadFilterApplyExpressionsEventArgs e)
        {

            String s = GetExpressionFormatedOf(e.ExpressionRoot);

            if (string.IsNullOrEmpty(s))
            {
                SqlDataSource2.SelectCommand = REPORTQUERY;
            }
            else
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("select * from (");
                sbQuery.Append(REPORTQUERY);

                if (!string.IsNullOrEmpty(s))
                {
                    sbQuery.Append(") as Q ");
                    sbQuery.Append(" where ");
                    sbQuery.AppendLine(s);
                    //STrace.WriteLineIf(ZTrace.IsVerbose, "Se aplico el siguiente filtro : ");
                    //STrace.WriteLineIf(ZTrace.IsVerbose, s.Replace("(", "").Replace(")", ""));
                    //STrace.WriteLineIf(ZTrace.IsVerbose, "");
                }
                else
                {
                    sbQuery.AppendLine(") as Q ");
                }

                SqlDataSource2.SelectCommand = sbQuery.ToString();
                sbQuery.Length = 0;
                sbQuery = null;
            }

            SqlDataSource2.DataBind();
            RadGrid1.Rebind();
            FormatColumnsView();
        }

        protected String GetExpressionFormatedOf(Telerik.Web.UI.RadFilterGroupExpression rootExpression)
        {
            const String betweenGroupOperator = " AND ";
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
                        expression = GetExpressionFormatedOf(groupExpression);
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
    }
}