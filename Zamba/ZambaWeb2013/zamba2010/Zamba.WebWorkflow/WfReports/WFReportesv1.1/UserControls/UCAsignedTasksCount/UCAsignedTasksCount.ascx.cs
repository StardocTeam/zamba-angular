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

public partial class UCAsignedTasksCount : System.Web.UI.UserControl

{
    static DataSet  CurrentTaskCount=null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (cmbWorkflow.Items.Count == 0)
                InitializesComponents();
            else
                ShowReports();
            
            //if (rdbVerGrafico.Checked)
            //{
            //    rpvGrdTasksCount.Visible = false;
            //}

            ShowReports();

            if (RefreshTime() > 0)
                Timer1.Interval = (RefreshTime() * 1000);
            else
                Timer1.Enabled = false;
        }
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
        if (null != Session["UserId"])
        {
            DsWF WFList = new DsWF();

            WFList = WFBusiness.GetWFsByUserRightMONITORING(Int64.Parse(Session["UserID"].ToString()));

            foreach (DataRow CurrentWorkflow in WFList.Tables[0].Rows)
                cmbWorkflow.Items.Add(new ListItem(CurrentWorkflow[5].ToString(), CurrentWorkflow[0].ToString()));
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
        Int32 TotalTasks = 0;
        Int32 assignedtasks = 0;

        if (!String.IsNullOrEmpty(cmbWorkflow.SelectedValue))
        {
            DataSet ds = new DataSet();

            if (rdbByUser.Checked)
            {
                ds = Tasks.GetAsignedTasksCountsGroupByUser(Int64.Parse(cmbWorkflow.SelectedValue.ToString()));
                DataSet dsunAsigned = new DataSet();
                dsunAsigned = Tasks.GetAsignedTasksCountsGroupByStep(Int64.Parse(cmbWorkflow.SelectedValue.ToString()));

                if (null != ds && null != ds.Tables && null != dsunAsigned && null != dsunAsigned.Tables)
                {
                    if (ds.Tables.Count > 0 && dsunAsigned.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0 && dsunAsigned.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in dsunAsigned.Tables[0].Rows)
                            {

                                TotalTasks += Int32.Parse(r[1].ToString());
                            }

                        }
                    }
                }

            }
            else
            {
                ds = Tasks.GetAsignedTasksCountsGroupByStep(Int64.Parse(cmbWorkflow.SelectedValue.ToString()));
            }

            if (null != ds && null != ds.Tables)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblNoData.Visible = false;
                        gvTaskCount.Visible = true ;
                        dsAsignedTasksCounts dsTyped = new dsAsignedTasksCounts();

                        List<dsAsignedTasksCounts.dtAsignedTasksCountsRow> DstypedRows = new List<dsAsignedTasksCounts.dtAsignedTasksCountsRow>();

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            if (rdbByUser.Checked && TotalTasks > 0)
                            {
                                assignedtasks += Int32.Parse(dr[1].ToString());
                            }
                            dsAsignedTasksCounts.dtAsignedTasksCountsRow dr1 = dsTyped.dtAsignedTasksCounts.NewdtAsignedTasksCountsRow();
                            //if (rdbVerGrafico.Checked)
                            //{
                            //    //corto la cadena para que no se vaya del rotulo del grafico
                            //    if (dr[0].ToString().Length > 80)
                            //    {
                            //        dr1.AsignedTasks_Source = dr[0].ToString().Remove(80);
                            //    }
                            //    else
                            //    {
                            //        dr1.AsignedTasks_Source = dr[0].ToString();
                            //    }
                            //}
                            //else
                            //{
                            //    dr1.AsignedTasks_Source = dr[0].ToString();
                            //}
                            dr1.AsignedTasks_Counts = Int32.Parse(dr[1].ToString());
                            dr1.AsignedTasks_Source = dr[0].ToString();
                            DstypedRows.Add(dr1);
                            dr1 = null;
                        }
                        if (rdbByUser.Checked && TotalTasks - assignedtasks > 0)
                        {
                            dsAsignedTasksCounts.dtAsignedTasksCountsRow dr1 = dsTyped.dtAsignedTasksCounts.NewdtAsignedTasksCountsRow();
                            dr1.AsignedTasks_Source = "Sin Asignar";
                            dr1.AsignedTasks_Counts = TotalTasks - assignedtasks;
                            DstypedRows.Add(dr1);
                            dr1 = null;
                        }

                        foreach (dsAsignedTasksCounts.dtAsignedTasksCountsRow dsRow in DstypedRows)
                        {
                            dsTyped.Tables[0].Rows.Add(dsRow);
                        }
                        dsTyped.AcceptChanges();
                        gvTaskCount.DataSource = dsTyped;
                        gvTaskCount.DataBind();
                        CurrentTaskCount = dsTyped;
                        //this.rpvTasksCounts.LocalReport.DataSources.Clear();
                        //this.rpvGrdTasksCount.LocalReport.DataSources.Clear();

                        //ReportDataSource rptDatasource = new ReportDataSource("dsAsignedTasksCounts_dtAsignedTasksCounts", dsTyped.Tables[0]);
                        //if (rdbVerGrafico.Checked)
                        //{
                        //    this.rpvTasksCounts.LocalReport.DataSources.Add(rptDatasource);
                        //    rpvTasksCounts.Visible = true;
                        //    rpvGrdTasksCount.Visible = false;
                        //}
                        //else
                        //{
                        //    this.rpvGrdTasksCount.LocalReport.DataSources.Add(rptDatasource);
                        //    rpvTasksCounts.Visible = false;
                        //    rpvGrdTasksCount.Visible = true;
                        //}
                    }

                    else // en caso de que no se escuentren datos en base
                    {
                        //rpvTasksCounts.Visible = false;
                        //rpvGrdTasksCount.Visible = false;
                        gvTaskCount.Visible = false;
                        lblNoData.Visible = true;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Método que sirve para mostrar y guardar un gráfico
    /// </summary>
    /// <history>
    ///     [Gaston]  28/11/2008  Modified  Llamada al método setExcelGraphics3
    /// </history>
    public void SaveGraph(DataSet DatSt)
    {
        ExcelReportService.ExcelService ExcelGraph = new ExcelReportService.ExcelService();

        string root = "c:\\GraficoTasksCount" + Session["userid"].ToString() + ".jpg";
        string ServerRoot = Server.MapPath("~/temp") + "\\GraficoTasksCount" + Session["userid"].ToString() + ".jpg";
        //ExcelGraph.setExcelGraphics(DatSt, "columnas", root,false , true);
        ExcelGraph.setExcelGraphics(DatSt, "columnas", root, false, true);
        //Se realiza una copia en la carpeta temporal para poder visualizar el grafico desde la web [sebastian 12/12/2008]
        ExcelGraph.setExcelGraphics(DatSt, "columnas", ServerRoot, false, true);
    }

    protected void TaskBalances_Click(object sender, EventArgs e)
    {
        SaveGraph(CurrentTaskCount );        
    }


    #endregion

    #region Eventos

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
    }

    protected void rdbByWf_CheckedChanged(object sender, EventArgs e)
    {
        rdbByStep.Checked = false;
        ShowReports();
    }

    protected void rdbByStep_CheckedChanged(object sender, EventArgs e)
    {
        rdbByUser.Checked = false;
        ShowReports();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ShowReports();
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
    }

    #endregion

    protected void btnVerTaskCount_Click(object sender, EventArgs e)
    {
        string FileRoot = Server.MapPath("~/temp") + "\\GraficoTasksCount" + Session["userid"].ToString() + ".jpg";
        string path = "~/temp/GraficoTasksCount" + Session["userid"].ToString() + ".jpg";


        SaveGraph(CurrentTaskCount); 
        if (System.IO.File.Exists(FileRoot) == true)
            Response.Redirect("GraphExcel.aspx?GraphicPath=" + path);
    }

    /// <summary>
    /// Este método sirve para actualizar el reporte [Sebastian 09/12/208]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ActualizarTasksCount_Click(object sender, EventArgs e)
    {
        ShowReports();
    }
}
