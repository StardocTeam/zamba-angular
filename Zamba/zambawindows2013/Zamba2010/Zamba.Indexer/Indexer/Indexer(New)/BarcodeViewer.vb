Imports Zamba.Core
Public Class BarcodeViewer
    Inherits Zamba.AppBlock.ZForm

    Public Sub New(ByVal newresult As NewResult)
        InitializeComponent()

        'Add the index to the combobox
        result = newresult
        For Each i As Index In result.Indexs
            cmbItems.Items.Add(i.Name)
        Next
        cmbItems.SelectedIndex = 0
        'Add the Alignment to the combobox
        Alignment = "Derecha"
        CmbAlignment.Items.Add("Derecha")
        CmbAlignment.Items.Add("Centro")
        CmbAlignment.Items.Add("Izquierda")
        CmbAlignment.SelectedIndex = 0
    End Sub
    Public texto As String
    Public Alignment As String
    Private result As NewResult

    'Return the text of the textbox
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        texto = txtName.Text
        Close()
    End Sub

    'Return the text empty
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
        texto = ""
        Close()
    End Sub

    'Show the select index data on the textbox
    Private Sub cmbItems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbItems.SelectedIndexChanged
        Dim i As Index = DirectCast(result.Indexs(cmbItems.SelectedIndex), Index)
        If i.Data <> "" And IsNumeric(i.Data) = True Then
            txtName.Text = i.Data
        End If
    End Sub

    'Save the new alignment on the variable
    Private Sub CmbAlignment_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CmbAlignment.SelectedIndexChanged
        Alignment = CmbAlignment.Text
    End Sub
End Class