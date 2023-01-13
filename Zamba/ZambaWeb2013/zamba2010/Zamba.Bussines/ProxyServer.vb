Imports Zamba.Core.Search
Imports Zamba.Filters
Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports Zamba.Membership

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

    Public Shared Event ValidateLogin()
    Public Shared Event ShowTask(ByVal TaskId As Int64, ByVal stepId As Int64, ByVal docTypeId As Int64)
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
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Argumento: " & Argument)
        Try
            'initialmode = Environment.GetCommandLineArgs(1)
            If Argument.IndexOf("zinsc") <> -1 Then

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
            Try
                'Batch = New ZBatch(0, 9999)
                'Batch.Name = "Insercion con copia " & Now.ToString
                'Dim i As Int32
                'For i = 1 To Environment.GetCommandLineArgs.Length - 1
                '    Dim Arg As String = Environment.GetCommandLineArgs(i)
                '    If New IO.FileInfo(Arg).Exists Then
                '        Batch.Results.Add(Arg)
                '    End If
                'Next
            Catch
            End Try
        End Try
        '''''''''''''''''''''''''''''''''''''
        'Para Mails exportados. LOTUS NOTES '
        '''''''''''''''''''''''''''''''''''''
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Cantidad de Argumentos: " & Environment.GetCommandLineArgs.Length)
        '   Dim k As Int32
        'initialmode = ""
        'For k = 1 To Environment.GetCommandLineArgs.Length - 1
        '    initialmode &= " " & Environment.GetCommandLineArgs(k)
        'Next

        If Argument.ToLower.StartsWith("zamba:\\") Then
            Argument = Argument.Remove(0, 8)
        ElseIf Argument.ToLower.StartsWith("zamba://") Then
            Argument = Argument.Remove(0, 8)
        End If

        ZTrace.WriteLineIf(ZTrace.IsVerbose, "-------------------------------------------------------------------")
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Cadena: " & Argument)

        Dim core As ZCore = ZCore.GetInstance()
        core.LoadCore()

        If Argument.ToLower.StartsWith("dt=") Then
            Dim userid As Int64
            Try
                userid = Int64.Parse(UserPreferences.getValue("UserId", Sections.UserPreferences, 0))
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Usuario: " & userid.ToString())
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, ex.ToString)
                MessageBox.Show(ex.ToString)
            End Try

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "1")

            If MembershipHelper.CurrentUser Is Nothing Then
                RaiseEvent ValidateLogin()
            End If

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "2")
            If Argument.EndsWith("/") Then Argument = Argument.Substring(0, Argument.Length - 1)
            If Argument.EndsWith("\") Then Argument = Argument.Substring(0, Argument.Length - 1)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "3: " & Argument)
            Dim DocTypeId As Int32
            Dim result As Zamba.Core.Result
            Try
                Dim campos() As String = Argument.Trim.Split("&")
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Campos: " & campos.Length)
                DocTypeId = CInt(campos(0).ToLower.Replace("dt=", String.Empty).Trim)
                Dim DocType As DocType = DocTypesBusiness.GetDocType(DocTypeId, True)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "DocType: " & DocType.Name)
                DocType.Indexs = core.FilterIndex(DocType.ID)

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "4")
                result = Zamba.Core.Results_Business.GetSearchResult(DocType, userid)
                Dim J As Int16
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "FOR I=0 to " & campos.Length - 2)

                'Check all the indexs and save the ones who match on the result
                Dim indexFlag As Boolean = False
                Dim i As Int32
                For i = 1 To campos.Length - 1 ' The first is the docType so skip it
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "I=" & i)
                    Dim indexComp() As String = campos(i).Split("=")
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "FirstIndex=" & indexComp(0))
                    If String.Compare(indexComp(0).ToUpper, "DOCID") <> 0 Then
                        If UCase(indexComp(0)) = "MAIL" Then
                            indexComp(0) = UserPreferences.getValue("ilmIndex", Sections.UserPreferences, 110)
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "FirstIndexMail=" & indexComp(0))
                        End If
                        For J = 0 To result.Indexs.Count - 1
                            'If id matchs one of the index save the data on the result
                            If result.Indexs(J).id = indexComp(0) Then
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Result.Indexs(" & indexComp(0) & ").datatemp=" & indexComp(1))
                                result.Indexs(J).data = indexComp(1)
                                result.Indexs(J).datatemp = indexComp(1)
                                indexFlag = True
                                Exit For
                            End If
                        Next
                    Else
                        Dim docid(0) As String
                        docid.SetValue(indexComp(1), 0)
                        Zamba.Core.Search.ModDocuments.DoSearch(DocType.ID, docid)

                        Return True
                    End If
                Next

                'If their is at least one index to do the search
                If indexFlag = True Then
                    Dim DTs As New ArrayList
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "6")
                    DTs.Add(DocType)
                    Dim index As New Index
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "7")
                    Dim Search As New Searchs.Search(result.Indexs.ToArray(index.GetType), String.Empty, False, String.Empty, String.Empty, DTs.ToArray(DocType.GetType), True, String.Empty)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "8")
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Preparando busqueda")
                    If Not IsNothing(Search) Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Iniciando busqueda")
                        Dim MD As New Zamba.Core.Search.ModDocuments
                        MD.DoSearch(Search, MembershipHelper.CurrentUser.ID, 0, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)))
                        MD = Nothing
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Busqueda ejecutada")
                    End If
                End If
                Return True
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, ex.ToString)
                Return False
            End Try
        ElseIf Argument.ToLower.StartsWith("taskid") Then
            Dim taskid As Int64 = 0
            Argument = Argument.Split("=")(1).Replace("/", String.Empty)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Argumento de la tarea:" & Argument)
            Int64.TryParse(Argument, taskid)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Id de la tarea:" & taskid)
            Dim ds As DataSet = WF.WF.WFTaskBusiness.GetTaskDs(taskid)

            If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                RaiseEvent ShowTask(taskid, Int64.Parse(ds.Tables(0).Rows(0)("step_id").ToString()), Int64.Parse(ds.Tables(0).Rows(0)("doc_type_id").ToString()))
            End If
        End If
    End Function

    Public Function ExecuteRule(ByVal ruleId As Int64, ByVal stepId As Int64, ByVal results As List(Of ITaskResult)) As List(Of ITaskResult) Implements IZRemoting.ExecuteRule
        Return New List(Of ITaskResult)
    End Function

    Public Function ExecuteRule(ByVal RuleId As Int64, ByRef _results As List(Of ITaskResult)) As Object Implements IZRemoting.ExecuteRule
        Return New Object
    End Function
End Class
