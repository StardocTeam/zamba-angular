Imports System.Windows.Forms
Imports Zamba.Core



Public Class frmIndexValueSearch

    Private IndexId As Int32 = 0


    Private Sub cmbIndexValue_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbIndexValue.SelectedIndexChanged
        Try

            txtValueCode.Text = cmbIndexValue.SelectedValue.ToString

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub txtValueCode_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtValueCode.TextChanged
        Try
            'ToDo:  El método getDescription debería pasar como parámetro el tipo de dato del Atributo (4to parámetro).
            '       Dado que a la clase solo de le pasa el indexId y no el atributo completo, no se pudo obtener el 
            '       tipo de dato. El tipo de dato se utiliza dentro de getDescription para verificar si debe remover o
            '       no los ceros que se encuentren delante del código. Si los ceros son removidos (como es este caso ya
            '       que por defecto se remueven) y el Atributo es alfanumerico, es posible que no se encuentre el código
            '       a buscar. Un ejemplo sería que en un Atributo alfanumérico exista un código 001 o 014. Cuando se 
            '       realizó este cambio se modifico todas las llamadas menos esta y se decidió no continuar con este
            '       método ya que existían otras tareas más críticas. Cualquier cosnulta el id de la tarea es el 3882.
            cmbIndexValue.Text = AutoSubstitutionBusiness.getDescription(txtValueCode.Text, IndexId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' [Sebastian 17-04-09] obtiene los valores de la tabla de sustitucion si los tiene.
    ''' </summary>
    ''' <param name="IndexName"></param>
    ''' <remarks></remarks>
    Private Sub frmIndexValueSearch_Load(ByVal IndexName As String)
        Try

            Dim b As New DataTable
            RemoveHandler cmbIndexValue.SelectedValueChanged, AddressOf cmbIndexValue_SelectedIndexChanged
            b = AutoSubstitutionBusiness.GetIndexData(IndexName.Substring(IndexName.IndexOf("i") + 1), False)
            If IsDBNull(b) = False Then
                cmbIndexValue.DataSource = b
                cmbIndexValue.ValueMember = b.Columns("codigo").ToString
                cmbIndexValue.DisplayMember = b.Columns("descripcion").ToString
            Else
                DialogResult = DialogResult.Cancel
            End If

            txtValueCode.Text = cmbIndexValue.SelectedValue
            AddHandler cmbIndexValue.SelectedValueChanged, AddressOf cmbIndexValue_SelectedIndexChanged

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub New(ByVal IndexName As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        frmIndexValueSearch_Load(IndexName)
    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnAcept_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAcept.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub
End Class