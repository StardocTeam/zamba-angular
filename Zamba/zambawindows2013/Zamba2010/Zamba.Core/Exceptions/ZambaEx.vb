Imports System.Windows.Forms

Public Class ZambaEx
    Inherits Exception

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    Public Property Icon() As MessageBoxIcon

    Public Sub New(ByVal message As String, _
                   ByVal innerException As Exception, _
                   Optional ByVal icon As MessageBoxIcon = MessageBoxIcon.Error)
        MyBase.New(message, innerException)
        Me.Icon = icon
    End Sub

    Public Sub New(message As String, icon As MessageIcon)
        MyBase.New(message)
        Me.Icon = DirectCast(icon, MessageBoxIcon)
    End Sub

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    Public Enum MessageIcon
        None = 0
        [Error] = 16
        Question = 32
        Exclamation = 48
        Information = 64
    End Enum
End Class
