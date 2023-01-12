Imports Zamba.Core
Imports Zamba.WFExecution


<RuleCategory("Attributos"), RuleDescription("Limpiar cache de lista de atributo"), RuleHelp("Permite limpiar las listas almacenadas en memoria de un atributo."), RuleFeatures(True)> <Serializable()> _
Public Class DoClearAttributeListCache

    Inherits WFRuleParent
    Implements IDoClearAttributeListCache

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _attributeId As Long
    Private playRule As PlayDoClearAttributeListCache

    Public Property AttributeId() As Long Implements IDoClearAttributeListCache.AttributeId
        Get
            Return _attributeId
        End Get
        Set(ByVal value As Long)
            _attributeId = value
        End Set
    End Property

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
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New(0, String.Empty, 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal attributeId As Long)
        MyBase.New(Id, Name, wfstepid)
        Me._attributeId = attributeId
        Me.playRule = New PlayDoClearAttributeListCache(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

End Class