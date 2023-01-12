Imports Zamba.Core
Imports System.Drawing.Printing
Imports System.Drawing
Imports System.Threading
Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class PlayDoGenerateCoverPage


    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Try
            Me.Myrule = Myrule
            For Each CurrentResult As TaskResult In results
      
                NewResult = Results_Business.GetNewNewResult(Myrule.DocTypeId)
                NewResult.Parent = ZCore.FilterDocTypes(Myrule.DocTypeId)
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
                Dim Inserted As Boolean = PrintBarCode(Myrule.DontOpenTaskAfterInsert, CurrentResult)

                If Me.Myrule.continueWithGeneratedDocument Then
                    Dim task As ITaskResult = WF.WF.WFTaskBusiness.GetTaskByDocId(NewResult.ID)
                    If Not IsNothing(task) Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Se adquiere la tarea del WF: " & task.WorkId)
                    Else
                        Trace.WriteLineIf(ZTrace.IsInfo, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia")
                        task = New TaskResult()
                        task.ID = NewResult.ID
                        task.Indexs = NewResult.Indexs
                        task.DocType = NewResult.DocType
                        task.DocTypeId = NewResult.DocTypeId

                    End If
                    arrayItaskResult.Add(task)
                End If

                If Inserted Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Carátula generada con éxito!")
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "La caratula generada fue eliminada")
                End If

                Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad de replicas: " & Myrule.Copies)
                If Me.Myrule.Copies > 0 Then
                    For copie As Int16 = 0 To Me.Myrule.Copies - 1
                        Trace.WriteLineIf(ZTrace.IsInfo, "Generando carátula...")
                        NewResult = Results_Business.GetNewNewResult(Myrule.DocTypeId)
                        NewResult.Parent = ZCore.FilterDocTypes(Myrule.DocTypeId)
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
                        PrintBarCode(Myrule.DontOpenTaskAfterInsert, CurrentResult)
                        Trace.WriteLineIf(ZTrace.IsInfo, "Carátula generada con éxito!")
                    Next
                End If
            Next

        Finally

        End Try
        If Me.Myrule.continueWithGeneratedDocument Then
            Return arrayItaskResult
        Else
            Return results
        End If
    End Function

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
    Private Function PrintBarCode(ByVal DontOpenTaskAfterInsert As Boolean, ByVal t As ITaskResult) As Boolean

        Dim dlg As PrintDialog = Nothing
        Dim printer As StandardPrintController
        Dim resultado As DialogResult = DialogResult.OK
        Dim Inserted As Boolean = False

        Try
            'Verifica si debe mostrar el cuadro de seleccion de impresora
            If Me.Myrule.SetPrinter Then
                dlg = New PrintDialog()
                dlg.UseEXDialog = True
                dlg.Document = pdocumdoctypes
                resultado = dlg.ShowDialog()
            End If

            'Verifica si debe imprimir
            If resultado = DialogResult.OK Then
                'Lo que hacen estas 2 lineas de código es ocultar el cuadro por defecto que se genera
                'con el botón para cancelar la impresión.
                printer = New StandardPrintController
                pdocumdoctypes.PrintController = printer

                'Se generan nuevos ids
                Me._barcodeId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.Caratulas)
                NewResult.ID = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.DOCID)
        
                'Se inserta la carátula
                If BarcodesBusiness.Insert(NewResult, CInt(NewResult.Parent.ID), CInt(Membership.MembershipHelper.CurrentUser.ID), _barcodeId, DontOpenTaskAfterInsert) Then
                    'valido que el documento se haya insertado
                    If Results_Business.ValidateNewResult(CInt(NewResult.Parent.ID), CInt(NewResult.ID)) Then
                        'Si se inserto correctamente se imprime el documento y se loguea la acción
                        pdocumdoctypes.Print()
                        WF.WF.WFTaskBusiness.LogOtherActions(t.TaskId, t.Name, t.DocTypeId, t.DocType.Name, t.StepId, t.State.Name, t.WorkId, "La carátula ha sido impresa")
                        UserBusiness.Rights.SaveAction(NewResult.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "Usuario Imprimio Caratula")
                        Inserted = True
                    Else
                        Inserted = False
                    End If
                Else
                    If Not t Is Nothing Then
                        'Si no se inserto se loguea la acción y se avisa al usuario
                        WF.WF.WFTaskBusiness.LogOtherActions(t.TaskId, t.Name, t.DocTypeId, t.DocType.Name, t.StepId, t.State.Name, t.WorkId, "La carátula no ha podido ser insertada")
                    End If
                    MessageBox.Show("No se pudo insertar el código de barras", "Error en Inserción", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
            Else
                    'Si cancela se guarda la acción en la tarea
                WF.WF.WFTaskBusiness.LogOtherActions(t.TaskId, t.Name, t.DocTypeId, t.DocType.Name, t.StepId, t.State.Name, t.WorkId, "Se ha cancelado la impresión de la carátula")
            End If

            Return Inserted
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If dlg IsNot Nothing Then
                dlg.Dispose()
                dlg = Nothing
            End If
            If printer IsNot Nothing Then
                printer = Nothing
            End If
        End Try

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

        'Fecha y Hora
        e.Graphics.DrawString(Now.ToString, New System.Drawing.Font("Times New Roman", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), Brushes.Black, 590, 75)
        e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, 100, 735, 70))

        'Caratula
        e.Graphics.DrawString("Caratula Nro:", New Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, 40, 125)
        e.Graphics.DrawString(_barcodeId.ToString, New Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, 40, 138)
        Barcode_Motor.Print(e, _barcodeId.ToString, 150, 112)
        e.Graphics.DrawLine(New Pen(Color.Black), 390, 100, 390, 170)
        e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, 170, 735, 70))

        'User
        e.Graphics.DrawString(UserBusiness.Rights.CurrentUser.Name(), New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 400, 125)
        e.Graphics.DrawString("(" & Membership.MembershipHelper.CurrentUser.ID.ToString & ")", New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 400, 138)
        Barcode_Motor.Print(e, Membership.MembershipHelper.CurrentUser.ID.ToString, 520, 110)

        'docType
        '        doctypeID = Me.DsDocTypes.DOC_TYPE(CboDocType.SelectedIndex).DOC_TYPE_ID
        e.Graphics.DrawString(Me.NewResult.DocType.Name.Trim & "    (" & Me.NewResult.DocType.ID.ToString & ")", New Font(FontFamily.GenericSansSerif, 11, FontStyle.Regular), Brushes.Black, 40, 199)
        Barcode_Motor.Print(e, doctypeID.ToString, 520, 182)

        IndexCount = 0
        For Each printIndex As Index In Me.NewResult.Indexs
            'Imprimo comentario y salgo
            If IndexCount = 11 Then
                e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, y, 735, 70))
                e.Graphics.DrawString("Nota: ", New Font(FontFamily.GenericSerif, 9, FontStyle.Bold), Brushes.Black, 40, y2)
                If Myrule.Note.Length > 100 Then
                    'e.Graphics.DrawString(myrule.Note.Substring(0, 100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                    'e.Graphics.DrawString(myrule.Note.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2 + 13)
                    e.Graphics.DrawString(Myrule.Note.Substring(0, 100) & vbCrLf & Myrule.Note.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                Else
                    e.Graphics.DrawString(Myrule.Note, New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
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
        If Myrule.Note.Length > 100 Then
            'e.Graphics.DrawString(myrule.Note.Substring(0, 100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
            'e.Graphics.DrawString(myrule.Note.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2 + 13)
            e.Graphics.DrawString(Myrule.Note.Substring(0, 100) & vbCrLf & Myrule.Note.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
        Else
            e.Graphics.DrawString(Myrule.Note, New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
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
        Me.myRule = rule
    End Sub
End Class
