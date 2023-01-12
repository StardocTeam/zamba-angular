Imports System.Data
Imports Zamba.Core
Imports Zamba.Services

Partial Public Class TaskGrid

    Inherits System.Web.UI.UserControl

    Public Sub LoadTasks(ByVal WFId As Int32, ByVal StepId As Int32, ByVal DocTypeId As Int32, ByVal Node As TreeNode)
        Dim sRights As New SRights

        If Not Node Is Nothing AndAlso sRights.GetUserRights(ObjectTypes.WFSteps, RightsType.Use, StepId) Then
            If WFId = 0 Then
                If Not String.IsNullOrEmpty(ViewState("WFId")) Then
                    WFId = ViewState("WFId")
                End If
            End If

            If StepId = 0 Then
                If Not String.IsNullOrEmpty(ViewState("StepId")) Then
                    StepId = ViewState("StepId")
                End If
            End If

            If DocTypeId = 0 Then
                If Not String.IsNullOrEmpty(ViewState("DocTypeId")) Then
                    DocTypeId = ViewState("DocTypeId")
                End If
            End If

            'Variables de estado solo usadas en la grilla de tareas
            ViewState("WFId") = WFId
            ViewState("StepId") = StepId
            ViewState("DocTypeId") = DocTypeId

            'Variables de sesion usadas en el taskviewer (si se abre una tarea)
            Session("WFId") = WFId
            Session("StepId") = StepId
            Session("DocTypeId") = DocTypeId


            UserPreferences.setValue("UltimoWFStepUtilizado", StepId, Zamba.Core.Sections.WorkFlow)
            ucTaskGrid.Visible = True
            lblMsg.Visible = False

            'ucTaskGrid.loadTasks(StepId, Node, ucTaskGridFilter.FC)
            Dim zopt As New ZOptBusiness
            Dim showAysaFilter As String = zopt.GetValue("ShowAysaGridFilter")
            Dim showMarshFilter As String = zopt.GetValue("ShowMarshGridFilter")
            zopt = Nothing
            If Not String.IsNullOrEmpty(showAysaFilter) AndAlso Boolean.Parse(showAysaFilter) = True Then
                If (ucTaskGridFilter IsNot Nothing) Then ucTaskGridFilter.Visible = True
            ElseIf Not String.IsNullOrEmpty(showAysaFilter) AndAlso Boolean.Parse(showAysaFilter) = True Then
                If (ucTaskGridFilter IsNot Nothing) Then ucTaskGridFilter.Visible = True
            Else
                If (ucTaskGridFilter IsNot Nothing) Then ucTaskGridFilter.Visible = False
            End If
        Else
            ucTaskGrid.Visible = False
            If (ucTaskGridFilter IsNot Nothing) Then ucTaskGridFilter.Visible = False
            lblMsg.Visible = True
        End If
    End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    'AddHandler Grid.DocTypeSelected, AddressOf DocTypeSelected
    '    'Me.Grid.GridWidth = "650"
    'End Sub

    'Private Sub DocTypeSelected(ByVal DocTypeId As Long)
    '    'ViewState("DocTypeId") = String.Empty
    '    'LoadTasks(Session("WFId"), Session("StepId"), DocTypeId)
    '    'cuando se llame a esta funcion (DocTypeSelected) se debe usar el request en lugar de la llamada a "session"
    '    'LoadTasks(Request("WFid")...
    'End Sub

    Protected Sub ucTaskGridFilter_OnFilterCall(ByVal sender As Object, ByVal e As EventArgs) Handles ucTaskGridFilter.OnFilterCall
        ucTaskGrid.ApplyFilter()
    End Sub

    Protected Sub ucTaskGridFilterMarsh_OnFilterCall(ByVal sender As Object, ByVal e As EventArgs) Handles ucTaskGridFilterMarsh.OnFilterCall
        ucTaskGrid.ApplyFilter()
    End Sub

    Public Sub ClearCurrentFilters(ByVal StepId As Long)
        If (ucTaskGridFilter IsNot Nothing) Then ucTaskGridFilter.ClearCurrentFilters(StepId)
        If (ucTaskGridFilterMarsh IsNot Nothing) Then ucTaskGridFilterMarsh.ClearCurrentFilters(StepId)
    End Sub

    Public Sub SetFilters(ByVal StepId As Long)
        If (ucTaskGridFilter IsNot Nothing) Then ucTaskGridFilter.SetFilters(StepId)
        If (ucTaskGridFilterMarsh IsNot Nothing) Then ucTaskGridFilterMarsh.SetFilters(StepId)
    End Sub

    ''' <summary>
    ''' Realiza el rebindeo de la grilla de tareas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RebindGrid()
        ucTaskGrid.BindZGrid(True)
    End Sub
End Class