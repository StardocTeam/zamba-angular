Imports Zamba.Core
Imports System.Data.SqlClient

'Imports zamba.foro.core
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
Public Class ZForo_Factory
    Inherits Zamba.Core.ZClass
    Public Overrides Sub Dispose()

    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene todos los mensajes de los cuales el usuario es partícipe.
    ''' </summary>
    ''' <remarks>
    '''     Nota: Habrán respuestas que quedarán sin padre, ya que fueron eliminadas al filtrar
    '''     por las que el usuario no participa, pero cuando esta tabla sea procesada, esos 
    '''     mensajes no serán mostrados.
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''     Marcelo     30/06/2009  Modified
    '''     Tomas       18/05/2010  Modified
    '''     pablo       18/11/2010  Modified - Se trae metodo del branch BPN
    ''' </history>
    ''' --------------------------------------------------------------------------
    Public Shared Function GetAllMessages(ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal ShowAllConversations As Boolean, ByVal CurrentUser As IUser, ByVal RootDocumentId As Int64) As DataTable
        Dim dtMessages As DataTable

        'Se construye el select para traer los mensajes
        Dim sbQuery As New System.Text.StringBuilder()

        sbQuery.Append("SELECT distinct zf.UserId, zf.IdMensaje, zf.ParentId, zf.LinkName, zf.Mensaje, zf.Fecha, ")
        sbQuery.Append("zfd.doct, ut.name, ut.nombres, ut.apellido, zf.DiasVto FROM zforum zf ")
        sbQuery.Append("INNER JOIN ZFORUM_R_DOC zfd on zfd.IdMensaje = zf.IdMensaje ")
        sbQuery.Append("INNER JOIN USRTABLE ut on ut.id = zf.userid ")

        If RootDocumentId <> 0 Then
            sbQuery.Append("WHERE zfd.DocId in (select doc_id from Doc_t" & DocTypeId & " where rootid = " & RootDocumentId & " or doc_id = " & RootDocumentId & ")")
        Else
            sbQuery.Append("WHERE zfd.DocId in (select doc_id from Doc_t" & DocTypeId & " where rootid = " & DocId & " or doc_id = " & DocId & ")")
        End If
        sbQuery.Append(" order by fecha")

        'Se obtienen todos los mensajes (+ datos de usuario) de un documento en particular.
        dtMessages = Server.Con.ExecuteDataset(CommandType.Text, sbQuery.ToString).Tables(0)

        'Si el usuario no tiene permisos de ver todos los mensajes, se agrega un filtro
        'para traer las conversaciones de las que se encuentra agregado.
        If dtMessages.Rows.Count > 0 AndAlso Not ShowAllConversations Then
            Dim dtTempIds As DataTable
            sbQuery.Remove(0, sbQuery.Length)
            sbQuery.Append("SELECT zfu.idmensaje FROM zforum_r_usr zfu ")
            sbQuery.Append("INNER JOIN zforum zf ON zfu.idmensaje = zf.idmensaje ")
            sbQuery.Append("INNER JOIN ZFORUM_R_DOC zfd on zfd.IdMensaje = zf.IdMensaje ")
            sbQuery.Append("WHERE zf.parentid = 0 AND zfu.idusuario in (")
            sbQuery.Append(CurrentUser.ID.ToString)
            For Each grp As UserGroup In CurrentUser.Groups
                sbQuery.Append(", ")
                sbQuery.Append(grp.ID.ToString)
            Next

            sbQuery.Append(")")
            sbQuery.Append(" and zfd.DocId ")


            If RootDocumentId <> 0 Then
                sbQuery.Append("in (select doc_id from doc_t" & DocTypeId & " where rootid = " & RootDocumentId & " or doc_id = " & RootDocumentId & ")")
            Else
                sbQuery.Append("in (select doc_id from doc_t" & DocTypeId & " where rootid = " & DocId & " or doc_id = " & DocId & ")")
            End If

            dtTempIds = Server.Con.ExecuteDataset(CommandType.Text, sbQuery.ToString).Tables(0)

            If dtTempIds.Rows.Count > 0 Then
                'Si el usuario participa en alguna conversación, se eliminan  
                'los mensajes principales de los cuales no es partícipe.

                Dim dv As New DataView(dtMessages)
                sbQuery.Remove(0, sbQuery.Length)

                'Se arma el filtro.
                sbQuery.Append("IDMENSAJE NOT IN (")
                For i As Int32 = 0 To dtTempIds.Rows.Count - 1
                    sbQuery.Append(dtTempIds.Rows(i).Item(0).ToString)
                    sbQuery.Append(",")
                Next
                sbQuery.Remove(sbQuery.Length - 1, 1)
                sbQuery.Append(") AND PARENTID = 0")
                dv.RowFilter = sbQuery.ToString

                'Se eliminan los mensajes de los cuales el usuario no participa. 
                While dv.Count > 0
                    dv.Delete(0)
                End While

                dv.Dispose()
                dv = Nothing
            Else
                'En el caso de que no participe en alguna conversación 
                'para ese documento se devuelve el datatable vacio.
                dtMessages.Clear()
            End If

            dtTempIds.Dispose()
            dtTempIds = Nothing
        End If

        sbQuery = Nothing
        Return dtMessages
    End Function
    ''' <summary>
    ''' Obtiene la cantidad de mensajes que hay en total, tanto mensajes como respuestas, para el documento.
    ''' </summary>
    ''' <param name="DocId"></param>
    '''     pablo       18/11/2010  Modified - Se trae metodo del branch BPN
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Shared Function GetCountAllMessages(ByVal docId As Int64, ByVal userId As Int64) As Int64
        Dim dtUserMsgIds As DataTable
        Dim count As Int32 = 0

        'Se obtienen los ids de los mensajes que inician las conversaciones 
        'en los que el usuario se encuentra participando.
        If Server.isOracle Then

            dtUserMsgIds = Server.Con.ExecuteDataset(CommandType.Text, String.Format("Select d.idmensaje from zforum_r_doc d inner Join zforum_r_usr u on d.idmensaje = u.idmensaje where u.idusuario = {0} And d.docid = {1} order by d.idmensaje", userId, docId)).Tables(0)

        Else
            Dim parValues() As Object = {userId, docId}
            dtUserMsgIds = Server.Con.ExecuteDataset("ZSP_FORUM_100_GETUSERMESSAGESBYDOC", parValues).Tables(0)
        End If

        'Se verifica que el usuario participe en alguna conversación.
        If dtUserMsgIds.Rows.Count = 0 Then
            'Se devuelve 0 si el usuario no participa en alguna conversación del documento.
            dtUserMsgIds.Dispose()
            dtUserMsgIds = Nothing
            Return 0
        Else
            Dim dtAllMsgIds As DataTable
            Dim tempId As Int32
            Dim lstUserMsgIds As New Generic.List(Of Int32)
            Dim lstAllMsgIds As New Generic.List(Of Int32)
            Dim lstAllParentIds As New Generic.List(Of Int32)
            Dim i As Int32

            'Se obtienen los ids de todas las conversaciones y de sus respuestas.
            If Server.isOracle = True Then
                dtAllMsgIds = Server.Con.ExecuteDataset("select idmensaje,parentid from zforum order by idmensaje,parentid").Tables(0)
            Else
                dtAllMsgIds = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "ZSP_FORUM_100_GETFORUMIDS").Tables(0)
            End If

            'Se convierte el resultado de los datatables a listas para facilitar el uso.
            For i = 0 To dtUserMsgIds.Rows.Count - 1
                lstUserMsgIds.Add(dtUserMsgIds.Rows(i).ItemArray(0))
            Next
            For i = 0 To dtAllMsgIds.Rows.Count - 1
                lstAllMsgIds.Add(dtAllMsgIds.Rows(i).ItemArray(0))
                lstAllParentIds.Add(dtAllMsgIds.Rows(i).ItemArray(1))
            Next

            'Se procesa la cantidad de mensajes y respuestas
            For i = 0 To lstUserMsgIds.Count - 1
                If lstAllMsgIds.Contains(lstUserMsgIds(i)) Then
                    'Se suma la cuenta del mensaje principal y sus respuestas.
                    count += 1
                    CountMessages(count, lstUserMsgIds(i), lstAllMsgIds, lstAllParentIds)
                End If
            Next
            lstAllMsgIds.Clear()
            lstAllParentIds.Clear()
            lstUserMsgIds.Clear()
            dtAllMsgIds.Dispose()
            dtUserMsgIds.Dispose()
            lstAllMsgIds = Nothing
            lstAllParentIds = Nothing
            lstUserMsgIds = Nothing
            dtAllMsgIds = Nothing
            dtUserMsgIds = Nothing

            Return count
        End If
    End Function
    ''' <summary>
    ''' Cuenta los mensajes de una conversación.
    ''' </summary>
    ''' <param name="count">Contador para los mensajes. No incluye el mensaje principal.</param>
    ''' <param name="msgId">Id del mensaje a buscar si tiene respuestas.</param>
    ''' <param name="lstAllMsgIds">Lista con el id de los mensajes</param>
    ''' <param name="lstAllParentIds">Lista con el id de las respuestas de los mensajes.</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     Tomas       20/05/2010  Created
    '''     pablo       18/11/2010  - Se trae metodo del branch BPN
    ''' </history>
    Private Shared Sub CountMessages(ByRef count As Int32, ByVal msgId As Int32, ByVal lstAllMsgIds As Generic.List(Of Int32), ByVal lstAllParentIds As Generic.List(Of Int32))
        'Verifica si el mensaje tiene una respuesta.
        If lstAllParentIds.Contains(msgId) Then
            Dim i As Int32 = 0
            'Verifica TODAS las respuestas a ese mensaje.
            While lstAllParentIds.IndexOf(msgId, i) <> -1
                'Aumenta el contador de mensaje encontrado.
                count += 1
                'Busca si la respuesta tiene respuestas.
                CountMessages(count, lstAllMsgIds(lstAllParentIds.IndexOf(msgId, i)), lstAllMsgIds, lstAllParentIds)
                'Aumenta el contador que indica desde donde buscar si hay mas respuestas al mensaje inicial.
                i = lstAllParentIds.IndexOf(msgId, i) + 1
            End While
        End If
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene los participantes de una conversacion.
    ''' </summary>
    ''' <history>
    '''     Tomas       18/05/2010  Created
    '''     Javier      28/12/2010  Modified
    ''' </history>
    ''' --------------------------------------------------------------------------
    Public Shared Function GetFullParticipants(ByVal messageId As Int32) As DataTable
        'Dim query As String = "Select ID, NAME, NOMBRES, APELLIDO FROM USRtable U INNER JOIN ZFORUM_R_USR Z On U.ID = Z.IDUSUARIO WHERE Z.IDMENSAJE = " + messageId.ToString
        Dim query As String = "Select ID, NAME, NOMBRES, APELLIDO FROM USRtable U " _
                                + "WHERE ID In (Select idusuario FROM  ZFORUM_R_USR Z  WHERE Z.IDMENSAJE = " + messageId.ToString + ") " _
                                + "Or ID In (Select distinct usrid from usr_r_group ugr inner join  ZFORUM_R_USR Zr On ugr.groupid=zr.idusuario " _
                                + " where Zr.IDMENSAJE = " + messageId.ToString + ")"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene los participantes de una conversacion.
    ''' </summary>
    ''' <history>
    ''' </history>
    ''' --------------------------------------------------------------------------
    Public Shared Function GetGroupsToNotify(ByVal MessageID As Int32) As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "Select idusuario FROM zforum_r_usr WHERE idmensaje = " & MessageID.ToString & "And idusuario In (Select id FROM usrgroup)")
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene los participantes de una conversacion.
    ''' </summary>
    ''' <history>
    ''' </history>
    ''' --------------------------------------------------------------------------
    Public Shared Function GetUsersToNotify(ByVal MessageID As Int32) As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "Select idusuario FROM zforum_r_usr WHERE idmensaje = " & MessageID & "And idusuario In (Select id FROM usrtable)")
    End Function

    Public Shared Function GetUserAndGroupsParticipantsId(ByVal messageId As Int32) As DataTable
        'Dim query As String = "Select ID,NAME,NOMBRES,APELLIDO FROM USRtable U INNER JOIN ZFORUM_R_USR Z On U.ID = Z.IDUSUARIO WHERE Z.IDMENSAJE = " + messageId.ToString
        Dim query As String = "Select idusuario FROM  ZFORUM_R_USR Z  WHERE Z.IDMENSAJE = " + messageId.ToString
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el id del usuario que creó el mensaje.
    ''' </summary>
    ''' <history>
    '''     Tomas       18/05/2010  Created
    '''     pablo       18/11/2010  Modified - Se trae metodo del branch BPN
    ''' </history>
    ''' --------------------------------------------------------------------------
    Public Shared Function GetCreatorId(ByVal messageId As Int32) As Int64
        Dim query As String = "Select USERID FROM ZFORUM WHERE IDMENSAJE = " + messageId.ToString
        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un mensaje
    ''' </summary>
    ''' <param name="DocT">Id de Entidad</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="ParentId">Id que representa el comentario original</param>
    ''' <param name="IdMensaje">Id del Mensaje que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''     Tomas       07/05/2010  Modified
    '''     pablo       18/11/2010  Modified - Se trae metodo del branch BPN
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteMessage(ByVal IdMensaje As Int32)
        Server.Con.ExecuteNonQuery(CommandType.Text, "Delete From zforum where(IdMensaje=" & IdMensaje & ")")
        Server.Con.ExecuteNonQuery(CommandType.Text, "Delete From zforum_r_doc where(IdMensaje=" & IdMensaje & ")")
        Server.Con.ExecuteNonQuery(CommandType.Text, "Delete From zforum_r_usr where(IdMensaje=" & IdMensaje & ")")
    End Sub
    ''' <summary>
    ''' Obtiene la cantidad de mensajes que hay en total, tanto mensajes como respuestas, para el documento.
    ''' </summary>
    ''' <param name="DocId"></param>
    '''     pablo       18/11/2010  Modified - Se trae metodo del branch BPN
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCountAllMessages(ByVal DocId As Int64) As Int64
        Dim DSTEMP As DataSet
        Dim count As Int64

        If Server.isOracle = True Then
            Dim parValues() As Object = {DocId, 2}

            DSTEMP = Server.Con.ExecuteDataset("zsp_zforum_200.GetCountAllMessages", parValues)
            count = CInt(DSTEMP.Tables(0).Rows(0).Item(0))
        Else
            Dim parValues() As Object = {DocId}
            count = (Server.Con.ExecuteScalar("zsp_Forum_100_GetCountAllMessages", parValues))

        End If

        DSTEMP = Nothing
        Return count
    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega un mensaje al foro
    ''' </summary>
    ''' <param name="DocT">Id de Entidad</param>
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
    Public Shared Sub InsertMessage(ByVal DocId As Int64, ByVal Doctypeid As Int64, ByVal IdMensaje As Int32, ByVal ParentId As Int32, ByVal LinkName As String, ByVal Mensaje As String, ByVal Fecha As Date, ByVal UserId As Int32, ByVal StateId As Integer)
        Try
            If Mensaje.Length > 300 Then
                Mensaje = Mensaje.Substring(0, 299)
            End If
            If LinkName.Length > 60 Then
                LinkName = LinkName.Substring(0, 59)
            End If
            Dim strInsert As String = "Insert Into ZForum (IdMensaje,ParentId,LinkName,Mensaje,Fecha,UserId) Values (" & IdMensaje & "," & ParentId & ",'" & LinkName & "','" & Mensaje & "'," & Servers.Server.Con.ConvertDateTime(Fecha) & "," & UserId & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub InsertMessage(ByVal IdMensaje As Int32, ByVal ParentId As Int32, ByVal LinkName As String, ByVal Mensaje As String, ByVal UserId As Int64)
        If Mensaje.Length > 4000 Then
            Mensaje = Mensaje.Substring(0, 4000)
        End If
        If LinkName.Length > 250 Then
            LinkName = LinkName.Substring(0, 300)
        End If
        Dim strInsert As String = "Insert Into ZForum (IDMENSAJE,PARENTID,LINKNAME,MENSAJE,FECHA,USERID, DIASVTO) Values (" & IdMensaje.ToString & "," & ParentId.ToString & ",'" & LinkName & "','" & Mensaje & "'," & Servers.Server.Con.ConvertDateTime(Date.Now) & "," & UserId.ToString & ",0)"
        Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
    End Sub

    Public Shared Sub InsertMessageDoc(ByVal IdMensaje As Int32, ByVal DocId As Int64, ByVal Doctypeid As Int64)
        Dim strInsert As String = "Insert Into ZFORUM_R_DOC (IdMensaje,DocId,DocT) Values (" & IdMensaje.ToString & "," & DocId.ToString & "," & Doctypeid.ToString & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
    End Sub
    Public Shared Function GetRelatedDocs(ByVal parentId As Int32, ByVal docId As Int64) As DataTable
        Dim query As String = "select docid,doct from zforum_r_doc where idmensaje = " + parentId.ToString() + " and docid <> " + docId.ToString()
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function
    Public Shared Sub RemoveParticipants(ByVal IdMensaje As Int32)
        Dim strQuery As String = "DELETE ZFORUM_R_USR WHERE IdMensaje = " & IdMensaje.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, strQuery)
    End Sub
    Public Shared Sub RemoveParticipant(ByVal IdMensaje As Int32, ByVal UserId As Int64)
        Dim strQuery As String = "DELETE ZFORUM_R_USR WHERE IdMensaje = " & IdMensaje.ToString & " and IdUsuario= " & UserId
        Server.Con.ExecuteNonQuery(CommandType.Text, strQuery)
    End Sub
    Public Shared Sub InsertMessageParticipant(ByVal IdMensaje As Int32, ByVal UserId As Int64)
        Dim strInsert As String = "Insert Into ZFORUM_R_USR (IdMensaje,IdUsuario) Values (" & IdMensaje.ToString & "," & UserId.ToString & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
    End Sub
    Public Shared Sub InsertMessageParticipant(ByVal IdMensaje As Int32, ByVal UserId As Int64, ByRef t As Transaction)
        Dim strInsert As String = "Insert Into ZFORUM_R_USR (IdMensaje,IdUsuario) Values (" & IdMensaje.ToString & "," & UserId.ToString & ")"
        Server.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, strInsert)
    End Sub
    Public Shared Function GetMessageReplyParticipant(ByVal IdMensaje As Int32) As DataSet
        Dim ds As DataSet
        Dim strInsert As String = "Select Idusuario from ZFORUM_R_USR where IdMensaje = " & IdMensaje.ToString
        Return Server.Con.ExecuteDataset(CommandType.Text, strInsert)
    End Function




