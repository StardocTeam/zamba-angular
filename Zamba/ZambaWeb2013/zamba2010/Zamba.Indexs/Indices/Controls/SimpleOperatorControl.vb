Imports Zamba.AppBlock
Public Class SimpleOperatorControl
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal Index As Zamba.Core.Index)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Select Case CInt(Index.Type)
            Case 1, 2, 3, 4, 6, 5
                If Index.DropDown = Core.IndexAdditionalType.AutoSustitución Then
                    Me.Panel2.Visible = False
                    Me.Panel1.Visible = True
                Else
                    Me.Panel2.Visible = True
                    Me.Panel1.Visible = False
                End If
              
            Case 7, 8
                Me.Panel2.Visible = False
                Me.Panel1.Visible = True
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Igual = New System.Windows.Forms.LinkLabel
        Me.Esnulo1 = New System.Windows.Forms.LinkLabel
        Me.Alguno = New System.Windows.Forms.LinkLabel
        Me.Termina = New System.Windows.Forms.LinkLabel
        Me.Empieza = New System.Windows.Forms.LinkLabel
        Me.Contiene = New System.Windows.Forms.LinkLabel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.EsNulo = New System.Windows.Forms.LinkLabel
        Me.Entre = New System.Windows.Forms.LinkLabel
        Me.Distinto = New System.Windows.Forms.LinkLabel
        Me.MenorIgual = New System.Windows.Forms.LinkLabel
        Me.Menor = New System.Windows.Forms.LinkLabel
        Me.MayorIgual = New System.Windows.Forms.LinkLabel
        Me.Mayor = New System.Windows.Forms.LinkLabel
        Me.Igual1 = New System.Windows.Forms.LinkLabel
        Me.Diferente = New System.Windows.Forms.LinkLabel
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Igual)
        Me.Panel1.Controls.Add(Me.Esnulo1)
        Me.Panel1.Controls.Add(Me.Alguno)
        Me.Panel1.Controls.Add(Me.Diferente)
        Me.Panel1.Controls.Add(Me.Contiene)
        Me.Panel1.Controls.Add(Me.Termina)
        Me.Panel1.Controls.Add(Me.Empieza)
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(96, 192)
        Me.Panel1.TabIndex = 0
        '
        'Igual
        '
        Me.Igual.Dock = System.Windows.Forms.DockStyle.Top
        Me.Igual.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Igual.ForeColor = System.Drawing.Color.Black
        Me.Igual.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Igual.Location = New System.Drawing.Point(0, 138)
        Me.Igual.Name = "Igual"
        Me.Igual.Size = New System.Drawing.Size(94, 23)
        Me.Igual.TabIndex = 0
        Me.Igual.TabStop = True
        Me.Igual.Text = "="
        Me.Igual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Esnulo1
        '
        Me.Esnulo1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Esnulo1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Esnulo1.ForeColor = System.Drawing.Color.Black
        Me.Esnulo1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Esnulo1.Location = New System.Drawing.Point(0, 115)
        Me.Esnulo1.Name = "Esnulo1"
        Me.Esnulo1.Size = New System.Drawing.Size(94, 23)
        Me.Esnulo1.TabIndex = 5
        Me.Esnulo1.TabStop = True
        Me.Esnulo1.Text = "Es Nulo"
        Me.Esnulo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Alguno
        '
        Me.Alguno.Dock = System.Windows.Forms.DockStyle.Top
        Me.Alguno.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Alguno.ForeColor = System.Drawing.Color.Black
        Me.Alguno.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Alguno.Location = New System.Drawing.Point(0, 92)
        Me.Alguno.Name = "Alguno"
        Me.Alguno.Size = New System.Drawing.Size(94, 23)
        Me.Alguno.TabIndex = 4
        Me.Alguno.TabStop = True
        Me.Alguno.Text = "Alguno"
        Me.Alguno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Termina
        '
        Me.Termina.Dock = System.Windows.Forms.DockStyle.Top
        Me.Termina.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Termina.ForeColor = System.Drawing.Color.Black
        Me.Termina.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Termina.Location = New System.Drawing.Point(0, 23)
        Me.Termina.Name = "Termina"
        Me.Termina.Size = New System.Drawing.Size(94, 23)
        Me.Termina.TabIndex = 3
        Me.Termina.TabStop = True
        Me.Termina.Text = "Termina"
        Me.Termina.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Empieza
        '
        Me.Empieza.Dock = System.Windows.Forms.DockStyle.Top
        Me.Empieza.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Empieza.ForeColor = System.Drawing.Color.Black
        Me.Empieza.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Empieza.Location = New System.Drawing.Point(0, 0)
        Me.Empieza.Name = "Empieza"
        Me.Empieza.Size = New System.Drawing.Size(94, 23)
        Me.Empieza.TabIndex = 2
        Me.Empieza.TabStop = True
        Me.Empieza.Text = "Empieza"
        Me.Empieza.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Contiene
        '
        Me.Contiene.Dock = System.Windows.Forms.DockStyle.Top
        Me.Contiene.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Contiene.ForeColor = System.Drawing.Color.Black
        Me.Contiene.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Contiene.Location = New System.Drawing.Point(0, 46)
        Me.Contiene.Name = "Contiene"
        Me.Contiene.Size = New System.Drawing.Size(94, 23)
        Me.Contiene.TabIndex = 1
        Me.Contiene.TabStop = True
        Me.Contiene.Text = "Contiene"
        Me.Contiene.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.EsNulo)
        Me.Panel2.Controls.Add(Me.Entre)
        Me.Panel2.Controls.Add(Me.Distinto)
        Me.Panel2.Controls.Add(Me.MenorIgual)
        Me.Panel2.Controls.Add(Me.Menor)
        Me.Panel2.Controls.Add(Me.MayorIgual)
        Me.Panel2.Controls.Add(Me.Mayor)
        Me.Panel2.Controls.Add(Me.Igual1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.Black
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(4)
        Me.Panel2.Size = New System.Drawing.Size(104, 192)
        Me.Panel2.TabIndex = 1
        '
        'EsNulo
        '
        Me.EsNulo.Dock = System.Windows.Forms.DockStyle.Top
        Me.EsNulo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EsNulo.ForeColor = System.Drawing.Color.Blue
        Me.EsNulo.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.EsNulo.LinkColor = System.Drawing.Color.Blue
        Me.EsNulo.Location = New System.Drawing.Point(4, 165)
        Me.EsNulo.Name = "EsNulo"
        Me.EsNulo.Size = New System.Drawing.Size(94, 23)
        Me.EsNulo.TabIndex = 7
        Me.EsNulo.TabStop = True
        Me.EsNulo.Text = "Es nulo"
        Me.EsNulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Entre
        '
        Me.Entre.Dock = System.Windows.Forms.DockStyle.Top
        Me.Entre.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Entre.ForeColor = System.Drawing.Color.Blue
        Me.Entre.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Entre.LinkColor = System.Drawing.Color.Blue
        Me.Entre.Location = New System.Drawing.Point(4, 142)
        Me.Entre.Name = "Entre"
        Me.Entre.Size = New System.Drawing.Size(94, 23)
        Me.Entre.TabIndex = 6
        Me.Entre.TabStop = True
        Me.Entre.Text = "Entre"
        Me.Entre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Distinto
        '
        Me.Distinto.Dock = System.Windows.Forms.DockStyle.Top
        Me.Distinto.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Distinto.ForeColor = System.Drawing.Color.Blue
        Me.Distinto.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Distinto.LinkColor = System.Drawing.Color.Blue
        Me.Distinto.Location = New System.Drawing.Point(4, 119)
        Me.Distinto.Name = "Distinto"
        Me.Distinto.Size = New System.Drawing.Size(94, 23)
        Me.Distinto.TabIndex = 5
        Me.Distinto.TabStop = True
        Me.Distinto.Text = "<>"
        Me.Distinto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MenorIgual
        '
        Me.MenorIgual.Dock = System.Windows.Forms.DockStyle.Top
        Me.MenorIgual.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenorIgual.ForeColor = System.Drawing.Color.Blue
        Me.MenorIgual.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.MenorIgual.LinkColor = System.Drawing.Color.Blue
        Me.MenorIgual.Location = New System.Drawing.Point(4, 96)
        Me.MenorIgual.Name = "MenorIgual"
        Me.MenorIgual.Size = New System.Drawing.Size(94, 23)
        Me.MenorIgual.TabIndex = 4
        Me.MenorIgual.TabStop = True
        Me.MenorIgual.Text = "<="
        Me.MenorIgual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Menor
        '
        Me.Menor.Dock = System.Windows.Forms.DockStyle.Top
        Me.Menor.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Menor.ForeColor = System.Drawing.Color.Blue
        Me.Menor.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Menor.LinkColor = System.Drawing.Color.Blue
        Me.Menor.Location = New System.Drawing.Point(4, 73)
        Me.Menor.Name = "Menor"
        Me.Menor.Size = New System.Drawing.Size(94, 23)
        Me.Menor.TabIndex = 3
        Me.Menor.TabStop = True
        Me.Menor.Text = "<"
        Me.Menor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MayorIgual
        '
        Me.MayorIgual.Dock = System.Windows.Forms.DockStyle.Top
        Me.MayorIgual.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MayorIgual.ForeColor = System.Drawing.Color.Blue
        Me.MayorIgual.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.MayorIgual.LinkColor = System.Drawing.Color.Blue
        Me.MayorIgual.Location = New System.Drawing.Point(4, 50)
        Me.MayorIgual.Name = "MayorIgual"
        Me.MayorIgual.Size = New System.Drawing.Size(94, 23)
        Me.MayorIgual.TabIndex = 2
        Me.MayorIgual.TabStop = True
        Me.MayorIgual.Text = ">="
        Me.MayorIgual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Mayor
        '
        Me.Mayor.Dock = System.Windows.Forms.DockStyle.Top
        Me.Mayor.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Mayor.ForeColor = System.Drawing.Color.Blue
        Me.Mayor.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Mayor.LinkColor = System.Drawing.Color.Blue
        Me.Mayor.Location = New System.Drawing.Point(4, 27)
        Me.Mayor.Name = "Mayor"
        Me.Mayor.Size = New System.Drawing.Size(94, 23)
        Me.Mayor.TabIndex = 1
        Me.Mayor.TabStop = True
        Me.Mayor.Text = ">"
        Me.Mayor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Igual1
        '
        Me.Igual1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Igual1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Igual1.ForeColor = System.Drawing.Color.Blue
        Me.Igual1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Igual1.LinkColor = System.Drawing.Color.Blue
        Me.Igual1.Location = New System.Drawing.Point(4, 4)
        Me.Igual1.Name = "Igual1"
        Me.Igual1.Size = New System.Drawing.Size(94, 23)
        Me.Igual1.TabIndex = 0
        Me.Igual1.TabStop = True
        Me.Igual1.Text = "="
        Me.Igual1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Diferente
        '
        Me.Diferente.Dock = System.Windows.Forms.DockStyle.Top
        Me.Diferente.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Diferente.ForeColor = System.Drawing.Color.Black
        Me.Diferente.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Diferente.Location = New System.Drawing.Point(0, 69)
        Me.Diferente.Name = "Diferente"
        Me.Diferente.Size = New System.Drawing.Size(94, 23)
        Me.Diferente.TabIndex = 6
        Me.Diferente.TabStop = True
        Me.Diferente.Text = "Distinto"
        Me.Diferente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SimpleOperatorControl
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(104, 192)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SimpleOperatorControl"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public [Operator] As String
    Private Sub Igual1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Igual1.LinkClicked
        Try
            Me.[Operator] = "="
            Me.Close()
        Catch
        End Try
    End Sub
    Private Sub Mayor_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Mayor.LinkClicked
        Try
            Me.[Operator] = ">"
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MayorIgual_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles MayorIgual.LinkClicked
        Try
            Me.[Operator] = ">="
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Menor_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Menor.LinkClicked
        Try
            Me.[Operator] = "<"
            Me.Close()
        Catch
        End Try
    End Sub
    Private Sub MenorIgual_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles MenorIgual.LinkClicked
        Try
            Me.[Operator] = "<="
            Me.Close()
        Catch
        End Try
    End Sub
    Private Sub Distinto_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Distinto.LinkClicked
        Try
            Me.[Operator] = "<>"
            Me.Close()
        Catch
        End Try
    End Sub
    Private Sub Entre_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Entre.LinkClicked
        Try
            Me.[Operator] = "Entre"
            Me.Close()
        Catch
        End Try
    End Sub
    Private Sub Igual_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Igual.LinkClicked
        Try
            Me.[Operator] = "="
            Me.Close()
        Catch
        End Try
    End Sub
    Private Sub Contiene_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Contiene.LinkClicked
        Try
            Me.[Operator] = "Contiene"
            Me.Close()
        Catch
        End Try
    End Sub
    Private Sub Empieza_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Empieza.LinkClicked
        Try
            Me.[Operator] = "Empieza"
            Me.Close()
        Catch
        End Try
    End Sub
    Private Sub Termina_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Termina.LinkClicked
        Try
            Me.[Operator] = "Termina"
            Me.Close()
        Catch
        End Try
    End Sub
    Private Sub Alguno_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Alguno.LinkClicked
        Try
            Me.[Operator] = "Alguno"
            Me.Close()
        Catch
        End Try
    End Sub
    Private Sub EsNulo_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles EsNulo.LinkClicked
        Try
            Me.[Operator] = "Es nulo"
            Me.Close()
        Catch
        End Try
    End Sub

    Private Sub SimpleOperatorControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case Me.[Operator]
            Case "Alguno"
                Me.Alguno.LinkColor = Color.Blue
            Case "Termina"
                Me.Termina.LinkColor = Color.Blue
            Case "Empieza"
                Me.Empieza.LinkColor = Color.Blue
            Case "Contiene"
                Me.Contiene.LinkColor = Color.Blue
            Case "Diferente"
                Me.Diferente.LinkColor = Color.Blue
            Case "="
                Me.Igual.LinkColor = Color.Blue
                Me.Igual1.LinkColor = Color.Blue
            Case "Entre"
                Me.Entre.LinkColor = Color.Blue
            Case "<>"
                Me.Distinto.LinkColor = Color.Blue
            Case "<="
                Me.MenorIgual.LinkColor = Color.Blue
            Case "<"
                Me.Mayor.LinkColor = Color.Blue
            Case ">="
                Me.MayorIgual.LinkColor = Color.Blue
            Case ">"
                Me.Mayor.LinkColor = Color.Blue
        End Select
    End Sub

    Private Sub Esnulo1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Esnulo1.LinkClicked
        Try
            Me.[Operator] = "Es nulo"
            Me.Close()
        Catch
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Diferente.LinkClicked
        Try
            Me.[Operator] = "Distinto"
            Me.Close()
        Catch
        End Try
    End Sub
End Class
