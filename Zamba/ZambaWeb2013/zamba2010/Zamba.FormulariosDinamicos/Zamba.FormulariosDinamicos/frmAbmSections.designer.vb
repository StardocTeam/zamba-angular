<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbmSections
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
        Me.dgrvSectionList = New System.Windows.Forms.DataGridView
        Me.txtSection = New System.Windows.Forms.TextBox
        Me.lblSection = New System.Windows.Forms.Label
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnAcept = New System.Windows.Forms.Button
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnEdit = New System.Windows.Forms.Button
        CType(Me.dgrvSectionList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrvSectionList
        '
        Me.dgrvSectionList.AllowUserToAddRows = False
        Me.dgrvSectionList.AllowUserToDeleteRows = False
        Me.dgrvSectionList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgrvSectionList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgrvSectionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrvSectionList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgrvSectionList.Location = New System.Drawing.Point(32, 107)
        Me.dgrvSectionList.MultiSelect = False
        Me.dgrvSectionList.Name = "dgrvSectionList"
        Me.dgrvSectionList.ReadOnly = True
        Me.dgrvSectionList.RowHeadersVisible = False
        Me.dgrvSectionList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrvSectionList.Size = New System.Drawing.Size(419, 181)
        Me.dgrvSectionList.TabIndex = 4
        Me.dgrvSectionList.VirtualMode = True
        '
        'txtSection
        '
        Me.txtSection.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSection.Location = New System.Drawing.Point(81, 38)
        Me.txtSection.Name = "txtSection"
        Me.txtSection.Size = New System.Drawing.Size(370, 20)
        Me.txtSection.TabIndex = 0
        '
        'lblSection
        '
        Me.lblSection.AutoSize = True
        Me.lblSection.Location = New System.Drawing.Point(29, 41)
        Me.lblSection.Name = "lblSection"
        Me.lblSection.Size = New System.Drawing.Size(46, 13)
        Me.lblSection.TabIndex = 2
        Me.lblSection.Text = "Sección"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(187, 77)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(84, 24)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "Agregar"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnAcept
        '
        Me.btnAcept.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAcept.Location = New System.Drawing.Point(277, 294)
        Me.btnAcept.Name = "btnAcept"
        Me.btnAcept.Size = New System.Drawing.Size(84, 24)
        Me.btnAcept.TabIndex = 6
        Me.btnAcept.Text = "Aceptar"
        Me.btnAcept.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(367, 77)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(84, 24)
        Me.btnRemove.TabIndex = 3
        Me.btnRemove.Text = "Remover"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(367, 294)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(84, 24)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(277, 77)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(84, 24)
        Me.btnEdit.TabIndex = 2
        Me.btnEdit.Text = "Modificar"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'frmAbmSections
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(481, 343)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAcept)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lblSection)
        Me.Controls.Add(Me.txtSection)
        Me.Controls.Add(Me.dgrvSectionList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbmSections"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ABM Secciones"
        CType(Me.dgrvSectionList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgrvSectionList As System.Windows.Forms.DataGridView
    Friend WithEvents txtSection As System.Windows.Forms.TextBox
    Friend WithEvents lblSection As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnAcept As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
End Class
