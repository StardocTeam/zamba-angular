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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmGrilla))
        Me.Grilla = New System.Windows.Forms.DataGrid
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Grilla
        '
        Me.Grilla.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.Grilla.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Grilla.BackgroundColor = System.Drawing.Color.LightGray
        Me.Grilla.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Grilla.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.Grilla.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Grilla.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.Grilla.DataMember = ""
        Me.Grilla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grilla.FlatMode = True
        Me.Grilla.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Grilla.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Grilla.GridLineColor = System.Drawing.Color.Gainsboro
        Me.Grilla.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.Grilla.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.Grilla.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Grilla.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.Grilla.LinkColor = System.Drawing.Color.Teal
        Me.Grilla.Location = New System.Drawing.Point(0, 0)
        Me.Grilla.Name = "Grilla"
        Me.Grilla.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Me.Grilla.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.Grilla.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.Grilla.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        Me.Grilla.Size = New System.Drawing.Size(352, 266)
        Me.Grilla.TabIndex = 0
        '
        'frmGrilla
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(352, 266)
        Me.Controls.Add(Me.Grilla)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmGrilla"
        Me.Text = "Seleccione"
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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

    Private Sub Grilla_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grilla.DoubleClick
        Me.Id = CShort(Grilla.CurrentRowIndex)
        Me.Close()
    End Sub

    Private Sub frmGrilla_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Grilla.PreferredColumnWidth = 250
    End Sub

    Dim _id As Int16
    Public Shadows Property Id() As Int16 Implements Core.IfrmGrilla.Id
        Get
            Return Me._id
        End Get
        Set(ByVal value As Int16)
            Me._id = value
        End Set
    End Property

End Class
