Imports System

Public Interface IPrintTreeViewHelper

    Sub PrintPreviewTree(ByVal tree As System.Windows.Forms.TreeView, ByVal title As String)
    Sub PrintTree(ByVal tree As System.Windows.Forms.TreeView, ByVal title As String)
    Sub SaveTreeBitmap(ByVal tree As System.Windows.Forms.TreeView, ByVal FileName As String)
    Sub Dispose()

End Interface
