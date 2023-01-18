<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCSelectMail
    Inherits System.Windows.Forms.UserControl

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

    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents txtFor As Zamba.AppBlock.TextoInteligente
    Friend WithEvents txtCC As Zamba.AppBlock.TextoInteligente
    Friend WithEvents txtCCO As Zamba.AppBlock.TextoInteligente
    Friend WithEvents txtSubject As Zamba.AppBlock.TextoInteligente
    Friend WithEvents chkAttachDocument As System.Windows.Forms.CheckBox
    Friend WithEvents txtBody As Zamba.AppBlock.TextoInteligente

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnAccept = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtFor = New Zamba.AppBlock.TextoInteligente
        Me.txtCC = New Zamba.AppBlock.TextoInteligente
        Me.txtCCO = New Zamba.AppBlock.TextoInteligente
        Me.txtSubject = New Zamba.AppBlock.TextoInteligente
        Me.chkAttachDocument = New System.Windows.Forms.CheckBox
        Me.txtBody = New Zamba.AppBlock.TextoInteligente
        Me.btnPara = New System.Windows.Forms.Button
        Me.btnCC = New System.Windows.Forms.Button
        Me.btnCCO = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(40, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 19)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Para"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAccept
        '
        Me.btnAccept.Location = New System.Drawing.Point(216, 400)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(112, 23)
        Me.btnAccept.TabIndex = 12
        Me.btnAccept.Text = "Aceptar"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(40, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 19)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "CC"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(40, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 19)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "CCO"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(40, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 19)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Asunto"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(40, 200)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 16)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Cuerpo"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(40, 168)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(158, 16)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Adjuntar Documento"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFor
        '
        Me.txtFor.Location = New System.Drawing.Point(112, 40)
        Me.txtFor.Name = "txtFor"
        Me.txtFor.Size = New System.Drawing.Size(368, 21)
        Me.txtFor.TabIndex = 18
        Me.txtFor.Text = ""
        '
        'txtCC
        '
        Me.txtCC.Location = New System.Drawing.Point(112, 72)
        Me.txtCC.Name = "txtCC"
        Me.txtCC.Size = New System.Drawing.Size(368, 21)
        Me.txtCC.TabIndex = 19
        Me.txtCC.Text = ""
        '
        'txtCCO
        '
        Me.txtCCO.Location = New System.Drawing.Point(112, 104)
        Me.txtCCO.Name = "txtCCO"
        Me.txtCCO.Size = New System.Drawing.Size(368, 21)
        Me.txtCCO.TabIndex = 20
        Me.txtCCO.Text = ""
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(112, 136)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(368, 21)
        Me.txtSubject.TabIndex = 21
        Me.txtSubject.Text = ""
        '
        'chkAttachDocument
        '
        Me.chkAttachDocument.BackColor = System.Drawing.Color.Transparent
        Me.chkAttachDocument.Location = New System.Drawing.Point(216, 170)
        Me.chkAttachDocument.Name = "chkAttachDocument"
        Me.chkAttachDocument.Size = New System.Drawing.Size(24, 16)
        Me.chkAttachDocument.TabIndex = 22
        Me.chkAttachDocument.UseVisualStyleBackColor = False
        '
        'txtBody
        '
        Me.txtBody.Location = New System.Drawing.Point(40, 224)
        Me.txtBody.Name = "txtBody"
        Me.txtBody.Size = New System.Drawing.Size(440, 160)
        Me.txtBody.TabIndex = 23
        Me.txtBody.Text = ""
        '
        'btnPara
        '
        Me.btnPara.Location = New System.Drawing.Point(486, 38)
        Me.btnPara.Name = "btnPara"
        Me.btnPara.Size = New System.Drawing.Size(30, 23)
        Me.btnPara.TabIndex = 24
        Me.btnPara.Text = "..."
        Me.btnPara.UseVisualStyleBackColor = True
        '
        'btnCC
        '
        Me.btnCC.Location = New System.Drawing.Point(486, 70)
        Me.btnCC.Name = "btnCC"
        Me.btnCC.Size = New System.Drawing.Size(30, 23)
        Me.btnCC.TabIndex = 25
        Me.btnCC.Text = "..."
        Me.btnCC.UseVisualStyleBackColor = True
        '
        'btnCCO
        '
        Me.btnCCO.Location = New System.Drawing.Point(486, 104)
        Me.btnCCO.Name = "btnCCO"
        Me.btnCCO.Size = New System.Drawing.Size(30, 23)
        Me.btnCCO.TabIndex = 26
        Me.btnCCO.Text = "..."
        Me.btnCCO.UseVisualStyleBackColor = True
        '
        'UCSelectMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Controls.Add(Me.btnCCO)
        Me.Controls.Add(Me.btnCC)
        Me.Controls.Add(Me.btnPara)
        Me.Controls.Add(Me.txtBody)
        Me.Controls.Add(Me.chkAttachDocument)
        Me.Controls.Add(Me.txtSubject)
        Me.Controls.Add(Me.txtCCO)
        Me.Controls.Add(Me.txtCC)
        Me.Controls.Add(Me.txtFor)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAccept)
        Me.Controls.Add(Me.Label4)
        Me.Name = "UCSelectMail"
        Me.Size = New System.Drawing.Size(576, 440)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPara As System.Windows.Forms.Button
    Friend WithEvents btnCC As System.Windows.Forms.Button
    Friend WithEvents btnCCO As System.Windows.Forms.Button

End Class
