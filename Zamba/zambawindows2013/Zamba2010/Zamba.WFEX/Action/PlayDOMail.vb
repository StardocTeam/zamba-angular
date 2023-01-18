Imports Zamba.Core
Imports System.Windows.Forms
Imports System.IO
Imports Zamba.AdminControls
Imports Zamba.Viewers
Imports Zamba.Controls

Public Class PlayDOMail

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
    Private link As ArrayList
    Private para2 As String
    Private R As String
    Private _smtpConfig As Hashtable
    Private _mailPath As String
    Private txtValue As String
    Private txtFilterDocID As String
    Private attachsAux As String
    Private BtnRuleName As String
    Private BtnAdditionalRuleName As String
    Private ColumnNameNumber As String
    Private ColumnRouteNumber As String
    Private AdditionalColumnNameNumber As String
    Private AdditionalColumnRouteNumber As String

    Sub New(ByVal rule As IDOMail)
        Me._myRule = rule
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
        Try

            Me.mails = New SortedList()
            Me.resultsAux = New System.Collections.Generic.List(Of Core.ITaskResult)
            Me.Params = New Hashtable()
            Me.link = New ArrayList()
            Me.attachsAux = Me._myRule.PathImages

            'se utiliza esta variable para sobreescribir su variableInterRegla  
            'ya que es modificada en tiempo de ejecucion por otra regla
            Dim Attach As String = Nothing

            Try
                ' [AlejandroR] - 05/03/2010 - Created
                ' Se chequea antes de enviar el mail que se tenga configurada y con acceso
                ' la ruta para el historial de emails, si falla se cancela la regla
                MessagesBusiness.CheckHistoryExportPath()
            Catch ex As Exception
                If Not Me._myRule.Automatic Then
                    MessageBox.Show(ex.Message, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Trace.WriteLineIf(ZTrace.IsWarning, "Ocurrió un error al verificar la ruta para el historial de mails")
                raiseerror(ex)
                Throw ex
            End Try

            If Me._myRule.UseSMTPConfig Then
                Me._smtpConfig = New Hashtable
                With Me._smtpConfig
                    Dim valor As String = Me._myRule.SmtpServer
                    valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(valor)
                    If results.Count > 0 Then
                        valor = TextoInteligente.ReconocerCodigo(valor, results(0))
                    End If
                    .Add("Server", valor)

                    valor = Me._myRule.SmtpPass
                    valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(valor)
                    If results.Count > 0 Then
                        valor = TextoInteligente.ReconocerCodigo(valor, results(0))
                    End If
                    .Add("Pass", valor)

                    valor = Me._myRule.SmtpUser
                    valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(valor)
                    If results.Count > 0 Then
                        valor = TextoInteligente.ReconocerCodigo(valor, results(0))
                    End If
                    .Add("User", valor)

                    valor = Me._myRule.SmtpPort
                    valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(valor)
                    If results.Count > 0 Then
                        valor = TextoInteligente.ReconocerCodigo(valor, results(0))
                    End If
                    .Add("Port", valor)

                    valor = Me._myRule.SmtpMail
                    valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(valor)
                    If results.Count > 0 Then
                        valor = TextoInteligente.ReconocerCodigo(valor, results(0))
                    End If
                    .Add("Mail", valor)
                    .Add("EnableSsl", Me._myRule.SmtpEnableSsl)
                End With
            End If

            If _myRule.VarAttachs.Trim() <> String.Empty Then
                'cargo los adjuntos existentes en variables interreglas
                Dim attachs As Object = WFRuleParent.ReconocerVariablesAsObject(_myRule.VarAttachs)
                attachs = WFRuleParent.AttachPathsFromZvar(attachs)

                'guardo su valor para reasignarlo luego
                Attach = WFRuleParent.ObtenerNombreVariable(_myRule.VarAttachs)
                Dim NameAttach As String = ";" + Attach
                'valido que el zvar() ingresado corresponda a un documento a adjuntar
                If String.Compare(attachs.ToString, _myRule.VarAttachs) <> 0 And String.Compare(attachs.ToString, NameAttach) <> 0 Then
                    Me.attachsAux += WFRuleParent.AttachPathsFromZvar(attachs)
                End If
            End If

            'Obtención de adjuntos de tipo binario a partir de un DataTable con las claves de DocTypeId y DocId
            attachsAux &= AddBlobAttachments()

            'Nombre Boton de ejecucion de regla
            Me.BtnRuleName = String.Empty
            Me.BtnRuleName = Zamba.Core.TextoInteligente.ReconocerCodigo(_myRule.BtnName, R)
            If Me._myRule.BtnName.ToString.ToLower.Contains("zvar") Then
                Me.BtnRuleName = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.BtnRuleName).Trim()
            End If

            'Nombre Boton de ejecucion adicional de regla
            Me.BtnAdditionalRuleName = String.Empty
            Me.BtnAdditionalRuleName = Zamba.Core.TextoInteligente.ReconocerCodigo(_myRule.BtnAdditionalRuleName, R)
            If Me._myRule.BtnName.ToString.ToLower.Contains("zvar") Then
                Me.BtnAdditionalRuleName = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.BtnAdditionalRuleName).Trim()
            End If

            'Execution Rule Parameters
            ColumnNameNumber = TextoInteligente.ReconocerCodigo(_myRule.ColumnName, results(0)).Trim()
            ColumnRouteNumber = TextoInteligente.ReconocerCodigo(_myRule.ColumnRoute, results(0)).Trim()
            AdditionalColumnNameNumber = TextoInteligente.ReconocerCodigo(_myRule.AdditionalRuleColumnName, results(0)).Trim()
            AdditionalColumnRouteNumber = TextoInteligente.ReconocerCodigo(_myRule.AdditionalRuleColumnRoute, results(0)).Trim()

            If (Me._myRule.groupMailTo) Then
                If (results.Count > 0) Then
                    results(0).AutoName = "sinAttach"
                End If

                For Each r As TaskResult In results
                    Me.mails = Me.ReemplazarVariables(r, Me.mails)
                Next

                Dim mailpath As String
                For Each Params As Hashtable In Me.mails.Values
                    Params("Body") = Params("Body").ToString().Replace("§", String.Empty)
                    Dim SM As New MailActions(Me._smtpConfig, Me._myRule.SendDocument)
                    mailpath = SM.SendMail(ResultActions.EnvioDeMail, results(0), Params, _
                                           (_myRule.KeepAssociatedDocsName AndAlso _myRule.AttachAssociatedDocuments), _
                                           Me._myRule.AttachLink, Me._myRule.EmbedImages, _
                                           Me._myRule.DisableHistory, Me._myRule.ThrowExceptionIfCancel, _
                                           Params.Item("AssociatedResults"), _myRule.Automatic, _
                                           _myRule.RuleID, _myRule.BtnName, _myRule.VarAttachs, _
                                           ColumnNameNumber, ColumnRouteNumber, _myRule.ExecuteAdditionalRuleID, _
                                           BtnAdditionalRuleName, _myRule.ViewOriginal, _myRule.ViewAssociateDocuments, _
                                           AdditionalColumnNameNumber, AdditionalColumnRouteNumber, results(0))
                    If Me._myRule.SaveMailPath Then
                        Trace.WriteLineIf((ZTrace.IsVerbose) AndAlso (Not IsNothing(mailpath)), "Ruta del mail: " & mailpath)
                        Me._mailPath = Me._myRule.MailPath.Trim()

                        If VariablesInterReglas.ContainsKey(Me._mailPath) = False Then
                            VariablesInterReglas.Add(Me._mailPath, mailpath, False)
                        Else
                            VariablesInterReglas.Item(Me._myRule.MailPath) = mailpath
                        End If
                    End If
                Next

                ' Si el mail general se cancelo o no se pudo envíar se borran los results
                If (results(0).PrintName = "Cancel") Then
                    Trace.WriteLineIf(ZTrace.IsWarning, "Error en el envío del mail.")
                    results.Clear()
                Else
                    Trace.WriteLineIf(ZTrace.IsWarning, "Mail enviado con éxito.")
                End If

            Else

                Me.resultsAux.Clear()
                Me.counter = 0
                Dim mailpath As String

                For Each r As TaskResult In results
                    Me.resultAux = results(Me.counter)

                    'Convierto texto inteligente y ZVar
                    Me.txtValue = String.Empty
                    Me.txtValue = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.IndexValue, r).Trim()
                    If Me._myRule.IndexValue.ToString.ToLower.Contains("zvar") Then
                        Me.txtValue = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.txtValue)
                    End If
                    Me.txtFilterDocID = String.Empty
                    Me.txtFilterDocID = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.FilterDocID, r)
                    If Me._myRule.FilterDocID.ToString.ToLower.Contains("zvar") Then
                        Me.txtFilterDocID = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.txtFilterDocID)
                    End If

                    Me.Params.Clear()
                    Me.Params = Me.ReemplazarVariables(r)

                    isSendDocument(Me.resultAux)

                    Me.Params.Add("Automatic", Me._myRule.Automatic)
                    Dim SM As New MailActions(Me._smtpConfig, Me._myRule.SendDocument)
                    mailpath = SM.SendMail(ResultActions.EnvioDeMail, Me.resultAux, Me.Params, _
                                           (_myRule.KeepAssociatedDocsName AndAlso _myRule.AttachAssociatedDocuments), _
                                           Me._myRule.AttachLink, Me._myRule.EmbedImages, _
                                           Me._myRule.DisableHistory, Me._myRule.ThrowExceptionIfCancel, _
                                           Params.Item("AssociatedResults"), _myRule.Automatic, _
                                           _myRule.RuleID, Me.BtnRuleName, _myRule.VarAttachs, _
                                           ColumnNameNumber, ColumnRouteNumber, _myRule.ExecuteAdditionalRuleID, _
                                           BtnAdditionalRuleName, _myRule.ViewOriginal, _myRule.ViewAssociateDocuments, _
                                           AdditionalColumnNameNumber, AdditionalColumnRouteNumber, results(0))

                    If Me._myRule.SaveMailPath Then
                        TextoInteligente.AsignItemFromSmartText(Me._myRule.MailPath, r, mailpath)
                        Trace.WriteLineIf((ZTrace.IsVerbose) AndAlso (Not IsNothing(mailpath)), "Path del mail: " & mailpath)

                        Me._mailPath = Me._myRule.MailPath.Trim()

                        If VariablesInterReglas.ContainsKey(Me._mailPath) = False Then
                            VariablesInterReglas.Add(Me._mailPath, mailpath, False)
                        Else
                            VariablesInterReglas.Item(Me._mailPath) = mailpath
                        End If
                    End If

                    'Result.HandleModule(ResultActions.EnvioDeMail, Me.resultAux, Me.Params)
                    ' Se agregan a la colección resultsAux las tareas cuyo form. de mail fue cancelado o cuyo correo no pudo ser envíado
                    If (Me.resultAux.PrintName = "Cancel") Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Error en el envío del mail.")
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
                    End If
                Next
            End If

            If Not Attach Is Nothing And String.Compare(Attach, String.Empty) <> 0 Then
                If VariablesInterReglas.ContainsKey(Attach) Then
                    VariablesInterReglas.Item(Attach) = Attach
                End If
            End If

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
            Me.txtValue = Nothing
            Me.txtFilterDocID = Nothing
            If Not Me._smtpConfig Is Nothing Then
                Me._smtpConfig.Clear()
                Me._smtpConfig = Nothing
            End If
        End Try

        Return (results)
    End Function

