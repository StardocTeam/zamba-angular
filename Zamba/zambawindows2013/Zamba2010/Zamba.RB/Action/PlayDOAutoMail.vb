Imports System.Windows.Forms
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness

Public Class PlayDOAutoMail

    Private _myRule As IDOAutoMail
    Private automails As New SortedList()
    Private Automail As AutoMail
    Private mailto As String
    Private body As String
    Private counter As Int32
    Private indexlist As List(Of String)
    Private attachList As List(Of String)

    Sub New(ByVal rule As IDOAutoMail)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            ' [AlejandroR] - 05/03/2010 - Created
            ' Se chequea antes de enviar el mail que se tenga configurada y con acceso
            ' la ruta para el historial de emails, si falla se cancela la regla
            MessagesBusiness.CheckHistoryExportPath()
        Catch ex As Exception
            raiseerror(ex)
            Throw ex
        End Try

        Try
            If Me._myRule.groupMailTo Then
                Me.automails = New SortedList()

                For Each taskResult As Core.ITaskResult In results

                  
                    Me.Automail = Nothing

                    Try
                        Me.Automail = GetAutomailById(Me._myRule.Automail.ID)
                    Catch ex As Exception
                        Trace.WriteLineIf(ZTrace.IsInfo, "Error al obtener configuración de AutoMail. Documento: " & taskResult.Name & ", ID de tarea: " & taskResult.TaskId)
                        Throw ex
                    End Try

                    Me.mailto = String.Empty

                    Try
                        mailto = Zamba.Core.TextoInteligente.ReconocerCodigo(Automail.MailTo, taskResult)
                        mailto = WFRuleParent.ReconocerVariablesValuesSoloTexto(Automail.MailTo)
                    Catch ex As Exception
                        Trace.WriteLineIf(ZTrace.IsInfo, "Error al obtener direcciones de mail para el Envío. Documento: " & taskResult.Name & ", ID de tarea: " & taskResult.TaskId)
                        Throw ex
                    End Try

                    If automails.Contains(mailto) = False Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Configurando Automail...")
                        Automail.Subject = Zamba.Core.TextoInteligente.ReconocerCodigo(Automail.Subject, taskResult)

                        Automail.MailTo = mailto
                        Automail.CC = Zamba.Core.TextoInteligente.ReconocerCodigo(Automail.CC, taskResult)
                        Automail.CCO = Zamba.Core.TextoInteligente.ReconocerCodigo(Automail.CCO, taskResult)
                        Automail.Body = Zamba.Core.TextoInteligente.ReconocerCodigo(Automail.Body, taskResult)
                        Automail.TaskId = taskResult.ID
                        Automail.DocTypeID = taskResult.DocTypeId

                        automails.Add(mailto, Automail)
                    Else
                        Me.body = Zamba.Core.TextoInteligente.ReconocerCodigo(Automail.Body, taskResult, DirectCast(automails(mailto), AutoMail).Body)
                        DirectCast(automails(mailto), AutoMail).Body = body
                    End If


                Next

                Me.counter = 0

                For Each automail As AutoMail In automails.Values
                    counter += 1
                    Me.indexlist = New List(Of String)
                    Me.indexlist.AddRange(Me._myRule.IndexNames)
                    automail.Body = automail.Body.Replace("§", String.Empty)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Enviando Automail " & counter & " de " & automails.Count)

                    Try
                        MessagesBusiness.AutoMail_SMTP(automail, Nothing, Me._myRule.AddDocument, Me._myRule.AddLink, Me._myRule.AddIndexs, False, Me._myRule.smtp, Me.indexlist)
                        Trace.WriteLineIf(ZTrace.IsInfo, "Automail enviado con éxito!")

                        ' [AlejandroR] 03/12/2009 - Created - Guardar el envio de este email en el historial
                        MessagesBusiness.SaveHistory(automail.MailTo, automail.CC, automail.CCO, automail.Subject, automail.Body, automail.AttachmentsPaths, automail.TaskId, automail.DocTypeID, String.Empty)
                    Catch ex As Exception
                        Throw ex
                    End Try
                Next
            Else
                For Each taskResult As Core.ITaskResult In results
                    Me.indexlist = New List(Of String)
                    Me.indexlist.AddRange(Me._myRule.IndexNames)
                    Me.Automail = GetAutomailById(Me._myRule.Automail.ID)
                    Me.Automail.Subject = Zamba.Core.TextoInteligente.ReconocerCodigo(Automail.Subject, taskResult)

                    Me.mailto = Zamba.Core.TextoInteligente.ReconocerCodigo(Automail.MailTo, taskResult)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Reconociendo Para: " & mailto)
                    mailto = obtenerPara(mailto, taskResult)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Para: " & mailto)
                    AutoMail.MailTo = mailto

                    AutoMail.CC = Zamba.Core.TextoInteligente.ReconocerCodigo(AutoMail.CC, taskResult)
                    AutoMail.CCO = Zamba.Core.TextoInteligente.ReconocerCodigo(AutoMail.CCO, taskResult)
                    AutoMail.Body = Zamba.Core.TextoInteligente.ReconocerCodigo(AutoMail.Body, taskResult)

                    AutoMail.Subject = WFRuleParent.ReconocerVariablesValuesSoloTexto(AutoMail.Subject)
                    AutoMail.MailTo = WFRuleParent.ReconocerVariablesValuesSoloTexto(AutoMail.MailTo)
                    AutoMail.CC = WFRuleParent.ReconocerVariablesValuesSoloTexto(AutoMail.CC)
                    AutoMail.CCO = WFRuleParent.ReconocerVariablesValuesSoloTexto(AutoMail.CCO)
                    AutoMail.Body = WFRuleParent.ReconocerVariablesValuesSoloTexto(AutoMail.Body)
                    Me.attachList = New List(Of String)
                    Me.attachList.AddRange(Automail.AttachmentsPaths)
                    Me.attachList.AddRange(Automail.PathImages)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Enviando Automail...")

                    Try
                        MessagesBusiness.SendMail(Automail.MailTo, Automail.CC, Automail.CCO, Automail.Subject, Automail.Body, True, Me.attachList)
                        Trace.WriteLineIf(ZTrace.IsInfo, "Automail enviado con éxito!")

                        ' [AlejandroR] 03/12/2009 - Created - Guardar el envio de este email en el historial
                        MessagesBusiness.SaveHistory(Automail.MailTo, Automail.CC, Automail.CCO, Automail.Subject, Automail.Body, Automail.AttachmentsPaths, taskResult.ID, taskResult.DocTypeId, String.Empty)
                    Catch ex As Exception
                        Throw ex
                    End Try
                Next
            End If

        Catch smtpEx As Net.Mail.SmtpException
            Trace.WriteLineIf(ZTrace.IsInfo, "Ocurrió un error en el envío del Mail.")
            Throw smtpEx
        Finally

            Me.automails = Nothing
            Me.Automail = Nothing
            Me.mailto = Nothing
            Me.body = Nothing
            Me.counter = Nothing
            Me.indexlist = Nothing
            Me.attachList = Nothing
        End Try
        Return results
    End Function

    Public Shared Function obtenerPara(ByVal mailto As String, ByVal Task As ITaskResult) As String
        Dim para As String = String.Empty
        Dim R As String = String.Empty

        Dim ValorVariable As Object
        Try
            Dim Variable As String = WFRuleParent.ObtenerNombreVariable(mailto)
            If mailto.ToLower.Contains("zvar") = True Then
                ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(mailto)
            Else
                ValorVariable = Variable
            End If

            If IsNothing(ValorVariable) = False Then
                If TypeOf (ValorVariable) Is DataSet Then
                    Dim ds As DataSet = DirectCast(ValorVariable, DataSet)
                    'Se cambió por el for each de abajo para poder
                    'adjuntar varias direcciones [Alejandro].
                    'R = ds.Tables(0).Rows(0)(0).ToString()
                    If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        For Each tmpDR As DataRow In ds.Tables(0).Rows
                            If Not IsNothing(tmpDR) AndAlso Not IsDBNull(tmpDR) Then
                                If String.IsNullOrEmpty(R) Then
                                    R = tmpDR(0).ToString()
                                Else
                                    'R = R + "," + tmpDR(0).ToString()
                                    'Esto se hizo para que dependiendo del correo del usuario ponga como 
                                    'separador la "," o ";"
                                    Select Case UserBusiness.Rights.CurrentUser.eMail.Type
                                        Case Zamba.Core.MailTypes.LotusNotesMail
                                            R = R + "," + tmpDR(0).ToString()
                                        Case Zamba.Core.MailTypes.NetMail
                                            R = R + ";" + tmpDR(0).ToString()
                                        Case Zamba.Core.MailTypes.OutLookMail
                                            R = R + ";" + tmpDR(0).ToString()
                                        Case Else
                                            R = R + ";" + tmpDR(0).ToString()
                                    End Select
                                End If
                            End If
                        Next
                    End If
                    para = para.Replace("zvar(" & Variable & ")", R)
                    ds = Nothing
                ElseIf IsNumeric(ValorVariable) Then
                    Dim Users As New Generic.List(Of Int64)
                    Dim u As IUser
                    u = UserBusiness.GetUserById(ValorVariable)
                    If u Is Nothing Then
                        Users = UserGroupBusiness.GetUsersIds(ValorVariable, True)
                    Else
                        Users.Add(ValorVariable)
                    End If

                    For Each UserId As Int64 In Users
                        Dim mail As String = Zamba.Core.UserBusiness.GetUserById(UserId).eMail.Mail
                        'Esto se hizo para que dependiendo del correo del usuario ponga como 
                        'separador la "," o ";"
                        If String.IsNullOrEmpty(R) = True Then
                            R = mail
                        ElseIf String.IsNullOrEmpty(mail) = False Then

                            Select Case UserBusiness.Rights.CurrentUser.eMail.Type
                                Case Zamba.Core.MailTypes.LotusNotesMail
                                    R = R + "," + mail
                                Case Zamba.Core.MailTypes.NetMail
                                    R = R + ";" + mail
                                Case Zamba.Core.MailTypes.OutLookMail
                                    R = R + ";" + mail
                                Case Else
                                    R = R + ";" + mail
                            End Select
                        End If
                    Next
                    If Variable.ToLower.Contains("zvar") = True Then
                        para = para.Replace("zvar(" & Variable & ")", R)
                    Else
                        para = R
                    End If

                    Users = Nothing
                ElseIf TypeOf (ValorVariable) Is String Then
                    If Not ValorVariable.ToString.Contains(".") AndAlso Not ValorVariable.ToString.Contains("@") Then
                        'id de usuario
                        Dim uId As Int32 = Zamba.Core.UserBusiness.GetUserID(ValorVariable)
                        Dim u As New User(uId)
                        Zamba.Core.UserBusiness.Mail.FillUserMailConfig(u)
                        R = u.eMail.Mail
                        para = para.Replace("zvar(" & Variable & ")", R)
                        uId = Nothing
                        u = Nothing
                    Else
                        'Es una direccion de mail
                        R = ValorVariable.ToString
                        If Variable.ToLower.Contains("zvar") = True Then
                            para = para.Replace("zvar(" & Variable & ")", R)
                        Else
                            para = R
                        End If
                    End If
                End If
            End If
            Variable = Nothing
        Finally
            R = Nothing
            ValorVariable = Nothing
        End Try
        Return para
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
