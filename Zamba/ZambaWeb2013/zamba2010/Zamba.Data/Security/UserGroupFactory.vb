Imports Zamba.Data
Imports Zamba
Imports Zamba.Core
Imports System.Text

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : Data.UserGroupFactory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Fabrica para trabajar con Grupos de Usuarios de Zamba
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class UserGroupFactory
    Inherits ZClass

#Region "Funciones Privadas"

    Private Sub Addgroup(ByVal usr As IUserGroup)
        Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO ZUSER_OR_GROUP (ID, UserType, UserName) VALUES ({0}, {1}, '{2}')", usr.ID, Int64.Parse(Usertypes.Group), usr.Name))
        Dim str As String = "insert into usrgroup(id,name) values(" & usr.ID & ",'" & usr.Name.Replace("'", "") & "')"
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, str)
        'hernan: porque comentastes esto ?? donde se carga ??
        'GroupTable.Add(usr.id, usr) ya se carga


    End Sub

#End Region

#Region "Funciones Publicas"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna un HASHTABLE con todos los grupos existentes en Zamba
    ''' </summary>
    ''' <returns>HASHTABLE</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna un ARRAYLIST con todos los grupos existentes en Zamba
    ''' </summary>
    ''' <returns>Arraylist de objetos UserGrouops</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------

    Public Function GetNewGroup(ByVal Name As String) As IUserGroup


        Dim group As New UserGroup
        group.Name = Name
        Try
            group.ID = CoreData.GetNewID(IdTypes.USERTABLEID)
        Catch ex As Exception
            Throw New Exception("Ocurrió un error al obtener el Id del grupo.", ex)
        End Try
        Try
            Addgroup(group)
        Catch ex As Exception
            Throw New Exception("Ocurrió un error registrar el grupo.", ex)
        End Try
        Return group
    End Function
    Public Function GetUserGroupsIds(ByVal usrid As Integer) As ArrayList
        Dim arr As New ArrayList
        Dim ds As DataSet

        If Server.isOracle Then
            Dim strselect As String = "Select GROUPID from usr_r_group where usrid = " & usrid & " UNION select inheritedusergroup from group_r_group where usergroup in ( Select groupid from usr_r_group where usrid = " & usrid & ")"
            ds = Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Else
            ds = Server.Con.ExecuteDataset("zsp_100_Groups_GetGroupsIds", New Object() {usrid})
        End If

        For Each r As DataRow In ds.Tables(0).Rows
            'TODO: Ver si no existe el grupo  If GroupTable.ContainsKey(CInt(r("GROUPID"))) Then
            arr.Add(r("GROUPID"))
            ' End If
        Next
        Return arr
    End Function



    Public Sub DeleteGroupRights(ByVal groupid As Int32)
        Try
            Dim sql As String = "Delete from Usr_Rights where GROUPID=" & groupid
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch
        End Try
    End Sub

#End Region


    Public Overrides Sub Dispose()

    End Sub



End Class