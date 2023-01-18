using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using Zamba.Core;
using ZambaInsertService;
using System.Threading;
using Zamba.ThreadPool;

public partial class ZambaInsert
    : ServiceBase
{
    #region Constructor
    public ZambaInsert()
    {
        InitializeComponent();
    }

    #endregion

    protected override void OnStart(string[] args)
    {
        try
        {
            StartThreadService();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
   
    ThreadPoolService TPS = new ThreadPoolService();
    private void StartThreadService()
    {
        Decimal MaxThreads = 10;
        Decimal MinThreads = 1;
        Decimal Interval = 2000;
        MaxThreads = Decimal.Parse(UserPreferences.getValue("MaxThreads", UserPreferences.Sections.InsertPreferences,"10"));
        MinThreads = Decimal.Parse(UserPreferences.getValue("MinThreads", UserPreferences.Sections.InsertPreferences,"1"));
        Interval = Decimal.Parse(UserPreferences.getValue("Interval", UserPreferences.Sections.InsertPreferences, "10000"));

        TPS.StartThreadService(MaxThreads, MinThreads, Interval);
    }

    //protected override void OnStop(string[] args)
    //{
    //    try
    //    {
    //        TPS.StopThreadService();
    //    }
    //    catch (Exception ex)
    //    {
    //        ZClass.raiseerror(ex);
    //    }
    //}
}