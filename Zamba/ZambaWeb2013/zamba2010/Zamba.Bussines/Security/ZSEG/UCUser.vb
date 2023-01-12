Imports Zamba
Imports Zamba.Data

Imports System.Windows.Forms
Imports Zamba.Core
Public Class UCUser
    Inherits System.Windows.Forms.UserControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

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
    Public WithEvents LstuserGroup As System.Windows.Forms.ListView
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
        Me.Label1.Text = "Usuarios"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UCUser
        '
        Me.Controls.Add(Me.LstuserGroup)
        Me.Controls.Add(Me.Label1)
        Me.Name = "UCUser"
        Me.Size = New System.Drawing.Size(272, 312)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public Users As New ArrayList
    '    Public User As New User


    Public Event UsersSelected(ByVal SelectedUsers As ArrayList)
    Public Event UserError(ByVal ex As Exception)

    Public SelectedUsers As ArrayList
    Private Sub LoadUsers()
        Try
            If Me.FlagEspecific = False Then
                Users.Clear()
                Dim UB As New UserBusiness

                Users = UB.GetUsersArrayList

                ' Dim i As Int32
                For Each u As iuser In Users
                    Dim item As New System.Windows.Forms.ListViewItem
                    item.Tag = u.id
                    item.Text = u.Name
                    Me.LstuserGroup.Items.Add(item)
                Next
            End If
        Catch ex As Exception
            RaiseEvent UserError(ex)
        End Try
    End Sub

    Dim FlagEspecific As Boolean
    Private Sub LoadUsers(ByVal ObjectId As Integer, ByVal ObjectType As Zamba.ObjectTypes, ByVal RightType As Zamba.Core.RightsType, ByVal Value As Boolean)
        Try
            Me.FlagEspecific = True
            Dim UB As New UserBusiness

            Users = UB.GetUsersArrayList
            For Each u As iuser In Users
                Dim item As New System.Windows.Forms.ListViewItem
                item.Tag = u.ID
                item.Text = u.Name
                Me.LstuserGroup.Items.Add(item)
            Next
        Catch ex As Exception
            RaiseEvent UserError(ex)
        End Try
    End Sub

    Private Sub LoadUsers(ByVal Users As ArrayList)
        Try
            Me.FlagEspecific = True
            Me.Users = Users

            '       Dim i As Int32
            For Each u As iuser In Users
                Dim item As New System.Windows.Forms.ListViewItem
                item.Tag = u.ID
                item.Text = u.Name
                Me.LstuserGroup.Items.Add(item)
            Next
        Catch ex As Exception
            RaiseEvent UserError(ex)
        End Try
    End Sub

    Public Sub New(ByVal ObjectId As Integer, ByVal ObjectType As Zamba.ObjectTypes, ByVal RightType As Zamba.Core.RightsType, ByVal Value As Boolean)
        Me.New()
        LoadUsers(ObjectId, ObjectType, RightType, Value)
    End Sub

    Public Sub New(ByVal users As ArrayList)
        Me.New()
        LoadUsers(users)
    End Sub
    Private Sub UCUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadUsers()
    End Sub

    Private Sub LstuserGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstuserGroup.SelectedIndexChanged
        Try
            If Me.LstuserGroup.SelectedIndices.Count > 0 Then
                Dim SelectedUsers As New ArrayList
                Dim i As Int32
                For i = 0 To Me.LstuserGroup.SelectedIndices.Count - 1
                    SelectedUsers.Add(Me.LstuserGroup.SelectedItems(i).Tag)
                Next
                Me.SelectedUsers = SelectedUsers
                RaiseEvent UsersSelected(SelectedUsers)
            End If
        Catch ex As Exception
            RaiseEvent UserError(ex)
        End Try
    End Sub
End Class
