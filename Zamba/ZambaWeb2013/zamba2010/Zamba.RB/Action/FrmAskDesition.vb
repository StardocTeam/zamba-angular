Public Class FrmAskDesition
    Private _Ask_Desition As String

    Public Property Ask_Desition() As String
        Get
            Return _Ask_Desition
        End Get
        Set(ByVal Value As String)
            _Ask_Desition = Value
        End Set
    End Property

    Private Sub ButtonSI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonYES.Click
        'Dim Valor As String
        'Valor = "SI"
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        Me._Ask_Desition = "SI"
    End Sub

    Private Sub ButtonNO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNO.Click
        'Dim Valor As String
        'Valor = "NO"
        Me.DialogResult = Windows.Forms.DialogResult.No
        Me._Ask_Desition = "NO"
    End Sub
End Class