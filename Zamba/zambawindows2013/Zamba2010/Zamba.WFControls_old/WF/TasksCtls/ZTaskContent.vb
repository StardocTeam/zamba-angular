
Imports Zamba.Core

Namespace WF.TasksCtls
    Public Class ZTaskContent
        Inherits TabPage
        Implements IDisposable

        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If Not isDisposed Then
                If disposing Then
                    If TaskResult IsNot Nothing Then TaskResult = Nothing
                    'If TaskResult IsNot Nothing Then TaskResult.Dispose()
                End If
                MyBase.Dispose(disposing)
                isDisposed = True
            End If
        End Sub

        Private isDisposed As Boolean
        Public TaskResult As ITaskResult
        Public Sub New(ByRef TaskResult As ITaskResult)
            Me.TaskResult = TaskResult
        End Sub

        Public ReadOnly Property TaskViewer() As UCTaskViewer
            Get
                If Controls.Count > 0 Then
                    Return DirectCast(Controls(0), UCTaskViewer)
                Else
                    Return Nothing
                End If

            End Get
        End Property
    End Class
End Namespace