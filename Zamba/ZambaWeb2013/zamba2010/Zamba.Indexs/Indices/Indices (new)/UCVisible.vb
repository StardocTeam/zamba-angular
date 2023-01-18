Public Class UCVisible
    Inherits Zamba.AppBlock.ZBlueControl

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
    Friend WithEvents gp As System.Windows.Forms.GroupBox
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents chkIndexer As System.Windows.Forms.CheckBox
    Friend WithEvents chkCaratulas As System.Windows.Forms.CheckBox
    Friend WithEvents chkvisualizador As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UCVisible))
        Me.gp = New System.Windows.Forms.GroupBox
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.chkIndexer = New System.Windows.Forms.CheckBox
        Me.chkCaratulas = New System.Windows.Forms.CheckBox
        Me.chkvisualizador = New System.Windows.Forms.CheckBox
        Me.gp.SuspendLayout()
        Me.SuspendLayout()
        '
        'ZIconList
        '

        '
        'gp
        '
        Me.gp.BackColor = System.Drawing.Color.Transparent
        Me.gp.Controls.Add(Me.chkvisualizador)
        Me.gp.Controls.Add(Me.chkCaratulas)
        Me.gp.Controls.Add(Me.chkIndexer)
        Me.gp.Controls.Add(Me.chkSearch)
        Me.gp.Location = New System.Drawing.Point(16, 16)
        Me.gp.Name = "gp"
        Me.gp.Size = New System.Drawing.Size(368, 152)
        Me.gp.TabIndex = 0
        Me.gp.TabStop = False
        Me.gp.Text = "Visivilidad"
        '
        'chkSearch
        '
        Me.chkSearch.Location = New System.Drawing.Point(40, 24)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(280, 24)
        Me.chkSearch.TabIndex = 0
        Me.chkSearch.Text = "Al Buscar"
        '
        'chkIndexer
        '
        Me.chkIndexer.Location = New System.Drawing.Point(40, 48)
        Me.chkIndexer.Name = "chkIndexer"
        Me.chkIndexer.Size = New System.Drawing.Size(280, 24)
        Me.chkIndexer.TabIndex = 1
        Me.chkIndexer.Text = "Al Insertar"
        '
        'chkCaratulas
        '
        Me.chkCaratulas.Location = New System.Drawing.Point(40, 72)
        Me.chkCaratulas.Name = "chkCaratulas"
        Me.chkCaratulas.Size = New System.Drawing.Size(280, 24)
        Me.chkCaratulas.TabIndex = 2
        Me.chkCaratulas.Text = "Al generar caratulas"
        '
        'chkvisualizador
        '
        Me.chkvisualizador.Location = New System.Drawing.Point(40, 96)
        Me.chkvisualizador.Name = "chkvisualizador"
        Me.chkvisualizador.Size = New System.Drawing.Size(280, 24)
        Me.chkvisualizador.TabIndex = 3
        Me.chkvisualizador.Text = "Al ver resultados"
        '
        'UCVisible
        '
        Me.Controls.Add(Me.gp)
        Me.Name = "UCVisible"
        Me.Size = New System.Drawing.Size(408, 192)
        Me.gp.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event indexer(ByVal ver As Boolean)
    Public Event viewer(ByVal ver As Boolean)
    Public Event search(ByVal ver As Boolean)
    Public Event caratulas(ByVal ver As Boolean)

    Private Sub chkSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSearch.CheckedChanged
        RaiseEvent search(Me.chkSearch.Checked)
    End Sub

    Private Sub chkIndexer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIndexer.CheckedChanged
        RaiseEvent indexer(Me.chkIndexer.Checked)
    End Sub

    Private Sub chkCaratulas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCaratulas.CheckedChanged
        RaiseEvent caratulas(Me.chkCaratulas.Checked)
    End Sub

    Private Sub chkvisualizador_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkvisualizador.CheckedChanged
        RaiseEvent viewer(Me.chkvisualizador.Checked)
    End Sub
End Class
