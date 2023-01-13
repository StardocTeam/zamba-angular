<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCSearchIndexHyerachical
    Inherits Zamba.AppBlock.ZControl

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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnGenerarIndiceJerarquico = New Zamba.AppBlock.ZButton()
        Me.pnlAsociacionIndices = New System.Windows.Forms.Panel()
        Me.dgHierarchicalValues = New System.Windows.Forms.DataGridView()
        Me.ZPanel = New Zamba.AppBlock.ZPanel()
        Me.btEliminar = New Zamba.AppBlock.ZButton()
        Me.BtnInsert = New Zamba.AppBlock.ZButton()
        Me.btnEliminarIndiceJerarquico = New Zamba.AppBlock.ZButton()
        Me.lblAsociadoCon = New Zamba.AppBlock.ZLabel()
        Me.Panel2.SuspendLayout()
        Me.pnlAsociacionIndices.SuspendLayout()
        CType(Me.dgHierarchicalValues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ZPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnGenerarIndiceJerarquico)
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(380, 119)
        Me.Panel2.TabIndex = 1
        '
        'btnGenerarIndiceJerarquico
        '
        Me.btnGenerarIndiceJerarquico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerarIndiceJerarquico.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnGenerarIndiceJerarquico.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerarIndiceJerarquico.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerarIndiceJerarquico.ForeColor = System.Drawing.Color.White
        Me.btnGenerarIndiceJerarquico.Location = New System.Drawing.Point(0, 38)
        Me.btnGenerarIndiceJerarquico.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGenerarIndiceJerarquico.Name = "btnGenerarIndiceJerarquico"
        Me.btnGenerarIndiceJerarquico.Size = New System.Drawing.Size(376, 33)
        Me.btnGenerarIndiceJerarquico.TabIndex = 75
        Me.btnGenerarIndiceJerarquico.Text = "Generar atributo jerarquico"
        Me.btnGenerarIndiceJerarquico.UseVisualStyleBackColor = False
        '
        'pnlAsociacionIndices
        '
        Me.pnlAsociacionIndices.Controls.Add(Me.dgHierarchicalValues)
        Me.pnlAsociacionIndices.Controls.Add(Me.ZPanel)
        Me.pnlAsociacionIndices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAsociacionIndices.Location = New System.Drawing.Point(0, 0)
        Me.pnlAsociacionIndices.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlAsociacionIndices.Name = "pnlAsociacionIndices"
        Me.pnlAsociacionIndices.Size = New System.Drawing.Size(376, 761)
        Me.pnlAsociacionIndices.TabIndex = 0
        '
        'dgHierarchicalValues
        '
        Me.dgHierarchicalValues.AllowUserToAddRows = False
        Me.dgHierarchicalValues.AllowUserToDeleteRows = False
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.MidnightBlue
        Me.dgHierarchicalValues.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        Me.dgHierarchicalValues.BackgroundColor = System.Drawing.Color.Lavender
        Me.dgHierarchicalValues.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgHierarchicalValues.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgHierarchicalValues.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgHierarchicalValues.DefaultCellStyle = DataGridViewCellStyle11
        Me.dgHierarchicalValues.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgHierarchicalValues.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgHierarchicalValues.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.dgHierarchicalValues.GridColor = System.Drawing.Color.MidnightBlue
        Me.dgHierarchicalValues.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.dgHierarchicalValues.Location = New System.Drawing.Point(0, 128)
        Me.dgHierarchicalValues.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgHierarchicalValues.MultiSelect = False
        Me.dgHierarchicalValues.Name = "dgHierarchicalValues"
        Me.dgHierarchicalValues.ReadOnly = True
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgHierarchicalValues.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgHierarchicalValues.Size = New System.Drawing.Size(376, 633)
        Me.dgHierarchicalValues.TabIndex = 86
        '
        'ZPanel
        '
        Me.ZPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.ZPanel.Controls.Add(Me.btEliminar)
        Me.ZPanel.Controls.Add(Me.BtnInsert)
        Me.ZPanel.Controls.Add(Me.btnEliminarIndiceJerarquico)
        Me.ZPanel.Controls.Add(Me.lblAsociadoCon)
        Me.ZPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.ZPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel.Location = New System.Drawing.Point(0, 0)
        Me.ZPanel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ZPanel.Name = "ZPanel"
        Me.ZPanel.Size = New System.Drawing.Size(376, 128)
        Me.ZPanel.TabIndex = 87
        '
        'btEliminar
        '
        Me.btEliminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btEliminar.ForeColor = System.Drawing.Color.White
        Me.btEliminar.Location = New System.Drawing.Point(200, 79)
        Me.btEliminar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btEliminar.Name = "btEliminar"
        Me.btEliminar.Size = New System.Drawing.Size(172, 36)
        Me.btEliminar.TabIndex = 86
        Me.btEliminar.Text = "Quitar Relacion"
        Me.btEliminar.UseVisualStyleBackColor = False
        '
        'BtnInsert
        '
        Me.BtnInsert.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnInsert.ForeColor = System.Drawing.Color.White
        Me.BtnInsert.Location = New System.Drawing.Point(8, 79)
        Me.BtnInsert.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnInsert.Name = "BtnInsert"
        Me.BtnInsert.Size = New System.Drawing.Size(184, 36)
        Me.BtnInsert.TabIndex = 85
        Me.BtnInsert.Text = "Insertar Relacion"
        Me.BtnInsert.UseVisualStyleBackColor = False
        '
        'btnEliminarIndiceJerarquico
        '
        Me.btnEliminarIndiceJerarquico.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnEliminarIndiceJerarquico.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEliminarIndiceJerarquico.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarIndiceJerarquico.ForeColor = System.Drawing.Color.White
        Me.btnEliminarIndiceJerarquico.Location = New System.Drawing.Point(8, 36)
        Me.btnEliminarIndiceJerarquico.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnEliminarIndiceJerarquico.Name = "btnEliminarIndiceJerarquico"
        Me.btnEliminarIndiceJerarquico.Size = New System.Drawing.Size(364, 36)
        Me.btnEliminarIndiceJerarquico.TabIndex = 83
        Me.btnEliminarIndiceJerarquico.Text = "Borrar relacion de jerarquia"
        Me.btnEliminarIndiceJerarquico.UseVisualStyleBackColor = False
        '
        'lblAsociadoCon
        '
        Me.lblAsociadoCon.AutoSize = True
        Me.lblAsociadoCon.BackColor = System.Drawing.Color.White
        Me.lblAsociadoCon.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAsociadoCon.FontSize = 9.75!
        Me.lblAsociadoCon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAsociadoCon.Location = New System.Drawing.Point(4, 16)
        Me.lblAsociadoCon.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAsociadoCon.Name = "lblAsociadoCon"
        Me.lblAsociadoCon.Size = New System.Drawing.Size(106, 16)
        Me.lblAsociadoCon.TabIndex = 84
        Me.lblAsociadoCon.Text = "lblAsociadoCon"
        Me.lblAsociadoCon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCSearchIndexHyerachical
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlAsociacionIndices)
        Me.Controls.Add(Me.Panel2)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCSearchIndexHyerachical"
        Me.Size = New System.Drawing.Size(376, 761)
        Me.Panel2.ResumeLayout(False)
        Me.pnlAsociacionIndices.ResumeLayout(False)
        CType(Me.dgHierarchicalValues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ZPanel.ResumeLayout(False)
        Me.ZPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnGenerarIndiceJerarquico As Zamba.AppBlock.ZButton
    Friend WithEvents pnlAsociacionIndices As System.Windows.Forms.Panel
    Friend WithEvents dgHierarchicalValues As System.Windows.Forms.DataGridView
    Friend WithEvents ZPanel As Zamba.AppBlock.ZPanel
    Friend WithEvents btEliminar As Zamba.AppBlock.ZButton
    Friend WithEvents BtnInsert As Zamba.AppBlock.ZButton
    Friend WithEvents btnEliminarIndiceJerarquico As Zamba.AppBlock.ZButton
    Friend WithEvents lblAsociadoCon As ZLabel

End Class
