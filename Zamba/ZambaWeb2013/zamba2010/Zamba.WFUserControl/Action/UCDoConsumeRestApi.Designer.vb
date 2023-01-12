<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoConsumeRestApi
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
        Me.txtUrl = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnAceptar = New Zamba.AppBlock.ZButton()
        Me.lblAyudaUrl = New Zamba.AppBlock.ZLabel()
        Me.lblCambios = New Zamba.AppBlock.ZLabel()
        Me.txtResult = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblAyudaResult = New Zamba.AppBlock.ZLabel()
        Me.btnTest = New Zamba.AppBlock.ZButton()
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
        Me.tbRule.Controls.Add(Me.btnTest)
        Me.tbRule.Controls.Add(Me.lblAyudaResult)
        Me.tbRule.Controls.Add(Me.txtResult)
        Me.tbRule.Controls.Add(Me.lblCambios)
        Me.tbRule.Controls.Add(Me.lblAyudaUrl)
        Me.tbRule.Controls.Add(Me.btnAceptar)
        Me.tbRule.Controls.Add(Me.txtUrl)
        Me.tbRule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(787, 513)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(795, 542)
        '
        'txtUrl
        '
        Me.txtUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUrl.Location = New System.Drawing.Point(53, 54)
        Me.txtUrl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtUrl.Multiline = False
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtUrl.Size = New System.Drawing.Size(432, 105)
        Me.txtUrl.TabIndex = 41
        Me.txtUrl.Text = ""
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(16, 271)
        Me.btnAceptar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(117, 33)
        Me.btnAceptar.TabIndex = 42
        Me.btnAceptar.Text = "Guardar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'lblAyudaUrl
        '
        Me.lblAyudaUrl.AutoSize = True
        Me.lblAyudaUrl.BackColor = System.Drawing.Color.Transparent
        Me.lblAyudaUrl.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAyudaUrl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAyudaUrl.Location = New System.Drawing.Point(49, 34)
        Me.lblAyudaUrl.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAyudaUrl.Name = "lblAyudaUrl"
        Me.lblAyudaUrl.Size = New System.Drawing.Size(237, 16)
        Me.lblAyudaUrl.TabIndex = 43
        Me.lblAyudaUrl.Text = "Ingrese la URL que desea procesar"
        Me.lblAyudaUrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCambios
        '
        Me.lblCambios.AutoSize = True
        Me.lblCambios.BackColor = System.Drawing.Color.Transparent
        Me.lblCambios.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCambios.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblCambios.Location = New System.Drawing.Point(49, 326)
        Me.lblCambios.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCambios.Name = "lblCambios"
        Me.lblCambios.Size = New System.Drawing.Size(0, 16)
        Me.lblCambios.TabIndex = 44
        Me.lblCambios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtResult
        '
        Me.txtResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResult.Location = New System.Drawing.Point(53, 198)
        Me.txtResult.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtResult.Multiline = False
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtResult.Size = New System.Drawing.Size(263, 30)
        Me.txtResult.TabIndex = 45
        Me.txtResult.Text = ""
        '
        'lblAyudaResult
        '
        Me.lblAyudaResult.AutoSize = True
        Me.lblAyudaResult.BackColor = System.Drawing.Color.Transparent
        Me.lblAyudaResult.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAyudaResult.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAyudaResult.Location = New System.Drawing.Point(49, 178)
        Me.lblAyudaResult.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAyudaResult.Name = "lblAyudaResult"
        Me.lblAyudaResult.Size = New System.Drawing.Size(219, 16)
        Me.lblAyudaResult.TabIndex = 46
        Me.lblAyudaResult.Text = "Ingrese el nombre de la variable"
        Me.lblAyudaResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnTest
        '
        Me.btnTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTest.ForeColor = System.Drawing.Color.White
        Me.btnTest.Location = New System.Drawing.Point(163, 271)
        Me.btnTest.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(117, 33)
        Me.btnTest.TabIndex = 47
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = False
        '
        'UCDoConsumeRestApi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoConsumeRestApi"
        Me.Size = New System.Drawing.Size(795, 542)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents lblAyudaUrl As Zamba.AppBlock.ZLabel
    Friend WithEvents btnAceptar As Zamba.AppBlock.ZButton
    Friend WithEvents txtUrl As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblCambios As Zamba.AppBlock.ZLabel
    Friend WithEvents txtResult As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblAyudaResult As Zamba.AppBlock.ZLabel
    Friend WithEvents btnTest As Zamba.AppBlock.ZButton
End Class
