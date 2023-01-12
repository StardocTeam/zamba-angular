Imports Zamba.Core.WF.WF
Imports System.IO

Public Class PlayDoExportHTMLToPDF
    Private _myRule As IDoExportHTMLToPDF

    Public Sub New(ByVal rule As IDoExportHTMLToPDF)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return PlayWeb(results, Nothing)
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByRef params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim Content As String = _myRule.Content

        If Not String.IsNullOrEmpty(Content) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Contenido a cargar " & Content)
            Dim VarInterReglas As New VariablesInterReglas()
            Content = VarInterReglas.ReconocerVariables(Content)
            VarInterReglas = Nothing
            If Not IsNothing(Content) Then
                Content = Zamba.Core.TextoInteligente.ReconocerCodigo(Content, results(0))
            End If

            Dim strTemp As String

            If Content.StartsWith("<html") OrElse Content.StartsWith("<HTML") OrElse Content.StartsWith("<!DOCTYPE") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El contenido es HTML.")
                strTemp = Content
            Else
                Try
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El contenido no es HTML, validando path.")
                    Dim fInfo As New FileInfo(Content)
                    If fInfo.Exists Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El path es válido, leyendo el archivo.")

                        Dim reader As New StreamReader(Content)
                        strTemp = reader.ReadToEnd()
                        If Not String.IsNullOrEmpty(strTemp) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo leído con éxito.")
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo se encuentra vacío.")
                        End If
                    
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo a cargar no existe")
                    End If
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al leer archivo:" & ex.Message)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se cargará nada en el params.")
                    ZCore.raiseerror(ex)
                End Try
            End If

            If Not String.IsNullOrEmpty(strTemp) Then
                If strTemp.StartsWith("<HTML") OrElse strTemp.StartsWith("<!DOCTYPE") Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Es un HTML completo, extrayendo contenido.")
                    If strTemp.Contains("<body>") Then
                        Dim separator() As String = {"<body>"}
                        strTemp = strTemp.Split(separator, StringSplitOptions.RemoveEmptyEntries)(1)
                    End If
                    If strTemp.Contains("</body>") Then
                        Dim separator2() As String = {"</body>"}
                        strTemp = strTemp.Split(separator2, StringSplitOptions.RemoveEmptyEntries)(0)
                    End If
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto: " + strTemp)
                params.Add("Content", strTemp)
            End If

            If Not String.IsNullOrEmpty(_myRule.ReturnFileName) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo de retorno: " & _myRule.ReturnFileName)
                params.Add("ReturnFileName", _myRule.ReturnFileName)
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Es editable: " & _myRule.CanEditable.ToString())
            params.Add("CanEditable", _myRule.CanEditable)
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El contenido no se ha reconocido.")
        End If

        Return results
    End Function
End Class