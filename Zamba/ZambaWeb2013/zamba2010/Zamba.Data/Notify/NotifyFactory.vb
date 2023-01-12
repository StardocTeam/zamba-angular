
Imports System
Imports Zamba.Core
Imports System.Text
Imports System.Collections

Public Class NotifyFactory

    Public Shared Function GetMailToNotify(ByVal parentID As Int32) As String
        Dim mail As String
        mail = Server.Con.ExecuteScalar(CommandType.Text, "SELECT Correo FROM ZMailConfig WHERE UserID = (SELECT userID FROM zforum WHERE idmensaje = " + parentID.ToString + ")")
        Return mail
    End Function

    Public Shared Function GetUserToNotify(ByVal typeid As GroupToNotifyTypes, ByVal Id As Int64) As DataSet
        Dim ds As New DataSet
        Dim _typeId As Int16 = typeid
        ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT UserId FROM Z_GroupToNotify where TypeId = " + _typeId.ToString() + " AND DocId = " + Id.ToString + " AND UserId  is not Null")
        Return ds
    End Function

    Public Shared Function GetUserGroupToNotify(ByVal typeId As GroupToNotifyTypes, ByVal docId As Int64) As DataSet
        Dim tempDS As New DataSet
        Dim _typeId As Int16 = typeId
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT GroupId FROM Z_GroupToNotify WHERE TypeId = ")
        sqlBuilder.Append(_typeId.ToString())
        sqlBuilder.Append(" AND DocId = ")
        sqlBuilder.Append(docId.ToString())

        tempDS = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return tempDS

    End Function
    Public Shared Function GetUserGroupToNotify(ByVal typeId As GroupToNotifyTypes, ByVal docIds As Generic.List(Of Int64)) As DataSet
        Dim tempDS As New DataSet
        Dim _typeId As Int16 = typeId
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT GroupId FROM Z_GroupToNotify WHERE TypeId = ")
        sqlBuilder.Append(_typeId.ToString())
        sqlBuilder.Append(" AND DOCID IN (")
        For Each id As Int64 In docIds
            sqlBuilder.Append(id.ToString)
            sqlBuilder.Append(",")
        Next
        sqlBuilder.Remove(sqlBuilder.Length - 1, 1)
        sqlBuilder.Append(")")

        tempDS = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return tempDS

    End Function

    Public Shared Function GetUserToNotify(ByVal typeid As GroupToNotifyTypes, ByVal Ids As Generic.List(Of Int64)) As DataSet
        Dim ds As New DataSet
        Dim _typeId As Int16 = typeid
        Dim query As String = "SELECT UserId FROM Z_GroupToNotify where TypeId = " + _typeId.ToString() + " AND UserId  is not Null AND DOCID IN ("
        For Each id As Int64 In Ids
            query += id.ToString + ","
        Next
        query = query.Remove(query.Length - 1, 1)
        query += ")"

        ds = Server.Con.ExecuteDataset(CommandType.Text, query)
        Return ds
    End Function

    Public Shared Function GetGroupExternalMails(ByVal typeid As GroupToNotifyTypes, ByVal Id As Int64) As ArrayList
        Dim Users As New ArrayList
        Dim ds As New DataSet
        Dim _typeId As Int16 = typeid
        ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT ExtraData FROM Z_GroupToNotify where TypeId = " + _typeId.ToString() + " AND DocId = " + Id.ToString + " AND ExtraData <> ''")
        For Each r As DataRow In ds.Tables(0).Rows
            Users.Add(r.Item(0))
        Next
        Return Users
    End Function
    Public Shared Function GetGroupExternalMails(ByVal typeid As GroupToNotifyTypes, ByVal Ids As Generic.List(Of Int64)) As ArrayList
        Dim Users As New ArrayList
        Dim ds As New DataSet
        Dim _typeId As Int16 = typeid
        Dim query As String = "SELECT ExtraData FROM Z_GroupToNotify where TypeId = " + _typeId.ToString() + " AND ExtraData <> '' AND DOCID IN ("

        For Each id As Int64 In Ids
            query += id.ToString + ","
        Next
        query = query.Remove(query.Length - 1, 1)
        query += ")"

        ds = Server.Con.ExecuteDataset(CommandType.Text, query)
        For Each r As DataRow In ds.Tables(0).Rows
            Users.Add(r.Item(0))
        Next
        Return Users
    End Function

    Public Shared Function ValidateGroupToNotifyExist(ByVal topicId As Int64) As Boolean

        Dim sqlBuilder As New StringBuilder()
        Dim resultCount As Int16

        Try
            sqlBuilder.Append("SELECT COUNT (GroupId) FROM Z_GroupToNotify WHERE GroupId = '")
            sqlBuilder.Append(topicId.ToString())
            sqlBuilder.Append("'")

            'Oracle inserta el SemiColon (;) automáticamente asi que lo
            'agregamos solo en caso SQL
            Select Case Servers.Server.ServerType
                Case DBTypes.MSSQLServer
                    sqlBuilder.Append(";")
                Case DBTypes.MSSQLServer7Up
                    sqlBuilder.Append(";")
                Case Else
            End Select

            resultCount = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())

            If resultCount > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return False

    End Function

    Public Shared Sub SetNewUserToNotify(ByVal typeid As GroupToNotifyTypes, ByVal _groupId As Int64, ByVal _userId As Int64)
        Dim _typeid As Int16 = typeid
        Dim sqlBuilder As New StringBuilder()
        Try
            sqlBuilder.Append("INSERT INTO Z_GroupToNotify (TypeId, DocId, UserId, ExtraData, GroupId) VALUES (")
            sqlBuilder.Append(_typeid.ToString())
            sqlBuilder.Append(", ")
            sqlBuilder.Append(_groupId.ToString())
            sqlBuilder.Append(", ")
            sqlBuilder.Append(_userId.ToString())
            sqlBuilder.Append(", '',0)")
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub SetNewUserGroupToNotify(ByVal typeid As GroupToNotifyTypes, ByVal _groupId As Int64, ByVal _userGroupId As Int64)
        Dim _typeid As Int16 = typeid
        Dim sqlBuilder As New StringBuilder()
        Try
            sqlBuilder.Append("INSERT INTO Z_GroupToNotify (TypeId, DocId, UserId, ExtraData, GroupId) VALUES (")
            sqlBuilder.Append(_typeid.ToString())
            sqlBuilder.Append(", ")
            sqlBuilder.Append(_groupId.ToString())
            sqlBuilder.Append(", 0, '', ")
            sqlBuilder.Append(_userGroupId.ToString())
            sqlBuilder.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub SetNewMailToNotify(ByVal typeid As GroupToNotifyTypes, ByVal _groupId As Int64, ByVal _mail As String)

        Dim _typeid As Int16 = typeid

        Dim sqlBuilder As New StringBuilder()
        Try

            sqlBuilder.Append("INSERT INTO Z_GroupToNotify (TypeId, DocId, UserId, ExtraData, GroupId) VALUES (")
            sqlBuilder.Append(_typeid.ToString())
            sqlBuilder.Append(", ")
            sqlBuilder.Append(_groupId.ToString())
            sqlBuilder.Append(", 0, '")
            sqlBuilder.Append(_mail)
            sqlBuilder.Append("', 0)")

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Public Shared Sub DeleteUserToNotify(ByVal _groupId As Int64, ByVal _userId As Int64)
        Dim sqlBuilder As New StringBuilder()
        sqlBuilder.Append("DELETE FROM Z_GroupToNotify WHERE DocId = ")
        sqlBuilder.Append(_groupId.ToString())
        sqlBuilder.Append(" AND UserId = ")
        sqlBuilder.Append(_userId.ToString())
        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
    End Sub

    Public Shared Sub DeleteUserGroupToNotify(ByVal _groupId As Int64, ByVal _userGroupId As Int64)
        Dim sqlBuilder As New StringBuilder()
        sqlBuilder.Append("DELETE FROM Z_GroupToNotify WHERE DocId = ")
        sqlBuilder.Append(_groupId.ToString())
        sqlBuilder.Append(" AND GroupId = ")
        sqlBuilder.Append(_userGroupId.ToString())
        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
    End Sub

    Public Shared Sub DeleteMailToNotify(ByVal _groupId As Int64, ByVal _mail As String)
        Dim sqlBuilder As New StringBuilder()
        sqlBuilder.Append("DELETE FROM Z_GroupToNotify WHERE DocId = ")
        sqlBuilder.Append(_groupId.ToString())
        sqlBuilder.Append(" AND ExtraData = '")
        sqlBuilder.Append(_mail)
        sqlBuilder.Append("'")
        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
    End Sub

    Public Shared Sub DeleteNotify(ByVal documentID As Int64, ByVal typeNotify As GroupToNotifyTypes)
        Dim sqlBuilder As New StringBuilder
        sqlBuilder.Append("DELETE FROM Z_GroupToNotify WHERE DocId = ")
        sqlBuilder.Append(documentID.ToString())
        sqlBuilder.Append("AND TypeId = ")
        sqlBuilder.Append(Convert.ToInt16(typeNotify).ToString())
        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
    End Sub

    ''' <summary>
    ''' Metodo | Valida si el usuario ya pertenece al Grupo de Notificacion indicado. Si ya pertenece devuelve True. 
    ''' </summary>
    Public Shared Function ValidateUserInGroupToNotify(ByVal _groupId As Int64, ByVal _userId As Int64) As Boolean

        Dim sqlBuilder As New StringBuilder()
        Dim resultCount As Int16

        Try
            sqlBuilder.Append("SELECT COUNT UserId FROM Z_GroupToNotify WHERE UserID = '")
            sqlBuilder.Append(_userId.ToString())
            sqlBuilder.Append("' AND GroupId = '")
            sqlBuilder.Append(_groupId.ToString())
            sqlBuilder.Append("'")

            resultCount = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())

            If resultCount > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return True

    End Function

    Public Shared Function GetAllData(ByVal doc_id As Int64) As DataSet
        Dim query As New StringBuilder
        query.Append("SELECT * FROM Z_GroupToNotify WHERE DocId = ")
        query.Append(doc_id.ToString)
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
    End Function

    Public Shared Sub SaveAllData(ByVal doc_id As Int64, ByVal typeid As Int32, ByVal userid As Int64, ByVal extradata As String, ByVal groupid As Int64)
        Dim query As New StringBuilder
        query.Append("INSERT INTO Z_GroupToNotify (TypeId , DocId, UserId, ExtraData, GroupId)")
        query.Append("VALUES(")
        query.Append(typeid.ToString)
        query.Append(", ")
        query.Append(doc_id.ToString)
        query.Append(", ")
        query.Append(userid.ToString)
        query.Append(", '")
        query.Append(extradata)
        query.Append("', ")
        query.Append(groupid.ToString)
        query.Append(")")

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)

    End Sub


End Class
