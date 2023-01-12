<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VirtualDocumentSelector
    Inherits Zamba.AppBlock.ZForm

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
        Me.btCrearDocumentoVirtual = New ZButton()
        Me.cboTipoDeDocumento = New System.Windows.Forms.ListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btCrearDocumentoVirtual
        '
        Me.btCrearDocumentoVirtual.BackColor = System.Drawing.Color.FromArgb(0, 157, 224)
        Me.btCrearDocumentoVirtual.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btCrearDocumentoVirtual.ForeColor = System.Drawing.Color.White
        Me.btCrearDocumentoVirtual.Location = New System.Drawing.Point(200, 8)
        Me.btCrearDocumentoVirtual.Name = "btCrearDocumentoVirtual"
        Me.btCrearDocumentoVirtual.Size = New System.Drawing.Size(75, 23)
        Me.btCrearDocumentoVirtual.TabIndex = 1
        Me.btCrearDocumentoVirtual.Text = "Crear"
        Me.btCrearDocumentoVirtual.UseVisualStyleBackColor = False
        '
        'cboTipoDeDocumento
        '
        Me.cboTipoDeDocumento.BackColor = System.Drawing.Color.White
        Me.cboTipoDeDocumento.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.cboTipoDeDocumento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboTipoDeDocumento.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.cboTipoDeDocumento.FormattingEnabled = True
        Me.cboTipoDeDocumento.Location = New System.Drawing.Point(2, 2)
        Me.cboTipoDeDocumento.Name = "cboTipoDeDocumento"
        Me.cboTipoDeDocumento.Size = New System.Drawing.Size(316, 187)
        Me.cboTipoDeDocumento.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btCrearDocumentoVirtual)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(2, 189)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(316, 38)
        Me.Panel1.TabIndex = 4
        '
        'VirtualDocumentSelector
        '
        Me.AcceptButton = Me.btCrearDocumentoVirtual
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(320, 229)
        Me.Controls.Add(Me.cboTipoDeDocumento)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "VirtualDocumentSelector"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Selecciona un Formulario"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btCrearDocumentoVirtual As ZButton
    Friend WithEvents cboTipoDeDocumento As ListBox
    Friend WithEvents Panel1 As Panel
End Class
