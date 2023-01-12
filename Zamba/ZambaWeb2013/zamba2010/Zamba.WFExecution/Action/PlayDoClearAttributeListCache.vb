Public Class PlayDoClearAttributeListCache
    Private _myRule As IDoClearAttributeListCache

    Sub New(ByVal rule As IDoClearAttributeListCache)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As List(Of Core.ITaskResult)) As List(Of ITaskResult)


        Dim index As IIndex = ZCore.GetInstance().GetIndex(_myRule.AttributeId)

            If index.DropDown = IndexAdditionalType.AutoSustitución _
                OrElse index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then

                If Cache.DocTypesAndIndexs.hsIndexsDT.ContainsKey(index.ID) Then
                    Cache.DocTypesAndIndexs.hsIndexsDT.Remove(index.ID)
                End If
            End If

            If index.DropDown = IndexAdditionalType.DropDown _
                OrElse index.DropDown = IndexAdditionalType.DropDownJerarquico Then

                If Cache.DocTypesAndIndexs.hsIndexsArray.ContainsKey(index.ID) Then
                    Cache.DocTypesAndIndexs.hsIndexsArray.Remove(index.ID)
                End If
            End If

            If index.DropDown = IndexAdditionalType.DropDownJerarquico _
                OrElse index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then

                If Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue.Keys.Count > 0 Then
                    Dim keys(Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue.Keys.Count) As String
                    Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue.Keys.CopyTo(keys, 0)

                    For Each key As String In keys
                        If key IsNot Nothing AndAlso key.Contains(index.ID) Then
                            Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue.Remove(key)
                        End If
                    Next
                End If
            End If

        Return results
    End Function

End Class
