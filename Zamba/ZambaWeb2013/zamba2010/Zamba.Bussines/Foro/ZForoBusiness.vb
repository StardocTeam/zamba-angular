Imports Zamba.Data
Imports Zamba.Core
Imports System.Collections
Imports System.Collections.Generic
Imports Zamba.DataExt.WSResult.Consume
Imports Zamba.DataExt

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

    Dim FF As New ZForo_Factory
    ''' <summary>
    ''' Para web
    ''' </summary>
    ''' <param name="messageId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserAndGroupsParticipantsById(ByVal messageId As Int32) As DataTable
        Return FF.GetUserAndGroupsParticipantsById(messageId)
    End Function

    ''' <summary>
    ''' Para web
    ''' </summary>
    ''' <param name="messageId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserAndGroupsParticipantsByIdAsList(ByVal messageId As Int32) As List(Of Int64)
        Dim dt As DataTable = FF.GetUserAndGroupsParticipantsById(messageId)
        Dim lst As New List(Of Int64)

        If Not IsNothing(dt) Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                lst.Add(dt.Rows(i)(0))
            Next
        End If

        Return lst
    End Function

    Public Function GetInformation(ByVal messageId As Int32) As DataTable
        Return FF.GetInformation(messageId)
    End Function
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
    Public Sub GetAllMessages(ByVal DocId As Int64, ByVal DocTypeId As Int64, ByRef ArrayMensajes As ArrayList, ByRef ArrayRespuestas As ArrayList, ByVal CheckIfVersionMessagesShouldShow As Boolean)
        Dim RiB As New RightsBusiness
        ProcessMessages(FF.GetAllMessages(DocId, DocTypeId, CheckIfVersionMessagesShouldShow, RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, Zamba.Core.RightsType.MostrarConversaciones, DocTypeId)), ArrayMensajes, ArrayRespuestas)
        RiB = Nothing
    End Sub

    Public Function GetAllMessages(ByVal DocId As Int64) As List(Of MensajeForo)

        Return ProcessMessages(FF.GetAllMessages(DocId))

    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist de objetos Mensajes para un documento especifico dentro de un Entidad
    ''' </summary>
    ''' <param name="DocT">Id del Entidad</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <returns>Arraylist de objetos Mensajes</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''     Marcelo     30/06/2009  Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub ProcessMessages(ByVal dtMensajes As DataTable, ByRef ArrayMensajes As ArrayList, ByRef ArrayRespuestas As ArrayList)
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
                If Not IsDBNull(row.ItemArray(rowLinkName)) Then
                    mensaje.Name = row.ItemArray(rowLinkName)
                End If
                If IsDBNull(row.ItemArray(rowMensaje)) Then
                    mensaje.Mensaje = ""
                Else
                    mensaje.Mensaje = row.ItemArray(rowMensaje)
                End If
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
                If Not IsDBNull(row.ItemArray(rowLinkName)) Then
                    mensaje.Name = row.ItemArray(rowLinkName)
                End If
                If IsDBNull(row.ItemArray(rowMensaje)) Then
                    mensaje.Mensaje = ""
                Else
                    mensaje.Mensaje = row.ItemArray(rowMensaje)
                End If
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
    Private Function ProcessMessages(ByVal dtMensajes As DataTable) As List(Of MensajeForo)

        Dim ArrayMensajes As New Dictionary(Of Int64, MensajeForo)

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
                If Not IsDBNull(row.ItemArray(rowLinkName)) Then
                    mensaje.Name = row.ItemArray(rowLinkName)
                End If
                If IsDBNull(row.ItemArray(rowMensaje)) Then
                    mensaje.Mensaje = ""
                Else
                    mensaje.Mensaje = row.ItemArray(rowMensaje)
                End If
                mensaje.Fecha = row.ItemArray(rowFecha)
                nombreUsuario = row.ItemArray(rowUserApellido).ToString().Trim() & " " & row.ItemArray(rowUserNombres).ToString().Trim()
                If nombreUsuario = String.Empty Then nombreUsuario = row.ItemArray(rowUserName)
                mensaje.UserName = nombreUsuario
                If IsDBNull(row.ItemArray(rowDiasVto)) Then
                    mensaje.DiasVto = 0
                Else
                    mensaje.DiasVto = row.ItemArray(rowDiasVto)
                End If
                ArrayMensajes.Add(mensaje.ID, mensaje)
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
                If Not IsDBNull(row.ItemArray(rowLinkName)) Then
                    mensaje.Name = row.ItemArray(rowLinkName)
                End If
                If IsDBNull(row.ItemArray(rowMensaje)) Then
                    mensaje.Mensaje = ""
                Else
                    mensaje.Mensaje = row.ItemArray(rowMensaje)
                End If
                mensaje.Fecha = row.ItemArray(rowFecha)
                nombreUsuario = row.ItemArray(rowUserApellido).ToString().Trim() & " " & row.ItemArray(rowUserNombres).ToString().Trim()
                If nombreUsuario = String.Empty Then nombreUsuario = row.ItemArray(rowUserName)
                mensaje.UserName = nombreUsuario
                If IsDBNull(row.ItemArray(rowDiasVto)) Then
                    mensaje.DiasVto = 0
                Else
                    mensaje.DiasVto = row.ItemArray(rowDiasVto)
                End If
                Dim parentMessage As MensajeForo = ArrayMensajes(mensaje.ParentId)
                If parentMessage IsNot Nothing Then
                    parentMessage.MensajesForo.Add(mensaje)
                End If
            Next

            dv.Dispose()
            dv = Nothing
            nombreUsuario = Nothing
        End If

        Return ArrayMensajes.Values.ToList()
    End Function
    Public Function GetCountAllMessages(ByVal docId As Int64, ByVal userId As Int64) As Int32
        Return FF.GetCountAllMessages(docId, userId)
    End Function

    Public Function GetIfExistMessage(ByVal MessageID As Int32) As Boolean
        Return FF.GetExistMessage(MessageID)
    End Function

    Public Function GetCreatorId(ByVal messageId As Int32) As Int64
        Return FF.GetCreatorId(messageId)
    End Function

    Public Function GetFullParticipants(ByVal messageId As Int32) As DataTable
        Return FF.GetFullParticipants(messageId)
    End Function

    Public Function GetUserAndGroupsParticipantsId(ByVal messageId As Int32) As List(Of Int64)
        Dim ds As DataTable = FF.GetUserAndGroupsParticipantsId(messageId)
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
    ''' <param name="DocT">Id del Entidad</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="ParentId">Id que representa el comentario original</param>
    ''' <param name="IdMensaje">Id del Mensaje que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub DeleteMessage(ByVal IdMensaje As Int32)
        FF.DeleteMessage(IdMensaje)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega un mensaje al foro
    ''' </summary>
    ''' <param name="DocT">Id del Entidad</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="IdMensaje">Id del mensaje</param>
    ''' <param name="ParentId">Id que representa el comentario original, si no es respuesta a un comentario, entonces es 0</param>
    ''' <param name="LinkName">... ,maximo 60 caracteres</param>
    ''' <param name="Mensaje">Mensaje que se desea persistir, maximo 300 caracteres</param>
    ''' <param name="Fecha">Fecha de creacion</param>
    ''' <param name="UserId"></param>
    ''' <param name="StateId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub InsertMessage(ByVal DocId As Int64, ByVal Doctypeid As Int64, ByVal IdMensaje As Int32, ByVal ParentId As Int32, ByVal LinkName As String, ByVal Mensaje As String, ByVal Fecha As Date, ByVal UserId As Int32, ByVal StateId As Integer)
        FF.InsertMessage(DocId, Doctypeid, IdMensaje, ParentId, LinkName, Mensaje, Fecha, UserId, StateId)
    End Sub
    ''' <summary>
    ''' Agrega un mensaje al foro.
    ''' </summary>
    Public Sub InsertMessage(ByVal IdMensaje As Int32, ByVal ParentId As Int32, ByVal LinkName As String, ByVal Mensaje As String, ByVal UserId As Int64)
        FF.InsertMessage(IdMensaje, ParentId, LinkName, Mensaje, UserId)
    End Sub
    ''' <summary>
    ''' Asocia un mensaje a un documento.
    ''' </summary>
    Public Sub InsertMessageDoc(ByVal IdMensaje As Int32, ByVal DocId As Int64, ByVal Doctypeid As Int64)
        FF.InsertMessageDoc(IdMensaje, DocId, Doctypeid)
    End Sub
    ''' <summary>
    ''' Agrega un participante a un mensaje.
    ''' </summary>
    Public Sub InsertMessageParticipant(ByVal IdMensaje As Int32, ByVal UserId As Int64)
        FF.InsertMessageParticipant(IdMensaje, UserId)
    End Sub

    ''' <summary>
    ''' Quita el participante del foro
    ''' </summary>
    ''' <param name="IdMensaje"></param>
    ''' <param name="UserId"></param>
    ''' <remarks></remarks>
    Public Sub RemoveParticipant(ByVal IdMensaje As Int32, ByVal UserId As Int64)
        FF.RemoveParticipant(IdMensaje, UserId)
    End Sub

    ''' <summary>
    ''' Quita los participantes del foro
    ''' </summary>
    ''' <param name="IdMensaje"></param>
    ''' <param name="UserId"></param>
    ''' <remarks></remarks>
    Public Sub RemoveParticipants(ByVal IdMensaje As Int32)
        FF.RemoveParticipants(IdMensaje)
    End Sub

    ''' <summary>
    ''' Agrega un conjunto de participantes a un mensaje.
    ''' </summary>
    Public Sub InsertMessageParticipants(ByVal IdMensaje As Int32, ByVal UserIds As Generic.List(Of Int64))

        Try
            'Remuevo todos los participantes.
            FF.RemoveParticipants(IdMensaje)

            'Agrego todos los seleccionados.
            For Each userId As Int64 In UserIds
                FF.InsertMessageParticipant(IdMensaje, userId)
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Obtiene los ids de los participantes en una respuesta.
    ''' </summary>
    Public Function GetMessageReplyParticipant(ByVal IdMensaje As Int32) As DataSet
        Try
            Return FF.GetMessageReplyParticipant(IdMensaje)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los mails enviados al notificar un mensaje en el foro.
    ''' </summary>
    Public Function GetForumMails(ByVal docId As Int64, ByVal idMensaje As Int32, ByVal parentId As Int64) As String
        Return FF.GetForumMails(docId, idMensaje, parentId)
    End Function

    ''' <summary>
    ''' Agrega los mails de los mensajes que fueron notificados.
    ''' </summary>
    Public Sub InsertForumMail(ByVal docId As Int64, ByVal idMensaje As Int32, ByVal parentId As Int64, ByVal mails As String)
        FF.InsertForumMail(docId, idMensaje, parentId, mails)
    End Sub

    ''' <summary>
    ''' Agrega los mails de los mensajes que fueron notificados.
    ''' </summary>
    Public Sub InsertForumMail(ByVal selectedResults As Generic.List(Of Zamba.Core.IResult), ByVal idMensaje As Int32, ByVal parentId As Int64, ByVal mails As String)
        If TypeOf (selectedResults(0)) Is Result Then
            For Each r As Result In selectedResults
                FF.InsertForumMail(r.ID, idMensaje, parentId, mails)
            Next
        Else
            For Each r As TaskResult In selectedResults
                FF.InsertForumMail(r.ID, idMensaje, parentId, mails)
            Next
        End If
    End Sub
    '''' <summary>
    '''' Remueve un participante de un mensaje.
    '''' </summary>
    'Public  Sub RemoveParticipants(ByVal IdMensaje As Int32, ByVal UserIds As Generic.List(Of Int64))
    '    FF.RemoveParticipants(IdMensaje, UserIds)
    'End Sub
    ''' <summary>
    ''' Obtiene todas las respuestas de un mensaje.
    ''' </summary>
    Public Function GetRealtedDocs(ByVal parentId As Int32, ByVal docId As Int64) As DataTable
        Return FF.GetRelatedDocs(parentId, docId)
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un nuevo ID para un mensaje
    ''' </summary>
    ''' <param name="DocT">Id del Entidad</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <returns>Numero entero que representa el nuevo ID</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function SiguienteId(ByVal DocId As Int64) As Integer
        Return FF.SiguienteId(DocId)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un nuevo ParentID
    ''' </summary>
    ''' <param name="DocT">Id del Entidad</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="IdMensaje">Id del mensaje</param>
    ''' <returns>ID que representa el Nuevo Parent ID</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function SiguienteParent(ByVal DocId As Int64, ByVal IdMensaje As Int32) As Int32
        Return FF.SiguienteParent(DocId, IdMensaje)
    End Function
#Region "Adjuntos"
    ''' <summary>
    ''' Obtiene un datatable con la lista de nombres de archivos adjuntos.
    ''' </summary>
    Public Function GetAttachsNames(ByVal idMensaje As Int32) As DataTable
        'Se obtienen las rutas completas de los adjuntos.
        Dim tempTable As DataTable = FF.GetAttachsNames(idMensaje)
        Return tempTable
    End Function
    ''' <summary>
    ''' Obtiene un datatable con el archivo en la base de datos.
    ''' </summary>
    Public Function GetAttachFileByFileName(ByVal idMensaje As Int32, ByVal FileName As String) As DataTable
        'Se obtienen las rutas completas de los adjuntos.
        Dim tempTable As DataTable = FF.GetAttachFileByName(idMensaje, FileName)
        Return tempTable
    End Function

    Public Function GetIfExistAttach(ByVal MessageID As Int32) As Boolean
        Return FF.GetAttachExist(MessageID)
    End Function

    ''' <summary>
    ''' Agrega una lista de adjuntos relacionado a un mensaje.
    ''' </summary>
    Public Sub InsertAttach(ByVal idMensaje As Int32, ByRef file As Byte(), ByVal maxLength As Int64, ByVal fileName As String)
        If Not IsNothing(file) AndAlso file.Length > 0 AndAlso idMensaje > 0 Then

            Dim Zoptb As New ZOptBusiness

                If Not IsNothing(Zoptb.GetValue("ServAdjuntosRuta")) Then
                    FF.InsertAttach(idMensaje, file, maxLength, fileName, IO.Path.Combine(IO.Path.Combine(Zoptb.GetValue("ServAdjuntosRuta"), idMensaje.ToString()), fileName))
                Else
                    Throw New Exception("No esta configurada la ruta de los adjuntos (ServAdjuntosRuta)")
                End If
                Zoptb = Nothing

        End If
    End Sub

    Public Sub InsertAttachInAExistRecord(ByVal idMensaje As Int32, ByRef file As Byte(), ByVal maxLength As Int64, ByVal fileName As String)
        If Not IsNothing(file) AndAlso file.Length > 0 AndAlso idMensaje > 0 Then

            Dim Zoptb As New ZOptBusiness

                FF.InsertAttachInAExistRecord(idMensaje, file, maxLength, fileName, IO.Path.Combine(IO.Path.Combine(Zoptb.GetValue("ServAdjuntosRuta"), idMensaje.ToString()), fileName))
                Zoptb = Nothing

        End If
    End Sub

    ''' <summary>
    ''' Elimina los adjuntos de a un mensaje.
    ''' </summary>
    Public Function DeleteAttachs(ByVal idMensaje As Int32) As Integer
        Return FF.DeleteAttachs(idMensaje)
    End Function

#Region "Consumo de WS"
    ''' <summary>
    ''' LLama al web service para hacer la insercion de attach de foro
    ''' </summary>
    ''' <param name="messageId"></param>
    ''' <param name="file"></param>
    ''' <param name="userId"></param>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertForumAttachWS(ByVal messageId As Long, ByRef file As Byte(), ByVal userId As Long, ByVal fileName As String) As Boolean
        Dim wsFactory As New WSResultsFactory()
        Dim returnVal As Boolean = False
        Try
            returnVal = wsFactory.ConsumeInsertForumAttach(messageId, file, userId, fileName)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            wsFactory.Dispose()
        End Try

        Return returnVal
    End Function

    ''' <summary>
    ''' LLama al web service para obtener una attach de foro
    ''' </summary>
    ''' <param name="messageId"></param>
    ''' <param name="fileName"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttachFileByMessageIdAndNameWS(ByVal messageId As Long, ByVal fileName As String, ByVal userId As Long) As Byte()
        Dim wsFactory As New WSResultsFactory()
        Dim returnVal As Byte() = Nothing
        Try
            returnVal = wsFactory.ConsumeGetAttachFileByMessageIdAndName(messageId, fileName, userId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            wsFactory.Dispose()
        End Try

        Return returnVal
    End Function

    ''' <summary>
    ''' Obtiene los nombres de los attach por mensaje de foro, llamando al WS
    ''' </summary>
    ''' <param name="messageId"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks> 
    Public Function GetAttachsNamesByMessageIdWS(ByVal messageId As Long, ByVal userId As Long) As String()
        Dim wsFactory As New WSResultsFactory()
        Dim returnVal As String() = Nothing
        Try
            returnVal = wsFactory.ConsumeGetAttachsNamesByMessageId(messageId, userId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            wsFactory.Dispose()
        End Try

        Return returnVal
    End Function
#End Region
#End Region
End Class
