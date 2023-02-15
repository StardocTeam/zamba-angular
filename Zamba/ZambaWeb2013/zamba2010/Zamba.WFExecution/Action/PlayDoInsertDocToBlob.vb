Imports Zamba.Core

Public Class PlayDoInsertDocToBlob
    Private _myRule As IDoInsertDocToBlob

    Sub New(ByVal rule As IDoInsertDocToBlob)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Return PlayWeb(results, Nothing)
    End Function

    Function PlayWeb(ByVal results As List(Of ITaskResult), ByVal Params As Hashtable) As List(Of ITaskResult)

        Return results
    End Function
End Class
