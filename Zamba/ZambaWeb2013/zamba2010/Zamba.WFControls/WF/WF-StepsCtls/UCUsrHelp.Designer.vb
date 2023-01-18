<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCUsrHelp
    Inherits Zamba.AppBlock.ZControl

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
        Me.PanelFill = New Zamba.AppBlock.ZPanel()
        Me.lblMessage = New ZLabel()
        Me.grpRuleHelp = New System.Windows.Forms.GroupBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.btnSaveHelp = New ZButton()
        Me.grpExecutionMode = New System.Windows.Forms.GroupBox()
        Me.rdoAllTasks = New System.Windows.Forms.RadioButton()
        Me.rdoTaskByTask = New System.Windows.Forms.RadioButton()
        Me.PanelFill.SuspendLayout()
        Me.grpRuleHelp.SuspendLayout()
        Me.grpExecutionMode.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelFill
        '
        Me.PanelFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelFill.Controls.Add(Me.lblMessage)
        Me.PanelFill.Controls.Add(Me.btnSaveHelp)
        Me.PanelFill.Controls.Add(Me.grpRuleHelp)
        Me.PanelFill.Controls.Add(Me.grpExecutionMode)
        Me.PanelFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelFill.Location = New System.Drawing.Point(0, 0)
        Me.PanelFill.Name = "PanelFill"
        Me.PanelFill.Size = New System.Drawing.Size(693, 513)
        Me.PanelFill.TabIndex = 103
        '
        'lblMessage
        '
        Me.lblMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(0, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(692, 38)
        Me.lblMessage.TabIndex = 5
        Me.lblMessage.Text = "MENSAJE_DE_AYUDA"
        '
        'grpRuleHelp
        '
        Me.grpRuleHelp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpRuleHelp.BackColor = System.Drawing.Color.Transparent
        Me.grpRuleHelp.Controls.Add(Me.txtDescription)
        Me.grpRuleHelp.Location = New System.Drawing.Point(17, 98)
        Me.grpRuleHelp.Name = "grpRuleHelp"
        Me.grpRuleHelp.Size = New System.Drawing.Size(656, 365)
        Me.grpRuleHelp.TabIndex = 4
        Me.grpRuleHelp.TabStop = False
        Me.grpRuleHelp.Text = "Mensaje de ayuda de la cadena de reglas (en caso de ser una accion de usuario, la" &
    " misma podra ser visualizada en el cliente)"
        '
        'txtDescription
        '
        Me.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDescription.Location = New System.Drawing.Point(3, 17)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(650, 345)
        Me.txtDescription.TabIndex = 2
        '
        'btnSaveHelp
        '
        Me.btnSaveHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveHelp.Location = New System.Drawing.Point(556, 469)
        Me.btnSaveHelp.Name = "btnSaveHelp"
        Me.btnSaveHelp.Size = New System.Drawing.Size(117, 27)
        Me.btnSaveHelp.TabIndex = 0
        Me.btnSaveHelp.Text = "&Guardar"
        Me.btnSaveHelp.UseVisualStyleBackColor = True
        '
        'grpExecutionMode
        '
        Me.grpExecutionMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpExecutionMode.BackColor = System.Drawing.Color.Transparent
        Me.grpExecutionMode.Controls.Add(Me.rdoAllTasks)
        Me.grpExecutionMode.Controls.Add(Me.rdoTaskByTask)
        Me.grpExecutionMode.Location = New System.Drawing.Point(17, 41)
        Me.grpExecutionMode.Name = "grpExecutionMode"
        Me.grpExecutionMode.Size = New System.Drawing.Size(656, 51)
        Me.grpExecutionMode.TabIndex = 3
        Me.grpExecutionMode.TabStop = False
        Me.grpExecutionMode.Text = "Modo de ejecución de las tareas"
        '
        'rdoAllTasks
        '
        Me.rdoAllTasks.AutoSize = True
        Me.rdoAllTasks.Location = New System.Drawing.Point(305, 20)
        Me.rdoAllTasks.Name = "rdoAllTasks"
        Me.rdoAllTasks.Size = New System.Drawing.Size(272, 17)
        Me.rdoAllTasks.TabIndex = 2
        Me.rdoAllTasks.TabStop = True
        Me.rdoAllTasks.Text = "Ejecutarlas en conjunto respecto al árbol de reglas."
        Me.rdoAllTasks.UseVisualStyleBackColor = True
        '
        'rdoTaskByTask
        '
        Me.rdoTaskByTask.AutoSize = True
        Me.rdoTaskByTask.Location = New System.Drawing.Point(10, 20)
        Me.rdoTaskByTask.Name = "rdoTaskByTask"
        Me.rdoTaskByTask.Size = New System.Drawing.Size(289, 17)
        Me.rdoTaskByTask.TabIndex = 1
        Me.rdoTaskByTask.TabStop = True
        Me.rdoTaskByTask.Text = "Ejecutarlas individualmente respecto al árbol de reglas."
        Me.rdoTaskByTask.UseVisualStyleBackColor = True
        '
        'UCUsrHelp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.Controls.Add(Me.PanelFill)
        Me.Name = "UCUsrHelp"
        Me.Size = New System.Drawing.Size(693, 513)
        Me.PanelFill.ResumeLayout(False)
        Me.grpRuleHelp.ResumeLayout(False)
        Me.grpRuleHelp.PerformLayout()
        Me.grpExecutionMode.ResumeLayout(False)
        Me.grpExecutionMode.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelFill As Zamba.AppBlock.ZPanel
    Friend WithEvents btnSaveHelp As ZButton
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents grpRuleHelp As System.Windows.Forms.GroupBox
    Friend WithEvents grpExecutionMode As System.Windows.Forms.GroupBox
    Friend WithEvents rdoAllTasks As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTaskByTask As System.Windows.Forms.RadioButton
    Friend WithEvents lblMessage As ZLabel

End Class
