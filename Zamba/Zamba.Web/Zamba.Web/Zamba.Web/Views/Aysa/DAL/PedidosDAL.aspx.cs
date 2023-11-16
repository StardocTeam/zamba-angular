﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using Zamba.Services;
using Zamba.Core;
using System.Web.Security;
using Zamba.Web.App_Code.Helpers;

public partial class Views_Aysa_PedidosDAL : System.Web.UI.Page
{
    long lastDocIDSelected = 0;
    long lastdoctypeid = 0;
    private const string PAGELOCATION = "Aysa/DAL/PedidosDAL.aspx{0}";
    private  string TASKSELECTORURL = "WF/TaskSelector.ashx?DocId={0}&DocTypeId={1}&userId=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
    const string REPORTQUERY =
" select DAL.i12362  as [Numero de Pedido], DAL.i12475  as [Numero de Requerimiento],  " +
" CAST( wfdoc.Checkin AS smalldatetime) AS [Fecha], usuario1.nombres + ' ' + usuario1.apellido as [Administrativo],  " +
" usuario2.nombres + ' ' + usuario2.apellido as [Autorizador], DAL.i12368  as [Usuario Solicitante],  " +
" Area.name  as [Direccion], Sitio.i12404 as [Sitio], Deposito.i12352 as [Deposito], Proceso.i12344  as [Proceso],  " +
" etapa.name  as [Etapa],  estado.name  as [Estado], wfdoc.Doc_type_ID as [Id Entidad],  wfdoc.Doc_ID as [ID de Zamba]  " +
" from doc_i12123 DAL  " +
" inner join wfdocument wfdoc on wfdoc.doc_id = DAL.doc_id  inner join wfworkflow wfwk  on wfwk.work_id = wfdoc.work_id  " +
" inner join wfstep etapa on etapa.step_id = wfdoc.step_id  " +
" inner join wfstepstates estado on estado.doc_state_id = wfdoc.do_state_id  " +
" inner join usrtable usuario1 on usuario1.id = DAL.i12461 inner join usrtable usuario2 on usuario2.id = DAL.i12433  " +
" inner join usrgroup area on area.id = DAL.i12451 inner join doc_i12116 sitio on sitio.i12403 = DAL.i12427  " +
" inner join doc_i12098 deposito on deposito.i12351 = DAL.i12428 inner join doc_i12125 proceso on proceso.i12436 = DAL.i12437 ORDER BY DAL.i12362  desc " ;
    UserPreferences UP = new UserPreferences();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Actualiza el timemout
            if (Zamba.Membership.MembershipHelper.CurrentUser != null)
            {
                SqlDataSource2.ConnectionString = Zamba.Servers.Server.get_Con(false, false, false).ConString;
                IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
                SqlDataSource2.SelectCommand = REPORTQUERY;

                if (!Page.IsPostBack)
                {
                    this.Title = "Reporte de Pedidos DAL" + " - Zamba Software";
                    SRights rights = new SRights();
                    Int32 type = 0;
                    if (user.WFLic) type = 1;
                    if (user.ConnectionId > 0)
                    {
                        UserPreferences UserPreferences = new UserPreferences();
                        Ucm.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "30")), type);
                        UserPreferences = null;
                    }
                    else
                        FormsAuthentication.RedirectToLoginPage();
                    rights = null;
                    RadGrid1.MasterTableView.Rebind();

                }
                else
                {
                    if (RadGrid1.SelectedItems.Count > 0)
                    {
                        long.TryParse(RadGrid1.SelectedItems[0].Cells[RadGrid1.SelectedItems[0].Cells.Count - 1].Text, out lastDocIDSelected);
                        long.TryParse(RadGrid1.SelectedItems[0].Cells[RadGrid1.SelectedItems[0].Cells.Count - 2].Text, out lastdoctypeid);
                    }

                }
            }
            else
                FormsAuthentication.RedirectToLoginPage();
            
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }
    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {        
	 String Query = String.Empty;
        try
        {
		Query = REPORTQUERY;
            SqlDataSource2.SelectCommand = Query;
            SqlDataSource2.DataBind();
            RadGrid1.Rebind();

            if (e.CommandName == "FilterRadGrid")
            {
                RadFilter1.FireApplyCommand();
            }

            if (e.CommandName == "ExportToExcel")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToExcel();
            }

            if (e.CommandName == "ExportToCSV")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToCSV();
            }

            if (e.CommandName == "ExportToWord")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToWord();
            }

            if (e.CommandName == "ExportToPDF")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToPdf();
            }
            
            if (e.CommandName == "OpenTask")
            {
                OpenTask();
            }
            
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
              e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
              e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
            {
                ConfigureExport();
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
            Zamba.Core.ZClass.raiseerror(new Exception("Query: " + Query.ToString()));
        }
    }

    protected void Header1_SkinChanged(object sender, SkinChangedEventArgs e)
        {
            //Required for dynamic skin changing
            RadGrid1.Rebind();
        }

    protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            //
        }

    protected string GetFilterIcon()
        {
            return RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Vista.Grid.Filter.gif");
        }

    protected string GetExcelIcon()
    {
        return "../../Content/images/icons/3.png";
    }

    protected string GetCSVIcon()
    {
        return "../../Content/images/icons/8.png";
    }

    protected string GetWordIcon()
    {
        return "../../Content/images/icons/2.png";
    }

    protected string GetPDFIcon()
    {
        return "../../Content/images/icons/4.png";
    }

    protected string GetOpenTaskIcon()
    {
        return "../../Content/images/icons/30.png";
    }

    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
	String Query = String.Empty;
        try
        {
		Query = REPORTQUERY;
            SqlDataSource2.SelectCommand = REPORTQUERY;
            SqlDataSource2.DataBind();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
            Zamba.Core.ZClass.raiseerror(new Exception("Query: " + Query.ToString()));
        }
    }

    protected void RadGrid1_Load(object sender, EventArgs e)
    {
        try
        {
            foreach (GridColumn column in RadGrid1.MasterTableView.RenderColumns)
            {
                if (column.UniqueName.Contains("Zamba"))
                {
                    column.Visible = false;
                }
            }
            RadGrid1.Rebind();
            FormatColumnsView();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    private void FormatColumnsView()
    {
        foreach (GridColumn col in RadGrid1.MasterTableView.RenderColumns)
        {
            if (col is GridDateTimeColumn)
            {
                GridDateTimeColumn column = (GridDateTimeColumn)col;

                column.EditDataFormatString = "{0:dd/MM/yyyy}";
                column.DataFormatString = "{0:dd/MM/yyyy}";
                column.EditFormHeaderTextFormat = "{0:dd/MM/yyyy}";

            }
        }
        RadGrid1.MasterTableView.Rebind();
    }

    protected void RadFilter1_PreRender(object sender, EventArgs e)
    {
        try
        {
            var menu = RadFilter1.FindControl("rfContextMenu") as RadContextMenu;
            menu.DefaultGroupSettings.Height = Unit.Pixel(400);
            menu.EnableAutoScroll = true;

            IEnumerable<RadDateTimePicker> pickers = ControlsHelper.FindControlsOfType<RadDateTimePicker>(RadFilter1);

            foreach (RadDateTimePicker picker in pickers)
            {
                picker.TimePopupButton.Style.Add("visibility", "hidden");
                picker.TimePopupButton.Style.Add("display", "none");
                picker.DateInput.DisplayDateFormat = "dd/MM/yyyy";
                picker.DateInput.DateFormat = "dd/MM/yyyy";
            }
            IEnumerable<RadNumericTextBox> controls = ControlsHelper.FindControlsOfType<RadNumericTextBox>(RadFilter1);
            foreach (RadNumericTextBox textBox in controls)
            {
                textBox.NumberFormat.GroupSeparator = string.Empty;
                textBox.NumberFormat.DecimalDigits = 5;
                textBox.NumberFormat.AllowRounding = true;
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    public void ConfigureExport()
    { 
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
    }

    protected void OpenTask()
    {
        //TO-DO: llamar al opener, y validar que sea el correcto.
        try
        {
            String script = String.Empty;
            if (lastDocIDSelected > 0 && lastdoctypeid > 0)
            {
                string docName = new SResult().GetResultName(lastDocIDSelected, lastdoctypeid);
                Uri currUrl = HttpContext.Current.Request.Url;

                string urlToOpen = currUrl.ToString().Replace(string.Format(PAGELOCATION, currUrl.Query), string.Format(TASKSELECTORURL, lastDocIDSelected, lastdoctypeid));

                script = "OpenTaskInOpener('" + urlToOpen + "'," + lastDocIDSelected + ",'" + docName + "');";
            }
            else
            {
                script = "alert('No se ha seleccionado ningun documento para visualizar');";
            }

            Page.ClientScript.RegisterStartupScript(typeof(Page), "DoOpenTaskScript", script, true);
            hdnIdIndustria.Value = string.Empty;
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            OpenTask();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckBox2.Checked == true)
            {
                SFileTools sf = new SFileTools();
                String Query = string.Empty;
                Query = REPORTQUERY;

                String url = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/log/temp/exportPedidosDAL" + Zamba.Membership.MembershipHelper.CurrentUser.ID + ".xls";

                String path = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\exportPedidosDAL" + Zamba.Membership.MembershipHelper.CurrentUser.ID + ".xls";

                if (sf.ExportToXLS(ServersBusiness.BuildExecuteDataSet(CommandType.Text, Query).Tables[0], path))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("window.open('{0}');", url), true);
                }
            }
            else
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToExcel();
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckBox2.Checked == true)
            {
                SFileTools sf = new SFileTools();
                String Query = string.Empty;
				Query = REPORTQUERY;


                String url = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/log/temp/exportPedidosDAL" + Zamba.Membership.MembershipHelper.CurrentUser.ID + ".csv";

                String path = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\exportPedidosDAL" + Zamba.Membership.MembershipHelper.CurrentUser.ID + ".csv";

                if (sf.ExportToCSV(ServersBusiness.BuildExecuteDataSet(CommandType.Text, Query).Tables[0], path))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("window.open('{0}');", url), true);
                }
            }
            else
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToCSV();
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }

    }

    
}