<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UcForumParticipants
    Inherits System.Windows.Forms.UserControl
    Implements IDisposable


    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Me.components IsNot Nothing Then Me.components.Dispose()
                If Me.dgvParticipants IsNot Nothing Then Me.dgvParticipants.Dispose()
                If Me.btnEdit IsNot Nothing Then Me.btnEdit.Dispose()
                If Me.dtParticipants IsNot Nothing Then Me.dtParticipants.Dispose()
            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.btnEdit = New ZButton
        Me.dgvParticipants = New System.Windows.Forms.DataGridView
        CType(Me.dgvParticipants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(4, 5)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(166, 23)
        Me.btnEdit.TabIndex = 0
        Me.btnEdit.Text = "Agregar o quitar participantes"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'dgvParticipants
        '
        Me.dgvParticipants.AllowUserToAddRows = False
        Me.dgvParticipants.AllowUserToDeleteRows = False
        Me.dgvParticipants.AllowUserToOrderColumns = True
        Me.dgvParticipants.AllowUserToResizeColumns = False
        Me.dgvParticipants.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.dgvParticipants.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvParticipants.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvParticipants.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvParticipants.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvParticipants.Location = New System.Drawing.Point(4, 34)
        Me.dgvParticipants.MultiSelect = False
        Me.dgvParticipants.Name = "dgvParticipants"
        Me.dgvParticipants.ReadOnly = True
        Me.dgvParticipants.RowHeadersVisible = False
        Me.dgvParticipants.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvParticipants.Size = New System.Drawing.Size(514, 54)
        Me.dgvParticipants.TabIndex = 2
        '
        'UcForumParticipants
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dgvParticipants)
        Me.Controls.Add(Me.btnEdit)
        Me.Name = "UcForumParticipants"
        Me.Size = New System.Drawing.Size(521, 91)
        CType(Me.dgvParticipants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnEdit As ZButton
    Friend WithEvents dgvParticipants As System.Windows.Forms.DataGridView

End Class
