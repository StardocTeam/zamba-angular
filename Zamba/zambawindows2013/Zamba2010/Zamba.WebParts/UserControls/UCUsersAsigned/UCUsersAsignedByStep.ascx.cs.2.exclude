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

public partial class UserControls_UCUsersAsigned_UCUsersAsignedByStep : System.Web.UI.UserControl
{
    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (cmbEtapas.Items.Count == 0)
        {
            InitializesComponents();
        }
        rpvGrdUsersAsigned.Visible = false;
  
    }
    protected void cmbWorkflow_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbEtapas.Items.Clear();
        LoadSteps(Int64.Parse(cmbWorkflow.SelectedValue.ToString()));
    }

    private void ShowReports()
    {
        if (!String.IsNullOrEmpty(cmbEtapas.SelectedValue))
        {

            dsUsersAsigned dsTyped = new dsUsersAsigned();

            dsUsersAsigned.dtUserAsignedRow dr1 = dsTyped.dtUserAsigned.NewdtUserAsignedRow();

            dr1.UserAsigned_Source = "Etapa: " + cmbEtapas.SelectedItem.ToString();
            dr1.UserAsigned_Count = Users.GetAsignedUsersCountByStep(Int64.Parse(cmbEtapas.SelectedValue.ToString()));

            dsTyped.Tables[0].Rows.Add(dr1);
            dsTyped.AcceptChanges();

            this.rpvUsersAsigned.LocalReport.DataSources.Clear();
            this.rpvGrdUsersAsigned.LocalReport.DataSources.Clear();
            ReportDataSource rptDatasource = new ReportDataSource("dsUsersAsigned_dtUserAsigned", dsTyped.Tables[0]);
            this.rpvUsersAsigned.LocalReport.DataSources.Add(rptDatasource);
            this.rpvGrdUsersAsigned.LocalReport.DataSources.Add(rptDatasource);
            rpvUsersAsigned.Visible = true;
            rpvGrdUsersAsigned.Visible = true;

            if (this.rdbVerGrafico.Checked == true)
            {
                this.rpvGrdUsersAsigned.Visible = false;
                this.rpvUsersAsigned.Visible = true;

            }
            else
            {
                this.rpvGrdUsersAsigned.Visible = true;
                this.rpvUsersAsigned.Visible = false;
            }
        }
    }

    #endregion
    
    #region METODOS

    public void InitializesComponents()
    {
        List<IWorkFlow> WFList = new List<IWorkFlow>();

        WFList = Workflow.GetWorkflows();

        foreach (IWorkFlow CurrentWorkflow in WFList)
            cmbWorkflow.Items.Add(new ListItem(CurrentWorkflow.Name, CurrentWorkflow.ID.ToString()));
        if (cmbWorkflow.Items.Count > 0)
        {
            if (null != cmbWorkflow.SelectedValue)
            {
                LoadSteps(Int64.Parse(cmbWorkflow.SelectedValue.ToString()));
            }

            
        }
    }

    private void LoadSteps(Int64 workflowId)
    {
        List<IWFStep> StepList = new List<IWFStep>();//usar el metodo que corresponda de etapas
        StepList = Zamba.Services.Steps.GetAllSteps(workflowId);

        foreach (IWFStep CurrentStep in StepList)
            cmbEtapas.Items.Add(new ListItem(CurrentStep.Name, CurrentStep.ID.ToString()));

        ShowReports();
    }

    #endregion

    protected void rdbVerGrafico_CheckedChanged(object sender, EventArgs e)
    {
        rdbVerTabla.Checked = false;
        ShowReports();
    }
    protected void rdbVerTabla_CheckedChanged(object sender, EventArgs e)
    {
        rdbVerGrafico.Checked = false;
        ShowReports();
    }
    protected void cmbEtapas_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowReports();
    }
}
