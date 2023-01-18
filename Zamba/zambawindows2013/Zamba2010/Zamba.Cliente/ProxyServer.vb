Imports Zamba.Core.Search
Imports System.Collections.Generic
Imports Zamba.Core
Imports System.ComponentModel
Imports Zamba.Core.WF.WF

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Cliente
''' Class	 : Client.ProxyServer
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear un servidor como objeto Remoting
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class ProxyServer
    Inherits MarshalByRefObject
    Implements Zamba.Core.IZRemoting

    Public Function IsRunning() As Boolean Implements Core.IZRemoting.IsRunning

    End Function

    Private Sub Maximizar() Implements Core.IZRemoting.Maximizar
        ModDocuments.Maximizar()
    End Sub

    Public Shared Event ValidateLogin()
    Public Shared Event ShowTask(ByVal TaskId As Int64, ByVal stepId As Int64, ByVal docTypeId As Int64)
    Public Shared Event ShowResult(ByVal Result As IResult)
    Public Shared Event InsertFile(ByVal File As String)


    ''' <summary>
    ''' Ejecución
    ''' </summary>
    ''' <param name="Action"></param>
    ''' <param name="Argument"></param>
    ''' <param name="obs"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  Modified    30/11/2010  Se corrige Raiseevent Showtask(), se pasaban mal los parámetros
    ''' </history>
    Public Function Run(ByRef Action As String, ByVal Argument As String, ByVal obs As Hashtable) As Boolean Implements Core.IZRemoting.Run
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Argumento: " & Argument)
        Try

            If Argument.ToUpper().Contains("USERID=") AndAlso (Argument.ToUpper().Contains("ZAMBA:\\") OrElse Argument.ToUpper().Contains("ZAMBA://")) Then
                Dim usrWithId As String = Argument.Substring(Argument.IndexOf("USERID"), Argument.Length - Argument.IndexOf("USERID"))
                Argument = Argument.Remove(Argument.IndexOf("USERID"), Argument.Length - Argument.IndexOf("USERID"))

                Dim userId = Int64.Parse(usrWithId.Split("=")(1))

                If Membership.MembershipHelper.CurrentUser Is Nothing Then
                    If Not IsNothing(UserBusiness.Rights.ValidateLogIn(userId, ClientType.Desktop)) Then
                        'Dim timeout As Integer = UserPreferences.getValue("TimeOut", Sections.UserPreferences, "20")
                        'Dim winusername As String = UserGroupBusiness.GetUserorGroupNamebyId(userId)
                        'UcmServices.Login(timeout, "Cliente", userId, winusername, Environment.MachineName, 0, ServiceTypes.Report)
                        MainForm.UserValidatedFlag = True
                    End If
                End If

            ElseIf Argument.Contains("USERID=") Then

                Dim userId = Int64.Parse((Argument.Split(Char.Parse("="))(1)).Split(Char.Parse(" "))(0))

                'Remuevo los caracteres que no sirven para los parámetros.
                Argument = Argument.Replace("USERID=" & userId.ToString, "")
                Argument = Argument.Trim()

                If Membership.MembershipHelper.CurrentUser Is Nothing Then

                    If Not IsNothing(UserBusiness.Rights.ValidateLogIn(userId, ClientType.Desktop)) Then
                        'Dim timeout As Integer = UserPreferences.getValue("TimeOut", Sections.UserPreferences, "20")
                        'Dim winusername As String = UserGroupBusiness.GetUserorGroupNamebyId(userId)
                        'UcmServices.Login(timeout, "Cliente", userId, winusername, Environment.MachineName, 0, ServiceTypes.Report)
                    End If
                End If

            Else
                'If MainForm.UserValidatedFlag = False Then
                Dim legalresult As String = CheckForLegalNotice()

                If legalresult = "cerrando" Then
                    Action = legalresult
                End If
                'End If

            End If

            If Argument.IndexOf("zinsc", StringComparison.CurrentCultureIgnoreCase) <> -1 Then
                RaiseEvent InsertFile(Argument.Replace("zinsc", String.Empty))
                Exit Function
            End If

            Select Case Argument.ToLower
                Case "expt"
                    ' ExportMode = True
                Case "demo"
                Case "newv"
                    Try
                        ' Results_Factory.InsertNewVersion(Environment.GetCommandLineArgs(2), True)
                    Catch
                    End Try
                Case "info"
                Case "zinsc"
                    'Batch = New ZBatch(0, 9999)
                    'Batch.Name = "Insercion con copia " & Now.ToString
                    'Dim i As Int32
                    'For i = 2 To Environment.GetCommandLineArgs.Length - 1
                    '    Dim Arg As String = Environment.GetCommandLineArgs(i)
                    '    If New IO.FileInfo(Arg).Exists Then
                    '        Batch.Results.Add(Arg)
                    '    End If
                    'Next

                Case "insm"
                    'Batch = New ZBatch(0, 9999)
                    'Batch.Name = "Mover Documento a Zamba " & Now.ToString
                    'Dim i As Int32
                    'For i = 2 To Environment.GetCommandLineArgs.Length - 1
                    '    Dim Arg As String = Environment.GetCommandLineArgs(i)
                    '    If New IO.FileInfo(Arg).Exists Then
                    '        Batch.Results.Add(Arg)
                    '    End If
                    'Next
                Case "correctupdate"
                    Dim updateVersion As String = UpdaterBusiness.GetLastestVersion()
                    UpdaterBusiness.UpdateEstreg(updateVersion, Membership.MembershipHelper.CurrentUser.Name)
                    UpdaterBusiness.FinalizarUpdate()
                Case "wrongupdate"
                    UpdaterBusiness.FinalizarUpdate()
                Case "save"
                    'Try
                    '    Insert(Environment.GetCommandLineArgs(2))
                    'Catch ex As Exception
                    'End Try
                Case "sear"
                Case "view"
                    'Batch = New ZBatch(0, 9999)
                    'Batch.Name = "Insercion con copia " & Now.ToString
                    'Dim i As Int32
                    'For i = 2 To Environment.GetCommandLineArgs.Length - 1
                    '    Dim Arg As String = Environment.GetCommandLineArgs(i)
                    '    If New IO.FileInfo(Arg).Exists Then
                    '        Batch.Results.Add(Arg)
                    '    End If
                    'Next
            End Select
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de Argumentos: " & Environment.GetCommandLineArgs.Length)
        If Argument.ToLower.StartsWith("zamba:\\\\") Then
            Argument = Argument.Remove(0, 10)
        End If

        If Argument.ToLower.StartsWith("zamba:\\") OrElse Argument.ToLower.StartsWith("zamba://") Then
            Argument = Argument.Remove(0, 8)
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, "-------------------------------------------------------------------")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cadena: " & Argument)



        If Argument.ToLower.StartsWith("dt=") Then
            If Membership.MembershipHelper.CurrentUser Is Nothing Then
                RaiseEvent ValidateLogin()
            End If

            If Argument.EndsWith("/") Then Argument = Argument.Substring(0, Argument.Length - 1)
            If Argument.EndsWith("\") Then Argument = Argument.Substring(0, Argument.Length - 1)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "3: " & Argument)
            Dim DocTypeId As Int64
            Dim result As Zamba.Core.Result
            Dim RB As New Results_Business
            Dim WFTB As New WFTaskBusiness
            Try
                MainForm.LoadMainform1()
                If IsNothing(Membership.MembershipHelper.CurrentUser) Then MainForm.LoadMainform2(False)
                Dim campos() As String = Argument.Trim.Split("&")
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Campos: " & campos.Length)
                DocTypeId = CInt(campos(0).ToLower.Replace("dt=", String.Empty).Trim)

                If campos.Length > 2 Then

                    Dim DocType As DocType = DocTypesBusiness.GetDocType(DocTypeId, True)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "DocType: " & DocType.Name)
                    DocType.Indexs = ZCore.FilterIndex(DocType.ID)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "4")
                    result = RB.GetSearchResult(DocType, Membership.MembershipHelper.CurrentUser.ID)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "FOR I=0 to " & campos.Length - 2)
                    'Check all the indexs and save the ones who match on the result
                    RB = Nothing

                    Dim indexFlag As Boolean = False
                    Dim i As Int32
                    Dim J As Int16
                    For i = 1 To campos.Length - 1 ' The first is the docType so skip it
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "I=" & i)
                        If (campos(i).Length > 0) Then
                            'indexComp(0) = indexID, indexComp(1) = indexData 
                            Dim indexComp() As String = campos(i).Split("=")
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "FirstIndex=" & indexComp(0))
                            If String.Compare(indexComp(0).ToUpper, "DOCID") <> 0 Then
                                'Esta verificación se realiza para Lotus Notes
                                If UCase(indexComp(0)) = "MAIL" Then
                                    indexComp(0) = UserPreferences.getValue("ilmIndex", UPSections.UserPreferences, 110)
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "FirstIndexMail=" & indexComp(0))
                                End If
                                For J = 0 To result.Indexs.Count - 1
                                    'If id matchs one of the index save the data on the result
                                    If result.Indexs(J).ID = indexComp(0) Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Result.Indexs(" & indexComp(0) & ").datatemp=" & indexComp(1))
                                        result.Indexs(J).Data = indexComp(1)
                                        result.Indexs(J).DataTemp = indexComp(1)
                                        indexFlag = True
                                        Exit For
                                    End If
                                Next
                            Else

                                ''Dim BW As New System.ComponentModel.BackgroundWorker
                                ''AddHandler BW.DoWork, AddressOf DoSearch
                                ''BW.RunWorkerAsync(indexComp)
                                ''---------------
                                ''Dim doWork As DoSearchEvntEventHandler

                                ''AddHandler DoSearchEvnt, doWork
                                ''doWork.Invoke(indexComp, DocTypeId)

                                'Dim dowork As New DoSearchEvntEventHandler(AddressOf DoSearch)

                                'dowork.Invoke(indexComp, DocTypeId)

                                Return True
                            End If
                        End If
                    Next

                    'If their is at least one index to do the search
                    If indexFlag = True Then
                        Dim DTs As New List(Of IDocType)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "6")
                        DTs.Add(DocType)
                        Dim index As New Index
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "7")
                        Dim Search As New Zamba.Core.Searchs.Search(result.Indexs, String.Empty, DTs, True, String.Empty)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "8")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Preparando busqueda")
                        If Not IsNothing(Search) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando busqueda")
                            ModDocuments.DoSearch(Search, Membership.MembershipHelper.CurrentUser.ID, Nothing, 0,
                                          Int32.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100)),
                                          True, False, FilterTypes.Document, False, Nothing,, True)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Busqueda ejecutada")
                        End If
                    End If

                Else
                    Dim DocId As Int64 = CInt(campos(1).ToLower.Replace("docid=", String.Empty).Trim)

                    Dim Task As ITaskResult = WFTB.GetTaskByDocId(DocId)

                    If Task Is Nothing Then
                        result = Results_Business.GetResult(DocId, DocTypeId)

                        If Not IsNothing(result) Then
                            RaiseEvent ShowResult(result)
                        Else
                            RaiseEvent ShowTask(0, 0, 0)
                        End If
                    Else
                        RaiseEvent ShowTask(Task.TaskId, Task.StepId, Task.DocTypeId)
                    End If
                End If

                Return True
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
                Return False
            Finally
                WFTB = Nothing
            End Try
        ElseIf Argument.ToLower.StartsWith("i") Then
            SearchLink(Argument)
        ElseIf Argument.ToLower.StartsWith("taskid") Then
            Dim taskid As Int64 = 0
            Argument = Argument.Split("=")(1).Replace("/", String.Empty)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Argumento de la tarea:" & Argument)
            Int64.TryParse(Argument, taskid)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Id de la tarea:" & taskid)
            Dim WFTB As New WFTaskBusiness
            Dim Task As ITaskResult = WFtb.GetTask(taskid)
            WFTB = Nothing
            If Not IsNothing(Task) Then
                RaiseEvent ShowTask(taskid, Task.StepId, Task.DocTypeId)
            Else
                RaiseEvent ShowTask(0, 0, 0)
            End If
        End If
    End Function

    Public Delegate Function DoWorkDoSearch()
    Public Event DoSearchEvnt(indexComp() As String, ByVal docTypeId As Long)

    Function DoSearch(indexComp() As String, ByVal docTypeId As Long)
        Dim docid(0) As String
        docid.SetValue(indexComp(1), 0)
        Threading.Thread.CurrentThread.Sleep(2000)
        ModDocuments.DoSearch(docTypeId, docid, True)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Busca un archivo en base al DoctypeID y al DOC_ID
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	24/07/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------

    Public Shared Function CheckForLegalNotice() As String

        Dim frmResult As DialogResult
        Dim noticiaLegal As String = ZOptBusiness.GetValue("LegalNoticeMessage")

        If (String.IsNullOrEmpty(noticiaLegal) = True) Then

            Dim message As String = "Este sistema es para ser utilizado solamente por usuarios autorizados. Toda la información contenida en los sistemas es propiedad de la Empresa y pueden ser supervisados, cifrados, leídos, copiados o capturados y dados a conocer de alguna manera, solamente por personas autorizadas." & Convert.ToChar(13) & "El uso del sistema por cualquier persona, constituye de su parte un expreso consentimiento al monitoreo, intervención, grabación, lectura, copia o captura y revelación de tal intervención." & Convert.ToChar(13) & "EL usuario debe saber que en la utilización del sistema no tendrá privacidad frente a los derechos de la empresa responsable del sistema." & Convert.ToChar(13) & "El uso indebido o no autorizado de este sistema genera  responsabilidad para el infractor, quién por ello estará sujeto al resultado de las acciones civiles y penales que la Empresa considere pertinente realizar en defensa de sus derechos y resguardo de la privacidad del sistema." & Convert.ToChar(10) & "Si Usted no presta conformidad con las reglas precedentes y no está de acuerdo con ellas, desconéctese ahora."
            frmResult = MessageBox.Show(message, "Noticia Legal", MessageBoxButtons.OKCancel)

        ElseIf noticiaLegal = "NotShow" Then
            frmResult = 1
        Else
            frmResult = 1
            frmResult = MessageBox.Show(noticiaLegal, "Noticia Legal", MessageBoxButtons.OKCancel.OKCancel)

        End If

        If frmResult = DialogResult.Cancel Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se acepto el mensaje legal, se cierra la aplicacion.")
            Return "cerrando"
        End If
        Return String.Empty
    End Function

    Public Function SearchLink(ByVal Line As String) As Boolean
        'Zamba:\\I233|125423&3445245& 2345555
        'DTID|Idoc_id & doc_id & doc_id & .....& doc_id
        Line = Line.Replace("Zamba:\\", "")
        Dim DocTypeId As Int16 = Line.Split("|")(0).Replace("i", "")
        Dim DocIds() As String = Line.Split("|")(1).Split("&")
        'ModDocuments.SearchRows(DocTypeId, DocIds)
        ModDocuments.DoSearch(DocTypeId, DocIds, True)
    End Function

    Public Function ExecuteRule(ByVal ruleId As Int64, ByVal stepId As Int64, ByVal results As List(Of ITaskResult)) As List(Of ITaskResult) Implements IZRemoting.ExecuteRule
        Return New List(Of ITaskResult)

    End Function

    Public Function ExecuteRule(ByVal RuleId As Int64, ByRef _results As List(Of ITaskResult)) As Object Implements IZRemoting.ExecuteRule
        Return New Object
    End Function
End Class
