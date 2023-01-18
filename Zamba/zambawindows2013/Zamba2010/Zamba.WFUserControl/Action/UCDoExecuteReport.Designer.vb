Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCDoExecuteReport

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
    Private Overloads Sub InitializeComponent()
        Me.btGuardar = New Zamba.AppBlock.ZButton()
        Me.cmbReports = New System.Windows.Forms.ComboBox()
        Me.lbTitulo = New Zamba.AppBlock.ZLabel()
        Me.lstTemplates = New System.Windows.Forms.ListBox()
        Me.grpboxDocument = New System.Windows.Forms.GroupBox()
        Me.rdbword = New System.Windows.Forms.RadioButton()
        Me.rdbexcel = New System.Windows.Forms.RadioButton()
        Me.rdbpowerpoint = New System.Windows.Forms.RadioButton()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbState.Size = New System.Drawing.Size(824, 781)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbHabilitation.Size = New System.Drawing.Size(824, 781)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbConfiguration.Size = New System.Drawing.Size(824, 781)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Size = New System.Drawing.Size(824, 781)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.lbTitulo)
        Me.tbRule.Controls.Add(Me.cmbReports)
        Me.tbRule.Controls.Add(Me.btGuardar)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(624, 442)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(632, 471)
        '
        'btGuardar
        '
        Me.btGuardar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btGuardar.ForeColor = System.Drawing.Color.White
        Me.btGuardar.Location = New System.Drawing.Point(289, 105)
        Me.btGuardar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btGuardar.Name = "btGuardar"
        Me.btGuardar.Size = New System.Drawing.Size(100, 28)
        Me.btGuardar.TabIndex = 0
        Me.btGuardar.Text = "Guardar"
        Me.btGuardar.UseVisualStyleBackColor = True
        '
        'cmbReports
        '
        Me.cmbReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbReports.FormattingEnabled = True
        Me.cmbReports.Location = New System.Drawing.Point(15, 53)
        Me.cmbReports.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbReports.Name = "cmbReports"
        Me.cmbReports.Size = New System.Drawing.Size(373, 24)
        Me.cmbReports.TabIndex = 1
        '
        'lbTitulo
        '
        Me.lbTitulo.AutoSize = True
        Me.lbTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lbTitulo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lbTitulo.Location = New System.Drawing.Point(12, 21)
        Me.lbTitulo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbTitulo.Name = "lbTitulo"
        Me.lbTitulo.Size = New System.Drawing.Size(148, 16)
        Me.lbTitulo.TabIndex = 2
        Me.lbTitulo.Text = "Seleccione el reporte"
        Me.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstTemplates
        '
        Me.lstTemplates.Location = New System.Drawing.Point(0, 0)
        Me.lstTemplates.Name = "lstTemplates"
        Me.lstTemplates.Size = New System.Drawing.Size(120, 96)
        Me.lstTemplates.TabIndex = 0
        '
        'grpboxDocument
        '
        Me.grpboxDocument.Location = New System.Drawing.Point(0, 0)
        Me.grpboxDocument.Name = "grpboxDocument"
        Me.grpboxDocument.Size = New System.Drawing.Size(200, 100)
        Me.grpboxDocument.TabIndex = 0
        Me.grpboxDocument.TabStop = False
        '
        'rdbword
        '
        Me.rdbword.Location = New System.Drawing.Point(0, 0)
        Me.rdbword.Name = "rdbword"
        Me.rdbword.Size = New System.Drawing.Size(104, 24)
        Me.rdbword.TabIndex = 0
        '
        'rdbexcel
        '
        Me.rdbexcel.Location = New System.Drawing.Point(0, 0)
        Me.rdbexcel.Name = "rdbexcel"
        Me.rdbexcel.Size = New System.Drawing.Size(104, 24)
        Me.rdbexcel.TabIndex = 0
        '
        'rdbpowerpoint
        '
        Me.rdbpowerpoint.Location = New System.Drawing.Point(0, 0)
        Me.rdbpowerpoint.Name = "rdbpowerpoint"
        Me.rdbpowerpoint.Size = New System.Drawing.Size(104, 24)
        Me.rdbpowerpoint.TabIndex = 0
        '
        'UCDoExecuteReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoExecuteReport"
        Me.Size = New System.Drawing.Size(632, 471)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstTemplates As ListBox
    Friend WithEvents grpboxDocument As GroupBox
    Friend WithEvents rdbword As RadioButton
    Friend WithEvents rdbexcel As RadioButton
    Friend WithEvents rdbpowerpoint As RadioButton

    Friend WithEvents btGuardar As ZButton
    Friend WithEvents cmbReports As System.Windows.Forms.ComboBox
    Friend WithEvents lbTitulo As ZLabel

End Class
