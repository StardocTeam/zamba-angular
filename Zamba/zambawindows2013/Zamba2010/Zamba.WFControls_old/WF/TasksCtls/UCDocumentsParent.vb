Public Class UCDocumentsParent
    Inherits TabControl
    Implements IDisposable

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region " C�digo generado por el Dise�ador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Dise�ador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicializaci�n despu�s de la llamada a InitializeComponent()

    End Sub

    'Requerido por el Dise�ador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Dise�ador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Dise�ador de Windows Forms. 
    'No lo modifique con el editor de c�digo.
    ' Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New ComponentModel.Container
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(UCDocumentsParent))
        '   Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        SuspendLayout()
        '
        'ImageList1
        '
        '  Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        '  Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '  Me.ImageList1.Images.SetKeyName(0, "")
        '
        'UCDocumentsParent
        '
        Name = "UCDocumentsParent"
        Size = New System.Drawing.Size(566, 432)
        ResumeLayout(False)


    End Sub

    Protected Overrides Sub OnTabIndexChanged(e As EventArgs)
        MyBase.OnTabIndexChanged(e)

    End Sub

#End Region

    '#Region "InertButton"
    'Public Event ShowTaskList()
    'Private Sub InertButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    RaiseEvent ShowTaskList()
    'End Sub
    '#End Region


End Class
