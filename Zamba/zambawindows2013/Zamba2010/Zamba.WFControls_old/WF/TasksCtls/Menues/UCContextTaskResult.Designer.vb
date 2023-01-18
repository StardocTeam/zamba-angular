Partial Class UCContextTaskResult
    Inherits ContextMenuStrip

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Requerido para la compatibilidad con el Diseñador de composiciones de clases Windows.Forms
        Container.Add(Me)

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'El Diseñador de componentes requiere esta llamada.
        InitializeComponent()

    End Sub

    'Component reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de componentes
    Private components As System.ComponentModel.IContainer
    Friend WithEvents ExportarAPDFToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnEditar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btHistorial As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropiedadesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ImprimirIndicesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuShowVersionComment As System.Windows.Forms.ToolStripMenuItem

    'NOTA: el Diseñador de componentes requiere el siguiente procedimiento
    'Se puede modificar usando el Diseñador de componentes.
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()

        Me.ExportarAPDFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnEditar = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.btHistorial = New System.Windows.Forms.ToolStripMenuItem
        Me.PropiedadesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ImprimirIndicesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuShowVersionComment = New System.Windows.Forms.ToolStripMenuItem

        'MenuShowVersionComment
        '
        Me.MenuShowVersionComment.Name = "MenuShowVersionComment"
        Me.MenuShowVersionComment.Size = New System.Drawing.Size(205, 22)
        Me.MenuShowVersionComment.Text = "Versión del documento"
        Me.MenuShowVersionComment.Tag = "ShowVersion"
        '
        'ExportarAPDFToolStripMenuItem
        '
        Me.ExportarAPDFToolStripMenuItem.Name = "ExportarAPDFToolStripMenuItem"
        Me.ExportarAPDFToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.ExportarAPDFToolStripMenuItem.Text = "Exportar a PDF"
        Me.ExportarAPDFToolStripMenuItem.Tag = "ExportToPDF"
        '
        'ImprimirIndicesToolStripMenuItem
        '
        Me.ImprimirIndicesToolStripMenuItem.Name = "ImprimirIndicesToolStripMenuItem"
        Me.ImprimirIndicesToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.ImprimirIndicesToolStripMenuItem.Text = "Imprimir Atributos"
        Me.ImprimirIndicesToolStripMenuItem.Tag = "PrintIndexs"   '
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(202, 6)
        '
        'btnEditar
        '
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(205, 22)
        Me.btnEditar.Text = "Editor de TIF"
        Me.btnEditar.Tag = "EditTIF"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(202, 6)
        '
        'btHistorial
        '
        Me.btHistorial.Name = "btHistorial"
        Me.btHistorial.Size = New System.Drawing.Size(205, 22)
        Me.btHistorial.Text = "Historial"
        Me.btHistorial.Tag = "History"
        '
        'PropiedadesToolStripMenuItem
        '
        Me.PropiedadesToolStripMenuItem.Name = "PropiedadesToolStripMenuItem"
        Me.PropiedadesToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.PropiedadesToolStripMenuItem.Text = "Propiedades"
        Me.PropiedadesToolStripMenuItem.Tag = "Property"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(202, 6)
        '
        'UCContextResult
        '
        Me.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportarAPDFToolStripMenuItem, Me.ImprimirIndicesToolStripMenuItem, Me.ToolStripSeparator1, Me.btnEditar, Me.ToolStripSeparator6, Me.btHistorial, Me.PropiedadesToolStripMenuItem, Me.ToolStripSeparator5, Me.MenuShowVersionComment})
        Me.Name = "ContextMenuStrip1"
        Me.Size = New System.Drawing.Size(206, 370)
    End Sub
End Class
