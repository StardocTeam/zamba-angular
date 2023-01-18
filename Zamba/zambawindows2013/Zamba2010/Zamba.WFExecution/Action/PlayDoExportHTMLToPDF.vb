Imports System.IO
Imports System.Text
Imports System.Windows.Forms

Public Class PlayDoExportHTMLToPDF
    Private _myRule As IDoExportHTMLToPDF

    Public Sub New(ByVal rule As IDoExportHTMLToPDF)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim Content As String = _myRule.Content

        If Not String.IsNullOrEmpty(Content) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Contenido a cargar " & Content)

            Dim strTemp As String

            If Content.ToLower().Trim().StartsWith("<html") OrElse Content.ToLower().Trim().StartsWith("<!doctype") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El contenido es HTML.")
                strTemp = Content
            Else

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

            End If


            Dim Dir As New DirectoryInfo(Path.Combine(Membership.MembershipHelper.AppTempPath, "Temp"))
            If Dir.Exists = False Then
                Dir.Create()
            End If
            Dim pdffile As String = FileBusiness.GetUniqueFileName(Dir.FullName, _myRule.ReturnFileName, ".pdf")
            Dim header1 As String
            If Not String.IsNullOrEmpty(strTemp) Then
                If strTemp.ToLower().Trim().StartsWith("<html") OrElse strTemp.ToLower().Trim().StartsWith("<!doctype") Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Es un HTML completo, extrayendo contenido.")
                    If strTemp.Contains("<body>") Then
                        Dim separator() As String = {"<body>"}
                        header1 = strTemp.Split(separator, StringSplitOptions.RemoveEmptyEntries)(0)
                        strTemp = strTemp.Split(separator, StringSplitOptions.RemoveEmptyEntries)(1)
                    End If
                    If strTemp.Contains("</body>") Then
                        Dim separator2() As String = {"</body>"}
                        strTemp = strTemp.Split(separator2, StringSplitOptions.RemoveEmptyEntries)(0)
                    End If
                End If

                If (results IsNot Nothing AndAlso results.Count > 0 AndAlso results(0) IsNot Nothing) Then
                    strTemp = TextoInteligente.ReconocerCodigo(strTemp, results(0))
                End If
                strTemp = WFRuleParent.ReconocerVariablesYTablas(strTemp)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto: " + strTemp)

                Dim WebServer As String = String.Empty 'ZOptBusiness.GetValue("ThisDomain")
                If (WebServer.Length > 0) Then
                    WebServer = ""
                End If
                header1 = header1.Replace("""scripts/Zamba.js""", """" & WebServer & "scripts/Zamba.js""")
                Dim header As New StringBuilder
                header.Append(" <link rel = ""stylesheet"" href=""" & WebServer & "scripts/kendoui/styles/kendo.common.min.css"" /> ")
                header.Append(" <link rel = ""stylesheet"" href=""" & WebServer & "scripts/kendoui/styles/kendo.rtl.min.css"" /> ")
                header.Append(" <link rel = ""stylesheet"" href=""" & WebServer & "scripts/kendoui/styles/kendo.silver.min.css"" /> ")
                header.Append(" <link rel = ""stylesheet"" href=""" & WebServer & "scripts/kendoui/styles/kendo.default.min.css"" /> ")
                header.Append(" <link rel = ""stylesheet"" href=""" & WebServer & "scripts/kendoui/styles/kendo.dataviz.min.css"" /> ")
                header.Append(" <link rel = ""stylesheet"" href=""" & WebServer & "scripts/kendoui/styles/kendo.dataviz.default.min.css"" /> ")
                header.Append(" <link rel = ""stylesheet"" href=""" & WebServer & "scripts/kendoui/styles/kendo.mobile.all.min.css"" />")
                header.Append(" <script src = """ & WebServer & "Scripts/jquery-2.2.2.min.js"" ></script>")
                header.Append(" <script src = """ & WebServer & "Scripts/jquery-3.3.1.min.js"" ></script>")
                header.Append(" <script src = """ & WebServer & "scripts/kendoui/js/angular.min.js"" ></script>")
                header.Append(" <script src = """ & WebServer & "scripts/kendoui/js/jszip.min.js"" ></script>")
                header.Append(" <script src = """ & WebServer & "scripts/kendoui/js/kendo.all.min.js"" ></script>")
                header.Append(" <script src = """ & WebServer & "scripts/kendoui/js/kendo.grid.min.js"" ></script>")

                Dim tempfile As String = FileBusiness.GetUniqueFileName(Dir.FullName, _myRule.ReturnFileName, ".html")
                Dim sw As New StreamWriter(tempfile)
                sw.WriteLine(header1)
                sw.WriteLine("<body>")
                sw.WriteLine(header)
                sw.WriteLine(strTemp)
                sw.WriteLine("</body>")
                sw.WriteLine("</html>")

                sw.Flush()
                sw.Close()
                sw.Dispose()
                sw = Nothing

                Dim sp As New FileTools.SpireTools
                sp.ConvertHtmlToPDF(tempfile, pdffile)

            End If

            If (VariablesInterReglas.ContainsKey(_myRule.ReturnFileName) = False) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertando resultado en la colección VariablesInterReglas " & pdffile)
                VariablesInterReglas.Add(_myRule.ReturnFileName, pdffile, False)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando resultado en la colección VariablesInterReglas " & pdffile)
                VariablesInterReglas.Item(_myRule.ReturnFileName) = pdffile
            End If
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El contenido no se ha reconocido.")
        End If

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class