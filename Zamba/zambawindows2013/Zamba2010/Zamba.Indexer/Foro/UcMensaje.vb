Imports Zamba.Core
Public Class UcMensaje
    Inherits Zamba.AppBlock.ZControl
    Implements IDisposable

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Not (components Is Nothing) Then components.Dispose()
                If Panel1 IsNot Nothing Then
                    Panel1.Controls.Clear()
                    Panel1.Dispose()
                End If
                If TextBox1 IsNot Nothing Then TextBox1.Dispose()
                If txtMensaje IsNot Nothing Then txtMensaje.Dispose()
            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    'Friend WithEvents txtusuario As ZLabel
    'Friend WithEvents txtfecha As ZLabel
    Friend WithEvents txtMensaje As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel1 = New System.Windows.Forms.Panel
        txtMensaje = New TextBox
        'Me.txtusuario = New ZLabel
        'Me.txtfecha = New ZLabel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.AutoScroll = True
        Panel1.BackColor = System.Drawing.Color.White
        Panel1.Controls.Add(txtMensaje)
        'Me.Panel1.Controls.Add(Me.txtusuario)
        'Me.Panel1.Controls.Add(Me.txtfecha)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Padding = New System.Windows.Forms.Padding(10)
        Panel1.Size = New System.Drawing.Size(331, 172)
        Panel1.TabIndex = 10
        '
        'txtMensaje
        '
        txtMensaje.BackColor = System.Drawing.Color.White
        txtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.None
        txtMensaje.Dock = System.Windows.Forms.DockStyle.Fill
        txtMensaje.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtMensaje.ForeColor = System.Drawing.Color.RoyalBlue
        txtMensaje.Location = New System.Drawing.Point(10, 36)
        txtMensaje.Multiline = True
        txtMensaje.Name = "txtMensaje"
        txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        txtMensaje.Size = New System.Drawing.Size(311, 126)
        txtMensaje.TabIndex = 20
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
        BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Controls.Add(Panel1)
        Name = "UcMensaje"
        Size = New System.Drawing.Size(331, 172)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub FillMensaje(ByVal Mensaje As MensajeForo)
        txtMensaje.Text = Mensaje.Mensaje
        'Me.txtUsuario.Text = "Usuario: " & Mensaje.UserName
        'Me.txtfecha.Text = Mensaje.Fecha & " " & Mensaje.Name
    End Sub

    Public Sub Bloquear(ByVal Estado As Boolean)
        txtMensaje.ReadOnly = Estado
        'Me.txtAsunto.ReadOnly = Estado

        If Estado = False Then
            'Me.txtAsunto.Focus()
            txtMensaje.Text = ""
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