#Region "Mails enviados"
    ''' <summary>
    ''' Agrega los mails de los mensajes que fueron notificados.
    ''' </summary>
    Public Shared Sub InsertForumMail(ByVal docId As Int64, ByVal idMensaje As Int32, ByVal parentId As Int64, ByVal mails As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO ZFORUMMAILS VALUES (" + docId.ToString + "," + idMensaje.ToString + "," + parentId.ToString + ",'" + mails + "')")
    End Sub
    ''' <summary>
    ''' Obtiene los mails enviados al notificar un mensaje en el foro.
    ''' </summary>
    Public Shared Function GetForumMails(ByVal docId As Int64, ByVal idMensaje As Int32, ByVal parentId As Int64) As String
        Dim mails As Object = Server.Con.ExecuteScalar(CommandType.Text, "SELECT MAILS FROM ZFORUMMAILS WHERE DOCID = " + docId.ToString + " AND IDMENSAJE = " + idMensaje.ToString + " AND PARENTID = " + parentId.ToString)

        If IsNothing(mails) Then
            Return String.Empty
        Else
            Return mails.ToString()
        End If
    End Function
#End Region

#Region "Adjuntos"
    Public Shared Function GetAttachs(ByVal idMensaje As Int32) As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT PATH FROM ZFORUMATTACHS WHERE IDMENSAJE = " + idMensaje.ToString + " ORDER BY PATH").Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene los attach para la grilla del UC
    ''' </summary>
    ''' <param name="idMensaje"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBlobAttachs(ByVal idMensaje As Int32) As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT FILENAME AS PATH, FILENAME AS ADJUNTOS, IDMensaje FROM ZFORUMATTACHBLOB WHERE IDMENSAJE = " + idMensaje.ToString + " ORDER BY FILENAME").Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene todos los attach que no estan en blob
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetNonBlobAttachs() As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT IDMensaje, Path FROM ZFORUMATTACHBLOB where [File] IS null").Tables(0)
    End Function

    Public Shared Sub InsertAttach(ByVal idMensaje As Int32, ByVal path As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO ZFORUMATTACHS VALUES(" + idMensaje.ToString + ",'" + path.Replace("'", "''") + "')")
    End Sub
    Public Shared Function DeleteAttachs(ByVal idMensaje As Int32) As Integer
        Return Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE ZFORUMATTACHSBLOB WHERE IDMENSAJE = " + idMensaje.ToString())
    End Function

    ''' <summary>
    ''' Obtiene el attach de blob
    ''' </summary>
    ''' <param name="idMensaje"></param>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBlobAttachFileByName(ByVal idMensaje As Int32, ByVal FileName As String) As DataTable
        Dim pMessageId As SqlParameter
        pMessageId = New SqlParameter("@MessageId", SqlDbType.Int)
        pMessageId.Value = idMensaje

        Dim pFileName As SqlParameter
        pFileName = New SqlParameter("@FileName", SqlDbType.NVarChar, 200)
        pFileName.Value = FileName

        Dim params As IDbDataParameter() = {pMessageId, pFileName}

        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT [File] FROM ZFORUMATTACHBLOB WHERE IDMENSAJE = @MessageId AND [FileName] = @FileName", params).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene la ruta del attach
    ''' </summary>
    ''' <param name="idMensaje"></param>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAttachFileByName(ByVal idMensaje As Int32, ByVal FileName As String) As String
        Dim pMessageId As SqlParameter
        pMessageId = New SqlParameter("@MessageId", SqlDbType.Int)
        pMessageId.Value = idMensaje

        Dim pFileName As SqlParameter
        pFileName = New SqlParameter("@FileName", SqlDbType.NVarChar, 200)
        pFileName.Value = FileName

        Dim params As IDbDataParameter() = {pMessageId, pFileName}

        Return Server.Con.ExecuteScalar(CommandType.Text, "SELECT Path FROM ZFORUMATTACHBLOB WHERE IDMENSAJE = @MessageId AND [FileName] = @FileName", params).Tables(0)
    End Function

    ''' <summary>
    ''' Inserta los adjuntos como blob
    ''' </summary>
    ''' <param name="IdMenssage"></param>
    ''' <param name="file"></param>
    ''' <param name="maxlength"></param>
    ''' <param name="fileName"></param>
    ''' <param name="filePath"></param>
    ''' <remarks></remarks>
    Public Shared Sub InsertBlobAttach(ByVal IdMenssage As Int32, ByRef file As Byte(), ByVal maxlength As Int32, ByVal fileName As String, ByVal filePath As String)
        If file.Length > maxlength Then
            Throw New Exception("El archivo a insertar supera el máximo permitido.")
        End If

        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Dim pDocFile As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")

            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pDocFile = New SqlParameter("@DocFile", SqlDbType.Image)
            Else
                pDocFile = New SqlParameter("@DocFile", SqlDbType.VarBinary)
            End If

            pDocFile.Value = file

            Dim pMessageId As SqlParameter
            pMessageId = New SqlParameter("@MessageId", SqlDbType.Int)
            pMessageId.Value = IdMenssage

            Dim pFileName As SqlParameter
            pFileName = New SqlParameter("@FileName", SqlDbType.NVarChar, 200)
            pFileName.Value = fileName

            Dim pPath As SqlParameter
            pPath = New SqlParameter("@Path", SqlDbType.NVarChar, 1000)
            pPath.Value = filePath

            Dim params As IDbDataParameter() = {pDocFile, pMessageId, pFileName, pPath}

            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO ZFORUMATTACHBLOB(IdMensaje,[FileName],[File],[Path]) VALUES(@MessageId,@FileName,@DocFile, @Path)", params)
        End If
    End Sub


    ''' <summary>
    ''' Inserta el adjunto si ya existe
    ''' </summary>
    ''' <param name="IdMenssage"></param>
    ''' <param name="file"></param>
    ''' <param name="maxlength"></param>
    ''' <param name="fileName"></param>
    ''' <param name="filePath"></param>
    ''' <remarks></remarks>
    Public Shared Sub InsertBlobAttachInAExistRecord(ByVal IdMenssage As Int32, ByRef file As Byte(), ByVal maxlength As Int32, ByVal fileName As String, ByVal filePath As String)
        If file.Length > maxlength Then
            Throw New Exception("El archivo a insertar supera el máximo permitido.")
        End If

        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Dim pDocFile As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")

            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pDocFile = New SqlParameter("@DocFile", SqlDbType.Image)
            Else
                pDocFile = New SqlParameter("@DocFile", SqlDbType.VarBinary)
            End If

            pDocFile.Value = file

            Dim pMessageId As SqlParameter
            pMessageId = New SqlParameter("@MessageId", SqlDbType.Int)
            pMessageId.Value = IdMenssage

            Dim pFileName As SqlParameter
            pFileName = New SqlParameter("@FileName", SqlDbType.NVarChar, 200)
            pFileName.Value = fileName

            Dim pPath As SqlParameter
            pPath = New SqlParameter("@Path", SqlDbType.NVarChar, 1000)
            pPath.Value = filePath

            Dim params As IDbDataParameter() = {pDocFile, pMessageId, pFileName, pPath}

            Server.Con.ExecuteNonQuery(CommandType.Text, "update ZForumAttachBlob " +
                                                         "set [FileName] = @FileName,[File] = @DocFile,[Path] = @Path" +
                                                         "where IdMensaje = @MessageId", params)
        End If
    End Sub

    ''' <summary>
    ''' Valida si a existe el attach
    ''' </summary>
    ''' <param name="MessageID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAttachExist(ByVal MessageID As Integer) As Boolean
        Try
            Dim strSelect As String = "select count(1) from ZFORUMATTACHBLOB where IdMensaje = @IdMensaje"
            Dim pMessageId As SqlParameter
            pMessageId = New SqlParameter("@IdMensaje", SqlDbType.Int)
            pMessageId.Value = MessageID

            Dim params As IDbDataParameter() = {pMessageId}

            Dim count As Integer = Server.Con.ExecuteScalar(CommandType.Text, strSelect, params)

            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            raiseerror(ex)
            Return False
        End Try
    End Function
#End Region
End Class
