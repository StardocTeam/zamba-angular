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
        Me.btCrearDocumentoVirtual = New System.Windows.Forms.Button
        Me.cboTipoDeDocumento = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'btCrearDocumentoVirtual
        '
        Me.btCrearDocumentoVirtual.Location = New System.Drawing.Point(233, 49)
        Me.btCrearDocumentoVirtual.Name = "btCrearDocumentoVirtual"
        Me.btCrearDocumentoVirtual.Size = New System.Drawing.Size(75, 23)
        Me.btCrearDocumentoVirtual.TabIndex = 1
        Me.btCrearDocumentoVirtual.Text = "Crear"
        Me.btCrearDocumentoVirtual.UseVisualStyleBackColor = True
        '
        'cboTipoDeDocumento
        '
        Me.cboTipoDeDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoDeDocumento.FormattingEnabled = True
        Me.cboTipoDeDocumento.Location = New System.Drawing.Point(12, 12)
        Me.cboTipoDeDocumento.Name = "cboTipoDeDocumento"
        Me.cboTipoDeDocumento.Size = New System.Drawing.Size(243, 21)
        Me.cboTipoDeDocumento.Sorted = True
        Me.cboTipoDeDocumento.TabIndex = 2
        '
        'VirtualDocumentSelector
        '
        Me.AcceptButton = Me.btCrearDocumentoVirtual
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(320, 84)
        Me.Controls.Add(Me.cboTipoDeDocumento)
        Me.Controls.Add(Me.btCrearDocumentoVirtual)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "VirtualDocumentSelector"
        Me.Text = "Seleccione un Formulario"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btCrearDocumentoVirtual As System.Windows.Forms.Button
    Friend WithEvents cboTipoDeDocumento As System.Windows.Forms.ComboBox
End Class
