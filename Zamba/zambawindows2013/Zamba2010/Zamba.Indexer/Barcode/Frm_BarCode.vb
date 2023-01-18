Public Class Frm_BarCode
    Inherits Zamba.AppBlock.ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    Dim CurrentUserId As Int64
    Public Sub New(ByVal CurrentUserId As Int64)
        MyBase.New()
        Me.CurrentUserId = CurrentUserId
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
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Frm_BarCode))
        '
        'Frm_BarCode
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        AutoScroll = True
        ClientSize = New System.Drawing.Size(512, 366)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Name = "Frm_BarCode"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = " CODIGO DE BARRAS"

    End Sub

#End Region

    Private Sub Frm_BarCode_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim UCBarCode As New UCBarCode(CurrentUserId)
        UCBarCode.Dock = DockStyle.Fill
        Controls.Add(UCBarCode)
    End Sub
End Class
