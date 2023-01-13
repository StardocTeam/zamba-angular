Imports Zamba.Core
Imports Zamba.WFExecution

<RuleCategory("Archivos y Aplicaciones"), RuleDescription("Editar Word"), RuleHelp("Permite editar un archivo Word"), RuleFeatures(False)> <Serializable()>
Public Class DoEditWord
    Inherits WFRuleParent
    Implements IDoEditWord

#Region "Attributes"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean

    Private _wordPath As String

    Private playRule As PlayDoEditWord
#End Region

#Region "Properties"
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

    Public Property WordPath As String Implements IDoEditWord.WordPath
        Get
            Return _wordPath
        End Get
        Set(ByVal value As String)
            _wordPath = value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal wordPath As String)
        MyBase.New(Id, Name, wfstepId)

        _wordPath = wordPath

        playRule = New PlayDoEditWord(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New PlayDoEditWord(Me)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New PlayDoEditWord(Me)
        Return playRule.PlayWeb(results, RulePendingEvent, RulePendingEvent, Params)
    End Function

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub
#End Region
End Class
