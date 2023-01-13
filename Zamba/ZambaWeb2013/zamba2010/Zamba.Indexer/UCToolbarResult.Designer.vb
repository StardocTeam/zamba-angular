Partial Class UCToolbarResult
    Inherits System.Windows.Forms.ToolStrip

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
    Public WithEvents btnSaveAs As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnAdjuntarEmail As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnAdjuntarMensaje As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnImprimirImagenesIndices As System.Windows.Forms.ToolStripButton
    Public WithEvents btnAgregarDocumentoCarpeta As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    'Public WithEvents btnVerDocumentosAsociados As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    'Public WithEvents btnVerForo As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnVerVersionesDelDocumento As System.Windows.Forms.ToolStripButton
    Public WithEvents btnAgregarUnaNuevaVersionDelDocuemto As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnGotoWorkflow As System.Windows.Forms.ToolStripButton
    Public WithEvents btnChangePosition As System.Windows.Forms.ToolStripButton
    Public WithEvents btnGotoHelp As System.Windows.Forms.ToolStripButton

    'NOTA: el Diseñador de componentes requiere el siguiente procedimiento
    'Se puede modificar usando el Diseñador de componentes.
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCToolbarResult))
        Me.btnSaveAs = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.btnAdjuntarEmail = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.btnAdjuntarMensaje = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.btnImprimirImagenesIndices = New System.Windows.Forms.ToolStripButton
        Me.btnAgregarDocumentoCarpeta = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
        'Me.btnVerDocumentosAsociados = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.btnVerVersionesDelDocumento = New System.Windows.Forms.ToolStripButton
        Me.btnAgregarUnaNuevaVersionDelDocuemto = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator
        'Me.btnVerForo = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator
        Me.btnGotoWorkflow = New System.Windows.Forms.ToolStripButton
        Me.btnChangePosition = New System.Windows.Forms.ToolStripButton
        Me.btnGotoHelp = New System.Windows.Forms.ToolStripButton

        Me.SuspendLayout()
        '
        'btnSaveAs
        '
        Me.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnSaveAs.Image = CType(resources.GetObject("btnSaveAs.Image"), System.Drawing.Image)
        Me.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(23, 102)
        Me.btnSaveAs.Tag = "GUARDARDOCUMENTOCOMO"
        Me.btnSaveAs.Text = "Guardar Como"
        Me.btnSaveAs.ToolTipText = "Guardar Como"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 105)
        '
        'btnAdjuntarEmail
        '
        Me.btnAdjuntarEmail.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdjuntarEmail.ForeColor = System.Drawing.Color.Black
        Me.btnAdjuntarEmail.Image = CType(resources.GetObject("btnAdjuntarEmail.Image"), System.Drawing.Image)
        Me.btnAdjuntarEmail.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAdjuntarEmail.Name = "btnAdjuntarEmail"
        Me.btnAdjuntarEmail.Size = New System.Drawing.Size(69, 102)
        Me.btnAdjuntarEmail.Tag = "EMAIL"
        Me.btnAdjuntarEmail.Text = "Envio Mail"
        Me.btnAdjuntarEmail.ToolTipText = "Enviar por mail"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 105)
        '
        'btnAdjuntarMensaje
        '
        Me.btnAdjuntarMensaje.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdjuntarMensaje.ForeColor = System.Drawing.Color.Black
        Me.btnAdjuntarMensaje.Image = CType(resources.GetObject("btnAdjuntarMensaje.Image"), System.Drawing.Image)
        Me.btnAdjuntarMensaje.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAdjuntarMensaje.Name = "btnAdjuntarMensaje"
        Me.btnAdjuntarMensaje.Size = New System.Drawing.Size(87, 102)
        Me.btnAdjuntarMensaje.Tag = "MENSAJE"
        Me.btnAdjuntarMensaje.Text = "Envio mensaje"
        Me.btnAdjuntarMensaje.ToolTipText = "Enviar por mensaje interno"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 105)
        '
        'btnImprimirImagenesIndices
        '
        Me.btnImprimirImagenesIndices.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimirImagenesIndices.ForeColor = System.Drawing.Color.Black
        Me.btnImprimirImagenesIndices.Image = CType(resources.GetObject("btnImprimirImagenesIndices.Image"), System.Drawing.Image)
        Me.btnImprimirImagenesIndices.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImprimirImagenesIndices.Name = "btnImprimirImagenesIndices"
        Me.btnImprimirImagenesIndices.Size = New System.Drawing.Size(60, 102)
        Me.btnImprimirImagenesIndices.Tag = "IMPRIMIR"
        Me.btnImprimirImagenesIndices.Text = "Imprimir"
        Me.btnImprimirImagenesIndices.ToolTipText = "Imprimir"
        '
        'btnAgregarDocumentoCarpeta
        '
        Me.btnAgregarDocumentoCarpeta.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregarDocumentoCarpeta.ForeColor = System.Drawing.Color.Black
        Me.btnAgregarDocumentoCarpeta.Image = CType(resources.GetObject("btnAgregarDocumentoCarpeta.Image"), System.Drawing.Image)
        Me.btnAgregarDocumentoCarpeta.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAgregarDocumentoCarpeta.Name = "btnAgregarDocumentoCarpeta"
        Me.btnAgregarDocumentoCarpeta.Size = New System.Drawing.Size(101, 102)
        Me.btnAgregarDocumentoCarpeta.Tag = "AGREGARACARPETA"
        Me.btnAgregarDocumentoCarpeta.Text = "Incorporar documento"
        Me.btnAgregarDocumentoCarpeta.ToolTipText = "Incorporar nuevo documento a la carpeta"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 105)
        '
        'btnVerDocumentosAsociados
        '
        'Me.btnVerDocumentosAsociados.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.btnVerDocumentosAsociados.ForeColor = System.Drawing.Color.Black
        'Me.btnVerDocumentosAsociados.Image = CType(resources.GetObject("btnVerDocumentosAsociados.Image"), System.Drawing.Image)
        'Me.btnVerDocumentosAsociados.ImageTransparentColor = System.Drawing.Color.Magenta
        'Me.btnVerDocumentosAsociados.Name = "btnVerDocumentosAsociados"
        'Me.btnVerDocumentosAsociados.Size = New System.Drawing.Size(66, 102)
        'Me.btnVerDocumentosAsociados.Tag = "DOCUMENTOSASOCIADOS"
        'Me.btnVerDocumentosAsociados.Text = "Asociados"
        'Me.btnVerDocumentosAsociados.ToolTipText = "Ver documentos asociados"
        ''
        ''btnVerDocumentosAsociados
        ''
        'Me.btnVerForo.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.btnVerForo.ForeColor = System.Drawing.Color.Black
        'Me.btnVerForo.Image = CType(resources.GetObject("btnforum.image"), System.Drawing.Image)
        'Me.btnVerForo.ImageTransparentColor = System.Drawing.Color.Magenta
        'Me.btnVerForo.Name = "btnVerForo"
        'Me.btnVerForo.Size = New System.Drawing.Size(66, 102)
        'Me.btnVerForo.Tag = "VERFORO"
        'Me.btnVerForo.Text = "Foro"
        'Me.btnVerForo.ToolTipText = "Ver foro"
        ''
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 105)
        '
        'btnVerVersionesDelDocumento
        '
        Me.btnVerVersionesDelDocumento.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVerVersionesDelDocumento.ForeColor = System.Drawing.Color.Black
        Me.btnVerVersionesDelDocumento.Image = CType(resources.GetObject("btnVerVersionesDelDocumento.Image"), System.Drawing.Image)
        Me.btnVerVersionesDelDocumento.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnVerVersionesDelDocumento.Name = "btnVerVersionesDelDocumento"
        Me.btnVerVersionesDelDocumento.Size = New System.Drawing.Size(65, 102)
        Me.btnVerVersionesDelDocumento.Tag = "VERSIONESDELDOCUMENTO"
        Me.btnVerVersionesDelDocumento.Text = "Versiones"
        Me.btnVerVersionesDelDocumento.ToolTipText = "Ver versiones del documento"
        '
        'btnAgregarUnaNuevaVersionDelDocuemto
        '
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregarUnaNuevaVersionDelDocuemto.ForeColor = System.Drawing.Color.Black
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Image = CType(resources.GetObject("btnAgregarUnaNuevaVersionDelDocuemto.Image"), System.Drawing.Image)
        Me.btnAgregarUnaNuevaVersionDelDocuemto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Name = "btnAgregarUnaNuevaVersionDelDocuemto"
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Size = New System.Drawing.Size(86, 102)
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Tag = "AGREGARNUEVAVERSION"
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Text = "Nueva version"
        Me.btnAgregarUnaNuevaVersionDelDocuemto.ToolTipText = "Agregar nueva version del documento"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(6, 105)
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(6, 105)
        '
        'btnGotoWorkflow
        '
        Me.btnGotoWorkflow.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGotoWorkflow.ForeColor = System.Drawing.Color.Black
        Me.btnGotoWorkflow.Image = CType(resources.GetObject("btnGotoWorkflow.Image"), System.Drawing.Image)
        Me.btnGotoWorkflow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGotoWorkflow.Name = "btnGotoWorkflow"
        Me.btnGotoWorkflow.Size = New System.Drawing.Size(53, 102)
        Me.btnGotoWorkflow.Tag = "IRAWORKFLOW"
        Me.btnGotoWorkflow.Text = "Tareas"
        Me.btnGotoWorkflow.ToolTipText = "Ver la tarea asociada al documento"
        '
        'btnChangePosition
        '
        Me.btnChangePosition.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangePosition.ForeColor = System.Drawing.Color.Black
        Me.btnChangePosition.Image = CType(resources.GetObject("position"), System.Drawing.Image)
        Me.btnChangePosition.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnChangePosition.Name = "btnChangePosition"
        Me.btnChangePosition.Size = New System.Drawing.Size(53, 102)
        Me.btnChangePosition.Tag = "CHANGEPOSITION"
        Me.btnChangePosition.Text = ""
        Me.btnChangePosition.ToolTipText = "Mostrar Listado solo o Listado y Documentos"
        '
        'UCToolbarResult
        '
        Me.AllowItemReorder = True
        Me.CanOverflow = False
        Me.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSaveAs, Me.ToolStripSeparator7, Me.btnAdjuntarEmail, Me.ToolStripSeparator8, Me.btnAdjuntarMensaje, Me.ToolStripSeparator9, Me.btnImprimirImagenesIndices, Me.btnAgregarDocumentoCarpeta, Me.ToolStripSeparator10, Me.ToolStripSeparator11, Me.ToolStripSeparator13, Me.btnVerVersionesDelDocumento, Me.btnAgregarUnaNuevaVersionDelDocuemto, Me.ToolStripSeparator12, Me.btnGotoWorkflow, Me.btnChangePosition})
        Me.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.Name = "UCResultsToolbar"
        Me.Size = New System.Drawing.Size(1012, 105)
        Me.ResumeLayout(False)

    End Sub

End Class
