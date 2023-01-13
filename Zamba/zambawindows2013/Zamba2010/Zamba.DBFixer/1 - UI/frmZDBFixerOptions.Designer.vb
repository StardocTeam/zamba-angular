<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZDBFixerOptions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmZDBFixerOptions))
        Me.tbcOptions = New System.Windows.Forms.TabControl
        Me.tbpGeneral = New System.Windows.Forms.TabPage
        Me.gbOptions = New System.Windows.Forms.GroupBox
        Me.chkProcessRelations = New System.Windows.Forms.CheckBox
        Me.chkProcessTables = New System.Windows.Forms.CheckBox
        Me.chkProcessStores = New System.Windows.Forms.CheckBox
        Me.chkProcessViews = New System.Windows.Forms.CheckBox
        Me.tbpDetalle = New System.Windows.Forms.TabPage
        Me.ucDetailsOptions = New DBFixer.UCDetailsOptions
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.chkPreQuery = New System.Windows.Forms.CheckBox
        Me.chkPostQuerys = New System.Windows.Forms.CheckBox
        Me.tbcOptions.SuspendLayout()
        Me.tbpGeneral.SuspendLayout()
        Me.gbOptions.SuspendLayout()
        Me.tbpDetalle.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbcOptions
        '
        Me.tbcOptions.Controls.Add(Me.tbpGeneral)
        Me.tbcOptions.Controls.Add(Me.tbpDetalle)
        Me.tbcOptions.Location = New System.Drawing.Point(12, 12)
        Me.tbcOptions.Name = "tbcOptions"
        Me.tbcOptions.SelectedIndex = 0
        Me.tbcOptions.Size = New System.Drawing.Size(403, 275)
        Me.tbcOptions.TabIndex = 0
        '
        'tbpGeneral
        '
        Me.tbpGeneral.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tbpGeneral.Controls.Add(Me.gbOptions)
        Me.tbpGeneral.Location = New System.Drawing.Point(4, 22)
        Me.tbpGeneral.Name = "tbpGeneral"
        Me.tbpGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpGeneral.Size = New System.Drawing.Size(395, 249)
        Me.tbpGeneral.TabIndex = 0
        Me.tbpGeneral.Text = "General"
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.chkPostQuerys)
        Me.gbOptions.Controls.Add(Me.chkPreQuery)
        Me.gbOptions.Controls.Add(Me.chkProcessRelations)
        Me.gbOptions.Controls.Add(Me.chkProcessTables)
        Me.gbOptions.Controls.Add(Me.chkProcessStores)
        Me.gbOptions.Controls.Add(Me.chkProcessViews)
        Me.gbOptions.Location = New System.Drawing.Point(91, 28)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(206, 215)
        Me.gbOptions.TabIndex = 3
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Opciones de Proceso"
        '
        'chkProcessRelations
        '
        Me.chkProcessRelations.AutoSize = True
        Me.chkProcessRelations.Location = New System.Drawing.Point(64, 81)
        Me.chkProcessRelations.Name = "chkProcessRelations"
        Me.chkProcessRelations.Size = New System.Drawing.Size(124, 17)
        Me.chkProcessRelations.TabIndex = 3
        Me.chkProcessRelations.Text = "Procesar Relaciones"
        Me.chkProcessRelations.UseVisualStyleBackColor = True
        '
        'chkProcessTables
        '
        Me.chkProcessTables.AutoSize = True
        Me.chkProcessTables.Location = New System.Drawing.Point(38, 58)
        Me.chkProcessTables.Name = "chkProcessTables"
        Me.chkProcessTables.Size = New System.Drawing.Size(103, 17)
        Me.chkProcessTables.TabIndex = 0
        Me.chkProcessTables.Text = "Procesar Tablas"
        Me.chkProcessTables.UseVisualStyleBackColor = True
        '
        'chkProcessStores
        '
        Me.chkProcessStores.AutoSize = True
        Me.chkProcessStores.Location = New System.Drawing.Point(38, 145)
        Me.chkProcessStores.Name = "chkProcessStores"
        Me.chkProcessStores.Size = New System.Drawing.Size(150, 17)
        Me.chkProcessStores.TabIndex = 2
        Me.chkProcessStores.Text = "Procesar StoreProcedures"
        Me.chkProcessStores.UseVisualStyleBackColor = True
        '
        'chkProcessViews
        '
        Me.chkProcessViews.AutoSize = True
        Me.chkProcessViews.Location = New System.Drawing.Point(38, 113)
        Me.chkProcessViews.Name = "chkProcessViews"
        Me.chkProcessViews.Size = New System.Drawing.Size(99, 17)
        Me.chkProcessViews.TabIndex = 1
        Me.chkProcessViews.Text = "Procesar Vistas"
        Me.chkProcessViews.UseVisualStyleBackColor = True
        '
        'tbpDetalle
        '
        Me.tbpDetalle.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tbpDetalle.Controls.Add(Me.ucDetailsOptions)
        Me.tbpDetalle.Location = New System.Drawing.Point(4, 22)
        Me.tbpDetalle.Name = "tbpDetalle"
        Me.tbpDetalle.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpDetalle.Size = New System.Drawing.Size(395, 249)
        Me.tbpDetalle.TabIndex = 1
        Me.tbpDetalle.Text = "Detalle"
        '
        'ucDetailsOptions
        '
        Me.ucDetailsOptions.BackColor = System.Drawing.Color.Transparent
        Me.ucDetailsOptions.Location = New System.Drawing.Point(6, 6)
        Me.ucDetailsOptions.Name = "ucDetailsOptions"
        Me.ucDetailsOptions.Size = New System.Drawing.Size(383, 232)
        Me.ucDetailsOptions.TabIndex = 0
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(238, 293)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.TabIndex = 4
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(333, 293)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 5
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'chkPreQuery
        '
        Me.chkPreQuery.AutoSize = True
        Me.chkPreQuery.Location = New System.Drawing.Point(38, 25)
        Me.chkPreQuery.Name = "chkPreQuery"
        Me.chkPreQuery.Size = New System.Drawing.Size(130, 17)
        Me.chkPreQuery.TabIndex = 4
        Me.chkPreQuery.Text = "Ejecutar PreConsultas"
        Me.chkPreQuery.UseVisualStyleBackColor = True
        '
        'chkPostQuerys
        '
        Me.chkPostQuerys.AutoSize = True
        Me.chkPostQuerys.Location = New System.Drawing.Point(38, 180)
        Me.chkPostQuerys.Name = "chkPostQuerys"
        Me.chkPostQuerys.Size = New System.Drawing.Size(135, 17)
        Me.chkPostQuerys.TabIndex = 5
        Me.chkPostQuerys.Text = "Ejecutar PostConsultas"
        Me.chkPostQuerys.UseVisualStyleBackColor = True
        '
        'frmZDBFixerOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(425, 326)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.tbcOptions)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmZDBFixerOptions"
        Me.Text = "Opciones"
        Me.tbcOptions.ResumeLayout(False)
        Me.tbpGeneral.ResumeLayout(False)
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.tbpDetalle.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbcOptions As System.Windows.Forms.TabControl
    Friend WithEvents tbpDetalle As System.Windows.Forms.TabPage
    Public WithEvents chkProcessStores As System.Windows.Forms.CheckBox
    Public WithEvents chkProcessViews As System.Windows.Forms.CheckBox
    Public WithEvents chkProcessTables As System.Windows.Forms.CheckBox
    Public WithEvents ucDetailsOptions As DBFixer.UCDetailsOptions
    Public WithEvents gbOptions As System.Windows.Forms.GroupBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Public WithEvents tbpGeneral As System.Windows.Forms.TabPage
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents chkProcessRelations As System.Windows.Forms.CheckBox
    Public WithEvents chkPostQuerys As System.Windows.Forms.CheckBox
    Public WithEvents chkPreQuery As System.Windows.Forms.CheckBox
End Class
