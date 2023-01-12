Imports Zamba.Core

<RuleMainCategory("Datos"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Reemplazar Texto"), RuleHelp("Permite reemplazar diferentes partes de un texto a partir de uno o varios parametros."), RuleFeatures(False)> <Serializable()> _
Public Class DoReplaceText
    Inherits WFRuleParent
    Implements IDoReplaceText, IRuleValidate


#Region "Atributos"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _text As String
    Private _replaceFields As String
    Private _saveTextAs As String
    Private _isFile
    Private _isValid As Boolean
    Private playRule As WFExecution.PlayDoReplaceText
#End Region

#Region "Constructor"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal text As String, ByVal replacefields As String, ByVal savetextas As String, ByVal isfile As Boolean)
        MyBase.New(Id, Name, wfstepid)
        _text = text
        _replaceFields = replacefields
        _saveTextAs = savetextas
        _isFile = isfile
        playRule = New WFExecution.PlayDoReplaceText(Me)
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

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides Sub Load()

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

    Public Property ReplaceFields() As String Implements Core.IDoReplaceText.ReplaceFields
        Get
            Return _replaceFields
        End Get
        Set(ByVal value As String)
            _replaceFields = value
        End Set
    End Property

    Public Property Text() As String Implements Core.IDoReplaceText.Text
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property

    Public Property SaveTextAs() As String Implements IDoReplaceText.SaveTextAs
        Get
            Return _saveTextAs
        End Get
        Set(ByVal value As String)
            _saveTextAs = value
        End Set
    End Property

    Public Property IsFile() As Boolean Implements IDoReplaceText.IsFile
        Get
            Return _isFile
        End Get
        Set(ByVal value As Boolean)
            _isFile = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
              Return String.Empty
        End Get
    End Property
End Class
