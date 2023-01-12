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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
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
        Me.mnuSendByMail = New System.Windows.Forms.ToolStripMenuItem
        Me.SuspendLayout()
        '
        'AgregarAWorkFlowToolStripMenuItem
        '
        Me.AgregarAWorkFlowToolStripMenuItem.Name = "AgregarAWorkFlowToolStripMenuItem"
        Me.AgregarAWorkFlowToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.AgregarAWorkFlowToolStripMenuItem.Tag = "AddToWF"
        Me.AgregarAWorkFlowToolStripMenuItem.Text = "Agregar a WorkFlow"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(216, 6)
        '
        'ExportarAPDFToolStripMenuItem
        '
        Me.ExportarAPDFToolStripMenuItem.Name = "ExportarAPDFToolStripMenuItem"
        Me.ExportarAPDFToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ExportarAPDFToolStripMenuItem.Tag = "ExportToPDF"
        Me.ExportarAPDFToolStripMenuItem.Text = "Exportar a PDF"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(216, 6)
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.EliminarToolStripMenuItem.Tag = "Delete"
        Me.EliminarToolStripMenuItem.Text = "Eliminar"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(216, 6)
        '
        'btMoverCopiarDocumento
        '
        Me.btMoverCopiarDocumento.Name = "btMoverCopiarDocumento"
        Me.btMoverCopiarDocumento.Size = New System.Drawing.Size(219, 22)
        Me.btMoverCopiarDocumento.Tag = "MoveCopyDoc"
        Me.btMoverCopiarDocumento.Text = "Copiar o Mover"
        '
        'btnEditar
        '
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(219, 22)
        Me.btnEditar.Tag = "EditTIF"
        Me.btnEditar.Text = "Editor de TIF"
        '
        'CambiarNombreToolStripMenuItem
        '
        Me.CambiarNombreToolStripMenuItem.Name = "CambiarNombreToolStripMenuItem"
        Me.CambiarNombreToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.CambiarNombreToolStripMenuItem.Tag = "ChangeName"
        Me.CambiarNombreToolStripMenuItem.Text = "Cambiar Nombre"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(216, 6)
        '
        'btHistorial
        '
        Me.btHistorial.Name = "btHistorial"
        Me.btHistorial.Size = New System.Drawing.Size(219, 22)
        Me.btHistorial.Tag = "History"
        Me.btHistorial.Text = "Historial"
        '
        'PropiedadesToolStripMenuItem
        '
        Me.PropiedadesToolStripMenuItem.Name = "PropiedadesToolStripMenuItem"
        Me.PropiedadesToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.PropiedadesToolStripMenuItem.Tag = "Property"
        Me.PropiedadesToolStripMenuItem.Text = "Propiedades"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(216, 6)
        '
        'GenerarLinkAResultadoToolStripMenuItem
        '
        Me.GenerarLinkAResultadoToolStripMenuItem.Name = "GenerarLinkAResultadoToolStripMenuItem"
        Me.GenerarLinkAResultadoToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.GenerarLinkAResultadoToolStripMenuItem.Tag = "GenerateLink"
        Me.GenerarLinkAResultadoToolStripMenuItem.Text = "Generar Link a Resultado"
        '
        'GenerarLinkAResultadoWebToolStripMenuItem
        '
        Me.GenerarLinkAResultadoWebToolStripMenuItem.Name = "GenerarLinkAResultadoWebToolStripMenuItem"
        Me.GenerarLinkAResultadoWebToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.GenerarLinkAResultadoWebToolStripMenuItem.Tag = "GenerateLinkWeb"
        Me.GenerarLinkAResultadoWebToolStripMenuItem.Text = "Generar Link a Resultado Web"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(216, 6)
        '
        'BorrarBusquedasToolStripMenuItem
        '
        Me.BorrarBusquedasToolStripMenuItem.Name = "BorrarBusquedasToolStripMenuItem"
        Me.BorrarBusquedasToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.BorrarBusquedasToolStripMenuItem.Tag = "ClearSearch"
        Me.BorrarBusquedasToolStripMenuItem.Text = "Borrar Busquedas"
        '
        'ImprimirIndicesToolStripMenuItem
        '
        Me.ImprimirIndicesToolStripMenuItem.Name = "ImprimirIndicesToolStripMenuItem"
        Me.ImprimirIndicesToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ImprimirIndicesToolStripMenuItem.Tag = "PrintIndexs"
        Me.ImprimirIndicesToolStripMenuItem.Text = "Imprimir Atributos"
        '
        'MenuShowVersionComment
        '
        Me.MenuShowVersionComment.Name = "MenuShowVersionComment"
        Me.MenuShowVersionComment.Size = New System.Drawing.Size(219, 22)
        Me.MenuShowVersionComment.Tag = "ShowVersion"
        Me.MenuShowVersionComment.Text = "Versión del documento"
        '
        'MenuInsertDocument
        '
        Me.MenuInsertDocument.Name = "MenuInsertDocument"
        Me.MenuInsertDocument.Size = New System.Drawing.Size(219, 22)
        Me.MenuInsertDocument.Tag = "InsertDocument"
        Me.MenuInsertDocument.Text = "Insertar Relacion"
        '
        'mnuSendByMail
        '
        Me.mnuSendByMail.Name = "mnuSendByMail"
        Me.mnuSendByMail.Size = New System.Drawing.Size(219, 22)
        Me.mnuSendByMail.Tag = "SendByMail"
        Me.mnuSendByMail.Text = "Enviar por Mail"
        '
        'UCContextResult
        '
        Me.Items.AddRange(New ToolStripItem() {Me.AgregarAWorkFlowToolStripMenuItem, Me.ToolStripSeparator3, Me.mnuSendByMail, Me.ExportarAPDFToolStripMenuItem, Me.ImprimirIndicesToolStripMenuItem, Me.ToolStripSeparator2, Me.EliminarToolStripMenuItem, Me.ToolStripSeparator1, Me.btMoverCopiarDocumento, Me.btnEditar, Me.CambiarNombreToolStripMenuItem, Me.ToolStripSeparator6, Me.btHistorial, Me.PropiedadesToolStripMenuItem, Me.ToolStripSeparator4, Me.GenerarLinkAResultadoToolStripMenuItem, Me.GenerarLinkAResultadoWebToolStripMenuItem, Me.ToolStripSeparator5, Me.BorrarBusquedasToolStripMenuItem, Me.MenuShowVersionComment, Me.MenuInsertDocument})
        Me.Name = "ContextMenuStrip1"
        Me.Size = New System.Drawing.Size(220, 370)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents mnuSendByMail As System.Windows.Forms.ToolStripMenuItem
End Class
