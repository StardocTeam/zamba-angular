Imports Zamba.Core
Public Class PlayIfDates
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal rule As IIfDates) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (rule.ChildRulesIds Is Nothing OrElse rule.ChildRulesIds.Count = 0) Then
            rule.ChildRulesIds = WFRB.GetChildRulesIds(rule.ID)
        End If


        If rule.ChildRulesIds.Count = 2 Then
            For Each childruleId As Int64 In rule.ChildRulesIds
                Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                R.ParentRule = rule
                R.IsAsync = rule.IsAsync
                If R.GetType().ToString().Contains("IfBranch") Then
                    Return results
                End If
            Next
        End If
        Dim SL As New System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ingreso en el PLAY de IfDates")
        Select Case rule.MiFecha
            Case Result.DocumentDates.Creacion
                For Each TR As TaskResult In results
                    If validarFecha(TR, rule) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Verdadero")
                        SL.Add(TR)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Falso")
                    End If
                Next
            Case Result.DocumentDates.Edicion
                For Each TR As TaskResult In results
                    If validarFecha(TR, rule) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Verdadero")
                        SL.Add(TR)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Falso")
                    End If
                Next
            Case Result.DocumentDates.Entrada
                For Each TR As TaskResult In results
                    If validarFecha(TR, rule) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Verdadero")
                        SL.Add(TR)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Falso")
                    End If
                Next
            Case Result.DocumentDates.Salida
                For Each TR As TaskResult In results
                    If validarFecha(TR, rule) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Verdadero")
                        SL.Add(TR)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Falso")
                    End If
                Next
        End Select
        Return SL
    End Function

    Private Function validarFecha(ByRef TR As TaskResult, ByRef _rule As IIfDates) As Boolean
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparando fechas...")
        Select Case _rule.TipoComparacion

            Case TipoComparaciones.Horas
                'Le saco los minutos a CreateDate haciéndolo Date y adderiendole las horas
                Dim fechaCreateDate As DateTime = TR.CreateDate.Date.AddHours(TR.CreateDate.Hour)
                Dim fechaHoyMasHoras As DateTime = Date.Now.Date.AddHours(_rule.CantidadHoras + Date.Now.Hour)
                Select Case _rule.Comparador
                    Case Comparadores.Igual
                        Return fechaCreateDate = fechaHoyMasHoras
                    Case Comparadores.Distinto
                        Return fechaHoyMasHoras <> fechaHoyMasHoras
                    Case Comparadores.Mayor
                        Return fechaHoyMasHoras > fechaCreateDate
                    Case Comparadores.Menor
                        Return fechaHoyMasHoras < fechaCreateDate
                    Case Comparadores.IgualMayor
                        Return fechaHoyMasHoras >= fechaCreateDate
                    Case Comparadores.IgualMenor
                        Return fechaHoyMasHoras <= fechaCreateDate
                End Select

            Case TipoComparaciones.Dias
                Select Case _rule.Comparador
                    Case Comparadores.Igual
                        Return TR.CreateDate.Date = Date.Now.AddDays(_rule.CantidadDias).Date
                    Case Comparadores.Distinto
                        Return TR.CreateDate.Date <> Date.Now.AddDays(_rule.CantidadDias).Date
                    Case Comparadores.Mayor
                        Return Date.Now.AddDays(_rule.CantidadDias).Date > TR.CreateDate.Date
                    Case Comparadores.Menor
                        Return Date.Now.AddDays(_rule.CantidadDias).Date < TR.CreateDate.Date
                    Case Comparadores.IgualMayor
                        Return Date.Now.AddDays(_rule.CantidadDias).Date >= TR.CreateDate.Date
                    Case Comparadores.IgualMenor
                        Return Date.Now.AddDays(_rule.CantidadDias).Date <= TR.CreateDate.Date
                End Select

            Case TipoComparaciones.Especifica
                Dim Fecha As Date
                Select Case _rule.FechaDocumentoComparar
                    Case DocumentDates.Creacion
                        Fecha = TR.CreateDate.Date
                    Case DocumentDates.Edicion
                        Fecha = TR.EditDate.Date
                    Case DocumentDates.Entrada
                        Fecha = TR.AsignedDate.Date
                    Case DocumentDates.Salida
                        Fecha = TR.ExpireDate.Date
                End Select
                Select Case _rule.Comparador
                    Case Comparadores.Igual
                        Return TR.CreateDate.Date = Fecha
                    Case Comparadores.Distinto
                        Return TR.CreateDate.Date <> Fecha
                    Case Comparadores.Mayor
                        Return Fecha > TR.CreateDate.Date
                    Case Comparadores.Menor
                        Return Fecha < TR.CreateDate.Date
                    Case Comparadores.IgualMayor
                        Return Fecha >= TR.CreateDate.Date
                    Case Comparadores.IgualMenor
                        Return Fecha <= TR.CreateDate.Date
                End Select

            Case TipoComparaciones.Fija
                Select Case _rule.Comparador
                    Case Comparadores.Igual
                        Return TR.CreateDate.Date = _rule.FechaAComparar.Date
                    Case Comparadores.Distinto
                        Return TR.CreateDate.Date <> _rule.FechaAComparar.Date
                    Case Comparadores.Mayor
                        Return _rule.FechaAComparar.Date > TR.CreateDate.Date
                    Case Comparadores.Menor
                        Return _rule.FechaAComparar.Date < TR.CreateDate.Date
                    Case Comparadores.IgualMayor
                        Return _rule.FechaAComparar.Date >= TR.CreateDate.Date
                    Case Comparadores.IgualMenor
                        Return _rule.FechaAComparar.Date <= TR.CreateDate.Date
                End Select
        End Select

    End Function

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal rule As IIfDates, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim SL As New System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ingreso en el PLAY de IfDates")
        Select Case rule.MiFecha
            Case Result.DocumentDates.Creacion
                For Each TR As TaskResult In results
                    If validarFecha(TR, rule) = ifType Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Verdadero")
                        SL.Add(TR)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Falso")
                    End If
                Next
            Case Result.DocumentDates.Edicion
                For Each TR As TaskResult In results
                    If validarFecha(TR, rule) = ifType Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Verdadero")
                        SL.Add(TR)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Falso")
                    End If
                Next
            Case Result.DocumentDates.Entrada
                For Each TR As TaskResult In results
                    If validarFecha(TR, rule) = ifType Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Verdadero")
                        SL.Add(TR)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Falso")
                    End If
                Next
            Case Result.DocumentDates.Salida
                For Each TR As TaskResult In results
                    If validarFecha(TR, rule) = ifType Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Verdadero")
                        SL.Add(TR)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Falso")
                    End If
                Next
        End Select
        Return SL
    End Function

