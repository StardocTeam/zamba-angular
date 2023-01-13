<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDOInputIndex
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
        Me.CBOIndexs = New System.Windows.Forms.ComboBox
        Me.Label2 = New ZLabel
        Me.BTNADD = New Zamba.AppBlock.ZButton
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.BTNADD)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.CBOIndexs)
        Me.tbRule.Size = New System.Drawing.Size(323, 192)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(331, 218)
        '
        'CBOIndexs
        '
        Me.CBOIndexs.FormattingEnabled = True
        Me.CBOIndexs.Location = New System.Drawing.Point(32, 51)
        Me.CBOIndexs.Name = "CBOIndexs"
        Me.CBOIndexs.Size = New System.Drawing.Size(272, 21)
        Me.CBOIndexs.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(139, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Atributo que desea completar"
        '
        'BTNADD
        '
        Me.BTNADD.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BTNADD.Location = New System.Drawing.Point(119, 105)
        Me.BTNADD.Name = "BTNADD"
        Me.BTNADD.Size = New System.Drawing.Size(75, 23)
        Me.BTNADD.TabIndex = 5
        Me.BTNADD.Text = "Aceptar"
        '
        'UCDOInputIndex
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDOInputIndex"
        Me.Size = New System.Drawing.Size(331, 218)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CBOIndexs As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents BTNADD As Zamba.AppBlock.ZButton 'ZButton


End Class
