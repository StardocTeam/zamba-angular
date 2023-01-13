Public Class WFService
    Inherits ZClass
    Implements IWFService

#Region " Atributos "
    Private _dateStarted As String = String.Empty
    Private _dateReStarted As String = String.Empty
    Private _dateStoped As String = String.Empty
    Private _refreshRate As Double
    ' Private _workflows As ArrayList = Nothing
    Private _WFIds As System.Collections.Generic.List(Of Int64)
    Private _dateLastRefresh As Date
    Private _servicetype As ClientType = ClientType.Service
    Private _serviceID As Int64
#End Region

#Region " Propiedades "
    Public Property ServiceID() As Int64
        Get
            Return _serviceid
        End Get
        Set(ByVal value As Int64)
            _serviceid = value
        End Set
    End Property
    Public Property ServiceType() As ClientType Implements IWFService.ServiceType
        Get
            Return _servicetype
        End Get
        Set(ByVal value As ClientType)
            _servicetype = value
        End Set
    End Property
    Public Property DateStarted() As String Implements IWFService.DateStarted
        Get
            Return _dateStarted
        End Get
        Set(ByVal value As String)
            _dateStarted = value
        End Set
    End Property
    Public Property DateReStarted() As String Implements IWFService.DateReStarted
        Get
            Return _dateReStarted
        End Get
        Set(ByVal value As String)
            _dateReStarted = value
        End Set
    End Property
    Public Property DateStoped() As String Implements IWFService.DateStoped
        Get
            Return _dateStoped
        End Get
        Set(ByVal value As String)
            _dateStoped = value
        End Set
    End Property
    Public Property DateLastRefresh() As Date Implements IWFService.DateLastRefresh
        Get
            Return _dateLastRefresh
        End Get
        Set(ByVal value As Date)
            _dateLastRefresh = value
        End Set
    End Property
    'Public Property WFlows() As ArrayList Implements IWFService.WFlows
    '    Get
    '        Return _workflows
    '    End Get
    '    Set(ByVal value As ArrayList)
    '        _workflows = value
    '    End Set
    'End Property
    Public Property WFIds() As System.Collections.Generic.List(Of Int64) Implements IWFService.WFIds
        Get
            Return _WFIds
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Int64))
            _WFIds = value
        End Set
    End Property
    Public Property RefreshRate() As Double Implements IWFService.RefreshRate
        Get
            Return _refreshRate
        End Get
        Set(ByVal value As Double)
            _refreshRate = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal pWFIds As System.Collections.Generic.List(Of Int64), ByVal refreshRate As Double, ByVal serviceID As Int64)
        _WFIds = pWFIds
        _refreshRate = refreshRate
        _serviceID = serviceID

        'Try
        '    '            StartService(TService)
        'Catch ex As SynchronizationLockException
        '   ZClass.raiseerror(ex)
        'Catch ex As ThreadAbortException
        '   ZClass.raiseerror(ex)
        'Catch ex As ThreadInterruptedException
        '   ZClass.raiseerror(ex)
        'Catch ex As ThreadStateException
        '   ZClass.raiseerror(ex)
        'Catch ex As Exception
        '   ZClass.raiseerror(ex)
        'End Try
    End Sub
