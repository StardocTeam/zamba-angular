Imports Zamba.Core
Imports System.Windows.Forms
Imports System.IO
Imports Zamba.AdminControls
Imports Zamba.Viewers
Imports Zamba.Core.MessagesBusiness
Imports Zamba.Core.ZOptBusiness
Imports System.Web.Service
Imports System.Web.Services.Description
Imports System.Web.Services.Protocols
Imports Zamba.WorkFlow.Business
Imports Zamba.Services
Imports Zamba.Membership
Imports Spire.Email
Imports Zamba.FileTools
Imports System.Dynamic
Imports System.Collections.ObjectModel


Public Class PlayDOMail

    Private UB As New UserBusiness
#Region "propiedades"
    Private _myRule As IDOMail
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
    Private para2 As String
    Private R As String
    Private _smtpConfig As Hashtable
    Private _mailPath As String
    Private originalDocument As Byte()
    Private originalDocumentFileName As String
    Private MessagesBusiness As MessagesBusiness
#End Region


    Sub New(ByVal rule As IDOMail)
        Me._myRule = rule
        MessagesBusiness = New MessagesBusiness()
    End Sub

    ''' <summary>
    ''' Play de la regla DOMail
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myrule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        'Seteamos una variable que guarda el path del documento local
        Return PlayWeb(results, Nothing)
    End Function

    ''' <summary>
    ''' Método que pregunta si la regla tiene o no el envío de documentos. Si es, es "conAttach", sino "sinAttach"
    ''' </summary>
    ''' <param name="result"></param>
    ''' <param name="myrule"></param>
    ''' <remarks></remarks>
    '''     ''' <history> 
    '''     [Gaston]    09/09/2008  Modified     
    ''' </history>
    Private Sub isSendDocument(ByRef result As TaskResult)

        If (Me._myRule.SendDocument) Then
            result.AutoName = "conAttach"
        Else
            result.AutoName = "sinAttach"
        End If

    End Sub

    ''' <summary>
    '''     Realiza el reemplazo de zvar y texto inteligente
    ''' </summary>
    ''' <param name="res"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  14/01/2011  Modified    Se extraen reemplazo de Para, cc y cco
    ''' </history>
    Public Function ReemplazarVariables(ByVal res As TaskResult, ByVal UserId As Int64) As Hashtable
        Me.Body = String.Empty
        Me.Asunto = String.Empty
        Para = String.Empty
        CC = String.Empty
        CCO = String.Empty
        '[Sebastian 05-06-2009]se realizo new porque lanzaba object reference
        Dim AssociatedResults As New List(Of IResult) ' = New List(Of IResult)()
        Dim PathImages() As String = {}
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            Try
                Dim R As String = String.Empty
                Me.Asunto = Me._myRule.Asunto
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo Asunto: " & Me.Asunto)
                Me.Asunto = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.Asunto, res)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Asunto1: " & Me.Asunto)

                Dim ValorVariable As Object
                Dim Variable As String = VarInterReglas.ObtenerNombreVariable(Me.Asunto)
                ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Me.Asunto)

                If IsNothing(ValorVariable) = False Then
                    If (TypeOf (ValorVariable) Is DataSet) Then
                        For Each DR As DataRow In ValorVariable.tables(0).rows
                            R &= DR.Item(0) & ","
                        Next
                        Me.Asunto = Me.Asunto.Replace("zvar(" & Variable & ")", R)
                    End If
                    Me.Asunto = Me.Asunto.Replace("zvar(" & Variable & ")", ValorVariable)
                    Me.Asunto = VarInterReglas.ReconocerVariables(Me.Asunto)
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Asunto2: " & Me.Asunto)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                ZClass.raiseerror(ex)
            End Try

            Me.Para = obtenerPara(res)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Para: " & Me.Para)

            obtenerCC(res)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "CC: " & Me.CC)

            obtenerCCO(res)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "CCO: " & Me.CCO)

            Try

                Me.Body = TextoInteligente.GetValueFromZvarOrSmartText(_myRule.Body, res, Body)
                Body = TextoInteligente.GetValueFromZvarOrSmartText(Body, res, Body)

                Me.Body = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.Body, res, False, Body)
                Body = TextoInteligente.GetValueFromZvarOrSmartText(Body, res, Body)
                'Me.Body = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Body, res, False)
                Me.Body = VarInterReglas.ReconocerVariables(Me.Body, False)
                Me.Body = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.Body, res, False, Body)
                If Body.Contains("</") = False Then
                    Me.Body = Me.Body.Replace(vbCrLf, "<br/>")
                Else
                    Me.Body = Me.Body.Replace(vbLf, "")
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                ZClass.raiseerror(ex)
            End Try

            Try
                If String.IsNullOrEmpty(Me._myRule.PathImages) = False Then
                    PathImages = Split(Me._myRule.PathImages, ";")
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                ZClass.raiseerror(ex)
            End Try
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            ZClass.raiseerror(ex)
        End Try


        Dim Params As New Hashtable

        AttachOriginalDocument(res)
        If (Me._myRule.Answer And VariablesInterReglas.ContainsKey("rutaDocumento")) Then


            Dim rutaDocumento As String = VarInterReglas.Item("rutaDocumento").ToString()

            If Not (rutaDocumento.Contains(".msg")) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento no es un .msg")
                Throw New Exception("El documento no es de tipo mail")
            End If

            Dim St As New Zamba.FileTools.SpireTools
            Dim PathPdf As String = String.Empty


            Dim MailInfo = St.ConvertMSGToJSON(rutaDocumento, PathPdf, True)
            Dim Mailto = String.Empty
            Dim Cc = String.Empty
            Dim Cco = String.Empty

            For indexTo As Double = 0 To MailInfo.[to].Count - 1
                If MailInfo.[to](indexTo) = MailInfo.[to](MailInfo.[to].Count - 1) Then
                    Mailto += MailInfo.[to](indexTo)
                Else
                    Mailto += MailInfo.[to](indexTo) + "; "
                End If
            Next

            For indexCc As Double = 0 To MailInfo.cc.Count - 1
                If MailInfo.cc(indexCc) = MailInfo.cc(MailInfo.cc.Count - 1) Then
                    Cc += MailInfo.cc(indexCc)
                Else
                    Cc += MailInfo.cc(indexCc) + "; "
                End If
            Next

            For indexCco As Double = 0 To MailInfo.cco.Count - 1
                If MailInfo.cco(indexCco) = MailInfo.cco(MailInfo.cco.Count - 1) Then
                    Cco += MailInfo.cco(indexCco)
                Else
                    Cco += MailInfo.cco(indexCco) + "; "
                End If
            Next

            MailInfo.from = Me.ReconocerGruposYUsuarios(Mailto)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DoMail To: " & MailInfo.from)
            If (String.IsNullOrEmpty(MailInfo.from) AndAlso ZOptBusiness.GetValueOrDefault("EMailThrowErrorIfTOIsEmpty", True)) Then
                Throw New Exception("ERROR: Email - El destinatario no puede estar vacio, para permitir valor vacio en el Destinatario, configurar EMailThrowErrorIfTOIsEmpty en False")
            End If

            Params.Add("To", MailInfo.from)

            Me.CC = Me.ReconocerGruposYUsuarios(Cc)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DoMail CC: " & Me.CC)
            Params.Add("CC", Me.CC)

            Me.CCO = Me.ReconocerGruposYUsuarios(Cco)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DoMail CCO: " & Me.CCO)
            Params.Add("CCO", Me.CCO)

            Params.Add("Subject", "RE:" + MailInfo.subject)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DoMail Body: " & MailInfo.subject)
            Params.Add("Body", MailInfo.Body)

        Else

            Me.Para = Me.ReconocerGruposYUsuarios(Me.Para)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DoMail To: " & Me.Para)
            If (String.IsNullOrEmpty(Me.Para) AndAlso ZOptBusiness.GetValueOrDefault("EMailThrowErrorIfTOIsEmpty", True)) Then
                Throw New Exception("ERROR: Email - El destinatario no puede estar vacio, para permitir valor vacio en el Destinatario, configurar EMailThrowErrorIfTOIsEmpty en False")
            End If

            Params.Add("To", Me.Para)

            Me.CC = Me.ReconocerGruposYUsuarios(Me.CC)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DoMail CC: " & Me.CC)
            Params.Add("CC", Me.CC)

            Me.CCO = Me.ReconocerGruposYUsuarios(Me.CCO)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DoMail CCO: " & Me.CCO)
            Params.Add("CCO", Me.CCO)

            Params.Add("Subject", Me.Asunto)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DoMail Body: " & Me.Body)
            Params.Add("Body", Me.Body)
        End If





        '[Ezequiel]: Se filtran los documentos asociados a adjuntar.
        Dim TempDocAsociated As New ArrayList

        If Me._myRule.AttachAssociatedDocuments Then
            If Not res.UserRules.ContainsKey("_DoMailAsocHash") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo asociados.")
                '[Ezequiel]: Valido si se adjuntan todos o los seleccionados por el usuario
                If Me._myRule.DTType = IMailConfigDocAsoc.DTTypes.AllDT Then
                    Dim DAB As New DocTypes.DocAsociated.DocAsociatedBusiness
                    TempDocAsociated = DAB.getAsociatedResultsFromResult(res, 100, UserId)
                    DAB = Nothing
                    If IsNothing(TempDocAsociated) = False AndAlso TempDocAsociated.Count > 0 Then
                        '[Ezequiel]: Valido si se selecciona el primero, se filtra, se adjunta de manera manual o se adjunta todo

                        Select Case Me._myRule.Selection
                            Case IMailConfigDocAsoc.Selections.First
                                AssociatedResults.Add(TempDocAsociated(0))
                            Case IMailConfigDocAsoc.Selections.Filter
                                Dim IB As New IndexsBusiness
                                For Each resu As IResult In TempDocAsociated
                                    Dim inlist As List(Of IIndex) = IB.GetIndexsData(resu.ID, resu.DocTypeId)
                                    For Each i As Index In inlist
                                        If Me._myRule.Index = i.ID AndAlso ToolsBusiness.ValidateComp(i.Data, Me._myRule.IndexValue, Me._myRule.Oper, False) Then
                                            AssociatedResults.Add(resu)
                                        End If
                                    Next
                                Next
                                IB = Nothing
                            Case IMailConfigDocAsoc.Selections.All
                                For Each resu As IResult In TempDocAsociated
                                    AssociatedResults.Add(resu)
                                Next
                            Case IMailConfigDocAsoc.Selections.Manual
                                If Me._myRule.Automatic Then
                                    For Each resu As IResult In TempDocAsociated
                                        AssociatedResults.Add(resu)
                                    Next
                                End If
                            Case IMailConfigDocAsoc.Selections.FilterDocId
                                Dim txtfilterid As String = TextoInteligente.ReconocerCodigo(Me._myRule.FilterDocID, res)
                                txtfilterid = VarInterReglas.ReconocerVariablesValuesSoloTexto(txtfilterid)
                                For Each resu As IResult In TempDocAsociated
                                    If resu.ID = CLng(txtfilterid) Then AssociatedResults.Add(resu)
                                Next
                        End Select

                    End If
                Else
                    Dim DAB As New DocTypes.DocAsociated.DocAsociatedBusiness
                    For Each DTId As String In ArrayList.Adapter(Me._myRule.DocTypes.Split("|"))
                        Dim asoc As ArrayList = DAB.getAsociatedResultsFromResult(Int64.Parse(DTId), res, 100, UserId)
                        If Not IsNothing(asoc) Then
                            TempDocAsociated.AddRange(asoc)
                        End If
                    Next
                    DAB = Nothing
                    If IsNothing(TempDocAsociated) = False AndAlso TempDocAsociated.Count > 0 Then
                        Select Case Me._myRule.Selection
                            Case IMailConfigDocAsoc.Selections.First
                                AssociatedResults.Add(TempDocAsociated(0))
                            Case IMailConfigDocAsoc.Selections.Filter
                                Dim IB As New IndexsBusiness
                                For Each resu As IResult In TempDocAsociated
                                    Dim inlist As List(Of IIndex) = IB.GetIndexsData(resu.ID, resu.DocTypeId)
                                    For Each i As Index In inlist
                                        If Me._myRule.Index = i.ID AndAlso ToolsBusiness.ValidateComp(i.Data, Me._myRule.IndexValue, Me._myRule.Oper, False) Then
                                            AssociatedResults.Add(resu)
                                        End If
                                    Next
                                Next
                                IB = Nothing
                            Case IMailConfigDocAsoc.Selections.All
                                For Each resu As IResult In TempDocAsociated
                                    AssociatedResults.Add(resu)
                                Next
                            Case IMailConfigDocAsoc.Selections.Manual
                                If Me._myRule.Automatic Then
                                    For Each resu As IResult In TempDocAsociated
                                        AssociatedResults.Add(resu)
                                    Next
                                End If
                            Case IMailConfigDocAsoc.Selections.FilterDocId
                                Dim txtfilterid As String = TextoInteligente.ReconocerCodigo(Me._myRule.FilterDocID, res)
                                txtfilterid = VarInterReglas.ReconocerVariablesValuesSoloTexto(txtfilterid)
                                For Each resu As IResult In TempDocAsociated
                                    If resu.ID = CLng(txtfilterid) Then AssociatedResults.Add(resu)
                                Next
                        End Select

                    End If
                End If
                res.UserRules.Add("_DoMailAsocHash", AssociatedResults)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo asociados en memoria de la tarea.")
                AssociatedResults = res.UserRules.Item("_DoMailAsocHash")
            End If
        End If

        VarInterReglas = Nothing
        Params.Add("AssociatedResults", AssociatedResults)
        If Not IsNothing(AssociatedResults) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Documentos Asociados: " & AssociatedResults.Count)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "DoMail Imagenes: " & Me._myRule.PathImages)
        Params.Add("AttachPaths", PathImages)
        Params.Add("MailPathVariable", _myRule.MailPath)

        Return Params
    End Function

    Public Function ReemplazarVariables(ByVal res As TaskResult, ByVal mails As SortedList, ByVal UserId As Int64) As SortedList
        Dim para As String = obtenerPara(res)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Para: " & para)

        If mails.Contains(para) = False Then
            Dim params As Hashtable = ReemplazarVariables(res, UserId)
            params.Add("Automatic", Me._myRule.Automatic)
            mails.Add(para, params)
        Else

            Dim body As String = TextoInteligente.GetValueFromZvarOrSmartText(_myRule.Body, res) 'TextoInteligente.ReconocerCodigo(_myRule.Body, res, mails(para)("Body"))
            body = TextoInteligente.GetValueFromZvarOrSmartText(body, res)

            'Dim body As String = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Body, res, mails(para)("Body"))
            Dim VarInterReglas As New VariablesInterReglas()
            body = VarInterReglas.ReconocerVariables(body)
            VarInterReglas = Nothing
            body = body.Replace(vbCrLf, "<br/>")
            mails(para)("Body") = body
        End If

        Return mails
    End Function

    ''' <summary>
    '''     Analiza el Para y reconoce las variables
    ''' </summary>
    ''' <param name="res"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  14/01/2011  Modified    Se quita obtención de mail para posterior tratado
    ''' </history>
    Public Function obtenerPara(ByVal res As TaskResult) As String
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            Me.para2 = String.Empty
            Me.R = String.Empty

            Me.para2 = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Para, res)

            Dim ValorVariable As Object
            Dim Variable As String = VarInterReglas.ObtenerNombreVariable(Me._myRule.Para)
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
                                    Select Case Zamba.Membership.MembershipHelper.CurrentUser.eMail.Type
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
                    Me.para2 = Me.para2.Replace("zvar(" & Variable & ")", ValorVariable)
                ElseIf TypeOf (ValorVariable) Is String Then
                    If Not ValorVariable.ToString.Contains(".") AndAlso Not ValorVariable.ToString.Contains("@") Then
                        Me.para2 = Me.para2.Replace("zvar(" & Variable & ")", ValorVariable)
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
        Finally
            VarInterReglas = Nothing
        End Try
    End Function

    ''' <summary>
    '''     Analiza el CC y reconoce las variables
    ''' </summary>
    ''' <param name="res"></param>
    ''' <history>
    '''     Javier  14/01/2011  Created
    ''' </history>
    Public Sub obtenerCC(ByVal res As TaskResult)
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            Dim R As String
            Me.CC = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.CC, res)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "CC1: " & Me.CC)

            Dim ValorVariable As Object
            Dim Variable As String = VarInterReglas.ObtenerNombreVariable(Me._myRule.CC)
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
                    Me.CC = Me.CC.Replace("zvar(" & Variable & ")", ValorVariable)
                ElseIf TypeOf (ValorVariable) Is String Then
                    If Not ValorVariable.ToString.Contains(".") AndAlso Not ValorVariable.ToString.Contains("@") Then
                        Me.CC = Me.CC.Replace("zvar(" & Variable & ")", ValorVariable)
                    Else
                        'Es una direccion de mail
                        R = ValorVariable.ToString
                        Me.CC = Me.CC.Replace("zvar(" & Variable & ")", R)
                    End If
                End If
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo CC2: " & Me.CC)
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            ZClass.raiseerror(ex)
        Finally
            VarInterReglas = Nothing
        End Try
    End Sub

    ''' <summary>
    '''     Analiza el CCO y reconoce las variables
    ''' </summary>
    ''' <param name="res"></param>
    ''' <history>
    '''     Javier  14/01/2011  Created
    ''' </history>
    Public Sub obtenerCCO(ByVal res As TaskResult)
        Try
            Dim R As String
            Me.CCO = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.CCO, res)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo CCO1: " & Me.CCO)

            Dim ValorVariable As Object
            Dim VarInterReglas As New VariablesInterReglas()
            Dim Variable As String = VarInterReglas.ObtenerNombreVariable(Me._myRule.CCO)
            VarInterReglas = Nothing
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
                    Me.CCO = Me.CCO.Replace("zvar(" & Variable & ")", ValorVariable)
                ElseIf TypeOf (ValorVariable) Is String Then
                    If Not ValorVariable.ToString.Contains(".") AndAlso Not ValorVariable.ToString.Contains("@") Then
                        Me.CCO = Me.CC.Replace("zvar(" & Variable & ")", ValorVariable)
                    Else
                        'Es una direccion de mail
                        R = ValorVariable.ToString
                        Me.CCO = Me.CCO.Replace("zvar(" & Variable & ")", R)
                    End If
                End If
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo CCO2: " & Me.CCO)
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    '''     Reconoce de un texto los ids de grupo o usuario, los nombres de grupo o usuario 
    '''         y devuelve la lista de mails
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  14/01/2011  Created
    ''' </history>
    Private Function ReconocerGruposYUsuarios(ByVal Text As String) As String
        Dim strReconocido As String = String.Empty
        Dim valorFinal As String = String.Empty
        Dim lsMails As New List(Of String)
        Dim email As ICorreo
        Dim usuarios As ArrayList = Nothing
        Dim grId As Int64
        Dim us As IUser

        'Antes se agregaba un espacio, pero estaba mal. Los grupos pueden contener espacios si se toma
        'al espacio como un separador, entonces el reconocimiento del grupo por su nombre falla.
        Dim valores() As String = Text.Trim.Split(New Char() {";", ","}, StringSplitOptions.RemoveEmptyEntries)

        For Each valor As String In valores
            valor = valor.Trim

            If Not String.IsNullOrEmpty(valor) Then
                'Si contiene arroba directamente lo agregamos como mail
                If valor.Contains("@") Then
                    If Not lsMails.Contains(valor) Then
                        lsMails.Add(valor)
                    End If
                Else
                    If IsNumeric(valor) Then
                        usuarios = UB.GetGroupUsers(Convert.ToInt64(valor))

                        'si el grupo tiene usuario obtenemos los mails de todos
                        If usuarios.Count > 0 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, $"El grupo tiene {usuarios.Count} usuarios")

                            For Each User As IUser In usuarios
                                email = UB.FillUserMailConfig(User.ID)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, $"El usuario {User.Name} en el grupo con email: {email}")
                                If email IsNot Nothing AndAlso email.Mail IsNot Nothing AndAlso Not lsMails.Contains(email.Mail) Then
                                    lsMails.Add(email.Mail)
                                End If
                            Next
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, $"El grupo tiene {usuarios.Count} usuarios, se verifica si es un usuario directo")
                            'Si no es grupo es usuario (usuarios.Count = 0)
                            email = UB.FillUserMailConfig(Convert.ToInt64(valor))
                            ZTrace.WriteLineIf(ZTrace.IsInfo, $"El usuario con id {valor} con email: {email}")
                            If email IsNot Nothing AndAlso Not lsMails.Contains(email.Mail) Then
                                lsMails.Add(email.Mail)
                            End If
                        End If

                    ElseIf TypeOf (valor) Is String Then
                        'Mismo procesamiento que para numeric solo que se obtiene el id de usuario o grupo
                        ' para el string pasado
                        Dim UserBusiness As New UserBusiness
                        Dim UserGroupBusiness As New UserGroupBusiness

                        us = UserBusiness.GetUserByname(valor, True)
                        grId = UserGroupBusiness.GetGroupIdByName(valor)

                        If Not IsNothing(us) Or grId <> 0 Then
                            usuarios = UserBusiness.GetGroupUsers(grId)

                            If usuarios.Count > 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, $"El grupo tiene {usuarios.Count} usuarios")
                                For Each User As IUser In usuarios
                                    email = UB.FillUserMailConfig(User.ID)
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, $"El usuario {User.Name} en el grupo con email: {email}")

                                    If email IsNot Nothing AndAlso Not lsMails.Contains(email.Mail) Then
                                        lsMails.Add(email.Mail)
                                    End If
                                Next
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, $"El grupo tiene {usuarios.Count} usuarios, se verifica si es un usuario directo")
                                'Si no es grupo es usuario (usuarios.Count = 0)
                                email = UB.FillUserMailConfig(us.ID)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, $"El usuario con id {valor} con email: {email}")

                                If email IsNot Nothing AndAlso Not lsMails.Contains(email.Mail) Then
                                    lsMails.Add(email.Mail)
                                End If
                            End If
                        Else
                            If Not lsMails.Contains(valor) Then
                                lsMails.Add(valor)
                            End If
                        End If
                    End If
                End If
            End If
        Next

        valores = Nothing
        email = Nothing
        us = Nothing
        If usuarios IsNot Nothing Then
            usuarios.Clear()
            usuarios = Nothing
        End If

        'Recorre la lista de mails y las agrega a un string
        For Each mail As String In lsMails
            If Not String.IsNullOrEmpty(mail) Then
                If String.IsNullOrEmpty(strReconocido) Then
                    strReconocido = mail
                Else
                    Select Case Zamba.Membership.MembershipHelper.CurrentUser.eMail.Type
                        Case Zamba.Core.MailTypes.LotusNotesMail
                            strReconocido = strReconocido + "," + mail
                        Case Zamba.Core.MailTypes.NetMail
                            strReconocido = strReconocido + ";" + mail
                        Case Zamba.Core.MailTypes.OutLookMail
                            strReconocido = strReconocido + ";" + mail
                        Case Else
                            strReconocido = strReconocido + ";" + mail
                    End Select
                End If
            End If
        Next

        Return strReconocido

    End Function

    Function PlayWeb(ByVal results As List(Of ITaskResult), ByVal Params As Hashtable) As List(Of ITaskResult)
        Dim mailServer As String
        Dim mailPass As String
        Dim mailUser As String
        Dim mailPort As String
        Dim smptMail As String
        Dim enableSsl As Boolean

        Try
            If IsNothing(Params) Then
                Params = New Hashtable
            End If

            Me.mails = New SortedList()
            Me.resultsAux = New System.Collections.Generic.List(Of Core.ITaskResult)

            Try
                MessagesBusiness.CheckHistoryExportPath()
            Catch ex As Exception
                If Not Me._myRule.Automatic Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en MessagesBusiness.CheckHistoryExportPath()")
                End If
                raiseerror(ex)
                Throw
            End Try

            If Me._myRule.UseSMTPConfig Then


                Dim valor As String = Me._myRule.SmtpServer
                valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(valor)
                If results.Count > 0 Then
                    valor = TextoInteligente.ReconocerCodigo(valor, results(0))
                End If
                mailServer = valor

                valor = Me._myRule.SmtpPass
                valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(valor)
                If results.Count > 0 Then
                    valor = TextoInteligente.ReconocerCodigo(valor, results(0))
                End If
                mailPass = valor

                valor = Me._myRule.SmtpUser
                valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(valor)
                If results.Count > 0 Then
                    valor = TextoInteligente.ReconocerCodigo(valor, results(0))
                End If
                mailUser = valor

                valor = Me._myRule.SmtpPort
                valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(valor)
                If results.Count > 0 Then
                    valor = TextoInteligente.ReconocerCodigo(valor, results(0))
                End If
                mailPort = valor

                valor = Me._myRule.SmtpMail
                valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(valor)
                If results.Count > 0 Then
                    valor = TextoInteligente.ReconocerCodigo(valor, results(0))
                End If
                smptMail = valor
                enableSsl = Me._myRule.SmtpEnableSsl

            Else
                Dim ZOptB As New ZOptBusiness

                Dim smtpConfig = New EmailBusiness().GetSMPTConfig()

                If smtpConfig IsNot Nothing Then
                    mailUser = smtpConfig.User
                    mailPass = smtpConfig.Pass
                    smptMail = smtpConfig.From
                    mailPort = smtpConfig.Port
                    mailServer = smtpConfig.MailServer
                    enableSsl = smtpConfig.EnableSSL

                ElseIf (ZOptB.GetValue("WebView_SendBySMTP") IsNot Nothing AndAlso ZOptB.GetValue("WebView_SendBySMTP").ToLower = "true") Then

                    mailUser = ZOptB.GetValue("WebView_UserSMTP")
                    mailPass = ZOptB.GetValue("WebView_PassSMTP")
                    smptMail = ZOptB.GetValue("WebView_FromSMTP")
                    mailPort = ZOptB.GetValue("WebView_PortSMTP")
                    mailServer = ZOptB.GetValue("WebView_SMTP")
                    Try
                        Boolean.TryParse(ZOptB.GetValue("WebView_SslSMTP"), enableSsl)
                    Catch ex As Exception
                    End Try

                ElseIf (Zamba.Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.NetMail) Then '// el Then usuario usa smtp?
                    mailServer = Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP
                    mailPass = Membership.MembershipHelper.CurrentUser.eMail.Password
                    mailUser = Membership.MembershipHelper.CurrentUser.eMail.UserName
                    mailPort = Membership.MembershipHelper.CurrentUser.eMail.Puerto
                    smptMail = Membership.MembershipHelper.CurrentUser.eMail.Mail
                    enableSsl = Membership.MembershipHelper.CurrentUser.eMail.EnableSsl
                Else
                    mailUser = ZOptB.GetValue("WebView_UserSMTP")
                    mailPass = ZOptB.GetValue("WebView_PassSMTP")
                    smptMail = ZOptB.GetValue("WebView_FromSMTP")
                    mailPort = ZOptB.GetValue("WebView_PortSMTP")
                    mailServer = ZOptB.GetValue("WebView_SMTP")
                    Try
                        Boolean.TryParse(ZOptB.GetValue("WebView_SslSMTP"), enableSsl)
                    Catch ex As Exception
                    End Try

                End If
            End If

            If (Me._myRule.groupMailTo) Then
                If (results.Count > 0) Then
                    results(0).AutoName = "sinAttach"
                End If

                For Each r As TaskResult In results
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtener las variables")
                    Me.mails = Me.ReemplazarVariables(r, Me.mails, Membership.MembershipHelper.CurrentUser.ID)
                Next
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Sin agrupacion")
                Me.resultsAux.Clear()
                Me.counter = 0
                For Each r As TaskResult In results
                    Dim mailpath As String
                    Me.resultAux = results(Me.counter)

                    Dim parameters As Hashtable = Me.ReemplazarVariables(r, Zamba.Membership.MembershipHelper.CurrentUser.ID)
                    Dim entry As DictionaryEntry
                    For Each entry In parameters
                        Params.Add(entry.Key, entry.Value)
                    Next

                    Params.Add("AttachLink", _myRule.AttachLink)
                    Params.Add("SendDocument", _myRule.SendDocument)

                    If (_myRule.ChildRulesIds IsNot Nothing) Then
                        Params.Add("NextRuleIds", String.Join(",", _myRule.ChildRulesIds))
                    End If

                    entry = Nothing

                    parameters.Clear()
                    parameters = Nothing

                    isSendDocument(Me.resultAux)


                    If (Me._myRule.Automatic) Then
                        Dim mail As SendMailConfig = Nothing
                        Dim Zoptb As New ZOptBusiness
                        Dim filePath As List(Of String) = New List(Of String)
                        Dim sUseWebService As String
                        Dim useWebService As Boolean

                        Try
                            If (Me._myRule.SendDocument) Then
                                '//ver cuando el volumen es -2
                                AttachOriginalDocument(r)
                                If IsNothing(originalDocument) AndAlso Not String.IsNullOrEmpty(r.FullPath) Then
                                    filePath.Add(r.FullPath)
                                End If
                            End If

                            If (Me._myRule.Body.ToLower().Contains("zamba.attach")) Then
                                '//ver cuando el volumen es -2
                                Dim startindex As Integer = Me._myRule.Body.IndexOf("zamba.attach(")
                                Dim endindex As Integer = Me._myRule.Body.IndexOf(Char.Parse(")"), startindex)
                                Dim attstr As String = Me._myRule.Body.Substring(startindex, endindex - startindex)
                                Dim filesvars As String() = attstr.Split(",")
                                For Each fi As String In filesvars
                                    filePath.Add(WFRuleParent.ReconocerVariablesValuesSoloTexto(fi))
                                Next

                            End If


                            Dim FlagAttach As Boolean = True

                            Dim _attachsResults As List(Of IResult) = Params("AssociatedResults")
                            mail = New SendMailConfig()

                            For Each CurrentResult As Result In _attachsResults
                                If Not IsNothing(CurrentResult) Then
                                    If String.IsNullOrEmpty(CurrentResult.FullPath) Then
                                        FlagAttach = False
                                        Exit For
                                    End If
                                Else
                                    FlagAttach = False
                                    Exit For
                                End If
                                If Not IsNothing(CurrentResult.DocType) AndAlso Not String.IsNullOrEmpty(CurrentResult.DocType.Name) Then
                                    Dim newFileName As String

                                    If (Not String.IsNullOrEmpty(CurrentResult.OriginalName)) AndAlso (Not IsNumeric(IO.Path.GetFileNameWithoutExtension(CurrentResult.OriginalName))) Then
                                        newFileName = MessagesBusiness.GetNewFile(CurrentResult.FullPath, IO.Path.GetFileName(CurrentResult.OriginalName))
                                    Else
                                        newFileName = MessagesBusiness.GetNewFile(CurrentResult.FullPath, CurrentResult.Name)
                                    End If

                                    If Not String.IsNullOrEmpty(newFileName) Then
                                        mail.Attachments.Add(New Net.Mail.Attachment(newFileName))
                                    End If
                                    '                                     flagTempResultCreated = True
                                Else
                                    mail.Attachments.Add(New Net.Mail.Attachment(MessagesBusiness.GetNewFile(CurrentResult.FullPath, CurrentResult.Name)))
                                End If
                            Next

                            sUseWebService = Zoptb.GetValue("UseWSSendMail")

                            If String.IsNullOrEmpty(sUseWebService) Then
                                useWebService = False
                            Else
                                useWebService = Boolean.Parse(sUseWebService)
                            End If

                            With mail
                                .MailType = MailTypes.NetMail
                                .SMTPServer = mailServer
                                .From = smptMail
                                .Port = mailPort
                                .UserName = mailUser
                                .Password = mailPass
                                .MailTo = Params("To")
                                .Cc = Params("CC")
                                .Cco = Params("CCO")
                                .Subject = Params("Subject").ToString().Replace(Environment.NewLine, " ").Replace(vbLf, " ")
                                .Body = Params("Body")
                                .IsBodyHtml = True
                                .AttachFileNames = filePath
                                .UserId = Membership.MembershipHelper.CurrentUser.ID
                                .ImagesToEmbedPaths = Params("Link")
                                .OriginalDocument = originalDocument
                                .OriginalDocumentFileName = originalDocumentFileName
                                .EnableSsl = enableSsl
                                .UseWebService = useWebService
                                .SourceDocId = r.ID
                                .SourceDocTypeId = r.DocTypeId
                                .LinkToZamba = _myRule.AttachLink
                                .SaveHistory = MessagesBusiness.IsEmailHistoryEnabled()
                            End With

                            MessagesBusiness.SendMail(mail)


                            'si es responder = true llamar
                            Dim rb As New Results_Business

                            If (Me._myRule.Answer And VariablesInterReglas.ContainsKey("rutaDocumento")) Then

                                Dim sRes = New SResult()
                                Dim exportPath As String = String.Empty
                                For i As Int32 = 0 To results.Count - 1

                                    Dim ZO As New SZOptBusiness()
                                    Dim Ef As New Zamba.Data.Email_Factory
                                    exportPath = Ef.CreateExportFolder(Convert.ToInt32(results.Item(i).ID))
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Ruta exportacion: " + exportPath)

                                    Dim MailTo As String = mail.From.ToString()
                                    Dim CC As String = String.Empty
                                    If Not IsNothing(mail.Cc) Then
                                        CC = mail.Cc.ToString()
                                    End If

                                    Dim DocID As Long = mail.SourceDocId

                                    Dim from = ZO.GetValue("WebView_FromSMTP")
                                    Dim MailPathVariable As String = "MailPathVariable"

                                    Ef.SaveMsgFromDomail(mail.MailTo, CC, mail.Subject, mail.Body, DocID, MailPathVariable, from, exportPath)


                                    Dim GetMailPathVariable As String = VariablesInterReglas.Item(MailPathVariable)
                                    Dim Res = sRes.GetResult(Convert.ToInt64(results.Item(i).ID), results.Item(i).DocTypeId, True)
                                    rb.ReplaceDocument(Res, GetMailPathVariable.ToString(), False, Nothing)
                                Next


                            End If

                            Return results
                        Catch ex As Exception
                            raiseerror(ex)
                            Throw

                        Finally
                            filePath = Nothing
                            Zoptb = Nothing
                            If filePath IsNot Nothing Then
                                filePath.Clear()
                                filePath = Nothing
                            End If
                            If mail IsNot Nothing Then
                                mail.Dispose()
                                mail = Nothing
                            End If
                        End Try
                    End If

                    Me.counter = Me.counter + 1
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
        Return results
    End Function

    Private Sub AttachOriginalDocument(ByVal result As TaskResult)
        Try
            Dim file As Byte()
            Dim Zopt As ZOptBusiness = New ZOptBusiness()
            Dim SResult As New SResult()

            If result.Disk_Group_Id > 0 AndAlso
                        (VolumesBusiness.GetVolumeType(result.Disk_Group_Id) = Convert.ToInt32(VolumeType.DataBase) OrElse
                        (Not String.IsNullOrEmpty(Zopt.GetValue("ForceBlob")) AndAlso Convert.ToBoolean(Zopt.GetValue("ForceBlob")))) Then
                SResult.LoadFileFromDB(result)
            End If

            If Not IsNothing(result.EncodedFile) Then
                file = result.EncodedFile
            Else
                Dim sUseWebService As String = Zopt.GetValue("UseWebService")

                If Not String.IsNullOrEmpty(sUseWebService) AndAlso Convert.ToBoolean(sUseWebService) Then
                    Dim resB As New Results_Business()
                    file = resB.GetWebDocFileWS(result.DocTypeId, result.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID)
                    resB = Nothing
                Else
                    Dim isBlob As Boolean
                    file = SResult.GetFileFromResultForWeb(result, isBlob)
                End If
            End If

            If Not IsNothing(file) AndAlso file.Length > 0 Then
                originalDocument = file
                originalDocumentFileName = result.Doc_File
            End If

            file = Nothing
            Zopt = Nothing
            SResult = Nothing

        Catch ex As Exception
        End Try
    End Sub

End Class

Public Class MailActions

    Private _smtpConfig As Hashtable

    Sub New(ByVal smtpconfig As Hashtable)
        Me._smtpConfig = smtpconfig
    End Sub

#Region "Mensajes"
    ''' <summary>
    ''' Obtiene los nombres originales de una lista de documentos y 
    ''' los inserta en una lista de String.
    ''' </summary>
    ''' <param name="attachResults">Lista de documentos asociados</param>
    ''' <history> 
    '''     [Tomas] 09/12/2009  Created
    ''' </history>
    Private Sub SetOriginalFileNames(ByVal attachResults As Generic.List(Of IResult))
        Dim name As String = String.Empty
        For i As Int32 = 0 To attachResults.Count - 1
            If Not attachResults(i).ISVIRTUAL Then
                'Se asigna el nombre del archivo original
                name = attachResults(i).OriginalName
                attachResults(i).Name = name.Substring(0, name.LastIndexOf(".")).Substring(name.LastIndexOf("\") + 1)
            End If
        Next
        name = Nothing
    End Sub

#End Region


End Class
