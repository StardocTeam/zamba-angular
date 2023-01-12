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

public partial class UCTaskToExpire : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (txtHoursToExpire.Text == string.Empty)
            txtHoursToExpire.Text = "0";

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
            rpvGrdTaskToExpireByWorkflow.Visible = false;
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
        try
        {
            DsWF WFList = new DsWF();

            WFList = WFBusiness.GetWFsByUserRightMONITORING(Int64.Parse(Session["UserID"].ToString()));

            foreach (DataRow CurrentWorkflow in WFList.Tables[0].Rows)
                cmbWorkflow.Items.Add(new ListItem(CurrentWorkflow[5].ToString(), CurrentWorkflow[0].ToString()));

            ShowReports();
        }
        catch (Exception ex)
        { }
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
        Int32 TotalTasks = 0;
        Int32 assignedtasks = 0;
        if (!String.IsNullOrEmpty(cmbWorkflow.SelectedValue))
        {
            if (IsNumeric(txtHoursToExpire.Text))
            {
                Int32 hourstoexpire;
                hourstoexpire = Int32.Parse(txtHoursToExpire.Text);

                DataSet ds = new DataSet();

                if (rdbByStep.Checked)
                {
                    if (hourstoexpire == 0)
                    {
                        ds = Tasks.GetExpiredTasksGroupByStep(Int32.Parse(cmbWorkflow.SelectedValue.ToString()));
                    }
                    else
                    {
                    //Diego: Hardcodeo con 0 el parametro para que la funcion me devuelva los registros
                    //desde este momento hasta lo que defina el usuario
                    ds = Tasks.GetTasksToExpireGroupByStep(Int32.Parse(cmbWorkflow.SelectedValue.ToString()), 0, hourstoexpire);
                                        }
                }
                else
                {
                    //Diego: Hardcodeo con 0 el parametro para que la funcion me devuelva los registros
                    //desde este momento hasta lo que defina el usuario
                    if (hourstoexpire == 0)
                    {
                        ds = Tasks.GetExpiredTasksGroupByUser(Int32.Parse(cmbWorkflow.SelectedValue.ToString()));
                    }
                    else
                    {
                        ds = Tasks.GetTasksToExpireGroupByUser(Int32.Parse(cmbWorkflow.SelectedValue.ToString()), 0, hourstoexpire);
                    }
                    DataSet dsunAsigned = new DataSet();
                    dsunAsigned = Tasks.GetTasksToExpireGroupByStep(Int32.Parse(cmbWorkflow.SelectedValue.ToString()), 0, hourstoexpire);
                    if (null != ds && null != ds.Tables && null != dsunAsigned && null != dsunAsigned.Tables)
                    {
                        if (ds.Tables.Count > 0 && dsunAsigned.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0 && dsunAsigned.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow r in dsunAsigned.Tables[0].Rows)
                                {

                                    TotalTasks += Int32.Parse(r[2].ToString());
                                }

                            }
                        }
                    }
                }

                if (null != ds && null != ds.Tables)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblNoData.Visible = false;

                            dsTaskToExpire dsTyped = new dsTaskToExpire();

                            List<dsTaskToExpire.dtTaskToExpireRow> DstypedRows = new List<dsTaskToExpire.dtTaskToExpireRow>();

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                string source;

                                if (rdbByStep.Checked)
                                {
                                    source = WFStepBussines.GetStepNameById(Int64.Parse(dr[0].ToString()));
                                }
                                else
                                {
                                    source = UserBusiness.GetUserNamebyId(Int32.Parse(dr[0].ToString()));
                                    assignedtasks += Int32.Parse(dr[2].ToString());
                                }
                                dsTaskToExpire.dtTaskToExpireRow dr1 = dsTyped.dtTaskToExpire.NewdtTaskToExpireRow();
                                if (rdbVerGrafico.Checked)
                                {
                                    //corto la cadena para que no se vaya del rotulo del grafico
                                    if (source.Length > 80)
                                    {
                                        dr1.TaskToExpire_Source = source.Remove(80);
                                    }
                                    else
                                    {
                                        dr1.TaskToExpire_Source = source;
                                    }
                                }
                                else
                                {
                                    dr1.TaskToExpire_Source = source;
                                }
                                dr1.TaskToExpire_Count = Int32.Parse(dr[2].ToString());
                                DstypedRows.Add(dr1);
                                dr1 = null;
                            }

                            foreach (dsTaskToExpire.dtTaskToExpireRow dsRow in DstypedRows)
                            {
                                dsTyped.Tables[0].Rows.Add(dsRow);
                            }
                            if (rdbByUser.Checked && TotalTasks - assignedtasks > 0)
                            {
                                dsTaskToExpire.dtTaskToExpireRow dr1 = dsTyped.dtTaskToExpire.NewdtTaskToExpireRow();
                                dr1.TaskToExpire_Source = "Sin Asignar";
                                dr1.TaskToExpire_Count = TotalTasks - assignedtasks;
                                dsTyped.Tables[0].Rows.Add(dr1);
                                dr1 = null;
                            }

                            dsTyped.AcceptChanges();

                            this.rpvTaskToExpireByWorkflow.LocalReport.DataSources.Clear();
                            this.rpvGrdTaskToExpireByWorkflow.LocalReport.DataSources.Clear();
                            ReportDataSource rptDatasource = new ReportDataSource("dsTaskToExpire_dtTaskToExpire", dsTyped.Tables[0]);
                            if (rdbVerGrafico.Checked)
                            {
                                this.rpvTaskToExpireByWorkflow.LocalReport.DataSources.Add(rptDatasource);
                                rpvTaskToExpireByWorkflow.Visible = true;
                                rpvGrdTaskToExpireByWorkflow.Visible = false;
                            }
                            else
                            {
                                this.rpvGrdTaskToExpireByWorkflow.LocalReport.DataSources.Add(rptDatasource);
                                rpvTaskToExpireByWorkflow.Visible = false;
                                rpvGrdTaskToExpireByWorkflow.Visible = true;
                            }
                        }
                        else // en caso de que no se escuentren datos en base
                        {
                            rpvTaskToExpireByWorkflow.Visible = false;
                            rpvGrdTaskToExpireByWorkflow.Visible = false;
                            lblNoData.Visible = true;
                        }
                    }
                }

            }

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
    }
    protected void rdbByStep_CheckedChanged(object sender, EventArgs e)
    {
        rdbByUser.Checked = false;
        ShowReports();
    }
    protected void rdbByUser_CheckedChanged(object sender, EventArgs e)
    {
        rdbByStep.Checked = false;
        ShowReports();
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {

    }

    #endregion

}
