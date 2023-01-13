<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoCompleteTableInWord
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
        Me.btnFile = New Zamba.AppBlock.ZButton()
        Me.lblReadFile = New ZLabel()
        Me.txtFile = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblTextToReplace = New ZLabel()
        Me.Label6 = New ZLabel()
        Me.Label5 = New ZLabel()
        Me.txtTableNumber = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtNumber = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label3 = New ZLabel()
        Me.txtVarName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.chkWithHeader = New System.Windows.Forms.CheckBox()
        Me.btnZambaSave = New Zamba.AppBlock.ZButton()
        Me.Label7 = New ZLabel()
        Me.txtDataTable = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.chkInTable = New System.Windows.Forms.CheckBox()
        Me.Label8 = New ZLabel()
        Me.txtRowNumber = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.chkFontConfig = New System.Windows.Forms.CheckBox()
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
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.ColorDialog2 = New System.Windows.Forms.ColorDialog()
        Me.Label9 = New ZLabel()
        Me.chkSaveOriginal = New System.Windows.Forms.CheckBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.grpFontConfig.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.chkSaveOriginal)
        Me.tbRule.Controls.Add(Me.Label9)
        Me.tbRule.Controls.Add(Me.grpFontConfig)
        Me.tbRule.Controls.Add(Me.chkFontConfig)
        Me.tbRule.Controls.Add(Me.Label8)
        Me.tbRule.Controls.Add(Me.txtRowNumber)
        Me.tbRule.Controls.Add(Me.chkInTable)
        Me.tbRule.Controls.Add(Me.Label7)
        Me.tbRule.Controls.Add(Me.txtDataTable)
        Me.tbRule.Controls.Add(Me.btnZambaSave)
        Me.tbRule.Controls.Add(Me.chkWithHeader)
        Me.tbRule.Controls.Add(Me.Label3)
        Me.tbRule.Controls.Add(Me.txtVarName)
        Me.tbRule.Controls.Add(Me.Label6)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.txtTableNumber)
        Me.tbRule.Controls.Add(Me.txtNumber)
        Me.tbRule.Controls.Add(Me.btnFile)
        Me.tbRule.Controls.Add(Me.lblReadFile)
        Me.tbRule.Controls.Add(Me.txtFile)
        Me.tbRule.Controls.Add(Me.lblTextToReplace)
        '
        'btnFile
        '
        Me.btnFile.Location = New System.Drawing.Point(391, 22)
        Me.btnFile.Name = "btnFile"
        Me.btnFile.Size = New System.Drawing.Size(76, 23)
        Me.btnFile.TabIndex = 40
        Me.btnFile.Text = "Examinar"
        '
        'lblReadFile
        '
        Me.lblReadFile.AutoSize = True
        Me.lblReadFile.BackColor = System.Drawing.Color.Transparent
        Me.lblReadFile.Location = New System.Drawing.Point(15, 26)
        Me.lblReadFile.Name = "lblReadFile"
        Me.lblReadFile.Size = New System.Drawing.Size(102, 13)
        Me.lblReadFile.TabIndex = 39
        Me.lblReadFile.Text = "Leer desde archivo:"
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(125, 26)
        Me.txtFile.MaxLength = 4000
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(247, 21)
        Me.txtFile.TabIndex = 38
        Me.txtFile.Text = ""
        '
        'lblTextToReplace
        '
        Me.lblTextToReplace.AutoSize = True
        Me.lblTextToReplace.BackColor = System.Drawing.Color.Transparent
        Me.lblTextToReplace.Location = New System.Drawing.Point(15, 26)
        Me.lblTextToReplace.Name = "lblTextToReplace"
        Me.lblTextToReplace.Size = New System.Drawing.Size(104, 13)
        Me.lblTextToReplace.TabIndex = 37
        Me.lblTextToReplace.Text = "Texto a reemplazar:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(15, 86)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 13)
        Me.Label6.TabIndex = 46
        Me.Label6.Text = "Numero de tabla:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(15, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Numero de hoja:"
        '
        'txtTableNumber
        '
        Me.txtTableNumber.Location = New System.Drawing.Point(125, 83)
        Me.txtTableNumber.MaxLength = 4000
        Me.txtTableNumber.Name = "txtTableNumber"
        Me.txtTableNumber.Size = New System.Drawing.Size(247, 21)
        Me.txtTableNumber.TabIndex = 44
        Me.txtTableNumber.Text = ""
        '
        'txtNumber
        '
        Me.txtNumber.Location = New System.Drawing.Point(125, 56)
        Me.txtNumber.MaxLength = 4000
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.Size = New System.Drawing.Size(247, 21)
        Me.txtNumber.TabIndex = 43
        Me.txtNumber.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(15, 226)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 13)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Guardar en variable:"
        '
        'txtVarName
        '
        Me.txtVarName.Location = New System.Drawing.Point(125, 223)
        Me.txtVarName.MaxLength = 4000
        Me.txtVarName.Name = "txtVarName"
        Me.txtVarName.Size = New System.Drawing.Size(247, 21)
        Me.txtVarName.TabIndex = 47
        Me.txtVarName.Text = ""
        '
        'chkWithHeader
        '
        Me.chkWithHeader.AutoSize = True
        Me.chkWithHeader.BackColor = System.Drawing.Color.Transparent
        Me.chkWithHeader.Location = New System.Drawing.Point(18, 137)
        Me.chkWithHeader.Name = "chkWithHeader"
        Me.chkWithHeader.Size = New System.Drawing.Size(206, 17)
        Me.chkWithHeader.TabIndex = 50
        Me.chkWithHeader.Text = "Tener en cuenta nombre de columnas"
        Me.chkWithHeader.UseVisualStyleBackColor = False
        '
        'btnZambaSave
        '
        Me.btnZambaSave.Location = New System.Drawing.Point(296, 493)
        Me.btnZambaSave.Name = "btnZambaSave"
        Me.btnZambaSave.Size = New System.Drawing.Size(76, 23)
        Me.btnZambaSave.TabIndex = 51
        Me.btnZambaSave.Text = "Guardar"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(15, 113)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 53
        Me.Label7.Text = "Set de datos:"
        '
        'txtDataTable
        '
        Me.txtDataTable.Location = New System.Drawing.Point(125, 110)
        Me.txtDataTable.MaxLength = 4000
        Me.txtDataTable.Name = "txtDataTable"
        Me.txtDataTable.Size = New System.Drawing.Size(247, 21)
        Me.txtDataTable.TabIndex = 52
        Me.txtDataTable.Text = ""
        '
        'chkInTable
        '
        Me.chkInTable.AutoSize = True
        Me.chkInTable.BackColor = System.Drawing.Color.Transparent
        Me.chkInTable.Location = New System.Drawing.Point(18, 160)
        Me.chkInTable.Name = "chkInTable"
        Me.chkInTable.Size = New System.Drawing.Size(188, 17)
        Me.chkInTable.TabIndex = 54
        Me.chkInTable.Text = "Se encuentra dentro de una tabla"
        Me.chkInTable.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(15, 186)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 13)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "Numero de fila:"
        '
        'txtRowNumber
        '
        Me.txtRowNumber.Location = New System.Drawing.Point(125, 183)
        Me.txtRowNumber.MaxLength = 4000
        Me.txtRowNumber.Name = "txtRowNumber"
        Me.txtRowNumber.Size = New System.Drawing.Size(247, 21)
        Me.txtRowNumber.TabIndex = 55
        Me.txtRowNumber.Text = ""
        '
        'chkFontConfig
        '
        Me.chkFontConfig.AutoSize = True
        Me.chkFontConfig.BackColor = System.Drawing.Color.White
        Me.chkFontConfig.Location = New System.Drawing.Point(18, 282)
        Me.chkFontConfig.Name = "chkFontConfig"
        Me.chkFontConfig.Size = New System.Drawing.Size(249, 17)
        Me.chkFontConfig.TabIndex = 57
        Me.chkFontConfig.Text = "Configurar fuente y color del texto (Solo Web)"
        Me.chkFontConfig.UseVisualStyleBackColor = False
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
        Me.grpFontConfig.Location = New System.Drawing.Point(18, 305)
        Me.grpFontConfig.Name = "grpFontConfig"
        Me.grpFontConfig.Size = New System.Drawing.Size(578, 182)
        Me.grpFontConfig.TabIndex = 67
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
        'FontDialog1
        '
        Me.FontDialog1.ShowColor = True
        Me.FontDialog1.ShowEffects = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(378, 59)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(210, 13)
        Me.Label9.TabIndex = 68
        Me.Label9.Text = "(Para escribir en la ultima hoja ingresar -1)"
        '
        'chkSaveOriginal
        '
        Me.chkSaveOriginal.AutoSize = True
        Me.chkSaveOriginal.BackColor = System.Drawing.Color.Transparent
        Me.chkSaveOriginal.Location = New System.Drawing.Point(18, 259)
        Me.chkSaveOriginal.Name = "chkSaveOriginal"
        Me.chkSaveOriginal.Size = New System.Drawing.Size(112, 17)
        Me.chkSaveOriginal.TabIndex = 69
        Me.chkSaveOriginal.Text = "Guardar ruta local"
        Me.chkSaveOriginal.UseVisualStyleBackColor = False
        '
        'UCDoCompleteTableInWord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoCompleteTableInWord"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.grpFontConfig.ResumeLayout(False)
        Me.grpFontConfig.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnFile As Zamba.AppBlock.ZButton
    Friend WithEvents lblReadFile As ZLabel
    Friend WithEvents txtFile As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblTextToReplace As ZLabel
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtTableNumber As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtNumber As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtVarName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents chkWithHeader As System.Windows.Forms.CheckBox
    Friend WithEvents btnZambaSave As Zamba.AppBlock.ZButton
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents txtDataTable As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label8 As ZLabel
    Friend WithEvents txtRowNumber As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents chkInTable As System.Windows.Forms.CheckBox
    Friend WithEvents grpFontConfig As System.Windows.Forms.GroupBox
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
    Friend WithEvents lblStyleSelected As ZLabel
    Friend WithEvents lblStyle As ZLabel
    Friend WithEvents lblBackColorSelected As ZLabel
    Friend WithEvents lblBackColor As ZLabel
    Friend WithEvents btnBackColor As Zamba.AppBlock.ZButton
    Friend WithEvents ColorDialog2 As System.Windows.Forms.ColorDialog
    Friend WithEvents Label9 As ZLabel
    Friend WithEvents chkSaveOriginal As System.Windows.Forms.CheckBox

End Class
