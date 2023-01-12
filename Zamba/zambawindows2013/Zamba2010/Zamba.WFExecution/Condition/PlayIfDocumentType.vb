Public Class PlayIfDocumentType

    Private _myRule As IIfDocumentType

    Sub New(ByVal rule As IIfDocumentType)
        _myRule = rule
    End Sub
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ingreso al PLAY de IfDocumentType")
        Select Case _myRule.Comp
            Case Comparators.Equal
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparo por =")
                For Each r As TaskResult In results
                    If (r.DocType.ID = _myRule.DocTypeId) = ifType Then
                        NewList.Add(r)
                    End If
                Next
            Case Comparators.Different
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparo por Distinto")
                For Each r As TaskResult In results
                    If (r.DocType.ID <> _myRule.DocTypeId) = ifType Then
                        NewList.Add(r)
                    End If
                Next
        End Select

        Return NewList
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
