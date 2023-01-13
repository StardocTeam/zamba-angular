Imports System.Windows.Forms
Imports Zamba.Core
Imports Zamba.WFActivity.Regular
Imports Zamba
Imports System.Net
Imports Zamba.PreLoad
Imports System.ComponentModel

' ------------------------------------------------------------------------------------------------------------------------------------------------
' ------------------------------------------------------------------------------------------------------------------------------------------------
'   
'   Clase utilizada para ejecutar un servicio de Windows
'
'   [Gaston]  05/12/2008  Created
' ------------------------------------------------------------------------------------------------------------------------------------------------
' ------------------------------------------------------------------------------------------------------------------------------------------------

Namespace GenericService

    Public Class GenericService
        Inherits ServiceProcess.ServiceBase

#Region " Component Designer generated code "

        Public Sub New()
            MyBase.New()

            ' This call is required by the Component Designer.
            InitializeComponent()

        End Sub

        'UserService overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        ' The main entry point for the process
        <STAThread()>
        Shared Sub Main()
            Dim ServicesToRun() As ServiceProcess.ServiceBase

            ' More than one NT Service may run within the same process. To add
            ' another service to this process, change the following line to
            ' create a second service object. For example,
            '
            '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
            '
            ServicesToRun = New ServiceProcess.ServiceBase() {New GenericService}
            Run(ServicesToRun)


        End Sub

        'Required by the Component Designer
        Private components As System.ComponentModel.IContainer

        ' NOTE: The following procedure is required by the Component Designer
        ' It can be modified using the Component Designer.  
        ' Do not modify it using the code editor.
        Friend WithEvents ImageList1 As ImageList
        <DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(GenericService))
        End Sub

#End Region
        Public Shared NetworkConnection As NetworkConnection
        Dim service As Service.Service

        ''' <summary>
        ''' Procedimiento utilizado para iniciar el servicio de Windows
        ''' </summary>
        ''' <param name="args"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	05/12/2008	Created 
        '''     [Gaston]	10/12/2008	Modified
        '''     [Marcelo]   01/10/2009  Modified     Rules Preferences Load Event
        ''' </history>
        Protected Overrides Sub OnStart(ByVal args() As String)
            Try
                Dim count As Int64 = 0
                Dim go As Boolean = True
                Dim IsService As Boolean = True

                'While go
                '    count = count + 1
                '    Threading.Thread.Sleep(count * 1000)
                'End While

                Dim status As String
                If DBBusiness.InitializeSystem(ObjectTypes.Services, New RulesInstance().GetWFActivityRegularAssembly(), IsService, status, New ErrorReportBusiness()) Then
                    Try

                        If (Boolean.Parse(UserPreferences.getValueForMachine("UseMasterCredentials", UPSections.UserPreferences, False).ToString())) Then
                            Dim MasterCredentials As New NetworkCredential()
                            MasterCredentials.Domain = ZOptBusiness.GetValueOrDefault("Domain", "pseguros.com")
                            MasterCredentials.UserName = ZOptBusiness.GetValueOrDefault("DomainUserName", "stardocservice")
                            MasterCredentials.Password = ZOptBusiness.GetValueOrDefault("DomainPasswordEncrypted", "sodio2017_nzt")
                            Dim ServerShare As String = ZOptBusiness.GetValueOrDefault("DomainServerShare", "\\svrimage\zamba")
                            NetworkConnection = New NetworkConnection(ServerShare, MasterCredentials)
                        End If
                    Catch ex As Win32Exception
                        ZClass.raiseerror(ex)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    service = New Service.Service(False)
                    AddHandler service.StopService, AddressOf stopService

                    If service.LoadService(0) = True Then

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando el servicio Generico")

                        If service.StartService() Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicio iniciado correctamente")
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha iniciado el servicio")
                            [Stop]()
                        End If
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha cargado el servicio")
                        [Stop]()
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay conexion activa con la base")
                    [Stop]()

                End If
            Catch ex As StackOverflowException
                ZClass.raiseerror(ex)
                EventLog.WriteEntry(String.Format("Zamba Service \n Exception Message: {0}\nTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error)
            Catch ex As InvalidOperationException
                ZClass.raiseerror(ex)
                EventLog.WriteEntry(String.Format("Zamba Service \n Exception Message: {0}\nTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error)
            Catch ex As InvalidProgramException
                ZClass.raiseerror(ex)
                EventLog.WriteEntry(String.Format("Zamba Service \n Exception Message: {0}\nTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error)
            Catch ex As SystemException
                ZClass.raiseerror(ex)
                EventLog.WriteEntry(String.Format("Zamba Service \n Exception Message: {0}\nTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                EventLog.WriteEntry(String.Format("Zamba Service \n Exception Message: {0}\nTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error)
            End Try
        End Sub

        Private Sub stopService()
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Entrando en el stopService")
            [Stop]()
        End Sub

        Protected Overrides Sub OnStop()
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Deteniendo el servicio Generico")

            If Not service Is Nothing AndAlso Not service.Service Is Nothing Then
                'Si es manager o agent no hace guardar el preload
                If service.Service.ServiceType <> ServiceTypes.Agent AndAlso
                    service.Service.ServiceType <> ServiceTypes.Manager Then
                End If

                ' [AlejandroR] - 21/12/09 - Created
                ' Se libera la licencia
                '---------------------------------------------
                UcmServices.Logout(service.Service.ServiceType)
                '---------------------------------------------
            End If

            service = Nothing
        End Sub


    End Class
End Namespace