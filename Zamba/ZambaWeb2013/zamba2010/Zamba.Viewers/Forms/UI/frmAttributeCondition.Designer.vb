<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttributeCondition
    Inherits Zamba.AppBlock.ZForm

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
        Me.ZLabel3 = New Zamba.AppBlock.ZLabel()
        Me.ZLabel4 = New Zamba.AppBlock.ZLabel()
        Me.ZLabel5 = New Zamba.AppBlock.ZLabel()
        Me.ZLabel6 = New Zamba.AppBlock.ZLabel()
        Me.ZLabel7 = New Zamba.AppBlock.ZLabel()
        Me.ZLabel8 = New Zamba.AppBlock.ZLabel()
        Me.cmbIndexSource = New System.Windows.Forms.ComboBox()
        Me.cmbComparator = New System.Windows.Forms.ComboBox()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.cmbAction = New System.Windows.Forms.ComboBox()
        Me.gvDisplayConditions = New System.Windows.Forms.DataGridView()
        Me.cmbIndexTarget = New System.Windows.Forms.ComboBox()
        Me.Label1 = New ZLabel()
        Me.lblEntity = New ZLabel()
        Me.Label3 = New ZLabel()
        Me.lblForm = New ZLabel()
        Me.btnAddCondition = New ZButton()
        Me.btnEdit = New ZButton()
        Me.btnDelete = New ZButton()
        CType(Me.gvDisplayConditions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ZLabel3
        '
        Me.ZLabel3.AutoSize = True
        Me.ZLabel3.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel3.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ZLabel3.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.ZLabel3.Location = New System.Drawing.Point(5, 59)
        Me.ZLabel3.Name = "ZLabel3"
        Me.ZLabel3.Size = New System.Drawing.Size(90, 13)
        Me.ZLabel3.TabIndex = 2
        Me.ZLabel3.Text = "Atributo a validar"
        Me.ZLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZLabel4
        '
        Me.ZLabel4.AutoSize = True
        Me.ZLabel4.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel4.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ZLabel4.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.ZLabel4.Location = New System.Drawing.Point(240, 59)
        Me.ZLabel4.Name = "ZLabel4"
        Me.ZLabel4.Size = New System.Drawing.Size(66, 13)
        Me.ZLabel4.TabIndex = 3
        Me.ZLabel4.Text = "Comparador"
        Me.ZLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZLabel5
        '
        Me.ZLabel5.AutoSize = True
        Me.ZLabel5.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel5.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ZLabel5.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.ZLabel5.Location = New System.Drawing.Point(367, 59)
        Me.ZLabel5.Name = "ZLabel5"
        Me.ZLabel5.Size = New System.Drawing.Size(31, 13)
        Me.ZLabel5.TabIndex = 4
        Me.ZLabel5.Text = "Valor"
        Me.ZLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZLabel6
        '
        Me.ZLabel6.AutoSize = True
        Me.ZLabel6.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel6.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ZLabel6.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.ZLabel6.Location = New System.Drawing.Point(240, 100)
        Me.ZLabel6.Name = "ZLabel6"
        Me.ZLabel6.Size = New System.Drawing.Size(81, 13)
        Me.ZLabel6.TabIndex = 5
        Me.ZLabel6.Text = "Accion a aplicar"
        Me.ZLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZLabel7
        '
        Me.ZLabel7.AutoSize = True
        Me.ZLabel7.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel7.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ZLabel7.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.ZLabel7.Location = New System.Drawing.Point(5, 100)
        Me.ZLabel7.Name = "ZLabel7"
        Me.ZLabel7.Size = New System.Drawing.Size(89, 13)
        Me.ZLabel7.TabIndex = 6
        Me.ZLabel7.Text = "Atributo a aplicar"
        Me.ZLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZLabel8
        '
        Me.ZLabel8.AutoSize = True
        Me.ZLabel8.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel8.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ZLabel8.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.ZLabel8.Location = New System.Drawing.Point(5, 167)
        Me.ZLabel8.Name = "ZLabel8"
        Me.ZLabel8.Size = New System.Drawing.Size(68, 13)
        Me.ZLabel8.TabIndex = 7
        Me.ZLabel8.Text = "Condiciones:"
        Me.ZLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbIndexSource
        '
        Me.cmbIndexSource.FormattingEnabled = True
        Me.cmbIndexSource.Location = New System.Drawing.Point(8, 75)
        Me.cmbIndexSource.Name = "cmbIndexSource"
        Me.cmbIndexSource.Size = New System.Drawing.Size(223, 21)
        Me.cmbIndexSource.TabIndex = 8
        '
        'cmbComparator
        '
        Me.cmbComparator.FormattingEnabled = True
        Me.cmbComparator.Location = New System.Drawing.Point(243, 76)
        Me.cmbComparator.Name = "cmbComparator"
        Me.cmbComparator.Size = New System.Drawing.Size(121, 21)
        Me.cmbComparator.TabIndex = 9
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(370, 75)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(177, 21)
        Me.txtValue.TabIndex = 10
        '
        'cmbAction
        '
        Me.cmbAction.FormattingEnabled = True
        Me.cmbAction.Location = New System.Drawing.Point(243, 116)
        Me.cmbAction.Name = "cmbAction"
        Me.cmbAction.Size = New System.Drawing.Size(121, 21)
        Me.cmbAction.TabIndex = 11
        '
        'gvDisplayConditions
        '
        Me.gvDisplayConditions.AllowUserToAddRows = False
        Me.gvDisplayConditions.AllowUserToDeleteRows = False
        Me.gvDisplayConditions.AllowUserToOrderColumns = True
        Me.gvDisplayConditions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvDisplayConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gvDisplayConditions.Location = New System.Drawing.Point(8, 183)
        Me.gvDisplayConditions.MultiSelect = False
        Me.gvDisplayConditions.Name = "gvDisplayConditions"
        Me.gvDisplayConditions.ReadOnly = True
        Me.gvDisplayConditions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gvDisplayConditions.Size = New System.Drawing.Size(537, 430)
        Me.gvDisplayConditions.TabIndex = 13
        '
        'cmbIndexTarget
        '
        Me.cmbIndexTarget.FormattingEnabled = True
        Me.cmbIndexTarget.Location = New System.Drawing.Point(8, 116)
        Me.cmbIndexTarget.Name = "cmbIndexTarget"
        Me.cmbIndexTarget.Size = New System.Drawing.Size(223, 21)
        Me.cmbIndexTarget.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 18)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Entidad:"
        '
        'lblEntity
        '
        Me.lblEntity.AutoSize = True
        Me.lblEntity.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntity.Location = New System.Drawing.Point(106, 6)
        Me.lblEntity.Name = "lblEntity"
        Me.lblEntity.Size = New System.Drawing.Size(14, 18)
        Me.lblEntity.TabIndex = 18
        Me.lblEntity.Text = "-"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 18)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Formulario:"
        '
        'lblForm
        '
        Me.lblForm.AutoSize = True
        Me.lblForm.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblForm.Location = New System.Drawing.Point(106, 28)
        Me.lblForm.Name = "lblForm"
        Me.lblForm.Size = New System.Drawing.Size(14, 18)
        Me.lblForm.TabIndex = 20
        Me.lblForm.Text = "-"
        '
        'btnAddCondition
        '
        Me.btnAddCondition.Location = New System.Drawing.Point(8, 143)
        Me.btnAddCondition.Name = "btnAddCondition"
        Me.btnAddCondition.Size = New System.Drawing.Size(177, 21)
        Me.btnAddCondition.TabIndex = 21
        Me.btnAddCondition.Text = "Agregar condicion"
        Me.btnAddCondition.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(191, 143)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(177, 21)
        Me.btnEdit.TabIndex = 22
        Me.btnEdit.Text = "Modificar condicion"
        Me.btnEdit.UseVisualStyleBackColor = True
        Me.btnEdit.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(370, 143)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(177, 21)
        Me.btnDelete.TabIndex = 23
        Me.btnDelete.Text = "Borrar condicion"
        Me.btnDelete.UseVisualStyleBackColor = True
        Me.btnDelete.Visible = False
        '
        'frmAttributeCondition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(550, 618)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAddCondition)
        Me.Controls.Add(Me.lblForm)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblEntity)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbIndexTarget)
        Me.Controls.Add(Me.gvDisplayConditions)
        Me.Controls.Add(Me.cmbAction)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.cmbComparator)
        Me.Controls.Add(Me.cmbIndexSource)
        Me.Controls.Add(Me.ZLabel8)
        Me.Controls.Add(Me.ZLabel7)
        Me.Controls.Add(Me.ZLabel6)
        Me.Controls.Add(Me.ZLabel5)
        Me.Controls.Add(Me.ZLabel4)
        Me.Controls.Add(Me.ZLabel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmAttributeCondition"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Condiciones en atributos"
        CType(Me.gvDisplayConditions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ZLabel3 As Zamba.AppBlock.ZLabel
    Friend WithEvents ZLabel4 As Zamba.AppBlock.ZLabel
    Friend WithEvents ZLabel5 As Zamba.AppBlock.ZLabel
    Friend WithEvents ZLabel6 As Zamba.AppBlock.ZLabel
    Friend WithEvents ZLabel7 As Zamba.AppBlock.ZLabel
    Friend WithEvents ZLabel8 As Zamba.AppBlock.ZLabel
    Friend WithEvents cmbIndexSource As System.Windows.Forms.ComboBox
    Friend WithEvents cmbComparator As System.Windows.Forms.ComboBox
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents cmbAction As System.Windows.Forms.ComboBox
    Friend WithEvents gvDisplayConditions As System.Windows.Forms.DataGridView
    Friend WithEvents cmbIndexTarget As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents lblEntity As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents lblForm As ZLabel
    Friend WithEvents btnAddCondition As ZButton
    Friend WithEvents btnEdit As ZButton
    Friend WithEvents btnDelete As ZButton
End Class
