Public Class UCDoMail
    Inherits ZRuleControl

#Region " Código generado por el Diseñador de Windows Forms "
    Friend WithEvents Label4 As ZLabel
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents txtPara As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtCC As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtCCO As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtAsunto As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents chkAttach As System.Windows.Forms.CheckBox
    Friend WithEvents chkAttachAssociatedDocuments As System.Windows.Forms.CheckBox
    Friend WithEvents txtSmtpPort As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblSmtpPort As ZLabel
    Friend WithEvents txtSmtpServer As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblSmtpServer As ZLabel
    Friend WithEvents txtSmtpMail As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblSmtpMail As ZLabel
    Friend WithEvents txtSmtpPass As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblSmtpPassword As ZLabel
    Friend WithEvents txtSmtpUser As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblSmtpUser As ZLabel
    Friend WithEvents lblexecuteRule As ZLabel
    Friend WithEvents CboDoMailRule As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblBtnName As ZLabel
    Friend WithEvents txtbtnName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents BtnCleanRuleValues As ZButton
    Friend WithEvents lblvarAttachs As ZLabel
    Friend WithEvents txtVarAttachs As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblColumnRoute As ZLabel
    Friend WithEvents lblColumnName As ZLabel
    Friend WithEvents txtColumnRoute As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtColumnName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblConfiguration As ZLabel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblDatasetConfig As ZLabel
    Friend WithEvents lblAditionalExec As ZLabel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btnCleanAddRule As ZButton
    Friend WithEvents CboExecAdditionalRule As ComboBox
    Friend WithEvents txtBtnExecuteAdditionalRuleName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblAditionalExecution As ZLabel
    Friend WithEvents btnExecAdditionalRule As ZButton
    Friend WithEvents btnDoMailRule As ZButton
    Friend WithEvents tabMail As System.Windows.Forms.TabPage
    Friend WithEvents tabSMTP As System.Windows.Forms.TabPage
    Friend WithEvents chkUseSMTPConf As System.Windows.Forms.CheckBox
    Friend WithEvents tabExecuteRule As System.Windows.Forms.TabPage
    Friend WithEvents TbDoMail As System.Windows.Forms.TabControl
    Friend WithEvents lblFor As ZLabel
    Friend WithEvents lblCC As ZLabel
    Friend WithEvents lblCCO As ZLabel
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents txtAdditionalRuleColumnName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtAdditionalRuleColumnRoute As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents lblImages As ZLabel
    Friend WithEvents btnOpenImage As ZButton
    Friend WithEvents lstImages As ListBox
    Friend WithEvents btnRemoveImage As ZButton
    Friend WithEvents chkViewOriginal As System.Windows.Forms.CheckBox
    Friend WithEvents chkDisableHistory As System.Windows.Forms.CheckBox
    Friend WithEvents txtMailPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents CHKSaveMailPath As System.Windows.Forms.CheckBox
    Friend WithEvents chkgroupMailTo As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As ZLabel
    Friend WithEvents chkAttachLink As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As ZLabel
    Friend WithEvents ChkAutomatic As System.Windows.Forms.CheckBox
    Friend WithEvents tabImages As System.Windows.Forms.TabPage
    Friend WithEvents tabAttachments As System.Windows.Forms.TabPage
    Friend WithEvents BtnConfDocAsoc As ZButton
    Friend WithEvents chkAtachDirectory As System.Windows.Forms.CheckBox











    Friend WithEvents chkEmbedImages As System.Windows.Forms.CheckBox
    Friend WithEvents lblSaveMessage As ZLabel
    Friend WithEvents lblAttachTableVar As ZLabel
    Friend WithEvents lblAttachTableDocTypeId As ZLabel
    Friend WithEvents lblAttachTableDocId As ZLabel
    Friend WithEvents lblAttachTableDocName As ZLabel
    Friend WithEvents txtAttachTableVar As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtAttachTableDocTypeId As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtAttachTableDocId As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtAttachTableDocName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkEnableSsl As System.Windows.Forms.CheckBox
    Friend WithEvents txtBody As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents tabOptions As System.Windows.Forms.TabPage
    Friend WithEvents chkViewAssociatedDocuments As System.Windows.Forms.CheckBox

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        btnAceptar = New ZButton()
        Label3 = New ZLabel()
        Label5 = New ZLabel()
        txtPara = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtCC = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtCCO = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtAsunto = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtBody = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblSmtpServer = New ZLabel()
        txtSmtpServer = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblSmtpPort = New ZLabel()
        txtSmtpPort = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblSmtpUser = New ZLabel()
        txtSmtpUser = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblSmtpPassword = New ZLabel()
        txtSmtpPass = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblSmtpMail = New ZLabel()
        txtSmtpMail = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblexecuteRule = New ZLabel()
        CboDoMailRule = New ComboBox()
        txtbtnName = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblBtnName = New ZLabel()
        GroupBox1 = New GroupBox()
        btnDoMailRule = New ZButton()
        BtnCleanRuleValues = New ZButton()
        lblvarAttachs = New ZLabel()
        txtVarAttachs = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblColumnName = New ZLabel()
        lblColumnRoute = New ZLabel()
        txtColumnRoute = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtColumnName = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblConfiguration = New ZLabel()
        lblDatasetConfig = New ZLabel()
        GroupBox2 = New GroupBox()
        GroupBox3 = New GroupBox()
        btnExecAdditionalRule = New ZButton()
        btnCleanAddRule = New ZButton()
        Label6 = New ZLabel()
        CboExecAdditionalRule = New ComboBox()
        txtBtnExecuteAdditionalRuleName = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblAditionalExecution = New ZLabel()
        lblAditionalExec = New ZLabel()
        TbDoMail = New System.Windows.Forms.TabControl()
        tabMail = New System.Windows.Forms.TabPage()
        lblFor = New ZLabel()
        lblCC = New ZLabel()
        lblCCO = New ZLabel()
        tabOptions = New System.Windows.Forms.TabPage()
        chkViewAssociatedDocuments = New System.Windows.Forms.CheckBox()
        chkViewOriginal = New System.Windows.Forms.CheckBox()
        chkDisableHistory = New System.Windows.Forms.CheckBox()
        txtMailPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        CHKSaveMailPath = New System.Windows.Forms.CheckBox()
        chkgroupMailTo = New System.Windows.Forms.CheckBox()
        chkAttachLink = New System.Windows.Forms.CheckBox()
        ChkAutomatic = New System.Windows.Forms.CheckBox()
        tabAttachments = New System.Windows.Forms.TabPage()
        lblAttachTableVar = New ZLabel()
        lblAttachTableDocTypeId = New ZLabel()
        lblAttachTableDocId = New ZLabel()
        lblAttachTableDocName = New ZLabel()
        txtAttachTableVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtAttachTableDocTypeId = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtAttachTableDocId = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtAttachTableDocName = New Zamba.AppBlock.TextoInteligenteTextBox()
        chkAttachAssociatedDocuments = New System.Windows.Forms.CheckBox()
        BtnConfDocAsoc = New ZButton()
        chkAttach = New System.Windows.Forms.CheckBox()
        chkAtachDirectory = New System.Windows.Forms.CheckBox()
        tabSMTP = New System.Windows.Forms.TabPage()
        chkEnableSsl = New System.Windows.Forms.CheckBox()
        chkUseSMTPConf = New System.Windows.Forms.CheckBox()
        tabExecuteRule = New System.Windows.Forms.TabPage()
        GroupBox4 = New GroupBox()
        txtAdditionalRuleColumnName = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtAdditionalRuleColumnRoute = New Zamba.AppBlock.TextoInteligenteTextBox()
        Label7 = New ZLabel()
        Label8 = New ZLabel()
        tabImages = New System.Windows.Forms.TabPage()
        chkEmbedImages = New System.Windows.Forms.CheckBox()
        lstImages = New ListBox()
        btnOpenImage = New ZButton()
        btnRemoveImage = New ZButton()
        lblSaveMessage = New ZLabel()
        Panel1 = New System.Windows.Forms.Panel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox3.SuspendLayout()
        TbDoMail.SuspendLayout()
        tabMail.SuspendLayout()
        tabOptions.SuspendLayout()
        tabAttachments.SuspendLayout()
        tabSMTP.SuspendLayout()
        tabExecuteRule.SuspendLayout()
        GroupBox4.SuspendLayout()
        tabImages.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(TbDoMail)
        tbRule.Controls.Add(Panel1)
        tbRule.Size = New System.Drawing.Size(528, 499)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(536, 528)
        '
        'btnAceptar
        '
        btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnAceptar.ForeColor = System.Drawing.Color.White
        btnAceptar.Location = New System.Drawing.Point(410, 3)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(102, 30)
        btnAceptar.TabIndex = 12
        btnAceptar.Text = "Guardar"
        btnAceptar.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(3, 89)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(54, 16)
        Label3.TabIndex = 15
        Label3.Text = "Asunto"
        Label3.TextAlign = ContentAlignment.MiddleRight
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label5.Location = New System.Drawing.Point(3, 117)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(54, 16)
        Label5.TabIndex = 16
        Label5.Text = "Cuerpo"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtPara
        '
        txtPara.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtPara.Location = New System.Drawing.Point(56, 6)
        txtPara.Name = "txtPara"
        txtPara.Size = New System.Drawing.Size(452, 21)
        txtPara.TabIndex = 18
        txtPara.Text = ""
        '
        'txtCC
        '
        txtCC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtCC.Location = New System.Drawing.Point(56, 33)
        txtCC.Name = "txtCC"
        txtCC.Size = New System.Drawing.Size(452, 21)
        txtCC.TabIndex = 19
        txtCC.Text = ""
        '
        'txtCCO
        '
        txtCCO.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtCCO.Location = New System.Drawing.Point(56, 60)
        txtCCO.Name = "txtCCO"
        txtCCO.Size = New System.Drawing.Size(452, 21)
        txtCCO.TabIndex = 20
        txtCCO.Text = ""
        '
        'txtAsunto
        '
        txtAsunto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtAsunto.Location = New System.Drawing.Point(56, 87)
        txtAsunto.Name = "txtAsunto"
        txtAsunto.Size = New System.Drawing.Size(452, 21)
        txtAsunto.TabIndex = 21
        txtAsunto.Text = ""
        '
        'txtBody
        '
        txtBody.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtBody.Location = New System.Drawing.Point(56, 114)
        txtBody.Name = "txtBody"
        txtBody.Size = New System.Drawing.Size(452, 304)
        txtBody.TabIndex = 23
        txtBody.Text = ""
        '
        'lblSmtpServer
        '
        lblSmtpServer.BackColor = System.Drawing.Color.Transparent
        lblSmtpServer.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSmtpServer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSmtpServer.Location = New System.Drawing.Point(3, 49)
        lblSmtpServer.Name = "lblSmtpServer"
        lblSmtpServer.Size = New System.Drawing.Size(51, 13)
        lblSmtpServer.TabIndex = 38
        lblSmtpServer.Text = "Servidor:"
        lblSmtpServer.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtSmtpServer
        '
        txtSmtpServer.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtSmtpServer.Location = New System.Drawing.Point(60, 46)
        txtSmtpServer.Name = "txtSmtpServer"
        txtSmtpServer.Size = New System.Drawing.Size(303, 21)
        txtSmtpServer.TabIndex = 39
        txtSmtpServer.Text = ""
        '
        'lblSmtpPort
        '
        lblSmtpPort.AutoSize = True
        lblSmtpPort.BackColor = System.Drawing.Color.Transparent
        lblSmtpPort.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSmtpPort.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSmtpPort.Location = New System.Drawing.Point(370, 49)
        lblSmtpPort.Name = "lblSmtpPort"
        lblSmtpPort.Size = New System.Drawing.Size(51, 16)
        lblSmtpPort.TabIndex = 40
        lblSmtpPort.Text = "Puerto"
        lblSmtpPort.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtSmtpPort
        '
        txtSmtpPort.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtSmtpPort.Location = New System.Drawing.Point(419, 46)
        txtSmtpPort.Name = "txtSmtpPort"
        txtSmtpPort.Size = New System.Drawing.Size(56, 21)
        txtSmtpPort.TabIndex = 41
        txtSmtpPort.Text = ""
        '
        'lblSmtpUser
        '
        lblSmtpUser.BackColor = System.Drawing.Color.Transparent
        lblSmtpUser.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSmtpUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSmtpUser.Location = New System.Drawing.Point(3, 76)
        lblSmtpUser.Name = "lblSmtpUser"
        lblSmtpUser.Size = New System.Drawing.Size(47, 13)
        lblSmtpUser.TabIndex = 42
        lblSmtpUser.Text = "Usuario:"
        lblSmtpUser.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtSmtpUser
        '
        txtSmtpUser.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtSmtpUser.Location = New System.Drawing.Point(60, 73)
        txtSmtpUser.Name = "txtSmtpUser"
        txtSmtpUser.Size = New System.Drawing.Size(187, 21)
        txtSmtpUser.TabIndex = 43
        txtSmtpUser.Text = ""
        '
        'lblSmtpPassword
        '
        lblSmtpPassword.BackColor = System.Drawing.Color.Transparent
        lblSmtpPassword.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSmtpPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSmtpPassword.Location = New System.Drawing.Point(254, 76)
        lblSmtpPassword.Name = "lblSmtpPassword"
        lblSmtpPassword.Size = New System.Drawing.Size(38, 13)
        lblSmtpPassword.TabIndex = 44
        lblSmtpPassword.Text = "Clave:"
        lblSmtpPassword.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtSmtpPass
        '
        txtSmtpPass.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtSmtpPass.Location = New System.Drawing.Point(298, 73)
        txtSmtpPass.Name = "txtSmtpPass"
        txtSmtpPass.Size = New System.Drawing.Size(177, 21)
        txtSmtpPass.TabIndex = 45
        txtSmtpPass.Text = ""
        '
        'lblSmtpMail
        '
        lblSmtpMail.BackColor = System.Drawing.Color.Transparent
        lblSmtpMail.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSmtpMail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSmtpMail.Location = New System.Drawing.Point(3, 103)
        lblSmtpMail.Name = "lblSmtpMail"
        lblSmtpMail.Size = New System.Drawing.Size(35, 13)
        lblSmtpMail.TabIndex = 46
        lblSmtpMail.Text = "Email:"
        lblSmtpMail.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtSmtpMail
        '
        txtSmtpMail.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtSmtpMail.Location = New System.Drawing.Point(60, 100)
        txtSmtpMail.Name = "txtSmtpMail"
        txtSmtpMail.Size = New System.Drawing.Size(415, 21)
        txtSmtpMail.TabIndex = 47
        txtSmtpMail.Text = ""
        '
        'lblexecuteRule
        '
        lblexecuteRule.AutoSize = True
        lblexecuteRule.BackColor = System.Drawing.Color.Transparent
        lblexecuteRule.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblexecuteRule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblexecuteRule.Location = New System.Drawing.Point(10, 14)
        lblexecuteRule.Name = "lblexecuteRule"
        lblexecuteRule.Size = New System.Drawing.Size(334, 16)
        lblexecuteRule.TabIndex = 52
        lblexecuteRule.Text = "Ejecutar Regla desde formulario de envio de mail:"
        lblexecuteRule.TextAlign = ContentAlignment.MiddleLeft
        '
        'CboDoMailRule
        '
        CboDoMailRule.FormattingEnabled = True
        CboDoMailRule.Location = New System.Drawing.Point(10, 30)
        CboDoMailRule.Name = "CboDoMailRule"
        CboDoMailRule.Size = New System.Drawing.Size(453, 24)
        CboDoMailRule.TabIndex = 53
        '
        'txtbtnName
        '
        txtbtnName.Location = New System.Drawing.Point(213, 94)
        txtbtnName.Name = "txtbtnName"
        txtbtnName.Size = New System.Drawing.Size(254, 37)
        txtbtnName.TabIndex = 54
        txtbtnName.Text = ""
        '
        'lblBtnName
        '
        lblBtnName.AutoSize = True
        lblBtnName.BackColor = System.Drawing.Color.Transparent
        lblBtnName.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblBtnName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblBtnName.Location = New System.Drawing.Point(6, 97)
        lblBtnName.Name = "lblBtnName"
        lblBtnName.Size = New System.Drawing.Size(277, 16)
        lblBtnName.TabIndex = 55
        lblBtnName.Text = "Nombre del botón de ejecución de regla:"
        lblBtnName.TextAlign = ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        GroupBox1.Controls.Add(btnDoMailRule)
        GroupBox1.Controls.Add(BtnCleanRuleValues)
        GroupBox1.Controls.Add(lblBtnName)
        GroupBox1.Controls.Add(CboDoMailRule)
        GroupBox1.Controls.Add(txtbtnName)
        GroupBox1.Controls.Add(lblexecuteRule)
        GroupBox1.Location = New System.Drawing.Point(5, 88)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(474, 157)
        GroupBox1.TabIndex = 56
        GroupBox1.TabStop = False
        '
        'btnDoMailRule
        '
        btnDoMailRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnDoMailRule.FlatStyle = FlatStyle.Flat
        btnDoMailRule.ForeColor = System.Drawing.Color.White
        btnDoMailRule.Location = New System.Drawing.Point(10, 57)
        btnDoMailRule.Name = "btnDoMailRule"
        btnDoMailRule.Size = New System.Drawing.Size(136, 23)
        btnDoMailRule.TabIndex = 57
        btnDoMailRule.Text = "Ir a la regla de destino"
        btnDoMailRule.UseVisualStyleBackColor = True
        '
        'BtnCleanRuleValues
        '
        BtnCleanRuleValues.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        BtnCleanRuleValues.FlatStyle = FlatStyle.Flat
        BtnCleanRuleValues.ForeColor = System.Drawing.Color.White
        BtnCleanRuleValues.Location = New System.Drawing.Point(10, 126)
        BtnCleanRuleValues.Name = "BtnCleanRuleValues"
        BtnCleanRuleValues.Size = New System.Drawing.Size(86, 21)
        BtnCleanRuleValues.TabIndex = 56
        BtnCleanRuleValues.Text = "Limpiar Valores"
        BtnCleanRuleValues.UseVisualStyleBackColor = True
        '
        'lblvarAttachs
        '
        lblvarAttachs.AutoSize = True
        lblvarAttachs.BackColor = System.Drawing.Color.Transparent
        lblvarAttachs.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblvarAttachs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblvarAttachs.Location = New System.Drawing.Point(6, 54)
        lblvarAttachs.Name = "lblvarAttachs"
        lblvarAttachs.Size = New System.Drawing.Size(148, 16)
        lblvarAttachs.TabIndex = 57
        lblvarAttachs.Text = "Variable de adjuntos:"
        lblvarAttachs.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtVarAttachs
        '
        txtVarAttachs.Location = New System.Drawing.Point(114, 46)
        txtVarAttachs.Name = "txtVarAttachs"
        txtVarAttachs.Size = New System.Drawing.Size(187, 21)
        txtVarAttachs.TabIndex = 58
        txtVarAttachs.Text = ""
        '
        'lblColumnName
        '
        lblColumnName.AutoSize = True
        lblColumnName.BackColor = System.Drawing.Color.Transparent
        lblColumnName.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblColumnName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblColumnName.Location = New System.Drawing.Point(6, 62)
        lblColumnName.Name = "lblColumnName"
        lblColumnName.Size = New System.Drawing.Size(175, 16)
        lblColumnName.TabIndex = 59
        lblColumnName.Text = "Numero columna nombre:"
        lblColumnName.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblColumnRoute
        '
        lblColumnRoute.AutoSize = True
        lblColumnRoute.BackColor = System.Drawing.Color.Transparent
        lblColumnRoute.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblColumnRoute.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblColumnRoute.Location = New System.Drawing.Point(6, 35)
        lblColumnRoute.Name = "lblColumnRoute"
        lblColumnRoute.Size = New System.Drawing.Size(157, 16)
        lblColumnRoute.TabIndex = 60
        lblColumnRoute.Text = "Numero columna Ruta:"
        lblColumnRoute.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtColumnRoute
        '
        txtColumnRoute.Location = New System.Drawing.Point(141, 27)
        txtColumnRoute.Name = "txtColumnRoute"
        txtColumnRoute.Size = New System.Drawing.Size(164, 21)
        txtColumnRoute.TabIndex = 61
        txtColumnRoute.Text = ""
        '
        'txtColumnName
        '
        txtColumnName.Location = New System.Drawing.Point(141, 54)
        txtColumnName.Name = "txtColumnName"
        txtColumnName.Size = New System.Drawing.Size(164, 21)
        txtColumnName.TabIndex = 62
        txtColumnName.Text = ""
        '
        'lblConfiguration
        '
        lblConfiguration.AutoSize = True
        lblConfiguration.BackColor = System.Drawing.Color.Transparent
        lblConfiguration.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblConfiguration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblConfiguration.Location = New System.Drawing.Point(6, 19)
        lblConfiguration.Name = "lblConfiguration"
        lblConfiguration.Size = New System.Drawing.Size(564, 16)
        lblConfiguration.TabIndex = 63
        lblConfiguration.Text = "Configuracion de Adjuntos por ejecucion de regla desde formulario de envio de mai" &
    "l:"
        lblConfiguration.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblDatasetConfig
        '
        lblDatasetConfig.AutoSize = True
        lblDatasetConfig.BackColor = System.Drawing.Color.Transparent
        lblDatasetConfig.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblDatasetConfig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblDatasetConfig.Location = New System.Drawing.Point(7, 46)
        lblDatasetConfig.Name = "lblDatasetConfig"
        lblDatasetConfig.Size = New System.Drawing.Size(0, 16)
        lblDatasetConfig.TabIndex = 64
        lblDatasetConfig.TextAlign = ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        GroupBox2.Controls.Add(txtColumnName)
        GroupBox2.Controls.Add(txtColumnRoute)
        GroupBox2.Controls.Add(lblColumnRoute)
        GroupBox2.Controls.Add(lblColumnName)
        GroupBox2.Location = New System.Drawing.Point(9, 262)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New System.Drawing.Size(320, 85)
        GroupBox2.TabIndex = 65
        GroupBox2.TabStop = False
        GroupBox2.Text = "Si el resultado de la consulta es de tipo Dataset/Datatable:"
        '
        'GroupBox3
        '
        GroupBox3.Controls.Add(btnExecAdditionalRule)
        GroupBox3.Controls.Add(btnCleanAddRule)
        GroupBox3.Controls.Add(Label6)
        GroupBox3.Controls.Add(CboExecAdditionalRule)
        GroupBox3.Controls.Add(txtBtnExecuteAdditionalRuleName)
        GroupBox3.Controls.Add(lblAditionalExecution)
        GroupBox3.Location = New System.Drawing.Point(4, 409)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New System.Drawing.Size(474, 163)
        GroupBox3.TabIndex = 66
        GroupBox3.TabStop = False
        '
        'btnExecAdditionalRule
        '
        btnExecAdditionalRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnExecAdditionalRule.FlatStyle = FlatStyle.Flat
        btnExecAdditionalRule.ForeColor = System.Drawing.Color.White
        btnExecAdditionalRule.Location = New System.Drawing.Point(10, 57)
        btnExecAdditionalRule.Name = "btnExecAdditionalRule"
        btnExecAdditionalRule.Size = New System.Drawing.Size(136, 23)
        btnExecAdditionalRule.TabIndex = 57
        btnExecAdditionalRule.Text = "Ir a la regla de destino"
        btnExecAdditionalRule.UseVisualStyleBackColor = True
        '
        'btnCleanAddRule
        '
        btnCleanAddRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnCleanAddRule.FlatStyle = FlatStyle.Flat
        btnCleanAddRule.ForeColor = System.Drawing.Color.White
        btnCleanAddRule.Location = New System.Drawing.Point(6, 134)
        btnCleanAddRule.Name = "btnCleanAddRule"
        btnCleanAddRule.Size = New System.Drawing.Size(86, 21)
        btnCleanAddRule.TabIndex = 56
        btnCleanAddRule.Text = "Limpiar Valores"
        btnCleanAddRule.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.BackColor = System.Drawing.Color.Transparent
        Label6.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label6.Location = New System.Drawing.Point(2, 105)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(277, 16)
        Label6.TabIndex = 55
        Label6.Text = "Nombre del botón de ejecución de regla:"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        '
        'CboExecAdditionalRule
        '
        CboExecAdditionalRule.FormattingEnabled = True
        CboExecAdditionalRule.Location = New System.Drawing.Point(10, 30)
        CboExecAdditionalRule.Name = "CboExecAdditionalRule"
        CboExecAdditionalRule.Size = New System.Drawing.Size(453, 24)
        CboExecAdditionalRule.TabIndex = 53
        '
        'txtBtnExecuteAdditionalRuleName
        '
        txtBtnExecuteAdditionalRuleName.Location = New System.Drawing.Point(209, 102)
        txtBtnExecuteAdditionalRuleName.Name = "txtBtnExecuteAdditionalRuleName"
        txtBtnExecuteAdditionalRuleName.Size = New System.Drawing.Size(254, 37)
        txtBtnExecuteAdditionalRuleName.TabIndex = 54
        txtBtnExecuteAdditionalRuleName.Text = ""
        '
        'lblAditionalExecution
        '
        lblAditionalExecution.AutoSize = True
        lblAditionalExecution.BackColor = System.Drawing.Color.Transparent
        lblAditionalExecution.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblAditionalExecution.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblAditionalExecution.Location = New System.Drawing.Point(10, 17)
        lblAditionalExecution.Name = "lblAditionalExecution"
        lblAditionalExecution.Size = New System.Drawing.Size(422, 16)
        lblAditionalExecution.TabIndex = 52
        lblAditionalExecution.Text = "Ejecutar una regla adicional desde formulario de envio de mail:"
        lblAditionalExecution.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblAditionalExec
        '
        lblAditionalExec.AutoSize = True
        lblAditionalExec.BackColor = System.Drawing.Color.Transparent
        lblAditionalExec.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblAditionalExec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblAditionalExec.Location = New System.Drawing.Point(11, 393)
        lblAditionalExec.Name = "lblAditionalExec"
        lblAditionalExec.Size = New System.Drawing.Size(160, 16)
        lblAditionalExec.TabIndex = 67
        lblAditionalExec.Text = "Configuracion Adicional"
        lblAditionalExec.TextAlign = ContentAlignment.MiddleLeft
        '
        'TbDoMail
        '
        TbDoMail.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        TbDoMail.Controls.Add(tabMail)
        TbDoMail.Controls.Add(tabOptions)
        TbDoMail.Controls.Add(tabAttachments)
        TbDoMail.Controls.Add(tabSMTP)
        TbDoMail.Controls.Add(tabExecuteRule)
        TbDoMail.Controls.Add(tabImages)
        TbDoMail.Dock = System.Windows.Forms.DockStyle.Fill
        TbDoMail.Location = New System.Drawing.Point(3, 3)
        TbDoMail.Name = "TbDoMail"
        TbDoMail.SelectedIndex = 0
        TbDoMail.Size = New System.Drawing.Size(522, 456)
        TbDoMail.TabIndex = 70
        '
        'tabMail
        '
        tabMail.Controls.Add(lblFor)
        tabMail.Controls.Add(lblCC)
        tabMail.Controls.Add(lblCCO)
        tabMail.Controls.Add(Label3)
        tabMail.Controls.Add(Label5)
        tabMail.Controls.Add(txtPara)
        tabMail.Controls.Add(txtCC)
        tabMail.Controls.Add(txtCCO)
        tabMail.Controls.Add(txtAsunto)
        tabMail.Controls.Add(txtBody)
        tabMail.Location = New System.Drawing.Point(4, 28)
        tabMail.Name = "tabMail"
        tabMail.Padding = New System.Windows.Forms.Padding(3)
        tabMail.Size = New System.Drawing.Size(514, 424)
        tabMail.TabIndex = 0
        tabMail.Text = "Correo"
        '
        'lblFor
        '
        lblFor.AutoSize = True
        lblFor.BackColor = System.Drawing.Color.Transparent
        lblFor.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblFor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblFor.Location = New System.Drawing.Point(3, 8)
        lblFor.Name = "lblFor"
        lblFor.Size = New System.Drawing.Size(37, 16)
        lblFor.TabIndex = 72
        lblFor.Text = "Para"
        lblFor.TextAlign = ContentAlignment.MiddleRight
        '
        'lblCC
        '
        lblCC.AutoSize = True
        lblCC.BackColor = System.Drawing.Color.Transparent
        lblCC.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblCC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblCC.Location = New System.Drawing.Point(3, 35)
        lblCC.Name = "lblCC"
        lblCC.Size = New System.Drawing.Size(26, 16)
        lblCC.TabIndex = 71
        lblCC.Text = "CC"
        lblCC.TextAlign = ContentAlignment.MiddleRight
        '
        'lblCCO
        '
        lblCCO.AutoSize = True
        lblCCO.BackColor = System.Drawing.Color.Transparent
        lblCCO.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblCCO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblCCO.Location = New System.Drawing.Point(3, 62)
        lblCCO.Name = "lblCCO"
        lblCCO.Size = New System.Drawing.Size(36, 16)
        lblCCO.TabIndex = 70
        lblCCO.Text = "CCO"
        lblCCO.TextAlign = ContentAlignment.MiddleRight
        '
        'tabOptions
        '
        tabOptions.Controls.Add(chkViewAssociatedDocuments)
        tabOptions.Controls.Add(chkViewOriginal)
        tabOptions.Controls.Add(chkDisableHistory)
        tabOptions.Controls.Add(txtMailPath)
        tabOptions.Controls.Add(CHKSaveMailPath)
        tabOptions.Controls.Add(chkgroupMailTo)
        tabOptions.Controls.Add(chkAttachLink)
        tabOptions.Controls.Add(ChkAutomatic)
        tabOptions.Location = New System.Drawing.Point(4, 28)
        tabOptions.Name = "tabOptions"
        tabOptions.Padding = New System.Windows.Forms.Padding(3)
        tabOptions.Size = New System.Drawing.Size(514, 424)
        tabOptions.TabIndex = 3
        tabOptions.Text = "Opciones"
        tabOptions.UseVisualStyleBackColor = True
        '
        'chkViewAssociatedDocuments
        '
        chkViewAssociatedDocuments.AutoSize = True
        chkViewAssociatedDocuments.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkViewAssociatedDocuments.Location = New System.Drawing.Point(6, 99)
        chkViewAssociatedDocuments.Name = "chkViewAssociatedDocuments"
        chkViewAssociatedDocuments.Size = New System.Drawing.Size(100, 18)
        chkViewAssociatedDocuments.TabIndex = 82
        chkViewAssociatedDocuments.Text = "Ver asociados"
        chkViewAssociatedDocuments.UseVisualStyleBackColor = True
        '
        'chkViewOriginal
        '
        chkViewOriginal.AutoSize = True
        chkViewOriginal.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkViewOriginal.Location = New System.Drawing.Point(6, 68)
        chkViewOriginal.Name = "chkViewOriginal"
        chkViewOriginal.Size = New System.Drawing.Size(153, 18)
        chkViewOriginal.TabIndex = 81
        chkViewOriginal.Text = "Ver documento original"
        chkViewOriginal.UseVisualStyleBackColor = True
        '
        'chkDisableHistory
        '
        chkDisableHistory.AutoSize = True
        chkDisableHistory.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkDisableHistory.Location = New System.Drawing.Point(6, 161)
        chkDisableHistory.Name = "chkDisableHistory"
        chkDisableHistory.Size = New System.Drawing.Size(173, 18)
        chkDisableHistory.TabIndex = 80
        chkDisableHistory.Text = "Deshabilitar historial de mail"
        chkDisableHistory.UseVisualStyleBackColor = True
        '
        'txtMailPath
        '
        txtMailPath.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtMailPath.Location = New System.Drawing.Point(216, 190)
        txtMailPath.Name = "txtMailPath"
        txtMailPath.Size = New System.Drawing.Size(292, 21)
        txtMailPath.TabIndex = 79
        txtMailPath.Text = ""
        '
        'CHKSaveMailPath
        '
        CHKSaveMailPath.AutoSize = True
        CHKSaveMailPath.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        CHKSaveMailPath.Location = New System.Drawing.Point(6, 192)
        CHKSaveMailPath.Name = "CHKSaveMailPath"
        CHKSaveMailPath.Size = New System.Drawing.Size(204, 18)
        CHKSaveMailPath.TabIndex = 78
        CHKSaveMailPath.Text = "Guardar ruta del mail en variable:"
        CHKSaveMailPath.UseVisualStyleBackColor = True
        '
        'chkgroupMailTo
        '
        chkgroupMailTo.AutoSize = True
        chkgroupMailTo.BackColor = System.Drawing.Color.Transparent
        chkgroupMailTo.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkgroupMailTo.Location = New System.Drawing.Point(6, 130)
        chkgroupMailTo.Name = "chkgroupMailTo"
        chkgroupMailTo.Size = New System.Drawing.Size(159, 18)
        chkgroupMailTo.TabIndex = 72
        chkgroupMailTo.Text = "Agrupar por Destinatario"
        chkgroupMailTo.UseVisualStyleBackColor = False
        '
        'chkAttachLink
        '
        chkAttachLink.AutoSize = True
        chkAttachLink.BackColor = System.Drawing.Color.Transparent
        chkAttachLink.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkAttachLink.Location = New System.Drawing.Point(6, 6)
        chkAttachLink.Name = "chkAttachLink"
        chkAttachLink.Size = New System.Drawing.Size(94, 18)
        chkAttachLink.TabIndex = 73
        chkAttachLink.Text = "Agregar Link"
        chkAttachLink.UseVisualStyleBackColor = False
        '
        'ChkAutomatic
        '
        ChkAutomatic.AutoSize = True
        ChkAutomatic.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ChkAutomatic.Location = New System.Drawing.Point(6, 37)
        ChkAutomatic.Name = "ChkAutomatic"
        ChkAutomatic.Size = New System.Drawing.Size(120, 18)
        ChkAutomatic.TabIndex = 74
        ChkAutomatic.Text = "Envio automatico"
        ChkAutomatic.UseVisualStyleBackColor = True
        '
        'tabAttachments
        '
        tabAttachments.Controls.Add(lblAttachTableVar)
        tabAttachments.Controls.Add(lblAttachTableDocTypeId)
        tabAttachments.Controls.Add(lblAttachTableDocId)
        tabAttachments.Controls.Add(lblAttachTableDocName)
        tabAttachments.Controls.Add(txtAttachTableVar)
        tabAttachments.Controls.Add(txtAttachTableDocTypeId)
        tabAttachments.Controls.Add(txtAttachTableDocId)
        tabAttachments.Controls.Add(txtAttachTableDocName)
        tabAttachments.Controls.Add(chkAttachAssociatedDocuments)
        tabAttachments.Controls.Add(BtnConfDocAsoc)
        tabAttachments.Controls.Add(chkAttach)
        tabAttachments.Controls.Add(chkAtachDirectory)
        tabAttachments.Location = New System.Drawing.Point(4, 28)
        tabAttachments.Name = "tabAttachments"
        tabAttachments.Padding = New System.Windows.Forms.Padding(3)
        tabAttachments.Size = New System.Drawing.Size(514, 424)
        tabAttachments.TabIndex = 5
        tabAttachments.Text = "Adjuntos"
        tabAttachments.UseVisualStyleBackColor = True
        '
        'lblAttachTableVar
        '
        lblAttachTableVar.AutoSize = True
        lblAttachTableVar.BackColor = System.Drawing.Color.Transparent
        lblAttachTableVar.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblAttachTableVar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblAttachTableVar.Location = New System.Drawing.Point(6, 116)
        lblAttachTableVar.Name = "lblAttachTableVar"
        lblAttachTableVar.Size = New System.Drawing.Size(219, 16)
        lblAttachTableVar.TabIndex = 88
        lblAttachTableVar.Text = "Variable con Tabla de Asociados"
        lblAttachTableVar.TextAlign = ContentAlignment.MiddleRight
        '
        'lblAttachTableDocTypeId
        '
        lblAttachTableDocTypeId.AutoSize = True
        lblAttachTableDocTypeId.BackColor = System.Drawing.Color.Transparent
        lblAttachTableDocTypeId.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblAttachTableDocTypeId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblAttachTableDocTypeId.Location = New System.Drawing.Point(6, 143)
        lblAttachTableDocTypeId.Name = "lblAttachTableDocTypeId"
        lblAttachTableDocTypeId.Size = New System.Drawing.Size(218, 16)
        lblAttachTableDocTypeId.TabIndex = 87
        lblAttachTableDocTypeId.Text = "Columna con el ID de la Entidad"
        lblAttachTableDocTypeId.TextAlign = ContentAlignment.MiddleRight
        '
        'lblAttachTableDocId
        '
        lblAttachTableDocId.AutoSize = True
        lblAttachTableDocId.BackColor = System.Drawing.Color.Transparent
        lblAttachTableDocId.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblAttachTableDocId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblAttachTableDocId.Location = New System.Drawing.Point(6, 170)
        lblAttachTableDocId.Name = "lblAttachTableDocId"
        lblAttachTableDocId.Size = New System.Drawing.Size(230, 16)
        lblAttachTableDocId.TabIndex = 86
        lblAttachTableDocId.Text = "Columna con el ID del Documento"
        lblAttachTableDocId.TextAlign = ContentAlignment.MiddleRight
        '
        'lblAttachTableDocName
        '
        lblAttachTableDocName.AutoSize = True
        lblAttachTableDocName.BackColor = System.Drawing.Color.Transparent
        lblAttachTableDocName.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblAttachTableDocName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblAttachTableDocName.Location = New System.Drawing.Point(6, 197)
        lblAttachTableDocName.Name = "lblAttachTableDocName"
        lblAttachTableDocName.Size = New System.Drawing.Size(242, 16)
        lblAttachTableDocName.TabIndex = 81
        lblAttachTableDocName.Text = "Columna con el Nombre del Adjunto"
        lblAttachTableDocName.TextAlign = ContentAlignment.MiddleRight
        '
        'txtAttachTableVar
        '
        txtAttachTableVar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtAttachTableVar.Location = New System.Drawing.Point(222, 114)
        txtAttachTableVar.Name = "txtAttachTableVar"
        txtAttachTableVar.Size = New System.Drawing.Size(277, 21)
        txtAttachTableVar.TabIndex = 82
        txtAttachTableVar.Text = ""
        '
        'txtAttachTableDocTypeId
        '
        txtAttachTableDocTypeId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtAttachTableDocTypeId.Location = New System.Drawing.Point(222, 141)
        txtAttachTableDocTypeId.Name = "txtAttachTableDocTypeId"
        txtAttachTableDocTypeId.Size = New System.Drawing.Size(277, 21)
        txtAttachTableDocTypeId.TabIndex = 83
        txtAttachTableDocTypeId.Text = ""
        '
        'txtAttachTableDocId
        '
        txtAttachTableDocId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtAttachTableDocId.Location = New System.Drawing.Point(222, 168)
        txtAttachTableDocId.Name = "txtAttachTableDocId"
        txtAttachTableDocId.Size = New System.Drawing.Size(277, 21)
        txtAttachTableDocId.TabIndex = 84
        txtAttachTableDocId.Text = ""
        '
        'txtAttachTableDocName
        '
        txtAttachTableDocName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtAttachTableDocName.Location = New System.Drawing.Point(222, 195)
        txtAttachTableDocName.Name = "txtAttachTableDocName"
        txtAttachTableDocName.Size = New System.Drawing.Size(277, 21)
        txtAttachTableDocName.TabIndex = 85
        txtAttachTableDocName.Text = ""
        '
        'chkAttachAssociatedDocuments
        '
        chkAttachAssociatedDocuments.AutoSize = True
        chkAttachAssociatedDocuments.BackColor = System.Drawing.Color.Transparent
        chkAttachAssociatedDocuments.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkAttachAssociatedDocuments.Location = New System.Drawing.Point(6, 68)
        chkAttachAssociatedDocuments.Name = "chkAttachAssociatedDocuments"
        chkAttachAssociatedDocuments.Size = New System.Drawing.Size(203, 18)
        chkAttachAssociatedDocuments.TabIndex = 79
        chkAttachAssociatedDocuments.Text = "Adjuntar Documentos Asociados"
        chkAttachAssociatedDocuments.UseVisualStyleBackColor = False
        '
        'BtnConfDocAsoc
        '
        BtnConfDocAsoc.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        BtnConfDocAsoc.FlatStyle = FlatStyle.Flat
        BtnConfDocAsoc.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        BtnConfDocAsoc.ForeColor = System.Drawing.Color.White
        BtnConfDocAsoc.Location = New System.Drawing.Point(215, 66)
        BtnConfDocAsoc.Name = "BtnConfDocAsoc"
        BtnConfDocAsoc.Size = New System.Drawing.Size(100, 25)
        BtnConfDocAsoc.TabIndex = 80
        BtnConfDocAsoc.Text = "Configurar"
        BtnConfDocAsoc.UseVisualStyleBackColor = True
        '
        'chkAttach
        '
        chkAttach.AutoSize = True
        chkAttach.BackColor = System.Drawing.Color.Transparent
        chkAttach.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkAttach.Location = New System.Drawing.Point(6, 6)
        chkAttach.Name = "chkAttach"
        chkAttach.Size = New System.Drawing.Size(141, 18)
        chkAttach.TabIndex = 77
        chkAttach.Text = "Adjuntar Documento"
        chkAttach.UseVisualStyleBackColor = False
        '
        'chkAtachDirectory
        '
        chkAtachDirectory.AutoSize = True
        chkAtachDirectory.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkAtachDirectory.Location = New System.Drawing.Point(6, 37)
        chkAtachDirectory.Name = "chkAtachDirectory"
        chkAtachDirectory.Size = New System.Drawing.Size(119, 18)
        chkAtachDirectory.TabIndex = 78
        chkAtachDirectory.Text = "Adjuntar Carpeta"
        chkAtachDirectory.UseVisualStyleBackColor = True
        '
        'tabSMTP
        '
        tabSMTP.Controls.Add(chkEnableSsl)
        tabSMTP.Controls.Add(chkUseSMTPConf)
        tabSMTP.Controls.Add(lblSmtpServer)
        tabSMTP.Controls.Add(txtSmtpServer)
        tabSMTP.Controls.Add(lblSmtpPort)
        tabSMTP.Controls.Add(txtSmtpPort)
        tabSMTP.Controls.Add(lblSmtpUser)
        tabSMTP.Controls.Add(txtSmtpUser)
        tabSMTP.Controls.Add(lblSmtpPassword)
        tabSMTP.Controls.Add(txtSmtpPass)
        tabSMTP.Controls.Add(lblSmtpMail)
        tabSMTP.Controls.Add(txtSmtpMail)
        tabSMTP.Location = New System.Drawing.Point(4, 28)
        tabSMTP.Name = "tabSMTP"
        tabSMTP.Padding = New System.Windows.Forms.Padding(3)
        tabSMTP.Size = New System.Drawing.Size(514, 424)
        tabSMTP.TabIndex = 1
        tabSMTP.Text = "SMTP"
        '
        'chkEnableSsl
        '
        chkEnableSsl.AutoSize = True
        chkEnableSsl.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkEnableSsl.Location = New System.Drawing.Point(60, 127)
        chkEnableSsl.Name = "chkEnableSsl"
        chkEnableSsl.Size = New System.Drawing.Size(92, 18)
        chkEnableSsl.TabIndex = 49
        chkEnableSsl.Text = "Habilitar SSL"
        chkEnableSsl.TextAlign = ContentAlignment.BottomCenter
        chkEnableSsl.UseVisualStyleBackColor = True
        '
        'chkUseSMTPConf
        '
        chkUseSMTPConf.AutoSize = True
        chkUseSMTPConf.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkUseSMTPConf.Location = New System.Drawing.Point(6, 6)
        chkUseSMTPConf.Name = "chkUseSMTPConf"
        chkUseSMTPConf.Size = New System.Drawing.Size(273, 18)
        chkUseSMTPConf.TabIndex = 48
        chkUseSMTPConf.Text = "Utilizar configuracion SMTP para envio de mail"
        chkUseSMTPConf.TextAlign = ContentAlignment.BottomCenter
        chkUseSMTPConf.UseVisualStyleBackColor = True
        '
        'tabExecuteRule
        '
        tabExecuteRule.AutoScroll = True
        tabExecuteRule.Controls.Add(GroupBox4)
        tabExecuteRule.Controls.Add(lblConfiguration)
        tabExecuteRule.Controls.Add(lblAditionalExec)
        tabExecuteRule.Controls.Add(GroupBox1)
        tabExecuteRule.Controls.Add(GroupBox3)
        tabExecuteRule.Controls.Add(lblvarAttachs)
        tabExecuteRule.Controls.Add(GroupBox2)
        tabExecuteRule.Controls.Add(txtVarAttachs)
        tabExecuteRule.Controls.Add(lblDatasetConfig)
        tabExecuteRule.Location = New System.Drawing.Point(4, 28)
        tabExecuteRule.Name = "tabExecuteRule"
        tabExecuteRule.Padding = New System.Windows.Forms.Padding(3)
        tabExecuteRule.Size = New System.Drawing.Size(514, 424)
        tabExecuteRule.TabIndex = 2
        tabExecuteRule.Text = "Ejecución de Reglas"
        '
        'GroupBox4
        '
        GroupBox4.Controls.Add(txtAdditionalRuleColumnName)
        GroupBox4.Controls.Add(txtAdditionalRuleColumnRoute)
        GroupBox4.Controls.Add(Label7)
        GroupBox4.Controls.Add(Label8)
        GroupBox4.Location = New System.Drawing.Point(5, 589)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New System.Drawing.Size(320, 85)
        GroupBox4.TabIndex = 68
        GroupBox4.TabStop = False
        GroupBox4.Text = "Si el resultado de la consulta es de tipo Dataset/Datatable:"
        '
        'txtAdditionalRuleColumnName
        '
        txtAdditionalRuleColumnName.Location = New System.Drawing.Point(141, 54)
        txtAdditionalRuleColumnName.Name = "txtAdditionalRuleColumnName"
        txtAdditionalRuleColumnName.Size = New System.Drawing.Size(164, 21)
        txtAdditionalRuleColumnName.TabIndex = 62
        txtAdditionalRuleColumnName.Text = ""
        '
        'txtAdditionalRuleColumnRoute
        '
        txtAdditionalRuleColumnRoute.Location = New System.Drawing.Point(141, 27)
        txtAdditionalRuleColumnRoute.Name = "txtAdditionalRuleColumnRoute"
        txtAdditionalRuleColumnRoute.Size = New System.Drawing.Size(164, 21)
        txtAdditionalRuleColumnRoute.TabIndex = 61
        txtAdditionalRuleColumnRoute.Text = ""
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.BackColor = System.Drawing.Color.Transparent
        Label7.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label7.Location = New System.Drawing.Point(6, 35)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(157, 16)
        Label7.TabIndex = 60
        Label7.Text = "Numero columna Ruta:"
        Label7.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.BackColor = System.Drawing.Color.Transparent
        Label8.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label8.Location = New System.Drawing.Point(6, 62)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(175, 16)
        Label8.TabIndex = 59
        Label8.Text = "Numero columna nombre:"
        Label8.TextAlign = ContentAlignment.MiddleLeft
        '
        'tabImages
        '
        tabImages.Controls.Add(chkEmbedImages)
        tabImages.Controls.Add(lstImages)
        tabImages.Controls.Add(btnOpenImage)
        tabImages.Controls.Add(btnRemoveImage)
        tabImages.Location = New System.Drawing.Point(4, 28)
        tabImages.Name = "tabImages"
        tabImages.Padding = New System.Windows.Forms.Padding(3)
        tabImages.Size = New System.Drawing.Size(514, 424)
        tabImages.TabIndex = 4
        tabImages.Text = "Imágenes"
        tabImages.UseVisualStyleBackColor = True
        '
        'chkEmbedImages
        '
        chkEmbedImages.AutoSize = True
        chkEmbedImages.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkEmbedImages.Location = New System.Drawing.Point(6, 6)
        chkEmbedImages.Name = "chkEmbedImages"
        chkEmbedImages.Size = New System.Drawing.Size(282, 18)
        chkEmbedImages.TabIndex = 91
        chkEmbedImages.Text = "Embeber las imagenes del HTML en el mensaje"
        chkEmbedImages.UseVisualStyleBackColor = True
        '
        'lstImages
        '
        lstImages.Dock = System.Windows.Forms.DockStyle.Bottom
        lstImages.FormattingEnabled = True
        lstImages.ItemHeight = 16
        lstImages.Location = New System.Drawing.Point(3, 81)
        lstImages.Name = "lstImages"
        lstImages.Size = New System.Drawing.Size(508, 340)
        lstImages.TabIndex = 87
        '
        'btnOpenImage
        '
        btnOpenImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnOpenImage.FlatStyle = FlatStyle.Flat
        btnOpenImage.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnOpenImage.ForeColor = System.Drawing.Color.White
        btnOpenImage.Location = New System.Drawing.Point(6, 40)
        btnOpenImage.Name = "btnOpenImage"
        btnOpenImage.Size = New System.Drawing.Size(100, 25)
        btnOpenImage.TabIndex = 88
        btnOpenImage.Text = "Buscar"
        btnOpenImage.UseVisualStyleBackColor = True
        '
        'btnRemoveImage
        '
        btnRemoveImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnRemoveImage.FlatStyle = FlatStyle.Flat
        btnRemoveImage.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnRemoveImage.ForeColor = System.Drawing.Color.White
        btnRemoveImage.Location = New System.Drawing.Point(112, 40)
        btnRemoveImage.Name = "btnRemoveImage"
        btnRemoveImage.Size = New System.Drawing.Size(100, 25)
        btnRemoveImage.TabIndex = 90
        btnRemoveImage.Text = "Remover"
        btnRemoveImage.UseVisualStyleBackColor = True
        '
        'lblSaveMessage
        '
        lblSaveMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        lblSaveMessage.BackColor = System.Drawing.Color.Transparent
        lblSaveMessage.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSaveMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSaveMessage.Location = New System.Drawing.Point(160, 4)
        lblSaveMessage.Name = "lblSaveMessage"
        lblSaveMessage.Size = New System.Drawing.Size(358, 30)
        lblSaveMessage.TabIndex = 71
        lblSaveMessage.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        lblSaveMessage.TextAlign = ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Panel1.Controls.Add(btnAceptar)
        Panel1.Controls.Add(lblSaveMessage)
        Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel1.Location = New System.Drawing.Point(3, 459)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(522, 37)
        Panel1.TabIndex = 72
        '
        'UCDoMail
        '
        Name = "UCDoMail"
        Size = New System.Drawing.Size(536, 528)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        TbDoMail.ResumeLayout(False)
        tabMail.ResumeLayout(False)
        tabMail.PerformLayout()
        tabOptions.ResumeLayout(False)
        tabOptions.PerformLayout()
        tabAttachments.ResumeLayout(False)
        tabAttachments.PerformLayout()
        tabSMTP.ResumeLayout(False)
        tabSMTP.PerformLayout()
        tabExecuteRule.ResumeLayout(False)
        tabExecuteRule.PerformLayout()
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        tabImages.ResumeLayout(False)
        tabImages.PerformLayout()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub
