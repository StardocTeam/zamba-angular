using System;
using System.Web.UI.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.ChartHelpers
{
    public class SalesByCategoryChartBuilder : ChartBuilder
    {
        public SalesByCategoryChartBuilder(Chart chart) : base(chart, 1) {}

        public string CategoryName { get; set; }
        public int OrderYear { get; set; }

        protected override void CustomizeChartSeries(IList<Series> seriesList)
        {
            Series mySeries = seriesList.First();
            mySeries.Name = this.OrderYear.ToString();
            mySeries.ChartType = SeriesChartType.Line;
            mySeries.BorderWidth = 5;
            mySeries.Palette = ChartColorPalette.None;

            using (var context = new ZambaEntities())
            {
                //var salesResults = context.WFWorkflow(this.Name, this.OrderYear.ToString());
                //foreach (var result in salesResults)
                //    mySeries.Points.AddXY(result.ProductName, result.TotalPurchase.Value);
            }
        }

        protected override void CustomizeChartTitle(Title title)
        {
            title.Text = string.Format("Sales For {0} In {1}", this.CategoryName, this.OrderYear);
        }

        protected override void CustomizeChartArea(ChartArea area)
        {
            area.Area3DStyle.Enable3D = false;
            area.BackGradientStyle = GradientStyle.None;
        }
    }
}