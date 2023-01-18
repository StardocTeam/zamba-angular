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
////using Zamba.WFBusiness;
using Microsoft.Reporting.WebForms;

public partial class UCTaskBalances : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (rdbByWf.Checked)
             RowSteps.Visible = false;
        
        if (cmbWorkflow.Items.Count == 0)
             InitializesComponents();
        else
        {
            ShowReports();
        }
        //if (rdbVerGrafico.Checked)
        //{
        //    rpvGrdTasksBalance.Visible = false;
        //}

        ShowReports();
        if (RefreshTime() > 0)
            Timer1.Interval = (RefreshTime() * 1000);
        else
            Timer1.Enabled = false;


    }

    #region Metodos

    /// <summary>
    /// Obtiene el valor de intervalo de refresh de los reportes
    /// </summary>
    private Int16 RefreshTime()
    {
        return Int16.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["RefreshTime"].ToString());
    }


    public void InitializesComponents()
    {
        DsWF WFList = new DsWF();

        WFList = WFBusiness.GetWFsByUserRightMONITORING(Int64.Parse(Session["UserID"].ToString()));

        foreach (DataRow CurrentWorkflow in WFList.Tables[0].Rows)
            cmbWorkflow.Items.Add(new ListItem(CurrentWorkflow[5].ToString(), CurrentWorkflow[0].ToString()));
        
        if (cmbWorkflow.Items.Count > 0)
        {
            if (null != cmbWorkflow.SelectedValue)
            {
                LoadSteps(Int64.Parse(cmbWorkflow.SelectedValue.ToString()));
            }


        }
        if (rdbByWf.Checked)
        {
            ShowReports();
        }
    }
    public bool IsNumeric(object Expression)
    {
        bool isNum;
        double retNum;

        isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }

    private void ShowReports()
    {
        if (!String.IsNullOrEmpty(cmbWorkflow.SelectedValue))
        {

               DataSet ds = new DataSet();

                if (rdbByWf.Checked)
                {
                    ds = Tasks.GetTasksBalanceGroupByWorkflow(Int32.Parse(cmbWorkflow.SelectedValue.ToString()));
                }
                else
                {
                    ds = Tasks.GetTasksBalanceGroupByStep(Int32.Parse(cmbEtapas.SelectedValue.ToString()));
                }

                if (null != ds && null != ds.Tables)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblNoData.Visible = false;
                            gvTaskBalance.Visible = true;
                            dsTasksBalance dsTyped = new dsTasksBalance();

                            List<dsTasksBalance.dtTasksBalanceRow> DstypedRows = new List<dsTasksBalance.dtTasksBalanceRow>();

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                dsTasksBalance.dtTasksBalanceRow dr1 = dsTyped.dtTasksBalance.NewdtTasksBalanceRow();
                                //if (rdbVerGrafico.Checked)
                                //{
                                //    //corto la cadena para que no se vaya del rotulo del grafico
                                //    if (dr[0].ToString().Length > 80)
                                //    {
                                //        dr1.TasksBalance_State = dr[0].ToString().Remove(80);
                                //    }
                                //    else
                                //    {
                                //        dr1.TasksBalance_State = dr[0].ToString();
                                //    }
                                //}
                                //else
                                //{
                                    dr1.TasksBalance_State = dr[0].ToString();
                                //}
                                dr1.TasksBalance_Count = Int32.Parse(dr[1].ToString());
                                DstypedRows.Add(dr1);
                                dr1 = null;
                            }

                            foreach (dsTasksBalance.dtTasksBalanceRow dsRow in DstypedRows)
                            {
                                dsTyped.Tables[0].Rows.Add(dsRow);
                            }
                            dsTyped.AcceptChanges();
                            gvTaskBalance.DataSource = dsTyped;
                            gvTaskBalance.DataBind();
                            //this.rpvTasksBalance.LocalReport.DataSources.Clear();
                            //this.rpvGrdTasksBalance.LocalReport.DataSources.Clear();
                            //ReportDataSource rptDatasource = new ReportDataSource("dsTasksBalance_dtTasksBalance", dsTyped.Tables[0]);
                            //if (rdbVerGrafico.Checked)
                            //{
                            //    this.rpvTasksBalance.LocalReport.DataSources.Add(rptDatasource);
                            //    rpvTasksBalance.Visible = true;
                            //    rpvGrdTasksBalance.Visible = false;
                            //}
                            //else
                            //{
                            //    this.rpvGrdTasksBalance.LocalReport.DataSources.Add(rptDatasource);
                            //    rpvTasksBalance.Visible = false;
                            //    rpvGrdTasksBalance.Visible = true;
                            //}
                        }

                        else // en caso de que no se escuentren datos en base
                        {
                            //rpvTasksBalance.Visible = false;
                            //rpvGrdTasksBalance.Visible = false;
                            lblNoData.Visible = true;
                            gvTaskBalance.Visible = false;
                        }
                    }                          
           }
    }

}

    private void LoadSteps(Int64 workflowId)
    {
        cmbEtapas.Items.Clear();
        List<IWFStep> StepList = new List<IWFStep>();//usar el metodo que corresponda de etapas
        StepList = Zamba.Services.Steps.GetAllSteps(workflowId);

        foreach (IWFStep CurrentStep in StepList)
            cmbEtapas.Items.Add(new ListItem(CurrentStep.Name, CurrentStep.ID.ToString()));

        if (rdbByStep.Checked)
        {
            ShowReports();
        }


    }
    #endregion

    #region Eventos

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ShowReports();
    }

    protected void rdbVerGrafico_CheckedChanged(object sender, EventArgs e)
    {
        //rdbVerTabla.Checked = false;
        ShowReports();
    }
    protected void rdbVerTabla_CheckedChanged(object sender, EventArgs e)
    {
        //rdbVerGrafico.Checked = false;
        ShowReports();
    }
    protected void cmbWorkflow_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowReports();
        LoadSteps(Int64.Parse(cmbWorkflow.SelectedValue.ToString()));
    }


    protected void DropEtapas_SelectedIndexChanged(object sender, EventArgs e)
    {

        ShowReports();
    }

    protected void rdbByWf_CheckedChanged(object sender, EventArgs e)
    {
        RowSteps.Visible = false;
        rdbByStep.Checked = false;
        ShowReports();
    }

    protected void rdbByStep_CheckedChanged(object sender, EventArgs e)
    {
        RowSteps.Visible = true;
        rdbByWf.Checked = false;
        ShowReports();
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {

    }

    #endregion



}
