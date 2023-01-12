Imports Zamba.Core
Imports System.Text
Imports Zamba.Servers

Public Class UserFactoryExt
    ''' <summary>
    ''' Obtiene todos los usuarios de uno o mas grupos
    ''' </summary>
    ''' <returns>ArrayList de objetos User. Solo se completa el ID y NAME del usuario.</returns>
    ''' <remarks>Este método es copia de Zamba.Data.Security.UserFactory.GetGroupUsers(ByVal GroupId As Integer).
    '''          Para obtener todos los usuarios completos crear otro método</remarks>
    Public Function GetUserIdAndNameByGroupIds(ByVal groupIds As ArrayList) As ArrayList
        Dim users As New ArrayList
        Dim sbQuery As New StringBuilder
        Dim ds As DataSet

        'Se construye la consulta
        sbQuery.Append("SELECT DISTINCT(U.ID),U.NAME FROM USRTABLE U INNER JOIN USR_R_GROUP R ON U.ID=R.USRID WHERE R.GROUPID IN(")
        For i As Int16 = 0 To groupIds.Count - 1
            sbQuery.Append(groupIds(i))
            sbQuery.Append(",")
        Next
        sbQuery = sbQuery.Remove(sbQuery.Length - 1, 1)
        sbQuery.Append(")")

        'Se obtienen los usuarios sin repetir de todos los grupos
        ds = Server.Con.ExecuteDataset(CommandType.Text, sbQuery.ToString)
        For Each r As DataRow In ds.Tables(0).Rows
            Dim usr As New User
            usr.ID = r("ID")
            usr.Name = r("NAME")
            users.Add(usr)
        Next
        Return users
    End Function

    Public Function CheckUserNameAvailability(ByVal name As String) As Object
        Dim query As String = "SELECT COUNT(1) FROM USRTABLE WHERE NAME='" & name & "'"
        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

    Public Function GetUserAndGroupsMailsByUserOrGroupId(ids As List(Of Int64)) As String
        Dim query As String = String.Format("select correo from zmailconfig A where a.userid in ({0}) or exists ( select 1 from usr_r_group C  where a.userid = c.usrid and c.groupid  in ({0}) ) or exists ( select 1 from group_r_group D, usr_r_group E where  d.USERGROUP = e.groupid and e.usrid = a.userid and D.INHERITEDUSERGROUP  in ({0}) ) ", String.Join(",", ids.ToArray()))
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query)
        Dim Mails As String
        If (ds IsNot Nothing AndAlso ds.Tables.Count > 0) Then
            For Each r As DataRow In ds.Tables(0).Rows
                Mails = Mails & r(0).ToString & ","
            Next
            If Mails.Length > 0 Then Mails = Mails.Remove(Mails.Length - 1, 1)
            Return Mails
        End If
        Return String.Empty
    End Function
End Class
