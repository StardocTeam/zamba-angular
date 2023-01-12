Imports Zamba.Core

''' <summary>
''' Regla que permite cerrar tareas.
''' </summary>
''' <remarks></remarks>
<RuleMainCategory("Interfaz"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Ejecutar Script"), RuleHelp("Ejecuta un Script en la interfaz de usuario"), RuleFeatures(False)> <Serializable()>
Public Class DoExecuteScript
    Inherits WFRuleParent
    Implements IDoExecuteScript, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDoExecuteScript

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal Script As String)
        MyBase.New(Id, Name, wfstepId)
        Me.Script = Script
        playRule = New WFExecution.PlayDoExecuteScript(Me)
    End Sub

    Public Overrides Sub Dispose()
        Me.Script = Nothing
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

    Public Property Script() As String Implements Core.IDoExecuteScript.Script

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
End Class
