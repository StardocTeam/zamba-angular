Imports Zamba.Core
Imports Zamba.WFBusiness
Imports Zamba.WFBusiness.WFBusiness
Imports System.Drawing.Design
Imports System.Data
Imports System.Net.Mail
Imports System.Web
Imports Zamba.Data
Imports System.Windows
Imports System.Collections.Generic

Partial Class _Monitor
    Inherits UI.Page

    Protected WithEvents MyMaster As MasterPage
    ''' <summary>
    ''' Propiedad que devuelve un string con los IDs de las tareas seleccionadas para guardarlas en el Session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedTasks() As String
        Get
            Dim StrBuilder As New StringBuilder()
            For Each id As Int32 In UCTareas.GetSelectedIds()
                StrBuilder.Append(id.ToString())
                StrBuilder.Append("*")
            Next
            If StrBuilder.Length > 0 Then
                StrBuilder = StrBuilder.Remove(StrBuilder.Length - 1, 1)
            End If
            Return StrBuilder.ToString()
        End Get
    End Property

    ''' <summary>
    ''' Actualiza la lista de tareas al cambiar la etapa seleccionada
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub WUC_WFSteps1_WFStepSelected() Handles WUC_WFSteps1.LoadTasks
        Try
            UCTareas.SetDataSource()
        Catch ex As Exception
            RaiseError(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Chequea si el usuario logueado es el pasado como parametro
    ''' </summary>
    ''' <param name="U">usuario</param>
    ''' <returns>buleano</returns>
    ''' <remarks>FORMA PARTE DEL CONTROL DE LOGIN</remarks>
    Private Function ComprobarUsuario(ByVal U As Usuario) As Boolean
        Return U.Nombre = Session("Usuario")
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Header.Title = "Control de Workflows"

        If Not Page.IsPostBack Then
            Try
                MyMaster = Master
                ValidateUser()
            Catch ex As Exception
                RaiseError(ex)
            End Try
        End If
    End Sub

    Private Sub ValidateUser()
        If IsNothing(Session("Usuario")) Then
            Response.Redirect("Log.aspx")
        End If

        Dim UserList As Generic.List(Of Usuario) = Application("Usuarios")
        If IsNothing(UserList) Then
            Response.Redirect("Log.aspx")
        End If
        If UserList.Exists(AddressOf ComprobarUsuario) = False Then
            Response.Redirect("Log.aspx")
        End If
    End Sub

    Protected Sub MyMaster_Refrescar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MyMaster.Refrescar_Click
        Try
            UCTareas.DataBind()
            WUC_WFSteps1.DataBind()
            WUC_WFList1.DataBind()
        Catch ex As Exception
            RaiseError(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Devuelve las tareas seleccionadas en la grilla utilizando el Session
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeleccionadas() As ArrayList
        If IsNothing(Session("SelectedTasks")) Then Return New ArrayList
        Dim A() As String = Session("SelectedTasks").ToString.Split("*")
        If A(0) = String.Empty Then Return Nothing
        Dim ArrayList As New ArrayList
        For Each Item As Int32 In A
            ArrayList.Add(Zamba.WFBusiness.WFTaskBussines.GetTaskByTaskId(Item, Session("WfId")))
        Next
        Return ArrayList
    End Function

    ''' <summary>
    ''' Carga el calendario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btCalendario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCalendario.Click
        Try
            Response.Redirect("WfCalendar.aspx")
        Catch ex As Exception
            RaiseError(ex)
        End Try
    End Sub

    Protected Sub btInformes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btInformes.Click
        Try
            Response.Redirect("Informes\Informes.aspx")
        Catch ex As Exception
            RaiseError(ex)
        End Try
    End Sub
End Class