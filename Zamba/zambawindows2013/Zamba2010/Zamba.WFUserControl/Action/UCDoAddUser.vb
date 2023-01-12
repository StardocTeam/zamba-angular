'Imports Zamba.WFBusiness

Public Class UCDoAddUser
    Inherits ZRuleControl

#Region " Código generado por el Diseñador de Windows Forms "


    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Friend WithEvents lblName As ZLabel
    Friend WithEvents btnGuardar As ZButton
    Friend WithEvents lblTel As ZLabel
    Friend WithEvents lblEmail As ZLabel
    Friend WithEvents lblPuesto As ZLabel
    Friend WithEvents lblPassword As ZLabel
    Friend WithEvents lblAvatar As ZLabel
    Friend WithEvents lblApellido As ZLabel
    Friend WithEvents btnBrowserAvatar As ZButton
    Friend WithEvents lblNameUser As ZLabel
    Friend WithEvents lblVarAutomaticas As ZLabel
    Friend WithEvents lstVariablesAutomaticas As ListBox
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtNombreVar As AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtNameUser As AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtPuesto As AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtEmail As AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtTelefono As AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtPassword As AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtAvatar As AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtApellido As AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtNombre As AppBlock.TextoInteligenteTextBox

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    <DebuggerStepThrough()>
    Private Overloads Sub InitializeComponent()
        lblName = New ZLabel()
        btnGuardar = New ZButton()
        lblApellido = New ZLabel()
        lblPassword = New ZLabel()
        lblAvatar = New ZLabel()
        lblTel = New ZLabel()
        lblEmail = New ZLabel()
        lblPuesto = New ZLabel()
        btnBrowserAvatar = New ZButton()
        lblNameUser = New ZLabel()
        lstVariablesAutomaticas = New ListBox()
        lblVarAutomaticas = New ZLabel()
        Label3 = New ZLabel()
        txtNombreVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtNameUser = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtNombre = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtApellido = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtAvatar = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtPassword = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtTelefono = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtEmail = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtPuesto = New Zamba.AppBlock.TextoInteligenteTextBox()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(txtPuesto)
        tbRule.Controls.Add(txtEmail)
        tbRule.Controls.Add(txtTelefono)
        tbRule.Controls.Add(txtPassword)
        tbRule.Controls.Add(txtAvatar)
        tbRule.Controls.Add(txtApellido)
        tbRule.Controls.Add(txtNombre)
        tbRule.Controls.Add(txtNameUser)
        tbRule.Controls.Add(txtNombreVar)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(lblVarAutomaticas)
        tbRule.Controls.Add(lstVariablesAutomaticas)
        tbRule.Controls.Add(lblNameUser)
        tbRule.Controls.Add(btnBrowserAvatar)
        tbRule.Controls.Add(lblTel)
        tbRule.Controls.Add(lblEmail)
        tbRule.Controls.Add(lblPuesto)
        tbRule.Controls.Add(lblPassword)
        tbRule.Controls.Add(lblAvatar)
        tbRule.Controls.Add(lblApellido)
        tbRule.Controls.Add(btnGuardar)
        tbRule.Controls.Add(lblName)
        tbRule.Size = New System.Drawing.Size(814, 432)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(822, 461)
        '
        'lblName
        '
        lblName.AutoSize = True
        lblName.BackColor = System.Drawing.Color.Transparent
        lblName.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblName.Location = New System.Drawing.Point(14, 89)
        lblName.Name = "lblName"
        lblName.Size = New System.Drawing.Size(57, 16)
        lblName.TabIndex = 0
        lblName.Tag = "NOMBRE"
        lblName.Text = "Nombre"
        lblName.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnGuardar
        '
        btnGuardar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnGuardar.FlatStyle = FlatStyle.Flat
        btnGuardar.ForeColor = System.Drawing.Color.White
        btnGuardar.Location = New System.Drawing.Point(524, 336)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New System.Drawing.Size(70, 23)
        btnGuardar.TabIndex = 13
        btnGuardar.Tag = ""
        btnGuardar.Text = "Guardar"
        btnGuardar.UseVisualStyleBackColor = True
        '
        'lblApellido
        '
        lblApellido.AutoSize = True
        lblApellido.BackColor = System.Drawing.Color.Transparent
        lblApellido.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblApellido.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblApellido.Location = New System.Drawing.Point(386, 92)
        lblApellido.Name = "lblApellido"
        lblApellido.Size = New System.Drawing.Size(58, 16)
        lblApellido.TabIndex = 14
        lblApellido.Tag = "APELLIDO"
        lblApellido.Text = "Apellido"
        lblApellido.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblPassword
        '
        lblPassword.AutoSize = True
        lblPassword.BackColor = System.Drawing.Color.Transparent
        lblPassword.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblPassword.Location = New System.Drawing.Point(386, 43)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New System.Drawing.Size(83, 16)
        lblPassword.TabIndex = 37
        lblPassword.Tag = "PASSWORD"
        lblPassword.Text = "Contraseña"
        lblPassword.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblAvatar
        '
        lblAvatar.AutoSize = True
        lblAvatar.BackColor = System.Drawing.Color.Transparent
        lblAvatar.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblAvatar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblAvatar.Location = New System.Drawing.Point(14, 190)
        lblAvatar.Name = "lblAvatar"
        lblAvatar.Size = New System.Drawing.Size(52, 16)
        lblAvatar.TabIndex = 34
        lblAvatar.Tag = "IMG"
        lblAvatar.Text = "Avatar"
        lblAvatar.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblTel
        '
        lblTel.AutoSize = True
        lblTel.BackColor = System.Drawing.Color.Transparent
        lblTel.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblTel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblTel.Location = New System.Drawing.Point(7, 137)
        lblTel.Name = "lblTel"
        lblTel.Size = New System.Drawing.Size(64, 16)
        lblTel.TabIndex = 47
        lblTel.Tag = "TELEFONO"
        lblTel.Text = "Telefono"
        lblTel.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblEmail
        '
        lblEmail.AutoSize = True
        lblEmail.BackColor = System.Drawing.Color.Transparent
        lblEmail.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblEmail.Location = New System.Drawing.Point(386, 144)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New System.Drawing.Size(41, 16)
        lblEmail.TabIndex = 41
        lblEmail.Tag = "EMAIL"
        lblEmail.Text = "Email"
        lblEmail.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblPuesto
        '
        lblPuesto.AutoSize = True
        lblPuesto.BackColor = System.Drawing.Color.Transparent
        lblPuesto.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblPuesto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblPuesto.Location = New System.Drawing.Point(386, 186)
        lblPuesto.Name = "lblPuesto"
        lblPuesto.Size = New System.Drawing.Size(53, 16)
        lblPuesto.TabIndex = 39
        lblPuesto.Tag = "PUESTO"
        lblPuesto.Text = "Puesto"
        lblPuesto.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnBrowserAvatar
        '
        btnBrowserAvatar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnBrowserAvatar.FlatStyle = FlatStyle.Flat
        btnBrowserAvatar.ForeColor = System.Drawing.Color.White
        btnBrowserAvatar.Location = New System.Drawing.Point(329, 183)
        btnBrowserAvatar.Name = "btnBrowserAvatar"
        btnBrowserAvatar.Size = New System.Drawing.Size(44, 22)
        btnBrowserAvatar.TabIndex = 57
        btnBrowserAvatar.Text = "...."
        btnBrowserAvatar.UseVisualStyleBackColor = True
        '
        'lblNameUser
        '
        lblNameUser.AutoSize = True
        lblNameUser.BackColor = System.Drawing.Color.Transparent
        lblNameUser.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblNameUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblNameUser.Location = New System.Drawing.Point(14, 31)
        lblNameUser.Name = "lblNameUser"
        lblNameUser.Size = New System.Drawing.Size(76, 32)
        lblNameUser.TabIndex = 65
        lblNameUser.Tag = "NOMBREUSUARIO"
        lblNameUser.Text = "Nombre " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "de usuario"
        lblNameUser.TextAlign = ContentAlignment.MiddleLeft
        '
        'lstVariablesAutomaticas
        '
        lstVariablesAutomaticas.BorderStyle = System.Windows.Forms.BorderStyle.None
        lstVariablesAutomaticas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lstVariablesAutomaticas.FormattingEnabled = True
        lstVariablesAutomaticas.ItemHeight = 16
        lstVariablesAutomaticas.Location = New System.Drawing.Point(329, 278)
        lstVariablesAutomaticas.Name = "lstVariablesAutomaticas"
        lstVariablesAutomaticas.Size = New System.Drawing.Size(179, 112)
        lstVariablesAutomaticas.TabIndex = 67
        '
        'lblVarAutomaticas
        '
        lblVarAutomaticas.AutoSize = True
        lblVarAutomaticas.BackColor = System.Drawing.Color.Transparent
        lblVarAutomaticas.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblVarAutomaticas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblVarAutomaticas.Location = New System.Drawing.Point(326, 259)
        lblVarAutomaticas.Name = "lblVarAutomaticas"
        lblVarAutomaticas.Size = New System.Drawing.Size(152, 16)
        lblVarAutomaticas.TabIndex = 68
        lblVarAutomaticas.Text = "Variables automaticas"
        lblVarAutomaticas.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(14, 259)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(270, 16)
        Label3.TabIndex = 69
        Label3.Text = "Nombre de la variable de Nuevo Usuario"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtNombreVar
        '
        txtNombreVar.Location = New System.Drawing.Point(17, 278)
        txtNombreVar.Name = "txtNombreVar"
        txtNombreVar.Size = New System.Drawing.Size(156, 23)
        txtNombreVar.TabIndex = 70
        txtNombreVar.Text = ""
        '
        'txtNameUser
        '
        txtNameUser.Location = New System.Drawing.Point(96, 43)
        txtNameUser.Name = "txtNameUser"
        txtNameUser.Size = New System.Drawing.Size(277, 23)
        txtNameUser.TabIndex = 72
        txtNameUser.Text = ""
        '
        'txtNombre
        '
        txtNombre.Location = New System.Drawing.Point(96, 89)
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New System.Drawing.Size(277, 23)
        txtNombre.TabIndex = 73
        txtNombre.Text = ""
        '
        'txtApellido
        '
        txtApellido.Location = New System.Drawing.Point(475, 89)
        txtApellido.Name = "txtApellido"
        txtApellido.Size = New System.Drawing.Size(277, 23)
        txtApellido.TabIndex = 74
        txtApellido.Text = ""
        '
        'txtAvatar
        '
        txtAvatar.Location = New System.Drawing.Point(96, 183)
        txtAvatar.Name = "txtAvatar"
        txtAvatar.Size = New System.Drawing.Size(236, 23)
        txtAvatar.TabIndex = 75
        txtAvatar.Text = ""
        '
        'txtPassword
        '
        txtPassword.Location = New System.Drawing.Point(475, 40)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New System.Drawing.Size(277, 23)
        txtPassword.TabIndex = 76
        txtPassword.Text = ""
        '
        'txtTelefono
        '
        txtTelefono.Location = New System.Drawing.Point(96, 141)
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Size = New System.Drawing.Size(277, 23)
        txtTelefono.TabIndex = 77
        txtTelefono.Text = ""
        '
        'txtEmail
        '
        txtEmail.Location = New System.Drawing.Point(475, 141)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New System.Drawing.Size(277, 23)
        txtEmail.TabIndex = 78
        txtEmail.Text = ""
        '
        'txtPuesto
        '
        txtPuesto.Location = New System.Drawing.Point(475, 183)
        txtPuesto.Name = "txtPuesto"
        txtPuesto.Size = New System.Drawing.Size(277, 23)
        txtPuesto.TabIndex = 79
        txtPuesto.Text = ""
        '
        'UCDoAddUser
        '
        Name = "UCDoAddUser"
        Size = New System.Drawing.Size(822, 461)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region
    Dim CurrentRule As IDoAddUser


    Public Sub New(ByVal CurrentRule As IDoAddUser, ByRef _wfPanelCircuit As IWFPanelCircuit)

        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()

        Me.CurrentRule = CurrentRule
        txtPassword.Text = CurrentRule.password
        txtTelefono.Text = CurrentRule.telefono
        txtEmail.Text = CurrentRule.email
        txtAvatar.Text = CurrentRule.avatar
        txtPuesto.Text = CurrentRule.puesto
        txtNombre.Text = CurrentRule.nombre
        txtApellido.Text = CurrentRule.apellido
        txtNameUser.Text = CurrentRule.nameUser
        txtNombreVar.Text = CurrentRule.varUsr
        AddAutomaticVariables()
    End Sub



    'Public Sub New(ByRef _rule As IRule)
    '    MyBase.New(_rule)
    '    InitializeComponent()
    'End Sub
    'Public Shadows ReadOnly Property MyRule() As IDoAddUser
    '    Get
    '        Return DirectCast(Me.Rule, IDoAddUser)
    '    End Get
    'End Property


