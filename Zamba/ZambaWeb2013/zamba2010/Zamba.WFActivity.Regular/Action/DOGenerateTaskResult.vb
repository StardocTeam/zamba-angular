Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization


''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DOGenerateTaskResult
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla de Zamba WorkFlow
''' </summary>
''' <remarks>
''' Hereda de WFRuleParent
''' </remarks>
''' <history>
''' 	[Marcelo]	25/02/2008	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Tareas"), RuleDescription("Generar Tarea"), RuleHelp("Crea una nueva tarea a partir del entidad seleccionado"), RuleFeatures(False)> <Serializable()> _
Public Class DOGenerateTaskResult
    'toda regla de accion debe derivar de WFRuleParent
    Inherits WFRuleParent
    Implements IDOGenerateTaskResult
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _continueWithCurrentTasks As Boolean
    Private _doctypeId As Int64
    Private playRule As Zamba.WFExecution.PlayDOGenerateTaskResult
    Private _indices As String
    Private _filePath As String
    Private _addcWf As Boolean
    Private _showDocument As Boolean
    Private _dontOpenTaskAfterInsert As Boolean
    Private _autocompleteIndexsInCommon As Boolean
    Private _specificWorkflowId As Int64
    Public Property OpenMode() As Int32 Implements IDOGenerateTaskResult.OpenMode
    Public Property addCurrentwf() As Boolean Implements IDOGenerateTaskResult.addCurrentwf
        Get
            Return Me._addcWf
        End Get
        Set(ByVal value As Boolean)
            Me._addcWf = value
        End Set
    End Property
    Public Property showDocument() As Boolean Implements IDOGenerateTaskResult.showDocument
        Get
            Return Me._showDocument
        End Get
        Set(ByVal value As Boolean)
            Me._showDocument = value
        End Set
    End Property
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property

    Public Property ContinueWithCurrentTasks() As Boolean Implements IDOGenerateTaskResult.ContinueWithCurrentTasks
        Get
            Return Me._continueWithCurrentTasks
        End Get
        Set(ByVal value As Boolean)
            Me._continueWithCurrentTasks = value
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

    Public Property specificWorkflowId As Long Implements IDOGenerateTaskResult.SpecificWorkflowId
        Get
            Return _specificWorkflowId
        End Get
        Set(value As Long)
            _specificWorkflowId = value
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal docTypeId As Int64, ByVal indices As String, ByVal addcWF As Boolean, ByVal continuewithcurrenttasks As Boolean, ByVal filePath As String, ByVal showDocument As Boolean, ByVal dontOpenTaskAfterInsert As Boolean, ByVal autocompleteIndexsInCommon As Boolean, ByVal specificWorkflowId As Int64, ByVal OpenMode As Int32)
        MyBase.New(Id, Name, wfstepId)
        _doctypeId = docTypeId
        _indices = indices
        _addcWf = addcWF
        _filePath = filePath
        _showDocument = showDocument
        _continueWithCurrentTasks = continuewithcurrenttasks
        _dontOpenTaskAfterInsert = dontOpenTaskAfterInsert
        _autocompleteIndexsInCommon = autocompleteIndexsInCommon
        _specificWorkflowId = specificWorkflowId
        Me.OpenMode = OpenMode
        Me.playRule = New Zamba.WFExecution.PlayDOGenerateTaskResult(Me)
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
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.ExecuteEntryInWFDoGenerateTaskResult
            results = playRule.PlayWeb(results, Params)
            Return results
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function
End Class