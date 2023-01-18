Public Class UCInterval
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
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Button1 As ZButton
    Friend WithEvents Button2 As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UCInterval))
        NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Label1 = New ZLabel
        Label2 = New ZLabel
        Button1 = New ZButton
        Button2 = New ZButton
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'NumericUpDown1
        '
        NumericUpDown1.BackColor = System.Drawing.Color.White
        NumericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        NumericUpDown1.Location = New System.Drawing.Point(128, 72)
        NumericUpDown1.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New System.Drawing.Size(72, 21)
        NumericUpDown1.TabIndex = 0
        NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        NumericUpDown1.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label1
        '
        Label1.Dock = System.Windows.Forms.DockStyle.Top
        Label1.Font = New Font("Tahoma", 10.0!, FontStyle.Bold)
        Label1.ForeColor = System.Drawing.Color.White
        Label1.Location = New System.Drawing.Point(0, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(416, 48)
        Label1.TabIndex = 1
        Label1.Text = "Intervalo de Actualizacion de Etapas de WorkFlow"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Tahoma", 8.0!)
        Label2.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label2.Location = New System.Drawing.Point(8, 72)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(112, 23)
        Label2.TabIndex = 2
        Label2.Text = "Intervalo en Minutos:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'Button1
        '
        Button1.DialogResult = System.Windows.Forms.DialogResult.None
        Button1.Location = New System.Drawing.Point(224, 64)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(72, 32)
        Button1.TabIndex = 4
        Button1.Text = "Guardar"
        '
        'Button2
        '
        Button2.DialogResult = System.Windows.Forms.DialogResult.None
        Button2.Location = New System.Drawing.Point(312, 64)
        Button2.Name = "Button2"
        Button2.Size = New System.Drawing.Size(80, 32)
        Button2.TabIndex = 5
        Button2.Text = "Cancelar"
        '
        'UCInterval
        '
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(NumericUpDown1)
        Name = "UCInterval"
        Size = New System.Drawing.Size(416, 120)
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region


    Public Sub New(ByVal WfInterval As Int32)
        Me.New()
        If WfInterval > 0 Then
            Interval = WfInterval
        Else
            Interval = 5
        End If
    End Sub

    Public Event Cancel()
    Public Event Save(ByVal Interval As Int32)

    Property Interval() As Int32
        Get
            Return CInt(NumericUpDown1.Value)
        End Get
        Set(ByVal Value As Int32)
            NumericUpDown1.Value = Value
        End Set
    End Property
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles NumericUpDown1.ValueChanged
        Interval = CInt(NumericUpDown1.Value)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button2.Click
        Visible = False
        RaiseEvent Cancel()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button1.Click
        RaiseEvent Save(Interval)
    End Sub
End Class
