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

        strselect.Append("select r.DOC_TYPE_ID,r.INDEX_ID,REPLACE(r.STRING_VALUE,CHR(13) || CHR(10), ' ') STRING_VALUE,r.RESTRICTION_ID,r.RESTRICTION_NAME,d.USER_ID from doc_restrictions r,doc_restriction_r_user d where r.restriction_id=d.restriction_id and r.doc_type_id=")
        strselect.Append(docTypeId)

        u = Zamba.Membership.MembershipHelper.CurrentUser.Groups
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

    Public Function GetRestrictionWebStrings(ByVal userId As Int64, ByVal docTypeId As Int64, ByVal userName As String) As String
        Dim ds As DataSet = Nothing
        Dim sb As New System.Text.StringBuilder
        Dim restrictionH As RestrictionHelper = Nothing

        Try
            ds = GetEntityRestrictions(userId, docTypeId)
            ds.Tables(0).TableName = "Restriction"

            If ds.Tables(0).Rows.Count > 0 Then
                restrictionH = New RestrictionHelper(userId)
                sb.Append(restrictionH.BuildString(ds.Tables(0)(0)))

                If ds.Tables(0).Rows.Count > 1 Then
                    sb.Insert(0, " (")
                    For i As Int32 = 1 To ds.Tables(0).Rows.Count - 1
                        sb.Append(" OR ")
                        sb.Append(restrictionH.BuildString(ds.Tables(0)(i)))
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
    Public Function GetRestrictionIndexs(ByVal userId As Int64, ByVal docTypeId As Int64) As Generic.List(Of IIndex)
        Dim Indexs As New Generic.List(Of IIndex)

        Dim key As String = userId & "-" & docTypeId
        If Not Cache.DocTypesAndIndexs.hsRestrictionsIndexsByUserId.ContainsKey(key) Then
            Dim ds As DataSet = Nothing
            Dim ind As Index
            Dim indvalue As String
            Dim RF As New Zamba.Data.RestrictionsFactory
            Dim IB As New IndexsBusiness
            Dim restrictionH As New RestrictionHelper(userId)

            Try
                ds = RF.getRestrictionIndexs(userId, docTypeId)
                ds.Tables(0).TableName = "Restriction"


                For i As Int32 = 0 To ds.Tables(0).Rows.Count - 1
                    ind = IB.GetIndex(ds.Tables(0)(i)("INDEX_ID"))
                    restrictionH.Load(ds.Tables(0)(i)("STRING_VALUE"))
                    ind.Operator = restrictionH.Operator
                    ind.Data = restrictionH.Value
                    Indexs.Add(ind)
                Next

            Catch ex As Exception
                Return Nothing
            Finally
                IB = Nothing
                RF = Nothing
                restrictionH = Nothing
                If Not ds Is Nothing Then
                    ds.Dispose()
                    ds = Nothing
                End If
            End Try

            SyncLock Cache.DocTypesAndIndexs.hsRestrictionsIndexsByUserId.SyncRoot
                If Not Cache.DocTypesAndIndexs.hsRestrictionsIndexsByUserId.ContainsKey(key) Then
                    Cache.DocTypesAndIndexs.hsRestrictionsIndexsByUserId.Add(key, Indexs)
                End If
            End SyncLock
        Else
            Indexs = Cache.DocTypesAndIndexs.hsRestrictionsIndexsByUserId(key)
            End If

        Return Indexs
    End Function

    Public Function GetRestrictionIndexs(ByVal userId As Int64, ByVal docTypeId As Int64, ByVal RestrictionId As Int64) As Generic.List(Of IIndex)
        Dim Indexs As New Generic.List(Of IIndex)

        Dim key As String = userId & "-" & docTypeId & "-" & RestrictionId
        If Not Cache.DocTypesAndIndexs.hsRestrictionsIndexsByUserId.ContainsKey(key) Then
                Dim ds As DataSet = Nothing
                Dim ind As Index
                Dim indvalue As String
                Dim RF As New Zamba.Data.RestrictionsFactory
                Dim IB As New IndexsBusiness
                Dim restrictionH As New RestrictionHelper(userId)

                Try
                    ds = RF.getRestrictionIndexs(userId, docTypeId, RestrictionId)
                    ds.Tables(0).TableName = "Restriction"


                    For i As Int32 = 0 To ds.Tables(0).Rows.Count - 1
                        ind = IB.GetIndex(ds.Tables(0)(i)("INDEX_ID"))
                        restrictionH.Load(ds.Tables(0)(i)("STRING_VALUE"))
                        ind.Operator = restrictionH.Operator
                        ind.Data = restrictionH.Value
                        Indexs.Add(ind)
                    Next

                Catch ex As Exception
                    Return Nothing
                Finally
                    IB = Nothing
                    RF = Nothing
                    restrictionH = Nothing
                    If Not ds Is Nothing Then
                        ds.Dispose()
                        ds = Nothing
                    End If
                End Try

            SyncLock Cache.DocTypesAndIndexs.hsRestrictionsIndexsByUserId.SyncRoot
                If Not Cache.DocTypesAndIndexs.hsRestrictionsIndexsByUserId.ContainsKey(key) Then
                    Cache.DocTypesAndIndexs.hsRestrictionsIndexsByUserId.Add(key, Indexs)
                End If
            End SyncLock
        Else
            Indexs = Cache.DocTypesAndIndexs.hsRestrictionsIndexsByUserId(key)
            End If

        Return Indexs
    End Function

End Class

Class RestrictionHelper
    Const CHAR_SPACE As String = " "
    Const CHAR_I As String = " i"
    Private val As String
    Private op As String
    Private userId As Int64

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

    Public Sub New(ByVal userId As Int64)
        Me.userId = userId
    End Sub

    Public Sub Load(ByVal restrictionValue As String)
        val = restrictionValue
        op = FindOperator(val)
        val = val.Remove(0, op.Length)

        If val.StartsWith("'") AndAlso val.EndsWith("'") Then
            val = val.Remove(0, 1)
            If val.EndsWith("'") Then val = val.Remove(val.Length - 1, 1)
        End If

        val = val.Replace("currentuserid", userId.ToString())
        If val.Contains("currentusername") Then
            Dim UB As New UserBusiness
            val = val.Replace("currentusername", UB.GetUserNamebyId(userId))
        End If

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
        If value.StartsWith("Empieza") Then Return "Empieza"
        If value.StartsWith("Termina") Then Return "Termina"
        If value.StartsWith("Es nulo") Then Return "Es nulo"
        If value.StartsWith("Entre") Then Return "Entre"
        If value.StartsWith("Alguno") Then Return "Alguno"
        If value.StartsWith("Distinto") Then Return "Distinto"
        Return "="
    End Function
End Class
