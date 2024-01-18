Imports Zamba.Core

<RuleMainCategory("Usuario"), RuleCategory("Interaccion"), RuleSubCategory(""), RuleDescription("Pedir decision al Usuario"), RuleHelp("Permite realizarle preguntas al usuario del tipo SI/NO."), RuleFeatures(True)> <Serializable()> _
Public Class DOAskDesition
    Inherits WFRuleParent
    Implements IDOAskDesition, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOAskDesition
    Private _isValid As Boolean

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

    Private _TXTVar As String
    Private _TXTAsk As String

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal TXTVar As String, ByVal TXTAsk As String)
        MyBase.New(Id, Name, wfStepId)
        _TXTVar = TXTVar
        _TXTAsk = TXTAsk
        playRule = New WFExecution.PlayDOAskDesition(Me)
    End Sub

    Public Property TxtVar() As String Implements IDOAskDesition.TXTVar
        Get
            Return _TXTVar
        End Get
        Set(ByVal value As String)
            _TXTVar = value
        End Set
    End Property


    Public Property TxtAsk() As String Implements IDOAskDesition.TXTAsk
        Get
            Return _TXTAsk
        End Get
        Set(ByVal value As String)
            _TXTAsk = value
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

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Preguntar al Usuario."
        End Get
    End Property

End Class