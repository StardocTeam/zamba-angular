<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoSaveFile
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
        Me.BtnSaveFile = New Zamba.AppBlock.ZButton
        Me.txtTextToSave = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.Label6 = New ZLabel
        Me.txtFileExtension = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.Label7 = New ZLabel
        Me.txtPathVariable = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.Label4 = New ZLabel
        Me.txtFilePath = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.Label5 = New ZLabel
        Me.Label8 = New ZLabel
        Me.TxtFileName = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.TxtFileName)
        Me.tbRule.Controls.Add(Me.Label8)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.txtFilePath)
        Me.tbRule.Controls.Add(Me.Label4)
        Me.tbRule.Controls.Add(Me.txtPathVariable)
        Me.tbRule.Controls.Add(Me.Label6)
        Me.tbRule.Controls.Add(Me.txtFileExtension)
        Me.tbRule.Controls.Add(Me.txtTextToSave)
        Me.tbRule.Controls.Add(Me.Label7)
        Me.tbRule.Controls.Add(Me.BtnSaveFile)
        '
        'BtnSaveFile
        '
        Me.BtnSaveFile.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnSaveFile.Location = New System.Drawing.Point(243, 287)
        Me.BtnSaveFile.Name = "BtnSaveFile"
        Me.BtnSaveFile.Size = New System.Drawing.Size(70, 27)
        Me.BtnSaveFile.TabIndex = 33
        Me.BtnSaveFile.Text = "Guardar"
        '
        'txtTextToSave
        '
        Me.txtTextToSave.Location = New System.Drawing.Point(183, 72)
        Me.txtTextToSave.Name = "txtTextToSave"
        Me.txtTextToSave.Size = New System.Drawing.Size(280, 21)
        Me.txtTextToSave.TabIndex = 43
        Me.txtTextToSave.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(86, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Texto a Guardar:"
        '
        'txtFileExtension
        '
        Me.txtFileExtension.Location = New System.Drawing.Point(183, 163)
        Me.txtFileExtension.Name = "txtFileExtension"
        Me.txtFileExtension.Size = New System.Drawing.Size(110, 21)
        Me.txtFileExtension.TabIndex = 45
        Me.txtFileExtension.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(63, 166)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(113, 13)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "Extension del archivo:"
        '
        'txtPathVariable
        '
        Me.txtPathVariable.Location = New System.Drawing.Point(182, 242)
        Me.txtPathVariable.Name = "txtPathVariable"
        Me.txtPathVariable.Size = New System.Drawing.Size(281, 21)
        Me.txtPathVariable.TabIndex = 52
        Me.txtPathVariable.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(6, 206)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Ruta donde se guarda el archivo: "
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(182, 203)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(286, 21)
        Me.txtFilePath.TabIndex = 54
        Me.txtFilePath.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(117, 245)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "Variable:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(73, 116)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(103, 13)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "Nombre del archivo:"
        '
        'TxtFileName
        '
        Me.TxtFileName.Location = New System.Drawing.Point(183, 116)
        Me.TxtFileName.Name = "TxtFileName"
        Me.TxtFileName.Size = New System.Drawing.Size(280, 21)
        Me.TxtFileName.TabIndex = 58
        Me.TxtFileName.Text = ""
        '
        'UCDoSaveFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoSaveFile"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnSaveFile As Zamba.AppBlock.ZButton
    Friend WithEvents txtTextToSave As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents txtFileExtension As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents txtPathVariable As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtFilePath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents TxtFileName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label8 As ZLabel

End Class
