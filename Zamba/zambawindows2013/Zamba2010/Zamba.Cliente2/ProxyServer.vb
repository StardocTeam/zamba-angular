Imports Zamba.Core.Search
Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports Zamba.Core
Imports System.Data

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
    Public Function Run(ByVal Action As String, ByVal Argument As String, ByVal obs As Hashtable) As Boolean Implements Core.IZRemoting.Run
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Argumento: " & Argument)
        Try

            If Argument.ToUpper().Contains("USERID=") And Argument.ToUpper().Contains("ZAMBA:\\") Then
                Dim usrWithId As String = Argument.Substring(Argument.IndexOf("USERID"), Argument.Length - Argument.IndexOf("USERID"))
                Argument = Argument.Remove(Argument.IndexOf("USERID"), Argument.Length - Argument.IndexOf("USERID"))

                Dim userId = Int64.Parse(usrWithId.Split("=")(1))

                If Not IsNothing(UserBusiness.Rights.ValidateLogIn(userId, ClientType.Desktop)) Then
                    'Dim timeout As Integer = UserPreferences.getValue("TimeOut", Sections.UserPreferences, "20")
                    'Dim winusername As String = UserGroupBusiness.GetUserorGroupNamebyId(userId)
                    'UcmServices.Login(timeout, "Cliente", userId, winusername, Environment.MachineName, 0, ServiceTypes.Report)
                End If

            ElseIf Argument.Contains("USERID=") Then

                Dim userId = Int64.Parse((Argument.Split(Char.Parse("="))(1)).Split(Char.Parse(" "))(0))

                'Remuevo los caracteres que no sirven para los parámetros.
                Argument = Argument.Replace("USERID=" & userId.ToString, "")
                Argument = Argument.Trim()

                If Not IsNothing(UserBusiness.Rights.ValidateLogIn(userId, ClientType.Desktop)) Then
                    'Dim timeout As Integer = UserPreferences.getValue("TimeOut", Sections.UserPreferences, "20")
                    'Dim winusername As String = UserGroupBusiness.GetUserorGroupNamebyId(userId)
                    'UcmServices.Login(timeout, "Cliente", userId, winusername, Environment.MachineName, 0, ServiceTypes.Report)
                End If

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
                    UpdaterBusiness.UpdateEstreg(updateVersion)
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
            Dim userid As Int64
            Try
                userid = Int64.Parse(UserPreferences.getValue("UserId", Sections.UserPreferences, 0))
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Usuario: " & userid.ToString())
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
                MessageBox.Show(ex.ToString)
            End Try

            ZTrace.WriteLineIf(ZTrace.IsInfo, "1")

            'If Membership.MembershipHelper.CurrentUser Is Nothing Then
            '    RaiseEvent ValidateLogin()
            'End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "2")
            ' Ejemplo
            ' Zamba:\\DT=56 & 16=32006 & 18=159 & Mail=C5F7D64795B21B1C032570810049535E
            '             initialmode = initialmode.Substring(8)

            If Argument.EndsWith("/") Then Argument = Argument.Substring(0, Argument.Length - 1)
            If Argument.EndsWith("\") Then Argument = Argument.Substring(0, Argument.Length - 1)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "3: " & Argument)
            Dim DocTypeId As Int64
            Dim result As Zamba.Core.Result
            Try
                MainForm.TareasMantenimientoInicialesdeZamba()
                MainForm.LoadMainform1()
                If IsNothing(Membership.MembershipHelper.CurrentUser) Then MainForm.LoadMainform2(False)
                Dim campos() As String = Argument.Trim.Split("&")
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Campos: " & campos.Length)
                DocTypeId = CInt(campos(0).ToLower.Replace("dt=", String.Empty).Trim)

                If campos.Count > 2 Then

                    Dim DocType As DocType = DocTypesBusiness.GetDocType(DocTypeId, True)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "DocType: " & DocType.Name)
                    DocType.Indexs = ZCore.FilterIndex(DocType.ID)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "4")
                    result = Zamba.Core.Results_Business.GetSearchResult(DocType, userid)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "FOR I=0 to " & campos.Length - 2)
                    'Check all the indexs and save the ones who match on the result
                    Dim indexFlag As Boolean = False
                    Dim i As Int32
                    Dim J As Int16
                    For i = 1 To campos.Length - 1 ' The first is the docType so skip it
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "I=" & i)
                        'indexComp(0) = indexID, indexComp(1) = indexData 
                        Dim indexComp() As String = campos(i).Split("=")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "FirstIndex=" & indexComp(0))
                        If String.Compare(indexComp(0).ToUpper, "DOCID") <> 0 Then
                            'Esta verificación se realiza para Lotus Notes
                            If UCase(indexComp(0)) = "MAIL" Then
                                indexComp(0) = UserPreferences.getValue("ilmIndex", Sections.UserPreferences, 110)
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
                            Dim docid(0) As String
                            docid.SetValue(indexComp(1), 0)

                            ModDocuments.DoSearch(DocType.ID, docid, True)
                            Return True
                        End If
                    Next

                    'If their is at least one index to do the search
                    If indexFlag = True Then
                        Dim DTs As New List(Of IDocType)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "6")
                        DTs.Add(DocType)
                        Dim index As New Index
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "7")
                        Dim Search As New Searchs.Search(result.Indexs, String.Empty, DTs, True, String.Empty)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "8")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Preparando busqueda")
                        If Not IsNothing(Search) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando busqueda")
                            ModDocuments.DoSearch(Search, Membership.MembershipHelper.CurrentUser.ID, Nothing, 0,
                                              Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)),
                                              True, False, FilterTypes.Document, False, Nothing,, True)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Busqueda ejecutada")
                        End If
                    End If

                Else
                    Dim DocId As Int64 = CInt(campos(1).ToLower.Replace("docid=", String.Empty).Trim)

                    Dim Task As ITaskResult = Zamba.Core.WF.WF.WFTaskBusiness.GetTaskByDocId(DocId)

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
            End Try
        ElseIf Argument.ToLower.StartsWith("i") Then
            Me.SearchLink(Argument)
        ElseIf Argument.ToLower.StartsWith("taskid") Then
            Dim taskid As Int64 = 0
            Argument = Argument.Split("=")(1).Replace("/", String.Empty)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Argumento de la tarea:" & Argument)
            Int64.TryParse(Argument, taskid)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Id de la tarea:" & taskid)
            Dim Task As ITaskResult = WF.WF.WFTaskBusiness.GetTask(taskid)

            If Not IsNothing(Task) Then
                RaiseEvent ShowTask(taskid, Task.StepId, Task.DocTypeId)
            Else
                RaiseEvent ShowTask(0, 0, 0)
            End If
        End If
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
