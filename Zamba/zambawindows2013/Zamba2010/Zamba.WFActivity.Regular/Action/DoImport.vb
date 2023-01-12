Imports Zamba.WFExecution
Imports Zamba.Core

<RuleMainCategory("Zamba"), RuleCategory("Importacion"), RuleSubCategory(""), RuleDescription("Importación"), RuleHelp("Permite realizar una Importación"), RuleFeatures(False)> <Serializable()>
Public Class DoImport
    Inherits WFRuleParent
    Implements IDoImport, IRuleValidate
    Private _textToParse As String
    Private _playRule As PlayDoImport
    Private _listToParse As Object
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    ''Private _textToParse As String
    ''Private _listToParse As Object
    Private playRule As Zamba.WFExecution.PlayDoImport
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
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal texttoparse As String, ByVal listtoparse As String)
        MyBase.New(Id, Name, wfstepid)
        playRule = New WFExecution.PlayDoImport(Me)
        Me.TextToParse = texttoparse
        Me.ListToParse = listtoparse
        _playRule = New Zamba.WFExecution.PlayDoImport(Me)
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Valido o Realizo"
        End Get
    End Property


    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Property TextToParse() As String Implements IDoImport.TextToParse
        Get
            Return _textToParse
        End Get
        Set(ByVal value As String)
            _textToParse = value
        End Set
    End Property

    Public Property ListToParse() As Object Implements IDoImport.ListToParse
        Get
            Return _listToParse
        End Get
        Set(ByVal value As Object)
            _listToParse = value
        End Set
    End Property

End Class
