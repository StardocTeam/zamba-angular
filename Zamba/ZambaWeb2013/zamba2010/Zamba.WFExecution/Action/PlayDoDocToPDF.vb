Imports System.IO
Imports System.Windows.Forms

Public Class PlayDoDocToPDF

    Private _myRule As IDoDocToPDF

    Sub New(ByVal rule As IDoDocToPDF)
        _myRule = rule
    End Sub

    Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Dim fileToPrint As String
        Dim fileName As String
        Dim ZOPTB As New ZOptBusiness

        Dim printer As String = ZOPTB.GetValue("PDFPrinter")
        ZOPTB = Nothing
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Play de la DoODocToPDF")

        If _myRule.UseNewConversion = True Or Not String.IsNullOrEmpty(printer) Then
            Dim resCount As Integer = results.Count

            If (resCount > 0) Then
                Dim r As IResult
                Dim varInterReglas As New VariablesInterReglas()

                Try
                    For i As Integer = 0 To resCount - 1
                        r = results(i)

                        If Me._myRule.ExportTask Then
                            'ver cuando el volumen es -2
                            fileToPrint = r.RealFullPath
                        Else
                            fileToPrint = TextoInteligente.ReconocerCodigo(Me._myRule.FullPath, r)
                            fileToPrint = varInterReglas.ReconocerVariablesValuesSoloTexto(fileToPrint)
                        End If

                        fileName = TextoInteligente.ReconocerCodigo(Me._myRule.FileName, r)
                        fileName = varInterReglas.ReconocerVariablesValuesSoloTexto(fileName).Trim()

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo para convertir a PDF: " & fileToPrint)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo PDF destino: " & fileName)

                        Dim FI As New FileInfo(fileToPrint)

                        If FI.Exists AndAlso (FI.Extension.ToLower.Contains("doc") OrElse FI.Extension.ToLower.Contains("dot") _
                                              OrElse FI.Extension.ToLower.Contains("docx") OrElse FI.Extension.ToLower.Contains("dotx")) Then
                            'hacer una copia temporal para poder cambiar el nombre del archivo
                            Dim pathDirectory As String = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp\PDFTemp").FullName

                            If Not Directory.Exists(pathDirectory) Then
                                Directory.CreateDirectory(pathDirectory)
                            End If

                            Dim newFile As String
                            If fileName.EndsWith(".pdf") Then
                                newFile = pathDirectory & "\" & fileName
                            Else
                                newFile = pathDirectory & "\" & fileName & ".pdf"
                            End If

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Borrando temporal: " & newFile)

                            If File.Exists(newFile) Then
                                File.Delete(newFile)
                            End If

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Convirtiendo a PDF el archivo temporal: " & newFile)

                          
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando PDF con Spire")
                                Dim st As New Zamba.FileTools.SpireTools()
                                st.ConvertWordToPDF(fileToPrint, newFile)
                                st = Nothing
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "PDF generado correctamente")

                                If (VariablesInterReglas.ContainsKey(fileName) = False) Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertando resultado en la colección VariablesInterReglas " & fileName)
                                    VariablesInterReglas.Add(fileName, newFile)
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando resultado en la colección VariablesInterReglas " & fileName)
                                    VariablesInterReglas.Item(fileName) = newFile
                                End If

                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al convertir a PDF, el archivo no existe o no es un documento de Word: " & fileToPrint)
                        End If

                        FI = Nothing
                    Next
                Finally
                    varInterReglas = Nothing
                End Try
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay tareas para convertir a PDF.")
            End If

        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha configurado la impresora de PDFs")
        End If

        Return results
    End Function

End Class
