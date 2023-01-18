Imports Zamba.Core
Imports Zamba.WFBussines
Imports System.Web
Partial Class WUC_WFSteps
    Inherits UI.UserControl

    Public DsSteps As New DsSteps
    Public Event LoadTasks()

    ''' <summary>
    ''' Selecciona una etapa de acuerdo a su id
    ''' </summary>
    ''' <param name="StepId"></param>
    ''' <remarks></remarks>
    Public Sub SelectStep(ByVal StepId As Int32)
        lstWF.SelectedValue = StepId
    End Sub
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'Try
    '    If Not IsPostBack Then LoadWfSeps()
    'Catch ex As Exception
    '    RaiseError(ex)
    'End Try
    'End Sub

    'Private Sub LoadWfSeps()
    '    DsSteps = Zamba.WFBussines.WFStepBussines.GetDsAllSteps()
    '    Me.lstWF.DataSource = DsSteps.WFSteps
    '    Me.lstWF.DataTextField = "Name"
    '    Me.lstWF.DataValueField = "Step_id"
    '    Me.lstWF.DataBind()
    'End Sub
    Protected Sub WFList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstWF.SelectedIndexChanged
        If String.IsNullOrEmpty(lstWF.SelectedValue) = False Then
            Try
                Dim StepID As Integer = lstWF.SelectedValue
                Dim StepName As String = lstWF.SelectedItem.Text
                Session.Add("StepId", lstWF.SelectedValue)
                RaiseEvent LoadTasks()
            Catch ex As Exception
                RaiseError(ex)
            End Try
        End If
    End Sub

End Class
'Public Sub SetWfId(ByVal WfId As Int32)
'    DsSteps = Zamba.WFBussines.WFStepBussines.GetDsSteps(WfId)
'    Me.lstWF.DataSource = DsSteps.WFSteps
'    Me.lstWF.DataTextField = "Name"
'    Me.lstWF.DataValueField = "Step_id"
'    Me.lstWF.DataBind()
'End Sub