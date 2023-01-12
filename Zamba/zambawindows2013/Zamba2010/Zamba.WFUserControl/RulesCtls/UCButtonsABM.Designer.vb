Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCButtonsABM
    Inherits ZControl

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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PanelTop = New Zamba.AppBlock.ZLabel
        Me.PanelFill = New Zamba.AppBlock.ZPanel
        Me.pnlFilters = New System.Windows.Forms.Panel
        Me.lblFilter = New ZLabel
        Me.cboFilters = New System.Windows.Forms.ComboBox
        Me.dgvButtons = New System.Windows.Forms.DataGridView
        Me.btnDelete = New ZButton
        Me.btnEdit = New ZButton
        Me.btnAdd = New ZButton
        Me.PanelFill.SuspendLayout()
        Me.pnlFilters.SuspendLayout()
        CType(Me.dgvButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.White
        Me.PanelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelTop.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(596, 32)
        Me.PanelTop.TabIndex = 99
        Me.PanelTop.Text = "  Reglas Generales"
        Me.PanelTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelFill
        '
        Me.PanelFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelFill.Controls.Add(Me.pnlFilters)
        Me.PanelFill.Controls.Add(Me.dgvButtons)
        Me.PanelFill.Controls.Add(Me.btnDelete)
        Me.PanelFill.Controls.Add(Me.btnEdit)
        Me.PanelFill.Controls.Add(Me.btnAdd)
        Me.PanelFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelFill.Location = New System.Drawing.Point(0, 32)
        Me.PanelFill.Name = "PanelFill"
        Me.PanelFill.Size = New System.Drawing.Size(596, 488)
        Me.PanelFill.TabIndex = 100
        '
        'pnlFilters
        '
        Me.pnlFilters.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFilters.AutoSize = True
        Me.pnlFilters.Controls.Add(Me.lblFilter)
        Me.pnlFilters.Controls.Add(Me.cboFilters)
        Me.pnlFilters.Location = New System.Drawing.Point(418, 5)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Size = New System.Drawing.Size(164, 31)
        Me.pnlFilters.TabIndex = 6
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Location = New System.Drawing.Point(3, 9)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(35, 13)
        Me.lblFilter.TabIndex = 4
        Me.lblFilter.Text = "Filtro:"
        '
        'cboFilters
        '
        Me.cboFilters.FormattingEnabled = True
        Me.cboFilters.Location = New System.Drawing.Point(40, 6)
        Me.cboFilters.Name = "cboFilters"
        Me.cboFilters.Size = New System.Drawing.Size(121, 21)
        Me.cboFilters.TabIndex = 3
        '
        'dgvButtons
        '
        Me.dgvButtons.AllowUserToAddRows = False
        Me.dgvButtons.AllowUserToDeleteRows = False
        Me.dgvButtons.AllowUserToResizeRows = False
        Me.dgvButtons.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvButtons.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvButtons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvButtons.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvButtons.Location = New System.Drawing.Point(-1, 42)
        Me.dgvButtons.MultiSelect = False
        Me.dgvButtons.Name = "dgvButtons"
        Me.dgvButtons.RowHeadersVisible = False
        Me.dgvButtons.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvButtons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvButtons.ShowCellErrors = False
        Me.dgvButtons.ShowEditingIcon = False
        Me.dgvButtons.ShowRowErrors = False
        Me.dgvButtons.Size = New System.Drawing.Size(596, 445)
        Me.dgvButtons.TabIndex = 5
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(177, 11)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "&Borrar"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(95, 11)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "&Editar"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(13, 12)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "&Agregar"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'UCButtonsABM
        '
        Me.Controls.Add(Me.PanelFill)
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "UCButtonsABM"
        Me.Size = New System.Drawing.Size(596, 520)
        Me.PanelFill.ResumeLayout(False)
        Me.PanelFill.PerformLayout()
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        CType(Me.dgvButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTop As Zamba.AppBlock.ZLabel
    Friend WithEvents PanelFill As ZPanel
    Friend WithEvents dgvButtons As System.Windows.Forms.DataGridView
    Friend WithEvents lblFilter As ZLabel
    Friend WithEvents cboFilters As System.Windows.Forms.ComboBox
    Friend WithEvents btnDelete As ZButton
    Friend WithEvents btnEdit As ZButton
    Friend WithEvents btnAdd As ZButton
    Friend WithEvents pnlFilters As System.Windows.Forms.Panel

End Class
