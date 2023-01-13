Public Class frmComplete
    Inherits Zamba.AppBlock.ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

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
    Friend WithEvents UcComplete1 As UCComplete
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(frmComplete))
        UcComplete1 = New Zamba.Controls.UCComplete
        SuspendLayout()
        '
        'UcComplete1
        '
        UcComplete1.Dock = System.Windows.Forms.DockStyle.Fill
        UcComplete1.IndexKey = False
        UcComplete1.Location = New System.Drawing.Point(2, 2)
        UcComplete1.Name = "UcComplete1"
        UcComplete1.Size = New System.Drawing.Size(631, 349)
        UcComplete1.TabIndex = 0
        '
        'frmComplete
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(635, 353)
        Controls.Add(UcComplete1)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmComplete"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Configurar Autocompletar"
        ResumeLayout(False)

    End Sub

#End Region

End Class
