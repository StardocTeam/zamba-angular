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
        Me.btnAceptar = New Zamba.AppBlock.ZButton()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.txtPara = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtCC = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtCCO = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtAsunto = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtBody = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblSmtpServer = New Zamba.AppBlock.ZLabel()
        Me.txtSmtpServer = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblSmtpPort = New Zamba.AppBlock.ZLabel()
        Me.txtSmtpPort = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblSmtpUser = New Zamba.AppBlock.ZLabel()
        Me.txtSmtpUser = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblSmtpPassword = New Zamba.AppBlock.ZLabel()
        Me.txtSmtpPass = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblSmtpMail = New Zamba.AppBlock.ZLabel()
        Me.txtSmtpMail = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblexecuteRule = New Zamba.AppBlock.ZLabel()
        Me.CboDoMailRule = New System.Windows.Forms.ComboBox()
        Me.txtbtnName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblBtnName = New Zamba.AppBlock.ZLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnDoMailRule = New Zamba.AppBlock.ZButton()
        Me.BtnCleanRuleValues = New Zamba.AppBlock.ZButton()
        Me.lblvarAttachs = New Zamba.AppBlock.ZLabel()
        Me.txtVarAttachs = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblColumnName = New Zamba.AppBlock.ZLabel()
        Me.lblColumnRoute = New Zamba.AppBlock.ZLabel()
        Me.txtColumnRoute = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtColumnName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblConfiguration = New Zamba.AppBlock.ZLabel()
        Me.lblDatasetConfig = New Zamba.AppBlock.ZLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnExecAdditionalRule = New Zamba.AppBlock.ZButton()
        Me.btnCleanAddRule = New Zamba.AppBlock.ZButton()
        Me.Label6 = New Zamba.AppBlock.ZLabel()
        Me.CboExecAdditionalRule = New System.Windows.Forms.ComboBox()
        Me.txtBtnExecuteAdditionalRuleName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblAditionalExecution = New Zamba.AppBlock.ZLabel()
        Me.lblAditionalExec = New Zamba.AppBlock.ZLabel()
        Me.TbDoMail = New System.Windows.Forms.TabControl()
        Me.tabMail = New System.Windows.Forms.TabPage()
        Me.lblFor = New Zamba.AppBlock.ZLabel()
        Me.lblCC = New Zamba.AppBlock.ZLabel()
        Me.lblCCO = New Zamba.AppBlock.ZLabel()
        Me.tabOptions = New System.Windows.Forms.TabPage()
        Me.chkViewAssociatedDocuments = New System.Windows.Forms.CheckBox()
        Me.chkViewOriginal = New System.Windows.Forms.CheckBox()
        Me.chkDisableHistory = New System.Windows.Forms.CheckBox()
        Me.txtMailPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.CHKSaveMailPath = New System.Windows.Forms.CheckBox()
        Me.chkgroupMailTo = New System.Windows.Forms.CheckBox()
        Me.chkAttachLink = New System.Windows.Forms.CheckBox()
        Me.ChkAutomatic = New System.Windows.Forms.CheckBox()
        Me.tabAttachments = New System.Windows.Forms.TabPage()
        Me.lblAttachTableVar = New Zamba.AppBlock.ZLabel()
        Me.lblAttachTableDocTypeId = New Zamba.AppBlock.ZLabel()
        Me.lblAttachTableDocId = New Zamba.AppBlock.ZLabel()
        Me.lblAttachTableDocName = New Zamba.AppBlock.ZLabel()
        Me.txtAttachTableVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtAttachTableDocTypeId = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtAttachTableDocId = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtAttachTableDocName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.chkAttachAssociatedDocuments = New System.Windows.Forms.CheckBox()
        Me.BtnConfDocAsoc = New Zamba.AppBlock.ZButton()
        Me.chkAttach = New System.Windows.Forms.CheckBox()
        Me.chkAtachDirectory = New System.Windows.Forms.CheckBox()
        Me.tabSMTP = New System.Windows.Forms.TabPage()
        Me.chkEnableSsl = New System.Windows.Forms.CheckBox()
        Me.chkUseSMTPConf = New System.Windows.Forms.CheckBox()
        Me.tabExecuteRule = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtAdditionalRuleColumnName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtAdditionalRuleColumnRoute = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label7 = New Zamba.AppBlock.ZLabel()
        Me.Label8 = New Zamba.AppBlock.ZLabel()
        Me.tabImages = New System.Windows.Forms.TabPage()
        Me.chkEmbedImages = New System.Windows.Forms.CheckBox()
        Me.lstImages = New System.Windows.Forms.ListBox()
        Me.btnOpenImage = New Zamba.AppBlock.ZButton()
        Me.btnRemoveImage = New Zamba.AppBlock.ZButton()
        Me.lblSaveMessage = New Zamba.AppBlock.ZLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TbDoMail.SuspendLayout()
        Me.tabMail.SuspendLayout()
        Me.tabOptions.SuspendLayout()
        Me.tabAttachments.SuspendLayout()
        Me.tabSMTP.SuspendLayout()
        Me.tabExecuteRule.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.tabImages.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.TbDoMail)
        Me.tbRule.Controls.Add(Me.Panel1)
        Me.tbRule.Size = New System.Drawing.Size(528, 499)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(536, 528)
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(410, 3)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(102, 30)
        Me.btnAceptar.TabIndex = 12
        Me.btnAceptar.Text = "Guardar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.FontSize = 9.75!
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(3, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 16)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Asunto"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.FontSize = 9.75!
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(3, 117)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 16)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Cuerpo"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPara
        '
        Me.txtPara.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPara.Location = New System.Drawing.Point(56, 6)
        Me.txtPara.Name = "txtPara"
        Me.txtPara.Size = New System.Drawing.Size(452, 21)
        Me.txtPara.TabIndex = 18
        Me.txtPara.Text = ""
        '
        'txtCC
        '
        Me.txtCC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCC.Location = New System.Drawing.Point(56, 33)
        Me.txtCC.Name = "txtCC"
        Me.txtCC.Size = New System.Drawing.Size(452, 21)
        Me.txtCC.TabIndex = 19
        Me.txtCC.Text = ""
        '
        'txtCCO
        '
        Me.txtCCO.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCCO.Location = New System.Drawing.Point(56, 60)
        Me.txtCCO.Name = "txtCCO"
        Me.txtCCO.Size = New System.Drawing.Size(452, 21)
        Me.txtCCO.TabIndex = 20
        Me.txtCCO.Text = ""
        '
        'txtAsunto
        '
        Me.txtAsunto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAsunto.Location = New System.Drawing.Point(56, 87)
        Me.txtAsunto.Name = "txtAsunto"
        Me.txtAsunto.Size = New System.Drawing.Size(452, 21)
        Me.txtAsunto.TabIndex = 21
        Me.txtAsunto.Text = ""
        '
        'txtBody
        '
        Me.txtBody.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBody.Location = New System.Drawing.Point(56, 114)
        Me.txtBody.Name = "txtBody"
        Me.txtBody.Size = New System.Drawing.Size(452, 304)
        Me.txtBody.TabIndex = 23
        Me.txtBody.Text = ""
        '
        'lblSmtpServer
        '
        Me.lblSmtpServer.BackColor = System.Drawing.Color.Transparent
        Me.lblSmtpServer.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmtpServer.FontSize = 9.75!
        Me.lblSmtpServer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSmtpServer.Location = New System.Drawing.Point(3, 49)
        Me.lblSmtpServer.Name = "lblSmtpServer"
        Me.lblSmtpServer.Size = New System.Drawing.Size(51, 13)
        Me.lblSmtpServer.TabIndex = 38
        Me.lblSmtpServer.Text = "Servidor:"
        Me.lblSmtpServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSmtpServer
        '
        Me.txtSmtpServer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSmtpServer.Location = New System.Drawing.Point(60, 46)
        Me.txtSmtpServer.Name = "txtSmtpServer"
        Me.txtSmtpServer.Size = New System.Drawing.Size(303, 21)
        Me.txtSmtpServer.TabIndex = 39
        Me.txtSmtpServer.Text = ""
        '
        'lblSmtpPort
        '
        Me.lblSmtpPort.AutoSize = True
        Me.lblSmtpPort.BackColor = System.Drawing.Color.Transparent
        Me.lblSmtpPort.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmtpPort.FontSize = 9.75!
        Me.lblSmtpPort.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSmtpPort.Location = New System.Drawing.Point(370, 49)
        Me.lblSmtpPort.Name = "lblSmtpPort"
        Me.lblSmtpPort.Size = New System.Drawing.Size(51, 16)
        Me.lblSmtpPort.TabIndex = 40
        Me.lblSmtpPort.Text = "Puerto"
        Me.lblSmtpPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSmtpPort
        '
        Me.txtSmtpPort.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSmtpPort.Location = New System.Drawing.Point(419, 46)
        Me.txtSmtpPort.Name = "txtSmtpPort"
        Me.txtSmtpPort.Size = New System.Drawing.Size(56, 21)
        Me.txtSmtpPort.TabIndex = 41
        Me.txtSmtpPort.Text = ""
        '
        'lblSmtpUser
        '
        Me.lblSmtpUser.BackColor = System.Drawing.Color.Transparent
        Me.lblSmtpUser.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmtpUser.FontSize = 9.75!
        Me.lblSmtpUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSmtpUser.Location = New System.Drawing.Point(3, 76)
        Me.lblSmtpUser.Name = "lblSmtpUser"
        Me.lblSmtpUser.Size = New System.Drawing.Size(47, 13)
        Me.lblSmtpUser.TabIndex = 42
        Me.lblSmtpUser.Text = "Usuario:"
        Me.lblSmtpUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSmtpUser
        '
        Me.txtSmtpUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSmtpUser.Location = New System.Drawing.Point(60, 73)
        Me.txtSmtpUser.Name = "txtSmtpUser"
        Me.txtSmtpUser.Size = New System.Drawing.Size(187, 21)
        Me.txtSmtpUser.TabIndex = 43
        Me.txtSmtpUser.Text = ""
        '
        'lblSmtpPassword
        '
        Me.lblSmtpPassword.BackColor = System.Drawing.Color.Transparent
        Me.lblSmtpPassword.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmtpPassword.FontSize = 9.75!
        Me.lblSmtpPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSmtpPassword.Location = New System.Drawing.Point(254, 76)
        Me.lblSmtpPassword.Name = "lblSmtpPassword"
        Me.lblSmtpPassword.Size = New System.Drawing.Size(38, 13)
        Me.lblSmtpPassword.TabIndex = 44
        Me.lblSmtpPassword.Text = "Clave:"
        Me.lblSmtpPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSmtpPass
        '
        Me.txtSmtpPass.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSmtpPass.Location = New System.Drawing.Point(298, 73)
        Me.txtSmtpPass.Name = "txtSmtpPass"
        Me.txtSmtpPass.Size = New System.Drawing.Size(177, 21)
        Me.txtSmtpPass.TabIndex = 45
        Me.txtSmtpPass.Text = ""
        '
        'lblSmtpMail
        '
        Me.lblSmtpMail.BackColor = System.Drawing.Color.Transparent
        Me.lblSmtpMail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmtpMail.FontSize = 9.75!
        Me.lblSmtpMail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSmtpMail.Location = New System.Drawing.Point(3, 103)
        Me.lblSmtpMail.Name = "lblSmtpMail"
        Me.lblSmtpMail.Size = New System.Drawing.Size(35, 13)
        Me.lblSmtpMail.TabIndex = 46
        Me.lblSmtpMail.Text = "Email:"
        Me.lblSmtpMail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSmtpMail
        '
        Me.txtSmtpMail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSmtpMail.Location = New System.Drawing.Point(60, 100)
        Me.txtSmtpMail.Name = "txtSmtpMail"
        Me.txtSmtpMail.Size = New System.Drawing.Size(415, 21)
        Me.txtSmtpMail.TabIndex = 47
        Me.txtSmtpMail.Text = ""
        '
        'lblexecuteRule
        '
        Me.lblexecuteRule.AutoSize = True
        Me.lblexecuteRule.BackColor = System.Drawing.Color.Transparent
        Me.lblexecuteRule.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblexecuteRule.FontSize = 9.75!
        Me.lblexecuteRule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblexecuteRule.Location = New System.Drawing.Point(10, 14)
        Me.lblexecuteRule.Name = "lblexecuteRule"
        Me.lblexecuteRule.Size = New System.Drawing.Size(334, 16)
        Me.lblexecuteRule.TabIndex = 52
        Me.lblexecuteRule.Text = "Ejecutar Regla desde formulario de envio de mail:"
        Me.lblexecuteRule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CboDoMailRule
        '
        Me.CboDoMailRule.FormattingEnabled = True
        Me.CboDoMailRule.Location = New System.Drawing.Point(10, 30)
        Me.CboDoMailRule.Name = "CboDoMailRule"
        Me.CboDoMailRule.Size = New System.Drawing.Size(453, 24)
        Me.CboDoMailRule.TabIndex = 53
        '
        'txtbtnName
        '
        Me.txtbtnName.Location = New System.Drawing.Point(213, 94)
        Me.txtbtnName.Name = "txtbtnName"
        Me.txtbtnName.Size = New System.Drawing.Size(254, 37)
        Me.txtbtnName.TabIndex = 54
        Me.txtbtnName.Text = ""
        '
        'lblBtnName
        '
        Me.lblBtnName.AutoSize = True
        Me.lblBtnName.BackColor = System.Drawing.Color.Transparent
        Me.lblBtnName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBtnName.FontSize = 9.75!
        Me.lblBtnName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblBtnName.Location = New System.Drawing.Point(6, 97)
        Me.lblBtnName.Name = "lblBtnName"
        Me.lblBtnName.Size = New System.Drawing.Size(277, 16)
        Me.lblBtnName.TabIndex = 55
        Me.lblBtnName.Text = "Nombre del botón de ejecución de regla:"
        Me.lblBtnName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnDoMailRule)
        Me.GroupBox1.Controls.Add(Me.BtnCleanRuleValues)
        Me.GroupBox1.Controls.Add(Me.lblBtnName)
        Me.GroupBox1.Controls.Add(Me.CboDoMailRule)
        Me.GroupBox1.Controls.Add(Me.txtbtnName)
        Me.GroupBox1.Controls.Add(Me.lblexecuteRule)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 88)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(474, 157)
        Me.GroupBox1.TabIndex = 56
        Me.GroupBox1.TabStop = False
        '
        'btnDoMailRule
        '
        Me.btnDoMailRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnDoMailRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDoMailRule.ForeColor = System.Drawing.Color.White
        Me.btnDoMailRule.Location = New System.Drawing.Point(10, 57)
        Me.btnDoMailRule.Name = "btnDoMailRule"
        Me.btnDoMailRule.Size = New System.Drawing.Size(136, 23)
        Me.btnDoMailRule.TabIndex = 57
        Me.btnDoMailRule.Text = "Ir a la regla de destino"
        Me.btnDoMailRule.UseVisualStyleBackColor = True
        '
        'BtnCleanRuleValues
        '
        Me.BtnCleanRuleValues.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnCleanRuleValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCleanRuleValues.ForeColor = System.Drawing.Color.White
        Me.BtnCleanRuleValues.Location = New System.Drawing.Point(10, 126)
        Me.BtnCleanRuleValues.Name = "BtnCleanRuleValues"
        Me.BtnCleanRuleValues.Size = New System.Drawing.Size(86, 21)
        Me.BtnCleanRuleValues.TabIndex = 56
        Me.BtnCleanRuleValues.Text = "Limpiar Valores"
        Me.BtnCleanRuleValues.UseVisualStyleBackColor = True
        '
        'lblvarAttachs
        '
        Me.lblvarAttachs.AutoSize = True
        Me.lblvarAttachs.BackColor = System.Drawing.Color.Transparent
        Me.lblvarAttachs.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvarAttachs.FontSize = 9.75!
        Me.lblvarAttachs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblvarAttachs.Location = New System.Drawing.Point(6, 54)
        Me.lblvarAttachs.Name = "lblvarAttachs"
        Me.lblvarAttachs.Size = New System.Drawing.Size(148, 16)
        Me.lblvarAttachs.TabIndex = 57
        Me.lblvarAttachs.Text = "Variable de adjuntos:"
        Me.lblvarAttachs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVarAttachs
        '
        Me.txtVarAttachs.Location = New System.Drawing.Point(114, 46)
        Me.txtVarAttachs.Name = "txtVarAttachs"
        Me.txtVarAttachs.Size = New System.Drawing.Size(187, 21)
        Me.txtVarAttachs.TabIndex = 58
        Me.txtVarAttachs.Text = ""
        '
        'lblColumnName
        '
        Me.lblColumnName.AutoSize = True
        Me.lblColumnName.BackColor = System.Drawing.Color.Transparent
        Me.lblColumnName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColumnName.FontSize = 9.75!
        Me.lblColumnName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblColumnName.Location = New System.Drawing.Point(6, 62)
        Me.lblColumnName.Name = "lblColumnName"
        Me.lblColumnName.Size = New System.Drawing.Size(175, 16)
        Me.lblColumnName.TabIndex = 59
        Me.lblColumnName.Text = "Numero columna nombre:"
        Me.lblColumnName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblColumnRoute
        '
        Me.lblColumnRoute.AutoSize = True
        Me.lblColumnRoute.BackColor = System.Drawing.Color.Transparent
        Me.lblColumnRoute.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColumnRoute.FontSize = 9.75!
        Me.lblColumnRoute.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblColumnRoute.Location = New System.Drawing.Point(6, 35)
        Me.lblColumnRoute.Name = "lblColumnRoute"
        Me.lblColumnRoute.Size = New System.Drawing.Size(157, 16)
        Me.lblColumnRoute.TabIndex = 60
        Me.lblColumnRoute.Text = "Numero columna Ruta:"
        Me.lblColumnRoute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtColumnRoute
        '
        Me.txtColumnRoute.Location = New System.Drawing.Point(141, 27)
        Me.txtColumnRoute.Name = "txtColumnRoute"
        Me.txtColumnRoute.Size = New System.Drawing.Size(164, 21)
        Me.txtColumnRoute.TabIndex = 61
        Me.txtColumnRoute.Text = ""
        '
        'txtColumnName
        '
        Me.txtColumnName.Location = New System.Drawing.Point(141, 54)
        Me.txtColumnName.Name = "txtColumnName"
        Me.txtColumnName.Size = New System.Drawing.Size(164, 21)
        Me.txtColumnName.TabIndex = 62
        Me.txtColumnName.Text = ""
        '
        'lblConfiguration
        '
        Me.lblConfiguration.AutoSize = True
        Me.lblConfiguration.BackColor = System.Drawing.Color.Transparent
        Me.lblConfiguration.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfiguration.FontSize = 9.75!
        Me.lblConfiguration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblConfiguration.Location = New System.Drawing.Point(6, 19)
        Me.lblConfiguration.Name = "lblConfiguration"
        Me.lblConfiguration.Size = New System.Drawing.Size(564, 16)
        Me.lblConfiguration.TabIndex = 63
        Me.lblConfiguration.Text = "Configuracion de Adjuntos por ejecucion de regla desde formulario de envio de mai" &
    "l:"
        Me.lblConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDatasetConfig
        '
        Me.lblDatasetConfig.AutoSize = True
        Me.lblDatasetConfig.BackColor = System.Drawing.Color.Transparent
        Me.lblDatasetConfig.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDatasetConfig.FontSize = 9.75!
        Me.lblDatasetConfig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblDatasetConfig.Location = New System.Drawing.Point(7, 46)
        Me.lblDatasetConfig.Name = "lblDatasetConfig"
        Me.lblDatasetConfig.Size = New System.Drawing.Size(0, 16)
        Me.lblDatasetConfig.TabIndex = 64
        Me.lblDatasetConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtColumnName)
        Me.GroupBox2.Controls.Add(Me.txtColumnRoute)
        Me.GroupBox2.Controls.Add(Me.lblColumnRoute)
        Me.GroupBox2.Controls.Add(Me.lblColumnName)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 262)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(320, 85)
        Me.GroupBox2.TabIndex = 65
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Si el resultado de la consulta es de tipo Dataset/Datatable:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnExecAdditionalRule)
        Me.GroupBox3.Controls.Add(Me.btnCleanAddRule)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.CboExecAdditionalRule)
        Me.GroupBox3.Controls.Add(Me.txtBtnExecuteAdditionalRuleName)
        Me.GroupBox3.Controls.Add(Me.lblAditionalExecution)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 409)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(474, 163)
        Me.GroupBox3.TabIndex = 66
        Me.GroupBox3.TabStop = False
        '
        'btnExecAdditionalRule
        '
        Me.btnExecAdditionalRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnExecAdditionalRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExecAdditionalRule.ForeColor = System.Drawing.Color.White
        Me.btnExecAdditionalRule.Location = New System.Drawing.Point(10, 57)
        Me.btnExecAdditionalRule.Name = "btnExecAdditionalRule"
        Me.btnExecAdditionalRule.Size = New System.Drawing.Size(136, 23)
        Me.btnExecAdditionalRule.TabIndex = 57
        Me.btnExecAdditionalRule.Text = "Ir a la regla de destino"
        Me.btnExecAdditionalRule.UseVisualStyleBackColor = True
        '
        'btnCleanAddRule
        '
        Me.btnCleanAddRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnCleanAddRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCleanAddRule.ForeColor = System.Drawing.Color.White
        Me.btnCleanAddRule.Location = New System.Drawing.Point(6, 134)
        Me.btnCleanAddRule.Name = "btnCleanAddRule"
        Me.btnCleanAddRule.Size = New System.Drawing.Size(86, 21)
        Me.btnCleanAddRule.TabIndex = 56
        Me.btnCleanAddRule.Text = "Limpiar Valores"
        Me.btnCleanAddRule.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.FontSize = 9.75!
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(2, 105)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(277, 16)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "Nombre del botón de ejecución de regla:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CboExecAdditionalRule
        '
        Me.CboExecAdditionalRule.FormattingEnabled = True
        Me.CboExecAdditionalRule.Location = New System.Drawing.Point(10, 30)
        Me.CboExecAdditionalRule.Name = "CboExecAdditionalRule"
        Me.CboExecAdditionalRule.Size = New System.Drawing.Size(453, 24)
        Me.CboExecAdditionalRule.TabIndex = 53
        '
        'txtBtnExecuteAdditionalRuleName
        '
        Me.txtBtnExecuteAdditionalRuleName.Location = New System.Drawing.Point(209, 102)
        Me.txtBtnExecuteAdditionalRuleName.Name = "txtBtnExecuteAdditionalRuleName"
        Me.txtBtnExecuteAdditionalRuleName.Size = New System.Drawing.Size(254, 37)
        Me.txtBtnExecuteAdditionalRuleName.TabIndex = 54
        Me.txtBtnExecuteAdditionalRuleName.Text = ""
        '
        'lblAditionalExecution
        '
        Me.lblAditionalExecution.AutoSize = True
        Me.lblAditionalExecution.BackColor = System.Drawing.Color.Transparent
        Me.lblAditionalExecution.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAditionalExecution.FontSize = 9.75!
        Me.lblAditionalExecution.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAditionalExecution.Location = New System.Drawing.Point(10, 17)
        Me.lblAditionalExecution.Name = "lblAditionalExecution"
        Me.lblAditionalExecution.Size = New System.Drawing.Size(422, 16)
        Me.lblAditionalExecution.TabIndex = 52
        Me.lblAditionalExecution.Text = "Ejecutar una regla adicional desde formulario de envio de mail:"
        Me.lblAditionalExecution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAditionalExec
        '
        Me.lblAditionalExec.AutoSize = True
        Me.lblAditionalExec.BackColor = System.Drawing.Color.Transparent
        Me.lblAditionalExec.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAditionalExec.FontSize = 9.75!
        Me.lblAditionalExec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAditionalExec.Location = New System.Drawing.Point(11, 393)
        Me.lblAditionalExec.Name = "lblAditionalExec"
        Me.lblAditionalExec.Size = New System.Drawing.Size(160, 16)
        Me.lblAditionalExec.TabIndex = 67
        Me.lblAditionalExec.Text = "Configuracion Adicional"
        Me.lblAditionalExec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TbDoMail
        '
        Me.TbDoMail.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TbDoMail.Controls.Add(Me.tabMail)
        Me.TbDoMail.Controls.Add(Me.tabOptions)
        Me.TbDoMail.Controls.Add(Me.tabAttachments)
        Me.TbDoMail.Controls.Add(Me.tabSMTP)
        Me.TbDoMail.Controls.Add(Me.tabExecuteRule)
        Me.TbDoMail.Controls.Add(Me.tabImages)
        Me.TbDoMail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TbDoMail.Location = New System.Drawing.Point(3, 3)
        Me.TbDoMail.Name = "TbDoMail"
        Me.TbDoMail.SelectedIndex = 0
        Me.TbDoMail.Size = New System.Drawing.Size(522, 456)
        Me.TbDoMail.TabIndex = 70
        '
        'tabMail
        '
        Me.tabMail.Controls.Add(Me.lblFor)
        Me.tabMail.Controls.Add(Me.lblCC)
        Me.tabMail.Controls.Add(Me.lblCCO)
        Me.tabMail.Controls.Add(Me.Label3)
        Me.tabMail.Controls.Add(Me.Label5)
        Me.tabMail.Controls.Add(Me.txtPara)
        Me.tabMail.Controls.Add(Me.txtCC)
        Me.tabMail.Controls.Add(Me.txtCCO)
        Me.tabMail.Controls.Add(Me.txtAsunto)
        Me.tabMail.Controls.Add(Me.txtBody)
        Me.tabMail.Location = New System.Drawing.Point(4, 28)
        Me.tabMail.Name = "tabMail"
        Me.tabMail.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMail.Size = New System.Drawing.Size(514, 424)
        Me.tabMail.TabIndex = 0
        Me.tabMail.Text = "Correo"
        '
        'lblFor
        '
        Me.lblFor.AutoSize = True
        Me.lblFor.BackColor = System.Drawing.Color.Transparent
        Me.lblFor.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFor.FontSize = 9.75!
        Me.lblFor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblFor.Location = New System.Drawing.Point(3, 8)
        Me.lblFor.Name = "lblFor"
        Me.lblFor.Size = New System.Drawing.Size(37, 16)
        Me.lblFor.TabIndex = 72
        Me.lblFor.Text = "Para"
        Me.lblFor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCC
        '
        Me.lblCC.AutoSize = True
        Me.lblCC.BackColor = System.Drawing.Color.Transparent
        Me.lblCC.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCC.FontSize = 9.75!
        Me.lblCC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblCC.Location = New System.Drawing.Point(3, 35)
        Me.lblCC.Name = "lblCC"
        Me.lblCC.Size = New System.Drawing.Size(26, 16)
        Me.lblCC.TabIndex = 71
        Me.lblCC.Text = "CC"
        Me.lblCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCCO
        '
        Me.lblCCO.AutoSize = True
        Me.lblCCO.BackColor = System.Drawing.Color.Transparent
        Me.lblCCO.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCCO.FontSize = 9.75!
        Me.lblCCO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblCCO.Location = New System.Drawing.Point(3, 62)
        Me.lblCCO.Name = "lblCCO"
        Me.lblCCO.Size = New System.Drawing.Size(36, 16)
        Me.lblCCO.TabIndex = 70
        Me.lblCCO.Text = "CCO"
        Me.lblCCO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabOptions
        '
        Me.tabOptions.Controls.Add(Me.chkViewAssociatedDocuments)
        Me.tabOptions.Controls.Add(Me.chkViewOriginal)
        Me.tabOptions.Controls.Add(Me.chkDisableHistory)
        Me.tabOptions.Controls.Add(Me.txtMailPath)
        Me.tabOptions.Controls.Add(Me.CHKSaveMailPath)
        Me.tabOptions.Controls.Add(Me.chkgroupMailTo)
        Me.tabOptions.Controls.Add(Me.chkAttachLink)
        Me.tabOptions.Controls.Add(Me.ChkAutomatic)
        Me.tabOptions.Location = New System.Drawing.Point(4, 28)
        Me.tabOptions.Name = "tabOptions"
        Me.tabOptions.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOptions.Size = New System.Drawing.Size(514, 424)
        Me.tabOptions.TabIndex = 3
        Me.tabOptions.Text = "Opciones"
        Me.tabOptions.UseVisualStyleBackColor = True
        '
        'chkViewAssociatedDocuments
        '
        Me.chkViewAssociatedDocuments.AutoSize = True
        Me.chkViewAssociatedDocuments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkViewAssociatedDocuments.Location = New System.Drawing.Point(6, 99)
        Me.chkViewAssociatedDocuments.Name = "chkViewAssociatedDocuments"
        Me.chkViewAssociatedDocuments.Size = New System.Drawing.Size(100, 18)
        Me.chkViewAssociatedDocuments.TabIndex = 82
        Me.chkViewAssociatedDocuments.Text = "Ver asociados"
        Me.chkViewAssociatedDocuments.UseVisualStyleBackColor = True
        '
        'chkViewOriginal
        '
        Me.chkViewOriginal.AutoSize = True
        Me.chkViewOriginal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkViewOriginal.Location = New System.Drawing.Point(6, 68)
        Me.chkViewOriginal.Name = "chkViewOriginal"
        Me.chkViewOriginal.Size = New System.Drawing.Size(153, 18)
        Me.chkViewOriginal.TabIndex = 81
        Me.chkViewOriginal.Text = "Ver documento original"
        Me.chkViewOriginal.UseVisualStyleBackColor = True
        '
        'chkDisableHistory
        '
        Me.chkDisableHistory.AutoSize = True
        Me.chkDisableHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDisableHistory.Location = New System.Drawing.Point(6, 161)
        Me.chkDisableHistory.Name = "chkDisableHistory"
        Me.chkDisableHistory.Size = New System.Drawing.Size(173, 18)
        Me.chkDisableHistory.TabIndex = 80
        Me.chkDisableHistory.Text = "Deshabilitar historial de mail"
        Me.chkDisableHistory.UseVisualStyleBackColor = True
        '
        'txtMailPath
        '
        Me.txtMailPath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMailPath.Location = New System.Drawing.Point(216, 190)
        Me.txtMailPath.Name = "txtMailPath"
        Me.txtMailPath.Size = New System.Drawing.Size(292, 21)
        Me.txtMailPath.TabIndex = 79
        Me.txtMailPath.Text = ""
        '
        'CHKSaveMailPath
        '
        Me.CHKSaveMailPath.AutoSize = True
        Me.CHKSaveMailPath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSaveMailPath.Location = New System.Drawing.Point(6, 192)
        Me.CHKSaveMailPath.Name = "CHKSaveMailPath"
        Me.CHKSaveMailPath.Size = New System.Drawing.Size(204, 18)
        Me.CHKSaveMailPath.TabIndex = 78
        Me.CHKSaveMailPath.Text = "Guardar ruta del mail en variable:"
        Me.CHKSaveMailPath.UseVisualStyleBackColor = True
        '
        'chkgroupMailTo
        '
        Me.chkgroupMailTo.AutoSize = True
        Me.chkgroupMailTo.BackColor = System.Drawing.Color.Transparent
        Me.chkgroupMailTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkgroupMailTo.Location = New System.Drawing.Point(6, 130)
        Me.chkgroupMailTo.Name = "chkgroupMailTo"
        Me.chkgroupMailTo.Size = New System.Drawing.Size(159, 18)
        Me.chkgroupMailTo.TabIndex = 72
        Me.chkgroupMailTo.Text = "Agrupar por Destinatario"
        Me.chkgroupMailTo.UseVisualStyleBackColor = False
        '
        'chkAttachLink
        '
        Me.chkAttachLink.AutoSize = True
        Me.chkAttachLink.BackColor = System.Drawing.Color.Transparent
        Me.chkAttachLink.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAttachLink.Location = New System.Drawing.Point(6, 6)
        Me.chkAttachLink.Name = "chkAttachLink"
        Me.chkAttachLink.Size = New System.Drawing.Size(94, 18)
        Me.chkAttachLink.TabIndex = 73
        Me.chkAttachLink.Text = "Agregar Link"
        Me.chkAttachLink.UseVisualStyleBackColor = False
        '
        'ChkAutomatic
        '
        Me.ChkAutomatic.AutoSize = True
        Me.ChkAutomatic.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAutomatic.Location = New System.Drawing.Point(6, 37)
        Me.ChkAutomatic.Name = "ChkAutomatic"
        Me.ChkAutomatic.Size = New System.Drawing.Size(120, 18)
        Me.ChkAutomatic.TabIndex = 74
        Me.ChkAutomatic.Text = "Envio automatico"
        Me.ChkAutomatic.UseVisualStyleBackColor = True
        '
        'tabAttachments
        '
        Me.tabAttachments.Controls.Add(Me.lblAttachTableVar)
        Me.tabAttachments.Controls.Add(Me.lblAttachTableDocTypeId)
        Me.tabAttachments.Controls.Add(Me.lblAttachTableDocId)
        Me.tabAttachments.Controls.Add(Me.lblAttachTableDocName)
        Me.tabAttachments.Controls.Add(Me.txtAttachTableVar)
        Me.tabAttachments.Controls.Add(Me.txtAttachTableDocTypeId)
        Me.tabAttachments.Controls.Add(Me.txtAttachTableDocId)
        Me.tabAttachments.Controls.Add(Me.txtAttachTableDocName)
        Me.tabAttachments.Controls.Add(Me.chkAttachAssociatedDocuments)
        Me.tabAttachments.Controls.Add(Me.BtnConfDocAsoc)
        Me.tabAttachments.Controls.Add(Me.chkAttach)
        Me.tabAttachments.Controls.Add(Me.chkAtachDirectory)
        Me.tabAttachments.Location = New System.Drawing.Point(4, 28)
        Me.tabAttachments.Name = "tabAttachments"
        Me.tabAttachments.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAttachments.Size = New System.Drawing.Size(514, 424)
        Me.tabAttachments.TabIndex = 5
        Me.tabAttachments.Text = "Adjuntos"
        Me.tabAttachments.UseVisualStyleBackColor = True
        '
        'lblAttachTableVar
        '
        Me.lblAttachTableVar.AutoSize = True
        Me.lblAttachTableVar.BackColor = System.Drawing.Color.Transparent
        Me.lblAttachTableVar.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttachTableVar.FontSize = 9.75!
        Me.lblAttachTableVar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAttachTableVar.Location = New System.Drawing.Point(6, 116)
        Me.lblAttachTableVar.Name = "lblAttachTableVar"
        Me.lblAttachTableVar.Size = New System.Drawing.Size(219, 16)
        Me.lblAttachTableVar.TabIndex = 88
        Me.lblAttachTableVar.Text = "Variable con Tabla de Asociados"
        Me.lblAttachTableVar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAttachTableDocTypeId
        '
        Me.lblAttachTableDocTypeId.AutoSize = True
        Me.lblAttachTableDocTypeId.BackColor = System.Drawing.Color.Transparent
        Me.lblAttachTableDocTypeId.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttachTableDocTypeId.FontSize = 9.75!
        Me.lblAttachTableDocTypeId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAttachTableDocTypeId.Location = New System.Drawing.Point(6, 143)
        Me.lblAttachTableDocTypeId.Name = "lblAttachTableDocTypeId"
        Me.lblAttachTableDocTypeId.Size = New System.Drawing.Size(218, 16)
        Me.lblAttachTableDocTypeId.TabIndex = 87
        Me.lblAttachTableDocTypeId.Text = "Columna con el ID de la Entidad"
        Me.lblAttachTableDocTypeId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAttachTableDocId
        '
        Me.lblAttachTableDocId.AutoSize = True
        Me.lblAttachTableDocId.BackColor = System.Drawing.Color.Transparent
        Me.lblAttachTableDocId.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttachTableDocId.FontSize = 9.75!
        Me.lblAttachTableDocId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAttachTableDocId.Location = New System.Drawing.Point(6, 170)
        Me.lblAttachTableDocId.Name = "lblAttachTableDocId"
        Me.lblAttachTableDocId.Size = New System.Drawing.Size(230, 16)
        Me.lblAttachTableDocId.TabIndex = 86
        Me.lblAttachTableDocId.Text = "Columna con el ID del Documento"
        Me.lblAttachTableDocId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAttachTableDocName
        '
        Me.lblAttachTableDocName.AutoSize = True
        Me.lblAttachTableDocName.BackColor = System.Drawing.Color.Transparent
        Me.lblAttachTableDocName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttachTableDocName.FontSize = 9.75!
        Me.lblAttachTableDocName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAttachTableDocName.Location = New System.Drawing.Point(6, 197)
        Me.lblAttachTableDocName.Name = "lblAttachTableDocName"
        Me.lblAttachTableDocName.Size = New System.Drawing.Size(242, 16)
        Me.lblAttachTableDocName.TabIndex = 81
        Me.lblAttachTableDocName.Text = "Columna con el Nombre del Adjunto"
        Me.lblAttachTableDocName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAttachTableVar
        '
        Me.txtAttachTableVar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAttachTableVar.Location = New System.Drawing.Point(222, 114)
        Me.txtAttachTableVar.Name = "txtAttachTableVar"
        Me.txtAttachTableVar.Size = New System.Drawing.Size(277, 21)
        Me.txtAttachTableVar.TabIndex = 82
        Me.txtAttachTableVar.Text = ""
        '
        'txtAttachTableDocTypeId
        '
        Me.txtAttachTableDocTypeId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAttachTableDocTypeId.Location = New System.Drawing.Point(222, 141)
        Me.txtAttachTableDocTypeId.Name = "txtAttachTableDocTypeId"
        Me.txtAttachTableDocTypeId.Size = New System.Drawing.Size(277, 21)
        Me.txtAttachTableDocTypeId.TabIndex = 83
        Me.txtAttachTableDocTypeId.Text = ""
        '
        'txtAttachTableDocId
        '
        Me.txtAttachTableDocId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAttachTableDocId.Location = New System.Drawing.Point(242, 168)
        Me.txtAttachTableDocId.Name = "txtAttachTableDocId"
        Me.txtAttachTableDocId.Size = New System.Drawing.Size(257, 21)
        Me.txtAttachTableDocId.TabIndex = 84
        Me.txtAttachTableDocId.Text = ""
        '
        'txtAttachTableDocName
        '
        Me.txtAttachTableDocName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAttachTableDocName.Location = New System.Drawing.Point(254, 195)
        Me.txtAttachTableDocName.Name = "txtAttachTableDocName"
        Me.txtAttachTableDocName.Size = New System.Drawing.Size(245, 21)
        Me.txtAttachTableDocName.TabIndex = 85
        Me.txtAttachTableDocName.Text = ""
        '
        'chkAttachAssociatedDocuments
        '
        Me.chkAttachAssociatedDocuments.AutoSize = True
        Me.chkAttachAssociatedDocuments.BackColor = System.Drawing.Color.Transparent
        Me.chkAttachAssociatedDocuments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAttachAssociatedDocuments.Location = New System.Drawing.Point(6, 68)
        Me.chkAttachAssociatedDocuments.Name = "chkAttachAssociatedDocuments"
        Me.chkAttachAssociatedDocuments.Size = New System.Drawing.Size(203, 18)
        Me.chkAttachAssociatedDocuments.TabIndex = 79
        Me.chkAttachAssociatedDocuments.Text = "Adjuntar Documentos Asociados"
        Me.chkAttachAssociatedDocuments.UseVisualStyleBackColor = False
        '
        'BtnConfDocAsoc
        '
        Me.BtnConfDocAsoc.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnConfDocAsoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnConfDocAsoc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnConfDocAsoc.ForeColor = System.Drawing.Color.White
        Me.BtnConfDocAsoc.Location = New System.Drawing.Point(215, 66)
        Me.BtnConfDocAsoc.Name = "BtnConfDocAsoc"
        Me.BtnConfDocAsoc.Size = New System.Drawing.Size(100, 25)
        Me.BtnConfDocAsoc.TabIndex = 80
        Me.BtnConfDocAsoc.Text = "Configurar"
        Me.BtnConfDocAsoc.UseVisualStyleBackColor = True
        '
        'chkAttach
        '
        Me.chkAttach.AutoSize = True
        Me.chkAttach.BackColor = System.Drawing.Color.Transparent
        Me.chkAttach.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAttach.Location = New System.Drawing.Point(6, 6)
        Me.chkAttach.Name = "chkAttach"
        Me.chkAttach.Size = New System.Drawing.Size(141, 18)
        Me.chkAttach.TabIndex = 77
        Me.chkAttach.Text = "Adjuntar Documento"
        Me.chkAttach.UseVisualStyleBackColor = False
        '
        'chkAtachDirectory
        '
        Me.chkAtachDirectory.AutoSize = True
        Me.chkAtachDirectory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAtachDirectory.Location = New System.Drawing.Point(6, 37)
        Me.chkAtachDirectory.Name = "chkAtachDirectory"
        Me.chkAtachDirectory.Size = New System.Drawing.Size(119, 18)
        Me.chkAtachDirectory.TabIndex = 78
        Me.chkAtachDirectory.Text = "Adjuntar Carpeta"
        Me.chkAtachDirectory.UseVisualStyleBackColor = True
        '
        'tabSMTP
        '
        Me.tabSMTP.Controls.Add(Me.chkEnableSsl)
        Me.tabSMTP.Controls.Add(Me.chkUseSMTPConf)
        Me.tabSMTP.Controls.Add(Me.lblSmtpServer)
        Me.tabSMTP.Controls.Add(Me.txtSmtpServer)
        Me.tabSMTP.Controls.Add(Me.lblSmtpPort)
        Me.tabSMTP.Controls.Add(Me.txtSmtpPort)
        Me.tabSMTP.Controls.Add(Me.lblSmtpUser)
        Me.tabSMTP.Controls.Add(Me.txtSmtpUser)
        Me.tabSMTP.Controls.Add(Me.lblSmtpPassword)
        Me.tabSMTP.Controls.Add(Me.txtSmtpPass)
        Me.tabSMTP.Controls.Add(Me.lblSmtpMail)
        Me.tabSMTP.Controls.Add(Me.txtSmtpMail)
        Me.tabSMTP.Location = New System.Drawing.Point(4, 28)
        Me.tabSMTP.Name = "tabSMTP"
        Me.tabSMTP.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSMTP.Size = New System.Drawing.Size(514, 424)
        Me.tabSMTP.TabIndex = 1
        Me.tabSMTP.Text = "SMTP"
        '
        'chkEnableSsl
        '
        Me.chkEnableSsl.AutoSize = True
        Me.chkEnableSsl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnableSsl.Location = New System.Drawing.Point(60, 127)
        Me.chkEnableSsl.Name = "chkEnableSsl"
        Me.chkEnableSsl.Size = New System.Drawing.Size(92, 18)
        Me.chkEnableSsl.TabIndex = 49
        Me.chkEnableSsl.Text = "Habilitar SSL"
        Me.chkEnableSsl.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chkEnableSsl.UseVisualStyleBackColor = True
        '
        'chkUseSMTPConf
        '
        Me.chkUseSMTPConf.AutoSize = True
        Me.chkUseSMTPConf.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseSMTPConf.Location = New System.Drawing.Point(6, 6)
        Me.chkUseSMTPConf.Name = "chkUseSMTPConf"
        Me.chkUseSMTPConf.Size = New System.Drawing.Size(273, 18)
        Me.chkUseSMTPConf.TabIndex = 48
        Me.chkUseSMTPConf.Text = "Utilizar configuracion SMTP para envio de mail"
        Me.chkUseSMTPConf.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chkUseSMTPConf.UseVisualStyleBackColor = True
        '
        'tabExecuteRule
        '
        Me.tabExecuteRule.AutoScroll = True
        Me.tabExecuteRule.Controls.Add(Me.GroupBox4)
        Me.tabExecuteRule.Controls.Add(Me.lblConfiguration)
        Me.tabExecuteRule.Controls.Add(Me.lblAditionalExec)
        Me.tabExecuteRule.Controls.Add(Me.GroupBox1)
        Me.tabExecuteRule.Controls.Add(Me.GroupBox3)
        Me.tabExecuteRule.Controls.Add(Me.lblvarAttachs)
        Me.tabExecuteRule.Controls.Add(Me.GroupBox2)
        Me.tabExecuteRule.Controls.Add(Me.txtVarAttachs)
        Me.tabExecuteRule.Controls.Add(Me.lblDatasetConfig)
        Me.tabExecuteRule.Location = New System.Drawing.Point(4, 28)
        Me.tabExecuteRule.Name = "tabExecuteRule"
        Me.tabExecuteRule.Padding = New System.Windows.Forms.Padding(3)
        Me.tabExecuteRule.Size = New System.Drawing.Size(514, 424)
        Me.tabExecuteRule.TabIndex = 2
        Me.tabExecuteRule.Text = "Ejecución de Reglas"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtAdditionalRuleColumnName)
        Me.GroupBox4.Controls.Add(Me.txtAdditionalRuleColumnRoute)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Location = New System.Drawing.Point(5, 589)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(320, 85)
        Me.GroupBox4.TabIndex = 68
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Si el resultado de la consulta es de tipo Dataset/Datatable:"
        '
        'txtAdditionalRuleColumnName
        '
        Me.txtAdditionalRuleColumnName.Location = New System.Drawing.Point(141, 54)
        Me.txtAdditionalRuleColumnName.Name = "txtAdditionalRuleColumnName"
        Me.txtAdditionalRuleColumnName.Size = New System.Drawing.Size(164, 21)
        Me.txtAdditionalRuleColumnName.TabIndex = 62
        Me.txtAdditionalRuleColumnName.Text = ""
        '
        'txtAdditionalRuleColumnRoute
        '
        Me.txtAdditionalRuleColumnRoute.Location = New System.Drawing.Point(141, 27)
        Me.txtAdditionalRuleColumnRoute.Name = "txtAdditionalRuleColumnRoute"
        Me.txtAdditionalRuleColumnRoute.Size = New System.Drawing.Size(164, 21)
        Me.txtAdditionalRuleColumnRoute.TabIndex = 61
        Me.txtAdditionalRuleColumnRoute.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.FontSize = 9.75!
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(6, 35)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(157, 16)
        Me.Label7.TabIndex = 60
        Me.Label7.Text = "Numero columna Ruta:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.FontSize = 9.75!
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(6, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(175, 16)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "Numero columna nombre:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabImages
        '
        Me.tabImages.Controls.Add(Me.chkEmbedImages)
        Me.tabImages.Controls.Add(Me.lstImages)
        Me.tabImages.Controls.Add(Me.btnOpenImage)
        Me.tabImages.Controls.Add(Me.btnRemoveImage)
        Me.tabImages.Location = New System.Drawing.Point(4, 28)
        Me.tabImages.Name = "tabImages"
        Me.tabImages.Padding = New System.Windows.Forms.Padding(3)
        Me.tabImages.Size = New System.Drawing.Size(514, 424)
        Me.tabImages.TabIndex = 4
        Me.tabImages.Text = "Imágenes"
        Me.tabImages.UseVisualStyleBackColor = True
        '
        'chkEmbedImages
        '
        Me.chkEmbedImages.AutoSize = True
        Me.chkEmbedImages.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEmbedImages.Location = New System.Drawing.Point(6, 6)
        Me.chkEmbedImages.Name = "chkEmbedImages"
        Me.chkEmbedImages.Size = New System.Drawing.Size(282, 18)
        Me.chkEmbedImages.TabIndex = 91
        Me.chkEmbedImages.Text = "Embeber las imagenes del HTML en el mensaje"
        Me.chkEmbedImages.UseVisualStyleBackColor = True
        '
        'lstImages
        '
        Me.lstImages.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lstImages.FormattingEnabled = True
        Me.lstImages.ItemHeight = 16
        Me.lstImages.Location = New System.Drawing.Point(3, 81)
        Me.lstImages.Name = "lstImages"
        Me.lstImages.Size = New System.Drawing.Size(508, 340)
        Me.lstImages.TabIndex = 87
        '
        'btnOpenImage
        '
        Me.btnOpenImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnOpenImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenImage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenImage.ForeColor = System.Drawing.Color.White
        Me.btnOpenImage.Location = New System.Drawing.Point(6, 40)
        Me.btnOpenImage.Name = "btnOpenImage"
        Me.btnOpenImage.Size = New System.Drawing.Size(100, 25)
        Me.btnOpenImage.TabIndex = 88
        Me.btnOpenImage.Text = "Buscar"
        Me.btnOpenImage.UseVisualStyleBackColor = True
        '
        'btnRemoveImage
        '
        Me.btnRemoveImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnRemoveImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveImage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveImage.ForeColor = System.Drawing.Color.White
        Me.btnRemoveImage.Location = New System.Drawing.Point(112, 40)
        Me.btnRemoveImage.Name = "btnRemoveImage"
        Me.btnRemoveImage.Size = New System.Drawing.Size(100, 25)
        Me.btnRemoveImage.TabIndex = 90
        Me.btnRemoveImage.Text = "Remover"
        Me.btnRemoveImage.UseVisualStyleBackColor = True
        '
        'lblSaveMessage
        '
        Me.lblSaveMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSaveMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblSaveMessage.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaveMessage.FontSize = 9.75!
        Me.lblSaveMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSaveMessage.Location = New System.Drawing.Point(160, 4)
        Me.lblSaveMessage.Name = "lblSaveMessage"
        Me.lblSaveMessage.Size = New System.Drawing.Size(358, 30)
        Me.lblSaveMessage.TabIndex = 71
        Me.lblSaveMessage.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.lblSaveMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnAceptar)
        Me.Panel1.Controls.Add(Me.lblSaveMessage)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(3, 459)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(522, 37)
        Me.Panel1.TabIndex = 72
        '
        'UCDoMail
        '
        Me.Name = "UCDoMail"
        Me.Size = New System.Drawing.Size(536, 528)
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TbDoMail.ResumeLayout(False)
        Me.tabMail.ResumeLayout(False)
        Me.tabMail.PerformLayout()
        Me.tabOptions.ResumeLayout(False)
        Me.tabOptions.PerformLayout()
        Me.tabAttachments.ResumeLayout(False)
        Me.tabAttachments.PerformLayout()
        Me.tabSMTP.ResumeLayout(False)
        Me.tabSMTP.PerformLayout()
        Me.tabExecuteRule.ResumeLayout(False)
        Me.tabExecuteRule.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.tabImages.ResumeLayout(False)
        Me.tabImages.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

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