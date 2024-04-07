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
[assembly: System.Security.AllowPartiallyTrustedCallers]
public partial class ReportsViewer : System.Web.UI.Page
{

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (null != Session["UserId"])
        {
            if (Page.IsPostBack != true)
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

                this.TreeView1.Nodes.Add(B1);

                iframeAsignedTasks.Visible = false;
                iframeAverageTime.Visible = false;
                iframeTasksBalances.Visible = false;
                iframeTasksToExpire.Visible = false;

            }
        }
        else
            FormsAuthentication.RedirectToLoginPage();
    }


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
                        this.iframeTasksBalances.Visible = true;
                        this.iframeTasksToExpire.Visible = true;
                        this.iframeAsignedTasks.Visible = true;
                        this.iframeAverageTime.Visible = true;
                    }
                }
            }
            else
            {
                foreach (TreeNode C in B.ChildNodes)
                {
                    foreach (TreeNode D in C.ChildNodes)
                    {
                        switch (D.Value)
                        {

                            case "UCTaskBalances":
                                this.iframeTasksBalances.Visible = D.Checked;
                                break;
                            case "UCTaskToExpire":
                                this.iframeTasksToExpire.Visible = D.Checked;
                                break;
                            case "UCAsignedTasksCount":
                                this.iframeAsignedTasks.Visible = D.Checked;
                                break;
                            case "UCAverageTimeInSteps":
                                this.iframeAverageTime.Visible = D.Checked;
                                break;
                            default:
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
    protected void btnTest_Click(object sender, EventArgs e)
    {
        ShowReports();
    }
}