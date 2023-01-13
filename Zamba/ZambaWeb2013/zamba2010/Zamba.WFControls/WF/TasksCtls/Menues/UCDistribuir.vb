Imports ZAMBA.Core
Public Class UCDistribuir
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
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents lvwSteps As System.Windows.Forms.ListView
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel1 = New ZPanel
        lvwSteps = New System.Windows.Forms.ListView
        BtnDerivar = New ZButton
        Label4 = New ZLabel
        Label1 = New ZLabel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.BackColor = System.Drawing.SystemColors.Control
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.Controls.Add(lvwSteps)
        Panel1.Controls.Add(BtnDerivar)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(216, 192)
        Panel1.TabIndex = 1
        '
        'lvwSteps
        '
        lvwSteps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lvwSteps.FullRowSelect = True
        lvwSteps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        lvwSteps.HideSelection = False
        lvwSteps.Location = New System.Drawing.Point(16, 40)
        lvwSteps.MultiSelect = False
        lvwSteps.Name = "lvwSteps"
        lvwSteps.Size = New System.Drawing.Size(176, 92)
        lvwSteps.Sorting = System.Windows.Forms.SortOrder.Ascending
        lvwSteps.TabIndex = 21
        lvwSteps.View = System.Windows.Forms.View.List
        '
        'BtnDerivar
        '
        BtnDerivar.BackColor = System.Drawing.Color.Transparent
        BtnDerivar.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        BtnDerivar.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        BtnDerivar.Location = New System.Drawing.Point(28, 144)
        BtnDerivar.Name = "BtnDerivar"
        BtnDerivar.Size = New System.Drawing.Size(144, 24)
        BtnDerivar.TabIndex = 20
        BtnDerivar.Text = "Distribuir"
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label4.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label4.Location = New System.Drawing.Point(16, 24)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(120, 17)
        Label4.TabIndex = 16
        Label4.Text = "Etapa"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Cursor = System.Windows.Forms.Cursors.Hand
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(196, 4)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(15, 19)
        Label1.TabIndex = 0
        Label1.Text = "X"
        '
        'UcDerivar
        '
        Controls.Add(Panel1)
        Name = "UcDerivar"
        Size = New System.Drawing.Size(216, 192)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Dim Result As TaskResult
    Sub New(ByRef Result As TaskResult)
        MyBase.New()
        InitializeComponent()
        Me.Result = Result
        LoadUcDerivar()
    End Sub

#Region "Load"
    Private Sub LoadUcDerivar()
        Try
            lvwSteps.Items.Clear()
            For Each s As WFStep In WFStepBusiness.GetStepsByWorkflow(Result.WorkId)
                lvwSteps.Items.Add(New StepItem(s))
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Class StepItem
        Inherits ListViewItem
        Public WFStep As WFStep
        Sub New(ByRef wfstep As WFStep)
            MyBase.new()
            Me.WFStep = WFStep
            Text = WFStep.Name
        End Sub
    End Class
    Private Class UserItem
        Inherits ListViewItem
        Public User As iuser
        Sub New(ByVal User As iuser)
            MyBase.new()
            Me.User = User
            Text = User.Name
        End Sub
    End Class
#End Region

#Region "Eventos"
    Public Event RefreshWf()
    Public Event Derivate(ByRef Result As TaskResult, ByVal NewWFStep As WFStep)
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnDerivar.Click
        Try
            If lvwSteps.SelectedItems.Count > 0 Then
                Dim NewWFStep As WFStep = DirectCast(lvwSteps.SelectedItems(0), StepItem).WFStep
                If Result.StepId <> NewWFStep.ID Then
                    RaiseEvent Derivate(Result, NewWFStep)
                End If

            End If
            RaiseEvent RefreshWf()
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Close"
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Label1.Click
        Try
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub UcDerivar_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Leave
        Try
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class
