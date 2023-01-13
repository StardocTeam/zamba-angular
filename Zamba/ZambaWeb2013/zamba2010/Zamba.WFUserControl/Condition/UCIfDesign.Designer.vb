<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCIfDesign
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
        Me.txtHelp = New System.Windows.Forms.TextBox
        Me.BtnSave = New Zamba.AppBlock.ZButton
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
        Me.tbRule.Controls.Add(Me.BtnSave)
        Me.tbRule.Controls.Add(Me.txtHelp)
        '
        'txtHelp
        '
        Me.txtHelp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHelp.Location = New System.Drawing.Point(20, 48)
        Me.txtHelp.Multiline = True
        Me.txtHelp.Name = "txtHelp"
        Me.txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHelp.Size = New System.Drawing.Size(576, 202)
        Me.txtHelp.TabIndex = 0
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnSave.Location = New System.Drawing.Point(526, 266)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(70, 27)
        Me.BtnSave.TabIndex = 34
        Me.BtnSave.Text = "Guardar"
        '
        'lblHelp
        '
        Me.lblHelp.AutoSize = True
        Me.lblHelp.BackColor = System.Drawing.Color.Transparent
        Me.lblHelp.Location = New System.Drawing.Point(17, 23)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(141, 13)
        Me.lblHelp.TabIndex = 35
        Me.lblHelp.Text = "Texto de diseño de la regla:"
        '
        'btnConvertir
        '
        Me.btnConvertir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConvertir.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnConvertir.Location = New System.Drawing.Point(443, 266)
        Me.btnConvertir.Name = "btnConvertir"
        Me.btnConvertir.Size = New System.Drawing.Size(77, 27)
        Me.btnConvertir.TabIndex = 37
        Me.btnConvertir.Text = "Convertir"
        '
        'UCIfDesign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCIfDesign"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtHelp As System.Windows.Forms.TextBox
    Friend WithEvents BtnSave As Zamba.AppBlock.ZButton
    Friend WithEvents lblHelp As ZLabel
    Friend WithEvents btnConvertir As Zamba.AppBlock.ZButton

End Class
