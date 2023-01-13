<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCDODecisionMatrix
    Inherits ZRuleControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Overloads Sub InitializeComponent()
        Me.CBOIndexs = New System.Windows.Forms.ComboBox()
        Me.CBOEntities = New System.Windows.Forms.ComboBox()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.LabelEntity = New Zamba.AppBlock.ZLabel()
        Me.BTNADD = New Zamba.AppBlock.ZButton()
        Me.ZLabel1 = New Zamba.AppBlock.ZLabel()
        Me.TextoInteligenteTextBox1 = New Zamba.AppBlock.TextoInteligenteTextBox()
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
        Me.tbRule.Controls.Add(Me.TextoInteligenteTextBox1)
        Me.tbRule.Controls.Add(Me.ZLabel1)
        Me.tbRule.Controls.Add(Me.BTNADD)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.CBOIndexs)
        Me.tbRule.Controls.Add(Me.LabelEntity)
        Me.tbRule.Controls.Add(Me.CBOEntities)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(668, 360)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(676, 389)
        '
        'CBOIndexs
        '
        Me.CBOIndexs.FormattingEnabled = True
        Me.CBOIndexs.Location = New System.Drawing.Point(42, 154)
        Me.CBOIndexs.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOIndexs.Name = "CBOIndexs"
        Me.CBOIndexs.Size = New System.Drawing.Size(581, 24)
        Me.CBOIndexs.TabIndex = 0
        '
        'CBOEntities
        '
        Me.CBOEntities.FormattingEnabled = True
        Me.CBOEntities.Location = New System.Drawing.Point(42, 70)
        Me.CBOEntities.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOEntities.Name = "CBOEntities"
        Me.CBOEntities.Size = New System.Drawing.Size(581, 24)
        Me.CBOEntities.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label2.FontSize = 9.75!
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(39, 134)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Atributo de Salida"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelEntity
        '
        Me.LabelEntity.AutoSize = True
        Me.LabelEntity.BackColor = System.Drawing.Color.Transparent
        Me.LabelEntity.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LabelEntity.FontSize = 9.75!
        Me.LabelEntity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.LabelEntity.Location = New System.Drawing.Point(39, 50)
        Me.LabelEntity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelEntity.Name = "LabelEntity"
        Me.LabelEntity.Size = New System.Drawing.Size(139, 16)
        Me.LabelEntity.TabIndex = 4
        Me.LabelEntity.Text = "Entidad de la Matriz"
        Me.LabelEntity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BTNADD
        '
        Me.BTNADD.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTNADD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTNADD.ForeColor = System.Drawing.Color.White
        Me.BTNADD.Location = New System.Drawing.Point(490, 290)
        Me.BTNADD.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNADD.Name = "BTNADD"
        Me.BTNADD.Size = New System.Drawing.Size(100, 28)
        Me.BTNADD.TabIndex = 5
        Me.BTNADD.Text = "Aceptar"
        Me.BTNADD.UseVisualStyleBackColor = False
        '
        'ZLabel1
        '
        Me.ZLabel1.AutoSize = True
        Me.ZLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel1.FontSize = 9.75!
        Me.ZLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel1.Location = New System.Drawing.Point(42, 201)
        Me.ZLabel1.Name = "ZLabel1"
        Me.ZLabel1.Size = New System.Drawing.Size(262, 16)
        Me.ZLabel1.TabIndex = 6
        Me.ZLabel1.Text = "Variable a Asignar Parametro de Salida"
        Me.ZLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextoInteligenteTextBox1
        '
        Me.TextoInteligenteTextBox1.Location = New System.Drawing.Point(45, 234)
        Me.TextoInteligenteTextBox1.Name = "TextoInteligenteTextBox1"
        Me.TextoInteligenteTextBox1.Size = New System.Drawing.Size(559, 30)
        Me.TextoInteligenteTextBox1.TabIndex = 7
        Me.TextoInteligenteTextBox1.Text = ""
        '
        'UCDODecisionMatrix
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDODecisionMatrix"
        Me.Size = New System.Drawing.Size(676, 389)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CBOIndexs As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents CBOEntities As System.Windows.Forms.ComboBox
    Friend WithEvents LabelEntity As ZLabel
    Friend WithEvents BTNADD As Zamba.AppBlock.ZButton 'ZButton
    Friend WithEvents TextoInteligenteTextBox1 As TextoInteligenteTextBox
    Friend WithEvents ZLabel1 As ZLabel

End Class
