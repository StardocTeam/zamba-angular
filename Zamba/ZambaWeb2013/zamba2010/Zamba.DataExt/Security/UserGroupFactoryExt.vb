Imports Zamba.Core
Imports System.Text
Imports Zamba.Servers
Public Class UserGroupFactoryExt

    ''' <summary>
    ''' Obtiene los grupos de los cual hereda 
    ''' </summary>
    ''' <param name="groupid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInheritedGroups(ByVal groupid As Int64) As DataTable
        Try
            If Server.isOracle Then
                Dim strbuilder As New StringBuilder()
                strbuilder.Append("select group_r_group.inheritedUserGroup as ")
                strbuilder.Append(Chr(34))
                strbuilder.Append("ID")
                strbuilder.Append(Chr(34))
                strbuilder.Append(", usrgroup.Name from group_r_group inner join usrgroup on usrgroup.id =")
                strbuilder.Append("group_r_group.inheritedUserGroup where UserGroup = ")
                strbuilder.Append(groupid)
                Return Server.Con.ExecuteDataset(CommandType.Text, strbuilder.ToString()).Tables(0)
            Else
                Dim parameters() As Object = {groupid}
                Return Server.Con.ExecuteDataset("zsp_100_GetInheritedGroups", parameters).Tables(0)
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex)
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los grupos de los cual no se hereda ni heredan de
    ''' </summary>
    ''' <param name="groupid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNonInheritedGroups(ByVal groupid As Int64) As DataTable
        Try
            If Server.isOracle Then
                Dim strbuilder As New StringBuilder()
                strbuilder.Append("select usrgroup.id, usrgroup.Name, usrgroup.Descripcion")
                strbuilder.Append(" from usrgroup where id <> ")
                strbuilder.Append(groupid)
                strbuilder.Append(" and id not in (select usergroup from group_r_group where inheritedUserGroup = ")
                strbuilder.Append(groupid)
                strbuilder.Append(") and id not in (select inheritedUserGroup from group_r_group where usergroup = ")
                strbuilder.Append(groupid)
                strbuilder.Append(")")
                Return Server.Con.ExecuteDataset(CommandType.Text, strbuilder.ToString()).Tables(0)
            Else
                Dim parameters() As Object = {groupid}
                Return Server.Con.ExecuteDataset("zsp_100_GetNonInheritedGroups", parameters).Tables(0)
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex)
            ZClass.raiseerror(ex)
        End Try
    End Function


    ''' <summary>
    ''' Inserta los grupos de los cuales va a heredar
    ''' </summary>
    ''' <param name="groupid1"></param>
    ''' <param name="groupid2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function InsertInheritedGroups(ByVal groupid1 As Int64, ByVal groupid2 As Int64) As Boolean
        Try
            Dim parameters() As Object = {groupid1, groupid2}
            Server.Con.ExecuteNonQuery("zsp_100_InsertInheritedGroups", parameters)
            Return True
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex)
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Elimina los grupos de los cuales va a dejar de heredar.
    ''' </summary>
    ''' <param name="groupid1"></param>
    ''' <param name="groupid2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteInheritedGroups(ByVal groupid1 As Int64, ByVal groupid2 As Int64) As Boolean
        Try
            Dim parameters() As Object = {groupid1, groupid2}
            Server.Con.ExecuteNonQuery("zsp_100_DeleteInheritedGroups", parameters)
            Return True
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex)
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Elimina todos los grupos de los cuales hereda. Esto se utiliza para cuando se va a eliminar el grupo.
    ''' </summary>
    ''' <param name="groupid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteAllInheritedGroups(ByVal groupid As Int64) As Boolean
        Try
            If Server.isOracle Then
                Server.Con.ExecuteNonQuery(CommandType.Text, "delete group_r_group where usergroup = " & groupid)
            Else
                Dim parameters() As Object = {groupid}
                Server.Con.ExecuteNonQuery("zsp_100_DeleteAllInheritedGroups", parameters)
            End If

            Return True
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex)
            ZClass.raiseerror(ex)
            Return False
        End Try

    End Function

    'Function getInheritance(ByVal groupid As Int64) As DataTable
    '    If Server.isOracle Then
    '        Dim strbuilder As New StringBuilder()
    '        strbuilder.Append("select distinct group_r_group.inheritedUserGroup as ID,usrgroup.name  from group_r_group ")
    '        strbuilder.Append("inner join usrgroup on group_r_group.inheritedUserGroup = usrgroup.ID ")
    '        strbuilder.Append("where usergroup = ")
    '        strbuilder.Append(groupid)

    '        Return Server.Con.ExecuteDataset(CommandType.Text, strbuilder.ToString()).Tables(0)
    '    Else
    '        Dim parameters() As Object = {groupid}
    '        Return Server.Con.ExecuteDataset("zsp_100_getInheritance", parameters).Tables(0)
    '    End If
    'End Function

    Function getOffspring(ByVal groupid As Int64) As DataTable
        If Server.isOracle Then
            Dim strbuilder As New StringBuilder()
            strbuilder.Append("select distinct group_r_group.usergroup as ID ,usrgroup.name  from group_r_group inner join usrgroup ")
            strbuilder.Append("on group_r_group.usergroup = usrgroup.ID ")
            strbuilder.Append("where inheritedUserGroup = ")
            strbuilder.Append(groupid)

            Return Server.Con.ExecuteDataset(CommandType.Text, strbuilder.ToString()).Tables(0)
        Else
            Dim parameters() As Object = {groupid}
            Return Server.Con.ExecuteDataset("zsp_100_getOffspring", parameters).Tables(0)
        End If
    End Function
End Class