<Serializable()> Public Class AssociatedIndexsRightsInfo

#Region "Fields"
    Public ParentDocType As Int64
    Public DocType As Int64
    Public Indexid As Int64
    Public GroupId As Int64
    Public Name As String
    Public View As Boolean
    
#End Region

    Public Sub New(ByVal _ParentDocType As Int64, ByVal _DocType As Int64, ByVal _IndexId As Int64, ByVal _name As String)
        ParentDocType = _ParentDocType
        DocType = _DocType
        Name = _name
        Indexid = _IndexId
        Me.View = True
    End Sub

    Public Function GetIndexRightValue(ByVal RightType As RightsType) As Boolean
        Select Case RightType
            Case RightsType.AssociateIndexView
                Return View
        End Select
    End Function

    Public Sub DisableIndexRightValue(ByVal RightTypeId As Int64)
        Select Case RightTypeId
            Case RightsType.AssociateIndexView
                Me.View = False
        End Select
    End Sub

End Class
