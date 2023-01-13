Imports Zamba.Core
'Imports Zamba.Users.Factory
Public Class UserGroupComponentFactory

#Region "Funciones Privadas"
    Private Shared Sub Addgroup(ByVal usr As IUserGroup)
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO ZUSER_OR_GROUP (ID, UserType, name) VALUES ({0}, {1}, '{2}')", usr.ID, Int64.Parse(Usertypes.Group), usr.Name))
        Dim str As String = "insert into usrgroup(id,name) values(" & usr.ID & ",'" & usr.Name.Replace("'", "") & "')"
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, str)
    End Sub
#End Region

#Region "Funciones Publicas"
    Public Shared Function GetGroupsArray() As ArrayList
        Dim groups As New ArrayList
        Dim strselect As String = "select * from usrgroup order by name"
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)

        For Each r As DataRow In ds.Tables(0).Rows
            Dim group As New UserGroup
            group.ID = r("ID")

            Dim temp As Object = r("NAME")
            Try
                If temp Is DBNull.Value Then
                    group.Name = String.Empty
                Else
                    group.Name = temp
                End If
            Catch
                group.Name = String.Empty
            End Try

            temp = r("DESCRIPCION")
            Try
                If temp Is DBNull.Value Then
                    group.Description = String.Empty
                Else
                    group.Description = temp
                End If
            Catch
                group.Description = String.Empty
            End Try
            temp = Nothing

            groups.Add(group)
        Next
        Return groups
    End Function
    Public Shared Function GetGroupAdminReport() As DataTable
        Return Server.Con.ExecuteDataset(CommandType.StoredProcedure, "ZSP_ADMIN_100_GetGroups").Tables(0)
    End Function
    Public Shared Function GetGroups(Optional ByVal filter As String = "", Optional ByVal Order As String = "NAME") As Hashtable
        Dim hsGroupTable As Hashtable = New Hashtable
        Dim strselect As String = "select * from usrgroup " & filter & " order by " & Order
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        For Each r As DataRow In ds.Tables(0).Rows
            Dim ug As New UserGroup
            ug.ID = r("ID")

            Dim temp As Object = r("NAME")
            Try
                If temp Is DBNull.Value Then
                    ug.Name = String.Empty
                Else
                    ug.Name = temp
                End If
            Catch
                ug.Name = String.Empty
            End Try

            temp = r("DESCRIPCION")
            Try
                If temp Is DBNull.Value Then
                    ug.Description = String.Empty
                Else
                    ug.Description = temp
                End If
            Catch
                ug.Description = String.Empty
            End Try
            temp = Nothing

            If hsGroupTable.ContainsKey(ug.ID) Then
                hsGroupTable(ug.ID) = ug
            Else
                hsGroupTable.Add(ug.ID, ug)
            End If
        Next
        Return hsGroupTable
    End Function


    Public Shared Sub DeleteGroup(ByVal ugID As Int64)
        Dim strDel As String = "delete usr_r_group where groupid = " & ugID
        Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
        strDel = "Delete from UsrGroup where ID=" & ugID
        Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
    End Sub
    Public Shared Sub DeleteGroupRights(ByVal groupid As Int32)
        Dim sql As String = "Delete from Usr_Rights where GROUPID=" & groupid
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

#End Region

    Public Shared Function RemoveUser(ByVal ug As iusergroup, ByVal u As iuser) As Boolean
        If ug.Users.Contains(u) Then
            UserFactory.RemoveUser(u, ug)
            ug.Users.Remove(u)
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function AssignUser(ByVal u As iuser, ByVal ug As iusergroup) As Boolean
        If Not ug.Users.Contains(u) Then
            UserFactory.AssignGroup(u, ug)
            ug.Users.Add(u)
            Return True
        Else
            Return False
        End If
    End Function

End Class