Public Class frmMultiline
    Inherits Zamba.appblock.zform

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal data As String, ByVal name As String)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        txtIndexValue.Text = data
        Me.Name = name
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If txtIndexValue IsNot Nothing Then
                    txtIndexValue.Dispose()
                    txtIndexValue = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents txtIndexValue As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        txtIndexValue = New TextBox
        SuspendLayout()
        '
        'TextBox1
        '
        txtIndexValue.BackColor = Color.White
        txtIndexValue.Dock = DockStyle.Fill
        txtIndexValue.Location = New Point(2, 2)
        txtIndexValue.Multiline = True
        txtIndexValue.Name = "TextBox1"
        txtIndexValue.Size = New Size(276, 268)
        txtIndexValue.TabIndex = 0
        txtIndexValue.Text = ""
        '
        'Form1
        '
        AutoScaleBaseSize = New Size(5, 14)
        ClientSize = New Size(280, 272)
        Controls.Add(txtIndexValue)
        DockPadding.All = 2
        MaximizeBox = False
        MinimizeBox = False
        Name = "Form1"
        ShowInTaskbar = False
        SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Text = "Edicion de Atributo"
        StartPosition = FormStartPosition.CenterScreen
        ResumeLayout(False)

    End Sub

#End Region


    Public Shadows Event FormClosing(ByVal data As String)


    Private Sub Textbox1_DoubleClick(ByVal Sender As Object, ByVal e As EventArgs) Handles txtIndexValue.DoubleClick
        Close()
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        RaiseEvent FormClosing(txtIndexValue.Text)
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        BringToFront()
    End Sub
End Class