End Class


'Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal rule As IIfDates) As System.Collections.Generic.List(Of Core.ITaskResult)
'    Dim SL As New System.Collections.Generic.List(Of Core.ITaskResult)
'    ZTrace.WriteLineIf(ZTrace.IsInfo,"Ingreso en el PLAY de IfDates")
'    Try
'        Select Case rule.MiFecha
'            Case Result.DocumentDates.Creacion
'                For Each TR As TaskResult In results
'                    Try
'                        If validarFecha(TR.CreateDate, rule.Comparador, TR, rule.TipoComparacion, rule.MiFecha, rule.FechaDocumentoComparar, rule.CantidadDias, rule.CantidadHoras, rule.FechaAComparar) Then
'                            SL.Add(TR)
'                        End If
'                    Catch ex As Exception
'                        Dim exn As New Exception("Error en IfDates.ValidateFilter(...), excepción" & ex.ToString)
'                       zamba.core.zclass.raiseerror(ex)
'                    End Try
'                Next
'            Case Result.DocumentDates.Edicion
'                For Each TR As TaskResult In results
'                    Try
'                        If validarFecha(TR.CreateDate, rule.Comparador, TR, rule.TipoComparacion, rule.MiFecha, rule.FechaDocumentoComparar, rule.CantidadDias, rule.CantidadHoras, rule.FechaAComparar) Then
'                            SL.Add(TR)
'                        End If
'                    Catch ex As Exception
'                        Dim exn As New Exception("Error en IfDates.ValidateFilter(...), excepción" & ex.ToString)
'                       zamba.core.zclass.raiseerror(ex)
'                    End Try
'                Next
'            Case Result.DocumentDates.Entrada
'                For Each TR As TaskResult In results
'                    Try
'                        If validarFecha(TR.CreateDate, rule.Comparador, TR, rule.TipoComparacion, rule.MiFecha, rule.FechaDocumentoComparar, rule.CantidadDias, rule.CantidadHoras, rule.FechaAComparar) Then
'                            SL.Add(TR)
'                        End If
'                    Catch ex As Exception
'                        Dim exn As New Exception("Error en IfDates.ValidateFilter(...), excepción" & ex.ToString)
'                       zamba.core.zclass.raiseerror(ex)
'                    End Try
'                Next
'            Case Result.DocumentDates.Salida
'                For Each TR As TaskResult In results
'                    Try
'                        If validarFecha(TR.CreateDate, rule.Comparador, TR, rule.TipoComparacion, rule.MiFecha, rule.FechaDocumentoComparar, rule.CantidadDias, rule.CantidadHoras, rule.FechaAComparar) Then
'                            SL.Add(TR)
'                        End If
'                    Catch ex As Exception
'                        Dim exn As New Exception("Error en IfDates.ValidateFilter(...), excepción" & ex.ToString)
'                       zamba.core.zclass.raiseerror(ex)
'                    End Try
'                Next
'        End Select
'    Catch ex As Exception
'        Dim exn As New Exception("Error (2) en IfDates.ValidateFilter(...), excepción" & ex.ToString)
'       zamba.core.zclass.raiseerror(ex)
'    End Try
'    Return SL
'End Function

