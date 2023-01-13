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
        Me.UserList = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnSeleccionar = New Zamba.AppBlock.ZButton()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.lblUsuario = New Zamba.AppBlock.ZLabel()
        Me.GroupList = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.rdbUser = New System.Windows.Forms.RadioButton()
        Me.rdbGroup = New System.Windows.Forms.RadioButton()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.rdbManual = New System.Windows.Forms.RadioButton()
        Me.txtUsuario = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZPanel1 = New Zamba.AppBlock.ZPanel()
        Me.ZPanel2 = New Zamba.AppBlock.ZPanel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.ZPanel1.SuspendLayout()
        Me.ZPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.UserList)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.ZPanel2)
        Me.tbRule.Controls.Add(Me.ZPanel1)
        Me.tbRule.Size = New System.Drawing.Size(595, 420)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(603, 449)
        '
        'UserList
        '
        Me.UserList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.UserList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserList.FullRowSelect = True
        Me.UserList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.UserList.HideSelection = False
        Me.UserList.Location = New System.Drawing.Point(3, 19)
        Me.UserList.MultiSelect = False
        Me.UserList.Name = "UserList"
        Me.UserList.Size = New System.Drawing.Size(389, 292)
        Me.UserList.TabIndex = 11
        Me.UserList.UseCompatibleStateImageBehavior = False
        Me.UserList.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = ""
        Me.ColumnHeader1.Width = 200
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSeleccionar.ForeColor = System.Drawing.Color.White
        Me.btnSeleccionar.Location = New System.Drawing.Point(510, 37)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(79, 23)
        Me.btnSeleccionar.TabIndex = 12
        Me.btnSeleccionar.Text = "&Guardar"
        Me.btnSeleccionar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label1.FontSize = 9.75!
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(288, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(217, 16)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Ingresar usuario manualmente :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label2.FontSize = 9.75!
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 16)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Lista de Usuarios:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label3.FontSize = 9.75!
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(12, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(210, 16)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Usuario o Grupo Seleccionado:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUsuario
        '
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.BackColor = System.Drawing.Color.Transparent
        Me.lblUsuario.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblUsuario.FontSize = 9.75!
        Me.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblUsuario.Location = New System.Drawing.Point(228, 70)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(77, 16)
        Me.lblUsuario.TabIndex = 17
        Me.lblUsuario.Text = "lblUsuarios"
        Me.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupList
        '
        Me.GroupList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.GroupList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupList.FullRowSelect = True
        Me.GroupList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.GroupList.HideSelection = False
        Me.GroupList.Location = New System.Drawing.Point(0, 16)
        Me.GroupList.MultiSelect = False
        Me.GroupList.Name = "GroupList"
        Me.GroupList.Size = New System.Drawing.Size(200, 292)
        Me.GroupList.TabIndex = 18
        Me.GroupList.UseCompatibleStateImageBehavior = False
        Me.GroupList.View = System.Windows.Forms.View.List
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = ""
        Me.ColumnHeader2.Width = 200
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label4.FontSize = 9.75!
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 16)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Lista de Grupos:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdbUser
        '
        Me.rdbUser.AutoSize = True
        Me.rdbUser.BackColor = System.Drawing.Color.Transparent
        Me.rdbUser.Location = New System.Drawing.Point(15, 31)
        Me.rdbUser.Name = "rdbUser"
        Me.rdbUser.Size = New System.Drawing.Size(74, 20)
        Me.rdbUser.TabIndex = 20
        Me.rdbUser.TabStop = True
        Me.rdbUser.Text = "Usuario"
        Me.rdbUser.UseVisualStyleBackColor = False
        '
        'rdbGroup
        '
        Me.rdbGroup.AutoSize = True
        Me.rdbGroup.BackColor = System.Drawing.Color.Transparent
        Me.rdbGroup.Location = New System.Drawing.Point(105, 31)
        Me.rdbGroup.Name = "rdbGroup"
        Me.rdbGroup.Size = New System.Drawing.Size(64, 20)
        Me.rdbGroup.TabIndex = 21
        Me.rdbGroup.TabStop = True
        Me.rdbGroup.Text = "Grupo"
        Me.rdbGroup.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label5.FontSize = 9.75!
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(12, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 16)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Asignar por:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdbManual
        '
        Me.rdbManual.AutoSize = True
        Me.rdbManual.BackColor = System.Drawing.Color.Transparent
        Me.rdbManual.Location = New System.Drawing.Point(202, 31)
        Me.rdbManual.Name = "rdbManual"
        Me.rdbManual.Size = New System.Drawing.Size(72, 20)
        Me.rdbManual.TabIndex = 23
        Me.rdbManual.TabStop = True
        Me.rdbManual.Text = "Manual"
        Me.rdbManual.UseVisualStyleBackColor = False
        '
        'txtUsuario
        '
        Me.txtUsuario.Location = New System.Drawing.Point(295, 37)
        Me.txtUsuario.MaxLength = 4000
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(210, 20)
        Me.txtUsuario.TabIndex = 24
        Me.txtUsuario.Text = ""
        '
        'ZPanel1
        '
        Me.ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel1.Controls.Add(Me.Label1)
        Me.ZPanel1.Controls.Add(Me.txtUsuario)
        Me.ZPanel1.Controls.Add(Me.btnSeleccionar)
        Me.ZPanel1.Controls.Add(Me.rdbManual)
        Me.ZPanel1.Controls.Add(Me.Label3)
        Me.ZPanel1.Controls.Add(Me.Label5)
        Me.ZPanel1.Controls.Add(Me.lblUsuario)
        Me.ZPanel1.Controls.Add(Me.rdbGroup)
        Me.ZPanel1.Controls.Add(Me.rdbUser)
        Me.ZPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ZPanel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel1.Location = New System.Drawing.Point(3, 311)
        Me.ZPanel1.Name = "ZPanel1"
        Me.ZPanel1.Size = New System.Drawing.Size(589, 106)
        Me.ZPanel1.TabIndex = 25
        '
        'ZPanel2
        '
        Me.ZPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel2.Controls.Add(Me.GroupList)
        Me.ZPanel2.Controls.Add(Me.Label4)
        Me.ZPanel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.ZPanel2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel2.Location = New System.Drawing.Point(392, 3)
        Me.ZPanel2.Name = "ZPanel2"
        Me.ZPanel2.Size = New System.Drawing.Size(200, 308)
        Me.ZPanel2.TabIndex = 26
        '
        'UCDOASIGN
        '
        Me.Name = "UCDOASIGN"
        Me.Size = New System.Drawing.Size(603, 449)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ZPanel1.ResumeLayout(False)
        Me.ZPanel1.PerformLayout()
        Me.ZPanel2.ResumeLayout(False)
        Me.ZPanel2.PerformLayout()
        Me.ResumeLayout(False)

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

    Private Sub btnSeleccionar_Click(
        ByVal sender As System.Object,
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