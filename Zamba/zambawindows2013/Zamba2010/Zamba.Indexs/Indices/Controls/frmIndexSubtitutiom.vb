Public Class frmIndexSubtitutiom
    Inherits ZForm
    Implements IDisposable

#Region " Código generado por el Diseñador de Windows Forms "

    Private indexid As Int64
    Private IndexSubCtrl As UCListaSustitucion

    Private Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                RemoveHandler MyBase.Load, AddressOf frmIndexSubtitutiom_Load
                RemoveHandler MyBase.Closing, AddressOf frmIndexSubtitutiom_Closing

                If IndexSubCtrl IsNot Nothing Then
                    RemoveHandler IndexSubCtrl.RowSelected, AddressOf RowSelected
                    IndexSubCtrl.Dispose()
                    IndexSubCtrl = Nothing
                End If
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
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(frmIndexSubtitutiom))
        SuspendLayout()
        '
        'frmIndexSubtitutiom
        '
        AutoScaleBaseSize = New Size(5, 14)
        ClientSize = New Size(420, 350)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "frmIndexSubtitutiom"
        Text = "Búsqueda de Atributo"
        ResumeLayout(False)

    End Sub

    'Private Sub init()
    '    '
    '    'IndexSubCtrl
    '    '

    'End Sub



#End Region

#Region "Constructores"

    Public Sub New(ByVal indexId As Int64, ByVal Reload As Boolean)
        Me.New()
        Me.indexid = indexId

        IndexSubCtrl = New UCListaSustitucion(Me.indexid, Reload, False)
        Size = IndexSubCtrl.Size
        Controls.Add(IndexSubCtrl)
        IndexSubCtrl.Dock = DockStyle.Fill
        RemoveHandler IndexSubCtrl.RowSelected, AddressOf RowSelected
        AddHandler IndexSubCtrl.RowSelected, AddressOf RowSelected
    End Sub

    Public Sub New(ByVal indexid As Int64, ByVal Table As DataTable)
        Me.New()
        Me.indexid = indexid

        IndexSubCtrl = New UCListaSustitucion(Me.indexid, Table)
        Size = IndexSubCtrl.Size
        Controls.Add(IndexSubCtrl)
        IndexSubCtrl.Dock = DockStyle.Fill
        RemoveHandler IndexSubCtrl.RowSelected, AddressOf RowSelected
        AddHandler IndexSubCtrl.RowSelected, AddressOf RowSelected
    End Sub

#End Region

#Region "Atributos"
    Public Codigo As String
    Public Descripcion As String = ""
    Public index As Int64
    Private AceptFlag As Boolean
#End Region

#Region "Eventos"

    Private Sub RowSelected(ByVal Codigo As String, ByVal Descripcion As String, ByVal Index As Int64)
        Me.Codigo = Codigo
        AceptFlag = True
        Me.Descripcion = Descripcion
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub frmIndexSubtitutiom_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If AceptFlag = False Then
            Codigo = Nothing
            Descripcion = String.Empty
            IndexSubCtrl.ClearSearch()
        End If
    End Sub

    Private Sub frmIndexSubtitutiom_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        AceptFlag = False
    End Sub

#End Region


End Class
