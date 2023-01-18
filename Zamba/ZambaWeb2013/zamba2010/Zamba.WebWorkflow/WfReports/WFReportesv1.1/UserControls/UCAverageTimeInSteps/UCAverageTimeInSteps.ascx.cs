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
               
                  
    static DataSet   CurrentTimeSteps = null ;  


    /// <summary>
    /// Evento que se ejecuta cuando se carga la página
    /// </summary>
    /// <history> 
    ///    [Gaston]    13/11/2008   Modified    Se agrego la validación de PostBack
    /// </history
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            if (null != Session["UserId"])
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

                ShowReports();

                if (RefreshTime() > 0)
                    Timer1.Interval = (RefreshTime() * 1000);
                else
                    Timer1.Enabled = false;
            }
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

    /// <summary>
    /// Método que sirve para armar el reporte
    /// </summary>
    /// <history> 
    ///    [Gaston]    12/11/2008   Modified    Adaptado a GridView
    /// </history
    private void ShowReports()
    {
        if (!String.IsNullOrEmpty(cmbWorkflow.SelectedValue))
        {
            Hashtable hash = new Hashtable();

            if (rdbWorkflow.Checked)
                hash = Tasks.GetTasksAverageTimeInSteps(Int64.Parse(cmbWorkflow.SelectedValue.ToString()));
            else
                hash = Tasks.GetTasksAverageTimeByStep(Int64.Parse(cmbEtapas.SelectedValue.ToString()));
            
            if (null != hash)
            {
                if (hash.Count > 0)
                {
                    gvAverageTimeInSteps.Visible = true;
                    lblNoData.Visible = false;
                    
                    dsAverageTimeInSteps dsTyped = new dsAverageTimeInSteps();
                    List<dsAverageTimeInSteps.dtAverageTimeInStepsRow> DstypedRows = new List<dsAverageTimeInSteps.dtAverageTimeInStepsRow>();

                    foreach (Int64 stepid in hash.Keys)
                    {
                        dsAverageTimeInSteps.dtAverageTimeInStepsRow dr1 = dsTyped.dtAverageTimeInSteps.NewdtAverageTimeInStepsRow();
                        string sourcename = WFStepBussines.GetStepNameById(stepid);
                        dr1.AverageTime_Source = sourcename;
                        dr1.AverageTime = (Int32)Math.Truncate(double.Parse(hash[stepid].ToString()));
                        DstypedRows.Add(dr1);
                        dr1 = null;
                    }

                    foreach (dsAverageTimeInSteps.dtAverageTimeInStepsRow dsRow in DstypedRows)
                    {
                        dsTyped.Tables[0].Rows.Add(dsRow);
                    }

                    dsTyped.AcceptChanges();
                    gvAverageTimeInSteps.DataSource = dsTyped;
                    gvAverageTimeInSteps.DataBind();
                    CurrentTimeSteps = dsTyped;
                }
                else
                {
                    gvAverageTimeInSteps.Visible = false;
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


        public void SaveGraph(DataSet DatSt)
    {
        ExcelReportService.ExcelService ExcelGraph = new ExcelReportService.ExcelService();

        string root = "c:\\GraficoAverageTimeInSteps" + Session["userid"].ToString() + ".jpg";
        string ServerRoot = Server.MapPath("~/temp") + "\\GraficoAverageTimeInSteps" + Session["userid"].ToString() + ".jpg";

        ExcelGraph.setExcelGraphics(DatSt, "columnas", root, false, true );
    //Se realiza una copia en la carpeta temporal para poder visualizar el grafico desde la web [sebastian 12/12/2008]
        ExcelGraph.setExcelGraphics(DatSt, "columnas", ServerRoot , false, true);
    }



        protected void AverageTimeInSteps_Click(object sender, EventArgs e)
    {
        SaveGraph(CurrentTimeSteps );        
    }

    #endregion

    #region Eventos

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ShowReports();
    }

    protected void rdbVerGrafico_CheckedChanged(object sender, EventArgs e)
    {
        ShowReports();
    }
    protected void rdbVerTabla_CheckedChanged(object sender, EventArgs e)
    {
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string FileRoot = Server.MapPath("~/temp") + "\\GraficoAverageTimeInSteps" + Session["userid"].ToString() + ".jpg";
        string path = "~/temp/GraficoAverageTimeInSteps" + Session["userid"].ToString() + ".jpg";

        SaveGraph(CurrentTimeSteps); 
        if (System.IO.File.Exists(FileRoot) == true)
            Response.Redirect("GraphExcel.aspx?GraphicPath=" + path);
    }

    /// <summary>
    /// Este método sirve para actualizar el reporte [Sebastian 09/12/208]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ActualizarAverageTimeInSteps_Click(object sender, EventArgs e)
    {
        ShowReports();
    }
    protected void btnVerAverageSteps_Click(object sender, EventArgs e)
    {
        string FileRoot = Server.MapPath("~/temp") + "\\GraficoAverageTimeInSteps" + Session["userid"].ToString() + ".jpg";
        string path = "~/temp/GraficoAverageTimeInSteps" + Session["userid"].ToString() + ".jpg";

        SaveGraph(CurrentTimeSteps);
        if (System.IO.File.Exists(FileRoot) == true)
            Response.Redirect("GraphExcel.aspx?GraphicPath=" + path);
    }
}
