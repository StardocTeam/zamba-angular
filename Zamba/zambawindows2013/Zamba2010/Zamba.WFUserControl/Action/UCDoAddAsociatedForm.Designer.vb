<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoAddAsociatedForm
    Inherits Zamba.WFUserControl.ZRuleControl

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
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkSpecificConfig = New System.Windows.Forms.CheckBox()
        Me.chkFillCommonAttributes = New System.Windows.Forms.CheckBox()
        Me.chkDontOpenTaskAfterInsert = New System.Windows.Forms.CheckBox()
        Me.chkContinueWithCurrentTasks = New System.Windows.Forms.CheckBox()
        Me.lbTitulo = New Zamba.AppBlock.ZLabel()
        Me.btSave = New Zamba.AppBlock.ZButton()
        Me.cboFormType = New System.Windows.Forms.ComboBox()
        Me.gvAttribute = New System.Windows.Forms.DataGridView()
        Me.Index_Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ValorIndice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZPanel1 = New Zamba.AppBlock.ZPanel()
        Me.ZPanel2 = New Zamba.AppBlock.ZPanel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gvAttribute, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ZPanel1.SuspendLayout()
        Me.ZPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.gvAttribute)
        Me.tbRule.Controls.Add(Me.ZPanel2)
        Me.tbRule.Controls.Add(Me.ZPanel1)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(745, 594)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(753, 623)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkSpecificConfig)
        Me.GroupBox1.Controls.Add(Me.chkFillCommonAttributes)
        Me.GroupBox1.Controls.Add(Me.chkDontOpenTaskAfterInsert)
        Me.GroupBox1.Controls.Add(Me.chkContinueWithCurrentTasks)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 64)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(597, 140)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Opciones disponibles"
        '
        'chkSpecificConfig
        '
        Me.chkSpecificConfig.AutoSize = True
        Me.chkSpecificConfig.Location = New System.Drawing.Point(9, 112)
        Me.chkSpecificConfig.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSpecificConfig.Name = "chkSpecificConfig"
        Me.chkSpecificConfig.Size = New System.Drawing.Size(262, 20)
        Me.chkSpecificConfig.TabIndex = 3
        Me.chkSpecificConfig.Text = "Configurar por atributos especificos"
        Me.chkSpecificConfig.UseVisualStyleBackColor = True
        '
        'chkFillCommonAttributes
        '
        Me.chkFillCommonAttributes.AutoSize = True
        Me.chkFillCommonAttributes.Location = New System.Drawing.Point(9, 84)
        Me.chkFillCommonAttributes.Margin = New System.Windows.Forms.Padding(4)
        Me.chkFillCommonAttributes.Name = "chkFillCommonAttributes"
        Me.chkFillCommonAttributes.Size = New System.Drawing.Size(226, 20)
        Me.chkFillCommonAttributes.TabIndex = 2
        Me.chkFillCommonAttributes.Text = "Completar atributos en común"
        Me.chkFillCommonAttributes.UseVisualStyleBackColor = True
        '
        'chkDontOpenTaskAfterInsert
        '
        Me.chkDontOpenTaskAfterInsert.AutoSize = True
        Me.chkDontOpenTaskAfterInsert.Location = New System.Drawing.Point(9, 55)
        Me.chkDontOpenTaskAfterInsert.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDontOpenTaskAfterInsert.Name = "chkDontOpenTaskAfterInsert"
        Me.chkDontOpenTaskAfterInsert.Size = New System.Drawing.Size(490, 20)
        Me.chkDontOpenTaskAfterInsert.TabIndex = 1
        Me.chkDontOpenTaskAfterInsert.Text = "No abrir las tareas luego de insertarlas (Disponible solo para windows)"
        Me.chkDontOpenTaskAfterInsert.UseVisualStyleBackColor = True
        '
        'chkContinueWithCurrentTasks
        '
        Me.chkContinueWithCurrentTasks.AutoSize = True
        Me.chkContinueWithCurrentTasks.Location = New System.Drawing.Point(9, 26)
        Me.chkContinueWithCurrentTasks.Margin = New System.Windows.Forms.Padding(4)
        Me.chkContinueWithCurrentTasks.Name = "chkContinueWithCurrentTasks"
        Me.chkContinueWithCurrentTasks.Size = New System.Drawing.Size(327, 20)
        Me.chkContinueWithCurrentTasks.TabIndex = 0
        Me.chkContinueWithCurrentTasks.Text = "Continuar con la ejecución anterior de tareas"
        Me.chkContinueWithCurrentTasks.UseVisualStyleBackColor = True
        '
        'lbTitulo
        '
        Me.lbTitulo.AutoSize = True
        Me.lbTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lbTitulo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTitulo.FontSize = 9.75!
        Me.lbTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lbTitulo.Location = New System.Drawing.Point(13, 12)
        Me.lbTitulo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbTitulo.Name = "lbTitulo"
        Me.lbTitulo.Size = New System.Drawing.Size(234, 16)
        Me.lbTitulo.TabIndex = 12
        Me.lbTitulo.Text = "Seleccione un formulario a asociar"
        Me.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btSave
        '
        Me.btSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btSave.ForeColor = System.Drawing.Color.White
        Me.btSave.Location = New System.Drawing.Point(550, 20)
        Me.btSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(100, 28)
        Me.btSave.TabIndex = 10
        Me.btSave.Text = "Guardar"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'cboFormType
        '
        Me.cboFormType.FormattingEnabled = True
        Me.cboFormType.Location = New System.Drawing.Point(16, 32)
        Me.cboFormType.Margin = New System.Windows.Forms.Padding(4)
        Me.cboFormType.Name = "cboFormType"
        Me.cboFormType.Size = New System.Drawing.Size(596, 24)
        Me.cboFormType.TabIndex = 11
        '
        'gvAttribute
        '
        Me.gvAttribute.AllowUserToAddRows = False
        Me.gvAttribute.AllowUserToDeleteRows = False
        Me.gvAttribute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gvAttribute.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Index_Id, Me.Nombre, Me.ValorIndice})
        Me.gvAttribute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAttribute.Location = New System.Drawing.Point(4, 222)
        Me.gvAttribute.Margin = New System.Windows.Forms.Padding(4)
        Me.gvAttribute.Name = "gvAttribute"
        Me.gvAttribute.Size = New System.Drawing.Size(737, 303)
        Me.gvAttribute.TabIndex = 11
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
        'ZPanel1
        '
        Me.ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel1.Controls.Add(Me.btSave)
        Me.ZPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ZPanel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel1.Location = New System.Drawing.Point(4, 525)
        Me.ZPanel1.Name = "ZPanel1"
        Me.ZPanel1.Size = New System.Drawing.Size(737, 65)
        Me.ZPanel1.TabIndex = 11
        '
        'ZPanel2
        '
        Me.ZPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel2.Controls.Add(Me.GroupBox1)
        Me.ZPanel2.Controls.Add(Me.lbTitulo)
        Me.ZPanel2.Controls.Add(Me.cboFormType)
        Me.ZPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.ZPanel2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel2.Location = New System.Drawing.Point(4, 4)
        Me.ZPanel2.Name = "ZPanel2"
        Me.ZPanel2.Size = New System.Drawing.Size(737, 218)
        Me.ZPanel2.TabIndex = 12
        '
        'UCDoAddAsociatedForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoAddAsociatedForm"
        Me.Size = New System.Drawing.Size(753, 623)
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.gvAttribute, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ZPanel1.ResumeLayout(False)
        Me.ZPanel2.ResumeLayout(False)
        Me.ZPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkFillCommonAttributes As System.Windows.Forms.CheckBox
    Friend WithEvents chkDontOpenTaskAfterInsert As System.Windows.Forms.CheckBox
    Friend WithEvents chkContinueWithCurrentTasks As System.Windows.Forms.CheckBox
    Friend WithEvents lbTitulo As ZLabel
    Friend WithEvents btSave As ZButton
    Friend WithEvents cboFormType As System.Windows.Forms.ComboBox
    Friend WithEvents chkSpecificConfig As System.Windows.Forms.CheckBox
    Friend WithEvents gvAttribute As System.Windows.Forms.DataGridView
    Friend WithEvents Index_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValorIndice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZPanel2 As ZPanel
    Friend WithEvents ZPanel1 As ZPanel
End Class
