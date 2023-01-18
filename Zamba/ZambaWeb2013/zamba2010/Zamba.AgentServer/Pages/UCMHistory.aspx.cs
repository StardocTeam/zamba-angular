using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Charting;
using System.Data;

namespace Zamba.AgentServer.Pages
{
    public partial class UCMHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RadSlider2.EnableDragRange = true;
        }

        bool GroupByType = false;

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                RadGrid1.ExportSettings.IgnorePaging = true;
                //isExporting = true;
                if (!RadGrid1.ExportSettings.IgnorePaging)
                {
                    RadGrid1.Rebind();
                }
            }
        }

        protected void RadChart1_Click(object sender, ChartClickEventArgs args)
        {
            if (args.SeriesItem != null)

                if (args.SeriesItem.Parent.Name == "Cantidad")
                {
                    String Param = ((Telerik.Charting.ChartSeriesItem)(args.Element)).Name;
                    this.HiddenParam.Value = Param;
                }
        }

        private void LoadGrid()
        {
            String Client = Request.QueryString["Cliente"];
            String Month = this.DropDownList2.SelectedValue;
            String Year = this.DropDownList1.SelectedValue;
            String DesdeDia = lblSelectionStart2.Text;
            String HastaDia = lblSelectionEnd2.Text;
            string query;
            if (GroupByType)
            {
                query = String.Format("select Cliente,Anio, Mes, Dia, Hora, [Tipo], SUM(Cantidad) as Cantidad from ( select   Client as Cliente,CASE WHEN [TipoLicencia] = 0 THEN 'Documental' WHEN  [TipoLicencia] = 1 then 'Workflow' else 'Otro' END as Tipo,   count(1) as Cantidad,  YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes, DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora   from (SELECT count(1) as Cantidad,TYPE as [TipoLicencia], Client, UpdateDate FROM UCMClientSset where winuser not in ('bpm-1','Servicio')  AND (([Client] <> '{0}') OR ([Client] = '{0}' AND WINPC LIKE 'DEVPC%' AND WINPC NOT IN ('DEVPC119', 'DEVPC118', 'DEVPC108')))  and YEAR(updatedate) = {1}  and Month(updatedate) = {2}  and Day(updatedate) >= {3} and Day(updatedate) <= {4}  and datepart(hh,UpdateDate) > 8 and datepart(hh,UpdateDate) <19      group by type,user_id, winuser,  Client,UpdateDate) as LicxTipoSinUsuaDupxPC                group by Client,UpdateDate, [TipoLicencia]  )  as Sub               group by Cliente,Anio, Mes, Dia, Hora, [Tipo] order by Anio, Mes, Dia, Hora", Client, Year, Month, DesdeDia, HastaDia);
            }
            else
            {
                query = String.Format("select Cliente,Anio, Mes, Dia, Hora, SUM(Cantidad) as Cantidad from ( select   Client as Cliente,  count(1) as Cantidad,  YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes, DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora   from (SELECT count(1) as Cantidad, Client,              UpdateDate FROM UCMClientSset where winuser not in ('bpm-1','Servicio') AND (([Client] <> '{0}') OR ([Client] = '{0}' AND WINPC LIKE 'DEVPC%' AND WINPC NOT IN ('DEVPC119', 'DEVPC118', 'DEVPC108')))  and YEAR(updatedate) = {1}  and Month(updatedate) = {2}  and Day(updatedate) >= {3} and Day(updatedate) <= {4}  and datepart(hh,UpdateDate) > 8 and datepart(hh,UpdateDate) <19 group by user_id, winuser,  Client,UpdateDate) as LicxTipoSinUsuaDupxPC                group by Client,UpdateDate )  as Sub               group by Cliente,Anio, Mes, Dia, Hora  order by Anio, Mes, Dia, Hora", Client, Year, Month, DesdeDia, HastaDia);
            }
            DataSet ds = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
            this.RadGrid1.DataSource = ds;
            this.RadGrid1.DataBind();
        }

        private void LoadStadistics()
        {
            String Client = Request.QueryString["Cliente"];
            String Month = this.DropDownList2.SelectedValue;
            String Year = this.DropDownList1.SelectedValue;
            String DesdeDia = lblSelectionStart2.Text;
            String HastaDia = lblSelectionEnd2.Text;
            string query;
            if (GroupByType)
            {
                query = String.Format("select MAX(Cantidad) as Maximo,MIN(Cantidad) as Minimo,AVG(Cantidad) as Promedio from ( select Anio, Mes, Dia, Hora, SUM(Cantidad) as Cantidad from ( select   CASE WHEN [TipoLicencia] = 0 THEN 'Documental' WHEN  [TipoLicencia] = 1 then 'Workflow' else 'Otro' END as Tipo,   count(1) as Cantidad,  YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes, DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora   from (SELECT count(1) as Cantidad,TYPE as [TipoLicencia], UpdateDate FROM UCMClientSset where winuser not in ('bpm-1','Servicio') AND (([Client] <> '{0}') OR ([Client] = '{0}' AND WINPC LIKE 'DEVPC%' AND WINPC NOT IN ('DEVPC119', 'DEVPC118', 'DEVPC108')))  and YEAR(updatedate) = {1}  and Month(updatedate) = {2} and Day(updatedate) >= {3} and Day(updatedate) <= {4} and datepart(hh,UpdateDate) > 8 and datepart(hh,UpdateDate) <19  group by type,user_id, winuser, UpdateDate) as LicxTipoSinUsuaDupxPC   group by UpdateDate, [TipoLicencia]  )  as Sub  group by Anio, Mes, Dia, Hora  )  as Sub2   ", Client, Year, Month, DesdeDia, HastaDia);
            }
            else
            {
                query = String.Format("select MAX(Cantidad) as Maximo,MIN(Cantidad) as Minimo,AVG(Cantidad) as Promedio from ( select Anio, Mes, Dia, Hora, SUM(Cantidad) as Cantidad from ( select   count(1) as Cantidad,  YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes, DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora   from (SELECT count(1) as Cantidad, UpdateDate FROM UCMClientSset where winuser not in ('bpm-1','Servicio') AND (([Client] <> '{0}') OR ([Client] = '{0}' AND WINPC LIKE 'DEVPC%' AND WINPC NOT IN ('DEVPC119', 'DEVPC118', 'DEVPC108')))  and YEAR(updatedate) = {1}  and Month(updatedate) = {2} and Day(updatedate) >= {3} and Day(updatedate) <= {4} and datepart(hh,UpdateDate) > 8 and datepart(hh,UpdateDate) <19  group by user_id, winuser, UpdateDate) as LicxTipoSinUsuaDupxPC   group by UpdateDate )  as Sub  group by Anio, Mes, Dia, Hora  )  as Sub2   ", Client, Year, Month, DesdeDia, HastaDia);
            }
            DataSet ds = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
            this.DataList1.DataSource = ds;
            this.DataList1.DataBind();

        }

        //private void LoadGraph()
        //{ 
        //String Client = Request.QueryString["Cliente"] ;
        //    String Month = this.DropDownList2.SelectedValue;
        //    String Year = this.DropDownList1.SelectedValue;
        //    String DesdeDia = lblSelectionStart2.Text;
        //    String HastaDia = lblSelectionEnd2.Text;
        //    string query;
        //    if (GroupByType)
        //    {
        //        query = String.Format("select (CONVERT(NVARCHAR,Dia ) + '/' + CONVERT(NVARCHAR,Mes) + '/' + CONVERT(NVARCHAR,Anio) + ' ' + CONVERT(NVARCHAR,Hora)) as Fecha, [Tipo], SUM(Cantidad) as Cantidad from ( select  CASE WHEN [TipoLicencia] = 0 THEN 'Documental' WHEN  [TipoLicencia] = 1 then 'Workflow' else 'Otro' END as Tipo,   count(1) as Cantidad,  YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes, DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora  from (SELECT count(1) as Cantidad,TYPE as [TipoLicencia], UpdateDate FROM UCMClientSset where winuser not in ('bpm-1','Servicio') and [Client] = '{0}' and YEAR(updatedate) = {1}  and Month(updatedate) = {2}  and Day(updatedate) >= {3} and Day(updatedate) <= {4}  and datepart(hh,UpdateDate) > 8 and datepart(hh,UpdateDate) <19  group by type,user_id, winuser, UpdateDate) as LicxTipoSinUsuaDupxPC group by UpdateDate, [TipoLicencia]  )  as Sub   group by Anio, Mes, Dia, Hora, [Tipo] order by   Anio, Mes, Dia, Hora", Client, Year, Month, DesdeDia, HastaDia);
        //    }
        //    else
        //    {
        //        query = String.Format("select (CONVERT(NVARCHAR,Dia ) + '/' + CONVERT(NVARCHAR,Mes) + '/' + CONVERT(NVARCHAR,Anio) + ' ' + CONVERT(NVARCHAR,Hora)) as Fecha, SUM(Cantidad) as Cantidad from ( select   count(1) as Cantidad,  YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes, DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora  from (SELECT count(1) as Cantidad,UpdateDate FROM UCMClientSset where winuser not in ('bpm-1','Servicio') and [Client] = '{0}' and YEAR(updatedate) = {1}  and Month(updatedate) = {2}  and Day(updatedate) >= {3} and Day(updatedate) <= {4}  and datepart(hh,UpdateDate) > 8 and datepart(hh,UpdateDate) <19  group by user_id, winuser, UpdateDate) as LicxTipoSinUsuaDupxPC group by UpdateDate  )  as Sub   group by Anio, Mes, Dia, Hora  order by   Anio, Mes, Dia, Hora", Client, Year, Month, DesdeDia, HastaDia);
        //    }
        //DataSet ds = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
        //this.RadChart1.DataSource = ds;
        //this.RadChart1.DataBind();

        //}

        protected void RadChart1_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
        {
            String[] Items = ((DataRowView)e.DataItem)["Fecha"].ToString().Split(char.Parse("/"));

            String NewName = Request.QueryString["Cliente"] + "/";
            NewName += Items[2].Split(char.Parse(" "))[0] + "/";
            NewName += Items[1] + "/";
            NewName += Items[0] + "/";
            NewName += Items[2].Split(char.Parse(" "))[1];

            e.SeriesItem.Name = NewName;
        }

        protected void BtnSendReportByMail_Click(object sender, EventArgs e)
        {
            Zamba.AgentServer.WS.UCMService US = new WS.UCMService();
            US.SendResume(this.TextBox1.Text, Request.QueryString["Cliente"]);
        }

        protected void PostBack1_Click(object sender, EventArgs e)
        {
            LoadStadistics();
            LoadGrid();
            //LoadGraph();
        }

        protected void RadSlider2_ValueChanged(object sender, EventArgs e)
        {
            lblSelectionStart2.Text = RadSlider2.SelectionStart.ToString();
            lblSelectionEnd2.Text = RadSlider2.SelectionEnd.ToString();
            LoadStadistics();
            LoadGrid();
            //LoadGraph();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            GroupByType = this.CheckBox1.Checked;
            LoadStadistics();
            LoadGrid();
            //LoadGraph();
        }

        //protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //protected void DropDownList2_TextChanged(object sender, EventArgs e)
        //{

        //}

        //protected void DropDownList2_Init(object sender, EventArgs e)
        //{

        //}

        //protected void DropDownList2_Load(object sender, EventArgs e)
        //{

        //}

        protected void DropDownList2_DataBound(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                LoadStadistics();
                LoadGrid();
                //LoadGraph();
            }
        }
    }
}
