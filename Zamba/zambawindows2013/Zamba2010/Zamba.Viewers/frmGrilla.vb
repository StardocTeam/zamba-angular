Public Class frmGrilla
    Inherits Zamba.AppBlock.ZForm 'System.Windows.Forms.Form
    Implements Zamba.Core.IfrmGrilla

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal ds As DataSet)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Grilla.DataSource = ds.Tables(0)
        Grilla.ReadOnly = True
        Grilla.Refresh()
    End Sub

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
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
    Friend WithEvents Grilla As System.Windows.Forms.DataGrid
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmGrilla))
        Grilla = New System.Windows.Forms.DataGrid
        CType(Grilla, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'Grilla
        '
        Grilla.AlternatingBackColor = System.Drawing.Color.Lavender
        Grilla.BackColor = System.Drawing.Color.WhiteSmoke
        Grilla.BackgroundColor = System.Drawing.Color.LightGray
        Grilla.BorderStyle = System.Windows.Forms.BorderStyle.None
        Grilla.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Grilla.CaptionFont = New Font("Microsoft Sans Serif", 8.0!)
        Grilla.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Grilla.DataMember = ""
        Grilla.Dock = System.Windows.Forms.DockStyle.Fill
        Grilla.FlatMode = True
        Grilla.Font = New Font("Microsoft Sans Serif", 8.0!)
        Grilla.ForeColor = System.Drawing.Color.MidnightBlue
        Grilla.GridLineColor = System.Drawing.Color.Gainsboro
        Grilla.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Grilla.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Grilla.HeaderFont = New Font("Microsoft Sans Serif", 8.0!)
        Grilla.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Grilla.LinkColor = System.Drawing.Color.Teal
        Grilla.Location = New System.Drawing.Point(0, 0)
        Grilla.Name = "Grilla"
        Grilla.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Grilla.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Grilla.SelectionBackColor = System.Drawing.Color.CadetBlue
        Grilla.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        Grilla.Size = New System.Drawing.Size(352, 266)
        Grilla.TabIndex = 0
        '
        'frmGrilla
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        ClientSize = New System.Drawing.Size(352, 266)
        Controls.Add(Grilla)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Name = "frmGrilla"
        Text = "Seleccione"
        CType(Grilla, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    'Public Event eIdFila(ByVal idrow As Int16)

    Public Property DS() As DataSet Implements Core.IfrmGrilla.DS
        Get
            Return Nothing
        End Get
        Set(ByVal value As DataSet)
            'Agregar cualquier inicialización después de la llamada a InitializeComponent()
            Grilla.DataSource = value.Tables(0)
            Grilla.ReadOnly = True
            Grilla.Refresh()
        End Set
    End Property

    Private Sub Grilla_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles Grilla.DoubleClick
        Id = CShort(Grilla.CurrentRowIndex)
        Close()
    End Sub

    Private Sub frmGrilla_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Grilla.PreferredColumnWidth = 250
    End Sub

    Dim _id As Int16
    Public Shadows Property Id() As Int16 Implements Core.IfrmGrilla.Id
        Get
            Return _id
        End Get
        Set(ByVal value As Int16)
            _id = value
        End Set
    End Property

End Class
