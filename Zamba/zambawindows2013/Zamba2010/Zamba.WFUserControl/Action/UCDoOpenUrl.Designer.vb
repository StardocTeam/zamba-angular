<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoOpenUrl
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
        Me.lblAyuda = New Zamba.AppBlock.ZLabel()
        Me.lblCambios = New Zamba.AppBlock.ZLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Rbt_Home = New System.Windows.Forms.RadioButton()
        Me.Rbt_Modal = New System.Windows.Forms.RadioButton()
        Me.Rbt_NewTab = New System.Windows.Forms.RadioButton()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4)
        Me.tbState.Size = New System.Drawing.Size(604, 378)
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
        Me.tbRule.Controls.Add(Me.GroupBox1)
        Me.tbRule.Controls.Add(Me.lblCambios)
        Me.tbRule.Controls.Add(Me.lblAyuda)
        Me.tbRule.Controls.Add(Me.btnAceptar)
        Me.tbRule.Controls.Add(Me.txtUrl)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(604, 378)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(612, 407)
        '
        'txtUrl
        '
        Me.txtUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUrl.Location = New System.Drawing.Point(32, 54)
        Me.txtUrl.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUrl.Multiline = False
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtUrl.Size = New System.Drawing.Size(513, 29)
        Me.txtUrl.TabIndex = 41
        Me.txtUrl.Text = ""
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(232, 238)
        Me.btnAceptar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(117, 34)
        Me.btnAceptar.TabIndex = 42
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'lblAyuda
        '
        Me.lblAyuda.AutoSize = True
        Me.lblAyuda.BackColor = System.Drawing.Color.Transparent
        Me.lblAyuda.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblAyuda.FontSize = 9.75!
        Me.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblAyuda.Location = New System.Drawing.Point(28, 22)
        Me.lblAyuda.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAyuda.Name = "lblAyuda"
        Me.lblAyuda.Size = New System.Drawing.Size(317, 16)
        Me.lblAyuda.TabIndex = 43
        Me.lblAyuda.Text = "Ingrese la URL del documento que desea abrir:"
        Me.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCambios
        '
        Me.lblCambios.AutoSize = True
        Me.lblCambios.BackColor = System.Drawing.Color.Transparent
        Me.lblCambios.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblCambios.FontSize = 9.75!
        Me.lblCambios.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblCambios.Location = New System.Drawing.Point(28, 256)
        Me.lblCambios.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCambios.Name = "lblCambios"
        Me.lblCambios.Size = New System.Drawing.Size(0, 16)
        Me.lblCambios.TabIndex = 44
        Me.lblCambios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Rbt_Home)
        Me.GroupBox1.Controls.Add(Me.Rbt_Modal)
        Me.GroupBox1.Controls.Add(Me.Rbt_NewTab)
        Me.GroupBox1.Location = New System.Drawing.Point(32, 91)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(317, 116)
        Me.GroupBox1.TabIndex = 45
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Modo de apertura"
        '
        'Rbt_Home
        '
        Me.Rbt_Home.AutoSize = True
        Me.Rbt_Home.Location = New System.Drawing.Point(8, 81)
        Me.Rbt_Home.Margin = New System.Windows.Forms.Padding(4)
        Me.Rbt_Home.Name = "Rbt_Home"
        Me.Rbt_Home.Size = New System.Drawing.Size(138, 20)
        Me.Rbt_Home.TabIndex = 2
        Me.Rbt_Home.Text = "Mostrar en Home"
        Me.Rbt_Home.UseVisualStyleBackColor = True
        '
        'Rbt_Modal
        '
        Me.Rbt_Modal.AutoSize = True
        Me.Rbt_Modal.Location = New System.Drawing.Point(8, 53)
        Me.Rbt_Modal.Margin = New System.Windows.Forms.Padding(4)
        Me.Rbt_Modal.Name = "Rbt_Modal"
        Me.Rbt_Modal.Size = New System.Drawing.Size(180, 20)
        Me.Rbt_Modal.TabIndex = 1
        Me.Rbt_Modal.Text = "Modal (Misma ventana)"
        Me.Rbt_Modal.UseVisualStyleBackColor = True
        '
        'Rbt_NewTab
        '
        Me.Rbt_NewTab.AutoSize = True
        Me.Rbt_NewTab.Checked = True
        Me.Rbt_NewTab.Cursor = System.Windows.Forms.Cursors.Default
        Me.Rbt_NewTab.Location = New System.Drawing.Point(8, 25)
        Me.Rbt_NewTab.Margin = New System.Windows.Forms.Padding(4)
        Me.Rbt_NewTab.Name = "Rbt_NewTab"
        Me.Rbt_NewTab.Size = New System.Drawing.Size(126, 20)
        Me.Rbt_NewTab.TabIndex = 0
        Me.Rbt_NewTab.TabStop = True
        Me.Rbt_NewTab.Text = "Nueva ventana"
        Me.Rbt_NewTab.UseVisualStyleBackColor = True
        '
        'UCDoOpenUrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoOpenUrl"
        Me.Size = New System.Drawing.Size(612, 407)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents lblAyuda As Zamba.AppBlock.ZLabel
    Friend WithEvents btnAceptar As Zamba.AppBlock.ZButton
    Friend WithEvents txtUrl As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblCambios As Zamba.AppBlock.ZLabel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Rbt_Home As RadioButton
    Friend WithEvents Rbt_Modal As RadioButton
    Friend WithEvents Rbt_NewTab As RadioButton
End Class
