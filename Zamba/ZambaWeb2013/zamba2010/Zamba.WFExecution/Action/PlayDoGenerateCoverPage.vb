Imports System.Drawing.Printing
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading

Public Class PlayDoGenerateCoverPage

    Public Function Play(ByVal results As List(Of ITaskResult), ByVal myRule As IDoGenerateCoverPage) As List(Of ITaskResult)
        Try
            Dim ZC As New ZCore()
            Dim RB As New Results_Business()
            myRule = myRule
            Dim TemplatePath As String
            For Each CurrentResult As TaskResult In results

                CurrentOriginalResult = CurrentResult

                Dim copies As Int16
                If IsNumeric(myRule.CopiesCount) Then
                    copies = myRule.CopiesCount
                Else
                    Dim tmpCopies As String
                    tmpCopies = TextoInteligente.ReconocerCodigo(myRule.CopiesCount.ToString(), CurrentResult)
                    tmpCopies = WFRuleParent.ReconocerVariablesValuesSoloTexto(myRule.CopiesCount.ToString())
                    If IsNumeric(tmpCopies) Then
                        copies = tmpCopies
                    End If
                End If
                'Si fallo todo ponele 1
                If copies <= 0 Then
                    copies = 1
                End If

                Dim replicates As Int16
                If Not myRule.Copies.Equals(String.Empty) Then
                    If IsNumeric(myRule.Copies) Then
                        replicates = myRule.Copies
                    Else
                        Dim tmpReplicates As String
                        tmpReplicates = Int16.Parse(TextoInteligente.ReconocerCodigo(myRule.Copies.ToString(), CurrentResult))
                        tmpReplicates = Int16.Parse(WFRuleParent.ReconocerVariablesValuesSoloTexto(myRule.Copies.ToString()))

                        If IsNumeric(tmpReplicates) Then
                            replicates = tmpReplicates
                        End If
                    End If
                End If
                'Si fallo todo ponele 0
                If replicates < 0 Then
                    replicates = 0
                End If


                If Not myRule.UseCurrentTask Then
                    NewResult = RB.GetNewNewResult(myRule.DocTypeId)
                    NewResult.Parent = ZC.FilterDocTypes(myRule.DocTypeId)

                    For Each I As Index In NewResult.Indexs
                        For Each RI As Index In CurrentResult.Indexs
                            If I.ID = RI.ID Then
                                I.Data = RI.Data
                                I.DataTemp = RI.Data
                                I.dataDescription = RI.dataDescription
                                I.dataDescriptionTemp = RI.dataDescription
                                Exit For
                            End If
                        Next
                    Next
                End If

                Dim Inserted As Boolean
                If Not myRule.UseTemplate Then
                    Inserted = PrintBarCode(myRule.DontOpenTaskAfterInsert, CurrentResult)
                Else
                    Inserted = GenerateZambaBarcode(myRule.DontOpenTaskAfterInsert, CurrentResult, Inserted)

                    TemplatePath = myRule.TemplatePath 'Hacer reconocimento de variable y texto inteligente
                    TemplatePath = TextoInteligente.ReconocerCodigo(TemplatePath, CurrentResult)
                    TemplatePath = WFRuleParent.ReconocerVariablesValuesSoloTexto(TemplatePath)

                    Dim TemplateName As String = New FileInfo(TemplatePath).Name
                    Dim TemplateTempFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, "temp", TemplateName)

                    If Not File.Exists(TemplateTempFile) Then
                        File.Copy(TemplatePath, TemplateTempFile)
                    End If

                    Dim spire As New FileTools.SpireTools()
                    Dim _resultToPrint As IResult
                    If Not myRule.UseCurrentTask Then
                        NewResult.barcodeInBase64 = spire.GenerateBarcodeImage(TemplateTempFile, _barcodeId)
                        _resultToPrint = NewResult
                    Else
                        CurrentResult.barcodeInBase64 = spire.GenerateBarcodeImage(TemplateTempFile, _barcodeId)
                        _resultToPrint = CurrentResult
                    End If

                    Dim tempFile As String = FileBusiness.GetUniqueFileName(Path.Combine(Membership.MembershipHelper.AppTempPath, "OfficeTemp", Membership.MembershipHelper.CurrentUser.ID, _resultToPrint.ID, "BarcodePdfTemp.pdf"))


                    Dim T1 As New Thread(New ParameterizedThreadStart(AddressOf PrintForm))

                    Dim Obj As New ArrayList
                    Obj.Add(TemplateTempFile)
                    Obj.Add(copies)
                    Obj.Add(_resultToPrint)
                    Obj.Add(tempFile)
                    Obj.Add(Membership.MembershipHelper.CurrentUser.ID)
                    Obj.Add(myRule.templateWidth)
                    Obj.Add(myRule.templateHeight)

                    T1.SetApartmentState(ApartmentState.STA)
                    T1.Start(Obj)

                End If

                If myRule.continueWithGeneratedDocument Then
                    Dim task As ITaskResult
                    If myRule.UseCurrentTask Then
                        task = CurrentResult
                    Else
                        Dim WFTB As New WF.WF.WFTaskBusiness()
                        task = WFTB.GetTaskByDocIdAndDocTypeId(NewResult.ID, NewResult.DocTypeId)
                    End If
                    If Not IsNothing(task) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se adquiere la tarea del WF: " & task.WorkId)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia")
                        task = New TaskResult()
                        task.ID = NewResult.ID
                        task.Indexs = NewResult.Indexs
                        task.DocType = NewResult.DocType
                        task.DocTypeId = NewResult.DocTypeId

                    End If
                    arrayItaskResult.Add(task)
                End If

                If Inserted Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Carátula generada con éxito!")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La caratula generada fue eliminada")
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de replicas: " & replicates)
                If replicates > 0 Then
                    For copie As Int16 = 0 To replicates - 1
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando carátula...")
                        If Not myRule.UseCurrentTask Then
                            NewResult = RB.GetNewNewResult(myRule.DocTypeId)
                            NewResult.Parent = ZC.FilterDocTypes(myRule.DocTypeId)
                            For Each I As Index In NewResult.Indexs
                                For Each RI As Index In CurrentResult.Indexs
                                    If I.ID = RI.ID Then
                                        I.Data = RI.Data
                                        I.DataTemp = RI.Data
                                        I.dataDescription = RI.dataDescription
                                        I.dataDescriptionTemp = RI.dataDescription
                                        Exit For
                                    End If
                                Next
                            Next
                        End If

                        If Not myRule.UseTemplate Then
                            PrintBarCode(myRule.DontOpenTaskAfterInsert, CurrentResult)
                        Else
                            Dim spire As New FileTools.SpireTools()
                            If Not myRule.UseCurrentTask Then
                                Inserted = spire.GenerateBarcodeImage(TemplatePath, _barcodeId)
                            Else
                                Inserted = spire.GenerateBarcodeImage(TemplatePath, _barcodeId)
                            End If
                        End If

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Carátula generada con éxito!")

                    Next
                End If
            Next

        Finally

        End Try
        If myRule.continueWithGeneratedDocument Then
            Return arrayItaskResult
        Else
            Return results
        End If
    End Function

    Public Function PlayWeb(Myrule As IDoGenerateCoverPage,
                            results As List(Of ITaskResult),
                            ByRef rulePendingEvent As RulePendingEvents,
                            ByRef executionResult As RuleExecutionResult,
                            ByRef params As Hashtable) As List(Of ITaskResult)

        Dim ruleid As Integer = Myrule.ID

        Try
            Dim ZC As New ZCore()
            Dim RB As New Results_Business()
            Dim TemplatePath As String
            For Each CurrentResult As TaskResult In results

                CurrentOriginalResult = CurrentResult

                Dim copies As Int16
                If IsNumeric(Myrule.CopiesCount) Then
                    copies = Myrule.CopiesCount
                Else
                    Dim tmpCopies As String
                    tmpCopies = TextoInteligente.ReconocerCodigo(Myrule.CopiesCount.ToString(), CurrentResult)
                    tmpCopies = WFRuleParent.ReconocerVariablesValuesSoloTexto(Myrule.CopiesCount.ToString())
                    If IsNumeric(tmpCopies) Then
                        copies = tmpCopies
                    End If
                End If
                'Si fallo todo ponele 1
                If copies <= 0 Then
                    copies = 1
                End If

                Dim replicates As Int16
                If Not Myrule.Copies.Equals(String.Empty) Then
                    If IsNumeric(Myrule.Copies) Then
                        replicates = Myrule.Copies
                    Else
                        Dim tmpReplicates As String
                        tmpReplicates = Int16.Parse(TextoInteligente.ReconocerCodigo(Myrule.Copies.ToString(), CurrentResult))
                        tmpReplicates = Int16.Parse(WFRuleParent.ReconocerVariablesValuesSoloTexto(Myrule.Copies.ToString()))

                        If IsNumeric(tmpReplicates) Then
                            replicates = tmpReplicates
                        End If
                    End If
                End If
                'Si fallo todo ponele 0
                If replicates < 0 Then
                    replicates = 0
                End If


                If Not Myrule.UseCurrentTask Then
                    NewResult = RB.GetNewNewResult(Myrule.DocTypeId)
                    NewResult.Parent = ZC.FilterDocTypes(Myrule.DocTypeId)

                    For Each I As Index In NewResult.Indexs
                        For Each RI As Index In CurrentResult.Indexs
                            If I.ID = RI.ID Then
                                I.Data = RI.Data
                                I.DataTemp = RI.Data
                                I.dataDescription = RI.dataDescription
                                I.dataDescriptionTemp = RI.dataDescription
                                Exit For
                            End If
                        Next
                    Next
                End If

                Dim Inserted As Boolean
                If Not Myrule.UseTemplate Then
                    Inserted = PrintBarCode(Myrule.DontOpenTaskAfterInsert, CurrentResult)
                Else
                    Inserted = GenerateZambaBarcode(Myrule.DontOpenTaskAfterInsert, CurrentResult, Inserted)

                    TemplatePath = Myrule.TemplatePath
                    TemplatePath = TextoInteligente.ReconocerCodigo(TemplatePath, CurrentResult)
                    TemplatePath = WFRuleParent.ReconocerVariablesValuesSoloTexto(TemplatePath)

                    Dim TemplateName As String = New FileInfo(TemplatePath).Name
                    Dim TemplateTempFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, "temp", TemplateName)

                    If Not File.Exists(TemplateTempFile) Then
                        File.Copy(TemplatePath, TemplateTempFile)
                    End If

                    Dim spire As New FileTools.SpireTools()
                    Dim _resultToPrint As IResult
                    If Not Myrule.UseCurrentTask Then
                        NewResult.barcodeInBase64 = spire.GenerateBarcodeImage(TemplateTempFile, _barcodeId)
                        _resultToPrint = NewResult
                    Else
                        CurrentResult.barcodeInBase64 = spire.GenerateBarcodeImage(TemplateTempFile, _barcodeId)
                        _resultToPrint = CurrentResult
                    End If

                    ' Ruta donde se guarda el archivo Html generado para imprimir 
                    Dim tempFile As String = FileBusiness.GetUniqueFileName(Path.Combine(Path.Combine(Membership.MembershipHelper.AppTempPath, "OfficeTemp", Membership.MembershipHelper.CurrentUser.ID, _resultToPrint.ID, "BarcodePdfTemp.html")))
                    ' Ruta donde va a buscar el archivo el FrontEnd 
                    Dim TempFileToParams As String = tempFile.Replace(Membership.MembershipHelper.AppTempPath, "log").Replace("\", "/")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "TempFile: " & tempFile)

                    'Proceso el html y recibo el resultado
                    Dim FrmB As FormBusinessExt = New FormBusinessExt(TemplateTempFile, _resultToPrint, copies, New FileTools.SpireTools, tempFile, Membership.MembershipHelper.CurrentUser.ID, Myrule.templateWidth, Myrule.templateHeight, VariablesInterReglas.clone())
                    Dim htmlToPrint = FrmB.AsignValuesWithAgilityPack(TemplateTempFile)
                    'Guardo el archivo 
                    FileBusiness.CreateFile(tempFile, htmlToPrint)

                    rulePendingEvent = RulePendingEvents.LookingForFileToPrint
                    executionResult = RuleExecutionResult.PendingEventExecution
                    params.Add("BarcodeToPrintPath", tempFile)
                    params.Add("CopiesCount", copies)
                    params.Add("Width", Myrule.templateWidth)
                    params.Add("Height", Myrule.templateHeight)

                End If

                If Myrule.continueWithGeneratedDocument Then
                    Dim task As ITaskResult
                    If Myrule.UseCurrentTask Then
                        task = CurrentResult
                    Else
                        Dim WFTB As New WF.WF.WFTaskBusiness()
                        task = WFTB.GetTaskByDocIdAndDocTypeId(NewResult.ID, NewResult.DocTypeId)
                    End If
                    If Not IsNothing(task) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se adquiere la tarea del WF: " & task.WorkId)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia")
                        task = New TaskResult()
                        task.ID = NewResult.ID
                        task.Indexs = NewResult.Indexs
                        task.DocType = NewResult.DocType
                        task.DocTypeId = NewResult.DocTypeId

                    End If
                    arrayItaskResult.Add(task)
                End If

                If Inserted Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Carátula generada con éxito!")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La caratula generada fue eliminada")
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de replicas: " & replicates)
                If replicates > 0 Then
                    For copie As Int16 = 0 To replicates - 1
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando carátula...")
                        If Not Myrule.UseCurrentTask Then
                            NewResult = RB.GetNewNewResult(Myrule.DocTypeId)
                            NewResult.Parent = ZC.FilterDocTypes(Myrule.DocTypeId)
                            For Each I As Index In NewResult.Indexs
                                For Each RI As Index In CurrentResult.Indexs
                                    If I.ID = RI.ID Then
                                        I.Data = RI.Data
                                        I.DataTemp = RI.Data
                                        I.dataDescription = RI.dataDescription
                                        I.dataDescriptionTemp = RI.dataDescription
                                        Exit For
                                    End If
                                Next
                            Next
                        End If

                        If Not Myrule.UseTemplate Then
                            PrintBarCode(Myrule.DontOpenTaskAfterInsert, CurrentResult)
                        Else
                            Dim spire As New FileTools.SpireTools()
                            If Not Myrule.UseCurrentTask Then
                                Inserted = spire.GenerateBarcodeImage(TemplatePath, _barcodeId)
                            Else
                                Inserted = spire.GenerateBarcodeImage(TemplatePath, _barcodeId)
                            End If
                        End If

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Carátula generada con éxito!")

                    Next
                End If
            Next

        Finally

        End Try

        If Myrule.continueWithGeneratedDocument Then
            Return arrayItaskResult
        Else
            Return results
        End If
    End Function

    Private Sub PrintForm(obj As Object)

        Dim HtmlTemplate As String = obj(0)
        Dim copies As Int64 = obj(1)
        Dim ResultToPrint As IResult = obj(2)
        Dim TempFile As String = obj(3)
        Dim userId As String = obj(4)
        Dim width As String = obj(5)
        Dim height As String = obj(6)
        Dim VarInterReglas As Hashtable = obj(7)

        Dim FBB As New FormBusinessExt(HtmlTemplate, ResultToPrint, copies, New FileTools.SpireTools, TempFile, userId, width, height, VarInterReglas)

    End Sub

    Public Function PlayTest() As Boolean

    End Function

    Function DiscoverParams() As List(Of String)

    End Function

