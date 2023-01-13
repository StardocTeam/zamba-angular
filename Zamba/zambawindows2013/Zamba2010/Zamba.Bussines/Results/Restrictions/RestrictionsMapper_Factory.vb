Imports Zamba.Servers
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Membership
Imports System.Text
Imports System.Collections.Generic

Public Class RestrictionsMapper_Factory
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
        Dim dsRest As dsDocRestrictions = Nothing
        Dim sb As New System.Text.StringBuilder
        Dim dtRestriction As dsDocRestrictions.RestrictionDataTable = Nothing
        Dim restrictionH As RestrictionHelper = Nothing

        Try
            dsRest = New dsDocRestrictions
            ds = GetEntityRestrictions(userId, docTypeId)
            ds.Tables(0).TableName = dsRest.Restriction.TableName
            dsRest.Merge(ds)
            dtRestriction = dsRest.Restriction

            If dtRestriction.Count > 0 Then
                restrictionH = New RestrictionHelper
                sb.Append(restrictionH.BuildString(dtRestriction(0)))

                If dtRestriction.Count > 1 Then
                    sb.Insert(0, " (")
                    For i As Int32 = 1 To dtRestriction.Count - 1
                        sb.Append(" OR ")
                        sb.Append(restrictionH.BuildString(dtRestriction(i)))
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
            If dtRestriction IsNot Nothing Then
                dtRestriction.Dispose()
                dtRestriction = Nothing
            End If
            If dsRest IsNot Nothing Then
                dsRest.Dispose()
                dsRest = Nothing
            End If
        End Try
    End Function

    Public Shared Sub AddRestriction(ByRef rest As Restriction)
        rest.Id = CoreData.GetNewID(Zamba.Core.IdTypes.DOCRESTRICTION)
        Dim instr As String = "insert into doc_restrictions(DOC_TYPE_ID,INDEX_ID,STRING_VALUE,RESTRICTION_ID,RESTRICTION_NAME) values("
        instr = instr & rest.DocTypeId & "," & rest.IndexId & "," & rest.Value & "," & rest.Id & ",'" & rest.Name & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, instr)
        'Grabo el Log
        UserBusiness.Rights.SaveAction(rest.Id, ObjectTypes.Restriccion, Zamba.Core.RightsType.insert, "Se agrego la restriccion: " & rest.Name)
    End Sub
    Public Shared Sub UpdateRestriction(ByRef rest As Restriction)
        Dim Upstr As String = "UPDATE doc_restrictions "
        Upstr = Upstr & "SET RESTRICTION_NAME='" & rest.Name & "', STRING_VALUE=" & rest.Value & " WHERE RESTRICTION_ID=" & rest.Id
        Server.Con.ExecuteNonQuery(CommandType.Text, Upstr)
        'Grabo el Log
        UserBusiness.Rights.SaveAction(rest.Id, ObjectTypes.Restriccion, Zamba.Core.RightsType.Edit, "Se actualizo la restriccion: " & rest.Name)
    End Sub
    Public Shared Sub DeleteRestriction(ByRef rest As Restriction)
        Dim query As String = "DELETE FROM DOC_RESTRICTION_R_USER WHERE RESTRICTION_ID=" & rest.Id
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

        query = "DELETE FROM DOC_RESTRICTIONS WHERE RESTRICTION_ID=" & rest.Id
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

        UserBusiness.Rights.SaveAction(rest.Id, ObjectTypes.Restriccion, Zamba.Core.RightsType.Delete, "Se borro la restriccion: " & rest.Name)
    End Sub
    Public Shared Function IsRestrictionAssigned(ByVal restrictionId As Int64)
        Dim query As String = "SELECT COUNT(1) FROM DOC_RESTRICTION_R_USER WHERE RESTRICTION_ID = " & restrictionId
        Dim count As Int32 = CInt(Server.Con.ExecuteScalar(CommandType.Text, query))
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
            If rest.IndexOf(dst.UsersDocRestriction(i).Restriction_Id) <> -1 Then
                dst.UsersDocRestriction(i).Assigned = True
            Else
                dst.UsersDocRestriction(i).Assigned = False
            End If
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
    Public Shared Function getRestrictionIndexs(ByVal Userid As Int64, ByVal doctype As Int64) As List(Of IIndex)
        Dim strselect As New System.Text.StringBuilder
        Dim u As ArrayList = Nothing
        Dim ugroup As String
        Dim ds As DataSet = Nothing
        Dim dsRest As New dsDocRestrictions
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
            ds.Tables(0).TableName = dsRest.Restriction.TableName
            dsRest.Merge(ds)

            Dim i As Integer

            Dim ind As Index
            Dim indvalue As String

            For i = 0 To dsRest.Restriction.Count - 1
                ind = IndexsBussinesExt.getIndex(dsRest.Restriction(i).INDEX_ID)
                restrictionH.Load(dsRest.Restriction(i).STRING_VALUE)
                ind.Operator = restrictionH.Operator
                ind.Data = restrictionH.Value
                indexs.Add(ind)
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
            dsRest.Dispose()
            dsRest = Nothing
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

        val = val.Replace("currentuserid", MembershipHelper.CurrentUser.ID.ToString())
        val = val.Replace("currentusername", MembershipHelper.CurrentUser.Name)

        If op = "Dentro" AndAlso val.ToLower.StartsWith("select") Then
            val = Indexs_Factory.GetIndexFilterText(Value)
        End If
    End Sub

    Public Function BuildString(ByVal restrictionRow As dsDocRestrictions.RestrictionRow) As String
        Load(restrictionRow.STRING_VALUE)
        Return CHAR_I & restrictionRow.INDEX_ID & CHAR_SPACE & op & val & CHAR_SPACE
    End Function

    Private Function FindOperator(ByVal value As String) As String
        If value.StartsWith("=") Then Return "="
        If value.StartsWith("<>") Then Return "<>"
        If value.StartsWith("Dentro") Then Return "Dentro"
        If value.StartsWith("SQL") Then Return "SQL"
        If value.StartsWith("Contiene") Then Return "Contiene"
        If value.StartsWith(">=") Then Return ">="
        If value.StartsWith(">") Then Return ">"
        If value.StartsWith("<=") Then Return "<="
        If value.StartsWith("<") Then Return "<"
        If value.StartsWith("Empieza") Then Return "Empieza"
        If value.StartsWith("Termina") Then Return "Termina"
        If value.StartsWith("Es nulo") Then Return "Es nulo"
        If value.StartsWith("Entre") Then Return "Entre"
        If value.StartsWith("Alguno") Then Return "Alguno"
        If value.StartsWith("Distinto") Then Return "Distinto"
        Return "="
    End Function
End Class