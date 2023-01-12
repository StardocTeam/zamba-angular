Public Class FrmPreviewGold
    Inherits Zamba.appblock.zform

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
  

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmPreviewGold))
        '
        'FrmPreviewGold
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        BackColor = System.Drawing.Color.LightSteelBlue
        ClientSize = New System.Drawing.Size(292, 266)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Name = "FrmPreviewGold"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        WindowState = System.Windows.Forms.FormWindowState.Maximized

    End Sub

#End Region

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)

        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub


End Class
