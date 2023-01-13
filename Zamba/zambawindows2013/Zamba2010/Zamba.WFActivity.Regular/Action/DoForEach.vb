Imports Zamba.Core


<RuleMainCategory("Workflow"), RuleCategory("Reglas"), RuleSubCategory(""), RuleDescription("Repetir Regla"), RuleHelp("Repite una regla tantas veces como le sea indicado. Ejecutando también todas las reglas hijas que contenga"), RuleFeatures(False)> <Serializable()> _
Public Class DoForEach

    Inherits WFRuleParent
    Implements IDoForEach, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoForEach

    Private _Value As String
    Private _ContinueIterations As Boolean

    Private _splitText As Boolean
    Private _splitType As SplitType
    Private _splitChar As String

    Public Property ContinueIterations() As Boolean Implements IDoForEach.ContinueIterations
        Get
            Return _ContinueIterations
        End Get
        Set(ByVal value As Boolean)
            _ContinueIterations = value
        End Set
    End Property

    Public Property Value() As String Implements IDoForEach.Value
        Get
            Return _Value
        End Get
        Set(ByVal value As String)
            _Value = value
        End Set
    End Property

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Property SplitText() As Boolean Implements IDoForEach.SplitText
        Get
            Return _splitText
        End Get
        Set(ByVal value As Boolean)
            _splitText = value
        End Set
    End Property

    Public Property SplitChar() As String Implements IDoForEach.SplitChar
        Get
            Return _splitChar
        End Get
        Set(ByVal value As String)
            _splitChar = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Repetir Regla"
        End Get
    End Property

    Public Overrides Sub Dispose()

    End Sub
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

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

    Public Property SplitType() As SplitType Implements IDoForEach.SplitType
        Get
            Return _splitType
        End Get
        Set(ByVal value As SplitType)
            _splitType = value
        End Set
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal Value As String, ByVal splittext As Boolean, ByVal splitchar As String, ByVal splittype As SplitType, ByVal continueIterations As Boolean)

        MyBase.New(Id, Name, wfStepId)
        _Value = Value
        _splitText = splittext
        _splitChar = splitchar
        _splitType = splittype
        _ContinueIterations = continueIterations
        playRule = New Zamba.WFExecution.PlayDoForEach(Me)
    End Sub


    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results, refreshTasks)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

End Class
