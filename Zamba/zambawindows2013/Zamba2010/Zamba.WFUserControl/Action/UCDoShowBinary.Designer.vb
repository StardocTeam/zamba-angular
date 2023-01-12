<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoShowBinary
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
        Me.BtnSave = New Zamba.AppBlock.ZButton()
        Me.lblBinary = New ZLabel()
        Me.txtBinary = New System.Windows.Forms.TextBox()
        Me.lblMime = New ZLabel()
        Me.lblOk = New ZLabel()
        Me.cmbMime = New System.Windows.Forms.ComboBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.cmbMime)
        Me.tbRule.Controls.Add(Me.lblOk)
        Me.tbRule.Controls.Add(Me.lblMime)
        Me.tbRule.Controls.Add(Me.txtBinary)
        Me.tbRule.Controls.Add(Me.lblBinary)
        Me.tbRule.Controls.Add(Me.BtnSave)
        '
        'BtnSave
        '
        Me.BtnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnSave.Location = New System.Drawing.Point(23, 110)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(70, 27)
        Me.BtnSave.TabIndex = 33
        Me.BtnSave.Text = "Guardar"
        '
        'lblBinary
        '
        Me.lblBinary.AutoSize = True
        Me.lblBinary.Location = New System.Drawing.Point(20, 21)
        Me.lblBinary.Name = "lblBinary"
        Me.lblBinary.Size = New System.Drawing.Size(194, 13)
        Me.lblBinary.TabIndex = 34
        Me.lblBinary.Text = "Variable donde se encuentra el binario:"
        '
        'txtBinary
        '
        Me.txtBinary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBinary.Location = New System.Drawing.Point(23, 38)
        Me.txtBinary.Name = "txtBinary"
        Me.txtBinary.Size = New System.Drawing.Size(569, 21)
        Me.txtBinary.TabIndex = 35
        '
        'lblMime
        '
        Me.lblMime.AutoSize = True
        Me.lblMime.Location = New System.Drawing.Point(20, 67)
        Me.lblMime.Name = "lblMime"
        Me.lblMime.Size = New System.Drawing.Size(203, 13)
        Me.lblMime.TabIndex = 36
        Me.lblMime.Text = "Modo de visualización del binario (MIME):"
        '
        'lblOk
        '
        Me.lblOk.AutoSize = True
        Me.lblOk.Location = New System.Drawing.Point(99, 124)
        Me.lblOk.Name = "lblOk"
        Me.lblOk.Size = New System.Drawing.Size(0, 13)
        Me.lblOk.TabIndex = 38
        '
        'cmbMime
        '
        Me.cmbMime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbMime.FormattingEnabled = True
        Me.cmbMime.Location = New System.Drawing.Point(23, 83)
        Me.cmbMime.Name = "cmbMime"
        Me.cmbMime.Size = New System.Drawing.Size(569, 21)
        Me.cmbMime.TabIndex = 39
        '
        'UCDoShowBinary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoShowBinary"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnSave As Zamba.AppBlock.ZButton
    Friend WithEvents lblOk As ZLabel
    Friend WithEvents lblMime As ZLabel
    Friend WithEvents txtBinary As System.Windows.Forms.TextBox
    Friend WithEvents lblBinary As ZLabel
    Friend WithEvents cmbMime As System.Windows.Forms.ComboBox

End Class
