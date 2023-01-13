Imports System.Collections.Generic
Imports Zamba.Core
Imports Zamba.Membership

Public Class RestrictionsFactory
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
    Public Function getRestrictionIndexs(ByVal Userid As Int64, ByVal doctype As Int64) As DataSet
        Dim strselect As New System.Text.StringBuilder
        Dim ds As DataSet = Nothing
        Dim ugroup As Int64
        If Server.isOracle Then
            strselect.Append("select distinct r.DOC_TYPE_ID, r.INDEX_ID, REPLACE(r.STRING_VALUE,CHR(13) || CHR(10), ' ') STRING_VALUE, r.RESTRICTION_ID, r.RESTRICTION_NAME from doc_restrictions r,doc_restriction_r_user d where r.restriction_id = d.restriction_id and r.doc_type_id = " & doctype & " and (d.user_id=" & Userid & " or d.user_id in (select inheritedusergroup from group_r_group where usergroup = " & Userid & ") or d.user_id in ( Select groupid from usr_r_group where usrid=" & Userid & ") or d.user_id in (select inheritedusergroup from group_r_group where usergroup in ( Select groupid from usr_r_group where usrid=" & Userid & ")))")
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
        Else
            strselect.Append("select distinct r.DOC_TYPE_ID, r.INDEX_ID, replace(replace(r.STRING_VALUE, char(13), ''), char(10), '') STRING_VALUE, r.RESTRICTION_ID, r.RESTRICTION_NAME from doc_restrictions r,doc_restriction_r_user d where r.restriction_id = d.restriction_id and r.doc_type_id = " & doctype & " and (d.user_id=" & Userid & " or d.user_id in (select inheritedusergroup from group_r_group where usergroup = " & Userid & ") or d.user_id in ( Select groupid from usr_r_group where usrid=" & Userid & ") or d.user_id in (select inheritedusergroup from group_r_group where usergroup in ( Select groupid from usr_r_group where usrid=" & Userid & ")))")
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
        End If
        Return ds

    End Function

    Public Function getRestrictionIndexs(ByVal Userid As Int64, ByVal doctype As Int64, RestrictionId As Int64) As DataSet
        Dim strselect As New System.Text.StringBuilder
        Dim ds As DataSet = Nothing
        Dim ugroup As Int64
        If Server.isOracle Then
            strselect.Append("select distinct r.DOC_TYPE_ID, r.INDEX_ID, REPLACE(r.STRING_VALUE,CHR(13) || CHR(10), ' ') STRING_VALUE, r.RESTRICTION_ID, r.RESTRICTION_NAME from doc_restrictions r  where r.restriction_id = " & RestrictionId & " and r.doc_type_id = " & doctype)
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
        Else
            strselect.Append("select distinct r.DOC_TYPE_ID, r.INDEX_ID, replace(replace(r.STRING_VALUE, char(13), ''), char(10), '') STRING_VALUE, r.RESTRICTION_ID, r.RESTRICTION_NAME from doc_restrictions r where r.restriction_id = " & RestrictionId & "  and r.doc_type_id = " & doctype)
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
        End If
        Return ds

    End Function
End Class
