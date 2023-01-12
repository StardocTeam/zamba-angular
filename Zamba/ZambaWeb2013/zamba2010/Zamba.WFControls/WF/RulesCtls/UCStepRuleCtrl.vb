
Imports zamba.Core
Public Class UCStepRuleCtrl
    Inherits zControl

#Region " Código generado por el Diseñador de Windows Forms "

    Private Sub New()
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
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Button2 As ZButton
    Friend WithEvents Button1 As ZButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents CboRules As ComboBox
    Friend WithEvents CboSSTats As ComboBox
    Friend WithEvents Nombre As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Descripcion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Help As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Tipo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UCStepRuleCtrl))
        Panel3 = New System.Windows.Forms.Panel
        CboSSTats = New ComboBox
        Button2 = New ZButton
        Button1 = New ZButton
        CboRules = New ComboBox
        Panel1 = New System.Windows.Forms.Panel
        DataGrid1 = New System.Windows.Forms.DataGrid
        DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Nombre = New System.Windows.Forms.DataGridTextBoxColumn
        Descripcion = New System.Windows.Forms.DataGridTextBoxColumn
        Help = New System.Windows.Forms.DataGridTextBoxColumn
        Tipo = New System.Windows.Forms.DataGridTextBoxColumn
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        CType(DataGrid1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'Panel3
        '
        Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel3.Controls.Add(CboSSTats)
        Panel3.Controls.Add(Button2)
        Panel3.Controls.Add(Button1)
        Panel3.Controls.Add(CboRules)
        Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Panel3.Location = New System.Drawing.Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New System.Drawing.Size(384, 64)
        Panel3.TabIndex = 5
        '
        'CboSSTats
        '
        CboSSTats.BackColor = System.Drawing.Color.White
        CboSSTats.Location = New System.Drawing.Point(192, 7)
        CboSSTats.Name = "CboSSTats"
        CboSSTats.Size = New System.Drawing.Size(160, 21)
        CboSSTats.TabIndex = 3
        CboSSTats.Text = "ComboBox2"
        '
        'Button2
        '
        Button2.Location = New System.Drawing.Point(112, 32)
        Button2.Name = "Button2"
        Button2.Size = New System.Drawing.Size(72, 24)
        Button2.TabIndex = 2
        Button2.Text = "Editar"
        '
        'Button1
        '
        Button1.Location = New System.Drawing.Point(16, 32)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(80, 24)
        Button1.TabIndex = 1
        Button1.Text = "Agregar"
        '
        'CboRules
        '
        CboRules.BackColor = System.Drawing.Color.White
        CboRules.Location = New System.Drawing.Point(16, 8)
        CboRules.Name = "CboRules"
        CboRules.Size = New System.Drawing.Size(168, 21)
        CboRules.TabIndex = 0
        CboRules.Text = "ComboBox1"
        '
        'Panel1
        '
        Panel1.Controls.Add(DataGrid1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 64)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(384, 128)
        Panel1.TabIndex = 6
        '
        'DataGrid1
        '
        DataGrid1.DataMember = ""
        DataGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        DataGrid1.Location = New System.Drawing.Point(0, 0)
        DataGrid1.Name = "DataGrid1"
        DataGrid1.Size = New System.Drawing.Size(384, 128)
        DataGrid1.TabIndex = 0
        DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        DataGridTableStyle1.DataGrid = DataGrid1
        DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {DataGridTextBoxColumn1, DataGridTextBoxColumn2})
        DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        DataGridTableStyle1.MappingName = "Rules"
        '
        'DataGridTextBoxColumn1
        '
        DataGridTextBoxColumn1.Format = ""
        DataGridTextBoxColumn1.FormatInfo = Nothing
        DataGridTextBoxColumn1.MappingName = "Name"
        DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        DataGridTextBoxColumn2.Format = ""
        DataGridTextBoxColumn2.FormatInfo = Nothing
        DataGridTextBoxColumn2.MappingName = "Description"
        DataGridTextBoxColumn2.Width = 75
        '
        'Nombre
        '
        Nombre.Format = ""
        Nombre.FormatInfo = Nothing
        Nombre.HeaderText = "Nombre"
        Nombre.MappingName = "Name"
        Nombre.Width = 75
        '
        'Descripcion
        '
        Descripcion.Format = ""
        Descripcion.FormatInfo = Nothing
        Descripcion.HeaderText = "Descripcion"
        Descripcion.MappingName = "Description"
        Descripcion.Width = 75
        '
        'Help
        '
        Help.Format = ""
        Help.FormatInfo = Nothing
        Help.HeaderText = "Help"
        Help.MappingName = "Help"
        Help.Width = 75
        '
        'Tipo
        '
        Tipo.Format = ""
        Tipo.FormatInfo = Nothing
        Tipo.HeaderText = "Tipo"
        Tipo.MappingName = "SStat"
        Tipo.Width = 75
        '
        'UCStepRuleCtrl
        '
        Controls.Add(Panel1)
        Controls.Add(Panel3)
        Name = "UCStepRuleCtrl"
        Size = New System.Drawing.Size(384, 192)
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        CType(DataGrid1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    'Dim WfStep As WFStep
    Dim StepTypes As New ArrayList
    'Dim userId As Int32
    Public Sub New(ByRef WFStep As WFStep, ByVal UserId As Int32)
        Me.New()
        'Me.WfStep = WFStep
        'Me.userid = UserId
    End Sub

    Private Sub FillRules()
        Dim PreRules As New ArrayList '= WFBusiness.GetExistingRules
        CboRules.DataSource = PreRules
    End Sub
    Private Shared Sub FillExistingRules()
        'WfStep.Rules = WFBusiness.GetRules(WfStep)
        'Me.DataGrid1.DataSource = WfStep.Rules
    End Sub
    Private Sub FillTypes()
        StepTypes.Add("Entrada")
        StepTypes.Add("Salida")
        StepTypes.Add("Actualizacion")
        StepTypes.Add("Accion de Usuario")
        StepTypes.Add("Planificada")
        CboSSTats.DataSource = StepTypes
    End Sub
    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button1.Click
        Try
            DataGrid1.Refresh()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Event EditRule()
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button2.Click
        Try
            If DataGrid1.CurrentRowIndex <> -1 Then
                RaiseEvent EditRule()
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub UCStepRuleCtrl_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            FillRules()
            FillTypes()
            FillExistingRules()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try

    End Sub

End Class
