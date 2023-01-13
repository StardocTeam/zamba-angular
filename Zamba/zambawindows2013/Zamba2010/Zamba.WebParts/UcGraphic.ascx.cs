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
using CrystalDecisions.CrystalReports.Engine;

public partial class UcGraphic : System.Web.UI.UserControl
{
    protected void btGraphic_Click(object sender, EventArgs e)
    {
        Int32 wf = 1742;
        Zamba.Data.WFFactory.fillBalanceByWF(wf);

        DsPerformance ds = new DsPerformance();
        DsPerformance.DtUsersPerformanceRow dr1 = ds.DtUsersPerformance.NewDtUsersPerformanceRow();
        DsPerformance.DtUsersPerformanceRow dr2 = ds.DtUsersPerformance.NewDtUsersPerformanceRow();
        DsPerformance.DtUsersPerformanceRow dr3 = ds.DtUsersPerformance.NewDtUsersPerformanceRow();


        dr1.StepId = (Int64)1;
        dr1.StepName = "Etapa1";
        dr1.UsersCount = (decimal)0.1;
        dr1.TasksCount = (decimal)0.3;


        dr2.StepId = (Int64)2;
        dr2.StepName = "Etapa2";
        dr2.UsersCount = (decimal)0.3;
        dr2.TasksCount = (decimal)0.1;


        dr3.StepId = (Int64)3;
        dr3.StepName = "Etapa3";
        dr3.UsersCount = (decimal)0.2;
        dr3.TasksCount = (decimal)0.2;


        ds.Tables[0].Rows.Add(dr1);
        ds.Tables[0].Rows.Add(dr2);
        ds.Tables[0].Rows.Add(dr3);
        ds.AcceptChanges();

        ReportDocument CurrentReport = new ReportDocument();

        CurrentReport.Load(Server.MapPath("~/") +  @"UsersPerfomance.rpt");
        CurrentReport.SetDataSource(ds);
        crvGraphic.ReportSource = CurrentReport;
    }
}