#End Region

    Dim currentRule As IDOMail
    Dim ofdDialog As New OpenFileDialog
    Private Event OpenMissedRule(ByVal workflowId As Int64, ByVal ruleId As Int64)
    Private Delegate Sub ChangeCursorDelegate(ByVal cur As Cursor)

#Region "Constructor"

    Public Sub New(ByRef this As IDOMail, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(this, _wfPanelCircuit)
        Try
            RemoveHandler OpenMissedRule, AddressOf _wfPanelCircuit.OpenMissedRule
            AddHandler OpenMissedRule, AddressOf _wfPanelCircuit.OpenMissedRule
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        InitializeComponent()
        currentRule = this
        this_Load()
    End Sub

#End Region

#Region "Propiedades"

    Public Shadows ReadOnly Property MyRule() As IDOMail
        Get
            Return DirectCast(Rule, IDOMail)
        End Get
    End Property

#End Region

#Region "Eventos y métodos"

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el userControl UCDoMail
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	07/08/2008	Modified  Validación y llamada al método recoverImages()
    ''' </history>
    Private Sub this_Load()
        Try
            txtPara.Text = currentRule.Para
            txtCC.Text = currentRule.CC
            txtCCO.Text = currentRule.CCO
            txtAsunto.Text = currentRule.Asunto
            txtAsunto.ModificarColores()
            chkAttach.Checked = currentRule.SendDocument
            txtBody.Text = currentRule.Body
            txtBody.ModificarColores()
            chkAttachAssociatedDocuments.Checked = currentRule.AttachAssociatedDocuments
            BtnConfDocAsoc.Enabled = chkAttachAssociatedDocuments.Checked
            chkgroupMailTo.Checked = currentRule.groupMailTo
            chkAttachLink.Checked = currentRule.AttachLink
            ChkAutomatic.Checked = currentRule.Automatic
            ofdDialog.Filter = "Archivos de imagen (*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.TIFF)|*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.TIFF"
            txtSmtpMail.Text = currentRule.SmtpMail
            txtSmtpPass.Text = currentRule.SmtpPass
            txtSmtpPort.Text = currentRule.SmtpPort
            txtSmtpServer.Text = currentRule.SmtpServer
            txtSmtpUser.Text = currentRule.SmtpUser
            chkEnableSsl.Checked = currentRule.SmtpEnableSsl
            chkUseSMTPConf.Checked = currentRule.UseSMTPConfig
            chkEmbedImages.Checked = currentRule.EmbedImages
            txtColumnName.Text = currentRule.ColumnName
            txtColumnRoute.Text = currentRule.ColumnRoute

            txtAttachTableVar.Text = currentRule.AttachTableVar
            txtAttachTableDocTypeId.Text = currentRule.AttachTableColDocTypeId
            txtAttachTableDocId.Text = currentRule.AttachTableColDocId
            txtAttachTableDocName.Text = currentRule.AttachTableColDocName
            CheckAttachTableState()

            txtSmtpMail.Enabled = currentRule.UseSMTPConfig
            txtSmtpPass.Enabled = currentRule.UseSMTPConfig
            txtSmtpPort.Enabled = currentRule.UseSMTPConfig
            txtSmtpServer.Enabled = currentRule.UseSMTPConfig
            txtSmtpUser.Enabled = currentRule.UseSMTPConfig
            chkEnableSsl.Enabled = currentRule.UseSMTPConfig
            CHKSaveMailPath.Checked = currentRule.SaveMailPath
            txtMailPath.Text = currentRule.MailPath
            txtMailPath.Enabled = CHKSaveMailPath.Checked
            chkDisableHistory.Checked = currentRule.DisableHistory
            txtVarAttachs.Text = currentRule.VarAttachs

            If ((currentRule.ImagesNames.Length > 0) AndAlso (currentRule.PathImages.Length > 0)) Then
                recoverImages()
            End If

            'cargo el combo de reglas
            Dim dt As DataTable = WFRulesBusiness.GetDoExecuteRules().Tables(0)
            CboDoMailRule.DataSource = dt
            CboDoMailRule.DisplayMember = dt.Columns(0).ColumnName 'name
            CboDoMailRule.ValueMember = dt.Columns(1).ColumnName   'ID
            CboDoMailRule.SelectedValue = MyRule.RuleID

            'cargo el combo de ejecucion de regla adicional
            Dim dtAddR As DataTable = WFRulesBusiness.GetDoExecuteRules().Tables(0)
            CboExecAdditionalRule.DataSource = dtAddR
            CboExecAdditionalRule.DisplayMember = dtAddR.Columns(0).ColumnName 'name
            CboExecAdditionalRule.ValueMember = dtAddR.Columns(1).ColumnName   'ID
            CboExecAdditionalRule.SelectedValue = MyRule.ExecuteAdditionalRuleID
            txtAdditionalRuleColumnName.Text = currentRule.AdditionalRuleColumnName
            txtAdditionalRuleColumnRoute.Text = MyRule.AdditionalRuleColumnRoute
            txtBtnExecuteAdditionalRuleName.Text = currentRule.BtnAdditionalRuleName

            'view original
            chkViewOriginal.Checked = currentRule.ViewOriginal
            chkViewAssociatedDocuments.Checked = currentRule.ViewAssociateDocuments
            txtbtnName.Text = currentRule.BtnName

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	07/08/2008	Modified    Se guardan los nombres y paths de las imágenes
    ''' </history>
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Try
            CheckFields()
            currentRule.Para = txtPara.Text
            currentRule.CC = txtCC.Text
            currentRule.CCO = txtCCO.Text
            currentRule.Asunto = txtAsunto.Text
            currentRule.SendDocument = chkAttach.Checked
            currentRule.Body = txtBody.Text
            currentRule.AttachAssociatedDocuments = chkAttachAssociatedDocuments.Checked
            currentRule.AttachLink = chkAttachLink.Checked
            currentRule.AttachTableVar = txtAttachTableVar.Text
            currentRule.AttachTableColDocTypeId = txtAttachTableDocTypeId.Text
            currentRule.AttachTableColDocId = txtAttachTableDocId.Text
            currentRule.AttachTableColDocName = txtAttachTableDocName.Text
            currentRule.groupMailTo = chkgroupMailTo.Checked
            currentRule.Automatic = ChkAutomatic.Checked
            currentRule.UseSMTPConfig = chkUseSMTPConf.Checked
            currentRule.SmtpServer = txtSmtpServer.Text
            currentRule.SmtpUser = txtSmtpUser.Text
            currentRule.SmtpPort = txtSmtpPort.Text
            currentRule.SmtpPass = txtSmtpPass.Text
            currentRule.SmtpMail = txtSmtpMail.Text
            currentRule.SmtpEnableSsl = chkEnableSsl.Checked
            currentRule.EmbedImages = chkEmbedImages.Checked
            currentRule.SaveMailPath = CHKSaveMailPath.Checked
            currentRule.MailPath = txtMailPath.Text
            currentRule.DisableHistory = chkDisableHistory.Checked
            currentRule.VarAttachs = txtVarAttachs.Text
            currentRule.BtnName = txtbtnName.Text
            currentRule.BtnAdditionalRuleName = txtBtnExecuteAdditionalRuleName.Text
            currentRule.ViewOriginal = chkViewOriginal.Checked
            currentRule.ViewAssociateDocuments = chkViewAssociatedDocuments.Checked
            currentRule.ColumnName = txtColumnName.Text
            currentRule.ColumnRoute = txtColumnRoute.Text
            currentRule.AdditionalRuleColumnName = txtAdditionalRuleColumnName.Text
            currentRule.AdditionalRuleColumnRoute = txtAdditionalRuleColumnRoute.Text

            currentRule.ImagesNames = String.Empty
            currentRule.PathImages = String.Empty
            For Each Image As Image In lstImages.Items
                If String.IsNullOrEmpty(currentRule.PathImages) Then
                    currentRule.ImagesNames = Image.Name
                    currentRule.PathImages = Image.Path
                Else
                    currentRule.ImagesNames = currentRule.ImagesNames & ";" & Image.Name
                    currentRule.PathImages = currentRule.PathImages & ";" & Image.Path
                End If
            Next

            'TODO: verificar porque se asigna sobre myrule y no sobre currentrule
            If CboDoMailRule.SelectedValue <> Nothing Then
                MyRule.RuleID = Int32.Parse(CboDoMailRule.SelectedValue)
            Else
                MyRule.RuleID = -1
            End If
            If CboExecAdditionalRule.SelectedValue <> Nothing Then
                MyRule.ExecuteAdditionalRuleID = Int32.Parse(CboExecAdditionalRule.SelectedValue)
            Else
                MyRule.ExecuteAdditionalRuleID = -1
            End If

            WFRulesBusiness.UpdateParamItem(currentRule.ID, 0, currentRule.Para)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 1, currentRule.CC)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 2, currentRule.CCO)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 3, currentRule.Asunto)
            If currentRule.SendDocument Then
                WFRulesBusiness.UpdateParamItem(currentRule.ID, 4, 1)
            Else
                WFRulesBusiness.UpdateParamItem(currentRule.ID, 4, 0)
            End If
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 5, currentRule.Body)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 6, currentRule.AttachAssociatedDocuments)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 7, currentRule.ImagesNames)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 8, currentRule.PathImages)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 9, currentRule.groupMailTo)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 10, currentRule.AttachLink)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 11, currentRule.DTType)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 12, currentRule.Selection)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 13, currentRule.DocTypes)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 14, currentRule.Index)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 15, currentRule.Oper)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 16, currentRule.IndexValue)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 17, currentRule.Automatic)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 18, currentRule.UseSMTPConfig)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 19, currentRule.SmtpServer)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 20, currentRule.SmtpPort)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 21, currentRule.SmtpUser)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 22, currentRule.SmtpPass)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 23, currentRule.SmtpMail)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 24, currentRule.KeepAssociatedDocsName)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 25, currentRule.EmbedImages)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 26, currentRule.SaveMailPath)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 27, currentRule.MailPath)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 28, currentRule.DisableHistory)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 29, currentRule.FilterDocID)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 30, currentRule.RuleID)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 31, currentRule.BtnName)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 32, currentRule.VarAttachs)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 33, currentRule.ColumnName)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 34, currentRule.ColumnRoute)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 35, currentRule.ExecuteAdditionalRuleID)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 36, currentRule.BtnAdditionalRuleName)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 37, currentRule.ViewOriginal)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 38, currentRule.ViewAssociateDocuments)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 39, currentRule.AdditionalRuleColumnName)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 40, currentRule.AdditionalRuleColumnRoute)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 41, currentRule.AttachTableVar)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 42, currentRule.AttachTableColDocTypeId)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 43, currentRule.AttachTableColDocId)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 44, currentRule.AttachTableColDocName)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 45, currentRule.SmtpEnableSsl)

            lblSaveMessage.ForeColor = Color.FromArgb(76, 76, 76)
            lblSaveMessage.Text = "Modificaciones aplicadas de manera exitosa"

            UserBusiness.Rights.SaveAction(currentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & currentRule.Name & "(" & currentRule.ID & ")")
        Catch ex As ZambaEx
            lblSaveMessage.ForeColor = Color.Red
            lblSaveMessage.Text = ex.Message
            MessageBox.Show(ex.Message, "Mail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            lblSaveMessage.ForeColor = Color.Red
            lblSaveMessage.Text = "Error al aplicar las modificaciones"
        End Try
    End Sub

    Private Function CheckFields() As Boolean
        If txtColumnName.Text.Length > 0 AndAlso Not IsNumeric(txtColumnName.Text) Then
            txtColumnName.Focus()
            Throw New ZambaEx("Debe ingresar un valor Numerico para la columna Nombre")
        End If
        If txtColumnRoute.Text.Length > 0 AndAlso Not IsNumeric(txtColumnRoute.Text) Then
            txtColumnRoute.Focus()
            Throw New ZambaEx("Debe ingresar un valor Numerico para la columna Ruta")
        End If
        If txtAdditionalRuleColumnName.Text.Length > 0 AndAlso Not IsNumeric(txtAdditionalRuleColumnName.Text) Then
            txtAdditionalRuleColumnName.Focus()
            Throw New ZambaEx("Debe ingresar un valor Numerico para la columna Nombre")
        End If
        If txtAdditionalRuleColumnRoute.Text.Length > 0 AndAlso Not IsNumeric(txtAdditionalRuleColumnRoute.Text) Then
            txtAdditionalRuleColumnRoute.Focus()
            Throw New ZambaEx("Debe ingresar un valor Numerico para la columna Ruta")
        End If
        If txtColumnName.Text.Length > 0 AndAlso txtColumnRoute.Text.Length = 0 Then
            txtColumnRoute.Focus()
            Throw New ZambaEx("Si especifica un valor Numerico para la columna Nombre debe hacerlo tambien en la columna Ruta")
        End If
        If txtAttachTableVar.Text.Trim.Length > 0 Then
            If txtAttachTableDocTypeId.Text.Trim.Length = 0 Then
                txtAttachTableDocTypeId.Focus()
                Throw New ZambaEx("Debe completar el campo 'Columna con el ID de la Entidad'")
            End If
            If txtAttachTableDocId.Text.Trim.Length = 0 Then
                txtAttachTableDocId.Focus()
                Throw New ZambaEx("Debe completar el campo 'Columna con el ID del Documento'")
            End If
            If txtAttachTableDocName.Text.Trim.Length = 0 Then
                txtAttachTableDocName.Focus()
                Throw New ZambaEx("Debe completar el campo 'Columna con el Nombre del Adjunto'")
            End If
        End If
    End Function

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón "Buscar"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	06/08/2008	Created
    ''' </history>
    Private Sub btnOpenImage_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnOpenImage.Click
        If (ofdDialog.ShowDialog() = DialogResult.OK) Then
            ' Parámetros: Nombre y Path de la imágen
            Dim imag As New Image(ofdDialog.SafeFileName.Substring(0, ofdDialog.SafeFileName.Length - 4), ofdDialog.FileName)
            lstImages.Items.Add(imag)
            lstImages.ValueMember = "Name"
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón "Remover"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	07/08/2008	Created
    ''' </history>
    Private Sub btnRemoveImage_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRemoveImage.Click
        If Not (IsNothing(lstImages.SelectedItem)) Then
            lstImages.Items.Remove(lstImages.SelectedItem)
        End If
    End Sub

    Private Sub BtnConfDocAsoc_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnConfDocAsoc.Click
        Try
            Dim frmConfig As New MailDocAsocConfig(MyRule)
            If frmConfig.ShowDialog = DialogResult.OK Then
                With MyRule
                    .Selection = frmConfig.mRule.Selection
                    .DTType = frmConfig.mRule.DTType
                    .DocTypes = frmConfig.mRule.DocTypes
                    .Oper = frmConfig.mRule.Oper
                    .IndexValue = frmConfig.mRule.IndexValue
                    .KeepAssociatedDocsName = frmConfig.mRule.KeepAssociatedDocsName
                    .FilterDocID = frmConfig.mRule.FilterDocID
                End With
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub chkAttachAssociatedDocuments_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkAttachAssociatedDocuments.CheckedChanged
        BtnConfDocAsoc.Enabled = chkAttachAssociatedDocuments.Checked
    End Sub

    Private Sub chkAtachDirectory_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkAtachDirectory.CheckedChanged
        Dim path As String = String.Empty
        Dim files() As String = {}
        Dim Dlg As New FolderBrowserDialog
        Dim FileName As String = String.Empty
        Try
            If chkAtachDirectory.Checked = True Then
                If Dlg.ShowDialog = DialogResult.OK Then
                    path = Dlg.SelectedPath
                    files = System.IO.Directory.GetFiles(path)

                    For Each root As String In files
                        FileName = root.Substring(root.LastIndexOf("\"), (root.Length - root.LastIndexOf("\")))
                        Dim imag As New Image(FileName.Remove(FileName.LastIndexOf("\"), 1), root)
                        lstImages.Items.Add(imag)
                        lstImages.ValueMember = "Name"

                    Next
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CHKSaveMailPath_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CHKSaveMailPath.CheckedChanged
        If CHKSaveMailPath.Checked Then
            txtMailPath.Enabled = True
        Else
            txtMailPath.Enabled = False
        End If
    End Sub

    Private Sub BtnCleanRuleValues_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnCleanRuleValues.Click
        CboDoMailRule.SelectedValue = -1
        txtbtnName.Text = String.Empty
    End Sub

    Private Sub btnCleanAddRule_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCleanAddRule.Click
        CboExecAdditionalRule.SelectedValue = -1
        txtBtnExecuteAdditionalRuleName.Text = String.Empty
    End Sub

    Private Sub btnDoMailRule_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnDoMailRule.Click
        If Not IsNothing(CboDoMailRule.SelectedValue) Then
            Dim wfbe As New WFBusinessExt
            Try
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
                Dim ruleId As Int64 = Int64.Parse(CboDoMailRule.SelectedValue)
                RaiseEvent OpenMissedRule(wfbe.GetWorkflowIdByRule(ruleId), ruleId)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                wfbe = Nothing
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
            End Try
        End If
    End Sub

    Private Sub btnExecAdditionalRule_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExecAdditionalRule.Click
        If Not IsNothing(CboExecAdditionalRule.SelectedValue) Then
            Dim wfbe As New WFBusinessExt
            Try
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
                Dim ruleId As Int64 = Int64.Parse(CboExecAdditionalRule.SelectedValue)
                RaiseEvent OpenMissedRule(wfbe.GetWorkflowIdByRule(ruleId), ruleId)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                wfbe = Nothing
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
            End Try
        End If
    End Sub

    Private Sub chkUseSMTPConf_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkUseSMTPConf.CheckedChanged
        txtSmtpMail.Enabled = chkUseSMTPConf.Checked
        txtSmtpPass.Enabled = chkUseSMTPConf.Checked
        txtSmtpPort.Enabled = chkUseSMTPConf.Checked
        txtSmtpServer.Enabled = chkUseSMTPConf.Checked
        txtSmtpUser.Enabled = chkUseSMTPConf.Checked
        chkEnableSsl.Enabled = chkUseSMTPConf.Checked
    End Sub

    ''' <summary>
    ''' Coloca las instancias Image en el listbox, cada una con su correspondiente nombre y path
    ''' </summary>
    Private Sub recoverImages()

        Dim tableImagesNames() As String
        Dim tablePathImages() As String
        Dim n As Integer
        tableImagesNames = Split(currentRule.ImagesNames, ";")
        tablePathImages = Split(currentRule.PathImages, ";")

        For n = 0 To UBound(tableImagesNames, 1)
            ' Parámetros: Nombre y Path de la imagen
            Dim image As New Image(tableImagesNames(n), tablePathImages(n))
            lstImages.Items.Add(image)
        Next

        lstImages.ValueMember = "Name"

    End Sub

    Private Sub ChangeCursor(ByVal cur As Cursor)
        Try
            Cursor = cur
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CheckAttachTableState()
        Dim enable As Boolean = Not String.IsNullOrEmpty(txtAttachTableVar.Text)
        txtAttachTableDocId.Enabled = enable
        txtAttachTableDocName.Enabled = enable
        txtAttachTableDocTypeId.Enabled = enable
    End Sub

    Private Sub txtAttachTableVar_KeyUp(sender As Object, e As KeyEventArgs) Handles txtAttachTableVar.KeyUp
        CheckAttachTableState()
    End Sub
#End Region

#Region "Clase privada Image"

    ''' <summary>
    ''' Clase utiliza para guardar un elemento Image en el listbox de imágenes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	07/08/2008	Created
    ''' </history>
    Private Class Image

        Public m_name As String
        Public m_path As String

        Public Sub New(ByVal n As String, ByVal p As String)
            Name = n
            Path = p
        End Sub

        Public Property Name() As String
            Get
                Return (m_name)
            End Get
            Set(ByVal value As String)
                m_name = value
            End Set
        End Property

        Public Property Path() As String
            Get
                Return (m_path)
            End Get
            Set(ByVal value As String)
                m_path = value
            End Set
        End Property

    End Class

#End Region

End Class