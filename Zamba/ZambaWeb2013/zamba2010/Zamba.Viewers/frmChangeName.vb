Imports Zamba.AppBlock
Imports zamba.core
Public Class frmNameChange
	Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "

	Public Sub New()
		MyBase.New()

		'El Diseñador de Windows Forms requiere esta llamada.
		InitializeComponent()

		'Agregar cualquier inicialización después de la llamada a InitializeComponent()

	End Sub

	'Form reemplaza a Dispose para limpiar la lista de componentes.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Requerido por el Diseñador de Windows Forms
	Private components As System.ComponentModel.IContainer

	'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
	'Puede modificarse utilizando el Diseñador de Windows Forms. 
	'No lo modifique con el editor de código.
	Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
	Friend WithEvents Button2 As ZButton1
	Friend WithEvents Label1 As ZTitleLabel
	Friend WithEvents Button3 As ZButton
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNameChange))
        Me.Label1 = New Zamba.AppBlock.ZTitleLabel
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Button2 = New Zamba.AppBlock.ZButton1
        Me.Button3 = New Zamba.AppBlock.ZButton
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(5, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(296, 34)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nombre:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(5, 39)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(296, 21)
        Me.TextBox1.TabIndex = 0
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Button2.Location = New System.Drawing.Point(229, 66)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 26)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Cancelar"
        '
        'Button3
        '
        Me.Button3.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Button3.Location = New System.Drawing.Point(151, 66)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(72, 26)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Aplicar"
        '
        'frmNameChange
        '
        Me.AcceptButton = Me.Button3
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(305, 97)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(311, 121)
        Me.MinimumSize = New System.Drawing.Size(311, 121)
        Me.Name = "frmNameChange"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cambiar Nombre de Documento"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Public cambios As String
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        cambios = Me.TextBox1.Text
        Me.Close()
    End Sub
    Private result As Result
    Public Sub New(ByRef Result As Result)
        InitializeComponent()
        Try
            Me.result = result
            Me.TextBox1.Text = result.Name
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

   
End Class
