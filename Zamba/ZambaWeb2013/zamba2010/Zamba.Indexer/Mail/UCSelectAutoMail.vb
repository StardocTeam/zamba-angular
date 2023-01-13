Imports System.Text
Imports System.Net.Mail
Imports Zamba.AppBlock
Imports Zamba.Core
Imports Zamba.Controls
Imports Zamba.Data
Imports System.Collections.Generic
Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class UCSelectAutoMail
    Inherits Windows.Forms.UserControl

#Region " Código generado por el Diseñador de Windows Forms "
    Friend WithEvents lstAutoMails As System.Windows.Forms.ListView
    Friend WithEvents btAceptar As System.Windows.Forms.Button
    Private WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents rbLink As System.Windows.Forms.RadioButton
    Friend WithEvents rbDocumento As System.Windows.Forms.RadioButton
    Friend WithEvents tbProveedor As System.Windows.Forms.TextBox
    Friend WithEvents tbPuerto As System.Windows.Forms.TextBox
    Friend WithEvents tbPassword As System.Windows.Forms.TextBox
    Friend WithEvents tbUsuario As System.Windows.Forms.TextBox
    Friend WithEvents lbProveedor As System.Windows.Forms.Label
    Friend WithEvents lbPort As System.Windows.Forms.Label
    Friend WithEvents lbPassword As System.Windows.Forms.Label
    Friend WithEvents lbUsuario As System.Windows.Forms.Label
    Friend WithEvents lblTituloIndices As System.Windows.Forms.Label
    Friend WithEvents panelIndices As System.Windows.Forms.Panel
    Friend WithEvents btQuitar As System.Windows.Forms.Button
    Friend WithEvents btAgregar As System.Windows.Forms.Button
    Friend WithEvents btNuevoAutomail As System.Windows.Forms.Button
    Friend WithEvents btModificarAutoMail As System.Windows.Forms.Button
    Friend WithEvents lstIndices As System.Windows.Forms.ListBox
    Friend WithEvents gbAutoMail As System.Windows.Forms.GroupBox
    Friend WithEvents gbSMTP As System.Windows.Forms.GroupBox
    Friend WithEvents btTestConfiguracionSMTP As System.Windows.Forms.Button
    Friend WithEvents chkGroup As System.Windows.Forms.CheckBox
    Friend WithEvents chkAddIndexs As System.Windows.Forms.CheckBox

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Overloads Sub InitializeComponent()
        Me.lstAutoMails = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.btAceptar = New System.Windows.Forms.Button
        Me.chkAddIndexs = New System.Windows.Forms.CheckBox
        Me.rbLink = New System.Windows.Forms.RadioButton
        Me.rbDocumento = New System.Windows.Forms.RadioButton
        Me.tbProveedor = New System.Windows.Forms.TextBox
        Me.tbPuerto = New System.Windows.Forms.TextBox
        Me.tbPassword = New System.Windows.Forms.TextBox
        Me.tbUsuario = New System.Windows.Forms.TextBox
        Me.lbProveedor = New System.Windows.Forms.Label
        Me.lbPort = New System.Windows.Forms.Label
        Me.lbPassword = New System.Windows.Forms.Label
        Me.lbUsuario = New System.Windows.Forms.Label
        Me.panelIndices = New System.Windows.Forms.Panel
        Me.lstIndices = New System.Windows.Forms.ListBox
        Me.lblTituloIndices = New System.Windows.Forms.Label
        Me.btQuitar = New System.Windows.Forms.Button
        Me.btAgregar = New System.Windows.Forms.Button
        Me.btNuevoAutomail = New System.Windows.Forms.Button
        Me.gbAutoMail = New System.Windows.Forms.GroupBox
        Me.btModificarAutoMail = New System.Windows.Forms.Button
        Me.gbSMTP = New System.Windows.Forms.GroupBox
        Me.btTestConfiguracionSMTP = New System.Windows.Forms.Button
        Me.chkGroup = New System.Windows.Forms.CheckBox
        Me.panelIndices.SuspendLayout()
        Me.gbAutoMail.SuspendLayout()
        Me.gbSMTP.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstAutoMails
        '
        Me.lstAutoMails.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lstAutoMails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstAutoMails.FullRowSelect = True
        Me.lstAutoMails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstAutoMails.HideSelection = False
        Me.lstAutoMails.Location = New System.Drawing.Point(6, 20)
        Me.lstAutoMails.MultiSelect = False
        Me.lstAutoMails.Name = "lstAutoMails"
        Me.lstAutoMails.Size = New System.Drawing.Size(394, 167)
        Me.lstAutoMails.TabIndex = 0
        Me.lstAutoMails.UseCompatibleStateImageBehavior = False
        Me.lstAutoMails.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = ""
        Me.ColumnHeader1.Width = 200
        '
        'btAceptar
        '
        Me.btAceptar.BackColor = System.Drawing.Color.Transparent
        Me.btAceptar.Location = New System.Drawing.Point(308, 504)
        Me.btAceptar.Name = "btAceptar"
        Me.btAceptar.Size = New System.Drawing.Size(112, 23)
        Me.btAceptar.TabIndex = 11
        Me.btAceptar.Text = "Guardar Configuracion"
        Me.btAceptar.UseVisualStyleBackColor = False
        '
        'chkAddIndexs
        '
        Me.chkAddIndexs.BackColor = System.Drawing.Color.Transparent
        Me.chkAddIndexs.Location = New System.Drawing.Point(327, 222)
        Me.chkAddIndexs.Name = "chkAddIndexs"
        Me.chkAddIndexs.Size = New System.Drawing.Size(73, 24)
        Me.chkAddIndexs.TabIndex = 5
        Me.chkAddIndexs.Text = "Adjuntar Indices"
        Me.chkAddIndexs.UseVisualStyleBackColor = False
        '
        'rbLink
        '
        Me.rbLink.AutoSize = True
        Me.rbLink.BackColor = System.Drawing.Color.Transparent
        Me.rbLink.Location = New System.Drawing.Point(6, 224)
        Me.rbLink.Name = "rbLink"
        Me.rbLink.Size = New System.Drawing.Size(87, 17)
        Me.rbLink.TabIndex = 3
        Me.rbLink.TabStop = True
        Me.rbLink.Text = "Adjuntar Link"
        Me.rbLink.UseVisualStyleBackColor = False
        '
        'rbDocumento
        '
        Me.rbDocumento.AutoSize = True
        Me.rbDocumento.BackColor = System.Drawing.Color.Transparent
        Me.rbDocumento.Location = New System.Drawing.Point(109, 224)
        Me.rbDocumento.Name = "rbDocumento"
        Me.rbDocumento.Size = New System.Drawing.Size(122, 17)
        Me.rbDocumento.TabIndex = 4
        Me.rbDocumento.TabStop = True
        Me.rbDocumento.Text = "Adjuntar Documento"
        Me.rbDocumento.UseVisualStyleBackColor = False
        '
        'tbProveedor
        '
        Me.tbProveedor.Location = New System.Drawing.Point(109, 78)
        Me.tbProveedor.Name = "tbProveedor"
        Me.tbProveedor.Size = New System.Drawing.Size(285, 20)
        Me.tbProveedor.TabIndex = 8
        '
        'tbPuerto
        '
        Me.tbPuerto.Location = New System.Drawing.Point(109, 123)
        Me.tbPuerto.Name = "tbPuerto"
        Me.tbPuerto.Size = New System.Drawing.Size(49, 20)
        Me.tbPuerto.TabIndex = 9
        Me.tbPuerto.Text = "25"
        Me.tbPuerto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPassword
        '
        Me.tbPassword.Location = New System.Drawing.Point(109, 51)
        Me.tbPassword.Name = "tbPassword"
        Me.tbPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbPassword.Size = New System.Drawing.Size(169, 20)
        Me.tbPassword.TabIndex = 7
        '
        'tbUsuario
        '
        Me.tbUsuario.Location = New System.Drawing.Point(109, 23)
        Me.tbUsuario.Name = "tbUsuario"
        Me.tbUsuario.Size = New System.Drawing.Size(285, 20)
        Me.tbUsuario.TabIndex = 6
        '
        'lbProveedor
        '
        Me.lbProveedor.AutoSize = True
        Me.lbProveedor.BackColor = System.Drawing.Color.Transparent
        Me.lbProveedor.Location = New System.Drawing.Point(28, 78)
        Me.lbProveedor.Name = "lbProveedor"
        Me.lbProveedor.Size = New System.Drawing.Size(56, 13)
        Me.lbProveedor.TabIndex = 0
        Me.lbProveedor.Text = "Proveedor"
        '
        'lbPort
        '
        Me.lbPort.AutoSize = True
        Me.lbPort.BackColor = System.Drawing.Color.Transparent
        Me.lbPort.Location = New System.Drawing.Point(45, 105)
        Me.lbPort.Name = "lbPort"
        Me.lbPort.Size = New System.Drawing.Size(38, 13)
        Me.lbPort.TabIndex = 0
        Me.lbPort.Text = "Puerto"
        '
        'lbPassword
        '
        Me.lbPassword.AutoSize = True
        Me.lbPassword.BackColor = System.Drawing.Color.Transparent
        Me.lbPassword.Location = New System.Drawing.Point(23, 48)
        Me.lbPassword.Name = "lbPassword"
        Me.lbPassword.Size = New System.Drawing.Size(61, 13)
        Me.lbPassword.TabIndex = 0
        Me.lbPassword.Text = "Contraseña"
        '
        'lbUsuario
        '
        Me.lbUsuario.AutoSize = True
        Me.lbUsuario.BackColor = System.Drawing.Color.Transparent
        Me.lbUsuario.Location = New System.Drawing.Point(45, 23)
        Me.lbUsuario.Name = "lbUsuario"
        Me.lbUsuario.Size = New System.Drawing.Size(43, 13)
        Me.lbUsuario.TabIndex = 6
        Me.lbUsuario.Text = "Usuario"
        '
        'panelIndices
        '
        Me.panelIndices.BackColor = System.Drawing.Color.Transparent
        Me.panelIndices.Controls.Add(Me.lstIndices)
        Me.panelIndices.Controls.Add(Me.lblTituloIndices)
        Me.panelIndices.Controls.Add(Me.btQuitar)
        Me.panelIndices.Controls.Add(Me.btAgregar)
        Me.panelIndices.Location = New System.Drawing.Point(432, 21)
        Me.panelIndices.Name = "panelIndices"
        Me.panelIndices.Size = New System.Drawing.Size(253, 205)
        Me.panelIndices.TabIndex = 27
        Me.panelIndices.Visible = False
        '
        'lstIndices
        '
        Me.lstIndices.FormattingEnabled = True
        Me.lstIndices.Location = New System.Drawing.Point(6, 30)
        Me.lstIndices.Name = "lstIndices"
        Me.lstIndices.Size = New System.Drawing.Size(163, 160)
        Me.lstIndices.TabIndex = 0
        '
        'lblTituloIndices
        '
        Me.lblTituloIndices.AutoSize = True
        Me.lblTituloIndices.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTituloIndices.Location = New System.Drawing.Point(3, 13)
        Me.lblTituloIndices.Name = "lblTituloIndices"
        Me.lblTituloIndices.Size = New System.Drawing.Size(157, 13)
        Me.lblTituloIndices.TabIndex = 3
        Me.lblTituloIndices.Text = "Lista de Indices a adjuntar"
        '
        'btQuitar
        '
        Me.btQuitar.Location = New System.Drawing.Point(175, 167)
        Me.btQuitar.Name = "btQuitar"
        Me.btQuitar.Size = New System.Drawing.Size(75, 23)
        Me.btQuitar.TabIndex = 0
        Me.btQuitar.Text = "Quitar"
        Me.btQuitar.UseVisualStyleBackColor = True
        '
        'btAgregar
        '
        Me.btAgregar.BackColor = System.Drawing.Color.Transparent
        Me.btAgregar.Location = New System.Drawing.Point(175, 138)
        Me.btAgregar.Name = "btAgregar"
        Me.btAgregar.Size = New System.Drawing.Size(75, 23)
        Me.btAgregar.TabIndex = 0
        Me.btAgregar.Text = "Agregar"
        Me.btAgregar.UseVisualStyleBackColor = False
        '
        'btNuevoAutomail
        '
        Me.btNuevoAutomail.BackColor = System.Drawing.Color.Transparent
        Me.btNuevoAutomail.Location = New System.Drawing.Point(290, 193)
        Me.btNuevoAutomail.Name = "btNuevoAutomail"
        Me.btNuevoAutomail.Size = New System.Drawing.Size(110, 23)
        Me.btNuevoAutomail.TabIndex = 2
        Me.btNuevoAutomail.Text = "Nuevo AutoMail"
        Me.btNuevoAutomail.UseVisualStyleBackColor = False
        '
        'gbAutoMail
        '
        Me.gbAutoMail.BackColor = System.Drawing.Color.Transparent
        Me.gbAutoMail.Controls.Add(Me.btModificarAutoMail)
        Me.gbAutoMail.Controls.Add(Me.lstAutoMails)
        Me.gbAutoMail.Controls.Add(Me.btNuevoAutomail)
        Me.gbAutoMail.Controls.Add(Me.rbDocumento)
        Me.gbAutoMail.Controls.Add(Me.chkAddIndexs)
        Me.gbAutoMail.Controls.Add(Me.rbLink)
        Me.gbAutoMail.Location = New System.Drawing.Point(20, 21)
        Me.gbAutoMail.Name = "gbAutoMail"
        Me.gbAutoMail.Size = New System.Drawing.Size(406, 290)
        Me.gbAutoMail.TabIndex = 29
        Me.gbAutoMail.TabStop = False
        Me.gbAutoMail.Text = "Seleccione el Automail a enviarse"
        '
        'btModificarAutoMail
        '
        Me.btModificarAutoMail.Location = New System.Drawing.Point(209, 193)
        Me.btModificarAutoMail.Name = "btModificarAutoMail"
        Me.btModificarAutoMail.Size = New System.Drawing.Size(75, 23)
        Me.btModificarAutoMail.TabIndex = 1
        Me.btModificarAutoMail.Text = "Modificar AutoMail"
        Me.btModificarAutoMail.UseVisualStyleBackColor = True
        '
        'gbSMTP
        '
        Me.gbSMTP.BackColor = System.Drawing.Color.Transparent
        Me.gbSMTP.Controls.Add(Me.btTestConfiguracionSMTP)
        Me.gbSMTP.Controls.Add(Me.lbUsuario)
        Me.gbSMTP.Controls.Add(Me.lbPassword)
        Me.gbSMTP.Controls.Add(Me.lbPort)
        Me.gbSMTP.Controls.Add(Me.tbProveedor)
        Me.gbSMTP.Controls.Add(Me.lbProveedor)
        Me.gbSMTP.Controls.Add(Me.tbPuerto)
        Me.gbSMTP.Controls.Add(Me.tbUsuario)
        Me.gbSMTP.Controls.Add(Me.tbPassword)
        Me.gbSMTP.Location = New System.Drawing.Point(26, 326)
        Me.gbSMTP.Name = "gbSMTP"
        Me.gbSMTP.Size = New System.Drawing.Size(400, 162)
        Me.gbSMTP.TabIndex = 0
        Me.gbSMTP.TabStop = False
        Me.gbSMTP.Text = "Configuración SMTP"
        '
        'btTestConfiguracionSMTP
        '
        Me.btTestConfiguracionSMTP.Location = New System.Drawing.Point(266, 133)
        Me.btTestConfiguracionSMTP.Name = "btTestConfiguracionSMTP"
        Me.btTestConfiguracionSMTP.Size = New System.Drawing.Size(128, 23)
        Me.btTestConfiguracionSMTP.TabIndex = 10
        Me.btTestConfiguracionSMTP.Text = "Probar Configuración"
        Me.btTestConfiguracionSMTP.UseVisualStyleBackColor = True
        '
        'chkGroup
        '
        Me.chkGroup.AutoSize = True
        Me.chkGroup.BackColor = System.Drawing.Color.Transparent
        Me.chkGroup.Location = New System.Drawing.Point(145, 472)
        Me.chkGroup.Name = "chkGroup"
        Me.chkGroup.Size = New System.Drawing.Size(140, 17)
        Me.chkGroup.TabIndex = 30
        Me.chkGroup.Text = "Agrupar por Destinatario"
        Me.chkGroup.UseVisualStyleBackColor = False
        '
        'UCSelectAutoMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Controls.Add(Me.chkGroup)
        Me.Controls.Add(Me.gbSMTP)
        Me.Controls.Add(Me.gbAutoMail)
        Me.Controls.Add(Me.panelIndices)
        Me.Controls.Add(Me.btAceptar)
        Me.Name = "UCSelectAutoMail"
        Me.Size = New System.Drawing.Size(706, 554)
        Me.panelIndices.ResumeLayout(False)
        Me.panelIndices.PerformLayout()
        Me.gbAutoMail.ResumeLayout(False)
        Me.gbAutoMail.PerformLayout()
        Me.gbSMTP.ResumeLayout(False)
        Me.gbSMTP.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _ruleId As Int64
    Public Sub New(ByVal ruleId As Int64)
        InitializeComponent()
        _ruleId = ruleId
        LoadAutoMails()
    End Sub

    ''' <summary>
    ''' Carga los todos los automails en lstAutomails
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAutoMails()
        lstAutoMails.Clear()
        For Each automail As AutoMail In AutoMailDBFacade.GetAutoMailList
            lstAutoMails.Items.Add(New StepItem(automail, False))
        Next
    End Sub

    Private Sub btAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAceptar.Click
        If rbDocumento.Checked Then
            WFBusiness.SetRulesPreferences(_ruleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailAttach, 0, rbDocumento.Text)
        Else
            WFBusiness.SetRulesPreferences(_ruleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailAttach, 0, rbLink.Text)
        End If

        If chkAddIndexs.Checked Then
            WFBusiness.SetRulesPreferences(_ruleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailEnableAttach, 1)
        Else
            WFBusiness.SetRulesPreferences(_ruleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailEnableAttach, 0)
        End If

        If chkGroup.Checked Then
            WFBusiness.SetRulesPreferences(_ruleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPGroupByDest, 0, 1)
        Else
            WFBusiness.SetRulesPreferences(_ruleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPGroupByDest, 0, 0)
        End If

        WFBusiness.SetRulesPreferences(_ruleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPUser, 0, tbUsuario.Text)
        WFBusiness.SetRulesPreferences(_ruleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPPass, 0, tbPassword.Text)
        WFBusiness.SetRulesPreferences(_ruleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPProvider, 0, tbProveedor.Text)
        WFBusiness.SetRulesPreferences(_ruleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPPort, 0, tbPuerto.Text)


    End Sub

    Private Class StepItem
        Inherits ListViewItem
        Private _autoMail As AutoMail
        Private _selectedStep As Boolean
        Private _selectedCheck As Boolean
        Public Property Automail() As AutoMail
            Get
                Return _autoMail
            End Get
            Set(ByVal value As AutoMail)
                _autoMail = value
            End Set
        End Property
        Public Property SelectedStep() As Boolean
            Get
                Return _selectedStep
            End Get
            Set(ByVal Value As Boolean)
                _selectedStep = Value
                If Value = 0 Then
                    Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                Else
                    Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                End If
            End Set
        End Property
        Sub New(ByVal mail As AutoMail, ByVal selectStep As Boolean)
            Automail = mail
            Text = Automail.Name
            SelectedStep = selectStep
            Me.Selected = Me.SelectedStep
        End Sub
    End Class

End Class