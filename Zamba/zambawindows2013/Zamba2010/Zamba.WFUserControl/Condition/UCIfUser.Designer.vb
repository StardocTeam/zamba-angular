Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCIfUser
    Inherits ZRuleControl

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
        Me.lbUsuarios = New System.Windows.Forms.ListBox()
        Me.lbUsuario = New Zamba.AppBlock.ZLabel()
        Me.btAceptar = New Zamba.AppBlock.ZButton()
        Me.gbCondicion = New System.Windows.Forms.GroupBox()
        Me.rbNoUsuarioActual = New System.Windows.Forms.RadioButton()
        Me.rbUsuarioActual = New System.Windows.Forms.RadioButton()
        Me.rbNoAsignado = New System.Windows.Forms.RadioButton()
        Me.rbAsignado = New System.Windows.Forms.RadioButton()
        Me.lbNombreUser = New Zamba.AppBlock.ZLabel()
        Me.ZPanel1 = New Zamba.AppBlock.ZPanel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.gbCondicion.SuspendLayout()
        Me.ZPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.lbUsuarios)
        Me.tbRule.Controls.Add(Me.gbCondicion)
        Me.tbRule.Controls.Add(Me.ZPanel1)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(601, 493)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(609, 522)
        '
        'lbUsuarios
        '
        Me.lbUsuarios.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbUsuarios.FormattingEnabled = True
        Me.lbUsuarios.ItemHeight = 16
        Me.lbUsuarios.Location = New System.Drawing.Point(4, 47)
        Me.lbUsuarios.Margin = New System.Windows.Forms.Padding(4)
        Me.lbUsuarios.Name = "lbUsuarios"
        Me.lbUsuarios.Size = New System.Drawing.Size(382, 442)
        Me.lbUsuarios.TabIndex = 0
        '
        'lbUsuario
        '
        Me.lbUsuario.AutoSize = True
        Me.lbUsuario.BackColor = System.Drawing.Color.Transparent
        Me.lbUsuario.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUsuario.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lbUsuario.Location = New System.Drawing.Point(4, 13)
        Me.lbUsuario.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbUsuario.Name = "lbUsuario"
        Me.lbUsuario.Size = New System.Drawing.Size(62, 16)
        Me.lbUsuario.TabIndex = 1
        Me.lbUsuario.Text = "Usuario:"
        Me.lbUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btAceptar
        '
        Me.btAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btAceptar.ForeColor = System.Drawing.Color.White
        Me.btAceptar.Location = New System.Drawing.Point(46, 281)
        Me.btAceptar.Margin = New System.Windows.Forms.Padding(4)
        Me.btAceptar.Name = "btAceptar"
        Me.btAceptar.Size = New System.Drawing.Size(100, 28)
        Me.btAceptar.TabIndex = 2
        Me.btAceptar.Text = "Guardar"
        Me.btAceptar.UseVisualStyleBackColor = True
        '
        'gbCondicion
        '
        Me.gbCondicion.BackColor = System.Drawing.Color.Transparent
        Me.gbCondicion.Controls.Add(Me.rbNoUsuarioActual)
        Me.gbCondicion.Controls.Add(Me.rbUsuarioActual)
        Me.gbCondicion.Controls.Add(Me.btAceptar)
        Me.gbCondicion.Controls.Add(Me.rbNoAsignado)
        Me.gbCondicion.Controls.Add(Me.rbAsignado)
        Me.gbCondicion.Dock = System.Windows.Forms.DockStyle.Right
        Me.gbCondicion.Location = New System.Drawing.Point(386, 47)
        Me.gbCondicion.Margin = New System.Windows.Forms.Padding(4)
        Me.gbCondicion.Name = "gbCondicion"
        Me.gbCondicion.Padding = New System.Windows.Forms.Padding(4)
        Me.gbCondicion.Size = New System.Drawing.Size(211, 442)
        Me.gbCondicion.TabIndex = 3
        Me.gbCondicion.TabStop = False
        Me.gbCondicion.Text = "Condición"
        '
        'rbNoUsuarioActual
        '
        Me.rbNoUsuarioActual.AutoSize = True
        Me.rbNoUsuarioActual.Location = New System.Drawing.Point(21, 134)
        Me.rbNoUsuarioActual.Margin = New System.Windows.Forms.Padding(4)
        Me.rbNoUsuarioActual.Name = "rbNoUsuarioActual"
        Me.rbNoUsuarioActual.Size = New System.Drawing.Size(148, 20)
        Me.rbNoUsuarioActual.TabIndex = 3
        Me.rbNoUsuarioActual.TabStop = True
        Me.rbNoUsuarioActual.Text = "No Usuario Actual."
        Me.rbNoUsuarioActual.UseVisualStyleBackColor = True
        '
        'rbUsuarioActual
        '
        Me.rbUsuarioActual.AutoSize = True
        Me.rbUsuarioActual.Location = New System.Drawing.Point(21, 102)
        Me.rbUsuarioActual.Margin = New System.Windows.Forms.Padding(4)
        Me.rbUsuarioActual.Name = "rbUsuarioActual"
        Me.rbUsuarioActual.Size = New System.Drawing.Size(125, 20)
        Me.rbUsuarioActual.TabIndex = 2
        Me.rbUsuarioActual.TabStop = True
        Me.rbUsuarioActual.Text = "Usuario actual."
        Me.rbUsuarioActual.UseVisualStyleBackColor = True
        '
        'rbNoAsignado
        '
        Me.rbNoAsignado.AutoSize = True
        Me.rbNoAsignado.Location = New System.Drawing.Point(21, 70)
        Me.rbNoAsignado.Margin = New System.Windows.Forms.Padding(4)
        Me.rbNoAsignado.Name = "rbNoAsignado"
        Me.rbNoAsignado.Size = New System.Drawing.Size(177, 20)
        Me.rbNoAsignado.TabIndex = 1
        Me.rbNoAsignado.TabStop = True
        Me.rbNoAsignado.Text = "No asignado a Usuario."
        Me.rbNoAsignado.UseVisualStyleBackColor = True
        '
        'rbAsignado
        '
        Me.rbAsignado.AutoSize = True
        Me.rbAsignado.Location = New System.Drawing.Point(21, 38)
        Me.rbAsignado.Margin = New System.Windows.Forms.Padding(4)
        Me.rbAsignado.Name = "rbAsignado"
        Me.rbAsignado.Size = New System.Drawing.Size(156, 20)
        Me.rbAsignado.TabIndex = 0
        Me.rbAsignado.TabStop = True
        Me.rbAsignado.Text = "Asignado a Usuario."
        Me.rbAsignado.UseVisualStyleBackColor = True
        '
        'lbNombreUser
        '
        Me.lbNombreUser.AutoSize = True
        Me.lbNombreUser.BackColor = System.Drawing.Color.Transparent
        Me.lbNombreUser.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNombreUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lbNombreUser.Location = New System.Drawing.Point(87, 13)
        Me.lbNombreUser.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbNombreUser.Name = "lbNombreUser"
        Me.lbNombreUser.Size = New System.Drawing.Size(0, 16)
        Me.lbNombreUser.TabIndex = 4
        Me.lbNombreUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZPanel1
        '
        Me.ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel1.Controls.Add(Me.lbNombreUser)
        Me.ZPanel1.Controls.Add(Me.lbUsuario)
        Me.ZPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ZPanel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel1.Location = New System.Drawing.Point(4, 4)
        Me.ZPanel1.Name = "ZPanel1"
        Me.ZPanel1.Size = New System.Drawing.Size(593, 43)
        Me.ZPanel1.TabIndex = 5
        '
        'UCIfUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCIfUser"
        Me.Size = New System.Drawing.Size(609, 522)
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.gbCondicion.ResumeLayout(False)
        Me.gbCondicion.PerformLayout()
        Me.ZPanel1.ResumeLayout(False)
        Me.ZPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbUsuarios As System.Windows.Forms.ListBox
    Friend WithEvents lbUsuario As ZLabel
    Friend WithEvents btAceptar As ZButton
    Friend WithEvents gbCondicion As System.Windows.Forms.GroupBox
    Friend WithEvents rbNoUsuarioActual As System.Windows.Forms.RadioButton
    Friend WithEvents rbUsuarioActual As System.Windows.Forms.RadioButton
    Friend WithEvents rbNoAsignado As System.Windows.Forms.RadioButton
    Friend WithEvents rbAsignado As System.Windows.Forms.RadioButton
    Friend WithEvents lbNombreUser As ZLabel
    Friend WithEvents ZPanel1 As ZPanel
End Class
