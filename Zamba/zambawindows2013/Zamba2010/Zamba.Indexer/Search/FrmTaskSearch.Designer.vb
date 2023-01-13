<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTaskSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTaskSearch))
        Me.rgvTasks = New Telerik.WinControls.UI.RadGridView()
        CType(Me.rgvTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rgvTasks
        '
        Me.rgvTasks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rgvTasks.Location = New System.Drawing.Point(0, 0)
        '
        'rgvTasks
        '
        Me.rgvTasks.MasterTemplate.AllowAddNewRow = False
        Me.rgvTasks.MasterTemplate.AllowDeleteRow = False
        Me.rgvTasks.MasterTemplate.AllowEditRow = False
        Me.rgvTasks.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.rgvTasks.MasterTemplate.EnableFiltering = True
        Me.rgvTasks.MasterTemplate.ShowRowHeaderColumn = False
        Me.rgvTasks.Name = "rgvTasks"
        Me.rgvTasks.ReadOnly = True
        Me.rgvTasks.ShowCellErrors = False
        Me.rgvTasks.ShowRowErrors = False
        Me.rgvTasks.Size = New System.Drawing.Size(792, 473)
        Me.rgvTasks.TabIndex = 0
        '
        'FrmTaskSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 473)
        Me.Controls.Add(Me.rgvTasks)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmTaskSearch"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Doble click sobre la tarea para visualizarla"
        CType(Me.rgvTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rgvTasks As Telerik.WinControls.UI.RadGridView
End Class
