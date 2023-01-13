<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoExecuteRule
    Inherits ZRuleControl

    'UserControl overrides dispose to clean up the component list.
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
    Private Overloads Sub InitializeComponent()
        Me.CBORules = New System.Windows.Forms.ComboBox()
        Me.btnGoRule = New Zamba.AppBlock.ZButton()
        Me.BtnSave = New Zamba.AppBlock.ZButton()
        Me.rbSelectRule = New System.Windows.Forms.RadioButton()
        Me.rbIDRule = New System.Windows.Forms.RadioButton()
        Me.txtIDRule = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblSaveMessage = New Zamba.AppBlock.ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.lblSaveMessage)
        Me.tbRule.Controls.Add(Me.txtIDRule)
        Me.tbRule.Controls.Add(Me.CBORules)
        Me.tbRule.Controls.Add(Me.btnGoRule)
        Me.tbRule.Controls.Add(Me.rbIDRule)
        Me.tbRule.Controls.Add(Me.rbSelectRule)
        Me.tbRule.Controls.Add(Me.BtnSave)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(836, 461)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(844, 490)
        '
        'CBORules
        '
        Me.CBORules.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBORules.FormattingEnabled = True
        Me.CBORules.Location = New System.Drawing.Point(23, 49)
        Me.CBORules.Margin = New System.Windows.Forms.Padding(4)
        Me.CBORules.Name = "CBORules"
        Me.CBORules.Size = New System.Drawing.Size(580, 22)
        Me.CBORules.TabIndex = 0
        '
        'btnGoRule
        '
        Me.btnGoRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnGoRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoRule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoRule.ForeColor = System.Drawing.Color.White
        Me.btnGoRule.Location = New System.Drawing.Point(91, 194)
        Me.btnGoRule.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGoRule.Name = "btnGoRule"
        Me.btnGoRule.Size = New System.Drawing.Size(139, 31)
        Me.btnGoRule.TabIndex = 3
        Me.btnGoRule.Text = "Ir a Regla"
        Me.btnGoRule.UseVisualStyleBackColor = False
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(263, 194)
        Me.BtnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(133, 31)
        Me.BtnSave.TabIndex = 2
        Me.BtnSave.Text = "Guardar"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'rbSelectRule
        '
        Me.rbSelectRule.AutoSize = True
        Me.rbSelectRule.BackColor = System.Drawing.Color.White
        Me.rbSelectRule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSelectRule.Location = New System.Drawing.Point(23, 23)
        Me.rbSelectRule.Margin = New System.Windows.Forms.Padding(4)
        Me.rbSelectRule.Name = "rbSelectRule"
        Me.rbSelectRule.Size = New System.Drawing.Size(119, 18)
        Me.rbSelectRule.TabIndex = 3
        Me.rbSelectRule.Text = "Seleccionar Regla"
        Me.rbSelectRule.UseVisualStyleBackColor = False
        '
        'rbIDRule
        '
        Me.rbIDRule.AutoSize = True
        Me.rbIDRule.BackColor = System.Drawing.Color.White
        Me.rbIDRule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbIDRule.Location = New System.Drawing.Point(24, 121)
        Me.rbIDRule.Margin = New System.Windows.Forms.Padding(4)
        Me.rbIDRule.Name = "rbIDRule"
        Me.rbIDRule.Size = New System.Drawing.Size(118, 18)
        Me.rbIDRule.TabIndex = 4
        Me.rbIDRule.Text = "Ingresar ID Regla"
        Me.rbIDRule.UseVisualStyleBackColor = False
        '
        'txtIDRule
        '
        Me.txtIDRule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDRule.Location = New System.Drawing.Point(23, 147)
        Me.txtIDRule.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDRule.Name = "txtIDRule"
        Me.txtIDRule.Size = New System.Drawing.Size(207, 25)
        Me.txtIDRule.TabIndex = 5
        Me.txtIDRule.Text = ""
        '
        'lblSaveMessage
        '
        Me.lblSaveMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSaveMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblSaveMessage.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblSaveMessage.FontSize = 9.75!
        Me.lblSaveMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSaveMessage.Location = New System.Drawing.Point(174, 264)
        Me.lblSaveMessage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSaveMessage.Name = "lblSaveMessage"
        Me.lblSaveMessage.Size = New System.Drawing.Size(4159, 31)
        Me.lblSaveMessage.TabIndex = 7
        Me.lblSaveMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDoExecuteRule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoExecuteRule"
        Me.Size = New System.Drawing.Size(844, 490)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CBORules As System.Windows.Forms.ComboBox
    Friend WithEvents BtnSave As AppBlock.ZButton
    Friend WithEvents txtIDRule As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents rbIDRule As System.Windows.Forms.RadioButton
    Friend WithEvents rbSelectRule As System.Windows.Forms.RadioButton
    Friend WithEvents btnGoRule As Zamba.AppBlock.ZButton
    Friend WithEvents lblSaveMessage As ZLabel
End Class