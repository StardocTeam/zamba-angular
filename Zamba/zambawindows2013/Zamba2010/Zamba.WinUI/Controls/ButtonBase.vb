Imports System.ComponentModel

<DefaultEvent("Click"), ToolboxItem(False)> _
Public Class ButtonBase
    Inherits Control
    Implements IButtonControl

    Private dialogResultValue As DialogResult = DialogResult.None
    Protected mouseIsDown, mouseOver, buttonPressed As Boolean
    Protected defaultButton As Boolean

    Protected Sub New()
        MyBase.New()
        MyBase.SetStyle(ControlStyles.CacheText, True)
        SetStyle(ControlStyles.StandardClick Or _
            ControlStyles.StandardDoubleClick, False)
    End Sub

    Protected Overrides Sub OnClick(ByVal e As EventArgs)
        Dim f As Form = FindForm()
        If Not (f Is Nothing) Then
            f.DialogResult = dialogResultValue
        End If
        MyBase.OnClick(e)
    End Sub


    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub 'OnTextChanged

#Region "mouse events"
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            mouseIsDown = True
            buttonPressed = True
            Focus()
            Invalidate()
        End If
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            mouseIsDown = False
            If buttonPressed Then
                buttonPressed = False
                Invalidate()
                OnClick(e)
            End If
        End If
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        If mouseIsDown Then
            'Comprueba que el cursor est dentro del botn
            If ClientRectangle.Contains(e.X, e.Y) <> buttonPressed Then
                buttonPressed = Not buttonPressed
                Invalidate()
            End If
        End If
        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        mouseOver = True
        MyBase.OnMouseEnter(e)
    End Sub 'OnMouseEnter

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        mouseOver = False
        MyBase.OnMouseLeave(e)
    End Sub 'OnMouseLeave
#End Region

#Region "keyboard events"

    Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Space AndAlso Not buttonPressed Then
            buttonPressed = True
            Invalidate()
            e.Handled = True
        End If
        MyBase.OnKeyDown(e)
    End Sub

    Protected Overrides Sub OnKeyUp(ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Space AndAlso buttonPressed Then
            buttonPressed = False
            Invalidate()
            e.Handled = True
            OnClick(EventArgs.Empty)
        End If
        MyBase.OnKeyUp(e)
    End Sub


    Protected Overrides Function ProcessMnemonic(ByVal charCode As Char) As Boolean
        If CanSelect AndAlso IsMnemonic(charCode, Text) Then
            PerformClick()
            Return True
        Else
            Return False
        End If
    End Function 'ProcessMnemonic
#End Region

#Region "IButtonControl Members"

    <DefaultValue(System.Windows.Forms.DialogResult.None)> _
    Public Property DialogResult() As System.Windows.Forms.DialogResult Implements IButtonControl.DialogResult
        Get
            Return dialogResultValue
        End Get
        Set(ByVal Value As System.Windows.Forms.DialogResult)
            dialogResultValue = Value
        End Set
    End Property

    Public Sub PerformClick() Implements IButtonControl.PerformClick
        OnClick(EventArgs.Empty)
    End Sub


    Public Sub NotifyDefault(ByVal value As Boolean) Implements IButtonControl.NotifyDefault
        defaultButton = value
        Invalidate()
    End Sub
#End Region
End Class 'ButtonBase 
