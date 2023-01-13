<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoReadInboxMail
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
        Me.lblChooseTextBox = New ZLabel()
        Me.Btn_Save = New ZButton()
        Me.lblDocID = New ZLabel()
        Me.txtPop3Port = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblTaskID = New ZLabel()
        Me.txtPop3Server = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.chkEnableSSL = New System.Windows.Forms.CheckBox()
        Me.Label3 = New ZLabel()
        Me.Label5 = New ZLabel()
        Me.Label6 = New ZLabel()
        Me.txtZvarName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label7 = New ZLabel()
        Me.txtEndDate = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label8 = New ZLabel()
        Me.txtStartDate = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label9 = New ZLabel()
        Me.txtPathToExport = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtPop3User = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtPop3Password = New System.Windows.Forms.TextBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.txtPop3Password)
        Me.tbRule.Controls.Add(Me.txtPop3User)
        Me.tbRule.Controls.Add(Me.Label8)
        Me.tbRule.Controls.Add(Me.txtStartDate)
        Me.tbRule.Controls.Add(Me.Label9)
        Me.tbRule.Controls.Add(Me.txtPathToExport)
        Me.tbRule.Controls.Add(Me.Label6)
        Me.tbRule.Controls.Add(Me.txtZvarName)
        Me.tbRule.Controls.Add(Me.Label7)
        Me.tbRule.Controls.Add(Me.txtEndDate)
        Me.tbRule.Controls.Add(Me.Label3)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.lblChooseTextBox)
        Me.tbRule.Controls.Add(Me.Btn_Save)
        Me.tbRule.Controls.Add(Me.lblDocID)
        Me.tbRule.Controls.Add(Me.txtPop3Port)
        Me.tbRule.Controls.Add(Me.lblTaskID)
        Me.tbRule.Controls.Add(Me.txtPop3Server)
        Me.tbRule.Controls.Add(Me.chkEnableSSL)
        '
        'lblChooseTextBox
        '
        Me.lblChooseTextBox.AutoSize = True
        Me.lblChooseTextBox.Location = New System.Drawing.Point(6, 3)
        Me.lblChooseTextBox.Name = "lblChooseTextBox"
        Me.lblChooseTextBox.Size = New System.Drawing.Size(137, 13)
        Me.lblChooseTextBox.TabIndex = 16
        Me.lblChooseTextBox.Text = "Complete todos los campos"
        '
        'Btn_Save
        '
        Me.Btn_Save.Location = New System.Drawing.Point(79, 272)
        Me.Btn_Save.Name = "Btn_Save"
        Me.Btn_Save.Size = New System.Drawing.Size(97, 30)
        Me.Btn_Save.TabIndex = 15
        Me.Btn_Save.Text = "Guardar"
        Me.Btn_Save.UseVisualStyleBackColor = True
        '
        'lblDocID
        '
        Me.lblDocID.AutoSize = True
        Me.lblDocID.Location = New System.Drawing.Point(6, 63)
        Me.lblDocID.Name = "lblDocID"
        Me.lblDocID.Size = New System.Drawing.Size(70, 13)
        Me.lblDocID.TabIndex = 14
        Me.lblDocID.Text = "Puerto Pop3:"
        '
        'txtPop3Port
        '
        Me.txtPop3Port.Location = New System.Drawing.Point(179, 60)
        Me.txtPop3Port.Name = "txtPop3Port"
        Me.txtPop3Port.Size = New System.Drawing.Size(157, 21)
        Me.txtPop3Port.TabIndex = 13
        Me.txtPop3Port.Text = ""
        '
        'lblTaskID
        '
        Me.lblTaskID.AutoSize = True
        Me.lblTaskID.Location = New System.Drawing.Point(6, 36)
        Me.lblTaskID.Name = "lblTaskID"
        Me.lblTaskID.Size = New System.Drawing.Size(78, 13)
        Me.lblTaskID.TabIndex = 12
        Me.lblTaskID.Text = "Servidor Pop3:"
        '
        'txtPop3Server
        '
        Me.txtPop3Server.Location = New System.Drawing.Point(179, 33)
        Me.txtPop3Server.Name = "txtPop3Server"
        Me.txtPop3Server.Size = New System.Drawing.Size(157, 21)
        Me.txtPop3Server.TabIndex = 11
        Me.txtPop3Server.Text = ""
        '
        'chkEnableSSL
        '
        Me.chkEnableSSL.AutoSize = True
        Me.chkEnableSSL.Location = New System.Drawing.Point(179, 141)
        Me.chkEnableSSL.Name = "chkEnableSSL"
        Me.chkEnableSSL.Size = New System.Drawing.Size(85, 17)
        Me.chkEnableSSL.TabIndex = 10
        Me.chkEnableSSL.Text = "Habilitar SSL"
        Me.chkEnableSSL.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 248)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Variable a guardar datos de mails:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 167)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Ruta a exportar:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 221)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Fecha hasta:"
        '
        'txtZvarName
        '
        Me.txtZvarName.Location = New System.Drawing.Point(179, 245)
        Me.txtZvarName.Name = "txtZvarName"
        Me.txtZvarName.Size = New System.Drawing.Size(157, 21)
        Me.txtZvarName.TabIndex = 23
        Me.txtZvarName.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 117)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 13)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Password Pop3:"
        '
        'txtEndDate
        '
        Me.txtEndDate.Location = New System.Drawing.Point(179, 218)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.Size = New System.Drawing.Size(157, 21)
        Me.txtEndDate.TabIndex = 21
        Me.txtEndDate.Text = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 194)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 13)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Fecha desde:"
        '
        'txtStartDate
        '
        Me.txtStartDate.Location = New System.Drawing.Point(179, 191)
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.Size = New System.Drawing.Size(157, 21)
        Me.txtStartDate.TabIndex = 27
        Me.txtStartDate.Text = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 90)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 13)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Usuario Pop3:"
        '
        'txtPathToExport
        '
        Me.txtPathToExport.Location = New System.Drawing.Point(179, 164)
        Me.txtPathToExport.Name = "txtPathToExport"
        Me.txtPathToExport.Size = New System.Drawing.Size(157, 21)
        Me.txtPathToExport.TabIndex = 25
        Me.txtPathToExport.Text = ""
        '
        'txtPop3User
        '
        Me.txtPop3User.Location = New System.Drawing.Point(179, 87)
        Me.txtPop3User.Name = "txtPop3User"
        Me.txtPop3User.Size = New System.Drawing.Size(157, 21)
        Me.txtPop3User.TabIndex = 29
        Me.txtPop3User.Text = ""
        '
        'txtPop3Password
        '
        Me.txtPop3Password.Location = New System.Drawing.Point(179, 114)
        Me.txtPop3Password.Name = "txtPop3Password"
        Me.txtPop3Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPop3Password.Size = New System.Drawing.Size(157, 21)
        Me.txtPop3Password.TabIndex = 33
        '
        'UCDoReadInboxMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoReadInboxMail"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblChooseTextBox As ZLabel
    Friend WithEvents Btn_Save As ZButton
    Friend WithEvents lblDocID As ZLabel
    Friend WithEvents txtPop3Port As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblTaskID As ZLabel
    Friend WithEvents txtPop3Server As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents chkEnableSSL As System.Windows.Forms.CheckBox
    Friend WithEvents txtPop3User As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label8 As ZLabel
    Friend WithEvents txtStartDate As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label9 As ZLabel
    Friend WithEvents txtPathToExport As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents txtZvarName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents txtEndDate As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtPop3Password As System.Windows.Forms.TextBox

End Class
