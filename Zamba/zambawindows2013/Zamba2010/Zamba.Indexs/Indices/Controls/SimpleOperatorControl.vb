Public Class SimpleOperatorControl
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal Index As Zamba.Core.Index)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Select Case Index.Type
            Case 1, 2, 3, 4, 6, 5
                If Index.DropDown = IndexAdditionalType.AutoSustitución _
                    Or Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                    Panel2.Visible = False
                    Panel1.Visible = True
                Else
                    Panel2.Visible = True
                    Panel1.Visible = False
                End If

            Case 7, 8
                Panel2.Visible = False
                Panel1.Visible = True
                '   combo.Items.Add("=")
                'SI TIENE TABLA DE SUSTITUCION NO AGREGA LAS SIGUIENTES OPCIONES
                '   If Index.DropDown = IndexAdditionalType.AutoSustitución Then Exit Sub
                '   combo.Items.Add("Contiene")
                '   combo.Items.Add("Empieza")
                '   combo.Items.Add("Alguno")
                '   combo.Text = "Contiene"
                '   combo.SelectedIndex = 0
        End Select
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                RemoveHandler MyBase.Load, AddressOf SimpleOperatorControl_Load

                If Panel1 IsNot Nothing Then
                    Panel1.Dispose()
                    Panel1 = Nothing
                End If
                If Panel2 IsNot Nothing Then
                    Panel2.Dispose()
                    Panel2 = Nothing
                End If
                If Igual1 IsNot Nothing Then
                    RemoveHandler Igual1.LinkClicked, AddressOf Igual1_LinkClicked
                    Igual1.Dispose()
                    Igual1 = Nothing
                End If
                If Mayor IsNot Nothing Then
                    RemoveHandler Mayor.LinkClicked, AddressOf Mayor_LinkClicked
                    Mayor.Dispose()
                    Mayor = Nothing
                End If
                If MayorIgual IsNot Nothing Then
                    RemoveHandler MayorIgual.LinkClicked, AddressOf MayorIgual_LinkClicked
                    MayorIgual.Dispose()
                    MayorIgual = Nothing
                End If
                If Menor IsNot Nothing Then
                    RemoveHandler Menor.LinkClicked, AddressOf Menor_LinkClicked
                    Menor.Dispose()
                    Menor = Nothing
                End If
                If MenorIgual IsNot Nothing Then
                    RemoveHandler MenorIgual.LinkClicked, AddressOf MenorIgual_LinkClicked
                    MenorIgual.Dispose()
                    MenorIgual = Nothing
                End If
                If Distinto IsNot Nothing Then
                    RemoveHandler Distinto.LinkClicked, AddressOf Distinto_LinkClicked
                    Distinto.Dispose()
                    Distinto = Nothing
                End If
                If Entre IsNot Nothing Then
                    RemoveHandler Entre.LinkClicked, AddressOf Entre_LinkClicked
                    Entre.Dispose()
                    Entre = Nothing
                End If
                If Igual IsNot Nothing Then
                    RemoveHandler Igual.LinkClicked, AddressOf Igual_LinkClicked
                    Igual.Dispose()
                    Igual = Nothing
                End If
                If Contiene IsNot Nothing Then
                    RemoveHandler Contiene.LinkClicked, AddressOf Contiene_LinkClicked
                    Contiene.Dispose()
                    Contiene = Nothing
                End If
                If Empieza IsNot Nothing Then
                    RemoveHandler Empieza.LinkClicked, AddressOf Empieza_LinkClicked
                    Empieza.Dispose()
                    Empieza = Nothing
                End If
                If Termina IsNot Nothing Then
                    RemoveHandler Termina.LinkClicked, AddressOf Termina_LinkClicked
                    Termina.Dispose()
                    Termina = Nothing
                End If
                If Alguno IsNot Nothing Then
                    RemoveHandler Alguno.LinkClicked, AddressOf Alguno_LinkClicked
                    Alguno.Dispose()
                    Alguno = Nothing
                End If
                If EsNulo IsNot Nothing Then
                    RemoveHandler EsNulo.LinkClicked, AddressOf EsNulo_LinkClicked
                    EsNulo.Dispose()
                    EsNulo = Nothing
                End If
                If Diferente IsNot Nothing Then
                    RemoveHandler Diferente.LinkClicked, AddressOf Diferente_LinkClicked
                    Diferente.Dispose()
                    Diferente = Nothing
                End If
                If Esnulo1 IsNot Nothing Then
                    RemoveHandler Esnulo1.LinkClicked, AddressOf Esnulo1_LinkClicked
                    Esnulo1.Dispose()
                    Esnulo1 = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Igual1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Mayor As System.Windows.Forms.LinkLabel
    Friend WithEvents MayorIgual As System.Windows.Forms.LinkLabel
    Friend WithEvents Menor As System.Windows.Forms.LinkLabel
    Friend WithEvents MenorIgual As System.Windows.Forms.LinkLabel
    Friend WithEvents Distinto As System.Windows.Forms.LinkLabel
    Friend WithEvents Entre As System.Windows.Forms.LinkLabel
    Friend WithEvents Igual As System.Windows.Forms.LinkLabel
    Friend WithEvents Contiene As System.Windows.Forms.LinkLabel
    Friend WithEvents Empieza As System.Windows.Forms.LinkLabel
    Friend WithEvents Termina As System.Windows.Forms.LinkLabel
    Friend WithEvents Alguno As System.Windows.Forms.LinkLabel
    Friend WithEvents EsNulo As System.Windows.Forms.LinkLabel
    Friend WithEvents Diferente As System.Windows.Forms.LinkLabel
    Friend WithEvents Esnulo1 As System.Windows.Forms.LinkLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel1 = New System.Windows.Forms.Panel()
        Igual = New System.Windows.Forms.LinkLabel()
        Esnulo1 = New System.Windows.Forms.LinkLabel()
        Alguno = New System.Windows.Forms.LinkLabel()
        Diferente = New System.Windows.Forms.LinkLabel()
        Contiene = New System.Windows.Forms.LinkLabel()
        Termina = New System.Windows.Forms.LinkLabel()
        Empieza = New System.Windows.Forms.LinkLabel()
        Panel2 = New System.Windows.Forms.Panel()
        EsNulo = New System.Windows.Forms.LinkLabel()
        Entre = New System.Windows.Forms.LinkLabel()
        Distinto = New System.Windows.Forms.LinkLabel()
        MenorIgual = New System.Windows.Forms.LinkLabel()
        Menor = New System.Windows.Forms.LinkLabel()
        MayorIgual = New System.Windows.Forms.LinkLabel()
        Mayor = New System.Windows.Forms.LinkLabel()
        Igual1 = New System.Windows.Forms.LinkLabel()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.BackColor = Color.White
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(Igual)
        Panel1.Controls.Add(Esnulo1)
        Panel1.Controls.Add(Alguno)
        Panel1.Controls.Add(Diferente)
        Panel1.Controls.Add(Contiene)
        Panel1.Controls.Add(Termina)
        Panel1.Controls.Add(Empieza)
        Panel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Panel1.ForeColor = Color.FromArgb(76, 76, 76)
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(96, 192)
        Panel1.TabIndex = 0
        '
        'Igual
        '
        Igual.Dock = DockStyle.Top
        Igual.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Igual.ForeColor = Color.FromArgb(76, 76, 76)
        Igual.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Igual.Location = New Point(0, 138)
        Igual.Name = "Igual"
        Igual.Size = New Size(94, 23)
        Igual.TabIndex = 0
        Igual.TabStop = True
        Igual.Text = "="
        Igual.TextAlign = ContentAlignment.MiddleCenter
        '
        'Esnulo1
        '
        Esnulo1.Dock = DockStyle.Top
        Esnulo1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Esnulo1.ForeColor = Color.FromArgb(76, 76, 76)
        Esnulo1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Esnulo1.Location = New Point(0, 115)
        Esnulo1.Name = "Esnulo1"
        Esnulo1.Size = New Size(94, 23)
        Esnulo1.TabIndex = 5
        Esnulo1.TabStop = True
        Esnulo1.Text = "Es Nulo"
        Esnulo1.TextAlign = ContentAlignment.MiddleCenter
        '
        'Alguno
        '
        Alguno.Dock = DockStyle.Top
        Alguno.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Alguno.ForeColor = Color.FromArgb(76, 76, 76)
        Alguno.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Alguno.Location = New Point(0, 92)
        Alguno.Name = "Alguno"
        Alguno.Size = New Size(94, 23)
        Alguno.TabIndex = 4
        Alguno.TabStop = True
        Alguno.Text = "Alguno"
        Alguno.TextAlign = ContentAlignment.MiddleCenter
        '
        'Diferente
        '
        Diferente.Dock = DockStyle.Top
        Diferente.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Diferente.ForeColor = Color.FromArgb(76, 76, 76)
        Diferente.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Diferente.Location = New Point(0, 69)
        Diferente.Name = "Diferente"
        Diferente.Size = New Size(94, 23)
        Diferente.TabIndex = 6
        Diferente.TabStop = True
        Diferente.Text = "Distinto"
        Diferente.TextAlign = ContentAlignment.MiddleCenter
        '
        'Contiene
        '
        Contiene.Dock = DockStyle.Top
        Contiene.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Contiene.ForeColor = Color.FromArgb(76, 76, 76)
        Contiene.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Contiene.Location = New Point(0, 46)
        Contiene.Name = "Contiene"
        Contiene.Size = New Size(94, 23)
        Contiene.TabIndex = 1
        Contiene.TabStop = True
        Contiene.Text = "Contiene"
        Contiene.TextAlign = ContentAlignment.MiddleCenter
        '
        'Termina
        '
        Termina.Dock = DockStyle.Top
        Termina.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Termina.ForeColor = Color.FromArgb(76, 76, 76)
        Termina.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Termina.Location = New Point(0, 23)
        Termina.Name = "Termina"
        Termina.Size = New Size(94, 23)
        Termina.TabIndex = 3
        Termina.TabStop = True
        Termina.Text = "Termina"
        Termina.TextAlign = ContentAlignment.MiddleCenter
        '
        'Empieza
        '
        Empieza.Dock = DockStyle.Top
        Empieza.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Empieza.ForeColor = Color.FromArgb(76, 76, 76)
        Empieza.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Empieza.Location = New Point(0, 0)
        Empieza.Name = "Empieza"
        Empieza.Size = New Size(94, 23)
        Empieza.TabIndex = 2
        Empieza.TabStop = True
        Empieza.Text = "Empieza"
        Empieza.TextAlign = ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Panel2.BackColor = Color.White
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel2.Controls.Add(EsNulo)
        Panel2.Controls.Add(Entre)
        Panel2.Controls.Add(Distinto)
        Panel2.Controls.Add(MenorIgual)
        Panel2.Controls.Add(Menor)
        Panel2.Controls.Add(MayorIgual)
        Panel2.Controls.Add(Mayor)
        Panel2.Controls.Add(Igual1)
        Panel2.Dock = DockStyle.Fill
        Panel2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Panel2.ForeColor = Color.FromArgb(76, 76, 76)
        Panel2.Location = New Point(2, 2)
        Panel2.Name = "Panel2"
        Panel2.Padding = New System.Windows.Forms.Padding(4)
        Panel2.Size = New Size(116, 188)
        Panel2.TabIndex = 1
        '
        'EsNulo
        '
        EsNulo.Dock = DockStyle.Top
        EsNulo.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        EsNulo.ForeColor = Color.Blue
        EsNulo.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        EsNulo.LinkColor = Color.Blue
        EsNulo.Location = New Point(4, 165)
        EsNulo.Name = "EsNulo"
        EsNulo.Size = New Size(106, 23)
        EsNulo.TabIndex = 7
        EsNulo.TabStop = True
        EsNulo.Text = "Es nulo"
        EsNulo.TextAlign = ContentAlignment.MiddleCenter
        '
        'Entre
        '
        Entre.Dock = DockStyle.Top
        Entre.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Entre.ForeColor = Color.Blue
        Entre.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Entre.LinkColor = Color.Blue
        Entre.Location = New Point(4, 142)
        Entre.Name = "Entre"
        Entre.Size = New Size(106, 23)
        Entre.TabIndex = 6
        Entre.TabStop = True
        Entre.Text = "Entre"
        Entre.TextAlign = ContentAlignment.MiddleCenter
        '
        'Distinto
        '
        Distinto.Dock = DockStyle.Top
        Distinto.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Distinto.ForeColor = Color.Blue
        Distinto.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Distinto.LinkColor = Color.Blue
        Distinto.Location = New Point(4, 119)
        Distinto.Name = "Distinto"
        Distinto.Size = New Size(106, 23)
        Distinto.TabIndex = 5
        Distinto.TabStop = True
        Distinto.Text = "<>"
        Distinto.TextAlign = ContentAlignment.MiddleCenter
        '
        'MenorIgual
        '
        MenorIgual.Dock = DockStyle.Top
        MenorIgual.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        MenorIgual.ForeColor = Color.Blue
        MenorIgual.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        MenorIgual.LinkColor = Color.Blue
        MenorIgual.Location = New Point(4, 96)
        MenorIgual.Name = "MenorIgual"
        MenorIgual.Size = New Size(106, 23)
        MenorIgual.TabIndex = 4
        MenorIgual.TabStop = True
        MenorIgual.Text = "<="
        MenorIgual.TextAlign = ContentAlignment.MiddleCenter
        '
        'Menor
        '
        Menor.Dock = DockStyle.Top
        Menor.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Menor.ForeColor = Color.Blue
        Menor.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Menor.LinkColor = Color.Blue
        Menor.Location = New Point(4, 73)
        Menor.Name = "Menor"
        Menor.Size = New Size(106, 23)
        Menor.TabIndex = 3
        Menor.TabStop = True
        Menor.Text = "<"
        Menor.TextAlign = ContentAlignment.MiddleCenter
        '
        'MayorIgual
        '
        MayorIgual.Dock = DockStyle.Top
        MayorIgual.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        MayorIgual.ForeColor = Color.Blue
        MayorIgual.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        MayorIgual.LinkColor = Color.Blue
        MayorIgual.Location = New Point(4, 50)
        MayorIgual.Name = "MayorIgual"
        MayorIgual.Size = New Size(106, 23)
        MayorIgual.TabIndex = 2
        MayorIgual.TabStop = True
        MayorIgual.Text = ">="
        MayorIgual.TextAlign = ContentAlignment.MiddleCenter
        '
        'Mayor
        '
        Mayor.Dock = DockStyle.Top
        Mayor.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Mayor.ForeColor = Color.Blue
        Mayor.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Mayor.LinkColor = Color.Blue
        Mayor.Location = New Point(4, 27)
        Mayor.Name = "Mayor"
        Mayor.Size = New Size(106, 23)
        Mayor.TabIndex = 1
        Mayor.TabStop = True
        Mayor.Text = ">"
        Mayor.TextAlign = ContentAlignment.MiddleCenter
        '
        'Igual1
        '
        Igual1.Dock = DockStyle.Top
        Igual1.Font = New Font("Verdana", 9.0!, FontStyle.Underline, GraphicsUnit.Point, 0)
        Igual1.ForeColor = Color.Blue
        Igual1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Igual1.LinkColor = Color.Blue
        Igual1.Location = New Point(4, 4)
        Igual1.Name = "Igual1"
        Igual1.Size = New Size(106, 23)
        Igual1.TabIndex = 0
        Igual1.TabStop = True
        Igual1.Text = "="
        Igual1.TextAlign = ContentAlignment.MiddleCenter
        '
        'SimpleOperatorControl
        '
        AutoScaleBaseSize = New Size(6, 14)
        BackColor = Color.FromArgb(241, 241, 241)
        ClientSize = New Size(120, 192)
        Controls.Add(Panel1)
        Controls.Add(Panel2)
        MyBase.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
        FormBorderStyle = FormBorderStyle.None
        Location = New Point(0, 0)
        MaximizeBox = False
        MinimizeBox = False
        Name = "SimpleOperatorControl"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public [Operator] As String
    Private Sub Igual1_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Igual1.LinkClicked
        Try
            [Operator] = "="
            Close()
        Catch
        End Try
    End Sub
    Private Sub Mayor_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Mayor.LinkClicked
        Try
            [Operator] = ">"
            Close()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MayorIgual_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles MayorIgual.LinkClicked
        Try
            [Operator] = ">="
            Close()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Menor_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Menor.LinkClicked
        Try
            [Operator] = "<"
            Close()
        Catch
        End Try
    End Sub
    Private Sub MenorIgual_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles MenorIgual.LinkClicked
        Try
            [Operator] = "<="
            Close()
        Catch
        End Try
    End Sub
    Private Sub Distinto_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Distinto.LinkClicked
        Try
            [Operator] = "<>"
            Close()
        Catch
        End Try
    End Sub
    Private Sub Entre_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Entre.LinkClicked
        Try
            [Operator] = "Entre"
            Close()
        Catch
        End Try
    End Sub
    Private Sub Igual_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Igual.LinkClicked
        Try
            [Operator] = "="
            Close()
        Catch
        End Try
    End Sub
    Private Sub Contiene_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Contiene.LinkClicked
        Try
            [Operator] = "Contiene"
            Close()
        Catch
        End Try
    End Sub
    Private Sub Empieza_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Empieza.LinkClicked
        Try
            [Operator] = "Empieza"
            Close()
        Catch
        End Try
    End Sub
    Private Sub Termina_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Termina.LinkClicked
        Try
            [Operator] = "Termina"
            Close()
        Catch
        End Try
    End Sub
    Private Sub Alguno_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Alguno.LinkClicked
        Try
            [Operator] = "Alguno"
            Close()
        Catch
        End Try
    End Sub
    Private Sub EsNulo_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles EsNulo.LinkClicked
        Try
            [Operator] = "Es nulo"
            Close()
        Catch
        End Try
    End Sub

    Private Sub SimpleOperatorControl_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Select Case [Operator]
            Case "Alguno"
                Alguno.LinkColor = Color.Blue
            Case "Termina"
                Termina.LinkColor = Color.Blue
            Case "Empieza"
                Empieza.LinkColor = Color.Blue
            Case "Contiene"
                Contiene.LinkColor = Color.Blue
            Case "Diferente"
                Diferente.LinkColor = Color.Blue
            Case "="
                Igual.LinkColor = Color.Blue
                Igual1.LinkColor = Color.Blue
            Case "Entre"
                Entre.LinkColor = Color.Blue
            Case "<>"
                Distinto.LinkColor = Color.Blue
            Case "<="
                MenorIgual.LinkColor = Color.Blue
            Case "<"
                Mayor.LinkColor = Color.Blue
            Case ">="
                MayorIgual.LinkColor = Color.Blue
            Case ">"
                Mayor.LinkColor = Color.Blue
        End Select
    End Sub

    Private Sub Esnulo1_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Esnulo1.LinkClicked
        Try
            [Operator] = "Es nulo"
            Close()
        Catch
        End Try
    End Sub

    Private Sub Diferente_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles Diferente.LinkClicked
        Try
            [Operator] = "Distinto"
            Close()
        Catch
        End Try
    End Sub

    Private Sub Panel1_Leave(sender As Object, e As EventArgs) Handles Panel1.Leave
        Close()
    End Sub

    Private Sub SimpleOperatorControl_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        Close()
    End Sub

End Class
