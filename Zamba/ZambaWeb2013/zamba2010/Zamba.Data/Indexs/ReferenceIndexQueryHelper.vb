Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Core

Public Class ReferenceIndexQueryHelper

    ' Esta clase tiene cosas hardcodeadas para la base de Boston especificamente
    ' Ver como hacerlo generico


    'Public Function GetReferenceIndexesByDoctypeId(DoctypeId As Long) As List(Of ReferenceIndex)

    '    'Obtengo los indices referenciales para el doctypeid
    '    Dim RefIndfactory As New ReferenceIndexFactory()
    '    'REtorno la lista
    '    Return RefIndfactory.GetReferenceIndexesByDoctypeId(DoctypeId)

    'End Function

    Public Function GetStringForDocIQuery(indexId As Long, doctypeId As Long, refIndexes As List(Of ReferenceIndex)) As String

        Dim columnString As New StringBuilder
        Dim refIndex As ReferenceIndex

        refIndex = refIndexes.Where(Function(ind) ind.IndexId = indexId AndAlso ind.DoctypeId = doctypeId).FirstOrDefault
        If refIndex IsNot Nothing Then

            columnString.Append(If(Not String.IsNullOrEmpty(refIndex.DBServer), refIndex.DBServer & ".", String.Empty))
            columnString.Append(If(Not String.IsNullOrEmpty(refIndex.DBDataBase), refIndex.DBDataBase & ".", "dtbbpm."))
            columnString.Append(If(Not String.IsNullOrEmpty(refIndex.DBUser), refIndex.DBUser & ".", "bpm."))
            columnString.Append(If(Not String.IsNullOrEmpty(refIndex.DBTable), refIndex.DBTable & ".", String.Empty))
            columnString.Append($"{refIndex.DBColumn}") ' AS I{refIndex.IndexId}

        End If

        Return columnString.ToString

    End Function

    Public Function GetStringForAliasQuery(indexId As Long, doctypeId As Long, refIndexes As List(Of ReferenceIndex), Optional fromWFStepTasks As Boolean = False) As String

        Dim columnAliasString As String = String.Empty
        Dim refIndex As ReferenceIndex

        refIndex = refIndexes.Where(Function(ind) ind.IndexId = indexId AndAlso ind.DoctypeId = doctypeId).FirstOrDefault
        If refIndex IsNot Nothing Then

            columnAliasString = $"{If(fromWFStepTasks, "WD", "I")}.I{refIndex.IndexId} AS {Chr(34)}{refIndex.IndexName}{Chr(34)}"

            columnAliasString = columnAliasString.Replace("..", String.Empty)
            columnAliasString = If(columnAliasString.StartsWith("."), columnAliasString.Substring(1), columnAliasString)
        End If

        Return columnAliasString
    End Function

    Public Function GetStringJoinQuery(indexId As Long, doctypeId As Long, refIndexes As List(Of ReferenceIndex)) As String

        Dim JoinString As New StringBuilder
        Dim refIndex As ReferenceIndex

        refIndex = refIndexes.Where(Function(ind) ind.IndexId = indexId AndAlso ind.DoctypeId = doctypeId).FirstOrDefault
        If refIndex IsNot Nothing Then

            JoinString.Append("LEFT OUTER JOIN ")
            JoinString.Append(If(Not String.IsNullOrEmpty(refIndex.DBServer), refIndex.DBServer & ".", String.Empty))
            JoinString.Append(If(Not String.IsNullOrEmpty(refIndex.DBDataBase), refIndex.DBDataBase & ".", "dtbbpm."))
            JoinString.Append(If(Not String.IsNullOrEmpty(refIndex.DBUser), refIndex.DBUser & ".", "bpm."))
            JoinString.Append(If(Not String.IsNullOrEmpty(refIndex.DBTable), refIndex.DBTable, String.Empty))
            JoinString.Append(If(Server.isSQLServer, " WITH (NOLOCK) ON ", " ON "))
            JoinString.Append(If(Not String.IsNullOrEmpty(refIndex.DBServer), refIndex.DBServer & ".", String.Empty))
            JoinString.Append(If(Not String.IsNullOrEmpty(refIndex.DBDataBase), refIndex.DBDataBase & ".", "dtbbpm."))
            JoinString.Append(If(Not String.IsNullOrEmpty(refIndex.DBUser), refIndex.DBUser & ".", "bpm."))
            JoinString.Append(If(Not String.IsNullOrEmpty(refIndex.DBTable), refIndex.DBTable & ".", String.Empty))
            JoinString.Append($"{refIndex.RefColumn} = I.I{refIndex.RefIndexId}")

        End If

        Return JoinString.ToString
    End Function

    Public Function GetFilterRefIndexRealColumn(filterString As String, refIndexes As List(Of ReferenceIndex)) As String

        Dim columnString As New StringBuilder
        Dim indexStr As String
        Dim filterRefInd As ReferenceIndex

        For Each refInd As ReferenceIndex In refIndexes
            columnString.Clear()
            indexStr = $"[I{refInd.IndexId}]"
            If filterString.Contains(indexStr) Then

                columnString.Append(If(Not String.IsNullOrEmpty(refInd.DBServer), refInd.DBServer & ".", String.Empty))
                columnString.Append(If(Not String.IsNullOrEmpty(refInd.DBDataBase), refInd.DBDataBase & ".", "dtbbpm."))
                columnString.Append(If(Not String.IsNullOrEmpty(refInd.DBUser), refInd.DBUser & ".", "bpm."))
                columnString.Append(If(Not String.IsNullOrEmpty(refInd.DBTable), refInd.DBTable & ".", String.Empty))
                columnString.Append($"{refInd.DBColumn}")

                filterString = filterString.Replace(indexStr, columnString.ToString)

            End If
        Next

        Return filterString

    End Function

    Public Function GetRefIndexRealColumn(indexId As Long, refIndexes As List(Of ReferenceIndex)) As String

        Dim realTable As New StringBuilder
        Dim refIndex As ReferenceIndex = refIndexes.Where(Function(ind) ind.IndexId = indexId).FirstOrDefault

        If refIndex IsNot Nothing Then
            realTable.Append(If(Not String.IsNullOrEmpty(refIndex.DBServer), refIndex.DBServer & ".", String.Empty))
            realTable.Append(If(Not String.IsNullOrEmpty(refIndex.DBDataBase), refIndex.DBDataBase & ".", "dtbbpm."))
            realTable.Append(If(Not String.IsNullOrEmpty(refIndex.DBUser), refIndex.DBUser & ".", "bpm."))
            realTable.Append(If(Not String.IsNullOrEmpty(refIndex.DBTable), refIndex.DBTable & ".", String.Empty))
            realTable.Append($"{refIndex.DBColumn}")
        End If

        Return realTable.ToString

    End Function

End Class
