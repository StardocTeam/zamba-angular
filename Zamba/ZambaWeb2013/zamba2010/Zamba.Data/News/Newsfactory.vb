Imports System.Text
Imports Zamba.Core

Public Class Newsfactory
    ''' <summary>
    ''' Guarda la notificacion
    ''' </summary>
    ''' <param name="NewsID"></param>
    ''' <param name="DocID"></param>
    ''' <param name="DocTypeID"></param>
    ''' <param name="comment"></param>
    ''' <remarks></remarks>
    Public Sub SaveNews(ByVal NewsID As Int64, ByVal DocID As Int64, ByVal DocTypeID As Int64, ByVal comment As String, Userid As Int64, details As String)
        Dim Query As New StringBuilder
        Query.Append("Insert into ZNews values(")
        Query.Append(NewsID)
        Query.Append(",")
        Query.Append(DocID)
        Query.Append(",")
        Query.Append(DocTypeID)
        Query.Append(",'")
        Query.Append(comment.Trim())
        Query.Append("',")
        If Server.isOracle Then
            Query.Append("sysdate")
        Else
            Query.Append("getdate()")
        End If
        Query.Append(",")
        Query.Append(Userid)
        Query.Append(",'")
        Query.Append(details.Trim())
        Query.Append("'")
        Query.Append(")")
        Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())
    End Sub


    ''' <summary>
    ''' Set news as read
    ''' </summary>
    ''' <param name="Users">Users ID separate by ,</param>
    ''' <param name="newsID">News ID</param>
    ''' <remarks></remarks>
    Public Sub SetRead(ByVal Users As String, ByVal docTypeID As Int64, ByVal DocID As Int64)
        Dim Query As New StringBuilder


        Query.Append("update znewsusers set status=1 where znewsusers.UserID in (")
        Query.Append(Users)
        Query.Append(") and newsid in (select newsid from znews where znews.docID=")
        Query.Append(DocID)
        Query.Append(" and znews.DocTypeID=")
        Query.Append(docTypeID)
        Query.Append(")")


        Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())
    End Sub

    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [German] 04/04/11 - Created.
    ''' </history>
    Public Function GetAllNewsSummary(userid As Long, Optional searchType As NewsSearchType = NewsSearchType.UNREAD) As DataSet

        Dim strselect As New StringBuilder()

        Dim valueCol As String = If(Server.isOracle, "c_value", "value")

        strselect.Append($"Select znu.NewsId, zn.docid, zn.doctypeid, zn.{valueCol}, Zn.crdate, znu.status as isRead from ZNewsUsers as znu ")
        strselect.Append("inner join ZNews as Zn on znu.newsid = zn.newsid ")
        strselect.Append($"where znu.userid = {userid} ")

        If searchType = NewsSearchType.UNREAD Then
            strselect.Append($"and znu.status = 0 ")
        ElseIf searchType = NewsSearchType.READ Then
            strselect.Append($"and znu.status = 1 ")
        End If

        strselect.Append("order by zn.crdate desc")

        Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString())

    End Function

    Public Function GetNews(userId As Long, resultId As Long, AsociatedEntityIds As String) As DataSet
        Dim strselect As New StringBuilder()

        If Server.isOracle Then
            strselect.Append("select zn.NewsId, zn.docid,zn.doctypeid,zn.c_value accion,Zn.crdate Fecha, zn.userid,zn.details detalle, (u.Apellido || ' ' || u.Nombres) Usuario from ZNews  zn ")
        Else
            strselect.Append("select zn.NewsId, zn.docid,zn.doctypeid,zn.value,Zn.crdate, zn.userid,zn.details, (u.Apellido + ' ' + u.Nombres) Usuario from ZNews  zn ")
        End If
        '        strselect.Append("left join ZNewsUsers  Znu ")
        '       strselect.Append("on znu.newsid = zn.newsid ")
        strselect.Append("left join usrtable  u ")
        strselect.Append("on u.id = zn.userid ")
        strselect.Append(" where  zn.docid = ")
        strselect.Append(resultId)

        If (AsociatedEntityIds.Trim <> String.Empty) Then
            strselect.Append(" and zn.doctypeid in (")
            strselect.Append(AsociatedEntityIds.Trim)
            strselect.Append(")")
        End If

        strselect.Append("  order by zn.crdate desc")

        Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString())
    End Function

End Class
