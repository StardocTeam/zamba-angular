Public Class PlayDOAsk
    Private _myRule As IDOAsk
    Private valor As String
    Private mensaje As String
    Private tamaño As Integer
    Private valorPorDefecto As Object
    'Dim formdoask As InputBoxDoAsk
    Sub New(ByVal rule As IDOAsk)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        valor = String.Empty

        Try
            For Each r As Result In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
            Next

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo mensaje a consultar..." & Me._myRule.Mensaje)
            Me.mensaje = String.Empty
            Try
                Dim VarInterReglas As New VariablesInterReglas()
                mensaje = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._myRule.Mensaje)
                VarInterReglas = Nothing
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al obtener el mensaje.")
                Throw ex
            End Try

            Me.valorPorDefecto = String.Empty
            Try
                Dim VariablesInterReglas As New VariablesInterReglas()
                valorPorDefecto = VariablesInterReglas.ReconocerVariablesAsObject(Me._myRule.ValorPorDefecto)
                VariablesInterReglas = Nothing
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al obtener el mensaje.")
                Throw ex
            End Try

            If results.Count > 0 Then
                For Each r As ITaskResult In results
                    mensaje = Zamba.Core.TextoInteligente.ReconocerCodigo(mensaje, r)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Mensaje a consultar: " & mensaje)

                    'If TypeOf (valorPorDefecto) Is DataSet Then
                    '    formdoask = New InputBoxDoAsk("Ingreso de datos", mensaje, DirectCast(valorPorDefecto, DataSet).Tables(0))
                    'ElseIf TypeOf (valorPorDefecto) Is DataTable Then
                    '    formdoask = New InputBoxDoAsk("Ingreso de datos", mensaje, DirectCast(valorPorDefecto, DataTable))
                    'Else
                    '    valorPorDefecto = Zamba.Core.TextoInteligente.ReconocerCodigo(valorPorDefecto.ToString(), r)
                    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor por defecto: " & valorPorDefecto.ToString())

                    '    formdoask = New InputBoxDoAsk("Ingreso de datos", mensaje, valorPorDefecto.ToString())
                    'End If

                    'If formdoask.ShowDialog() =DialogResult.OK Then
                    '    valor = formdoask.Message
                    '    While valor = String.Empty
                    '        If MsgBox("El comentario es obligatorio, presione cancelar para ingresar el comentario, de lo contrario presione aceptar para cancelar la acción.", MsgBoxStyle.OkCancel, "ATENCION - ¿DESEA CERRAR?") = MsgBoxResult.Cancel Then
                    '            If valor = String.Empty Then
                    '                If formdoask.ShowDialog() =DialogResult.OK Then
                    '                    valor = formdoask.Message
                    '                Else
                    '                    If _myRule.ThrowExceptionIfCancel Then
                    '                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                    '                        Throw New Exception("El usuario cancelo la ejecucion de la regla")
                    '                        Exit While
                    '                    Else
                    '                        results = Nothing
                    '                        Exit While
                    '                    End If
                    '                End If
                    '            End If
                    '        Else
                    '            If _myRule.ThrowExceptionIfCancel Then
                    '                ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                    '                Throw New Exception("El usuario cancelo la ejecucion de la regla")
                    '                Exit While
                    '            Else
                    '                results = Nothing
                    '                Exit While
                    '            End If
                    '        End If
                    '    End While
                    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Respuesta al mensaje: " & valor)
                    '    TextoInteligente.AsignItemFromSmartText(Me._myRule.Variable, r, valor)
                    'Else
                    '    If _myRule.ThrowExceptionIfCancel Then
                    '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                    '        Throw New Exception("El usuario cancelo la ejecucion de la regla")
                    '    Else
                    '        'Cancel
                    '        results = Nothing
                    '    End If
                    'End If

                Next
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Mensaje a consultar: " & mensaje)
                valor = InputBox(mensaje, "Ingreso de datos", valorPorDefecto.ToString())
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Respuesta al mensaje: " & valor)
            End If

            If VariablesInterReglas.ContainsKey(Me._myRule.Variable) = False Then
                VariablesInterReglas.Add(Me._myRule.Variable, valor)
            Else
                VariablesInterReglas.Item(Me._myRule.Variable) = valor
            End If

        Finally
            'If Not IsNothing(formdoask) Then
            '    Me.formdoask.Dispose()
            '    Me.formdoask = Nothing
            'End If
            Me.valorPorDefecto = Nothing
            Me.valor = Nothing
            Me.mensaje = Nothing
        End Try
        Return (results)
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        valor = String.Empty
        Try
            For Each r As Result In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
            Next

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo mensaje a consultar...")
            Me.mensaje = String.Empty
            Try

                'If _myRule.Mensaje.Contains("zvar") Then
                '    _myRule.Mensaje = _myRule.Mensaje.Replace("zvar(", "")
                '    _myRule.Mensaje = _myRule.Mensaje.Replace(")", "")
                'End If

                Dim VarInterReglas As New VariablesInterReglas()
                mensaje = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._myRule.Mensaje)
                VarInterReglas = Nothing
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al obtener el mensaje.")
                Throw ex
            End Try

            Me.valorPorDefecto = String.Empty
            Try
                Dim VarInterReglas As New VariablesInterReglas()
                valorPorDefecto = VarInterReglas.ReconocerVariablesAsObject(Me._myRule.ValorPorDefecto)
                VarInterReglas = Nothing
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al obtener el mensaje.")
                Throw ex
            End Try

            If results.Count > 0 Then
                Dim TextoInteligente As New TextoInteligente()

                For Each r As ITaskResult In results
                    mensaje = TextoInteligente.ReconocerCodigo(mensaje, r)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Mensaje a consultar: " & mensaje)

                    If TypeOf (valorPorDefecto) Is DataSet Then
                        valorPorDefecto = DirectCast(valorPorDefecto, DataSet).Tables(0)
                        Params.Add("singleData", False)
                    ElseIf TypeOf (valorPorDefecto) Is DataTable Then
                        Params.Add("singleData", False)
                    Else
                        valorPorDefecto = Zamba.Core.TextoInteligente.ReconocerCodigo(valorPorDefecto.ToString(), r)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor por defecto: " & valorPorDefecto.ToString())

                        Params.Add("singleData", True)
                    End If

                    Params.Add("mensaje", mensaje)
                    Params.Add("valorPorDefecto", valorPorDefecto)
                Next
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Mensaje a consultar: " & mensaje)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor por defecto: " & valorPorDefecto.ToString())
            End If
        Finally
            Me.valorPorDefecto = Nothing
            Me.valor = Nothing
            Me.mensaje = Nothing
        End Try
        Return results
    End Function

    Public Function PlayWebSecondExecution(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        If Params.Contains("valor") Then
            valor = Params("valor")
            If _myRule.Variable.Contains("zvar") Then
                _myRule.Variable = _myRule.Variable.Replace("zvar(", "")
                _myRule.Variable = _myRule.Variable.Replace(")", "")
            End If
            If results.Count > 0 Then

                Dim TextoInteligente As New TextoInteligente()
                For Each r As ITaskResult In results

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Respuesta al mensaje: " & valor)
                    TextoInteligente.AsignItemFromSmartText(Me._myRule.Variable, r, valor)
                Next
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Respuesta al mensaje: " & valor)
            End If

            If VariablesInterReglas.ContainsKey(Me._myRule.Variable) = False Then
                VariablesInterReglas.Add(Me._myRule.Variable, valor)
            Else
                VariablesInterReglas.Item(Me._myRule.Variable) = valor
            End If
        End If

        Params.Clear()

        Return results
    End Function
End Class
