<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTestMailSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.txtMail = New System.Windows.Forms.TextBox()
        Me.txtUsr = New System.Windows.Forms.TextBox()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.lblMail = New System.Windows.Forms.Label()
        Me.lblUsr = New System.Windows.Forms.Label()
        Me.lblPass = New System.Windows.Forms.Label()
        Me.lblPort = New System.Windows.Forms.Label()
        Me.chkEnableSsl = New System.Windows.Forms.CheckBox()
        Me.chkUseDefaultCredentials = New System.Windows.Forms.CheckBox()
        Me.chkBlankCredentials = New System.Windows.Forms.CheckBox()
        Me.lblHost = New System.Windows.Forms.Label()
        Me.txtHost = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txtMail
        '
        Me.txtMail.Location = New System.Drawing.Point(79, 12)
        Me.txtMail.Name = "txtMail"
        Me.txtMail.Size = New System.Drawing.Size(150, 20)
        Me.txtMail.TabIndex = 0
        '
        'txtUsr
        '
        Me.txtUsr.Location = New System.Drawing.Point(79, 40)
        Me.txtUsr.Name = "txtUsr"
        Me.txtUsr.Size = New System.Drawing.Size(150, 20)
        Me.txtUsr.TabIndex = 1
        '
        'txtPass
        '
        Me.txtPass.Location = New System.Drawing.Point(79, 68)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPass.Size = New System.Drawing.Size(150, 20)
        Me.txtPass.TabIndex = 3
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(79, 94)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(150, 20)
        Me.txtPort.TabIndex = 4
        Me.txtPort.Text = "25"
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(280, 97)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(75, 23)
        Me.btnTest.TabIndex = 7
        Me.btnTest.Text = "Probar"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'lblMail
        '
        Me.lblMail.AutoSize = True
        Me.lblMail.Location = New System.Drawing.Point(13, 15)
        Me.lblMail.Name = "lblMail"
        Me.lblMail.Size = New System.Drawing.Size(29, 13)
        Me.lblMail.TabIndex = 8
        Me.lblMail.Text = "Mail:"
        '
        'lblUsr
        '
        Me.lblUsr.AutoSize = True
        Me.lblUsr.Location = New System.Drawing.Point(13, 43)
        Me.lblUsr.Name = "lblUsr"
        Me.lblUsr.Size = New System.Drawing.Size(46, 13)
        Me.lblUsr.TabIndex = 9
        Me.lblUsr.Text = "Usuario:"
        '
        'lblPass
        '
        Me.lblPass.AutoSize = True
        Me.lblPass.Location = New System.Drawing.Point(13, 71)
        Me.lblPass.Name = "lblPass"
        Me.lblPass.Size = New System.Drawing.Size(64, 13)
        Me.lblPass.TabIndex = 10
        Me.lblPass.Text = "Contraseña:"
        '
        'lblPort
        '
        Me.lblPort.AutoSize = True
        Me.lblPort.Location = New System.Drawing.Point(17, 97)
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(41, 13)
        Me.lblPort.TabIndex = 11
        Me.lblPort.Text = "Puerto:"
        '
        'chkEnableSsl
        '
        Me.chkEnableSsl.AutoSize = True
        Me.chkEnableSsl.Location = New System.Drawing.Point(280, 14)
        Me.chkEnableSsl.Name = "chkEnableSsl"
        Me.chkEnableSsl.Size = New System.Drawing.Size(81, 17)
        Me.chkEnableSsl.TabIndex = 12
        Me.chkEnableSsl.Text = "Habilitar Ssl"
        Me.chkEnableSsl.UseVisualStyleBackColor = True
        '
        'chkUseDefaultCredentials
        '
        Me.chkUseDefaultCredentials.AutoSize = True
        Me.chkUseDefaultCredentials.Location = New System.Drawing.Point(280, 43)
        Me.chkUseDefaultCredentials.Name = "chkUseDefaultCredentials"
        Me.chkUseDefaultCredentials.Size = New System.Drawing.Size(168, 17)
        Me.chkUseDefaultCredentials.TabIndex = 13
        Me.chkUseDefaultCredentials.Text = "Usar credenciales por defecto"
        Me.chkUseDefaultCredentials.UseVisualStyleBackColor = True
        '
        'chkBlankCredentials
        '
        Me.chkBlankCredentials.AutoSize = True
        Me.chkBlankCredentials.Location = New System.Drawing.Point(280, 71)
        Me.chkBlankCredentials.Name = "chkBlankCredentials"
        Me.chkBlankCredentials.Size = New System.Drawing.Size(159, 17)
        Me.chkBlankCredentials.TabIndex = 14
        Me.chkBlankCredentials.Text = "Usar credenciales anonimas"
        Me.chkBlankCredentials.UseVisualStyleBackColor = True
        '
        'lblHost
        '
        Me.lblHost.AutoSize = True
        Me.lblHost.Location = New System.Drawing.Point(18, 127)
        Me.lblHost.Name = "lblHost"
        Me.lblHost.Size = New System.Drawing.Size(52, 13)
        Me.lblHost.TabIndex = 15
        Me.lblHost.Text = "Servidor :"
        '
        'txtHost
        '
        Me.txtHost.Location = New System.Drawing.Point(79, 120)
        Me.txtHost.Name = "txtHost"
        Me.txtHost.Size = New System.Drawing.Size(150, 20)
        Me.txtHost.TabIndex = 16
        '
        'FrmTestMailSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 148)
        Me.Controls.Add(Me.txtHost)
        Me.Controls.Add(Me.lblHost)
        Me.Controls.Add(Me.chkBlankCredentials)
        Me.Controls.Add(Me.chkUseDefaultCredentials)
        Me.Controls.Add(Me.chkEnableSsl)
        Me.Controls.Add(Me.lblPort)
        Me.Controls.Add(Me.lblPass)
        Me.Controls.Add(Me.lblUsr)
        Me.Controls.Add(Me.lblMail)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.txtPass)
        Me.Controls.Add(Me.txtUsr)
        Me.Controls.Add(Me.txtMail)
        Me.Name = "FrmTestMailSettings"
        Me.Text = "Prueba de configuración de mail SMTP"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMail As System.Windows.Forms.TextBox
    Friend WithEvents txtUsr As System.Windows.Forms.TextBox
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents lblMail As System.Windows.Forms.Label
    Friend WithEvents lblUsr As System.Windows.Forms.Label
    Friend WithEvents lblPass As System.Windows.Forms.Label
    Friend WithEvents lblPort As System.Windows.Forms.Label
    Friend WithEvents chkEnableSsl As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseDefaultCredentials As System.Windows.Forms.CheckBox
    Friend WithEvents chkBlankCredentials As System.Windows.Forms.CheckBox
    Friend WithEvents lblHost As System.Windows.Forms.Label
    Friend WithEvents txtHost As System.Windows.Forms.TextBox

End Class
