Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Office"), RuleDescription("Outlook Calendar"), RuleHelp("Permite crear un nuevo calendario en Outlook."), RuleFeatures(False)> <Serializable()> _
Public Class DoCreateOutlookCalendar

    Inherits WFRuleParent
    Implements IDOCreateOutlookCalendar, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDOCreateOutlookCalendar

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepID As Int64, ByVal Subject As String, ByVal Body As String, ByVal Location As String, ByVal AppointmentDateType As OLAppointmentDateType, ByVal StartDate As String, ByVal StartTime As String, ByVal EndDate As String, ByVal EndTime As String, ByVal ShowGeneratedCalendar As Boolean, ByVal AllDayEvent As Boolean, ByVal toMails As String, ByVal Organizer As String, ByVal AutomaticSend As Boolean)
        MyBase.New(Id, Name, wfStepID)
        _subject = Subject
        _body = Body
        _location = Location
        _startDate = StartDate
        _startTime = StartTime
        _endDate = EndDate
        _endTime = EndTime
        _appointmentDateType = AppointmentDateType
        _showGeneratedCalendar = ShowGeneratedCalendar
        _allDayAppointment = AllDayEvent
        _toMails = toMails
        _organizer = Organizer
        _automaticSend = AutomaticSend
        playRule = New Zamba.WFExecution.PlayDOCreateOutlookCalendar(Me)
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

    Private _organizer As String
    Private _toMails As String
    Private _subject As String
    Private _body As String
    Private _location As String
    Private _appointmentDateType As OLAppointmentDateType
    Private _startDate As String
    Private _startTime As String
    Private _endDate As String
    Private _endTime As String
    Private _showGeneratedCalendar As Boolean
    Private _allDayAppointment As Boolean
    Private _automaticSend As Boolean
    Private _isValid As Boolean

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

    Public Property Body() As String Implements Core.IDOCreateOutlookCalendar.Body
        Get
            Return _body
        End Get
        Set(ByVal value As String)
            _body = value
        End Set
    End Property

    Public Property AppointmentStartDate() As String Implements Core.IDOCreateOutlookCalendar.AppointmentStartDate
        Get
            Return _startDate
        End Get
        Set(ByVal value As String)
            _startDate = value
        End Set
    End Property

    Public Property AppointmentEndDate() As String Implements Core.IDOCreateOutlookCalendar.AppointmentEndDate
        Get
            Return _endDate
        End Get
        Set(ByVal value As String)
            _endDate = value
        End Set
    End Property

    Public Property AppointmentStartTime() As String Implements Core.IDOCreateOutlookCalendar.AppointmentStartTime
        Get
            Return _startTime
        End Get
        Set(ByVal value As String)
            _startTime = value
        End Set
    End Property

    Public Property AppointmentEndTime() As String Implements Core.IDOCreateOutlookCalendar.AppointmentEndTime
        Get
            Return _endTime
        End Get
        Set(ByVal value As String)
            _endTime = value
        End Set
    End Property

    Public Property Location() As String Implements Core.IDOCreateOutlookCalendar.Location
        Get
            Return _location
        End Get
        Set(ByVal value As String)
            _location = value
        End Set
    End Property

    Public Property Organizer() As String Implements Core.IDOCreateOutlookCalendar.Organizer
        Get
            Return _organizer
        End Get
        Set(ByVal value As String)
            _organizer = value
        End Set
    End Property

    Public Property ToMails() As String Implements Core.IDOCreateOutlookCalendar.ToMails
        Get
            Return _toMails
        End Get
        Set(ByVal value As String)
            _toMails = value
        End Set
    End Property

    Public Property Subject() As String Implements Core.IDOCreateOutlookCalendar.Subject
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _subject = value
        End Set
    End Property

    Public Property AppointmentDateType() As OLAppointmentDateType Implements Core.IDOCreateOutlookCalendar.AppointmentDateType
        Get
            Return _appointmentDateType
        End Get
        Set(ByVal value As OLAppointmentDateType)
            _appointmentDateType = value
        End Set
    End Property

    Public Property ShowGeneratedCalendar() As Boolean Implements Core.IDOCreateOutlookCalendar.ShowGeneratedCalendar
        Get
            Return _showGeneratedCalendar
        End Get
        Set(ByVal value As Boolean)
            _showGeneratedCalendar = value
        End Set
    End Property

    Public Property AllDayAppointment() As Boolean Implements Core.IDOCreateOutlookCalendar.AllDayAppointment
        Get
            Return _allDayAppointment
        End Get
        Set(ByVal value As Boolean)
            _allDayAppointment = value
        End Set
    End Property

    Public Property AutomaticSend() As Boolean Implements Core.IDOCreateOutlookCalendar.AutomaticSend
        Get
            Return _automaticSend
        End Get
        Set(ByVal value As Boolean)
            _automaticSend = value
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
            Return "Crea un Calendario en Outlook"
        End Get
    End Property
End Class
