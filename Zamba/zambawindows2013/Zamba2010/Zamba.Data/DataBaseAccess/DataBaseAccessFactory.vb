Public Class DataBaseAccessFactory
    Public Shared Function GetQuerys() As DataSet
        Dim sql As String = "Select * from Zqueryname"

        Return Servers.Server.Con(True).ExecuteDataset(CommandType.Text, sql)
    End Function

    Public Shared Function ExecuteAndGetDs(ByVal ServerType As String, ByVal ServerName As String, ByVal DBName As String, ByVal DBUser As String, ByVal DBPassword As String, ByVal sql As String) As DataSet
        Dim Con As IConnection = Server.Con(ServerType, ServerName, DBName, DBUser, DBPassword)

        Return Con.ExecuteDataset(CommandType.Text, sql)
    End Function
    Public Shared Function ExecuteAndGetDs(ByVal sql As String) As DataSet
        Dim Con As IConnection = Server.Con()

        Return Con.ExecuteDataset(CommandType.Text, sql)
    End Function


    Public Shared Function GetTabla(ByVal IDConsulta As Int32) As String
        Dim sql As New System.Text.StringBuilder
        Sql.Append("select SelectTable from ZQColumns where ID=")
        sql.Append(IDConsulta)

        Return Server.Con.ExecuteScalar(CommandType.Text, sql.ToString)
    End Function

    Public Shared Function GetDscolumns(ByVal IDConsulta As Int32) As DataSet
        Dim sql As New System.Text.StringBuilder
        sql.Append("Select SelectColumns from ZQColumns where Id=")
        sql.Append(IDConsulta)

        Return Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
    End Function
    Public Shared Function GetDsClaves(ByVal IDConsulta As Int32) As DataSet
        Dim sql As New System.Text.StringBuilder
        Sql.Append("Select Claves from ZQKeys where ID=")
        sql.Append(IDConsulta)

        Return Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
    End Function

    Public Shared Function GetAllZQColumnsDs(ByVal id As Int32) As DataSet
        Dim sql As String = "Select * from zqcolumns where id=" & id
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Return ds
    End Function


End Class
