Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Generar Tarea"), RuleHelp("Crea una nueva tarea a partir de la entidad seleccionado"), RuleFeatures(False)> <Serializable()> _
Public Class DOGenerateTaskResult
    'toda regla de accion debe derivar de WFRuleParent
    Inherits WFRuleParent
    Implements IDOGenerateTaskResult, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _continueWithCurrentTasks As Boolean
    Private _doctypeId As Int64
    Private playRule As Zamba.WFExecution.PlayDOGenerateTaskResult
    Private _indices As String
    Private _filePath As String
    Private _addcWf As Boolean
    Private _dontOpenTaskAfterInsert As Boolean
    Private _autocompleteIndexsInCommon As Boolean
    Private _isValid As Boolean
    Private _SpecificWorkflowId As Int64
    Public Property OpenMode() As Int32 Implements IDOGenerateTaskResult.OpenMode


    Public Property addCurrentwf() As Boolean Implements IDOGenerateTaskResult.addCurrentwf
        Get
            Return _addcWf
        End Get
        Set(ByVal value As Boolean)
            _addcWf = value
        End Set
    End Property
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property

    Public Property ContinueWithCurrentTasks() As Boolean Implements IDOGenerateTaskResult.ContinueWithCurrentTasks
        Get
            Return _continueWithCurrentTasks
        End Get
        Set(ByVal value As Boolean)
            _continueWithCurrentTasks = value
        End Set
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Public Property docTypeId() As Int64 Implements IDOGenerateTaskResult.docTypeId
        Get
            Return _doctypeId
        End Get
        Set(ByVal value As Int64)
            _doctypeId = value
        End Set
    End Property
    Public Property indices() As String Implements IDOGenerateTaskResult.indices
        Get
            Return _indices
        End Get
        Set(ByVal value As String)
            _indices = value
        End Set
    End Property
    Public Property filePath() As String Implements IDOGenerateTaskResult.FilePath
        Get
            Return _filePath
        End Get
        Set(ByVal value As String)
            _filePath = value
        End Set
    End Property

    Public Property DontOpenTaskAfterInsert() As Boolean Implements IDOGenerateTaskResult.DontOpenTaskAfterInsert
        Get
            Return _dontOpenTaskAfterInsert
        End Get
        Set(ByVal value As Boolean)
            _dontOpenTaskAfterInsert = value
        End Set
    End Property

    Public Property AutocompleteIndexsInCommon() As Boolean Implements IDOGenerateTaskResult.AutocompleteIndexsInCommon
        Get
            Return _autocompleteIndexsInCommon
        End Get
        Set(ByVal value As Boolean)
            _autocompleteIndexsInCommon = value
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
            Return String.Empty
        End Get
    End Property

    Public Property SpecificWorkflowId As Long Implements IDOGenerateTaskResult.SpecificWorkflowId
        Get
            Return _SpecificWorkflowId
        End Get
        Set(value As Long)
            _SpecificWorkflowId = value
        End Set
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub

    Public Overrides Sub Dispose()

    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="Id">Id de la regla</param>
    ''' <param name="Name">Nombre para mostrar la regla</param>
    ''' <param name="WFStepid">Etapa Inicial</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	25/02/2008	Created
    '''
    '''  </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal docTypeId As Int64, ByVal indices As String, ByVal addcWF As Boolean, ByVal continuewithcurrenttasks As Boolean, ByVal filePath As String, ByVal showDocument As Boolean, ByVal dontOpenTaskAfterInsert As Boolean, ByVal autocompleteIndexsInCommon As Boolean, ByVal SpecificWorkflowId As Int64, ByVal OpenMode As Int32)
        MyBase.New(Id, Name, wfstepId)
        _doctypeId = docTypeId
        _indices = indices
        _addcWf = addcWF
        _filePath = filePath
        _continueWithCurrentTasks = continuewithcurrenttasks
        _dontOpenTaskAfterInsert = dontOpenTaskAfterInsert
        _autocompleteIndexsInCommon = autocompleteIndexsInCommon
        _SpecificWorkflowId = SpecificWorkflowId
        Me.OpenMode = OpenMode
        playRule = New Zamba.WFExecution.PlayDOGenerateTaskResult(Me)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo generico que se invoca para ejecutar la regla, este es el punto de entrada
    ''' en la ejecucion de la misma.
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	25/02/2008	Created
    '''     [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    ''' -----------------------------------------------------------------------------
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