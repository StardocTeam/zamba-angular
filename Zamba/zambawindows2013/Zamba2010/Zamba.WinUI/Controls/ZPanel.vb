Public Class ZPanel
    Inherits System.windows.forms.Panel

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Try


            If Parent IsNot Nothing Then
                path = Parent.Name & "-" & Name
            Else
                path = Name
            End If
            Dim HelpMenu As New HelpMenu
            HelpMenu.LoadContextMenu(Me, ContextMenu, Handle)
        Catch ex As Exception
        End Try
    End Sub

    'UserControl1 overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'ZPanel
        '
        BackColor = zamba.AppBlock.ZambaUIHelpers.GetPanelBackGroundsColor
        Font = ZambaUIHelpers.GetFontFamily
        ForeColor = ZambaUIHelpers.GetFontsColor
        BorderStyle = BorderStyle.None

    End Sub

#End Region


End Class
