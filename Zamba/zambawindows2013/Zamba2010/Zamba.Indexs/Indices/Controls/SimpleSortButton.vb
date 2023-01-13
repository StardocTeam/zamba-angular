Public Class SimpleSortButton
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If Label1 IsNot Nothing Then
                    RemoveHandler Label1.Click, AddressOf Clicks
                    Label1.Dispose()
                    Label1 = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch ex As Exception
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Public Label1 As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Label1 = New ZLabel
        SuspendLayout()
        '
        'Label1
        '
        Label1.BackColor = Color.White
        Label1.Dock = DockStyle.Fill
        Label1.FlatStyle = FlatStyle.Flat
        Label1.Font = New Font("Microsoft Sans Serif", 6.0!)
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(3, 3)
        Label1.Name = "Label1"
        Label1.Size = New Size(20, 18)
        Label1.TabIndex = 0
        Label1.Text = "ABC"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        AddHandler Label1.Click, AddressOf Clicks
        '
        'SimpleSortButton
        '
        BackColor = Color.White
        Controls.Add(Label1)
        Name = "SimpleSortButton"
        Size = New Size(26, 26)
        ResumeLayout(False)

    End Sub

#End Region

    Public Event SortClick()
    Private Sub Clicks(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SortClick()
    End Sub
End Class
