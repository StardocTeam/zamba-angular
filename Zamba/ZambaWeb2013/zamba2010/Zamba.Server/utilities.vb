Imports Zamba.Tools
Imports Zamba.AppBlock
Imports Zamba.Core

Public Class utilities

    Private SQLTrace As New SQLTrace
    Public Shared Event LogPerformanceIssue(subject As String, description As String)

    Public Shared Function Convert_Datetime(ByVal str As String) As String
        Dim pos As Integer = str.IndexOf("CONVERT", StringComparison.CurrentCultureIgnoreCase)
        Dim flag As Boolean = False
        Dim strFinal As String = str
        If pos > 0 Then
            flag = True
        Else
            flag = False
        End If
        'SACO LOS STRINGS ANTES Y DESPUES DEL CONVERT
        While flag
            Dim str1 As String = strFinal.Substring(0, pos)
            strFinal = strFinal.Remove(0, pos)
            Dim pos2 As Integer = strFinal.IndexOf(")")
            Dim str2 As String = strFinal.Substring(pos2, strFinal.Length - pos2)

            'SACO LA FECHA Y HORA
            Dim pos3 As Integer = strFinal.IndexOf("'", 0)
            Dim pos4 As Integer = strFinal.IndexOf("'", pos3 + 1)
            Dim date_hour As String = strFinal.Substring(pos3, pos4 - pos3)

            'CONVIERTO LA FECHA
            Dim anio As String = date_hour.Substring(1, 4)
            Dim mes As String = date_hour.Substring(6, 2)
            Dim dia As String = date_hour.Substring(9, 2)
            Select Case mes
                Case "01"
                    mes = "01"
                Case "02"
                    mes = "02"
                Case "03"
                    mes = "03"
                Case "04"
                    mes = "04"
                Case "05"
                    mes = "05"
                Case "06"
                    mes = "06"
                Case "07"
                    mes = "07"
                Case "08"
                    mes = "08"
                Case "09"
                    mes = "09"
                Case "10"
                    mes = "10"
                Case "11"
                    mes = "11"
                Case "12"
                    mes = "12"
            End Select
            Dim fecha As String = dia & "-" & mes & "-" & anio

            'ARMO EL STRING FINAL
            strFinal = String.Empty

            ''SACO LAS COMILLAS SIMPLES QUE QUEDARON EN EL STRING
            strFinal = str1 & "TO_DATE ('" & fecha & "', 'dd-MM-yyyy HH:MI:SS AM'" & str2
            pos = strFinal.IndexOf("CONVERT", StringComparison.CurrentCultureIgnoreCase)
            If pos > 0 Then
                flag = True
            Else
                flag = False
            End If
        End While
        Return strFinal
    End Function

    Public Function StringClean(ByVal str As String) As String
        Dim straux As String = String.Empty
        Dim i As Int16 = 0
        For i = 0 To str.Length
            If str.Chars(i) = "'"c Then
                straux = str.Chars(i)
            End If
            straux = str.Chars(i)
        Next
        Return straux
    End Function

    ''' <summary>
    ''' Log de Querys
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <history>
    '''     Javier  11/01/2011  Modified    Se lee config para saber si loguear querys o no (sale de app.ini)
    '''     Tomas   07/10/2011  Modified    Se modifica el encabezado del log de trace
    ''' </history>
    Private LogEnabled As Boolean = True
    Public Sub LogCommands(ByVal Text As String, ByVal Params As System.Data.IDataParameterCollection, ByVal duration As TimeSpan)
        Dim StrParams As String
        Dim querytime As Int64

        If (LogEnabled) Then
            Try

                LogEnabled = False
                If Not Params Is Nothing AndAlso Params.Count > 0 Then
                    Dim i As Int32
                    For i = 0 To Params.Count - 1
                        If i > 0 Then StrParams += ","
                        Dim Param As System.Data.IDataParameter
                        Param = DirectCast(Params(i), System.Data.IDataParameter)
                        If Not IsNothing(Param.Value) Then
                            StrParams += DirectCast(Params(i), System.Data.IDataParameter).Value.ToString
                        End If

                        Try
                            Dim p As IDbDataParameter = Params(i)
                            'check for derived output value with no value assigned
                            If p.Direction = ParameterDirection.InputOutput OrElse p.Direction = ParameterDirection.Output OrElse p.Direction = ParameterDirection.ReturnValue Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Valor Parametro de Salida {0} : {1} ", p.ParameterName, p.Value))
                                If VariablesInterReglas.ContainsKey(p.ParameterName) Then
                                    VariablesInterReglas.Item(p.ParameterName) = p.Value
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigno a la variable {0} el valor {}", p.ParameterName, p.Value))
                                Else
                                    VariablesInterReglas.Add(p.ParameterName, p.Value)
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigno a la variable {0} el valor {}", p.ParameterName, p.Value))
                                End If
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                End If

                querytime = ((duration.Minutes * 60 * 1000) + (duration.Seconds * 1000) + duration.Milliseconds)

                If Not Text.Contains("ZDoSearchResults") Then
                    SQLTrace.Write(DateTime.Now.ToString("dd/MM/yy HH:mm:ss") & vbTab & querytime & vbTab & Text & " " & StrParams)

                    If querytime > 1500 Then
                        Dim subject As String = "Una consulta SQL ha demorado " & querytime & " milisegundos"
                        Dim description As String = String.Format("La consulta ha demorado {0} milisegundos: {1}", querytime, vbCrLf & Text & " " & StrParams)
                        ZClass.raiseerror(New Exception(description))
                        RaiseEvent LogPerformanceIssue(subject, description)
                    End If
                End If

            Catch ex As Exception
            Finally
                LogEnabled = True
            End Try
        End If
    End Sub



End Class