#Region "Eventos"
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try

            If Not String.IsNullOrEmpty(txtNameUser.Text) And Not String.IsNullOrEmpty(txtNombre.Text) And Not String.IsNullOrEmpty(txtApellido.Text) Then

                CurrentRule.nombre = txtNombre.Text
                CurrentRule.apellido = txtApellido.Text
                CurrentRule.nameUser = txtNameUser.Text
                CurrentRule.password = txtPassword.Text
                CurrentRule.telefono = txtTelefono.Text
                CurrentRule.email = txtEmail.Text
                CurrentRule.avatar = txtAvatar.Text
                CurrentRule.puesto = txtPuesto.Text
                CurrentRule.varUsr = txtNombreVar.Text


                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.nombre)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.apellido)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.nameUser)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.password)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.telefono)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.email)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.avatar)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.puesto)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 8, CurrentRule.varUsr)
                UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.nombre & "(" & CurrentRule.ID & ")")

                AddAutomaticVariables()
            Else
                MessageBox.Show("Error, debe completar los campos 'Nombre', 'Apellido' y 'Nombre de Usuario'")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnBrowserAvatar_Click(sender As Object, e As EventArgs) Handles btnBrowserAvatar.Click
        Dim folderSave As New OpenFileDialog

        folderSave.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg"

        If (folderSave.ShowDialog() = DialogResult.OK) Then

            txtAvatar.Text = folderSave.SafeFileName

        End If
    End Sub
    Private Sub AddAutomaticVariables()
        Try
            lstVariablesAutomaticas.Items.Clear()
            lstVariablesAutomaticas.Items.Add("zvar(" & CurrentRule.varUsr & ".Id)")
            lstVariablesAutomaticas.Items.Add("zvar(" & CurrentRule.varUsr & ".Nombre)")
            'todo: ML Descubrir las columnas devueltas por el select y mostrar las variables automaticas en DS y Scalar
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

End Class