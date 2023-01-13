<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoAttachToDocument
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
        Me.cmbDocTypes = New System.Windows.Forms.ComboBox()
        Me.chkLimitKB = New System.Windows.Forms.CheckBox()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.lblLimit = New Zamba.AppBlock.ZLabel()
        Me.BtnSave = New Zamba.AppBlock.ZButton()
        Me.txtLimit = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.txtSize = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbState.Size = New System.Drawing.Size(824, 781)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbHabilitation.Size = New System.Drawing.Size(824, 781)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbConfiguration.Size = New System.Drawing.Size(824, 781)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Size = New System.Drawing.Size(824, 781)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.txtSize)
        Me.tbRule.Controls.Add(Me.Label4)
        Me.tbRule.Controls.Add(Me.txtLimit)
        Me.tbRule.Controls.Add(Me.BtnSave)
        Me.tbRule.Controls.Add(Me.lblLimit)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.chkLimitKB)
        Me.tbRule.Controls.Add(Me.cmbDocTypes)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(771, 669)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(779, 698)
        '
        'cmbDocTypes
        '
        Me.cmbDocTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDocTypes.FormattingEnabled = True
        Me.cmbDocTypes.Location = New System.Drawing.Point(103, 36)
        Me.cmbDocTypes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbDocTypes.Name = "cmbDocTypes"
        Me.cmbDocTypes.Size = New System.Drawing.Size(506, 24)
        Me.cmbDocTypes.TabIndex = 1
        '
        'chkLimitKB
        '
        Me.chkLimitKB.AutoSize = True
        Me.chkLimitKB.Location = New System.Drawing.Point(18, 90)
        Me.chkLimitKB.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkLimitKB.Name = "chkLimitKB"
        Me.chkLimitKB.Size = New System.Drawing.Size(177, 20)
        Me.chkLimitKB.TabIndex = 11
        Me.chkLimitKB.Text = "Establecer limite de KB"
        Me.chkLimitKB.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(15, 39)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 16)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Entidad:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLimit
        '
        Me.lblLimit.AutoSize = True
        Me.lblLimit.BackColor = System.Drawing.Color.Transparent
        Me.lblLimit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblLimit.Location = New System.Drawing.Point(15, 126)
        Me.lblLimit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLimit.Name = "lblLimit"
        Me.lblLimit.Size = New System.Drawing.Size(73, 16)
        Me.lblLimit.TabIndex = 13
        Me.lblLimit.Text = "Limite KB:"
        Me.lblLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(400, 207)
        Me.BtnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(96, 33)
        Me.BtnSave.TabIndex = 15
        Me.BtnSave.Text = "Guardar"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'txtLimit
        '
        Me.txtLimit.Location = New System.Drawing.Point(132, 126)
        Me.txtLimit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtLimit.MaxLength = 4000
        Me.txtLimit.Name = "txtLimit"
        Me.txtLimit.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtLimit.Size = New System.Drawing.Size(208, 27)
        Me.txtLimit.TabIndex = 16
        Me.txtLimit.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(15, 167)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 16)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Tamaño actual:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSize
        '
        Me.txtSize.Location = New System.Drawing.Point(132, 161)
        Me.txtSize.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSize.MaxLength = 4000
        Me.txtSize.Name = "txtSize"
        Me.txtSize.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtSize.Size = New System.Drawing.Size(208, 27)
        Me.txtSize.TabIndex = 18
        Me.txtSize.Text = ""
        '
        'UCDoAttachToDocument
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoAttachToDocument"
        Me.Size = New System.Drawing.Size(779, 698)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbDocTypes As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents chkLimitKB As System.Windows.Forms.CheckBox
    Friend WithEvents lblLimit As ZLabel
    Friend WithEvents BtnSave As Zamba.AppBlock.ZButton
    Friend WithEvents txtLimit As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtSize As Zamba.AppBlock.TextoInteligenteTextBox

End Class
