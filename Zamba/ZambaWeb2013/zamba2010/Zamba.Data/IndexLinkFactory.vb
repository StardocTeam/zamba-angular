Imports Zamba.Servers
Imports Zamba.Core
Imports Zamba.Data

Public Class IndexLinkFactory
    Public Shared Function GetindexsLinks() As ArrayList
        Dim result As New ArrayList
        Dim strselect As String = "select id,name, replace(descripcion, '''', ' ' ) from index_link"
        Dim ds As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)

        For Each d As DataRow In ds.Tables(0).Rows
            Dim link As IndexLink

            If Not IsDBNull(d(2)) Then
                link = New IndexLink(d(0), d(1), d(2), IndexLinkFactory.GetLinks(d(0)))
            Else
                link = New IndexLink(d(0), d(1), "", IndexLinkFactory.GetLinks(d(0)))
            End If

            result.Add(link)
        Next

        Return result
    End Function
    Public Shared Function GetLinks(ByVal IndexLink As IndexLink) As ArrayList
        Dim result As New ArrayList
        Dim ds As New DataSet
        ds = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, "select id,data,flag,doctype,docindex,name from index_link_info where id=" & IndexLink.Id)

        For Each r As DataRow In ds.Tables(0).Rows
            For Each column As Object In r.ItemArray
                If IsDBNull(column) Then
                    column = Nothing
                End If
            Next
            Dim index As New IndexLinkInfo(IndexLink, r(0), r(1), r(2), r(3), r(4), r(5))
            result.Add(index)
        Next

        Return result
    End Function
    Public Shared Function GetlinkedIndexs() As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "Select doctypename1 as Documento1, doctypename2 as documento2, indexname1 as Indice1, Indexname2 as indice2 from index_link")
    End Function

   

    Public Shared Sub SaveNewLink(ByVal ii As IndexLink)
        Dim newid As Integer = CoreData.GetNewID(Zamba.Core.IdTypes.IndexLink)
        Dim strinsert As String = "insert into index_link(id,name,descripcion) values(" & newid & ",'" & ii.Name & "','" & ii.Description.Replace(",", "") & "')"
        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    End Sub

    Public Shared Sub Delete(ByVal il As IndexLink)
        Dim strdelete As String = "Delete Index_Link where Id =" & il.Id
        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub

    Public Shared Sub Update(ByVal il As IndexLink)
        Dim strUpdate As String = "update Index_Link set id=" & il.Id & ",description='" & il.Description.Replace("'", "") & "', name='" & il.Name.Replace("'", "") & "' where Id =" & il.Id
        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
    End Sub

    Public Shared Sub DeleteindexLinkInfo(ByVal ili As IndexLinkInfo)
        Dim strdelete As String = "Delete index_link_info where id=" & ili.id & " and doctype=" & ili.doctype & " and Docindex = " & ili.index
        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub

    
    
    
    
End Class
