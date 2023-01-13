<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class cmbTextoInteligente
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
        Me.lstItems = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lstItems
        '
        Me.lstItems.BackColor = System.Drawing.Color.White
        Me.lstItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstItems.ForeColor = System.Drawing.Color.Black
        Me.lstItems.FormattingEnabled = True
        Me.lstItems.Location = New System.Drawing.Point(0, 0)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(402, 219)
        Me.lstItems.Sorted = True
        Me.lstItems.TabIndex = 0
        '
        'cmbTextoInteligente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(402, 219)
        Me.Controls.Add(Me.lstItems)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "cmbTextoInteligente"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Sugerencias"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents lstItems As System.Windows.Forms.ListBox
End Class
