<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WFTemplates
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WFTemplates))
        Me.btEditar = New Zamba.AppBlock.ZButton()
        Me.btnDescripcion = New Zamba.AppBlock.ZButton()
        Me.btCambiarNombre = New Zamba.AppBlock.ZButton()
        Me.tabControlGeneral = New System.Windows.Forms.TabControl()
        Me.tabWorkflow = New System.Windows.Forms.TabPage()
        Me.tabEtapa = New System.Windows.Forms.TabPage()
        Me.tabRegla = New System.Windows.Forms.TabPage()
        Me.tabControlGeneral.SuspendLayout()
        Me.SuspendLayout()
        '
        'btEditar
        '
        Me.btEditar.Location = New System.Drawing.Point(12, 12)
        Me.btEditar.Name = "btEditar"
        Me.btEditar.Size = New System.Drawing.Size(188, 27)
        Me.btEditar.TabIndex = 17
        Me.btEditar.Text = "Generar"
        '
        'btnDescripcion
        '
        Me.btnDescripcion.Location = New System.Drawing.Point(206, 12)
        Me.btnDescripcion.Name = "btnDescripcion"
        Me.btnDescripcion.Size = New System.Drawing.Size(188, 27)
        Me.btnDescripcion.TabIndex = 15
        Me.btnDescripcion.Text = "Exportar XML"
        '
        'btCambiarNombre
        '
        Me.btCambiarNombre.Location = New System.Drawing.Point(107, 312)
        Me.btCambiarNombre.Name = "btCambiarNombre"
        Me.btCambiarNombre.Size = New System.Drawing.Size(188, 27)
        Me.btCambiarNombre.TabIndex = 13
        Me.btCambiarNombre.Text = "Eliminar"
        '
        'tabControlGeneral
        '
        Me.tabControlGeneral.Controls.Add(Me.tabWorkflow)
        Me.tabControlGeneral.Controls.Add(Me.tabEtapa)
        Me.tabControlGeneral.Controls.Add(Me.tabRegla)
        Me.tabControlGeneral.Location = New System.Drawing.Point(12, 58)
        Me.tabControlGeneral.Name = "tabControlGeneral"
        Me.tabControlGeneral.SelectedIndex = 0
        Me.tabControlGeneral.Size = New System.Drawing.Size(382, 248)
        Me.tabControlGeneral.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabControlGeneral.TabIndex = 20
        '
        'tabWorkflow
        '
        Me.tabWorkflow.Location = New System.Drawing.Point(4, 22)
        Me.tabWorkflow.Name = "tabWorkflow"
        Me.tabWorkflow.Padding = New System.Windows.Forms.Padding(3)
        Me.tabWorkflow.Size = New System.Drawing.Size(374, 222)
        Me.tabWorkflow.TabIndex = 0
        Me.tabWorkflow.Text = "Workflow"
        Me.tabWorkflow.UseVisualStyleBackColor = True
        '
        'tabEtapa
        '
        Me.tabEtapa.Location = New System.Drawing.Point(4, 22)
        Me.tabEtapa.Name = "tabEtapa"
        Me.tabEtapa.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEtapa.Size = New System.Drawing.Size(296, 222)
        Me.tabEtapa.TabIndex = 1
        Me.tabEtapa.Text = "Etapa"
        Me.tabEtapa.UseVisualStyleBackColor = True
        '
        'tabRegla
        '
        Me.tabRegla.Location = New System.Drawing.Point(4, 22)
        Me.tabRegla.Name = "tabRegla"
        Me.tabRegla.Size = New System.Drawing.Size(296, 222)
        Me.tabRegla.TabIndex = 3
        Me.tabRegla.Text = "Regla"
        Me.tabRegla.UseVisualStyleBackColor = True
        '
        'WFTemplates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 354)
        Me.Controls.Add(Me.tabControlGeneral)
        Me.Controls.Add(Me.btEditar)
        Me.Controls.Add(Me.btnDescripcion)
        Me.Controls.Add(Me.btCambiarNombre)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "WFTemplates"
        Me.Text = "Modelos de Workflow, Etapas, Procesos y Reglas"
        Me.tabControlGeneral.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btEditar As Zamba.AppBlock.ZButton
    Friend WithEvents btnDescripcion As Zamba.AppBlock.ZButton
    Friend WithEvents btCambiarNombre As Zamba.AppBlock.ZButton
    Friend WithEvents tabControlGeneral As System.Windows.Forms.TabControl
    Friend WithEvents tabWorkflow As System.Windows.Forms.TabPage
    Friend WithEvents tabEtapa As System.Windows.Forms.TabPage
    Friend WithEvents tabRegla As System.Windows.Forms.TabPage
End Class
