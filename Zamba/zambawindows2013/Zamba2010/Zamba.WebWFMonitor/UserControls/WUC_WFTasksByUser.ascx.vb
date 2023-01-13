Imports Zamba.Core
Imports Zamba.WFBussines

Partial Class WUC_WFTasks
    Inherits System.Web.UI.UserControl

    ''' <summary>
    ''' DEVUELVE UN ARRAY CON LOS TASKS SELECCIONADOS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectedTasks() As ArrayList
        Dim Arr As New ArrayList
        Dim i As Integer = 0
        While i < GridView1.Rows.Count
            Dim row As GridViewRow = GridView1.Rows(i)
            Dim isChecked As Boolean = CType(row.FindControl("CheckBox1"), CheckBox).Checked
            If isChecked Then
                Dim TaskId As Int32 = CInt(Val(Me.GridView1.DataKeys(i).Value))
                Arr.Add(Zamba.WFBusiness.WFTaskBussines.GetTaskByTaskId(TaskId, Session.Item("WfId")))
            End If
            System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        Return Arr
    End Function

    ''' <summary>
    ''' DEVUELVE UN ARRAY CON LOS IDS DE LOS TASKS SELECCIONADOS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectedIds() As ArrayList
        Dim Arr As New ArrayList
        Dim i As Integer = 0
        While i < GridView1.Rows.Count
            Dim row As GridViewRow = GridView1.Rows(i)
            Dim isChecked As Boolean = CType(row.FindControl("CheckBox1"), CheckBox).Checked
            If isChecked Then
                Dim TaskId As Int32 = CInt(Val(Me.GridView1.DataKeys(i).Value))
                Arr.Add(TaskId)
            End If
            System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        Return Arr
    End Function

    Public Event ShowTaskDetails(ByVal TaskId As Int32)

    ''' <summary>
    ''' HACE UN RAISEEVENT PARA MOSTRAR LOS DETALLES DE LA TAREA CLICKEADA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Select Case e.CommandName

            Case "ShowDetails"
                Dim TaskId As Int32 = CInt(Val(Me.GridView1.DataKeys(e.CommandArgument).Value))
                Session.Add("TaskId", TaskId)
                If TaskId <> 0 Then RaiseEvent ShowTaskDetails(TaskId)
        End Select
    End Sub

    ''' <summary>
    ''' MODIFICA UNA VARIABLE EN EL SESSION A LA CUAL ESTA ENLAZADO EL DATASOURCE DEL COMBO DE USUARIOS, PARA ASI ACTUALIZARLO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DropDownList4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList4.SelectedIndexChanged
        Session.Add("Filter4", Me.DropDownList4.SelectedValue)
    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim C(0) As String
        C(0) = "Id"
        Me.GridView1.DataKeyNames = C
    End Sub

End Class
