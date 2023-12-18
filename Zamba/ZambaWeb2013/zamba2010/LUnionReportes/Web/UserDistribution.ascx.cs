﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using Telerik.Web.UI;

namespace Web
{
    public partial class UserDistribution : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Chart1.Series["Series1"]["PieLabelStyle"] = "Outside";

            RadAjaxManager ajM = RadAjaxManager.GetCurrent(this.Page);
            ajM.AjaxSettings.Add(new AjaxSetting("RadGrid1"));
        }

        protected void Chart1_DataBound(object sender, EventArgs e)
        {
            foreach (DataPoint point in Chart1.Series["Series1"].Points)
            {
                point["Exploded"] = "true";
            }

            bool isDataSourceEmpty = Chart1.Series["Series1"].Points.Count == 0;
            Chart1.Titles["NoDataFound"].Visible = isDataSourceEmpty;
            PrintChart.Visible = !isDataSourceEmpty;
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
            {
                ConfigureExport();
            }
        }

        public void ConfigureExport()
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = CheckBox2.Checked;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
        }
    }
}