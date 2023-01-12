<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.AxWebBrowser1 = New System.Windows.Forms.WebBrowser
        Me.tbHtml = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'AxWebBrowser1
        '
        Me.AxWebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.AxWebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.AxWebBrowser1.Name = "AxWebBrowser1"
        Me.AxWebBrowser1.Size = New System.Drawing.Size(20, 20)
        Me.AxWebBrowser1.TabIndex = 0
        Me.AxWebBrowser1.Visible = False
        '
        'tbHtml
        '
        Me.tbHtml.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbHtml.Location = New System.Drawing.Point(0, 0)
        Me.tbHtml.Multiline = True
        Me.tbHtml.Name = "tbHtml"
        Me.tbHtml.Size = New System.Drawing.Size(462, 319)
        Me.tbHtml.TabIndex = 1
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 319)
        Me.Controls.Add(Me.tbHtml)
        Me.Controls.Add(Me.AxWebBrowser1)
        Me.Name = "MainForm"
        Me.Text = "MainForm1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AxWebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents tbHtml As System.Windows.Forms.TextBox
End Class
