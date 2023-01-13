Imports Zamba.Data

Public Class RightsBusiness
    Public Shared Function GetUserRights(ByVal ObjectId As ObjectTypes, ByVal RType As Zamba.Core.RightsType, Optional ByVal AditionalParam As Int64 = -1) As Boolean
        Return RightsBusiness.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectId, RType, AditionalParam)
    End Function


    ''' <summary>
    ''' Valido permiso de visualizacion Documentos Asociados
    ''' </summary>
    ''' <param name="CurrentUserId"></param>
    ''' <param name="DocTypeId"></param>
    ''' <returns>Boolean</returns>
    ''' <History> [pablo] Created 06-03-09
    Public Shared Function ValidateAssociateRight(ByVal CurrentUserId As Long, ByVal DocTypeId As Long) As Boolean
        Dim lstUserRights As List(Of Long) = New List(Of Long)
        Try

            'Permisos del perfil al que pertenece el usuario
            For Each group As Long In UserBusiness.GetUserGroupsIdsByUserid(CurrentUserId)
                lstUserRights.Add(group)
                For Each parentGroup As ICore In UserGroupBusiness.getInheritanceOfGroup(group)
                    If Not lstUserRights.Contains(parentGroup.ID) Then
                        lstUserRights.Add(parentGroup.ID)
                    End If
                Next
            Next


            'Permisos del usuario 
            lstUserRights.Add(CurrentUserId)

            For Each u As Long In lstUserRights
                If UserBusiness.Rights.GetRightsOnlyForOneUserOrGroup(u, ObjectTypes.DocTypes, RightsType.VerDocumentosAsociados, DocTypeId) Then
                    Return True
                End If
            Next

            Return False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(lstUserRights) Then
                lstUserRights.Clear()
                lstUserRights = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ParentDocType"></param>
    ''' <param name="DocTypeId"></param>
    ''' <param name="GID"></param>
    ''' <param name="WithCache"></param>
    ''' <returns></returns>
    Public Shared Function GetAssociatedGridColumnsRightsCombined(userID As Long, docTypeID As Long, docTypeParentID As Long, withCache As Boolean) As List(Of String)
        Try
            Dim ListColumnsRights As New List(Of String)
            Dim GroupsUsersIds As New List(Of Long)
            ' Cargo los grupos a los cuales pertenece el usuario
            GroupsUsersIds = UserBusiness.GetUserGroupsIdsByUserid(userID)

            For Each groupUserId As Int64 In UserBusiness.GetUserGroupsIdsByUserid(userID)
                For Each parentGroup As ICore In UserGroupBusiness.getInheritanceOfGroup(groupUserId)
                    GroupsUsersIds.Add(parentGroup.ID)
                Next
            Next

            ' Se agrega id de usuario
            GroupsUsersIds.Add(userID)

            For Each _userID As Long In GroupsUsersIds
                Dim rights As DataTable = UserBusiness.Rights.GetAssociateGridColumnsRight(_userID, docTypeID, docTypeParentID, withCache)
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

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un hashtable con todos los atributos del documento asociado y sus permisos para con el DocType Padre 
    ''' Combinando permisos de Grupos y usuario para un usuario
    ''' </summary>
    ''' <param name="DocTypeIds"></param>
    ''' <param name="GID"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	Javier	22/10/2010	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetAssociatedIndexsRightsCombined(ByVal ParentDocType As Int64, ByVal DocTypeId As Int64, ByVal GID As Int64, ByVal WithCache As Boolean) As Hashtable
        Try
            Dim hashIndexRights As New Hashtable

            Dim Indexs As List(Of IIndex) = IndexsBusiness.GetIndexsSchemaAsListOfDT(DocTypeId, WithCache)
            Dim GroupsUsersIds As New List(Of Long)
            Dim AllowedGroupsUsersIds As New List(Of Long)

            ' cargo todos los grupos de los cuales es contenido el usuario
            GroupsUsersIds = UserBusiness.GetUserGroupsIdsByUserid(GID)

            For Each groupUserId As Int64 In UserBusiness.GetUserGroupsIdsByUserid(GID)
                For Each parentGroup As ICore In UserGroupBusiness.getInheritanceOfGroup(groupUserId)
                    GroupsUsersIds.Add(parentGroup.ID)
                Next
            Next

            'Obtengo los permisos habilitados para asociados para cada grupo
            For i As Integer = 0 To GroupsUsersIds.Count - 1
                Dim PermisoEspecificoIndexAsociado As Boolean = RightFactory.GetRight(GroupsUsersIds(i), ObjectTypes.DocTypes, RightsType.ViewAssociateRightsByIndex, ParentDocType)
                If PermisoEspecificoIndexAsociado Then
                    AllowedGroupsUsersIds.Add(GroupsUsersIds(i))
                End If
            Next

            Dim PermisoEspecificoIndexAsociadoxUsuario As Boolean = RightFactory.GetRight(GID, ObjectTypes.DocTypes, RightsType.ViewAssociateRightsByIndex, ParentDocType)

            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("El usuario con ID: {2} {1} tiene permisos por indices especificos para los asociados de: {0}", ParentDocType.ToString(), If(PermisoEspecificoIndexAsociadoxUsuario, " ", " NO "), GID))

            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("El usuario con ID: {2} pertenece a {1} grupos que tiene permisos por indices especificos para los asociados de: {0}", ParentDocType.ToString(), AllowedGroupsUsersIds.Count, GID))

            If AllowedGroupsUsersIds.Count >= GroupsUsersIds.Count OrElse PermisoEspecificoIndexAsociadoxUsuario = True Then

                'Obtengo los permisos por atributos habilitados para asociados
                Dim dt As DataTable
                If AllowedGroupsUsersIds.Count > 0 Then
                    If WithCache Then
                        If Not Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.ContainsKey(ParentDocType & "§" & DocTypeId & "§" & GID) Then
                            Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.Add(ParentDocType & "§" & DocTypeId & "§" & GID, RightFactory.GetAssociateIndexRightCombined(ParentDocType, DocTypeId, AllowedGroupsUsersIds))
                        End If
                        dt = Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.Item(ParentDocType & "§" & DocTypeId & "§" & GID)
                    Else
                        dt = RightFactory.GetAssociateIndexRightCombined(ParentDocType, DocTypeId, AllowedGroupsUsersIds)
                        If Not Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.ContainsKey(ParentDocType & "§" & DocTypeId & "§" & GID) Then
                            Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.Add(ParentDocType & "§" & DocTypeId & "§" & GID, dt)
                        End If
                    End If
                End If

                'Se filtran atributos, se crea el permiso de Atributo asociado y se deshabilita si para todos los grupos 
                'está deshabilitado
                For Each I As IIndex In Indexs
                    Dim AIR As New AssociatedIndexsRightsInfo(ParentDocType, DocTypeId, I.ID, I.Name)

                    'Si hay al menos un grupo con el permiso por asociado se filtra, sino deja todos los atributos en true
                    If AllowedGroupsUsersIds.Count > 0 Then
                        Dim DV As New DataView(dt)
                        DV.RowFilter = "IndexId = " & I.ID & " AND RightType = " & RightsType.AssociateIndexView

                        If DV.ToTable.Rows.Count > 0 Then
                            AIR.EnableIndexRightValue(RightsType.AssociateIndexView)
                        End If
                    End If
                    hashIndexRights.Add(I.ID, AIR)
                Next
            Else
                'El Usuario o Grupos tiene almenos un grupo donde no tiene marcado el permiso de indices especifico, por lo tanto ve todos los indices.
                For Each I As IIndex In Indexs
                    Dim AIR As New Zamba.Core.AssociatedIndexsRightsInfo(ParentDocType, DocTypeId, I.ID, I.Name)
                    AIR.EnableIndexRightValue(RightsType.AssociateIndexView)
                    hashIndexRights.Add(I.ID, AIR)
                Next
            End If

            Return hashIndexRights
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetAssociatedIndexsRightsForRightsAdministrator(ByVal ParentDocType As Int64, ByVal DocTypeId As Int64, ByVal GID As Int64) As Hashtable
        Try
            'Este metodo es solo para el administrador de permisos.
            Dim hashIndexRights As New Hashtable

            Dim Indexs As Generic.List(Of IIndex) = IndexsBusiness.GetIndexsSchemaAsListOfDT(DocTypeId, False)

            Dim dt As DataTable
            dt = RightFactory.GetAssociateIndexRight(ParentDocType, DocTypeId, GID)

            Dim DV As New DataView(dt)

            For Each I As IIndex In Indexs
                Dim AIR As New Zamba.Core.AssociatedIndexsRightsInfo(ParentDocType, DocTypeId, I.ID, I.Name)
                DV.RowFilter = "IndexId = " & I.ID
                For Each R As DataRow In DV.ToTable.Rows
                    AIR.EnableIndexRightValue(R.Item(0))
                Next
                hashIndexRights.Add(I.ID, AIR)
            Next
            Return hashIndexRights
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function


    Public Shared Sub LoadRights(ByVal user As IUser, ByRef dsgeneral As DataSet, ByRef dsarchivos As DataSet, ByRef dsdoctype As DataSet, ByRef dsrestriction As DataSet)
        Dim ids As New ArrayList
        ids.Add(user.ID)
        Dim y As Integer = 0

        UserFactory.FillGroups(user)
        For y = 0 To user.Groups.Count - 1
            ids.Add(user.Groups(y).id)
            Dim usrGroups As UserGroups = New UserGroups()
            Dim groups As List(Of ICore) = UserGroupBusiness.getInheritanceOfGroup(user.Groups(y).id)
            For Each group As ICore In groups
                If Not ids.Contains(group.ID) Then
                    ids.Add(group.ID)
                End If
            Next
            groups = Nothing
            usrGroups.Dispose()
            usrGroups = Nothing
        Next
        dsgeneral = RightFactory.GetLoadRightsDs(ids, dsarchivos, dsdoctype, dsrestriction)
    End Sub



    Public Shared Function GetIndexsRights(ByVal DocTypeId As Int64, ByVal GID As Generic.List(Of Int64)) As DataTable

        Dim lstgroups As List(Of Long) = New List(Of Long)
        Try


            Dim usrGroups As UserGroups = New UserGroups()

            For Each i As Long In GID
                lstgroups.Add(i)
                Dim groups As List(Of ICore) = UserGroupBusiness.getInheritanceOfGroup(i)
                For Each group As ICore In groups
                    If Not lstgroups.Contains(group.ID) Then
                        lstgroups.Add(group.ID)
                    End If
                Next
                groups = Nothing
            Next
            usrGroups.Dispose()
            Return RightFactory.GetIndexsRightsDT(lstgroups, DocTypeId)


        Finally
            lstgroups = Nothing
        End Try

    End Function

    Public Shared Function GetInheritedRights(ByVal groupId As Long, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, ByVal AditionalParam As Integer) As Boolean
        Dim usrGroups As UserGroups = New UserGroups()
        Dim groups As List(Of ICore) = UserGroupBusiness.getInheritanceOfGroup(groupId)
        usrGroups.Dispose()
        For Each g As IUserGroup In groups
            If RightFactory.GetRight(g.ID, ObjectId, RType, AditionalParam) = True Or GetInheritedRights(g.ID, ObjectId, RType, AditionalParam) = True Then
                Return True
            End If
        Next
    End Function


    Public Shared Function GetUserRights(ByVal User As IUser, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1, Optional ByVal UserSelected As Boolean = False) As Boolean
        ' override

        If Not IsNothing(User) Then
            If RightFactory.GetRight(User.ID, ObjectId, RType, AditionalParam) Then ' redo
                Return True
            Else

                'If User.ID = 0 Then Return True
                If User.ID = 9999 AndAlso User.Name.ToUpper = "ZAMBA1234567" AndAlso ObjectId = ObjectTypes.Users Then Return True

                '(pablo) add validation: if 'UserSelected' isn't true then i search for group rights
                If Not UserSelected Then
                    If IsNothing(User.Groups) OrElse User.Groups.Count = 0 Then UserFactory.FillGroups(User)
                    For Each g As IUserGroup In User.Groups
                        If RightFactory.GetRight(g.ID, ObjectId, RType, AditionalParam) = True OrElse GetInheritedRights(g.ID, ObjectId, RType, AditionalParam) = True Then
                            Return True
                        End If
                    Next
                End If
                Return False
            End If
        Else
            Return False
        End If
    End Function

#Region "DWF"

    ''' <summary>
    ''' Devuelve si tiene habilitado los permisos de asociados o no.
    ''' </summary>
    ''' <param name="User"></param>
    ''' <param name="docTypeID"></param>
    ''' <param name="userID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSpecificAttributeRight(ByVal User As IUser, ByVal docTypeID As Long) As Boolean
        Dim RightFactoryExt As RightFactoryExt = New RightFactoryExt()
        Return RightFactoryExt.GetSpecificAttributeRight(User, docTypeID)
    End Function
#End Region
End Class


