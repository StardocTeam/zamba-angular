<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDOCreateOutlookCalendar
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
        Me.txtSubject = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.BtnSave = New Zamba.AppBlock.ZButton()
        Me.txtLocation = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New Zamba.AppBlock.ZLabel()
        Me.Label10 = New Zamba.AppBlock.ZLabel()
        Me.Label11 = New Zamba.AppBlock.ZLabel()
        Me.Label12 = New Zamba.AppBlock.ZLabel()
        Me.txtBody = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label13 = New Zamba.AppBlock.ZLabel()
        Me.dtStartTime = New System.Windows.Forms.DateTimePicker()
        Me.dtEndTime = New System.Windows.Forms.DateTimePicker()
        Me.optFecha_FechaFija = New System.Windows.Forms.RadioButton()
        Me.optFecha_TextoInteligente = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkAllDayTxtInteligente = New System.Windows.Forms.CheckBox()
        Me.chkAllDay = New System.Windows.Forms.CheckBox()
        Me.txtEndTime = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtEndDate = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label6 = New Zamba.AppBlock.ZLabel()
        Me.txtStartTime = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.txtStartDate = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkEnvioAutomatico = New System.Windows.Forms.CheckBox()
        Me.optForm_EnviarMail = New System.Windows.Forms.RadioButton()
        Me.optForm_Abrir = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtOrganizador = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtTo = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.tbRule.Controls.Add(Me.txtBody)
        Me.tbRule.Controls.Add(Me.Label12)
        Me.tbRule.Controls.Add(Me.GroupBox2)
        Me.tbRule.Controls.Add(Me.GroupBox1)
        Me.tbRule.Controls.Add(Me.GroupBox3)
        Me.tbRule.Controls.Add(Me.BtnSave)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(743, 770)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(751, 799)
        '
        'txtSubject
        '
        Me.txtSubject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSubject.Location = New System.Drawing.Point(109, 102)
        Me.txtSubject.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSubject.MaxLength = 4000
        Me.txtSubject.Multiline = False
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(607, 25)
        Me.txtSubject.TabIndex = 0
        Me.txtSubject.Text = ""
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(459, 697)
        Me.BtnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(125, 32)
        Me.BtnSave.TabIndex = 0
        Me.BtnSave.Text = "Guardar"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'txtLocation
        '
        Me.txtLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLocation.Location = New System.Drawing.Point(109, 135)
        Me.txtLocation.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtLocation.MaxLength = 4000
        Me.txtLocation.Multiline = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(607, 25)
        Me.txtLocation.TabIndex = 0
        Me.txtLocation.Text = ""
        '
        'dtStartDate
        '
        Me.dtStartDate.CustomFormat = "dddd dd/MM/yyyy"
        Me.dtStartDate.Enabled = False
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.Location = New System.Drawing.Point(109, 79)
        Me.dtStartDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(227, 23)
        Me.dtStartDate.TabIndex = 0
        '
        'dtEndDate
        '
        Me.dtEndDate.CustomFormat = "dddd dd/MM/yyyy"
        Me.dtEndDate.Enabled = False
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtEndDate.Location = New System.Drawing.Point(109, 118)
        Me.dtEndDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(227, 23)
        Me.dtEndDate.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(68, 118)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 16)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Fin:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(35, 139)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 16)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "Ubicacion:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(49, 106)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 16)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "Asunto:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(4, 591)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(95, 16)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "Comentarios:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBody
        '
        Me.txtBody.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtBody.Location = New System.Drawing.Point(4, 607)
        Me.txtBody.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtBody.MaxLength = 4000
        Me.txtBody.Name = "txtBody"
        Me.txtBody.Size = New System.Drawing.Size(735, 82)
        Me.txtBody.TabIndex = 0
        Me.txtBody.Text = ""
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(53, 84)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 16)
        Me.Label13.TabIndex = 28
        Me.Label13.Text = "Inicio:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtStartTime
        '
        Me.dtStartTime.CustomFormat = "HH:mm"
        Me.dtStartTime.Enabled = False
        Me.dtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartTime.Location = New System.Drawing.Point(345, 79)
        Me.dtStartTime.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtStartTime.Name = "dtStartTime"
        Me.dtStartTime.ShowUpDown = True
        Me.dtStartTime.Size = New System.Drawing.Size(111, 23)
        Me.dtStartTime.TabIndex = 0
        '
        'dtEndTime
        '
        Me.dtEndTime.CustomFormat = "HH:mm"
        Me.dtEndTime.Enabled = False
        Me.dtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtEndTime.Location = New System.Drawing.Point(345, 118)
        Me.dtEndTime.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtEndTime.Name = "dtEndTime"
        Me.dtEndTime.ShowUpDown = True
        Me.dtEndTime.Size = New System.Drawing.Size(109, 23)
        Me.dtEndTime.TabIndex = 0
        '
        'optFecha_FechaFija
        '
        Me.optFecha_FechaFija.AutoSize = True
        Me.optFecha_FechaFija.Checked = True
        Me.optFecha_FechaFija.Location = New System.Drawing.Point(24, 39)
        Me.optFecha_FechaFija.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.optFecha_FechaFija.Name = "optFecha_FechaFija"
        Me.optFecha_FechaFija.Size = New System.Drawing.Size(492, 20)
        Me.optFecha_FechaFija.TabIndex = 0
        Me.optFecha_FechaFija.TabStop = True
        Me.optFecha_FechaFija.Text = "Seleccionar valores fijos para fecha y hora de inicio y fin de la reunion"
        Me.optFecha_FechaFija.UseVisualStyleBackColor = True
        '
        'optFecha_TextoInteligente
        '
        Me.optFecha_TextoInteligente.AutoSize = True
        Me.optFecha_TextoInteligente.Location = New System.Drawing.Point(24, 162)
        Me.optFecha_TextoInteligente.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.optFecha_TextoInteligente.Name = "optFecha_TextoInteligente"
        Me.optFecha_TextoInteligente.Size = New System.Drawing.Size(491, 20)
        Me.optFecha_TextoInteligente.TabIndex = 0
        Me.optFecha_TextoInteligente.Text = "Usar texto inteligente para la fecha y hora de inicio y fin de la reunion"
        Me.optFecha_TextoInteligente.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkAllDayTxtInteligente)
        Me.GroupBox1.Controls.Add(Me.chkAllDay)
        Me.GroupBox1.Controls.Add(Me.txtEndTime)
        Me.GroupBox1.Controls.Add(Me.txtEndDate)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtStartTime)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtStartDate)
        Me.GroupBox1.Controls.Add(Me.optFecha_TextoInteligente)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.optFecha_FechaFija)
        Me.GroupBox1.Controls.Add(Me.dtStartDate)
        Me.GroupBox1.Controls.Add(Me.dtStartTime)
        Me.GroupBox1.Controls.Add(Me.dtEndTime)
        Me.GroupBox1.Controls.Add(Me.dtEndDate)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(4, 201)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(735, 282)
        Me.GroupBox1.TabIndex = 40
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Opciones de fecha"
        '
        'chkAllDayTxtInteligente
        '
        Me.chkAllDayTxtInteligente.AutoSize = True
        Me.chkAllDayTxtInteligente.Location = New System.Drawing.Point(481, 208)
        Me.chkAllDayTxtInteligente.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkAllDayTxtInteligente.Name = "chkAllDayTxtInteligente"
        Me.chkAllDayTxtInteligente.Size = New System.Drawing.Size(99, 20)
        Me.chkAllDayTxtInteligente.TabIndex = 45
        Me.chkAllDayTxtInteligente.Text = "Todo el dia"
        Me.chkAllDayTxtInteligente.UseVisualStyleBackColor = True
        '
        'chkAllDay
        '
        Me.chkAllDay.AutoSize = True
        Me.chkAllDay.Location = New System.Drawing.Point(481, 84)
        Me.chkAllDay.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkAllDay.Name = "chkAllDay"
        Me.chkAllDay.Size = New System.Drawing.Size(99, 20)
        Me.chkAllDay.TabIndex = 44
        Me.chkAllDay.Text = "Todo el dia"
        Me.chkAllDay.UseVisualStyleBackColor = True
        '
        'txtEndTime
        '
        Me.txtEndTime.Enabled = False
        Me.txtEndTime.Location = New System.Drawing.Point(344, 235)
        Me.txtEndTime.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtEndTime.MaxLength = 4000
        Me.txtEndTime.Multiline = False
        Me.txtEndTime.Name = "txtEndTime"
        Me.txtEndTime.Size = New System.Drawing.Size(111, 26)
        Me.txtEndTime.TabIndex = 0
        Me.txtEndTime.Text = ""
        '
        'txtEndDate
        '
        Me.txtEndDate.Enabled = False
        Me.txtEndDate.Location = New System.Drawing.Point(109, 235)
        Me.txtEndDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtEndDate.MaxLength = 4000
        Me.txtEndDate.Multiline = False
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.Size = New System.Drawing.Size(227, 26)
        Me.txtEndDate.TabIndex = 0
        Me.txtEndDate.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(68, 235)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 16)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "Fin:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtStartTime
        '
        Me.txtStartTime.Enabled = False
        Me.txtStartTime.Location = New System.Drawing.Point(345, 202)
        Me.txtStartTime.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtStartTime.MaxLength = 4000
        Me.txtStartTime.Multiline = False
        Me.txtStartTime.Name = "txtStartTime"
        Me.txtStartTime.Size = New System.Drawing.Size(109, 26)
        Me.txtStartTime.TabIndex = 0
        Me.txtStartTime.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(53, 206)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 16)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Inicio:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtStartDate
        '
        Me.txtStartDate.Enabled = False
        Me.txtStartDate.Location = New System.Drawing.Point(109, 202)
        Me.txtStartDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtStartDate.MaxLength = 4000
        Me.txtStartDate.Multiline = False
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.Size = New System.Drawing.Size(227, 25)
        Me.txtStartDate.TabIndex = 0
        Me.txtStartDate.Text = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkEnvioAutomatico)
        Me.GroupBox2.Controls.Add(Me.optForm_EnviarMail)
        Me.GroupBox2.Controls.Add(Me.optForm_Abrir)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(4, 483)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(735, 108)
        Me.GroupBox2.TabIndex = 41
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Al finalizar"
        '
        'chkEnvioAutomatico
        '
        Me.chkEnvioAutomatico.AutoSize = True
        Me.chkEnvioAutomatico.Location = New System.Drawing.Point(344, 66)
        Me.chkEnvioAutomatico.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkEnvioAutomatico.Name = "chkEnvioAutomatico"
        Me.chkEnvioAutomatico.Size = New System.Drawing.Size(141, 20)
        Me.chkEnvioAutomatico.TabIndex = 46
        Me.chkEnvioAutomatico.Text = "Envio automatico"
        Me.chkEnvioAutomatico.UseVisualStyleBackColor = True
        '
        'optForm_EnviarMail
        '
        Me.optForm_EnviarMail.AutoSize = True
        Me.optForm_EnviarMail.Location = New System.Drawing.Point(23, 66)
        Me.optForm_EnviarMail.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.optForm_EnviarMail.Name = "optForm_EnviarMail"
        Me.optForm_EnviarMail.Size = New System.Drawing.Size(284, 20)
        Me.optForm_EnviarMail.TabIndex = 0
        Me.optForm_EnviarMail.Text = "Enviar por email el calendario generado"
        Me.optForm_EnviarMail.UseVisualStyleBackColor = True
        '
        'optForm_Abrir
        '
        Me.optForm_Abrir.AutoSize = True
        Me.optForm_Abrir.Checked = True
        Me.optForm_Abrir.Location = New System.Drawing.Point(23, 38)
        Me.optForm_Abrir.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.optForm_Abrir.Name = "optForm_Abrir"
        Me.optForm_Abrir.Size = New System.Drawing.Size(210, 20)
        Me.optForm_Abrir.TabIndex = 0
        Me.optForm_Abrir.TabStop = True
        Me.optForm_Abrir.Text = "Abrir el calendario generado"
        Me.optForm_Abrir.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtOrganizador)
        Me.GroupBox3.Controls.Add(Me.txtTo)
        Me.GroupBox3.Controls.Add(Me.txtLocation)
        Me.GroupBox3.Controls.Add(Me.txtSubject)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(735, 197)
        Me.GroupBox3.TabIndex = 42
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Opciones de la reunion"
        Me.GroupBox3.Controls.SetChildIndex(Me.Label10, 0)
        Me.GroupBox3.Controls.SetChildIndex(Me.Label11, 0)
        Me.GroupBox3.Controls.SetChildIndex(Me.txtSubject, 0)
        Me.GroupBox3.Controls.SetChildIndex(Me.txtLocation, 0)
        Me.GroupBox3.Controls.SetChildIndex(Me.txtTo, 0)
        Me.GroupBox3.Controls.SetChildIndex(Me.txtOrganizador, 0)
        '
        'txtOrganizador
        '
        Me.txtOrganizador.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOrganizador.Location = New System.Drawing.Point(109, 36)
        Me.txtOrganizador.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtOrganizador.MaxLength = 4000
        Me.txtOrganizador.Multiline = False
        Me.txtOrganizador.Name = "txtOrganizador"
        Me.txtOrganizador.Size = New System.Drawing.Size(607, 25)
        Me.txtOrganizador.TabIndex = 29
        Me.txtOrganizador.Text = ""
        '
        'txtTo
        '
        Me.txtTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTo.Location = New System.Drawing.Point(109, 69)
        Me.txtTo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtTo.MaxLength = 4000
        Me.txtTo.Multiline = False
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(607, 25)
        Me.txtTo.TabIndex = 29
        Me.txtTo.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(14, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Organizador:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(49, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Para:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UCDOCreateOutlookCalendar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MinimumSize = New System.Drawing.Size(716, 654)
        Me.Name = "UCDOCreateOutlookCalendar"
        Me.Size = New System.Drawing.Size(751, 799)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtSubject As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents BtnSave As Zamba.AppBlock.ZButton
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtLocation As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtBody As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As ZLabel
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents Label10 As ZLabel
    Friend WithEvents Label11 As ZLabel
    Friend WithEvents Label13 As ZLabel
    Friend WithEvents dtStartTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optFecha_TextoInteligente As System.Windows.Forms.RadioButton
    Friend WithEvents optFecha_FechaFija As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEndTime As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtEndDate As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents txtStartTime As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtStartDate As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents optForm_EnviarMail As System.Windows.Forms.RadioButton
    Friend WithEvents optForm_Abrir As System.Windows.Forms.RadioButton
    Friend WithEvents chkAllDayTxtInteligente As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllDay As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTo As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtOrganizador As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents chkEnvioAutomatico As System.Windows.Forms.CheckBox

End Class
