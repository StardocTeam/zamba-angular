using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using Zamba;
using Zamba.Core;


[RunInstaller(true)]
public class ZambaAfipDepDigiServiceInstaller : System.Configuration.Install.Installer
{
    private ServiceInstaller serviceInstaller1;
    private ServiceProcessInstaller processInstaller;


    public ZambaAfipDepDigiServiceInstaller() : base()
    {

        // This call is required by the Component Designer.
        InitializeComponent();
       

        // Add any initialization after the InitializeComponent() call
        // Instantiate installers for process and services.
        processInstaller = new ServiceProcessInstaller();
        serviceInstaller1 = new ServiceInstaller();
        // The services will run under the system account.
        processInstaller.Account = ServiceAccount.LocalSystem;
        // The services will be started manually.
        serviceInstaller1.StartType = ServiceStartMode.Automatic;
        // ServiceName must equal those on ServiceBase derived classes.

        string Name = "ZambaAfipDepDigi";
        ZTrace.WriteLineIf(ZTrace.IsError, "name:" + Name);
        Zamba.AppBlock.ZException.ModuleName = Name;

        try
        {
           
            ZTrace.WriteLineIf(ZTrace.IsVerbose, Name);
            serviceInstaller1.ServiceName = Name;
            serviceInstaller1.Description = Name;
            serviceInstaller1.DisplayName = Name;
        }
        catch (Exception ex)
        {
            ZTrace.WriteLineIf(ZTrace.IsError, "Error de Instalacion" + ex.ToString());
            serviceInstaller1.ServiceName = Name;
        }
        // Add installers to collection. Order is not important.
        Installers.Add(serviceInstaller1);
        Installers.Add(processInstaller);
    }

    // Installer overrides dispose to clean up the component list.
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!(components == null))
                components.Dispose();
        }
        base.Dispose(disposing);
    }

    // Required by the Component Designer
    private System.ComponentModel.IContainer components;

    // NOTE: The following procedure is required by the Component Designer
    // It can be modified using the Component Designer.  
    // Do not modify it using the code editor.
    [DebuggerStepThrough()]
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
    }
}