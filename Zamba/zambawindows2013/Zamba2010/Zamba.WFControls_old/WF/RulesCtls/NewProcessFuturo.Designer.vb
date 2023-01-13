<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewProcessFuturo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewProcessFuturo))
        Me.Label4 = New ZLabel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnAceptar = New ZButton()
        Me.Label2 = New ZLabel()
        Me.Button1 = New ZButton()
        Me.Label1 = New ZLabel()
        Me.Label5 = New ZLabel()
        Me.Label7 = New ZLabel()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label3 = New ZLabel()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(505, 24)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Completar datos reglas del nuevo proceso"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(26, 160)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(453, 20)
        Me.TextBox1.TabIndex = 87
        Me.TextBox1.Text = "Enviar mail a los usuarios"
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.Location = New System.Drawing.Point(383, 376)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(105, 29)
        Me.btnAceptar.TabIndex = 86
        Me.btnAceptar.Text = "Generar"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 138)
        Me.Label2.MaximumSize = New System.Drawing.Size(516, 0)
        Me.Label2.MinimumSize = New System.Drawing.Size(0, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 19)
        Me.Label2.TabIndex = 88
        Me.Label2.Text = "Nombre"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(41, 376)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(105, 29)
        Me.Button1.TabIndex = 89
        Me.Button1.Text = "Cancelar"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 192)
        Me.Label1.MaximumSize = New System.Drawing.Size(516, 0)
        Me.Label1.MinimumSize = New System.Drawing.Size(0, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 19)
        Me.Label1.TabIndex = 90
        Me.Label1.Text = "Clase"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(22, 240)
        Me.Label5.MaximumSize = New System.Drawing.Size(516, 0)
        Me.Label5.MinimumSize = New System.Drawing.Size(0, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 19)
        Me.Label5.TabIndex = 92
        Me.Label5.Text = "Tipo"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(28, 289)
        Me.Label7.MaximumSize = New System.Drawing.Size(516, 0)
        Me.Label7.MinimumSize = New System.Drawing.Size(0, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(102, 19)
        Me.Label7.TabIndex = 94
        Me.Label7.Text = "Descripcion"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(27, 214)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(453, 20)
        Me.TextBox2.TabIndex = 96
        Me.TextBox2.Text = "DoMail"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(27, 262)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(453, 20)
        Me.TextBox3.TabIndex = 97
        Me.TextBox3.Text = "Enviar Mail"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(27, 311)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(453, 53)
        Me.TextBox4.TabIndex = 98
        Me.TextBox4.Text = "Permite completar y enviar un mail"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Items.AddRange(New Object() {"Diseño de Reglas", "Ejecutar consulta a Base de Datos", "Repetir Regla", "Enviar por Mail"})
        Me.ListBox1.Location = New System.Drawing.Point(26, 66)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(220, 69)
        Me.ListBox1.TabIndex = 100
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 44)
        Me.Label3.MaximumSize = New System.Drawing.Size(516, 0)
        Me.Label3.MinimumSize = New System.Drawing.Size(0, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(128, 19)
        Me.Label3.TabIndex = 101
        Me.Label3.Text = "Listado Reglas"
        '
        'NewProcessFuturo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(515, 425)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.Label4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "NewProcessFuturo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Datos regla"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Button1 As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As ZLabel
End Class
