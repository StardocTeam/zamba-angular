Public Class UCDOASIGN
    Inherits ZRuleControl


#Region " Código generado por el Diseñador de Windows Forms "
    Friend WithEvents UserList As System.Windows.Forms.ListView
    Friend Shadows WithEvents btnSeleccionar As ZButton
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents lblUsuario As ZLabel
    Friend WithEvents GroupList As System.Windows.Forms.ListView
    Private WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label4 As ZLabel
    Friend Shadows WithEvents rdbUser As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGroup As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents rdbManual As System.Windows.Forms.RadioButton
    Friend WithEvents txtUsuario As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents ZPanel2 As ZPanel
    Friend WithEvents ZPanel1 As ZPanel
    Private WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        UserList = New System.Windows.Forms.ListView()
        ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        btnSeleccionar = New ZButton()
        Label1 = New ZLabel()
        Label2 = New ZLabel()
        Label3 = New ZLabel()
        lblUsuario = New ZLabel()
        GroupList = New System.Windows.Forms.ListView()
        ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Label4 = New ZLabel()
        rdbUser = New System.Windows.Forms.RadioButton()
        rdbGroup = New System.Windows.Forms.RadioButton()
        Label5 = New ZLabel()
        rdbManual = New System.Windows.Forms.RadioButton()
        txtUsuario = New Zamba.AppBlock.TextoInteligenteTextBox()
        ZPanel1 = New ZPanel()
        ZPanel2 = New ZPanel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        ZPanel1.SuspendLayout()
        ZPanel2.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(UserList)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(ZPanel2)
        tbRule.Controls.Add(ZPanel1)
        tbRule.Size = New System.Drawing.Size(595, 420)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(603, 449)
        '
        'UserList
        '
        UserList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1})
        UserList.Dock = System.Windows.Forms.DockStyle.Fill
        UserList.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        UserList.FullRowSelect = True
        UserList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        UserList.HideSelection = False
        UserList.Location = New System.Drawing.Point(3, 19)
        UserList.MultiSelect = False
        UserList.Name = "UserList"
        UserList.Size = New System.Drawing.Size(389, 292)
        UserList.TabIndex = 11
        UserList.UseCompatibleStateImageBehavior = False
        UserList.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = ""
        ColumnHeader1.Width = 200
        '
        'btnSeleccionar
        '
        btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnSeleccionar.FlatStyle = FlatStyle.Flat
        btnSeleccionar.ForeColor = System.Drawing.Color.White
        btnSeleccionar.Location = New System.Drawing.Point(459, 70)
        btnSeleccionar.Name = "btnSeleccionar"
        btnSeleccionar.Size = New System.Drawing.Size(79, 23)
        btnSeleccionar.TabIndex = 12
        btnSeleccionar.Text = "&Guardar"
        btnSeleccionar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(321, 11)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(217, 16)
        Label1.TabIndex = 14
        Label1.Text = "Ingresar usuario manualmente :"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Dock = System.Windows.Forms.DockStyle.Top
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(3, 3)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(126, 16)
        Label2.TabIndex = 15
        Label2.Text = "Lista de Usuarios:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(12, 70)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(210, 16)
        Label3.TabIndex = 16
        Label3.Text = "Usuario o Grupo Seleccionado:"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblUsuario
        '
        lblUsuario.AutoSize = True
        lblUsuario.BackColor = System.Drawing.Color.Transparent
        lblUsuario.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblUsuario.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblUsuario.Location = New System.Drawing.Point(228, 70)
        lblUsuario.Name = "lblUsuario"
        lblUsuario.Size = New System.Drawing.Size(77, 16)
        lblUsuario.TabIndex = 17
        lblUsuario.Text = "lblUsuarios"
        lblUsuario.TextAlign = ContentAlignment.MiddleLeft
        '
        'GroupList
        '
        GroupList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader2})
        GroupList.Dock = System.Windows.Forms.DockStyle.Fill
        GroupList.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        GroupList.FullRowSelect = True
        GroupList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        GroupList.HideSelection = False
        GroupList.Location = New System.Drawing.Point(0, 16)
        GroupList.MultiSelect = False
        GroupList.Name = "GroupList"
        GroupList.Size = New System.Drawing.Size(200, 292)
        GroupList.TabIndex = 18
        GroupList.UseCompatibleStateImageBehavior = False
        GroupList.View = System.Windows.Forms.View.List
        '
        'ColumnHeader2
        '
        ColumnHeader2.Text = ""
        ColumnHeader2.Width = 200
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Dock = System.Windows.Forms.DockStyle.Top
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(0, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(116, 16)
        Label4.TabIndex = 19
        Label4.Text = "Lista de Grupos:"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'rdbUser
        '
        rdbUser.AutoSize = True
        rdbUser.BackColor = System.Drawing.Color.Transparent
        rdbUser.Location = New System.Drawing.Point(15, 31)
        rdbUser.Name = "rdbUser"
        rdbUser.Size = New System.Drawing.Size(74, 20)
        rdbUser.TabIndex = 20
        rdbUser.TabStop = True
        rdbUser.Text = "Usuario"
        rdbUser.UseVisualStyleBackColor = False
        '
        'rdbGroup
        '
        rdbGroup.AutoSize = True
        rdbGroup.BackColor = System.Drawing.Color.Transparent
        rdbGroup.Location = New System.Drawing.Point(105, 31)
        rdbGroup.Name = "rdbGroup"
        rdbGroup.Size = New System.Drawing.Size(64, 20)
        rdbGroup.TabIndex = 21
        rdbGroup.TabStop = True
        rdbGroup.Text = "Grupo"
        rdbGroup.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label5.Location = New System.Drawing.Point(12, 11)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(88, 16)
        Label5.TabIndex = 22
        Label5.Text = "Asignar por:"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'rdbManual
        '
        rdbManual.AutoSize = True
        rdbManual.BackColor = System.Drawing.Color.Transparent
        rdbManual.Location = New System.Drawing.Point(202, 31)
        rdbManual.Name = "rdbManual"
        rdbManual.Size = New System.Drawing.Size(72, 20)
        rdbManual.TabIndex = 23
        rdbManual.TabStop = True
        rdbManual.Text = "Manual"
        rdbManual.UseVisualStyleBackColor = False
        '
        'txtUsuario
        '
        txtUsuario.Location = New System.Drawing.Point(328, 37)
        txtUsuario.MaxLength = 4000
        txtUsuario.Name = "txtUsuario"
        txtUsuario.Size = New System.Drawing.Size(210, 20)
        txtUsuario.TabIndex = 24
        txtUsuario.Text = ""
        '
        'ZPanel1
        '
        ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel1.Controls.Add(Label1)
        ZPanel1.Controls.Add(txtUsuario)
        ZPanel1.Controls.Add(btnSeleccionar)
        ZPanel1.Controls.Add(rdbManual)
        ZPanel1.Controls.Add(Label3)
        ZPanel1.Controls.Add(Label5)
        ZPanel1.Controls.Add(lblUsuario)
        ZPanel1.Controls.Add(rdbGroup)
        ZPanel1.Controls.Add(rdbUser)
        ZPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        ZPanel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel1.Location = New System.Drawing.Point(3, 311)
        ZPanel1.Name = "ZPanel1"
        ZPanel1.Size = New System.Drawing.Size(589, 106)
        ZPanel1.TabIndex = 25
        '
        'ZPanel2
        '
        ZPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel2.Controls.Add(GroupList)
        ZPanel2.Controls.Add(Label4)
        ZPanel2.Dock = System.Windows.Forms.DockStyle.Right
        ZPanel2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel2.Location = New System.Drawing.Point(392, 3)
        ZPanel2.Name = "ZPanel2"
        ZPanel2.Size = New System.Drawing.Size(200, 308)
        ZPanel2.TabIndex = 26
        '
        'UCDOASIGN
        '
        Name = "UCDOASIGN"
        Size = New System.Drawing.Size(603, 449)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ZPanel1.ResumeLayout(False)
        ZPanel1.PerformLayout()
        ZPanel2.ResumeLayout(False)
        ZPanel2.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

#Region "Constructores"

    Public Sub New(ByRef this As IDoAsign, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(this, _wfPanelCircuit)
        InitializeComponent()
        lblUsuario.Text = String.Empty
        Try
            FillUsers()
            FillGroups()
            FillManual()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Propiedades"

    Public Shadows ReadOnly Property CurrentRule() As IDoAsign
        Get
            Return DirectCast(Rule, IDoAsign)
        End Get
    End Property

#End Region

#Region "Eventos"

    Private Sub UCDOASIGN_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        'Try
        '    FillUsers()
        'Catch ex As Exception
        '    ZClass.raiseerror(ex)
        'End Try
    End Sub

    Private Sub btnSeleccionar_Click( _
        ByVal sender As System.Object, _
        ByVal e As EventArgs) _
        Handles btnSeleccionar.Click
        Try
            Dim UserItem As UserGroupItem = Nothing
            If rdbUser.Checked = True Then
                If UserList.SelectedItems.Count > 0 Then
                    ' tomo el usuario...
                    UserItem = UserList.SelectedItems(0)
                Else
                    MessageBox.Show("Debe elegir al menos un usuario", "Atencion")
                End If
            ElseIf rdbGroup.Checked = True Then
                If GroupList.SelectedItems.Count > 0 Then
                    ' tomo el usuario...
                    UserItem = GroupList.SelectedItems(0)
                Else
                    MessageBox.Show("Debe elegir al menos un grupo", "Atencion")
                End If
            ElseIf rdbManual.Checked = True Then
                If String.IsNullOrEmpty(txtUsuario.Text) = True Then
                    MessageBox.Show("Debe completar el nombre del usuario", "Atencion")
                End If
            Else
                MessageBox.Show("Debe elegir el tipo de asinación a realizar", "Atencion")
            End If
            ' Si se ingreso un usuario...
            If Not IsNothing(UserItem) Then
                ' Cargo como parametro su idUser...
                WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 0, UserItem.Id)

                lblUsuario.Text = UserItem.Text

                ' Marco el usuario seleccionado en la vista...
                For Each l As ListViewItem In UserList.Items
                    DirectCast(l, UserGroupItem).SelectedUser = False
                Next
                UserItem.SelectedUser = True
                CurrentRule.UserId = UserItem.Id
                'Me.CurrentRule.AlternateUser = txtUsuario.Text
                CurrentRule.AlternateUser = String.Empty
                txtUsuario.Text = String.Empty
                WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 1, txtUsuario.Text)
                UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
            ElseIf txtUsuario.Text <> String.Empty Then
                'ver
                WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 0, "0")
                CurrentRule.AlternateUser = txtUsuario.Text
                WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 1, txtUsuario.Text)
                UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
                lblUsuario.Text = txtUsuario.Text
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Métodos"
    'Llena los usuario
    Private Sub FillUsers()
        '[DIEGO]: Comente esta linea porque se borro el metodo
        'Dim dsFull As DataSet = WFUserBusiness.GetDistinctUsersByStepID(Me.CurrentRule.WFStepId)
        Dim dsFull As DataSet = WFUserBusiness.GetUsersDsByStepID(CurrentRule.WFStepId)
        If Not IsNothing(dsFull) Then
            If dsFull.Tables.Count > 0 Then
                For Each row As DataRow In dsFull.Tables(0).Rows
                    Try
                        If Int64.Parse(row("Id").ToString()) = CurrentRule.UserId Then
                            UserList.Items.Add(New UserGroupItem(Int64.Parse(row("id").ToString()), row("name").ToString(), True))
                            lblUsuario.Text = row("name").ToString()
                            rdbUser.Checked = True
                        Else
                            UserList.Items.Add(New UserGroupItem(Int64.Parse(row("id").ToString()), row("name").ToString(), False))
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                Next
            End If
        End If
    End Sub
    Private Sub FillGroups()
        Dim dsFull As DataSet = WFUserBusiness.GetGroupsDsByStepID(CurrentRule.WFStepId)
        If Not IsNothing(dsFull) Then
            If dsFull.Tables.Count > 0 Then
                For Each row As DataRow In dsFull.Tables(0).Rows
                    Try
                        If Int64.Parse(row("ID").ToString()) = CurrentRule.UserId Then
                            GroupList.Items.Add(New UserGroupItem(Int64.Parse(row("ID").ToString()), row("Name").ToString(), True))
                            lblUsuario.Text = row("Name").ToString()
                            rdbGroup.Checked = True
                        Else
                            GroupList.Items.Add(New UserGroupItem(Int64.Parse(row("ID").ToString()), row("Name").ToString(), False))
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                Next
            End If
        End If
    End Sub
    Private Sub FillManual()
        If CurrentRule.AlternateUser <> "" Then
            txtUsuario.Text = CurrentRule.AlternateUser
            lblUsuario.Text = CurrentRule.AlternateUser
            rdbManual.Checked = True
        End If
    End Sub
#End Region

#Region "Clases"
    Private Class UserGroupItem
        Inherits ListViewItem
        Public Id As Int64
        Private _SelectedUser As Boolean

        Public Property SelectedUser() As Boolean
            Get
                Return _SelectedUser
            End Get
            Set(ByVal Value As Boolean)
                _SelectedUser = Value
                If Value = 0 Then
                    Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                Else
                    Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
                End If
            End Set
        End Property

        Sub New(ByVal userId As Int64, ByVal userName As String, ByVal SelectedUser As Boolean)
            Id = userId
            Text = userName
            Me.SelectedUser = SelectedUser
        End Sub
    End Class
#End Region
End Class