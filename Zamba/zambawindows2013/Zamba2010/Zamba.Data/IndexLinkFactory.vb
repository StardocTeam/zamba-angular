Imports Zamba.Core

Public Class IndexLinkFactory
    Public Shared Function GetindexsLinks() As ArrayList
        Dim result As New ArrayList
        Dim strselect As String = "select id,name, replace(descripcion, '''', ' ' ) from index_link"
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)

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
        ds = Server.Con.ExecuteDataset(CommandType.Text, "select id,data,flag,doctype,docindex,name from index_link_info where id=" & IndexLink.Id)

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

    Public Shared Sub UpdateInfo(ByVal ili As IndexLinkInfo)
        IndexLinkFactory.DeleteindexLinkInfo(ili)
        IndexLinkFactory.SaveLinkInfo(ili)
    End Sub

    Public Shared Sub SaveNewLink(ByVal ii As IndexLink)
        Dim newid As Integer = CoreData.GetNewID(Zamba.Core.IdTypes.IndexLink)
        Dim strinsert As String = "insert into index_link(id,name,descripcion) values(" & newid & ",'" & ii.Name & "','" & ii.Description.Replace(",", "") & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    End Sub

    Public Shared Sub Delete(ByVal il As IndexLink)
        Dim strdelete As String = "Delete Index_Link where Id =" & il.Id
        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub

    Public Shared Sub Update(ByVal il As IndexLink)
        Dim strUpdate As String = "update Index_Link set id=" & il.Id & ",description='" & il.Description.Replace("'", "") & "', name='" & il.Name.Replace("'", "") & "' where Id =" & il.Id
        Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
    End Sub

    Public Shared Sub DeleteindexLinkInfo(ByVal ili As IndexLinkInfo)
        Dim strdelete As String = "Delete index_link_info where id=" & ili.id & " and doctype=" & ili.doctype & " and Docindex = " & ili.index
        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub

    Public Shared Sub linkindexs(ByVal DocTypeId1 As Int32, ByVal DocTypeId2 As Int32, ByVal Index1 As Index, ByVal Index2 As Index, ByVal CheckAll As Boolean)

        If Server.IsOracle Then
            Dim parValues() As Object = {DocTypeId1, DocTypesFactory.GetDocTypeName(DocTypeId1), DocTypeId2, DocTypesFactory.GetDocTypeName(DocTypeId2), Index1.ID, Indexs_Factory.GetIndexName(Index1.ID), Index2.ID, Indexs_Factory.GetIndexName(Index2.ID), CheckAll}
            Server.Con.ExecuteNonQuery("ZIndLnkIns_Pkg.ZIndLnkInsRow", parValues)

        Else
            Dim parValues() As Object = {DocTypeId1, DocTypesFactory.GetDocTypeName(DocTypeId1), DocTypeId2, DocTypesFactory.GetDocTypeName(DocTypeId2), Index1.ID, Indexs_Factory.GetIndexName(Index1.ID), Index2.ID, Indexs_Factory.GetIndexName(Index2.ID), CheckAll}
            Try
                Server.Con.ExecuteNonQuery("ZIndLnkInsRow", parValues)
                'EL STORED PROCEDURE NO EXISTE, LEER EL COMENTARIO PARA EL PAQUETE DE ORACLE.
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

    End Sub
    Public Shared Sub DeleteLinkedIndexs(ByVal DocTypeName1 As String, ByVal DocTypeName2 As String, ByVal IndexName1 As String, ByVal IndexName2 As String)
        Dim sql As String = "Delete from index_link where doctypename1='" & DocTypeName1 & "' and doctypename2='" & DocTypeName2 & "' and Indexname1='" & IndexName1 & "' and Indexname2='" & IndexName2 & "'"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub
    Public Shared Sub SaveLinkInfo(ByVal linfo As IndexLinkInfo)

        If Server.IsOracle Then
            Dim parValues() As Object = {linfo.Id, linfo.Data, linfo.Flag, linfo.Doctype, linfo.Index, linfo.Name}
            Server.Con.ExecuteNonQuery("zsp_index_100.InsertLinkInfo", parValues)
        Else
            Dim parValues() As Object = {linfo.id, linfo.data, linfo.flag, linfo.doctype, linfo.index, linfo.Name}
            Server.Con.ExecuteNonQuery("zsp_index_100_InsertLinkInfo", parValues)
        End If
    End Sub
    Public Shared Sub AddLinkInfo(ByVal indexlink As IndexLink, ByVal linfo As IndexLinkInfo)
        For Each l As IndexLinkInfo In indexlink.Links
            If l.doctype = linfo.doctype AndAlso l.index = linfo.index Then
                Exit Sub
            End If
        Next

        indexlink.Links.Add(linfo)
        IndexLinkFactory.SaveLinkInfo(linfo)
    End Sub
End Class
