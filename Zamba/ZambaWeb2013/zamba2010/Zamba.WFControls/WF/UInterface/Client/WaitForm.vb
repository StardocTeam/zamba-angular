Public Class WaitForm


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        TopMost = True

        ' Add any initialization after the InitializeComponent() call.
        Show()
        BringToFront()
    End Sub

    Public Sub New(ByVal Message As String)
        Me.New()
        ShowMessage(Message)
    End Sub

    Delegate Sub DShowMessage(ByVal Message As String)

    Public Sub ShowMessage(ByVal Message As String)
        Show()
        Invoke(New DShowMessage(AddressOf setmessage), New Object() {Message})
 
    End Sub


    Private Sub setmessage(ByVal message As String)
        Label1.Text = Message
        Application.DoEvents()
        BringToFront()
    End Sub


End Class