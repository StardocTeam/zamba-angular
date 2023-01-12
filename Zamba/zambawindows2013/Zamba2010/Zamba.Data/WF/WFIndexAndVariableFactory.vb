Public Class WFIndexAndVariableFactory
    Public Shared Function GetIndexAndVariable() As DataSet
        If Server.IsOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "select * from WFIndexAndVariable")
        Else
            Return Server.Con.ExecuteDataset("ZSP_100_GetIndexAndVariable")
        End If
    End Function

    Public Shared Function GetIndexAndVariableNotInRule(ByVal ruleID As Int64) As DataSet
        If Server.IsOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "select wfi.ID, wfi.Name, wfi.type from WFIndexAndVariable wfi where wfi.ID not in (select id from WFIndexAndVariable_r_WFRules wfr where wfr.rule_id=" & ruleID & ")")
        Else
            Dim parameters() As Object = {ruleID}
            Return Server.Con.ExecuteDataset("zsp_100_GetIndexAndVariableNotInRule", parameters)
        End If
    End Function

    Public Shared Function GetIndexAndVariableByRuleID(ByVal ruleID As Int64) As DataSet
        If Server.isOracle Then

            Return Server.Con.ExecuteDataset(CommandType.Text, "select wfi.ID, wfi.Name, wfi.type from WFIndexAndVariable wfi inner join WFIndexAndVariable_r_WFRules wfr on wfi.ID = wfr.ID where wfr.rule_id=" & ruleID)
        Else
            Dim parameters() As Object = {ruleID}
            Return Server.Con.ExecuteDataset("zsp_100_GetIndexAndVariableByRuleID", parameters)
        End If
    End Function

    Public Shared Sub InsertIndexAndVariableByRuleID(ByVal ID As Int64, ByVal ruleID As Int64)
        If Server.IsOracle Then
            Server.Con.ExecuteNonQuery(CommandType.Text, "Insert into WFIndexAndVariable_r_WFRules (Id, Rule_ID) values (" & ID & "," & ruleID & ")")
        Else
            Dim parameters() As Object = {ID, ruleID}
            Server.Con.ExecuteDataset("zsp_100_InsertIndexAndVariableByRuleID", parameters)
        End If
    End Sub

    Public Shared Function GetIndexAndVariableConfiguration(ByVal IndexAndVariableID As Int64) As DataSet
        If Server.IsOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "select * from WFIndexAndVariableConfig where ID=" & IndexAndVariableID)
        Else
            Dim parameters() As Object = {IndexAndVariableID}
            Return Server.Con.ExecuteDataset("zsp_100_GetIndexAndVariableConfiguration", parameters)
        End If
    End Function

    Public Shared Sub DeleteIndexAndVariable(ByVal IndexAndVariableID As Int64)
        If Server.IsOracle Then
            Server.Con.ExecuteNonQuery(CommandType.Text, "delete WFIndexAndVariable where ID=" & IndexAndVariableID)
        Else
            Dim parameters() As Object = {IndexAndVariableID}
            Server.Con.ExecuteDataset("zsp_100_DeleteIndexAndVariable", parameters)
        End If
    End Sub

    Public Shared Sub DeleteIndexAndVariableSelection(ByVal IndexAndVariableID As Int64, ByVal ruleID As Int64)
        If Server.IsOracle Then
            Server.Con.ExecuteNonQuery(CommandType.Text, "delete WFIndexAndVariable_r_wfrules where ID=" & IndexAndVariableID & " and rule_id=" & ruleID)
        Else
            Dim parameters() As Object = {IndexAndVariableID, ruleID}
            Server.Con.ExecuteDataset("zsp_100_DeleteIndexAndVariableSelection", parameters)
        End If
    End Sub

    Public Shared Sub DeleteIndexAndVariableConfiguration(ByVal IndexAndVariableID As Int64)
        If Server.IsOracle Then
            Server.Con.ExecuteNonQuery(CommandType.Text, "delete WFIndexAndVariableConfig where ID=" & IndexAndVariableID)
        Else
            Dim parameters() As Object = {IndexAndVariableID}
            Server.Con.ExecuteDataset("zsp_100_DeleteIndexAndVariableConfiguration", parameters)
        End If
    End Sub

    Public Shared Sub InsertIndexAndVariable(ByVal IndexAndVariableID As Int64, ByVal name As String, ByVal operador As String)
        If Server.IsOracle Then
            Server.Con.ExecuteNonQuery(CommandType.Text, "insert into WFIndexAndVariable (ID, name, type) values (" & IndexAndVariableID & ",'" & name & "','" & operador & "')")
        Else
            Dim parameters() As Object = {IndexAndVariableID, name, operador}
            Server.Con.ExecuteDataset("zsp_100_InsertIndexAndVariable", parameters)
        End If
    End Sub

    Public Shared Sub InsertIndexAndVariableConfig(ByVal ID As Int64, ByVal manual As String, ByVal name As String, ByVal operador As String, ByVal value As String)
        If Server.IsOracle Then
            Server.Con.ExecuteNonQuery(CommandType.Text, "insert into WFIndexAndVariableConfig (ID, manual, name, Operator, value) values (" & ID & ",'" & manual & "','" & name & "','" & operador & "','" & value & "')")
        Else
            Dim parameters() As Object = {ID, manual, name, operador, value}
            Server.Con.ExecuteDataset("zsp_100_InsertIndexAndVariableConfig", parameters)
        End If
    End Sub

    Public Shared Sub updateIndexAndVariable(ByVal IndexAndVariableID As Int64, ByVal operador As String)
        If Server.IsOracle Then
            Server.Con.ExecuteNonQuery(CommandType.Text, "update WFIndexAndVariable set type='" & operador & "' where ID=" & IndexAndVariableID)
        Else
            Dim parameters() As Object = {IndexAndVariableID, operador}
            Server.Con.ExecuteDataset("zsp_100_updateIndexAndVariable", parameters)
        End If
    End Sub
End Class
