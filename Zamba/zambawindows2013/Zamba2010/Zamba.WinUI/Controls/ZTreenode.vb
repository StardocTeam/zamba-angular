Public Class ZTreenode
    Inherits System.Windows.Forms.TreeNode

    Public Shared Event ShowWait(ByVal Estado As Boolean, ByVal Cancel As Boolean)

    Protected Shared Sub MostrarWait(ByVal Estado As Boolean, Optional ByVal Cancel As Boolean = False)
        RaiseEvent ShowWait(Estado, Cancel)
    End Sub

    Public Sub New()
        MyBase.New
        Dim HelpMenu As New HelpMenu
        'Se pone este Try Catch porque no habia manera de preguntar si handle es nothing
        Try
            HelpMenu.LoadContextMenu(Me, ContextMenu, Handle)
        Catch ex As Exception
            HelpMenu.LoadContextMenu(Me, ContextMenu, IntPtr.Zero)
        End Try

    End Sub


End Class
