Imports Zamba.Data
Public Class UserGroupBusinessExt
    Inherits UserGroupBusiness

    ''' <summary>
    ''' Obtiene los grupos de donde hereda y los devuelve como una lista de Icore
    ''' </summary>
    ''' <param name="groupID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInheritedGroups(ByVal groupID As Int64) As List(Of ICore)
        Dim usrGroupFactoryExt As New UserGroupFactoryExt()
        Dim dt As DataTable
        Try
            Dim lstGroups As New List(Of ICore)
            dt = usrGroupFactoryExt.GetInheritedGroups(groupID)
            For Each row As DataRow In dt.Rows
                Dim group As UserGroup = New UserGroup()
                group.ID = row("ID")
                group.Name = row("Name")
                lstGroups.Add(group)
            Next
            Return lstGroups
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
            raiseerror(ex)
        Finally
            usrGroupFactoryExt = Nothing
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los grupos de donde hereda y los devuelve como una lista de Icore
    ''' </summary>
    ''' <param name="groupID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNonInheritedGroups(ByVal groupID As Int64) As List(Of ICore)
        Dim usrGroupFactoryExt As New UserGroupFactoryExt()
        Dim dt As DataTable
        Try
            Dim lstGroups As List(Of ICore) = New List(Of ICore)
            dt = usrGroupFactoryExt.GetNonInheritedGroups(groupID)
            For Each row As DataRow In dt.Rows
                Dim group As UserGroup = New UserGroup()
                group.ID = row("ID")
                group.Name = row("Name")
                If Not IsDBNull(row("Descripcion")) Then
                    group.Description = row("Descripcion")
                Else
                    group.Description = String.Empty
                End If
                lstGroups.Add(group)
            Next
            Return lstGroups
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
            raiseerror(ex)
        Finally
            usrGroupFactoryExt = Nothing
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Inserta el grupo o los grupos de donde va a heredar
    ''' </summary>
    ''' <param name="group1"></param>
    ''' <param name="group2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function insertInheritedGroup(ByVal group1 As Int64, ByVal group2 As Int64) As Boolean
        Dim usrGroupFactoryExt As UserGroupFactoryExt = New UserGroupFactoryExt()
        Return usrGroupFactoryExt.InsertInheritedGroups(group1, group2)
    End Function
    ''' <summary>
    ''' Elimina los grupos de los cuales ya no va a heredar.
    ''' </summary>
    ''' <param name="group1"></param>
    ''' <param name="group2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function deleteInheritedGroup(ByVal group1 As Int64, ByVal group2 As Int64) As Boolean
        Dim usrGroupFactoryExt As UserGroupFactoryExt = New UserGroupFactoryExt()
        Return usrGroupFactoryExt.DeleteInheritedGroups(group1, group2)
    End Function
    ''' <summary>
    ''' Elimina todos los grupos de donde hereda. Se utiliza cuando se va a eliminar el grupo.
    ''' </summary>
    ''' <param name="group"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteAllInheritedGroups(ByVal group As Int64) As Boolean
        Dim usrGroupFactoryExt As UserGroupFactoryExt = New UserGroupFactoryExt()
        Return usrGroupFactoryExt.DeleteAllInheritedGroups(group)
    End Function
    ''' <summary>
    ''' Obtiene todos los grupos hijos de los cuales lo heredan. Quienes lo heredan.
    ''' </summary>
    ''' <param name="Groupid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getOffspring(ByVal Groupid As Int64, ByVal isReload As Boolean) As List(Of ICore)
        SyncLock (Cache.UsersAndGroups.hsOffspringGroups)
            If isReload Then
                If Cache.UsersAndGroups.hsOffspringGroups.ContainsKey(Groupid) Then
                    Cache.UsersAndGroups.hsOffspringGroups.Remove(Groupid)
                End If
            End If
            If Cache.UsersAndGroups.hsOffspringGroups.ContainsKey(Groupid) = False Then
                Cache.UsersAndGroups.hsOffspringGroups.Add(Groupid, getOffspringOfGroup(Groupid))
            End If
            Return Cache.UsersAndGroups.hsOffspringGroups(Groupid)
        End SyncLock
    End Function

    Function getOffspringOfGroup(ByVal Groupid As Int64) As List(Of ICore)
        Dim usrGroupFactoryExt As UserGroupFactoryExt = New UserGroupFactoryExt()
        Dim lstGroups As List(Of ICore) = New List(Of ICore)
        Dim dt As DataTable = usrGroupFactoryExt.getOffspring(Groupid)
        If dt.Rows.Count <> 0 Then
            For Each row As DataRow In dt.Rows
                Dim group As UserGroup = New UserGroup
                group.ID = row("ID")
                group.Name = row("Name")
                lstGroups.Add(group)
                Dim lst As List(Of ICore) = getOffspringOfGroup(group.ID)
                For Each child As ICore In lst
                    If Not lstGroups.Contains(DirectCast(child, Zamba.Core.UserGroup), New CoreComparer) Then
                        lstGroups.Add(child)
                        Dim a As UserGroup
                    End If
                Next
            Next
        End If
        Return lstGroups
    End Function



End Class
