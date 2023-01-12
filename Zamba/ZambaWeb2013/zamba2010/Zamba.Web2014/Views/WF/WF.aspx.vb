Imports Zamba.Services
Imports Zamba.Core

Partial Public Class _Main

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.Session("UserId") Is Nothing = False Then

            AddHandler Arbol.SelectedNodeChanged, AddressOf SelectedNodeChanged
            AddHandler Arbol.WFTreeRefreshed, AddressOf RefreshTaskGrid
            RemoveHandler Arbol.WFTreeIsEmpty, AddressOf WfTreeIsEmpty
            AddHandler Arbol.WFTreeIsEmpty, AddressOf WfTreeIsEmpty

            If Not Page.IsPostBack Then
                Arbol.FillWF()
            Else
                Arbol.Refresh()
            End If
        End If
        Me.Title = "Zamba - Tareas"
    End Sub

    Private Sub SelectedNodeChanged(ByVal WFId As Int32, ByVal StepId As Int32, ByVal DocTypeId As Int32)
        If Page.IsPostBack Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "TaskCount", "$(document).ready(function () { LoadStepsCounts(); });", True)
        End If

        Dim zoptb As New ZOptBusiness()
        Dim CurrentTheme As String = zoptb.GetValue("CurrentTheme")
        zoptb = Nothing

        If CurrentTheme = "AysaDiseno" Then
            TaskGrid.ClearCurrentFilters(StepId)
            TaskGrid.SetFilters(StepId)
        End If

        TaskGrid.LoadTasks(WFId, StepId, DocTypeId, Arbol.WFTreeView.SelectedNode)
    End Sub

    ''' <summary>
    ''' Handler para una vez finalizado el refresco de wf, refrescar la grilla
    ''' </summary>
    ''' <param name="StepId"></param>
    ''' <remarks></remarks>
    Private Sub RefreshTaskGrid(ByVal StepId As Int32)
        Dim zoptb As New ZOptBusiness()
        Dim CurrentTheme As String = zoptb.GetValue("CurrentTheme")
        zoptb = Nothing

        If CurrentTheme = "AysaDiseno" Then
            TaskGrid.ClearCurrentFilters(StepId)
            TaskGrid.SetFilters(StepId)
        End If

        TaskGrid.RebindGrid()
    End Sub
    
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If Page.Request.QueryString.Count > 0 AndAlso Not String.IsNullOrEmpty(Page.Request.QueryString("docid")) Then
            Dim docid As String = Page.Request.QueryString("docid")

            Dim _STask As New STasks
            Dim _task As ITaskResult = _STask.GetTaskByDocId(Int64.Parse(docid))
            Dim zopt As New ZOptBusiness
            Dim weblink As String = zopt.GetValue("WebViewPath")
            zopt = Nothing
            If _task IsNot Nothing AndAlso Not String.IsNullOrEmpty(weblink) Then
                Dim script As String = "parent.CreateTaskIframe('" & weblink & "/views/WF/TaskSelector.ashx?doctype=" & _task.DocTypeId & "&docid=" & _task.ID & "&taskid=" & _task.TaskId & "&wfstepid=" & _task.StepId & "'," & _task.TaskId & ",'" & _task.Name & "');"
                Page.ClientScript.RegisterStartupScript(Me.Page.GetType, "OpenTaskLink", script, True)
                _task = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para atrapar el evento que el arbol esta vacio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub WfTreeIsEmpty()
        UpdTaskGrid.Visible = False
        lblNoWFVisible.Visible = True
    End Sub
End Class


