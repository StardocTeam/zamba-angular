<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoGetNewId
    Inherits ZRuleControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Overloads Sub InitializeComponent()
        Me.CBOIndexs = New System.Windows.Forms.ComboBox()
        Me.CBODoctypes = New System.Windows.Forms.ComboBox()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.BTNADD = New Zamba.AppBlock.ZButton()
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
        Me.tbRule.Controls.Add(Me.BTNADD)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.Label1)
        Me.tbRule.Controls.Add(Me.CBODoctypes)
        Me.tbRule.Controls.Add(Me.CBOIndexs)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(476, 302)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(484, 331)
        '
        'CBOIndexs
        '
        Me.CBOIndexs.FormattingEnabled = True
        Me.CBOIndexs.Location = New System.Drawing.Point(43, 155)
        Me.CBOIndexs.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CBOIndexs.Name = "CBOIndexs"
        Me.CBOIndexs.Size = New System.Drawing.Size(361, 24)
        Me.CBOIndexs.TabIndex = 0
        '
        'CBODoctypes
        '
        Me.CBODoctypes.FormattingEnabled = True
        Me.CBODoctypes.Location = New System.Drawing.Point(43, 68)
        Me.CBODoctypes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CBODoctypes.Name = "CBODoctypes"
        Me.CBODoctypes.Size = New System.Drawing.Size(361, 24)
        Me.CBODoctypes.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(39, 31)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Seleccione la entidad"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(39, 124)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(204, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Atributo que desea completar"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BTNADD
        '
        Me.BTNADD.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTNADD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTNADD.ForeColor = System.Drawing.Color.White
        Me.BTNADD.Location = New System.Drawing.Point(304, 210)
        Me.BTNADD.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTNADD.Name = "BTNADD"
        Me.BTNADD.Size = New System.Drawing.Size(100, 28)
        Me.BTNADD.TabIndex = 5
        Me.BTNADD.Text = "Guardar"
        Me.BTNADD.UseVisualStyleBackColor = False
        '
        'UCDoGetNewId
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoGetNewId"
        Me.Size = New System.Drawing.Size(484, 331)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CBOIndexs As System.Windows.Forms.ComboBox
    Friend WithEvents CBODoctypes As System.Windows.Forms.ComboBox
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents BTNADD As Zamba.AppBlock.ZButton 'ZButton

End Class
