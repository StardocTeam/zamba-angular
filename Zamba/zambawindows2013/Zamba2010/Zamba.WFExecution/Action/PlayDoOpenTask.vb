Imports Zamba.Core.WF.WF

Public Class PlayDoOpenTask
    Private _myRule As IDOOpenTask
    Public Shared Event AddedTask(ByVal Results As Generic.List(Of ITaskResult), ByVal OpenTaskAfterInsert As Boolean)

    Sub New(ByVal rule As IDOOpenTask)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim params As New Hashtable
        Dim TaskID As String = Nothing
        Dim DocID As String = Nothing
        Dim taskResult As Zamba.Core.ITaskResult
        Dim TaskIds As System.Collections.Generic.List(Of Long)
        Dim WFTB As New WFTaskBusiness
        Try
            If _myRule.UseCurrentTask Then
                'se toma el TaskID del actual result
                If results(0).TaskId > 0 Then
                    params.Add("UseCurrentTask", True)
                    taskResult = results(0)
                    WFTB.OpenTask(taskResult, params)
                Else
                    ZambaCore.HandleModule(ResultActions.MostrarResultEnTareaActiva, results(0), Nothing)
                End If

            ElseIf _myRule.TaskID <> String.Empty Then
                'se toma el TaskID ingresado y se abre
                TaskID = _myRule.TaskID.Trim
                If TaskID.Contains("zvar") = True Then
                    TaskID = WFRuleParent.ReconocerVariablesValuesSoloTexto(TaskID)
                End If
                taskResult = results(0)
                TaskID = TextoInteligente.ReconocerCodigo(TaskID, taskResult)
                params.Add("TaskID", TaskID)
                WFTB.OpenTask(taskResult, params)
            ElseIf _myRule.DocID <> String.Empty Then
                DocID = _myRule.DocID.Trim
                If DocID.Contains("zvar") = True Then
                    DocID = WFRuleParent.ReconocerVariablesValuesSoloTexto(DocID)
                End If
                taskResult = results(0)
                DocID = TextoInteligente.ReconocerCodigo(DocID, taskResult)
                params.Add("DocIDs", DocID)
                WFTB.OpenTask(taskResult, params)
            End If

        Catch ex As System.UriFormatException
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            WFTB = Nothing
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class