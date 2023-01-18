Imports System.ComponentModel
Imports System.Drawing

<ToolboxItem(True), ToolboxBitmap(GetType(ZWhiteButton))>
Public Class ZWhiteButton
    Inherits Button

    Public Sub New()
        BackColor = Color.White
        ForeColor = ZambaUIHelpers.GetFontsColor
        FlatStyle = FlatStyle.Flat
        FlatAppearance.BorderColor = Color.White
    End Sub

    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(value As Color)
            MyBase.BackColor = Color.White
        End Set
    End Property

    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As Color)
            MyBase.ForeColor = ZambaUIHelpers.GetFontsColor
        End Set
    End Property

    Public Shadows Property FlatStyle As FlatStyle
        Get
            Return MyBase.FlatStyle
        End Get
        Set(value As FlatStyle)
            MyBase.FlatStyle = FlatStyle.Flat
        End Set
    End Property


End Class 'RoundButton 
