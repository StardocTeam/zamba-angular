Imports Zamba.Core.WF.WF
Imports ZAMBA.Core

Public Class UCAsignar
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

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
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents BtnDerivar As ZButton
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents lvwUsers As System.Windows.Forms.ListView
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel1 = New ZPanel
        lvwUsers = New System.Windows.Forms.ListView
        BtnDerivar = New ZButton
        Label3 = New ZLabel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.BackColor = System.Drawing.SystemColors.Control
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.Controls.Add(lvwUsers)
        Panel1.Controls.Add(BtnDerivar)
        Panel1.Controls.Add(Label3)
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(311, 232)
        Panel1.TabIndex = 1
        '
        'lvwUsers
        '
        lvwUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lvwUsers.FullRowSelect = True
        lvwUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        lvwUsers.HideSelection = False
        lvwUsers.Location = New System.Drawing.Point(16, 40)
        lvwUsers.MultiSelect = False
        lvwUsers.Name = "lvwUsers"
        lvwUsers.Size = New System.Drawing.Size(278, 146)
        lvwUsers.Sorting = System.Windows.Forms.SortOrder.Ascending
        lvwUsers.TabIndex = 22
        lvwUsers.UseCompatibleStateImageBehavior = False
        lvwUsers.View = System.Windows.Forms.View.List
        '
        'BtnDerivar
        '
        BtnDerivar.BackColor = System.Drawing.Color.Transparent
        BtnDerivar.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        BtnDerivar.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        BtnDerivar.Location = New System.Drawing.Point(63, 192)
        BtnDerivar.Name = "BtnDerivar"
        BtnDerivar.Size = New System.Drawing.Size(195, 24)
        BtnDerivar.TabIndex = 20
        BtnDerivar.Text = "Asignar"
        BtnDerivar.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label3.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label3.Location = New System.Drawing.Point(16, 24)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(158, 17)
        Label3.TabIndex = 17
        Label3.Text = "Usuarios"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        '
        'UCAsignar
        '
        Controls.Add(Panel1)
        Name = "UCAsignar"
        Size = New System.Drawing.Size(354, 232)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Dim Result As ITaskResult

    Public Enum AsignTypes
        Asignar
        Desasignar
    End Enum
    Sub New(ByRef Result As TaskResult, ByVal AsignType As AsignTypes)
        MyBase.New()
        InitializeComponent()
        Try
            Me.Result = Result
            'Me.AsignType = AsignType
            LoadUCAsignar()
            Select Case AsignType
                Case AsignTypes.Asignar
                    BtnDerivar.Text = "Asignar"
                Case AsignTypes.Desasignar
                    BtnDerivar.Text = "Desasignar"
            End Select
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Load"
    Private Sub LoadUCAsignar()
        Dim users As Generic.List(Of IZBaseCore)
        Dim tempUsers As New ArrayList
        Dim totalUsers As New Dictionary(Of Int64, IUser)
        Dim userBusinessExt As UserBusinessExt
        Dim wfStepBusinessExt As WFStepBusinessExt
        Dim dtGroupIds As DataTable = Nothing

        Try
            'Se cargan los usuarios asignados a la etapa
            users = WFStepBusiness.GetStepUsersIdsAndNames(Result.StepId)
            If users IsNot Nothing Then
                For Each tempUser As IZBaseCore In users
                    If tempUser.ID > 0 AndAlso Not totalUsers.ContainsKey(tempUser.ID) Then
                        Dim user As New User(tempUser.ID)
                        user.Name = tempUser.Name
                        totalUsers.Add(user.ID, user)
                    End If
                Next
            End If

            'Se cargan los usuarios de los grupos asignados a la etapa
            wfStepBusinessExt = New WFStepBusinessExt
            dtGroupIds = wfStepBusinessExt.GetStepGroupIds(Result.StepId)
            If dtGroupIds IsNot Nothing AndAlso dtGroupIds.Rows.Count > 0 Then
                userBusinessExt = New UserBusinessExt
                For i As Int32 = 0 To dtGroupIds.Rows.Count - 1
                    tempUsers.Add(CLng(dtGroupIds.Rows(i)(0)))
                Next

                tempUsers = userBusinessExt.GetUserIdAndNameByGroupIds(tempUsers)
                For Each user As IUser In tempUsers
                    If user.ID > 0 AndAlso Not totalUsers.ContainsKey(user.ID) Then
                        totalUsers.Add(user.ID, user)
                    End If
                Next
            End If

            'Se agregan todos los usuarios obtenidos al ListView
            For Each user As IUser In totalUsers.Values
                Dim userItem As New ListViewItem()
                userItem.Name = user.ID.ToString
                userItem.Text = user.Name
                lvwUsers.Items.Add(userItem)
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            'No se hace Clear para no afectar por referencia a los grupos y usuarios
            users = Nothing
            tempUsers = Nothing
            totalUsers = Nothing
            wfStepBusinessExt = Nothing
            userBusinessExt = Nothing
            If dtGroupIds IsNot Nothing Then
                dtGroupIds.Dispose()
                dtGroupIds = Nothing
            End If
        End Try
    End Sub
#End Region

#Region "Eventos"

    Private Sub BtnDerivar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnDerivar.Click
        Try
            If lvwUsers.SelectedItems.Count > 0 Then
                'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
                WFTaskBusiness.Derivar(Result, Result.StepId, CLng(lvwUsers.SelectedItems(0).Name), lvwUsers.SelectedItems(0).Text, UserBusiness.Rights.CurrentUser.ID, Now, True, UserBusiness.Rights.CurrentUser.ID)
                UserBusiness.Rights.SaveAction(Result.ID, ObjectTypes.ModuleWorkFlow, RightsType.DerivateTask, "Usuario Derivo La tarea")
                DirectCast(Parent, Form).DialogResult = DialogResult.OK
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Close"
    'Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.Dispose()
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    End Try
    'End Sub
    Private Sub UcDerivar_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Leave
        Try
            Dispose()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class
