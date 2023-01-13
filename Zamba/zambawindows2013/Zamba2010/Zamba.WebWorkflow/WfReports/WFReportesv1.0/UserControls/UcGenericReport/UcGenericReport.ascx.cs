using System;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Data;

public partial class WfReports_UserControls_UcGenericReport_UcGenericReport
    : UserControl
{

    private DataTable _dt;

    public DataTable Dt
    {
        get { return _dt; }
        set { _dt = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (null != Dt)
        {
          


            ReportDataSource rptDatasource = new ReportDataSource(_dt.TableName, _dt);
            this.rpvGenericReport.LocalReport.DataSources.Add(rptDatasource);

        }
    }
}
