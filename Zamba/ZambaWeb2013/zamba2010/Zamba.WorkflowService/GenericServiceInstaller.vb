Imports System.ServiceProcess
Imports System.ComponentModel
Imports Zamba.Core
Imports System.Windows.Forms
Imports Zamba.WFActivity.Regular
Imports Zamba

<RunInstaller(True)> Public Class GenericServiceInstaller
    Inherits System.Configuration.Install.Installer

    Private serviceInstaller1 As ServiceInstaller
    Private processInstaller As ServiceProcessInstaller

#Region " Component Designer generated code "

    Public Sub New()

        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()
        Dim status As String
        DBBusiness.InitializeSystem(ObjectTypes.Services, New RulesInstance().GetWFActivityRegularAssembly(), False, status, New ErrorReportBusiness())


        'Add any initialization after the InitializeComponent() call
        ' Instantiate installers for process and services.
        processInstaller = New ServiceProcessInstaller()
        serviceInstaller1 = New ServiceInstaller()
        ' The services will run under the system account.
        processInstaller.Account = ServiceAccount.LocalSystem
        ' The services will be started manually.
        serviceInstaller1.StartType = ServiceStartMode.Automatic
        ' ServiceName must equal those on ServiceBase derived classes.

        Dim ID As Int64 = Int64.Parse(ServiceBusiness.getIniValue("ServiceID", "0"))
        Dim Name As String = ServiceBusiness.getIniValue("ServiceName", "WFService")
        ZTrace.WriteLineIf(ZTrace.IsError, "name:" & Name)
        Zamba.AppBlock.ZException.ModuleName = Name

        Try

            ZTrace.WriteLineIf(ZTrace.IsError, "ID:" & ID)
            ZTrace.WriteLineIf(ZTrace.IsError, "Application.ProductName:" & Application.ProductName)
            ZTrace.WriteLineIf(ZTrace.IsError, "Application.StartupPath:" & Application.StartupPath)

            If ID = 0 Then
                ZTrace.WriteLineIf(ZTrace.IsError, "Application.ProductName:" & Application.ProductName)
                ZTrace.WriteLineIf(ZTrace.IsError, "Application.StartupPath:" & Application.StartupPath)

                serviceInstaller1.ServiceName = Name
                serviceInstaller1.Description = "Zamba sin INI - Configurar y reinstalar"
                ZTrace.WriteLineIf(ZTrace.IsError, "Zamba sin INI - Configurar y reinstalar" & Application.StartupPath)

            Else
                serviceInstaller1.ServiceName = Name
                ZTrace.WriteLineIf(ZTrace.IsVerbose, Name)
                serviceInstaller1.Description = "Zamba Servicio sin BD - Configurar y reinstalar"
                ZTrace.WriteLineIf(ZTrace.IsVerbose, serviceInstaller1.Description)
                ZTrace.WriteLineIf(ZTrace.IsError, "Zamba sin BD - Configurar y reinstalar" & Application.StartupPath)

                ZTrace.WriteLineIf(ZTrace.IsVerbose, Name)
                serviceInstaller1.ServiceName = Name
                serviceInstaller1.Description = Name
                serviceInstaller1.DisplayName = Name
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "Error de Instalacion" & ex.ToString)
            serviceInstaller1.ServiceName = Name
        End Try
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
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

End Class