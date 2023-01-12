<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoGenerateExcelFromObject
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
    Private Shadows Sub InitializeComponent()
        Me.txtExcelName = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.Label2 = New ZLabel
        Me.Label4 = New ZLabel
        Me.txtDsName = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.lstFormatTypes = New System.Windows.Forms.ListBox
        Me.gbFormat = New System.Windows.Forms.GroupBox
        Me.chkAddColNom = New System.Windows.Forms.CheckBox
        Me.BtnSave = New Zamba.AppBlock.ZButton
        Me.Label5 = New ZLabel
        Me.txtpath = New System.Windows.Forms.TextBox
        Me.ZButton = New Zamba.AppBlock.ZButton
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.gbFormat.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.ZButton)
        Me.tbRule.Controls.Add(Me.txtpath)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.BtnSave)
        Me.tbRule.Controls.Add(Me.chkAddColNom)
        Me.tbRule.Controls.Add(Me.gbFormat)
        Me.tbRule.Controls.Add(Me.txtDsName)
        Me.tbRule.Controls.Add(Me.Label4)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.txtExcelName)
        Me.tbRule.Size = New System.Drawing.Size(512, 295)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(520, 321)
        '
        'txtExcelName
        '
        Me.txtExcelName.Location = New System.Drawing.Point(111, 27)
        Me.txtExcelName.MaxLength = 4000
        Me.txtExcelName.Name = "txtExcelName"
        Me.txtExcelName.Size = New System.Drawing.Size(188, 21)
        Me.txtExcelName.TabIndex = 25
        Me.txtExcelName.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(2, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 13)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Nombre del archivo:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(2, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Variable:"
        '
        'txtDsName
        '
        Me.txtDsName.Location = New System.Drawing.Point(111, 110)
        Me.txtDsName.MaxLength = 4000
        Me.txtDsName.Name = "txtDsName"
        Me.txtDsName.Size = New System.Drawing.Size(178, 21)
        Me.txtDsName.TabIndex = 28
        Me.txtDsName.Text = ""
        '
        'lstFormatTypes
        '
        Me.lstFormatTypes.FormattingEnabled = True
        Me.lstFormatTypes.Location = New System.Drawing.Point(17, 20)
        Me.lstFormatTypes.Name = "lstFormatTypes"
        Me.lstFormatTypes.Size = New System.Drawing.Size(164, 82)
        Me.lstFormatTypes.TabIndex = 29
        '
        'gbFormat
        '
        Me.gbFormat.BackColor = System.Drawing.Color.Transparent
        Me.gbFormat.Controls.Add(Me.lstFormatTypes)
        Me.gbFormat.Location = New System.Drawing.Point(306, 13)
        Me.gbFormat.Name = "gbFormat"
        Me.gbFormat.Size = New System.Drawing.Size(196, 118)
        Me.gbFormat.TabIndex = 30
        Me.gbFormat.TabStop = False
        Me.gbFormat.Text = "Formato"
        '
        'chkAddColNom
        '
        Me.chkAddColNom.AutoSize = True
        Me.chkAddColNom.BackColor = System.Drawing.Color.Transparent
        Me.chkAddColNom.Location = New System.Drawing.Point(5, 146)
        Me.chkAddColNom.Name = "chkAddColNom"
        Me.chkAddColNom.Size = New System.Drawing.Size(226, 17)
        Me.chkAddColNom.TabIndex = 31
        Me.chkAddColNom.Text = "Incluir nombres de columnas en la plantilla"
        Me.chkAddColNom.UseVisualStyleBackColor = False
        '
        'BtnSave
        '
        Me.BtnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnSave.Location = New System.Drawing.Point(229, 171)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(70, 27)
        Me.BtnSave.TabIndex = 32
        Me.BtnSave.Text = "Guardar"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(2, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Ubicacion:"
        '
        'txtpath
        '
        Me.txtpath.Location = New System.Drawing.Point(111, 54)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtpath.Size = New System.Drawing.Size(188, 21)
        Me.txtpath.TabIndex = 34
        '
        'ZButton
        '
        Me.ZButton.DialogResult = System.Windows.Forms.DialogResult.None
        Me.ZButton.Location = New System.Drawing.Point(111, 81)
        Me.ZButton.Name = "ZButton"
        Me.ZButton.Size = New System.Drawing.Size(76, 23)
        Me.ZButton.TabIndex = 35
        Me.ZButton.Text = "Examinar"
        '
        'UCDoGenerateExcelFromObject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoGenerateExcelFromObject"
        Me.Size = New System.Drawing.Size(520, 321)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.gbFormat.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtExcelName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtDsName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents lstFormatTypes As System.Windows.Forms.ListBox
    Friend WithEvents gbFormat As System.Windows.Forms.GroupBox
    Friend WithEvents chkAddColNom As System.Windows.Forms.CheckBox
    Friend WithEvents BtnSave As Zamba.AppBlock.ZButton
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents ZButton As Zamba.AppBlock.ZButton

End Class
