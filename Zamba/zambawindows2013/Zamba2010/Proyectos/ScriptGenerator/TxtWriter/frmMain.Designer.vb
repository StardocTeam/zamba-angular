<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.lstPaths = New System.Windows.Forms.ListBox
        Me.grpPaths = New System.Windows.Forms.GroupBox
        Me.lblNota = New System.Windows.Forms.Label
        Me.btnRemovePath = New System.Windows.Forms.Button
        Me.btnAddPath = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkRepSpaces = New System.Windows.Forms.CheckBox
        Me.dgvReplaces = New System.Windows.Forms.DataGridView
        Me.btnGenerate = New System.Windows.Forms.Button
        Me.grpScriptPath = New System.Windows.Forms.GroupBox
        Me.btnOpenFile = New System.Windows.Forms.Button
        Me.txtScript = New System.Windows.Forms.TextBox
        Me.grpSeparacion = New System.Windows.Forms.GroupBox
        Me.rdbNADA = New System.Windows.Forms.RadioButton
        Me.rdbORA = New System.Windows.Forms.RadioButton
        Me.rdbSQL = New System.Windows.Forms.RadioButton
        Me.grpPaths.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvReplaces, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpScriptPath.SuspendLayout()
        Me.grpSeparacion.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstPaths
        '
        Me.lstPaths.FormattingEnabled = True
        Me.lstPaths.HorizontalScrollbar = True
        Me.lstPaths.Location = New System.Drawing.Point(6, 19)
        Me.lstPaths.Name = "lstPaths"
        Me.lstPaths.ScrollAlwaysVisible = True
        Me.lstPaths.Size = New System.Drawing.Size(390, 108)
        Me.lstPaths.TabIndex = 0
        '
        'grpPaths
        '
        Me.grpPaths.Controls.Add(Me.lblNota)
        Me.grpPaths.Controls.Add(Me.btnRemovePath)
        Me.grpPaths.Controls.Add(Me.btnAddPath)
        Me.grpPaths.Controls.Add(Me.lstPaths)
        Me.grpPaths.Location = New System.Drawing.Point(12, 12)
        Me.grpPaths.Name = "grpPaths"
        Me.grpPaths.Size = New System.Drawing.Size(402, 178)
        Me.grpPaths.TabIndex = 1
        Me.grpPaths.TabStop = False
        Me.grpPaths.Text = "Directorios de archivos para agregar al script"
        '
        'lblNota
        '
        Me.lblNota.AutoSize = True
        Me.lblNota.Location = New System.Drawing.Point(6, 130)
        Me.lblNota.Name = "lblNota"
        Me.lblNota.Size = New System.Drawing.Size(199, 39)
        Me.lblNota.TabIndex = 3
        Me.lblNota.Text = "El orden en que el contenido de los " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "archivos se almacenará en el script será " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
            "tal cual sea ingresado en la lista."
        '
        'btnRemovePath
        '
        Me.btnRemovePath.Location = New System.Drawing.Point(240, 137)
        Me.btnRemovePath.Name = "btnRemovePath"
        Me.btnRemovePath.Size = New System.Drawing.Size(75, 23)
        Me.btnRemovePath.TabIndex = 2
        Me.btnRemovePath.Text = "Remover"
        Me.btnRemovePath.UseVisualStyleBackColor = True
        '
        'btnAddPath
        '
        Me.btnAddPath.Location = New System.Drawing.Point(321, 137)
        Me.btnAddPath.Name = "btnAddPath"
        Me.btnAddPath.Size = New System.Drawing.Size(75, 23)
        Me.btnAddPath.TabIndex = 1
        Me.btnAddPath.Text = "Agregar"
        Me.btnAddPath.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkRepSpaces)
        Me.GroupBox1.Controls.Add(Me.dgvReplaces)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 196)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(402, 164)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reemplazos de caracteres a realizar"
        '
        'chkRepSpaces
        '
        Me.chkRepSpaces.AutoSize = True
        Me.chkRepSpaces.Location = New System.Drawing.Point(6, 140)
        Me.chkRepSpaces.Name = "chkRepSpaces"
        Me.chkRepSpaces.Size = New System.Drawing.Size(195, 17)
        Me.chkRepSpaces.TabIndex = 3
        Me.chkRepSpaces.Text = "Reemplazar doble espacios por uno"
        Me.chkRepSpaces.UseVisualStyleBackColor = True
        '
        'dgvReplaces
        '
        Me.dgvReplaces.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvReplaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReplaces.Location = New System.Drawing.Point(6, 19)
        Me.dgvReplaces.MultiSelect = False
        Me.dgvReplaces.Name = "dgvReplaces"
        Me.dgvReplaces.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvReplaces.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvReplaces.Size = New System.Drawing.Size(390, 115)
        Me.dgvReplaces.TabIndex = 0
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(135, 477)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(156, 46)
        Me.btnGenerate.TabIndex = 3
        Me.btnGenerate.Text = "Generar script"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'grpScriptPath
        '
        Me.grpScriptPath.Controls.Add(Me.btnOpenFile)
        Me.grpScriptPath.Controls.Add(Me.txtScript)
        Me.grpScriptPath.Location = New System.Drawing.Point(12, 366)
        Me.grpScriptPath.Name = "grpScriptPath"
        Me.grpScriptPath.Size = New System.Drawing.Size(402, 52)
        Me.grpScriptPath.TabIndex = 4
        Me.grpScriptPath.TabStop = False
        Me.grpScriptPath.Text = "Script de salida"
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Location = New System.Drawing.Point(321, 17)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenFile.TabIndex = 5
        Me.btnOpenFile.Text = "Abrir..."
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'txtScript
        '
        Me.txtScript.Enabled = False
        Me.txtScript.Location = New System.Drawing.Point(6, 19)
        Me.txtScript.Name = "txtScript"
        Me.txtScript.Size = New System.Drawing.Size(309, 20)
        Me.txtScript.TabIndex = 0
        '
        'grpSeparacion
        '
        Me.grpSeparacion.Controls.Add(Me.rdbNADA)
        Me.grpSeparacion.Controls.Add(Me.rdbORA)
        Me.grpSeparacion.Controls.Add(Me.rdbSQL)
        Me.grpSeparacion.Location = New System.Drawing.Point(12, 424)
        Me.grpSeparacion.Name = "grpSeparacion"
        Me.grpSeparacion.Size = New System.Drawing.Size(402, 47)
        Me.grpSeparacion.TabIndex = 5
        Me.grpSeparacion.TabStop = False
        Me.grpSeparacion.Text = "Separación de sentencias"
        '
        'rdbNADA
        '
        Me.rdbNADA.AutoSize = True
        Me.rdbNADA.Location = New System.Drawing.Point(296, 20)
        Me.rdbNADA.Name = "rdbNADA"
        Me.rdbNADA.Size = New System.Drawing.Size(77, 17)
        Me.rdbNADA.TabIndex = 2
        Me.rdbNADA.TabStop = True
        Me.rdbNADA.Text = "No separar"
        Me.rdbNADA.UseVisualStyleBackColor = True
        '
        'rdbORA
        '
        Me.rdbORA.AutoSize = True
        Me.rdbORA.Location = New System.Drawing.Point(147, 20)
        Me.rdbORA.Name = "rdbORA"
        Me.rdbORA.Size = New System.Drawing.Size(143, 17)
        Me.rdbORA.TabIndex = 1
        Me.rdbORA.TabStop = True
        Me.rdbORA.Text = "Separar con / (ORACLE)"
        Me.rdbORA.UseVisualStyleBackColor = True
        '
        'rdbSQL
        '
        Me.rdbSQL.AutoSize = True
        Me.rdbSQL.Checked = True
        Me.rdbSQL.Location = New System.Drawing.Point(9, 20)
        Me.rdbSQL.Name = "rdbSQL"
        Me.rdbSQL.Size = New System.Drawing.Size(132, 17)
        Me.rdbSQL.TabIndex = 0
        Me.rdbSQL.TabStop = True
        Me.rdbSQL.Text = "Separar con GO (SQL)"
        Me.rdbSQL.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 534)
        Me.Controls.Add(Me.grpSeparacion)
        Me.Controls.Add(Me.grpScriptPath)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpPaths)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generador de scripts"
        Me.grpPaths.ResumeLayout(False)
        Me.grpPaths.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvReplaces, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpScriptPath.ResumeLayout(False)
        Me.grpScriptPath.PerformLayout()
        Me.grpSeparacion.ResumeLayout(False)
        Me.grpSeparacion.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstPaths As System.Windows.Forms.ListBox
    Friend WithEvents grpPaths As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemovePath As System.Windows.Forms.Button
    Friend WithEvents btnAddPath As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvReplaces As System.Windows.Forms.DataGridView
    Friend WithEvents lblNota As System.Windows.Forms.Label
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents grpScriptPath As System.Windows.Forms.GroupBox
    Friend WithEvents btnOpenFile As System.Windows.Forms.Button
    Friend WithEvents txtScript As System.Windows.Forms.TextBox
    Friend WithEvents grpSeparacion As System.Windows.Forms.GroupBox
    Friend WithEvents rdbNADA As System.Windows.Forms.RadioButton
    Friend WithEvents rdbORA As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSQL As System.Windows.Forms.RadioButton
    Friend WithEvents chkRepSpaces As System.Windows.Forms.CheckBox

End Class
