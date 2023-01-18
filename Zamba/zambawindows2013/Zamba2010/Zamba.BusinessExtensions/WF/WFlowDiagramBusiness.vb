Imports Zamba.Data

Public Class WFlowDiagramBusiness
    Public Shared Function GetAllWFs() As ArrayList
        Dim dsWf As DataSet = WFDiagramFactory.GetAllWFs
        Dim arrList As ArrayList = New ArrayList
        For Each r As DataRow In dsWf.Tables(0).Rows
            Dim sd As New WFDiagram
            sd.WorkId = r("WORK_ID")
            sd.WstatId = r("Wstat_id")
            sd.Name = r("NAME")
            sd.Description = r("Description")
            sd.InitialStepId = r("InitialStepId")
            arrList.Add(sd)
        Next
        Return arrList
    End Function
    Public Shared Function GetAllWFsByID(ByVal id As Long) As IWF
        Dim dsWf As DataSet = WFDiagramFactory.GetAllWFsByID(id)
        Dim sd As New WFDiagram
        For Each r As DataRow In dsWf.Tables(0).Rows
            sd.WorkId = r("WORK_ID")
            sd.WstatId = r("Wstat_id")
            sd.Name = r("NAME")
            sd.Description = r("Description")
            sd.InitialStepId = r("InitialStepId")
        Next
        Return sd
    End Function
    Public Shared Function GetInterfaceRules() As ArrayList
        Dim dsWf As DataSet = WFDiagramFactory.GetInterfaceRules
        Dim arrList As ArrayList = New ArrayList
        For Each r As DataRow In dsWf.Tables(0).Rows
            Dim sd As New InterfaceDiagram
            sd.ID = r("ID")
            sd.Name = r("NAME")
            sd.StepID = r("step_Id")
            arrList.Add(sd)
        Next
        Return arrList
    End Function
End Class
