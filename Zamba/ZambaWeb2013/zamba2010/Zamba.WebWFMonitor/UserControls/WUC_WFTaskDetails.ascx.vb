Imports Zamba.Core
Imports Zamba.WFBussines

Partial Class WUC_WFTaskDetails
    Inherits System.Web.UI.UserControl

    Public Sub ShowTaskDetails(ByVal TaskId As Int32, ByVal WfId As Int32)
        Try
            Session.Add("TaskId", TaskId)
            Session.Add("WfId", WfId)
            Me.FormView1.DataBind()
        Catch ex As Exception
            RaiseError(ex)
        End Try
    End Sub

End Class
