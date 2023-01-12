Public Class FrmTextBox
    Inherits Zamba.AppBlock.ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(Optional ByVal Texto As String = "")
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        txtTexto.Text = Texto
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
    Friend WithEvents txtTexto As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        txtTexto = New TextBox()
        SuspendLayout()
        '
        'txtTexto
        '
        txtTexto.Dock = System.Windows.Forms.DockStyle.Fill
        txtTexto.Font = New Font("Verdana", 12.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtTexto.Location = New System.Drawing.Point(2, 2)
        txtTexto.MaxLength = 1500
        txtTexto.Multiline = True
        txtTexto.Name = "txtTexto"
        txtTexto.Size = New System.Drawing.Size(404, 218)
        txtTexto.TabIndex = 0
        '
        'FrmTextBox
        '
        AutoScaleBaseSize = New System.Drawing.Size(8, 17)
        ClientSize = New System.Drawing.Size(408, 222)
        Controls.Add(txtTexto)
        Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Name = "FrmTextBox"
        ShowInTaskbar = False
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

    Private Sub FrmTextBox_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Clipboard.SetDataObject(txtTexto.Text, True)
    End Sub
End Class
