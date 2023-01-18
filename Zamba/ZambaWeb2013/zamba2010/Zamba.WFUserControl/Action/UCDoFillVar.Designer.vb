<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoFillVar
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
    Private Shadows Sub InitializeComponent()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.btnAceptar = New Zamba.AppBlock.ZButton()
        Me.lblVariableName = New Zamba.AppBlock.ZLabel()
        Me.lblVariableValue = New Zamba.AppBlock.ZLabel()
        Me.txtVariableName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblVariableTitle = New Zamba.AppBlock.ZLabel()
        Me.btnAccept = New Zamba.AppBlock.ZButton()
        Me.txtVariableValue = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.chkConc = New System.Windows.Forms.CheckBox()
        Me.txtConc = New System.Windows.Forms.TextBox()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
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
        Me.tbRule.Controls.Add(Me.Label4)
        Me.tbRule.Controls.Add(Me.txtConc)
        Me.tbRule.Controls.Add(Me.chkConc)
        Me.tbRule.Controls.Add(Me.txtVariableValue)
        Me.tbRule.Controls.Add(Me.btnAccept)
        Me.tbRule.Controls.Add(Me.lblVariableTitle)
        Me.tbRule.Controls.Add(Me.txtVariableName)
        Me.tbRule.Controls.Add(Me.lblVariableValue)
        Me.tbRule.Controls.Add(Me.lblVariableName)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(655, 424)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(663, 453)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label1.FontSize = 9.75!
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(45, 98)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nombre "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label2.FontSize = 9.75!
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(45, 145)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Valor "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(172, 95)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(212, 23)
        Me.TextBox1.TabIndex = 2
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(172, 137)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(212, 23)
        Me.TextBox2.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label3.FontSize = 9.75!
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(45, 44)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Variable"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(200, 218)
        Me.btnAceptar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(149, 28)
        Me.btnAceptar.TabIndex = 13
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'lblVariableName
        '
        Me.lblVariableName.AutoSize = True
        Me.lblVariableName.BackColor = System.Drawing.Color.Transparent
        Me.lblVariableName.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblVariableName.FontSize = 9.75!
        Me.lblVariableName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblVariableName.Location = New System.Drawing.Point(4, 35)
        Me.lblVariableName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVariableName.Name = "lblVariableName"
        Me.lblVariableName.Size = New System.Drawing.Size(57, 16)
        Me.lblVariableName.TabIndex = 0
        Me.lblVariableName.Text = "Nombre"
        Me.lblVariableName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblVariableValue
        '
        Me.lblVariableValue.AutoSize = True
        Me.lblVariableValue.BackColor = System.Drawing.Color.Transparent
        Me.lblVariableValue.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblVariableValue.FontSize = 9.75!
        Me.lblVariableValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblVariableValue.Location = New System.Drawing.Point(4, 120)
        Me.lblVariableValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVariableValue.Name = "lblVariableValue"
        Me.lblVariableValue.Size = New System.Drawing.Size(40, 16)
        Me.lblVariableValue.TabIndex = 1
        Me.lblVariableValue.Text = "Valor"
        Me.lblVariableValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVariableName
        '
        Me.txtVariableName.BackColor = System.Drawing.Color.White
        Me.txtVariableName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVariableName.Location = New System.Drawing.Point(7, 55)
        Me.txtVariableName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtVariableName.Name = "txtVariableName"
        Me.txtVariableName.Size = New System.Drawing.Size(646, 61)
        Me.txtVariableName.TabIndex = 2
        Me.txtVariableName.Text = ""
        '
        'lblVariableTitle
        '
        Me.lblVariableTitle.AutoSize = True
        Me.lblVariableTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblVariableTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblVariableTitle.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblVariableTitle.FontSize = 9.75!
        Me.lblVariableTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblVariableTitle.Location = New System.Drawing.Point(4, 4)
        Me.lblVariableTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVariableTitle.Name = "lblVariableTitle"
        Me.lblVariableTitle.Size = New System.Drawing.Size(130, 16)
        Me.lblVariableTitle.TabIndex = 4
        Me.lblVariableTitle.Text = "Completar Variable"
        Me.lblVariableTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAccept
        '
        Me.btnAccept.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAccept.ForeColor = System.Drawing.Color.White
        Me.btnAccept.Location = New System.Drawing.Point(356, 334)
        Me.btnAccept.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(100, 28)
        Me.btnAccept.TabIndex = 5
        Me.btnAccept.Text = "Guardar"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'txtVariableValue
        '
        Me.txtVariableValue.BackColor = System.Drawing.Color.White
        Me.txtVariableValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVariableValue.Location = New System.Drawing.Point(7, 140)
        Me.txtVariableValue.Margin = New System.Windows.Forms.Padding(4)
        Me.txtVariableValue.MaxLength = 4000
        Me.txtVariableValue.Name = "txtVariableValue"
        Me.txtVariableValue.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtVariableValue.Size = New System.Drawing.Size(646, 170)
        Me.txtVariableValue.TabIndex = 14
        Me.txtVariableValue.Text = ""
        '
        'chkConc
        '
        Me.chkConc.AutoSize = True
        Me.chkConc.Location = New System.Drawing.Point(13, 318)
        Me.chkConc.Margin = New System.Windows.Forms.Padding(4)
        Me.chkConc.Name = "chkConc"
        Me.chkConc.Size = New System.Drawing.Size(206, 20)
        Me.chkConc.TabIndex = 15
        Me.chkConc.Text = "Concatenar Variable Actual"
        Me.chkConc.UseVisualStyleBackColor = True
        '
        'txtConc
        '
        Me.txtConc.BackColor = System.Drawing.Color.White
        Me.txtConc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConc.Location = New System.Drawing.Point(4, 372)
        Me.txtConc.Margin = New System.Windows.Forms.Padding(4)
        Me.txtConc.Multiline = True
        Me.txtConc.Name = "txtConc"
        Me.txtConc.Size = New System.Drawing.Size(376, 44)
        Me.txtConc.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label4.FontSize = 9.75!
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(40, 346)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(148, 16)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Texto Concatenación"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDoFillVar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoFillVar"
        Me.Size = New System.Drawing.Size(663, 453)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.TextBox1, 0)
        Me.Controls.SetChildIndex(Me.TextBox2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Controls.SetChildIndex(Me.tbctrMain, 0)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents btnAccept As ZButton
    Friend WithEvents lblVariableTitle As ZLabel
    Friend WithEvents txtVariableName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblVariableValue As ZLabel
    Friend WithEvents lblVariableName As ZLabel
    Friend WithEvents txtVariableValue As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtConc As System.Windows.Forms.TextBox
    Friend WithEvents chkConc As System.Windows.Forms.CheckBox

End Class
