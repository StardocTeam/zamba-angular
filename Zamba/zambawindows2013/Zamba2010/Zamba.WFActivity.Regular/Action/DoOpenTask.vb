Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Abrir Tarea"), RuleHelp("Permite abrir una tarea"), RuleFeatures(True)> _
<Serializable()> Public Class DoOpenTask
    Inherits WFRuleParent
    Implements IDOOpenTask, IRuleValidate
    Private playRule As Zamba.WFExecution.PlayDoOpenTask
    Public Overrides ReadOnly Property IsFull() As Boolean
    Public Overrides ReadOnly Property IsLoaded() As Boolean
    Public Property IsValid As Boolean Implements IRuleValidate.isValid

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub

#Region "campos privados"
#End Region
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal TaskID As String, ByVal DocID As String, ByVal UseCurrentTask As Boolean, ByVal OpenMode As Int16)
        MyBase.New(Id, Name, wfstepid)
        Me.TaskID = TaskID
        Me.DocID = DocID
        Me.UseCurrentTask = UseCurrentTask
        Me.OpenMode = OpenMode
        playRule = New Zamba.WFExecution.PlayDoOpenTask(Me)
    End Sub
    Public Property TaskID() As String Implements IDOOpenTask.TaskID
    Public Property DocID() As String Implements IDOOpenTask.DocID
    Public Property OpenMode() As Int32 Implements IDOOpenTask.OpenMode
    Public Property UseCurrentTask() As Boolean Implements IDOOpenTask.UseCurrentTask

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

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
