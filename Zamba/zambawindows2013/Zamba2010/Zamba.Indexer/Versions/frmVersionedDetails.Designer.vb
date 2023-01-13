<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVersionedDetails
    Inherits Zamba.AppBlock.ZForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVersionedDetails))
        Me.treeViewVersionedDetails = New System.Windows.Forms.TreeView()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.lblComment = New Zamba.AppBlock.ZLabel()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.lblFechaCreacion = New Zamba.AppBlock.ZLabel()
        Me.lblUsuarioCreador = New Zamba.AppBlock.ZLabel()
        Me.lblFechaEdicion = New Zamba.AppBlock.ZLabel()
        Me.txtComentarioVersion = New System.Windows.Forms.TextBox()
        Me.GBVersion = New System.Windows.Forms.GroupBox()
        Me.lblVersionNum = New Zamba.AppBlock.ZLabel()
        Me.lblDoc = New Zamba.AppBlock.ZLabel()
        Me.lblPublicationDate = New Zamba.AppBlock.ZLabel()
        Me.lblVer = New Zamba.AppBlock.ZLabel()
        Me.lblDocName = New Zamba.AppBlock.ZLabel()
        Me.lblFP = New Zamba.AppBlock.ZLabel()
        Me.lblUserPublish = New Zamba.AppBlock.ZLabel()
        Me.lblPublish = New Zamba.AppBlock.ZLabel()
        Me.lblUPub = New Zamba.AppBlock.ZLabel()
        Me.lblPub = New Zamba.AppBlock.ZLabel()
        Me.ZPanel1 = New Zamba.AppBlock.ZPanel()
        Me.ZToolBar1 = New Zamba.AppBlock.ZToolBar()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.GBVersion.SuspendLayout
        Me.ZPanel1.SuspendLayout
        Me.ZToolBar1.SuspendLayout
        Me.SuspendLayout
        '
        'treeViewVersionedDetails
        '
        Me.treeViewVersionedDetails.BackColor = System.Drawing.Color.White
        Me.treeViewVersionedDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.treeViewVersionedDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeViewVersionedDetails.ForeColor = System.Drawing.Color.Black
        Me.treeViewVersionedDetails.FullRowSelect = true
        Me.treeViewVersionedDetails.Indent = 20
        Me.treeViewVersionedDetails.ItemHeight = 20
        Me.treeViewVersionedDetails.Location = New System.Drawing.Point(0, 30)
        Me.treeViewVersionedDetails.Margin = New System.Windows.Forms.Padding(4)
        Me.treeViewVersionedDetails.Name = "treeViewVersionedDetails"
        Me.treeViewVersionedDetails.ShowNodeToolTips = true
        Me.treeViewVersionedDetails.Size = New System.Drawing.Size(337, 315)
        Me.treeViewVersionedDetails.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.Label1.Location = New System.Drawing.Point(7, 71)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fecha de Creación:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.Label2.Location = New System.Drawing.Point(7, 95)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Creado por:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblComment
        '
        Me.lblComment.AutoSize = true
        Me.lblComment.BackColor = System.Drawing.Color.Transparent
        Me.lblComment.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblComment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblComment.Location = New System.Drawing.Point(7, 232)
        Me.lblComment.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(161, 16)
        Me.lblComment.TabIndex = 4
        Me.lblComment.Text = "Comentario de Version:"
        Me.lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.Label3.Location = New System.Drawing.Point(7, 119)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Fecha de Edición:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFechaCreacion
        '
        Me.lblFechaCreacion.AutoSize = true
        Me.lblFechaCreacion.BackColor = System.Drawing.Color.Transparent
        Me.lblFechaCreacion.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFechaCreacion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblFechaCreacion.Location = New System.Drawing.Point(152, 71)
        Me.lblFechaCreacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFechaCreacion.Name = "lblFechaCreacion"
        Me.lblFechaCreacion.Size = New System.Drawing.Size(50, 16)
        Me.lblFechaCreacion.TabIndex = 5
        Me.lblFechaCreacion.Text = "Label1"
        Me.lblFechaCreacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUsuarioCreador
        '
        Me.lblUsuarioCreador.AutoSize = true
        Me.lblUsuarioCreador.BackColor = System.Drawing.Color.Transparent
        Me.lblUsuarioCreador.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblUsuarioCreador.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblUsuarioCreador.Location = New System.Drawing.Point(101, 95)
        Me.lblUsuarioCreador.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUsuarioCreador.Name = "lblUsuarioCreador"
        Me.lblUsuarioCreador.Size = New System.Drawing.Size(50, 16)
        Me.lblUsuarioCreador.TabIndex = 6
        Me.lblUsuarioCreador.Text = "Label2"
        Me.lblUsuarioCreador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFechaEdicion
        '
        Me.lblFechaEdicion.AutoSize = true
        Me.lblFechaEdicion.BackColor = System.Drawing.Color.Transparent
        Me.lblFechaEdicion.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFechaEdicion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblFechaEdicion.Location = New System.Drawing.Point(141, 119)
        Me.lblFechaEdicion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFechaEdicion.Name = "lblFechaEdicion"
        Me.lblFechaEdicion.Size = New System.Drawing.Size(50, 16)
        Me.lblFechaEdicion.TabIndex = 7
        Me.lblFechaEdicion.Text = "Label3"
        Me.lblFechaEdicion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtComentarioVersion
        '
        Me.txtComentarioVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtComentarioVersion.BackColor = System.Drawing.Color.White
        Me.txtComentarioVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComentarioVersion.Enabled = false
        Me.txtComentarioVersion.ForeColor = System.Drawing.Color.Black
        Me.txtComentarioVersion.Location = New System.Drawing.Point(7, 252)
        Me.txtComentarioVersion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtComentarioVersion.Multiline = true
        Me.txtComentarioVersion.Name = "txtComentarioVersion"
        Me.txtComentarioVersion.ReadOnly = true
        Me.txtComentarioVersion.Size = New System.Drawing.Size(415, 78)
        Me.txtComentarioVersion.TabIndex = 8
        '
        'GBVersion
        '
        Me.GBVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.GBVersion.Controls.Add(Me.lblVersionNum)
        Me.GBVersion.Controls.Add(Me.lblDoc)
        Me.GBVersion.Controls.Add(Me.lblPublicationDate)
        Me.GBVersion.Controls.Add(Me.lblFechaCreacion)
        Me.GBVersion.Controls.Add(Me.lblFechaEdicion)
        Me.GBVersion.Controls.Add(Me.txtComentarioVersion)
        Me.GBVersion.Controls.Add(Me.lblUsuarioCreador)
        Me.GBVersion.Controls.Add(Me.lblVer)
        Me.GBVersion.Controls.Add(Me.lblComment)
        Me.GBVersion.Controls.Add(Me.lblDocName)
        Me.GBVersion.Controls.Add(Me.lblFP)
        Me.GBVersion.Controls.Add(Me.Label1)
        Me.GBVersion.Controls.Add(Me.lblUserPublish)
        Me.GBVersion.Controls.Add(Me.lblPublish)
        Me.GBVersion.Controls.Add(Me.lblUPub)
        Me.GBVersion.Controls.Add(Me.lblPub)
        Me.GBVersion.Controls.Add(Me.Label3)
        Me.GBVersion.Controls.Add(Me.Label2)
        Me.GBVersion.Location = New System.Drawing.Point(347, 5)
        Me.GBVersion.Name = "GBVersion"
        Me.GBVersion.Size = New System.Drawing.Size(428, 339)
        Me.GBVersion.TabIndex = 9
        Me.GBVersion.TabStop = false
        Me.GBVersion.Text = "Detalles de la Version"
        '
        'lblVersionNum
        '
        Me.lblVersionNum.AutoSize = true
        Me.lblVersionNum.BackColor = System.Drawing.Color.Transparent
        Me.lblVersionNum.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblVersionNum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblVersionNum.Location = New System.Drawing.Point(151, 47)
        Me.lblVersionNum.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVersionNum.Name = "lblVersionNum"
        Me.lblVersionNum.Size = New System.Drawing.Size(91, 16)
        Me.lblVersionNum.TabIndex = 5
        Me.lblVersionNum.Text = "LabelVerNum"
        Me.lblVersionNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDoc
        '
        Me.lblDoc.AutoSize = true
        Me.lblDoc.BackColor = System.Drawing.Color.Transparent
        Me.lblDoc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblDoc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblDoc.Location = New System.Drawing.Point(103, 23)
        Me.lblDoc.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDoc.Name = "lblDoc"
        Me.lblDoc.Size = New System.Drawing.Size(103, 16)
        Me.lblDoc.TabIndex = 5
        Me.lblDoc.Text = "LabelDocName"
        Me.lblDoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPublicationDate
        '
        Me.lblPublicationDate.AutoSize = true
        Me.lblPublicationDate.BackColor = System.Drawing.Color.Transparent
        Me.lblPublicationDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPublicationDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblPublicationDate.Location = New System.Drawing.Point(168, 198)
        Me.lblPublicationDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPublicationDate.Name = "lblPublicationDate"
        Me.lblPublicationDate.Size = New System.Drawing.Size(97, 16)
        Me.lblPublicationDate.TabIndex = 5
        Me.lblPublicationDate.Text = "LabelPubDate"
        Me.lblPublicationDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblVer
        '
        Me.lblVer.AutoSize = true
        Me.lblVer.BackColor = System.Drawing.Color.Transparent
        Me.lblVer.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblVer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblVer.Location = New System.Drawing.Point(7, 47)
        Me.lblVer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVer.Name = "lblVer"
        Me.lblVer.Size = New System.Drawing.Size(136, 16)
        Me.lblVer.TabIndex = 1
        Me.lblVer.Text = "Numero de Version:"
        Me.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDocName
        '
        Me.lblDocName.AutoSize = true
        Me.lblDocName.BackColor = System.Drawing.Color.Transparent
        Me.lblDocName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblDocName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblDocName.Location = New System.Drawing.Point(7, 23)
        Me.lblDocName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDocName.Name = "lblDocName"
        Me.lblDocName.Size = New System.Drawing.Size(88, 16)
        Me.lblDocName.TabIndex = 1
        Me.lblDocName.Text = "Documento:"
        Me.lblDocName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFP
        '
        Me.lblFP.AutoSize = true
        Me.lblFP.BackColor = System.Drawing.Color.Transparent
        Me.lblFP.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFP.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblFP.Location = New System.Drawing.Point(7, 198)
        Me.lblFP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFP.Name = "lblFP"
        Me.lblFP.Size = New System.Drawing.Size(153, 16)
        Me.lblFP.TabIndex = 1
        Me.lblFP.Text = "Fecha de Publicación:"
        Me.lblFP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserPublish
        '
        Me.lblUserPublish.AutoSize = true
        Me.lblUserPublish.BackColor = System.Drawing.Color.Transparent
        Me.lblUserPublish.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblUserPublish.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblUserPublish.Location = New System.Drawing.Point(117, 174)
        Me.lblUserPublish.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUserPublish.Name = "lblUserPublish"
        Me.lblUserPublish.Size = New System.Drawing.Size(125, 16)
        Me.lblUserPublish.TabIndex = 3
        Me.lblUserPublish.Text = "labelUserPublisher"
        Me.lblUserPublish.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPublish
        '
        Me.lblPublish.AutoSize = true
        Me.lblPublish.BackColor = System.Drawing.Color.Transparent
        Me.lblPublish.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPublish.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblPublish.Location = New System.Drawing.Point(91, 150)
        Me.lblPublish.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPublish.Name = "lblPublish"
        Me.lblPublish.Size = New System.Drawing.Size(95, 16)
        Me.lblPublish.TabIndex = 3
        Me.lblPublish.Text = "labelIsPublish"
        Me.lblPublish.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUPub
        '
        Me.lblUPub.AutoSize = true
        Me.lblUPub.BackColor = System.Drawing.Color.Transparent
        Me.lblUPub.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblUPub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblUPub.Location = New System.Drawing.Point(7, 174)
        Me.lblUPub.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUPub.Name = "lblUPub"
        Me.lblUPub.Size = New System.Drawing.Size(102, 16)
        Me.lblUPub.TabIndex = 3
        Me.lblUPub.Text = "Publicado por:"
        Me.lblUPub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPub
        '
        Me.lblPub.AutoSize = true
        Me.lblPub.BackColor = System.Drawing.Color.Transparent
        Me.lblPub.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblPub.Location = New System.Drawing.Point(7, 150)
        Me.lblPub.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPub.Name = "lblPub"
        Me.lblPub.Size = New System.Drawing.Size(76, 16)
        Me.lblPub.TabIndex = 3
        Me.lblPub.Text = "Publicado:"
        Me.lblPub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZPanel1
        '
        Me.ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(236,Byte),Integer), CType(CType(236,Byte),Integer))
        Me.ZPanel1.Controls.Add(Me.treeViewVersionedDetails)
        Me.ZPanel1.Controls.Add(Me.ZToolBar1)
        Me.ZPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ZPanel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.ZPanel1.Location = New System.Drawing.Point(3, 2)
        Me.ZPanel1.Name = "ZPanel1"
        Me.ZPanel1.Size = New System.Drawing.Size(337, 345)
        Me.ZPanel1.TabIndex = 11
        '
        'ZToolBar1
        '
        Me.ZToolBar1.AutoSize = false
        Me.ZToolBar1.BackColor = System.Drawing.Color.White
        Me.ZToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ZToolBar1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2})
        Me.ZToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ZToolBar1.Name = "ZToolBar1"
        Me.ZToolBar1.Size = New System.Drawing.Size(337, 30)
        Me.ZToolBar1.TabIndex = 1
        Me.ZToolBar1.Text = "ZToolBar1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.AutoSize = false
        Me.ToolStripButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(157,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton1.ForeColor = System.Drawing.Color.White
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"),System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Margin = New System.Windows.Forms.Padding(0, 1, 5, 2)
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(95, 22)
        Me.ToolStripButton1.Text = "Abrir Version"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.ToolStripButton1.ToolTipText = "Abrir Version Seleccionada"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.AutoSize = false
        Me.ToolStripButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(157,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton2.ForeColor = System.Drawing.Color.White
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"),System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(95, 22)
        Me.ToolStripButton2.Text = "Publicar Versión"
        Me.ToolStripButton2.ToolTipText = "Publicar Versión Seleccionada"
        '
        'frmVersionedDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(781, 349)
        Me.Controls.Add(Me.GBVersion)
        Me.Controls.Add(Me.ZPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmVersionedDetails"
        Me.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Versionado"
        Me.GBVersion.ResumeLayout(false)
        Me.GBVersion.PerformLayout
        Me.ZPanel1.ResumeLayout(false)
        Me.ZToolBar1.ResumeLayout(false)
        Me.ZToolBar1.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents treeViewVersionedDetails As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents lblComment As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents lblFechaCreacion As ZLabel
    Friend WithEvents lblUsuarioCreador As ZLabel
    Friend WithEvents lblFechaEdicion As ZLabel
    Friend WithEvents txtComentarioVersion As System.Windows.Forms.TextBox
    Friend WithEvents GBVersion As GroupBox
    Friend WithEvents lblDocName As ZLabel
    Friend WithEvents lblDoc As ZLabel
    Friend WithEvents lblVersionNum As ZLabel
    Friend WithEvents lblVer As ZLabel
    Friend WithEvents lblUserPublish As ZLabel
    Friend WithEvents lblPublish As ZLabel
    Friend WithEvents lblUPub As ZLabel
    Friend WithEvents lblPub As ZLabel
    Friend WithEvents lblPublicationDate As ZLabel
    Friend WithEvents lblFP As ZLabel
    Friend WithEvents ZPanel1 As ZPanel
    Friend WithEvents ZToolBar1 As ZToolBar
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
End Class
