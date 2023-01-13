''' <summary>
''' Clase que se encarga de la eleccion de los procesos a ejecutarse
''' </summary>
''' <history>
''' Marcelo Modified 12/06/2008
''' </history>
''' <remarks></remarks>
Public Class frmZDBFixerOptions
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub chkProcessTables_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProcessTables.CheckedChanged
        Me.chkProcessRelations.Enabled = Me.chkProcessTables.Checked
        If Not Me.chkProcessTables.Checked Then
            Me.chkProcessRelations.Checked = False
        End If
    End Sub
End Class