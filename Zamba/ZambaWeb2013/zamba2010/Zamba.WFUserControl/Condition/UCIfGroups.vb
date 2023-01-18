'Imports Zamba.WFuserBusiness
Imports System.Text

''' <summary>
'''      Formulario Utilizado para validar por grupo de usuarios, solo estaba
'''      la regla que validaba por usuario y no por grupo
''' </summary>
''' <remarks></remarks>

Public Class UCIfGroups
    Inherits ZRuleControl

#Region "Diseño"
    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <DebuggerStepThrough()> _
    Private Overloads Sub InitializeComponent()
        lstGroup = New ListBox()
        cboGroups = New ComboBox()
        btAgregar = New ZButton()
        btGuardar = New ZButton()
        btQuitar = New ZButton()
        rbAsignedTo = New System.Windows.Forms.RadioButton()
        rbCurrentUser = New System.Windows.Forms.RadioButton()
        rbNotAsignedTo = New System.Windows.Forms.RadioButton()
        rbNotCurrentUser = New System.Windows.Forms.RadioButton()
        Label3 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
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
        tbRule.Controls.Add(cboGroups)
        tbRule.Controls.Add(lstGroup)
        tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbRule.Size = New System.Drawing.Size(880, 372)
        '
        'tbctrMain
        '
        tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        tbctrMain.Size = New System.Drawing.Size(888, 401)
        '
        'lstGroup
        '
        lstGroup.FormattingEnabled = True
        lstGroup.ItemHeight = 16
        lstGroup.Location = New System.Drawing.Point(314, 60)
        lstGroup.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        lstGroup.MultiColumn = True
        lstGroup.Name = "lstGroup"
        lstGroup.SelectionMode = SelectionMode.MultiExtended
        lstGroup.Size = New System.Drawing.Size(533, 260)
        lstGroup.Sorted = True
        lstGroup.TabIndex = 0
        '
        'cboGroups
        '
        cboGroups.DropDownStyle = ComboBoxStyle.DropDownList
        cboGroups.FormattingEnabled = True
        cboGroups.Location = New System.Drawing.Point(314, 28)
        cboGroups.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        cboGroups.Name = "cboGroups"
        cboGroups.Size = New System.Drawing.Size(300, 24)
        cboGroups.Sorted = True
        cboGroups.TabIndex = 1
        '
        'btAgregar
        '
        btAgregar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btAgregar.FlatStyle = FlatStyle.Flat
        btAgregar.ForeColor = System.Drawing.Color.White
        btAgregar.Location = New System.Drawing.Point(628, 28)
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
        btGuardar.Location = New System.Drawing.Point(747, 328)
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
        btQuitar.Location = New System.Drawing.Point(747, 28)
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
        rbAsignedTo.Location = New System.Drawing.Point(42, 60)
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
        rbCurrentUser.Location = New System.Drawing.Point(42, 117)
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
        rbNotAsignedTo.Location = New System.Drawing.Point(42, 89)
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
        rbNotCurrentUser.Location = New System.Drawing.Point(42, 145)
        rbNotCurrentUser.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        rbNotCurrentUser.Name = "rbNotCurrentUser"
        rbNotCurrentUser.Size = New System.Drawing.Size(145, 20)
        rbNotCurrentUser.TabIndex = 10
        rbNotCurrentUser.TabStop = True
        rbNotCurrentUser.Text = "Usuario Actual NO"
        rbNotCurrentUser.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(42, 28)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(117, 16)
        Label3.TabIndex = 11
        Label3.Text = "Verificar que el"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label5.Location = New System.Drawing.Point(39, 193)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(253, 16)
        Label5.TabIndex = 12
        Label5.Text = "pertenece a los siguientes grupos"
        '
        'UCIfGroups
        '
        AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Name = "UCIfGroups"
        Size = New System.Drawing.Size(888, 401)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub
    Friend WithEvents lstGroup As ListBox
    Friend WithEvents cboGroups As ComboBox
    Friend WithEvents btAgregar As ZButton
    Friend WithEvents btGuardar As ZButton
    Friend WithEvents btQuitar As ZButton
    Friend WithEvents rbAsignedTo As System.Windows.Forms.RadioButton
    Friend WithEvents rbCurrentUser As System.Windows.Forms.RadioButton
    Friend WithEvents rbNotAsignedTo As System.Windows.Forms.RadioButton
    Friend WithEvents rbNotCurrentUser As System.Windows.Forms.RadioButton
#End Region
    Private _currentRule As IIfGroups
    Private _AvaibleGroups As New Hashtable
    Private Property CurrentRule() As IIfGroups
        Get
            Return _currentRule
        End Get
        Set(ByVal value As IIfGroups)
            _currentRule = value
        End Set
    End Property
    Private Property AvaibleGroups() As Hashtable
        Get
            Return _AvaibleGroups
        End Get
        Set(ByVal value As Hashtable)
            _AvaibleGroups = value
        End Set
    End Property

    Public Sub New(ByRef rule As IIfGroups, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = rule
    End Sub
    Private Sub UCIfUser_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        LoadAsignedGroups()
        LoadComparator()
        LoadAvaibleGroups()
    End Sub
    Private Sub btGuardar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btGuardar.Click
        Try
            SaveComparator() 'Guardo valores en la regla

            Dim strBuilder As New StringBuilder 'Armo un string con los ids de Grouplist separados por 
            For Each GroupID As Int64 In CurrentRule.GroupList
                strBuilder.Append(GroupID)
                strBuilder.Append("|")
            Next
            If strBuilder.Length > 0 Then
                strBuilder.Remove(strBuilder.Length - 1, 1) 'saco el ultimo |
            End If

            'Guarda los valores en la BD
            WFRulesBusiness.UpdateParamItem(Rule, 0, strBuilder.ToString())
            WFRulesBusiness.UpdateParamItem(Rule, 1, CInt(CurrentRule.Comparator))
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub btAgregar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btAgregar.Click
        Try
            If Not IsNothing(cboGroups.SelectedItem()) AndAlso cboGroups.SelectedItem.ToString() <> "" Then
                Dim Group As IUserGroup = cboGroups.SelectedItem
                CurrentRule.GroupList.Add(Group.ID) 'Agrega al GRUPO de groupList
                lstGroup.Items.Add(Group.Name) 'Agrega al grupo de lstGroup
                AvaibleGroups.Remove(Group.ID)
                cboGroups.Items.Remove(Group) 'Saca al grupo a cbgroup

                If cboGroups.Items.Count > 0 AndAlso Not IsNothing(cboGroups.Items.Item(0)) Then
                    cboGroups.SelectedText = DirectCast(cboGroups.Items(0), UserGroup).Name
                    cboGroups.Text = DirectCast(cboGroups.Items(0), UserGroup).Name
                End If

                IsCboUsersEmpty()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub btQuitar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btQuitar.Click
        Try
            If lstGroup.SelectedItems.Count > 0 Then
                Dim ItemsToRemove As New List(Of String)
                Dim groupid As Int32
                For Each itemSelected As Object In lstGroup.SelectedItems()
                    ItemsToRemove.Add(itemSelected.ToString)
                    groupid = UserGroupBusiness.GetGroupIdByName(itemSelected.ToString)
                    CurrentRule.GroupList.Remove(Int64.Parse(groupid.ToString)) 'Saca al grupo de lstgroup


                    For Each groups As IUserGroup In UserGroupBusiness.GetAllGroupsArrayList()
                        If groups.ID = groupid Then
                            cboGroups.Items.Add(groups)
                            cboGroups.DisplayMember = "Name"
                            AvaibleGroups.Add(groups.ID, groups)
                            Exit For
                        End If
                    Next


                    If Not IsNothing(cboGroups.Items.Item(0)) Then
                        cboGroups.SelectedIndex = 0
                    End If



                Next

                For Each Item As String In ItemsToRemove
                    lstGroup.Items.Remove(Item) 'Saca al grupo de lstgroup
                Next
                If lstGroup.Items.Count > 0 AndAlso Not IsNothing(lstGroup.Items.Item(0)) Then
                    lstGroup.SelectedItem = lstGroup.Items.Item(0)
                End If
                IsLstUsersEmpty()

            Else
                MessageBox.Show("Seleccione un grupo de la lista")
            End If
            IsLstUsersEmpty()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub cboUsers_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cboGroups.TextChanged
        IsCboUsersEmpty()
    End Sub
    Private Sub lstUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstGroup.SelectedIndexChanged
        IsLstUsersEmpty()
    End Sub

    ''' <summary>
    ''' Carga la lista de Grupos de la regla a lstGroups
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAsignedGroups()
        Try
            lstGroup.Items.Clear()
            For Each GroupID As Int64 In CurrentRule.GroupList
                Dim name As String = UserGroupBusiness.GetUserorGroupNamebyId(GroupID)
                If String.IsNullOrEmpty(name) = False Then
                    lstGroup.Items.Add(name)
                End If
            Next
            If lstGroup.Items.Count = 0 Then btQuitar.Enabled = False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Carga los usuarios restantes (que no estan en la regla) en cboUser
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAvaibleGroups()
        Try
            For Each groups As IUserGroup In UserGroupBusiness.GetAllGroupsArrayList()
                If Not CurrentRule.GroupList.Contains(groups.ID) Then
                    cboGroups.Items.Add(groups)
                    cboGroups.DisplayMember = "Name"
                    AvaibleGroups.Add(groups.ID, groups)
                End If
            Next
            If cboGroups.Items.Count > 0 AndAlso Not IsNothing(cboGroups.Items.Item(0)) Then
                cboGroups.SelectedItem = cboGroups.Items.Item(0)
            ElseIf cboGroups.Items.Count = 0 Then
                btAgregar.Enabled = False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
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
            ZClass.raiseerror(ex)
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
        If cboGroups.Items.Count > 0 Then
            btAgregar.Enabled = True
        Else
            btAgregar.Enabled = False
            cboGroups.Text = ""
        End If
    End Sub
    Private Sub IsLstUsersEmpty()
        If lstGroup.Items.Count > 0 Then
            btQuitar.Enabled = True
        Else
            btQuitar.Enabled = False
        End If
    End Sub
End Class