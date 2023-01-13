Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCDoRequestAction
    Inherits ZRuleControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Overloads Sub InitializeComponent()
        Me.btSave = New ZButton
        Me.lbRules = New ZLabel
        Me.lbUsers = New ZLabel
        Me.clsRules = New System.Windows.Forms.CheckedListBox
        Me.clsUsers = New System.Windows.Forms.CheckedListBox
        Me.lblMessage = New ZLabel
        Me.txtMessage = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.tbServerLocation = New System.Windows.Forms.TextBox
        Me.Label1 = New ZLabel
        Me.lbServerLocation = New ZLabel
        Me.chkSendTheUserAssigned = New System.Windows.Forms.CheckBox
        Me.txtSubject = New System.Windows.Forms.TextBox
        Me.lblSubject = New ZLabel
        Me.txtLinkMail = New System.Windows.Forms.TextBox
        Me.Label2 = New ZLabel
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.txtLinkMail)
        Me.tbRule.Controls.Add(Me.lblSubject)
        Me.tbRule.Controls.Add(Me.txtSubject)
        Me.tbRule.Controls.Add(Me.chkSendTheUserAssigned)
        Me.tbRule.Controls.Add(Me.lbServerLocation)
        Me.tbRule.Controls.Add(Me.tbServerLocation)
        Me.tbRule.Controls.Add(Me.txtMessage)
        Me.tbRule.Controls.Add(Me.lblMessage)
        Me.tbRule.Controls.Add(Me.clsUsers)
        Me.tbRule.Controls.Add(Me.clsRules)
        Me.tbRule.Controls.Add(Me.lbUsers)
        Me.tbRule.Controls.Add(Me.lbRules)
        Me.tbRule.Controls.Add(Me.btSave)
        Me.tbRule.Size = New System.Drawing.Size(441, 395)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(449, 421)
        '
        'btSave
        '
        Me.btSave.Location = New System.Drawing.Point(369, 338)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(57, 23)
        Me.btSave.TabIndex = 0
        Me.btSave.Text = "Guardar"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'lbRules
        '
        Me.lbRules.AutoSize = True
        Me.lbRules.BackColor = System.Drawing.Color.Transparent
        Me.lbRules.Location = New System.Drawing.Point(11, 14)
        Me.lbRules.Name = "lbRules"
        Me.lbRules.Size = New System.Drawing.Size(88, 13)
        Me.lbRules.TabIndex = 3
        Me.lbRules.Text = "Listado de reglas"
        '
        'lbUsers
        '
        Me.lbUsers.AutoSize = True
        Me.lbUsers.BackColor = System.Drawing.Color.Transparent
        Me.lbUsers.Location = New System.Drawing.Point(199, 14)
        Me.lbUsers.Name = "lbUsers"
        Me.lbUsers.Size = New System.Drawing.Size(99, 13)
        Me.lbUsers.TabIndex = 4
        Me.lbUsers.Text = "Listado de usuarios"
        '
        'clsRules
        '
        Me.clsRules.FormattingEnabled = True
        Me.clsRules.Location = New System.Drawing.Point(14, 30)
        Me.clsRules.Name = "clsRules"
        Me.clsRules.Size = New System.Drawing.Size(177, 132)
        Me.clsRules.TabIndex = 5
        '
        'clsUsers
        '
        Me.clsUsers.FormattingEnabled = True
        Me.clsUsers.Location = New System.Drawing.Point(202, 30)
        Me.clsUsers.Name = "clsUsers"
        Me.clsUsers.Size = New System.Drawing.Size(177, 132)
        Me.clsUsers.TabIndex = 6
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.lblMessage.Location = New System.Drawing.Point(7, 224)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(89, 13)
        Me.lblMessage.TabIndex = 8
        Me.lblMessage.Text = "Mensaje a enviar"
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(10, 245)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(353, 89)
        Me.txtMessage.TabIndex = 33
        Me.txtMessage.Text = ""
        '
        'tbServerLocation
        '
        Me.tbServerLocation.Location = New System.Drawing.Point(85, 170)
        Me.tbServerLocation.Name = "tbServerLocation"
        Me.tbServerLocation.Size = New System.Drawing.Size(294, 21)
        Me.tbServerLocation.TabIndex = 34
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 260)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "lbServerLocation"
        '
        'lbServerLocation
        '
        Me.lbServerLocation.AutoSize = True
        Me.lbServerLocation.Location = New System.Drawing.Point(11, 173)
        Me.lbServerLocation.Name = "lbServerLocation"
        Me.lbServerLocation.Size = New System.Drawing.Size(65, 13)
        Me.lbServerLocation.TabIndex = 35
        Me.lbServerLocation.Text = "URL Server:"
        '
        'chkSendTheUserAssigned
        '
        Me.chkSendTheUserAssigned.AutoSize = True
        Me.chkSendTheUserAssigned.Location = New System.Drawing.Point(10, 368)
        Me.chkSendTheUserAssigned.Name = "chkSendTheUserAssigned"
        Me.chkSendTheUserAssigned.Size = New System.Drawing.Size(151, 17)
        Me.chkSendTheUserAssigned.TabIndex = 36
        Me.chkSendTheUserAssigned.Text = "Enviar al usuario asignado"
        Me.chkSendTheUserAssigned.UseVisualStyleBackColor = True
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(85, 197)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(278, 21)
        Me.txtSubject.TabIndex = 37
        '
        'lblSubject
        '
        Me.lblSubject.AutoSize = True
        Me.lblSubject.Location = New System.Drawing.Point(11, 200)
        Me.lblSubject.Name = "lblSubject"
        Me.lblSubject.Size = New System.Drawing.Size(41, 13)
        Me.lblSubject.TabIndex = 38
        Me.lblSubject.Text = "Asunto"
        '
        'txtLinkMail
        '
        Me.txtLinkMail.Location = New System.Drawing.Point(80, 340)
        Me.txtLinkMail.Name = "txtLinkMail"
        Me.txtLinkMail.Size = New System.Drawing.Size(283, 21)
        Me.txtLinkMail.TabIndex = 39
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 347)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Link del Mail"
        '
        'UCDoRequestAction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoRequestAction"
        Me.Size = New System.Drawing.Size(449, 421)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btSave As ZButton
    Friend WithEvents lbRules As ZLabel
    Friend WithEvents lbUsers As ZLabel
    Friend WithEvents clsRules As System.Windows.Forms.CheckedListBox
    Friend WithEvents clsUsers As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblMessage As ZLabel
    Friend WithEvents txtMessage As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents tbServerLocation As System.Windows.Forms.TextBox
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents lbServerLocation As ZLabel
    Friend WithEvents chkSendTheUserAssigned As System.Windows.Forms.CheckBox
    Friend WithEvents lblSubject As ZLabel
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtLinkMail As System.Windows.Forms.TextBox

End Class

