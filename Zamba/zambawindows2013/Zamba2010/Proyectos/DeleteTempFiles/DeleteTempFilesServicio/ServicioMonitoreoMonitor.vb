Imports System.ServiceProcess
Imports System.ComponentModel
Imports System.Configuration.Install

<RunInstaller(True)> Public Class ServerImportInstaller
    Inherits System.Configuration.Install.Installer

    Private serviceInstaller1 As ServiceInstaller
    Private processInstaller As ServiceProcessInstaller

#Region " Component Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        ' Instantiate installers for process and services.
        processInstaller = New ServiceProcessInstaller
        serviceInstaller1 = New ServiceInstaller
        ' The services will run under the system account.
        processInstaller.Account = ServiceAccount.LocalSystem
        ' The services will be started manually.
        serviceInstaller1.StartType = ServiceStartMode.Automatic
        ' ServiceName must equal those on ServiceBase derived classes.
        serviceInstaller1.ServiceName = "Zamba Servicio de borrado de Temporales"
        ' Add installers to collection. Order is not important.
        Installers.Add(serviceInstaller1)
        Installers.Add(processInstaller)
    End Sub

    'Installer overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub
#End Region
End Class
