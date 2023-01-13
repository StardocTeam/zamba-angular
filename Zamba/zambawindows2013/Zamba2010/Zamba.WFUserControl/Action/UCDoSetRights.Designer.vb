<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoSetRights
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
        Me.lblHelp = New ZLabel()
        Me.cmbRights = New Telerik.WinControls.UI.RadDropDownList()
        Me.dgvRights = New Telerik.WinControls.UI.RadGridView()
        Me.chkAction = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnSaveRule = New Telerik.WinControls.UI.RadButton()
        Me.btnAddRight = New Telerik.WinControls.UI.RadButton()
        Me.btnRemoveRight = New Telerik.WinControls.UI.RadButton()
        Me.lblSavedData = New Telerik.WinControls.UI.RadLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        CType(Me.cmbRights, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRights, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveRule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRemoveRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSavedData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.lblSavedData)
        Me.tbRule.Controls.Add(Me.btnRemoveRight)
        Me.tbRule.Controls.Add(Me.btnAddRight)
        Me.tbRule.Controls.Add(Me.btnSaveRule)
        Me.tbRule.Controls.Add(Me.chkAction)
        Me.tbRule.Controls.Add(Me.dgvRights)
        Me.tbRule.Controls.Add(Me.cmbRights)
        Me.tbRule.Controls.Add(Me.lblHelp)
        '
        'lblHelp
        '
        Me.lblHelp.AutoSize = True
        Me.lblHelp.BackColor = System.Drawing.Color.Transparent
        Me.lblHelp.Location = New System.Drawing.Point(12, 18)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(99, 13)
        Me.lblHelp.TabIndex = 35
        Me.lblHelp.Text = "Permiso a modificar"
        '
        'cmbRights
        '
        Me.cmbRights.DropDownAnimationEnabled = True
        Me.cmbRights.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbRights.Location = New System.Drawing.Point(15, 34)
        Me.cmbRights.Name = "cmbRights"
        Me.cmbRights.ShowImageInEditorArea = True
        Me.cmbRights.Size = New System.Drawing.Size(212, 20)
        Me.cmbRights.TabIndex = 0
        Me.cmbRights.Text = "RadDropDownList1"
        '
        'dgvRights
        '
        Me.dgvRights.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRights.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditProgrammatically
        Me.dgvRights.Location = New System.Drawing.Point(15, 61)
        '
        'dgvRights
        '
        Me.dgvRights.MasterTemplate.AllowAddNewRow = False
        Me.dgvRights.MasterTemplate.AllowCellContextMenu = False
        Me.dgvRights.MasterTemplate.AllowColumnChooser = False
        Me.dgvRights.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.dgvRights.MasterTemplate.AllowColumnReorder = False
        Me.dgvRights.MasterTemplate.AllowDragToGroup = False
        Me.dgvRights.MasterTemplate.AllowRowResize = False
        Me.dgvRights.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.dgvRights.MasterTemplate.ShowRowHeaderColumn = False
        Me.dgvRights.Name = "dgvRights"
        Me.dgvRights.ReadOnly = True
        Me.dgvRights.ShowCellErrors = False
        Me.dgvRights.ShowGroupPanel = False
        Me.dgvRights.ShowRowErrors = False
        Me.dgvRights.Size = New System.Drawing.Size(582, 519)
        Me.dgvRights.TabIndex = 4
        '
        'chkAction
        '
        Me.chkAction.Location = New System.Drawing.Point(233, 34)
        Me.chkAction.Name = "chkAction"
        Me.chkAction.Size = New System.Drawing.Size(79, 18)
        Me.chkAction.TabIndex = 1
        Me.chkAction.Text = "Deshabilitar"
        '
        'btnSaveRule
        '
        Me.btnSaveRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveRule.Location = New System.Drawing.Point(467, 586)
        Me.btnSaveRule.Name = "btnSaveRule"
        Me.btnSaveRule.Size = New System.Drawing.Size(130, 24)
        Me.btnSaveRule.TabIndex = 5
        Me.btnSaveRule.Text = "Guardar"
        '
        'btnAddRight
        '
        Me.btnAddRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddRight.Location = New System.Drawing.Point(403, 31)
        Me.btnAddRight.Name = "btnAddRight"
        Me.btnAddRight.Size = New System.Drawing.Size(94, 24)
        Me.btnAddRight.TabIndex = 2
        Me.btnAddRight.Text = "Agregar"
        '
        'btnRemoveRight
        '
        Me.btnRemoveRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveRight.Location = New System.Drawing.Point(503, 31)
        Me.btnRemoveRight.Name = "btnRemoveRight"
        Me.btnRemoveRight.Size = New System.Drawing.Size(94, 24)
        Me.btnRemoveRight.TabIndex = 3
        Me.btnRemoveRight.Text = "Quitar"
        '
        'lblSavedData
        '
        Me.lblSavedData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSavedData.Location = New System.Drawing.Point(15, 586)
        Me.lblSavedData.Name = "lblSavedData"
        Me.lblSavedData.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblSavedData.Size = New System.Drawing.Size(2, 2)
        Me.lblSavedData.TabIndex = 36
        Me.lblSavedData.TextAlignment = System.Drawing.ContentAlignment.TopRight
        '
        'UCDoSetRights
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoSetRights"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        CType(Me.cmbRights, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRights, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveRule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRemoveRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSavedData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblHelp As ZLabel
    Friend WithEvents btnSaveRule As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkAction As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents dgvRights As Telerik.WinControls.UI.RadGridView
    Friend WithEvents cmbRights As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents btnRemoveRight As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAddRight As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSavedData As Telerik.WinControls.UI.RadLabel

End Class
