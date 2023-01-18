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

public partial class UCTaskBalances 
    : UserControl
{

    static DataSet CurrentBalances = null;

    /// <summary>
    /// Evento que se ejecuta cuando se carga la página
    /// </summary>
    /// <history> 
    ///    [Gaston]    13/11/2008   Modified    Se agrego la validación de PostBack
    /// </history
    protected void Page_Load(object sender, EventArgs e)
    {    
        if (!Page.IsPostBack)
        {
            if (null != Session["UserId"])
            {
                if (rdbByWf.Checked)
                    RowSteps.Visible = false;

                if (cmbWorkflow.Items.Count == 0)
                    InitializesComponents();
                else
                    ShowReports();
               
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

    /// <summary>
    /// Método que sirve para armar el reporte
    /// </summary>
    /// <history> 
    ///    [Gaston]    13/11/2008   Modified    Adaptado a GridView
    /// </history
    private void ShowReports()
    {
        if (!String.IsNullOrEmpty(cmbWorkflow.SelectedValue))
        {
            DataSet ds = new DataSet();

                if (rdbByWf.Checked)
                    ds = Tasks.GetTasksBalanceGroupByWorkflow(Int32.Parse(cmbWorkflow.SelectedValue.ToString()));
                else
                    ds = Tasks.GetTasksBalanceGroupByStep(Int32.Parse(cmbEtapas.SelectedValue.ToString()));
                
                if (null != ds && null != ds.Tables)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gvTasksBalance.Visible = true;
                            lblNoData.Visible = false;

                            dsTasksBalance dsTyped = new dsTasksBalance();
                            List<dsTasksBalance.dtTasksBalanceRow> DstypedRows = new List<dsTasksBalance.dtTasksBalanceRow>();

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                dsTasksBalance.dtTasksBalanceRow dr1 = dsTyped.dtTasksBalance.NewdtTasksBalanceRow();
                                dr1.TasksBalance_State = dr[0].ToString();
                                dr1.TasksBalance_Count = Int32.Parse(dr[1].ToString());
                                DstypedRows.Add(dr1);
                                dr1 = null;
                            }

                            foreach (dsTasksBalance.dtTasksBalanceRow dsRow in DstypedRows)
                            {
                                dsTyped.Tables[0].Rows.Add(dsRow);
                            }

                            dsTyped.AcceptChanges();
                            gvTasksBalance.DataSource = dsTyped;
                            gvTasksBalance.DataBind();

                            CurrentBalances = dsTyped;
                        }
                        else // en caso de que no se escuentren datos en base
                        {
                            gvTasksBalance.Visible = false;
                            lblNoData.Visible = true;
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

    /// <summary>
    /// Método que sirve para mostrar y guardar un gráfico
    /// </summary>
    /// <history>
    ///     [Gaston]  28/11/2008  Modified  Llamada al método setExcelGraphics3
    /// </history>
    public void SaveGraph(DataSet DatSt)
    {
        ExcelReportService.ExcelService ExcelGraph = new ExcelReportService.ExcelService();

        string root = "c:\\GraficoTaskBalances" + Session["userid"].ToString() + ".jpg";
        string ServerRoot=Server.MapPath("~/temp") + "\\GraficoTaskBalances" + Session["userid"].ToString() + ".jpg";
        //ExcelGraph.setExcelGraphics(DatSt, "columnas", root, false, true );
        ExcelGraph.setExcelGraphics(DatSt, "columnas", root, false, true);
        //Se guarda una cpaia del gráfico en la  carpeta temporal para poder levantarlo y visualizar el grafico en la web
        ExcelGraph.setExcelGraphics(DatSt, "columnas", ServerRoot , false, true);
        
    }


    #endregion
    protected void TaskBalances_Click(object sender, EventArgs e)
    {
        SaveGraph(CurrentBalances);        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string FileRoot = Server.MapPath("~/temp") + "\\GraficoTaskBalances" + Session["userid"].ToString() + ".jpg";
        string path = "~/temp/GraficoTaskBalances" + Session["userid"].ToString() + ".jpg";

        SaveGraph(CurrentBalances); 
        if (System.IO.File.Exists(FileRoot) == true)
            Response.Redirect("GraphExcel.aspx?GraphicPath=" + path);
    }
    /// <summary>
    /// Este método sirve para actualizar el reporte [Sebastian 09/12/208]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ActualizarTaskBalances_Click(object sender, EventArgs e)
    {        
        ShowReports();
    }
}
