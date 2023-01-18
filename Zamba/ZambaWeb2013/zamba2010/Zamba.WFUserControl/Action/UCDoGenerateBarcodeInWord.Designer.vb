<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoGenerateBarcodeInWord
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
        Me.cmbDocTypes = New System.Windows.Forms.ComboBox()
        Me.grdIndices = New System.Windows.Forms.DataGridView()
        Me.Index_Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ValorIndice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.txtInputPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.BtnSave = New Zamba.AppBlock.ZButton()
        Me.btnSearch = New Zamba.AppBlock.ZButton()
        Me.ChkContinueWithCurrentTasks = New System.Windows.Forms.CheckBox()
        Me.ChkAutoPrint = New System.Windows.Forms.CheckBox()
        Me.ChkInsertBarcode = New System.Windows.Forms.CheckBox()
        Me.txtTop = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label6 = New Zamba.AppBlock.ZLabel()
        Me.txtLeft = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label7 = New Zamba.AppBlock.ZLabel()
        Me.txtOutputPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.CHKSavePath = New System.Windows.Forms.CheckBox()
        Me.chkWithoutInsert = New System.Windows.Forms.CheckBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        CType(Me.grdIndices, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tbRule.Controls.Add(Me.chkWithoutInsert)
        Me.tbRule.Controls.Add(Me.txtOutputPath)
        Me.tbRule.Controls.Add(Me.CHKSavePath)
        Me.tbRule.Controls.Add(Me.ChkInsertBarcode)
        Me.tbRule.Controls.Add(Me.txtTop)
        Me.tbRule.Controls.Add(Me.Label6)
        Me.tbRule.Controls.Add(Me.txtLeft)
        Me.tbRule.Controls.Add(Me.Label7)
        Me.tbRule.Controls.Add(Me.ChkAutoPrint)
        Me.tbRule.Controls.Add(Me.ChkContinueWithCurrentTasks)
        Me.tbRule.Controls.Add(Me.btnSearch)
        Me.tbRule.Controls.Add(Me.BtnSave)
        Me.tbRule.Controls.Add(Me.txtInputPath)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.Panel1)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.grdIndices)
        Me.tbRule.Controls.Add(Me.cmbDocTypes)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(824, 781)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(832, 810)
        '
        'cmbDocTypes
        '
        Me.cmbDocTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDocTypes.FormattingEnabled = True
        Me.cmbDocTypes.Location = New System.Drawing.Point(180, 186)
        Me.cmbDocTypes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbDocTypes.Name = "cmbDocTypes"
        Me.cmbDocTypes.Size = New System.Drawing.Size(615, 24)
        Me.cmbDocTypes.TabIndex = 1
        '
        'grdIndices
        '
        Me.grdIndices.AllowUserToAddRows = False
        Me.grdIndices.AllowUserToDeleteRows = False
        Me.grdIndices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdIndices.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Index_Id, Me.Nombre, Me.ValorIndice})
        Me.grdIndices.Location = New System.Drawing.Point(40, 238)
        Me.grdIndices.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdIndices.Name = "grdIndices"
        Me.grdIndices.Size = New System.Drawing.Size(756, 251)
        Me.grdIndices.TabIndex = 12
        '
        'Index_Id
        '
        Me.Index_Id.DataPropertyName = "Index_Id"
        Me.Index_Id.HeaderText = "ID Indice"
        Me.Index_Id.Name = "Index_Id"
        '
        'Nombre
        '
        Me.Nombre.DataPropertyName = "Index_Name"
        Me.Nombre.HeaderText = "Nombre Indice"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.ReadOnly = True
        '
        'ValorIndice
        '
        Me.ValorIndice.DataPropertyName = "ValorIndice"
        Me.ValorIndice.HeaderText = "Valor Indice"
        Me.ValorIndice.Name = "ValorIndice"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(33, 190)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 16)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Entidad:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(40, 496)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(756, 208)
        Me.Panel1.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(36, 711)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 16)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Carátula:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInputPath
        '
        Me.txtInputPath.Location = New System.Drawing.Point(113, 711)
        Me.txtInputPath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtInputPath.Name = "txtInputPath"
        Me.txtInputPath.Size = New System.Drawing.Size(641, 25)
        Me.txtInputPath.TabIndex = 16
        Me.txtInputPath.Text = ""
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(341, 737)
        Me.BtnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(93, 33)
        Me.BtnSave.TabIndex = 33
        Me.BtnSave.Text = "Guardar"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(764, 711)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(32, 33)
        Me.btnSearch.TabIndex = 34
        Me.btnSearch.Text = "..."
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'ChkContinueWithCurrentTasks
        '
        Me.ChkContinueWithCurrentTasks.AutoSize = True
        Me.ChkContinueWithCurrentTasks.BackColor = System.Drawing.Color.Transparent
        Me.ChkContinueWithCurrentTasks.Location = New System.Drawing.Point(31, 7)
        Me.ChkContinueWithCurrentTasks.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkContinueWithCurrentTasks.Name = "ChkContinueWithCurrentTasks"
        Me.ChkContinueWithCurrentTasks.Size = New System.Drawing.Size(334, 20)
        Me.ChkContinueWithCurrentTasks.TabIndex = 39
        Me.ChkContinueWithCurrentTasks.Text = "Continuar la ejecución con las tareas actuales"
        Me.ChkContinueWithCurrentTasks.UseVisualStyleBackColor = False
        '
        'ChkAutoPrint
        '
        Me.ChkAutoPrint.AutoSize = True
        Me.ChkAutoPrint.BackColor = System.Drawing.Color.Transparent
        Me.ChkAutoPrint.Location = New System.Drawing.Point(31, 36)
        Me.ChkAutoPrint.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkAutoPrint.Name = "ChkAutoPrint"
        Me.ChkAutoPrint.Size = New System.Drawing.Size(273, 20)
        Me.ChkAutoPrint.TabIndex = 40
        Me.ChkAutoPrint.Text = "Imprimir automáticamente la carátula"
        Me.ChkAutoPrint.UseVisualStyleBackColor = False
        '
        'ChkInsertBarcode
        '
        Me.ChkInsertBarcode.AutoSize = True
        Me.ChkInsertBarcode.BackColor = System.Drawing.Color.Transparent
        Me.ChkInsertBarcode.Location = New System.Drawing.Point(31, 64)
        Me.ChkInsertBarcode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkInsertBarcode.Name = "ChkInsertBarcode"
        Me.ChkInsertBarcode.Size = New System.Drawing.Size(425, 20)
        Me.ChkInsertBarcode.TabIndex = 46
        Me.ChkInsertBarcode.Text = "Insertar código de barras y dejar pendiente de digitalización"
        Me.ChkInsertBarcode.UseVisualStyleBackColor = False
        '
        'txtTop
        '
        Me.txtTop.Location = New System.Drawing.Point(104, 95)
        Me.txtTop.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(145, 25)
        Me.txtTop.TabIndex = 43
        Me.txtTop.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(58, 95)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 16)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Top:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLeft
        '
        Me.txtLeft.Location = New System.Drawing.Point(329, 95)
        Me.txtLeft.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtLeft.Name = "txtLeft"
        Me.txtLeft.Size = New System.Drawing.Size(145, 25)
        Me.txtLeft.TabIndex = 45
        Me.txtLeft.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(264, 98)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 16)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "Left:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtOutputPath
        '
        Me.txtOutputPath.Location = New System.Drawing.Point(341, 153)
        Me.txtOutputPath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtOutputPath.Name = "txtOutputPath"
        Me.txtOutputPath.Size = New System.Drawing.Size(453, 25)
        Me.txtOutputPath.TabIndex = 52
        Me.txtOutputPath.Text = ""
        '
        'CHKSavePath
        '
        Me.CHKSavePath.Location = New System.Drawing.Point(31, 154)
        Me.CHKSavePath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CHKSavePath.Name = "CHKSavePath"
        Me.CHKSavePath.Size = New System.Drawing.Size(309, 25)
        Me.CHKSavePath.TabIndex = 51
        Me.CHKSavePath.Text = "Guardar ruta del documento en variable:"
        Me.CHKSavePath.UseVisualStyleBackColor = True
        '
        'chkWithoutInsert
        '
        Me.chkWithoutInsert.AutoSize = True
        Me.chkWithoutInsert.Location = New System.Drawing.Point(31, 126)
        Me.chkWithoutInsert.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkWithoutInsert.Name = "chkWithoutInsert"
        Me.chkWithoutInsert.Size = New System.Drawing.Size(408, 20)
        Me.chkWithoutInsert.TabIndex = 53
        Me.chkWithoutInsert.Text = "No insertar el documento en Zamba, solo generar el Word"
        Me.chkWithoutInsert.UseVisualStyleBackColor = True
        '
        'UCDoGenerateBarcodeInWord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoGenerateBarcodeInWord"
        Me.Size = New System.Drawing.Size(832, 810)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        CType(Me.grdIndices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbDocTypes As System.Windows.Forms.ComboBox
    Friend WithEvents grdIndices As System.Windows.Forms.DataGridView
    Friend WithEvents Index_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValorIndice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtVariable As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtInputPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnSearch As Zamba.AppBlock.ZButton
    Friend WithEvents BtnSave As Zamba.AppBlock.ZButton
    Friend WithEvents ChkAutoPrint As System.Windows.Forms.CheckBox
    Friend WithEvents ChkContinueWithCurrentTasks As System.Windows.Forms.CheckBox
    Friend WithEvents ChkInsertBarcode As System.Windows.Forms.CheckBox
    Friend WithEvents txtTop As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents txtLeft As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents txtOutputPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents CHKSavePath As System.Windows.Forms.CheckBox
    Friend WithEvents chkWithoutInsert As System.Windows.Forms.CheckBox

End Class