#Region "Obtención de adjuntos de tipo binario a partir de un DataTable con las claves de DocTypeId y DocId"

    ''' <summary>
    ''' Obtiene los binarios de los adjuntos seleccionados, los almacena en disco temporalmente y devuelve las rutas concatenadas por un ';'
    ''' </summary>
    ''' <returns>Rutas de los adjuntos separadas por ;</returns>
    Private Function AddBlobAttachments() As String
        Dim attachsAux As String = String.Empty

        'Verifica si la opción de búsqueda de adjuntos a partir de un DT se encuentra activada
        If Not String.IsNullOrEmpty(_myRule.AttachTableVar) Then
            'Obtiene el DT que contiene las claves para obtener los binarios de los adjuntos
            Dim dt As DataTable = GetAttachsTable(_myRule.AttachTableVar)

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    ''                    Trace.WriteLineIf(ZTrace.IsInfo, "Buscando índice de columna DocTypeId: ")
                    Dim colDocTypeId As Int32 = GetColumnIndex(dt, _myRule.AttachTableColDocTypeId)
                    ''                  Trace.WriteLineIf(ZTrace.IsInfo, colDocTypeId)

                    ''Trace.WriteLineIf(ZTrace.IsInfo, "Buscando índice de columna DocId: ")
                    Dim colDocId As Int32 = GetColumnIndex(dt, _myRule.AttachTableColDocId)
                    ''  Tr() ''ace.WriteIf(ZTrace.IsVerbose, colDocId)

                    ''Trace.WriteLineIf(ZTrace.IsInfo, "Buscando índice de columna DocName: ")
                    Dim colDocName As Int32 = GetColumnIndex(dt, _myRule.AttachTableColDocName)
                    ''                  Trace.WriteLineIf(ZTrace.IsInfo, colDocName)

                    Dim docTemp As BlobDocument
                    Dim officePath As String = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp\").FullName
                    Dim tempPath As String

                    For i As Int32 = 0 To dt.Rows.Count - 1
                        docTemp = GetBlobDocument(dt.Rows(i)(colDocTypeId), dt.Rows(i)(colDocId))
                        tempPath = officePath & FileBusiness.RemoveInvalidFileChars(dt.Rows(i)(colDocName)) & docTemp.Extension
                        FileEncode.Decode(tempPath, docTemp.BlobFile)
                        attachsAux &= ";" & tempPath
                    Next

                    If docTemp IsNot Nothing Then
                        docTemp.Dispose()
                        docTemp = Nothing
                    End If
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "No se encontraron adjuntos en la tabla '" & _myRule.AttachTableVar & "'.")
                End If

                dt.Dispose()
                dt = Nothing
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "La tabla de adjuntos '" & _myRule.AttachTableVar & "' es nula.")
            End If
        End If

        Return attachsAux
    End Function

    ''' <summary>
    ''' Obtiene una tabla a partir de una variable
    ''' </summary>
    ''' <param name="tableNameVar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAttachsTable(tableNameVar As String) As DataTable
        Dim attachs As Object = WFRuleParent.ReconocerVariablesAsObject(tableNameVar)

        If attachs IsNot Nothing Then
            Select Case attachs.GetType().ToString
                Case "System.Data.DataTable"
                    Return attachs

                Case "System.Data.DataSet"
                    Return DirectCast(attachs, DataSet).Tables(0)

                Case "System.Data.DataRow"
                    Dim dt As New DataTable
                    dt.Rows.Add(attachs)
                    Return dt

                Case Else
                    Return Nothing
            End Select
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Verifica y obtiene el índice de una columna
    ''' </summary>
    ''' <param name="dt">Tabla a buscar</param>
    ''' <param name="colConfig">Índice o nombre de columna</param>
    Private Function GetColumnIndex(dt As DataTable, colConfig As String) As Integer
        Dim colIndex As Int32
        If Not String.IsNullOrEmpty(colConfig) Then
            If Int32.TryParse(colConfig, colIndex) Then
                If colIndex >= dt.Columns.Count OrElse colIndex < 0 Then
                    Throw New IndexOutOfRangeException("La posición " & colIndex.ToString & " se encuentra fuera del intervalo de la tabla: de 0 a " & (dt.Rows.Count - 1).ToString & ")")
                End If
            Else
                colIndex = dt.Columns.IndexOf(colConfig)
                If colIndex = -1 Then
                    Throw New ArgumentException("La columna '" & colConfig & "' no existe en la tabla de origen.")
                End If
            End If
        Else
            Throw New ArgumentException("Columna no configurada.")
        End If
        Return colIndex
    End Function

    ''' <summary>
    ''' Obtiene el documento blob
    ''' </summary>
    Private Function GetBlobDocument(docTypeId As Int64, docId As Int64) As BlobDocument
        Dim rbe As New ResultBusinessExt
        Dim blobDoc As BlobDocument = rbe.GetBlobDocument(docTypeId, docId)
        rbe = Nothing

        Return blobDoc
    End Function