#End Region

    Public Overrides Sub Dispose()

    End Sub
    'Dim State As Object = Nothing
    '  Public Running As Boolean = False

    '#Region "Timers"
    '    Dim TimerRefresh As Threading.Timer
    '    Dim TimerSchedule As Threading.Timer
    '    Dim TimerMonitor As Threading.Timer
    '    Dim CBTimerB As New Threading.TimerCallback(AddressOf CheckB)
    '    Dim CBTimerC As New Threading.TimerCallback(AddressOf CheckC)
    '    Dim CBTimerM As New Threading.TimerCallback(AddressOf CheckM)
    '#End Region

    '#Region "CheckB, CheckC y CheckM"
    '    Public Sub CheckB(ByVal State As Object)
    '        SyncLock Me
    '            Me.DateLastRefresh = Now
    '            RaiseEvent ShowMsg("Chequeando " & Me.WFlows.Count & " WorkFlows")
    '            For Each WF As Zamba.core.WorkFlow In Me.WFlows
    '                WF.Steps = WFStepsFactory.GetSteps(WF)
    '                RaiseEvent ShowMsg("Chequeando " & WF.Steps.Count & " Etapas")
    '                For Each Wfstep As Wfstep In WF.Steps
    '                    Try
    '                        'chequeo reglas
    '                        RaiseEvent ShowMsg("Chequeando Etapa: " & Wfstep.Name)
    '                        WFStepBusiness.Refresh(Wfstep)
    '                        'actualizo UI
    '                        ' RaiseEvent WfStepsRefresh()
    '                    Catch ex As StackOverflowException
    '                        zamba.core.zclass.raiseerror(ex)
    '                    Catch ex As Exception
    '                        zamba.core.zclass.raiseerror(ex)
    '                    End Try
    '                Next
    '            Next
    '        End SyncLock
    '    End Sub
    '    Public Sub CheckC(ByVal State As Object)
    '        SyncLock Me
    '            Me.DateLastRefresh = Now
    '            RaiseEvent ShowMsg("Chequeando " & Me.WFlows.Count & " WorkFlows")
    '            For Each WF As Zamba.core.WorkFlow In Me.WFlows
    '                WF.Steps = WFStepsFactory.GetSteps(WF)
    '                RaiseEvent ShowMsg("Chequeando " & WF.Steps.Count & " Etapas")
    '                For Each Wfstep As Wfstep In WF.Steps
    '                    Try
    '                        RaiseEvent ShowMsg("Chequeando Etapa: " & Wfstep.Name)
    '                        WFStepBusiness.CheckSchedule(Wfstep)
    '                    Catch ex As StackOverflowException
    '                        zamba.core.zclass.raiseerror(ex)
    '                    Catch ex As Exception
    '                        zamba.core.zclass.raiseerror(ex)
    '                    End Try
    '                Next
    '            Next
    '        End SyncLock
    '    End Sub
    '    Public Sub CheckM(ByVal State As Object)
    '        Me.DateLastRefresh = Now
    '        RaiseEvent WfStepsRefresh()
    '    End Sub
    '#End Region

    '#Region "Public Functions"
    '    Public Sub RefreshRateChanged(ByVal Interval As Int32)
    '        Try
    '            Me.RefreshRate = Interval
    '            If Me.TimerMonitor Is Nothing = False Then Me.TimerMonitor.Change(Interval * 60000, Interval * 60000)
    '            If Me.TimerRefresh Is Nothing = False Then Me.TimerRefresh.Change(0, Interval * 60000)
    '            If Me.TimerSchedule Is Nothing = False Then Me.TimerSchedule.Change(0, Interval * 60000)
    '            Me.DateReStarted = Now
    '        Catch ex As Threading.SynchronizationLockException
    '            zamba.core.zclass.raiseerror(ex)
    '        Catch ex As Threading.ThreadAbortException
    '            zamba.core.zclass.raiseerror(ex)
    '        Catch ex As Threading.ThreadInterruptedException
    '            zamba.core.zclass.raiseerror(ex)
    '        Catch ex As Threading.ThreadStateException
    '            zamba.core.zclass.raiseerror(ex)
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        Catch
    '        End Try
    '    End Sub
    '    Public Sub StartService(ByVal TService As Tipo)
    '        Try
    '            Select Case TService
    '                Case Tipo.Cliente
    '                    TimerMonitor = New Threading.Timer(CBTimerM, State, RefreshRate * 60000, RefreshRate * 60000)
    '                    Running = True
    '                    Me.DateStarted = Now
    '                Case Tipo.Monitoreo
    '                    TimerMonitor = New Threading.Timer(CBTimerM, State, RefreshRate * 60000, RefreshRate * 60000)
    '                    Running = True
    '                    Me.DateStarted = Now
    '                Case Tipo.Servicio
    '                    TimerSchedule = New Threading.Timer(CBTimerC, State, 0, RefreshRate * 60000)
    '                    TimerRefresh = New Threading.Timer(CBTimerB, State, 0, RefreshRate * 60000)
    '                    Running = True
    '                    Me.DateStarted = Now
    '            End Select
    '        Catch ex As Exception
    '            Running = False
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '    Public Sub StopService()
    '        Try
    '            If Me.TimerMonitor Is Nothing = False Then Me.TimerMonitor.Dispose()
    '            If Me.TimerRefresh Is Nothing = False Then Me.TimerRefresh.Dispose()
    '            If Me.TimerSchedule Is Nothing = False Then Me.TimerSchedule.Dispose()
    '            Me.Running = False
    '            Me.DateStoped = Now
    '        Catch ex As Threading.SynchronizationLockException
    '            zamba.core.zclass.raiseerror(ex)
    '        Catch ex As Threading.ThreadAbortException
    '            zamba.core.zclass.raiseerror(ex)
    '        Catch ex As Threading.ThreadInterruptedException
    '            zamba.core.zclass.raiseerror(ex)
    '        Catch ex As Threading.ThreadStateException
    '            zamba.core.zclass.raiseerror(ex)
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        Catch
    '        End Try
    '    End Sub
    '#End Region
End Class