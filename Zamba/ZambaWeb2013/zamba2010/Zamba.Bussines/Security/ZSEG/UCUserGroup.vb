Imports Zamba
Imports Zamba.Data
Imports System.Windows.Forms
Imports Zamba.core

Public Class UCUserGroup
    Inherits System.Windows.Forms.UserControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        LoadUserGroups()
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
        MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents LstuserGroup As System.Windows.Forms.ListView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LstuserGroup = New System.Windows.Forms.ListView
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'LstuserGroup
        '
        Me.LstuserGroup.BackColor = System.Drawing.Color.White
        Me.LstuserGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstuserGroup.Location = New System.Drawing.Point(0, 23)
        Me.LstuserGroup.Name = "LstuserGroup"
        Me.LstuserGroup.Size = New System.Drawing.Size(272, 289)
        Me.LstuserGroup.TabIndex = 0
        Me.LstuserGroup.View = System.Windows.Forms.View.List
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Grupos de Usuario"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UCUserGroup
        '
        Me.Controls.Add(Me.LstuserGroup)
        Me.Controls.Add(Me.Label1)
        Me.Name = "UCUserGroup"
        Me.Size = New System.Drawing.Size(272, 312)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public UserGroups As New ArrayList
    Public UserGroup As New UserGroup
    Private Index As Int32

    Public Event UserGroupSelected(ByVal UserGroupId As Int32, ByVal UserGroupName As String)
    Public Event UserGroupIndexSelected(ByVal Index As Int32)
    Public Event UserGroupError(ByVal ex As Exception)

    Private Sub LoadUserGroups()
        UserGroups.Clear()
        Dim UserGroupBusiness As New UserGroupBusiness
        UserGroups = UserGroupBusiness.GetAllGroupsArrayList
        Try
            Dim i As Int32
            For i = 0 To UserGroups.Count - 1
                Me.LstuserGroup.Items.Add(UserGroups(i).Name)
            Next
        Catch ex As Exception
            RaiseEvent UserGroupError(ex)
        End Try
    End Sub

    'Dim FlagEspecific As Boolean
    Private Sub LoadUserGroups(ByVal ObjectId As Integer, ByVal ObjectType As Zamba.ObjectTypes, ByVal RightType As Zamba.Core.RightsType, ByVal Value As Boolean)
        Try
            Dim UserGroupBusiness As New UserGroupBusiness
            'Me.FlagEspecific = True
            UserGroups.Clear()
            UserGroups = UserGroupBusiness.GetAllGroupsArrayList

            Dim i As Int32
            For i = 0 To UserGroups.Count - 1
                Me.LstuserGroup.Items.Add(UserGroups(i).Name)
            Next
        Catch ex As Exception
            RaiseEvent UserGroupError(ex)
        End Try
    End Sub

    Private Sub LoadUserGroups(ByVal UserGroups As ArrayList)
        Try
            'Me.FlagEspecific = True
            Me.UserGroups = UserGroups

            Dim i As Int32
            For i = 0 To Me.UserGroups.Count - 1
                Me.LstuserGroup.Items.Add(Me.UserGroups(i).Name)
            Next
        Catch ex As Exception
            RaiseEvent UserGroupError(ex)
        End Try
    End Sub
    Public Sub New(ByVal usergroups As ArrayList)
        Me.New()
        LoadUserGroups(usergroups)
    End Sub

    Public Sub New(ByVal ObjectId As Integer, ByVal ObjectType As Zamba.ObjectTypes, ByVal RightType As Zamba.Core.RightsType, ByVal Value As Boolean)
        Me.New()
        LoadUserGroups(ObjectId, ObjectType, RightType, Value)
    End Sub

    Private Sub UCUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadUserGroups()
    End Sub

    Private Sub LstuserGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstuserGroup.SelectedIndexChanged
        Index = Me.LstuserGroup.SelectedIndices(0)
        RaiseEvent UserGroupSelected(UserGroups(Index).ID, UserGroups(Index).Name)
        RaiseEvent UserGroupIndexSelected(Index)
    End Sub
End Class
