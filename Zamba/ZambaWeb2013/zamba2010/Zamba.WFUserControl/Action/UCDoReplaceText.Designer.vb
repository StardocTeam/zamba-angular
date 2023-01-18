<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoReplaceText
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
        Me.txtText = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblReadFile = New ZLabel()
        Me.btnFile = New Zamba.AppBlock.ZButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkFile = New System.Windows.Forms.RadioButton()
        Me.chkVar = New System.Windows.Forms.RadioButton()
        Me.lstReplaceFields = New System.Windows.Forms.ListBox()
        Me.txtTextReplace = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtReaplceTo = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label5 = New ZLabel()
        Me.Label6 = New ZLabel()
        Me.btnAdd = New Zamba.AppBlock.ZButton()
        Me.btnRemove = New Zamba.AppBlock.ZButton()
        Me.Label7 = New ZLabel()
        Me.txtSaveOnVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnSave = New Zamba.AppBlock.ZButton()
        Me.lblTextToReplace = New ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.btnSave)
        Me.tbRule.Controls.Add(Me.txtSaveOnVar)
        Me.tbRule.Controls.Add(Me.Label7)
        Me.tbRule.Controls.Add(Me.btnRemove)
        Me.tbRule.Controls.Add(Me.btnAdd)
        Me.tbRule.Controls.Add(Me.Label6)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.txtReaplceTo)
        Me.tbRule.Controls.Add(Me.txtTextReplace)
        Me.tbRule.Controls.Add(Me.lstReplaceFields)
        Me.tbRule.Controls.Add(Me.GroupBox1)
        Me.tbRule.Controls.Add(Me.btnFile)
        Me.tbRule.Controls.Add(Me.lblReadFile)
        Me.tbRule.Controls.Add(Me.txtText)
        Me.tbRule.Controls.Add(Me.lblTextToReplace)
        Me.tbRule.Size = New System.Drawing.Size(596, 482)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(604, 508)
        '
        'txtText
        '
        Me.txtText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtText.Location = New System.Drawing.Point(116, 77)
        Me.txtText.MaxLength = 4000
        Me.txtText.Name = "txtText"
        Me.txtText.Size = New System.Drawing.Size(380, 21)
        Me.txtText.TabIndex = 26
        Me.txtText.Text = ""
        '
        'lblReadFile
        '
        Me.lblReadFile.AutoSize = True
        Me.lblReadFile.BackColor = System.Drawing.Color.Transparent
        Me.lblReadFile.Location = New System.Drawing.Point(6, 85)
        Me.lblReadFile.Name = "lblReadFile"
        Me.lblReadFile.Size = New System.Drawing.Size(102, 13)
        Me.lblReadFile.TabIndex = 27
        Me.lblReadFile.Text = "Leer desde archivo:"
        '
        'btnFile
        '
        Me.btnFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFile.Location = New System.Drawing.Point(502, 75)
        Me.btnFile.Name = "btnFile"
        Me.btnFile.Size = New System.Drawing.Size(76, 23)
        Me.btnFile.TabIndex = 36
        Me.btnFile.Text = "Examinar"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.chkFile)
        Me.GroupBox1.Controls.Add(Me.chkVar)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(138, 58)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Obtener texto desde:"
        '
        'chkFile
        '
        Me.chkFile.AutoSize = True
        Me.chkFile.Location = New System.Drawing.Point(71, 24)
        Me.chkFile.Name = "chkFile"
        Me.chkFile.Size = New System.Drawing.Size(61, 17)
        Me.chkFile.TabIndex = 38
        Me.chkFile.TabStop = True
        Me.chkFile.Text = "Archivo"
        Me.chkFile.UseVisualStyleBackColor = True
        '
        'chkVar
        '
        Me.chkVar.AutoSize = True
        Me.chkVar.Location = New System.Drawing.Point(6, 24)
        Me.chkVar.Name = "chkVar"
        Me.chkVar.Size = New System.Drawing.Size(63, 17)
        Me.chkVar.TabIndex = 0
        Me.chkVar.TabStop = True
        Me.chkVar.Text = "Variable"
        Me.chkVar.UseVisualStyleBackColor = True
        '
        'lstReplaceFields
        '
        Me.lstReplaceFields.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstReplaceFields.FormattingEnabled = True
        Me.lstReplaceFields.HorizontalScrollbar = True
        Me.lstReplaceFields.Location = New System.Drawing.Point(15, 243)
        Me.lstReplaceFields.Name = "lstReplaceFields"
        Me.lstReplaceFields.Size = New System.Drawing.Size(481, 121)
        Me.lstReplaceFields.TabIndex = 38
        '
        'txtTextReplace
        '
        Me.txtTextReplace.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTextReplace.Location = New System.Drawing.Point(104, 111)
        Me.txtTextReplace.MaxLength = 4000
        Me.txtTextReplace.Name = "txtTextReplace"
        Me.txtTextReplace.Size = New System.Drawing.Size(392, 60)
        Me.txtTextReplace.TabIndex = 39
        Me.txtTextReplace.Text = ""
        '
        'txtReaplceTo
        '
        Me.txtReaplceTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtReaplceTo.Location = New System.Drawing.Point(104, 177)
        Me.txtReaplceTo.MaxLength = 4000
        Me.txtReaplceTo.Name = "txtReaplceTo"
        Me.txtReaplceTo.Size = New System.Drawing.Size(392, 60)
        Me.txtReaplceTo.TabIndex = 40
        Me.txtReaplceTo.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(6, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Reemplazar texto:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(6, 177)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Reemplazar por:"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(502, 158)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(76, 23)
        Me.btnAdd.TabIndex = 43
        Me.btnAdd.Text = "Agregar"
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemove.Location = New System.Drawing.Point(502, 243)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(76, 23)
        Me.btnRemove.TabIndex = 44
        Me.btnRemove.Text = "Eliminar"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(12, 378)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 13)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Guardar en variable:"
        '
        'txtSaveOnVar
        '
        Me.txtSaveOnVar.Location = New System.Drawing.Point(121, 375)
        Me.txtSaveOnVar.MaxLength = 4000
        Me.txtSaveOnVar.Name = "txtSaveOnVar"
        Me.txtSaveOnVar.Size = New System.Drawing.Size(375, 21)
        Me.txtSaveOnVar.TabIndex = 46
        Me.txtSaveOnVar.Text = ""
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(194, 410)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 47
        Me.btnSave.Text = "Guardar"
        '
        'lblTextToReplace
        '
        Me.lblTextToReplace.AutoSize = True
        Me.lblTextToReplace.BackColor = System.Drawing.Color.Transparent
        Me.lblTextToReplace.Location = New System.Drawing.Point(4, 85)
        Me.lblTextToReplace.Name = "lblTextToReplace"
        Me.lblTextToReplace.Size = New System.Drawing.Size(104, 13)
        Me.lblTextToReplace.TabIndex = 0
        Me.lblTextToReplace.Text = "Texto a reemplazar:"
        '
        'UCDoReplaceText
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoReplaceText"
        Me.Size = New System.Drawing.Size(604, 508)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblReadFile As ZLabel
    Friend WithEvents txtText As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnFile As Zamba.AppBlock.ZButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkFile As System.Windows.Forms.RadioButton
    Friend WithEvents chkVar As System.Windows.Forms.RadioButton
    Friend WithEvents lstReplaceFields As System.Windows.Forms.ListBox
    Friend WithEvents txtReaplceTo As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtTextReplace As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents btnRemove As Zamba.AppBlock.ZButton
    Friend WithEvents btnAdd As Zamba.AppBlock.ZButton
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents txtSaveOnVar As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnSave As Zamba.AppBlock.ZButton
    Friend WithEvents lblTextToReplace As ZLabel

End Class
