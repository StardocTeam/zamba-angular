Public Class WFRulesParamItems
    'Public Shared Function GetAllParamItemByRuleId(ByVal ruleId As Int32) As DataSet
    '    Dim sql As String
    '    Dim Dstemp As New DataSet
    '    Try
    '        If Server.isOracle Then
    '            sql = "Select rule_id,item,c_value as value from WFRuleParamItems where Rule_id=" & ruleId
    '        Else
    '            sql = "Select * from WFRuleParamItems where Rule_id=" & ruleId
    '        End If

    '        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
    '        Return Dstemp
    '    Catch ex As Exception
    '        Return Dstemp
    '    End Try
    'End Function
End Class
