Partial Class UCToolbarResult
    Inherits ZToolBar
    Implements IDisposable

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Requerido para la compatibilidad con el Diseñador de composiciones de clases Windows.Forms
        Container.Add(Me)

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New()
        'El Diseñador de componentes requiere esta llamada.
        InitializeComponent()
    End Sub

    'Component reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If

            If Me.Items IsNot Nothing Then
                'For i As Int32 = 0 To Me.Items.Count - 1
                '    Me.Items(i).Dispose()
                'Next
                Me.Items.Clear()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de componentes
    Private components As System.ComponentModel.IContainer
    Public WithEvents btnSaveAs As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnAdjuntarEmail As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    'Public WithEvents btnAdjuntarMensaje As System.Windows.Forms.ToolStripButton
    'Public WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
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
    Public WithEvents btnChangePosition As System.Windows.Forms.ToolStripButton
    Public WithEvents btnGotoHelp As System.Windows.Forms.ToolStripButton
    Public WithEvents btnSendZip As System.Windows.Forms.ToolStripButton
    'Public WithEvents btnSizeUp As System.Windows.Forms.ToolStripButton
    'Public WithEvents btnSizeDown As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator



    'Public WithEvents btnGotoWorkflow As System.Windows.Forms.ToolStripButton

    'NOTA: el Diseñador de componentes requiere el siguiente procedimiento
    'Se puede modificar usando el Diseñador de componentes.
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCToolbarResult))
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnGotoHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSaveAs = New System.Windows.Forms.ToolStripButton()
        Me.btnAdjuntarEmail = New System.Windows.Forms.ToolStripButton()
        Me.btnImprimirImagenesIndices = New System.Windows.Forms.ToolStripButton()
        Me.btnAgregarDocumentoCarpeta = New System.Windows.Forms.ToolStripButton()
        Me.btnVerVersionesDelDocumento = New System.Windows.Forms.ToolStripButton()
        Me.btnAgregarUnaNuevaVersionDelDocuemto = New System.Windows.Forms.ToolStripButton()
        Me.btnChangePosition = New System.Windows.Forms.ToolStripButton()
        Me.btnSendZip = New System.Windows.Forms.ToolStripButton()
        'Me.btnSizeUp = New System.Windows.Forms.ToolStripButton()
        'Me.btnSizeDown = New System.Windows.Forms.ToolStripButton()
        Me.BtnCloseTabs = New System.Windows.Forms.ToolStripButton()
        Me.SuspendLayout()
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 105)
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 105)
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 105)
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 105)
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
        'btnGotoHelp
        '
        Me.btnGotoHelp.Name = "btnGotoHelp"
        Me.btnGotoHelp.Size = New System.Drawing.Size(23, 23)
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(6, 105)
        '
        'btnSaveAs
        '
        Me.btnSaveAs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSaveAs.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_save
        Me.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(75, 102)
        Me.btnSaveAs.Tag = "GUARDARDOCUMENTOCOMO"
        Me.btnSaveAs.Text = "Guardar"
        Me.btnSaveAs.ToolTipText = "Guardar Como"
        '
        'btnAdjuntarEmail
        '
        Me.btnAdjuntarEmail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdjuntarEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAdjuntarEmail.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_email
        Me.btnAdjuntarEmail.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAdjuntarEmail.Name = "btnAdjuntarEmail"
        Me.btnAdjuntarEmail.Size = New System.Drawing.Size(99, 102)
        Me.btnAdjuntarEmail.Tag = "EMAIL"
        Me.btnAdjuntarEmail.Text = "Envio Mail"
        Me.btnAdjuntarEmail.ToolTipText = "Enviar por mail"
        '
        'btnImprimirImagenesIndices
        '
        Me.btnImprimirImagenesIndices.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimirImagenesIndices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnImprimirImagenesIndices.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_printer_text
        Me.btnImprimirImagenesIndices.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImprimirImagenesIndices.Name = "btnImprimirImagenesIndices"
        Me.btnImprimirImagenesIndices.Size = New System.Drawing.Size(85, 102)
        Me.btnImprimirImagenesIndices.Tag = "IMPRIMIR"
        Me.btnImprimirImagenesIndices.Text = "Imprimir"
        Me.btnImprimirImagenesIndices.ToolTipText = "Imprimir"
        '
        'btnAgregarDocumentoCarpeta
        '
        Me.btnAgregarDocumentoCarpeta.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregarDocumentoCarpeta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAgregarDocumentoCarpeta.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_add
        Me.btnAgregarDocumentoCarpeta.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAgregarDocumentoCarpeta.Name = "btnAgregarDocumentoCarpeta"
        Me.btnAgregarDocumentoCarpeta.Size = New System.Drawing.Size(86, 102)
        Me.btnAgregarDocumentoCarpeta.Tag = "AGREGARACARPETA"
        Me.btnAgregarDocumentoCarpeta.Text = "Insertar"
        Me.btnAgregarDocumentoCarpeta.ToolTipText = "Incorporar nuevo documento a la carpeta"
        '
        'btnVerVersionesDelDocumento
        '
        Me.btnVerVersionesDelDocumento.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVerVersionesDelDocumento.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnVerVersionesDelDocumento.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_page_copy
        Me.btnVerVersionesDelDocumento.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnVerVersionesDelDocumento.Name = "btnVerVersionesDelDocumento"
        Me.btnVerVersionesDelDocumento.Size = New System.Drawing.Size(96, 102)
        Me.btnVerVersionesDelDocumento.Tag = "VERSIONESDELDOCUMENTO"
        Me.btnVerVersionesDelDocumento.Text = "Versiones"
        Me.btnVerVersionesDelDocumento.ToolTipText = "Ver versiones del documento"
        Me.btnVerVersionesDelDocumento.Visible = False
        '
        'btnAgregarUnaNuevaVersionDelDocuemto
        '
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregarUnaNuevaVersionDelDocuemto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_page_location_add
        Me.btnAgregarUnaNuevaVersionDelDocuemto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Name = "btnAgregarUnaNuevaVersionDelDocuemto"
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Size = New System.Drawing.Size(127, 102)
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Tag = "AGREGARNUEVAVERSION"
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Text = "Nueva version"
        Me.btnAgregarUnaNuevaVersionDelDocuemto.ToolTipText = "Agregar nueva version del documento"
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Visible = False
        '
        'btnChangePosition
        '
        Me.btnChangePosition.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangePosition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnChangePosition.Image = CType(resources.GetObject("btnChangePosition.Image"), System.Drawing.Image)
        Me.btnChangePosition.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnChangePosition.Name = "btnChangePosition"
        Me.btnChangePosition.Size = New System.Drawing.Size(124, 102)
        Me.btnChangePosition.Tag = "CHANGEPOSITION"
        Me.btnChangePosition.Text = "Cambiar Vista"
        Me.btnChangePosition.ToolTipText = "Mostrar Listado solo o Listado y Documentos"
        '
        'btnSendZip
        '
        Me.btnSendZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSendZip.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_archive
        Me.btnSendZip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSendZip.Name = "btnSendZip"
        Me.btnSendZip.Size = New System.Drawing.Size(85, 102)
        Me.btnSendZip.Tag = "ENVIARZIP"
        Me.btnSendZip.Text = "Enviar Zip"
        Me.btnSendZip.ToolTipText = "Enviar Zip"
        ''
        ''btnSendZip
        ''
        'Me.btnSizeUp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        'Me.btnSizeUp.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_text_size_up
        'Me.btnSizeUp.ImageTransparentColor = System.Drawing.Color.Magenta
        'Me.btnSizeUp.Name = "btnSizeUp"
        'Me.btnSizeUp.Size = New System.Drawing.Size(85, 102)
        'Me.btnSizeUp.Tag = "SizeUp"
        'Me.btnSizeUp.Text = "Aumentar Fuente"
        'Me.btnSizeUp.ToolTipText = "Aumentar Fuente"
        'Me.btnSizeUp.DisplayStyle = ToolStripItemDisplayStyle.Image
        ''
        ''btnSizeDown
        ''
        'Me.btnSizeDown.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        'Me.btnSizeDown.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_text_size_down
        'Me.btnSizeDown.ImageTransparentColor = System.Drawing.Color.Magenta
        'Me.btnSizeDown.Name = "btnSizeDown"
        'Me.btnSizeDown.Size = New System.Drawing.Size(85, 102)
        'Me.btnSizeDown.Tag = "SizeDown"
        'Me.btnSizeDown.Text = "Disminuir Fuente"
        'Me.btnSizeDown.ToolTipText = "Disminuir Fuente"
        'Me.btnSizeDown.DisplayStyle = ToolStripItemDisplayStyle.Image
        '
        'BtnCloseTabs
        '
        Me.BtnCloseTabs.ForeColor = System.Drawing.Color.Black
        Me.BtnCloseTabs.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_close
        Me.BtnCloseTabs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnCloseTabs.Name = "BtnCloseTabs"
        Me.BtnCloseTabs.Size = New System.Drawing.Size(136, 102)
        Me.BtnCloseTabs.Text = "Cerrar Documentos"
        Me.BtnCloseTabs.Tag = "CLOSETABS"
        '
        'UCToolbarResult
        '
        Me.AllowItemReorder = True
        Me.BackColor = System.Drawing.Color.White
        Me.CanOverflow = False
        Me.ImageScalingSize = New System.Drawing.Size(22, 22)
        Me.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSaveAs, Me.btnAdjuntarEmail, Me.btnImprimirImagenesIndices, Me.btnAgregarDocumentoCarpeta, Me.btnVerVersionesDelDocumento, Me.btnAgregarUnaNuevaVersionDelDocuemto, Me.btnChangePosition, Me.btnSendZip, Me.BtnCloseTabs})
        Me.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.Name = "UCResultsToolbar"
        Me.Size = New System.Drawing.Size(1012, 105)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnCloseTabs As ToolStripButton
End Class
