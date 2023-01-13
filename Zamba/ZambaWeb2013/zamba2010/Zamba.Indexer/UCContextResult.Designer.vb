Partial Class UCContextResult
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
    Friend WithEvents AgregarAWorkFlowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExportarAPDFToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EliminarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btMoverCopiarDocumento As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnEditar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CambiarNombreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btHistorial As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropiedadesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GenerarLinkAResultadoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerarLinkAResultadoWebToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BorrarBusquedasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImprimirIndicesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuShowVersionComment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuInsertDocument As System.Windows.Forms.ToolStripMenuItem

    'NOTA: el Diseñador de componentes requiere el siguiente procedimiento
    'Se puede modificar usando el Diseñador de componentes.
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()

        Me.AgregarAWorkFlowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ExportarAPDFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.EliminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btMoverCopiarDocumento = New System.Windows.Forms.ToolStripMenuItem
        Me.btnEditar = New System.Windows.Forms.ToolStripMenuItem
        Me.CambiarNombreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.btHistorial = New System.Windows.Forms.ToolStripMenuItem
        Me.PropiedadesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.GenerarLinkAResultadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GenerarLinkAResultadoWebToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.BorrarBusquedasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImprimirIndicesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuShowVersionComment = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuInsertDocument = New System.Windows.Forms.ToolStripMenuItem

        'MenuShowVersionComment
        '
        Me.MenuShowVersionComment.Name = "MenuShowVersionComment"
        Me.MenuShowVersionComment.Size = New System.Drawing.Size(205, 22)
        Me.MenuShowVersionComment.Text = "Versión del documento"
        Me.MenuShowVersionComment.Tag = "ShowVersion"

        'MenuInsertDocument
        '
        Me.MenuInsertDocument.Name = "MenuInsertDocument"
        Me.MenuInsertDocument.Size = New System.Drawing.Size(205, 22)
        Me.MenuInsertDocument.Text = "Insertar Relacion"
        Me.MenuInsertDocument.Tag = "InsertDocument"

        '
        'AgregarAWorkFlowToolStripMenuItem
        '
        Me.AgregarAWorkFlowToolStripMenuItem.Name = "AgregarAWorkFlowToolStripMenuItem"
        Me.AgregarAWorkFlowToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.AgregarAWorkFlowToolStripMenuItem.Text = "Agregar a WorkFlow"
        Me.AgregarAWorkFlowToolStripMenuItem.Tag = "AddToWF"

        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(202, 6)
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
        Me.ImprimirIndicesToolStripMenuItem.Text = "Imprimir Indices"
        Me.ImprimirIndicesToolStripMenuItem.Tag = "PrintIndexs"   '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(202, 6)
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.EliminarToolStripMenuItem.Text = "Eliminar"
        Me.EliminarToolStripMenuItem.Tag = "Delete"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(202, 6)
        '
        'btMoverCopiarDocumento
        '
        Me.btMoverCopiarDocumento.Name = "btMoverCopiarDocumento"
        Me.btMoverCopiarDocumento.Size = New System.Drawing.Size(205, 22)
        Me.btMoverCopiarDocumento.Text = "Copiar o Mover"
        Me.btMoverCopiarDocumento.Tag = "MoveCopyDoc"
        '
        'btnEditar
        '
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(205, 22)
        Me.btnEditar.Text = "Editor de TIF"
        Me.btnEditar.Tag = "EditTIF"
        '
        'CambiarNombreToolStripMenuItem
        '
        Me.CambiarNombreToolStripMenuItem.Name = "CambiarNombreToolStripMenuItem"
        Me.CambiarNombreToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.CambiarNombreToolStripMenuItem.Text = "Cambiar Nombre"
        Me.CambiarNombreToolStripMenuItem.Tag = "ChangeName"
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
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(202, 6)
        '
        'GenerarLinkAResultadoToolStripMenuItem
        '
        Me.GenerarLinkAResultadoToolStripMenuItem.Name = "GenerarLinkAResultadoToolStripMenuItem"
        Me.GenerarLinkAResultadoToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.GenerarLinkAResultadoToolStripMenuItem.Text = "Generar Link a Resultado"
        Me.GenerarLinkAResultadoToolStripMenuItem.Tag = "GenerateLink"
        '
        'GenerarLinkAResultadoToolStripMenuItem
        '
        Me.GenerarLinkAResultadoWebToolStripMenuItem.Name = "GenerarLinkAResultadoWebToolStripMenuItem"
        Me.GenerarLinkAResultadoWebToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.GenerarLinkAResultadoWebToolStripMenuItem.Text = "Generar Link a Resultado Web"
        Me.GenerarLinkAResultadoWebToolStripMenuItem.Tag = "GenerateLinkWeb"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(202, 6)
        '
        'BorrarBusquedasToolStripMenuItem
        '
        Me.BorrarBusquedasToolStripMenuItem.Name = "BorrarBusquedasToolStripMenuItem"
        Me.BorrarBusquedasToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.BorrarBusquedasToolStripMenuItem.Text = "Borrar Busquedas"
        Me.BorrarBusquedasToolStripMenuItem.Tag = "ClearSearch"
        '
        'UCContextResult
        '
        Me.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AgregarAWorkFlowToolStripMenuItem, Me.ToolStripSeparator3, Me.ExportarAPDFToolStripMenuItem, Me.ImprimirIndicesToolStripMenuItem, Me.ToolStripSeparator2, Me.EliminarToolStripMenuItem, Me.ToolStripSeparator1, Me.btMoverCopiarDocumento, Me.btnEditar, Me.CambiarNombreToolStripMenuItem, Me.ToolStripSeparator6, Me.btHistorial, Me.PropiedadesToolStripMenuItem, Me.ToolStripSeparator4, Me.GenerarLinkAResultadoToolStripMenuItem, Me.GenerarLinkAResultadoWebToolStripMenuItem, Me.ToolStripSeparator5, Me.BorrarBusquedasToolStripMenuItem, Me.MenuShowVersionComment, Me.MenuInsertDocument})
        Me.Name = "ContextMenuStrip1"
        Me.Size = New System.Drawing.Size(206, 370)
    End Sub
End Class