#Region "Variables"
    Private Myrule As IDoGenerateCoverPage
    Private arrayItaskResult As New System.Collections.Generic.List(Of Core.ITaskResult)()
    Private NewResult As NewResult
    Private _barcodeId As Integer
    Private WithEvents pdocumdoctypes As New Printing.PrintDocument
    Private Property ResultToPrint As IResult
    Private Property CurrentOriginalResult As ITaskResult
#End Region

    ''' <summary>
    ''' Imprime la carátula logueando los resultados de la impresión en el historial de la tarea.
    ''' </summary>
    ''' <param name="DontOpenTaskAfterInsert"></param>
    ''' <param name="t"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     Tomas   12/05/2011  Modified    Se refactorizo el método, se loguean los resultados de impresion
    '''                                     en la tarea y se oculta el cuadro de cancelación de impresión.
    ''' </history>
    Private Function PrintBarCode(ByVal DontOpenTaskAfterInsert As Boolean, ByVal TaskResult As ITaskResult) As Boolean

        Dim dlg As PrintDialog = Nothing
        Dim resultado As DialogResult = DialogResult.OK
        Dim Inserted As Boolean = False

        Try
            'Verifica si debe mostrar el cuadro de seleccion de impresora
            If Myrule.SetPrinter Then
                dlg = New PrintDialog()
                dlg.UseEXDialog = True
                dlg.Document = pdocumdoctypes
                resultado = dlg.ShowDialog()
            End If

            'Verifica si debe imprimir
            If resultado = DialogResult.OK Then
                'Lo que hacen estas 2 lineas de código es ocultar el cuadro por defecto que se genera
                'con el botón para cancelar la impresión.
                Inserted = GenerateZambaBarcode(DontOpenTaskAfterInsert, TaskResult, Inserted)
                If Inserted Then
                    'Si se inserto correctamente se imprime el documento y se loguea la acción
                    If Not Myrule.UseCurrentTask Then
                        ResultToPrint = NewResult
                    Else
                        ResultToPrint = TaskResult
                    End If

                    Dim copies As Int16 = If(Myrule.CopiesCount.Equals(String.Empty), 0, Myrule.CopiesCount)
                    While copies > 0
                        Dim T1 As New Thread(AddressOf PrintPage)
                        T1.Start()
                        copies = copies - 1
                    End While
                    Dim WFTB As New WF.WF.WFTaskBusiness
                    WFTB.LogTask(TaskResult, "La carátula ha sido impresa")
                    Dim UB As New UserBusiness
                    UB.SaveAction(ResultToPrint.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "Usuario Imprimio Caratula")
                End If
            Else
                'Si cancela se guarda la acción en la tarea
                Dim WFTB As New WF.WF.WFTaskBusiness
                WFTB.LogTask(TaskResult, "Se ha cancelado la impresión de la carátula")
            End If

            Return Inserted
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
        End Try
    End Function

    Private Sub PrintPage()
        Dim printer As New StandardPrintController
        pdocumdoctypes.PrintController = printer
        pdocumdoctypes.DefaultPageSettings.PrinterSettings.Copies = Myrule.CopiesCount
        pdocumdoctypes.Print()
    End Sub

    Private Function GenerateZambaBarcode(DontOpenTaskAfterInsert As Boolean, TaskResult As ITaskResult, Inserted As Boolean) As Boolean
        'Se generan nuevos ids
        _barcodeId = ToolsBusiness.GetNewID(IdTypes.Caratulas)

        If VariablesInterReglas.ContainsKey("Barcode") Then
            VariablesInterReglas.Item("Barcode") = _barcodeId
        Else
            VariablesInterReglas.Add("Barcode", _barcodeId)
        End If

        Dim BarcodesBusiness As New BarcodesBusiness

        If Not Myrule.UseCurrentTask Then
            NewResult.ID = ToolsBusiness.GetNewID(IdTypes.DOCID)

            'Se inserta la carátula
            If BarcodesBusiness.Insert(NewResult, CInt(NewResult.Parent.ID), CInt(Membership.MembershipHelper.CurrentUser.ID), _barcodeId, DontOpenTaskAfterInsert) Then
                Dim RB As New Results_Business()
                Inserted = RB.ValidateNewResult(NewResult.Parent.ID, NewResult.ID)
            Else
                If TaskResult IsNot Nothing Then
                    'Si no se inserto se loguea la acción y se avisa al usuario
                    Dim WFTB As New WF.WF.WFTaskBusiness
                    WFTB.LogTask(TaskResult, "La carátula no ha podido ser insertada")
                End If
                MessageBox.Show("No se pudo insertar el código de barras", "Error en Inserción", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            'Se inserta la carátula
            If BarcodesBusiness.Insert(TaskResult, CInt(TaskResult.Parent.ID), CInt(Membership.MembershipHelper.CurrentUser.ID), _barcodeId, DontOpenTaskAfterInsert) Then
                'valido que el documento se haya insertado
                Dim RB As New Results_Business()
                Inserted = RB.ValidateNewResult(TaskResult.Parent.ID, TaskResult.ID)
            Else
                If TaskResult IsNot Nothing Then
                    'Si no se inserto se loguea la acción y se avisa al usuario
                    Dim WFTB As New WF.WF.WFTaskBusiness
                    WFTB.LogTask(TaskResult, "La carátula no ha podido ser insertada")
                End If
                MessageBox.Show("No se pudo insertar el código de barras", "Error en Inserción", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
        Return Inserted
    End Function

#Region "Print"
    Private Sub pdocumdoctypes_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdocumdoctypes.PrintPage
        Evento_pdocumdoctypes_PrintPage(e)
    End Sub

    Private Sub Evento_pdocumdoctypes_PrintPage(ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        ' Dim i As Integer
        Dim y As Integer = 240
        Dim y2 As Integer = 270
        Dim y3 As Integer = 252
        Dim ylst As Integer = 282
        Dim ynam As Integer = 282

        'Dim doctypeALL As String
        Dim doctypeID As Int64
        Dim indexALL As String = String.Empty
        Dim dataBC As String = String.Empty
        Dim dataALL As String = String.Empty
        Dim IndexCount As Integer = 0

        Dim noteText As String = Myrule.Note
        noteText = TextoInteligente.ReconocerCodigo(noteText, CurrentOriginalResult)
        noteText = WFRuleParent.ReconocerVariablesValuesSoloTexto(noteText)

        Dim currentUser As User = New UserBusiness().GetUserById(Membership.MembershipHelper.CurrentUser.ID)

        Dim header As String = Now.ToString & " - " &
                                currentUser.Nombres &
                                " " & currentUser.Apellidos &
                                " - " & currentUser.Name() &
                                " (" & currentUser.ID.ToString & ")"
        'Fecha y Hora
        e.Graphics.DrawString(header,
                              New Font("Times New Roman", 9, FontStyle.Regular, GraphicsUnit.Point, 0),
                              Brushes.Black,
                              400,
                              75)

        'Fecha y Hora
        'e.Graphics.DrawString(Now.ToString,
        '                      New Font("Times New Roman", 9, FontStyle.Regular, GraphicsUnit.Point, 0),
        '                      Brushes.Black, 590, 75)

        'e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, 100, 735, 70))

        'Caratula
        e.Graphics.DrawString("Caratula Nro:", New Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, 40, 125)
        e.Graphics.DrawString(_barcodeId.ToString, New Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, 40, 138)
        Barcode_Motor.Print(e, _barcodeId.ToString, 150, 112)
        'e.Graphics.DrawLine(New Pen(Color.Black), 390, 100, 390, 170)
        'e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, 170, 735, 70))

        'User
        'e.Graphics.DrawString(UserBusiness.Rights.CurrentUser.Name(), New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 400, 125)
        'e.Graphics.DrawString("(" & Membership.MembershipHelper.CurrentUser.ID.ToString & ")", New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 400, 138)
        'Barcode_Motor.Print(e, Membership.MembershipHelper.CurrentUser.ID.ToString, 520, 110)
        'docType
        '        doctypeID = Me.DsDocTypes.DOC_TYPE(CboDocType.SelectedIndex).DOC_TYPE_ID
        e.Graphics.DrawString(ResultToPrint.DocType.Name.Trim & "    (" & ResultToPrint.DocType.ID.ToString & ")", New Font(FontFamily.GenericSansSerif, 11, FontStyle.Regular), Brushes.Black, 40, 199)
        'Barcode_Motor.Print(e, doctypeID.ToString, 520, 182)

        IndexCount = 0
        For Each printIndex As Index In ResultToPrint.Indexs
            'Imprimo comentario y salgo
            If IndexCount = 11 Then
                e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, y, 735, 70))
                e.Graphics.DrawString("Nota: ", New Font(FontFamily.GenericSerif, 9, FontStyle.Bold), Brushes.Black, 40, y2)
                If noteText.Length > 100 Then
                    'e.Graphics.DrawString(myrule.Note.Substring(0, 100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                    'e.Graphics.DrawString(myrule.Note.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2 + 13)
                    e.Graphics.DrawString(noteText.Substring(0, 100) & vbCrLf & noteText.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                Else
                    e.Graphics.DrawString(noteText, New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                End If
                Exit Sub
            End If

            'Si el atributo esta vacio no imprimo rectangulo
            If Not String.IsNullOrEmpty(printIndex.Data) OrElse Myrule.PrintIndexs = True Then
                IndexCount += 1
                Try
                    e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, y, 735, 70))
                    e.Graphics.DrawLine(New Pen(Color.Black), 210, y, 210, y + 70)

                    'index.name
                    indexALL = printIndex.Name & "    (" & printIndex.ID & ")"
                    If indexALL.Length <= 25 Then
                        e.Graphics.DrawString(indexALL, New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 40, y2)
                    Else
                        'e.Graphics.DrawString(indexALL.Substring(0, 25), New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 40, y2)
                        'e.Graphics.DrawString(indexALL.Substring(25), New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 40, ynam)
                        e.Graphics.DrawString(indexALL.Substring(0, 25) & vbCrLf & indexALL.Substring(25), New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 40, y2)
                    End If

                    'barCode
                    'valido si es numerico para no imprimir codigo de barra
                    Dim flagChar As Boolean = False
                    If Not IsNumeric(printIndex.Data) Then
                        flagChar = True
                    End If
                    If IsDate(printIndex.Data) Then
                        flagChar = False
                    End If
                    If Not String.IsNullOrEmpty(printIndex.Data) Then
                        Try
                            If IsNumeric(printIndex.Data) AndAlso printIndex.Data.Length <= 10 Then
                                dataBC = convertCodeBar(CInt(printIndex.Data))
                            Else
                                dataBC = printIndex.Data
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                        If Not flagChar = True Then
                            Try
                                If Myrule.PrintIndexs = True Then
                                    Barcode_Motor.Print(e, dataBC, 470, y3)
                                End If
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try
                        End If

                        'index.data
                        'If String.IsNullOrEmpty(printIndex.dataDescription) Then
                        If printIndex.DropDown <> IndexAdditionalType.AutoSustitución AndAlso printIndex.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico Then

                            '1
                            If flagChar = True Then
                                If Myrule.PrintIndexs = True Then
                                    If printIndex.Data.Length > 35 Then
                                        e.Graphics.DrawString(printIndex.Data.Substring(0, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                        If printIndex.Data.Length > 70 Then
                                            e.Graphics.DrawString(printIndex.Data.Substring(35, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                        Else
                                            e.Graphics.DrawString(printIndex.Data.Substring(35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                        End If
                                    Else
                                        e.Graphics.DrawString(printIndex.Data, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                    End If
                                Else
                                    If printIndex.Data.Length > 80 Then
                                        e.Graphics.DrawString(printIndex.Data.Substring(0, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                        If printIndex.Data.Length > 160 Then
                                            e.Graphics.DrawString(printIndex.Data.Substring(80, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                        Else
                                            e.Graphics.DrawString(printIndex.Data.Substring(80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                        End If
                                    Else
                                        e.Graphics.DrawString(printIndex.Data, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                    End If
                                End If

                            Else

                                '2
                                dataALL = printIndex.Data & "    (" & dataBC & ")"
                                If Myrule.PrintIndexs = True Then

                                    If dataALL.Length > 35 Then
                                        e.Graphics.DrawString(dataALL.Substring(0, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                        If dataALL.Length > 70 Then
                                            e.Graphics.DrawString(dataALL.Substring(35, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                        Else
                                            e.Graphics.DrawString(dataALL.Substring(35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                        End If
                                    Else
                                        e.Graphics.DrawString(dataALL, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                    End If
                                Else
                                    If dataALL.Length > 80 Then
                                        e.Graphics.DrawString(dataALL.Substring(0, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                        If dataALL.Length > 160 Then
                                            e.Graphics.DrawString(dataALL.Substring(80, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                        Else
                                            e.Graphics.DrawString(dataALL.Substring(80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                        End If
                                    Else
                                        e.Graphics.DrawString(dataALL, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                    End If
                                End If
                            End If
                        Else
                            '3
                            'lista de sustitucion
                            dataALL = printIndex.Data & " - " & printIndex.dataDescription & "  (" & dataBC & ")"
                            If Myrule.PrintIndexs = True Then
                                If dataALL.Length > 35 Then
                                    e.Graphics.DrawString(dataALL.Substring(0, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                    If dataALL.Length > 70 Then
                                        e.Graphics.DrawString(dataALL.Substring(35, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, ylst)
                                    Else
                                        e.Graphics.DrawString(dataALL.Substring(35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, ylst)
                                    End If
                                Else
                                    e.Graphics.DrawString(dataALL, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                End If
                            Else
                                If dataALL.Length > 80 Then
                                    e.Graphics.DrawString(dataALL.Substring(0, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                    If dataALL.Length > 160 Then
                                        e.Graphics.DrawString(dataALL.Substring(80, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, ylst)
                                    Else
                                        e.Graphics.DrawString(dataALL.Substring(80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, ylst)
                                    End If
                                Else
                                    e.Graphics.DrawString(dataALL, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                End If
                            End If
                        End If
                    End If
                Catch
                End Try

                y += 70
                y2 += 70
                y3 += 70
                ylst += 70
            End If
            'Try

            'Catch ex As Exception
            '   zamba.core.zclass.raiseerror(ex)
            'End Try

        Next

        'Comentario en caso de menos de 10 atributos
        e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, y, 735, 70))
        e.Graphics.DrawString("Nota: ", New Font(FontFamily.GenericSerif, 9, FontStyle.Bold), Brushes.Black, 40, y2)
        If noteText.Length > 100 Then
            'e.Graphics.DrawString(myrule.Note.Substring(0, 100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
            'e.Graphics.DrawString(myrule.Note.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2 + 13)
            e.Graphics.DrawString(noteText.Substring(0, 100) & vbCrLf & noteText.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
        Else
            e.Graphics.DrawString(noteText, New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
        End If

        If Not IsNothing(indexALL) Then indexALL = Nothing
        If Not IsNothing(dataBC) Then dataBC = Nothing
        If Not IsNothing(dataALL) Then dataALL = Nothing
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega ceros al value del barcode 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Shared Function convertCodeBar(ByVal data As Integer) As String
        Dim s As New System.Text.StringBuilder
        Try
            s.Append(data)
            If s.Length <= 9 Then
                s.Insert(0, "0", 9 - s.Length)
            End If
            Return s.ToString
        Finally
            s = Nothing
        End Try
    End Function
#End Region

    Public Sub New(ByVal rule As IDoGenerateCoverPage)
        Myrule = rule
    End Sub
End Class
