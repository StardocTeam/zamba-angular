Imports Zamba.Core

Public Class PlayDoClearAttributeListCache
    Private _myRule As IDoClearAttributeListCache

    Sub New(ByVal rule As IDoClearAttributeListCache)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim attributeId As Int64 = _myRule.AttributeId
        Cache.DocTypesAndIndexs.hsSustIndex.Remove(attributeId)
        Cache.DocTypesAndIndexs.hsIndexsDT.Remove(attributeId)
        Cache.DocTypesAndIndexs.hsIndexsArray.Remove(attributeId)
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
