Imports Zamba.FormBrowser
Imports Zamba.Membership
Imports Zamba.FileTools
Imports System.Text
Imports System.Threading
Imports System.IO
Imports Zamba.HTMLToPDFConverter

Public Class PlayDoGenerateHTMLReport
    Private _myRule As IDoGenerateHTMLReport

    Sub New(ByVal rule As IDoGenerateHTMLReport)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return PlayWeb(results, Nothing)
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim converter As New HTMLToPDFPechkin()
        Dim fb As FormBrowserController = Nothing
        Dim varInterReglas As New VariablesInterReglas()

        Try
            If IsNothing(Params) Then
                Params = New Hashtable
            End If

            Dim reportFolderTempPath As String = MembershipHelper.AppTempDir("\ReportTemp\").FullName
            Dim reportUrl As String = MembershipHelper.AppUrl & "/Log/ReportTemp/"

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "reportFolderTempPath: " + reportFolderTempPath + " reportUrl:" + reportUrl)

            Dim reportFileName As String
            Dim reportHTML As String
            Dim exportReportParams As Object()

            For Each taskResult As Core.TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & taskResult.Name)

                'Se obtiene el HTML del formulario
                fb = New FormBrowserController(taskResult, _myRule.FormId, Zamba.Membership.MembershipHelper.CurrentUser)
                reportHTML = fb.RenderForm(Helpers.HTMLSection.ALL)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Form Rendered ")

                'Se reemplazan las variables existentes que el HTML contenga
                reportHTML = TextoInteligente.ReconocerCodigo(reportHTML, taskResult)
                reportHTML = varInterReglas.ReconocerVariablesValuesSoloTexto(reportHTML)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Form Variables Reconocidas ")

                'Se genera el nombre del archivo PDF
                If String.IsNullOrEmpty(_myRule.ReportName) Then
                    reportFileName = "Report_" & DateTime.Now.Ticks & ".pdf"
                Else
                    reportFileName = TextoInteligente.ReconocerCodigo(_myRule.ReportName, taskResult)
                    reportFileName = varInterReglas.ReconocerVariablesValuesSoloTexto(reportFileName)
                    reportFileName &= DateTime.Now.Ticks & ".pdf"
                End If

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "reportFileName: " + reportFileName)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "reportFileDirectory: " + reportFolderTempPath)

                'Se aplica la conversión de HTML a PDF
                converter.ConvertCode(reportHTML, reportFolderTempPath + reportFileName, _myRule.ReportOrietation, MembershipHelper.AppUrl)

                'Se guarda la ruta del PDF accesible por URL
                Params.Add("url", reportUrl & reportFileName)
            Next

        Finally
            converter = Nothing
            fb = Nothing
            varInterReglas = Nothing
        End Try

        Return results

    End Function
End Class
