Imports Zamba
Imports Zamba.Servers
Imports System.Text
Imports Zamba.Data
Imports Zamba.Core
Public Class UserGroupsComponent
    Inherits ZClass
    '    Implements IUser

    Public Event LogError(ByVal ex As Exception)

    Public Shared ReadOnly Property GroupName(ByVal Group_Id As Integer) As String
        Get
            Dim strSelect As String = ("SELECT User_Group_Name FROM User_Group where ( User_Group_Id = " & Group_Id & ")")
            Try
                Return Server.Con.ExecuteScalar(CommandType.Text, strSelect).ToString()
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property
    Public Shared Function AddUserGroup(ByVal NewGroup As iusergroup) As Integer

        '   Dim table As String = "User_Group"
        Dim ColState As Integer
        If NewGroup.State = True Then
            ColState = 1
        Else
            ColState = 0
        End If

        Dim Columns As String = "User_GROUP_Id, User_GROUP_Name, User_GROUP_State, User_GROUP_CrDate"
        Dim Values As String = CoreData.GetNewID(Zamba.Core.IdTypes.USERGROUPID) & ",'" & NewGroup.Name & "', " & ColState & "," & "CONVERT(DATETIME, '" & NewGroup.CreateDate & "', 102)"
        Dim strInsert As String


        If Server.IsOracle Then
            strInsert = "INSERT INTO User_group (" & Columns & ") VALUES (" & Values & ")"
            strInsert = utilities.Convert_Datetime(strInsert)
        Else
            strInsert = "INSERT INTO User_group (" & Columns & ") VALUES (" & Values & ")"
        End If
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al agregar el Grupo de usuarios" & ex.ToString, "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        Return NewGroup.Id
    End Function
    Public Shared Sub UpdateUserGroup(ByVal Usergroup As iusergroup)
        Dim ColState As Integer
        If Usergroup.State = True Then
            ColState = 1
        Else
            ColState = 0
        End If

        Dim Updatestring As String = "User_group_Name = '" & Usergroup.Name & "', User_group_State = " & ColState
        Dim strUpdate As String = "UPDATE User_group SET " & Updatestring & " WHERE USER_group_ID = " & Usergroup.Id & ""
        If Server.IsOracle Then
            strUpdate = utilities.Convert_Datetime(strUpdate)
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
    End Sub
    Public Shared Sub DelUserGroup(ByVal UsergroupID As Integer)
        Dim strDelete As String = "DELETE from User_group where (user_group_id = " & UsergroupID & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
    End Sub
    Public Shared Function GetUserGroups() As DSUserGroup
        Dim DsUserGroups As New DSUserGroup
        Dim DSTEMP As DataSet
        Dim StrSelect As String = "SELECT User_group_ID, User_group_Name, User_group_State, user_group_CrDate FROM User_group ORDER BY User_group_Name"
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = "User_Group"
        DsUserGroups.Merge(DSTEMP)
        Return DsUserGroups
    End Function
    Public Shared Function GetUserGroupsArrayList() As ArrayList
        Dim DsUserGroups As New DSUserGroup
        Dim DSTEMP As DataSet
        Dim StrSelect As String = "SELECT User_group_ID, User_group_Name, User_group_State, user_group_CrDate FROM User_group ORDER BY User_group_Name"
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = "User_Group"
        DsUserGroups.Merge(DSTEMP)
        Dim UserGroups As New ArrayList
        Dim i As Int32
        For i = 0 To DsUserGroups.User_Group.Count - 1
            UserGroups.Add(New UserGroup(DsUserGroups.User_Group(i).User_Group_ID, DsUserGroups.User_Group(i).User_Group_Name, DsUserGroups.User_Group(i).User_Group_State))
        Next
        Return UserGroups
    End Function
    Public Shared Function IsUserGroupDuplicated(ByVal UserGroupName As String) As Boolean
        Try
            Dim strSelect As String = "SELECT COUNT(User_group_id) from USER_group where (User_group_Name = '" & UserGroupName.Trim & "')"
            Dim Qrows As Int32 = Server.Con.ExecuteScalar(CommandType.Text, strSelect)
            If Qrows > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al consultar la duplicidad del Grupo de Usuario " & ex.ToString, "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

#Region "Members"
    Public Shared Sub AddMember(ByVal UserId As Int32, ByVal UserGroupId As Int32)
        Dim StrInsert As String = "INSERT INTO USER_R_USER_GROUP (USER_ID,USER_GROUP_ID) VALUES (" & UserId & "," & UserGroupId & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
    End Sub
    Public Shared Sub DelMember(ByVal UserId As Int32, ByVal UserGroupId As Int32)
        Dim StrInsert As String = "DELETE FROM USER_R_USER_GROUP WHERE USER_ID = " & UserId & " AND USER_GROUP_ID = " & UserGroupId & ""
        Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
    End Sub
#End Region


    Public Overrides Sub Dispose()

    End Sub
End Class