#End Region

    Public Function PlayTest() As Boolean

    End Function

    Function DiscoverParams() As List(Of String)

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
    Public Function ReemplazarVariables(ByVal res As TaskResult) As Hashtable
        Me.Body = String.Empty
        Me.Asunto = String.Empty
        Para = String.Empty
        CC = String.Empty
        CCO = String.Empty
        Me.link = New ArrayList
        '[Sebastian 05-06-2009]se realizo new porque lanzaba object reference
        Dim AssociatedResults As New List(Of IResult) ' = New List(Of IResult)()
        Dim PathImages() As String = {}
        Try
            Try
                Dim R As String = String.Empty
                Me.Asunto = Me._myRule.Asunto
                Me.Asunto = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.Asunto, res)

                Dim ValorVariable As Object
                Dim Variable As String = WFRuleParent.ObtenerNombreVariable(Me.Asunto)
                ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Me.Asunto)

                If IsNothing(ValorVariable) = False Then
                    If (TypeOf (ValorVariable) Is DataSet) Then
                        For Each DR As DataRow In ValorVariable.tables(0).rows
                            R &= DR.Item(0) & ","
                        Next
                        Me.Asunto = Me.Asunto.Replace("zvar(" & Variable & ")", R)
                    End If
                    Me.Asunto = Me.Asunto.Replace("zvar(" & Variable & ")", ValorVariable)
                    Me.Asunto = WFRuleParent.ReconocerVariables(Me.Asunto)
                End If
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsError, "Ha ocurrido un error y no se ha podido obtener el valor del Asunto")
                ZClass.raiseerror(ex)
            End Try

            Me.Para = obtenerPara(res)
            obtenerCC(res)
            obtenerCCO(res)

            Try
                Me.Body = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Body, res)
                Me.Body = WFRuleParent.ReconocerVariables(Me.Body)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsError, "Ha ocurrido un error y no se ha podido obtener el valor del cuerpo del mail")
                ZClass.raiseerror(ex)
            End Try

            Try
                If _myRule.AttachLink Or Boolean.Parse(UserPreferences.getValue("LinkMailSeleccionado", Sections.FormPreferences, "False")) = True Then
                    Me.link.Add(Me._myRule.AttachLink)
                    Me.link.Add("Zamba:\\TaskID=" & res.TaskId.ToString)
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            Try
                If String.IsNullOrEmpty(Me.attachsAux) = False Then
                    Dim sep As Char() = {";"}
                    PathImages = Me.attachsAux.Split(sep, StringSplitOptions.RemoveEmptyEntries)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Dim Params As New Hashtable
        Me.Para = Me.ReconocerGruposYUsuarios(Me.Para)
        Params.Add("To", Me.Para)
        Me.CC = Me.ReconocerGruposYUsuarios(Me.CC)
        Params.Add("CC", Me.CC)
        Me.CCO = Me.ReconocerGruposYUsuarios(Me.CCO)
        Params.Add("CCO", Me.CCO)
        Params.Add("Subject", Me.Asunto)
        Params.Add("Body", Me.Body)

        '[Ezequiel]: Se filtran los documentos asociados a adjuntar.
        Dim TempDocAsociated As New ArrayList

        If Me._myRule.AttachAssociatedDocuments Then
            If Not res.UserRules.ContainsKey("_DoMailAsocHash") Then
                '[Ezequiel]: Valido si se adjuntan todos o los seleccionados por el usuario
                If Me._myRule.DTType = IMailConfigDocAsoc.DTTypes.AllDT Then
                    TempDocAsociated = DocTypes.DocAsociated.DocAsociatedBusiness.getAsociatedResultsFromResult(res, 0)
                    If IsNothing(TempDocAsociated) = False AndAlso TempDocAsociated.Count > 0 Then
                        '[Ezequiel]: Valido si se selecciona el primero, se filtra, se adjunta de manera manual o se adjunta todo

                        Select Case Me._myRule.Selection
                            Case IMailConfigDocAsoc.Selections.First
                                AssociatedResults.Add(TempDocAsociated(0))
                            Case IMailConfigDocAsoc.Selections.Filter
                                For Each resu As IResult In TempDocAsociated
                                    Dim inlist As List(Of IIndex) = IndexsBusiness.GetIndexs(resu.ID, resu.DocTypeId)
                                    For Each i As Index In inlist
                                        ' para mantener la funcionalidad vieja, ingrasamos False como parametro en tmpCaseInsensitive
                                        If Me._myRule.Index = i.ID AndAlso ToolsBusiness.ValidateComp(i.Data, txtValue, Me._myRule.Oper, False) Then
                                            AssociatedResults.Add(resu)
                                        End If
                                    Next
                                Next
                            Case IMailConfigDocAsoc.Selections.All
                                For Each resu As IResult In TempDocAsociated
                                    AssociatedResults.Add(resu)
                                Next
                            Case IMailConfigDocAsoc.Selections.Manual
                                If Me._myRule.Automatic Then
                                    For Each resu As IResult In TempDocAsociated
                                        AssociatedResults.Add(resu)
                                    Next
                                Else
                                    Dim frmSelectDocs As New Zamba.Viewers.UCSelectAsoc(ArrayList.Adapter(Me._myRule.DocTypes.Split("|")))
                                    frmSelectDocs.ShowDialog()
                                    For Each resu As IResult In TempDocAsociated
                                        If frmSelectDocs.DocsSel.Contains(resu.DocTypeId.ToString) Then
                                            AssociatedResults.Add(resu)
                                        End If
                                    Next
                                End If
                            Case IMailConfigDocAsoc.Selections.FilterDocId
                                Dim txtfilterid As String = TextoInteligente.ReconocerCodigo(txtFilterDocID, res)
                                txtfilterid = WFRuleParent.ReconocerVariablesValuesSoloTexto(txtfilterid)
                                For Each resu As IResult In TempDocAsociated
                                    If resu.ID = CLng(txtfilterid) Then AssociatedResults.Add(resu)
                                Next
                        End Select

                    End If
                Else
                    For Each DTId As String In ArrayList.Adapter(Me._myRule.DocTypes.Split("|"))
                        Dim asoc As ArrayList = DocTypes.DocAsociated.DocAsociatedBusiness.getAsociatedResultsFromResult(Int64.Parse(DTId), res, 0)
                        If Not IsNothing(asoc) Then
                            TempDocAsociated.AddRange(asoc)
                        End If
                    Next
                    If IsNothing(TempDocAsociated) = False AndAlso TempDocAsociated.Count > 0 Then
                        Select Case Me._myRule.Selection
                            Case IMailConfigDocAsoc.Selections.First
                                AssociatedResults.Add(TempDocAsociated(0))
                            Case IMailConfigDocAsoc.Selections.Filter
                                For Each resu As IResult In TempDocAsociated
                                    Dim inlist As List(Of IIndex) = IndexsBusiness.GetIndexs(resu.ID, resu.DocTypeId)
                                    For Each i As Index In inlist
                                        ' para mantener la funcionalidad vieja, ingrasamos False como parametro en tmpCaseInsensitive
                                        If Me._myRule.Index = i.ID AndAlso ToolsBusiness.ValidateComp(i.Data, txtValue, Me._myRule.Oper, False) Then
                                            AssociatedResults.Add(resu)
                                        End If
                                    Next
                                Next
                            Case IMailConfigDocAsoc.Selections.All
                                For Each resu As IResult In TempDocAsociated
                                    AssociatedResults.Add(resu)
                                Next
                            Case IMailConfigDocAsoc.Selections.Manual
                                If Me._myRule.Automatic Then
                                    For Each resu As IResult In TempDocAsociated
                                        AssociatedResults.Add(resu)
                                    Next
                                Else
                                    Dim frmSelectDocs As New Zamba.Viewers.UCSelectAsoc(ArrayList.Adapter(Me._myRule.DocTypes.Split("|")))
                                    frmSelectDocs.ShowDialog()
                                    For Each resu As IResult In TempDocAsociated
                                        If frmSelectDocs.DocsSel.Contains(resu.DocTypeId.ToString) Then
                                            AssociatedResults.Add(resu)
                                        End If
                                    Next
                                End If
                            Case IMailConfigDocAsoc.Selections.FilterDocId
                                Dim txtfilterid As String = TextoInteligente.ReconocerCodigo(txtFilterDocID, res)
                                txtfilterid = WFRuleParent.ReconocerVariablesValuesSoloTexto(txtfilterid)
                                For Each resu As IResult In TempDocAsociated
                                    If resu.ID = CLng(txtfilterid) Then AssociatedResults.Add(resu)
                                Next
                        End Select

                    End If
                End If
                res.UserRules.Add("_DoMailAsocHash", AssociatedResults)
            Else
                AssociatedResults = res.UserRules.Item("_DoMailAsocHash")
            End If
        End If

        Params.Add("AssociatedResults", AssociatedResults)
        If Not IsNothing(AssociatedResults) Then Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad de documentos Asociados: " & AssociatedResults.Count)
        Params.Add("AttachPaths", PathImages)
        Params.Add("Link", Me.link)
        Return Params
    End Function

    Public Function ReemplazarVariables(ByVal res As TaskResult, ByVal mails As SortedList) As SortedList
        Dim para As String = obtenerPara(res)

        If mails.Contains(para) = False Then
            Dim params As Hashtable = ReemplazarVariables(res)
            params.Add("Automatic", Me._myRule.Automatic)
            mails.Add(para, params)
        Else
            Dim body As String = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Body, res, mails(para)("Body"))
            body = WFRuleParent.ReconocerVariables(body)
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
                    'JNC
                    'Dim email As ICorreo
                    'email = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(ValorVariable)
                    'Me.R = email.Mail
                    'Me.para2 = Me.para2.Replace("zvar(" & Variable & ")", Me.R)
                    Me.para2 = Me.para2.Replace("zvar(" & Variable & ")", ValorVariable)
                ElseIf TypeOf (ValorVariable) Is String Then
                    If Not ValorVariable.ToString.Contains(".") AndAlso Not ValorVariable.ToString.Contains("@") Then
                        'id de usuario
                        'Dim uId As Int32 = Zamba.Core.UserBusiness.GetUserID(ValorVariable)
                        'JNC
                        'Dim email As ICorreo
                        'email = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(uId)
                        'Me.R = email.Mail
                        'Me.para2 = Me.para2.Replace("zvar(" & Variable & ")", Me.R)
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
            Trace.WriteLineIf(ZTrace.IsError, "Ha ocurrido un error y no se ha podido recuperar el valor del 'Para'")
            ZClass.raiseerror(ex)
            Return String.Empty
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
        Try
            Dim R As String
            Me.CC = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.CC, res)

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
                    'Dim email As ICorreo
                    'email = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(ValorVariable)
                    'R = email.Mail
                    'Me.CC = Me.CC.Replace("zvar(" & Variable & ")", R)
                    Me.CC = Me.CC.Replace("zvar(" & Variable & ")", ValorVariable)
                ElseIf TypeOf (ValorVariable) Is String Then
                    If Not ValorVariable.ToString.Contains(".") AndAlso Not ValorVariable.ToString.Contains("@") Then
                        'Id De Usuario
                        'Dim uId As Int32 = Zamba.Core.UserBusiness.GetUserID(ValorVariable)
                        'Dim email As ICorreo
                        'email = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(uId)
                        'R = email.Mail
                        'Me.CC = Me.CC.Replace("zvar(" & Variable & ")", R)
                        Me.CC = Me.CC.Replace("zvar(" & Variable & ")", ValorVariable)
                    Else
                        'Es una direccion de mail
                        R = ValorVariable.ToString
                        Me.CC = Me.CC.Replace("zvar(" & Variable & ")", R)
                    End If
                End If
            End If
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsError, "Ha ocurrido un error y no se ha podido obtener el valor del 'CC'")
            ZClass.raiseerror(ex)
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
                    'Dim email As ICorreo
                    'email = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(ValorVariable)
                    'R = email.Mail
                    'Me.CCO = Me.CCO.Replace("zvar(" & Variable & ")", R)
                    Me.CCO = Me.CCO.Replace("zvar(" & Variable & ")", ValorVariable)
                ElseIf TypeOf (ValorVariable) Is String Then
                    If Not ValorVariable.ToString.Contains(".") AndAlso Not ValorVariable.ToString.Contains("@") Then
                        'Id De Usuario
                        'Dim uId As Int32 = Zamba.Core.UserBusiness.GetUserID(ValorVariable)
                        'Dim email As ICorreo
                        'email = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(uId)
                        'R = email.Mail
                        'Me.CCO = Me.CCO.Replace("zvar(" & Variable & ")", R)
                        Me.CCO = Me.CC.Replace("zvar(" & Variable & ")", ValorVariable)
                    Else
                        'Es una direccion de mail
                        R = ValorVariable.ToString
                        Me.CCO = Me.CCO.Replace("zvar(" & Variable & ")", R)
                    End If
                End If
            End If
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsError, "Ha ocurrido un error y no se ha podido obtener el valor del 'CCO'")
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
        Dim separadores(2) As Char
        Dim strReconocido As String = String.Empty
        separadores(0) = ";"
        separadores(1) = ","
        separadores(2) = " "

        Dim lsMails As New List(Of String)

        Dim valores() As String = Text.Split(separadores)

        For Each valor As String In valores
            Dim valorFinal As String = String.Empty

            valor = valor.TrimStart(" ").TrimEnd(" ")

            If Not String.IsNullOrEmpty(valor) Then
                'Si contiene arroba directamente lo agregamos como mail
                If valor.Contains("@") Then
                    If Not lsMails.Contains(valor) Then
                        lsMails.Add(valor)
                    End If
                Else

                    If IsNumeric(valor) Then
                        Dim email As ICorreo

                        Dim usuarios As ArrayList = UserBusiness.GetGroupUsers(Convert.ToInt64(valor))

                        'si el grupo tiene usuario obtenemos los mails de todos
                        If usuarios.Count > 0 Then
                            For Each User As IUser In usuarios
                                email = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(User.ID)

                                If Not lsMails.Contains(email.Mail) Then
                                    lsMails.Add(email.Mail)
                                End If
                            Next
                        Else
                            'Si no es grupo es usuario (usuarios.Count = 0)
                            email = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(Convert.ToInt64(valor))

                            If Not lsMails.Contains(email.Mail) Then
                                lsMails.Add(email.Mail)
                            End If
                        End If

                    ElseIf TypeOf (valor) Is String Then
                        'Mismo procesamiento que para numeric solo que se obtiene el id de usuario o grupo
                        ' para el string pasado
                        Dim us As IUser = UserBusiness.GetUserByname(valor)
                        Dim grId As Int64 = UserGroupBusiness.GetGroupIdByName(valor)

                        If Not IsNothing(us) Or grId <> 0 Then
                            Dim email As ICorreo

                            Dim usuarios As ArrayList = UserBusiness.GetGroupUsers(grId)

                            If usuarios.Count > 0 Then
                                For Each User As IUser In usuarios
                                    email = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(User.ID)

                                    If Not lsMails.Contains(email.Mail) Then
                                        lsMails.Add(email.Mail)
                                    End If
                                Next
                            Else
                                email = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(us.ID)

                                If Not lsMails.Contains(email.Mail) Then
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

        'Recorre la lista de mails y las agrega a un string
        For Each email As String In lsMails
            If Not String.IsNullOrEmpty(email) Then
                If String.IsNullOrEmpty(strReconocido) Then
                    strReconocido = email
                Else
                    Select Case UserBusiness.Rights.CurrentUser.eMail.Type
                        Case Zamba.Core.MailTypes.LotusNotesMail
                            strReconocido = strReconocido + "," + email
                        Case Zamba.Core.MailTypes.NetMail
                            strReconocido = strReconocido + ";" + email
                        Case Zamba.Core.MailTypes.OutLookMail
                            strReconocido = strReconocido + ";" + email
                        Case Else
                            strReconocido = strReconocido + ";" + email
                    End Select
                End If
            End If
        Next
        Return strReconocido

    End Function

