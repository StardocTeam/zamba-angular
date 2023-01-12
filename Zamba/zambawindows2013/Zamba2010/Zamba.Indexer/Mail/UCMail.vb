Imports Zamba.Core
Imports System.Data

Public Class UCMail

    Private ruleId As Long
    ' Variable utilizada para almacenar la posición del elemento anterior del chklstOptions, antes de que se seleccione un nuevo elemento
    ' (que pasaría a ser el elemento actual)
    Private posPrevious As Integer = -1

    Public Sub New(ByVal _ruleId As Long)
        Me.ruleId = _ruleId
        Me.InitializeComponent()
    End Sub

    Private Sub chklstOptions_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chklstOptions.SelectedValueChanged

        ' No se muestra nada en el panel
        Me.pnlPanel.Controls(0).Visible = False
        Me.pnlPanel.Controls(1).Visible = False
        Me.pnlPanel.Controls(2).Visible = False

        ' Si el elemento de la lista tiene checked igual a true
        If (chklstOptions.GetItemChecked(chklstOptions.SelectedIndex) = True) Then

            ' El elemento actual pasa a ser el elemento anterior cuando se seleccione otro elemento de la lista
            posPrevious = chklstOptions.SelectedIndex
            ' Se coloca el elemento actual como checked igual a true
            chklstOptions.SetItemCheckState(chklstOptions.SelectedIndex, CheckState.Checked)

            ' Se obtiene el texto del elemento seleccionado en el chklstOptions
            Dim value As String = chklstOptions.Items(chklstOptions.SelectedIndex).ToString()

            Select Case value

                Case "Mail"
                    Me.pnlPanel.Controls(0).Visible = True
                    pnlPanel.Controls(0).Select()
                Case "Mensaje interno"
                    Me.pnlPanel.Controls(1).Visible = True
                    pnlPanel.Controls(1).Select()
                Case "Mail automático"
                    Me.pnlPanel.Controls(2).Visible = True
                    pnlPanel.Controls(2).Select()
            End Select

        End If

    End Sub

End Class