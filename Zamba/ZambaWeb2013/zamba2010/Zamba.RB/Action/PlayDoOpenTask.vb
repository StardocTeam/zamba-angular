Public Class PlayDoOpenTask
    Private _myRule As IDOOpenTask
    Public Shared Event AddedTask(ByVal Results As Generic.List(Of ITaskResult), ByVal OpenTaskAfterInsert As Boolean)

    Sub New(ByVal rule As IDOOpenTask)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim params As New Hashtable
        Dim TaskID As String = Nothing
        Dim DocID As String = Nothing
        Dim taskResult As Zamba.Core.ITaskResult
        Dim TaskIds As System.Collections.Generic.List(Of Long)

        Try
            If _myRule.UseCurrentTask Then
                'se toma el TaskID del actual result
                If results(0).TaskId > 0 Then
                    params.Add("UseCurrentTask", True)
                    taskResult = results(0)
                    WF.WF.WFTaskBusiness.OpenTask(taskResult, params)
                Else
                    ZambaCore.HandleModule(ResultActions.MostrarResult, results(0), Nothing)
                End If
                
            ElseIf _myRule.TaskID <> String.Empty Then
                'se toma el TaskID ingresado y se abre
                TaskID = _myRule.TaskID
                If TaskID.Contains("zvar") = True Then
                    TaskID = WFRuleParent.ReconocerVariablesValuesSoloTexto(TaskID)
                End If
                params.Add("TaskID", TaskID)
                WF.WF.WFTaskBusiness.OpenTask(taskResult, params)
            ElseIf _myRule.DocID <> String.Empty Then
                DocID = _myRule.DocID
                If DocID.Contains("zvar") = True Then
                    DocID = WFRuleParent.ReconocerVariablesValuesSoloTexto(DocID)
                End If
                params.Add("DocIDs", DocID)
                WF.WF.WFTaskBusiness.OpenTask(taskResult, params)
            End If

        Catch ex As System.UriFormatException
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class