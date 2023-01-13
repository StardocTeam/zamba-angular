<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDocTypes_Outlook
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmd_Cancelar = New System.Windows.Forms.Button
        Me.cmd_siguiente = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(13, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(249, 293)
        Me.Panel1.TabIndex = 0
        '
        'cmd_Cancelar
        '
        Me.cmd_Cancelar.Location = New System.Drawing.Point(281, 282)
        Me.cmd_Cancelar.Name = "cmd_Cancelar"
        Me.cmd_Cancelar.Size = New System.Drawing.Size(75, 23)
        Me.cmd_Cancelar.TabIndex = 1
        Me.cmd_Cancelar.Text = "Cancelar"
        Me.cmd_Cancelar.UseVisualStyleBackColor = True
        '
        'cmd_siguiente
        '
        Me.cmd_siguiente.Location = New System.Drawing.Point(363, 282)
        Me.cmd_siguiente.Name = "cmd_siguiente"
        Me.cmd_siguiente.Size = New System.Drawing.Size(75, 23)
        Me.cmd_siguiente.TabIndex = 2
        Me.cmd_siguiente.Text = "Siguiente"
        Me.cmd_siguiente.UseVisualStyleBackColor = True
        '
        'frmDocTypes_Outlook
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 316)
        Me.Controls.Add(Me.cmd_siguiente)
        Me.Controls.Add(Me.cmd_Cancelar)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDocTypes_Outlook"
        Me.Text = "Tipos de documentos"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmd_Cancelar As System.Windows.Forms.Button
    Friend WithEvents cmd_siguiente As System.Windows.Forms.Button
End Class
