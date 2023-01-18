Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Obtener Tarea"), RuleHelp("Obtiene un listado de todas las tareas seleccionadas."), RuleFeatures(False)> <Serializable()> _
Public Class DoGetTask
    Inherits WFRuleParent
    Implements IDoGetTask, IRuleValidate

    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoGetTask

    Public Overrides Sub Dispose()

    End Sub

    Private _isLoaded As Boolean
    Private _isFull As Boolean
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

    Private _TaskIdVariable As String
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal TaskIdVariable As String)
        MyBase.New(Id, Name, wfstepid)
        _TaskIdVariable = TaskIdVariable

        playRule = New WFExecution.PlayDoGetTask(Me)
    End Sub


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

    Public Property TaskIdVariable() As String Implements Core.IDoGetTask.TaskIdVariable
        Get
            Return _TaskIdVariable
        End Get
        Set(ByVal value As String)
            _TaskIdVariable = value
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
End Class