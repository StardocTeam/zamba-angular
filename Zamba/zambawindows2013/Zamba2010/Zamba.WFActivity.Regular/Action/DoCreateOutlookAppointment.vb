Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Office"), RuleDescription("Outlook Appointment"), RuleHelp("Permite crear un nuevo appointment en Outlook."), RuleFeatures(False)> <Serializable()> _
Public Class DoCreateOutlookAppointment

    Inherits WFRuleParent
    Implements IDOCreateOutlookAppointment, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDOCreateOutlookAppointment
    Private _isValid As Boolean
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepID As Int64, ByVal Subject As String, ByVal Body As String, ByVal Location As String, ByVal AppointmentDateType As OLAppointmentDateType, ByVal StartDate As String, ByVal StartTime As String, ByVal EndDate As String, ByVal EndTime As String, ByVal ShowAppointmentForm As Boolean)
        MyBase.New(Id, Name, wfStepID)
        _subject = Subject
        _body = Body
        _location = Location
        _startDate = StartDate
        _startTime = StartTime
        _endDate = EndDate
        _endTime = EndTime
        _appointmentDateType = AppointmentDateType
        _showAppointmentForm = ShowAppointmentForm
        playRule = New Zamba.WFExecution.PlayDOCreateOutlookAppointment(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
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

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

#End Region

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Crea una cita en outlook"
        End Get
    End Property

End Class
