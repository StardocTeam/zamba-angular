Imports System.ComponentModel
Imports System.Drawing

<ToolboxItem(True), ToolboxBitmap(GetType(ZButton))>
Public Class ZButton
    Inherits Button

    Public Sub New()
        BackColor = ZambaUIHelpers.GetToolbarsAndButtonsColor
        ForeColor = Color.White
        FlatStyle = FlatStyle.Flat
        Dim HelpMenu As New HelpMenu
        HelpMenu.LoadContextMenu(Me, ContextMenu, Handle)
        TextAlign = ContentAlignment.MiddleCenter
        Padding = New Padding(0)
    End Sub

    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(value As Color)
            MyBase.BackColor = value
        End Set
    End Property

    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As Color)
            MyBase.ForeColor = value
        End Set
    End Property


End Class 'RoundButton 
