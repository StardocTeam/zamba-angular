Imports Zamba.Servers

Public Class WFFactoryExt
    Shared Function GetAllWFActors() As DataTable
        Dim ds As DataSet = Server.Con.ExecuteDataset("zsp_workflow_100_GetWFActors", Nothing)
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function GetNamesAndIds() As DataTable
        Dim query As String = "Select work_id as ID, Name as NAME from WFWorkflow order by Name"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    Public Function GetWorkflowIdByRule(ruleId As Long) As Long
        Return Server.Con.ExecuteScalar("ZSP_WORKFLOW_100_GetWfIdByRuleId", New Object() {ruleId})
    End Function

End Class
