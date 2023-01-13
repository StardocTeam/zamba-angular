Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class UCButton
    Inherits System.Windows.Forms.UserControl

    Private _Icon As Icon
    Private _highlighted As Boolean

    Const SMALLICONSIZE As Int32 = 16
    Const ENLARGEDICONSIZE As Int32 = 32

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'UCButton
        '
        BackColor = Color.FromArgb(0, 157, 224)
        ForeColor = Color.White
        Name = "UCButton"
        Size = New System.Drawing.Size(32, 32)

    End Sub

    Private Function IconSize() As Int32
        If _highlighted Then
            IconSize = ENLARGEDICONSIZE
        Else
            IconSize = SMALLICONSIZE
        End If
        Return IconSize
    End Function

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        Dim bNeedRepainting As Boolean = False
        Cursor.Current = System.Windows.Forms.Cursors.Hand
        If Not _highlighted Then
            _highlighted = True
            bNeedRepainting = True
        End If
        If bNeedRepainting Then
            Invalidate()
            MyBase.OnMouseMove(e)
        End If
    End Sub


    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        If _highlighted Then
            Cursor.Current = System.Windows.Forms.Cursors.Default
            _highlighted = False
            Invalidate()
        End If
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim iconsize As Int32 = Me.IconSize
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality
        If _Icon Is Nothing = False Then
            e.Graphics.DrawIcon(New Icon(_Icon, New System.Drawing.Size(iconsize, iconsize)), New Rectangle((Size.Width - iconsize) / 2, (Size.Height - iconsize) / 2, iconsize, iconsize))
        End If
        BringToFront()
    End Sub

    <Description("The image associated with the control"), _
        Category("Appearance")> Public Property Icon() As Icon
        Get
            Return _Icon
        End Get
        Set(ByVal Value As Icon)
            _Icon = Value
            Invalidate()
        End Set
    End Property

End Class
