Imports Zamba.Data
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.IO
Imports Zamba.Servers
Imports Zamba.Core.WF.WF
Imports Zamba.Core.DocTypes.DocAsociated

Public Class FormBusinessExt
    Implements IDisposable

    Private Const ZAMBA_INDEX_TO_SEARCH As String = "zamba_index_"
    Private Const ZAMBA_ZVAR_TO_SEARCH As String = "zamba_zvar("
    Private Const ZAMBA_RULE_TO_SEARCH As String = "zamba_rule_"

    Shared Function GetFormsByUserProjects(ByVal user As IUser) As List(Of IZwebForm)
        Dim sbGrups As New StringBuilder

        For Each group As UserGroup In user.Groups
            sbGrups.Append(group.ID)
            sbGrups.Append("|")
        Next

        Dim dt As DataTable = FormFactoryExt.GetFormsByUserProjects(sbGrups.ToString(), ObjectTypes.FormulariosElectronicos)
        Return ParseTableToForms(dt)
    End Function

    Shared Function GetFormsByProject(ByVal projectID As Long) As DataTable
        Return FormFactoryExt.GetFormsByProject(projectID)
    End Function

    Shared Function ParseTableToForms(ByVal dt As DataTable) As List(Of IZwebForm)
        If dt Is Nothing OrElse dt.Rows.Count < 1 Then
            Return Nothing
        End If

        Dim max As Integer = dt.Rows.Count
        Dim returnList As New List(Of IZwebForm)
        For i As Integer = 0 To max - 1
            returnList.Add(New ZwebForm(dt.Rows(i)(0), dt.Rows(i)(1), dt.Rows(i)(2), dt.Rows(i)(3), dt.Rows(i)(4), dt.Rows(i)(5), dt.Rows(i)(6), DateTime.MinValue))
        Next

        Return returnList
    End Function

    ''' <summary>
    ''' Repara el html convirtiendo caracteres incorrectos en sus correspondientes códigos html.
    ''' </summary>
    ''' <param name="html">Html crudo con caracteres inválidos</param>
    ''' <returns>Html corregido de caracteres inválidos</returns>
    ''' <remarks>Método corregido y optimizado del original de FormBusiness</remarks>
    Public Function RepairHtml(ByVal html As String) As String
        Dim formFactoryExt As New FormFactoryExt()
        html = formFactoryExt.RepairHtml(html)
        formFactoryExt = Nothing

        Return html
    End Function

    ''' <summary>
    ''' Obtiene todos los indices involucrados en un formulario(que en el id tengan "zamba_index_")
    ''' </summary>
    ''' <param name="strForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFormIndexs(ByVal strForm As String, ByVal UseCache As Boolean) As List(Of IIndex)
        'Separa por el igual y obtiene todos los que comiencen con zamba_index_
        Dim splittedValues As String() = (From str In strForm.Split(New Char() {"=", Chr(34)})
                                          Where str.StartsWith(ZAMBA_INDEX_TO_SEARCH)
                                          Select str.Replace(ZAMBA_INDEX_TO_SEARCH, String.Empty).Replace(Chr(34), Chr(0))).Distinct().ToArray()

        Dim indexs As New List(Of IIndex)
        Dim indexID As Long

        For i As Long = 0 To splittedValues.Length - 1
            indexs.Add(IndexsBussinesExt.getIndex(splittedValues(i), UseCache))
        Next

        Return indexs
    End Function


    ''' <summary>
    ''' Obtiene las zvar usadas en formularios(cuyo id comience con zama_zvar
    ''' </summary>
    ''' <param name="strForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFormZvarInCoreView(ByVal strForm As String) As List(Of String)
        Dim splittedValues As String() = (From str In strForm.Split(New Char() {"=", Chr(34)})
                                          Where str.StartsWith(ZAMBA_ZVAR_TO_SEARCH)
                                          Select str.Replace(ZAMBA_ZVAR_TO_SEARCH, String.Empty).Replace(")", String.Empty)).Distinct().ToArray()

        Dim zvarNames As New List(Of String)

        For i As Long = 0 To splittedValues.Length - 1
            zvarNames.Add(splittedValues(i))
        Next

        Return zvarNames
    End Function

    ''' <summary>
    ''' Obtiene todas las reglas que aparezcan en el formulario (que el id comience con zamba_rule
    ''' Salvo zamba_rule_save y zamba_rule_cancel
    ''' </summary>
    ''' <param name="strForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFormRuleInCoreView(ByVal strForm As String) As List(Of IWFRuleParent)
        Dim splittedValues As String() = (From str In strForm.Split(New Char() {"=", Chr(34)})
                                          Where str.StartsWith(ZAMBA_RULE_TO_SEARCH)
                                          Select str.Replace(ZAMBA_RULE_TO_SEARCH, String.Empty).Replace(Chr(34), Chr(0))).Distinct().ToArray()
        Dim rules As New List(Of IWFRuleParent)
        Dim id As String

        For i As Long = 0 To splittedValues.Length - 1
            id = Regex.Match(splittedValues(i), "\d+").ToString().ToLower()

            If Not String.IsNullOrEmpty(id) AndAlso id <> "save" AndAlso id <> "cancel" Then
                rules.Add(WFRulesBusiness.GetInstanceRuleById(id, True))
            End If
        Next

        Return rules
    End Function

    ''' <summary>
    ''' Copia el formulario digital al temporal del usuario devolviendo la ruta del formulario
    ''' </summary>
    ''' <param name="form">Formulario</param>
    ''' <remarks></remarks>
    Public Function CopyBlobToTemp(ByVal form As ZwebForm, ByVal overwrite As Boolean) As String
        'Verifica si debe copiar el formulario
        If Not File.Exists(form.TempFullPath) OrElse overwrite Then
            'Verifica si debe cargar el archivo binario
            If form.EncodedFile Is Nothing Then
                Dim frmFactoryExt As New FormFactoryExt
                form.EncodedFile = frmFactoryExt.GetDigitalForm(form.ID)
                frmFactoryExt = Nothing

                'Verifica si debe cargar el binario desde el servidor (significa que no se encuentra insertado aun)
                If form.EncodedFile Is Nothing Then
                    form.EncodedFile = FileEncode.Encode(form.Path)

                    'Verifica si hubo algún error de carga
                    If form.EncodedFile Is Nothing Then
                        Throw New Exception("Ocurrió un error al cargar el formato digital del formulario")
                    End If
                End If
            End If

            'Se prepara el directorio y archivo local
            Dim tempDir As String = Path.GetDirectoryName(form.TempFullPath)
            If Not Directory.Exists(tempDir) Then
                Directory.CreateDirectory(tempDir)
            ElseIf overwrite Then
                If File.Exists(form.TempFullPath) Then
                    If File.GetAttributes(form.TempFullPath).ToString.Contains("ReadOnly") Then
                        File.SetAttributes(form.TempFullPath, FileAttributes.Normal)
                    End If
                    File.Delete(form.TempFullPath)
                End If
            End If

            'Se decodifica el formulario en los temporales del usuario
            FileEncode.Decode(form.TempFullPath, form.EncodedFile)
        End If

        Return form.TempFullPath
    End Function

    ''' <summary>
    ''' Copia el formulario digital al temporal del usuario devolviendo la ruta del formulario
    ''' </summary>
    ''' <param name="form">Formulario</param>
    ''' <param name="path">Ruta a copiar el formulario</param>
    ''' <remarks></remarks>
    Public Sub CopyBlobToTemp(ByVal form As ZwebForm, ByVal path As String)

        'Verifica si debe cargar el archivo binario
        If form.EncodedFile Is Nothing Then
            Dim frmFactoryExt As New FormFactoryExt
            form.EncodedFile = frmFactoryExt.GetDigitalForm(form.ID)
            frmFactoryExt = Nothing

            'Verifica si debe cargar el binario desde el servidor (significa que no se encuentra insertado aun)
            If form.EncodedFile Is Nothing Then
                form.EncodedFile = FileEncode.Encode(form.Path)

                'Verifica si hubo algún error de carga
                If form.EncodedFile Is Nothing Then
                    Throw New Exception("Ocurrió un error al cargar el formato digital del formulario")
                Else
                    Try
                        'copia del documento a blob
                    Catch ex As Exception

                    End Try
                End If
            End If
        End If

        'Se prepara el directorio y archivo local
        Dim tempDir As String = IO.Path.GetDirectoryName(path)
        If Not Directory.Exists(tempDir) Then
            Directory.CreateDirectory(tempDir)
        Else
            If File.Exists(path) Then
                If File.GetAttributes(path).ToString.Contains("ReadOnly") Then
                    File.SetAttributes(path, FileAttributes.Normal)
                End If
                File.Delete(path)
            End If
        End If

        'Se decodifica el formulario en los temporales del usuario
        FileEncode.Decode(path, form.EncodedFile)
    End Sub

    ''' <summary>
    ''' Inserta o actualiza un formulario con formato digital
    ''' </summary>
    ''' <param name="form">Formulario</param>
    ''' <remarks></remarks>
    Public Sub SetDigitalFile(ByRef form As ZwebForm)
        Dim formFactoryExt As New FormFactoryExt()
        Dim t As New Transaction(Server.Con(False))

        Try
            EncodeFormFile(form)

            If form.EncodedFile IsNot Nothing Then
                formFactoryExt.SetDigitalFile(form.ID, form.EncodedFile, t)
                formFactoryExt.SetBlobFlag(form.ID, t)
                form.UseBlob = True
                t.Commit()
            Else
                Throw New FileNotFoundException("No se ha encontrado el archivo o no tiene permisos para acceder al mismo", form.Path)
            End If

        Catch ex As Exception
            If Not IsNothing(t.Transaction) AndAlso t.Transaction.Connection.State <> ConnectionState.Closed Then
                t.Rollback()
            End If
            Throw

        Finally
            formFactoryExt = Nothing
            If Not IsNothing(t) Then
                If Not IsNothing(t.Con) Then
                    t.Con.Close()
                    t.Con.dispose()
                    t.Con = Nothing
                End If
                t.Dispose()
                t = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Codifica el archivo físico de un formulario
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub EncodeFormFile(ByRef form As ZwebForm)
        If Not File.Exists(form.Path) Then
            Throw New FileNotFoundException("No se ha encontrado el archivo o no tiene permisos para acceder al mismo", form.Path)
        End If

        Dim formFactoryExt As New FormFactoryExt()
        form.EncodedFile = FileEncode.Encode(form.Path)
    End Sub

    Public Sub UpdateForm(ByRef form As ZwebForm)
        If form.UseBlob Then EncodeFormFile(form)
        Dim formFactoryExt As New FormFactoryExt
        formFactoryExt.UpdateForm(form)
        formFactoryExt = Nothing

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(form.ID, ObjectTypes.FormulariosElectronicos, RightsType.Edit, "Se modificó el formulario: " & form.Name & "(" & form.ID & ")")
    End Sub

    Public Sub InsertForm(ByVal Form As ZwebForm)
        If Form.UseBlob Then EncodeFormFile(Form)
        Dim formFactoryExt As New FormFactoryExt
        formFactoryExt.InsertForm(Form)
        formFactoryExt = Nothing

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(Form.ID, ObjectTypes.FormulariosElectronicos, RightsType.insert, "Se agregó el formulario: " & Form.Name & "(" & Form.ID & ")")
    End Sub

    Sub SetFormToRebuild(ByVal formId As Long)
        Dim formFactoryExt As New FormFactoryExt
        formFactoryExt.SetFormToRebuild(formId)
    End Sub

    Sub SetAllFormsToRebuild()
        Dim formFactoryExt As New FormFactoryExt
        formFactoryExt.SetAllFormsToRebuild()
    End Sub

    Public Sub New()
        WebServiceURl(0) = ZOptBusiness.GetValue("WSResultsUrl")
    End Sub

#Region "AsignValues"

    Private WBrowser As WebBrowser
    Dim FlagAsigned As Boolean
    Dim flagrecover As Boolean

    Public Property DateIndexsList As List(Of String)
    Public Property DateTimeIndexsList As List(Of String)
    'Public Property CopiesCount As Short
    Private _currentResult As IResult
    Public Property CurrentResult() As IResult
        Get
            Return _currentResult
        End Get
        Set(ByVal value As IResult)
            _currentResult = value
        End Set
    End Property

    Public Sub New(ByVal HtmlTemplate As String, ByVal Result As IResult)
        Me.New()
        'If File.Exists(HtmlTemplate) Then

        Me.CurrentResult = Result
        'Me.CopiesCount = CopiesCount

        DateIndexsList = New List(Of String)
        DateTimeIndexsList = New List(Of String)

        WBrowser = New WebBrowser()
        RemoveHandler WBrowser.Navigated, AddressOf AsignValues
        AddHandler WBrowser.Navigated, AddressOf AsignValues
        WBrowser.Navigate(HtmlTemplate)
        Application.DoEvents()
        'End If
    End Sub

    Private _htmlString As String
    Public Property HtmlString() As String
        Get
            Return _htmlString
        End Get
        Set(ByVal value As String)
            _htmlString = value
        End Set
    End Property

    Delegate Sub mydelegate()

    'Public Sub LoadDocument(HtmlTemplate As String)
    '    Try
    '        If File.Exists(HtmlTemplate) Then
    '            WBrowser.Navigate(HtmlTemplate)
    '            Application.DoEvents()
    '        End If
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Sub
    'Public Sub IESetupFooter()
    '    Dim strKey As String = "Software\\Microsoft\\Internet Explorer\\PageSetup"
    '    Dim bolWritable As Boolean = True
    '    Dim strNameF As String = "footer"
    '    Dim strNameH As String = "header"
    '    Dim oValue As Object = ""
    '    Dim oKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strKey, bolWritable)
    '    oKey.SetValue(strNameF, oValue)
    '    oKey.SetValue(strNameH, oValue)
    '    oKey.Close()
    'End Sub

    Public Sub PrintCurrentDocument()
        Try
            'IESetupFooter()
            'WBrowser.Print()
            'FileTools.SpireTools.GeneratePDF(WBrowser.DocumentText)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Funcion que obtiene el elemento con id "idsConTextoInteligente" (si existiese), que contiene en su atributo value los ids de los elementos que contengan
    'texto inteligente, busca cada uno de esos elementos por el id y los guarda en una lista, que es posteriormente pasada a la funcion que resuelte el texto inteligente.
    'El elemento con id "idsConTextoInteligente" es creado en el document.ready de zamba.js
    Private Function getInteligentText()
        Dim Mydoc As HtmlDocument = WBrowser.Document
        If Not IsNothing(Mydoc) AndAlso Not IsNothing(Mydoc.Body) Then
            If Not String.IsNullOrEmpty(Mydoc.Body.InnerHtml) Then
                Dim elementIds As HtmlElement = Mydoc.GetElementById("idsConTextoInteligente")
                If elementIds IsNot Nothing Then
                    Dim arrayIds() As String = Split(elementIds.GetAttribute("value"), ",")
                    Dim elements As New List(Of HtmlElement)
                    If (arrayIds.Length > 0) Then
                        For Each id As String In arrayIds
                            elements.Add(Mydoc.GetElementById(id))
                        Next
                        resolveInteligentText(elements)
                    End If
                End If
            End If
        End If
    End Function

    'Funcion que obtiene una lista de elementos que contengan texto inteligente, busca el atributo que lo contenga y finalmente lo resuelva
    Private Function resolveInteligentText(ByRef elements As List(Of HtmlElement))
        Try
            Dim outerHtml As String
            Dim index As Int32
            Dim indexFinal As Int64
            Dim IndexInicioFound As Boolean = False
            Dim attr As String
            For Each e As HtmlElement In elements
                outerHtml = e.OuterHtml
                index = outerHtml.IndexOf(">>.<<")
                While ((index > -1) And (IndexInicioFound = False))
                    If outerHtml.Chars(index) <> " " Then
                        If (outerHtml.Chars(index) = "=") Then
                            indexFinal = index - 1
                        End If
                        index = index - 1
                    Else
                        IndexInicioFound = True
                        attr = outerHtml.Substring(index + 1, (indexFinal - index))
                    End If
                End While

                If (IndexInicioFound = True) Then
                    IndexInicioFound = False
                    Dim AttrValue As String = e.GetAttribute(attr)
                    Dim value As String = TextoInteligente.ReconocerCodigo(AttrValue, CurrentResult)
                    e.SetAttribute(attr, value)
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Dim bolmodified As Boolean = False


    Public Sub AsignValues(sender As Object, e As EventArgs)
        Dim doc1 As HtmlDocument = WBrowser.Document
        Dim Element As HtmlElement
        DateIndexsList.Clear()
        DateTimeIndexsList.Clear()

        Dim task As ITaskResult



        If TypeOf CurrentResult Is ITaskResult Then
            'Por cada regla buscar si en el formulario hay un boton que la llame
            'Si la regla esta deshabilitada, deshabilitar el boton
            task = DirectCast(CurrentResult, ITaskResult)
            getInteligentText()
        End If


        Try

            'El Form debe tener Id = zamba_(doctypeid) o Id = zamba_(Nombre del DocType)
            'Los controles deben tener Id = "zamba(Id de Atributo)"  o Id = "zamba_(Nombre del indice)"
            If doc1.All.Count > 0 Then
                FlagAsigned = True

                If Not IsNothing(CurrentResult) Then

                    Dim UseIndexsRights As Boolean = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, CurrentResult.DocType.ID)

                    Dim IndexsRights As Hashtable = Nothing
                    If UseIndexsRights Then IndexsRights = UserBusiness.Rights.GetIndexsRights(CurrentResult.DocType.ID, Membership.MembershipHelper.CurrentUser.ID, True, True)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de atributos: " & CurrentResult.Indexs.Count)
                    For i As Int32 = 0 To CurrentResult.Indexs.Count - 1
                        Try

                            Element = doc1.GetElementById("ZAMBA_INDEX_" & CurrentResult.Indexs(i).ID)
                            If IsNothing(Element) Then
                                Element = doc1.GetElementById("ZAMBA_INDEX_" & CurrentResult.Indexs(i).ID & "S")
                                If IsNothing(Element) Then
                                    Element = doc1.GetElementById("ZAMBA_INDEX_" & CurrentResult.Indexs(i).ID & "N")
                                End If
                            End If
                            If Not IsNothing(Element) Then
                                bolmodified = True
                                AsignValue(CurrentResult.Indexs(i), Element)

                                If UseIndexsRights AndAlso Not IsNothing(Element) Then

                                    Dim IR As IndexsRightsInfo = DirectCast(IndexsRights(CurrentResult.Indexs(i).ID), IndexsRightsInfo)
                                    For Each indexid As Int64 In IndexsRights.Keys
                                        If indexid = CurrentResult.Indexs(i).ID Then
                                            'aplica permiso Visible
                                            If IR.GetIndexRightValue(RightsType.IndexView) = False Then
                                                'Oculta el atributo
                                                Element.Style = "display:none"
                                                'Oculta el label del indice
                                                Dim htmlElement_label As HtmlElement = doc1.GetElementById("ZAMBA_INDEX_" & CurrentResult.Indexs(i).ID & "_LBL")
                                                If Not IsNothing(htmlElement_label) Then
                                                    htmlElement_label.Style = "display:none"
                                                End If
                                            End If

                                            ' [Gaston]    12/05/2009  Si el formulario no es un formulario dinámico entonces no se aplica el permiso de 
                                            '                         edición, porque sino estaría entrando en conflicto con un Atributo que tenga 
                                            '                         "sólo lectura" (en caso de que se haya configurado un Atributo para sólo lectura)
                                            If Not ((doc1.Body.InnerHtml.Contains("<FORM id=")) AndAlso (doc1.Body.InnerHtml.Contains("name=frmmain"))) Then
                                                'aplica permiso Edicion
                                                If Element.Enabled Then Element.Enabled = IR.GetIndexRightValue(RightsType.IndexEdit)
                                            End If
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    Next

                    Try
                        If DateIndexsList.Count > 0 Then
                            For Each id As String In DateIndexsList
                                WBrowser.Document.InvokeScript("makeCalendar", New Object() {id})
                            Next
                        End If

                        If DateTimeIndexsList.Count > 0 Then
                            For Each id As String In DateTimeIndexsList
                                WBrowser.Document.InvokeScript("makeDateTimeCalendar", New Object() {id})
                            Next
                        End If

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Try
                        Element = doc1.GetElementById("ZAMBA_IMAGE")
                        LoadImage(CurrentResult, Element)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Try
                        Element = doc1.GetElementById("ZAMBA_BARCODE")
                        LoadBarcode(CurrentResult.barcodeInBase64, Element)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try


                    Dim CurrentForm As IZwebForm = FormBusiness.GetForm(CurrentResult.CurrentFormID)
                    If Not IsNothing(CurrentForm) AndAlso CurrentForm.useRuleRights = True Then
                        If TypeOf CurrentResult Is ITaskResult Then
                            'Por cada regla buscar si en el formulario hay un boton que la llame
                            'Si la regla esta deshabilitada, deshabilitar el boton
                            task = DirectCast(CurrentResult, ITaskResult)
                            Dim rule As IWFRuleParent
                            Dim tagId, tempTagId As String

                            Try
                                Dim currentLockedUser As String
                                Dim IsTaskLocked As Boolean = WFTaskBusiness.LockTask(task.TaskId, currentLockedUser)

                                'todo: ml: ver si se puede obtener de manera mas directa los tags
                                For Each el As HtmlElement In WBrowser.Document.Body.All
                                    If el.Id IsNot Nothing AndAlso el.Id.Length > 11 Then
                                        tagId = el.Id.ToLower()
                                        If tagId.Contains("zamba_rule_") Then
                                            'Se verifica la correcta construcción del tag
                                            tempTagId = GetRuleIdFromTag(tagId)
                                            If Not String.IsNullOrEmpty(tempTagId) Then
                                                'ML: Se puede mejorar mas no instanciando y verificando con el id y el dsrules del cache
                                                rule = Zamba.Core.WFRulesBusiness.GetInstanceRuleById(GetRuleIdFromTag(tempTagId), True)
                                                If rule IsNot Nothing AndAlso IsTaskLocked = False AndAlso (
                                                    Not rule.Enable _
                                                    OrElse Not WFRulesBusiness.GetIsRuleEnabled(task.UserRules, rule) _
                                                    OrElse Not WFRulesBusiness.GetStateOfHabilitationOfState(rule, task.State.ID)) Then

                                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ocultando botón en el formulario para la regla: " & rule.ID)
                                                    el.Style = "display:none"
                                                End If
                                            Else
                                                LogBadTag(tagId, CurrentForm.Name)
                                            End If
                                        End If
                                    End If
                                Next
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            Finally
                                rule = Nothing
                                task = Nothing
                            End Try
                        End If
                    End If

                    Try

                        Dim WorkerProcess As New ComponentModel.BackgroundWorker
                        WorkerProcess.WorkerReportsProgress = True
                        RemoveHandler WorkerProcess.DoWork, AddressOf WP_DoWork
                        RemoveHandler WorkerProcess.RunWorkerCompleted, AddressOf RunWorkerCompleted
                        AddHandler WorkerProcess.DoWork, AddressOf WP_DoWork
                        AddHandler WorkerProcess.RunWorkerCompleted, AddressOf RunWorkerCompleted

                        WorkerProcess.RunWorkerAsync()

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Dim elements As List(Of String)
                    Try
                        elements = getItems("zvar")
                        For Each Str As String In elements
                            'zamba_zvar_nombrevariable
                            Dim varname As String
                            If Str.Contains("(") Then
                                varname = Str.Replace("zamba_zvar(", "zvar(")
                                varname = Str.Replace("zvar(", String.Empty).Replace(")", String.Empty)
                            Else
                                varname = Str.Replace("zamba_zvar(", "zvar(")
                                varname = Str.Remove(0, "zvar_".Length)
                            End If

                            Element = doc1.GetElementById(Str)
                            If IsNothing(Element) Then
                                Element = doc1.GetElementById("zamba_" & Str)
                            End If

                            If Not IsNothing(Element) Then

                                AsignVarValue(varname, Element)

                                If Str.Contains("(") Then
                                    Element.Id = varname
                                End If
                            End If
                        Next
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    Finally
                        elements = Nothing
                    End Try

                    enableRules(doc1)

                    If Not IsNothing(doc1) Then
                        If Not IsNothing(doc1.Body) Then
                            If Not String.IsNullOrEmpty(doc1.Body.InnerHtml) Then
                                Dim strHtml As String = doc1.Body.InnerHtml
                                CurrentResult.Html = strHtml
                            End If
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If _disableInputControls Then
            doDisableInputControls()
        End If

        'If CopiesCount > 0 Then
        'WBrowser.Print()
        'Application.DoEvents()
        'End If
        HtmlString = WBrowser.Document.Body.Parent.OuterHtml
        HtmlString = TextoInteligente.ReconocerCodigo(HtmlString, task)
        HtmlString = WFRuleParent.ReconocerVariablesValuesSoloTexto(HtmlString)
    End Sub


    ' [AlejandroR] 28/12/09 - Created
    ' Deshabilita todos los controles de tipo input para que no se pueda editar el formulario
    Private Function doDisableInputControls()
        Dim tag As String
        Try
            For Each el As HtmlElement In WBrowser.Document.Body.All
                tag = el.TagName.ToLower()
                If tag = "input" OrElse tag = "select" OrElse tag = "button" Then
                    el.Enabled = False
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private _disableInputControls As Boolean = False
    Public Property DisableInputControls() As Boolean
        Get
            Return _disableInputControls
        End Get
        Set(ByVal value As Boolean)
            _disableInputControls = value
        End Set
    End Property

    Private rulesIds As List(Of Int64)
    ''' <summary>
    ''' Se encarga de habilitar las reglas dependiendo del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function enableRules(ByVal doc1 As HtmlDocument)
        If Not IsNothing(rulesIds) Then
            Dim task As TaskResult
            Dim indexsAndVariables As List(Of IndexAndVariable)
            Dim WFIndexAndVariableBusiness As WFIndexAndVariableBusiness
            Dim DtUsersAndGroup As DataTable
            Dim Dt2 As DataTable

            Try
                task = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(CurrentResult.ID, 0)

                For i As Int32 = 0 To rulesIds.Count - 1
                    Dim wfstepID As Int64 = WFStepBusiness.GetStepIdByRuleId(rulesIds(i), True)
                    Dim DtOptions As DataTable = WFRulesBusiness.GetRuleOptionsDT(True, wfstepID)
                    Dim DV As New DataView(DtOptions)

                    Dim _enabled As Boolean = True

                    'If wfstepID = task.StepId Then
                    'Obtiene el valor 
                    Dim selectionvalue As RulePreferences = WFBusiness.recoverItemSelectedThatCanBe_StateOrUserOrGroup(wfstepID, rulesIds(i), False)
                    'Se Evalua el valor de la variable seleccion 
                    Select Case selectionvalue
                        'Caso de trabajo con Estados
                        Case RulePreferences.HabilitationSelectionState
                            _enabled = True
                            'Se Obtienen los ids de estados DESHABILITADOS
                            DV.RowFilter = "ruleid = " & rulesIds(i) & " and SectionId= " & RuleSectionOptions.Habilitacion & " And ObjectId =" & RulePreferences.HabilitationTypeState

                            'Por cada Id de estado se compara con el id de estado seleccionado, en cada de encontrar
                            'Coincidencia, se deshabilita la regla
                            For Each r As DataRow In DV.ToTable.Rows
                                If Int32.Parse(r.Item("ObjValue").ToString) = task.StateId Then
                                    _enabled = False
                                    Exit For
                                End If
                            Next
                            'Caso de trabajo con Usuarios o Grupos
                        Case RulePreferences.HabilitationSelectionUser
                            _enabled = True
                            'Se Obtienen los ids de USUARIOS DESHABILITADOS
                            DV.RowFilter = "ruleid = " & rulesIds(i) & " and SectionId= " & RuleSectionOptions.Habilitacion & " And ObjectId =" & RulePreferences.HabilitationTypeUser
                            '                           Dim Dt As DataTable = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeUser, True)
                            'Por cada Id de usuario se compara con el id de usuario logeado, en cada de encontrar
                            'Coincidencia, se deshabilita la regla
                            For Each r As DataRow In DV.ToTable.Rows
                                If Int64.Parse(r.Item("ObjValue").ToString) = Membership.MembershipHelper.CurrentUser.ID Then
                                    _enabled = False
                                    Exit For
                                End If
                            Next
                            'si no se deshabilito la regla por usuario se intenta deshabilitar por grupo
                            If _enabled = True Then
                                'Se Obtienen los ids de GRUPOS DESHABILITADOS
                                DV.RowFilter = "ruleid = " & rulesIds(i) & " and SectionId= " & RuleSectionOptions.Habilitacion & " And ObjectId =" & RulePreferences.HabilitationTypeGroup
                                '                                Dt2 = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeGroup, True)
                                'Por cada Id de Grupo se recorren sus usuario y se comparan con el id de usuario logeado, en cada de encontrar
                                'Coincidencia, se deshabilita la regla
                                For Each r As DataRow In DV.ToTable.Rows
                                    Dim uids As List(Of Int64) = UserGroupBusiness.GetUsersIds(Int64.Parse(r.Item("ObjValue").ToString()), True)
                                    If Not IsNothing(uids) Then
                                        For Each uid As Int64 In uids

                                            If uid = Membership.MembershipHelper.CurrentUser.ID Then
                                                _enabled = False
                                                Exit For
                                            End If
                                        Next
                                    End If
                                    If _enabled = False Then Exit For
                                Next
                            End If
                        Case RulePreferences.HabilitationSelectionIndexAndVariable
                            WFIndexAndVariableBusiness = New WFIndexAndVariableBusiness()
                            indexsAndVariables = WFIndexAndVariableBusiness.GetIndexAndVariableByRuleID(rulesIds(i))
                            'Se obtienen los ids de variables
                            _enabled = True

                            'Se Obtienen los ids de estados DESHABILITADOS
                            '                            Dim Dt As DataTable = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeIndexAndVariable, True)

                            For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                                If validar(_IndexAndVariable, task, WFIndexAndVariableBusiness) = True Then
                                    DV.RowFilter = "ruleid = " & rulesIds(i) & " and SectionId= " & RuleSectionOptions.Habilitacion & " And ObjectId =" & RulePreferences.HabilitationTypeIndexAndVariable & " and ObjValue= " & _IndexAndVariable.ID
                                    '                                    Dt.DefaultView.RowFilter = "ObjValue=" & _IndexAndVariable.ID
                                    If DV.ToTable().Rows.Count > 0 Then
                                        _enabled = False
                                    Else
                                        _enabled = True
                                        Exit For
                                    End If
                                End If
                            Next

                        Case RulePreferences.HabilitationSelectionBoth
                            _enabled = True
                            'Se Obtienen los ids de estados DESHABILITADOS
                            Dim Dt1 As DataTable = WFBusiness.recoverDisableItemsBoth(rulesIds(i)).Tables(0)

                            'Filtro por estado
                            Dt1.DefaultView.RowFilter = "ObjValue='" & task.StateId & "' and ObjectId in (37,38)"
                            Dt2 = Dt1.DefaultView.ToTable()

                            If Dt2.Rows.Count > 0 Then
                                'Se obtienen los ids de grupo del usuario y que tienen permiso en la etapa
                                DtUsersAndGroup = WFStepBusiness.GetStepUserGroupsIdsAsDS(WFStepBusiness.GetStepIdByRuleId(rulesIds(i), True), Membership.MembershipHelper.CurrentUser.ID)
                                WFIndexAndVariableBusiness = New WFIndexAndVariableBusiness()
                                indexsAndVariables = WFIndexAndVariableBusiness.GetIndexAndVariableByRuleID(rulesIds(i))

                                For Each r As DataRow In Dt2.Rows
                                    'Valido por grupo y usuario
                                    If Int32.Parse(r.Item("ObjExtraData").ToString) = Membership.MembershipHelper.CurrentUser.ID Then
                                        _enabled = False
                                        For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                                            If validar(_IndexAndVariable, task, WFIndexAndVariableBusiness) Then
                                                Dt1.DefaultView.RowFilter = "ObjExtraData='" & _IndexAndVariable.ID & "' and ObjectId =62 and ObjValue ='" & task.StateId & "'"

                                                If Dt1.DefaultView.ToTable().Rows.Count > 0 Then
                                                    _enabled = False
                                                Else
                                                    _enabled = True
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If

                                    For Each rUser As DataRow In DtUsersAndGroup.Rows
                                        If rUser.Item(0).ToString() = r.Item("ObjExtraData").ToString() Then
                                            _enabled = False
                                            For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                                                If validar(_IndexAndVariable, task, WFIndexAndVariableBusiness) Then
                                                    Dt1.DefaultView.RowFilter = "ObjExtraData='" & _IndexAndVariable.ID & "' and ObjectId =62 and ObjValue ='" & task.StateId & "'"

                                                    If Dt1.DefaultView.ToTable().Rows.Count > 0 Then
                                                        _enabled = False
                                                    Else
                                                        _enabled = True
                                                        Exit For
                                                    End If
                                                End If
                                            Next
                                        End If
                                    Next
                                Next
                            End If
                    End Select
                    'Else
                    '_enabled = False
                    'End If
                    Dim ruleElement As HtmlElement = doc1.GetElementById("Zamba_rule_" & rulesIds(i))
                    If Not IsNothing(ruleElement) Then
                        ruleElement.Enabled = _enabled
                    End If
                Next
            Finally
                task = Nothing
            End Try
        End If
    End Function

    Private Function validar(ByVal _IndexAndVariable As IndexAndVariable, ByVal _TaskResult As TaskResult, ByVal IndexAndVariableBusiness As WFIndexAndVariableBusiness) As Boolean
        Dim IndexAndVariableConfList As List(Of IndexAndVariableConfiguration) = IndexAndVariableBusiness.GetIndexAndVariableConfiguration(_IndexAndVariable.ID)
        Dim TextoInteligente As New Core.TextoInteligente()

        Try
            For Each IndexAndVariableConf As IndexAndVariableConfiguration In IndexAndVariableConfList
                Dim value1 As String = IndexAndVariableConf.Name
                If IndexAndVariableConf.Manual = "N" Then
                    For Each i As Index In _TaskResult.Indexs
                        If value1 = i.ID Then
                            value1 = i.Data
                            Exit For
                        End If
                    Next
                Else
                    value1 = WFRuleParent.ReconocerVariablesValuesSoloTexto(value1)
                    value1 = TextoInteligente.ReconocerCodigo(value1, _TaskResult)
                End If

                Dim value2 As String = IndexAndVariableConf.Value
                value2 = WFRuleParent.ReconocerVariablesValuesSoloTexto(value2)
                value2 = TextoInteligente.ReconocerCodigo(value2, _TaskResult)

                Dim comparator As Comparadores
                'Le asigno el comparador al IfIndex
                Select Case IndexAndVariableConf.Operador
                    Case "="
                        comparator = Comparadores.Igual
                    Case "<>"
                        comparator = Comparadores.Distinto
                    Case "<"
                        comparator = Comparadores.Menor
                    Case ">"
                        comparator = Comparadores.Mayor
                    Case "<="
                        comparator = Comparadores.IgualMenor
                    Case "Contiene"
                        comparator = Comparadores.Contiene
                    Case "Empieza"
                        comparator = Comparadores.Empieza
                    Case "Termina"
                        comparator = Comparadores.Termina
                    Case ">="
                        comparator = Comparadores.IgualMayor
                End Select
                ' para mantener la funcionalidad vieja, ingrasamos False como parametro en tmpCaseInsensitive
                If ToolsBusiness.ValidateComp(value1, value2, comparator, False) = False Then
                    Return False
                ElseIf _IndexAndVariable.Operador.ToLower() = "or" Then
                    Return True
                End If
            Next
        Finally
            TextoInteligente = Nothing
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Cargo el valor del indice al elemento html
    ''' </summary>
    ''' <param name="I"></param>
    ''' <param name="E"></param>
    ''' <history></history>
    '''     [Tomas] 21/09/2009  Modified    Se agrego una validación cuando carga los valores 
    '''                                     de los textbox y textareas para que si el campo Data 
    '''                                     del indice se encuentra vacio y el text se encuentre 
    '''                                     con datos (caso del estar completando datos por primera  
    '''                                     vez) estos datos no se pierdan.
    ''' <remarks></remarks>
    Private Sub AsignVarValue(ByVal VarName As String, ByVal E As HtmlElement)
        Try

            ' Filtra por tipo de control...
            Select Case E.TagName.ToLower
                Case "input" ', "SELECT"
                    Select Case CStr(E.DomElement.type).ToLower
                        Case "text", "hidden"
                            If IsNothing(VariablesInterReglas.Item(VarName)) Then
                                'Verifica si en el objeto existe algún valor o no
                                If IsNothing(E.DomElement.value) Then
                                    E.DomElement.value = String.Empty
                                End If
                            Else
                                E.DomElement.value = VariablesInterReglas.Item(VarName)
                            End If
                        Case "checkbox"
                            'If ZTrace.IsVerbose Then
                            '    Try
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor variable = " & VariablesInterReglas.Item(VarName))
                            '    Catch ex As Exception
                            '        Zamba.Core.ZClass.raiseerror(ex)
                            '    End Try
                            'End If


                            If IsNothing(VariablesInterReglas.Item(VarName)) OrElse VariablesInterReglas.Item(VarName) = "0" OrElse VariablesInterReglas.Item(VarName) = String.Empty Then
                                E.DomElement.checked = 0
                            Else
                                E.DomElement.checked = 1
                            End If
                        Case "radio"
                            'If ZTrace.IsVerbose Then
                            '    Try
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor variable = " & VariablesInterReglas.Item(VarName))
                            '    Catch ex As Exception
                            '        Zamba.Core.ZClass.raiseerror(ex)
                            '    End Try
                            'End If


                            If IsNothing(VariablesInterReglas.Item(VarName)) Then
                                ' ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es otra cosa")

                                If E.Id.ToUpper().EndsWith(")") Then
                                    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked N= " & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = False
                                End If
                            ElseIf String.Compare(Trim(VariablesInterReglas.Item(VarName).ToString), "0") = 0 Then
                                '  ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es nothing, es 0, o empty")
                                If E.Id.ToUpper().EndsWith(")") Then
                                    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked = " & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = False
                                End If
                            ElseIf Trim(CType(VariablesInterReglas.Item(VarName), String)) = "1" Then
                                '  ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es 1")
                                If E.Id.ToUpper().EndsWith(")") Then
                                    '     ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked =" & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = True
                                End If
                            Else
                                '   ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es otra cosa")

                                If E.Id.ToUpper().EndsWith(")") Then
                                    '     ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked N= " & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = False
                                End If
                            End If
                        Case "select-one"

                            If IsNothing(VariablesInterReglas.Item(VarName)) Then
                                E.DomElement.value = VariablesInterReglas.Item(VarName)
                            Else
                                'Verifica si en el objeto existe algún valor o no
                                If IsNothing(E.DomElement.value) Then
                                    E.DomElement.value = String.Empty
                                End If
                            End If
                    End Select
                Case "select"
                    If E.Children.Count = 0 Then

                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor variable" & VariablesInterReglas.Item(VarName))
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If

                        If IsNothing(VariablesInterReglas.Item(VarName)) = False OrElse VariablesInterReglas.Item(VarName) <> "0" OrElse VariablesInterReglas.Item(VarName) <> String.Empty Then
                            'If Reload Then
                            'Si no esta cargada, cargo solo el item seleccionado
                            Dim readonli As String = E.GetAttribute("ReadOnly")

                            If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                                Dim tag As HtmlElement = E.Document.CreateElement("option")

                                tag.SetAttribute("selected", VariablesInterReglas.Item(VarName))
                                tag.SetAttribute("value", VariablesInterReglas.Item(VarName))
                                tag.InnerText = VariablesInterReglas.Item(VarName)
                                E.AppendChild(tag)
                            End If
                            'End If

                            'If ZTrace.IsVerbose Then
                            '    Try
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Id " & E.Id & ". Value " & E.DomElement.value)
                            '    Catch ex As Exception
                            '        Zamba.Core.ZClass.raiseerror(ex)
                            '    End Try
                            'End If

                        End If
                    End If
                Case "textarea"
                    'If ZTrace.IsVerbose Then
                    '    Try
                    '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Id " & E.Id & ". Value " & E.DomElement.value)
                    '    Catch ex As Exception
                    '        Zamba.Core.ZClass.raiseerror(ex)
                    '    End Try
                    '    Try
                    '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor variable" & VariablesInterReglas.Item(VarName))
                    '    Catch ex As Exception
                    '        Zamba.Core.ZClass.raiseerror(ex)
                    '    End Try
                    'End If

                    If Not IsNothing(VariablesInterReglas.Item(VarName)) Then
                        E.SetAttribute("value", VariablesInterReglas.Item(VarName))
                    Else
                        E.SetAttribute("value", String.Empty)
                    End If
                Case "table"
                    'Valido si existe el nodo TBODY , para usarlo para las rows 
                    If E.Children.Count > 0 Then
                        For Each child As HtmlElement In E.Children
                            If String.Compare(child.TagName.ToLower(), "tbody") = 0 Then
                                E = child
                                Exit For
                            End If
                        Next
                    End If

                    If (Not IsNothing(E)) Then
                        If Not IsNothing(E.InnerHtml) Then
                            E.InnerText = String.Empty
                        End If
                        Dim dt As Object = VariablesInterReglas.Item(VarName)

                        If Not IsNothing(dt) Then
                            If Not IsNothing(E.Id) AndAlso String.IsNullOrEmpty(E.Id) = False Then
                                Dim dt2 As DataTable = New DataTable

                                dt2.Columns.Add(New DataColumn("Ejecutar"))

                                If (TypeOf (dt) Is DataSet) Then
                                    dt2.Merge(DirectCast(dt, DataSet).Tables(0))
                                Else
                                    dt2.Merge(dt)
                                End If

                                LoadTableHeader(E, dt2.Columns, WBrowser.Document)
                                LoadTableVarBody(E, dt2.Rows, WBrowser.Document)
                            Else
                                If (TypeOf (dt) Is DataSet) Then
                                    LoadTableHeader(E, DirectCast(dt, DataSet).Tables(0).Columns, WBrowser.Document)
                                    LoadTableVarBody(E, DirectCast(dt, DataSet).Tables(0).Rows, WBrowser.Document)
                                Else
                                    LoadTableHeader(E, DirectCast(dt, DataTable).Columns, WBrowser.Document)
                                    LoadTableVarBody(E, DirectCast(dt, DataTable).Rows, WBrowser.Document)
                                End If
                            End If
                        End If
                    End If
                Case "div"
                    If Not IsNothing(VariablesInterReglas.Item(VarName)) Then
                        E.InnerText = VariablesInterReglas.Item(VarName)
                    Else
                        E.InnerText = String.Empty
                    End If
            End Select
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Carga el contenido de un DataTabla en el body de una tabla Html del documento
    ''' </summary>
    ''' <param name="table">Tabla HTML donde se cargaran los datos</param>
    ''' <param name="drs">Rows que van a ser cargadas</param>
    ''' <param name="mydoc">Documento HTML que contiene la tabla</param>
    ''' <param name="withBtn">Agregar o no el Boton Ver</param>
    ''' <history>   Marcelo 06/01/10 Created</history>
    ''' <remarks></remarks>
    Private Sub LoadTableVarBody(ByRef table As HtmlElement, ByVal drs As DataRowCollection, ByRef mydoc As HtmlDocument)
        Dim CurrentRow As HtmlElement = Nothing
        Dim CurrentCell As HtmlElement = Nothing
        Dim i As Int32
        Dim textItem2, textAux As String
        Dim itemNum As Int32

        Dim items As Array
        If Not IsNothing(table.Id) AndAlso String.IsNullOrEmpty(table.Id) = False Then
            items = table.Id.Split("/")
        End If
        Dim zvarItems As Array
        Dim params As String

        Dim InnerHtml As StringBuilder = New StringBuilder()
        For Each dr As DataRow In drs
            CurrentRow = mydoc.CreateElement("tr")

            If Not IsNothing(items) Then

                InnerHtml.Append("<INPUT id=")
                InnerHtml.Append(Chr(34))

                'Si tiene zvar
                If items.Length > 2 Then

                    params = String.Empty

                    textItem2 = items(2).ToString()
                    InnerHtml.Append(items(0) + "_")

                    While String.IsNullOrEmpty(textItem2) = False
                        textAux = textItem2.Remove(0, 5)
                        zvarItems = textAux.Remove(textAux.IndexOf(")")).Split("=")
                        textItem2 = textItem2.Remove(0, textItem2.IndexOf(")") + 1)

                        If Int32.TryParse(zvarItems(1).ToString(), itemNum) = False Then
                            If zvarItems(1).ToString().ToLower().Contains("length") Then
                                itemNum = dr.ItemArray.Length - Int32.Parse(zvarItems(1).ToString().Split("-")(1))
                            End If
                        End If
                        InnerHtml.Append("zvar(" + zvarItems(0).ToString() + "=" + dr.ItemArray(itemNum).ToString() + ")")

                        params = params & "'" & dr.ItemArray(itemNum).ToString() & "',"
                    End While
                Else
                    InnerHtml.Append(items(0))
                End If

                InnerHtml.Append(Chr(34))
                InnerHtml.Append(" type=button onclick=")

                'Si hay un cuarto parametro es el nombre de la funcion JS que hay que llamar,
                'sino se llama a SetRuleId por default
                If items.Length > 3 Then
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append(items(3) & "(this, ")
                    InnerHtml.Append(params.Substring(0, params.Length - 1))
                    InnerHtml.Append(");")
                    InnerHtml.Append(Chr(34))
                Else
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append("SetRuleId(this);")
                    InnerHtml.Append(Chr(34))
                End If

                InnerHtml.Append(" value = ")
                InnerHtml.Append(Chr(34))
                InnerHtml.Append(items(1))
                InnerHtml.Append(Chr(34))
                InnerHtml.Append(" Name = ")
                InnerHtml.Append(Chr(34))
                InnerHtml.Append(items(0))
                InnerHtml.Append(Chr(34))
                InnerHtml.Append(" >")

                CurrentCell = mydoc.CreateElement("td")
                CurrentCell.InnerHtml = InnerHtml.ToString()
                CurrentRow.AppendChild(CurrentCell)
                InnerHtml.Remove(0, InnerHtml.Length)
            End If

            For i = 0 To dr.ItemArray.Length - 1
                'Salteo el 1er ciclo x el btn
                If i > 0 Or String.IsNullOrEmpty(table.Id) Then
                    CurrentCell = mydoc.CreateElement("td")
                    CurrentCell.InnerHtml = dr.ItemArray(i).ToString()
                    CurrentRow.AppendChild(CurrentCell)
                End If
            Next
            table.AppendChild(CurrentRow)
        Next
    End Sub

    ''' <summary>
    ''' Carga las columnas header de un DataTable en el una tabla Html del documento
    ''' </summary>
    ''' <param name="table"></param>
    ''' <param name="dcs"></param>
    ''' <remarks></remarks>
    Private Sub LoadTableHeader(ByRef table As HtmlElement, ByVal dcs As DataColumnCollection, ByRef mydoc As HtmlDocument)
        Dim Header As HtmlElement = mydoc.CreateElement("thead")

        Dim HeaderRow As HtmlElement = mydoc.CreateElement("tr")

        Dim HeaderColumn As HtmlElement = Nothing

        'Agrego columnas de atributos
        For Each Column As DataColumn In dcs
            HeaderColumn = mydoc.CreateElement("th")
            HeaderColumn.InnerHtml = Column.ColumnName
            HeaderRow.AppendChild(HeaderColumn)
        Next
        Header.AppendChild(HeaderRow)
        table.AppendChild(Header)
    End Sub

    Private Function getItems(ByVal elementName As String) As List(Of String)
        Dim elements As New List(Of String)
        Dim Mydoc As HtmlDocument = WBrowser.Document

        If Not IsNothing(Mydoc) AndAlso Not IsNothing(Mydoc.Body) Then
            If Not String.IsNullOrEmpty(Mydoc.Body.InnerHtml) Then
                Dim body As String = Mydoc.Body.InnerHtml

                While body.ToLower().Contains(elementName)
                    Dim index As Int32 = body.IndexOf(elementName, StringComparison.InvariantCultureIgnoreCase)
                    Dim elem As String = body.Substring(index)
                    elem = elem.Substring(0, elem.IndexOf(" ")).Replace(Chr(34), String.Empty)

                    If elem.Contains(">") Then
                        elem = elem.Substring(0, elem.IndexOf(">"))
                    End If
                    elements.Add(elem)

                    body = body.Substring(index)
                    body = body.Replace(elem, String.Empty)
                End While
            End If
        End If
        Return elements
    End Function

    Delegate Sub DLoadLists()

    ''' <summary>
    ''' Obtiene el tipo de dropdown en indices locales
    ''' </summary>
    ''' <param name="childID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetIndexDropDown(ByVal childID As Long) As IndexAdditionalType
        If childID > 0 Then
            Dim max As Integer = CurrentResult.Indexs.Count
            For i As Integer = 0 To max - 1
                If CurrentResult.Indexs(i).ID = childID Then
                    Return CurrentResult.Indexs(i).DropDown
                End If
            Next
        End If

        Return IndexAdditionalType.NoIndex
    End Function

    ''' <summary>
    ''' Crea un elemento option
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="prompt"></param>
    ''' <param name="indexData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOptionTag(ByVal value As String, ByVal prompt As String, ByVal indexData As String) As HtmlElement
        Dim tag As HtmlElement = WBrowser.Document.CreateElement("option")

        If String.Compare(indexData.Trim, value.Trim) = 0 Then
            tag.SetAttribute("selected", value.Trim)
        End If
        tag.SetAttribute("value", value.Trim)
        tag.InnerText = prompt.Trim

        Return tag
    End Function


    ''' <summary>
    ''' Handler del evento change de los indices jerarquicos
    ''' </summary>
    ''' <param name="element"></param>
    ''' <param name="parentID"></param>
    ''' <param name="childID"></param>
    ''' <remarks></remarks>
    Private Sub HierarchicalChange(ByVal element As HtmlElement, ByVal parentID As Long, ByVal childID As Long)
        Try
            'MessageBox.Show("Mensaje para poder debbugear el codigo sin que se cuelgue el cliente y el visual")
            Dim childElement As HtmlElement = WBrowser.Document.GetElementById("zamba_index_" & childID)
            'Si se encuentra el indice hijo
            If Not childElement Is Nothing Then
                childElement.InnerHtml = String.Empty

                Dim max As Integer = element.Children.Count
                Dim optionElement As HtmlElement
                Dim value As String = element.GetAttribute("value")

                Dim tableOptions As DataTable = IndexsBussinesExt.GetHierarchicalTableByValue(childID, parentID, value, True)

                If Not tableOptions Is Nothing Then
                    max = tableOptions.Rows.Count
                    Dim indexType As IndexAdditionalType = GetIndexDropDown(childID)

                    Dim elementValue As Object
                    Dim elementPrompt As Object
                    'Rerorremos las opciones retornadas
                    For i As Integer = 0 To max - 1

                        elementValue = tableOptions.Rows(i)("Value")
                        If IsDBNull(elementValue) Then
                            elementValue = String.Empty
                        End If

                        If indexType = IndexAdditionalType.DropDown OrElse indexType = IndexAdditionalType.DropDownJerarquico Then
                            elementPrompt = tableOptions.Rows(i)("Value")
                            If IsDBNull(elementPrompt) Then
                                elementPrompt = String.Empty
                            End If
                        Else
                            If IsDBNull(tableOptions.Rows(i)(0)) Then
                                elementPrompt = "A definir"
                            Else
                                elementPrompt = tableOptions.Rows(i)(0) & " - " & tableOptions.Rows(i)(1)
                            End If
                        End If

                        childElement.AppendChild(GetOptionTag(elementValue, elementPrompt, String.Empty))
                    Next

                    'Si el indice tiene hijos
                    Dim currIndex As IIndex = GetLocalIndex(childID)
                    If Not currIndex.HierarchicalChildID Is Nothing Then
                        Dim childCount As Integer = currIndex.HierarchicalChildID.Count

                        For j As Integer = 0 To childCount - 1
                            If currIndex.HierarchicalChildID(j) > 0 Then
                                HierarchicalChange(childElement, childID, currIndex.HierarchicalChildID(j))
                            End If
                        Next
                    End If
                    'AxWebBrowser1.Document.InvokeScript("ZFUNCTION")
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene el indice local
    ''' </summary>
    ''' <param name="indexID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLocalIndex(ByVal indexID As Long) As IIndex
        If indexID > 0 Then
            Dim max As Integer = CurrentResult.Indexs.Count
            For i As Integer = 0 To max - 1
                If CurrentResult.Indexs(i).ID = indexID Then
                    Return CurrentResult.Indexs(i)
                End If
            Next
        End If
    End Function

    Private Sub LoadAutosustitutionLists()
        Try
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Cargo Listas " & ASListToLoad.Count)
            If ASListToLoad.Count > 0 Then
                Dim doc1 As HtmlDocument ' mshtml.HTMLDocumentClass
                doc1 = WBrowser.Document
                Dim i As Int64
                Dim e As HtmlElement = Nothing
                If Not IsNothing(doc1) Then
                    For Each listitem As ListItem In ASListToLoad
                        e = doc1.GetElementById(listitem.ElementId)
                        If Not e Is Nothing Then
                            Dim table As DataTable

                            If listitem.Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                table = IndexsBussinesExt.GetHierarchicalTableByValue(listitem.Index.ID, listitem.Index.HierarchicalParentID, GetLocalIndexValue(listitem.Index.HierarchicalParentID), True)
                            Else
                                table = AutoSubstitutionBusiness.GetIndexData(listitem.Index.ID, False)
                            End If

                            Dim tagName As String = String.Empty

                            If e.Children.Count = table.Rows.Count = True Then
                                Exit Try
                            End If

                            For i = 0 To table.Rows.Count - 1
                                Dim optionCode As String = Convert.ToString(table.Rows(i).Item(0)).Trim
                                Dim optionValue As String = Convert.ToString(table.Rows(i).Item(1)).Trim

                                If String.IsNullOrEmpty(optionCode) Then
                                    tagName = "A definir"
                                Else
                                    tagName = String.Concat(optionCode, " - ", optionValue)
                                End If

                                e.AppendChild(GetOptionTag(optionCode, tagName, listitem.Index.Data))
                            Next

                            If String.IsNullOrEmpty(listitem.Index.Data) Then
                                e.SetAttribute("value", String.Empty)
                            End If

                            If Not listitem.Index.HierarchicalChildID Is Nothing Then
                                Dim countChild As Integer = listitem.Index.HierarchicalChildID.Count

                                For j As Integer = 0 To countChild - 1

                                    If listitem.Index.HierarchicalChildID(j) > 0 Then
                                        If listitem.Index.HierarchicalChildID(j) > 0 Then
                                            'Si tiene hijo para actualizar al disparar el change
                                            'Se instancian las variables para que queden en memoria a la hora de llamar al metodo anonimo
                                            Dim firedElement As HtmlElement = e
                                            Dim firedIndexID As Long = listitem.Index.ID
                                            Dim firedChildIndexID As Long = listitem.Index.HierarchicalChildID(j)
                                            'Se agrega el handler para jerarquicos
                                            e.AttachEventHandler("onchange", Sub() HierarchicalChange(firedElement, firedIndexID, firedChildIndexID))

                                            'HierarchicalChange(firedElement, firedIndexID, firedChildIndexID)
                                        End If
                                    End If
                                Next

                                If Not String.IsNullOrEmpty(listitem.Index.Data) Then
                                    e.DomElement.value = listitem.Index.Data
                                End If
                            End If
                        End If
                    Next
                End If
            End If
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fin carga listas")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            ASListToLoad.Clear()
        End Try
    End Sub

    Private Sub LoadSearchList()
        Try
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Cargo Listas de busqueda" & SearchListToLoad.Count & " a las " & Now.ToString)
            If SearchListToLoad.Count > 0 Then
                Dim doc1 As HtmlDocument ' mshtml.HTMLDocumentClass
                doc1 = WBrowser.Document

                Dim e As HtmlElement = Nothing
                Dim listOptions As List(Of String)
                Dim tableOptions As DataTable
                Dim max As Integer
                Dim prompt As String

                If Not IsNothing(doc1) Then
                    For Each listitem As ListItem In SearchListToLoad

                        e = doc1.GetElementById(listitem.ElementId)
                        If Not e Is Nothing Then
                            'Si es jerarquico
                            If listitem.Index.DropDown = IndexAdditionalType.DropDownJerarquico Then
                                tableOptions = IndexsBussinesExt.GetHierarchicalTableByValue(listitem.Index.ID, listitem.Index.HierarchicalParentID, GetLocalIndexValue(listitem.Index.HierarchicalParentID), True)

                                If Not tableOptions Is Nothing Then
                                    max = tableOptions.Rows.Count
                                    For i As Integer = 0 To max - 1
                                        If IsDBNull(tableOptions.Rows(i)("Value")) Then
                                            prompt = String.Empty
                                        Else
                                            prompt = tableOptions.Rows(i)("Value")
                                        End If

                                        e.AppendChild(GetOptionTag(prompt, prompt, listitem.Index.Data))
                                    Next
                                End If
                            Else
                                listOptions = IndexsBusiness.GetDropDownList(listitem.Index.ID)

                                If Not listOptions Is Nothing Then
                                    max = listOptions.Count
                                    For i As Integer = 0 To max - 1
                                        e.AppendChild(GetOptionTag(listOptions(i), listOptions(i), listitem.Index.Data))
                                    Next
                                End If
                            End If

                            If String.IsNullOrEmpty(listitem.Index.Data) Then
                                e.SetAttribute("value", String.Empty)
                            End If

                            If Not listitem.Index.HierarchicalChildID Is Nothing Then
                                Dim countChild As Integer = listitem.Index.HierarchicalChildID.Count

                                For j As Integer = 0 To countChild - 1
                                    If listitem.Index.HierarchicalChildID(j) > 0 Then
                                        'Si tiene hijo para actualizar al disparar el change
                                        'Se instancian las variables para que queden en memoria a la hora de llamar al metodo anonimo
                                        Dim firedElement As HtmlElement = e
                                        Dim firedIndexID As Long = listitem.Index.ID
                                        Dim firedChildIndexID As Long = listitem.Index.HierarchicalChildID(j)
                                        'Se agrega el handler para jerarquicos
                                        e.AttachEventHandler("onchange", Sub() HierarchicalChange(firedElement, firedIndexID, firedChildIndexID))
                                    End If
                                Next

                                If Not String.IsNullOrEmpty(listitem.Index.Data) Then
                                    e.DomElement.value = listitem.Index.Data
                                End If
                            End If
                        End If
                    Next
                End If
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Fin carga lista")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            SearchListToLoad.Clear()
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene un indice en particular en forma local
    ''' </summary>
    ''' <param name="indexID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLocalIndexValue(ByVal indexID As Long) As String
        If indexID > 0 Then
            Dim max As Integer = CurrentResult.Indexs.Count

            For i As Integer = 0 To max - 1
                If CurrentResult.Indexs(i).ID = indexID Then
                    Return CurrentResult.Indexs(i).Data
                End If
            Next

        End If
        Return String.Empty
    End Function


    ''' <summary>
    ''' Agrega los valores a las listas
    ''' </summary>
    ''' <history>
    '''     Marcelo 28/11/2010  Modified
    '''     Javier  22/10/2010  Modified
    '''</history>
    ''' <remarks></remarks>
    Private Sub LoadAllLists()
        Try
            If (Not WBrowser.Document Is Nothing AndAlso Not WBrowser.Document.ActiveElement Is Nothing AndAlso WBrowser.Document.ActiveElement.Id Is Nothing) OrElse (Not WBrowser.Document Is Nothing AndAlso Not WBrowser.Document.ActiveElement Is Nothing AndAlso Not WBrowser.Document.ActiveElement.TagName Is Nothing AndAlso String.Compare(WBrowser.Document.ActiveElement.TagName, "body", True)) Then
                If isDisposed = False Then
                    WBrowser.SuspendLayout()
                    'Primero cargo las listas, despues lo asociados
                    LoadAutosustitutionLists()
                    LoadSearchList()

                    Dim doc1 As System.Windows.Forms.HtmlDocument ' mshtml.HTMLDocumentClass
                    doc1 = WBrowser.Document

                    Try
                        '[Sebastian 09-06-2009] se agrego condicion para que al momento de cargar los asociados, solo
                        'lo haga si ya se inserto el form virtual.
                        If CurrentResult.ID <> 0 Then

                            Dim Mydoc As HtmlDocument = WBrowser.Document
                            If Not IsNothing(Mydoc.Body.InnerHtml) Then
                                If Mydoc.Body.InnerHtml.ToLower.Contains("zamba_associated_documents") = True OrElse Mydoc.Body.InnerHtml.ToLower.Contains("zamba_asoc") = True Then
                                    '******Obtengo todos los docTypesIDs a ser usados asi solo obtengo esos asociados en vez de todos
                                    Dim blnAll As Boolean
                                    Dim docTypesIds As New List(Of String)
                                    Dim elements As List(Of String)
                                    Dim AsociatedTable As HtmlElement

                                    'Se cargan los asociados marcados como importantes
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_importants"))
                                    AsociatedTable = Mydoc.GetElementById("zamba_associated_documents_importants")
                                    If Not IsNothing(AsociatedTable) Then
                                        Dim ImportantAsociatedResults As DataTable = DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(CurrentResult, Int32.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100)), docTypesIds, True)
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Busca asociado por entidad {0}en la coleccion", CurrentResult))

                                        If Not IsNothing(ImportantAsociatedResults) AndAlso ImportantAsociatedResults.Rows.Count > 0 Then
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Si no encuentra asociados{0}en la coleccion", ImportantAsociatedResults))

                                            If Not IsNothing(AsociatedTable) Then
                                                Dim tags As String = AsociatedTable.Name
                                                LoadTable(AsociatedTable, Mydoc, CurrentResult, ImportantAsociatedResults, False)
                                            End If

                                            elements = getItems("zamba_associated_documents_importants_")

                                            For Each str As String In elements
                                                Dim docTypeID As String = str.Replace("zamba_associated_documents_importants_", String.Empty)
                                                Dim number As Int64

                                                If Int64.TryParse(docTypeID, number) Then
                                                    Dim Table As HtmlElement = Mydoc.GetElementById(str)

                                                    If Not IsNothing(Table) Then
                                                        ImportantAsociatedResults.DefaultView.RowFilter = "doc_type_id=" & number
                                                        LoadTable(Table, Mydoc, CurrentResult, ImportantAsociatedResults.DefaultView.ToTable(), False)
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If


                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents"))
                                    AsociatedTable = Mydoc.GetElementById("zamba_associated_documents")
                                    If Not IsNothing(AsociatedTable) Then
                                        Dim tags As String = AsociatedTable.Name

                                        If String.IsNullOrEmpty(tags) = False AndAlso tags.ToLower().StartsWith("doc_type_ids(") Then
                                            Dim doc_types_ids As String = tags.Replace("doc_type_ids(", String.Empty).Replace(")", String.Empty)
                                            'Se recorren los asociados y se guarda el ID en un listado
                                            For Each docTypeID As String In doc_types_ids.Split(",")
                                                If docTypesIds.Contains(docTypeID) = False Then
                                                    docTypesIds.Add(docTypeID)
                                                End If
                                            Next
                                        Else
                                            blnAll = True
                                        End If
                                    End If

                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_WF"))
                                    AsociatedTable = Mydoc.GetElementById("zamba_associated_documents_WF")
                                    If Not IsNothing(AsociatedTable) Then
                                        blnAll = True
                                    End If

                                    If blnAll = True Then
                                        docTypesIds.Clear()
                                    Else

                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_"))
                                        elements = getItems("zamba_associated_documents_")
                                        For Each str As String In elements
                                            Dim docTypeID As String = str.Replace("zamba_associated_documents_", String.Empty)
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_ con EntityId: {0}", docTypeID))
                                            If docTypesIds.Contains(docTypeID) = False Then
                                                docTypesIds.Add(docTypeID)
                                            End If
                                        Next

                                        If Mydoc.Body.InnerHtml.ToLower.Contains("zamba_asoc") = True Then
                                            Dim lastDocTypeID As String

                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_asoc"))
                                            elements = getItems("zamba_asoc")
                                            For Each Str As String In elements
                                                If Str.Contains("index") Then
                                                    Dim values As String() = Str.ToLower().Replace("zamba_asoc_", String.Empty).Split("_")
                                                    Dim docTypeID As String = values(0)

                                                    If lastDocTypeID <> docTypeID Then
                                                        lastDocTypeID = docTypeID
                                                        If docTypesIds.Contains(docTypeID) = False Then
                                                            docTypesIds.Add(docTypeID)
                                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se agrega ID Entidad {0} en la coleccion", docTypeID))
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If

                                    Dim Asociated As DataTable

                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Busca asociados para la entidad {0}", CurrentResult.Name))

                                    Asociated = DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(CurrentResult, Int32.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100)), docTypesIds, False)

                                    If Not IsNothing(Asociated) AndAlso Asociated.Rows.Count > 0 Then
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se encontraron {0}  documentos asociados", Asociated.Rows.Count))

                                        'Se cargan las tablas que tengan documentos especificos
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents"))
                                        AsociatedTable = Mydoc.GetElementById("zamba_associated_documents")
                                        If Not IsNothing(AsociatedTable) Then
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("TAG con ID: zamba_associated_documents encontrado"))
                                            Dim tags As String = AsociatedTable.Name
                                            'Se cargan las tablas asociadas o de WF
                                            If String.IsNullOrEmpty(tags) = False AndAlso tags.ToLower().StartsWith("doc_type_ids(") Then
                                                Dim doc_types_ids As String = tags.Replace("doc_type_ids(", String.Empty).Replace(")", String.Empty)
                                                Asociated.DefaultView.RowFilter = "doc_type_id in(" & doc_types_ids & ")"
                                                LoadTable(AsociatedTable, Mydoc, CurrentResult, Asociated.DefaultView.ToTable(), False)
                                            Else
                                                LoadTable(AsociatedTable, Mydoc, CurrentResult, Asociated, False)
                                            End If
                                        End If

                                        'Se cargan los documentos asociados del result que estan unicamente en wf
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_WF"))
                                        AsociatedTable = Mydoc.GetElementById("zamba_associated_documents_WF")
                                        If Not IsNothing(AsociatedTable) Then
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("TAG con ID: zamba_associated_documents_WF encontrado"))
                                            LoadTable(AsociatedTable, Mydoc, CurrentResult, Asociated, True)
                                        End If


                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_"))
                                        elements = getItems("zamba_associated_documents_")
                                        For Each str As String In elements
                                            Dim docTypeID As String = str.Replace("zamba_associated_documents_", String.Empty)
                                            Dim number As Int64

                                            If Int64.TryParse(docTypeID, number) Then
                                                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_ con EntityId: {0}", docTypeID))
                                                Dim Table As HtmlElement = Mydoc.GetElementById(str)

                                                If Not IsNothing(Table) Then
                                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Cargando Asociados de la Entidad  {0} ", number))

                                                    Asociated.DefaultView.RowFilter = "doc_type_id=" & number
                                                    LoadTable(Table, Mydoc, CurrentResult, Asociated.DefaultView.ToTable(), False)
                                                End If
                                            End If
                                        Next


                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_asoc"))
                                        If Mydoc.Body.InnerHtml.ToLower.Contains("zamba_asoc") = True Then
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("El documento contiene {0} atributos asociados", Asociated))
                                            Dim res As DataRow
                                            Dim docTypeTable As DataTable
                                            Dim lastDocTypeID As String

                                            Dim list As SortedList = getIndexItems("zamba_asoc")
                                            For Each Str As String In list.Keys
                                                If Str.Contains("index") Then
                                                    Dim values As String() = Str.Replace("zamba_asoc_", String.Empty).Split("_")
                                                    Dim docTypeID As String = values(0)
                                                    Dim indexName As String = values(2)
                                                    Dim indexID As Int64
                                                    If Int64.TryParse(indexName, indexID) = False Then
                                                        indexID = IndexsBusiness.GetIndexIdByName(indexName.Replace("_s", String.Empty).Replace("_n", String.Empty))
                                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("reemplazo zamba_asoc {0} en la coleccion", indexName))

                                                    End If

                                                    'Valida que no se hayan ingresado atributos mal escritos
                                                    If indexID > 0 Then
                                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("valido atributos mal escritos {0} en la coleccion", indexID))
                                                        Dim indice As Index = ZCore.GetIndex(indexID)
                                                        'Verifica que se cargue una sola vez
                                                        If lastDocTypeID <> docTypeID Then
                                                            lastDocTypeID = docTypeID

                                                            'filtro por doc_type_id y cargo los atributos de la entidad
                                                            Asociated.DefaultView.RowFilter = "doc_type_id=" & docTypeID
                                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("filtro por doctypeid {0} en la coleccion", docTypeID))
                                                            docTypeTable = Asociated.DefaultView.ToTable()

                                                            If docTypeTable.Rows.Count > 0 Then
                                                                res = docTypeTable.Rows(0)
                                                            Else
                                                                res = Nothing
                                                            End If
                                                        End If

                                                        If Not IsNothing(res) Then
                                                            If indice.DropDown = IndexAdditionalType.AutoSustitución OrElse indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                                                If docTypeTable.Columns.Contains(indice.Name) AndAlso docTypeTable.Columns.Contains("I" & indice.ID) Then
                                                                    If Not IsDBNull(res("I" & indice.ID)) Then
                                                                        indice.Data = res("I" & indice.ID)
                                                                    End If
                                                                    If Not IsDBNull(res(indice.Name)) Then
                                                                        indice.dataDescription = res(indice.Name)
                                                                    End If
                                                                End If
                                                            Else
                                                                If docTypeTable.Columns.Contains(indice.Name) Then
                                                                    If Not IsDBNull(res(indice.Name)) Then
                                                                        indice.Data = res(indice.Name)
                                                                    End If
                                                                End If
                                                            End If

                                                            Dim Element As HtmlElement = doc1.GetElementById(Str)

                                                            If Not IsNothing(Element) Then
                                                                AsignValue(indice, Element)
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Next

                                        End If
                                    End If
                                End If
                            End If
                        End If
                        'doc1.InvokeScript("ZFUNCTION")

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        'Finally
                        'AxWebBrowser1.ResumeLayout()
                    End Try
                End If
            End If
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As InvalidCastException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Try
                If (Not WBrowser.Document Is Nothing AndAlso
                Not WBrowser.Document.ActiveElement Is Nothing) OrElse
                (Not WBrowser.Document Is Nothing AndAlso
                 Not WBrowser.Document.ActiveElement Is Nothing AndAlso
                 String.Compare(WBrowser.Document.ActiveElement.TagName, "body", True)) Then
                    If isDisposed = False Then
                        WBrowser.ResumeLayout()
                    End If
                End If
            Catch ex As InvalidCastException
            Catch ex As Exception
                '  ZClass.raiseerror(ex)
            End Try
        End Try
    End Sub

    Dim WithEvents WP As System.ComponentModel.BackgroundWorker

    Private Sub WP_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles WP.DoWork
        Try
            If Me IsNot Nothing AndAlso Not isDisposed Then
                LoadAllLists()
            End If
        Catch ex As Threading.SynchronizationLockException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadAbortException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadInterruptedException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadStateException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Dim WebServiceURl(0) As Object


    Private Sub RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Try
            If sender IsNot Nothing Then
                RemoveHandler DirectCast(sender, ComponentModel.BackgroundWorker).DoWork, AddressOf WP_DoWork
                RemoveHandler DirectCast(sender, ComponentModel.BackgroundWorker).RunWorkerCompleted, AddressOf RunWorkerCompleted
                DirectCast(sender, ComponentModel.BackgroundWorker).Dispose()
                sender = Nothing
            End If
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            'Finally
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ZFUNCTION")
            WBrowser.Document.InvokeScript("ZFUNCTION")
        Finally
            If Not IsNothing(WBrowser) AndAlso WBrowser.Disposing = False AndAlso WBrowser.IsDisposed = False Then
                WBrowser.Document.InvokeScript("SetGlobalParams", WebServiceURl)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "ProcessAjaxFunctions")
                WBrowser.Document.InvokeScript("ProcessAjaxFunctions")
            End If
        End Try
    End Sub


    ''' <summary>
    ''' Obtiene el id de la regla desde el tag html
    ''' </summary>
    ''' <param name="tagId">Tag Id del html</param>
    ''' <returns>Id de regla</returns>
    ''' <remarks></remarks>
    Public Function GetRuleIdFromTag(ByVal tagId As String) As Long
        tagId = tagId.Replace("zamba_rule_", String.Empty)
        If tagId.Contains("/") Then
            tagId = tagId.Substring(0, tagId.IndexOf("/"))
        End If
        If IsNumeric(tagId) Then
            Return tagId
        Else
            Return Nothing
        End If
    End Function

    Private Sub LogBadTag(ByVal tag As String, ByVal formName As String)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "El formulario contiene tags que no se han podido resolver de manera correcta.")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Debe corregir el tag del formulario para obtener un correcto funcionamiento.")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "El tag erroneo es: " & tag)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "El formulario afectado es: " & formName)
    End Sub

    Private Sub LoadBarcode(barcodeInBase64 As String, element As HtmlElement)
        element.SetAttribute("src", "data:image/png;base64," + barcodeInBase64)
    End Sub

    Private ASListToLoad As New Generic.List(Of ListItem)
    Private SearchListToLoad As New Generic.List(Of ListItem)
    Private IsDynamicForm As Boolean = False

    ''' <summary>
    ''' Cargo el valor del indice al elemento html
    ''' </summary>
    ''' <param name="I"></param>
    ''' <param name="E"></param>
    ''' <history> Marcelo 31/07/08 Modified</history>
    '''     [Tomas] 21/09/2009  Modified    Se agrego una validación cuando carga los valores 
    '''                                     de los textbox y textareas para que si el campo Data 
    '''                                     del indice se encuentra vacio y el text se encuentre 
    '''                                     con datos (caso del estar completando datos por primera  
    '''                                     vez) estos datos no se pierdan.
    ''' <remarks></remarks>
    Private Sub AsignValue(ByVal I As Index, ByVal E As HtmlElement)
        Try

            Try
                If I.AutoIncremental = True Then
                    E.SetAttribute("ReadOnly", "True")
                    E.SetAttribute("Value", I.Data)
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            'Se limita la cantidad de caracteres a ingresar en base al indice
            E.SetAttribute("maxlength", I.Len)

            ' Filtra por tipo de control...
            Select Case E.TagName.ToLower
                Case "input" ', "SELECT"
                    Select Case CStr(E.DomElement.type).ToLower
                        'Select Case CStr(DirectCast(E.DomElement, mshtml.HTMLInputElementClass).type).ToLower
                        Case "text", "hidden"



                            If I.DropDown = IndexAdditionalType.AutoSustitución OrElse I.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                If I.Data = Nothing Then
                                    'Verifica si en el objeto existe algún valor o no
                                    If IsNothing(E.DomElement.value) Then
                                        E.DomElement.value = String.Empty
                                        'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = String.Empty
                                    End If
                                Else
                                    E.DomElement.value = I.Data & " - " & AutoSubstitutionBusiness.getDescription(I.Data, I.ID, False, I.Type)
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = I.Data & " - " & AutoSubstitutionBusiness.getDescription(I.Data, I.ID)
                                End If

                            Else

                                If I.Data = Nothing Then
                                    'Verifica si en el objeto existe algún valor o no
                                    E.DomElement.value = String.Empty
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = String.Empty
                                Else
                                    E.DomElement.value = I.Data
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = I.Data
                                End If

                                If I.Type = IndexDataType.Fecha Then
                                    DateIndexsList.Add(E.Id)
                                ElseIf I.Type = IndexDataType.Fecha_Hora Then
                                    DateTimeIndexsList.Add(E.Id)
                                End If

                            End If


                            WriteDataIndexTrace(I, True)

                        Case "file"


                            If Not E.DomElement Is Nothing AndAlso Not E.DomElement.value Is Nothing Then
                                ' ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando Valor de Archivo Adjunto a la tarea:       " & E.DomElement.value.ToString())
                                SetDocumentFile(E.DomElement.value)
                            End If

                        Case "checkbox"


                            If IsNothing(I.Data) OrElse I.Data = "0" OrElse I.Data = String.Empty Then
                                E.DomElement.checked = 0
                            Else
                                E.DomElement.checked = 1
                            End If


                            WriteDataIndexTrace(I, True)

                        Case "radio"


                            If IsNothing(I.Data) Then
                                If E.Id.ToUpper().EndsWith("S") Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                ElseIf E.Id.ToUpper().EndsWith("N") = False Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                '   ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)

                            ElseIf I.Data = "0" Then

                                '     ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es nothing, es 0, o empty, Valor del checked N= " & E.Id & " " & E.DomElement.checked.ToString)
                                If E.Id.ToUpper().EndsWith("N") Then
                                    E.DomElement.checked = True
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = True
                                ElseIf E.Id.ToUpper().EndsWith("S") = True Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                '     ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked N= " & E.Id & " " & E.DomElement.checked.ToString)

                            ElseIf I.Data = "1" Then
                                '     ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es 1. Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)
                                If E.Id.ToUpper().EndsWith("S") Then
                                    E.DomElement.checked = True
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = True
                                ElseIf E.Id.ToUpper().EndsWith("N") = False Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                '      ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)

                            Else
                                '   ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es otra cosa. Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)
                                If E.Id.ToUpper().EndsWith("S") Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                ElseIf E.Id.ToUpper().EndsWith("N") = False Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)

                            End If
                            'If ZTrace.IsVerbose Then
                            '    Try
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Id " & E.Id & ". Value " & E.DomElement.checked.ToString)
                            '    Catch ex As Exception
                            '        Zamba.Core.ZClass.raiseerror(ex)
                            '    End Try
                            'End If
                            WriteDataIndexTrace(I, False)

                        Case "select-one"

                            ' WriteDataIndexTrace(I, False)

                            If IsNothing(I.Data) Then
                                E.DomElement.value = I.Data
                                'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = I.Data
                            Else
                                'Verifica si en el objeto existe algún valor o no
                                If IsNothing(E.DomElement.value) Then
                                    E.DomElement.value = String.Empty
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = String.Empty
                                End If
                            End If
                            'If ZTrace.IsVerbose Then
                            '    Try
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Id " & E.Id & " " & E.DomElement.value.ToString)
                            '    Catch ex As Exception
                            '        Zamba.Core.ZClass.raiseerror(ex)
                            '    End Try
                            'End If
                            WriteDataIndexTrace(I, False)
                    End Select
                Case "select"
                    'E.InnerHtml = ""
                    If E.Children.Count = 0 OrElse IsDynamicForm Then
                        ' WriteDataIndexTrace(I, False)

                        If IsNothing(I.Data) = False OrElse I.Data <> "0" OrElse I.Data <> String.Empty Then
                            Select Case I.DropDown
                                Case 1
                                    'Andres 8/8/07 - Se guarda el valor pero no se usa 
                                    'Dim Lista As ArrayList
                                    'If Reload = False Then
                                    'Lista = Indexs_Factory.retrieveArraylist(indice.ID)

                                    'Si no esta cargada, cargo solo el item seleccionado
                                    Dim readonli As String = E.GetAttribute("ReadOnly")

                                    If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                                        Dim tag As HtmlElement = E.Document.CreateElement("option")

                                        tag.SetAttribute("selected", I.Data)
                                        tag.SetAttribute("value", I.Data)
                                        tag.InnerText = I.Data
                                        E.AppendChild(tag)
                                    Else
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se agrega Elemento para carga de Lista", E.Id))
                                        Dim ListItem As New ListItem(I, E.Id)
                                        SearchListToLoad.Add(ListItem)
                                    End If
                                    'End If
                                Case 2
                                    'If Reload = False Then
                                    'Si no esta cargada, cargo solo el item seleccionado
                                    Dim readonli As String = E.GetAttribute("ReadOnly")

                                    If E.Enabled = False OrElse readonli.ToUpper() = "TRUE" Then
                                        Dim tag As HtmlElement = E.Document.CreateElement("option")

                                        Dim desc As String = AutoSubstitutionBusiness.getDescription(I.Data, I.ID, False, I.Type)
                                        tag.SetAttribute("selected", I.Data)
                                        tag.SetAttribute("value", I.Data)
                                        tag.InnerText = I.Data & " - " & desc
                                        E.AppendChild(tag)
                                    Else
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se agrega Elemento para carga de Lista", E.Id))
                                        Dim ListItem As New ListItem(I, E.Id)
                                        ASListToLoad.Add(ListItem)
                                    End If
                                    'End If
                                    If Not E.Children(I.Data) Is Nothing Then E.Children(I.Data).SetAttribute("selected", True)

                                Case IndexAdditionalType.AutoSustituciónJerarquico
                                    Dim readonli As String = E.GetAttribute("ReadOnly")

                                    If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                                        Dim tag As HtmlElement = E.Document.CreateElement("option")

                                        Dim desc As String = AutoSubstitutionBusiness.getDescription(I.Data, I.ID, False, I.Type)
                                        tag.SetAttribute("selected", I.Data)
                                        tag.SetAttribute("value", I.Data)
                                        tag.InnerText = I.Data & " - " & desc
                                        E.AppendChild(tag)
                                    Else
                                        Dim ListItem As New ListItem(I, E.Id)
                                        ASListToLoad.Add(ListItem)
                                    End If
                                    'End If
                                    If Not E.Children(I.Data) Is Nothing Then E.Children(I.Data).SetAttribute("selected", True)
                                Case IndexAdditionalType.DropDownJerarquico
                                    Dim readonli As String = E.GetAttribute("ReadOnly")

                                    If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                                        Dim tag As HtmlElement = E.Document.CreateElement("option")

                                        tag.SetAttribute("selected", I.Data)
                                        tag.SetAttribute("value", I.Data)
                                        tag.InnerText = I.Data
                                        E.AppendChild(tag)

                                    Else

                                        Dim ListItem As New ListItem(I, E.Id)
                                        SearchListToLoad.Add(ListItem)
                                    End If
                            End Select
                        End If

                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Id " & E.Id)
                        '        If Not IsNothing(E.DomElement) AndAlso Not IsNothing(E.DomElement.value) Then
                        '            ZTrace.WriteLineIf(ZTrace.IsInfo, "Value " & E.DomElement.value.ToString)
                        '        End If
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If
                        WriteDataIndexTrace(I, False)
                    End If

                Case "textarea"

                    ' WriteDataIndexTrace(I, False)

                    If Not IsNothing(I.Data) Then
                        E.SetAttribute("value", I.Data)
                    Else
                        'Verifica si en el objeto existe algún valor o no
                        If IsNothing(E.DomElement.value) Then
                            E.SetAttribute("value", String.Empty)
                        End If
                    End If

                    WriteDataIndexTrace(I, False)
            End Select
        Catch ex As System.Runtime.InteropServices.COMException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub SetDocumentFile(ByVal FileName As String)
        If Not CurrentResult Is Nothing Then
            CurrentResult.File = FileName
        End If
    End Sub

    Private Class ListItem
        Implements IDisposable

        Public Index As Index
        Public ElementId As String

        Public Sub New(ByVal Index As Index, ByVal ElementId As String)
            Me.Index = Index
            Me.ElementId = ElementId
        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            ElementId = Nothing
            Index = Nothing
        End Sub
    End Class

    ''' <summary>
    ''' Escribe el trace del valor Data de un indice
    ''' </summary>
    ''' <param name="I"></param>
    ''' <param name="withDataTemp">Especifica si se desea escribir el trace de la propiedad DataTemp</param>
    ''' <history>
    '''     [Tomas] 21/09/2009  Created
    ''' </history>
    ''' <remarks>Se crea simplemente mejorar la lectura del método AsignValue y su mantenimiento</remarks>
    Private Sub WriteDataIndexTrace(ByVal I As Index, ByVal withDataTemp As Boolean)
        Try

            If withDataTemp Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Atributo en DATA = " & I.Data & " - Valor Atributo en DATATEMP = " & I.DataTemp)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Atributo en DATA = " & I.Data)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub LoadImage(ByRef myResult As Result, ByVal E As HtmlElement)
        Try
            If myResult.ISVIRTUAL = False AndAlso myResult.FullPath <> String.Empty AndAlso Not IsNothing(E) Then
                Select Case E.TagName
                    Case "IMG"
                        E.DomElement.src = myResult.FullPath
                End Select
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Obtiene todos los atributos ordenados por doc_type
    ''' </summary>
    ''' <param name="elementName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getIndexItems(ByVal elementName As String) As SortedList
        Dim elements As New SortedList
        Dim Mydoc As HtmlDocument = WBrowser.Document

        If Not IsNothing(Mydoc) AndAlso Not IsNothing(Mydoc.Body) Then
            If Not String.IsNullOrEmpty(Mydoc.Body.InnerHtml) Then
                Dim body As String = Mydoc.Body.InnerHtml.ToLower()

                While body.Contains(elementName)
                    Dim index As Int32 = body.IndexOf(elementName)
                    Dim elem As String = body.Substring(index)
                    elem = elem.Substring(0, elem.IndexOf(" ")).Replace(Chr(34), String.Empty)

                    If elem.Contains(">") Then
                        elem = elem.Substring(0, elem.IndexOf(">"))
                    End If
                    elements.Add(elem, elem)

                    body = body.Substring(index)
                    body = body.Replace(elem, String.Empty)
                End While
            End If
        End If
        Return elements
    End Function


    ''' <summary>
    '''Carga el contenido de un DataTabla en una tabla Html del documento
    ''' </summary>
    ''' <param name="tableId"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     Javier  22/10/2010  Modified    Se modifica llamada a ParseResult. Se le pasa el padre
    '''</history>
    Private Sub LoadTable(ByVal table As HtmlElement, ByRef mydoc As HtmlDocument, ByVal ParentResult As IResult, ByVal AsociatedResults As DataTable, ByVal onlyWF As Boolean)
        Try
            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Cargando en la Tabla {0} asociados", AsociatedResults.Rows.Count.ToString))
            If Not IsNothing(table) AndAlso AsociatedResults.Rows.Count > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Cargando en la Tabla con ID: ", If(Not IsNothing(table.Id), table.Id, String.Empty)))
                'Valido si existe el nodo TBODY , para usarlo para las rows 
                If table.Children.Count > 0 Then
                    For Each child As HtmlElement In table.Children
                        If String.Compare(child.TagName.ToLower(), "tbody") = 0 Then
                            table = child
                            Exit For
                        End If
                    Next
                End If

                If (Not IsNothing(table)) Then
                    If Not IsNothing(table.InnerHtml) Then
                        table.InnerText = String.Empty
                    End If

                    Dim dt As DataTable = ParseResult(ParentResult, AsociatedResults, If(Not IsNothing(table.Id), table.Id, String.Empty), onlyWF)
                    'quitar columnas del table segun permisos.
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Aplicando a la Tabla {0} Results ", dt.Rows.Count.ToString))

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Aplicando a la Tabla los permisos de Grilla asociados para EntidadPadre: {0} y Entidad Hija {1}", ParentResult.DocTypeId, AsociatedResults.Rows(0).Item("doc_type_id")))
                    dt = SetColumnsByRights(dt, ParentResult.DocTypeId, AsociatedResults.Rows(0).Item("doc_type_id"))
                    LoadTableHeader(table, dt.Columns, mydoc)
                    LoadTableBody(table, dt.Rows, mydoc)
                End If
            Else
                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("La Tabla a Cargar esta en Nothing o no Hay Asociados para Cargar."))
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Carga el contenido de un DataTabla en el body de una tabla Html del documento
    ''' </summary>
    ''' <param name="table">Tabla HTML donde se cargaran los datos</param>
    ''' <param name="drs">Rows que van a ser cargadas</param>
    ''' <param name="mydoc">Documento HTML que contiene la tabla</param>
    ''' <remarks></remarks>
    Private Sub LoadTableBody(ByRef table As HtmlElement, ByVal drs As DataRowCollection, ByRef mydoc As HtmlDocument)
        Dim CurrentRow As HtmlElement = Nothing
        Dim CurrentCell As HtmlElement = Nothing
        Dim tBody As HtmlElement = mydoc.CreateElement("tbody")

        Dim i As Int32

        For Each dr As DataRow In drs
            CurrentRow = mydoc.CreateElement("tr")
            i = 0
            'Dim cont As Int32 = 0
            For Each CellValue As Object In dr.ItemArray

                CurrentCell = mydoc.CreateElement("td")
                '(pablo) 01-03-2011
                If CellValue.GetType.FullName.ToString = "System.DateTime" Then
                    If Not IsDBNull(CellValue) Then
                        Dim dateValue As String
                        dateValue = CellValue
                        If dateValue.Length >= 10 Then
                            Try
                                CurrentCell.InnerHtml = dateValue.ToString().Substring(0, 10)
                            Catch ex As Exception
                                CurrentCell.InnerHtml = ""
                            End Try
                        End If
                    End If
                Else
                    CurrentCell.InnerHtml = CellValue.ToString()
                End If

                CurrentRow.AppendChild(CurrentCell)

                i = i + 1
            Next
            tBody.AppendChild(CurrentRow)
            table.AppendChild(tBody)
        Next
    End Sub

    Private Function SetColumnsByRights(dt As DataTable, parentResultID As Long, asociatedResultID As Long) As DataTable
        Try
            Dim dtColumnsRights As List(Of String) = RightsBusiness.GetAssociatedGridColumnsRightsCombined(Membership.MembershipHelper.CurrentUser.ID, asociatedResultID, parentResultID, True)

            Dim tempDataTable As DataTable = dt.Copy()

            For Each column As DataColumn In dt.Columns
                Dim isIndex As Boolean = False

                If Not IsDBNull(IndexsBusiness.GetIndexIdByName(column.ColumnName)) AndAlso IndexsBusiness.GetIndexIdByName(column.ColumnName) > 0 Then
                    isIndex = True
                End If

                If Not isIndex AndAlso Not column.ColumnName.Equals("ver", StringComparison.CurrentCultureIgnoreCase) Then
                    If Not dtColumnsRights.Contains(column.ColumnName) Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Ocultando Columna {0}.", column.ColumnName))
                        tempDataTable.Columns.Remove(column.ColumnName)
                    End If
                End If

            Next
            Return tempDataTable
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return dt
        End Try
    End Function


    ''' <summary>
    ''' Convierte el contenido de un listado de results en un Datatable
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    19/11/2008     Modified     Se agrego la columna "Estado" y verificación del documento asociado para ver si es una tarea
    '''     [Gaston]    20/11/2008     Modified     Se agrego la columna "Usuario Asignado" 
    '''     [Gaston]    05/01/2009     Modified     Verificación de la columna "Nombre del Documento" en el UserPreferences para mostrar o ocultar 
    '''                                             dicha columna
    '''     [Gaston]    06/01/2009     Modified     Validación del valor de "Nombre del Documento" y código comentado en donde se intenta colocar
    '''                                             un String.Empty en una columna que no existe
    '''     Marcelo     05/02/2009     Modified     Se modifico la carga de los atributos para mejorar la performance 
    '''     Marcelo     06/01/2010     Modified     Se agrego variable para cargar solo las tareas que esten en WF
    '''     Javier      22/10/2010     Modified     Se agrega funcionalidad para filtrar permisos por asociados
    ''' </history>
    Public Shared Function ParseResult(ByVal ParentResult As IResult, ByVal results As DataTable, ByVal tableId As String, ByVal OnlyWF As Boolean) As DataTable
        Dim Dt As New DataTable()
        Dt.Columns.Add(New DataColumn(GridColumns.VER_COLUMNNAME))

        If String.IsNullOrEmpty(tableId) = False AndAlso tableId.Contains("§") Then
            For Each btn As String In tableId.Split("§")
                Dim items As Array = btn.Split("/")
                Dt.Columns.Add(items(1).ToString())
            Next
        End If

        Dt.Columns.Add(New DataColumn(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME))
        Dt.Columns.Add(New DataColumn(GridColumns.STATE_COLUMNNAME))
        Dt.Columns.Add(New DataColumn(GridColumns.USER_ASIGNEDNAME_COLUMNNAME))

        Try
            Dim CurrentIndexType As Type
            'Cargo todos los atributos de todos los results , como pueden ser diferentes tipos de documento recorro todos
            'Solo visualizo en la tabla los atributos sobre los cuales tiene permiso el documento. Mariela
            Dim lastDocTypeId As Int64 = 0
            Dim AIR As Hashtable = Nothing
            Dim tilde As Boolean = RightsBusiness.GetSpecificAttributeRight(Membership.MembershipHelper.CurrentUser, ParentResult.DocTypeId)

            For Each CurrentResult As DataRow In results.Rows
                If OnlyWF = False OrElse (OnlyWF = True AndAlso CurrentResult.Table.Columns.Contains(GridColumns.TASK_ID_COLUMNNAME) AndAlso Not IsDBNull(CurrentResult(GridColumns.TASK_ID_COLUMNNAME)) AndAlso Not String.IsNullOrEmpty(CurrentResult(GridColumns.TASK_ID_COLUMNNAME))) Then
                    'Se obtiene los permisos para el doctype, doctypeasociado y usuario
                    If lastDocTypeId <> CurrentResult("doc_type_ID") Then
                        lastDocTypeId = CurrentResult("doc_type_ID")

                        If tilde Then
                            AIR = RightsBusiness.GetAssociatedIndexsRightsCombined(ParentResult.DocTypeId, CurrentResult("doc_type_ID"), Membership.MembershipHelper.CurrentUser.ID, True)
                        End If
                    End If


                    For Each CurrentIndex As IIndex In ZCore.FilterIndex(CurrentResult("doc_type_ID"))
                        Dim ShowIndex As Boolean = False
                        Dim IR As AssociatedIndexsRightsInfo
                        If tilde Then
                            IR = DirectCast(AIR(CurrentIndex.ID), AssociatedIndexsRightsInfo)
                        End If

                        If tilde = False OrElse IR.GetIndexRightValue(RightsType.AssociateIndexView) Then
                            If Not Dt.Columns.Contains(CurrentIndex.Name) Then
                                CurrentIndexType = GetType(String)

                                If Not IsNothing(CurrentIndex.Type) Then
                                    If CurrentIndex.DropDown = IndexAdditionalType.LineText Then
                                        CurrentIndexType = GetIndexType(CurrentIndex.Type)
                                    Else
                                        CurrentIndexType = GetType(String)
                                    End If
                                End If

                                Dt.Columns.Add(CurrentIndex.Name.Trim(), CurrentIndexType)
                            End If
                        End If
                    Next
                End If

            Next

            Dt.Columns.Add(New DataColumn(GridColumns.CRDATE_COLUMNNAME))
            Dt.Columns.Add(New DataColumn(GridColumns.DOC_TYPE_NAME_COLUMNNAME))
            Dt.Columns.Add(New DataColumn(GridColumns.LASTUPDATE_COLUMNNAME))

            If Boolean.Parse(UserPreferences.getValue("NombreOriginal", UPSections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn(GridColumns.ORIGINAL_FILENAME_COLUMNNAME))
            End If
            If Boolean.Parse(UserPreferences.getValue("NumerodeVersion", UPSections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn(GridColumns.NUMERO_DE_VERSION_COLUMNNAME))
            End If
            If Boolean.Parse(UserPreferences.getValue("ParentId", UPSections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn(GridColumns.VER_PARENT_ID_COLUMNNAME))
            End If

            Dt.Columns.Add(New DataColumn("Ruta Documento"))
            Dt.Columns.Add(New DataColumn(GridColumns.DOCTYPEID_COLUMNNAME))
            Dt.Columns.Add(New DataColumn(GridColumns.DOC_ID_COLUMNNAME, System.Type.GetType("System.Int64")))
            Dt.AcceptChanges()

            Dim CurrentRow As DataRow = Nothing

            For Each CurrentResult As DataRow In results.Rows
                ' Se verifica si el documento es una tarea
                If OnlyWF = False OrElse (OnlyWF = True AndAlso CurrentResult.Table.Columns.Contains(GridColumns.TASK_ID_COLUMNNAME) AndAlso Not IsDBNull(CurrentResult(GridColumns.TASK_ID_COLUMNNAME)) AndAlso Not String.IsNullOrEmpty(CurrentResult(GridColumns.TASK_ID_COLUMNNAME))) Then

                    CurrentRow = Dt.NewRow()

                    CurrentRow.Item(GridColumns.DOC_ID_COLUMNNAME) = CurrentResult("DOC_ID")

                    If CurrentRow.Table.Columns.Contains("Ruta Documento") AndAlso CurrentRow.Table.Columns.Contains("DISK_VOL_PATH") AndAlso CurrentRow.Table.Columns.Contains("OFFSET") AndAlso CurrentRow.Table.Columns.Contains("DOC_FILE") Then
                        CurrentRow.Item("Ruta Documento") = CurrentResult("DISK_VOL_PATH") & "\" & CurrentResult("DOC_TYPE_ID") & "\" & CurrentResult("OFFSET") & "\" & CurrentResult("DOC_FILE")
                    End If

                    CurrentRow.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME) = CurrentResult(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME)
                    CurrentRow.Item(GridColumns.CRDATE_COLUMNNAME) = CurrentResult(GridColumns.CRDATE_COLUMNNAME)
                    CurrentRow.Item(GridColumns.DOC_TYPE_NAME_COLUMNNAME) = DocTypesBusiness.GetDocTypeName(CurrentResult("doc_type_ID"), True)
                    CurrentRow.Item(GridColumns.LASTUPDATE_COLUMNNAME) = CurrentResult(GridColumns.LASTUPDATE_COLUMNNAME)

                    If CurrentRow.Table.Columns.Contains(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) AndAlso CurrentResult.Table.Columns.Contains(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) Then
                        If Boolean.Parse(UserPreferences.getValue("NumerodeVersion", UPSections.FormPreferences, "True")) = True Then
                            CurrentRow.Item(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) = CurrentResult(GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
                        End If
                    End If

                    If CurrentRow.Table.Columns.Contains(GridColumns.VER_PARENT_ID_COLUMNNAME) AndAlso CurrentResult.Table.Columns.Contains(GridColumns.VER_PARENT_ID_COLUMNNAME) Then
                        If Boolean.Parse(UserPreferences.getValue("ParentId", UPSections.FormPreferences, "True")) = True Then
                            CurrentRow.Item(GridColumns.VER_PARENT_ID_COLUMNNAME) = CurrentResult(GridColumns.VER_PARENT_ID_COLUMNNAME)
                        End If
                    End If

                    CurrentRow.Item(GridColumns.DOCTYPEID_COLUMNNAME) = CurrentResult("doc_type_ID")


                    Dim IndexType As Type = GetType(String)
                    For Each CurrentIndex As IIndex In ZCore.FilterCIndex(CurrentResult("doc_type_ID"))
                        If Not IsNothing(CurrentIndex.Type) Then
                            IndexType = GetIndexType(CurrentIndex.Type)
                        Else
                            IndexType = GetType(String)
                        End If
                        Try
                            If CurrentRow.Table.Columns.Contains(CurrentIndex.Name) AndAlso CurrentResult.Table.Columns.Contains(CurrentIndex.Name) Then 'Si Data tiene un valor que se le asigne al Item
                                CurrentRow.Item(CurrentIndex.Name) = CurrentResult(CurrentIndex.Name)
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next

                    If CurrentRow.Table.Columns.Contains(GridColumns.STATE_COLUMNNAME) AndAlso CurrentResult.Table.Columns.Contains(GridColumns.STATE_COLUMNNAME) Then
                        CurrentRow.Item(GridColumns.STATE_COLUMNNAME) = CurrentResult(GridColumns.STATE_COLUMNNAME)
                    End If


                    If CurrentRow.Table.Columns.Contains(GridColumns.USER_ASIGNEDNAME_COLUMNNAME) AndAlso CurrentResult.Table.Columns.Contains(GridColumns.USER_ASIGNEDNAME_COLUMNNAME) Then
                        CurrentRow.Item(GridColumns.USER_ASIGNEDNAME_COLUMNNAME) = CurrentResult(GridColumns.USER_ASIGNEDNAME_COLUMNNAME)
                    End If



                    If CurrentRow.Table.Columns.Contains(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) AndAlso CurrentResult.Table.Columns.Contains(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) Then
                        'Nombre del documento
                        If Boolean.Parse(UserPreferences.getValue("NombreOriginal", UPSections.FormPreferences, "True")) = True Then
                            Dim FileName As String = CurrentResult(GridColumns.ORIGINAL_FILENAME_COLUMNNAME)
                            If FileName Is Nothing Then FileName = CurrentResult("Doc_File")

                            Dim indexpath As Int32 = FileName.LastIndexOf("\")
                            If indexpath = -1 OrElse FileName.Length - 1 = -1 Then
                            Else
                                If indexpath = -1 Then indexpath = 0
                                Try
                                    FileName = FileName.Substring(indexpath + 1, FileName.Length - indexpath - 1)
                                Catch ex As Exception
                                    FileName = CurrentResult(GridColumns.ORIGINAL_FILENAME_COLUMNNAME)
                                End Try
                            End If
                            CurrentRow.Item(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) = FileName
                        End If
                    End If


                    Dim InnerHtml As StringBuilder = New StringBuilder()
                    Dim htmlName As String = "zamba_asoc_" & CurrentResult("DOC_ID") & "_" & CurrentResult("doc_type_ID")

                    InnerHtml.Append("<INPUT id=")
                    InnerHtml.Append(htmlName)
                    InnerHtml.Append(" type=button onclick=SetAsocId(this); value=")
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append("Ver")
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append("Name = ")
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append(htmlName)
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append(" >")

                    CurrentRow("Ver") = InnerHtml.ToString()

                    If String.IsNullOrEmpty(tableId) = False Then
                        Dim textItem2, textAux As String

                        If tableId.Contains("§") Then
                            For Each btn As String In tableId.Split("§")
                                InnerHtml.Remove(0, InnerHtml.Length)
                                Dim items As Array = btn.Split("/")
                                Dim itemNum As Int32
                                Dim zvarItems As Array
                                Dim params As String

                                InnerHtml.Append("&nbsp;<INPUT id=")
                                InnerHtml.Append(Chr(34))

                                'Si tiene zvar
                                If items.Length > 2 Then
                                    textItem2 = items(2).ToString()
                                    InnerHtml.Append(items(0) + "_")

                                    While String.IsNullOrEmpty(textItem2) = False
                                        textAux = textItem2.Remove(0, 5)
                                        zvarItems = textAux.Remove(textAux.IndexOf(")")).Split("=")
                                        textItem2 = textItem2.Remove(0, textItem2.IndexOf(")") + 1)

                                        If Int32.TryParse(zvarItems(1).ToString(), itemNum) = False Then
                                            If zvarItems(1).ToString().ToLower().Contains("length") Then
                                                itemNum = Dt.Columns.Count - Int32.Parse(zvarItems(1).ToString().Split("-")(1))
                                            End If
                                        End If
                                        InnerHtml.Append("zvar(" + zvarItems(0).ToString() + "=" + CurrentRow.Item(itemNum).ToString() + ")")

                                        params = params & "'" & CurrentRow.ItemArray(itemNum).ToString() & "',"
                                    End While
                                Else
                                    InnerHtml.Append(items(0))
                                End If

                                InnerHtml.Append(Chr(34))
                                InnerHtml.Append(" type=button onclick=")

                                'Si hay un cuarto parametro es el nombre de la funcion JS que hay que llamar,
                                'sino se llama a SetRuleId por default
                                If items.Length > 3 Then
                                    InnerHtml.Append(Chr(34))
                                    InnerHtml.Append(items(3) & "(this, ")
                                    InnerHtml.Append(params.Substring(0, params.Length - 1).Replace("\", "\\"))
                                    InnerHtml.Append(");")
                                    InnerHtml.Append(Chr(34))
                                Else
                                    InnerHtml.Append(Chr(34))
                                    InnerHtml.Append("SetRuleId(this);")
                                    InnerHtml.Append(Chr(34))
                                End If

                                InnerHtml.Append(" value = ")
                                InnerHtml.Append(Chr(34))
                                InnerHtml.Append(items(1))
                                InnerHtml.Append(Chr(34))
                                InnerHtml.Append(" Name = ")
                                InnerHtml.Append(Chr(34))
                                InnerHtml.Append(items(0))
                                InnerHtml.Append(Chr(34))
                                InnerHtml.Append(" >")

                                CurrentRow.Item(items(1)) = InnerHtml.ToString()
                                params = String.Empty
                            Next
                        End If
                    End If

                    InnerHtml = Nothing
                    Dt.Rows.Add(CurrentRow)
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Dt.AcceptChanges()
        Dt.DefaultView.Sort = GridColumns.DOC_ID_COLUMNNAME & " DESC"

        Return Dt.DefaultView.ToTable()
    End Function


    ''' <summary>
    ''' Castea el tipo de un Atributo a Type
    ''' </summary>
    ''' <param name="indexType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetIndexType(ByVal indexType As IndexDataType) As Type
        Dim ParsedIndexType As Type

        Select Case indexType

            Case IndexDataType.Alfanumerico
                ParsedIndexType = GetType(String)
            Case IndexDataType.Alfanumerico_Largo
                ParsedIndexType = GetType(String)
            Case IndexDataType.Fecha
                ParsedIndexType = GetType(Date)
            Case IndexDataType.Fecha_Hora
                ParsedIndexType = GetType(DateTime)
            Case IndexDataType.Moneda
                ParsedIndexType = GetType(Decimal)
            Case IndexDataType.None
                ParsedIndexType = GetType(String)
            Case IndexDataType.Numerico
                ParsedIndexType = GetType(Int64)
            Case IndexDataType.Numerico_Decimales
                ParsedIndexType = GetType(Decimal)
            Case IndexDataType.Numerico_Largo
                ParsedIndexType = GetType(Decimal)
            Case IndexDataType.Si_No
                ParsedIndexType = GetType(String)
            Case Else
                ParsedIndexType = GetType(String)
        End Select

        Return ParsedIndexType
    End Function

    Private isDisposed As Boolean
    Public Sub Dispose() Implements IDisposable.Dispose
        WBrowser.Dispose()
    End Sub

    Protected Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            Try
                If disposing Then
                    If WBrowser IsNot Nothing Then WBrowser.Dispose()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
            End Try

            isDisposed = True
        End If
    End Sub
    Protected Overrides Sub Finalize()
        Try
            If Not IsNothing(WBrowser) Then
                WBrowser.Dispose()
                WBrowser = Nothing
            End If
        Catch
        End Try
        Dispose(False)
        MyBase.Finalize()
    End Sub



#End Region

End Class
