Imports Zamba.AppBlock
Imports Zamba.Servers
Imports Zamba.Tools
Imports Zamba.Data
Public Class LineImport
    Inherits ZClass
    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal ProcessId As Int32, ByVal Cadena As String, ByVal DocType As DocType, ByVal UserId As Int32)
        Me.New()
        Import(ProcessId, Cadena, ProcessTypes.Common, DocType, UserId)
    End Sub
    Dim Caracter As String
    Dim Result As Zamba.Core.NewResult

    Private Function CreateResult() As Boolean
        Try
            If IsNothing(Result) Then
                Dim RB As New Results_Business
                Me.Result = RB.GetNewNewResult(Process.DocType, Process.UserId)
                RB = Nothing
                '    EventOCurred("Asignando Entidad: " & Document.DocTypeName)
                'Results_Factory.Generacion_Indices(Document)
                '
                'Document.FlagCopyVerify = Me.Process.Verify
            End If
        Catch ex As Exception
            '         Process.History.Result = Results.Erroneo
            Throw New Exception("Error al crear el Documento Base del proceso " & ex.ToString)
        End Try
    End Function
    Dim Process As New Process
    Private Function CreateProcess(ByVal ProcessId As Int32, ByVal DocType As DocType, ByVal UserId As Int32) As Boolean
        If Process Is Nothing Then Me.Process = New Process
        Process.ID = ProcessId
        Process = ProcessFactory.GetProcess(ProcessId)
        Process.type = ProcessTypes.Common
        Process.Replace = True
        Process.AskConfirmations = False
        Process.Move = True
        Process.UserId = UserId
        Process.DocType = DocType
        GetprocessIndexData()

    End Function
    'Private Function GetprocessIndexData(ByVal ProcessId As Int32) As Boolean
    '    Try
    '        Process.DsProcessIndex = ProcessFactory.GetProcessIndexData(ProcessId)
    '        Return True
    '    Catch ex As Exception
    '        '           ErrorOcurred("Error obteniendo los datos del proceso: " & ex.tostring)
    '        '          Process.History.Result = Results.Erroneo
    '        Throw ex
    '    End Try
    'End Function

    Private Sub Import(ByVal ProcessId As Int32, ByVal x As String, ByVal Type As ProcessTypes, ByVal DocType As DocType, ByVal userId As Int32, Optional ByVal _caracter As Char = "|")

        CreateProcess(ProcessId, DocType, userId)
        x = Replace(x, ControlChars.NewLine, " ")
        'TODO Hernan: Aca poner una funcion que corrija todos los errores o que llame a los preprocesos que hagan falta

        'Separo los indices y archivos segun el separador
        Me.Caracter = _caracter
        Dim xsplit As String() = x.Split(Caracter.Trim)

        '   Dim preprocess As New Zamba.import.ZIPPreprocess.RenameFiles
        '   Dim campos As New ArrayList
        '   campos.Add(xsplit(10).Trim)
        '   preprocess.process(campos)

        'Declaraciones Internas
        Dim ProcessIndexCounter As Integer = 0
        Dim Ruta As String = ""
        Dim Extension As String = ""
        Dim NewFileName As String = ""
        Dim MultipleFiles As New ArrayList

        'reseteo el valor de los datos de los indices
        Me.CreateResult()
        Dim DocumentIndexCounter As Int32
        For DocumentIndexCounter = 0 To Result.Indexs.Count - 1
            Result.Indexs(DocumentIndexCounter).data = ""
        Next

        'chequeo el tipo y dato del indice
        For ProcessIndexCounter = 0 To Process.DsProcessIndex.IP_INDEX.Rows.Count - 1
            'TODO Falta ver si la columna es string no hacer la conversion y el trim que se hagga cuando se inserta el dato
            Dim IndexId As String = Process.DsProcessIndex.IP_INDEX.Rows(ProcessIndexCounter).Item("INDEX_ID")

            Select Case IndexId.ToUpper
                Case "RUTA"

                    Ruta = xsplit(ProcessIndexCounter)

                Case "RUTACOMPLETA"
                    If Me.Process.FlagMultipleFiles = True Then
                        MultipleFiles.Clear()

                        Dim FilesSplit() As String = xsplit(ProcessIndexCounter).Split(Me.Process.MultipleCaracter.Trim)
                        Result.File = xsplit(ProcessIndexCounter)
                        MultipleFiles.AddRange(FilesSplit)
                    Else
                        Result.File = xsplit(ProcessIndexCounter)
                    End If
                Case "NOMBREARCHIVO"

                    NewFileName = xsplit(ProcessIndexCounter)

                Case "EXTENSION"

                    Extension = xsplit(ProcessIndexCounter)

                Case "SININDEXAR"

                Case Else

                    Dim OIndexId As Object = CInt(IndexId)
                    'todo: buscar en el array si ya esta el ID
                    Dim i As Int64
                    Dim ArrayIndex As Integer
                    For i = 0 To Me.Result.Indexs.Count - 1
                        If Me.Result.Indexs(i).id = OIndexId Then
                            ArrayIndex = i
                            Exit For
                        End If
                    Next
                    'Dim ArrayIndex As Integer = Result.IndexsIdLst.IndexOf(OIndexId)
                    If ArrayIndex < 0 Then Throw New Exception("Indice no coincide")
                    Dim Data As String = Trim(xsplit(ProcessIndexCounter))
                    Data = Replace(Data, "'", " ")
                    If IsNothing(Data) Then Data = ""
                    If Data.Trim = "" Then
                        If Me.Process.FlagAceptBlankData = True Then
                            Select Case Int16.Parse(Result.Indexs(ArrayIndex).Type)
                                Case 1, 2, 3, 6
                                    Data = 0
                                Case 4, 5
                                    Data = String.Empty 'Now
                                Case Else
                                    Data = ""
                            End Select
                            Result.Indexs(ArrayIndex).data = Data
                        Else
                            Data = ""
                            '                                ErrorOcurred("Documento: " & ActualLine & " Fila " & x & " Indice " & IndexId)
                            '                               ErrorOcurred("Documento: " & ActualLine & " No se aceptan Indices en Blanco")
                            '                              Process.History.Result = Results.Con_Errores
                            Throw New Exception
                        End If
                    Else
                        'TODO Falta Condicionales especificos a abstraer
                        'validacion de atributos 'TODO Falta mandar a la validacion el len
                        Dim IndexValidated As String = Index.ValidateIndexTypeData(Data, Result.Indexs(ArrayIndex).type)
                        If IsNothing(IndexValidated) OrElse IndexValidated = "" Then
                            Result.Indexs(ArrayIndex).data = Data.Trim
                        Else
                            Data = ""
                            '                                ErrorOcurred("Documento: " & ActualLine & " Fila " & x)
                            '                               ErrorOcurred("Documento: " & ActualLine & " Error de Indice: " & IndexValidated & " Indice: " & IndexId)
                            '                              Process.History.Result = Results.Con_Errores
                            Throw New Exception
                        End If
                    End If

            End Select
        Next
        '   EventOCurred("Copiando Documento: " & ActualLine)
        If Result.File = "" Then
            Result.File = Ruta & NewFileName & Extension
        End If

        '       Result.CreateDate = Process.History.Process_Date
        'Creo el documento

        'TODO un solo proceso para insertar y salvar
        'Asigno el tipo de Documento nuevamente para actualizar el volumen
        'Inserto el Documento

        'Asigno el FolderId del documento, si es multiple doc, sera el mismo para todos.

        

        If Me.Process.FlagMultipleFiles = True Then
            Dim AlmostOneInserted As Boolean = False
            Dim i As Int32
            For i = 0 To MultipleFiles.Count - 1
                Result.File = MultipleFiles(i)
                If Result.File.Trim <> "" Then
                    '     EventOCurred("Verificando la existencia del archivo " & ActualLine)
                    If IO.File.Exists(Result.File) = False Then
                        '       Process.History.Result = Results.Con_Errores
                        Throw New Exception("El Archivo a importar no éxiste en la ubicación especificada: " & Result.File)
                    End If
                End If
            Next

            For i = 0 To MultipleFiles.Count - 1
                Result.File = MultipleFiles(i)
                If Result.File.Trim <> "" Then

                    Dim Resultado As InsertResult
                    If Process.type <> ProcessTypes.Test Then
                        Resultado = New Results_Business().Insert(Result, Process.Move, False, Me.Process.Replace, Process.AskConfirmations)
                    Else
                        Resultado = InsertResult.Insertado
                    End If
                    Select Case Resultado
                        Case InsertResult.ErrorCopia
                            Throw New Exception("Error al copiar el archivo")
                        Case InsertResult.ErrorInsertar
                            Throw New Exception("Error al Insertar")
                        Case InsertResult.ErrorReemplazar
                            Throw New Exception("Error al Reemplazar")
                        Case InsertResult.Insertado
                            DelOriginalDocumentFile()
                        Case InsertResult.InsertadoNuevoVolumen
                            DelOriginalDocumentFile()
                        Case InsertResult.InsertadoTempVolumen
                            DelOriginalDocumentFile()
                        Case InsertResult.NoInsertado
                            Throw New Exception("Error al Insertar")
                        Case InsertResult.NoRemplazado
                            Throw New Exception("No Reemplazado")
                        Case InsertResult.Remplazado
                            DelOriginalDocumentFile()
                        Case InsertResult.RemplazadoTodos
                            Me.Process.Replace = True
                            DelOriginalDocumentFile()
                        Case InsertResult.NoRemplazado
                            Me.Process.Replace = False
                            Process.AskConfirmations = False
                            Throw New Exception("No Reemplazado")
                    End Select
                    AlmostOneInserted = True

                    If Result.Volume.ID < 0 Then
                        '        ErrorOcurred("Almacenando en Volumen Temporal documento N: " & ActualLine)
                        '       EventOCurred("Insertando SubDocumento: " & i + 1 & " en VOLUMEN TEMPORAL, Documento Numero: " & ActualLine & " Nombre: " & Document.Name)
                        '          TempVolFiles += +1
                    Else
                        '         EventOCurred("Insertando SubDocumento: " & i + 1 & " de Documento Numero: " & ActualLine & " Nombre: " & Document.Name)
                    End If
                End If
            Next
            If AlmostOneInserted Then
                '    EventOCurred("Documento  Importado N: " & ActualLine)
                AlmostOneInserted = False
            Else
                Dim ex As New Exception("No se encontro ningun archivo para importar, verifique los indices")
                '   ErrorOcurred(ex.tostring)
                '  Process.History.Result = Results.Con_Errores
                Throw ex
            End If
        Else

            '       EventOCurred("Guardando Documento: " & ActualLine)
            If IO.File.Exists(Result.File) = False Then
                Throw New Exception("El Archivo a importar no existe en la ubicacion especificada: " & Result.File)
            End If
            Dim Resultado As InsertResult
            If Process.type <> ProcessTypes.Test Then
                Resultado = New Results_Business().Insert(Result, Process.Move, False, Me.Process.Replace, Process.AskConfirmations)
                '    results_factory.SaveIndexData(Document, False)
            Else
                Resultado = InsertResult.Insertado
            End If
            Select Case Resultado
                Case InsertResult.ErrorCopia
                    Throw New Exception("Error al copiar el archivo")
                Case InsertResult.ErrorInsertar
                    Throw New Exception("Error al Insertar")
                Case InsertResult.ErrorReemplazar
                    Throw New Exception("Error al Reemplazar")
                Case InsertResult.Insertado
                    DelOriginalDocumentFile()
                Case InsertResult.InsertadoNuevoVolumen
                    DelOriginalDocumentFile()
                Case InsertResult.InsertadoTempVolumen
                    DelOriginalDocumentFile()
                Case InsertResult.NoInsertado
                    Throw New Exception("Error al Insertar")
                Case InsertResult.NoRemplazado
                    Throw New Exception("No Reemplazado")
                Case InsertResult.Remplazado
                    DelOriginalDocumentFile()
                Case InsertResult.RemplazadoTodos
                    Me.Process.Replace = True
                    DelOriginalDocumentFile()
            End Select


            If Result.Volume.ID < 0 Then
                '      ErrorOcurred("Almacenando en Volumen Temporal documento N: " & ActualLine)
                '     EventOCurred("Insertando en VOLUMEN TEMPORAL, Documento Numero: " & ActualLine & " Nombre: " & Document.Name)
                '        TempVolFiles += +1
            Else
                '       EventOCurred("Insertando Documento Numero: " & ActualLine & " Nombre: " & Document.Name)
            End If
            '  EventOCurred("Documento Importado N:" & ActualLine)
        End If

        'If Process.History.Result = Results.Con_Errores Or Process.History.Result = Results.Erroneo Then
        '    Dim files As New ArrayList
        '    files.Add(Process.History.ERRORFILE)
        '    files.Add(Process.History.LOGFILE)
        '    Try
        '        Zamba.ZSMTP.SendMail("soporte@stardoc.com.ar", "El siguiente proceso de importación finalizo con errores.", "Errores de Importación", files)
        '    Catch
        '    End Try
        'End If
    End Sub
    Private Function GetprocessIndexData() As Boolean

        Process.DsProcessIndex = ProcessFactory.GetProcessIndexData(Me.Process.ID)
        Return True

    End Function
    Private Sub DelOriginalDocumentFile()
        Try
            If Process.type <> ProcessTypes.Test Then
                If Process.Move = True Then
                    Dim Fi As New IO.FileInfo(Result.File)
                    Fi.Delete()
                End If
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

End Class
