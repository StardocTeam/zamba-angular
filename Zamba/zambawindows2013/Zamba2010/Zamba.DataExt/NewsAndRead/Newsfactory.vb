Imports System.Text
Imports Zamba.Servers

Public Class Newsfactory
    ''' <summary>
    ''' Guarda la notificacion
    ''' </summary>
    ''' <param name="NewsID"></param>
    ''' <param name="DocID"></param>
    ''' <param name="DocTypeID"></param>
    ''' <param name="comment"></param>
    ''' <remarks></remarks>
    Public Sub SaveNews(ByVal NewsID As Int64, ByVal DocID As Int64, ByVal DocTypeID As Int64, ByVal comment As String)
        Dim Query As New StringBuilder

        If Server.isOracle  Then
            Query.Append("Insert into ZNews values(")
            Query.Append(NewsID)
            Query.Append(",")
            Query.Append(DocID)
            Query.Append(",")
            Query.Append(DocTypeID)
            Query.Append(",'")
            Query.Append(comment.Trim())
            Query.Append("',sysdate )")
            Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())
        Else
            Dim parValues() As Object = {NewsID, DocID, DocTypeID, comment.Trim()}
            Server.Con.ExecuteNonQuery("zsp_News_100_InsertZNewsValues", parValues)
        End If
    End Sub

    ''' <summary>
    ''' Set news as read
    ''' </summary>
    ''' <param name="Users">Users ID separate by ,</param>
    ''' <param name="newsID">News ID</param>
    ''' <remarks></remarks>
    Public Sub SetNewsRead(ByVal Users As String, ByVal docTypeID As Int64, ByVal DocID As Int64)
        Dim Query As New StringBuilder

        If Server.isOracle Then
            Query.Append("update znewsusers set status=1 where znewsusers.UserID in (")
            Query.Append(Users)
            Query.Append(") and newsid in (select newsid from znews where znews.docID=")
            Query.Append(DocID)
            Query.Append(" and znews.DocTypeID=")
            Query.Append(docTypeID)
            Query.Append(")")
        Else
            Query.Append("update users set status=1 from znews news inner join znewsusers users on news.newsid = users.newsid where news.docID= ")
            Query.Append(DocID)
            Query.Append(" and news.DocTypeID= ")
            Query.Append(docTypeID)
            Query.Append(" and users.UserID in (")
            Query.Append(Users)
            Query.Append(")")
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())
    End Sub

    ''' <summary>
    ''' Set news as read
    ''' </summary>
    ''' <param name="Users">Users ID separate by ,</param>
    ''' <param name="newsID">News ID</param>
    ''' <remarks></remarks>
    Public Sub SetDocRead(ByVal UserID As Int64, ByVal docTypeID As Int64, ByVal DocID As Int64)
        Dim Query As New StringBuilder
        Dim parValues() As Object = {UserID, DocID, docTypeID}

        If Server.isOracle Then
            Dim parnames As String() = {"puserId", "pdocId", "pdocTypeId"}
            Dim partypes As String() = {13, 13, 13}

            Server.Con.ExecuteNonQuery("zsp_News_100_InsertRead.InsertValue", parValues)
            parnames = Nothing
            partypes = Nothing
        Else
            Server.Con.ExecuteNonQuery("zsp_News_100_InsertRead", parValues)
        End If

        parValues = Nothing
    End Sub
End Class