Imports Zamba.Data

Public Class WFBusinessExt
    Public Function GetAllWFActors() As DataTable
        Return WFFactoryExt.GetAllWFActors()
    End Function

    Public Function GetDocTypeByWF(ByVal id As Long) As IEnumerable(Of ICore)
        Dim ds As DataSet = DocTypesFactory.GetAllWFDocTypes(id)
        If ds Is Nothing OrElse ds.Tables Is Nothing OrElse ds.Tables.Count = 0 Then
            Return Nothing
        End If
        Return From row In ds.Tables(0).Rows
               Select New ZCoreView(row("doc_type_id"), row("doc_type_name"))
    End Function

    Public Function GetNamesAndIds() As DataTable
        Dim wffe As New WFFactoryExt
        Return wffe.GetNamesAndIds
    End Function

    Public Function GetAdminSearchWorkflows() As DataTable
        Dim wffe As New WFFactoryExt
        Dim dt As DataTable = wffe.GetNamesAndIds
        wffe = Nothing

        Dim dr As DataRow = dt.NewRow
        dr.ItemArray = New Object() {-1, "Todos"}
        dt.Rows.InsertAt(dr, 0)

        Return dt
    End Function

    Public Function GetWorkflowIdByRule(ByVal ruleId As Int64) As Int64
        Dim wffe As New WFFactoryExt
        Return wffe.GetWorkflowIdByRule(ruleId)
    End Function
End Class