Imports System.Drawing
Public Class ZLinkLabel
    Inherits System.Windows.Forms.LinkLabel

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Dim HelpMenu As New HelpMenu
        Try
            HelpMenu.LoadContextMenu(Me, ContextMenu, Handle)
        Catch ex As Exception
            HelpMenu.LoadContextMenu(Me, ContextMenu, IntPtr.Zero)
        End Try
        'HelpMenu.LoadContextMenu(Me, Me.ContextMenu, Me.Handle)
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
        components = New System.ComponentModel.Container
        BackColor = Color.Transparent
        BorderStyle = BorderStyle.None
        TextAlign = ContentAlignment.MiddleCenter
        ForeColor = ZambaUIHelpers.GetFontsColor
        Font = ZambaUIHelpers.GetFontFamily
    End Sub

#End Region

    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(value As Color)
            MyBase.BackColor = Color.Transparent
        End Set
    End Property

    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(value As Font)
            MyBase.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        End Set
    End Property

    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As Color)
            MyBase.ForeColor = Zamba.AppBlock.ZambaUIHelpers.GetFontsColor()
        End Set
    End Property

    Public Overrides Property BorderStyle As BorderStyle
        Get
            Return MyBase.BorderStyle
        End Get
        Set(value As BorderStyle)
            MyBase.BorderStyle = BorderStyle.None
        End Set
    End Property

End Class
