Imports Zamba.Core
Public Class PlayDoGetNewId
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IDoGetNewId) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Dim Results_Business As New Results_Business
            For Each r As TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando un nuevo Id...")
                For Each i As Index In r.Indexs
                    If i.ID = Int32.Parse(myrule.IndexId) Then
                        i.Data = Zamba.Data.CoreData.GetNewID(IdTypes.VARIOS).ToString
                        i.DataTemp = i.Data
                        Results_Business.SaveModifiedIndexData(DirectCast(r, Zamba.Core.Result), True, False, Nothing, Nothing)
                        NewList.Add(r)
                        Exit For
                    End If
                Next
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Nuevo Id generado con éxito")
            Next
        Finally

        End Try

        Return NewList
    End Function
End Class
