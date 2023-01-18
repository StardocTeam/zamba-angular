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
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Services;
//using Zamba.WFBusiness;
using Microsoft.Reporting.WebForms;

public partial class UserControls_UCUsersAsigned_UCUsersAsignedByWorkflow : System.Web.UI.UserControl
{
    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        InitializesComponents();
        rpvUsersAsignedByWorkflow.Visible = false;
    }

    private void LoadReports()
    {
        if (!String.IsNullOrEmpty(cmbWorkflows.SelectedValue))
        {

            dsUsersAsigned dsTyped = new dsUsersAsigned();

            dsUsersAsigned.dtUserAsignedRow dr1 = dsTyped.dtUserAsigned.NewdtUserAsignedRow();

            dr1.UserAsigned_Source = "Workflow: " + cmbWorkflows.SelectedItem.ToString();
            dr1.UserAsigned_Count = Users.GetAsignedUsersCountByWorkflow(Int64.Parse(cmbWorkflows.SelectedValue.ToString()));


            dsTyped.Tables[0].Rows.Add(dr1);
            dsTyped.AcceptChanges();

            this.rpvUsersAsignedByWorkflow.LocalReport.DataSources.Clear();
            this.rpvGrdUsersAsignedByWorkflow.LocalReport.DataSources.Clear();
            ReportDataSource rptDatasource = new ReportDataSource("dsUsersAsigned_dtUserAsigned", dsTyped.Tables[0]);

            if (rdbVerGrafico.Checked)
            {
                this.rpvUsersAsignedByWorkflow.LocalReport.DataSources.Add(rptDatasource);
                rpvGrdUsersAsignedByWorkflow.Visible = false;
                rpvUsersAsignedByWorkflow.Visible = true;
            }
            else
            {
                this.rpvGrdUsersAsignedByWorkflow.LocalReport.DataSources.Add(rptDatasource);
                rpvGrdUsersAsignedByWorkflow.Visible = true;
                rpvUsersAsignedByWorkflow.Visible = false;
            }
        }
    }
    #endregion

    #region Eventos

    public void InitializesComponents()
    {
        List<IWorkFlow> WFList = new List<IWorkFlow>();

        WFList = Workflow.GetWorkflows();

        foreach (IWorkFlow CurrentWorkflow in WFList)
            cmbWorkflows.Items.Add(new ListItem(CurrentWorkflow.Name, CurrentWorkflow.ID.ToString()));

        LoadReports();
    }
    #endregion

    protected void rdbVerGrafico_CheckedChanged(object sender, EventArgs e)
    {
        rdbVerTabla.Checked = false;
        LoadReports();

    }
    protected void rdbVerTabla_CheckedChanged(object sender, EventArgs e)
    {
        rdbVerGrafico.Checked = false;
        LoadReports();
    }
    protected void cmbWorkflows_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReports();
    }
}
