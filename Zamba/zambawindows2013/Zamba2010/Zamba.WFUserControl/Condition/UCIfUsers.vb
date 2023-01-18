'Imports Zamba.WFBusiness
Imports System.Text

Public Class UCIfUsers
    Inherits ZRuleControl

#Region "Diseño"
    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <DebuggerStepThrough()>
    Private Overloads Sub InitializeComponent()
        lstUser = New ListBox()
        cboUsers = New ComboBox()
        btAgregar = New ZButton()
        btGuardar = New ZButton()
        btQuitar = New ZButton()
        rbAsignedTo = New System.Windows.Forms.RadioButton()
        rbCurrentUser = New System.Windows.Forms.RadioButton()
        rbNotAsignedTo = New System.Windows.Forms.RadioButton()
        rbNotCurrentUser = New System.Windows.Forms.RadioButton()
        Label5 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbState
        '
        tbState.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbState.Size = New System.Drawing.Size(824, 781)
        '
        'tbHabilitation
        '
        tbHabilitation.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbHabilitation.Size = New System.Drawing.Size(824, 781)
        '
        'tbConfiguration
        '
        tbConfiguration.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbConfiguration.Size = New System.Drawing.Size(824, 781)
        '
        'tbAlerts
        '
        tbAlerts.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbAlerts.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbAlerts.Size = New System.Drawing.Size(824, 781)
        '
        'tbRule
        '
        tbRule.Controls.Add(Label5)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(rbNotCurrentUser)
        tbRule.Controls.Add(rbNotAsignedTo)
        tbRule.Controls.Add(rbCurrentUser)
        tbRule.Controls.Add(rbAsignedTo)
        tbRule.Controls.Add(btQuitar)
        tbRule.Controls.Add(btGuardar)
        tbRule.Controls.Add(btAgregar)
        tbRule.Controls.Add(cboUsers)
        tbRule.Controls.Add(lstUser)
        tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbRule.Size = New System.Drawing.Size(880, 372)
        '
        'tbctrMain
        '
        tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbctrMain.Size = New System.Drawing.Size(888, 401)
        '
        'lstUser
        '
        lstUser.FormattingEnabled = True
        lstUser.ItemHeight = 16
        lstUser.Location = New System.Drawing.Point(315, 60)
        lstUser.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        lstUser.MultiColumn = True
        lstUser.Name = "lstUser"
        lstUser.SelectionMode = SelectionMode.MultiExtended
        lstUser.Size = New System.Drawing.Size(504, 260)
        lstUser.Sorted = True
        lstUser.TabIndex = 0
        '
        'cboUsers
        '
        cboUsers.FormattingEnabled = True
        cboUsers.Location = New System.Drawing.Point(315, 26)
        cboUsers.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        cboUsers.Name = "cboUsers"
        cboUsers.Size = New System.Drawing.Size(288, 24)
        cboUsers.Sorted = True
        cboUsers.TabIndex = 1
        '
        'btAgregar
        '
        btAgregar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btAgregar.FlatStyle = FlatStyle.Flat
        btAgregar.ForeColor = System.Drawing.Color.White
        btAgregar.Location = New System.Drawing.Point(611, 22)
        btAgregar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        btAgregar.Name = "btAgregar"
        btAgregar.Size = New System.Drawing.Size(100, 28)
        btAgregar.TabIndex = 2
        btAgregar.Text = "Agregar"
        btAgregar.UseVisualStyleBackColor = True
        '
        'btGuardar
        '
        btGuardar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btGuardar.FlatStyle = FlatStyle.Flat
        btGuardar.ForeColor = System.Drawing.Color.White
        btGuardar.Location = New System.Drawing.Point(705, 328)
        btGuardar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        btGuardar.Name = "btGuardar"
        btGuardar.Size = New System.Drawing.Size(100, 28)
        btGuardar.TabIndex = 5
        btGuardar.Text = "Guardar"
        btGuardar.UseVisualStyleBackColor = True
        '
        'btQuitar
        '
        btQuitar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btQuitar.FlatStyle = FlatStyle.Flat
        btQuitar.ForeColor = System.Drawing.Color.White
        btQuitar.Location = New System.Drawing.Point(719, 22)
        btQuitar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        btQuitar.Name = "btQuitar"
        btQuitar.Size = New System.Drawing.Size(100, 28)
        btQuitar.TabIndex = 6
        btQuitar.Text = "Quitar"
        btQuitar.UseVisualStyleBackColor = True
        '
        'rbAsignedTo
        '
        rbAsignedTo.AutoSize = True
        rbAsignedTo.BackColor = System.Drawing.Color.Transparent
        rbAsignedTo.Location = New System.Drawing.Point(43, 78)
        rbAsignedTo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        rbAsignedTo.Name = "rbAsignedTo"
        rbAsignedTo.Size = New System.Drawing.Size(207, 20)
        rbAsignedTo.TabIndex = 7
        rbAsignedTo.TabStop = True
        rbAsignedTo.Text = "Usuario Asignado a la tarea"
        rbAsignedTo.UseVisualStyleBackColor = False
        '
        'rbCurrentUser
        '
        rbCurrentUser.AutoSize = True
        rbCurrentUser.BackColor = System.Drawing.Color.Transparent
        rbCurrentUser.Location = New System.Drawing.Point(43, 135)
        rbCurrentUser.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        rbCurrentUser.Name = "rbCurrentUser"
        rbCurrentUser.Size = New System.Drawing.Size(121, 20)
        rbCurrentUser.TabIndex = 8
        rbCurrentUser.TabStop = True
        rbCurrentUser.Text = "Usuario Actual"
        rbCurrentUser.UseVisualStyleBackColor = False
        '
        'rbNotAsignedTo
        '
        rbNotAsignedTo.AutoSize = True
        rbNotAsignedTo.BackColor = System.Drawing.Color.Transparent
        rbNotAsignedTo.Location = New System.Drawing.Point(43, 107)
        rbNotAsignedTo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        rbNotAsignedTo.Name = "rbNotAsignedTo"
        rbNotAsignedTo.Size = New System.Drawing.Size(231, 20)
        rbNotAsignedTo.TabIndex = 9
        rbNotAsignedTo.TabStop = True
        rbNotAsignedTo.Text = "Usuario Asignado a la tarea NO"
        rbNotAsignedTo.UseVisualStyleBackColor = False
        '
        'rbNotCurrentUser
        '
        rbNotCurrentUser.AutoSize = True
        rbNotCurrentUser.BackColor = System.Drawing.Color.Transparent
        rbNotCurrentUser.Location = New System.Drawing.Point(43, 163)
        rbNotCurrentUser.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        rbNotCurrentUser.Name = "rbNotCurrentUser"
        rbNotCurrentUser.Size = New System.Drawing.Size(145, 20)
        rbNotCurrentUser.TabIndex = 10
        rbNotCurrentUser.TabStop = True
        rbNotCurrentUser.Text = "Usuario Actual NO"
        rbNotCurrentUser.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label5.Location = New System.Drawing.Point(37, 199)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(270, 16)
        Label5.TabIndex = 14
        Label5.Text = "es alguno de los siguientes usuarios"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(40, 34)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(117, 16)
        Label3.TabIndex = 13
        Label3.Text = "Verificar que el"
        '
        'UCIfUsers
        '
        AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Name = "UCIfUsers"
        Size = New System.Drawing.Size(888, 401)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub
    Friend WithEvents lstUser As ListBox
    Friend WithEvents cboUsers As ComboBox
    Friend WithEvents btAgregar As ZButton
    Friend WithEvents btGuardar As ZButton
    Friend WithEvents btQuitar As ZButton
    Friend WithEvents rbAsignedTo As System.Windows.Forms.RadioButton
    Friend WithEvents rbCurrentUser As System.Windows.Forms.RadioButton
    Friend WithEvents rbNotAsignedTo As System.Windows.Forms.RadioButton
    Friend WithEvents rbNotCurrentUser As System.Windows.Forms.RadioButton
