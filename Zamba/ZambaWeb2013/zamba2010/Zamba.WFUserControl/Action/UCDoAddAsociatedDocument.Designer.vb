Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCDoAddAsociatedDocument

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Overloads Sub InitializeComponent()
        Me.lstTemplates = New System.Windows.Forms.ListBox()
        Me.grpboxDocument = New System.Windows.Forms.GroupBox()
        Me.rdbword = New System.Windows.Forms.RadioButton()
        Me.rdbexcel = New System.Windows.Forms.RadioButton()
        Me.rdbpowerpoint = New System.Windows.Forms.RadioButton()
        Me.grpboxNone = New System.Windows.Forms.GroupBox()
        Me.rdbInsertForms = New System.Windows.Forms.RadioButton()
        Me.rdbScanDocuments = New System.Windows.Forms.RadioButton()
        Me.rdbInsertFolder = New System.Windows.Forms.RadioButton()
        Me.rdbInsertDocuments = New System.Windows.Forms.RadioButton()
        Me.ChkOpenByDefault = New System.Windows.Forms.CheckBox()
        Me.gvAttribute = New System.Windows.Forms.DataGridView()
        Me.Index_Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ValorIndice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkSpecificConfig = New System.Windows.Forms.CheckBox()
        Me.chkDontOpenIfAsociated = New System.Windows.Forms.CheckBox()
        Me.GrpTemplate = New System.Windows.Forms.GroupBox()
        Me.rdbDocumento = New System.Windows.Forms.RadioButton()
        Me.rdbTemplate = New System.Windows.Forms.RadioButton()
        Me.rdbNinguno = New System.Windows.Forms.RadioButton()
        Me.pnlOptions = New System.Windows.Forms.Panel()
        Me.lbTitulo = New Zamba.AppBlock.ZLabel()
        Me.cboTiposDeDocumento = New System.Windows.Forms.ComboBox()
        Me.btnSave = New Zamba.AppBlock.ZButton()
        Me.lblOk = New Zamba.AppBlock.ZLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.lstautomaticvariables = New System.Windows.Forms.ListBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.grpboxNone.SuspendLayout()
        CType(Me.gvAttribute, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTemplate.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbState.Size = New System.Drawing.Size(824, 781)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbHabilitation.Size = New System.Drawing.Size(824, 781)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbConfiguration.Size = New System.Drawing.Size(824, 781)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Size = New System.Drawing.Size(824, 781)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.Panel2)
        Me.tbRule.Controls.Add(Me.Panel1)
        Me.tbRule.Controls.Add(Me.Panel3)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(827, 594)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(835, 623)
        '
        'lstTemplates
        '
        Me.lstTemplates.Location = New System.Drawing.Point(0, 0)
        Me.lstTemplates.Name = "lstTemplates"
        Me.lstTemplates.Size = New System.Drawing.Size(120, 95)
        Me.lstTemplates.TabIndex = 0
        '
        'grpboxDocument
        '
        Me.grpboxDocument.Location = New System.Drawing.Point(0, 0)
        Me.grpboxDocument.Name = "grpboxDocument"
        Me.grpboxDocument.Size = New System.Drawing.Size(200, 100)
        Me.grpboxDocument.TabIndex = 0
        Me.grpboxDocument.TabStop = False
        '
        'rdbword
        '
        Me.rdbword.Location = New System.Drawing.Point(0, 0)
        Me.rdbword.Name = "rdbword"
        Me.rdbword.Size = New System.Drawing.Size(104, 24)
        Me.rdbword.TabIndex = 0
        '
        'rdbexcel
        '
        Me.rdbexcel.Location = New System.Drawing.Point(0, 0)
        Me.rdbexcel.Name = "rdbexcel"
        Me.rdbexcel.Size = New System.Drawing.Size(104, 24)
        Me.rdbexcel.TabIndex = 0
        '
        'rdbpowerpoint
        '
        Me.rdbpowerpoint.Location = New System.Drawing.Point(0, 0)
        Me.rdbpowerpoint.Name = "rdbpowerpoint"
        Me.rdbpowerpoint.Size = New System.Drawing.Size(104, 24)
        Me.rdbpowerpoint.TabIndex = 0
        '
        'grpboxNone
        '
        Me.grpboxNone.Controls.Add(Me.rdbInsertForms)
        Me.grpboxNone.Controls.Add(Me.rdbScanDocuments)
        Me.grpboxNone.Controls.Add(Me.rdbInsertFolder)
        Me.grpboxNone.Controls.Add(Me.rdbInsertDocuments)
        Me.grpboxNone.Controls.Add(Me.ChkOpenByDefault)
        Me.grpboxNone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpboxNone.Location = New System.Drawing.Point(0, 0)
        Me.grpboxNone.Name = "grpboxNone"
        Me.grpboxNone.Size = New System.Drawing.Size(290, 197)
        Me.grpboxNone.TabIndex = 5
        Me.grpboxNone.TabStop = False
        '
        'rdbInsertForms
        '
        Me.rdbInsertForms.AutoSize = True
        Me.rdbInsertForms.Location = New System.Drawing.Point(51, 123)
        Me.rdbInsertForms.Name = "rdbInsertForms"
        Me.rdbInsertForms.Size = New System.Drawing.Size(116, 17)
        Me.rdbInsertForms.TabIndex = 4
        Me.rdbInsertForms.TabStop = True
        Me.rdbInsertForms.Text = "Insertar Formularios"
        Me.rdbInsertForms.UseVisualStyleBackColor = True
        '
        'rdbScanDocuments
        '
        Me.rdbScanDocuments.AutoSize = True
        Me.rdbScanDocuments.Location = New System.Drawing.Point(51, 100)
        Me.rdbScanDocuments.Name = "rdbScanDocuments"
        Me.rdbScanDocuments.Size = New System.Drawing.Size(133, 17)
        Me.rdbScanDocuments.TabIndex = 3
        Me.rdbScanDocuments.TabStop = True
        Me.rdbScanDocuments.Text = "Escanear Documentos"
        Me.rdbScanDocuments.UseVisualStyleBackColor = True
        '
        'rdbInsertFolder
        '
        Me.rdbInsertFolder.AutoSize = True
        Me.rdbInsertFolder.Location = New System.Drawing.Point(51, 77)
        Me.rdbInsertFolder.Name = "rdbInsertFolder"
        Me.rdbInsertFolder.Size = New System.Drawing.Size(100, 17)
        Me.rdbInsertFolder.TabIndex = 2
        Me.rdbInsertFolder.TabStop = True
        Me.rdbInsertFolder.Text = "Insertar Carpeta"
        Me.rdbInsertFolder.UseVisualStyleBackColor = True
        '
        'rdbInsertDocuments
        '
        Me.rdbInsertDocuments.AutoSize = True
        Me.rdbInsertDocuments.Location = New System.Drawing.Point(51, 54)
        Me.rdbInsertDocuments.Name = "rdbInsertDocuments"
        Me.rdbInsertDocuments.Size = New System.Drawing.Size(123, 17)
        Me.rdbInsertDocuments.TabIndex = 1
        Me.rdbInsertDocuments.TabStop = True
        Me.rdbInsertDocuments.Text = "Insertar Documentos"
        Me.rdbInsertDocuments.UseVisualStyleBackColor = True
        '
        'ChkOpenByDefault
        '
        Me.ChkOpenByDefault.AutoSize = True
        Me.ChkOpenByDefault.Location = New System.Drawing.Point(10, 20)
        Me.ChkOpenByDefault.Name = "ChkOpenByDefault"
        Me.ChkOpenByDefault.Size = New System.Drawing.Size(145, 17)
        Me.ChkOpenByDefault.TabIndex = 0
        Me.ChkOpenByDefault.Text = "Abrir Pantalla por defecto"
        Me.ChkOpenByDefault.UseVisualStyleBackColor = True
        '
        'gvAttribute
        '
        Me.gvAttribute.AllowUserToAddRows = False
        Me.gvAttribute.AllowUserToDeleteRows = False
        Me.gvAttribute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gvAttribute.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Index_Id, Me.Nombre, Me.ValorIndice})
        Me.gvAttribute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAttribute.Location = New System.Drawing.Point(0, 0)
        Me.gvAttribute.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gvAttribute.Name = "gvAttribute"
        Me.gvAttribute.Size = New System.Drawing.Size(407, 531)
        Me.gvAttribute.TabIndex = 21
        '
        'Index_Id
        '
        Me.Index_Id.DataPropertyName = "Index_Id"
        Me.Index_Id.HeaderText = "ID Atributo"
        Me.Index_Id.Name = "Index_Id"
        '
        'Nombre
        '
        Me.Nombre.DataPropertyName = "Index_Name"
        Me.Nombre.HeaderText = "Nombre Atributo"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.ReadOnly = True
        '
        'ValorIndice
        '
        Me.ValorIndice.DataPropertyName = "ValorIndice"
        Me.ValorIndice.HeaderText = "Valor Atributo"
        Me.ValorIndice.Name = "ValorIndice"
        '
        'chkSpecificConfig
        '
        Me.chkSpecificConfig.AutoSize = True
        Me.chkSpecificConfig.Location = New System.Drawing.Point(17, 366)
        Me.chkSpecificConfig.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkSpecificConfig.Name = "chkSpecificConfig"
        Me.chkSpecificConfig.Size = New System.Drawing.Size(262, 20)
        Me.chkSpecificConfig.TabIndex = 20
        Me.chkSpecificConfig.Text = "Configurar por atributos específicos"
        Me.chkSpecificConfig.UseVisualStyleBackColor = True
        '
        'chkDontOpenIfAsociated
        '
        Me.chkDontOpenIfAsociated.AutoSize = True
        Me.chkDontOpenIfAsociated.Location = New System.Drawing.Point(16, 337)
        Me.chkDontOpenIfAsociated.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkDontOpenIfAsociated.Name = "chkDontOpenIfAsociated"
        Me.chkDontOpenIfAsociated.Size = New System.Drawing.Size(324, 20)
        Me.chkDontOpenIfAsociated.TabIndex = 19
        Me.chkDontOpenIfAsociated.Text = "No abrir el documento luego de ser insertado"
        Me.chkDontOpenIfAsociated.UseVisualStyleBackColor = True
        '
        'GrpTemplate
        '
        Me.GrpTemplate.BackColor = System.Drawing.Color.Transparent
        Me.GrpTemplate.Controls.Add(Me.rdbDocumento)
        Me.GrpTemplate.Controls.Add(Me.rdbTemplate)
        Me.GrpTemplate.Controls.Add(Me.rdbNinguno)
        Me.GrpTemplate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.GrpTemplate.Location = New System.Drawing.Point(17, 57)
        Me.GrpTemplate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GrpTemplate.Name = "GrpTemplate"
        Me.GrpTemplate.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GrpTemplate.Size = New System.Drawing.Size(375, 55)
        Me.GrpTemplate.TabIndex = 18
        Me.GrpTemplate.TabStop = False
        Me.GrpTemplate.Text = "Preconfigurar Selección"
        '
        'rdbDocumento
        '
        Me.rdbDocumento.AutoSize = True
        Me.rdbDocumento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.rdbDocumento.Location = New System.Drawing.Point(239, 25)
        Me.rdbDocumento.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbDocumento.Name = "rdbDocumento"
        Me.rdbDocumento.Size = New System.Drawing.Size(100, 20)
        Me.rdbDocumento.TabIndex = 2
        Me.rdbDocumento.Text = "Documento"
        Me.rdbDocumento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.rdbDocumento.UseVisualStyleBackColor = True
        '
        'rdbTemplate
        '
        Me.rdbTemplate.AutoSize = True
        Me.rdbTemplate.Location = New System.Drawing.Point(125, 25)
        Me.rdbTemplate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbTemplate.Name = "rdbTemplate"
        Me.rdbTemplate.Size = New System.Drawing.Size(76, 20)
        Me.rdbTemplate.TabIndex = 1
        Me.rdbTemplate.Text = "Plantilla"
        Me.rdbTemplate.UseVisualStyleBackColor = True
        '
        'rdbNinguno
        '
        Me.rdbNinguno.AutoSize = True
        Me.rdbNinguno.Checked = True
        Me.rdbNinguno.Location = New System.Drawing.Point(8, 25)
        Me.rdbNinguno.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbNinguno.Name = "rdbNinguno"
        Me.rdbNinguno.Size = New System.Drawing.Size(78, 20)
        Me.rdbNinguno.TabIndex = 0
        Me.rdbNinguno.TabStop = True
        Me.rdbNinguno.Text = "Ninguno"
        Me.rdbNinguno.UseVisualStyleBackColor = True
        '
        'pnlOptions
        '
        Me.pnlOptions.Location = New System.Drawing.Point(17, 119)
        Me.pnlOptions.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlOptions.Name = "pnlOptions"
        Me.pnlOptions.Size = New System.Drawing.Size(375, 210)
        Me.pnlOptions.TabIndex = 17
        '
        'lbTitulo
        '
        Me.lbTitulo.AutoSize = True
        Me.lbTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lbTitulo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTitulo.FontSize = 9.75!
        Me.lbTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lbTitulo.Location = New System.Drawing.Point(16, 16)
        Me.lbTitulo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbTitulo.Name = "lbTitulo"
        Me.lbTitulo.Size = New System.Drawing.Size(122, 16)
        Me.lbTitulo.TabIndex = 16
        Me.lbTitulo.Text = "Entidad a asociar"
        Me.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboTiposDeDocumento
        '
        Me.cboTiposDeDocumento.FormattingEnabled = True
        Me.cboTiposDeDocumento.Location = New System.Drawing.Point(143, 12)
        Me.cboTiposDeDocumento.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboTiposDeDocumento.Name = "cboTiposDeDocumento"
        Me.cboTiposDeDocumento.Size = New System.Drawing.Size(248, 24)
        Me.cboTiposDeDocumento.TabIndex = 15
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(584, 12)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(131, 33)
        Me.btnSave.TabIndex = 22
        Me.btnSave.Text = "Guardar"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'lblOk
        '
        Me.lblOk.AutoSize = True
        Me.lblOk.BackColor = System.Drawing.Color.Transparent
        Me.lblOk.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOk.FontSize = 9.75!
        Me.lblOk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblOk.Location = New System.Drawing.Point(155, 436)
        Me.lblOk.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOk.Name = "lblOk"
        Me.lblOk.Size = New System.Drawing.Size(0, 16)
        Me.lblOk.TabIndex = 23
        Me.lblOk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lstautomaticvariables)
        Me.Panel1.Controls.Add(Me.lbTitulo)
        Me.Panel1.Controls.Add(Me.cboTiposDeDocumento)
        Me.Panel1.Controls.Add(Me.pnlOptions)
        Me.Panel1.Controls.Add(Me.lblOk)
        Me.Panel1.Controls.Add(Me.GrpTemplate)
        Me.Panel1.Controls.Add(Me.chkSpecificConfig)
        Me.Panel1.Controls.Add(Me.chkDontOpenIfAsociated)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(4, 4)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(412, 531)
        Me.Panel1.TabIndex = 24
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.FontSize = 9.75!
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(16, 405)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(153, 16)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Variables Automaticas"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstautomaticvariables
        '
        Me.lstautomaticvariables.BackColor = System.Drawing.Color.White
        Me.lstautomaticvariables.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstautomaticvariables.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lstautomaticvariables.FormattingEnabled = True
        Me.lstautomaticvariables.ItemHeight = 16
        Me.lstautomaticvariables.Location = New System.Drawing.Point(20, 425)
        Me.lstautomaticvariables.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstautomaticvariables.Name = "lstautomaticvariables"
        Me.lstautomaticvariables.Size = New System.Drawing.Size(339, 80)
        Me.lstautomaticvariables.TabIndex = 29
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.gvAttribute)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(416, 4)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(407, 531)
        Me.Panel2.TabIndex = 25
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnSave)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(4, 535)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(819, 55)
        Me.Panel3.TabIndex = 26
        '
        'UCDoAddAsociatedDocument
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoAddAsociatedDocument"
        Me.Size = New System.Drawing.Size(835, 623)
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.grpboxNone.ResumeLayout(False)
        Me.grpboxNone.PerformLayout()
        CType(Me.gvAttribute, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTemplate.ResumeLayout(False)
        Me.GrpTemplate.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstTemplates As ListBox
    Friend WithEvents grpboxDocument As GroupBox
    Friend WithEvents rdbword As RadioButton
    Friend WithEvents rdbexcel As RadioButton
    Friend WithEvents rdbpowerpoint As RadioButton

    Friend WithEvents grpboxNone As System.Windows.Forms.GroupBox
    Friend WithEvents ChkOpenByDefault As System.Windows.Forms.CheckBox
    Friend WithEvents rdbScanDocuments As System.Windows.Forms.RadioButton
    Friend WithEvents rdbInsertFolder As System.Windows.Forms.RadioButton
    Friend WithEvents rdbInsertDocuments As System.Windows.Forms.RadioButton
    Friend WithEvents rdbInsertForms As System.Windows.Forms.RadioButton
    Friend WithEvents gvAttribute As System.Windows.Forms.DataGridView
    Friend WithEvents Index_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValorIndice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSpecificConfig As System.Windows.Forms.CheckBox
    Friend WithEvents chkDontOpenIfAsociated As System.Windows.Forms.CheckBox
    Friend WithEvents GrpTemplate As System.Windows.Forms.GroupBox
    Friend WithEvents rdbDocumento As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTemplate As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNinguno As System.Windows.Forms.RadioButton
    Friend WithEvents pnlOptions As System.Windows.Forms.Panel
    Friend WithEvents lbTitulo As ZLabel
    Friend WithEvents cboTiposDeDocumento As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As Zamba.AppBlock.ZButton
    Friend WithEvents lblOk As ZLabel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents lstautomaticvariables As System.Windows.Forms.ListBox

End Class
