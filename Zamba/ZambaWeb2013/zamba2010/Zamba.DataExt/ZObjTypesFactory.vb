Imports Zamba.Servers

Public Class ZObjTypesFactory
    Shared Function GetObjecUsedObjectTypes() As DataTable
        Dim query As String = "ZSP_OBJTYPE_300_GA"
        Dim ds As DataSet = Server.Con.ExecuteDataset(query, Nothing)

        If ds Is Nothing OrElse ds.Tables.Count < 1 OrElse ds.Tables(0).Rows.Count < 1 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function
End Class
