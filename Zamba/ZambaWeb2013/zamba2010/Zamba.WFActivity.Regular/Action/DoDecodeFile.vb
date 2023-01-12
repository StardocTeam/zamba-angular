Imports Zamba.Core

<RuleCategory("Secciones"), RuleDescription("Decodificar Archivo"), RuleHelp("Permite decodificar un archivo en base64."), RuleFeatures(False)> <Serializable()> _
Public Class DoDecodeFile
    Inherits WFRuleParent
    Implements IDoDecodeFile

#Region "Atributos"

    Private _isLoaded As Boolean
    Private _isFull As Boolean

    Private _binary As String
    Private _path As String
    Private _fname As String
    Private _varpath As String
    Private _textstart As String
    Private _textend As String
    Private _extfile As String
    Private playRule As WFExecution.PlayDoDecodeFile
#End Region

#Region "Constructor"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal binary As String, ByVal path As String, ByVal fname As String, ByVal varpath As String, ByVal textstart As String, ByVal textend As String, ByVal extfile As String)
        MyBase.New(Id, Name, wfstepid)
        Me._binary = binary
        Me._path = path
        Me._fname = fname
        Me._varpath = varpath
        Me._textstart = textstart
        Me._textend = textend
        Me._extfile = extfile
        Me.playRule = New WFExecution.PlayDoDecodeFile(Me)
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

    Public Overrides Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByRef RulePendingEvent As Core.RulePendingEvents, ByRef ExecutionResult As Core.RuleExecutionResult, ByRef Params As System.Collections.Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return Me.playRule.Play(results)
    End Function

    Public Property binary() As String Implements Core.IDoDecodeFile.binary
        Get
            Return Me._binary
        End Get
        Set(ByVal value As String)
            Me._binary = value
        End Set
    End Property

    Public Property fname() As String Implements Core.IDoDecodeFile.fname
        Get
            Return Me._fname
        End Get
        Set(ByVal value As String)
            Me._fname = value
        End Set
    End Property

    Public Property path() As String Implements Core.IDoDecodeFile.path
        Get
            Return Me._path
        End Get
        Set(ByVal value As String)
            Me._path = value
        End Set
    End Property

    Public Property varpath() As String Implements Core.IDoDecodeFile.varpath
        Get
            Return Me._varpath
        End Get
        Set(ByVal value As String)
            Me._varpath = value
        End Set
    End Property

    Public Property textstart() As String Implements Core.IDoDecodeFile.textstart
        Get
            Return Me._textstart
        End Get
        Set(ByVal value As String)
            Me._textstart = value
        End Set
    End Property

    Public Property textend() As String Implements Core.IDoDecodeFile.textend
        Get
            Return Me._textend
        End Get
        Set(ByVal value As String)
            Me._textend = value
        End Set
    End Property

    Public Property extfile() As String Implements Core.IDoDecodeFile.extfile
        Get
            Return Me._extfile
        End Get
        Set(ByVal value As String)
            Me._extfile = value
        End Set
    End Property
End Class
