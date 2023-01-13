Imports zamba.data
Imports Zamba.Core
Imports System.Collections
Imports System.Collections.Generic

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Foro
''' Class	 : Foro.ZForo_Factory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Factory para trabajar con elementos del foro
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class ZForoBusiness
    Inherits Zamba.Core.ZClass
    Public Overrides Sub Dispose()

    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist de objetos Mensajes para un documento especifico dentro de un Entidad
    ''' </summary>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''     Marcelo     30/06/2009  Modified
    '''     Tomas       07/05/2010  Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub GetAllMessages(ByVal DocId As Int64, ByVal DocTypeId As Int64, ByRef ArrayMensajes As ArrayList, ByRef ArrayRespuestas As ArrayList, ByVal CheckIfVersionMessagesShouldShow As Boolean, byval RootDocumentId As int64)
        Dim ShowAllConversations As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, Zamba.Core.RightsType.MostrarConversaciones, DocTypeId)
        ProcessMessages(ZForo_Factory.GetAllMessages(DocId, DocTypeId,  ShowAllConversations, Membership.MembershipHelper.CurrentUser, RootDocumentId), ArrayMensajes, ArrayRespuestas)
    End Sub

    Public Shared Function GetAllMessagesDT(ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal RootDocumentId As Int64) As DataTable
        Dim ShowAllConversations As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, Zamba.Core.RightsType.MostrarConversaciones, DocTypeId)
        Return ZForo_Factory.GetAllMessages(DocId, DocTypeId, ShowAllConversations, Membership.MembershipHelper.CurrentUser, RootDocumentId)
    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist de objetos Mensajes para un documento especifico dentro de un Entidad
    ''' </summary>
    ''' <param name="DocT">Id de la entidad</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <returns>Arraylist de objetos Mensajes</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''     Marcelo     30/06/2009  Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Shared Sub ProcessMessages(ByVal dtMensajes As DataTable, ByRef ArrayMensajes As ArrayList, ByRef ArrayRespuestas As ArrayList)
        If dtMensajes.Rows.Count > 0 Then
            Dim dv As DataView = New DataView(dtMensajes)

            dv.RowFilter = "ParentId = 0"
            Dim dt As DataTable = dv.ToTable()
            Dim nombreUsuario As String
            Dim mensaje As MensajeForo

            'Atributos de las columnas a leer. En oracle y sql la posición de las columnas cambian.
            Dim rowUserId As Int32 = dt.Columns.IndexOf("USERID")
            Dim rowIdMensaje As Int32 = dt.Columns.IndexOf("IDMENSAJE")
            Dim rowParentId As Int32 = dt.Columns.IndexOf("PARENTID")
            Dim rowLinkName As Int32 = dt.Columns.IndexOf("LINKNAME")
            Dim rowMensaje As Int32 = dt.Columns.IndexOf("MENSAJE")
            Dim rowFecha As Int32 = dt.Columns.IndexOf("FECHA")
            Dim rowUserName As String = dt.Columns.IndexOf("NAME")
            Dim rowUserNombres As String = dt.Columns.IndexOf("NOMBRES")
            Dim rowUserApellido As String = dt.Columns.IndexOf("APELLIDO")
            Dim rowDiasVto As Int32 = dt.Columns.IndexOf("DIASVTO")

            'Cargo los mensajes
            For Each row As DataRow In dt.Rows
                mensaje = New MensajeForo
                'Mensaje.DocId = row.Item(0)
                mensaje.UserId = row.ItemArray(rowUserId)
                mensaje.ID = row.ItemArray(rowIdMensaje)
                mensaje.ParentId = row.ItemArray(rowParentId)
                mensaje.Name = row.ItemArray(rowLinkName)
                mensaje.Mensaje = row.ItemArray(rowMensaje)
                mensaje.Fecha = row.ItemArray(rowFecha)
                nombreUsuario = row.ItemArray(rowUserApellido).ToString().Trim() & " " & row.ItemArray(rowUserNombres).ToString().Trim()
                If nombreUsuario = String.Empty Then nombreUsuario = row.ItemArray(rowUserName)
                mensaje.UserName = nombreUsuario
                If IsDBNull(row.ItemArray(rowDiasVto)) Then
                    mensaje.DiasVto = 0
                Else
                    mensaje.DiasVto = row.ItemArray(rowDiasVto)
                End If
                ArrayMensajes.Add(mensaje)
            Next

            dv.RowFilter = "ParentId <> 0"
            dt = dv.ToTable()

            'Cargo las respuestas
            For Each row As DataRow In dt.Rows
                mensaje = New MensajeForo
                'todo marcos, mapear con nombres
                'Mensaje.DocT = DsMensajes.Tables(0).Rows(i).Item(0)
                'Mensaje.DocId = row.Item(0)
                mensaje.UserId = row.ItemArray(rowUserId)
                mensaje.ID = row.ItemArray(rowIdMensaje)
                mensaje.ParentId = row.ItemArray(rowParentId)
                mensaje.Name = row.ItemArray(rowLinkName)
                mensaje.Mensaje = row.ItemArray(rowMensaje)
                mensaje.Fecha = row.ItemArray(rowFecha)
                nombreUsuario = row.ItemArray(rowUserApellido).ToString().Trim() & " " & row.ItemArray(rowUserNombres).ToString().Trim()
                If nombreUsuario = String.Empty Then nombreUsuario = row.ItemArray(rowUserName)
                mensaje.UserName = nombreUsuario
                If IsDBNull(row.ItemArray(rowDiasVto)) Then
                    mensaje.DiasVto = 0
                Else
                    mensaje.DiasVto = row.ItemArray(rowDiasVto)
                End If
                ArrayRespuestas.Add(mensaje)
            Next

            dv.Dispose()
            dv = Nothing
            nombreUsuario = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la cantidad de mensajes del documento
    ''' </summary>
    ''' <param name="docId"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCountAllMessages(ByVal docId As Int64, ByVal userId As Int64) As Int32
        Return ZForo_Factory.GetCountAllMessages(docId, userId)
    End Function

    Public Shared Function GetCreatorId(ByVal messageId As Int32) As Int64
        Return ZForo_Factory.GetCreatorId(messageId)
    End Function

    Public Shared Function GetFullParticipants(ByVal messageId As Int32) As DataTable
        Return ZForo_Factory.GetFullParticipants(messageId)
    End Function
    Public Shared Function GetGroupsToNotify(ByVal messageId As Int32) As List(Of Int64)
        Dim lstUserIDs As New List(Of Int64)
        Dim ds As New DataSet

        Try
            ds = ZForo_Factory.GetGroupsToNotify(messageId)
            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    If Not String.IsNullOrEmpty(r("IdUsuario").ToString) AndAlso Int64.Parse(r("IdUsuario").ToString) > 0 Then
                        lstUserIDs.Add(Convert.ToInt64(r("IdUsuario")))
                    End If
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return lstUserIDs
    End Function

    Public Shared Function GetUsersToNotify(ByVal messageId As Int32) As List(Of Int64)
        Dim lstUserIDs As New List(Of Int64)
        Dim ds As New DataSet

        Try
            ds = ZForo_Factory.GetUsersToNotify(messageId)
            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    If Not String.IsNullOrEmpty(r("IdUsuario").ToString) AndAlso Int64.Parse(r("IdUsuario").ToString) > 0 Then
                        lstUserIDs.Add(Convert.ToInt64(r("IdUsuario")))
                    End If
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return lstUserIDs
    End Function
    Public Shared Function GetUserAndGroupsParticipantsId(ByVal messageId As Int32) As List(Of Int64)
        Dim ds As DataTable = ZForo_Factory.GetUserAndGroupsParticipantsId(messageId)
        Dim listaIds As New List(Of Int64)
        For Each r As DataRow In ds.Rows
            listaIds.Add(Convert.ToInt64(r.Item(0)))
        Next
        Return listaIds
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un mensaje
    ''' </summary>
    ''' <param name="DocT">Id de la entidad</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="ParentId">Id que representa el comentario original</param>
    ''' <param name="IdMensaje">Id del Mensaje que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteMessage(ByVal IdMensaje As Int32)
        ZForo_Factory.DeleteMessage(IdMensaje)
    End Sub


    ''' <summary>
    ''' Agrega un mensaje al foro.
    ''' </summary>
    Public Shared Sub InsertMessage(ByVal IdMensaje As Int32, ByVal ParentId As Int32, ByVal LinkName As String, ByVal Mensaje As String, ByVal UserId As Int64)
        ZForo_Factory.InsertMessage(IdMensaje, ParentId, LinkName, Mensaje, UserId)
    End Sub
    ''' <summary>
    ''' Asocia un mensaje a un documento.
    ''' </summary>
    Public Shared Sub InsertMessageDoc(ByVal IdMensaje As Int32, ByVal DocId As Int64, ByVal Doctypeid As Int64)
        ZForo_Factory.InsertMessageDoc(IdMensaje, DocId, Doctypeid)
    End Sub
    ''' <summary>
    ''' Agrega un participante a un mensaje.
    ''' </summary>
    Public Shared Sub InsertMessageParticipant(ByVal IdMensaje As Int32, ByVal UserId As Int64)
        ZForo_Factory.InsertMessageParticipant(IdMensaje, UserId)
    End Sub

    ''' <summary>
    ''' Quita el participante del foro
    ''' </summary>
    ''' <param name="IdMensaje"></param>
    ''' <param name="UserId"></param>
    ''' <remarks></remarks>
    Public Shared Sub RemoveParticipant(ByVal IdMensaje As Int32, ByVal UserId As Int64)
        ZForo_Factory.RemoveParticipant(IdMensaje, UserId)
    End Sub
    ''' <summary>
    ''' Agrega un conjunto de participantes a un mensaje.
    ''' </summary>
    Public Shared Sub InsertMessageParticipants(ByVal IdMensaje As Int32, ByVal UserIds As Generic.List(Of Int64))

        Try
            'Remuevo todos los participantes.
            ZForo_Factory.RemoveParticipants(IdMensaje)

            'Agrego todos los seleccionados.
            If UserIds IsNot Nothing Then
                For Each userId As Int64 In UserIds
                    ZForo_Factory.InsertMessageParticipant(IdMensaje, userId)
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Obtiene los ids de los participantes en una respuesta.
    ''' </summary>
    Public Shared Function GetMessageReplyParticipant(ByVal IdMensaje As Int32) As DataSet
        Try
            Return ZForo_Factory.GetMessageReplyParticipant(IdMensaje)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los mails enviados al notificar un mensaje en el foro.
    ''' </summary>
    Public Shared Function GetForumMails(ByVal docId As Int64, ByVal idMensaje As Int32, ByVal parentId As Int64) As String
        Return ZForo_Factory.GetForumMails(docId, idMensaje, parentId)
    End Function

    ''' <summary>
    ''' Agrega los mails de los mensajes que fueron notificados.
    ''' </summary>
    Public Shared Sub InsertForumMail(ByVal docId As Int64, ByVal idMensaje As Int32, ByVal parentId As Int64, ByVal mails As String)
        ZForo_Factory.InsertForumMail(docId, idMensaje, parentId, mails)
    End Sub

    ''' <summary>
    ''' Agrega los mails de los mensajes que fueron notificados.
    ''' </summary>
    Public Shared Sub InsertForumMail(ByVal selectedResults As Generic.List(Of Zamba.Core.IResult), ByVal idMensaje As Int32, ByVal parentId As Int64, ByVal mails As String)
        If TypeOf (selectedResults(0)) Is Result Then
            For Each r As Result In selectedResults
                ZForo_Factory.InsertForumMail(r.ID, idMensaje, parentId, mails)
            Next
        Else
            For Each r As TaskResult In selectedResults
                ZForo_Factory.InsertForumMail(r.ID, idMensaje, parentId, mails)
            Next
        End If
    End Sub
    '''' <summary>
    '''' Remueve un participante de un mensaje.
    '''' </summary>
    'Public Shared Sub RemoveParticipants(ByVal IdMensaje As Int32, ByVal UserIds As Generic.List(Of Int64))
    '    ZForo_Factory.RemoveParticipants(IdMensaje, UserIds)
    'End Sub
    ''' <summary>
    ''' Obtiene todas las respuestas de un mensaje.
    ''' </summary>
    Public Shared Function GetRealtedDocs(ByVal parentId As Int32, ByVal docId As Int64) As DataTable
        Return ZForo_Factory.GetRelatedDocs(parentId, docId)
    End Function




