Imports Zamba.Core

Public Class UsersGroupsSelector
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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


    Friend WithEvents Splitter1 As ZSplitter
    Friend WithEvents PanelGroups As ZPanel
    Friend WithEvents PanelUsers As ZPanel
    Friend WithEvents pnlGroup As ZPanel
    Friend WithEvents pnlUser As ZPanel
    Friend WithEvents Splitter2 As ZSplitter
    Friend WithEvents Splitter3 As ZSplitter
    Friend WithEvents pnlDisGroup As ZPanel
    Friend WithEvents pnlDisUser As ZPanel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents ZLabel1 As ZLabel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents ZLabel2 As ZLabel
    Friend WithEvents LstGroups As System.Windows.Forms.ListView
    Friend WithEvents LstDisGroups As System.Windows.Forms.ListView
    Friend WithEvents LstUsers As System.Windows.Forms.ListView
    Friend WithEvents LstDisUsers As System.Windows.Forms.ListView
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        PanelGroups = New ZPanel()
        pnlGroup = New ZPanel()
        LstGroups = New System.Windows.Forms.ListView()
        Label1 = New ZLabel()
        Splitter2 = New ZSplitter()
        pnlDisGroup = New ZPanel()
        LstDisGroups = New System.Windows.Forms.ListView()
        ZLabel2 = New ZLabel()
        PanelUsers = New ZPanel()
        pnlUser = New ZPanel()
        LstUsers = New System.Windows.Forms.ListView()
        Label2 = New ZLabel()
        Splitter3 = New ZSplitter()
        pnlDisUser = New ZPanel()
        LstDisUsers = New System.Windows.Forms.ListView()
        ZLabel1 = New ZLabel()
        Splitter1 = New ZSplitter()
        PanelGroups.SuspendLayout()
        pnlGroup.SuspendLayout()
        pnlDisGroup.SuspendLayout()
        PanelUsers.SuspendLayout()
        pnlUser.SuspendLayout()
        pnlDisUser.SuspendLayout()
        SuspendLayout()
        '
        'PanelGroups
        '
        PanelGroups.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        PanelGroups.Controls.Add(pnlGroup)
        PanelGroups.Controls.Add(Splitter2)
        PanelGroups.Controls.Add(pnlDisGroup)
        PanelGroups.Dock = System.Windows.Forms.DockStyle.Top
        PanelGroups.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        PanelGroups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        PanelGroups.Location = New System.Drawing.Point(2, 2)
        PanelGroups.Name = "PanelGroups"
        PanelGroups.Size = New System.Drawing.Size(444, 206)
        PanelGroups.TabIndex = 13
        '
        'pnlGroup
        '
        pnlGroup.BackColor = System.Drawing.Color.White
        pnlGroup.Controls.Add(LstGroups)
        pnlGroup.Controls.Add(Label1)
        pnlGroup.Dock = System.Windows.Forms.DockStyle.Fill
        pnlGroup.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        pnlGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        pnlGroup.Location = New System.Drawing.Point(230, 0)
        pnlGroup.Name = "pnlGroup"
        pnlGroup.Size = New System.Drawing.Size(214, 206)
        pnlGroup.TabIndex = 15
        '
        'LstGroups
        '
        LstGroups.BackColor = System.Drawing.Color.White
        LstGroups.BorderStyle = System.Windows.Forms.BorderStyle.None
        LstGroups.Dock = System.Windows.Forms.DockStyle.Fill
        LstGroups.FullRowSelect = True
        LstGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        LstGroups.Location = New System.Drawing.Point(0, 23)
        LstGroups.MultiSelect = False
        LstGroups.Name = "LstGroups"
        LstGroups.Size = New System.Drawing.Size(214, 183)
        LstGroups.Sorting = System.Windows.Forms.SortOrder.Ascending
        LstGroups.TabIndex = 15
        LstGroups.UseCompatibleStateImageBehavior = False
        LstGroups.View = System.Windows.Forms.View.Tile

        '
        'Label1
        '
        Label1.Dock = System.Windows.Forms.DockStyle.Top
        Label1.Font = AppBlock.ZambaUIHelpers.GetFontFamily
        Label1.ForeColor = AppBlock.ZambaUIHelpers.GetTitlesColor
        Label1.Location = New System.Drawing.Point(0, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(214, 23)
        Label1.TabIndex = 14
        Label1.Text = "Grupos"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Splitter2
        '
        Splitter2.BackColor = System.Drawing.Color.White
        Splitter2.Location = New System.Drawing.Point(224, 0)
        Splitter2.Name = "Splitter2"
        Splitter2.Size = New System.Drawing.Size(6, 206)
        Splitter2.TabIndex = 16
        Splitter2.TabStop = False
        '
        'pnlDisGroup
        '
        pnlDisGroup.BackColor = System.Drawing.Color.White
        pnlDisGroup.Controls.Add(LstDisGroups)
        pnlDisGroup.Controls.Add(ZLabel2)
        pnlDisGroup.Dock = System.Windows.Forms.DockStyle.Left
        pnlDisGroup.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        pnlDisGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        pnlDisGroup.Location = New System.Drawing.Point(0, 0)
        pnlDisGroup.Name = "pnlDisGroup"
        pnlDisGroup.Size = New System.Drawing.Size(224, 206)
        pnlDisGroup.TabIndex = 14
        '
        'LstDisGroups
        '
        LstDisGroups.BackColor = System.Drawing.Color.White
        LstDisGroups.BorderStyle = System.Windows.Forms.BorderStyle.None
        LstDisGroups.Dock = System.Windows.Forms.DockStyle.Fill
        LstDisGroups.ForeColor = System.Drawing.Color.Black
        LstDisGroups.FullRowSelect = True
        LstDisGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        LstDisGroups.Location = New System.Drawing.Point(0, 23)
        LstDisGroups.MultiSelect = False
        LstDisGroups.Name = "LstDisGroups"
        LstDisGroups.Size = New System.Drawing.Size(224, 183)
        LstDisGroups.Sorting = System.Windows.Forms.SortOrder.Ascending
        LstDisGroups.TabIndex = 16
        LstDisGroups.UseCompatibleStateImageBehavior = False
        LstDisGroups.View = System.Windows.Forms.View.Tile
        '
        'ZLabel2
        '
        ZLabel2.Dock = System.Windows.Forms.DockStyle.Top
        ZLabel2.Font = AppBlock.ZambaUIHelpers.GetFontFamily
        ZLabel2.ForeColor = AppBlock.ZambaUIHelpers.GetTitlesColor
        ZLabel2.Location = New System.Drawing.Point(0, 0)
        ZLabel2.Name = "ZLabel2"
        ZLabel2.Size = New System.Drawing.Size(224, 23)
        ZLabel2.TabIndex = 15
        ZLabel2.Text = "Grupos Disponibles"
        ZLabel2.TextAlign = ContentAlignment.MiddleLeft
        '
        'PanelUsers
        '
        PanelUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        PanelUsers.Controls.Add(pnlUser)
        PanelUsers.Controls.Add(Splitter3)
        PanelUsers.Controls.Add(pnlDisUser)
        PanelUsers.Dock = System.Windows.Forms.DockStyle.Fill
        PanelUsers.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        PanelUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        PanelUsers.Location = New System.Drawing.Point(2, 214)
        PanelUsers.Name = "PanelUsers"
        PanelUsers.Size = New System.Drawing.Size(444, 288)
        PanelUsers.TabIndex = 14
        '
        'pnlUser
        '
        pnlUser.BackColor = System.Drawing.Color.White
        pnlUser.Controls.Add(LstUsers)
        pnlUser.Controls.Add(Label2)
        pnlUser.Dock = System.Windows.Forms.DockStyle.Fill
        pnlUser.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        pnlUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        pnlUser.Location = New System.Drawing.Point(230, 0)
        pnlUser.Name = "pnlUser"
        pnlUser.Size = New System.Drawing.Size(214, 288)
        pnlUser.TabIndex = 17
        '
        'LstUsers
        '
        LstUsers.BackColor = System.Drawing.Color.White
        LstUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        LstUsers.Dock = System.Windows.Forms.DockStyle.Fill
        LstUsers.Font = New Font("Tahoma", 9.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        LstUsers.FullRowSelect = True
        LstUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        LstUsers.Location = New System.Drawing.Point(0, 23)
        LstUsers.MultiSelect = False
        LstUsers.Name = "LstUsers"
        LstUsers.Size = New System.Drawing.Size(214, 265)
        LstUsers.Sorting = System.Windows.Forms.SortOrder.Ascending
        LstUsers.TabIndex = 18
        LstUsers.UseCompatibleStateImageBehavior = False
        LstUsers.View = System.Windows.Forms.View.Tile
        '
        'Label2
        '
        Label2.Dock = System.Windows.Forms.DockStyle.Top
        Label2.Font = AppBlock.ZambaUIHelpers.GetFontFamily
        Label2.ForeColor = AppBlock.ZambaUIHelpers.GetTitlesColor
        Label2.Location = New System.Drawing.Point(0, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(214, 23)
        Label2.TabIndex = 17
        Label2.Text = "Usuarios"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'Splitter3
        '
        Splitter3.BackColor = System.Drawing.Color.White
        Splitter3.Location = New System.Drawing.Point(224, 0)
        Splitter3.Name = "Splitter3"
        Splitter3.Size = New System.Drawing.Size(6, 288)
        Splitter3.TabIndex = 19
        Splitter3.TabStop = False
        '
        'pnlDisUser
        '
        pnlDisUser.BackColor = System.Drawing.Color.White
        pnlDisUser.Controls.Add(LstDisUsers)
        pnlDisUser.Controls.Add(ZLabel1)
        pnlDisUser.Dock = System.Windows.Forms.DockStyle.Left
        pnlDisUser.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        pnlDisUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        pnlDisUser.Location = New System.Drawing.Point(0, 0)
        pnlDisUser.Name = "pnlDisUser"
        pnlDisUser.Size = New System.Drawing.Size(224, 288)
        pnlDisUser.TabIndex = 18
        '
        'LstDisUsers
        '
        LstDisUsers.BackColor = System.Drawing.Color.White
        LstDisUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        LstDisUsers.Dock = System.Windows.Forms.DockStyle.Fill
        LstDisUsers.FullRowSelect = True
        LstDisUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        LstDisUsers.Location = New System.Drawing.Point(0, 23)
        LstDisUsers.MultiSelect = False
        LstDisUsers.Name = "LstDisUsers"
        LstDisUsers.Size = New System.Drawing.Size(224, 265)
        LstDisUsers.Sorting = System.Windows.Forms.SortOrder.Ascending
        LstDisUsers.TabIndex = 19
        LstDisUsers.UseCompatibleStateImageBehavior = False
        LstDisUsers.View = System.Windows.Forms.View.Tile
        '
        'ZLabel1
        '
        ZLabel1.Dock = System.Windows.Forms.DockStyle.Top
        ZLabel1.Font = AppBlock.ZambaUIHelpers.GetFontFamily
        ZLabel1.ForeColor = AppBlock.ZambaUIHelpers.GetTitlesColor
        ZLabel1.Location = New System.Drawing.Point(0, 0)
        ZLabel1.Name = "ZLabel1"
        ZLabel1.Size = New System.Drawing.Size(224, 23)
        ZLabel1.TabIndex = 18
        ZLabel1.Text = "Usuarios Disponibles"
        ZLabel1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Splitter1
        '
        Splitter1.BackColor = System.Drawing.Color.White
        Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Splitter1.Location = New System.Drawing.Point(2, 208)
        Splitter1.Name = "Splitter1"
        Splitter1.Size = New System.Drawing.Size(444, 6)
        Splitter1.TabIndex = 15
        Splitter1.TabStop = False
        '
        'UsersGroupsSelector
        '
        BackColor = System.Drawing.Color.White
        Controls.Add(PanelUsers)
        Controls.Add(Splitter1)
        Controls.Add(PanelGroups)
        Name = "UsersGroupsSelector"
        Padding = New System.Windows.Forms.Padding(2)
        Size = New System.Drawing.Size(448, 504)
        PanelGroups.ResumeLayout(False)
        pnlGroup.ResumeLayout(False)
        pnlDisGroup.ResumeLayout(False)
        PanelUsers.ResumeLayout(False)
        pnlUser.ResumeLayout(False)
        pnlDisUser.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Private WFStep As WFStep
    Private Groups As SortedList
    Private Users As SortedList

#Region "Refresh Rights"
    Public Sub RefreshSelectedUserGroups(ByVal wfstep As WFStep)
        Me.WFStep = wfstep
        FillUsersGroups()
    End Sub
    Private Sub FillUsersGroups()
        'grupos
        Groups = DirectCast(WFBusiness.GetAllGroups.Clone, SortedList)
        FilterGroups()
        LoadGroups()

        'usuarios
        Users = DirectCast(WFBusiness.GetAllUsers.Clone, SortedList)
        FilterUsers()
        LoadUsers(False)
    End Sub
    Private Sub FilterGroups()
        Try
            Dim WFStepUserGroupsIdsAndNames As Generic.List(Of IZBaseCore) = WFStepBusiness.GetStepUserGroupsIdsAndNames(WFStep.ID, True)
            For Each g As IZBaseCore In WFStepUserGroupsIdsAndNames
                Groups.Remove(g.ID)
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub FilterUsers()
        Try
            Dim WFStepUsersIdsAndNames As Generic.List(Of IZBaseCore) = WFStepBusiness.GetStepUsersIdsAndNames(WFStep.ID)
            For Each u As IZBaseCore In WFStepUsersIdsAndNames
                Users.Remove(u.ID)
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LoadGroups()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargando grupos")
            LstGroups.BeginUpdate()
            LstGroups.Clear()
            Dim WFStepUserGroupsIdsAndNames As Generic.List(Of IZBaseCore) = WFStepBusiness.GetStepUserGroupsIdsAndNames(WFStep.ID, True)

            For Each g As IZBaseCore In WFStepUserGroupsIdsAndNames
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando grupo: " & g.ID)
                LstGroups.Items.Add(New GroupItem(g))
            Next
            LstGroups.EndUpdate()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            LstDisGroups.BeginUpdate()
            LstDisGroups.Clear()
            For Each g As IZBaseCore In Groups.Values
                LstDisGroups.Items.Add(New GroupItem(g))
            Next
            LstDisGroups.EndUpdate()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LoadUsers(ByVal reloadfromDb As Boolean)
        Try
            LstUsers.BeginUpdate()
            LstUsers.Clear()
            Dim WFStepUsersIdsAndNames As Generic.List(Of IZBaseCore) = WFStepBusiness.GetStepUsersIdsAndNames(WFStep.ID, reloadfromDb)

            For Each u As IZBaseCore In WFStepUsersIdsAndNames
                If Not u.ID = 0 Then LstUsers.Items.Add(New UserItem(u))
            Next
            LstUsers.EndUpdate()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            LstDisUsers.BeginUpdate()
            LstDisUsers.Clear()
            For Each u As IZBaseCore In Users.Values
                LstDisUsers.Items.Add(New UserItem(u))
            Next
            LstDisUsers.EndUpdate()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "ListViewItems"
    Private Class GroupItem
        Inherits ListViewItem
        Private _group As IZBaseCore
        Public Overloads Property Group() As IZBaseCore
            Get
                Return _group
            End Get
            Set(ByVal value As IZBaseCore)
                _group = value
            End Set
        End Property

        Sub New(ByVal g As IZBaseCore)
            Group = g
            Text = g.Name
        End Sub
    End Class
    Private Class UserItem
        Inherits ListViewItem
        Public user As IZBaseCore
        Sub New(ByVal u As IZBaseCore)
            user = u
            Text = u.Name
        End Sub
    End Class
#End Region

#Region "Sort Arrays"
    'Private Sub sortGroups(ByVal a As ArrayList)
    '    Dim aux as iusergroup
    '    Dim i, j As Int32
    '    For i = 0 To a.Count - 2
    '        For j = i + 1 To a.Count - 1
    '            If a(i).Name > a(j).Name Then
    '                aux = a(i)
    '                a(i) = a(j)
    '                a(j) = aux
    '            End If
    '        Next
    '    Next
    'End Sub
    'Private Sub sortUsers(ByVal a As ArrayList)
    '    Dim aux as iuser
    '    Dim i, j As Int32
    '    For i = 0 To a.Count - 2
    '        For j = i + 1 To a.Count - 1
    '            If a(i).Name > a(j).Name Then
    '                aux = a(i)
    '                a(i) = a(j)
    '                a(j) = aux
    '            End If
    '        Next
    '    Next
    'End Sub
#End Region

#Region "DoubleClick"
    Public Event SaveUserRight(ByVal userid As Int64, ByVal value As Boolean)
    Public Event SaveGroupRight(ByVal groupid As Int64, ByVal value As Boolean)
    Public Event RemoveAllGroupRights(ByVal groupid As Int64)
    Public Event RemoveAllUserRights(ByVal Userid As Int64)

    Private Sub LstGroups_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles LstGroups.DoubleClick
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Quitando grupo")
        DeleteGroup()
    End Sub
    Private Sub DeleteGroup()
        Try
            If LstGroups.SelectedItems.Count > 0 Then
                Dim item As GroupItem = DirectCast(LstGroups.SelectedItems(0), GroupItem)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Quitando grupo: " & item.Group.ID)
                RaiseEvent RemoveAllGroupRights(item.Group.ID)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Quitando del listado de grupos")
                If Not Groups.Contains(item.Group.ID) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Grupo removido")
                    Groups.Add(item.Group.ID, item.Group)
                End If
                LoadGroups()
                ListLostFocus()
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al quitar grupo: " & ex.ToString())
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LstUsers_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles LstUsers.DoubleClick
        DeleteUser()
    End Sub
    Private Sub DeleteUser()
        Try
            If LstUsers.SelectedItems.Count > 0 Then
                Dim item As UserItem = DirectCast(LstUsers.SelectedItems(0), UserItem)
                RaiseEvent RemoveAllUserRights(item.user.ID)
                Dim WFStepUsersIdsAndNames As Generic.List(Of IZBaseCore) = WFStepBusiness.GetStepUsersIdsAndNames(WFStep.ID, False)
                If WFStepUsersIdsAndNames.Contains(item.user) Then
                    WFStepUsersIdsAndNames.Remove(item.user)
                End If

                If Not Users.Contains(item.user.ID) Then
                    Users.Add(item.user.ID, item.user)
                    'Me.sortUsers(Users)
                End If

                LoadUsers(False)
                ListLostFocus()

            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LstDisGroups_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles LstDisGroups.DoubleClick
        addUserGroup()
    End Sub
    Private Sub addUserGroup()
        Try
            If LstDisGroups.SelectedItems.Count > 0 Then
                Dim item As GroupItem = DirectCast(LstDisGroups.SelectedItems(0), GroupItem)
                RaiseEvent SaveGroupRight(item.Group.ID, True)
                If Groups.Contains(item.Group.ID) Then
                    Groups.Remove(item.Group.ID)
                End If

                'Dim WFStepUserGroupsIdsAndNames As Generic.List(Of IZBaseCore) = WFStepBusiness.GetStepUserGroupsIdsAndNames(Me.WFStep.ID, True)
                'If Not WFStepUserGroupsIdsAndNames.Contains(item.Group) Then
                'WFStepUserGroupsIdsAndNames.Add(item.Group)
                'Me.sortGroups(SelectedGroups)
                'End If
                LoadGroups()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LstDisUsers_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles LstDisUsers.DoubleClick
        addUser()
    End Sub



    Private Sub addUser()
        Try
            If LstDisUsers.SelectedItems.Count > 0 Then
                Dim item As UserItem = DirectCast(LstDisUsers.SelectedItems(0), UserItem)
                RaiseEvent SaveUserRight(item.user.ID, True)
                If Users.Contains(item.user.ID) Then
                    Users.Remove(item.user.ID)
                End If

                Dim WFStepUsersIdsAndNames As Generic.List(Of IZBaseCore) = WFStepBusiness.GetStepUsersIdsAndNames(WFStep.ID, False)
                If Not WFStepUsersIdsAndNames.Contains(item.user) Then
                    WFStepUsersIdsAndNames.Add(item.user)
                    'Me.sortUsers(SelectedUsers)
                End If

                LoadUsers(False)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Select"
    Public Event UserGroupSelected(ByVal UserGroupId As Int64)
    Public Event UserSelected(ByVal UserId As Int64)
    Public Event NothingSelected()
    Private Sub LstGroups_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LstGroups.Click
        Try
            If LstGroups.SelectedItems.Count > 0 Then
                LstGroups.HideSelection = False
                Dim item As GroupItem = DirectCast(LstGroups.SelectedItems(0), GroupItem)

                RaiseEvent UserGroupSelected(item.Group.ID)
            Else
                ListLostFocus()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LstUsers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LstUsers.Click
        Try
            If LstUsers.SelectedItems.Count > 0 Then
                LstUsers.HideSelection = False
                Dim item As UserItem = DirectCast(LstUsers.SelectedItems(0), UserItem)
                RaiseEvent UserSelected(item.user.ID)
            Else
                ListLostFocus()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LstDisUsers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LstDisUsers.Click
        Try
            ListLostFocus()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LstDisGroups_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LstDisGroups.Click
        Try
            ListLostFocus()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LstUsers_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles LstUsers.SelectedIndexChanged
        Try
            ListLostFocus()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LstGroups_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles LstGroups.SelectedIndexChanged
        Try
            ListLostFocus()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ListLostFocus()
        LstUsers.HideSelection = True
        LstGroups.HideSelection = True
        RaiseEvent NothingSelected()
    End Sub
#End Region

End Class