Public Class PlayDoWaitForDocument

    Private myRule As IDoWaitForDocument

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            WFBusiness.InsertWaitDoc(myRule)
        Finally

        End Try


        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoWaitForDocument)
        myRule = rule
    End Sub
End Class
