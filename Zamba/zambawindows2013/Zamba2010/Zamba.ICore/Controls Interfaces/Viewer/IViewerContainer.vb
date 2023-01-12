Public Interface IViewerContainer

    Sub Split(ByVal Viewer As TabPage, ByVal Splited As Boolean)
    Property Name As String
End Interface

Public Interface IMenuContextContainer
    Function GetSelectedResults() As List(Of IResult)
    Sub RefreshResults()

    Sub currentContextMenuClick(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer)

End Interface
