<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoAsk
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
        Me.lblSecondaryValue = New Zamba.AppBlock.ZLabel()
        Me.txtVariable = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnAceptar = New Zamba.AppBlock.ZButton()
        Me.txtMensaje = New System.Windows.Forms.TextBox()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.txtValorPorDefecto = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
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
        Me.tbRule.Controls.Add(Me.Label3)
        Me.tbRule.Controls.Add(Me.txtValorPorDefecto)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.txtMensaje)
        Me.tbRule.Controls.Add(Me.lblSecondaryValue)
        Me.tbRule.Controls.Add(Me.txtVariable)
        Me.tbRule.Controls.Add(Me.btnAceptar)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(604, 378)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(612, 407)
        '
        'lblSecondaryValue
        '
        Me.lblSecondaryValue.AutoSize = True
        Me.lblSecondaryValue.BackColor = System.Drawing.Color.Transparent
        Me.lblSecondaryValue.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSecondaryValue.FontSize = 9.75!
        Me.lblSecondaryValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSecondaryValue.Location = New System.Drawing.Point(24, 286)
        Me.lblSecondaryValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSecondaryValue.Name = "lblSecondaryValue"
        Me.lblSecondaryValue.Size = New System.Drawing.Size(142, 16)
        Me.lblSecondaryValue.TabIndex = 41
        Me.lblSecondaryValue.Text = "Guardar en Variable:"
        Me.lblSecondaryValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVariable
        '
        Me.txtVariable.Location = New System.Drawing.Point(209, 282)
        Me.txtVariable.Margin = New System.Windows.Forms.Padding(4)
        Me.txtVariable.Name = "txtVariable"
        Me.txtVariable.Size = New System.Drawing.Size(319, 25)
        Me.txtVariable.TabIndex = 39
        Me.txtVariable.Text = ""
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(475, 331)
        Me.btnAceptar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(104, 28)
        Me.btnAceptar.TabIndex = 40
        Me.btnAceptar.Text = "Guardar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'txtMensaje
        '
        Me.txtMensaje.Location = New System.Drawing.Point(48, 43)
        Me.txtMensaje.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMensaje.Multiline = True
        Me.txtMensaje.Name = "txtMensaje"
        Me.txtMensaje.Size = New System.Drawing.Size(480, 169)
        Me.txtMensaje.TabIndex = 42
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.FontSize = 9.75!
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(24, 23)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(206, 16)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Pregunta a realizar al usuario:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.FontSize = 9.75!
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(24, 239)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(129, 16)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "Valor por Defecto:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtValorPorDefecto
        '
        Me.txtValorPorDefecto.Location = New System.Drawing.Point(209, 235)
        Me.txtValorPorDefecto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtValorPorDefecto.Name = "txtValorPorDefecto"
        Me.txtValorPorDefecto.Size = New System.Drawing.Size(319, 25)
        Me.txtValorPorDefecto.TabIndex = 44
        Me.txtValorPorDefecto.Text = ""
        '
        'UCDoAsk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoAsk"
        Me.Size = New System.Drawing.Size(612, 407)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtMensaje As System.Windows.Forms.TextBox
    Friend WithEvents lblSecondaryValue As ZLabel
    Friend WithEvents txtVariable As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtValorPorDefecto As Zamba.AppBlock.TextoInteligenteTextBox
End Class
