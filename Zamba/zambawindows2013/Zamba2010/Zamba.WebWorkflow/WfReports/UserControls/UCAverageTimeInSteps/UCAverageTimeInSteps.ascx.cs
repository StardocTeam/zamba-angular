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
using Microsoft.Reporting.WebForms;

public partial class UCAverageTimeInSteps : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (rdbWorkflow.Checked)
        {
            RowSteps.Visible = false;
        }
        if (cmbWorkflow.Items.Count == 0)
        {
            InitializesComponents();
        }
        else
        {
            ShowReports();
        }
        if (rdbVerGrafico.Checked)
        {
            rpvGrdAverageTimeInSteps.Visible = false;
        }

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
        if (rdbWorkflow.Checked)
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

            Hashtable hash = new Hashtable();

            if (rdbWorkflow.Checked)
            {
                hash = Tasks.GetTasksAverageTimeInSteps(Int64.Parse(cmbWorkflow.SelectedValue.ToString()));
            }
            else
            {
                hash = Tasks.GetTasksAverageTimeByStep(Int64.Parse(cmbEtapas.SelectedValue.ToString()));
            }

            if (null != hash)
            {
                if (hash.Count > 0)
                {
                    lblNoData.Visible = false;

                    dsAverageTimeInSteps dsTyped = new dsAverageTimeInSteps();

                    List<dsAverageTimeInSteps.dtAverageTimeInStepsRow> DstypedRows = new List<dsAverageTimeInSteps.dtAverageTimeInStepsRow>();
                    
                    foreach (Int64 stepid in hash.Keys)
                    {
                        dsAverageTimeInSteps.dtAverageTimeInStepsRow dr1 = dsTyped.dtAverageTimeInSteps.NewdtAverageTimeInStepsRow();
                        string sourcename = WFStepBussines.GetStepNameById(stepid);
                        if (rdbVerGrafico.Checked)
                        {
                            //corto la cadena para que no se vaya del rotulo del grafico
                            if (sourcename.Length > 13)
                            {
                                dr1.AverageTime_Source = sourcename.Remove(13);
                            }
                            else
                            {
                                dr1.AverageTime_Source = sourcename;
                            }
                        }
                        else
                        {
                            dr1.AverageTime_Source = sourcename;
                        }
                        dr1.AverageTime = (Int32)Math.Truncate(double.Parse(hash[stepid].ToString()));
                        DstypedRows.Add(dr1);
                        dr1 = null;
                    }

                    foreach (dsAverageTimeInSteps.dtAverageTimeInStepsRow dsRow in DstypedRows)
                    {
                        dsTyped.Tables[0].Rows.Add(dsRow);
                    }
                    dsTyped.AcceptChanges();

                    this.rpvAverageTimeInSteps.LocalReport.DataSources.Clear();
                    this.rpvGrdAverageTimeInSteps.LocalReport.DataSources.Clear();
                    ReportDataSource rptDatasource = new ReportDataSource("dsAverageTimeInSteps_dtAverageTimeInSteps", dsTyped.Tables[0]);
                    if (rdbVerGrafico.Checked)
                    {
                        this.rpvAverageTimeInSteps.LocalReport.DataSources.Add(rptDatasource);
                        rpvAverageTimeInSteps.Visible = true;
                        rpvGrdAverageTimeInSteps.Visible = false;
                    }
                    else
                    {
                        this.rpvGrdAverageTimeInSteps.LocalReport.DataSources.Add(rptDatasource);
                        rpvAverageTimeInSteps.Visible = false;
                        rpvGrdAverageTimeInSteps.Visible = true;
                    }
                }

                else // en caso de que no se escuentren datos en base
                {
                    rpvAverageTimeInSteps.Visible = false;
                    rpvGrdAverageTimeInSteps.Visible = false;
                    lblNoData.Visible = true;
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
        rdbVerTabla.Checked = false;
        ShowReports();
    }
    protected void rdbVerTabla_CheckedChanged(object sender, EventArgs e)
    {
        rdbVerGrafico.Checked = false;
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

    protected void rdbWorkflow_CheckedChanged(object sender, EventArgs e)
    {
        RowSteps.Visible = false;
        rdbByStep.Checked = false;
        ShowReports();
    }
    protected void rdbByStep_CheckedChanged(object sender, EventArgs e)
    {
        RowSteps.Visible = true;
        rdbWorkflow.Checked = false;
        ShowReports();
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {

    }

    #endregion


}
