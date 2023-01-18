Imports Zamba.Core
Imports Zamba.Membership

Public Class PlayDoFillIndexDefault

    Private _myRule As IDoFillIndexDefault
    Private _index As Index
    Private value As String
    Private IndexToModify As List(Of Int64)

    Sub New(ByVal rule As IDoFillIndexDefault)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim UB As New UserBusiness
        Try
            Me.IndexToModify = New List(Of Int64)
            Me._index = Me._myRule.Index
            value = String.Empty

            If String.Compare(Me._myRule.TEXTODEFAULT, "FECHA Y HORA ACTUAL") = 0 Then
                Me.value = Now
            ElseIf String.Compare(Me._myRule.TEXTODEFAULT, "USUARIO ACTUAL") = 0 Then
                Me.value = Zamba.Membership.MembershipHelper.CurrentUser.Name
            ElseIf String.Compare(Me._myRule.TEXTODEFAULT, "USUARIO WINDOWS") = 0 Then
                Me.value = Environment.UserName
            ElseIf String.Compare(Me._myRule.TEXTODEFAULT, "NOMBRE DE PC") = 0 Then
                Me.value = Environment.MachineName
            End If

            '_index.Data = value
            Me.IndexToModify.Clear()
            Me.IndexToModify.Add(Me._index.ID)
            Dim Results_Business As New Results_Business
            For Each taskResult As Core.TaskResult In results
                Dim dtModifiedIndex As New DataTable
                dtModifiedIndex.Columns.Add("ID", GetType(Int64))
                dtModifiedIndex.Columns.Add("OldValue", GetType(String))
                dtModifiedIndex.Columns.Add("NewValue", GetType(String))

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & taskResult.Name)
                For Each index As Index In taskResult.Indexs
                    If Me._index.ID = index.ID Then
                        Dim row As DataRow = dtModifiedIndex.NewRow()
                        row("ID") = index.ID
                        row("OldValue") = index.Data
                        row("NewValue") = Me.value
                        dtModifiedIndex.Rows.Add(row)

                        index.Data = Me.value
                        index.DataTemp = Me.value
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Completando atributos...")
                        Results_Business.SaveModifiedIndexData(DirectCast(taskResult, Result), True, True, Me.IndexToModify, dtModifiedIndex)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Atributos completados con éxito!")
                        Exit For
                    End If
                Next
                UB.SaveAction(taskResult.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.Name)
            Next
        Finally
            UB = Nothing
            Me._index = Nothing
            Me.value = Nothing
            Me.IndexToModify = Nothing
        End Try
        Return results
    End Function
End Class
