Imports Zamba.Core

''' <summary>
''' Regla que permite cerrar tareas.
''' </summary>
''' <remarks></remarks>
<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Cerrar tarea"), RuleHelp("Permite cerrar tareas pasandole el TaskId"), RuleFeatures(False)> <Serializable()>
Public Class DoCloseTask
    Inherits WFRuleParent
    Implements IDoCloseTask, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDoCloseTask

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal taskId As String, ByVal ParentAction As Int16)
        MyBase.New(Id, Name, wfstepId)
        Me.TaskId = taskId
        Me.ParentAction = ParentAction
        playRule = New WFExecution.PlayDoCloseTask(Me)
    End Sub

    Public Overrides Sub Dispose()
        Me.TaskId = Nothing
    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean

    Public Overrides ReadOnly Property IsLoaded() As Boolean

    Public Property IsValid As Boolean Implements IRuleValidate.isValid

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

    Public Property TaskId() As String Implements Core.IDoCloseTask.TaskId


    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Property ParentAction As Int32 Implements IDoCloseTask.ParentAction

End Class
