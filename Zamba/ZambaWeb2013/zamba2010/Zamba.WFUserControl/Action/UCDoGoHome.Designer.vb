<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoGoHome
    Inherits ZRuleControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.RulePanel = New Zamba.AppBlock.ZPanel()
        Me.lblRuleName = New Zamba.AppBlock.ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.RulePanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.RulePanel)
        '
        'RulePanel
        '
        Me.RulePanel.BackColor = System.Drawing.Color.White
        Me.RulePanel.Controls.Add(Me.lblRuleName)
        Me.RulePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RulePanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RulePanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.RulePanel.Location = New System.Drawing.Point(3, 3)
        Me.RulePanel.Name = "RulePanel"
        Me.RulePanel.Size = New System.Drawing.Size(610, 623)
        Me.RulePanel.TabIndex = 0
        '
        'lblRuleName
        '
        Me.lblRuleName.AutoSize = True
        Me.lblRuleName.BackColor = System.Drawing.Color.Transparent
        Me.lblRuleName.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblRuleName.FontSize = 10.0!
        Me.lblRuleName.ForeColor = System.Drawing.Color.Black
        Me.lblRuleName.Location = New System.Drawing.Point(5, 4)
        Me.lblRuleName.Name = "lblRuleName"
        Me.lblRuleName.Size = New System.Drawing.Size(235, 17)
        Me.lblRuleName.TabIndex = 0
        Me.lblRuleName.Text = "Dirigirse al Home de la aplicacion"
        Me.lblRuleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDoGoHome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoGoHome"
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.RulePanel.ResumeLayout(False)
        Me.RulePanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RulePanel As ZPanel
    Friend WithEvents lblRuleName As ZLabel
End Class
