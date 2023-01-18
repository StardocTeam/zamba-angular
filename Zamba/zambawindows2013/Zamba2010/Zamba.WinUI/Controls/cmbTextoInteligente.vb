Public Class cmbTextoInteligente


   Public Sub New()
        InitializeComponent()
        Text = Text.Trim
    End Sub

    Dim _blnAcept As Boolean

    Public ReadOnly Property blnAcept() As Boolean
        Get
            Return _blnAcept
        End Get
    End Property


    Private Sub lstItems_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstItems.MouseDoubleClick
        _blnAcept = True
        Close()
    End Sub

    Private Sub lstItems_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstItems.KeyPress
        If e.KeyChar = Char.ConvertFromUtf32(System.Windows.Forms.Keys.Escape) Or e.KeyChar = Char.ConvertFromUtf32(System.Windows.Forms.Keys.Back) Then
            _blnAcept = False
            Close()
        ElseIf e.KeyChar = Char.ConvertFromUtf32(System.Windows.Forms.Keys.Enter) OrElse e.KeyChar = Char.ConvertFromUtf32(System.Windows.Forms.Keys.Space) Then
            _blnAcept = True
            Close()
        End If
    End Sub
End Class