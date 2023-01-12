Namespace Outlook

    Public Class frmSending

        Public Sub ShowSendingForm(ByVal timeOut As Integer)
            Me.ProgressBar1.Maximum = timeOut
            Me.ProgressBar1.Minimum = 0
            Me.ProgressBar1.Step = 1
            Me.Show()

        End Sub
        Public Sub ProgressPerformStep()
            Application.DoEvents()
            Me.ProgressBar1.PerformStep()
            Me.Refresh()
            Application.DoEvents()
        End Sub
    End Class

End Namespace
