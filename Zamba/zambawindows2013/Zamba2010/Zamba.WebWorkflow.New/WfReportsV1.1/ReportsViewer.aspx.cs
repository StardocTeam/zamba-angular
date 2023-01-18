using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.ReportBuilder.Business;
using System.Text;
using System.Collections.Generic;

public partial class ReportsViewer : System.Web.UI.Page
{
    private const string ZOPT_PREFIX = "ReportViewer_";

    private List<String> _checkedNodes = new List<string>();
    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (null != Session["UserId"])
        {
            if (!Page.IsPostBack)
            {

                TreeNode B1 = new TreeNode("Seleccionar Todos", "SeleccionarTodos");
                B1.ShowCheckBox = true;
                TreeNode C1 = new TreeNode("Balances", "Balances");
                C1.ShowCheckBox = false;
                TreeNode D1 = new TreeNode("Balance de tareas por Estado", "UCTaskBalances");
                TreeNode D2 = new TreeNode("Tiempo promedio de tareas", "UCAverageTimeInSteps");
                C1.ChildNodes.Add(D1);
                C1.ChildNodes.Add(D2);
                B1.ChildNodes.Add(C1);

                TreeNode C2 = new TreeNode("Vencimiento", "Vencimiento");
                C2.ShowCheckBox = false;
                TreeNode D3 = new TreeNode("Tareas por vencer", "UCTaskToExpire");
                C2.ChildNodes.Add(D3);
                B1.ChildNodes.Add(C2);

                TreeNode C3 = new TreeNode("Cantidad", "Cantidad");
                C3.ShowCheckBox = false;
                TreeNode D4 = new TreeNode("Cantidad de tareas", "UCAsignedTasksCount");
                C3.ChildNodes.Add(D4);
                B1.ChildNodes.Add(C3);

                DataSet DsQueries = ReportBuilderComponent.GetQueryIdsAndNames();

                TreeNode QueryNode = new TreeNode("Consultas", "Consultas");

                if (null != DsQueries && DsQueries.Tables.Count > 0)
                {
                    foreach (DataRow Dr in DsQueries.Tables[0].Rows)
                        QueryNode.ChildNodes.Add(new TreeNode(Dr[1].ToString(), Dr[0].ToString()));
                }

                B1.ChildNodes.Add(QueryNode);
                TreeView1.Nodes.Add(B1);

                ucTaskCount.Visible = false;
                ucAverageTime.Visible = false;
                ucTaskBalance.Visible = false;
                ucTaskExpire.Visible = false;

                LoadUserState();
            }

            SaveUserState();

            LoadGenericReports();

        }
        else
            FormsAuthentication.RedirectToLoginPage();
    }

    protected void OnSelectecQueryChanged(object sender, EventArgs e)
    { }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ShowReports();
        //this.TabContainer1.DataBind();
    }

    private void ShowReports()
    {
        bool showall = false;
        foreach (TreeNode B in this.TreeView1.Nodes)
        {
            switch (B.Value)
            {
                case "SeleccionarTodos":
                    showall = B.Checked;
                    break;
                default:
                    break;
            }
            if (showall)
            {
                foreach (TreeNode C in B.ChildNodes)
                {
                    foreach (TreeNode D in C.ChildNodes)
                    {
                        D.Checked = true;

                        ucTaskCount.Visible = true;
                        ucAverageTime.Visible = true;
                        ucTaskBalance.Visible = true;
                        ucTaskExpire.Visible = true;
                    }
                }
            }
            else
            {

                foreach (TreeNode C in B.ChildNodes)
                {
                    foreach (TreeNode D in C.ChildNodes)
                    {
                        //Int32 QueryId;
                        switch (D.Value)
                        {
                            case "UCTaskBalances":
                                this.ucTaskBalance.Visible = D.Checked;
                                break;
                            case "UCTaskToExpire":
                                this.ucTaskExpire.Visible = D.Checked;
                                break;
                            case "UCAsignedTasksCount":
                                this.ucTaskCount.Visible = D.Checked;
                                break;
                            case "UCAverageTimeInSteps":
                                this.ucAverageTime.Visible = D.Checked;
                                break;
                            default: //Es un ID de query
                                //if (D.Checked && Int32.TryParse(D.Value, out QueryId))
                                //{
                                //    LoadGenericReport(QueryId);
                                //    //upGenericReport.Update();
                                //}
                                break;
                        }
                    }
                }
            }
        }
        foreach (TreeNode B in this.TreeView1.Nodes)
        {
            switch (B.Value)
            {
                case "SeleccionarTodos":
                    B.Checked = false;
                    break;
            }
        }
    }
    private void LoadGenericReports()
    {
        Dictionary<String, String> Queries = new Dictionary<string, string>();
        DataSet ds = ReportBuilderComponent.GetQueryIdsAndNames();
        Dictionary<Int32, String> IdsNames = new Dictionary<Int32, String>();

        foreach (DataRow dr in ds.Tables[0].Rows)
            IdsNames.Add(Int32.Parse(dr[0].ToString()), dr[1].ToString());

        Int32 QueryId;
        String CurrentQuery;
        ds = new DataSet();
        ds.Tables.Add(new DataTable());
        ds.Tables[0].Columns.Add(new DataColumn("QueryName"));
        ds.Tables[0].Columns.Add(new DataColumn("Query"));

        foreach (string NodeValue in _checkedNodes)
        {
            if (Int32.TryParse(NodeValue, out QueryId) && IdsNames.ContainsKey(QueryId))
            {
                CurrentQuery = ReportBuilderComponent.GenerateQueryBuilder(QueryId, true);
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0] = IdsNames[QueryId];
                ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][1] = CurrentQuery;
            }
        }

        rpGeneric.DataSource = ds;
        rpGeneric.DataBind();
    }

    private void LoadGenericReport(Int32 queryId)
    {
        String Query = ReportBuilderComponent.GenerateQueryBuilder(queryId, true);

        WfReports_UserControls_UCGenericReport_UcGenericReport GenericReport = new WfReports_UserControls_UCGenericReport_UcGenericReport();
        GenericReport.Query = Query;
        divGenericos.Controls.Add(GenericReport);
    }


    /*
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        switch (this.TreeView1.SelectedValue)
        {
            case "Balances":
            case "UCTaskBalances":
            case "UCAverageTimeInSteps":
                this.TabContainer1.ActiveTabIndex = 0;
                break;
            case "Vencimiento":
            case "UCTaskToExpire":
                this.TabContainer1.ActiveTabIndex = 1;
                break;
            case "Cantidad":
            case "UCAsignedTasksCount":
                this.TabContainer1.ActiveTabIndex = 2;
                break;
            default:
                break;
        }
    }
    private void AddReport(string Report)
    {
        ArrayList Reports;
        Reports = (ArrayList)Session["Reports"];
        if (Reports == null)
        {
            Reports = new ArrayList();
        }
        if (Reports.Contains(Report) == false)
        {
            Reports.Add(Report);
            Session["Reports"] = Reports;

        }
    }
    private void RemoveReport(string Report)
    {
        ArrayList Reports;
        Reports = (ArrayList)Session["Reports"];
        if (Reports == null)
        {
            Reports = new ArrayList();
        }
        if (Reports.Contains(Report) == true)
        {
            Reports.Remove(Report);
            Session["Reports"] = Reports;
        }

    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.DataItem.ToString())
        {
            //case "UCAverageTimeInStepsByWorkflow":
            //    this.UCAverageTimeInStepsByWorkflow.Visible = true;
            //    break;
            //case "UCAverageTimeInStepsByUser":
            //    this.TabUCAverageTimeInStepsByUser.Visible = true;
            //    break;




            //Control UC = LoadControl("~/UserControls/UCUsersAsigned/UCUsersAsignedByWorkflow.ascx");

            //new UserControls_UCUsersAsigned_UCUsersAsignedByWorkflow();
            //             UCTaskToExpireByWorkflow UC = new UCTaskToExpireByWorkflow();

            //this.Panel1.Controls.Add(UC);
            //this.Panel1.DataBind();
            default:
                break;
        }

        //Microsoft.Reporting.WebForms.ReportViewer RV = (Microsoft.Reporting.WebForms.ReportViewer)e.Item.FindControl("ReportViewer1");
        //RV.LocalReport.ReportPath = ("Reports/rptUsersAsigned.rdlc");    

    }
    */
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        //UCAsignedTasksCount.DataBind();
        //UCAverageTimeInSteps.DataBind();
        //UCTaskBalances.DataBind();
        //UCTaskToExpire.DataBind();
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

    }


    //protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    //{
    //    Boolean showall = false ;

    //    switch (e.Node.Text.ToUpper())
    //    {
    //        case "SELECCIONAR TODOS":
    //            showall = e.Node.Checked;
    //            break;
    //        default:
    //            break;
    //    }

    //    if (showall)
    //    {
    //        foreach (TreeNode C in TreeView1.Nodes)
    //        {
    //            foreach (TreeNode D in C.ChildNodes)
    //            {
    //                D.Checked = true;
    //                this.iframeTasksBalances.Visible = true;
    //                this.iframeTasksToExpire.Visible = true;
    //                this.iframeAsignedTasks.Visible = true;
    //                this.iframeAverageTime.Visible = true;
    //            }
    //        }
    //    }

    //    foreach (TreeNode B in this.TreeView1.Nodes)
    //    {
    //        switch (B.Value)
    //        {
    //            case "SeleccionarTodos":
    //                B.Checked = false;
    //                break;
    //        }
    //    }
    //}
    protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        //Int32 QueryId;

        //if (e.Node.Checked && Int32.TryParse(e.Node.Value, out QueryId))
        //    LoadGenericReport(QueryId);

        //upGenericReport.Update();
    }

    private void LoadUserState()
    {
        StringBuilder QueryBuilder = new StringBuilder();
        QueryBuilder.Append("Select Value from zopt where Item='");
        QueryBuilder.Append(ZOPT_PREFIX);
        QueryBuilder.Append(Session["UserId"]);
        QueryBuilder.Append("'");

        DataSet ds = Zamba.Servers.Server.get_Con(false, false, false).ExecuteDataset(CommandType.Text, QueryBuilder.ToString());
        QueryBuilder.Remove(0, QueryBuilder.Length);

        if (ds.Tables[0].Rows.Count > 0)
        {

            List<String> values = new List<String>();
            values.AddRange(ds.Tables[0].Rows[0][0].ToString().Split(";".ToCharArray()));

            if (values.Count > 0)
            {
                foreach (TreeNode node in TreeView1.Nodes[0].ChildNodes)
                {
                    node.Checked = values.Contains(node.Value);

                    foreach (TreeNode childNode in node.ChildNodes)
                    {
                        childNode.Checked = values.Contains(childNode.Value);
                    }
                }
            }
        }
    }

    private void SaveUserState()
    {
        foreach (TreeNode Node in TreeView1.Nodes[0].ChildNodes)
        {
            if (Node.Checked)
                _checkedNodes.Add(Node.Value);

            foreach (TreeNode ChildNode in Node.ChildNodes)
            {
                if (ChildNode.Checked)
                    _checkedNodes.Add(ChildNode.Value);
            }
        }

        StringBuilder QueryBuilder = new StringBuilder();
        QueryBuilder.Append("select count(item) from zopt where item = '");
        QueryBuilder.Append(ZOPT_PREFIX);
        QueryBuilder.Append(Session["UserId"]);
        QueryBuilder.Append("'");

        Int32 ReturnValue = (Int32)Zamba.Servers.Server.get_Con(false, false, false).ExecuteScalar(CommandType.Text, QueryBuilder.ToString());
        QueryBuilder.Remove(0, QueryBuilder.Length);

        if (ReturnValue == 0)
        {
            QueryBuilder.Append("insert into zopt values('");
            QueryBuilder.Append(ZOPT_PREFIX);
            QueryBuilder.Append(Session["UserId"]);
            QueryBuilder.Append("','");

            if (_checkedNodes.Count > 0)
            {
                foreach (String CheckedNode in _checkedNodes)
                {
                    QueryBuilder.Append(CheckedNode);
                    QueryBuilder.Append(";");
                }
                QueryBuilder.Remove(QueryBuilder.Length - 1, 1);
            }
            QueryBuilder.Append("')");
        }
        else
        {
            QueryBuilder.Append("update zopt set value = '");
            if (_checkedNodes.Count > 0)
            {
                foreach (String CheckedNode in _checkedNodes)
                {
                    QueryBuilder.Append(CheckedNode);
                    QueryBuilder.Append(";");
                }
                QueryBuilder.Remove(QueryBuilder.Length - 1, 1);
            }
            QueryBuilder.Append("' where item = '");
            QueryBuilder.Append(ZOPT_PREFIX);
            QueryBuilder.Append(Session["UserId"]);
            QueryBuilder.Append("'");
        }

        try
        {
            Zamba.Servers.Server.get_Con(false, false, false).ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString());
        }
        catch (Exception ex)
        { }

        QueryBuilder.Remove(0, QueryBuilder.Length);
        QueryBuilder = null;
    }
}
