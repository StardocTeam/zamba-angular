Imports Zamba.Core
Imports Zamba.WFBussines
Imports System.Web.UI
Partial Class WUC_WFList
    Inherits UserControl
    ''' <summary>
    ''' Selecciona un Workflow de acuerdo a su Id
    ''' </summary>
    ''' <param name="WfId"></param>
    ''' <remarks></remarks>
    Public Sub SelectWf(ByVal WfId As Int32)
        lstWorkFlow.SelectedValue = WfId
        Session.Add("WfId", WfId)
    End Sub

    Protected Sub WFList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstWorkFlow.SelectedIndexChanged
        Session.Add("WfId", lstWorkFlow.SelectedValue)

    End Sub
End Class