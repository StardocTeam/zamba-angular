Public Class UCRuleCtrl
    Inherits Zamba.AppBlock.ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    'Public Sub New()
    '    MyBase.New()

    '    'El Diseñador de Windows Forms requiere esta llamada.
    '    InitializeComponent()

    '    'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    'End Sub

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

    Public WithEvents TxtName As TextBox
    Public WithEvents TxtDescription As TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Label3 As ZLabel
    Public WithEvents NumRefreshRate As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As ZLabel
    Public WithEvents txtId As ZLabel
    Friend WithEvents Label8 As ZLabel

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents BtnSave As ZButton
    Public WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ChkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents CboSSTats As ComboBox
    Friend WithEvents Tipo As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        TxtName = New TextBox
        TxtDescription = New TextBox
        Label2 = New ZLabel
        Label3 = New ZLabel
        NumRefreshRate = New System.Windows.Forms.NumericUpDown
        Label4 = New ZLabel
        txtId = New ZLabel
        Label8 = New ZLabel
        BtnSave = New ZButton
        TextBox1 = New TextBox
        Label1 = New ZLabel
        Panel2 = New System.Windows.Forms.Panel
        CboSSTats = New ComboBox
        Tipo = New ZLabel
        ChkEnabled = New System.Windows.Forms.CheckBox
        CType(NumRefreshRate, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        'TxtName
        '
        TxtName.AutoSize = False
        TxtName.BackColor = System.Drawing.Color.White
        TxtName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        TxtName.Location = New System.Drawing.Point(64, 8)
        TxtName.MaxLength = 0
        TxtName.Name = "TxtName"
        TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        TxtName.Size = New System.Drawing.Size(176, 22)
        TxtName.TabIndex = 0
        TxtName.Text = ""
        TxtName.WordWrap = False
        '
        'TxtDescription
        '
        TxtDescription.AutoSize = False
        TxtDescription.BackColor = System.Drawing.Color.White
        TxtDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl
        TxtDescription.Location = New System.Drawing.Point(64, 32)
        TxtDescription.MaxLength = 0
        TxtDescription.Name = "TxtDescription"
        TxtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        TxtDescription.Size = New System.Drawing.Size(176, 32)
        TxtDescription.TabIndex = 0
        TxtDescription.Text = ""
        TxtDescription.WordWrap = False
        '
        'Label2
        '
        Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label2.Location = New System.Drawing.Point(248, 16)
        Label2.Name = "Label2"
        Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label2.Size = New System.Drawing.Size(16, 16)
        Label2.TabIndex = 0
        Label2.Text = "Id"
        '
        'Label3
        '
        Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label3.Location = New System.Drawing.Point(8, 16)
        Label3.Name = "Label3"
        Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label3.Size = New System.Drawing.Size(48, 16)
        Label3.TabIndex = 0
        Label3.Text = "Nombre"
        '
        'NumRefreshRate
        '
        NumRefreshRate.BackColor = System.Drawing.Color.White
        NumRefreshRate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        NumRefreshRate.Location = New System.Drawing.Point(248, 88)
        NumRefreshRate.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        NumRefreshRate.Name = "NumRefreshRate"
        NumRefreshRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        NumRefreshRate.Size = New System.Drawing.Size(48, 20)
        NumRefreshRate.TabIndex = 0
        NumRefreshRate.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label4
        '
        Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label4.Location = New System.Drawing.Point(248, 72)
        Label4.Name = "Label4"
        Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label4.Size = New System.Drawing.Size(48, 24)
        Label4.TabIndex = 0
        Label4.Text = "Intervalo"
        '
        'txtId
        '
        txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtId.ImeMode = System.Windows.Forms.ImeMode.NoControl
        txtId.Location = New System.Drawing.Point(272, 16)
        txtId.Name = "txtId"
        txtId.RightToLeft = System.Windows.Forms.RightToLeft.No
        txtId.Size = New System.Drawing.Size(40, 16)
        txtId.TabIndex = 0
        '
        'Label8
        '
        Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label8.Location = New System.Drawing.Point(0, 40)
        Label8.Name = "Label8"
        Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label8.Size = New System.Drawing.Size(64, 24)
        Label8.TabIndex = 0
        Label8.Text = "Descripcion"
        '
        'BtnSave
        '
        BtnSave.Location = New System.Drawing.Point(328, 80)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New System.Drawing.Size(72, 24)
        BtnSave.TabIndex = 1
        BtnSave.Text = "Guardar"
        '
        'TextBox1
        '
        TextBox1.AutoSize = False
        TextBox1.BackColor = System.Drawing.Color.White
        TextBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        TextBox1.Location = New System.Drawing.Point(64, 64)
        TextBox1.MaxLength = 0
        TextBox1.Name = "TextBox1"
        TextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        TextBox1.Size = New System.Drawing.Size(176, 32)
        TextBox1.TabIndex = 2
        TextBox1.Text = ""
        TextBox1.WordWrap = False
        '
        'Label1
        '
        Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label1.Location = New System.Drawing.Point(8, 72)
        Label1.Name = "Label1"
        Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label1.Size = New System.Drawing.Size(48, 24)
        Label1.TabIndex = 3
        Label1.Text = "Help"
        '
        'Panel2
        '
        Panel2.Controls.Add(CboSSTats)
        Panel2.Controls.Add(Tipo)
        Panel2.Controls.Add(ChkEnabled)
        Panel2.Controls.Add(TxtDescription)
        Panel2.Controls.Add(Label1)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(NumRefreshRate)
        Panel2.Controls.Add(txtId)
        Panel2.Controls.Add(Label4)
        Panel2.Controls.Add(BtnSave)
        Panel2.Controls.Add(Label8)
        Panel2.Controls.Add(TextBox1)
        Panel2.Controls.Add(TxtName)
        Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Panel2.Location = New System.Drawing.Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(416, 120)
        Panel2.TabIndex = 5
        '
        'CboSSTats
        '
        CboSSTats.BackColor = System.Drawing.Color.White
        CboSSTats.Location = New System.Drawing.Point(272, 40)
        CboSSTats.Name = "CboSSTats"
        CboSSTats.Size = New System.Drawing.Size(136, 21)
        CboSSTats.TabIndex = 5
        CboSSTats.Text = "ComboBox2"
        '
        'Tipo
        '
        Tipo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Tipo.Location = New System.Drawing.Point(240, 40)
        Tipo.Name = "Tipo"
        Tipo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Tipo.Size = New System.Drawing.Size(32, 24)
        Tipo.TabIndex = 6
        Tipo.Text = "Tipo"
        '
        'ChkEnabled
        '
        ChkEnabled.Location = New System.Drawing.Point(328, 8)
        ChkEnabled.Name = "ChkEnabled"
        ChkEnabled.Size = New System.Drawing.Size(72, 24)
        ChkEnabled.TabIndex = 4
        ChkEnabled.Text = "Activada"
        '
        'UCRuleCtrl
        '
        Controls.Add(Panel2)
        Name = "UCRuleCtrl"
        Size = New System.Drawing.Size(416, 120)
        CType(NumRefreshRate, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    'Dim RuleRow As DsRules.RuleRow
    Dim StepTypes(5) As String

    Public Sub New()
        ' Me.RuleRow = RuleRow

    End Sub
    'Private Sub FillTypes()
    '    StepTypes.SetValue("Entrada", 0)
    '    StepTypes.SetValue("Salida", 1)
    '    StepTypes.SetValue("Actualizacion", 2)
    '    StepTypes.SetValue("Accion de Usuario", 3)
    '    StepTypes.SetValue("Planificada", 4)
    '    Me.CboSSTats.DataSource = StepTypes
    'End Sub

    Public Event DescriptionChanged(ByVal Description As String)
    Public Event NameChanged(ByVal Name As String)
    Public Event RefreshRateChanged(ByVal RefreshRate As Int32)
    Public Event Save()

    Private Sub TxtDescription_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TxtDescription.TextChanged
        Try
            RaiseEvent DescriptionChanged(TxtDescription.Text.Trim)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TxtName_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TxtName.TextChanged
        Try
            RaiseEvent NameChanged(TxtName.Text.Trim)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub NumRefreshRate_ValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles NumRefreshRate.ValueChanged
        Try
            RaiseEvent RefreshRateChanged(CInt(NumRefreshRate.Value))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        RaiseEvent Save()
    End Sub

    'Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    'End Sub

    'Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    'End Sub
End Class