#Region "Adjuntos"
    ''' <summary>
    ''' Obtiene un datatable con los datos necesarios para manejar los adjuntos.
    ''' </summary>
    Public Shared Function GetAttachs(ByVal idMensaje As Int32) As DataTable
        'Se obtienen las rutas completas de los adjuntos.
        Dim tempTable As DataTable = ZForo_Factory.GetAttachs(idMensaje)

        'Se genera una columna donde se almacenará unicamente el nombre archivo con su extensión.
        tempTable.Columns.Add("Adjuntos", GetType(String))

        'Se asigna el archivo y extensión.
        For Each row As DataRow In tempTable.Rows
            row.Item(1) = IO.Path.GetFileName(row.Item(0).ToString)
        Next

        Return tempTable
    End Function

    ''' <summary>
    ''' Obtiene un datatable con los datos necesarios para manejar los adjuntos.
    ''' </summary>
    Public Shared Function GetBlobAttachs(ByVal idMensaje As Int32) As DataTable
        Return ZForo_Factory.GetBlobAttachs(idMensaje)
    End Function

    Public Shared Function GetBlobAttachFileByName(ByVal idMensaje As Int32, ByVal FileName As String, ByVal destFileName As String) As DataTable
        Dim tempTable As DataTable = ZForo_Factory.GetBlobAttachFileByName(idMensaje, FileName)
        Dim file As Byte() = Nothing

        If (tempTable.Rows.Count > 0) Then
            file = tempTable.Rows(0)(0)
        End If

        If (IsNothing(file) OrElse file.Length <= 0) Then
            Dim path As String = ZForo_Factory.GetAttachFileByName(idMensaje, FileName)

            'Encodeo y guardo en la BD
            file = FileEncode.Encode(path)

            Try
                ZForo_Factory.InsertBlobAttachInAExistRecord(idMensaje, file, 11111111, FileName, path)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

        FileEncode.Decode(destFileName, file)
    End Function

    ''' <summary>
    ''' Obtiene la ruta del attach
    ''' </summary>
    ''' <param name="idMensaje"></param>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAttachFileByName(ByVal idMensaje As Int32, ByVal FileName As String) As String
        Return ZForo_Factory.GetAttachFileByName(idMensaje, FileName)
    End Function

    ''' <summary>
    ''' Obtiene todos los attach que no estan en blob
    ''' </summary>
    ''' <param name="idMensaje"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetNonBlobAttachs() As DataTable
        Return ZForo_Factory.GetNonBlobAttachs()
    End Function

    ''' <summary>
    ''' Agrega una lista de adjuntos relacionado a un mensaje.
    ''' </summary>
    Public Shared Sub InsertAttach(ByVal idMensaje As Int32, ByVal attachs As Generic.List(Of String))
        If Not IsNothing(attachs) AndAlso attachs.Count > 0 Then
            Try
                For Each path As String In attachs
                    ZForo_Factory.InsertAttach(idMensaje, path)
                Next
                'TODO: pasarle todos los path a un SP que los procese e inserte (como la funcion que existe de indexado)
                'se hizo de esta manera por falta de tiempo con BPN
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Agrega una lista de adjuntos relacionado a un mensaje.
    ''' </summary>
    Public Shared Sub InsertBlobAttach(ByVal idMensaje As Int32, ByRef file As Byte(), ByVal maxLength As Integer, ByVal fileName As String)
        If Not IsNothing(file) AndAlso file.Length > 0 AndAlso idMensaje > 0 Then
            Try
                ZForo_Factory.InsertBlobAttach(idMensaje, file, maxLength, fileName, String.Empty)
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    Public Shared Sub InsertBlobAttachInAExistRecord(ByVal idMensaje As Int32, ByRef file As Byte(), ByVal maxLength As Integer, ByVal fileName As String)
        If Not IsNothing(file) AndAlso file.Length > 0 AndAlso idMensaje > 0 Then
            Try
                ZForo_Factory.InsertBlobAttachInAExistRecord(idMensaje, file, maxLength, fileName, IO.Path.Combine(IO.Path.Combine(ZOptBusiness.GetValue("ServAdjuntosRuta"), idMensaje.ToString()), fileName))
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Elimina los adjuntos de a un mensaje.
    ''' </summary>
    Public Shared Function DeleteAttachs(ByVal idMensaje As Int32) As Integer
        Return ZForo_Factory.DeleteAttachs(idMensaje)
    End Function

    Public Shared Function GetIfExistAttach(ByVal MessageID As Int32) As Boolean
        Return ZForo_Factory.GetAttachExist(MessageID)
    End Function
#End Region
End Class
