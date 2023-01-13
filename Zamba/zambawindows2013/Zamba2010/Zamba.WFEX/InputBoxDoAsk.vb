Imports Zamba.AppBlock

Public Class InputBoxDoAsk
    Inherits ZForm

    Dim bolTxtMode As Boolean
    Sub New(ByVal title As String, ByVal mensaje As String, ByVal defaultValue As DataTable)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = title
        Me.lblmensaje.Text = mensaje
        Me.txtinput.Visible = False
        Me.lstValue.Visible = True

        bolTxtMode = False
        If Not IsNothing(defaultValue) Then
            For Each r As DataRow In defaultValue.Rows
                Dim s As String = String.Empty
                For i As Int16 = 0 To defaultValue.Columns.Count - 1
                    If String.IsNullOrEmpty(s) Then
                        s = r(i).ToString()
                    Else
                        s = s & " - " & r(i).ToString()
                    End If
                Next
                lstValue.Items.Add(s)
            Next
        End If
    End Sub

    Sub New(ByVal title As String, ByVal mensaje As String, ByVal defaultValue As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = title
        Me.lblmensaje.Text = mensaje
        Me.txtinput.Visible = True
        Me.txtinput.Text = defaultValue
        Me.lstValue.Visible = False
        bolTxtMode = True
    End Sub

    Public ReadOnly Property Message() As String
        Get
            If bolTxtMode = True Then
                Return txtinput.Text
            Else
                Return lstValue.SelectedItem.ToString()
            End If
        End Get
    End Property

    Private Sub Accept_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Accept.Click
        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

 
    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class




