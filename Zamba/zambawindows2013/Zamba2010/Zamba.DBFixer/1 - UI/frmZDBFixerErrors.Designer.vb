<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZDBFixerErrors
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmZDBFixerErrors))
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnGenerateFile = New System.Windows.Forms.Button
        Me.txtErrors = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'btnCerrar
        '
        Me.btnCerrar.AllowDrop = True
        Me.btnCerrar.Location = New System.Drawing.Point(347, 16)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(137, 23)
        Me.btnCerrar.TabIndex = 0
        Me.btnCerrar.Text = "Cerrar Ventana"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'btnGenerateFile
        '
        Me.btnGenerateFile.AllowDrop = True
        Me.btnGenerateFile.Location = New System.Drawing.Point(187, 16)
        Me.btnGenerateFile.Name = "btnGenerateFile"
        Me.btnGenerateFile.Size = New System.Drawing.Size(137, 23)
        Me.btnGenerateFile.TabIndex = 1
        Me.btnGenerateFile.Text = "Exportar a Archivo"
        Me.btnGenerateFile.UseVisualStyleBackColor = True
        '
        'txtErrors
        '
        Me.txtErrors.AllowDrop = True
        Me.txtErrors.Location = New System.Drawing.Point(12, 59)
        Me.txtErrors.Multiline = True
        Me.txtErrors.Name = "txtErrors"
        Me.txtErrors.ReadOnly = True
        Me.txtErrors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtErrors.Size = New System.Drawing.Size(647, 440)
        Me.txtErrors.TabIndex = 2
        '
        'frmZDBFixerErrors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(671, 511)
        Me.Controls.Add(Me.txtErrors)
        Me.Controls.Add(Me.btnGenerateFile)
        Me.Controls.Add(Me.btnCerrar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmZDBFixerErrors"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zamba DBFixer - Errores del Proceso"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnGenerateFile As System.Windows.Forms.Button
    Friend WithEvents txtErrors As System.Windows.Forms.TextBox
End Class
