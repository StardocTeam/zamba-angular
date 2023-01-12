Imports Zamba.Core

Public Class PlayDOAsk
    Private _myRule As IDOAsk
    Private valor As String
    Private mensaje As String
    Private tamaño As Integer
    Private valorPorDefecto As Object
    Dim formdoask As InputBoxDoAsk
    Sub New(ByVal rule As IDOAsk)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        valor = String.Empty

        Try
            Me.mensaje = String.Empty
            Try
                mensaje = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.Mensaje)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error al obtener el mensaje.")
                Throw ex
            End Try

            Me.valorPorDefecto = String.Empty
            Try
                valorPorDefecto = WFRuleParent.ReconocerVariablesAsObject(Me._myRule.ValorPorDefecto)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error al obtener el mensaje.")
                Throw ex
            End Try

            If results.Count > 0 Then
                For Each r As ITaskResult In results
                    mensaje = Zamba.Core.TextoInteligente.ReconocerCodigo(mensaje, r)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Mensaje a consultar: " & mensaje)

                    If TypeOf (valorPorDefecto) Is DataSet Then
                        formdoask = New InputBoxDoAsk("Ingreso de datos", mensaje, DirectCast(valorPorDefecto, DataSet).Tables(0))
                    ElseIf TypeOf (valorPorDefecto) Is DataTable Then
                        formdoask = New InputBoxDoAsk("Ingreso de datos", mensaje, DirectCast(valorPorDefecto, DataTable))
                    Else
                        valorPorDefecto = Zamba.Core.TextoInteligente.ReconocerCodigo(valorPorDefecto.ToString(), r)
                        formdoask = New InputBoxDoAsk("Ingreso de datos", mensaje, valorPorDefecto.ToString())
                    End If

                    If formdoask.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        valor = formdoask.Message
                        While valor = String.Empty
                            If MsgBox("El comentario es obligatorio, presione cancelar para ingresar el comentario, de lo contrario presione aceptar para cancelar la acción.", MsgBoxStyle.OkCancel, "ATENCION - ¿DESEA CERRAR?") = MsgBoxResult.Cancel Then
                                If valor = String.Empty Then
                                    If formdoask.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                        valor = formdoask.Message
                                    Else
                                        If _myRule.ThrowExceptionIfCancel Then
                                            Trace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                                            Throw New Exception("El usuario cancelo la ejecucion de la regla")
                                            Exit While
                                        Else
                                            results = Nothing
                                            Exit While
                                        End If
                                    End If
                                End If
                            Else
                                If _myRule.ThrowExceptionIfCancel Then
                                    Trace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                                    Throw New Exception("El usuario cancelo la ejecucion de la regla")
                                    Exit While
                                Else
                                    results = Nothing
                                    Exit While
                                End If
                            End If
                        End While
                        Trace.WriteLineIf(ZTrace.IsInfo, "Respuesta al mensaje: " & valor)
                        TextoInteligente.AsignItemFromSmartText(Me._myRule.Variable, r, valor)
                    Else
                        If _myRule.ThrowExceptionIfCancel Then
                            Trace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                            Throw New Exception("El usuario cancelo la ejecucion de la regla")
                        Else
                            'Cancel
                            results = Nothing
                        End If
                    End If

                Next
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "Mensaje a consultar: " & mensaje)
                valor = InputBox(mensaje, "Ingreso de datos", valorPorDefecto.ToString())
                Trace.WriteLineIf(ZTrace.IsInfo, "Respuesta al mensaje: " & valor)
            End If

            If VariablesInterReglas.ContainsKey(Me._myRule.Variable) = False Then
                VariablesInterReglas.Add(Me._myRule.Variable, valor, False)
            Else
                VariablesInterReglas.Item(Me._myRule.Variable) = valor
            End If

        Finally
            If Not IsNothing(formdoask) Then
                Me.formdoask.Dispose()
                Me.formdoask = Nothing
            End If
            Me.valor = Nothing
            Me.mensaje = Nothing
        End Try
        Return (results)
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
