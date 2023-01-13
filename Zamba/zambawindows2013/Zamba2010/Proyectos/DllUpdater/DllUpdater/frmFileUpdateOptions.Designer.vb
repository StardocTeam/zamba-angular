<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileUpdateOptions
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnOverwrite = New System.Windows.Forms.Button
        Me.btnUpdateServerFile = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'btnOverwrite
        '
        Me.btnOverwrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOverwrite.Location = New System.Drawing.Point(140, 56)
        Me.btnOverwrite.Name = "btnOverwrite"
        Me.btnOverwrite.Size = New System.Drawing.Size(95, 23)
        Me.btnOverwrite.TabIndex = 1
        Me.btnOverwrite.Text = "Sobreescribir"
        Me.btnOverwrite.UseVisualStyleBackColor = True
        '
        'btnUpdateServerFile
        '
        Me.btnUpdateServerFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateServerFile.Location = New System.Drawing.Point(241, 56)
        Me.btnUpdateServerFile.Name = "btnUpdateServerFile"
        Me.btnUpdateServerFile.Size = New System.Drawing.Size(178, 23)
        Me.btnUpdateServerFile.TabIndex = 2
        Me.btnUpdateServerFile.Text = "Actualizar Archivo de Servidor"
        Me.btnUpdateServerFile.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(425, 56)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmFileUpdateOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(637, 91)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnUpdateServerFile)
        Me.Controls.Add(Me.btnOverwrite)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmFileUpdateOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmFileUpdateOptions"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOverwrite As System.Windows.Forms.Button
    Friend WithEvents btnUpdateServerFile As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
