Imports Zamba.Servers

Public Class WFFactoryExt
    Shared Function GetAllWFActors() As DataTable
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "SELECT USRGROUP.ID,USRGROUP.NAME, USRGROUP.DESCRIPCION, WFSTEP.step_id FROM USRGROUP INNER JOIN USR_RIGHTS ON groupid = USRGROUP.ID and objid = 42 and rtype = 19 INNER JOIN WFSTEP ON .USR_RIGHTS.aditional = step_id GROUP BY WFSTEP.step_id, USRGROUP.ID, USRGROUP.NAME, USRGROUP.DESCRIPCION")
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
        Return Server.Con.ExecuteScalar(CommandType.Text, "select s.work_id from wfstep s inner join WFRules r on s.step_Id = r.step_Id where r.Id = " & ruleId)
    End Function

End Class
