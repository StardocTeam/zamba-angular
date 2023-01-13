<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UcForumAttachsGrid
    Inherits System.Windows.Forms.UserControl

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.dgvAttachs = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.dgvAttachs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvAttachs
        '
        Me.dgvAttachs.AllowUserToAddRows = False
        Me.dgvAttachs.AllowUserToDeleteRows = False
        Me.dgvAttachs.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.dgvAttachs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvAttachs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvAttachs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAttachs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvAttachs.Location = New System.Drawing.Point(0, 20)
        Me.dgvAttachs.MultiSelect = False
        Me.dgvAttachs.Name = "dgvAttachs"
        Me.dgvAttachs.ReadOnly = True
        Me.dgvAttachs.RowHeadersVisible = False
        Me.dgvAttachs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvAttachs.Size = New System.Drawing.Size(280, 130)
        Me.dgvAttachs.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(-3, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(284, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Para visualizar un adjunto haga doble click sobre el mismo."
        '
        'UcForumAttachsGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvAttachs)
        Me.Name = "UcForumAttachsGrid"
        Me.Size = New System.Drawing.Size(280, 150)
        CType(Me.dgvAttachs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvAttachs As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
