using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ZDispatcher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Page.IsPostBack))
        {
            if (Session["User"] != null)
            {
                if (Request.QueryString["Action"] != null)
                {                    
                    string action = Request.QueryString["Action"];
                    string script = "$(document).ready(function(){ ";
                    switch (action)
                    {
                        case "insert":
                            script += "if(parent != this && typeof(parent.InsertFormModal) != undefined) {parent.InsertFormModal('1050');}";
                            break;
                        case "search":
                            script += "if(parent != this) {parent.CloseInsert(); parent.ZDispatcherRedirection_tabResult();}";
                            break;
                        case "searchresults":
                            script += "if(parent != this) {parent.CloseInsert(); parent.ZDispatcherRedirection_tabSearchResults();}";
                            break;
                        case "tasklist":
                            script += "if(parent != this) {parent.CloseInsert(); parent.ZDispatcherRedirection_tabtasklist();}";
                            break;
                        case "activetasks":
                            script += "if(parent != this) {parent.CloseInsert(); parent.SelectTabFromMasterPage('tabtasks');}";
                            break;
                        case "dortable":
                            script += "if(parent != this) {parent.CloseInsert(); parent.HomeCabPresentation(); parent.ShowTabDOR();}";
                            break;
                        case "gralreport":
                            script += "if(parent != this) {parent.CloseInsert(); parent.ZDispatcherRedirection_ShowGralReport();}";
                            break;
                        case "reports":
                            script += "if(parent != this) {parent.CloseInsert(); parent.HomeCabPresentation(); parent.ShowReports();}";
                            break;
                        case "gralreportGEC":
                            script += "if(parent != this) {parent.CloseInsert(); parent.ZDispatcherRedirection_ShowGralReportGec();}";
                            break;
                        case "semaphore":
                            script += "if(parent != this) {parent.CloseInsert(); parent.HomeCabPresentation(); parent.ShowSemaphores();}";
                            break;
                        case "GoHome":
                            script += "if(parent != this) {parent.CloseInsert(); parent.HomeCabPresentation();}";
                            break;
                        case "PedidosAysa":
                            script += "if(parent != this) {parent.CloseInsert(); parent.PedidosAysa();}";
                            break;
                        case "PedidosDAL":
                            script += "if(parent != this) {parent.CloseInsert(); parent.PedidosDAL();}";
                            break;
                        case "Pedidos24hsReport":
                            script += "if(parent != this) {parent.CloseInsert(); parent.Pedidos24hsReport();}";
                            break;
                        case "PedidosRechazados":
                            script += "if(parent != this) {parent.CloseInsert(); parent.PedidosRechazados();}";
                            break;
                        case "PendientesConformidad":
                            script += "if(parent != this) {parent.CloseInsert(); parent.PendientesConformidad();}";
                            break;
                        case "PedidosSemaforo":
                            script += "if(parent != this) {parent.CloseInsert(); parent.PedidosSemaforo();}";
                            break;
                   }
                    script += "});";
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.Page.GetType(),"ExecDispatcher",script,true);
                }
            }
        }
    }
}
