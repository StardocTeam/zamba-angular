Imports System.Drawing.Drawing2D
Imports System.Drawing

Public Class cProgressBar
    Inherits System.Windows.Forms.Panel

    Private m_value As Integer
    Private m_maximum As Integer = 1
    Private m_fillstyle As LinearGradientMode = LinearGradientMode.ForwardDiagonal
    Private m_fill1 As Color = Color.Green
    Private m_fill2 As Color = Color.DarkGreen
    Private m_showpercent As Boolean = True

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        SetStyle(ControlStyles.ResizeRedraw, True)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
        'cProgressBar
        '
        Size = New System.Drawing.Size(392, 24)

    End Sub

#End Region


#Region " Public Properties "


    Public Property ShowPercent() As Boolean
        Get
            Return m_showpercent
        End Get
        Set(ByVal Value As Boolean)
            m_showpercent = Value
            Invalidate() ' Invalidate is used to force the control
            'to raise the onpaint event that responsible for the redraw of 
            'control 
        End Set
    End Property


    Public Property Gradient_Fill_2() As Color
        Get
            Return m_fill2
        End Get
        Set(ByVal Value As Color)
            m_fill2 = Value
            Invalidate()
        End Set
    End Property

    Public Property Gradient_Fill_1() As Color
        Get
            Return m_fill1
        End Get
        Set(ByVal Value As Color)
            m_fill1 = Value
            Invalidate()
        End Set
    End Property


    Public Property Fill() As LinearGradientMode
        Get
            Return m_fillstyle
        End Get
        Set(ByVal Value As LinearGradientMode)
            m_fillstyle = Value
            Invalidate()
        End Set
    End Property

    Public Property Maximum() As Integer
        Get
            Return m_maximum
        End Get
        Set(ByVal Value As Integer)
            If Me.Value > Value Then Value = Me.Value
            If Value < 1 Then Value = 1
            m_maximum = Value
            Invalidate()
        End Set
    End Property


    Public Property Value() As Integer
        Get
            Return m_value
        End Get
        Set(ByVal Value As Integer)
            If Value > Maximum Then Value = Maximum
            m_value = Value
            Invalidate()
        End Set
    End Property

#End Region

    Public Sub Increment()
        If Value < Maximum Then Value += 1
    End Sub

    Private Function ComputeAreaFill() As RectangleF
        Return New RectangleF(0, 0, Width * Value / Maximum, Height)
    End Function


    Private Sub DrawFillComplete(ByVal e As Graphics)
        Dim rect As RectangleF = ComputeAreaFill()
        '    Dim SBrush As SolidBrush = New SolidBrush(Gradient_Fill_1)
        Dim LBrush As LinearGradientBrush = New LinearGradientBrush(rect, m_fill1, m_fill2, Fill)
        e.FillRectangle(LBrush, rect)
    End Sub


    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        If Value > 0 Then DrawFillComplete(e.Graphics)
        If ShowPercent Then DrawPercentText(e.Graphics)
    End Sub

    Private Sub DrawPercentText(ByVal e As Graphics)
        Dim TextPercent As Integer = System.Math.Round(Value / Maximum * 100)
        Dim strsize As SizeF = e.MeasureString(TextPercent & "%", Font)
        Dim OffsetY As Integer
        Select Case BorderStyle
            Case BorderStyle.Fixed3D : OffsetY = 3
            Case Else : OffsetY = 1
        End Select
        e.DrawString(TextPercent & "%", Font, New SolidBrush(ForeColor), (Width - strsize.Width) / 2, (Height - strsize.Height) / 2 - OffsetY)
    End Sub

    Public Sub Increment(ByVal valor As Integer)
        If Value + valor <= Maximum Then
            Value += valor
        Else
            Value = Maximum
        End If
    End Sub

End Class
