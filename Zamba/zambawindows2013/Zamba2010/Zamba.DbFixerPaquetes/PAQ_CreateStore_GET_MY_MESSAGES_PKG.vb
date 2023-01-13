Imports zamba.Servers

Public Class PAQ_CreateStore_GET_MY_MESSAGES_PKG
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("14/05/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "1.6.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea el Stored Procedure para verificar los mensajes recibidos"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateStore_GET_MY_MESSAGES_PKG"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStore_GET_MY_MESSAGES_PKG
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property

    Public ReadOnly Property orden() As Int64 Implements IPAQ.orden
        Get
            Return 71
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        If Server.ServerType = Server.DBTYPES.MSSQLServer7Up OrElse Server.ServerType = Server.DBTYPES.MSSQLServer Then
            Return Me.execSql(GenerateScripts)
        Else
            Return Me.execOracle(GenerateScripts)
        End If
    End Function
    Private Function execOracle(ByVal generatescripts As Boolean) As Boolean
        Try
            Dim sql As System.Text.StringBuilder
            sql = New System.Text.StringBuilder
            sql.Append("CREATE OR REPLACE PACKAGE GET_MY_MESSAGES_PKG AS")
            sql.Append("TYPE t_cursor IS REF CURSOR;")
            sql.Append("PROCEDURE getMymessages(my_id IN USRTABLE.id%TYPE ,io_cursor OUT t_cursor);")
            sql.Append("END;")
            If generatescripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = Nothing
            sql = New System.Text.StringBuilder
            sql.Append("CREATE OR REPLACE PACKAGE BODY GET_MY_MESSAGES_PKG AS")
            sql.Append(ControlChars.NewLine)
            sql.Append("PROCEDURE getMymessages(my_id IN USRTABLE.id%TYPE ,io_cursor OUT t_cursor)IS")
            sql.Append(ControlChars.NewLine)
            sql.Append("v_cursor t_cursor;")
            sql.Append(ControlChars.NewLine)
            sql.Append("BEGIN")
            sql.Append(ControlChars.NewLine)
            sql.Append("OPEN v_cursor FOR SELECT MESSAGE.msg_id,")
            sql.Append(ControlChars.NewLine)
            sql.Append("MESSAGE.msg_from,")
            sql.Append(ControlChars.NewLine)
            sql.Append("USRTABLE.name User_Name,")
            sql.Append(ControlChars.NewLine)
            sql.Append("MESSAGE.subject,")
            sql.Append(ControlChars.NewLine)
            sql.Append("MESSAGE.msg_date,")
            sql.Append(ControlChars.NewLine)
            sql.Append("MESSAGE.reenvio,")
            sql.Append(ControlChars.NewLine)
            sql.Append("MESSAGE.deleted,")
            sql.Append(ControlChars.NewLine)
            sql.Append("MSG_DEST.user_id DEST,")
            sql.Append(ControlChars.NewLine)
            sql.Append("MSG_DEST.user_name DEST_NAME,")
            sql.Append(ControlChars.NewLine)
            sql.Append("MSG_DEST.dest_type,")
            sql.Append(ControlChars.NewLine)
            sql.Append("MSG_DEST.READ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("Message.msg_body")
            sql.Append(ControlChars.NewLine)
            sql.Append("FROM Message, MSG_DEST, USRTABLE")
            sql.Append(ControlChars.NewLine)
            sql.Append("WHERE message.msg_id=msg_dest.msg_id and")
            sql.Append(ControlChars.NewLine)
            sql.Append("message.msg_from=usrtable.id and")
            sql.Append(ControlChars.NewLine)
            sql.Append("message.msg_id in(SELECT msg_id FROM MSG_DEST where user_id=my_id AND")
            sql.Append(ControlChars.NewLine)
            sql.Append("deleted=0);")
            sql.Append(ControlChars.NewLine)
            sql.Append("io_cursor := v_cursor;")
            sql.Append(ControlChars.NewLine)
            sql.Append("END getmymessages;")
            sql.Append(ControlChars.NewLine)
            sql.Append("END Get_My_Messages_Pkg;")
            If generatescripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If

            sql = Nothing

            Return True
        Catch
            Return False
        End Try
    End Function
    Private Function execSql(ByVal generatescripts As Boolean) As Boolean
        'TODO store: SPGetMyMessages (existe como Getmymessages)
        Try
            Dim sql As System.Text.StringBuilder
            sql = New System.Text.StringBuilder
            sql.Append("CREATE procedure getmymessages")
            sql.Append(ControlChars.NewLine)
            sql.Append("@my_id INT")
            sql.Append(ControlChars.NewLine)
            sql.Append("AS")
            sql.Append(ControlChars.NewLine)
            sql.Append("Select message.msg_id, message.msg_from, usrtable.description as Name, message.subject, message.msg_date, message.reenvio, message.deleted, msg_dest.User_id as DEST, msg_dest.dest_type, [msg_dest].[read], message.msg_body, msg_dest.User_NAME as dest_name from message,msg_dest,usrtable where message.msg_id = msg_dest.msg_id and usrtable.id= message.msg_from and message.msg_id in (select msg.msg_id from message msg,msg_dest where  msg.msg_id = msg_dest.msg_id and msg_dest.User_id=@my_id and msg_dest.deleted=0)")
            If generatescripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = Nothing
            Return True
        Catch
            Return False
        End Try
    End Function

#End Region

End Class
