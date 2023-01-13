Imports System.Windows.Forms
Imports Zamba.Core

Public Class PlayDOScreenMessage

    Private _myRule As IDOSCREENMESSAGE
    Private sb As System.Text.StringBuilder
    Private NuevoMensaje As String

    Sub New(ByVal rule As IDOSCREENMESSAGE)
        Me._myRule = rule
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
                Me.sb = New System.Text.StringBuilder
                sb.Append(Me._myRule.Name)

                'Oculto el nombre del documento
                If Not Me._myRule.HideDocumentName Then
                    sb.Append("- Documento ")
                    sb.Append(t.Name)
                End If


                Me.NuevoMensaje = String.Empty
                Me.NuevoMensaje = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Mensaje, t)
                Me.NuevoMensaje = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.NuevoMensaje)

                MessageBox.Show(NuevoMensaje, sb.ToString, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Next
        Finally

            Me.sb = Nothing
            Me.NuevoMensaje = Nothing
        End Try

        Return results

    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)
        Return WFRuleParent.ReconocerZvar(Me._myRule.Mensaje)
    End Function
End Class
