Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class BaseNote
    Inherits System.Windows.Forms.UserControl

    Public title As String
    Public Notetext As String
    Public Id As Integer
    'TODO Falta hacer validacion interna de las fechas
    Public NoteDate As String
    Public NoteTime As String
    Public UserName As String
    Public UserApellidos As String
    Public UserID As Integer
    Public Edited As Boolean
    Public IsLoading As Boolean
    Public Type As Int32
    Public ImageId As Int32
    Public SignPath As String
    Public Encrypted As Boolean

    Enum Modes
        Small
        Large
    End Enum

    'Private Sub InitializeComponent()
    '    '
    '    'BaseNote
    '    '
    '    Me.Name = "BaseNote"

    'End Sub
End Class
