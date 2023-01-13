Imports Zamba.AppBlock

Public Class ReplaceMsgBox
    Inherits System.Windows.Forms.Form
    Public Overloads Shared Function Show(ByVal Msg As String, ByVal Title As String) As ReplaceMsgBoxResult
        Dim dlg As New ReplaceMsgBox(Msg, Title)
        dlg.ShowDialog()
        Dim r As ReplaceMsgBoxResult = dlg.ReplaceResult
        dlg.Dispose()
        Return r
    End Function

    Public Enum ReplaceMsgBoxResult
        yes = 0
        no = 1
        yesAll = 2
        noAll = 3
    End Enum
#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal msg As String, ByVal title As String)
        MyBase.New()
        InitializeComponent()

        Label1.Text = msg
        Text = title
    End Sub

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub
    Public ReplaceResult As ReplaceMsgBoxResult

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
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Button1 As ZButton
    Friend WithEvents Button2 As ZButton
    Friend WithEvents Button3 As ZButton
    Friend WithEvents Button4 As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Label1 = New ZLabel
        Button1 = New ZButton
        Button2 = New ZButton
        Button3 = New ZButton
        Button4 = New ZButton
        SuspendLayout()
        '
        'Label1
        '
        Label1.Location = New System.Drawing.Point(8, 8)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(472, 56)
        Label1.TabIndex = 0
        '
        'Button1
        '
        Button1.Location = New System.Drawing.Point(24, 88)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(88, 32)
        Button1.TabIndex = 1
        Button1.Text = "Si"
        '
        'Button2
        '
        Button2.Location = New System.Drawing.Point(138, 88)
        Button2.Name = "Button2"
        Button2.Size = New System.Drawing.Size(88, 32)
        Button2.TabIndex = 2
        Button2.Text = "No"
        '
        'Button3
        '
        Button3.Location = New System.Drawing.Point(252, 88)
        Button3.Name = "Button3"
        Button3.Size = New System.Drawing.Size(88, 32)
        Button3.TabIndex = 3
        Button3.Text = "Si a Todos"
        '
        'Button4
        '
        Button4.Location = New System.Drawing.Point(366, 88)
        Button4.Name = "Button4"
        Button4.Size = New System.Drawing.Size(88, 32)
        Button4.TabIndex = 4
        Button4.Text = "No a Todos"
        '
        'ReplaceMsgBox
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        ClientSize = New System.Drawing.Size(488, 134)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label1)
        Name = "ReplaceMsgBox"
        Text = "ReplaceMsgBox"
        ResumeLayout(False)

    End Sub

#End Region


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button1.Click
        ReplaceResult = ReplaceMsgBoxResult.yes
        Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button2.Click
        ReplaceResult = ReplaceMsgBoxResult.no
        Close()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button3.Click
        ReplaceResult = ReplaceMsgBoxResult.yesAll
        Close()
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button4.Click
        ReplaceResult = ReplaceMsgBoxResult.noAll
        Close()
    End Sub
End Class
