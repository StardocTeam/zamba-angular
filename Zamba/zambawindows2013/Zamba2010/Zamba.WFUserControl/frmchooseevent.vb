Imports Zamba.Core.Enumerators

Public Class frmchooseevent
    Inherits ZForm

    Public Selected As Integer
    Private Sub frmchooseevent_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        LoadEvents()
        lstEvents.SelectedIndex = 0
    End Sub

    Private Sub LoadEvents()
        lstEvents.Items.Add(TypesofRules.AbrirDocumento)
        lstEvents.Items.Add(TypesofRules.AbrirZamba)
        lstEvents.Items.Add(TypesofRules.Asignar)
        lstEvents.Items.Add(TypesofRules.Derivar)
        lstEvents.Items.Add(TypesofRules.Estado)
        lstEvents.Items.Add(TypesofRules.GuardarDocumento)
        lstEvents.Items.Add(TypesofRules.Iniciar)
        lstEvents.Items.Add(TypesofRules.Indices)
        lstEvents.Items.Add(TypesofRules.Insertar)
        lstEvents.Items.Add(TypesofRules.Terminar)
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Aceptar()
    End Sub
    Private Sub Aceptar()
        If lstEvents.SelectedItems.Count = 1 Then
            Selected = lstEvents.SelectedItem
            Close()
        Else
            MessageBox.Show("Debe seleccionar un tipo de evento para continuar", "Datos faltantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
End Class