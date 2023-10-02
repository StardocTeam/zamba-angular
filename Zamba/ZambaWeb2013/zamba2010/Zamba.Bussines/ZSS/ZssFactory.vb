Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports Zamba.Data
Imports Zamba.Servers
Imports Zamba.Membership
Imports System.Text
Imports Zamba.Core
Imports System.Data
Imports Newtonsoft.Json.Linq
Imports Zamba.Framework
'Namespace Zamba.Business
Public Class ZssFactory
    Public Function FormatDate(d As DateTime)
        Return Servers.Server.Con().ConvertDateTime(d.ToString())
    End Function
    Public Sub SetZssValues(ByVal zss As Zss)
        Dim [select] = "SELECT 1 FROM ZSS WHERE USERID=" & zss.UserId.ToString()
        Dim insert = "INSERT INTO ZSS (TOKEN, USERID, CREATEDATE, TOKENEXPIREDATE, CONNECTIONID,OKTAACCESSTOKEN,OKTAIDTOKEN) VALUES('" & zss.Token & "'," + zss.UserId.ToString() & "," + FormatDate(zss.CreateDate) & "," + FormatDate(zss.TokenExpireDate) & "," + zss.ConnectionId.ToString() + ",'" & zss.OktaAccessToken & "','" + zss.OktaIdToken + "')"
        Dim update = "UPDATE ZSS SET TOKEN='" & zss.Token & "', CREATEDATE= " + FormatDate(zss.CreateDate) & ", TOKENEXPIREDATE=" + FormatDate(zss.TokenExpireDate) & ", OKTAACCESSTOKEN='" + zss.OktaAccessToken + "',OKTAIDTOKEN='" + zss.OktaIdToken + "' WHERE USERID= " + zss.UserId.ToString()
        Dim isNewRow As Boolean = If(Server.Con().ExecuteScalar(CommandType.Text, [select]) Is Nothing, True, False)
        Dim success = Server.Con().ExecuteNonQuery(CommandType.Text, If(isNewRow, insert, update))
    End Sub
    Public Function KeepSessionAlive(Userid As Integer)
        Dim ExpireDate As String = FormatDate(DateTime.Now.AddDays(700))
        Dim SqlUpdate = $"UPDATE ZSS SET TOKENEXPIREDATE={ExpireDate} WHERE USERID={Userid}"
        Zamba.Servers.Server.Con().ExecuteNonQuery(CommandType.Text, SqlUpdate)
    End Function
    Public Function GetZss(ByVal user As IUser) As Zss
        Dim [select] As String = "SELECT * FROM ZSS WHERE USERID=" & user.ID.ToString()
        Dim ds As DataSet
        Dim ret As New Zss
        Try
            ds = Server.Con().ExecuteDataset(CommandType.Text, [select])
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dt As DataTable = ds.Tables(0)
                With ret
                    .Token = dt.Rows(0).Field(Of String)("Token")
                    .TokenExpireDate = dt.Rows(0).Field(Of DateTime)("TokenExpireDate")
                    .CreateDate = dt.Rows(0).Field(Of DateTime)("CreateDate")
                    .UserId = user.ID
                    .ConnectionId = user.ConnectionId
                    .OktaAccessToken = dt.Rows(0).Field(Of String)("OktaAccessToken")
                    .OktaIdToken = dt.Rows(0).Field(Of String)("OktaIdToken")
                End With
                Return ret
            Else
                Return Nothing
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    Public Sub RemoveZss(ByVal userid As Integer)
        Dim [select] = "Delete FROM ZSS WHERE USERID=" & userid
        Dim RemoveUser = Zamba.Servers.Server.Con().ExecuteNonQuery(CommandType.Text, [select])
    End Sub
    Public Function CheckTokenInDatabase(ByVal Userid As Integer, ByVal token As String, IsQueryString As Boolean) As Boolean
        Try
            Dim ObjZss As New Zss
            ObjZss = GetZss(New User With {.ID = Userid})
            If Not ObjZss Is Nothing Then
                If ((ObjZss.Token = token) Or (ObjZss.TokenQueryString = token And IsQueryString)) Then
                    'KeepSessionAlive(Userid)
                    Return True
                Else
                End If
                Return False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return False
    End Function
End Class
'End Namespace