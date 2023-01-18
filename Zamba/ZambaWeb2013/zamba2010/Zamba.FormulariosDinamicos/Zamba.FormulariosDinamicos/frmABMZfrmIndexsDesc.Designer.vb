<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmABMZfrmIndexsDesc
    Inherits System.Windows.Forms.Form
    'Form overrides dispose to clean up the component list.
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cbIndexsValue = New System.Windows.Forms.ComboBox
        Me.cbTypes = New System.Windows.Forms.ComboBox
        Me.lblIndex = New System.Windows.Forms.Label
        Me.cbIndexs = New System.Windows.Forms.ComboBox
        Me.lblType = New System.Windows.Forms.Label
        Me.dgvData = New System.Windows.Forms.DataGridView
        Me.fId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.iId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.indexName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.idType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.idValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.value = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.eId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnBack = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.cbTypes)
        Me.GroupBox1.Controls.Add(Me.lblIndex)
        Me.GroupBox1.Controls.Add(Me.cbIndexs)
        Me.GroupBox1.Controls.Add(Me.lblType)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(418, 176)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbIndexsValue)
        Me.GroupBox2.Location = New System.Drawing.Point(23, 98)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(378, 59)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Valor"
        '
        'cbIndexsValue
        '
        Me.cbIndexsValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbIndexsValue.FormattingEnabled = True
        Me.cbIndexsValue.Location = New System.Drawing.Point(53, 19)
        Me.cbIndexsValue.Name = "cbIndexsValue"
        Me.cbIndexsValue.Size = New System.Drawing.Size(306, 21)
        Me.cbIndexsValue.TabIndex = 31
        '
        'cbTypes
        '
        Me.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTypes.FormattingEnabled = True
        Me.cbTypes.Location = New System.Drawing.Point(76, 59)
        Me.cbTypes.Name = "cbTypes"
        Me.cbTypes.Size = New System.Drawing.Size(306, 21)
        Me.cbTypes.TabIndex = 25
        '
        'lblIndex
        '
        Me.lblIndex.AutoSize = True
        Me.lblIndex.Location = New System.Drawing.Point(20, 29)
        Me.lblIndex.Name = "lblIndex"
        Me.lblIndex.Size = New System.Drawing.Size(36, 13)
        Me.lblIndex.TabIndex = 24
        Me.lblIndex.Text = "Índice"
        '
        'cbIndexs
        '
        Me.cbIndexs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbIndexs.FormattingEnabled = True
        Me.cbIndexs.Location = New System.Drawing.Point(76, 26)
        Me.cbIndexs.Name = "cbIndexs"
        Me.cbIndexs.Size = New System.Drawing.Size(306, 21)
        Me.cbIndexs.TabIndex = 23
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(20, 62)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(28, 13)
        Me.lblType.TabIndex = 14
        Me.lblType.Text = "Tipo"
        '
        'dgvData
        '
        Me.dgvData.AllowUserToAddRows = False
        Me.dgvData.AllowUserToDeleteRows = False
        Me.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fId, Me.iId, Me.indexName, Me.idType, Me.type, Me.idValue, Me.value, Me.eId})
        Me.dgvData.Location = New System.Drawing.Point(10, 227)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.RowHeadersVisible = False
        Me.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvData.Size = New System.Drawing.Size(444, 233)
        Me.dgvData.TabIndex = 13
        '
        'fId
        '
        Me.fId.HeaderText = "Id Formulario"
        Me.fId.Name = "fId"
        Me.fId.ReadOnly = True
        Me.fId.Visible = False
        '
        'iId
        '
        Me.iId.HeaderText = "Id Indice"
        Me.iId.Name = "iId"
        Me.iId.ReadOnly = True
        Me.iId.Visible = False
        '
        'indexName
        '
        Me.indexName.HeaderText = "Indice"
        Me.indexName.Name = "indexName"
        Me.indexName.ReadOnly = True
        '
        'idType
        '
        Me.idType.HeaderText = "id Tipo"
        Me.idType.Name = "idType"
        Me.idType.ReadOnly = True
        Me.idType.Visible = False
        '
        'type
        '
        Me.type.HeaderText = "Tipo"
        Me.type.Name = "type"
        Me.type.ReadOnly = True
        Me.type.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'idValue
        '
        Me.idValue.HeaderText = "Id Valor"
        Me.idValue.Name = "idValue"
        Me.idValue.ReadOnly = True
        Me.idValue.Visible = False
        '
        'value
        '
        Me.value.HeaderText = "Valor"
        Me.value.Name = "value"
        Me.value.ReadOnly = True
        Me.value.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'eId
        '
        Me.eId.HeaderText = "Id"
        Me.eId.Name = "eId"
        Me.eId.ReadOnly = True
        Me.eId.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.eId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.eId.Visible = False
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(315, 195)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(67, 24)
        Me.btnAdd.TabIndex = 14
        Me.btnAdd.Text = "Agregar"
        Me.btnAdd.UseMnemonic = False
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Enabled = False
        Me.btnRemove.Location = New System.Drawing.Point(392, 195)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(62, 24)
        Me.btnRemove.TabIndex = 17
        Me.btnRemove.Text = "Eliminar"
        Me.btnRemove.UseMnemonic = False
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(279, 467)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(85, 24)
        Me.btnBack.TabIndex = 20
        Me.btnBack.Text = "« Atras"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(190, 467)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(85, 24)
        Me.btnCancel.TabIndex = 21
        Me.btnCancel.Text = "Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(369, 467)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(85, 24)
        Me.btnNext.TabIndex = 22
        Me.btnNext.Text = "Siguiente »"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'frmABMZfrmIndexsDesc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(466, 501)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.dgvData)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmABMZfrmIndexsDesc"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents dgvData As System.Windows.Forms.DataGridView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblIndex As System.Windows.Forms.Label
    Friend WithEvents cbIndexs As System.Windows.Forms.ComboBox
    Friend WithEvents cbTypes As System.Windows.Forms.ComboBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents fId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents iId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents indexName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents idType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents idValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents eId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbIndexsValue As System.Windows.Forms.ComboBox
End Class