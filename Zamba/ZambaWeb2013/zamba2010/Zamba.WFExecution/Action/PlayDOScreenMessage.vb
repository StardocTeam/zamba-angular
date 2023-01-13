Imports System.Windows.Forms
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
        Return PlayWeb(results, Nothing)
    End Function
    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            Dim TextoInteligente As New TextoInteligente()

            If IsNothing(Params) Then
                Params = New Hashtable
            End If

            For Each t As Core.TaskResult In results
                Me.sb = New System.Text.StringBuilder
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & t.Name)
                sb.Append(Me._myRule.Name)
                sb.Append(" - ")
                sb.Append(t.Name)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Configurando mensaje...")
                Me.NuevoMensaje = String.Empty
                Me.NuevoMensaje = TextoInteligente.ReconocerCodigo(Me._myRule.Mensaje, t)
                Me.NuevoMensaje = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me.NuevoMensaje)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "El mensaje mostrado es: " & Me.NuevoMensaje)
                Params.Add("Nuevo Mensaje", NuevoMensaje)
                Params.Add("Action", Me._myRule.Action)
                Params.Add("sb", sb.ToString())
            Next
        Finally
            VarInterReglas = Nothing
            Me.sb = Nothing
            Me.NuevoMensaje = Nothing
        End Try

        Return results

    End Function
End Class
