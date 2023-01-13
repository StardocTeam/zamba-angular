Public Class frmPrintPreView
    Inherits Zamba.AppBlock.ZForm

    Private m_estado As EstadosDelFormulario

    Public Enum EstadosDelFormulario
        IMPRIMIR
        CERRAR
    End Enum

    Public ReadOnly Property estado() As EstadosDelFormulario
        Get
            Return m_estado
        End Get
    End Property

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByRef doc As Printing.PrintDocument)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

        cmdCerrar.Location = New System.Drawing.Point(CInt(cmdCerrar.Parent.Width / 2 - cmdCerrar.Width / 2), CInt(cmdCerrar.Parent.Height / 2 - cmdCerrar.Height / 2))

        Panel1.ResumeLayout(True)
        PrintPreviewControl1.Document = doc
        PrintPreviewControl1.AutoZoom = True
        PrintPreviewControl1.Zoom = 1.0
        ResumeLayout(True)
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmdCerrar As ZButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PrintPreviewControl1 As System.Windows.Forms.PrintPreviewControl
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPrintPreView))
        Panel1 = New System.Windows.Forms.Panel
        cmdCerrar = New ZButton
        Panel2 = New System.Windows.Forms.Panel
        PrintPreviewControl1 = New System.Windows.Forms.PrintPreviewControl
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.Controls.Add(cmdCerrar)
        Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel1.Location = New System.Drawing.Point(0, 406)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(632, 32)
        Panel1.TabIndex = 1
        '
        'cmdCerrar
        '
        cmdCerrar.Location = New System.Drawing.Point(224, 8)
        cmdCerrar.Name = "cmdCerrar"
        cmdCerrar.TabIndex = 0
        cmdCerrar.Text = "&Cerrar"
        '
        'Panel2
        '
        Panel2.Controls.Add(PrintPreviewControl1)
        Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Panel2.Location = New System.Drawing.Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(632, 406)
        Panel2.TabIndex = 2
        '
        'PrintPreviewControl1
        '
        PrintPreviewControl1.AutoZoom = False
        PrintPreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill
        PrintPreviewControl1.Location = New System.Drawing.Point(0, 0)
        PrintPreviewControl1.Name = "PrintPreviewControl1"
        PrintPreviewControl1.Size = New System.Drawing.Size(632, 406)
        PrintPreviewControl1.TabIndex = 1
        PrintPreviewControl1.Zoom = 0.3
        '
        'frmPrintPreView
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        ClientSize = New System.Drawing.Size(632, 438)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Name = "frmPrintPreView"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "frmPrintPreView"
        WindowState = System.Windows.Forms.FormWindowState.Maximized
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Private Sub cerrar_click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCerrar.Click, MyBase.Closed
        Hide()
    End Sub

    Private Sub redimensionar(ByVal sender As Object, ByVal e As EventArgs) Handles Panel1.Resize
        Panel1.SuspendLayout()
        cmdCerrar.Location = New System.Drawing.Point(CInt(cmdCerrar.Parent.Width / 2 - cmdCerrar.Width / 2), CInt(cmdCerrar.Parent.Height / 2 - cmdCerrar.Height / 2))
        Panel1.ResumeLayout(False)
    End Sub

End Class
