<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGeneralRulesABM
    Inherits System.Windows.Forms.Form

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.UcButtons = New Zamba.Controls.UCButtonsABM()
        Me.SuspendLayout()
        '
        'UcButtons
        '
        Me.UcButtons.BackColor = System.Drawing.Color.White
        Me.UcButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcButtons.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.UcButtons.ForeColor = System.Drawing.Color.FromArgb(76,76,76)
        Me.UcButtons.Location = New System.Drawing.Point(0, 0)
        Me.UcButtons.Name = "UcButtons"
        Me.UcButtons.Size = New System.Drawing.Size(843, 395)
        Me.UcButtons.TabIndex = 0
        '
        'FrmGeneralRulesABM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(843, 395)
        Me.Controls.Add(Me.UcButtons)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FrmGeneralRulesABM"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Reglas Generales"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcButtons As Zamba.Controls.UCButtonsABM
End Class
