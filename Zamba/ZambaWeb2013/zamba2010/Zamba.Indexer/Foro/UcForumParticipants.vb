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

    ''' <summary>
    ''' Carga los participantes de la conversación y la configuración del control.
    ''' </summary>
    ''' <param name="messageId"></param>
    ''' <param name="userId"></param>
    ''' <param name="docId"></param>
    ''' <remarks></remarks>
    Public Sub FillParticipants(ByVal messageId As Int64, ByVal userId As Int64, ByVal docId As Int64)
        'Carga los participantes si el mensaje pertenece a otra conversación.
        If lastMessageId <> messageId Then
            'Si el messageId es -1 es porque no hay seleccionados.
            If messageId <> -1 Then
                Try
                    'Cargo los datos de los participantes. 
                    LoadDataGridView(ZForoBusiness.GetFullParticipants(messageId))

                    'Si el usuario fué el creador de la conversación, se habilitan
                    'los botones de agregar o quitar participantes.
                    If userId = ZForoBusiness.GetCreatorId(messageId) Then
                        btnEdit.Enabled = True
                    Else
                        btnEdit.Enabled = False
                    End If

                    SetParentText(dgvParticipants.RowCount)

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    LoadErrorRow()

                Finally
                    Me.lastMessageId = messageId
                End Try

            Else
                LoadErrorRow()
            End If

            Me.docId = docId
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
        DirectCast(Me.Parent, TabPage).Text = "Participantes (" + cant.ToString + ")"
    End Sub

    ''' <summary>
    ''' Edita los participantes de la conversación.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim docIds As New Generic.List(Of Int64)
        Dim userIds As Generic.List(Of Int64)
        docIds.Add(docId)

        'Obtiene los ids de los participantes.
        Dim newFrmSelectUsers As frmSelectUsers = New frmSelectUsers(GroupToNotifyTypes.Foro, docId, ZForoBusiness.GetUserAndGroupsParticipantsId(lastMessageId))
        'Dim newFrmSelectUsers As frmSelectUsers = New frmSelectUsers(GroupToNotifyTypes.Foro, docId, )
        newFrmSelectUsers.StartPosition = FormStartPosition.CenterParent

        If newFrmSelectUsers.ShowDialog() = DialogResult.OK Then
            userIds = Zamba.Core.NotifyBusiness.getAllSelectedUsers(GroupToNotifyTypes.Foro, docIds)

            'Si se quito el mismo, se vuelve a agregar. El creador 
            'siempre debe participar de la conversación.
            If Not userIds.Contains(UserBusiness.CurrentUser.ID) Then
                userIds.Add(UserBusiness.CurrentUser.ID)
            End If

            If HasChanges(userIds) Then
                'Agrega los participantes a la base.
                ZForoBusiness.InsertMessageParticipants(lastMessageId, userIds)

                'Obtenemos la grilla completa de usuarios
                Dim fullUserIds As Generic.List(Of Int64) = Zamba.Core.NotifyBusiness.GetAllUserIDsToNotify(GroupToNotifyTypes.Foro, docIds)

                'Recarga la grilla
                LoadDataGridView(UserBusiness.GetUserDataById(fullUserIds))

                'Refresco la solapa
                SetParentText(Me.dgvParticipants.RowCount)
            End If
        End If

        newFrmSelectUsers.Dispose()
        newFrmSelectUsers = Nothing
        docIds = Nothing
        userIds = Nothing
    End Sub

    ''' <summary>
    ''' Carga los participantes en la grilla.
    ''' </summary>
    ''' <param name="dtParticipants">DataTable con los datos de los participantes.</param>
    ''' <remarks>El DataTable debe tener cargadas las columnas NAME, NOMBRES, APELLIDO y ID.</remarks>
    Private Sub LoadDataGridView(ByVal dtParticipants As DataTable)
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
        Dim idCol As Int32 = Me.dgvParticipants.Columns("ID").Index
        Dim userIds As New Generic.List(Of Int64)
        Dim i As Int32

        'Verifica si hay participantes quitados.
        For i = 0 To Me.dgvParticipants.RowCount - 1
            'Se agrega a una lista auxiliar para mejorar la búsuqeda de los agregados.
            userIds.Add(CLng(Me.dgvParticipants.Rows(i).Cells(idCol).Value))

            If Not ids.Contains(userIds(i)) Then
                Return True
            End If
        Next

        'Verifica si hay participantes que se agregaron.
        For i = 0 To ids.Count - 1
            If Not userIds.Contains(ids(i)) Then
                Return True
            End If
        Next

        Return False
    End Function

    ''' <summary>
    ''' Obtiene los ids de los participantes cargados.
    ''' </summary>
    ''' <returns>Devuelve una colección de List(Of Int64) con los ids de los participantes.</returns>
    ''' <remarks></remarks>
    Private Function GetParticipantIds() As Generic.List(Of Int64)
        Dim ids As New Generic.List(Of Int64)
        Dim idCol As Int32 = Me.dgvParticipants.Columns("ID").Index

        For Each row As DataGridViewRow In Me.dgvParticipants.Rows
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
