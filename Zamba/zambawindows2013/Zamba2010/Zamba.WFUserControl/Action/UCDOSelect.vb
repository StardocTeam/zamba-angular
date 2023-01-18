'Imports Zamba.WFBusiness




Public Class UCDOSelect
    Inherits ZRuleControl


#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents txtScriptSql As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnWizard As ZButton
    Friend WithEvents BtnSave As ZButton
    Friend WithEvents lblServerType As System.Windows.Forms.LinkLabel
    Friend WithEvents lblServerName As System.Windows.Forms.LinkLabel
    Friend WithEvents lblDatabase As System.Windows.Forms.LinkLabel
    Friend WithEvents lblUser As System.Windows.Forms.LinkLabel
    Friend WithEvents lblPassword As System.Windows.Forms.LinkLabel
    Friend WithEvents cboServerType As ComboBox
    Friend WithEvents txtServerName As TextBox
    Friend WithEvents txtDatabase As TextBox
    Friend WithEvents chkUseAppIni As System.Windows.Forms.CheckBox
    Friend WithEvents txtUser As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtDescripcionConsulta As TextBox
    Friend WithEvents Label2 As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents optEscalar As System.Windows.Forms.RadioButton
    Friend WithEvents optNonQuery As System.Windows.Forms.RadioButton
    Friend WithEvents optDataset As System.Windows.Forms.RadioButton
    Friend WithEvents txtNombreConsulta As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents cmbODBC As ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents lstautomaticvariables As ListBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnTest As ZButton

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        txtScriptSql = New Zamba.AppBlock.TextoInteligenteTextBox()
        btnWizard = New ZButton()
        BtnSave = New ZButton()
        lblServerType = New System.Windows.Forms.LinkLabel()
        lblServerName = New System.Windows.Forms.LinkLabel()
        lblDatabase = New System.Windows.Forms.LinkLabel()
        lblUser = New System.Windows.Forms.LinkLabel()
        lblPassword = New System.Windows.Forms.LinkLabel()
        cboServerType = New ComboBox()
        txtServerName = New TextBox()
        txtDatabase = New TextBox()
        txtUser = New TextBox()
        txtPassword = New TextBox()
        chkUseAppIni = New System.Windows.Forms.CheckBox()
        btnTest = New ZButton()
        txtDescripcionConsulta = New TextBox()
        Label2 = New ZLabel()
        Label3 = New ZLabel()
        optEscalar = New System.Windows.Forms.RadioButton()
        optNonQuery = New System.Windows.Forms.RadioButton()
        optDataset = New System.Windows.Forms.RadioButton()
        txtNombreConsulta = New Zamba.AppBlock.TextoInteligenteTextBox()
        cmbODBC = New ComboBox()
        TabControl1 = New System.Windows.Forms.TabControl()
        TabPage1 = New System.Windows.Forms.TabPage()
        Panel1 = New System.Windows.Forms.Panel()
        Panel2 = New System.Windows.Forms.Panel()
        lstautomaticvariables = New ListBox()
        Label5 = New ZLabel()
        TabPage2 = New System.Windows.Forms.TabPage()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        TabPage2.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(TabControl1)
        tbRule.Size = New System.Drawing.Size(624, 500)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(632, 529)
        '
        'txtScriptSql
        '
        txtScriptSql.BackColor = System.Drawing.Color.White
        txtScriptSql.Dock = System.Windows.Forms.DockStyle.Fill
        txtScriptSql.ForeColor = System.Drawing.Color.Black
        txtScriptSql.Location = New System.Drawing.Point(3, 19)
        txtScriptSql.MaxLength = 4000
        txtScriptSql.Name = "txtScriptSql"
        txtScriptSql.Size = New System.Drawing.Size(604, 276)
        txtScriptSql.TabIndex = 0
        txtScriptSql.Text = ""
        '
        'btnWizard
        '
        btnWizard.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnWizard.FlatStyle = FlatStyle.Flat
        btnWizard.ForeColor = System.Drawing.Color.White
        btnWizard.Location = New System.Drawing.Point(89, 130)
        btnWizard.Name = "btnWizard"
        btnWizard.Size = New System.Drawing.Size(80, 27)
        btnWizard.TabIndex = 2
        btnWizard.Text = "Test"
        btnWizard.UseVisualStyleBackColor = False
        '
        'BtnSave
        '
        BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        BtnSave.FlatStyle = FlatStyle.Flat
        BtnSave.ForeColor = System.Drawing.Color.White
        BtnSave.Location = New System.Drawing.Point(270, 130)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New System.Drawing.Size(80, 27)
        BtnSave.TabIndex = 3
        BtnSave.Text = "Guardar"
        BtnSave.UseVisualStyleBackColor = False
        '
        'lblServerType
        '
        lblServerType.AutoSize = True
        lblServerType.BackColor = System.Drawing.Color.Transparent
        lblServerType.Location = New System.Drawing.Point(32, 49)
        lblServerType.Name = "lblServerType"
        lblServerType.Size = New System.Drawing.Size(88, 16)
        lblServerType.TabIndex = 6
        lblServerType.TabStop = True
        lblServerType.Text = "Server Type"
        '
        'lblServerName
        '
        lblServerName.AutoSize = True
        lblServerName.BackColor = System.Drawing.Color.Transparent
        lblServerName.Location = New System.Drawing.Point(32, 73)
        lblServerName.Name = "lblServerName"
        lblServerName.Size = New System.Drawing.Size(92, 16)
        lblServerName.TabIndex = 7
        lblServerName.TabStop = True
        lblServerName.Text = "Server Name"
        '
        'lblDatabase
        '
        lblDatabase.AutoSize = True
        lblDatabase.BackColor = System.Drawing.Color.Transparent
        lblDatabase.Location = New System.Drawing.Point(32, 99)
        lblDatabase.Name = "lblDatabase"
        lblDatabase.Size = New System.Drawing.Size(70, 16)
        lblDatabase.TabIndex = 8
        lblDatabase.TabStop = True
        lblDatabase.Text = "Database"
        '
        'lblUser
        '
        lblUser.AutoSize = True
        lblUser.BackColor = System.Drawing.Color.Transparent
        lblUser.Location = New System.Drawing.Point(32, 126)
        lblUser.Name = "lblUser"
        lblUser.Size = New System.Drawing.Size(37, 16)
        lblUser.TabIndex = 9
        lblUser.TabStop = True
        lblUser.Text = "User"
        '
        'lblPassword
        '
        lblPassword.AutoSize = True
        lblPassword.BackColor = System.Drawing.Color.Transparent
        lblPassword.Location = New System.Drawing.Point(32, 151)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New System.Drawing.Size(70, 16)
        lblPassword.TabIndex = 10
        lblPassword.TabStop = True
        lblPassword.Text = "Password"
        '
        'cboServerType
        '
        cboServerType.FormattingEnabled = True
        cboServerType.Location = New System.Drawing.Point(138, 43)
        cboServerType.Name = "cboServerType"
        cboServerType.Size = New System.Drawing.Size(265, 24)
        cboServerType.TabIndex = 11
        '
        'txtServerName
        '
        txtServerName.Location = New System.Drawing.Point(138, 71)
        txtServerName.Name = "txtServerName"
        txtServerName.Size = New System.Drawing.Size(265, 23)
        txtServerName.TabIndex = 12
        '
        'txtDatabase
        '
        txtDatabase.Location = New System.Drawing.Point(138, 97)
        txtDatabase.Name = "txtDatabase"
        txtDatabase.Size = New System.Drawing.Size(265, 23)
        txtDatabase.TabIndex = 13
        '
        'txtUser
        '
        txtUser.Location = New System.Drawing.Point(138, 124)
        txtUser.Name = "txtUser"
        txtUser.Size = New System.Drawing.Size(140, 23)
        txtUser.TabIndex = 14
        '
        'txtPassword
        '
        txtPassword.Location = New System.Drawing.Point(138, 151)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(64)
        txtPassword.Size = New System.Drawing.Size(140, 23)
        txtPassword.TabIndex = 15
        '
        'chkUseAppIni
        '
        chkUseAppIni.BackColor = System.Drawing.Color.Transparent
        chkUseAppIni.Location = New System.Drawing.Point(16, 20)
        chkUseAppIni.Name = "chkUseAppIni"
        chkUseAppIni.Size = New System.Drawing.Size(167, 20)
        chkUseAppIni.TabIndex = 3
        chkUseAppIni.Text = "Usar Configuracion Existente"
        chkUseAppIni.UseVisualStyleBackColor = False
        '
        'btnTest
        '
        btnTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnTest.FlatStyle = FlatStyle.Flat
        btnTest.ForeColor = System.Drawing.Color.White
        btnTest.Location = New System.Drawing.Point(170, 199)
        btnTest.Name = "btnTest"
        btnTest.Size = New System.Drawing.Size(78, 31)
        btnTest.TabIndex = 16
        btnTest.Text = "Test"
        btnTest.UseVisualStyleBackColor = False
        '
        'txtDescripcionConsulta
        '
        txtDescripcionConsulta.Location = New System.Drawing.Point(18, 28)
        txtDescripcionConsulta.Name = "txtDescripcionConsulta"
        txtDescripcionConsulta.Size = New System.Drawing.Size(224, 23)
        txtDescripcionConsulta.TabIndex = 17
        txtDescripcionConsulta.Visible = False
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Dock = System.Windows.Forms.DockStyle.Top
        Label2.Font = New Font("Verdana", 9.75!)
        Label2.FontSize = 9.75!
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(3, 3)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(102, 16)
        Label2.TabIndex = 19
        Label2.Text = "Script de sql :"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!)
        Label3.FontSize = 9.75!
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(12, 80)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(227, 16)
        Label3.TabIndex = 21
        Label3.Text = "Nombre de la variable resultado :"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'optEscalar
        '
        optEscalar.AutoSize = True
        optEscalar.BackColor = System.Drawing.Color.Transparent
        optEscalar.Checked = True
        optEscalar.Location = New System.Drawing.Point(12, 31)
        optEscalar.Name = "optEscalar"
        optEscalar.Size = New System.Drawing.Size(172, 20)
        optEscalar.TabIndex = 22
        optEscalar.TabStop = True
        optEscalar.Text = "Devolver un solo valor"
        optEscalar.UseVisualStyleBackColor = False
        '
        'optNonQuery
        '
        optNonQuery.AutoSize = True
        optNonQuery.BackColor = System.Drawing.Color.Transparent
        optNonQuery.Location = New System.Drawing.Point(12, 8)
        optNonQuery.Name = "optNonQuery"
        optNonQuery.Size = New System.Drawing.Size(157, 20)
        optNonQuery.TabIndex = 23
        optNonQuery.Text = "No Devolver valores"
        optNonQuery.UseVisualStyleBackColor = False
        '
        'optDataset
        '
        optDataset.AutoSize = True
        optDataset.BackColor = System.Drawing.Color.Transparent
        optDataset.Location = New System.Drawing.Point(12, 52)
        optDataset.Name = "optDataset"
        optDataset.Size = New System.Drawing.Size(240, 20)
        optDataset.TabIndex = 23
        optDataset.Text = "Devolver un conjunto de valores"
        optDataset.UseVisualStyleBackColor = False
        '
        'txtNombreConsulta
        '
        txtNombreConsulta.Location = New System.Drawing.Point(15, 99)
        txtNombreConsulta.MaxLength = 4000
        txtNombreConsulta.Name = "txtNombreConsulta"
        txtNombreConsulta.Size = New System.Drawing.Size(335, 21)
        txtNombreConsulta.TabIndex = 24
        txtNombreConsulta.Text = ""
        '
        'cmbODBC
        '
        cmbODBC.FormattingEnabled = True
        cmbODBC.Location = New System.Drawing.Point(138, 70)
        cmbODBC.Name = "cmbODBC"
        cmbODBC.Size = New System.Drawing.Size(265, 24)
        cmbODBC.TabIndex = 25
        cmbODBC.Visible = False
        '
        'TabControl1
        '
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        TabControl1.Location = New System.Drawing.Point(3, 3)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New System.Drawing.Size(618, 494)
        TabControl1.TabIndex = 26
        '
        'TabPage1
        '
        TabPage1.AutoScroll = True
        TabPage1.Controls.Add(txtScriptSql)
        TabPage1.Controls.Add(Panel1)
        TabPage1.Controls.Add(txtDescripcionConsulta)
        TabPage1.Controls.Add(Label2)
        TabPage1.Location = New System.Drawing.Point(4, 25)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New System.Windows.Forms.Padding(3)
        TabPage1.Size = New System.Drawing.Size(610, 465)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Consulta"
        TabPage1.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(optNonQuery)
        Panel1.Controls.Add(btnWizard)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(BtnSave)
        Panel1.Controls.Add(optEscalar)
        Panel1.Controls.Add(txtNombreConsulta)
        Panel1.Controls.Add(optDataset)
        Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel1.Location = New System.Drawing.Point(3, 295)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(604, 167)
        Panel1.TabIndex = 25
        '
        'Panel2
        '
        Panel2.Controls.Add(lstautomaticvariables)
        Panel2.Controls.Add(Label5)
        Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Panel2.Location = New System.Drawing.Point(356, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(248, 167)
        Panel2.TabIndex = 27
        '
        'lstautomaticvariables
        '
        lstautomaticvariables.BackColor = System.Drawing.Color.White
        lstautomaticvariables.BorderStyle = System.Windows.Forms.BorderStyle.None
        lstautomaticvariables.Dock = System.Windows.Forms.DockStyle.Fill
        lstautomaticvariables.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lstautomaticvariables.FormattingEnabled = True
        lstautomaticvariables.ItemHeight = 16
        lstautomaticvariables.Location = New System.Drawing.Point(0, 16)
        lstautomaticvariables.Name = "lstautomaticvariables"
        lstautomaticvariables.Size = New System.Drawing.Size(248, 151)
        lstautomaticvariables.TabIndex = 25
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Dock = System.Windows.Forms.DockStyle.Top
        Label5.Font = New Font("Verdana", 9.75!)
        Label5.FontSize = 9.75!
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label5.Location = New System.Drawing.Point(0, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(153, 16)
        Label5.TabIndex = 26
        Label5.Text = "Variables Automaticas"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'TabPage2
        '
        TabPage2.AutoScroll = True
        TabPage2.Controls.Add(lblServerType)
        TabPage2.Controls.Add(cmbODBC)
        TabPage2.Controls.Add(lblServerName)
        TabPage2.Controls.Add(lblDatabase)
        TabPage2.Controls.Add(lblUser)
        TabPage2.Controls.Add(lblPassword)
        TabPage2.Controls.Add(chkUseAppIni)
        TabPage2.Controls.Add(cboServerType)
        TabPage2.Controls.Add(txtServerName)
        TabPage2.Controls.Add(txtDatabase)
        TabPage2.Controls.Add(txtUser)
        TabPage2.Controls.Add(btnTest)
        TabPage2.Controls.Add(txtPassword)
        TabPage2.Location = New System.Drawing.Point(4, 25)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New System.Windows.Forms.Padding(3)
        TabPage2.Size = New System.Drawing.Size(610, 465)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Conexión"
        TabPage2.UseVisualStyleBackColor = True
        '
        'UCDOSelect
        '
        Name = "UCDOSelect"
        Size = New System.Drawing.Size(632, 529)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDOSELECT
    Public Sub New(ByRef DoSelect As IDOSELECT, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoSelect, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoSelect

        Try
            LoadServersTypes()
            LoadConnectionsParams()
            LoadRulesParams()
            AddAutomaticVariables()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        HasBeenModified = False
    End Sub
#End Region

#Region "Propietary"

#Region "Variables locales"
    'Dim DsDocTypes As New ArrayList
    'Dim DsIndex As DSIndex
    'Dim replaceindex As New ArrayList

#End Region



#Region "Atributos y Documentos"

    Private Sub UCDOSelect_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'Try
        'LoadServersTypes()
        'LoadConnetcionsParams()
        'LoadRulesParams()

        'Catch ex As Exception
        'ZClass.raiseerror(ex)
        'End Try
        HasBeenModified = False
    End Sub
    Private Sub LoadServersTypes()
        cboServerType.DataSource = System.Enum.GetValues(GetType(DBTYPES))
        'Me.cboServerType.Items.AddRange(DirectCast(System.Enum.GetValues(GetType(DBTypes)), Object()))
    End Sub

    Private Sub LoadConnectionsParams()
        Try
            RemoveHandler cboServerType.SelectedIndexChanged, AddressOf cboServerType_SelectedIndexChanged
            cboServerType.SelectedIndex = CurrentRule.Servertype
            AddHandler cboServerType.SelectedIndexChanged, AddressOf cboServerType_SelectedIndexChanged
            If String.IsNullOrEmpty(CurrentRule.Dbname) = False Then
                txtServerName.Text = CurrentRule.Servidor
                txtDatabase.Text = CurrentRule.Dbname
                txtUser.Text = CurrentRule.Dbuser
                txtPassword.Text = CurrentRule.Dbpassword
                If CurrentRule.Servertype = 6 Then
                    cmbODBC.Visible = True
                    GetSystemDataSourceNames()
                    cmbODBC.Text = CurrentRule.Servidor
                Else
                    cmbODBC.Text = String.Empty
                    cmbODBC.Visible = False
                End If
            Else
                chkUseAppIni.Checked = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
    Private Sub LoadRulesParams()
        Try
            txtDescripcionConsulta.Text = CurrentRule.Name
            txtScriptSql.Text = CurrentRule.SQL
            txtScriptSql.ModificarColores()
            txtNombreConsulta.Text = CurrentRule.HashTable
            txtNombreConsulta.ModificarColores()
            If Not IsNothing(CurrentRule.ExecuteType) Then
                If CurrentRule.ExecuteType.ToString = "ESCALAR" Then
                    optEscalar.Checked = True
                ElseIf CurrentRule.ExecuteType.ToString = "DATASET" Then
                    optDataset.Checked = True
                Else
                    optNonQuery.Checked = True
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub


#End Region




    Private Sub ShowQuery(ByVal sql As String)
        txtScriptSql.Text = sql
    End Sub

#End Region


    Private Sub btnTest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTest.Click
        Try
            Dim errorMsg As String = String.Empty
            If ServersBusiness.IsConnectionValid(DirectCast(CurrentRule.Servertype, DBTYPES), txtServerName.Text, txtDatabase.Text, txtUser.Text, txtPassword.Text, errorMsg) Then
                MessageBox.Show("La conexión ha sido correcta")
            Else
                MessageBox.Show("La conexión ha producido un error: " & errorMsg, "Zamba - Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            Dim Ht As String
            Try
                CurrentRule.Name = txtDescripcionConsulta.Text.Trim
                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Name)

                CurrentRule.SQL = txtScriptSql.Text

                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.SQL)
                Ht = txtNombreConsulta.Text
                If Ht = "" Then
                    MessageBox.Show("Debe ingresar un nombre al conjunto de resultados", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                If chkUseAppIni.Checked Then
                    CurrentRule.Servidor = String.Empty
                    CurrentRule.Dbname = String.Empty
                    CurrentRule.Dbuser = String.Empty
                    CurrentRule.Dbpassword = String.Empty
                    CurrentRule.Servertype = 0
                    CurrentRule.HashTable = Ht.Trim
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.HashTable.Trim)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.Servidor)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.Dbname)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.Dbuser)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.Dbpassword)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.Servertype)
                Else
                    If CurrentRule.Servertype = 6 Then
                        CurrentRule.Servidor = cmbODBC.Text
                    Else
                        CurrentRule.Servidor = txtServerName.Text
                    End If
                    CurrentRule.Dbname = txtDatabase.Text
                    CurrentRule.Dbuser = txtUser.Text
                    CurrentRule.Dbpassword = txtPassword.Text
                    CurrentRule.HashTable = Ht.Trim
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.HashTable.Trim)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.Servidor)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.Dbname)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.Dbuser)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.Dbpassword)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.Servertype)
                End If


                If optEscalar.Checked = True Then
                    CurrentRule.ExecuteType = "ESCALAR"
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 8, "ESCALAR")
                ElseIf optDataset.Checked Then
                    CurrentRule.ExecuteType = "DATASET"
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 8, "DATASET")
                Else
                    CurrentRule.ExecuteType = "NONQUERY"
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 8, "NONQUERY")
                End If

                AddAutomaticVariables()
                UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            HasBeenModified = False
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("ERROR DE CONEXION " & ex.ToString, "Zamba - Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cboServerType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        CurrentRule.Servertype = cboServerType.SelectedIndex
        cmbODBC.SelectedIndex = -1
        If CurrentRule.Servertype = 6 Then
            txtServerName.Visible = False
            cmbODBC.Visible = True
            cmbODBC.Items.Clear()
            GetSystemDataSourceNames()
        Else
            cmbODBC.Visible = False
            txtServerName.Visible = True
        End If
    End Sub

    '     <summary>
    ' Gets all System data source names for the local machine.
    ' </summary>
    Public Sub GetSystemDataSourceNames()
        'get system dsn's
        Dim reg As Microsoft.Win32.RegistryKey = (Microsoft.Win32.Registry.LocalMachine).OpenSubKey("Software")
        If Not IsNothing(reg) Then
            reg = reg.OpenSubKey("ODBC")
            If Not IsNothing(reg) Then

                reg = reg.OpenSubKey("ODBC.INI")
                If Not IsNothing(reg) Then

                    reg = reg.OpenSubKey("ODBC Data Sources")
                    If Not IsNothing(reg) Then
                        'Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
                        For Each sName As String In reg.GetValueNames()
                            cmbODBC.Items.Add(sName)
                        Next
                    End If
                    Try
                        reg.Close()
                        ' ignore this exception if we couldn't close
                    Catch
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub chkUseAppIni_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkUseAppIni.CheckedChanged
        If chkUseAppIni.Checked Then
            txtServerName.Enabled = False
            cboServerType.Enabled = False
            cmbODBC.Enabled = False
            txtDatabase.Enabled = False
            txtUser.Enabled = False
            txtPassword.Enabled = False
            btnTest.Visible = False
        Else
            txtServerName.Enabled = True
            cboServerType.Enabled = True
            cmbODBC.Enabled = True
            txtDatabase.Enabled = True
            txtUser.Enabled = True
            txtPassword.Enabled = True
            btnTest.Visible = True
        End If
    End Sub

    Private Sub cmbODBC_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbODBC.SelectedIndexChanged
        txtServerName.Text = cmbODBC.Text
        txtDatabase.Text = cmbODBC.Text
    End Sub

    Private Sub btnWizard_Click(sender As System.Object, e As EventArgs) Handles btnWizard.Click

    End Sub

    Private Sub AddAutomaticVariables()
        Try
            lstautomaticvariables.Items.Clear()
            lstautomaticvariables.Items.Add("zvar(" & CurrentRule.HashTable.Trim & ".Count)")
            'todo: ML Descubrir las columnas devueltas por el select y mostrar las variables automaticas en DS y Scalar
        Catch ex As Exception
        End Try

    End Sub

    Private Sub txtScriptSql_TextChanged(sender As Object, e As EventArgs) Handles txtScriptSql.TextChanged
        Try
            If (txtScriptSql.Text <> CurrentRule.SQL) Then
                HasBeenModified = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub optNonQuery_CheckedChanged(sender As Object, e As EventArgs) Handles optNonQuery.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub optEscalar_CheckedChanged(sender As Object, e As EventArgs) Handles optEscalar.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub optDataset_CheckedChanged(sender As Object, e As EventArgs) Handles optDataset.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub txtNombreConsulta_TextChanged(sender As Object, e As EventArgs) Handles txtNombreConsulta.TextChanged
        HasBeenModified = True
    End Sub
End Class