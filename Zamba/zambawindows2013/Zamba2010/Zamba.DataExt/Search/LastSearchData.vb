Imports Zamba.Servers
Imports Zamba.Core.Searchs

Public Class LastSearchData

    Public Shared Function GetSearchs(ByVal userId As Int64) As DataTable
        Dim query As String = "SELECT * FROM ZLS WHERE USERID=" & userId & " ORDER BY SEARCHID"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    Public Shared Sub Delete(ByVal searchId As Int64)
        Dim query As String = "DELETE FROM ZLSQ WHERE SEARCHID=" & searchId
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

        query = "DELETE FROM ZLS WHERE SEARCHID=" & searchId
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub DeleteAll(ByVal userId As Integer)

        Dim query As String = "DELETE FROM ZLSQ WHERE SEARCHID IN (SELECT ZLS.SEARCHID FROM ZLS JOIN ZLSQ ON ZLS.SEARCHID = ZLSQ.SEARCHID WHERE ZLS.USERID = " & userId & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

        query = "DELETE FROM ZLS WHERE USERID = " & userId
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub Add(search As LastSearch, ByVal userId As Int64, t As Transaction)
        Dim query As String = "INSERT INTO ZLS VALUES(" & search.Id & "," & userId & ",'" & search.Name.Replace("'", "''") & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Function GetSQL(ByVal id As Int64) As DataTable
        Dim querySearch As String = "SELECT QUERY FROM ZLSQ WHERE SEARCHID=" & id
        'Dim queryTable As DataSet = Server.Con.ExecuteDataset(CommandType.Text,querySearch) 

        Return Server.Con.ExecuteDataset(CommandType.Text, querySearch).Tables(0)
    End Function

    Public Shared Sub AddSearchObject(search As LastSearch, t As Transaction)
        Try
            Dim query As New Text.StringBuilder()
            Dim queryinner As New Text.StringBuilder()
            Dim serializedSearch As String = search.SerializedSearch.Replace("'", "''")

            If Server.isOracle Then
                query.Append("DECLARE ")

                Dim maxCharacters As Int32 = 30000
                Dim text As String = String.Empty
                Dim resto As Int32 = 0

                Dim count As Int32 = Math.DivRem(serializedSearch.Length, maxCharacters, resto)
                If resto > 0 Then count = count + 1

                For i As Int32 = 0 To count - 1
                    If i = count - 1 Then
                        text = serializedSearch.Substring(i * maxCharacters)
                    Else
                        text = serializedSearch.Substring(i * maxCharacters, maxCharacters)
                    End If
                    query.Append(String.Format("STR{0} VARCHAR2(32767) := '{1}' ; ", i, text))
                    queryinner.Append(String.Format("vClobVal := vClobVal || STR{0}; ", i))
                Next

                query.Append("vClobVal clob; ")
                query.Append("BEGIN ")
                query.Append(queryinner.ToString())
                query.Append("INSERT INTO ZLSQ  (SearchId,Query)  VALUES(")
                query.Append(search.Id)
                query.Append(", vClobVal); ")
                query.Append("END;")
            Else
                query.Append("INSERT INTO ZLSQ (SearchId,Query) VALUES(")
                query.Append(search.Id)
                query.Append(", '")
                query.Append(serializedSearch)
                query.Append("')")
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function GetSerializedSearchObject(SearchId As Long) As String
        Dim searchString As String = "SELECT QUERY FROM ZLSQ WHERE SEARCHID=" & SearchId
        Return (Server.Con.ExecuteDataset(CommandType.Text, searchString).Tables(0))(0)(0).ToString()
    End Function
End Class
