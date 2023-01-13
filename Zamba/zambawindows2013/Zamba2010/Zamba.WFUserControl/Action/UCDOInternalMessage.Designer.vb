<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class UCDOInternalMessage
    Inherits ZRuleControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Overloads Sub InitializeComponent()
        Me.btGuardar = New ZButton
        Me.tbBody = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.lbPara = New ZLabel
        Me.lbCC = New ZLabel
        Me.lbAsunto = New ZLabel
        Me.tbPara = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.tbCC = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.lbCCO = New ZLabel
        Me.tbCCO = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.tbAsunto = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chbMail = New System.Windows.Forms.CheckBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.Panel2)
        Me.tbRule.Controls.Add(Me.Splitter1)
        Me.tbRule.Controls.Add(Me.Panel1)
        Me.tbRule.Size = New System.Drawing.Size(497, 373)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(505, 399)
        '
        'btGuardar
        '
        Me.btGuardar.Location = New System.Drawing.Point(423, 218)
        Me.btGuardar.Name = "btGuardar"
        Me.btGuardar.Size = New System.Drawing.Size(58, 23)
        Me.btGuardar.TabIndex = 14
        Me.btGuardar.Text = "Guardar"
        Me.btGuardar.UseVisualStyleBackColor = True
        '
        'tbBody
        '
        Me.tbBody.Location = New System.Drawing.Point(12, 6)
        Me.tbBody.Name = "tbBody"
        Me.tbBody.Size = New System.Drawing.Size(467, 206)
        Me.tbBody.TabIndex = 12
        Me.tbBody.Text = ""
        '
        'lbPara
        '
        Me.lbPara.AutoSize = True
        Me.lbPara.Location = New System.Drawing.Point(8, 11)
        Me.lbPara.Name = "lbPara"
        Me.lbPara.Size = New System.Drawing.Size(44, 13)
        Me.lbPara.TabIndex = 1
        Me.lbPara.Text = "Para ..."
        '
        'lbCC
        '
        Me.lbCC.AutoSize = True
        Me.lbCC.Location = New System.Drawing.Point(8, 37)
        Me.lbCC.Name = "lbCC"
        Me.lbCC.Size = New System.Drawing.Size(36, 13)
        Me.lbCC.TabIndex = 3
        Me.lbCC.Text = "CC ..."
        '
        'lbAsunto
        '
        Me.lbAsunto.AutoSize = True
        Me.lbAsunto.Location = New System.Drawing.Point(8, 89)
        Me.lbAsunto.Name = "lbAsunto"
        Me.lbAsunto.Size = New System.Drawing.Size(44, 13)
        Me.lbAsunto.TabIndex = 7
        Me.lbAsunto.Text = "Asunto "
        '
        'tbPara
        '
        Me.tbPara.Location = New System.Drawing.Point(52, 8)
        Me.tbPara.Name = "tbPara"
        Me.tbPara.Size = New System.Drawing.Size(425, 21)
        Me.tbPara.TabIndex = 2
        Me.tbPara.Text = ""
        '
        'tbCC
        '
        Me.tbCC.Location = New System.Drawing.Point(52, 34)
        Me.tbCC.Name = "tbCC"
        Me.tbCC.Size = New System.Drawing.Size(425, 21)
        Me.tbCC.TabIndex = 4
        Me.tbCC.Text = ""
        '
        'lbCCO
        '
        Me.lbCCO.AutoSize = True
        Me.lbCCO.Location = New System.Drawing.Point(8, 63)
        Me.lbCCO.Name = "lbCCO"
        Me.lbCCO.Size = New System.Drawing.Size(44, 13)
        Me.lbCCO.TabIndex = 5
        Me.lbCCO.Text = "CCO ..."
        '
        'tbCCO
        '
        Me.tbCCO.Location = New System.Drawing.Point(52, 60)
        Me.tbCCO.Name = "tbCCO"
        Me.tbCCO.Size = New System.Drawing.Size(425, 21)
        Me.tbCCO.TabIndex = 6
        Me.tbCCO.Text = ""
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tbAsunto)
        Me.Panel1.Controls.Add(Me.lbPara)
        Me.Panel1.Controls.Add(Me.lbCC)
        Me.Panel1.Controls.Add(Me.lbCCO)
        Me.Panel1.Controls.Add(Me.tbCCO)
        Me.Panel1.Controls.Add(Me.lbAsunto)
        Me.Panel1.Controls.Add(Me.tbPara)
        Me.Panel1.Controls.Add(Me.tbCC)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(491, 117)
        Me.Panel1.TabIndex = 0
        '
        'tbAsunto
        '
        Me.tbAsunto.Location = New System.Drawing.Point(52, 86)
        Me.tbAsunto.Name = "tbAsunto"
        Me.tbAsunto.Size = New System.Drawing.Size(425, 21)
        Me.tbAsunto.TabIndex = 8
        Me.tbAsunto.Text = ""
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chbMail)
        Me.Panel2.Controls.Add(Me.btGuardar)
        Me.Panel2.Controls.Add(Me.tbBody)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 120)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(491, 247)
        Me.Panel2.TabIndex = 0
        '
        'chbMail
        '
        Me.chbMail.AutoSize = True
        Me.chbMail.Location = New System.Drawing.Point(12, 222)
        Me.chbMail.Name = "chbMail"
        Me.chbMail.Size = New System.Drawing.Size(184, 17)
        Me.chbMail.TabIndex = 13
        Me.chbMail.Text = "Enviar 1 Mensaje por Documento"
        Me.chbMail.UseVisualStyleBackColor = True
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(3, 367)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(491, 3)
        Me.Splitter1.TabIndex = 12
        Me.Splitter1.TabStop = False
        '
        'UCDOInternalMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDOInternalMessage"
        Me.Size = New System.Drawing.Size(505, 399)
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btGuardar As ZButton
    Friend WithEvents tbBody As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lbPara As ZLabel
    Friend WithEvents lbCC As ZLabel
    Friend WithEvents lbAsunto As ZLabel
    Friend WithEvents tbPara As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents tbCC As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lbCCO As ZLabel
    Friend WithEvents tbCCO As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents tbAsunto As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents chbMail As System.Windows.Forms.CheckBox

End Class
