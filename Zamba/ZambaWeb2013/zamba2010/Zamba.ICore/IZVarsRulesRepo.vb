

Public Interface IZVarsRulesRepo

    Function GetAll() As Object

    Function GetByVarName(userId As Long, taskID As Long, varName As String) As Object

    Function Add(userId As Long, varName As String, varValue As String, taskID As Long) As Integer

    Function Update(userId As Long, varName As String, varValue As String, taskID As Long) As Integer

    Function Remove() As Integer


End Interface
