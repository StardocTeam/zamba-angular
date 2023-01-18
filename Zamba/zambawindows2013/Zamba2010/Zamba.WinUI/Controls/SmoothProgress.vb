Imports System.Drawing.Drawing2D
Imports System.Drawing

Public Class SmoothProgress
    Public progressPanel As New Panel
    Private progressValue As Integer = 0
    Private progressMaximum As Integer = 100
    Private progressMinimum As Integer = 0
    Private color1 As Color = Color.Red
    Private color2 As Color = Color.Black
    Private tColor As Color = Color.BlanchedAlmond
    Private lastValue As Integer
    Private shwPercent As Boolean = True
    Private gradDirection As LinearGradientMode = LinearGradientMode.Vertical

    Public Enum Direction
        Horizontal = LinearGradientMode.Horizontal
        Vertical = LinearGradientMode.Vertical
        ForwardDiagonal = LinearGradientMode.ForwardDiagonal
        BackwardDiagonal = LinearGradientMode.BackwardDiagonal
    End Enum

    Private Sub DrawIt()
        'This sub is hidden from the programmer, and called only
        'by Increment() and Value()
        If progressValue = 0 Then
            progressPanel.Refresh()
        Else
            Dim newSize As Integer
            Dim min As Integer, max As Integer, val As Integer
            val = progressValue
            min = progressMinimum
            max = progressMaximum
            newSize = (((val - min) / (max - min)) * progressPanel.Width)

            Dim grap As Graphics
            Dim rec As New RectangleF(0, 0, newSize, progressPanel.Height)
            Dim nBrush As New LinearGradientBrush(rec, color1, color2, gradDirection)
            progressPanel.Refresh()
            grap = progressPanel.CreateGraphics()
            grap.FillRectangle(nBrush, rec)

            If shwPercent = True Then
                Dim fontSize As Byte = 10 'Set the font size here
                Dim pText As New Font("Times New Roman", fontSize, FontStyle.Bold)
                Dim pBrush As New SolidBrush(tColor)
                Dim strPercent As String
                Dim pLocationX As Integer, pLocationY As Integer
                strPercent = Percent & "%"
                pLocationX = (progressPanel.Width / 2) - ((strPercent.Length * fontSize) / 2)
                pLocationY = (progressPanel.Height / 2) - fontSize
                grap.DrawString(strPercent, pText, pBrush, pLocationX, pLocationY)
            End If
        End If
    End Sub

    Public Sub Increment(Optional ByVal amount As Integer = 1)
        progressValue += amount

        'In case the new value is greater than the maximum:
        If progressValue > progressMaximum Then progressValue = progressMaximum

        'In case the programmer specifies a negative number that would result in it going lower than the minimum:
        If progressValue < progressMinimum Then progressValue = progressMinimum

        DrawIt()

    End Sub

    Public Property Value() As Integer
        Get
            Return progressValue
        End Get
        Set(ByVal val As Integer)
            If val > progressMaximum Then val = progressMaximum
            If val < progressMinimum Then val = progressMinimum
            lastValue = progressValue
            progressValue = val
            DrawIt()
        End Set

    End Property

    Public Property Maximum() As Integer
        Get
            Return progressMaximum
        End Get
        Set(ByVal max As Integer)
            If max <= progressMinimum Then max = progressMinimum + 1 'Don't allow the maximum to be less than the minimum!
            If max < progressValue Then progressValue = max
            progressMaximum = max
        End Set
    End Property

    Public Property Minimum() As Integer
        Get
            Return progressMinimum
        End Get
        Set(ByVal min As Integer)
            If min < 0 Then min = 0 'Minimum cannot be less than zero.
            If min >= progressMaximum Then min = progressMaximum - 1 'Don't allow the minimum to be larger than the maximum!
            If min > progressValue Then progressValue = min
            progressMinimum = min
        End Set
    End Property

    Public ReadOnly Property Percent() As Integer
        Get
            Return ((progressValue - progressMinimum) / (progressMaximum - progressMinimum)) * 100
        End Get
    End Property

    Public Property ColorA() As Color
        Get
            Return color1
        End Get
        Set(ByVal col As Color)
            color1 = col
        End Set
    End Property

    Public Property ColorB() As Color
        Get
            Return color2
        End Get
        Set(ByVal col As Color)
            color2 = col
        End Set
    End Property

    Public Property TextColor() As Color
        Get
            Return tColor
        End Get
        Set(ByVal col As Color)
            tColor = col
        End Set
    End Property

    Public Property ShowPercent() As Boolean
        Get
            Return shwPercent
        End Get
        Set(ByVal val As Boolean)
            shwPercent = val
        End Set
    End Property

    Public Property GradientDirection() As Direction
        Get
            Return gradDirection
        End Get
        Set(ByVal direct As Direction)
            gradDirection = direct
        End Set
    End Property
End Class