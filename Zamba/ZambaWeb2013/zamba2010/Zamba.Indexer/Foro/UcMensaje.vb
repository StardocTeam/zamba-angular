Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Foro.Components
Public Class UcMensaje
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
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    'Friend WithEvents txtusuario As System.Windows.Forms.Label
    'Friend WithEvents txtfecha As System.Windows.Forms.Label
    Friend WithEvents txtMensaje As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtMensaje = New System.Windows.Forms.TextBox
        'Me.txtusuario = New System.Windows.Forms.Label
        'Me.txtfecha = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.txtMensaje)
        'Me.Panel1.Controls.Add(Me.txtusuario)
        'Me.Panel1.Controls.Add(Me.txtfecha)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel1.Size = New System.Drawing.Size(331, 172)
        Me.Panel1.TabIndex = 10
        '
        'txtMensaje
        '
        Me.txtMensaje.BackColor = System.Drawing.Color.White
        Me.txtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMensaje.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMensaje.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMensaje.ForeColor = System.Drawing.Color.RoyalBlue
        Me.txtMensaje.Location = New System.Drawing.Point(10, 36)
        Me.txtMensaje.Multiline = True
        Me.txtMensaje.Name = "txtMensaje"
        Me.txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMensaje.Size = New System.Drawing.Size(311, 126)
        Me.txtMensaje.TabIndex = 20
        ''
        ''txtusuario
        ''
        'Me.txtusuario.AutoSize = True
        'Me.txtusuario.Dock = System.Windows.Forms.DockStyle.Top
        'Me.txtusuario.Location = New System.Drawing.Point(10, 23)
        'Me.txtusuario.Name = "txtusuario"
        'Me.txtusuario.Size = New System.Drawing.Size(0, 13)
        'Me.txtusuario.TabIndex = 23
        ''
        ''txtfecha
        ''
        'Me.txtfecha.AutoSize = True
        'Me.txtfecha.Dock = System.Windows.Forms.DockStyle.Top
        'Me.txtfecha.Location = New System.Drawing.Point(10, 10)
        'Me.txtfecha.Name = "txtfecha"
        'Me.txtfecha.Size = New System.Drawing.Size(0, 13)
        'Me.txtfecha.TabIndex = 22
        '
        'UcMensaje
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UcMensaje"
        Me.Size = New System.Drawing.Size(331, 172)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub FillMensaje(ByVal Mensaje As MensajeForo)
        Me.txtMensaje.Text = Mensaje.Mensaje
        'Me.txtUsuario.Text = "Usuario: " & Mensaje.UserName
        'Me.txtfecha.Text = Mensaje.Fecha & " " & Mensaje.Name
    End Sub

    Public Sub Bloquear(ByVal Estado As Boolean)
        Me.txtMensaje.ReadOnly = Estado
        'Me.txtAsunto.ReadOnly = Estado

        If Estado = False Then
            'Me.txtAsunto.Focus()
            Me.txtMensaje.Text = ""
            'Me.txtfecha.Text = ""
            'Me.txtUsuario.Text = ""
        End If
    End Sub

    'Public Shared Sub CompletarAsunto(ByVal Asunto As String)
    '    'Me.txtAsunto.Text = "Re: " & Asunto
    'End Sub

    'Private Sub txtFecha_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'If Me.txtFecha.Text = "-" Then
    '    'Me.txtFecha.Visible = False
    '    'Else
    '    'Me.txtFecha.Visible = True
    '    'End If
    'End Sub
End Class
