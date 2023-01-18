Imports Zamba.Core

Public Class ZVarsRulesRepo
    Implements IZVarsRulesRepo

    Public Function GetAll() As Object Implements IZVarsRulesRepo.GetAll
    End Function

    Public Function GetByVarName(userId As Long, taskId As Long, varName As String) As Object Implements IZVarsRulesRepo.GetByVarName

        varName = varName.Replace("'", "''")

        Dim query As String = $"Select value from Zvars Where userid = {userId} and taskid = {taskId} and varname = '{varName}'"

        Dim result = Server.Con.ExecuteDataset(CommandType.Text, query)

    End Function

    Public Function Add(userId As Long, varName As String, varValue As String, taskID As Long) As Integer Implements IZVarsRulesRepo.Add

        'Escape texto
        varName = varName.Replace("'", "''")
        varValue = varValue.Replace("'", "''")

        Dim query As String = $"Insert into Zvars (userid, taskid, varname, value) values ({userId}, {taskID}, '{varName}', '{varValue}')"

        Dim result = Server.Con.ExecuteNonQuery(CommandType.Text, query)

    End Function

    Public Function Update(userId As Long, varName As String, varValue As String, taskID As Long) As Integer Implements IZVarsRulesRepo.Update

        'Escape texto
        varName = varName.Replace("'", "''")
        varValue = varValue.Replace("'", "''")

        Dim query As String = $"Update Zvars SET value = '{varValue}' Where userid = {userId} and taskid = {taskID} and varname = '{varName}'"

        Dim result = Server.Con.ExecuteNonQuery(CommandType.Text, query)

    End Function

    Public Function Remove() As Integer Implements IZVarsRulesRepo.Remove
    End Function
End Class
