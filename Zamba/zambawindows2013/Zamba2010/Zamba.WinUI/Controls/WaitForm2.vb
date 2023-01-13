Public Class WaitForm2
    Public X As Integer
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()


        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As EventArgs) Handles Timer1.Tick
        Close()
    End Sub

    Private Sub WaitForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Start()

    End Sub
End Class