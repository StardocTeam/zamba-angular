Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Actualiza el autonombre"), RuleHelp("Ejecuta el autoname para la tarea existente o para una nueva"), RuleFeatures(False)> <Serializable()> _
Public Class DoAutoName
    Inherits WFRuleParent
    Implements IDoAutoName, IRuleValidate

#Region "Atributos"
    Private playrule As Zamba.WFExecution.PlayDoAutoName
    Private _updateMultiple As Boolean
    Private _docTypeIds As String
    Private _variabledocid As String
    Private _variabledoctypeid As String
    Private _seleccion As String
    Private _nombreColumna As String
    Private _days As Int64
    Private _isValid As Boolean
#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get

        End Get
    End Property

    Public Overrides Sub Load()

    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playrule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playrule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playrule.DiscoverParams()
    End Function

    Public Sub New(ByVal Id As Int64, _
                   ByVal Name As String, _
                   ByVal wfstepId As Int64, _
                   ByVal Seleccion As String, _
                   ByVal variabledocid As String, _
                   ByVal variabledoctypeid As String, _
                   ByVal variablenombrecolumna As String, _
                   ByVal updateMultiple As Boolean, _
                   ByVal docTypeIds As String,
                   ByVal days As Int64)

        MyBase.New(Id, Name, wfstepId)

        _seleccion = Seleccion
        _variabledocid = variabledocid
        _variabledoctypeid = variabledoctypeid
        _nombreColumna = variablenombrecolumna
        _updateMultiple = updateMultiple
        _docTypeIds = docTypeIds
        _days = days
        playrule = New Zamba.WFExecution.PlayDoAutoName(Me)
    End Sub

    Public Property Seleccion() As String Implements Core.IDoAutoName.Seleccion
        Get
            Return _seleccion.ToString()
        End Get
        Set(ByVal value As String)
            _seleccion = value
        End Set
    End Property

    Public Property variabledocid() As String Implements Core.IDoAutoName.variabledocid
        Get
            Return _variabledocid.ToString()
        End Get
        Set(ByVal value As String)
            _variabledocid = value
        End Set
    End Property

    Public Property variabledoctypeid() As String Implements Core.IDoAutoName.variabledoctypeid
        Get
            Return _variabledoctypeid.ToString()
        End Get
        Set(ByVal value As String)
            _variabledoctypeid = value
        End Set
    End Property

    Public Property days() As String Implements Core.IDoAutoName.days
        Get
            Return _days
        End Get
        Set(ByVal value As String)
            _days = value
        End Set
    End Property

    Public Property variableconmrecolumna() As String Implements Core.IDoAutoName.nombreColumna
        Get
            Return _nombreColumna.ToString()
        End Get
        Set(ByVal value As String)
            _nombreColumna = value
        End Set


    End Property

    Public Property UpdateMultiple() As Boolean Implements Core.IDoAutoName.updateMultiple
        Get
            Return _updateMultiple
        End Get
        Set(ByVal value As Boolean)
            _updateMultiple = value
        End Set
    End Property

    Public Property DocTypeIds() As String Implements Core.IDoAutoName.docTypeIds
        Get
            Return _docTypeIds
        End Get
        Set(ByVal value As String)
            _docTypeIds = value
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
    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Actualizo Nombre de Tarea"
        End Get
    End Property
End Class