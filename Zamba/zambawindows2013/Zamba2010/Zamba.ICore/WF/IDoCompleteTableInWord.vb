Public Interface IDoCompleteTableInWord

    Property TableIndex() As Int64
    Property PageIndex() As Int64
    Property FullPath() As String
    Property VarName() As String
    Property WithHeader() As Boolean
    Property DataTable() As String
    Property InTable() As Boolean
    Property RowNumber() As Int64
    Property FontConfig() As Boolean
    Property Font() As String
    Property FontSize() As Single
    Property Style() As Int32
    Property Color() As String
    Property BackColor() As String
    Property SaveOriginalPath() As Boolean

End Interface