'Private Function validarFecha(ByVal FechaDoc As DateTime, ByVal Comparador As Comparadores, ByVal FComparativa As DateTime) As Boolean
'    Select Case Comparador
'        Case Comparadores.Distinto
'            Return FechaDoc <> FComparativa
'        Case Comparadores.Igual
'            Return FechaDoc = FComparativa
'        Case Comparadores.IgualMenor
'            Return FechaDoc.CompareTo(FComparativa) <= 0
'        Case Comparadores.IgualMayor
'            Return FechaDoc.CompareTo(FComparativa) >= 0
'        Case Comparadores.Menor
'            Return FechaDoc.CompareTo(FComparativa) < 0
'        Case Comparadores.Mayor
'            Return FechaDoc.CompareTo(FComparativa) > 0
'    End Select
'End Function
'Private Function validarFecha(ByVal FechaDoc As DateTime, ByVal Comparador As Comparadores, ByVal TR As TaskResult, ByVal TipoComp As TipoComparaciones, ByVal FechaTarea As TaskResult.DocumentDates, ByVal FechaComp As TaskResult.DocumentDates, ByVal dias As Int32, ByVal horas As Int32, ByVal FechaFija As DateTime) As Boolean
'    Select Case TipoComp
'        Case TipoComparaciones.Dias
'            Return validarFecha(FechaDoc, Comparador, DateTime.Now.AddDays(dias))
'        Case TipoComparaciones.Especifica
'            Dim Fecha As Date
'            Select Case FechaComp
'                Case Result.DocumentDates.Creacion
'                    Fecha = TR.CreateDate
'                Case Result.DocumentDates.Edicion
'                    Fecha = TR.EditDate
'                Case Result.DocumentDates.Entrada
'                    Fecha = TR.AsignedDate
'                Case Result.DocumentDates.Salida
'                    Fecha = TR.ExpireDate
'            End Select
'            Return validarFecha(FechaDoc, Comparador, Fecha)
'        Case TipoComparaciones.Fija
'            Return validarFecha(FechaDoc, Comparador, FechaFija)
'        Case TipoComparaciones.Horas
'            Return validarFecha(FechaDoc, Comparador, DateTime.Now.AddHours(horas))
'    End Select
'End Function
