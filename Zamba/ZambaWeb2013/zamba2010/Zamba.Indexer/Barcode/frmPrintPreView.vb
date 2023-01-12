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

        Me.cmdCerrar.Location = New System.Drawing.Point(CInt(Me.cmdCerrar.Parent.Width / 2 - Me.cmdCerrar.Width / 2), CInt(Me.cmdCerrar.Parent.Height / 2 - Me.cmdCerrar.Height / 2))

        Me.Panel1.ResumeLayout(True)
        Me.PrintPreviewControl1.Document = doc
        Me.PrintPreviewControl1.AutoZoom = True
        Me.PrintPreviewControl1.Zoom = 1.0
        Me.ResumeLayout(True)
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
    Friend WithEvents cmdCerrar As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PrintPreviewControl1 As System.Windows.Forms.PrintPreviewControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPrintPreView))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmdCerrar = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.PrintPreviewControl1 = New System.Windows.Forms.PrintPreviewControl
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdCerrar)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 406)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(632, 32)
        Me.Panel1.TabIndex = 1
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Location = New System.Drawing.Point(224, 8)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.TabIndex = 0
        Me.cmdCerrar.Text = "&Cerrar"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.PrintPreviewControl1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(632, 406)
        Me.Panel2.TabIndex = 2
        '
        'PrintPreviewControl1
        '
        Me.PrintPreviewControl1.AutoZoom = False
        Me.PrintPreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrintPreviewControl1.Location = New System.Drawing.Point(0, 0)
        Me.PrintPreviewControl1.Name = "PrintPreviewControl1"
        Me.PrintPreviewControl1.Size = New System.Drawing.Size(632, 406)
        Me.PrintPreviewControl1.TabIndex = 1
        Me.PrintPreviewControl1.Zoom = 0.3
        '
        'frmPrintPreView
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(632, 438)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPrintPreView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPrintPreView"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cerrar_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCerrar.Click, MyBase.Closed
        Me.Hide()
    End Sub

    Private Sub redimensionar(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.Resize
        Me.Panel1.SuspendLayout()
        Me.cmdCerrar.Location = New System.Drawing.Point(CInt(Me.cmdCerrar.Parent.Width / 2 - Me.cmdCerrar.Width / 2), CInt(Me.cmdCerrar.Parent.Height / 2 - Me.cmdCerrar.Height / 2))
        Me.Panel1.ResumeLayout(False)
    End Sub

End Class
