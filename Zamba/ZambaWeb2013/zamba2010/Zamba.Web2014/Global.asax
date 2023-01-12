<%@ Application Language="C#" %>

<script runat="server">
    static System.Timers.Timer checkInactiveSessions;
    public static string ZambaVersion = "2.9.3.0";
    void Application_Start(object sender, EventArgs e) 
    {  
        BundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);

        ZambaVersion = "2.9.3.0";
        
        checkInactiveSessions = new System.Timers.Timer(60000);
        checkInactiveSessions.Elapsed += CheckInactiveSessionsHandler;
        checkInactiveSessions.Enabled = true;

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        CheckInactiveSessions();
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started   
        CheckInactiveSessions();    
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        CheckInactiveSessions();
    }

    public static void CheckInactiveSessionsHandler(Object source, System.Timers.ElapsedEventArgs e)
    {
        CheckInactiveSessions();
    }

    private static void CheckInactiveSessions()
    {
        try
        {

        checkInactiveSessions.Enabled = false;
        if (Zamba.Servers.Server.ConInitialized == false)
        {
            long conid;
            while ((conid = Zamba.Core.Ucm.GetFirstExpiredConnection()) != 0)
            {
                //Zamba.Core.WF.WF.WFTaskBusiness.UpdateConIDTaskStateToAsign(conid);
                if (Zamba.Core.Ucm.UserUniqueConnection(conid))
                {
                    Zamba.Core.WF.WF.WFTaskBusiness.CloseOpenTasksByConId(conid);
                }
                Zamba.Core.WF.WF.WFTaskBusiness.UpdateConIDTaskStateToAsign(conid);
                Zamba.Core.Ucm.RemoveConnectionFromWeb(conid);

            }
                Zamba.Core.WF.WF.WFTaskBusiness.ReleaseOpenTasksWithOutConnection();
                checkInactiveSessions.Enabled = true;
        }

        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }

        }


  

    
</script>
