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
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnGenerarIndiceJerarquico = New Zamba.AppBlock.ZButton
        Me.pnlAsociacionIndices = New System.Windows.Forms.Panel
        Me.btnEliminarIndiceJerarquico = New Zamba.AppBlock.ZButton
        Me.cmbDataTableName = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblAsociadoCon = New System.Windows.Forms.Label
        Me.cmbIndiceJerarquico = New System.Windows.Forms.ComboBox
        Me.btnAsociateIndex = New Zamba.AppBlock.ZButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnModify = New Zamba.AppBlock.ZButton
        Me.btEliminar = New Zamba.AppBlock.ZButton
        Me.BtnInsert = New Zamba.AppBlock.ZButton
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.dgHierarchicalValues = New System.Windows.Forms.DataGrid
        Me.Panel2.SuspendLayout()
        Me.pnlAsociacionIndices.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgHierarchicalValues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.btnGenerarIndiceJerarquico)
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(285, 91)
        Me.Panel2.TabIndex = 1
        '
        'btnGenerarIndiceJerarquico
        '
        Me.btnGenerarIndiceJerarquico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerarIndiceJerarquico.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.btnGenerarIndiceJerarquico.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnGenerarIndiceJerarquico.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerarIndiceJerarquico.Location = New System.Drawing.Point(0, 31)
        Me.btnGenerarIndiceJerarquico.Name = "btnGenerarIndiceJerarquico"
        Me.btnGenerarIndiceJerarquico.Size = New System.Drawing.Size(282, 27)
        Me.btnGenerarIndiceJerarquico.TabIndex = 75
        Me.btnGenerarIndiceJerarquico.Text = "Generar indice jerarquico"
        '
        'pnlAsociacionIndices
        '
        Me.pnlAsociacionIndices.Controls.Add(Me.Panel3)
        Me.pnlAsociacionIndices.Controls.Add(Me.Panel1)
        Me.pnlAsociacionIndices.Controls.Add(Me.btnEliminarIndiceJerarquico)
        Me.pnlAsociacionIndices.Controls.Add(Me.cmbDataTableName)
        Me.pnlAsociacionIndices.Controls.Add(Me.Label2)
        Me.pnlAsociacionIndices.Controls.Add(Me.lblAsociadoCon)
        Me.pnlAsociacionIndices.Controls.Add(Me.cmbIndiceJerarquico)
        Me.pnlAsociacionIndices.Controls.Add(Me.btnAsociateIndex)
        Me.pnlAsociacionIndices.Controls.Add(Me.Label1)
        Me.pnlAsociacionIndices.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAsociacionIndices.Location = New System.Drawing.Point(0, 0)
        Me.pnlAsociacionIndices.Name = "pnlAsociacionIndices"
        Me.pnlAsociacionIndices.Size = New System.Drawing.Size(282, 651)
        Me.pnlAsociacionIndices.TabIndex = 0
        '
        'btnEliminarIndiceJerarquico
        '
        Me.btnEliminarIndiceJerarquico.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEliminarIndiceJerarquico.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.btnEliminarIndiceJerarquico.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnEliminarIndiceJerarquico.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarIndiceJerarquico.Location = New System.Drawing.Point(3, 589)
        Me.btnEliminarIndiceJerarquico.Name = "btnEliminarIndiceJerarquico"
        Me.btnEliminarIndiceJerarquico.Size = New System.Drawing.Size(279, 29)
        Me.btnEliminarIndiceJerarquico.TabIndex = 74
        Me.btnEliminarIndiceJerarquico.Text = "Eliminar indice jerarquico"
        '
        'cmbDataTableName
        '
        Me.cmbDataTableName.FormattingEnabled = True
        Me.cmbDataTableName.Location = New System.Drawing.Point(13, 82)
        Me.cmbDataTableName.Name = "cmbDataTableName"
        Me.cmbDataTableName.Size = New System.Drawing.Size(258, 21)
        Me.cmbDataTableName.TabIndex = 77
        Me.cmbDataTableName.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(182, 13)
        Me.Label2.TabIndex = 76
        Me.Label2.Text = "Tipo de documento fuente de datos:"
        Me.Label2.Visible = False
        '
        'lblAsociadoCon
        '
        Me.lblAsociadoCon.AutoSize = True
        Me.lblAsociadoCon.Location = New System.Drawing.Point(10, 115)
        Me.lblAsociadoCon.Name = "lblAsociadoCon"
        Me.lblAsociadoCon.Size = New System.Drawing.Size(79, 13)
        Me.lblAsociadoCon.TabIndex = 75
        Me.lblAsociadoCon.Text = "lblAsociadoCon"
        '
        'cmbIndiceJerarquico
        '
        Me.cmbIndiceJerarquico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbIndiceJerarquico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIndiceJerarquico.FormattingEnabled = True
        Me.cmbIndiceJerarquico.Location = New System.Drawing.Point(13, 32)
        Me.cmbIndiceJerarquico.Name = "cmbIndiceJerarquico"
        Me.cmbIndiceJerarquico.Size = New System.Drawing.Size(258, 21)
        Me.cmbIndiceJerarquico.TabIndex = 73
        '
        'btnAsociateIndex
        '
        Me.btnAsociateIndex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAsociateIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.btnAsociateIndex.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnAsociateIndex.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAsociateIndex.Location = New System.Drawing.Point(3, 131)
        Me.btnAsociateIndex.Name = "btnAsociateIndex"
        Me.btnAsociateIndex.Size = New System.Drawing.Size(279, 29)
        Me.btnAsociateIndex.TabIndex = 4
        Me.btnAsociateIndex.Text = "Asociar"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(13, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 13)
        Me.Label1.TabIndex = 72
        Me.Label1.Text = "Seleccione el indice padre:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnModify)
        Me.Panel1.Controls.Add(Me.btEliminar)
        Me.Panel1.Controls.Add(Me.BtnInsert)
        Me.Panel1.Location = New System.Drawing.Point(4, 167)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(267, 26)
        Me.Panel1.TabIndex = 78
        '
        'btnModify
        '
        Me.btnModify.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnModify.Location = New System.Drawing.Point(185, 3)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(79, 18)
        Me.btnModify.TabIndex = 8
        Me.btnModify.Text = "Modificar"
        '
        'btEliminar
        '
        Me.btEliminar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btEliminar.Location = New System.Drawing.Point(97, 3)
        Me.btEliminar.Name = "btEliminar"
        Me.btEliminar.Size = New System.Drawing.Size(72, 18)
        Me.btEliminar.TabIndex = 7
        Me.btEliminar.Text = "Eliminar"
        '
        'BtnInsert
        '
        Me.BtnInsert.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnInsert.Location = New System.Drawing.Point(10, 3)
        Me.BtnInsert.Name = "BtnInsert"
        Me.BtnInsert.Size = New System.Drawing.Size(70, 18)
        Me.BtnInsert.TabIndex = 6
        Me.BtnInsert.Text = "Insertar"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.dgHierarchicalValues)
        Me.Panel3.Location = New System.Drawing.Point(13, 200)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(258, 383)
        Me.Panel3.TabIndex = 79
        '
        'dgHierarchicalValues
        '
        Me.dgHierarchicalValues.AlternatingBackColor = System.Drawing.Color.GhostWhite
        Me.dgHierarchicalValues.BackColor = System.Drawing.Color.GhostWhite
        Me.dgHierarchicalValues.BackgroundColor = System.Drawing.Color.Lavender
        Me.dgHierarchicalValues.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgHierarchicalValues.CaptionBackColor = System.Drawing.Color.RoyalBlue
        Me.dgHierarchicalValues.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.dgHierarchicalValues.CaptionForeColor = System.Drawing.Color.White
        Me.dgHierarchicalValues.DataMember = ""
        Me.dgHierarchicalValues.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgHierarchicalValues.FlatMode = True
        Me.dgHierarchicalValues.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.dgHierarchicalValues.ForeColor = System.Drawing.Color.MidnightBlue
        Me.dgHierarchicalValues.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.dgHierarchicalValues.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.dgHierarchicalValues.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.dgHierarchicalValues.HeaderForeColor = System.Drawing.Color.Lavender
        Me.dgHierarchicalValues.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.dgHierarchicalValues.LinkColor = System.Drawing.Color.Teal
        Me.dgHierarchicalValues.Location = New System.Drawing.Point(0, 0)
        Me.dgHierarchicalValues.Name = "dgHierarchicalValues"
        Me.dgHierarchicalValues.ParentRowsBackColor = System.Drawing.Color.Lavender
        Me.dgHierarchicalValues.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.dgHierarchicalValues.PreferredColumnWidth = 160
        Me.dgHierarchicalValues.ReadOnly = True
        Me.dgHierarchicalValues.SelectionBackColor = System.Drawing.Color.Teal
        Me.dgHierarchicalValues.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.dgHierarchicalValues.Size = New System.Drawing.Size(258, 383)
        Me.dgHierarchicalValues.TabIndex = 2
        '
        'UCSearchIndexHyerachical
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlAsociacionIndices)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "UCSearchIndexHyerachical"
        Me.Size = New System.Drawing.Size(282, 618)
        Me.Panel2.ResumeLayout(False)
        Me.pnlAsociacionIndices.ResumeLayout(False)
        Me.pnlAsociacionIndices.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.dgHierarchicalValues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlAsociacionIndices As System.Windows.Forms.Panel
    Friend WithEvents cmbIndiceJerarquico As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAsociateIndex As Zamba.AppBlock.ZButton
    Friend WithEvents btnGenerarIndiceJerarquico As Zamba.AppBlock.ZButton
    Friend WithEvents btnEliminarIndiceJerarquico As Zamba.AppBlock.ZButton
    Friend WithEvents lblAsociadoCon As System.Windows.Forms.Label
    Friend WithEvents cmbDataTableName As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnModify As Zamba.AppBlock.ZButton
    Friend WithEvents btEliminar As Zamba.AppBlock.ZButton
    Friend WithEvents BtnInsert As Zamba.AppBlock.ZButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents dgHierarchicalValues As System.Windows.Forms.DataGrid

End Class
