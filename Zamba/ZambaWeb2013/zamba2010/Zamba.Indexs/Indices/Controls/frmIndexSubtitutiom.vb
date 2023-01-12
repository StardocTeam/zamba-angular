
Imports zamba.core
Public Class frmIndexSubtitutiom
    Inherits Zamba.appblock.zform

#Region " Código generado por el Diseñador de Windows Forms "

    Private indexid As Int64
    Private IndexSubCtrl As UCListaSustitucion

    Private Sub New()
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'frmIndexSubtitutiom
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(392, 350)
        Me.Name = "frmIndexSubtitutiom"
        Me.Text = "Búsqueda de índice"

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

        Me.IndexSubCtrl = New UCListaSustitucion(Me.indexid, Reload, False)
        Me.Size = Me.IndexSubCtrl.Size
        Me.Controls.Add(Me.IndexSubCtrl)
        Me.IndexSubCtrl.Dock = Windows.Forms.DockStyle.Fill
        RemoveHandler IndexSubCtrl.RowSelected, AddressOf RowSelected
        AddHandler IndexSubCtrl.RowSelected, AddressOf RowSelected
    End Sub

    Public Sub New(ByVal indexid As Int64, ByVal Table As DataTable)
        Me.New()
        Me.indexid = indexid

        Me.IndexSubCtrl = New UCListaSustitucion(Me.indexid, Table)
        Me.Size = Me.IndexSubCtrl.Size
        Me.Controls.Add(Me.IndexSubCtrl)
        Me.IndexSubCtrl.Dock = Windows.Forms.DockStyle.Fill
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
        Me.AceptFlag = True
        Me.Descripcion = Descripcion
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmIndexSubtitutiom_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Me.AceptFlag = False Then
            Me.Codigo = Nothing
            Me.Descripcion = ""
        End If
    End Sub

    Private Sub frmIndexSubtitutiom_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.AceptFlag = False
    End Sub

#End Region


End Class
