Imports Zamba.Core


<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Web"), RuleDescription("Abrir una dirección web"), RuleHelp("Abre una URL en Home, en la misma ventana o una ventana nueva de un explorador web."), RuleFeatures(True)> <Serializable()> _
Public Class DoOpenUrl
    Inherits WFRuleParent
    Implements IDoOpenUrl, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _url As String
    Private _isValid As Boolean
    Private _openMode As OpenType
    Private playRule As Zamba.WFExecution.PlayDoOpenUrl

    Public Property Url() As String Implements Core.IDoOpenUrl.Url
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property
    Public Property OpenMode() As OpenType Implements Core.IDoOpenUrl.OpenMode
        Get
            Return _openMode
        End Get
        Set(ByVal value As OpenType)
            _openMode = value
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
    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal pUrl As String, ByVal openType As OpenType)
        MyBase.New(Id, Name, wfstepid)
        _url = pUrl
        _openMode = openType
        playRule = New Zamba.WFExecution.PlayDoOpenUrl(Me)
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


End Class