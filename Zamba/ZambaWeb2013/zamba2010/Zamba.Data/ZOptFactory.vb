Imports System.Text
Imports Zamba.Servers
Public Class ZOptFactory

    Public Shared Sub Insert(ByVal key As String, ByVal value As String)
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("INSERT INTO Zopt(Item, Value)")
        QueryBuilder.Append("VALUES('")
        QueryBuilder.Append(key)
        QueryBuilder.Append("','")
        QueryBuilder.Append(value)
        QueryBuilder.Append("')")

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)

    End Sub

    Public Shared Sub Update(ByVal key As String, ByVal value As String)
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("UPDATE Zopt SET Value='")
        QueryBuilder.Append(value)
        QueryBuilder.Append("' WHERE Item='")
        QueryBuilder.Append(key)
        QueryBuilder.Append("'")

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)

    End Sub

    Public Shared Function GetValue(ByVal key As String) As String
        Dim ReturnValue As Object
            Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("SELECT Value FROM Zopt  " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & " WHERE Item='")
        QueryBuilder.Append(key)
            QueryBuilder.Append("'")
            ReturnValue = Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString())
            QueryBuilder.Remove(0, QueryBuilder.Length)
       
        If IsNothing(ReturnValue) Then
            Return Nothing
        Else
            Return ReturnValue.ToString()
        End If

        Return Nothing
    End Function

    Public Shared Function GetValue(ByVal key As String, ByVal t As Transaction) As String
        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("SELECT Value FROM Zopt " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & " WHERE Item='")
        QueryBuilder.Append(key)
        QueryBuilder.Append("'")

        Dim ReturnValue As Object = t.Con.ExecuteScalar(t.Transaction, CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)

        If IsNothing(ReturnValue) Then
            Return Nothing
        Else
            Return ReturnValue.ToString()
        End If

        Return Nothing
    End Function

    Public Shared Function getAllOptionsValues() As DataTable
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("SELECT Item, Value FROM Zopt " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", ""))
        Dim ReturnValue As DataSet = Servers.Server.Con().ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)

        If Not IsNothing(ReturnValue) AndAlso ReturnValue.Tables.Count > 0 Then
            Return ReturnValue.Tables(0)
        End If
        Return Nothing
    End Function

    Public Shared Sub InsertUpdateValue(ByVal key As String, ByVal value As String)
        Dim query = If(Server.isOracle, "SELECT COUNT(1) FROM ZOPT WHERE ITEM = '" & key & "'", "SELECT COUNT(1) FROM ZOPT WITH(NOLOCK) WHERE ITEM = '" & key & "'")
        Dim count As Int32 = Server.Con.ExecuteScalar(CommandType.Text, query)
        If count = 0 Then
            Insert(key, value)
        Else
            Update(key, value)
        End If
    End Sub

End Class
