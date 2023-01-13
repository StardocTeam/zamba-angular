<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDOAskDesition
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
        Me.TxtVar = New System.Windows.Forms.TextBox()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.btnsave = New Zamba.AppBlock.ZButton()
        Me.txtAsk = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZPanel1 = New Zamba.AppBlock.ZPanel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.ZPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.txtAsk)
        Me.tbRule.Controls.Add(Me.ZPanel1)
        Me.tbRule.Controls.Add(Me.Label4)
        Me.tbRule.Size = New System.Drawing.Size(490, 381)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(498, 410)
        '
        'TxtVar
        '
        Me.TxtVar.Location = New System.Drawing.Point(3, 28)
        Me.TxtVar.Name = "TxtVar"
        Me.TxtVar.Size = New System.Drawing.Size(303, 23)
        Me.TxtVar.TabIndex = 17
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(3, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(134, 16)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Nombre de Variable"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(127, 16)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Texto a Preguntar"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnsave
        '
        Me.btnsave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsave.ForeColor = System.Drawing.Color.White
        Me.btnsave.Location = New System.Drawing.Point(391, 23)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(77, 33)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Guardar"
        Me.btnsave.UseVisualStyleBackColor = True
        '
        'txtAsk
        '
        Me.txtAsk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAsk.Location = New System.Drawing.Point(3, 19)
        Me.txtAsk.Name = "txtAsk"
        Me.txtAsk.Size = New System.Drawing.Size(484, 296)
        Me.txtAsk.TabIndex = 19
        Me.txtAsk.Text = ""
        '
        'ZPanel1
        '
        Me.ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel1.Controls.Add(Me.btnsave)
        Me.ZPanel1.Controls.Add(Me.TxtVar)
        Me.ZPanel1.Controls.Add(Me.Label2)
        Me.ZPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ZPanel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel1.Location = New System.Drawing.Point(3, 315)
        Me.ZPanel1.Name = "ZPanel1"
        Me.ZPanel1.Size = New System.Drawing.Size(484, 63)
        Me.ZPanel1.TabIndex = 20
        '
        'UCDOAskDesition
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.AutoSize = True
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Name = "UCDOAskDesition"
        Me.Size = New System.Drawing.Size(498, 410)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ZPanel1.ResumeLayout(False)
        Me.ZPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents TxtVar As System.Windows.Forms.TextBox
    Friend WithEvents txtAsk As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents ZPanel1 As ZPanel
End Class
