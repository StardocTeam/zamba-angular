<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZDBFixerExplorer
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmZDBFixerExplorer))
        Me.tbpStoredProcedures = New System.Windows.Forms.TabPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.tvwStoredProcedures = New System.Windows.Forms.TreeView
        Me.lblEjecutandoConsulta = New System.Windows.Forms.Label
        Me.pgbGetStores = New System.Windows.Forms.ProgressBar
        Me.btnObtenerStores = New System.Windows.Forms.Button
        Me.tbpVistas = New System.Windows.Forms.TabPage
        Me.tvwViews = New System.Windows.Forms.TreeView
        Me.tbpTablas = New System.Windows.Forms.TabPage
        Me.tvwTables = New System.Windows.Forms.TreeView
        Me.tbcPrincipal = New System.Windows.Forms.TabControl
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.tbpStoredProcedures.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.tbpVistas.SuspendLayout()
        Me.tbpTablas.SuspendLayout()
        Me.tbcPrincipal.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbpStoredProcedures
        '
        Me.tbpStoredProcedures.Controls.Add(Me.SplitContainer1)
        Me.tbpStoredProcedures.Location = New System.Drawing.Point(4, 22)
        Me.tbpStoredProcedures.Name = "tbpStoredProcedures"
        Me.tbpStoredProcedures.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpStoredProcedures.Size = New System.Drawing.Size(532, 520)
        Me.tbpStoredProcedures.TabIndex = 2
        Me.tbpStoredProcedures.Text = "StoredProcedures"
        Me.tbpStoredProcedures.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.tvwStoredProcedures)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblEjecutandoConsulta)
        Me.SplitContainer1.Panel2.Controls.Add(Me.pgbGetStores)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnObtenerStores)
        Me.SplitContainer1.Size = New System.Drawing.Size(526, 514)
        Me.SplitContainer1.SplitterDistance = 461
        Me.SplitContainer1.TabIndex = 0
        '
        'tvwStoredProcedures
        '
        Me.tvwStoredProcedures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwStoredProcedures.Location = New System.Drawing.Point(0, 0)
        Me.tvwStoredProcedures.Name = "tvwStoredProcedures"
        Me.tvwStoredProcedures.Size = New System.Drawing.Size(526, 461)
        Me.tvwStoredProcedures.TabIndex = 1
        '
        'lblEjecutandoConsulta
        '
        Me.lblEjecutandoConsulta.AutoSize = True
        Me.lblEjecutandoConsulta.Location = New System.Drawing.Point(91, 16)
        Me.lblEjecutandoConsulta.Name = "lblEjecutandoConsulta"
        Me.lblEjecutandoConsulta.Size = New System.Drawing.Size(192, 13)
        Me.lblEjecutandoConsulta.TabIndex = 2
        Me.lblEjecutandoConsulta.Text = "Ejecutando Consulta. Por favor espere."
        Me.lblEjecutandoConsulta.Visible = False
        '
        'pgbGetStores
        '
        Me.pgbGetStores.Location = New System.Drawing.Point(24, 11)
        Me.pgbGetStores.Name = "pgbGetStores"
        Me.pgbGetStores.Size = New System.Drawing.Size(305, 23)
        Me.pgbGetStores.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pgbGetStores.TabIndex = 1
        Me.pgbGetStores.Visible = False
        '
        'btnObtenerStores
        '
        Me.btnObtenerStores.Location = New System.Drawing.Point(365, 5)
        Me.btnObtenerStores.Name = "btnObtenerStores"
        Me.btnObtenerStores.Size = New System.Drawing.Size(137, 40)
        Me.btnObtenerStores.TabIndex = 0
        Me.btnObtenerStores.Text = "Obtener Stores"
        Me.btnObtenerStores.UseVisualStyleBackColor = True
        '
        'tbpVistas
        '
        Me.tbpVistas.Controls.Add(Me.tvwViews)
        Me.tbpVistas.Location = New System.Drawing.Point(4, 22)
        Me.tbpVistas.Name = "tbpVistas"
        Me.tbpVistas.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpVistas.Size = New System.Drawing.Size(532, 520)
        Me.tbpVistas.TabIndex = 1
        Me.tbpVistas.Text = "Vistas"
        Me.tbpVistas.UseVisualStyleBackColor = True
        '
        'tvwViews
        '
        Me.tvwViews.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwViews.Location = New System.Drawing.Point(3, 3)
        Me.tvwViews.Name = "tvwViews"
        Me.tvwViews.Size = New System.Drawing.Size(526, 514)
        Me.tvwViews.TabIndex = 0
        '
        'tbpTablas
        '
        Me.tbpTablas.Controls.Add(Me.tvwTables)
        Me.tbpTablas.Location = New System.Drawing.Point(4, 22)
        Me.tbpTablas.Name = "tbpTablas"
        Me.tbpTablas.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpTablas.Size = New System.Drawing.Size(532, 520)
        Me.tbpTablas.TabIndex = 0
        Me.tbpTablas.Text = "Tablas"
        Me.tbpTablas.UseVisualStyleBackColor = True
        '
        'tvwTables
        '
        Me.tvwTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwTables.Location = New System.Drawing.Point(3, 3)
        Me.tvwTables.Name = "tvwTables"
        Me.tvwTables.Size = New System.Drawing.Size(526, 514)
        Me.tvwTables.TabIndex = 0
        '
        'tbcPrincipal
        '
        Me.tbcPrincipal.Controls.Add(Me.tbpTablas)
        Me.tbcPrincipal.Controls.Add(Me.tbpVistas)
        Me.tbcPrincipal.Controls.Add(Me.tbpStoredProcedures)
        Me.tbcPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.tbcPrincipal.Name = "tbcPrincipal"
        Me.tbcPrincipal.SelectedIndex = 0
        Me.tbcPrincipal.Size = New System.Drawing.Size(540, 546)
        Me.tbcPrincipal.TabIndex = 1
        '
        'frmZDBFixerExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 546)
        Me.Controls.Add(Me.tbcPrincipal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmZDBFixerExplorer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmZDBFixerExplorer"
        Me.tbpStoredProcedures.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.tbpVistas.ResumeLayout(False)
        Me.tbpTablas.ResumeLayout(False)
        Me.tbcPrincipal.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbpStoredProcedures As System.Windows.Forms.TabPage
    Friend WithEvents tbpVistas As System.Windows.Forms.TabPage
    Friend WithEvents tvwViews As System.Windows.Forms.TreeView
    Friend WithEvents tbpTablas As System.Windows.Forms.TabPage
    Friend WithEvents tvwTables As System.Windows.Forms.TreeView
    Friend WithEvents tbcPrincipal As System.Windows.Forms.TabControl
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents tvwStoredProcedures As System.Windows.Forms.TreeView
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnObtenerStores As System.Windows.Forms.Button
    Friend WithEvents pgbGetStores As System.Windows.Forms.ProgressBar
    Friend WithEvents lblEjecutandoConsulta As System.Windows.Forms.Label
End Class
