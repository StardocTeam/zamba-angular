Imports System.Drawing
Public Class ZLabel
    Inherits Label

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.

        Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        ForeColor = Zamba.AppBlock.ZambaUIHelpers.GetFontsColor()
        TextAlign = ContentAlignment.MiddleLeft
        BorderStyle = BorderStyle.None
        BackColor = Color.Transparent
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

    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(value As Color)
            MyBase.BackColor = value
        End Set
    End Property

    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(value As Font)
            MyBase.Font = value
        End Set
    End Property

    Public Property FontSize As Single
        Get
            Return Font.Size
        End Get
        Set(value As Single)
            Font = New Font(Zamba.AppBlock.ZambaUIHelpers.GetFontFamily().FontFamily, value)
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

    Public Overrides Property BorderStyle As BorderStyle
        Get
            Return MyBase.BorderStyle
        End Get
        Set(value As BorderStyle)
            MyBase.BorderStyle = BorderStyle.None
        End Set
    End Property

#End Region


End Class