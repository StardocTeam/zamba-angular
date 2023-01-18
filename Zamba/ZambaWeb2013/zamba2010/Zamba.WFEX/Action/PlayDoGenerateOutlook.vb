Imports Zamba.Core
Imports System.Windows.Forms
Imports System.IO
Imports Zamba.Viewers
Imports Zamba.Office
Imports Zamba.OfficeCommon
Imports Zamba.Office.Outlook

Public Class PlayDoGenerateOutlook

    Private _myRule As IDOGenerateOutlook
    Private mails As SortedList
    Private resultsAux As System.Collections.Generic.List(Of Core.ITaskResult)
    Private counter As Integer = 0
    Private Params As Hashtable
    Private resultAux As TaskResult
    Private Body As String
    Private Asunto As String
    Private Para As String
    Private CC As String
    Private CCO As String
    Private link As ArrayList
    Private para2 As String
    Private R As String
    Private _smtpConfig As Hashtable
    Private replyMsgPath As String

    Sub New(ByVal rule As IDOGenerateOutlook)
        Me._myRule = rule
    End Sub


    ''' <summary>
    ''' Play de la regla DOGenerateOutlook
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myrule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try

            Trace.WriteLineIf(ZTrace.IsInfo, "Play de la DOGenerateOutlook")
            Me.mails = New SortedList()
            Me.resultsAux = New System.Collections.Generic.List(Of Core.ITaskResult)
            Me.Params = New Hashtable()
            Me.link = New ArrayList()

            Try
                ' [AlejandroR] - 05/03/2010 - Created
                ' Se chequea antes de enviar el mail que se tenga configurada y con acceso
                ' la ruta para el historial de emails, si falla se cancela la regla
                MessagesBusiness.CheckHistoryExportPath()
            Catch ex As Exception
                If Not Me._myRule.automaticSend Then
                    MessageBox.Show(ex.Message, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                raiseerror(ex)
                Throw ex
            End Try

            If Me._myRule.ReplyMail Then
                Trace.WriteLineIf(ZTrace.IsInfo, "Generando un mail de respuesta...")
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "Generando un mail nuevo...")
            End If


            Me.resultsAux.Clear()
            Me.counter = 0

            For Each r As TaskResult In results
                Me.Params.Clear()
                Me.Params = Me.ReemplazarVariables(r)
                Trace.WriteLineIf(ZTrace.IsInfo, "Variables reemplazadas")
                Me.resultAux = results(Me.counter)

                isSendDocument(Me.resultAux)
                Trace.WriteLineIf(ZTrace.IsInfo, "Disparando Evento")

                Dim SM As New OutlookMailActions(Me._smtpConfig)
                SM.SendMail(ResultActions.EnvioDeMail, Me.resultAux, Me.Params)
                'Result.HandleModule(ResultActions.EnvioDeMail, Me.resultAux, Me.Params)
                Trace.WriteLineIf(ZTrace.IsInfo, "Fin Evento")
                ' Se agregan a la colección resultsAux las tareas cuyo form. de mail fue cancelado o cuyo correo no pudo ser envíado
                If (Me.resultAux.PrintName = "Cancel") Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error en el envío del mail.")
                    Trace.WriteLineIf(ZTrace.IsInfo, "Cancelado")
                    Me.resultsAux.Add(r)
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "Mail enviado con éxito.")
                End If

                Me.counter = Me.counter + 1
            Next

            ' Se eliminan de la colección results las tareas cuyo form. de mail fue cancelado o cuyo correo no pudo ser envíado. De esta
            ' forma no se ejecutarán las reglas hijas para esas tareas
            For Each r As TaskResult In Me.resultsAux
                If UserPreferences.getValue("CancelMailCancelWF", Sections.WorkFlow, "False") Then
                    results.Remove(r)
                    Results_Business.Delete(r, False, False)
                End If
            Next


        Finally
            Me.mails = Nothing
            Me.resultsAux = Nothing
            Me.counter = 0
            Me.Params = Nothing
            Me.resultAux = Nothing
            Me.Body = Nothing
            Me.Asunto = Nothing
            Me.Para = Nothing
            Me.CC = Nothing
            Me.CCO = Nothing
            Me.para2 = Nothing
            Me.R = Nothing
            Me.link = Nothing
            If Not Me._smtpConfig Is Nothing Then
                Me._smtpConfig.Clear()
                Me._smtpConfig = Nothing
            End If
            Me.replyMsgPath = Nothing
        End Try

        Return (results)
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    ''' <summary>
    ''' Método que pregunta si la regla tiene o no el envío de documentos. Si es, es "conAttach", sino "sinAttach"
    ''' </summary>
    ''' <param name="result"></param>
    ''' <param name="myrule"></param>

    Private Sub isSendDocument(ByRef result As TaskResult)

        If (Me._myRule.SendDocument) Then
            result.AutoName = "conAttach"
        Else
            'Result.HandleModule(ResultActions.EnvioDeMail, Nothing, Params)
            result.AutoName = "sinAttach"
        End If

    End Sub

    Public Function ReemplazarVariables(ByVal res As TaskResult) As Hashtable
        Me.Body = String.Empty
        Me.Asunto = String.Empty
        Para = String.Empty
        CC = String.Empty
        CCO = String.Empty
        Me.link = New ArrayList
        Dim AssociatedResults As New List(Of IResult) ' = New List(Of IResult)()
        Dim PathImages() As String = {}
        Try
            Try
                Dim R As String = String.Empty
                Me.Asunto = Me._myRule.Asunto
                Trace.WriteLineIf(ZTrace.IsInfo, "Reconociendo Asunto: " & Me.Asunto)
                Me.Asunto = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.Asunto, res)
                Trace.WriteLineIf(ZTrace.IsInfo, "Asunto1: " & Me.Asunto)

                Dim ValorVariable As Object
                Dim Variable As String = WFRuleParent.ObtenerNombreVariable(Me.Asunto)
                ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Me.Asunto)

                If IsNothing(ValorVariable) = False Then
                    If (TypeOf (ValorVariable) Is DataSet) Then
                        For Each DR As DataRow In ValorVariable.tables(0).rows
                            R &= DR.Item(0) & ","
                        Next
                    End If
                    Me.Asunto = Me.Asunto.Replace("zvar(" & Variable & ")", R)
                End If
                Trace.WriteLineIf(ZTrace.IsInfo, "Asunto2: " & Me.Asunto)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                ZClass.raiseerror(ex)
            End Try

            Me.Para = obtenerPara(res)
            Trace.WriteLineIf(ZTrace.IsInfo, "Para: " & Me.Para)
            Try
                Dim R As String
                Me.CC = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.CC, res)
                Trace.WriteLineIf(ZTrace.IsInfo, "CC1: " & Me.CC)

                Dim ValorVariable As Object
                Dim Variable As String = WFRuleParent.ObtenerNombreVariable(Me._myRule.CC)
                ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Me._myRule.CC)


                If IsNothing(ValorVariable) = False Then
                    If TypeOf (ValorVariable) Is DataSet Then
                        Dim ds As DataSet = ValorVariable
                        If (ds.Tables.Count > 0) Then
                            If ds.Tables(0).Rows.Count Then
                                R = ds.Tables(0).Rows(0)(0).ToString()
                                Me.CC = Me.CC.Replace("zvar(" & Variable & ")", R)
                            End If
                        End If
                    ElseIf IsNumeric(ValorVariable) Then
                        Dim u As New User(ValorVariable)
                        Zamba.Core.UserBusiness.Mail.FillUserMailConfig(u)
                        R = u.eMail.Mail
                        Me.CC = Me.CC.Replace("zvar(" & Variable & ")", R)
                    ElseIf TypeOf (ValorVariable) Is String Then
                        If Not ValorVariable.ToString.Contains(".") AndAlso Not ValorVariable.ToString.Contains("@") Then
                            'Id De Usuario
                            Dim uId As Int32 = Zamba.Core.UserBusiness.GetUserID(ValorVariable)
                            Dim u As New User(uId)
                            Zamba.Core.UserBusiness.Mail.FillUserMailConfig(u)
                            R = u.eMail.Mail
                            Me.CC = Me.CC.Replace("zvar(" & Variable & ")", R)
                        Else
                            'Es una direccion de mail
                            R = ValorVariable.ToString
                            Me.CC = Me.CC.Replace("zvar(" & Variable & ")", R)
                        End If
                    End If
                End If
                Trace.WriteLineIf(ZTrace.IsInfo, "Reconociendo CC2: " & Me.CC)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                ZClass.raiseerror(ex)
            End Try

            Try
                Dim R As String
                Me.CCO = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.CCO, res)
                Trace.WriteLineIf(ZTrace.IsInfo, "Reconociendo CCO1: " & Me.CCO)

                Dim ValorVariable As Object
                Dim Variable As String = WFRuleParent.ObtenerNombreVariable(Me._myRule.CCO)
                ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Me._myRule.CCO)

                If IsNothing(ValorVariable) = False Then
                    If TypeOf (ValorVariable) Is DataSet Then
                        Dim ds As DataSet = ValorVariable
                        If (ds.Tables.Count > 0) Then
                            If ds.Tables(0).Rows.Count Then
                                R = ds.Tables(0).Rows(0)(0).ToString()
                                Me.CCO = Me.CCO.Replace("zvar(" & Variable & ")", R)
                            End If
                        End If
                    ElseIf IsNumeric(ValorVariable) Then
                        Dim u As New User(ValorVariable)
                        Zamba.Core.UserBusiness.Mail.FillUserMailConfig(u)
                        R = u.eMail.Mail
                        Me.CCO = Me.CCO.Replace("zvar(" & Variable & ")", R)
                    ElseIf TypeOf (ValorVariable) Is String Then
                        If Not ValorVariable.ToString.Contains(".") AndAlso Not ValorVariable.ToString.Contains("@") Then
                            'Id De Usuario
                            Dim uId As Int32 = Zamba.Core.UserBusiness.GetUserID(ValorVariable)
                            Dim u As New User(uId)
                            Zamba.Core.UserBusiness.Mail.FillUserMailConfig(u)
                            R = u.eMail.Mail
                            Me.CCO = Me.CCO.Replace("zvar(" & Variable & ")", R)
                        Else
                            'Es una direccion de mail
                            R = ValorVariable.ToString
                            Me.CCO = Me.CCO.Replace("zvar(" & Variable & ")", R)
                        End If
                    End If
                End If
                Trace.WriteLineIf(ZTrace.IsInfo, "Reconociendo CCO2: " & Me.CCO)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                ZClass.raiseerror(ex)
            End Try

            Try
                Me.Body = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Body, res)
                Trace.WriteLineIf(ZTrace.IsInfo, "Reconociendo Body1: " & Me.Body)
                Me.Body = WFRuleParent.ReconocerVariables(Me.Body)
                Trace.WriteLineIf(ZTrace.IsInfo, "Reconociendo Body2: " & Me.Body)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                ZClass.raiseerror(ex)
            End Try

            Try

                Me.link.Add(Me._myRule.AttachLink)
                'link.Add("Zamba:\\DT=" & res.DocTypeId & "&DOCID=" & res.ID)
                Me.link.Add("Zamba:\\TaskID=" & res.TaskId.ToString)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                ZClass.raiseerror(ex)
            End Try

            'realiza este metodo de forma mas completa.


            Try
                If String.IsNullOrEmpty(Me._myRule.PathImages) = False Then
                    PathImages = Split(Me._myRule.PathImages, ";")
                End If
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                ZClass.raiseerror(ex)
            End Try

            If Me._myRule.ReplyMail AndAlso Not String.IsNullOrEmpty(Me._myRule.ReplyMsgPath) Then
                Try
                    Me.replyMsgPath = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.ReplyMsgPath, res)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Reconociendo ReplyMsgPath 1: " & Me.replyMsgPath)
                    Me.replyMsgPath = WFRuleParent.ReconocerVariables(Me.replyMsgPath)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Reconociendo ReplyMsgPath 2: " & Me.replyMsgPath)

                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                    ZClass.raiseerror(ex)
                    Me.replyMsgPath = res.FullPath
                End Try
            Else
                Me.replyMsgPath = String.Empty
            End If
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            ZClass.raiseerror(ex)
        End Try

        Dim Params As New Hashtable
        Trace.WriteLineIf(ZTrace.IsInfo, "DOGenerateOutlook To: " & Me.Para)
        Params.Add(OutlookMailParameters.TO, Me.Para)
        Trace.WriteLineIf(ZTrace.IsInfo, "DOGenerateOutlook CC: " & Me.CC)
        Params.Add(OutlookMailParameters.CC, Me.CC)
        Trace.WriteLineIf(ZTrace.IsInfo, "DOGenerateOutlook CCO: " & Me.CCO)
        Params.Add(OutlookMailParameters.BCC, Me.CCO)
        Trace.WriteLineIf(ZTrace.IsInfo, "DOGenerateOutlook Subject: " & Me.Asunto)
        Params.Add(OutlookMailParameters.SUBJECT, Me.Asunto)
        Trace.WriteLineIf(ZTrace.IsInfo, "DOGenerateOutlook Body: " & Me.Body)
        Params.Add(OutlookMailParameters.HTML_BODY, Me.Body)
        Trace.WriteLineIf(ZTrace.IsInfo, "DOGenerateOutlook Imagenes: " & Me._myRule.PathImages)
        Params.Add(OutlookMailParameters.ATTACH_PATHS, PathImages)
        Params.Add(OutlookMailParameters.LINK, Me.link)
        Params.Add(OutlookMailParameters.SEND_TIMEOUT, Me._myRule.sendTimeOut)
        Params.Add("AutomaticSend", Me._myRule.automaticSend)
        Params.Add("ReplyMail", Me._myRule.ReplyMail)
        Trace.WriteLineIf(ZTrace.IsInfo, "DOGenerateOutlook ReplyMsgPath: " & Me.replyMsgPath)
        Params.Add("ReplyMsgPath", Me.replyMsgPath)

        Return Params
    End Function

    Public Function ReemplazarVariables(ByVal res As TaskResult, ByVal mails As SortedList) As SortedList
        Dim para As String = obtenerPara(res)
        Trace.WriteLineIf(ZTrace.IsInfo, "Para: " & para)

        If mails.Contains(para) = False Then
            Dim params As Hashtable = ReemplazarVariables(res)
            mails.Add(para, params)
        Else
            Trace.WriteLineIf(ZTrace.IsInfo, "Body anterior: " & Me._myRule.Body)
            Dim body As String = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Body, res, mails(para)("Body"))
            mails(para)("Body") = body
            Trace.WriteLineIf(ZTrace.IsInfo, "Body modificado: " & mails(para)("Body"))
        End If

        Return mails
    End Function

    Public Function obtenerPara(ByVal res As TaskResult) As String
        Try
            Me.para2 = String.Empty
            Me.R = String.Empty

            Me.para2 = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Para, res)

            Dim ValorVariable As Object
            Dim Variable As String = WFRuleParent.ObtenerNombreVariable(Me._myRule.Para)
            ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Me._myRule.Para)


            If IsNothing(ValorVariable) = False Then
                If TypeOf (ValorVariable) Is DataSet Then
                    Dim ds As DataSet = ValorVariable
                    'Se cambió por el for each de abajo para poder
                    'adjuntar varias direcciones [Alejandro].
                    'R = ds.Tables(0).Rows(0)(0).ToString()
                    If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        For Each tmpDR As DataRow In ds.Tables(0).Rows
                            If Not IsNothing(tmpDR) AndAlso Not IsDBNull(tmpDR) Then
                                If String.IsNullOrEmpty(Me.R) Then
                                    Me.R = tmpDR(0).ToString()
                                Else
                                    'R = R + "," + tmpDR(0).ToString()
                                    'Esto se hizo para que dependiendo del correo del usuario ponga como 
                                    'separador la "," o ";"
                                    Select Case UserBusiness.Rights.CurrentUser.eMail.Type
                                        Case Zamba.Core.MailTypes.LotusNotesMail
                                            Me.R = Me.R + "," + tmpDR(0).ToString()
                                        Case Zamba.Core.MailTypes.NetMail
                                            Me.R = Me.R + ";" + tmpDR(0).ToString()
                                        Case Zamba.Core.MailTypes.OutLookMail
                                            Me.R = Me.R + ";" + tmpDR(0).ToString()
                                        Case Else
                                            Me.R = Me.R + ";" + tmpDR(0).ToString()
                                    End Select
                                End If
                            End If
                        Next
                    End If
                    Me.para2 = Me.para2.Replace("zvar(" & Variable & ")", Me.R)
                ElseIf IsNumeric(ValorVariable) Then
                    Dim u As New User(ValorVariable)
                    Zamba.Core.UserBusiness.Mail.FillUserMailConfig(u)
                    Me.R = u.eMail.Mail
                    Me.para2 = Me.para2.Replace("zvar(" & Variable & ")", Me.R)
                ElseIf TypeOf (ValorVariable) Is String Then
                    If Not ValorVariable.ToString.Contains(".") AndAlso Not ValorVariable.ToString.Contains("@") Then
                        'id de usuario
                        Dim uId As Int32 = Zamba.Core.UserBusiness.GetUserID(ValorVariable)
                        Dim u As New User(uId)
                        Zamba.Core.UserBusiness.Mail.FillUserMailConfig(u)
                        Me.R = u.eMail.Mail
                        Me.para2 = Me.para2.Replace("zvar(" & Variable & ")", Me.R)
                    Else
                        'Es una direccion de mail
                        Me.R = ValorVariable.ToString
                        Me.para2 = Me.para2.Replace("zvar(" & Variable & ")", Me.R)
                    End If
                End If
            End If
            Return Me.para2
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function
End Class
Public Class OutlookMailActions

    Private _smtpConfig As Hashtable

    Sub New(ByVal smtpconfig As Hashtable)
        Me._smtpConfig = smtpconfig
    End Sub

    Public Function SendMail(ByVal resultActionType As ResultActions, ByRef currentResult As ZambaCore, ByVal Params As Hashtable)

        Dim _currentResult As IResult = DirectCast(currentResult, Result)

        Dim Results(0) As Result
        Results(0) = _currentResult

        EnviarMail(Results, Params)


    End Function


#Region "Mensajes"

    '---------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que completa la propiedad htmlfile y crea el archivo mht del formulario
    ''' </summary>
    ''' <param name="r"></param>
    ''' <remarks></remarks>

    Private Function CompleteHtmlFile(ByRef r As Result) As Boolean

        If Not Me.GetHtml(r) Then
            Return False
        End If
        r.HtmlFile = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp").FullName & "\" & r.Name & "-temp" & (New Random).Next(1, 9999).ToString & ".html"

        Try
            If File.Exists(r.HtmlFile) Then
                File.Delete(r.HtmlFile)
            End If
        Catch ex As Exception

        End Try
        Dim form As ZwebForm = FormBusiness.GetShowAndEditForms(r.DocType.ID)(0)
        If File.Exists(form.Path.Replace(".html", ".mht")) Then
            Try
                Using write As New StreamWriter(r.HtmlFile.Substring(0, r.HtmlFile.Length - 4) & "mht")
                    write.AutoFlush = True
                    Dim reader As New StreamReader(form.Path.Replace(".html", ".mht"))
                    Dim mhtstring As String = reader.ReadToEnd()
                    write.Write(mhtstring.Replace("<Zamba.Html>", r.Html))
                End Using
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            r.HtmlFile = r.HtmlFile.Substring(0, r.HtmlFile.Length - 4) & "mht"
        Else

            Try
                Using write As New StreamWriter(r.HtmlFile)
                    write.AutoFlush = True
                    write.Write(r.Html)
                End Using
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End If

        Return True

    End Function


    ''' <summary>
    ''' Metodo que Muestra el form para completar la propiedad html
    ''' </summary>
    ''' <param name="r"></param>
    ''' <remarks></remarks>
    Public Function GetHtml(ByRef r As Result) As Boolean
        Try
            If Boolean.Parse(UserPreferences.getValue("PreviewFormInDoGenerateOutlook", Sections.UserPreferences, "True")) Then
                Dim prvfrm As New PreviewForm(r)

                prvfrm.ShowDialog()
                If prvfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                    r.Html = prvfrm.frmbrowser.GetHtml
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Método que ejecuta el formulario de envio de mail
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="Params"></param>
    ''' <history>   Marcelo Modified 12/01/2010</history>
    ''' <history>   Marcelo Modified 13/01/2010</history>
    ''' <history>   Tomas   Modified 15/06/2010</history>
    ''' <remarks></remarks>
    Public Sub EnviarMail(ByRef results() As Result, ByVal Params As Hashtable) 'FORM QUE PERMITE MANDAR UN MAIL
        If (RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.EnviarPorMail)) Then
            'Valido que la colección de results no venga en nothing (caso de mail sin attach)
            If Not IsNothing(results(0)) Then
                Dim path As String
                Dim indexerTemp As String
                Dim res As NewResult = Nothing
                Dim AutomaticSend As Boolean
                Dim NuevoFile As IO.FileInfo

                path = Params.Item("ReplyMsgPath").ToString()
                indexerTemp = Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName & "\"

                'Se verifica si el path debe ser el mail original o una copia para responder.
                If Boolean.Parse(Params.Item("ReplyMail")) AndAlso Not String.IsNullOrEmpty(path) Then

                    'Se verifica si el mail original existe.
                    If IO.File.Exists(path) Then
                        Try
                            'Se obtiene el path de destino y se realiza la copia.
                            Dim destPath As String = FileBusiness.GetUniqueFileName(indexerTemp, IO.Path.GetFileName(path))
                            File.Copy(path, destPath)
                            path = destPath

                        Catch ex As Exception
                            Throw New Exception("Error al copiar el archivo original del mensaje a responder.", ex)
                        End Try
                    Else
                        Throw New FileNotFoundException("El mensaje original a responder no pudo ser encontrado.", path)
                    End If

                    If String.Compare(IO.Path.GetExtension(path).ToLower().Trim(), ".msg") <> 0 Then
                        Throw New Exception("El archivo del mensaje a responder debe ser un MSG. Verifique la configuración de la regla. Ruta del archivo encontrado: " + path)
                    End If

                Else
                    path = FileBusiness.GetUniqueFileName(indexerTemp, "OutlookMail...", ".msg")
                End If

                NuevoFile = New IO.FileInfo(path)
                createLink(results, Params)

                If Params.Contains("AutomaticSend") Then
                    AutomaticSend = Params("AutomaticSend")
                Else
                    AutomaticSend = False
                End If

                If SharedOutlook.GetOutlook().GetNewMailItem(NuevoFile.FullName, True, True, Params, AutomaticSend) Then
                    res = New NewResult(NuevoFile.FullName)
                    res.DocType = results(0).DocType
                    res.ID = results(0).ID

                    res.Indexs = results(0).Indexs

                    'Reemplaza el archivo
                    FillIndexWithMailProperties(results(0), Params)
                    If Results_Business.UpdateInsert(res, False, False, False, False, False, False) = InsertResult.Insertado Then
                        Try
                            File.Copy(NuevoFile.FullName.Replace(".msg", ".html"), res.FullPath.Replace(".msg", ".html"))
                            SaveHistory(Params, results(0).ID, results(0).DocType.ID, res.FullPath)
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Else
                        Throw New Exception("El documento no pudo ser insertado, revisar las otras excepciones")
                    End If
                Else
                    results(0).PrintName = "Cancel"
                End If
            End If
        Else
            MessageBox.Show("No tiene permisos suficientes para enviar el documento por mail", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub


    Public Function SaveHistory(ByVal Params As Hashtable, ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal ExportPath As String)
        Dim _to As String
        Dim _cc As String
        Dim _cco As String
        Dim _subject As String
        Dim _body As String
        Dim _attachs As List(Of String) = Nothing

        If Not Params(OutlookMailParameters.TO) Is Nothing Then _to = Params(OutlookMailParameters.TO).ToString()
        If Not Params(OutlookMailParameters.CC) Is Nothing Then _cc = Params(OutlookMailParameters.CC).ToString()
        If Not Params(OutlookMailParameters.BCC) Is Nothing Then _cco = Params(OutlookMailParameters.BCC).ToString()
        If Not Params(OutlookMailParameters.SUBJECT) Is Nothing Then _subject = Params(OutlookMailParameters.SUBJECT).ToString()
        If Not Params(OutlookMailParameters.BODY) Is Nothing Then _body = Params(OutlookMailParameters.BODY).ToString()
        If Not Params(OutlookMailParameters.ATTACH_PATHS) Is Nothing Then _attachs = DirectCast(Params(OutlookMailParameters.ATTACH_PATHS), String()).ToList

        MessagesBusiness.SaveHistory(_to, _cc, _cco, _subject, _body, _attachs, DocId, DocTypeId, ExportPath)
    End Function

    ''' <summary>
    ''' Completa los atributos del documento con los valores del mail
    ''' </summary>
    ''' <param name="_result"></param>
    ''' <param name="_params"></param>
    ''' <history>   Marcelo Modified 13/01/2010</history>
    ''' <remarks></remarks>
    Private Sub FillIndexWithMailProperties(ByRef _result As Zamba.Core.Result, ByVal _params As Hashtable)
        Dim modifiedIndex As List(Of Int64)
        Dim mailIndex As Hashtable
        Try
            modifiedIndex = New List(Of Int64)
            mailIndex = getMailIndexSettings()
            For Each indice As IIndex In _result.Indexs
                If mailIndex.Contains(indice.ID.ToString()) Then
                    modifiedIndex.Add(indice.ID)
                    indice.Data = getMailValue(mailIndex(indice.ID.ToString()).ToString(), _params).Replace("'", "")
                    indice.DataTemp = indice.Data
                End If
            Next

            Dim rstBuss As New Results_Business()
            rstBuss.SaveModifiedIndexData(_result, True, True, modifiedIndex)
            rstBuss = Nothing
        Finally
            modifiedIndex = Nothing
            mailIndex = Nothing
        End Try
    End Sub

    Private Function getMailIndexSettings() As Hashtable

        Dim mailIndex As Hashtable = New Hashtable()
        Dim indexId As String = String.Empty
        indexId = UserPreferences.getValue("EnviadoPor", Sections.ExportaPreferences, "29")
        If Not mailIndex.ContainsKey(indexId) Then mailIndex.Add(indexId, "EnviadoPor")
        indexId = UserPreferences.getValue("Para", Sections.ExportaPreferences, "31")
        If Not mailIndex.ContainsKey(indexId) Then mailIndex.Add(indexId, "Para")
        indexId = UserPreferences.getValue("CC", Sections.ExportaPreferences, "32")
        If Not mailIndex.ContainsKey(indexId) Then mailIndex.Add(indexId, "CC")
        indexId = UserPreferences.getValue("BCC", Sections.ExportaPreferences, "33")
        If Not mailIndex.ContainsKey(indexId) Then mailIndex.Add(indexId, "BCC")
        indexId = UserPreferences.getValue("fecha", Sections.ExportaPreferences, "30")
        If Not mailIndex.ContainsKey(indexId) Then mailIndex.Add(indexId, "fecha")
        indexId = UserPreferences.getValue("Asunto", Sections.ExportaPreferences, "34")
        If Not mailIndex.ContainsKey(indexId) Then mailIndex.Add(indexId, "Asunto")
        indexId = UserPreferences.getValue("Cuerpo", Sections.ExportaPreferences, "0")
        If Not mailIndex.ContainsKey(indexId) Then mailIndex.Add(indexId, "Cuerpo")
        indexId = UserPreferences.getValue("UsuarioWindows", Sections.ExportaPreferences, "52")
        If Not mailIndex.ContainsKey(indexId) Then mailIndex.Add(indexId, "UsuarioWindows")
        indexId = UserPreferences.getValue("UsuarioZamba", Sections.ExportaPreferences, "53")
        If Not mailIndex.ContainsKey(indexId) Then mailIndex.Add(indexId, "UsuarioZamba")
        indexId = UserPreferences.getValue("Codigo", Sections.ExportaPreferences, "110")
        If Not mailIndex.ContainsKey(indexId) Then mailIndex.Add(indexId, "Codigo")

        Return mailIndex

    End Function

    Private Function getMailValue(ByVal MailValueName As String, ByVal _params As Hashtable) As String
        Dim value As String = String.Empty
        Select Case MailValueName
            Case "EnviadoPor"
                If Not IsNothing(_params(OutlookMailParameters.SENDER)) Then value = _params(OutlookMailParameters.SENDER).ToString()
            Case "Para"
                If Not IsNothing(_params(OutlookMailParameters.TO)) Then value = _params(OutlookMailParameters.TO).ToString()
            Case "CC"
                If Not IsNothing(_params(OutlookMailParameters.CC)) Then value = _params(OutlookMailParameters.CC).ToString()
            Case "BCC"
                If Not IsNothing(_params(OutlookMailParameters.BCC)) Then value = _params(OutlookMailParameters.BCC).ToString()
            Case "fecha"
                If Not IsNothing(_params(OutlookMailParameters.RECEIVEDDATE)) Then value = _params(OutlookMailParameters.RECEIVEDDATE).ToString()
            Case "Asunto"
                If Not IsNothing(_params(OutlookMailParameters.SUBJECT)) Then value = _params(OutlookMailParameters.SUBJECT).ToString()
            Case "Cuerpo"
                If Not IsNothing(_params(OutlookMailParameters.BODY)) Then value = _params(OutlookMailParameters.BODY).ToString()
            Case "UsuarioWindows"
                value = Environment.UserName
            Case "UsuarioZamba"
                value = Membership.MembershipHelper.CurrentUser.Name
            Case "Codigo"
                If Not IsNothing(_params(OutlookMailParameters.ENTRY_ID)) Then value = _params(OutlookMailParameters.ENTRY_ID).ToString()

        End Select

        Return value
    End Function

    Private Function createLink(ByRef results() As Result, ByRef params As Hashtable) As String

        Try
            Dim Links As ArrayList = DirectCast(params(OutlookMailParameters.LINK), ArrayList)
            Dim attachLink As Boolean = Convert.ToBoolean(Links(0))

            If (attachLink = True) Then

                Dim link As String = Nothing
                Dim attachResultLink As String

                If Not Links Is Nothing Then

                    Dim sBuilder As System.Text.StringBuilder = New System.Text.StringBuilder

                    sBuilder.Append("<br><a href=")
                    sBuilder.Append(Chr(34))
                    sBuilder.Append(Trim(Links(1)))
                    sBuilder.Append(Chr(34))
                    sBuilder.Append(">")
                    sBuilder.Append("Acceso a la tarea.")
                    sBuilder.Append("</a>")

                    link &= sBuilder.ToString
                    params.Add(OutlookMailParameters.HTML_LINK, link)

                End If

                Return (link)

            Else
                Return (Nothing)
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return (Nothing)
        End Try

    End Function



#End Region

End Class
