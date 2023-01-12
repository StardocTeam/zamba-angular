Public Class WFMainDescripcion
    Public Sub New(ByVal WFID As Int64, ByVal description As String, ByVal help As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _WFID = WFID
        txtAyuda.Text = help
        txtDescripcion.Text = description
    End Sub

    Private _WFID As Int64

    Private Sub btnCancelar_Click_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnGuardar_Click_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Zamba.Core.WFBusiness.SetWFDescriptionAndHelp(_WFID, txtDescripcion.Text, txtAyuda.Text)
        'guarda en la base de datos los datos que se modificaron en txtDescripcion y txtAyuda
        Close()
    End Sub
End Class