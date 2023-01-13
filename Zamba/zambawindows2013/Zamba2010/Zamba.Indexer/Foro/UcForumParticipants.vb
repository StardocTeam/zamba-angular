Imports Zamba.Core

''' <summary>
''' Clase encargada de mostrar, agregar y quitar participantes de una conversación.
''' </summary>
''' <remarks></remarks>
''' <history>
'''     Tomas   19/05/2010  Created
''' </history>
Public Class UcForumParticipants
    Private lastMessageId As Int32
    Private dtParticipants As DataTable
    Private docId As Int64
    Private _messageID As Int32
    Private taskId As Int64
    Private title As String

    ''' <summary>
    ''' Carga los participantes de la conversación y la configuración del control.
    ''' </summary>
    ''' <param name="messageId"></param>
    ''' <param name="userId"></param>
    ''' <param name="docId"></param>
    ''' <remarks></remarks>
    Public Sub FillParticipants(ByVal messageId As Int64, ByVal userId As Int64, ByVal docId As Int64, ByVal docTypeId As Int64, ByVal forceRefresh As Boolean, ByVal taskId As Int64, ByVal title As String)
        'Carga los participantes si el mensaje pertenece a otra conversación.
        If lastMessageId <> messageId OrElse forceRefresh Then
            'Si el messageId es -1 es porque no hay seleccionados.
            If messageId <> -1 Then
                Try
                    'Cargo los datos de los participantes. 
                    LoadDataGridView(ZForoBusiness.GetFullParticipants(messageId))

                    'Si el usuario fué el creador de la conversación, se habilitan
                    'los botones de agregar o quitar participantes.
                    If userId = ZForoBusiness.GetCreatorId(messageId) OrElse UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Foro, RightsType.ChangeParticipants, docTypeId) Then
                        btnEdit.Enabled = True
                    Else
                        btnEdit.Enabled = False
                    End If

                    SetParentText(dgvParticipants.RowCount)

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    LoadErrorRow()
                Finally
                    _messageID = messageId
                    lastMessageId = messageId
                End Try

            Else
                LoadErrorRow()
            End If

            Me.docId = docId
            Me.taskId = taskId
            Me.title = title
        End If
    End Sub

    ''' <summary>
    ''' Limpia la grilla y carga un mensaje de error. Deshabilita el botón de edición de participantes.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadErrorRow()
        btnEdit.Enabled = False

        'Si existió un error crea una fila con un mensaje.
        If dtParticipants Is Nothing Then
            dtParticipants = New DataTable()
        Else
            If dtParticipants.Columns.Contains("Error") Then Exit Sub
        End If

        dtParticipants.Clear()
        dtParticipants.Columns.Clear()
        dtParticipants.Columns.Add("Error", GetType(String))
        dtParticipants.Rows.Add(New Object() {"Error al cargar los participantes de la conversación."})
        dgvParticipants.DataSource = dtParticipants

        SetParentText(0)
    End Sub

    ''' <summary>
    ''' Modifica el nombre de la solapa de participantes describiendo 
    ''' la cantidad de participantes de la conversación.
    ''' </summary>
    ''' <param name="cant">Cantidad de participantes de la conversación.</param>
    ''' <remarks></remarks>
    Private Sub SetParentText(ByVal cant As Int32)
        DirectCast(Parent, TabPage).Text = "Participantes (" + cant.ToString + ")"
    End Sub

    ''' <summary>
    ''' Edita los participantes de la conversación.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEdit.Click
        Dim docIds As New Generic.List(Of Int64)
        Dim userIds As New Generic.List(Of Int64)
        docIds.Add(docId)

        'Obtiene los ids de los participantes.
        Dim newFrmSelectUsers As frmSelectUsers = New frmSelectUsers(GroupToNotifyTypes.Foro, docId, _messageID, ZForoBusiness.GetUserAndGroupsParticipantsId(lastMessageId))
        'Dim newFrmSelectUsers As frmSelectUsers = New frmSelectUsers(GroupToNotifyTypes.Foro, docId, )
        newFrmSelectUsers.StartPosition = FormStartPosition.CenterParent

        If newFrmSelectUsers.ShowDialog() = DialogResult.OK Then

            Dim dtParticipants As DataTable = ZForoBusiness.GetFullParticipants(_messageID)
            For Each p As DataRow In dtParticipants.Rows
                userIds.Add(Int64.Parse(p.Item(0)))
            Next
            'userIds = Zamba.Core.NotifyBusiness.getAllSelectedUsers(GroupToNotifyTypes.Foro, docIds)

            'Si se quito el mismo, se vuelve a agregar. El creador 
            'siempre debe participar de la conversación.
            If Not userIds.Contains(Membership.MembershipHelper.CurrentUser.ID) Then
                userIds.Add(Membership.MembershipHelper.CurrentUser.ID)
                ZForoBusiness.InsertMessageParticipants(_messageID, userIds)
            End If

            If HasChanges(userIds) Then
                'Agrega los participantes a la base.
                'ZForoBusiness.InsertMessageParticipants(lastMessageId, userIds)

                'Recarga la grilla
                LoadDataGridView(ZForoBusiness.GetFullParticipants(lastMessageId))

                'Refresco la solapa
                SetParentText(dgvParticipants.RowCount)
            End If
        End If

        newFrmSelectUsers.Dispose()
        newFrmSelectUsers = Nothing
        docIds = Nothing
        userIds = Nothing
    End Sub

    ''' <summary>
    ''' Guarda un log de lo ocurrido en el historial del usuario y de la tarea (en caso de poder hacerlo)
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Private Sub Log(ByVal message As String)
        'Guarda en el historial de tarea
        UserBusiness.Rights.SaveAction(_messageID, ObjectTypes.Foro, RightsType.ChangeParticipants, message)

        'Guarda en las tareas existentes
        If taskId > 0 Then
            WF.WF.WFTaskBusiness.LogAction(taskId, message)
        End If
    End Sub

    'Public Function AddOrRemoveParticipants()
    'End Function

    ''' <summary>
    ''' Carga los participantes en la grilla.
    ''' </summary>
    ''' <param name="dtParticipants">DataTable con los datos de los participantes.</param>
    ''' <remarks>El DataTable debe tener cargadas las columnas NAME, NOMBRES, APELLIDO y ID.</remarks>
    Private Sub LoadDataGridView(ByVal dtParticipants As DataTable)
        dgvParticipants.Controls.Clear()
        Me.dtParticipants = dtParticipants
        dtParticipants.Columns("NAME").ColumnName = "Usuario"
        dtParticipants.Columns("NOMBRES").ColumnName = "Nombre"
        dtParticipants.Columns("APELLIDO").ColumnName = "Apellido"
        dtParticipants.Columns("APELLIDO").SetOrdinal(2)
        'dgvParticipants.Rows.Clear()
        'dgvParticipants.Columns.Clear()
        dgvParticipants.DataSource = dtParticipants
        dgvParticipants.Columns("ID").Visible = False
    End Sub

    ''' <summary>
    ''' Verifica si se realizaron cambios en la selección de participantes.
    ''' </summary>
    ''' <param name="ids">Los ids de los participantes a verificar.</param>
    ''' <returns>True si existieron cambios.</returns>
    ''' <remarks></remarks>
    Private Function HasChanges(ByVal ids As Generic.List(Of Int64)) As Boolean
        Dim idCol As Int32 = dgvParticipants.Columns("ID").Index
        Dim userIds As New Generic.List(Of Int64)
        Dim i As Int32
        Dim added, removed As Boolean

        'Verifica si hay participantes quitados.
        For i = 0 To dgvParticipants.RowCount - 1
            'Se agrega a una lista auxiliar para mejorar la búsqueda de los agregados.
            userIds.Add(CLng(dgvParticipants.Rows(i).Cells(idCol).Value))

            If Not ids.Contains(userIds(i)) Then
                removed = True
            End If
        Next

        'Verifica si hay participantes que se agregaron.
        For i = 0 To ids.Count - 1
            If Not userIds.Contains(ids(i)) Then
                added = True
            End If
        Next

        If removed Then Log("Se han quitado participantes del mensaje de foro """ & title & """")
        If added Then Log("Se han agregado participantes al mensaje de foro """ & title & """")

        Return (added Or removed)
    End Function

    ''' <summary>
    ''' Obtiene los ids de los participantes cargados.
    ''' </summary>
    ''' <returns>Devuelve una colección de List(Of Int64) con los ids de los participantes.</returns>
    ''' <remarks></remarks>
    Private Function GetParticipantIds() As Generic.List(Of Int64)
        Dim ids As New Generic.List(Of Int64)
        Dim idCol As Int32 = dgvParticipants.Columns("ID").Index

        For Each row As DataGridViewRow In dgvParticipants.Rows
            ids.Add(Int64.Parse(row.Cells(idCol).Value.ToString))
        Next

        'ids = ZForoBusiness.GetUserAndGroupsParticipantsId(Me.lastMessageId)

        Return ids
    End Function

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
