Imports Telerik.WinControls.UI

Public Interface IPrintTreeViewHelper

    Sub PrintPreviewTree(ByVal tree As RadTreeView, ByVal title As String)
    Sub PrintTree(ByVal tree As RadTreeView, ByVal title As String)
    Sub SaveTreeBitmap(ByVal tree As RadTreeView, ByVal FileName As String)
    Sub Dispose()

End Interface
