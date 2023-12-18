Imports zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Asignar a Usuario"), RuleHelp("Permite seleccionar a que usuario o grupo se le va a asignar la tarea."), RuleFeatures(False)> <Serializable()> _
Public Class DoAsign
    Inherits WFRuleParent
    Implements IDoAsign, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoAsign
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

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub
    ' Constructor
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepID As Int64, ByVal UserId As Int32, ByVal AlternateUser As String)
        MyBase.New(Id, Name, wfStepID)
        _UserId = UserId
        _AlternateUser = AlternateUser
        playRule = New Zamba.WFExecution.PlayDoAsign(Me)
    End Sub
    'Items
    '0=UserId

    'Properties
    'Public Property RuleWStep() As IWFStep Implements IDoAsign.WFStep
    '    Get
    '        Return Me.WFStep
    '    End Get
    '    Set(ByVal value As IWFStep)
    '        Me.WFStep = value
    '    End Set
    'End Property

    Public Property UserId() As Int32 Implements IDoAsign.UserId
        Get
            Return _UserId
        End Get
        Set(ByVal value As Int32)
            _UserId = value
        End Set
    End Property
    Private _UserId As Int32

    Public Property AlternateUser() As String Implements IDoAsign.AlternateUser
        Get
            Return _AlternateUser
        End Get
        Set(ByVal value As String)
            _AlternateUser = value
        End Set
    End Property

    Private _AlternateUser As String

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Asigno Tarea"
        End Get
    End Property
End Class