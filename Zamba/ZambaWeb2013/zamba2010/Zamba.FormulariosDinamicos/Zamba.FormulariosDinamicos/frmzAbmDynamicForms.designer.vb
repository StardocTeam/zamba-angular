<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbmDynamicForms
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
        Me.lblIndices = New System.Windows.Forms.Label
        Me.lblOrden = New System.Windows.Forms.Label
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.lblSeccion = New System.Windows.Forms.Label
        Me.cmbSection = New System.Windows.Forms.ComboBox
        Me.dgrvValuesList = New System.Windows.Forms.DataGridView
        Me.Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IndexId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SectionName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SectionId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NroFila = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lblDataGrid = New System.Windows.Forms.Label
        Me.btnAddToList = New System.Windows.Forms.Button
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnAccept = New System.Windows.Forms.Button
        Me.cmbIndex = New System.Windows.Forms.ComboBox
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAddSections = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnOrderDown = New System.Windows.Forms.Button
        Me.btnOrderUp = New System.Windows.Forms.Button
        CType(Me.dgrvValuesList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblIndices
        '
        Me.lblIndices.AutoSize = True
        Me.lblIndices.Location = New System.Drawing.Point(17, 18)
        Me.lblIndices.Name = "lblIndices"
        Me.lblIndices.Size = New System.Drawing.Size(41, 13)
        Me.lblIndices.TabIndex = 6
        Me.lblIndices.Text = "Indices"
        '
        'lblOrden
        '
        Me.lblOrden.AutoSize = True
        Me.lblOrden.Location = New System.Drawing.Point(270, 77)
        Me.lblOrden.Name = "lblOrden"
        Me.lblOrden.Size = New System.Drawing.Size(23, 13)
        Me.lblOrden.TabIndex = 8
        Me.lblOrden.Text = "Fila"
        '
        'cmbOrder
        '
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.FormattingEnabled = True
        Me.cmbOrder.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"})
        Me.cmbOrder.Location = New System.Drawing.Point(273, 93)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.Size = New System.Drawing.Size(159, 21)
        Me.cmbOrder.TabIndex = 2
        '
        'lblSeccion
        '
        Me.lblSeccion.AutoSize = True
        Me.lblSeccion.Location = New System.Drawing.Point(17, 77)
        Me.lblSeccion.Name = "lblSeccion"
        Me.lblSeccion.Size = New System.Drawing.Size(46, 13)
        Me.lblSeccion.TabIndex = 12
        Me.lblSeccion.Text = "Sección"
        '
        'cmbSection
        '
        Me.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSection.FormattingEnabled = True
        Me.cmbSection.Location = New System.Drawing.Point(20, 93)
        Me.cmbSection.Name = "cmbSection"
        Me.cmbSection.Size = New System.Drawing.Size(199, 21)
        Me.cmbSection.TabIndex = 1
        '
        'dgrvValuesList
        '
        Me.dgrvValuesList.AllowUserToAddRows = False
        Me.dgrvValuesList.AllowUserToDeleteRows = False
        Me.dgrvValuesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgrvValuesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrvValuesList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Index, Me.IndexId, Me.SectionName, Me.SectionId, Me.NroFila})
        Me.dgrvValuesList.Location = New System.Drawing.Point(17, 160)
        Me.dgrvValuesList.Name = "dgrvValuesList"
        Me.dgrvValuesList.ReadOnly = True
        Me.dgrvValuesList.RowHeadersVisible = False
        Me.dgrvValuesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgrvValuesList.Size = New System.Drawing.Size(365, 148)
        Me.dgrvValuesList.TabIndex = 4
        '
        'Index
        '
        Me.Index.HeaderText = "Indice"
        Me.Index.Name = "Index"
        Me.Index.ReadOnly = True
        '
        'IndexId
        '
        Me.IndexId.HeaderText = "Id Indice"
        Me.IndexId.Name = "IndexId"
        Me.IndexId.ReadOnly = True
        Me.IndexId.Visible = False
        '
        'SectionName
        '
        Me.SectionName.HeaderText = "Sección"
        Me.SectionName.Name = "SectionName"
        Me.SectionName.ReadOnly = True
        '
        'SectionId
        '
        Me.SectionId.HeaderText = "Id Sección"
        Me.SectionId.Name = "SectionId"
        Me.SectionId.ReadOnly = True
        Me.SectionId.Visible = False
        '
        'NroFila
        '
        Me.NroFila.FillWeight = 30.0!
        Me.NroFila.HeaderText = "Fila"
        Me.NroFila.Name = "NroFila"
        Me.NroFila.ReadOnly = True
        '
        'lblDataGrid
        '
        Me.lblDataGrid.AutoSize = True
        Me.lblDataGrid.Location = New System.Drawing.Point(17, 144)
        Me.lblDataGrid.Name = "lblDataGrid"
        Me.lblDataGrid.Size = New System.Drawing.Size(94, 13)
        Me.lblDataGrid.TabIndex = 16
        Me.lblDataGrid.Text = "Listado de Valores"
        '
        'btnAddToList
        '
        Me.btnAddToList.Location = New System.Drawing.Point(330, 131)
        Me.btnAddToList.Name = "btnAddToList"
        Me.btnAddToList.Size = New System.Drawing.Size(102, 23)
        Me.btnAddToList.TabIndex = 3
        Me.btnAddToList.Text = "Agregar a la Lista"
        Me.btnAddToList.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(17, 315)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 23)
        Me.btnRemove.TabIndex = 5
        Me.btnRemove.Text = "Remover"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnAccept
        '
        Me.btnAccept.Location = New System.Drawing.Point(347, 315)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(85, 24)
        Me.btnAccept.TabIndex = 7
        Me.btnAccept.Text = "Siguiente »"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'cmbIndex
        '
        Me.cmbIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIndex.FormattingEnabled = True
        Me.cmbIndex.Location = New System.Drawing.Point(20, 36)
        Me.cmbIndex.Name = "cmbIndex"
        Me.cmbIndex.Size = New System.Drawing.Size(412, 21)
        Me.cmbIndex.TabIndex = 0
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(256, 315)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(85, 24)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnAddSections
        '
        Me.btnAddSections.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddSections.Location = New System.Drawing.Point(225, 90)
        Me.btnAddSections.Name = "btnAddSections"
        Me.btnAddSections.Size = New System.Drawing.Size(25, 24)
        Me.btnAddSections.TabIndex = 18
        Me.btnAddSections.Text = "..."
        Me.btnAddSections.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnOrderDown)
        Me.Panel1.Controls.Add(Me.btnOrderUp)
        Me.Panel1.Location = New System.Drawing.Point(396, 160)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(44, 148)
        Me.Panel1.TabIndex = 19
        '
        'btnOrderDown
        '
        Me.btnOrderDown.Location = New System.Drawing.Point(4, 122)
        Me.btnOrderDown.Name = "btnOrderDown"
        Me.btnOrderDown.Size = New System.Drawing.Size(36, 23)
        Me.btnOrderDown.TabIndex = 1
        Me.btnOrderDown.Text = "\/"
        Me.btnOrderDown.UseVisualStyleBackColor = True
        '
        'btnOrderUp
        '
        Me.btnOrderUp.Location = New System.Drawing.Point(4, 3)
        Me.btnOrderUp.Name = "btnOrderUp"
        Me.btnOrderUp.Size = New System.Drawing.Size(36, 23)
        Me.btnOrderUp.TabIndex = 0
        Me.btnOrderUp.Text = "/\"
        Me.btnOrderUp.UseVisualStyleBackColor = True
        '
        'frmAbmDynamicForms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 355)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnAddSections)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.cmbIndex)
        Me.Controls.Add(Me.btnAccept)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAddToList)
        Me.Controls.Add(Me.lblDataGrid)
        Me.Controls.Add(Me.dgrvValuesList)
        Me.Controls.Add(Me.lblSeccion)
        Me.Controls.Add(Me.cmbSection)
        Me.Controls.Add(Me.lblOrden)
        Me.Controls.Add(Me.cmbOrder)
        Me.Controls.Add(Me.lblIndices)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbmDynamicForms"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.dgrvValuesList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblIndices As System.Windows.Forms.Label
    Friend WithEvents lblOrden As System.Windows.Forms.Label
    Friend WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents lblSeccion As System.Windows.Forms.Label
    Friend WithEvents cmbSection As System.Windows.Forms.ComboBox
    Friend WithEvents dgrvValuesList As System.Windows.Forms.DataGridView
    Friend WithEvents lblDataGrid As System.Windows.Forms.Label
    Friend WithEvents btnAddToList As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents cmbIndex As System.Windows.Forms.ComboBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAddSections As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnOrderDown As System.Windows.Forms.Button
    Friend WithEvents btnOrderUp As System.Windows.Forms.Button
    Friend WithEvents Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IndexId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SectionName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SectionId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NroFila As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
