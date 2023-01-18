Public Class PlayDoGetNewId

    Private myrule As IDoGetNewId
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            For Each r As TaskResult In results
                For Each i As Index In r.Indexs
                    If i.ID = Int32.Parse(myrule.IndexId) Then
                        i.Data = Zamba.Data.CoreData.GetNewID(IdTypes.VARIOS).ToString
                        i.DataTemp = i.Data

                        Dim rstBuss As New Results_Business()
                        rstBuss.SaveModifiedIndexData(DirectCast(r, Zamba.Core.Result), True, False)
                        rstBuss = Nothing

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

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoGetNewId)
        myrule = rule
    End Sub
End Class
