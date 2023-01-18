Imports System.Xml.Serialization
Imports Zamba.Core

''' <summary>
''' [AlejandroR] - Created - 28/12/09
''' Regla que permite crear un nuevo appointment en Outlook de manera automatica o
''' dejando que el usuario vea el formulario y realice los cambios que necesite.
''' </summary>
''' <remarks></remarks>

<RuleCategory("Usuarios"), RuleDescription("Outlook Appointment"), RuleHelp("Permite crear un nuevo appointment en Outlook."), RuleFeatures(False)> <Serializable()> _
Public Class DoCreateOutlookAppointment

    Inherits WFRuleParent
    Implements IDOCreateOutlookAppointment

    Private playRule As Zamba.WFExecution.PlayDOCreateOutlookAppointment

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepID As Int64, ByVal Subject As String, ByVal Body As String, ByVal Location As String, ByVal AppointmentDateType As OLAppointmentDateType, ByVal StartDate As String, ByVal StartTime As String, ByVal EndDate As String, ByVal EndTime As String, ByVal ShowAppointmentForm As Boolean)
        MyBase.New(Id, Name, wfStepID)
        Me._subject = Subject
        Me._body = Body
        Me._location = Location
        Me._startDate = StartDate
        Me._startTime = StartTime
        Me._endDate = EndDate
        Me._endTime = EndTime
        Me._appointmentDateType = AppointmentDateType
        Me._showAppointmentForm = ShowAppointmentForm
        Me.playRule = New Zamba.WFExecution.PlayDOCreateOutlookAppointment(Me)
    End Sub

    Public Overrides Function Play(ByVal results As List(Of Core.ITaskResult)) As List(Of Core.ITaskResult)
        Return Me.playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overrides Sub Dispose()
    End Sub

    Public Overrides Sub FullLoad()
    End Sub

    Public Overrides Sub Load()
    End Sub

#Region "Propiedades"

    Private _isLoaded As Boolean
    Private _isFull As Boolean

    Private _subject As String
    Private _body As String
    Private _location As String
    Private _appointmentDateType As OLAppointmentDateType
    Private _startDate As String
    Private _startTime As String
    Private _endDate As String
    Private _endTime As String
    Private _showAppointmentForm As Boolean

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Public Property Body() As String Implements Core.IDOCreateOutlookAppointment.Body
        Get
            Return _body
        End Get
        Set(ByVal value As String)
            _body = value
        End Set
    End Property

    Public Property AppointmentStartDate() As String Implements Core.IDOCreateOutlookAppointment.AppointmentStartDate
        Get
            Return _startDate
        End Get
        Set(ByVal value As String)
            _startDate = value
        End Set
    End Property

    Public Property AppointmentEndDate() As String Implements Core.IDOCreateOutlookAppointment.AppointmentEndDate
        Get
            Return _endDate
        End Get
        Set(ByVal value As String)
            _endDate = value
        End Set
    End Property

    Public Property AppointmentStartTime() As String Implements Core.IDOCreateOutlookAppointment.AppointmentStartTime
        Get
            Return _startTime
        End Get
        Set(ByVal value As String)
            _startTime = value
        End Set
    End Property

    Public Property AppointmentEndTime() As String Implements Core.IDOCreateOutlookAppointment.AppointmentEndTime
        Get
            Return _endTime
        End Get
        Set(ByVal value As String)
            _endTime = value
        End Set
    End Property

    Public Property Location() As String Implements Core.IDOCreateOutlookAppointment.Location
        Get
            Return _location
        End Get
        Set(ByVal value As String)
            _location = value
        End Set
    End Property

    Public Property Subject() As String Implements Core.IDOCreateOutlookAppointment.Subject
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _subject = value
        End Set
    End Property

    Public Property AppointmentDateType() As OLAppointmentDateType Implements Core.IDOCreateOutlookAppointment.AppointmentDateType
        Get
            Return _appointmentDateType
        End Get
        Set(ByVal value As OLAppointmentDateType)
            _appointmentDateType = value
        End Set
    End Property

    Public Property ShowAppointmentForm() As Boolean Implements Core.IDOCreateOutlookAppointment.ShowAppointmentForm
        Get
            Return _showAppointmentForm
        End Get
        Set(ByVal value As Boolean)
            _showAppointmentForm = value
        End Set
    End Property

#End Region

End Class
