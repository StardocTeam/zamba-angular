Imports Zamba.Core.WF.WF

Public Class PlayDoOpenTask
    Private _myRule As IDOOpenTask
    Public Shared Event AddedTask(ByVal Results As Generic.List(Of ITaskResult), ByVal OpenTaskAfterInsert As Boolean)

    Sub New(ByVal rule As IDOOpenTask)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return PlayWeb(results, Nothing)
    End Function
    '04/07/11: Sumada la funcionalidad del la regla DoOpenTask
    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByRef params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim TaskID As String = Nothing
        Dim taskResult As Zamba.Core.ITaskResult
        Dim TaskIds As System.Collections.Generic.List(Of Long)

        Try

            If _myRule.UseCurrentTask Then
                'tomar los datos del result
                params.Add("DocID", results(0).ID)
                params.Add("DocTypeId", results(0).DocTypeId)
            ElseIf _myRule.TaskID <> String.Empty Then
                'se toma el TaskID ingresado, se lo transforma en taskresult y se obtiene los datos
                TaskID = _myRule.TaskID
                If TaskID.Contains("zvar") = True Then
                    Dim VarInterReglas As New VariablesInterReglas()
                    TaskID = VarInterReglas.ReconocerVariablesValuesSoloTexto(TaskID)
                    VarInterReglas = Nothing
                End If
                Dim WFTB As New WF.WF.WFTaskBusiness
                taskResult = WFTB.GetTask(TaskID, Zamba.Membership.MembershipHelper.CurrentUser.ID)
                WFTB = Nothing
                params.Add("DocID", taskResult.ID)
                params.Add("DocTypeId", taskResult.DocTypeId)
            End If
            params.Add("OpenMode", _myRule.OpenMode)
        Catch ex As System.UriFormatException
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            TaskID = Nothing
            taskResult = Nothing
            TaskIds = Nothing
        End Try

        Return results
    End Function
End Class