End Class

Public Class MailActions

    Private _smtpConfig As Hashtable
    Private _sendDocument As Boolean

    Sub New(ByVal smtpconfig As Hashtable, ByVal sendDocument As Boolean)
        Me._smtpConfig = smtpconfig
        Me._sendDocument = sendDocument
    End Sub

    Public Function SendMail(ByVal resultActionType As ResultActions, ByRef currentResult As ZambaCore, _
                             ByVal Params As Hashtable, ByVal keepOriginalDocName As Boolean, _
                             ByVal addlink As Boolean, ByVal EmbedImages As Boolean, _
                             ByVal disablehistory As Boolean, ByVal ThrowExceptionIfCancel As Boolean, _
                             ByVal AssociatedResults As Generic.List(Of IResult), _
                             ByVal AutomaticSend As Boolean, ByVal ExecuteRuleID As Int64, _
                             ByVal BtnRuleName As String, ByVal varAttachs As String, _
                             ByVal ColumnNameNumber As String, ByVal ColumnRouteNumber As String, _
                             ByVal ExecuteAdditionalRuleID As Int64, ByVal BtnAdditionalRuleName As String, _
                             ByVal ViewOriginalDocument As Boolean, ByVal ViewAssociateDocument As Boolean, _
                             ByVal AdditionalColumnNameNumber As String, ByVal AdditionalColumnRouteNumber As String, _
                             ByVal taskResult As ITaskResult) As String

        Dim mailpath As String
        Dim _currentResult As IResult = DirectCast(currentResult, Result)

        If AutomaticSend = False Then
            If _currentResult.ISVIRTUAL OrElse Results_Business.HasForms(_currentResult) Then
                Me.CompleteHtmlFile(_currentResult)
            End If

            '(pablo) recorro los asociados y valido si alguno es virtual
            For Each Result As IResult In AssociatedResults
                If Result.ISVIRTUAL OrElse Results_Business.HasForms(Result) Then
                    Me.CompleteHtmlFile(Result)
                End If
            Next
        End If

        If ((IsNothing(Params)) OrElse (Params.Count = 0)) Then
            Dim Results As New Generic.List(Of IResult)
            Results.Add(_currentResult)
            SendMail(Results, addlink, EmbedImages)
        Else
            Dim Results(0) As Result
            Results(0) = _currentResult
            If (IsNothing(Params.Item("AssociatedResults"))) Then
                If Not Params.Item("Link") Is Nothing Then
                    mailpath = EnviarMail(Results, Params.Item("To").ToString(), _
                                          Params.Item("CC").ToString(), Params.Item("CCO").ToString(), _
                                          Params.Item("Subject").ToString(), Params.Item("Body").ToString(), _
                                          DirectCast(Params.Item("AttachPaths"), String()), _
                                          ExecuteRuleID, BtnRuleName, varAttachs, ColumnNameNumber, _
                                          ColumnRouteNumber, ExecuteAdditionalRuleID, BtnAdditionalRuleName, ViewOriginalDocument, _
                                          ViewAssociateDocument, CType(Params.Item("Link"), ArrayList), Nothing, _
                                          CType(Params("Automatic"), Boolean), False, addlink, EmbedImages, _
                                          disablehistory, AdditionalColumnNameNumber, AdditionalColumnRouteNumber, taskResult)
                Else
                    mailpath = EnviarMail(Results, Params.Item("To").ToString(), Params.Item("CC").ToString(), _
                                          Params.Item("CCO").ToString(), Params.Item("Subject").ToString(), _
                                          Params.Item("Body").ToString(), DirectCast(Params.Item("AttachPaths"), String()), _
                                          ExecuteRuleID, BtnRuleName, varAttachs, ColumnNameNumber, ColumnRouteNumber, _
                                          ExecuteAdditionalRuleID, BtnAdditionalRuleName, ViewOriginalDocument, ViewAssociateDocument, Nothing, Nothing, _
                                          CType(Params("Automatic"), Boolean), False, addlink, EmbedImages, _
                                          disablehistory, AdditionalColumnNameNumber, AdditionalColumnRouteNumber, taskResult)
                End If
            Else
                If Not Params.Item("Link") Is Nothing Then
                    mailpath = EnviarMail(Results, Params.Item("To").ToString(), Params.Item("CC").ToString(), _
                                          Params.Item("CCO").ToString(), Params.Item("Subject").ToString(), _
                                          Params.Item("Body").ToString(), DirectCast(Params.Item("AttachPaths"), String()), _
                                          ExecuteRuleID, BtnRuleName, varAttachs, ColumnNameNumber, _
                                          ColumnRouteNumber, ExecuteAdditionalRuleID, BtnAdditionalRuleName, ViewOriginalDocument, _
                                          ViewAssociateDocument, CType(Params.Item("Link"), ArrayList), Params.Item("AssociatedResults"), _
                                          CType(Params("Automatic"), Boolean), keepOriginalDocName, addlink, _
                                          EmbedImages, disablehistory, AdditionalColumnNameNumber, AdditionalColumnRouteNumber, taskResult)
                Else
                    mailpath = EnviarMail(Results, Params.Item("To").ToString(), Params.Item("CC").ToString(), _
                                          Params.Item("CCO").ToString(), Params.Item("Subject").ToString(), _
                                          Params.Item("Body").ToString(), DirectCast(Params.Item("AttachPaths"), String()), _
                                          ExecuteRuleID, BtnRuleName, varAttachs, ColumnNameNumber, _
                                          ColumnRouteNumber, ExecuteAdditionalRuleID, BtnAdditionalRuleName, ViewOriginalDocument, _
                                          ViewAssociateDocument, DirectCast(Params.Item("AssociatedResults"), ArrayList), Nothing, _
                                          CType(Params("Automatic"), Boolean), keepOriginalDocName, addlink, _
                                          EmbedImages, disablehistory, AdditionalColumnNameNumber, AdditionalColumnRouteNumber, taskResult)
                End If
            End If

            ' Si se pudo enviar el correo
            If String.Compare(Results(0).AutoName, "OK") = 0 Then
                currentResult.PrintName = "OK"
            ElseIf String.Compare(Results(0).AutoName, "Cancel") = 0 Then

                currentResult.PrintName = "Cancel"

                If ThrowExceptionIfCancel Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "El usuario cancelo la ejecucion de la regla")
                    Throw New Exception("El usuario cancelo la ejecucion de la regla")
                End If
            End If

        End If

        Return mailpath

    End Function


