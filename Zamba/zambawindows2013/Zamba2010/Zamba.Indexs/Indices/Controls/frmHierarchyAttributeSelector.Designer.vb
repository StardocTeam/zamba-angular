<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHierarchyAttributeSelector
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
        Me.cmbIndiceJerarquico = New System.Windows.Forms.ComboBox
        Me.btnAsociateIndex = New Zamba.AppBlock.ZButton
        Me.Label1 = New ZLabel
        Me.SuspendLayout()
        '
        'cmbIndiceJerarquico
        '
        Me.cmbIndiceJerarquico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbIndiceJerarquico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIndiceJerarquico.FormattingEnabled = True
        Me.cmbIndiceJerarquico.Location = New System.Drawing.Point(5, 27)
        Me.cmbIndiceJerarquico.Name = "cmbIndiceJerarquico"
        Me.cmbIndiceJerarquico.Size = New System.Drawing.Size(258, 21)
        Me.cmbIndiceJerarquico.TabIndex = 76
        '
        'btnAsociateIndex
        '
        Me.btnAsociateIndex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAsociateIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.btnAsociateIndex.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnAsociateIndex.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAsociateIndex.Location = New System.Drawing.Point(5, 54)
        Me.btnAsociateIndex.Name = "btnAsociateIndex"
        Me.btnAsociateIndex.Size = New System.Drawing.Size(279, 29)
        Me.btnAsociateIndex.TabIndex = 74
        Me.btnAsociateIndex.Text = "Asociar"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(5, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 13)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "Seleccione el atributo padre:"
        '
        'frmHierarchyAttributeSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 93)
        Me.Controls.Add(Me.cmbIndiceJerarquico)
        Me.Controls.Add(Me.btnAsociateIndex)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHierarchyAttributeSelector"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Seleccione atributo padre"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbIndiceJerarquico As System.Windows.Forms.ComboBox
    Friend WithEvents btnAsociateIndex As Zamba.AppBlock.ZButton
    Friend WithEvents Label1 As ZLabel
End Class
