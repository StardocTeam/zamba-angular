<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoReplaceTextInWord
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
        Me.chkCaseSensitive = New System.Windows.Forms.CheckBox()
        Me.chkSaveOriginal = New System.Windows.Forms.CheckBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Size = New System.Drawing.Size(469, 363)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Size = New System.Drawing.Size(469, 363)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Size = New System.Drawing.Size(469, 363)
        '
        'tbAlerts
        '
        Me.tbAlerts.Size = New System.Drawing.Size(469, 363)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.chkSaveOriginal)
        Me.tbRule.Controls.Add(Me.chkCaseSensitive)
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
        Me.tbRule.Controls.Add(Me.btnFile)
        Me.tbRule.Controls.Add(Me.lblReadFile)
        Me.tbRule.Controls.Add(Me.txtText)
        Me.tbRule.Controls.Add(Me.lblTextToReplace)
        Me.tbRule.Size = New System.Drawing.Size(469, 363)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(477, 389)
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
        Me.txtText.Size = New System.Drawing.Size(247, 21)
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
        Me.btnFile.Location = New System.Drawing.Point(382, 20)
        Me.btnFile.Name = "btnFile"
        Me.btnFile.Size = New System.Drawing.Size(76, 23)
        Me.btnFile.TabIndex = 36
        Me.btnFile.Text = "Examinar"
        '
        'lstReplaceFields
        '
        Me.lstReplaceFields.FormattingEnabled = True
        Me.lstReplaceFields.HorizontalScrollbar = True
        Me.lstReplaceFields.Location = New System.Drawing.Point(9, 154)
        Me.lstReplaceFields.Name = "lstReplaceFields"
        Me.lstReplaceFields.Size = New System.Drawing.Size(365, 95)
        Me.lstReplaceFields.TabIndex = 38
        '
        'txtTextReplace
        '
        Me.txtTextReplace.Location = New System.Drawing.Point(104, 95)
        Me.txtTextReplace.MaxLength = 4000
        Me.txtTextReplace.Name = "txtTextReplace"
        Me.txtTextReplace.Size = New System.Drawing.Size(259, 21)
        Me.txtTextReplace.TabIndex = 39
        Me.txtTextReplace.Text = ""
        '
        'txtReaplceTo
        '
        Me.txtReaplceTo.Location = New System.Drawing.Point(104, 122)
        Me.txtReaplceTo.MaxLength = 4000
        Me.txtReaplceTo.Name = "txtReaplceTo"
        Me.txtReaplceTo.Size = New System.Drawing.Size(259, 21)
        Me.txtReaplceTo.TabIndex = 40
        Me.txtReaplceTo.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(6, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Reemplazar texto:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(6, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Reemplazar por:"
        '
        'btnAdd
        '
        Me.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnAdd.Location = New System.Drawing.Point(382, 108)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(76, 23)
        Me.btnAdd.TabIndex = 43
        Me.btnAdd.Text = "Agregar"
        '
        'btnRemove
        '
        Me.btnRemove.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnRemove.Location = New System.Drawing.Point(382, 190)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(76, 23)
        Me.btnRemove.TabIndex = 44
        Me.btnRemove.Text = "Eliminar"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(6, 286)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 13)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Guardar en variable:"
        '
        'txtSaveOnVar
        '
        Me.txtSaveOnVar.Location = New System.Drawing.Point(115, 283)
        Me.txtSaveOnVar.MaxLength = 4000
        Me.txtSaveOnVar.Name = "txtSaveOnVar"
        Me.txtSaveOnVar.Size = New System.Drawing.Size(259, 21)
        Me.txtSaveOnVar.TabIndex = 46
        Me.txtSaveOnVar.Text = ""
        '
        'btnSave
        '
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSave.Location = New System.Drawing.Point(188, 329)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 47
        Me.btnSave.Text = "Guardar"
        '
        'chkCaseSensitive
        '
        Me.chkCaseSensitive.AutoSize = True
        Me.chkCaseSensitive.BackColor = System.Drawing.Color.Transparent
        Me.chkCaseSensitive.Location = New System.Drawing.Point(9, 65)
        Me.chkCaseSensitive.Name = "chkCaseSensitive"
        Me.chkCaseSensitive.Size = New System.Drawing.Size(132, 17)
        Me.chkCaseSensitive.TabIndex = 49
        Me.chkCaseSensitive.Text = "Sensible a mayusculas" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.chkCaseSensitive.UseVisualStyleBackColor = False
        '
        'chkSaveOriginal
        '
        Me.chkSaveOriginal.AutoSize = True
        Me.chkSaveOriginal.BackColor = System.Drawing.Color.Transparent
        Me.chkSaveOriginal.Location = New System.Drawing.Point(9, 257)
        Me.chkSaveOriginal.Name = "chkSaveOriginal"
        Me.chkSaveOriginal.Size = New System.Drawing.Size(125, 17)
        Me.chkSaveOriginal.TabIndex = 50
        Me.chkSaveOriginal.Text = "Guardar ruta original"
        Me.chkSaveOriginal.UseVisualStyleBackColor = False
        '
        'UCDoReplaceTextInWord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoReplaceTextInWord"
        Me.Size = New System.Drawing.Size(477, 389)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTextToReplace As ZLabel
    Friend WithEvents lblReadFile As ZLabel
    Friend WithEvents txtText As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnFile As Zamba.AppBlock.ZButton
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
    Friend WithEvents chkCaseSensitive As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaveOriginal As System.Windows.Forms.CheckBox

End Class
