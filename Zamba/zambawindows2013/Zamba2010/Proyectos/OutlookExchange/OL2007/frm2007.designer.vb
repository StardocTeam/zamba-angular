<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm2007
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
    Private Sub InitializeComponent()
        Me.btnLibreta = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPara = New System.Windows.Forms.TextBox
        Me.txtCC = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCCO = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnLibreta
        '
        Me.btnLibreta.Location = New System.Drawing.Point(50, 87)
        Me.btnLibreta.Name = "btnLibreta"
        Me.btnLibreta.Size = New System.Drawing.Size(399, 25)
        Me.btnLibreta.TabIndex = 0
        Me.btnLibreta.Text = "&Abrir libreta de direcciones"
        Me.btnLibreta.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Para:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPara
        '
        Me.txtPara.Location = New System.Drawing.Point(50, 9)
        Me.txtPara.Name = "txtPara"
        Me.txtPara.Size = New System.Drawing.Size(530, 20)
        Me.txtPara.TabIndex = 2
        '
        'txtCC
        '
        Me.txtCC.Location = New System.Drawing.Point(50, 35)
        Me.txtCC.Name = "txtCC"
        Me.txtCC.Size = New System.Drawing.Size(530, 20)
        Me.txtCC.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "CC:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtCCO
        '
        Me.txtCCO.Location = New System.Drawing.Point(50, 61)
        Me.txtCCO.Name = "txtCCO"
        Me.txtCCO.Size = New System.Drawing.Size(530, 20)
        Me.txtCCO.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "CCO:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(455, 87)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(125, 25)
        Me.btnSalir.TabIndex = 7
        Me.btnSalir.Text = "&Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'frmAddressBook
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 116)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.txtCCO)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCC)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPara)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnLibreta)
        Me.Name = "frmAddressBook"
        Me.Text = "Exchange"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnLibreta As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPara As System.Windows.Forms.TextBox
    Friend WithEvents txtCC As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCCO As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSalir As System.Windows.Forms.Button

End Class
