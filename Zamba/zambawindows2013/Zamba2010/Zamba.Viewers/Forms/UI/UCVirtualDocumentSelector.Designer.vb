<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCVirtualDocumentSelector
    Inherits Zamba.AppBlock.ZControl

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
        Me.cboTipoDeDocumento = New System.Windows.Forms.ListBox()
        Me.SuspendLayout
        '
        'cboTipoDeDocumento
        '
        Me.cboTipoDeDocumento.BackColor = System.Drawing.Color.White
        Me.cboTipoDeDocumento.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.cboTipoDeDocumento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboTipoDeDocumento.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily
        Me.cboTipoDeDocumento.ForeColor = Zamba.AppBlock.ZambaUIHelpers.GetFontsColor
        Me.cboTipoDeDocumento.FormattingEnabled = true
        Me.cboTipoDeDocumento.ItemHeight = 32
        Me.cboTipoDeDocumento.Location = New System.Drawing.Point(0, 0)
        Me.cboTipoDeDocumento.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.cboTipoDeDocumento.Name = "cboTipoDeDocumento"
        Me.cboTipoDeDocumento.Size = New System.Drawing.Size(853, 564)
        Me.cboTipoDeDocumento.TabIndex = 3
        '
        'UCVirtualDocumentSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16!, 32!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cboTipoDeDocumento)
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Name = "UCVirtualDocumentSelector"
        Me.Size = New System.Drawing.Size(853, 564)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents cboTipoDeDocumento As ListBox
End Class
