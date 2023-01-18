<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoClearAttributeListCache
    Inherits Zamba.WFUserControl.ZRuleControl

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
        Me.btnAceptar = New Zamba.AppBlock.ZButton()
        Me.lblAyuda = New Zamba.AppBlock.ZLabel()
        Me.lblCambios = New Zamba.AppBlock.ZLabel()
        Me.cmbAttribute = New System.Windows.Forms.ComboBox()
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
        Me.tbRule.Controls.Add(Me.cmbAttribute)
        Me.tbRule.Controls.Add(Me.lblCambios)
        Me.tbRule.Controls.Add(Me.lblAyuda)
        Me.tbRule.Controls.Add(Me.btnAceptar)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(604, 378)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(612, 407)
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(350, 108)
        Me.btnAceptar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(117, 33)
        Me.btnAceptar.TabIndex = 42
        Me.btnAceptar.Text = "Guardar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'lblAyuda
        '
        Me.lblAyuda.AutoSize = True
        Me.lblAyuda.BackColor = System.Drawing.Color.Transparent
        Me.lblAyuda.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAyuda.Location = New System.Drawing.Point(28, 22)
        Me.lblAyuda.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAyuda.Name = "lblAyuda"
        Me.lblAyuda.Size = New System.Drawing.Size(293, 16)
        Me.lblAyuda.TabIndex = 43
        Me.lblAyuda.Text = "Seleccione atributo a limpiar cache de lista"
        Me.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCambios
        '
        Me.lblCambios.AutoSize = True
        Me.lblCambios.BackColor = System.Drawing.Color.Transparent
        Me.lblCambios.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCambios.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblCambios.Location = New System.Drawing.Point(28, 108)
        Me.lblCambios.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCambios.Name = "lblCambios"
        Me.lblCambios.Size = New System.Drawing.Size(0, 16)
        Me.lblCambios.TabIndex = 44
        Me.lblCambios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbAttribute
        '
        Me.cmbAttribute.FormattingEnabled = True
        Me.cmbAttribute.Location = New System.Drawing.Point(32, 58)
        Me.cmbAttribute.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbAttribute.Name = "cmbAttribute"
        Me.cmbAttribute.Size = New System.Drawing.Size(435, 24)
        Me.cmbAttribute.TabIndex = 45
        '
        'UCDoClearAttributeListCache
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoClearAttributeListCache"
        Me.Size = New System.Drawing.Size(612, 407)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents lblAyuda As Zamba.AppBlock.ZLabel
    Friend WithEvents btnAceptar As Zamba.AppBlock.ZButton
    Friend WithEvents lblCambios As Zamba.AppBlock.ZLabel
    Friend WithEvents cmbAttribute As System.Windows.Forms.ComboBox
End Class
