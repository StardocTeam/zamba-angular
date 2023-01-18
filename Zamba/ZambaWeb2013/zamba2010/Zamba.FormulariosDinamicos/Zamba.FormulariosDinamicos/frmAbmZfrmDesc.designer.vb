<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbmZfrmDesc
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
        Me.components = New System.ComponentModel.Container
        Me.lblIndexs = New System.Windows.Forms.Label
        Me.lblOperator = New System.Windows.Forms.Label
        Me.btnAddToList = New System.Windows.Forms.Button
        Me.btnFinish = New System.Windows.Forms.Button
        Me.btnRemove = New System.Windows.Forms.Button
        Me.grvList = New System.Windows.Forms.DataGridView
        Me.cboIndexs = New System.Windows.Forms.ComboBox
        Me.cboOpComparacion = New System.Windows.Forms.ComboBox
        Me.cboOpRelacion = New System.Windows.Forms.ComboBox
        Me.lblOpComparacion = New System.Windows.Forms.Label
        Me.lblOpRelacion = New System.Windows.Forms.Label
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnBack = New System.Windows.Forms.Button
        Me.panelValues = New System.Windows.Forms.Panel
        Me.Conexion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IndexName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IndexId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CompOperator = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grvList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblIndexs
        '
        Me.lblIndexs.AutoSize = True
        Me.lblIndexs.Location = New System.Drawing.Point(14, 48)
        Me.lblIndexs.Name = "lblIndexs"
        Me.lblIndexs.Size = New System.Drawing.Size(41, 13)
        Me.lblIndexs.TabIndex = 3
        Me.lblIndexs.Text = "Indices"
        '
        'lblOperator
        '
        Me.lblOperator.AutoSize = True
        Me.lblOperator.Location = New System.Drawing.Point(14, 147)
        Me.lblOperator.Name = "lblOperator"
        Me.lblOperator.Size = New System.Drawing.Size(111, 13)
        Me.lblOperator.TabIndex = 5
        Me.lblOperator.Text = "Valor de Comparación"
        '
        'btnAddToList
        '
        Me.btnAddToList.Location = New System.Drawing.Point(17, 213)
        Me.btnAddToList.Name = "btnAddToList"
        Me.btnAddToList.Size = New System.Drawing.Size(98, 23)
        Me.btnAddToList.TabIndex = 5
        Me.btnAddToList.Text = "Agregar a Lista"
        Me.btnAddToList.UseVisualStyleBackColor = True
        '
        'btnFinish
        '
        Me.btnFinish.Location = New System.Drawing.Point(393, 400)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(80, 24)
        Me.btnFinish.TabIndex = 9
        Me.btnFinish.Text = "Finalizar"
        Me.btnFinish.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(15, 400)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 23)
        Me.btnRemove.TabIndex = 7
        Me.btnRemove.Text = "Remover"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'grvList
        '
        Me.grvList.AllowUserToAddRows = False
        Me.grvList.AllowUserToDeleteRows = False
        Me.grvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Conexion, Me.IndexName, Me.IndexId, Me.CompOperator, Me.Value})
        Me.grvList.Location = New System.Drawing.Point(17, 244)
        Me.grvList.Name = "grvList"
        Me.grvList.ReadOnly = True
        Me.grvList.RowHeadersVisible = False
        Me.grvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grvList.Size = New System.Drawing.Size(456, 150)
        Me.grvList.TabIndex = 6
        '
        'cboIndexs
        '
        Me.cboIndexs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIndexs.FormattingEnabled = True
        Me.cboIndexs.Location = New System.Drawing.Point(17, 64)
        Me.cboIndexs.MaxDropDownItems = 10
        Me.cboIndexs.Name = "cboIndexs"
        Me.cboIndexs.Size = New System.Drawing.Size(456, 21)
        Me.cboIndexs.TabIndex = 0
        '
        'cboOpComparacion
        '
        Me.cboOpComparacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOpComparacion.FormattingEnabled = True
        Me.cboOpComparacion.Items.AddRange(New Object() {"=", "<>", ">", ">=", "<", "<=", "Contiene", "Empieza", "Termina"})
        Me.cboOpComparacion.Location = New System.Drawing.Point(17, 113)
        Me.cboOpComparacion.MaxDropDownItems = 9
        Me.cboOpComparacion.Name = "cboOpComparacion"
        Me.cboOpComparacion.Size = New System.Drawing.Size(186, 21)
        Me.cboOpComparacion.TabIndex = 1
        '
        'cboOpRelacion
        '
        Me.cboOpRelacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOpRelacion.FormattingEnabled = True
        Me.cboOpRelacion.Items.AddRange(New Object() {"Y", "O"})
        Me.cboOpRelacion.Location = New System.Drawing.Point(17, 25)
        Me.cboOpRelacion.MaxDropDownItems = 3
        Me.cboOpRelacion.Name = "cboOpRelacion"
        Me.cboOpRelacion.Size = New System.Drawing.Size(108, 21)
        Me.cboOpRelacion.TabIndex = 2
        '
        'lblOpComparacion
        '
        Me.lblOpComparacion.AutoSize = True
        Me.lblOpComparacion.Location = New System.Drawing.Point(14, 97)
        Me.lblOpComparacion.Name = "lblOpComparacion"
        Me.lblOpComparacion.Size = New System.Drawing.Size(141, 13)
        Me.lblOpComparacion.TabIndex = 28
        Me.lblOpComparacion.Text = "Operadores de comparación"
        '
        'lblOpRelacion
        '
        Me.lblOpRelacion.AutoSize = True
        Me.lblOpRelacion.Location = New System.Drawing.Point(14, 9)
        Me.lblOpRelacion.Name = "lblOpRelacion"
        Me.lblOpRelacion.Size = New System.Drawing.Size(117, 13)
        Me.lblOpRelacion.TabIndex = 29
        Me.lblOpRelacion.Text = "Operadores de relación"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(216, 400)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 24)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(302, 400)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(85, 24)
        Me.btnBack.TabIndex = 30
        Me.btnBack.Text = "« Atras"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'panelValues
        '
        Me.panelValues.BackColor = System.Drawing.Color.Transparent
        Me.panelValues.Location = New System.Drawing.Point(15, 167)
        Me.panelValues.Name = "panelValues"
        Me.panelValues.Size = New System.Drawing.Size(493, 39)
        Me.panelValues.TabIndex = 37
        '
        'Conexion
        '
        Me.Conexion.HeaderText = "Conector"
        Me.Conexion.Name = "Conexion"
        Me.Conexion.ReadOnly = True
        '
        'IndexName
        '
        Me.IndexName.HeaderText = "Indice"
        Me.IndexName.Name = "IndexName"
        Me.IndexName.ReadOnly = True
        '
        'IndexId
        '
        Me.IndexId.HeaderText = "Id Indice"
        Me.IndexId.Name = "IndexId"
        Me.IndexId.ReadOnly = True
        Me.IndexId.Visible = False
        '
        'CompOperator
        '
        Me.CompOperator.HeaderText = "Operador"
        Me.CompOperator.Name = "CompOperator"
        Me.CompOperator.ReadOnly = True
        '
        'Value
        '
        Me.Value.HeaderText = "Valor a Comparar"
        Me.Value.Name = "Value"
        Me.Value.ReadOnly = True
        '
        'frmAbmZfrmDesc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 441)
        Me.Controls.Add(Me.panelValues)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblOpRelacion)
        Me.Controls.Add(Me.lblOpComparacion)
        Me.Controls.Add(Me.cboOpRelacion)
        Me.Controls.Add(Me.cboOpComparacion)
        Me.Controls.Add(Me.cboIndexs)
        Me.Controls.Add(Me.grvList)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnFinish)
        Me.Controls.Add(Me.btnAddToList)
        Me.Controls.Add(Me.lblOperator)
        Me.Controls.Add(Me.lblIndexs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbmZfrmDesc"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.grvList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblIndexs As System.Windows.Forms.Label
    Friend WithEvents lblOperator As System.Windows.Forms.Label
    Friend WithEvents btnAddToList As System.Windows.Forms.Button
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnClean As System.Windows.Forms.Button
    Friend WithEvents grvList As System.Windows.Forms.DataGridView
    Friend WithEvents cboIndexs As System.Windows.Forms.ComboBox
    Friend WithEvents cboOpComparacion As System.Windows.Forms.ComboBox
    Friend WithEvents cboOpRelacion As System.Windows.Forms.ComboBox
    Friend WithEvents lblOpComparacion As System.Windows.Forms.Label
    Friend WithEvents lblOpRelacion As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents panelValues As System.Windows.Forms.Panel
    Friend WithEvents Conexion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IndexName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IndexId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CompOperator As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As System.Windows.Forms.DataGridViewTextBoxColumn
End Class