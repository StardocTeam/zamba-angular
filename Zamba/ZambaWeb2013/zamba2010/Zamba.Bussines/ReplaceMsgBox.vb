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

        Me.Label1.Text = msg
        Me.Text = title
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
    Friend WithEvents Label1 As System.windows.forms.Label
    Friend WithEvents Button1 As System.windows.forms.Button
    Friend WithEvents Button2 As System.windows.forms.Button
    Friend WithEvents Button3 As System.windows.forms.Button
    Friend WithEvents Button4 As System.windows.forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.windows.forms.Label
        Me.Button1 = New System.windows.forms.Button
        Me.Button2 = New System.windows.forms.Button
        Me.Button3 = New System.windows.forms.Button
        Me.Button4 = New System.windows.forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(472, 56)
        Me.Label1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(24, 88)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 32)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Si"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(138, 88)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(88, 32)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "No"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(252, 88)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(88, 32)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Si a Todos"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(366, 88)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(88, 32)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "No a Todos"
        '
        'ReplaceMsgBox
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(488, 134)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ReplaceMsgBox"
        Me.Text = "ReplaceMsgBox"
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.ReplaceResult = ReplaceMsgBoxResult.yes
        Me.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.ReplaceResult = ReplaceMsgBoxResult.no
        Me.Close()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.ReplaceResult = ReplaceMsgBoxResult.yesAll
        Me.Close()
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.ReplaceResult = ReplaceMsgBoxResult.noAll
        Me.Close()
    End Sub
End Class
