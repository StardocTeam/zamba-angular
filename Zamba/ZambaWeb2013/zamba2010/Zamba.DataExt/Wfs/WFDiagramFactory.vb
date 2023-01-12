Imports Zamba.Servers
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Servers.Server
Imports Zamba.Core.ZClass
Imports System.Text
Imports System.Collections.Generic

Public Class WFDiagramFactory
    Public Shared Function GetAllWFs() As DataSet
        Dim StrSelect As String
        StrSelect = "SELECT work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId FROM WFWorkflow "
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Return Dstemp
    End Function
    Public Shared Function GetAllWFsByID(ByVal id As Long) As DataSet
        Dim StrSelect As String
        StrSelect = "SELECT work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId FROM WFWorkflow where work_id = " & id.ToString()
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Return Dstemp
    End Function
    Public Shared Function GetInterfaceRules() As DataSet
        Dim StrSelect As String = "select r.Id, r.Name, r.step_Id, r.Type, r.ParentId, p.value " & _
                                    "from wfrules r inner join wfruleparamitems p on p.Rule_id=r.Id " & _
                                    "where (r.class='DoConsumeWebService' and p.Item=1) or (r.class='DoSelect' and p.Item=3 and p.Value<>'')"

        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Return Dstemp
    End Function
End Class
