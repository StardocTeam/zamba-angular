<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoInsertTextInWord
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
        Me.lblTextToReplace = New ZLabel()
        Me.txtText = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblReadFile = New ZLabel()
        Me.btnFile = New Zamba.AppBlock.ZButton()
        Me.txtVariable = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label5 = New ZLabel()
        Me.Label7 = New ZLabel()
        Me.txtSaveOnVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnSave = New Zamba.AppBlock.ZButton()
        Me.txtSection = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label3 = New ZLabel()
        Me.chkSaveOriginal = New System.Windows.Forms.CheckBox()
        Me.Label6 = New ZLabel()
        Me.grpFontConfig = New System.Windows.Forms.GroupBox()
        Me.lblBackColorSelected = New ZLabel()
        Me.lblBackColor = New ZLabel()
        Me.btnBackColor = New Zamba.AppBlock.ZButton()
        Me.lblStyleSelected = New ZLabel()
        Me.lblStyle = New ZLabel()
        Me.lblColorSelected = New ZLabel()
        Me.lblSizeSelected = New ZLabel()
        Me.lblFontSelected = New ZLabel()
        Me.btnFontColor = New Zamba.AppBlock.ZButton()
        Me.lblColor = New ZLabel()
        Me.btnFont = New Zamba.AppBlock.ZButton()
        Me.lblSize = New ZLabel()
        Me.lblFont = New ZLabel()
        Me.chkFontConfig = New System.Windows.Forms.CheckBox()
        Me.chkTextAsTable = New System.Windows.Forms.CheckBox()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.ColorDialog2 = New System.Windows.Forms.ColorDialog()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.grpFontConfig.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.chkTextAsTable)
        Me.tbRule.Controls.Add(Me.grpFontConfig)
        Me.tbRule.Controls.Add(Me.chkFontConfig)
        Me.tbRule.Controls.Add(Me.Label6)
        Me.tbRule.Controls.Add(Me.chkSaveOriginal)
        Me.tbRule.Controls.Add(Me.txtSection)
        Me.tbRule.Controls.Add(Me.Label3)
        Me.tbRule.Controls.Add(Me.btnSave)
        Me.tbRule.Controls.Add(Me.txtSaveOnVar)
        Me.tbRule.Controls.Add(Me.Label7)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.txtVariable)
        Me.tbRule.Controls.Add(Me.btnFile)
        Me.tbRule.Controls.Add(Me.lblReadFile)
        Me.tbRule.Controls.Add(Me.txtText)
        Me.tbRule.Controls.Add(Me.lblTextToReplace)
        Me.tbRule.Size = New System.Drawing.Size(613, 547)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(621, 573)
        '
        'lblTextToReplace
        '
        Me.lblTextToReplace.AutoSize = True
        Me.lblTextToReplace.BackColor = System.Drawing.Color.Transparent
        Me.lblTextToReplace.Location = New System.Drawing.Point(6, 24)
        Me.lblTextToReplace.Name = "lblTextToReplace"
        Me.lblTextToReplace.Size = New System.Drawing.Size(104, 13)
        Me.lblTextToReplace.TabIndex = 0
        Me.lblTextToReplace.Text = "Texto a reemplazar:"
        '
        'txtText
        '
        Me.txtText.Location = New System.Drawing.Point(116, 21)
        Me.txtText.MaxLength = 4000
        Me.txtText.Name = "txtText"
        Me.txtText.Size = New System.Drawing.Size(227, 21)
        Me.txtText.TabIndex = 26
        Me.txtText.Text = ""
        '
        'lblReadFile
        '
        Me.lblReadFile.AutoSize = True
        Me.lblReadFile.BackColor = System.Drawing.Color.Transparent
        Me.lblReadFile.Location = New System.Drawing.Point(6, 24)
        Me.lblReadFile.Name = "lblReadFile"
        Me.lblReadFile.Size = New System.Drawing.Size(102, 13)
        Me.lblReadFile.TabIndex = 27
        Me.lblReadFile.Text = "Leer desde archivo:"
        '
        'btnFile
        '
        Me.btnFile.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnFile.Location = New System.Drawing.Point(349, 19)
        Me.btnFile.Name = "btnFile"
        Me.btnFile.Size = New System.Drawing.Size(25, 23)
        Me.btnFile.TabIndex = 36
        Me.btnFile.Text = "..."
        '
        'txtVariable
        '
        Me.txtVariable.Location = New System.Drawing.Point(115, 49)
        Me.txtVariable.MaxLength = 4000
        Me.txtVariable.Name = "txtVariable"
        Me.txtVariable.Size = New System.Drawing.Size(259, 21)
        Me.txtVariable.TabIndex = 39
        Me.txtVariable.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(6, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Variable de texto:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(4, 109)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 13)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Guardar en variable:"
        '
        'txtSaveOnVar
        '
        Me.txtSaveOnVar.Location = New System.Drawing.Point(115, 106)
        Me.txtSaveOnVar.MaxLength = 4000
        Me.txtSaveOnVar.Name = "txtSaveOnVar"
        Me.txtSaveOnVar.Size = New System.Drawing.Size(259, 21)
        Me.txtSaveOnVar.TabIndex = 46
        Me.txtSaveOnVar.Text = ""
        '
        'btnSave
        '
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSave.Location = New System.Drawing.Point(267, 404)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 47
        Me.btnSave.Text = "Guardar"
        '
        'txtSection
        '
        Me.txtSection.Location = New System.Drawing.Point(115, 76)
        Me.txtSection.MaxLength = 4000
        Me.txtSection.Name = "txtSection"
        Me.txtSection.Size = New System.Drawing.Size(259, 21)
        Me.txtSection.TabIndex = 49
        Me.txtSection.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(6, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Seccion:"
        '
        'chkSaveOriginal
        '
        Me.chkSaveOriginal.AutoSize = True
        Me.chkSaveOriginal.BackColor = System.Drawing.Color.Transparent
        Me.chkSaveOriginal.Location = New System.Drawing.Point(9, 133)
        Me.chkSaveOriginal.Name = "chkSaveOriginal"
        Me.chkSaveOriginal.Size = New System.Drawing.Size(125, 17)
        Me.chkSaveOriginal.TabIndex = 51
        Me.chkSaveOriginal.Text = "Guardar ruta original"
        Me.chkSaveOriginal.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(380, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(210, 13)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "(Para escribir en la ultima hoja ingresar -1)"
        '
        'grpFontConfig
        '
        Me.grpFontConfig.BackColor = System.Drawing.Color.White
        Me.grpFontConfig.Controls.Add(Me.lblBackColorSelected)
        Me.grpFontConfig.Controls.Add(Me.lblBackColor)
        Me.grpFontConfig.Controls.Add(Me.btnBackColor)
        Me.grpFontConfig.Controls.Add(Me.lblStyleSelected)
        Me.grpFontConfig.Controls.Add(Me.lblStyle)
        Me.grpFontConfig.Controls.Add(Me.lblColorSelected)
        Me.grpFontConfig.Controls.Add(Me.lblSizeSelected)
        Me.grpFontConfig.Controls.Add(Me.lblFontSelected)
        Me.grpFontConfig.Controls.Add(Me.btnFontColor)
        Me.grpFontConfig.Controls.Add(Me.lblColor)
        Me.grpFontConfig.Controls.Add(Me.btnFont)
        Me.grpFontConfig.Controls.Add(Me.lblSize)
        Me.grpFontConfig.Controls.Add(Me.lblFont)
        Me.grpFontConfig.Enabled = False
        Me.grpFontConfig.Location = New System.Drawing.Point(9, 199)
        Me.grpFontConfig.Name = "grpFontConfig"
        Me.grpFontConfig.Size = New System.Drawing.Size(578, 182)
        Me.grpFontConfig.TabIndex = 69
        Me.grpFontConfig.TabStop = False
        Me.grpFontConfig.Text = "Configuración texto"
        '
        'lblBackColorSelected
        '
        Me.lblBackColorSelected.AutoSize = True
        Me.lblBackColorSelected.Location = New System.Drawing.Point(76, 138)
        Me.lblBackColorSelected.Name = "lblBackColorSelected"
        Me.lblBackColorSelected.Size = New System.Drawing.Size(0, 13)
        Me.lblBackColorSelected.TabIndex = 80
        '
        'lblBackColor
        '
        Me.lblBackColor.AutoSize = True
        Me.lblBackColor.BackColor = System.Drawing.Color.White
        Me.lblBackColor.Location = New System.Drawing.Point(7, 138)
        Me.lblBackColor.Name = "lblBackColor"
        Me.lblBackColor.Size = New System.Drawing.Size(69, 13)
        Me.lblBackColor.TabIndex = 79
        Me.lblBackColor.Text = "Color Fondo:"
        '
        'btnBackColor
        '
        Me.btnBackColor.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnBackColor.Location = New System.Drawing.Point(415, 138)
        Me.btnBackColor.Name = "btnBackColor"
        Me.btnBackColor.Size = New System.Drawing.Size(142, 23)
        Me.btnBackColor.TabIndex = 78
        Me.btnBackColor.Text = "Configurar fondo"
        '
        'lblStyleSelected
        '
        Me.lblStyleSelected.AutoSize = True
        Me.lblStyleSelected.Location = New System.Drawing.Point(82, 78)
        Me.lblStyleSelected.Name = "lblStyleSelected"
        Me.lblStyleSelected.Size = New System.Drawing.Size(0, 13)
        Me.lblStyleSelected.TabIndex = 77
        '
        'lblStyle
        '
        Me.lblStyle.AutoSize = True
        Me.lblStyle.Location = New System.Drawing.Point(38, 78)
        Me.lblStyle.Name = "lblStyle"
        Me.lblStyle.Size = New System.Drawing.Size(39, 13)
        Me.lblStyle.TabIndex = 75
        Me.lblStyle.Text = "Estilo :"
        '
        'lblColorSelected
        '
        Me.lblColorSelected.AutoSize = True
        Me.lblColorSelected.Location = New System.Drawing.Point(82, 119)
        Me.lblColorSelected.Name = "lblColorSelected"
        Me.lblColorSelected.Size = New System.Drawing.Size(0, 13)
        Me.lblColorSelected.TabIndex = 74
        '
        'lblSizeSelected
        '
        Me.lblSizeSelected.AutoSize = True
        Me.lblSizeSelected.Location = New System.Drawing.Point(82, 55)
        Me.lblSizeSelected.Name = "lblSizeSelected"
        Me.lblSizeSelected.Size = New System.Drawing.Size(0, 13)
        Me.lblSizeSelected.TabIndex = 73
        '
        'lblFontSelected
        '
        Me.lblFontSelected.AutoSize = True
        Me.lblFontSelected.Location = New System.Drawing.Point(82, 33)
        Me.lblFontSelected.Name = "lblFontSelected"
        Me.lblFontSelected.Size = New System.Drawing.Size(0, 13)
        Me.lblFontSelected.TabIndex = 72
        '
        'btnFontColor
        '
        Me.btnFontColor.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnFontColor.Location = New System.Drawing.Point(415, 109)
        Me.btnFontColor.Name = "btnFontColor"
        Me.btnFontColor.Size = New System.Drawing.Size(142, 23)
        Me.btnFontColor.TabIndex = 71
        Me.btnFontColor.Text = "Configurar color"
        '
        'lblColor
        '
        Me.lblColor.AutoSize = True
        Me.lblColor.BackColor = System.Drawing.Color.White
        Me.lblColor.Location = New System.Drawing.Point(37, 119)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(39, 13)
        Me.lblColor.TabIndex = 70
        Me.lblColor.Text = "Color :"
        '
        'btnFont
        '
        Me.btnFont.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnFont.Location = New System.Drawing.Point(415, 33)
        Me.btnFont.Name = "btnFont"
        Me.btnFont.Size = New System.Drawing.Size(142, 23)
        Me.btnFont.TabIndex = 69
        Me.btnFont.Text = "Configurar fuente"
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Location = New System.Drawing.Point(25, 55)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(52, 13)
        Me.lblSize.TabIndex = 68
        Me.lblSize.Text = "Tamaño :"
        '
        'lblFont
        '
        Me.lblFont.AutoSize = True
        Me.lblFont.Location = New System.Drawing.Point(29, 33)
        Me.lblFont.Name = "lblFont"
        Me.lblFont.Size = New System.Drawing.Size(48, 13)
        Me.lblFont.TabIndex = 67
        Me.lblFont.Text = "Fuente :"
        '
        'chkFontConfig
        '
        Me.chkFontConfig.AutoSize = True
        Me.chkFontConfig.BackColor = System.Drawing.Color.White
        Me.chkFontConfig.Location = New System.Drawing.Point(9, 176)
        Me.chkFontConfig.Name = "chkFontConfig"
        Me.chkFontConfig.Size = New System.Drawing.Size(249, 17)
        Me.chkFontConfig.TabIndex = 68
        Me.chkFontConfig.Text = "Configurar fuente y color del texto (Solo Web)"
        Me.chkFontConfig.UseVisualStyleBackColor = False
        '
        'chkTextAsTable
        '
        Me.chkTextAsTable.AutoSize = True
        Me.chkTextAsTable.BackColor = System.Drawing.Color.White
        Me.chkTextAsTable.Location = New System.Drawing.Point(9, 153)
        Me.chkTextAsTable.Name = "chkTextAsTable"
        Me.chkTextAsTable.Size = New System.Drawing.Size(205, 17)
        Me.chkTextAsTable.TabIndex = 70
        Me.chkTextAsTable.Text = "Insertar texto como tabla (Solo Web)"
        Me.chkTextAsTable.UseVisualStyleBackColor = False
        '
        'FontDialog1
        '
        Me.FontDialog1.ShowColor = True
        Me.FontDialog1.ShowEffects = False
        '
        'UCDoInsertTextInWord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoInsertTextInWord"
        Me.Size = New System.Drawing.Size(621, 573)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.grpFontConfig.ResumeLayout(False)
        Me.grpFontConfig.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTextToReplace As ZLabel
    Friend WithEvents lblReadFile As ZLabel
    Friend WithEvents txtText As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnFile As Zamba.AppBlock.ZButton
    Friend WithEvents txtVariable As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents txtSaveOnVar As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnSave As Zamba.AppBlock.ZButton
    Friend WithEvents txtSection As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents chkSaveOriginal As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents chkTextAsTable As System.Windows.Forms.CheckBox
    Friend WithEvents grpFontConfig As System.Windows.Forms.GroupBox
    Friend WithEvents lblBackColorSelected As ZLabel
    Friend WithEvents lblBackColor As ZLabel
    Friend WithEvents btnBackColor As Zamba.AppBlock.ZButton
    Friend WithEvents lblStyleSelected As ZLabel
    Friend WithEvents lblStyle As ZLabel
    Friend WithEvents lblColorSelected As ZLabel
    Friend WithEvents lblSizeSelected As ZLabel
    Friend WithEvents lblFontSelected As ZLabel
    Friend WithEvents btnFontColor As Zamba.AppBlock.ZButton
    Friend WithEvents lblColor As ZLabel
    Friend WithEvents btnFont As Zamba.AppBlock.ZButton
    Friend WithEvents lblSize As ZLabel
    Friend WithEvents lblFont As ZLabel
    Friend WithEvents chkFontConfig As System.Windows.Forms.CheckBox
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents ColorDialog2 As System.Windows.Forms.ColorDialog

End Class
