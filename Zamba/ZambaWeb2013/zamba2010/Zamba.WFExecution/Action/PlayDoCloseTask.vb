Imports Zamba.Core

Public Class PlayDoCloseTask
    Private _myRule As IDoCloseTask

    Sub New(ByVal rule As IDoCloseTask)
        Me._myRule = rule
    End Sub
    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal myRule As IDoCloseTask) As System.Collections.Generic.List(Of ITaskResult)
        Return PlayWeb(results, Nothing)
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)

        If IsNothing(Params) Then
            Params = New Hashtable
        End If


        Dim taskId As Int64
        Dim strTaskID As String = Me._myRule.TaskId
        Try
            If String.IsNullOrEmpty(strTaskID) Then
                taskId = results(0).TaskId
            Else
                If Not IsNothing(results) AndAlso results.Count > 0 Then
                    strTaskID = TextoInteligente.ReconocerCodigo(strTaskID, results(0))
                End If

                If IsNumeric(strTaskID) Then
                    taskId = Int64.Parse(strTaskID)
                Else
                    Dim VarInterReglas As New VariablesInterReglas()
                    taskId = VarInterReglas.ReconocerVariables(strTaskID).Trim
                    VarInterReglas = Nothing
                End If
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "TaskId de la tarea a cerrar: " & taskId.ToString)
            Params.Add("TaskID", taskId)
            Params.Add("DoCloseTask", True)
            Params.Add("ParentAction", Me._myRule.ParentAction)
        Finally
        End Try
    End Function

    Public Function PlayWebSecondExecution(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable, ByVal myRule As IDoCloseTask) As System.Collections.Generic.List(Of Core.ITaskResult)
        Params.Clear()
        Return results
    End Function
End Class
