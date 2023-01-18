Imports Zamba.Core

<RuleMainCategory("Usuario"), RuleCategory("Validar"), RuleSubCategory(""), RuleDescription("Asignar grupo a usuario"), RuleHelp("Se le asigna a un usuario el grupo que se desee, indicando este por ID o por nombre de grupo."), RuleFeatures(True)> <Serializable()>
Public Class DoAsignToGroup
    Inherits WFRuleParent
    Implements IDoAsignToGroup, IRuleValidate
    Private _isValid As Boolean
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _user As String
    Private _group As String
    Private playRule As Zamba.WFExecution.PlayDoAsignToGroup

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal user As String, ByVal group As String)
        MyBase.New(Id, Name, wfstepid)
        usuario = user
        grupo = group
        playRule = New Zamba.WFExecution.PlayDoAsignToGroup(Me)
    End Sub

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property
    Public Overrides ReadOnly Property IsFull As Boolean
        Get
            Return _isFull
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Creo nuevo usuario"
        End Get
    End Property

    Public Property usuario As String Implements IDoAsignToGroup.usuario
        Get
            Return _user
        End Get
        Set(ByVal value As String)
            _user = value
        End Set
    End Property

    Public Property grupo As String Implements IDoAsignToGroup.grupo
        Get
            Return _group
        End Get
        Set(ByVal value As String)
            _group = value
        End Set
    End Property

    Public Overrides Sub Dispose()
    End Sub

    Public Overrides Sub FullLoad()
    End Sub

    Public Overrides Sub Load()
    End Sub

    Public Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Overrides Function Play(results As List(Of ITaskResult), refreshTasks As List(Of Long)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function
End Class
