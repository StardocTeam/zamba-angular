<Serializable()> Public Class IndexsRightsInfo

#Region "Fields"
    Public Indexid As Int64
    'Public Name As String
    Public Search As Boolean
    Public View As Boolean
    Public Edit As Boolean
    Public Export As Boolean
    Public ViewOnTaskGrid As Boolean
    Public ExportOutlook As Boolean 
    Public Required As Boolean
    '[SEBASTIAN]
    Public DefaultSearch As Boolean
    Public AutoComplete As Boolean
#End Region


    Public Sub New(ByVal _IndexId As Int64)
        'Name = _name
        Indexid = _IndexId
    End Sub

    Public Function GetIndexRightValue(ByVal RightType As RightsType) As Boolean
        Select Case RightType
            Case RightsType.IndexEdit
                Return Edit
            Case RightsType.IndexExport
                Return Export
            Case RightsType.IndexRequired
                Return Required
            Case RightsType.IndexSearch
                Return Search
            Case RightsType.IndexView
                Return View
            Case RightsType.IndexDefaultSearch
                Return DefaultSearch
            Case RightsType.TaskGridIndexView
                Return ViewOnTaskGrid
            Case RightsType.ExportToOutlook
                Return ExportOutlook
                'Case RightsType.AutoComplete
                '    Return AutoComplete
        End Select
    End Function

End Class
