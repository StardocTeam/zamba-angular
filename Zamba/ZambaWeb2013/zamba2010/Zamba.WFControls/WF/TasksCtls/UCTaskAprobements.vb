Imports Zamba.Core.WF.WF
Imports Zamba.Filters
Imports Zamba.Core

Namespace WF.TasksCtls
    Public Class UCTaskAprobements
        Inherits Zamba.AppBlock.ZControl

        Dim TaskId As Int64


        Public Sub New(ByVal TaskId As Integer, ByVal CurrentUserId As Int64)
            Me.New(CurrentUserId)
            Me.TaskId = TaskId
        End Sub

        ''' <summary>
        ''' Carga el historial de una tarea en grdGeneral
        ''' </summary>
        ''' <param name="resultId"></param>
        ''' <remarks></remarks>
        Public Sub LoadHistory(ByVal TaskId As Int64)
            Try
                'grdGeneral.PreferredColumnWidth = 100
                'TODO: El dataset puede venir cargado con datos de la tabla ZAPROB  o REQUESTACTION(Nueva) Segun preferencia
                Dim ds As DataSet = WFTaskBusiness.GetTaskAprobementsHistory(TaskId)
                grdGeneral.DataSource = ds.Tables(0)
                grdGeneral.FixColumns()
                grdGeneral.Update()
                grdGeneral.Refresh()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub UCTaskHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                grdGeneral.ShortDateFormat = Boolean.Parse(UserPreferences.getValue("GridShortDateFormat", Sections.UserPreferences, "True"))
                grdGeneral.AlwaysFit = True
                grdGeneral.UseZamba = False
                grdGeneral.WithExcel = True
                LoadHistory(TaskId)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub tsbActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbActualizar.Click
            Try
                LoadHistory(TaskId)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub
    End Class
End Namespace