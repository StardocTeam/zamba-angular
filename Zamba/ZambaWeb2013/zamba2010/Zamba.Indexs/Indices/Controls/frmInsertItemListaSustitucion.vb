Imports System.Windows.Forms
Imports Zamba.AppBlock

Public Class frmInsertItemListaSustitucion
    Inherits ZForm
    Implements IDisposable

    Private _indexId As Int32
    Private _codigosExistentes As Generic.List(Of String)
    Private _insertHierarchy As Boolean
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
    Public Sub New(ByVal indexId As Int32, ByVal existingCodes As Generic.List(Of String), _
                         Optional ByVal InsertHierarchy As Boolean = False)
        'se modifico para que acepte codigos alfanumericos[sebastian 11/12/2008]
        Me.New(indexId)
        _codigosExistentes = existingCodes

        _insertHierarchy = InsertHierarchy
        If InsertHierarchy Then
            SetHierarchyLabels()
        End If
    End Sub

    Private Sub cmdAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        Try
            If CamposValidos() Then

                If _insertHierarchy Then
                    _codigosExistentes.Add(txtCodigo.Text & "|" & txtDescripcion.Text)
                Else
                    _codigosExistentes.Add(txtCodigo.Text)
                End If
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
            If _insertHierarchy Then
                MessageBox.Show("El valor padre no puede estar vacio.", "Error", MessageBoxButtons.OK)
            Else
                MessageBox.Show("El campo Código no puede estar vacio.", "Error", MessageBoxButtons.OK)
            End If

            txtCodigo.Focus()
            txtCodigo.Select()
            Return False
        ElseIf txtDescripcion.Text.Trim.Length = 0 Then
            If _insertHierarchy Then
                MessageBox.Show("El valor hijo no puede estar vacio.", "Error", MessageBoxButtons.OK)
            Else
                MessageBox.Show("El campo Descripción no puede estar vacio.", "Error", MessageBoxButtons.OK)
            End If

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
        ElseIf _insertHierarchy Then
            If _codigosExistentes.Contains(txtCodigo.Text.Trim() & "|" & txtDescripcion.Text.Trim()) Then
                MessageBox.Show("La combinación seleccionada ya existe en la lista.", "Error", MessageBoxButtons.OK)
                txtCodigo.Focus()
                txtCodigo.Select()
                Return False
            Else
                Return True
            End If
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
    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        RaiseEvent MidifiedIndex(txtCodigo.Text, txtDescripcion.Text)
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SetHierarchyLabels()
        Label1.Text = "Valor padre"
        Label3.Text = "Valor hijo"
    End Sub
End Class