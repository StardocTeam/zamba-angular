Imports System.IO
Imports System.Windows.Forms
Imports Zamba.Core

Public Class PlayDoDocToPDF

    Private _myRule As IDoDocToPDF

    Sub New(ByVal rule As IDoDocToPDF)
        _myRule = rule
    End Sub

    Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Dim fileToPrint As String
        Dim fileName As String
        Dim printer As String = ZOptBusiness.GetValue("PDFPrinter")

        Trace.WriteLineIf(ZTrace.IsInfo, "Play de la DoODocToPDF")

        If _myRule.UseNewConversion = True Or Not String.IsNullOrEmpty(printer) Then
            Dim resCount As Integer = results.Count

            If (resCount > 0) Then
                Dim r As IResult

                For i As Integer = 0 To resCount - 1
                    r = results(i)

                    If Me._myRule.ExportTask Then
                        If r.Disk_Group_Id <> 0 AndAlso VolumesBusiness.GetVolumeType(r.Disk_Group_Id) = VolumeTypes.DataBase Then
                            fileToPrint = Results_Business.GetDBTempFile(r)
                        Else
                            fileToPrint = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp\PDFTemp").FullName + r.File
                        End If
                    Else
                        fileToPrint = TextoInteligente.ReconocerCodigo(Me._myRule.FullPath, r)
                        fileToPrint = WFRuleParent.ReconocerVariablesValuesSoloTexto(fileToPrint)
                    End If

                    fileName = TextoInteligente.ReconocerCodigo(Me._myRule.FileName, r)
                    fileName = WFRuleParent.ReconocerVariablesValuesSoloTexto(fileName).Trim()

                    Trace.WriteLineIf(ZTrace.IsInfo, "Archivo para convertir a PDF: " & fileToPrint)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Archivo PDF destino: " & fileName)

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

                        Trace.WriteLineIf(ZTrace.IsInfo, "Borrando temporal: " & newFile)

                        If File.Exists(newFile) Then
                            File.Delete(newFile)
                        End If

                        Trace.WriteLineIf(ZTrace.IsInfo, "Convirtiendo a PDF el archivo temporal: " & newFile)

                        If _myRule.UseNewConversion = False Then
                            File.Copy(fileToPrint, newFile)

                            Dim w As New Zamba.Office.WordInterop()

                            Try
                                w.Print(newFile, printer)
                                File.Delete(newFile)
                            Catch ex As Exception
                                Trace.WriteLineIf(ZTrace.IsInfo, "Ocurrio un error al imprimir, archivo temporal: " & newFile)
                                Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
                                ZClass.raiseerror(ex)
                            Finally
                                w = Nothing
                            End Try
                        Else
                            Trace.WriteLineIf(ZTrace.IsInfo, "Generando PDF con Spire")
                            Dim st As New Zamba.FileTools.SpireTools()
                            st.ConvertWordToPDF(fileToPrint, newFile)
                            st = Nothing
                            Trace.WriteLineIf(ZTrace.IsInfo, "PDF generado correctamente")

                            If (VariablesInterReglas.ContainsKey(fileName) = False) Then
                                Trace.WriteLineIf(ZTrace.IsInfo, "Insertando resultado en la colección VariablesInterReglas " & fileName)
                                VariablesInterReglas.Add(fileName, newFile, False)
                            Else
                                Trace.WriteLineIf(ZTrace.IsInfo, "Actualizando resultado en la colección VariablesInterReglas " & fileName)
                                VariablesInterReglas.Item(fileName) = newFile
                            End If
                        End If
                    Else
                        Trace.WriteLineIf(ZTrace.IsInfo, "Error al convertir a PDF, el archivo no existe o no es un documento de Word: " & fileToPrint)
                    End If

                    FI = Nothing
                Next
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "No hay tareas para convertir a PDF.")
            End If

        Else
            Trace.WriteLineIf(ZTrace.IsInfo, "No se ha configurado la impresora de PDFs")
        End If

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class