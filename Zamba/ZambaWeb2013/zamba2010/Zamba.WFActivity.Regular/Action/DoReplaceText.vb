Imports Zamba.Core

<RuleCategory("Datos"), RuleDescription("Reemplazar Texto"), RuleHelp("Permite reemplazar diferentes partes de un texto a partir de uno o varios parametros."), RuleFeatures(False)> <Serializable()> _
Public Class DoReplaceText
    Inherits WFRuleParent
    Implements IDoReplaceText


#Region "Atributos"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _text As String
    Private _replaceFields As String
    Private _saveTextAs As String
    Private _isFile
    Private playRule As WFExecution.PlayDoReplaceText
#End Region

#Region "Constructor"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal text As String, ByVal replacefields As String, ByVal savetextas As String, ByVal isfile As Boolean)
        MyBase.New(Id, Name, wfstepid)
        Me._text = text
        Me._replaceFields = replacefields
        Me._saveTextAs = savetextas
        Me._isFile = isfile
        Me.playRule = New WFExecution.PlayDoReplaceText(Me)
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
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Property ReplaceFields() As String Implements Core.IDoReplaceText.ReplaceFields
        Get
            Return Me._replaceFields
        End Get
        Set(ByVal value As String)
            Me._replaceFields = value
        End Set
    End Property

    Public Property Text() As String Implements Core.IDoReplaceText.Text
        Get
            Return Me._text
        End Get
        Set(ByVal value As String)
            Me._text = value
        End Set
    End Property

    Public Property SaveTextAs() As String Implements IDoReplaceText.SaveTextAs
        Get
            Return Me._saveTextAs
        End Get
        Set(ByVal value As String)
            Me._saveTextAs = value
        End Set
    End Property

    Public Property IsFile() As Boolean Implements IDoReplaceText.IsFile
        Get
            Return Me._isFile
        End Get
        Set(ByVal value As Boolean)
            Me._isFile = value
        End Set
    End Property
End Class
