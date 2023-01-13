Public Class PlayDoFillIndexDefault
    Private _myRule As IDoFillIndexDefault
    Private value As String
    Private IndexToModify As List(Of Int64)

    Sub New(ByVal rule As IDoFillIndexDefault)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            IndexToModify = New List(Of Int64)
            IndexToModify.Add(_myRule.IndexID)

            value = String.Empty

            If String.Compare(_myRule.TEXTODEFAULT, "FECHA Y HORA ACTUAL") = 0 Then
                value = Now
            ElseIf String.Compare(_myRule.TEXTODEFAULT, "USUARIO ACTUAL") = 0 Then
                value = Membership.MembershipHelper.CurrentUser.Name
            ElseIf String.Compare(_myRule.TEXTODEFAULT, "USUARIO WINDOWS") = 0 Then
                value = Environment.UserName
            ElseIf String.Compare(_myRule.TEXTODEFAULT, "NOMBRE DE PC") = 0 Then
                value = Environment.MachineName
            End If

            For Each taskResult As Core.TaskResult In results
                For i As Int64 = 0 To taskResult.Indexs.Count - 1
                    If _myRule.IndexID = taskResult.Indexs(i).ID Then
                        taskResult.Indexs(i).Data = value
                        taskResult.Indexs(i).DataTemp = value
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Completando atributos...")
                        Dim rstBuss As New Results_Business()
                        rstBuss.SaveModifiedIndexData(DirectCast(taskResult, Result), True, True, IndexToModify)
                        rstBuss = Nothing
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Atributo completado con éxito!")
                        Exit For
                    End If
                Next
                UserBusiness.Rights.SaveAction(taskResult.ID, ObjectTypes.WFTask, RightsType.ExecuteRule, _myRule.Name)
            Next
        Finally
            value = Nothing
            If Not IsNothing(IndexToModify) Then
                IndexToModify.Clear()
                IndexToModify = Nothing
            End If
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class