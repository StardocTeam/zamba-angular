Public Class ZForo
    Inherits Zamba.AppBlock.ZForm
    'Dim UcQuestion As New UcQuestion
    'Dim UcMensaje As New UcMensaje
    'Dim LeyendoMensaje As Boolean = True
    ' Dim NuevoCual As Integer = 0
    '  Dim DocT_Recibido As Integer
    ' Dim DocId_Recibido As Integer

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'DocT_Recibido = DocT
        'DocId_Recibido = DocId

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()


    End Sub

    Private Shared Sub AfterSelect()

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
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    '  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.Gray
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(2, 2)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(905, 3)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'Splitter3
        '
        Me.Splitter3.BackColor = System.Drawing.Color.Silver
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Location = New System.Drawing.Point(2, 5)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(905, 2)
        Me.Splitter3.TabIndex = 5
        Me.Splitter3.TabStop = False
        '
        'ZForo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        Me.ClientSize = New System.Drawing.Size(909, 730)
        Me.Controls.Add(Me.Splitter3)
        Me.Controls.Add(Me.Splitter1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ZForo"
        Me.Text = "ZForo"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ZForo_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    End Sub

    'Private Sub FillTreeview()
    '    Dim Mensajes As New ZForo_Factory
    '    Dim ArrayMensajes As New ArrayList
    '    Dim ArrayRespuestas As New ArrayList
    '    ArrayMensajes = Mensajes.GetAllMessages(0, "")
    '    ArrayRespuestas = Mensajes.GetAllMessages(0, "")
    '    UcQuestion.CargarEnTreeview(ArrayMensajes, ArrayRespuestas)
    'End Sub

    'Private Sub ResponderMessage(ByVal ParentId As Integer)

    'End Sub

    'Private Sub NewMessage()

    'End Sub

    'Private Sub DeleteMessage(ByVal DocT As Integer, ByVal DocId As Integer, ByVal ParentId As Integer)

    'End Sub

    'Private Sub PreguntaSeleccionada(ByVal Mensaje As Mensaje)
    '    LeyendoMensaje = True
    '    UcMensaje.Bloquear(LeyendoMensaje)
    '    'btnGuardar.Enabled = False

    '    UcMensaje.FillMensaje(Mensaje)
    'End Sub

    'Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)

    'End Sub

    ''Private Sub btnFiltrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''    Dim DocT As Integer
    ''    If Me.cmbDocT.Text.Trim <> "" Then
    ''        DocT = CInt(DocTypesBusiness.GetDocTypeIdByName(Me.cmbDocT.Text))
    ''    Else
    ''        DocT = 0
    ''    End If
    ''    Dim Asunto As String = txtAsunto.Text.Trim

    ''    FillTreeview2(DocT, Asunto)
    ''    Exit Sub
    ''End Sub

    'Private Sub FillTreeview2(ByVal DocT As Integer, ByVal Asunto As String)
    '    Dim Mensajes As New ZForo_Factory
    '    Dim ArrayMensajes As New ArrayList
    '    Dim ArrayRespuestas As New ArrayList
    '    ArrayMensajes = Mensajes.GetAllMessages(DocT, Asunto)
    '    ArrayRespuestas = Mensajes.GetAllMessages(DocT, Asunto)
    '    UcQuestion.CargarEnTreeview(ArrayMensajes, ArrayRespuestas)
    'End Sub

    ''Private Sub BuscarTiposDeDocumento()
    ''    Dim DsDocTypes As New DataSet

    ''    DsDocTypes = DocTypesBusiness.GetDocTypes()
    ''    Dim i As Integer
    ''    For i = 0 To DsDocTypes.Tables(0).Rows.Count - 1
    ''        cmbDocT.Items.Add(DsDocTypes.Tables(0).Rows(i).Item(0))
    ''    Next

    ''End Sub

    ''Private Sub bntNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''    NuevoCual = 1
    ''    LeyendoMensaje = False
    ''    UcMensaje.Bloquear(LeyendoMensaje)
    ''    btnGuardar.Enabled = True
    ''End Sub

    ''Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''    LeyendoMensaje = True
    ''    btnGuardar.Enabled = False

    ''    Dim Mensajes As New ZForo_Factory
    ''    If NuevoCual = 1 Then
    ''        Mensajes.InsertMessage(CInt(UcMensaje.txtTipoDeDocumento.Text), CInt(UcMensaje.txtIdDocumento.Text), SiguienteId, 0, UcMensaje.txtAsunto.Text.Trim, UcMensaje.txtMensaje.Text.Trim, CDate(UcMensaje.txtFecha.Text), BuscarUsuario, 0)
    ''    ElseIf NuevoCual = 2 Then
    ''        Dim ArrayMensaje As ArrayList = UcQuestion.GetSeleccionado
    ''        Dim Parent As Integer = SiguienteParent(ArrayMensaje(0), ArrayMensaje(1))
    ''        Mensajes.InsertMessage(UcMensaje.txtTipoDeDocumento.Text, UcMensaje.txtIdDocumento.Text, SiguienteId, Parent, UcMensaje.txtAsunto.Text, UcMensaje.txtMensaje.Text, Date.Now, BuscarUsuario, 0)
    ''    End If
    ''    NuevoCual = 0
    ''End Sub

    'Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim ArrayMensaje As ArrayList = UcQuestion.DeleteMessage()
    '    Dim Mensajes As New ZForo_Factory
    '    Mensajes.DeleteMessage(ArrayMensaje(0), ArrayMensaje(1), ArrayMensaje(2))
    'End Sub

    ''Private Sub btnResponder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''    NuevoCual = 2
    ''    LeyendoMensaje = False
    ''    UcMensaje.Bloquear(LeyendoMensaje)
    ''    btnGuardar.Enabled = True
    ''End Sub

    'Private Function SiguienteId() As Integer
    '    Dim Mensajes As New ZForo_Factory
    '    Return Mensajes.SiguienteId
    'End Function
    'Private Function SiguienteParent(ByVal DocT As Integer, ByVal DocId As Integer) As Integer
    '    Dim Mensajes As New ZForo_Factory
    '    Return Mensajes.SiguienteParent(DocT, DocId)
    'End Function

    'Private Function BuscarUsuario() As Integer
    '    Return 0
    'End Function

    'Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub
End Class
