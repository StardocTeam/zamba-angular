<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig
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
        Me.lblFirstIndex = New System.Windows.Forms.Label
        Me.cmbIndex = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbDocType = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtExcelColumn = New System.Windows.Forms.TextBox
        Me.btnOk = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblFirstIndex
        '
        Me.lblFirstIndex.AutoSize = True
        Me.lblFirstIndex.Location = New System.Drawing.Point(12, 46)
        Me.lblFirstIndex.Name = "lblFirstIndex"
        Me.lblFirstIndex.Size = New System.Drawing.Size(130, 13)
        Me.lblFirstIndex.TabIndex = 13
        Me.lblFirstIndex.Text = "Índice clave de búsqueda"
        '
        'cmbIndex
        '
        Me.cmbIndex.FormattingEnabled = True
        Me.cmbIndex.Location = New System.Drawing.Point(159, 42)
        Me.cmbIndex.Name = "cmbIndex"
        Me.cmbIndex.Size = New System.Drawing.Size(254, 21)
        Me.cmbIndex.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Tipo de documento"
        '
        'cmbDocType
        '
        Me.cmbDocType.FormattingEnabled = True
        Me.cmbDocType.Location = New System.Drawing.Point(159, 15)
        Me.cmbDocType.Name = "cmbDocType"
        Me.cmbDocType.Size = New System.Drawing.Size(254, 21)
        Me.cmbDocType.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Columna excel de referencia"
        '
        'txtExcelColumn
        '
        Me.txtExcelColumn.Location = New System.Drawing.Point(159, 69)
        Me.txtExcelColumn.Name = "txtExcelColumn"
        Me.txtExcelColumn.Size = New System.Drawing.Size(254, 20)
        Me.txtExcelColumn.TabIndex = 21
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Location = New System.Drawing.Point(253, 124)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 22
        Me.btnOk.Text = "Aceptar"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(334, 124)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 23
        Me.btnCancel.Text = "Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(428, 159)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.txtExcelColumn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblFirstIndex)
        Me.Controls.Add(Me.cmbIndex)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbDocType)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuración"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblFirstIndex As System.Windows.Forms.Label
    Friend WithEvents cmbIndex As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbDocType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtExcelColumn As System.Windows.Forms.TextBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
