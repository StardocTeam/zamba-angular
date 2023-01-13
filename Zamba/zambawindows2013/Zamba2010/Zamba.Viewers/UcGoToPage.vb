Public Class UcGoToPage
    Inherits Zamba.AppBlock.ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        TextBox1 = New TextBox
        SuspendLayout()
        '
        'TextBox1
        '
        TextBox1.BackColor = System.Drawing.Color.White
        TextBox1.Font = New Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        TextBox1.Location = New System.Drawing.Point(4, 8)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(33, 22)
        TextBox1.TabIndex = 0
        TextBox1.Text = ""
        TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'UcGoToPage
        '
        BackColor = System.Drawing.Color.Lavender
        Controls.Add(TextBox1)
        Name = "UcGoToPage"
        Size = New System.Drawing.Size(39, 38)
        ResumeLayout(False)

    End Sub

#End Region

End Class
