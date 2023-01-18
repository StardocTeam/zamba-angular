Public Class PlayDoCloseTask
    Private myRule As IDoCloseTask
    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim taskId As Int64
        Dim strTaskID As String = myRule.TaskId
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
            Dim params As New Hashtable
            params.Add("TaskID", taskId)
            params.Add("DoCloseTask", True)
            ZambaCore.HandleRuleModule(ResultActions.CloseTask, results, params)
            Return results
        Finally

        End Try
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoCloseTask)
        myRule = rule
    End Sub
End Class
