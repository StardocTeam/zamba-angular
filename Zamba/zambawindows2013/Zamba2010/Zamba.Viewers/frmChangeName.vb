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
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button2 As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Button3 As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(frmNameChange))
        Label1 = New ZLabel
        TextBox1 = New TextBox
        Button2 = New ZButton
        Button3 = New ZButton
        SuspendLayout()
        '
        'Label1
        '
        Label1.Font = New Font("Tahoma", 10.0!, FontStyle.Bold)
        Label1.ForeColor = System.Drawing.Color.White
        Label1.Location = New System.Drawing.Point(5, 2)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(296, 34)
        Label1.TabIndex = 0
        Label1.Text = "Nombre:"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'TextBox1
        '
        TextBox1.BackColor = System.Drawing.Color.White
        TextBox1.Font = New Font("Microsoft Sans Serif", 9.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        TextBox1.Location = New System.Drawing.Point(5, 39)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(296, 21)
        TextBox1.TabIndex = 0
        '
        'Button2
        '
        Button2.DialogResult = System.Windows.Forms.DialogResult.None
        Button2.Location = New System.Drawing.Point(229, 66)
        Button2.Name = "Button2"
        Button2.Size = New System.Drawing.Size(72, 26)
        Button2.TabIndex = 2
        Button2.Text = "Cancelar"
        '
        'Button3
        '
        Button3.DialogResult = System.Windows.Forms.DialogResult.None
        Button3.Location = New System.Drawing.Point(151, 66)
        Button3.Name = "Button3"
        Button3.Size = New System.Drawing.Size(72, 26)
        Button3.TabIndex = 3
        Button3.Text = "Aplicar"
        '
        'frmNameChange
        '
        AcceptButton = Button3
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(305, 97)
        ControlBox = False
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(TextBox1)
        Controls.Add(Label1)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MaximumSize = New System.Drawing.Size(311, 121)
        MinimumSize = New System.Drawing.Size(311, 121)
        Name = "frmNameChange"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Cambiar Nombre de Documento"
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region
    Public cambios As String
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button3.Click
        DialogResult = DialogResult.OK
        cambios = TextBox1.Text
        Close()
    End Sub
    Private result As Result
    Public Sub New(ByRef Result As Result)
        InitializeComponent()
        Try
            Me.result = result
            TextBox1.Text = result.Name
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button2.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub


End Class
