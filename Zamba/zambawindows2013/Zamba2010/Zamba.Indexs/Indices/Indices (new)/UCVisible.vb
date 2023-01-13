Public Class UCVisible
    Inherits Zamba.AppBlock.ZControl

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
    Friend WithEvents gp As GroupBox
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents chkIndexer As System.Windows.Forms.CheckBox
    Friend WithEvents chkCaratulas As System.Windows.Forms.CheckBox
    Friend WithEvents chkvisualizador As System.Windows.Forms.CheckBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UCVisible))
        gp = New GroupBox
        chkSearch = New System.Windows.Forms.CheckBox
        chkIndexer = New System.Windows.Forms.CheckBox
        chkCaratulas = New System.Windows.Forms.CheckBox
        chkvisualizador = New System.Windows.Forms.CheckBox
        gp.SuspendLayout()
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'gp
        '
        gp.BackColor = Color.Transparent
        gp.Controls.Add(chkvisualizador)
        gp.Controls.Add(chkCaratulas)
        gp.Controls.Add(chkIndexer)
        gp.Controls.Add(chkSearch)
        gp.Location = New Point(16, 16)
        gp.Name = "gp"
        gp.Size = New Size(368, 152)
        gp.TabIndex = 0
        gp.TabStop = False
        gp.Text = "Visivilidad"
        '
        'chkSearch
        '
        chkSearch.Location = New Point(40, 24)
        chkSearch.Name = "chkSearch"
        chkSearch.Size = New Size(280, 24)
        chkSearch.TabIndex = 0
        chkSearch.Text = "Al Buscar"
        '
        'chkIndexer
        '
        chkIndexer.Location = New Point(40, 48)
        chkIndexer.Name = "chkIndexer"
        chkIndexer.Size = New Size(280, 24)
        chkIndexer.TabIndex = 1
        chkIndexer.Text = "Al Insertar"
        '
        'chkCaratulas
        '
        chkCaratulas.Location = New Point(40, 72)
        chkCaratulas.Name = "chkCaratulas"
        chkCaratulas.Size = New Size(280, 24)
        chkCaratulas.TabIndex = 2
        chkCaratulas.Text = "Al generar caratulas"
        '
        'chkvisualizador
        '
        chkvisualizador.Location = New Point(40, 96)
        chkvisualizador.Name = "chkvisualizador"
        chkvisualizador.Size = New Size(280, 24)
        chkvisualizador.TabIndex = 3
        chkvisualizador.Text = "Al ver resultados"
        '
        'UCVisible
        '
        Controls.Add(gp)
        Name = "UCVisible"
        Size = New Size(408, 192)
        gp.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public Event indexer(ByVal ver As Boolean)
    Public Event viewer(ByVal ver As Boolean)
    Public Event search(ByVal ver As Boolean)
    Public Event caratulas(ByVal ver As Boolean)

    Private Sub chkSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkSearch.CheckedChanged
        RaiseEvent search(chkSearch.Checked)
    End Sub

    Private Sub chkIndexer_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkIndexer.CheckedChanged
        RaiseEvent indexer(chkIndexer.Checked)
    End Sub

    Private Sub chkCaratulas_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkCaratulas.CheckedChanged
        RaiseEvent caratulas(chkCaratulas.Checked)
    End Sub

    Private Sub chkvisualizador_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkvisualizador.CheckedChanged
        RaiseEvent viewer(chkvisualizador.Checked)
    End Sub
End Class
