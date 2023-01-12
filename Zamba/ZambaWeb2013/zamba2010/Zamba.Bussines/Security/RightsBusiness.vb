Imports Zamba.Data
Imports System.Text
Imports Zamba.Servers
Imports Zamba.Core.Caching
Imports Zamba.Membership
Imports System.Collections.Generic

Public Class RightsBusiness

    Dim RF As New RightFactory

    Public Function GetUserRights(ByVal UserId As Int64, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Int64 = -1) As Boolean
        Try
            Dim IRight As Boolean = RF.GetUserRights(UserId, ObjectId, RType, AditionalParam)
            If Not IsDBNull(IRight) AndAlso IRight Then ' redo
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    'Public Function GetUserRightsGroup(ByVal UserId As Int64, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Int64 = -1) As Boolean
    '    Try
    '        Dim IRight As Boolean = RF.GetUserRightsGroup(UserId, ObjectId, RType, AditionalParam)
    '        If Not IsDBNull(IRight) AndAlso IRight Then ' redo
    '            Return True
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Function

    Public Function GetUserRights(ByVal UserId As Int64, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, ByVal AditionalParam As List(Of Int64)) As Boolean
        Try
            Dim IRight As Boolean = RF.GetUserRights(UserId, ObjectId, RType, AditionalParam)
            If Not IsDBNull(IRight) AndAlso IRight Then ' redo
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function
    Public Function GetSpecificAttributeRight(ByVal User As IUser, ByVal docTypeID As Long) As Boolean
        Dim UserGroupBusiness As New UserGroupBusiness
        Return RF.GetSpecificAttributeRight(User, docTypeID, UserGroupBusiness.getUserGroups(User.ID))
        UserGroupBusiness = Nothing
    End Function

    Public Sub SaveAction(ByVal ObjectId As Int64,
    ByVal ObjectType As Zamba.ObjectTypes,
    ByVal ActionType As Zamba.Core.RightsType,
    Optional ByVal S_Object_ID As String = "",
    Optional ByVal _userid As Int64 = 0)
        RF.SaveAction(ObjectId, ObjectType, ActionType, S_Object_ID, _userid)
    End Sub



    Public Sub SaveActionWebView(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, Optional ByVal S_Object_ID As String = "", Optional ByVal _currUserid As Int64 = 0)

        Try
            RF.SaveActionWebView(ObjectId, ObjectType, Environment.MachineName, ActionType, S_Object_ID, _currUserid)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub




    ''' <summary>
    ''' Obtiene los tipos de documento de un archivo determinado dependiendo de los permisos del usuario y de sus grupos
    ''' </summary>
    ''' <param name="Doc_GroupID">Id de archivo</param>
    ''' <returns>DsDoctypeRight</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 11/08/2011  Modified    Se optimizó la manera de obtener los resultados de los archivos. Ahora se obtiene  
    '''                                     todo con una consulta en vez de múltiples consultas por cada usuario y grupo.
    ''' </history>
    Public Function GetDocTypeUserRightFromArchive(ByVal Doc_GroupID As Int64) As DsDoctypeRight
        'Dim ids As New ArrayList
        'Dim key As String
        'Dim ds As New DsDoctypeRight
        'For Each g As IUserGroup In Membership.MembershipHelper.CurrentUser.Groups
        '    ids.Add(g.ID)
        'Next
        'ids.Add(Membership.MembershipHelper.CurrentUser.ID)
        'For Each id As Int64 In ids
        '    key = id.ToString & "," & Doc_GroupID.ToString
        '    If Caching.Cache.Item(CacheType.UsersRights, CacheSubType.DocTypeUserRightFromArchive, key) Is Nothing Then
        '        Caching.Cache.Add(CacheType.UsersRights, CacheSubType.DocTypeUserRightFromArchive, key, RF.GetDocTypeUserRightFromArchive(id, Doc_GroupID))
        '    End If
        '    ds.Merge(DirectCast(Caching.Cache.Item(CacheType.UsersRights, CacheSubType.DocTypeUserRightFromArchive, key), DsDoctypeRight))
        'Next
        'Return ds

        Dim key As String = Doc_GroupID.ToString
        Dim returnValue = Nothing

        'Javier Paez: Si es web, trabaja con la session como cache sino utiliza la cache antigua.
        If MembershipHelper.isWeb Then
            returnValue = MembershipHelper.GetSessionItem(CacheType.UsersRights, CacheSubType.DocTypeUserRightFromArchive, key)
            If returnValue Is Nothing Then
                returnValue = RF.GetDocTypeUserRightFromArchive(Membership.MembershipHelper.CurrentUser.ID, Doc_GroupID)
                MembershipHelper.SetSessionItem(CacheType.UsersRights, CacheSubType.DocTypeUserRightFromArchive, key, returnValue)
                Return returnValue
            Else
                Return returnValue
            End If
        Else
            'If Caching.Cache.Item(CacheType.UsersRights, CacheSubType.DocTypeUserRightFromArchive, key) Is Nothing Then
            'Caching.Cache.Add(CacheType.UsersRights, CacheSubType.DocTypeUserRightFromArchive, key, RF.GetDocTypeUserRightFromArchive(Membership.MembershipHelper.CurrentUser.ID, Doc_GroupID))
            'End If

            Return RF.GetDocTypeUserRightFromArchive(Membership.MembershipHelper.CurrentUser.ID, Doc_GroupID)
        End If
    End Function

    Public Function GetArchivosUserRight() As DataSet

        'If Caching.Cache.Item(CacheType.Archivos, CacheSubType.UserRight, Membership.MembershipHelper.CurrentUser.ID) Is Nothing Then
        '        Caching.Cache.Add(CacheType.Archivos, CacheSubType.UserRight, Membership.MembershipHelper.CurrentUser.ID, RF.GetArchivosUserRight(Membership.MembershipHelper.CurrentUser.ID))
        'End If

        Return RF.GetArchivosUserRight(Membership.MembershipHelper.CurrentUser.ID)

        'Return RF.GetArchivosUserRight(CurrentUser.ID)
    End Function


    Public Sub ClearHashTables()
        RF.ClearHashTables()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ParentDocType"></param>
    ''' <param name="DocTypeId"></param>
    ''' <param name="GID"></param>
    ''' <param name="WithCache"></param>
    ''' <returns></returns>
    Public Function GetAssociatedGridColumnsRightsCombined(userID As Long, docTypeID As Long, docTypeParentID As Long, withCache As Boolean) As List(Of String)
        Try
            Dim ListColumnsRights As New List(Of String)
            Dim GroupsUsersIds As New List(Of Long)
            ' Cargo los grupos a los cuales pertenece el usuario
            GroupsUsersIds = UserFactory.GetUserGroupsIdsByUserid(userID, False)

            ' Se agrega id de usuario
            GroupsUsersIds.Add(userID)

            For Each _userID As Long In GroupsUsersIds
                Dim rights As DataTable = (New UserBusiness).GetAssociateGridColumnsRight(_userID, docTypeID, docTypeParentID, withCache)
                If rights IsNot Nothing AndAlso rights.Rows.Count > 0 Then
                    For Each row As DataRow In rights.Rows
                        ListColumnsRights.Add(row.Item("COLUMN_NAME"))
                    Next
                End If
            Next

            Return ListColumnsRights
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

End Class





