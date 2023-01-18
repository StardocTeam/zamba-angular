Imports Zamba.Core
Imports System.Data.SqlClient

Public NotInheritable Class MessagesFactory
    Inherits ZClass

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve un Dataset tipeado con los Mensajes que tiene un usuario en la bandeja de mensajes 
    ''' </summary>
    '''     ''' <param name="UserId">Id del Usuario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getMessages(ByVal UserId As Int32) As dsMessages
        Dim ds As New DataSet
        Dim dsMsg As New dsMessages
        If Server.IsOracle Then
            Dim parnames As String() = {"my_id", "io_cursor"}
            Dim parvalues As String() = {UserId, 2}
            Dim partypes As String() = {13, 5}
            'ds = Server.Con.ExecuteDataset("get_my_messages_pkg.getmymessages",  parvalues)
            ds = Server.Con.ExecuteDataset("zsp_messages_100.GetMyMessages", parvalues)
        Else
            Dim parvalues As String() = {UserId}
            'ds = Server.Con.ExecuteDataset("getmymessages", parvalues)
            ds = Server.Con.ExecuteDataset("zsp_messages_100_GetMyMessages", parvalues)
        End If

        ds.Tables(0).TableName = dsMsg.Messages.TableName
        dsMsg.Merge(ds)
        Return dsMsg
    End Function

    Public Shared Function GetMessageFile(ByVal ID As Long) As Byte()
        Try

            Dim file As Byte()
            Dim blobFile As Object = Server.Con.ExecuteScalar(CommandType.Text, String.Format("SELECT EncodeFile FROM ZMAIL_HISTORY WHERE ID= {0} and EncodeFile is not null", ID))
            If Not blobFile Is Nothing Then
                file = DirectCast(blobFile, Byte())
            End If
            blobFile = Nothing
            Return file
        Catch ex As Exception
            raiseerror(ex)
            Return Nothing
        End Try

    End Function

    'Ver de hacer un stored sobre el proceso.
    Public Shared Sub SaveMessageFile(ByVal file As Byte(), ByVal url As String)
        Dim sql As String = "UPDATE ZMAIL_HISTORY SET ENCODEFILE=@mFile WHERE PATH='" & url & "'"

        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Dim pDocFile As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")

            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pDocFile = New SqlParameter("@mFile", SqlDbType.Image)
            Else
                pDocFile = New SqlParameter("@mFile", SqlDbType.VarBinary)
            End If

            pDocFile.Value = file
            Dim params As IDbDataParameter() = {pDocFile}

            Server.Con.ExecuteNonQuery(CommandType.Text, sql, params)
        End If
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un entero que representa la cantidad de mensajes no leidos por el usuario
    ''' </summary>
    ''' <param name="User">Objeto usuario del cual se obtendran los mensajes no leídos</param>
    ''' <returns>Numero Entero</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function countNewMessages(ByVal UserID As Int64) As Integer
        Dim ds As New DataSet
        If Server.isOracle Then
            ''Dim parNames() As Object = {"userId", "io_cursor"}
            Dim parValues() As Object = {UserID, 2}
            'Dim partype() As Object = {13, 5}
            ds = Server.Con.ExecuteDataset("zsp_messages_100.CountNewMessages", parValues)
        Else
            Dim parValues() As Object = {UserID}
            ds = Server.Con.ExecuteDataset("zsp_messages_100_CountNewMessages", parValues)
        End If
        Return CInt(ds.Tables(0).Rows(0).Item(0))
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset Tipeado con los adjuntos de los mensajes para un usuario
    ''' </summary>
    ''' <param name="user_id">ID del usuario</param>
    ''' <returns>Dataset DSAttach</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getMyMessageAttachs(ByVal user_id As Integer) As DSAttach
        Dim ds As New DataSet

        If Server.isOracle Then
            Dim parvalues() As Object = {user_id, 2}
            ds = Server.Con.ExecuteDataset("zsp_messages_100.GetMyAttachments", parvalues)
        Else
            Dim parvalues() As Object = {user_id}
            ds = Server.Con.ExecuteDataset("zsp_messages_100_GetMyAttachments", parvalues)
        End If

        Dim dsatt As New DSAttach
        ds.Tables(0).TableName = dsatt.MSG_ATTACH.TableName
        dsatt.Merge(ds)

        Return dsatt
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para eliminar un mensaje determinado
    ''' </summary>
    ''' <param name="idMessage">Id del mensaje que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteMessage(ByVal idMessage As Int32)
        Try
            Dim strDelete As String

            strDelete = "delete from msg_attach where msg_id =" & idMessage
            Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)

            strDelete = "delete from msg_dest where msg_id =" & idMessage
            Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)

            strDelete = "Delete from message where msg_id=" & idMessage
            Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)

        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el path para la libreta de direcciones (XML) para un usuario determinado
    ''' </summary>
    ''' <param name="usr_id">Id del Usuario</param>
    ''' <returns>String, ruta deseada</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getAddressBookPath(ByVal usr_id As Int64) As String

        If Server.isOracle Then
            Dim path As String = Server.Con.ExecuteScalar(CommandType.Text, "Select ADDRESS_BOOK from Usrtable where id=" & usr_id)
            Return path
        Else
            Dim sql As String = "Select ADDRESS_BOOK from Usrtable where id=" & usr_id
            Dim path As String = Server.Con.ExecuteScalar(CommandType.Text, sql)
            Return path
        End If
    End Function

    'Public Shared ReadOnly Property SMTP()
    '    Get
    '        Return Server.smtp
    '    End Get
    'End Property


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un objeto "AutoMail" en base al Id
    ''' </summary>
    ''' <param name="Id">Id con que se conoce al "AutoMail"</param>
    ''' <returns>Objeto AutoMail</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------

    Public Shared Function GetAutomailNameByid(ByVal Id As Int32) As String
        Dim strselect As String = "select Name from AutoMail where Id=" & Id
        Dim Nombre As String = Server.Con.ExecuteScalar(CommandType.Text, strselect)
        Return Nombre
    End Function
    Public Overrides Sub Dispose()
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que cambia el estado del mensaje actual a leido
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetAsRead(ByVal Id As Int64, ByVal UserId As Int32)
        Dim updatestr As String
        Try
            If Server.isOracle Then
                updatestr = "update msg_dest set read = 1 where msg_id=" & Id.ToString & " and user_id=" & UserId 'Me.Owner_User_ID.ToString
            Else
                updatestr = "update msg_dest set [read] = 1 where msg_id=" & Id.ToString & " and user_id=" & UserId 'Me.Owner_User_ID.ToString
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, updatestr)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub


    Public Shared Sub InsertAttach(ByVal Id As Int64, ByVal attachID As Int64, ByVal attachDocTypeID As Int64, ByVal attachIndex As Int32, ByVal attachName As String, ByVal attachIconId As Int32, ByVal attachDiskGroupId As Int32, ByVal attachDocFile As String, ByVal attachOffSet As Int32, ByVal attachDISKVOLPATH As String)
        If Server.isOracle Then
            Dim parValues() As Object = {Id, attachID, attachDocTypeID, 0, attachIndex, attachName, attachIconId, attachDiskGroupId, attachDocFile, attachOffSet, attachDISKVOLPATH}
            Server.Con.ExecuteNonQuery("zsp_messages_100.InsertAttach", parValues)
        Else
            Dim parValues() As Object = {Id, attachID, attachDocTypeID, 0, attachIndex, attachName, attachIconId, attachDiskGroupId, attachDocFile, attachOffSet, attachDISKVOLPATH}
            Server.Con.ExecuteNonQuery("zsp_messages_100_InsertAttach", parValues)
            parValues = Nothing
        End If
    End Sub


    Public Shared Sub MessageRegister(ByVal Id As Int64, ByVal De As Int32, ByVal Body As String, ByVal Subject As String, ByVal ConfirmChar As Int32)
        If Server.isOracle Then
            ''Dim parNames() As String = {"m_id", "m_from", "m_Body", "m_subject", "m_resend"}
            Dim parTypes() As String = {"13", "13", "7", "7", "13"}
            Dim parValues() As String = {Id, De, Body, Subject, ConfirmChar}
            Server.Con.ExecuteNonQuery("zsp_messages_100.InsertMsg", parValues)
        Else
            Dim parValues() As Object = {Id, De, Body, Subject, ConfirmChar}
            Server.Con.ExecuteNonQuery("zsp_messages_100_InsertMsg", parValues)
        End If
    End Sub


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Cancela el mensaje creado
    ''' </summary>
    ''' <remarks>
    ''' Se utiliza si se produce una falla en el envio del mensaje
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub rollBack(ByVal Id As Int64)
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, "Delete From msg_attach where msg_id=" & Id)
            Server.Con.ExecuteNonQuery(CommandType.Text, "Delete From msg_dest where msg_id=" & Id)
            Server.Con.ExecuteNonQuery(CommandType.Text, "Delete From message where msg_id=" & Id)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub


    Public Shared Sub RegisterMailToUser(ByVal mailId As Integer, ByVal userId As Integer, ByVal mailType As MessageType, ByVal userName As String)
        If Server.IsOracle Then
            Dim parValues() As Object = {mailId, userId, CInt(mailType), userName}
            Server.Con.ExecuteNonQuery("zsp_messages_100.InsertMsgDest", parValues)
        Else
            Dim parValues() As Object = {mailId, userId, CInt(mailType), userName}
            Server.Con.ExecuteNonQuery("zsp_messages_100_InsertMsgDest", parValues)
        End If
    End Sub


    Public Shared Sub InsertAutoMail(ByVal am As AutoMail)
        Dim InsertQuery As New System.Text.StringBuilder
        InsertQuery.Append("INSERT INTO automail(id,name,mailto,cc,cco,MailFrom,body,subject,confirmation) VALUES(")
        InsertQuery.Append(am.ID)
        InsertQuery.Append(",'")
        InsertQuery.Append(am.Name)
        InsertQuery.Append("'")
        InsertQuery.Append(",'")
        InsertQuery.Append(am.MailTo)
        InsertQuery.Append("'")
        InsertQuery.Append(",'")
        InsertQuery.Append(am.CC)
        InsertQuery.Append("'")
        InsertQuery.Append(",'")
        InsertQuery.Append(am.CCO)
        InsertQuery.Append("'")
        InsertQuery.Append(",'")
        InsertQuery.Append(am.From)
        InsertQuery.Append("'")
        InsertQuery.Append(",'")
        InsertQuery.Append(am.Body)
        InsertQuery.Append("'")
        InsertQuery.Append(",'")
        InsertQuery.Append(am.Subject)
        InsertQuery.Append("'")
        InsertQuery.Append(",")
        InsertQuery.Append(CInt(am.Confirmation))
        InsertQuery.Append(")")

        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, InsertQuery.ToString)
    End Sub

    ''' <summary>
    ''' Método que sirve para guardar un automail
    ''' </summary>
    ''' <param name="automail"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	07/08/2008	Modified  
    ''' </history>
    Public Shared Sub SaveAutomail(ByVal automail As AutoMail)

        Dim UpdateQuery As New System.Text.StringBuilder
        UpdateQuery.Append("UPDATE AutoMail SET ")
        UpdateQuery.Append(" CC ='")
        UpdateQuery.Append(automail.CC)
        UpdateQuery.Append("',CCO ='")
        UpdateQuery.Append(automail.CCO)
        UpdateQuery.Append("',MailTO='")
        UpdateQuery.Append(automail.MailTo)
        UpdateQuery.Append("',Name='")
        UpdateQuery.Append(automail.Name)
        UpdateQuery.Append("',Body='")
        UpdateQuery.Append(automail.Body.Trim())
        UpdateQuery.Append("',Subject='")
        UpdateQuery.Append(automail.Subject)
        UpdateQuery.Append("',Confirmation=")
        UpdateQuery.Append(CInt(automail.Confirmation).ToString())
        UpdateQuery.Append(",MailFrom='")
        UpdateQuery.Append(automail.From)
        UpdateQuery.Append("', PathImages ='")

        For Each strImage As String In automail.PathImages
            UpdateQuery.Append(strImage & ";")
        Next

        UpdateQuery.Append("', PathFiles ='")
        For Each file As String In automail.AttachmentsPaths
            UpdateQuery.Append(file & ";")
        Next

        UpdateQuery.Append("' Where id=")
        UpdateQuery.Append(automail.ID.ToString())

        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, UpdateQuery.ToString)

    End Sub

    Public Shared Function GetAutomailList() As DataSet
        Dim strselect As String = "SELECT * FROM AutoMail ORDER BY AutoMail.Name"
        'Dim ir As IDataReader = Server.Con.ExecuteReader(CommandType.Text, strselect)
        Dim ir As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Return ir
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un objeto AutoMail de la base de datos
    ''' </summary>
    ''' <param name="Am">Objeto que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub RemoveAutomail(ByVal AutomailId As Int32)
        Dim DeleteQuery As String = "DELETE AutoMail WHERE id = " & AutomailId.ToString()
        Server.Con.ExecuteNonQuery(CommandType.Text, DeleteQuery)
    End Sub



    Public Shared Sub DeleteMessageRecived(ByVal Id As Int64, ByVal UserId As Int32)
        Dim parValues() As Object = {Id, UserId}

        If Server.IsOracle Then
            Server.Con.ExecuteNonQuery("zsp_messages_100.DeleteRecivedMsg", parValues)
        Else
            Server.Con.ExecuteNonQuery("zsp_messages_100_DeleteRecivedMsg", parValues)
        End If
    End Sub

    Public Shared Sub DeleteMessageSended(ByVal Id As Int64)

        If Server.IsOracle Then
            Dim parValues() As Object = {Id}
            Server.Con.ExecuteNonQuery("zsp_messages_100.DeleteSenderMsg", parValues)
        Else
            'TODO Falta SQL SERVER
        End If
    End Sub

    Public Shared Function isAlreadyRead(ByVal msgid As Int32, ByVal usr As String) As Boolean
        Dim Readed As String
        Dim strSelect As String = "select READ from msg_dest where msg_id=" & msgid.ToString & " and user_name='" & usr & "'"
        Readed = Servers.Server.Con.ExecuteScalar(CommandType.Text, strSelect)
        Return Readed
    End Function

    Public Shared Function GetAutomailById(ByVal Id As Int32) As DataSet
        Dim strselect As String = "select * from AutoMail where Id=" & Id
        Return Server.Con.ExecuteDataset(CommandType.Text, strselect)
    End Function

    Public Shared Function GetAutomailAttachments(ByVal id As Int32) As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT Path FROM AutomailAttachment where id = " + id.ToString())
    End Function

End Class