#End Region
    Private _currentRule As IIfUsers
    Private _avaibleUsers As New Hashtable
    Private Property CurrentRule() As IIfUsers
        Get
            Return _currentRule
        End Get
        Set(ByVal value As IIfUsers)
            _currentRule = value
        End Set
    End Property
    Private Property AvaibleUsers() As Hashtable
        Get
            Return _avaibleUsers
        End Get
        Set(ByVal value As Hashtable)
            _avaibleUsers = value
        End Set
    End Property

    Public Sub New(ByRef rule As IIfUsers, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = rule
    End Sub
    Private Sub UCIfUser_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        LoadAsignedUsers()
        LoadComparator()
        LoadAvaibleUsers(True)
    End Sub
    Private Sub btGuardar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btGuardar.Click
        Try
            SaveComparator() 'Guardo valores en la regla

            Dim strBuilder As New StringBuilder 'Armo un string con los ids de Userlist separados por 
            For Each userid As Int64 In CurrentRule.UserIdsList
                strBuilder.Append(userid)
                strBuilder.Append("|")
            Next
            strBuilder.Remove(strBuilder.Length - 1, 1) 'saco el ultimo |

            'Guarda los valores en la BD
            WFRulesBusiness.UpdateParamItem(Rule, 0, strBuilder.ToString())
            WFRulesBusiness.UpdateParamItem(Rule, 1, CInt(CurrentRule.Comparator))
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub btAgregar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btAgregar.Click
        Try
            If Not IsNothing(cboUsers.SelectedItem()) AndAlso cboUsers.SelectedItem.ToString() <> "" Then
                Dim user As IUser = UserBusiness.GetUserByname(cboUsers.SelectedItem.ToString())
                CurrentRule.UserIdsList.Add(user.ID) 'Agrega al user de UserList
                lstUser.Items.Add(user.Name) 'Agrega al user de lstUser
                AvaibleUsers.Remove(user.ID)
                cboUsers.Items.Remove(user.Name) 'Saca al user a cbUsers

                If cboUsers.Items.Count > 0 AndAlso Not IsNothing(cboUsers.Items.Item(0)) Then
                    cboUsers.SelectedValue = cboUsers.Items.Item(0)
                    cboUsers.Text = cboUsers.Items.Item(0).ToString
                End If

                IsCboUsersEmpty()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub btQuitar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btQuitar.Click
        Try
            If lstUser.SelectedItems.Count > 0 Then
                Dim __userList As New List(Of User)

                For Each itemSelected As Object In lstUser.SelectedItems()
                    __userList.Add(UserBusiness.GetUserByname(itemSelected))
                Next

                For Each user As IUser In __userList
                    CurrentRule.UserIdsList.Remove(user.ID) 'Saca al user de UserList
                    cboUsers.Items.Add(user.Name) 'Agrega al user a cbUsers
                    AvaibleUsers.Add(user.ID, user)
                    lstUser.Items.Remove(user.Name) 'Saca al user de lstUser
                    If Not IsNothing(cboUsers.Items.Item(0)) Then
                        cboUsers.SelectedIndex = 0
                    End If
                Next

                If lstUser.Items.Count > 0 AndAlso Not IsNothing(lstUser.Items.Item(0)) Then
                    lstUser.SelectedItem = lstUser.Items.Item(0)
                End If
                IsLstUsersEmpty()
            Else
                MessageBox.Show("Seleccione un usuario de la lista")
            End If
            IsLstUsersEmpty()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub cboUsers_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cboUsers.TextChanged
        IsCboUsersEmpty()
    End Sub
    Private Sub lstUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstUser.SelectedIndexChanged
        IsLstUsersEmpty()
    End Sub

    ''' <summary>
    ''' Carga la lista de usuarios de la regla a lstUser
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAsignedUsers()
        Try
            lstUser.Items.Clear()
            For Each userid As Int64 In CurrentRule.UserIdsList
                lstUser.Items.Add(UserGroupBusiness.GetUserorGroupNamebyId(userid))
            Next
            If lstUser.Items.Count = 0 Then btQuitar.Enabled = False
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Carga los usuarios restantes (que no estan en la regla) en cboUser
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAvaibleUsers(LazyLoad As Boolean)
        Try
            For Each user As IUser In UserBusiness.GetUsersArrayList(LazyLoad)
                If Not CurrentRule.UserIdsList.Contains(user.ID) Then
                    cboUsers.Items.Add(user.Name)
                    AvaibleUsers.Add(user.ID, user)
                End If
            Next
            If cboUsers.Items.Count > 0 AndAlso Not IsNothing(cboUsers.Items.Item(0)) Then
                cboUsers.SelectedItem = cboUsers.Items.Item(0)
            ElseIf cboUsers.Items.Count = 0 Then
                btAgregar.Enabled = False
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Hace un checked = true al RadioButton acorde al Comparator de la regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadComparator()
        Try
            Select Case CurrentRule.Comparator
                Case UserComparators.AssignedTo
                    rbAsignedTo.Checked = True
                Case UserComparators.NotAsignedTo
                    rbNotAsignedTo.Checked = True
                Case UserComparators.CurrentUser
                    rbCurrentUser.Checked = True
                Case UserComparators.NotCurrentUser
                    rbNotCurrentUser.Checked = True
            End Select
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Carga a la regla el valor del Comparator
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveComparator()
        If rbAsignedTo.Checked Then
            CurrentRule.Comparator = UserComparators.AssignedTo
        ElseIf rbCurrentUser.Checked Then
            CurrentRule.Comparator = UserComparators.CurrentUser
        ElseIf rbNotAsignedTo.Checked Then
            CurrentRule.Comparator = UserComparators.NotAsignedTo
        ElseIf rbNotCurrentUser.Checked Then
            CurrentRule.Comparator = UserComparators.NotCurrentUser
        End If
    End Sub
    Private Sub IsCboUsersEmpty()
        If cboUsers.Items.Count > 0 Then
            btAgregar.Enabled = True
        Else
            btAgregar.Enabled = False
            cboUsers.Text = ""
        End If
    End Sub
    Private Sub IsLstUsersEmpty()
        If lstUser.Items.Count > 0 Then
            btQuitar.Enabled = True
        Else
            btQuitar.Enabled = False
        End If
    End Sub
End Class