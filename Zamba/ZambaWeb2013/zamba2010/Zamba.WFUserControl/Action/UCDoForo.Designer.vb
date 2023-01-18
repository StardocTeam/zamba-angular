<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoForo
    Inherits ZRuleControl
    'Inherits System.Windows.Forms.UserControl

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
    Private Overloads Sub InitializeComponent()
        Me.txtAsunto = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtBody = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblTema = New Zamba.AppBlock.ZLabel()
        Me.lblMensaje = New Zamba.AppBlock.ZLabel()
        Me.btnAceptar = New Zamba.AppBlock.ZButton()
        Me.txtIdMensaje = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtParticipantes = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.chkautomatic = New System.Windows.Forms.CheckBox()
        Me.lblDatasetConfig = New Zamba.AppBlock.ZLabel()
        Me.lblConfiguration = New Zamba.AppBlock.ZLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnForoRules = New Zamba.AppBlock.ZButton()
        Me.BtnCleanRuleValues = New Zamba.AppBlock.ZButton()
        Me.lblBtnName = New Zamba.AppBlock.ZLabel()
        Me.CboForumRules = New System.Windows.Forms.ComboBox()
        Me.txtbtnName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblexecuteRule = New Zamba.AppBlock.ZLabel()
        Me.lblParticipants = New Zamba.AppBlock.ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.tbRule.Controls.Add(Me.lblParticipants)
        Me.tbRule.Controls.Add(Me.lblDatasetConfig)
        Me.tbRule.Controls.Add(Me.lblConfiguration)
        Me.tbRule.Controls.Add(Me.chkautomatic)
        Me.tbRule.Controls.Add(Me.GroupBox1)
        Me.tbRule.Controls.Add(Me.lblexecuteRule)
        Me.tbRule.Controls.Add(Me.txtParticipantes)
        Me.tbRule.Controls.Add(Me.lblMensaje)
        Me.tbRule.Controls.Add(Me.btnAceptar)
        Me.tbRule.Controls.Add(Me.lblTema)
        Me.tbRule.Controls.Add(Me.txtBody)
        Me.tbRule.Controls.Add(Me.txtAsunto)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(712, 750)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(720, 779)
        '
        'txtAsunto
        '
        Me.txtAsunto.Location = New System.Drawing.Point(51, 54)
        Me.txtAsunto.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtAsunto.Name = "txtAsunto"
        Me.txtAsunto.Size = New System.Drawing.Size(631, 25)
        Me.txtAsunto.TabIndex = 0
        Me.txtAsunto.Text = ""
        '
        'txtBody
        '
        Me.txtBody.Location = New System.Drawing.Point(51, 122)
        Me.txtBody.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtBody.MaxLength = 240
        Me.txtBody.Name = "txtBody"
        Me.txtBody.Size = New System.Drawing.Size(631, 227)
        Me.txtBody.TabIndex = 1
        Me.txtBody.Text = ""
        '
        'lblTema
        '
        Me.lblTema.AutoSize = True
        Me.lblTema.BackColor = System.Drawing.Color.Transparent
        Me.lblTema.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTema.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblTema.Location = New System.Drawing.Point(53, 34)
        Me.lblTema.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTema.Name = "lblTema"
        Me.lblTema.Size = New System.Drawing.Size(54, 16)
        Me.lblTema.TabIndex = 2
        Me.lblTema.Text = "Asunto"
        Me.lblTema.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMensaje
        '
        Me.lblMensaje.AutoSize = True
        Me.lblMensaje.BackColor = System.Drawing.Color.Transparent
        Me.lblMensaje.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblMensaje.Location = New System.Drawing.Point(53, 102)
        Me.lblMensaje.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(62, 16)
        Me.lblMensaje.TabIndex = 3
        Me.lblMensaje.Text = "Mensaje"
        Me.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(583, 711)
        Me.btnAceptar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(100, 28)
        Me.btnAceptar.TabIndex = 4
        Me.btnAceptar.Text = "Guardar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'txtIdMensaje
        '
        Me.txtIdMensaje.Location = New System.Drawing.Point(0, 0)
        Me.txtIdMensaje.Name = "txtIdMensaje"
        Me.txtIdMensaje.Size = New System.Drawing.Size(100, 96)
        Me.txtIdMensaje.TabIndex = 0
        Me.txtIdMensaje.Text = ""
        '
        'txtParticipantes
        '
        Me.txtParticipantes.Location = New System.Drawing.Point(51, 379)
        Me.txtParticipantes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtParticipantes.Name = "txtParticipantes"
        Me.txtParticipantes.Size = New System.Drawing.Size(631, 25)
        Me.txtParticipantes.TabIndex = 5
        Me.txtParticipantes.Text = ""
        '
        'chkautomatic
        '
        Me.chkautomatic.AutoSize = True
        Me.chkautomatic.Location = New System.Drawing.Point(51, 428)
        Me.chkautomatic.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkautomatic.Name = "chkautomatic"
        Me.chkautomatic.Size = New System.Drawing.Size(439, 20)
        Me.chkautomatic.TabIndex = 7
        Me.chkautomatic.Text = "Creacion Automatica (No requiere de intervencion del usuario)"
        Me.chkautomatic.UseVisualStyleBackColor = True
        '
        'lblDatasetConfig
        '
        Me.lblDatasetConfig.AutoSize = True
        Me.lblDatasetConfig.BackColor = System.Drawing.Color.Transparent
        Me.lblDatasetConfig.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDatasetConfig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblDatasetConfig.Location = New System.Drawing.Point(52, 544)
        Me.lblDatasetConfig.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDatasetConfig.Name = "lblDatasetConfig"
        Me.lblDatasetConfig.Size = New System.Drawing.Size(0, 16)
        Me.lblDatasetConfig.TabIndex = 71
        Me.lblDatasetConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblConfiguration
        '
        Me.lblConfiguration.AutoSize = True
        Me.lblConfiguration.BackColor = System.Drawing.Color.Transparent
        Me.lblConfiguration.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfiguration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblConfiguration.Location = New System.Drawing.Point(48, 463)
        Me.lblConfiguration.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConfiguration.Name = "lblConfiguration"
        Me.lblConfiguration.Size = New System.Drawing.Size(319, 16)
        Me.lblConfiguration.TabIndex = 70
        Me.lblConfiguration.Text = "Configuracion de ejecución de regla desde foro"
        Me.lblConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnForoRules)
        Me.GroupBox1.Controls.Add(Me.BtnCleanRuleValues)
        Me.GroupBox1.Controls.Add(Me.lblBtnName)
        Me.GroupBox1.Controls.Add(Me.CboForumRules)
        Me.GroupBox1.Controls.Add(Me.txtbtnName)
        Me.GroupBox1.Location = New System.Drawing.Point(51, 514)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(632, 190)
        Me.GroupBox1.TabIndex = 67
        Me.GroupBox1.TabStop = False
        '
        'btnForoRules
        '
        Me.btnForoRules.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnForoRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnForoRules.ForeColor = System.Drawing.Color.White
        Me.btnForoRules.Location = New System.Drawing.Point(13, 71)
        Me.btnForoRules.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnForoRules.Name = "btnForoRules"
        Me.btnForoRules.Size = New System.Drawing.Size(181, 28)
        Me.btnForoRules.TabIndex = 57
        Me.btnForoRules.Text = "Ir a la regla de destino"
        Me.btnForoRules.UseVisualStyleBackColor = True
        '
        'BtnCleanRuleValues
        '
        Me.BtnCleanRuleValues.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnCleanRuleValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCleanRuleValues.ForeColor = System.Drawing.Color.White
        Me.BtnCleanRuleValues.Location = New System.Drawing.Point(13, 154)
        Me.BtnCleanRuleValues.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnCleanRuleValues.Name = "BtnCleanRuleValues"
        Me.BtnCleanRuleValues.Size = New System.Drawing.Size(115, 26)
        Me.BtnCleanRuleValues.TabIndex = 56
        Me.BtnCleanRuleValues.Text = "Limpiar Valores"
        Me.BtnCleanRuleValues.UseVisualStyleBackColor = True
        '
        'lblBtnName
        '
        Me.lblBtnName.AutoSize = True
        Me.lblBtnName.BackColor = System.Drawing.Color.Transparent
        Me.lblBtnName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBtnName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblBtnName.Location = New System.Drawing.Point(8, 118)
        Me.lblBtnName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBtnName.Name = "lblBtnName"
        Me.lblBtnName.Size = New System.Drawing.Size(277, 16)
        Me.lblBtnName.TabIndex = 55
        Me.lblBtnName.Text = "Nombre del botón de ejecución de regla:"
        Me.lblBtnName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CboForumRules
        '
        Me.CboForumRules.FormattingEnabled = True
        Me.CboForumRules.Location = New System.Drawing.Point(13, 37)
        Me.CboForumRules.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CboForumRules.Name = "CboForumRules"
        Me.CboForumRules.Size = New System.Drawing.Size(603, 24)
        Me.CboForumRules.TabIndex = 53
        '
        'txtbtnName
        '
        Me.txtbtnName.Location = New System.Drawing.Point(284, 118)
        Me.txtbtnName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtbtnName.Name = "txtbtnName"
        Me.txtbtnName.Size = New System.Drawing.Size(337, 61)
        Me.txtbtnName.TabIndex = 54
        Me.txtbtnName.Text = ""
        '
        'lblexecuteRule
        '
        Me.lblexecuteRule.AutoSize = True
        Me.lblexecuteRule.BackColor = System.Drawing.Color.Transparent
        Me.lblexecuteRule.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblexecuteRule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblexecuteRule.Location = New System.Drawing.Point(53, 494)
        Me.lblexecuteRule.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblexecuteRule.Name = "lblexecuteRule"
        Me.lblexecuteRule.Size = New System.Drawing.Size(178, 16)
        Me.lblexecuteRule.TabIndex = 52
        Me.lblexecuteRule.Text = "Ejecutar Regla desde foro"
        Me.lblexecuteRule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblParticipants
        '
        Me.lblParticipants.AutoSize = True
        Me.lblParticipants.BackColor = System.Drawing.Color.Transparent
        Me.lblParticipants.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParticipants.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblParticipants.Location = New System.Drawing.Point(52, 359)
        Me.lblParticipants.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblParticipants.Name = "lblParticipants"
        Me.lblParticipants.Size = New System.Drawing.Size(540, 16)
        Me.lblParticipants.TabIndex = 73
        Me.lblParticipants.Text = "Asignacion de Participantes por Id de Grupo o Id de Usuario, separados por , o ;"
        Me.lblParticipants.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDoForo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoForo"
        Me.Size = New System.Drawing.Size(720, 779)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtAsunto As Zamba.AppBlock.TextoInteligenteTextBox
    Public WithEvents txtBody As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblTema As ZLabel
    Friend WithEvents lblMensaje As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents txtIdMensaje As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtParticipantes As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents chkautomatic As System.Windows.Forms.CheckBox
    Friend WithEvents lblDatasetConfig As ZLabel
    Friend WithEvents lblConfiguration As ZLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnCleanRuleValues As ZButton
    Friend WithEvents lblBtnName As ZLabel
    Friend WithEvents CboForumRules As System.Windows.Forms.ComboBox
    Friend WithEvents txtbtnName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblexecuteRule As ZLabel
    Friend WithEvents lblParticipants As ZLabel
    Friend WithEvents btnForoRules As ZButton
End Class
