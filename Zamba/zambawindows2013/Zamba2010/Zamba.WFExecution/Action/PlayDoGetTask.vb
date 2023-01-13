Imports Zamba.Core.WF.WF

Public Class PlayDoGetTask

    Private myRule As IDoGetTask

    Sub New(ByVal rule As IDoGetTask)
        myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)

        Dim TaskId As String
        Dim WFTB As New WFTaskBusiness
        For Each t As Core.TaskResult In results

            TaskId = TextoInteligente.ReconocerCodigo(myRule.TaskIdVariable, t)
            TaskId = WFRuleParent.ReconocerVariablesValuesSoloTexto(TaskId)

            Dim task As ITaskResult = WFTB.GetTask(TaskId)

            If Not IsNothing(task) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Tarea obtenida " & task.Name & " (" & task.ID & ")")
                NewList.Add(task)
            End If
        Next
        WFTB = Nothing
        Return NewList
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
