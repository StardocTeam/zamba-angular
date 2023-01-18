Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.ServiceProcess


Public Class SearchServiceInstaller

    Private serviceInstall As ServiceInstaller
    Private processInstaller As ServiceProcessInstaller
    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent
        processInstaller = New ServiceProcessInstaller
        serviceInstall = New ServiceInstaller
        'el servicio corre sobre la cuenta local del sistema
        processInstaller.Account = ServiceAccount.LocalSystem
        '
        serviceInstall.StartType = ServiceStartMode.Manual
        serviceInstall.ServiceName = "Zamba.SearchService"

        Installers.Add(serviceInstall)
        Installers.Add(processInstaller)
    End Sub
End Class
