<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GetPasswordZip
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BoxPass = New System.Windows.Forms.TextBox()
        Me.NameFile = New System.Windows.Forms.TextBox()
        Me.Passlbl = New Zamba.AppBlock.ZLabel()
        Me.NameFilelbl = New Zamba.AppBlock.ZLabel()
        Me.AceptarPass = New Zamba.AppBlock.ZButton()
        Me.CancelPass = New Zamba.AppBlock.ZButton()
        Me.Label1 = New ZLabel()
        Me.SuspendLayout()
        '
        'BoxPass
        '
        Me.BoxPass.Location = New System.Drawing.Point(13, 44)
        Me.BoxPass.Name = "BoxPass"
        Me.BoxPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.BoxPass.Size = New System.Drawing.Size(173, 20)
        Me.BoxPass.TabIndex = 1
        '
        'NameFile
        '
        Me.NameFile.Location = New System.Drawing.Point(12, 113)
        Me.NameFile.Name = "NameFile"
        Me.NameFile.Size = New System.Drawing.Size(173, 20)
        Me.NameFile.TabIndex = 2
        '
        'Passlbl
        '
        Me.Passlbl.AutoSize = True
        Me.Passlbl.Location = New System.Drawing.Point(12, 28)
        Me.Passlbl.Name = "Passlbl"
        Me.Passlbl.Size = New System.Drawing.Size(64, 13)
        Me.Passlbl.TabIndex = 4
        Me.Passlbl.Tag = "PASWORD"
        Me.Passlbl.Text = "Contraseña:"
        '
        'NameFilelbl
        '
        Me.NameFilelbl.AutoSize = True
        Me.NameFilelbl.Location = New System.Drawing.Point(12, 97)
        Me.NameFilelbl.Name = "NameFilelbl"
        Me.NameFilelbl.Size = New System.Drawing.Size(85, 13)
        Me.NameFilelbl.TabIndex = 5
        Me.NameFilelbl.Tag = "NAMEFILE"
        Me.NameFilelbl.Text = "Nombre archivo:"
        '
        'AceptarPass
        '
        Me.AceptarPass.BackColor = System.Drawing.Color.FromArgb(0, 157, 224)
        Me.AceptarPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AceptarPass.ForeColor = System.Drawing.Color.White
        Me.AceptarPass.Location = New System.Drawing.Point(12, 185)
        Me.AceptarPass.Name = "AceptarPass"
        Me.AceptarPass.Size = New System.Drawing.Size(75, 23)
        Me.AceptarPass.TabIndex = 6
        Me.AceptarPass.Text = "Aceptar"
        Me.AceptarPass.UseVisualStyleBackColor = False
        '
        'CancelPass
        '
        Me.CancelPass.BackColor = System.Drawing.Color.FromArgb(0, 157, 224)
        Me.CancelPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CancelPass.ForeColor = System.Drawing.Color.White
        Me.CancelPass.Location = New System.Drawing.Point(185, 185)
        Me.CancelPass.Name = "CancelPass"
        Me.CancelPass.Size = New System.Drawing.Size(75, 23)
        Me.CancelPass.TabIndex = 7
        Me.CancelPass.Text = "Cancelar"
        Me.CancelPass.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(119, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "(Opcional)"
        '
        'GetPasswordZip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 219)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CancelPass)
        Me.Controls.Add(Me.AceptarPass)
        Me.Controls.Add(Me.NameFilelbl)
        Me.Controls.Add(Me.Passlbl)
        Me.Controls.Add(Me.NameFile)
        Me.Controls.Add(Me.BoxPass)
        Me.Name = "GetPasswordZip"
        Me.Text = "Ingresar Contraseña."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BoxPass As TextBox
    Friend WithEvents NameFile As TextBox
    Friend WithEvents Passlbl As AppBlock.ZLabel
    Friend WithEvents NameFilelbl As AppBlock.ZLabel
    Friend WithEvents AceptarPass As AppBlock.ZButton
    Friend WithEvents CancelPass As AppBlock.ZButton
    Friend WithEvents Label1 As Label
End Class
