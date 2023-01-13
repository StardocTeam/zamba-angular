Public Class PlayDoAddForumMessage
    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal rule As IDoAddForumMessage) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try

            For Each Result As Result In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "La función de la regla " & rule.Name & " no se encuentra implementada actualmente.")
                'ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & Result.Name)
                'Todo alejandro: usar un evento
                'Dim forum As New Zamba.Controls.UcForo(Zamba.Membership.MembershipHelper.CurrentUser)
                'forum.ShowInfo(Result.ID, Result.DocType.ID)
                'Dim frm As NewForm
                'forum.Dock =DockStyle.Fill
                'frm.Controls.Add(forum)
                'frm.Width = "500"
                'frm.Height = "300"
                'forum.AjustaSplitter()
                'frm.ShowDialog()
            Next
        Finally
        End Try
        Return results
    End Function
End Class
