Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Core
Imports Zamba.Tools

Public Class ReferenceIndexFactory


    Public Function GetReferenceIndexesByDoctypeId(doctypeId As Long) As List(Of ReferenceIndex)

        Dim RefIndexesList As New List(Of ReferenceIndex)()

        Dim query As New StringBuilder()
        query.Append($"SELECT ")
        query.Append($"IR.DTId AS doctypeid, IR.IId AS indexid, Rtrim(LTrim(DI.INDEX_NAME)) AS indexname, ")
        query.Append($"IR.IServer AS dbserver, IR.IUser AS dbuser, ")
        query.Append($"IR.IDataBase AS dbdatabase, IR.ITable AS dbtable, IR.IColumn AS dbcolumn, ")
        query.Append($"IRK.IndexId AS refindexid, IRK.IColumn AS refcolumn ")
        query.Append($"FROM ZIndexReference IR ")
        query.Append($"INNER JOIN ZIndexReferenceKeys IRK ")
        query.Append($"ON IRK.ReferenceId = IR.ReferenceId ")
        query.Append($"INNER JOIN DOC_INDEX DI ON IR.IId = DI.INDEX_ID ")
        query.Append($"AND IR.DTID = {doctypeId}")

        Dim RefIndexesDS As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())

        If Not RefIndexesDS Is Nothing AndAlso RefIndexesDS.Tables.Count > 0 Then

            For Each row As DataRow In RefIndexesDS.Tables(0).Rows
                Dim refIndObj As ReferenceIndex = RowMapper.Map(Of ReferenceIndex)(row)

                If refIndObj IsNot Nothing Then
                    RefIndexesList.Add(refIndObj)
                End If

            Next

        End If

        Return RefIndexesList

    End Function



End Class
