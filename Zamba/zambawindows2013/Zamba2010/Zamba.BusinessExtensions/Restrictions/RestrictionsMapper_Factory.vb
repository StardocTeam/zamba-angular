Imports Zamba.Servers
Imports Zamba.Data
Imports Zamba.Membership
Imports System.Text

Public Class RestrictionsMapper_Factory


    Public Shared Function GetEntityRestrictionsIdsByUserIdandEntityId(ByVal userId As Int64, ByVal docTypeId As Int64) As List(Of Int64)
        Dim strselect As New StringBuilder
        Dim u As ArrayList = Nothing

        strselect.Append("select distinct r.RESTRICTION_ID from doc_restrictions r,doc_restriction_r_user d where r.restriction_id=d.restriction_id and r.doc_type_id=")
        strselect.Append(docTypeId)

        u = MembershipHelper.CurrentUser.Groups
        If u.Count = 0 Then
            strselect.Append(" and d.user_id=")
            strselect.Append(userId)
        Else
            strselect.Append(" and (d.user_id=" & userId)
            For i As Int32 = 0 To u.Count - 1
                strselect.Append(" or d.user_id=" & u(i).id.ToString)
            Next
            strselect.Append(")")
        End If

        u = Nothing
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)

        Dim RestrictionsIds As New List(Of Int64)
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            For Each R As DataRow In ds.Tables(0).Rows
                RestrictionsIds.Add(Int64.Parse(R(0).ToString()))
            Next
        End If
        Return RestrictionsIds
    End Function

    Public Shared Function GetEntityRestrictionsIdsAndUsersIdsByUserIdandEntityId(ByVal userId As Int64, ByVal docTypeId As Int64) As List(Of Tuple(Of Int64, Int64))
        Dim strselect As New StringBuilder
        Dim u As ArrayList = Nothing

        strselect.Append("select r.RESTRICTION_ID,d.user_id from doc_restrictions r,doc_restriction_r_user d where r.restriction_id=d.restriction_id and r.doc_type_id=")
        strselect.Append(docTypeId)

        u = MembershipHelper.CurrentUser.Groups
        If u.Count = 0 Then
            strselect.Append(" and d.user_id=")
            strselect.Append(userId)
        Else
            strselect.Append(" and (d.user_id=" & userId)
            For i As Int32 = 0 To u.Count - 1
                strselect.Append(" or d.user_id=" & u(i).id.ToString)
            Next
            strselect.Append(")")
        End If

        u = Nothing
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)

        Dim RestrictionsIdsAndUsers As New List(Of Tuple(Of Int64, Int64))
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            For Each R As DataRow In ds.Tables(0).Rows
                RestrictionsIdsAndUsers.Add(New Tuple(Of Int64, Int64)(Int64.Parse(R(0).ToString()), Int64.Parse(R(1).ToString())))
            Next
        End If
        Return RestrictionsIdsAndUsers
    End Function


    Private Shared Function GetEntityRestrictions(ByVal userId As Int64, ByVal docTypeId As Int64) As DataSet
        Dim strselect As New StringBuilder
        Dim u As ArrayList = Nothing

        strselect.Append("select r.DOC_TYPE_ID,r.INDEX_ID,r.STRING_VALUE,r.RESTRICTION_ID,r.RESTRICTION_NAME,d.USER_ID from doc_restrictions r,doc_restriction_r_user d where r.restriction_id=d.restriction_id and r.doc_type_id=")
        strselect.Append(docTypeId)

        u = MembershipHelper.CurrentUser.Groups
        If u.Count = 0 Then
            strselect.Append(" and d.user_id=")
            strselect.Append(userId)
        Else
            strselect.Append(" and (d.user_id=" & userId)
            For i As Int32 = 0 To u.Count - 1
                strselect.Append(" or d.user_id=" & u(i).id.ToString)
            Next
            strselect.Append(")")
        End If

        u = Nothing
        Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
    End Function

    Public Shared Function GetRestrictionWebStrings(ByVal userId As Int64, ByVal docTypeId As Int64) As String
        Dim ds As DataSet = Nothing
        Dim sb As New StringBuilder
        Dim restrictionH As RestrictionHelper = Nothing

        Try
            ds = GetEntityRestrictions(userId, docTypeId)

            If ds.Tables(0).Rows.Count > 0 Then
                restrictionH = New RestrictionHelper
                sb.Append(restrictionH.BuildString(ds.Tables(0).Rows(0)))

                If ds.Tables(0).Rows.Count > 1 Then
                    sb.Insert(0, " (")
                    For i As Int32 = 1 To ds.Tables(0).Rows.Count - 1
                        sb.Append(" OR ")
                        sb.Append(restrictionH.BuildString(ds.Tables(0).Rows(i)))
                    Next
                    sb.Append(") ")
                End If
            End If

            If sb.ToString.Length > 0 Then
                Return sb.ToString
            Else
                Return String.Empty
            End If

        Catch ex As Exception
            Return String.Empty

        Finally
            sb = Nothing
            restrictionH = Nothing
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    Public Shared Sub AddRestriction(ByRef rest As Restriction)
        rest.Id = CoreData.GetNewID(Zamba.Core.IdTypes.DOCRESTRICTION)
        Dim instr As String = "insert into doc_restrictions(DOC_TYPE_ID,INDEX_ID,STRING_VALUE,RESTRICTION_ID,RESTRICTION_NAME) values("
        instr = instr & rest.DocTypeId & "," & rest.IndexId & "," & rest.Value & "," & rest.Id & ",'" & rest.Name & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, instr)
        'Grabo el Log
        UserBusiness.Rights.SaveAction(rest.Id, ObjectTypes.Restriccion, RightsType.insert, "Se agrego la restriccion: " & rest.Name)
    End Sub
    Public Shared Sub UpdateRestriction(ByRef rest As Restriction)
        Dim Upstr As String = "UPDATE doc_restrictions "
        Upstr = Upstr & "SET RESTRICTION_NAME='" & rest.Name & "', STRING_VALUE=" & rest.Value & " WHERE RESTRICTION_ID=" & rest.Id
        Server.Con.ExecuteNonQuery(CommandType.Text, Upstr)
        'Grabo el Log
        UserBusiness.Rights.SaveAction(rest.Id, ObjectTypes.Restriccion, RightsType.Edit, "Se actualizo la restriccion: " & rest.Name)
    End Sub
    Public Shared Sub DeleteRestriction(ByRef rest As Restriction)
        Dim query As String = "DELETE FROM DOC_RESTRICTION_R_USER WHERE RESTRICTION_ID=" & rest.Id
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

        query = "DELETE FROM DOC_RESTRICTIONS WHERE RESTRICTION_ID=" & rest.Id
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

        UserBusiness.Rights.SaveAction(rest.Id, ObjectTypes.Restriccion, RightsType.Delete, "Se borro la restriccion: " & rest.Name)
    End Sub
    Public Shared Function IsRestrictionAssigned(ByVal restrictionId As Int64)
        Dim query As String = "SELECT COUNT(1) FROM DOC_RESTRICTION_R_USER WHERE RESTRICTION_ID = " & restrictionId
        Dim count As Integer = Server.Con.ExecuteScalar(CommandType.Text, query)
        Return count > 0
    End Function

    Public Shared Sub AddRestrictionRights(ByVal userId As Int64, ByVal RestrictionId As Int32)
        Server.Con.ExecuteNonQuery(CommandType.Text, "insert into doc_restriction_r_user(user_id,restriction_Id) values(" & userId & "," & RestrictionId & ")")
    End Sub

    Public Shared Sub DeleteRestrictionRights(ByVal userId As Int64, ByVal RestrictionId As Int32)
        Server.Con.ExecuteNonQuery(CommandType.Text, "delete doc_restriction_r_user where user_id=" & userId & " and restriction_Id= " & RestrictionId)
    End Sub


    Public Shared Function GetUserDocTypeRestrictions(ByVal Doc_TypeId As Int64, ByVal user As Int64) As dsRestrictionsUsers
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "select Restriction_Id,Restriction_Name from doc_restrictions where DOC_TYPE_ID =" & Doc_TypeId)
        Dim dst As New dsRestrictionsUsers
        ds.Tables(0).TableName = dst.UsersDocRestriction.TableName
        dst.Merge(ds)
        Dim rest As ArrayList = getUserRestrictions(user)
        Dim i As Int32
        For i = 0 To dst.UsersDocRestriction.Count - 1

            Dim RestriccionId = dst.UsersDocRestriction(i).Restriction_Id
            dst.UsersDocRestriction(i).Assigned = False

            For Each item As Int32 In rest
                If RestriccionId = item Then
                    dst.UsersDocRestriction(i).Assigned = True
                Else
                End If
            Next

        Next
        Return dst
    End Function

    Public Shared Function GetUserDocTypeRestrictions() As dsRestrictionsUsers
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "select Restriction_Id,Restriction_Name from doc_restrictions")
        Dim dst As New dsRestrictionsUsers
        ds.Tables(0).TableName = dst.UsersDocRestriction.TableName
        dst.Merge(ds)
        Return dst
    End Function

    Private Shared Function getUserRestrictions(ByVal userId As Int64) As ArrayList
        Dim r As New ArrayList
        Dim ds As New DataSet
        Dim i As Int32
        If Server.isOracle Then

            ds = Server.Con.ExecuteDataset(CommandType.Text, "select RESTRICTION_ID from doc_restriction_r_user where user_id= " & userId)
        Else
            Dim params() As Object = {userId}
            ds = Server.Con.ExecuteDataset("zsp_security_100_GetUserDocumentsResctrictions", params)
        End If

        For i = 0 To ds.Tables(0).Rows.Count - 1
            r.Add(ds.Tables(0).Rows(i).Item(0))
        Next
        ds.Dispose()
        Return r
    End Function

    ''' <summary>
    '''     getRestrictionIndexs: Obtiene las restricciones para un determinado entidad
    ''' </summary>
    ''' <param name="Userid">Id del usuario de zamba</param>
    ''' <param name="doctype">Id del doctype del documento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>                                              
    '''     [Javier]    01/10/2010    Created    
    '''</history>
    Public Shared Function getRestrictionIndexs(ByVal Userid As Int64, ByVal doctype As Int64, ByVal UseCache As Boolean) As List(Of IIndex)
        Dim strselect As New StringBuilder
        Dim u As ArrayList = Nothing
        Dim ugroup As String
        Dim ds As DataSet = Nothing
        Dim value As Int32
        Dim restrictionH As New RestrictionHelper

        Try
            If Server.isOracle Then
                strselect.Append("select r.DOC_TYPE_ID, r.INDEX_ID, r.STRING_VALUE, r.RESTRICTION_ID, r.RESTRICTION_NAME,d.USER_ID from doc_restrictions r,doc_restriction_r_user d where r.restriction_id = d.restriction_id and r.doc_type_id = " & doctype & " and d.user_id=" & Userid)
                u = Membership.MembershipHelper.CurrentUser.Groups
                Dim y As Integer
                If u.Count > 0 Then
                    strselect.Remove(0, strselect.Length)
                    strselect.Append("select r.DOC_TYPE_ID, r.INDEX_ID, r.STRING_VALUE, r.RESTRICTION_ID, r.RESTRICTION_NAME,d.USER_ID from doc_restrictions r,doc_restriction_r_user d where r.restriction_id = d.restriction_id and r.doc_type_id = " & doctype & " and (d.user_id=" & Userid)
                    For y = 0 To u.Count - 1
                        If y = u.Count - 1 Then
                            ugroup = u(y).id
                            strselect.Append(" or d.user_id=" & ugroup & " )")
                        Else
                            ugroup = u(y).id
                            strselect.Append(" or d.user_id=" & ugroup.ToString)
                        End If
                    Next
                End If
                ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
            Else
                Dim parameters() As Object = {doctype, Userid}
                ds = Server.Con.ExecuteDataset("zsp_select_doc_restrictions_r_user_100", parameters)
            End If
            Dim indexs As New List(Of IIndex)
            If ds Is Nothing Then
                Return indexs
            End If
            Dim i As Integer
            Dim ind As Index
            Dim indvalue As String
            Dim RestrictionsIds As List(Of Int64) = RestrictionsMapper_Factory.GetEntityRestrictionsIdsByUserIdandEntityId(Userid, doctype)
            Dim RestrictionsIdsandUsers As List(Of Tuple(Of Int64, Int64)) = RestrictionsMapper_Factory.GetEntityRestrictionsIdsAndUsersIdsByUserIdandEntityId(Userid, doctype)
            Dim GroupsUsersIdsWithRightOfView As New List(Of Long)
            Dim AllGroupsUsersIds As New List(Of Long)
            ' cargo todos los grupos de los cuales es contenido el usuario
            AllGroupsUsersIds = UserBusiness.GetUserGroupsIdsByUserid(Userid)
            For Each groupUserId As Int64 In UserBusiness.GetUserGroupsIdsByUserid(Userid)
                If UserBusiness.Rights.GetRightsOnlyForOneUserOrGroup(groupUserId, ObjectTypes.DocTypes, RightsType.View, doctype) Then
                    GroupsUsersIdsWithRightOfView.Add(groupUserId)
                End If
                For Each parentGroup As ICore In UserGroupBusiness.getInheritanceOfGroup(groupUserId)
                    If UserBusiness.Rights.GetRightsOnlyForOneUserOrGroup(parentGroup.ID, ObjectTypes.DocTypes, RightsType.View, doctype) Then
                        GroupsUsersIdsWithRightOfView.Add(parentGroup.ID)
                    End If
                Next
            Next

            Dim AllowedGroupsUsersIds As New List(Of Long)
            Dim EnabledRestrictionsIds As New List(Of Int64)
            'Obtengo los permisos habilitados para asociados para cada grupo
            For Each RestrictionId As Int64 In RestrictionsIds
                For Each GroupId As Int64 In GroupsUsersIdsWithRightOfView
                    For Each restrictionasign As Tuple(Of Int64, Int64) In RestrictionsIdsandUsers
                        If restrictionasign.Item1 = RestrictionId AndAlso restrictionasign.Item2 = GroupId Then
                            AllowedGroupsUsersIds.Add(GroupId)
                        End If
                    Next
                Next

                If AllowedGroupsUsersIds.Count >= GroupsUsersIdsWithRightOfView.Count Then
                    'Todos los grupos tienen marcada la restriccion
                    EnabledRestrictionsIds.Add(RestrictionId)
                End If
                AllowedGroupsUsersIds.Clear()
            Next



            For i = 0 To ds.Tables(0).Rows.Count - 1
                If EnabledRestrictionsIds.Contains(ds.Tables(0).Rows(i)("RESTRICTION_ID")) Then
                    ind = IndexsBussinesExt.getIndex(ds.Tables(0).Rows(i)("INDEX_ID"), UseCache)
                    restrictionH.Load(ds.Tables(0).Rows(i)("STRING_VALUE"))
                    ind.Operator = restrictionH.Operator
                    ind.Data = restrictionH.Value
                    indexs.Add(ind)
                End If
            Next

            Return indexs
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        Finally
            strselect = Nothing
            ugroup = Nothing
            restrictionH = Nothing
            ds.Dispose()
            ds = Nothing
        End Try

    End Function

End Class

Class RestrictionHelper
    Const CHAR_SPACE As String = " "
    Const CHAR_I As String = " i"
    Private val As String
    Private op As String
    Private userId As Int64
    Private userName As String

    Public Property Value() As String
        Get
            Return val
        End Get
        Set(ByVal value As String)
            val = value
        End Set
    End Property
    Public Property [Operator]() As String
        Get
            Return op
        End Get
        Set(ByVal value As String)
            op = value
        End Set
    End Property

    Public Sub Load(ByVal restrictionValue As String)
        val = restrictionValue
        op = FindOperator(val)
        val = val.Remove(0, op.Length)

        'If val.StartsWith("'") Then val = val.Remove(0, 1)
        'If val.EndsWith("'") Then val = val.Remove(val.Length - 1, 1)

        Val = val.Replace("currentuserid", MembershipHelper.CurrentUser.ID.ToString())
        val = val.Replace("currentusername", MembershipHelper.CurrentUser.Name)

        If op = "Dentro" AndAlso val.ToLower.StartsWith("select") Then
            val = Indexs_Factory.GetIndexFilterText(Value)
        End If
    End Sub

    Public Function BuildString(ByVal restrictionRow As DataRow) As String
        Load(restrictionRow("STRING_VALUE"))
        Return CHAR_I & restrictionRow("INDEX_ID") & CHAR_SPACE & op & val & CHAR_SPACE
    End Function

    Private Function FindOperator(ByVal value As String) As String
        If value.StartsWith("=") Then Return "="
        If value.StartsWith("<>") Then Return "<>"
        If value.StartsWith("Dentro") Then Return "Dentro"
        If value.StartsWith("SQL Sin Atributo") Then Return "SQL Sin Atributo"
        If value.StartsWith("SQL") Then Return "SQL"
        If value.StartsWith("Contiene") Then Return "Contiene"
        If value.StartsWith(">=") Then Return ">="
        If value.StartsWith(">") Then Return ">"
        If value.StartsWith("<=") Then Return "<="
        If value.StartsWith("<") Then Return "<"
        If value.StartsWith(" Empieza") Then Return " Empieza"
        If value.StartsWith(" Termina") Then Return " Termina"
        If value.StartsWith(" Es nulo") Then Return " Es nulo"
        If value.StartsWith(" Entre") Then Return " Entre"
        If value.StartsWith(" Alguno") Then Return " Alguno"
        If value.StartsWith(" Distinto") Then Return " Distinto"
        Return "="
    End Function
End Class