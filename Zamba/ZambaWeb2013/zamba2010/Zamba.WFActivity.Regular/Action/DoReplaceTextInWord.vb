Imports Zamba.Core

<RuleCategory("Datos"), RuleDescription("Reemplazar Texto en word"), RuleHelp("Permite reemplazar diferentes partes de un texto en un documento word a partir de uno o varios parametros."), RuleFeatures(False)> <Serializable()> _
Public Class DoReplaceTextInWord
    Inherits WFRuleParent
    Implements IDoReplaceTextInWord


#Region "Atributos"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As WFExecution.PlayDoReplaceTextInWord
    Private _wordPath As String
    Private _replaceFields As String
    Private _newPath As String
    Private _caseSensitive As Boolean
    Private _saveoriginal As Boolean
#End Region

#Region "Constructor"

    Private _maskName As String

    Public Sub New(ByVal Id As Int64, ByVal name As String, ByVal wfstepid As Int64, ByVal wordpath As String,
                   ByVal replacefields As String, ByVal newpath As String, ByVal casesensitive As Boolean, ByVal saveoriginal As Boolean)
        MyBase.New(Id, name, wfstepid)
        Me._replaceFields = replacefields
        Me.playRule = New WFExecution.PlayDoReplaceTextInWord(Me)
        Me._caseSensitive = casesensitive
        Me._newPath = newpath
        Me._replaceFields = replacefields
        Me._wordPath = wordpath
        Me._saveoriginal = saveoriginal
    End Sub
#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

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

    Public Overrides Sub Load()

    End Sub

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return Me.playRule.Play(results)
    End Function

    Public Overrides Function PlayWeb(ByVal results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As List(Of ITaskResult)
        Return Me.playRule.Play(results)
    End Function

    Public Property WordPath() As String Implements IDoReplaceTextInWord.WordPath
        Get
            Return _wordPath
        End Get
        Set(ByVal value As String)
            _wordPath = value
        End Set
    End Property

    Public Property ReplaceFields() As String Implements IDoReplaceTextInWord.ReplaceFields
        Get
            Return _replaceFields
        End Get
        Set(ByVal value As String)
            _replaceFields = value
        End Set
    End Property

    Public Property NewPath() As String Implements IDoReplaceTextInWord.NewPath
        Get
            Return _newPath
        End Get
        Set(ByVal value As String)
            _newPath = value
        End Set
    End Property

    Public Property CaseSensitive() As Boolean Implements IDoReplaceTextInWord.CaseSensitive
        Get
            Return _caseSensitive
        End Get
        Set(ByVal value As Boolean)
            _caseSensitive = value
        End Set
    End Property

    Public Property SaveOriginalPath() As Boolean Implements IDoReplaceTextInWord.SaveOriginalPath
        Get
            Return _saveoriginal
        End Get
        Set(ByVal value As Boolean)
            _saveoriginal = value
        End Set
    End Property
End Class