#Region "Mensajes"
    Public Sub SendMail(ByVal Results As Generic.List(Of IResult), ByVal addlink As Boolean, ByVal EmbedImages As Boolean) 'FORM QUE PERMITE MANDAR UN MAIL
        If RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.EnviarPorMail) Then
            Try
                Dim resultsaux As New Generic.List(Of IResult)
                '[Ezequiel] Valido si es virtual para cargar el archivo mht
                For Each r As Result In Results
                    If r.ISVIRTUAL Then
                        Me.CompleteHtmlFile(r)
                    End If
                    resultsaux.Add(r)
                Next
                Results = resultsaux
                If Results.Count > 0 Then
                    If Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.OutLookMail Then
                        Using frmMessageOutlook As OutlookMessageForm = clsMessages.getOutlookMailForm(Results, Nothing, False, False, addlink)
                            frmMessageOutlook.EspecificarDatosDoc(Results(0).ID, Results(0).DocTypeId)
                            frmMessageOutlook.ShowDialog()
                        End Using
                    Else
                        Dim frmMessage As Form
                        frmMessage = Zamba.AdminControls.clsMessages.getMailForm(Results, False, -1, String.Empty, String.Empty, -1, -1, -1, String.Empty)

                        If Not frmMessage Is Nothing Then
                            ' [AlejandroR] 03/12/2009 - Created - Guardar ids del doc para el registro del envio del email en el historial
                            DirectCast(frmMessage, IZMessageForm).EspecificarDatosDoc(Results(0).ID, Results(0).DocTypeId, DirectCast(Results(0), TaskResult).StepId, MailEvent.DoMail)

                            frmMessage.WindowState = FormWindowState.Normal
                            frmMessage.ShowInTaskbar = True
                            frmMessage.ShowDialog()
                            frmMessage.Focus()
                        Else
                            Trace.WriteLineIf(ZTrace.IsWarning, "El usuario no tiene configurada la cuenta de correo")
                            MessageBox.Show("El usuario no tiene configurado el correo", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Else
            Trace.WriteLineIf(ZTrace.IsWarning, "No tiene permisos suficientes para enviar el documento por mail")
            MessageBox.Show("No tiene permisos suficientes para enviar el documento por mail", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        '---------------------------------------------------------------------
    End Sub

    '---------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que completa la propiedad htmlfile y crea el archivo mht del formulario
    ''' </summary>
    ''' <param name="r"></param>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 08/04/09 - Created
    '''             [Sebastian] Modified 09-06-2009 se agrego cast para salvar warnings
    '''</history> 
    Private Function CompleteHtmlFile(ByRef r As Result) As Boolean

        If Not Me.GetHtml(r, Me._sendDocument) Then
            Return False
        End If

        'Se borran todos los caracteres que windows no permite
        'para el nombre de un archivo.
        For Each invalidChar As Char In IO.Path.GetInvalidFileNameChars
            r.Name = r.Name.Replace(invalidChar, String.Empty)
        Next
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
            'Try
            '    File.Delete(r.HtmlFile)
            'Catch ex As Exception

            'End Try


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
    ''' <history> [Ezequiel] 08/04/09 - Created
    Public Function GetHtml(ByRef r As Result, ByVal sendDocument As Boolean) As Boolean
        Try
            If Boolean.Parse(UserPreferences.getValue("PreviewFormInDoMail", Sections.UserPreferences, "True")) And sendDocument Then
                Dim prvfrm As New PreviewForm(r)

                prvfrm.ShowDialog()
                If prvfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                    r.Html = prvfrm.frmbrowser.GetHtml
                    Return True
                Else
                    r.HtmlFile = Nothing
                    r.Html = Nothing
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
    ''' <param name="Tostr"></param>
    ''' <param name="CC"></param>
    ''' <param name="CCO"></param>
    ''' <param name="Subject"></param>
    ''' <param name="Body"></param>
    ''' <param name="attachDocsPaths"></param>
    ''' <param name="AssociatedDocuments"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    09/09/2008  Modified     
    '''                 15/09/2008  Modified    Si el usuario no tiene configurado el correo se muestra un mensaje de error
    ''' </history>
    Public Function EnviarMail(ByRef results() As Result, ByVal Tostr As String, _
                               ByVal CC As String, ByVal CCO As String, ByVal Subject As String, _
                               ByVal Body As String, ByVal attachDocsPaths() As String, _
                               ByVal ExecuteRuleID As Int64, ByVal RuleName As String, _
                               ByVal VarAttachs As String, ByVal ColumnNameNumber As String, _
                               ByVal ColumnRouteNumber As String, ByVal ExecuteAdditionalRuleID As String, _
                               ByVal BtnAdditionalRuleName As String, ByVal ViewOriginalDocument As Boolean, ByVal ViewAssociateDocument As Boolean, _
                               Optional ByVal Links As ArrayList = Nothing, _
                               Optional ByVal AssociatedDocuments As List(Of IResult) = Nothing, _
                               Optional ByVal Automatic As Boolean = False, _
                               Optional ByVal keepOriginalDocName As Boolean = False, _
                               Optional ByVal addlink As Boolean = False, _
                               Optional ByVal EmbedImages As Boolean = False, _
                               Optional ByVal disablehistory As Boolean = False, _
                               Optional ByVal AdditionalColumnNameNumber As String = "", _
                               Optional ByVal AdditionalColumnRouteNumber As String = "", _
                               Optional ByVal taskResult As ITaskResult = Nothing) As String 'FORM QUE PERMITE MANDAR UN MAIL

        Dim mailpath As String

        'Se encarga de almacenar temporalmente los nombres de los documentos asociados
        'en el caso de que la opción se encuentre activada.
        Dim tempAttachResultsNames As List(Of String)
        Dim frmMessage As New Form
        Dim frmCtrl As New Control()
        Dim frmMessageOutlook As OutlookMessageForm = Nothing

        Try
            'Valido que la colección de results no venga en nothing (caso de mail sin attach)
            For Each r As Result In results

                'If IsNothing(r) Then
                If Not Me._smtpConfig Is Nothing Then
                    If String.Compare(r.AutoName, "sinAttach") <> 0 Then
                        Dim SelectedResults As New Generic.List(Of IResult)
                        SelectedResults.AddRange(results)

                        frmMessage = New frmNetMailMessageSend(New ArrayList(SelectedResults.ToArray), _
                                                               Automatic, ExecuteRuleID, RuleName, _
                                                               VarAttachs, ColumnNameNumber, _
                                                               ColumnRouteNumber, ExecuteAdditionalRuleID, _
                                                               BtnAdditionalRuleName, String.Empty, _
                                                               String.Empty, String.Empty, Me._smtpConfig, _
                                                               AdditionalColumnNameNumber, AdditionalColumnRouteNumber)

                    ElseIf Links Is Nothing Then
                        frmMessage = New frmNetMailMessageSend()
                    Else
                        Dim SelectedResults As New Generic.List(Of IResult)
                        SelectedResults.AddRange(results)
                        frmMessage = New frmNetMailMessageSend(Links, True, Automatic, ExecuteRuleID, _
                                                               RuleName, VarAttachs, ColumnNameNumber, _
                                                               ColumnRouteNumber, New ArrayList(SelectedResults.ToArray), _
                                                               ExecuteAdditionalRuleID, BtnAdditionalRuleName, _
                                                               Me._smtpConfig, AdditionalColumnNameNumber, AdditionalColumnRouteNumber)
                    End If
                    DirectCast(frmMessage, frmNetMailMessageSend).DocId = r.ID
                    DirectCast(frmMessage, frmNetMailMessageSend).DocTypeId = r.DocTypeId
                    DirectCast(frmMessage, frmNetMailMessageSend).DisableHistory = disablehistory

                Else
                    If Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.OutLookMail And Not Automatic Then
                        If String.Compare(r.AutoName, "sinAttach") <> 0 Then
                            Dim SelectedResults As New Generic.List(Of IResult)
                            SelectedResults.AddRange(results)
                            frmMessageOutlook = clsMessages.getOutlookMailForm(SelectedResults, Nothing, Automatic, False, addlink)
                        ElseIf Links Is Nothing Then
                            frmMessageOutlook = clsMessages.getOutlookMailForm(Nothing, Nothing, Automatic, False, addlink)
                        Else
                            frmMessageOutlook = clsMessages.getOutlookMailForm(Nothing, Links, Automatic, False, addlink)
                        End If
                        frmMessageOutlook.DocId = r.ID
                        frmMessageOutlook.DocTypeId = r.DocTypeId
                        frmMessageOutlook.DisableHistory = disablehistory
                    Else
                        If String.Compare(r.AutoName, "sinAttach") <> 0 Then
                            Dim SelectedResults As New Generic.List(Of IResult)
                            SelectedResults.AddRange(results)

                            If ViewOriginalDocument Or ViewAssociateDocument Then
                                frmCtrl = New ctrlLotusMessageSend(SelectedResults, Automatic, _
                                                                    ExecuteRuleID, RuleName, VarAttachs, _
                                                                    ColumnNameNumber, ColumnRouteNumber, _
                                                                    ExecuteAdditionalRuleID, BtnAdditionalRuleName, _
                                                                    String.Empty, String.Empty, String.Empty, True, _
                                                                    AdditionalColumnNameNumber, AdditionalColumnRouteNumber)
                            Else
                                frmMessage = clsMessages.getMailForm(SelectedResults, Automatic, _
                                                                     ExecuteRuleID, RuleName, VarAttachs, _
                                                                     ColumnNameNumber, ColumnRouteNumber, _
                                                                     ExecuteAdditionalRuleID, BtnAdditionalRuleName, Nothing, _
                                                                     Nothing, taskResult)
                            End If
                        ElseIf Links Is Nothing Or addlink = False Then
                            Dim SelectedResults As New Generic.List(Of IResult)
                            SelectedResults.AddRange(results)

                            If ViewOriginalDocument Or ViewAssociateDocument Then
                                frmCtrl = New ctrlLotusMessageSend(Automatic, ExecuteRuleID, RuleName, VarAttachs, _
                                                                 ColumnNameNumber, ColumnRouteNumber, _
                                                                 SelectedResults, ExecuteAdditionalRuleID, BtnAdditionalRuleName, _
                                                                 AdditionalColumnNameNumber, AdditionalColumnRouteNumber)
                            Else
                                frmMessage = clsMessages.getMailForm(Automatic, ExecuteRuleID, RuleName, VarAttachs, _
                                                                 ColumnNameNumber, ColumnRouteNumber, _
                                                                 SelectedResults, ExecuteAdditionalRuleID, BtnAdditionalRuleName, _
                                                                 AdditionalColumnNameNumber, AdditionalColumnRouteNumber, taskResult)

                            End If
                        Else
                            If ViewOriginalDocument Or ViewAssociateDocument Then
                                frmCtrl = New ctrlLotusMessageSend(Links, r.ID)
                            Else
                                frmMessage = clsMessages.getMailForm(Links, r.ID)
                            End If

                        End If
                    End If
                End If

                If IsNothing(frmMessage) AndAlso IsNothing(frmMessageOutlook) Then

                    '[Ezequiel] 08/04/09 - valida si el mail es automatico para mostrar mensajes
                    Trace.WriteLineIf(ZTrace.IsWarning, "El usuario no tiene configurado el correo")
                    If Automatic Then
                        ZClass.raiseerror(New Exception("El usuario no tiene configurado el correo"))
                    Else
                        MessageBox.Show("El usuario no tiene configurado el correo", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    results(0).AutoName = "Cancel"

                Else
                    'Si la regla estaba configurada para que mantenga el nombre original
                    'de los documentos asociados y existen asociados adjuntos, se modifica
                    'el nombre de los results asociados.
                    If keepOriginalDocName AndAlso AssociatedDocuments.Count > 0 Then
                        SetOriginalFileNames(AssociatedDocuments)
                    End If

                    If Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.OutLookMail And Not Automatic And Not IsNothing(frmMessageOutlook) Then

                        frmMessageOutlook.EspecificarDatos(Tostr, CC, CCO, Subject, Body, attachDocsPaths, AssociatedDocuments, Automatic, Links, EmbedImages)

                    Else
                        If ViewOriginalDocument Or ViewAssociateDocument Then
                            DirectCast(frmCtrl, ctrlLotusMessageSend).EspecificarDatos(Tostr, CC, CCO, Subject, Body, attachDocsPaths, AssociatedDocuments, Automatic, Links, EmbedImages, mailpath)
                        Else
                            If Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.NetMail Then
                                DirectCast(frmMessage, frmNetMailMessageSend).DocId = r.ID
                                DirectCast(frmMessage, frmNetMailMessageSend).DocTypeId = r.DocTypeId
                                DirectCast(frmMessage, frmNetMailMessageSend).DisableHistory = disablehistory
                            End If

                            DirectCast(frmMessage, IZMessageForm).EspecificarDatos(Tostr, CC, CCO, Subject, Body, attachDocsPaths, AssociatedDocuments, Automatic, Links, EmbedImages, mailpath)
                        End If

                    End If

                End If

            Next

            If (Not (IsNothing(frmMessage)) OrElse Not IsNothing(frmMessageOutlook)) AndAlso Not Automatic Then
                Dim resulEnvio As Boolean = False

                If Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.OutLookMail And Not Automatic And Not IsNothing(frmMessageOutlook) Then
                    If Not IsNothing(results) AndAlso results.Length > 0 Then
                        frmMessageOutlook.EspecificarDatosDoc(results(0).ID, results(0).DocTypeId)
                        resulEnvio = frmMessageOutlook.ShowDialog(True, mailpath)
                    Else
                        Trace.WriteLineIf(ZTrace.IsWarning, "No hay documentos para el envio")
                    End If
                Else
                    If ViewOriginalDocument Or ViewAssociateDocument Then
                        DirectCast(frmCtrl, ctrlLotusMessageSend).EspecificarDatosDoc(results(0).ID, results(0).DocTypeId, DirectCast(results(0), TaskResult).StepId, MailEvent.DoMail)
                    Else
                        ' [AlejandroR] 03/12/2009 - Created - Guardar ids del doc para el registro del envio del email en el historial
                        DirectCast(frmMessage, IZMessageForm).EspecificarDatosDoc(results(0).ID, results(0).DocTypeId, DirectCast(results(0), TaskResult).StepId, MailEvent.DoMail)
                    End If



                    If ViewOriginalDocument Or ViewAssociateDocument Then
                        'instancio el formulario general para generar solapas
                        Dim form As frmDocumentVisualizer = New frmDocumentVisualizer(results(0).Name, _
                                                                                      "Formulario", frmCtrl, _
                                                                                      results(0), ViewAssociateDocument, ViewOriginalDocument)

                        form.WindowState = FormWindowState.Normal
                        form.ShowInTaskbar = True
                        form.ControlBox = False
                        form.ShowDialog()
                        form.Focus()

                        If (form.DialogResult = Windows.Forms.DialogResult.OK) Then
                            resulEnvio = True
                        End If
                    Else
                        frmMessage.WindowState = FormWindowState.Normal
                        frmMessage.ShowInTaskbar = True
                        frmMessage.ShowDialog()
                        frmMessage.Focus()

                        If Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.NetMail Then
                            mailpath = DirectCast(frmMessage, frmNetMailMessageSend).mailPath
                        End If

                        If (frmMessage.DialogResult = Windows.Forms.DialogResult.OK) Then
                            resulEnvio = True
                        End If
                    End If
                End If

                If Not IsNothing(results) AndAlso results.Length > 0 Then
                    ' Si se pudo enviar el correo
                    If resulEnvio Then
                        results(0).AutoName = "OK"
                    Else
                        results(0).AutoName = "Cancel"
                    End If
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(tempAttachResultsNames) Then
                tempAttachResultsNames.Clear()
                tempAttachResultsNames = Nothing
            End If
            If Not IsNothing(frmMessage) Then
                frmMessage.Dispose()
                frmMessage = Nothing
            End If
            If Not IsNothing(frmMessageOutlook) Then
                frmMessageOutlook.Dispose()
                frmMessageOutlook = Nothing
            End If
            If Not IsNothing(frmCtrl) Then
                frmCtrl.Dispose()
                frmCtrl = Nothing
            End If
        End Try

        Return mailpath
    End Function
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
