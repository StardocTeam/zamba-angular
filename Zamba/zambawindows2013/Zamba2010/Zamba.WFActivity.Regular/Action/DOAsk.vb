Imports Zamba.Core


<RuleMainCategory("Usuario"), RuleCategory("Interaccion"), RuleSubCategory(""), RuleDescription("Realiza una Pregunta al Usuario"), RuleHelp("Permite realizar una pregunta al usuario y guarda la respuesta a la misma."), RuleFeatures(True)> <Serializable()> _
Public Class DoAsk
    Inherits WFRuleParent
    Implements IDOAsk, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _Variable As String
    Private _Mensaje As String
    Private _askOnetime As Boolean
    Private _ValorPorDefecto As String
    Private _isValid As Boolean
    'Private _Tamaño As Integer
    Private playRule As Zamba.WFExecution.PlayDOAsk


    Public Property ValorPorDefecto() As String Implements IDOAsk.ValorPorDefecto
        Get
            Return _ValorPorDefecto
        End Get
        Set(ByVal value As String)
            _ValorPorDefecto = value
        End Set
    End Property

    Public Property Variable() As String Implements IDOAsk.Variable
        Get
            Return _Variable
        End Get
        Set(ByVal value As String)
            _Variable = value
        End Set
    End Property

    Public Property Mensaje() As String Implements IDOAsk.Mensaje
        Get
            Return _Mensaje
        End Get
        Set(ByVal value As String)
            _Mensaje = value
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

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal pMensaje As String, ByVal pVariable As String, ByVal askonetime As Boolean, ByVal pValorPorDefecto As String)
        MyBase.New(Id, Name, wfstepid)
        Mensaje = pMensaje
        Variable = pVariable
        _askOnetime = askonetime
        ValorPorDefecto = pValorPorDefecto
        playRule = New Zamba.WFExecution.PlayDOAsk(Me)
    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
    'Dim NewSortedList As New SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Pregunto al usuario"
        End Get
    End Property

    Public Property AskOnetime() As Boolean Implements Core.IDOAsk.AskOnetime
        Get
            Return _askOnetime
        End Get
        Set(ByVal value As Boolean)
            _askOnetime = value
        End Set
    End Property
    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
End Class
