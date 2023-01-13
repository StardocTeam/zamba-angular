Imports System.Windows.Forms
Public Class PlayDOScreenMessage

    Private _myRule As IDOSCREENMESSAGE
    Private sb As System.Text.StringBuilder
    Private NuevoMensaje As String

    Sub New(ByVal rule As IDOSCREENMESSAGE)
        _myRule = rule
    End Sub

    ''' <summary>
    '''  DoScreenMessage Play
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  Modified    02/12/2010  Param added to hide or not entity name in title of messagebox
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            For Each t As Core.TaskResult In results
                sb = New System.Text.StringBuilder
                sb.Append(_myRule.Name)

                'Oculto el nombre del documento
                If Not _myRule.HideDocumentName Then
                    sb.Append("- Documento ")
                    If Not t Is Nothing Then sb.Append(t.Name)
                End If


                NuevoMensaje = String.Empty
                NuevoMensaje = TextoInteligente.ReconocerCodigo(_myRule.Mensaje, t)
                NuevoMensaje = WFRuleParent.ReconocerVariablesValuesSoloTexto(NuevoMensaje)

                MessageBox.Show(NuevoMensaje, sb.ToString, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Next
        Finally

            sb = Nothing
            NuevoMensaje = Nothing
        End Try

        Return results

    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)
        Return WFRuleParent.ReconocerZvar(_myRule.Mensaje)
    End Function
End Class
