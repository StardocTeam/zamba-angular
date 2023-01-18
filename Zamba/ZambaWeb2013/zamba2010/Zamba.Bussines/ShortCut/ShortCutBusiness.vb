Imports Zamba.Tools
Public Class ShortCutBusiness


    Public Shared Sub ValidateShortcutInFolder(ByVal FolderFullName As String, ByVal TargetPath As String)
        ShortcutHandler.ValidateShortcutInFolder(FolderFullName, TargetPath)
    End Sub


    Public Shared Sub ValidateShortcutInFolder(ByVal FolderFullName As String, ByVal TargetPath As String, ByVal shortcutName As String, Optional ByVal deleteShortcutNames As Generic.List(Of String) = Nothing, Optional ByVal deleteShortcutTargets As Generic.List(Of String) = Nothing)
        ShortcutHandler.ValidateShortcutInFolder(FolderFullName, TargetPath, shortcutName, deleteShortcutNames, deleteShortcutTargets)
    End Sub

End Class
