Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Usuarios"), RuleDescription("Agina usuario a grupo"), RuleHelp("Relaciona un usuario por su ID o nombre de usuario con un grupo"), RuleFeatures(False)> <Serializable()>
Public Class DoAsignToGroup
    Inherits WFRuleParent
    Implements IDoAsignToGroup
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _user As String
    Private _group As String
    Private playRule As Zamba.WFExecution.PlayDoAsignToGroup

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal user As String, ByVal group As String)
        MyBase.New(Id, Name, wfstepid)
        Me.usuario = user
        Me.grupo = group
        Me.playRule = New Zamba.WFExecution.PlayDoAsignToGroup(Me)
    End Sub
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

    Public Overrides Function Play(results As List(Of ITaskResult)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overrides Function PlayWeb(results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
End Class
