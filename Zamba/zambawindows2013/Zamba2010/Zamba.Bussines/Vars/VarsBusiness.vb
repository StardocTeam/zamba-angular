Public Class VarsBusiness

    Public Function PersistVariable(VarName As String, TaskId As Int64, VarValue As String) As Boolean
        Zamba.Servers.Server.Con().ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO Zvars ([VarName],[TaskId],[Value]) VALUES('{0}',{1},'{2}')", VarName, TaskId, VarValue))
    End Function

    Public Function GetVariableValue(VarName, TaskId) As String
        Return Zamba.Servers.Server.Con().ExecuteScalar(CommandType.Text, String.Format("SELECT Value FROM zvars where VarName = '{0}' and TaskId = {1}", VarName, TaskId))
    End Function

End Class
