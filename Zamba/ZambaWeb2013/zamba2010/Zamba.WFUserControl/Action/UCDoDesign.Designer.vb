<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoDesign
    Inherits Zamba.WFUserControl.ZRuleControl

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
        Me.BtnSave = New Zamba.AppBlock.ZButton
        Me.txtHelp = New System.Windows.Forms.TextBox
        Me.lblHelp = New ZLabel
        Me.btnConvertir = New Zamba.AppBlock.ZButton
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.btnConvertir)
        Me.tbRule.Controls.Add(Me.lblHelp)
        Me.tbRule.Controls.Add(Me.txtHelp)
        Me.tbRule.Controls.Add(Me.BtnSave)
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnSave.Location = New System.Drawing.Point(530, 228)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(70, 27)
        Me.BtnSave.TabIndex = 33
        Me.BtnSave.Text = "Guardar"
        '
        'txtHelp
        '
        Me.txtHelp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHelp.Location = New System.Drawing.Point(15, 42)
        Me.txtHelp.Multiline = True
        Me.txtHelp.Name = "txtHelp"
        Me.txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHelp.Size = New System.Drawing.Size(585, 169)
        Me.txtHelp.TabIndex = 34
        '
        'lblHelp
        '
        Me.lblHelp.AutoSize = True
        Me.lblHelp.BackColor = System.Drawing.Color.Transparent
        Me.lblHelp.Location = New System.Drawing.Point(12, 17)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(141, 13)
        Me.lblHelp.TabIndex = 35
        Me.lblHelp.Text = "Texto de diseño de la regla:"
        '
        'btnConvertir
        '
        Me.btnConvertir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConvertir.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnConvertir.Location = New System.Drawing.Point(447, 228)
        Me.btnConvertir.Name = "btnConvertir"
        Me.btnConvertir.Size = New System.Drawing.Size(77, 27)
        Me.btnConvertir.TabIndex = 36
        Me.btnConvertir.Text = "Convertir"
        '
        'UCDoDesign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoDesign"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnSave As Zamba.AppBlock.ZButton
    Friend WithEvents txtHelp As System.Windows.Forms.TextBox
    Friend WithEvents lblHelp As ZLabel
    Friend WithEvents btnConvertir As Zamba.AppBlock.ZButton

End Class
