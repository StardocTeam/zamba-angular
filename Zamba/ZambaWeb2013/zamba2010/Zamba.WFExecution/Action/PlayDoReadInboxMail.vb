Imports Zamba.Pop3Utilities
Imports Zamba.Core.Pop3Utilities
Imports System.IO
Imports System.Windows.Forms
Imports System.Text

Public Class PlayDoReadInboxMail
    Private _myRule As IDoReadInboxMail
    Private _client As ZPop3Client

    Sub New(ByVal rule As IDoReadInboxMail)
        _myRule = rule
    End Sub

    Private Const MAIL_EXPORT_TEMPLATE_LOCATION = "RuleExportTemplates\Rule_ExportMail.html"

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim params As New Hashtable
        Dim obteinedMails As List(Of IDownloadedEmailHeader)
        Dim exportFolder As DirectoryInfo

        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Entrando en ejecucion de DoReadInboxMail")

            _myRule = DiscoverRuleParams(_myRule, results(0))

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Instanciando Pop3 Client")
            _client = New ZPop3Client(_myRule.Pop3Server, _myRule.Pop3Port, _myRule.Pop3User, _myRule.Pop3Password, _myRule.Pop3EnableSSL)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Pop3 Client instanciado, conectando")
            _client.Connect()
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Conectado")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo lista de emails")

            obteinedMails = New List(Of IDownloadedEmailHeader)()
            obteinedMails = _client.FetchEmailList(Date.Parse(_myRule.StartDate), Date.Parse(_myRule.EndDate))

            If obteinedMails IsNot Nothing AndAlso obteinedMails.Count > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de mails obtenidos: " & obteinedMails.Count)
                exportFolder = New DirectoryInfo(_myRule.PathToExport)

                If exportFolder.Exists Then
                    ExportMails(obteinedMails, exportFolder)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No existe la ruta de exportacion de mails")
                End If

                VariablesInterReglas.Add(_myRule.Zvarname, obteinedMails, False)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontraron mails para ese rango de fechas")
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Terminando ejecucion de DoReadInboxMail")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            params.Clear()
            params = Nothing
            _client.Dispose()
            _client = Nothing
            obteinedMails = Nothing
            exportFolder = Nothing
        End Try

        Return results
    End Function

    Private Function DiscoverRuleParams(ByVal ruleToDiscover As IDoReadInboxMail, ByVal res As ITaskResult) As IDoReadInboxMail
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo variables de regla")

        Dim ruleToReturn As IDoReadInboxMail = ruleToDiscover

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo Pop3Server")
        ruleToReturn.Pop3Server = WFRuleParent.ReconocerVariables(ruleToReturn.Pop3Server)
        ruleToReturn.Pop3Server = TextoInteligente.ReconocerCodigo(ruleToReturn.Pop3Server, res).Trim()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & ruleToReturn.Pop3Server)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo Pop3Port")
        ruleToReturn.Pop3Port = WFRuleParent.ReconocerVariables(ruleToReturn.Pop3Port)
        ruleToReturn.Pop3Port = TextoInteligente.ReconocerCodigo(ruleToReturn.Pop3Port, res).Trim()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & ruleToReturn.Pop3Port)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo Pop3User")
        ruleToReturn.Pop3User = WFRuleParent.ReconocerVariables(ruleToReturn.Pop3User)
        ruleToReturn.Pop3User = TextoInteligente.ReconocerCodigo(ruleToReturn.Pop3User, res).Trim()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & ruleToReturn.Pop3User)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo pathToExport")
        ruleToReturn.PathToExport = WFRuleParent.ReconocerVariables(ruleToReturn.PathToExport)
        ruleToReturn.PathToExport = TextoInteligente.ReconocerCodigo(ruleToReturn.PathToExport, res).Trim()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & ruleToReturn.PathToExport)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo zvarname")
        ruleToReturn.Zvarname = WFRuleParent.ReconocerVariables(ruleToReturn.Zvarname)
        ruleToReturn.Zvarname = TextoInteligente.ReconocerCodigo(ruleToReturn.Zvarname, res).Trim()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & ruleToReturn.Zvarname)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo startDate")
        ruleToReturn.StartDate = WFRuleParent.ReconocerVariables(ruleToReturn.StartDate)
        ruleToReturn.StartDate = TextoInteligente.ReconocerCodigo(ruleToReturn.StartDate, res).Trim()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & ruleToReturn.StartDate)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo endDate")
        ruleToReturn.EndDate = WFRuleParent.ReconocerVariables(ruleToReturn.EndDate)
        ruleToReturn.EndDate = TextoInteligente.ReconocerCodigo(ruleToReturn.EndDate, res).Trim()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & ruleToReturn.EndDate)

        Return ruleToReturn
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Private Sub ExportMails(ByVal obteinedMails As List(Of IDownloadedEmailHeader), ByVal exportFolder As DirectoryInfo)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Exportando mails a HTML")

        Dim sr As StreamReader
        Dim sw As StreamWriter
        Dim fiTemplate As FileInfo
        Dim fiExport As FileInfo
        Dim templateText As String
        Dim replacedText As String
        Dim mailBody As IDownloadedEmailBody
        Dim fileM As FileMode

        Try
            fiTemplate = New FileInfo(Path.Combine(Application.StartupPath, MAIL_EXPORT_TEMPLATE_LOCATION))
            If fiTemplate.Exists Then
                sr = New StreamReader(fiTemplate.FullName)
                templateText = sr.ReadToEnd()
                sr.Close()

                Dim max As Integer = obteinedMails.Count - 1
                For i As Integer = 0 To max
                    replacedText = templateText.Replace("ZMail_Id", obteinedMails(i).EmailId)
                    replacedText = replacedText.Replace("ZMail_Date", obteinedMails(i).UtcDateTime)
                    replacedText = replacedText.Replace("ZMail_From", obteinedMails(i).From)
                    replacedText = replacedText.Replace("ZMail_Subject", obteinedMails(i).Subject)

                    fiExport = New FileInfo(Path.Combine(exportFolder.FullName, obteinedMails(i).EmailId & ".html"))
                    mailBody = _client.GetDecodedMailBody(obteinedMails(i).EmailId)

                    replacedText = replacedText.Replace("ZMail_Body", mailBody.MessageText)

                    If Not fiExport.Exists Then
                        fileM = FileMode.CreateNew
                    Else
                        fileM = FileMode.Create
                    End If

                    sw = New StreamWriter(File.Open(fiExport.FullName, fileM), Encoding.GetEncoding(mailBody.Charset))
                    sw.WriteLine(replacedText)
                    sw.Close()
                Next
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontro el template para exportar los mails")
            End If
        Finally
            fiTemplate = Nothing
            fiExport = Nothing
            If sr IsNot Nothing Then
                sr.Dispose()
                sr = Nothing
            End If
            If sw IsNot Nothing Then
                sw.Dispose()
                sw = Nothing
            End If
            templateText = String.Empty
            templateText = Nothing
            replacedText = String.Empty
            replacedText = Nothing
            mailBody = Nothing
        End Try
    End Sub

End Class