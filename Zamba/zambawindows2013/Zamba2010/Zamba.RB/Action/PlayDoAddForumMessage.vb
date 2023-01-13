Public Class PlayDoAddForumMessage

    Private myRule As IDoAddForumMessage

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try

            For Each Result As Result In results
                 'Trace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & Result.Name)
                'Todo alejandro: usar un evento
                'Dim forum As New Zamba.Controls.UcForo(Membership.MembershipHelper.CurrentUser)
                'forum.ShowInfo(Result.ID, Result.DocType.ID)
                'Dim frm As New Windows.Forms.Form
                'forum.Dock = Windows.Forms.DockStyle.Fill
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

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoAddForumMessage)
        Me.myRule = rule
    End Sub
End Class
