Public Class frmInsertItemListaSustitucion
    Inherits ZForm
    Implements IDisposable

    Private _indexId As Int32
    Private _codigosExistentes As Generic.List(Of String)

    'se modifico para que acepte codigos alfanumericos[sebastian 11/12/2008]
    'Public Event NewItem(ByVal IndexId As Int32, ByVal codigo As Integer, ByVal descripcion As String)
    Public Event NewItem(ByVal IndexId As Int32, ByVal codigo As String, ByVal descripcion As String)

    Private Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByVal indexId As Int32)
        Me.New()
        _indexId = indexId
    End Sub
    'Public Sub New(ByVal indexId As Int32, ByVal existingCodes As Generic.List(Of Integer))
    Public Sub New(ByVal indexId As Int32, ByVal existingCodes As Generic.List(Of String))
        'se modifico para que acepte codigos alfanumericos[sebastian 11/12/2008]
        Me.New(indexId)
        _codigosExistentes = existingCodes
    End Sub

    Private Sub cmdAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdAceptar.Click
        Try
            If CamposValidos() Then

                _codigosExistentes.Add(txtCodigo.Text)
                'se modifico para que acepte codigos alfanumericos[sebastian 11/12/2008]
                'RaiseEvent NewItem(_indexId, Int32.Parse(txtCodigo.Text), txtDescripcion.Text)
                RaiseEvent NewItem(_indexId, txtCodigo.Text, txtDescripcion.Text)
            Else
                Exit Sub
            End If
            txtCodigo.Text = String.Empty
            txtDescripcion.Text = String.Empty
            txtCodigo.Focus()
            txtCodigo.Select()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function CamposValidos() As Boolean
        If txtCodigo.Text.Trim.Length = 0 Then
            MessageBox.Show("El campo Código no puede estar vacio.", "Error", MessageBoxButtons.OK)

            txtCodigo.Focus()
            txtCodigo.Select()
            Return False
        ElseIf txtDescripcion.Text.Trim.Length = 0 Then
            MessageBox.Show("El campo Descripción no puede estar vacio.", "Error", MessageBoxButtons.OK)

            txtDescripcion.Focus()
            txtDescripcion.Select()
            Return False
            'Se comento todo el codigo que esta abajo porque gustavo pidio que sean los códigos de las
            'descripciones en las tablas de sustitucion alfanumericos.[sebastian 11/12/2008]

            'ElseIf IsNumeric(txtCodigo.Text) = False Then
            '    MessageBox.Show("El código seleccionado debe ser un número.", "Error", MessageBoxButtons.OK)
            '    txtCodigo.Focus()
            '    txtCodigo.Select()
            '    Return False
        ElseIf _codigosExistentes.Contains(txtCodigo.Text.Trim()) Then
            MessageBox.Show("El código seleccionado ya existe en la lista.", "Error", MessageBoxButtons.OK)
            txtCodigo.Focus()
            txtCodigo.Select()
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Event MidifiedIndex(ByVal Codigo As String, ByVal description As String)
    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnModify.Click
        RaiseEvent MidifiedIndex(txtCodigo.Text, txtDescripcion.Text)
        DialogResult = DialogResult.OK
        Close()
    End Sub
End Class