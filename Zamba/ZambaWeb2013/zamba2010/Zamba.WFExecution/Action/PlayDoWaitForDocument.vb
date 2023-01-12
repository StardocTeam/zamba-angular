Imports Zamba.Core
Imports Zamba

Public Class PlayDoWaitForDocument

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDoWaitForDocument) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFB As New WFBusiness
        Try
            WFB.InsertWaitDoc(myRule)
        Finally
            WFB = Nothing
        End Try


        Return results
    End Function

End Class
