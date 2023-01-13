Imports Zamba.Core

Public Class WFRights
    Inherits zcontrol

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
    Friend WithEvents PanelFill As ZPanel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PanelRight As ZPanel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents PanelMain As System.Windows.Forms.Panel
    Friend WithEvents PanelTop As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        PanelMain = New System.Windows.Forms.Panel
        PanelFill = New ZPanel
        Panel5 = New System.Windows.Forms.Panel
        Panel3 = New System.Windows.Forms.Panel
        PanelRight = New ZPanel
        Panel4 = New System.Windows.Forms.Panel
        Panel2 = New System.Windows.Forms.Panel
        Panel6 = New System.Windows.Forms.Panel
        PanelTop = New ZLabel
        PanelMain.SuspendLayout()
        SuspendLayout()
        '
        'PanelMain
        '
        PanelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelMain.Controls.Add(PanelFill)
        PanelMain.Controls.Add(Panel5)
        PanelMain.Controls.Add(Panel3)
        PanelMain.Controls.Add(PanelRight)
        PanelMain.Controls.Add(Panel4)
        PanelMain.Controls.Add(Panel2)
        PanelMain.Controls.Add(Panel6)
        PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        PanelMain.Location = New System.Drawing.Point(0, 32)
        PanelMain.Name = "PanelMain"
        PanelMain.Size = New System.Drawing.Size(544, 467)
        PanelMain.TabIndex = 0
        '
        'PanelFill
        '
        PanelFill.Dock = System.Windows.Forms.DockStyle.Fill
        PanelFill.Location = New System.Drawing.Point(6, 6)
        PanelFill.Name = "PanelFill"
        PanelFill.Size = New System.Drawing.Size(342, 453)
        PanelFill.TabIndex = 10
        '
        'Panel5
        '
        Panel5.BackColor = System.Drawing.Color.Transparent
        Panel5.Dock = System.Windows.Forms.DockStyle.Right
        Panel5.Location = New System.Drawing.Point(348, 6)
        Panel5.Name = "Panel5"
        Panel5.Size = New System.Drawing.Size(6, 453)
        Panel5.TabIndex = 16
        '
        'Panel3
        '
        Panel3.BackColor = System.Drawing.Color.Transparent
        Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Panel3.Location = New System.Drawing.Point(0, 6)
        Panel3.Name = "Panel3"
        Panel3.Size = New System.Drawing.Size(6, 453)
        Panel3.TabIndex = 14
        '
        'PanelRight
        '
        PanelRight.Dock = System.Windows.Forms.DockStyle.Right
        PanelRight.Location = New System.Drawing.Point(354, 6)
        PanelRight.Name = "PanelRight"
        PanelRight.Size = New System.Drawing.Size(182, 453)
        PanelRight.TabIndex = 11
        '
        'Panel4
        '
        Panel4.BackColor = System.Drawing.Color.Transparent
        Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Panel4.Location = New System.Drawing.Point(536, 6)
        Panel4.Name = "Panel4"
        Panel4.Size = New System.Drawing.Size(6, 453)
        Panel4.TabIndex = 15
        '
        'Panel2
        '
        Panel2.BackColor = System.Drawing.Color.Transparent
        Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Panel2.Location = New System.Drawing.Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(542, 6)
        Panel2.TabIndex = 13
        '
        'Panel6
        '
        Panel6.BackColor = System.Drawing.Color.Transparent
        Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel6.Location = New System.Drawing.Point(0, 459)
        Panel6.Name = "Panel6"
        Panel6.Size = New System.Drawing.Size(542, 6)
        Panel6.TabIndex = 12
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.White
        PanelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.Font = New Font("Verdana", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        PanelTop.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        PanelTop.Location = New System.Drawing.Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(544, 32)
        PanelTop.TabIndex = 99
        PanelTop.Text = "  Permisos de "
        PanelTop.TextAlign = ContentAlignment.MiddleLeft
        '
        'WFRights
        '
        BackColor = System.Drawing.Color.White
        Controls.Add(PanelMain)
        Controls.Add(PanelTop)
        Name = "WFRights"
        Size = New System.Drawing.Size(544, 499)
        PanelMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        LoadControls()
        AddHandlers()
    End Sub

    Private _WFStep As WFStep
    Public Property WFStep() As WFStep
        Get
            Return _WFStep
        End Get
        Set(ByVal Value As WFStep)
            If IsNothing(Value) Then
                UserGroupSelector.Visible = False
                UCWfsteprights.Visible = False
            Else
                UserGroupSelector.Visible = True
                UCWfsteprights.Visible = False
                _WFStep = Value
                PanelTop.Text = " Permisos de " & _WFStep.Name
                RefreshStep()
            End If
        End Set
    End Property
    Private Sub RefreshStep()
        Try
            UserGroupSelector.RefreshSelectedUserGroups(WFStep)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#Region "Load"
    Dim UserGroupSelector As UsersGroupsSelector
    Dim UCWfsteprights As Zamba.AdminControls.UCWFStepsRights
    Private Sub LoadControls()
        Try
            UserGroupSelector = New UsersGroupsSelector
            UserGroupSelector.Dock = DockStyle.Fill
            UserGroupSelector.Visible = False
            PanelFill.Controls.Add(UserGroupSelector)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(EX)
        End Try
        Try
            UCWfsteprights = New Zamba.AdminControls.UCWFStepsRights
            UCWfsteprights.Dock = DockStyle.Fill
            UCWfsteprights.Visible = False
            PanelRight.Controls.Add(UCWfsteprights)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(EX)
        End Try
    End Sub
    Private Sub AddHandlers()
        RemoveHandler UserGroupSelector.UserGroupSelected, AddressOf userGroupselected
        RemoveHandler UserGroupSelector.UserSelected, AddressOf UserSelected
        RemoveHandler UserGroupSelector.NothingSelected, AddressOf NothingSelected
        RemoveHandler UserGroupSelector.SaveUserRight, AddressOf AsignUserRights
        RemoveHandler UserGroupSelector.SaveGroupRight, AddressOf AsignGroupRights
        RemoveHandler UserGroupSelector.RemoveAllGroupRights, AddressOf RemoveAllGroupRights
        RemoveHandler UserGroupSelector.RemoveAllUserRights, AddressOf RemoveAllUserRights

        AddHandler UserGroupSelector.UserGroupSelected, AddressOf userGroupselected
        AddHandler UserGroupSelector.UserSelected, AddressOf UserSelected
        AddHandler UserGroupSelector.NothingSelected, AddressOf NothingSelected
        AddHandler UserGroupSelector.SaveUserRight, AddressOf AsignUserRights
        AddHandler UserGroupSelector.SaveGroupRight, AddressOf AsignGroupRights
        AddHandler UserGroupSelector.RemoveAllGroupRights, AddressOf RemoveAllGroupRights
        AddHandler UserGroupSelector.RemoveAllUserRights, AddressOf RemoveAllUserRights
    End Sub
#End Region

#Region "Select"
    Private Sub UserSelected(ByVal userid As Int64)
        Try
            Dim user As IUser = UserBusiness.GetUserById(userid)
            UCWfsteprights.Visible = True
            UCWfsteprights.RefreshRights(user, WFStep.ID, True)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub userGroupselected(ByVal usergroupid As Int64)
        Try
            Dim usergroup As IUserGroup = UserGroupBusiness.GetUserGroupAsUserGroup(usergroupid)
            UCWfsteprights.Visible = True
            UCWfsteprights.RefreshRights(usergroup, WFStep.ID)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub NothingSelected()
        UCWfsteprights.Visible = False
    End Sub
#End Region

#Region "AsignRights"
    Private Sub AsignUserRights(ByVal userid As Int64, ByVal value As Boolean)
        Try
            If value Then
                UserBusiness.Rights.SetRight(userid, ObjectTypes.WFSteps, RightsType.Use, WFStep.ID, True)
            Else
                UserBusiness.Rights.SetRight(userid, ObjectTypes.WFSteps, RightsType.Use, WFStep.ID, False)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AsignGroupRights(ByVal groupid As Int64, ByVal value As Boolean)
        If value Then
            UserBusiness.Rights.SetRight(groupid, ObjectTypes.WFSteps, RightsType.Use, WFStep.ID, True)
        Else
            UserBusiness.Rights.SetRight(groupid, ObjectTypes.WFSteps, RightsType.Use, WFStep.ID, False)
        End If
    End Sub
    Private Sub RemoveAllUserRights(ByVal UserId As Int64)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.Use, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.Delegates, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.Terminar, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.Reject, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.Delete, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.ModificarVencimiento, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.VerAsignadosANadie, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.Iniciar, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.Asign, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.VerAsignadosAOtros, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserID, ObjectTypes.WFSteps, RightsType.UnAssign, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserId, ObjectTypes.WFSteps, RightsType.allowExecuteTasksAssignedToOtherUsers, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserId, ObjectTypes.WFSteps, RightsType.AllowExecuteAssociatedDocuments, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserId, ObjectTypes.WFSteps, RightsType.AllowStateComboBox, WFStep.ID, False)
    End Sub
    Private Sub RemoveAllGroupRights(ByVal UserGroupid As Int64)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Quitando permisos del grupo: " & UserGroupid)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.Use, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.Delegates, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.Terminar, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.Reject, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.Delete, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.ModificarVencimiento, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.VerAsignadosANadie, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.Iniciar, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.Asign, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.VerAsignadosAOtros, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.UnAssign, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.allowExecuteTasksAssignedToOtherUsers, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.AllowExecuteAssociatedDocuments, WFStep.ID, False)
        UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.WFSteps, RightsType.AllowStateComboBox, WFStep.ID, False)
    End Sub
#End Region

End Class
