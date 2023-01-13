Imports Zamba.Data

Public Class ZObjTypesBussines
    Shared Function GetObjecUsedObjectTypes() As DataTable
        Return ZObjTypesFactory.GetObjecUsedObjectTypes()
    End Function
End Class
