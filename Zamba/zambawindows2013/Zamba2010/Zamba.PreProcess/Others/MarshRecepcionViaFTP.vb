Imports ZAMBA.Core
Imports Zamba.Data
'Imports zamba.DocTypes.Factory

<Ipreprocess.PreProcessName("MarshRecepcionViaFTP"), Ipreprocess.PreProcessHelp("Inserta el documento monitoreado en Zamba y lo envia por Mail. Recibe como parámetro el nombre del tipo de documento, el nombre del índice asociado al nombre del archivo, carpeta de backup, Id de la consulta AutoComplete,nombre de los indices de busqueda separados por |, nombre de los indices de actualizacion separados por |, Id de la consulta de Cias,Nombre Indice Busqueda,Id de la consulta de destinatarios,Nombre Indice Busqueda")> _
Public Class ippMarshRecepcionViaFTP
    Inherits ZClass
    Implements Ipreprocess
    Public Overrides Sub Dispose()

    End Sub
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Inserta el documento monitoreado en Zamba y lo envia por Mail. Recibe como parámetro el nombre del tipo de documento, el nombre del índice asociado al nombre del archivo, carpeta de backup, Id de la consulta AutoComplete,nombre de los indices de busqueda separados por |, nombre de los indices de actualizacion separados por |, Id de la consulta de Cias,Nombre Indice Busqueda,Id de la consulta de destinatarios,Nombre Indice Busqueda"
    End Function
    Dim poliza As Poliza

#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
#End Region
#Region "Variables"
    Dim processed As Int32
    Dim Errors As Int32
    Dim Doctypename As String = "DocType"
    '    Dim docTypeId As Int32 = -1
    Dim DocType As DocType
    Dim BackUpFolder As String = ".\"
    Dim Document As New Result
    '    Dim IndexName As String = "Index"
    '   Dim WhereCols1 As New ArrayList
    '  Dim UpdateCols1 As New ArrayList
    '  Dim SqlId1 As Int32
    'Dim PolizasLineas As New ArrayList
    Dim path As String
    Dim fa As IO.FileInfo
    'Dim Destinatarios As String

    '   Dim SqlId2 As Int32
    '  Dim WhereCols2 As New ArrayList
    '  Dim SqlId3 As Int32
    '  Dim WhereCols3 As New ArrayList
    Dim CiaCode As Int32
    'Dim CiaSupCode As Int32
    Dim CiaName As String
    '  Dim PrimaryIndexdata As String

#End Region

    'Private Sub GetParameters(ByVal param As String)
    Private Sub GetParameters(ByVal param As ArrayList)
        'Dim params As String() = param.Split(",")
        Dim params As String() = param.ToArray
        '   Dim i As Integer
        Doctypename = params(0)
        Trace.WriteLineIf(ZTrace.IsInfo, "Doctypename: " & Doctypename)
        BackUpFolder = params(1)
        Trace.WriteLineIf(ZTrace.IsInfo, "BackupFolder: " & BackUpFolder)
        '        SqlId1 = params(3)
        '       Trace.WriteLineIf(ZTrace.IsInfo,"SqlId1: " & Me.SqlId1)
        '      WhereCols1.AddRange(params(4).Split("|"))
        '     Trace.WriteLineIf(ZTrace.IsInfo,"WhereCols1: " & params(4))
        '    UpdateCols1.AddRange(params(5).Split("|"))
        '   Trace.WriteLineIf(ZTrace.IsInfo,"UpdateCols1: " & params(5))
        '  SqlId2 = params(6)
        ' WhereCols2.AddRange(params(7).Split("|"))
        'SqlId3 = params(8)
        'WhereCols3.AddRange(params(9).Split("|"))
    End Sub

    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process
        'Trace.WriteLineIf(ZTrace.IsInfo,FM.FullFilename)
        Trace.WriteLineIf(ZTrace.IsInfo, Files(0))
        Dim PolizasLineas As New ArrayList

        'If Not IsNothing(FM.FullFilename) Then
        If Not IsNothing(Files(0)) Then

            '  Dim fa As New IO.FileInfo(FM.FullFilename)

            Try
                'aca filtras el TXT y lo lees y almacenas cada linea en el araylist PolizasLineas
                'Luego vas procesando cada linea y en createdocument asignas cada dato a los indices y el nombre del archivo con la ruta completa al file
                'If FM.FullFilename.ToUpper.EndsWith(".TXT") = False Then
                If Files(0).ToUpper.EndsWith(".TXT") = False Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "El archivo no es el de indices.(TXT)")
                    Return Nothing
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "Archivo de Indices, contando lineas")
                    'Dim sr As New IO.StreamReader(FM.FullFilename)
                    Dim sArch As String = Files(0)
                    Dim sr As New IO.StreamReader(sArch)
                    'path = New IO.FileInfo(FM.FullFilename).Directory.ToString
                    path = New IO.FileInfo(Files(0)).Directory.ToString

                    Dim line As String
                    While sr.Peek <> -1
                        line = sr.ReadLine
                        If line.Length > 7 Then PolizasLineas.Add(line)
                        'para no almacenar la ultima linea 00002
                    End While
                    sr.Close()
                End If
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Leyendo archivo de indices " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Return Nothing
            End Try

            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "GetParams")
                GetParameters(param)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Obteniendo Parametros " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Return Nothing
            End Try

            Dim i As Int32
            For i = 0 To PolizasLineas.Count - 1
                '   Me.PrimaryIndexdata = fi.Name.Split(".")(0)

                poliza = New Poliza(PolizasLineas(i))
                fa = New IO.FileInfo(path & poliza.File)
                Try
                    Trace.WriteLineIf(ZTrace.IsInfo, "Backupfile( " & fa.FullName & "," & BackUpFolder & ")")
                    BackUpFile(fa.FullName, BackUpFolder)
                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error Realizando BackUp " & ex.ToString)
                    zamba.core.zclass.raiseerror(ex)
                    showNotifyError(ex)
                    Return Nothing
                End Try
                Try
                    Trace.WriteLineIf(ZTrace.IsInfo, "CreateDocument")
                    CreateDocument(PolizasLineas(i))
                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error Creando Documento " & ex.ToString)
                    zamba.core.zclass.raiseerror(ex)
                    showNotifyError(ex)
                    Return Nothing
                End Try
                'Try
                '    Trace.WriteLineIf(ZTrace.IsInfo,"Autocomplete")
                '    Autocomplete(Document, WhereCols1, UpdateCols1, SqlId1)
                'Catch ex As Exception
                '    Trace.WriteLineIf(ZTrace.IsInfo,"Error Autocompletando " & ex.tostring)
                '   zamba.core.zclass.raiseerror(ex)
                '    Me.showNotifyError(ex)
                '    Exit Function
                'End Try
                'Try
                '    Trace.WriteLineIf(ZTrace.IsInfo,"Autocomplete")
                '    Me.ObtenerNombreCiaDestinatarios(Document)
                'Catch ex As Exception
                '    Trace.WriteLineIf(ZTrace.IsInfo,"Error Obteniendo Codigo de Cia " & ex.tostring)
                '   zamba.core.zclass.raiseerror(ex)
                '    Me.showNotifyError(ex)
                '    Exit Function
                'End Try
                'Try
                '    ObtenerDestinatarios(SqlId2)
                'Catch ex As Exception
                '    Trace.WriteLineIf(ZTrace.IsInfo,"Error Obteniendo Destinatarios " & ex.tostring)
                '   zamba.core.zclass.raiseerror(ex)
                '    Exit Function
                'End Try
                Try
                    Trace.WriteLineIf(ZTrace.IsInfo, "Insert")
                    Insert()
                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error Insertando Documento " & ex.ToString)
                    zamba.core.zclass.raiseerror(ex)
                    showNotifyError(ex)
                    Return Nothing
                End Try
                Try
                    Trace.WriteLineIf(ZTrace.IsInfo, "Mail")
                    Mail(Document)
                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error al enviar Mail " & ex.ToString)
                    zamba.core.zclass.raiseerror(ex)
                    showNotifyError(ex)
                    Return Nothing
                End Try

                Try
                    Trace.WriteLineIf(ZTrace.IsInfo, "Log")
                    'TODO: TIRA ERROR PORQUE ES PRIVATE, CORREGIR
                    'Log(fa.FullName)
                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error Realizando Log" & ex.ToString)
                    zamba.core.zclass.raiseerror(ex)
                    showNotifyError(ex)
                    Return Nothing
                End Try
                Try
                    fa.Delete()
                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error eliminando archivo " & ex.ToString)
                    zamba.core.zclass.raiseerror(ex)
                    showNotifyError(ex)
                    Return Nothing
                End Try

                Try
                    Trace.WriteLineIf(ZTrace.IsInfo, "SHOWNOTIFY")
                    showNotify()
                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error Realizando Log" & ex.ToString)
                    zamba.core.zclass.raiseerror(ex)
                    showNotifyError(ex)
                    Return Nothing
                End Try
            Next
        End If
        Return Nothing
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        'TODO:Implementar
        Return String.Empty
    End Function
#Region "XML"
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
#End Region

#Region "Validaciones"
    'Private Function ValidateFileName(ByVal filename As String) As Boolean
    '    ChequearTipoParte(filename.Substring(0, 2))
    'End Function




#End Region

#Region "Acciones"
    Private Shared Sub BackUpFile(ByVal File As String, ByVal Folder As String)
        Dim BK As New ippBackupFile
        BK.processFile(File, Folder)
    End Sub
    'Private Sub MoveToTempFolder(ByVal File As String, ByVal TempFolder As String)
    '    Dim TF As New ippMoveToTemp
    '    TF.processFile(File, TempFolder)
    'End Sub
    'Private Sub Autocomplete(ByVal Document As Result, ByVal WhereCols As ArrayList, ByVal UpdateCols As ArrayList, ByVal SqlId As Int32)
    '    Dim Autocomplete As New ippAutocomplete
    '    Autocomplete.ProcessDocument(Document, WhereCols, UpdateCols, SqlId)
    'End Sub
    'Private Sub ObtenerNombreCiaDestinatarios(ByVal Document As Document)
    '    Dim Autocomplete As New Autocomplete
    '    Dim WhereColsData As New ArrayList
    '    Dim i As Int32
    '    For i = 0 To Document.IndexNameLst.Count - 1
    '        If CStr(Document.IndexNameLst(i)).ToLower = CStr(WhereCols2(0)).ToLower Then
    '            Trace.WriteLineIf(ZTrace.IsInfo,"CiaCode = " & Document.IndexDataLst(i))
    '            Me.CiaCode = CInt(Document.IndexDataLst(i))
    '            WhereColsData.Add(Me.CiaCode)
    '            Exit For
    '        End If
    '    Next
    '    Dim DA As New Zamba.import.Execute
    '    Dim dstemp As DataSet = DA.ExecuteDataset(WhereColsData, SqlId2)

    '    ''  Dim b As Int32
    '    ' For b = 0 To dstemp.Tables(0).Rows.Count - 1
    '    Trace.WriteLineIf(ZTrace.IsInfo,dstemp.Tables(0).Rows(0).Item(0))
    '    Trace.WriteLineIf(ZTrace.IsInfo,dstemp.Tables(0).Rows(0).Item(1))
    '    Trace.WriteLineIf(ZTrace.IsInfo,dstemp.Tables(0).Rows(0).Item(2))

    '    CiaName = dstemp.Tables(0).Rows(0).Item(0)
    '    CiaSupCode = dstemp.Tables(0).Rows(0).Item(1)
    '    Destinatarios = dstemp.Tables(0).Rows(0).Item(2)
    '    'Autocomplete.ProcessScalar(WhereColsData, SqlId2)
    '    ' Next
    'End Sub
    'Private Sub ObtenerDestinatarios(ByVal SqlId3 As Int32)
    '    Dim WhereColsData As New ArrayList
    '    Dim i As Int32
    '    For i = 0 To Document.IndexNameLst.Count - 1
    '        If CStr(Document.IndexNameLst(i)).ToLower = CStr(WhereCols3(0)).ToLower Then
    '            Me.CiaCode = Document.IndexDataLst(i)
    '            WhereColsData.Add(CiaCode)
    '            Exit For
    '        End If
    '    Next

    '    Dim DA As New Zamba.import.Execute
    '    Dim dstemp As DataSet = DA.ExecuteDataset(WhereColsData, SqlId3)

    '    ''  Dim b As Int32
    '    ' For b = 0 To dstemp.Tables(0).Rows.Count - 1
    '    Destinatarios.Add(dstemp.Tables(0).Rows(b).Item(0))
    '    ' Next
    'End Sub
    Private Shared Sub Mail(ByVal Document As Result)
        'Trace.WriteLineIf(ZTrace.IsInfo,"Obteniendo template de Mail RecepPoliza")
        'Dim AutoMail As AutoMail = AutoMailDBFacade.GetAutomailByName("RecepPoliza")

        'Dim x As Int32
        'Dim i As Int32
        'Trace.WriteLineIf(ZTrace.IsInfo,"adjuntando" & "Zamba:\\Doc" & " " & Me.Document.Id & " " & Me.Document.DocTypeId)
        '' AutoMail.AddAttach("Zamba:\\Doc" & " " & Me.Document.Id & " " & Me.Document.DocTypeId)
        'Trace.WriteLineIf(ZTrace.IsInfo,"Subject" & poliza.Nro & " Cia: " & CiaName)
        'AutoMail.Subject &= poliza.Nro & " Cia: " & CiaName
        'Trace.WriteLineIf(ZTrace.IsInfo,"body" & AutoMail.Body)
        'AutoMail.Body &= ControlChars.NewLine
        'Trace.WriteLineIf(ZTrace.IsInfo,"destinatarios: " & Me.Destinatarios)
        'AutoMail.Body &= Document.FullPath
        'AutoMail.Body &= ControlChars.NewLine
        'AutoMail.MailTo = Me.Destinatarios
        ''TODO Falta ver si le agrego algun texto de los indices
        'Trace.WriteLineIf(ZTrace.IsInfo,"Envio")
        'AutoMail.Send()
    End Sub

    Private Sub CreateDocument(ByVal linea As String)
        Try
            DocType = DocTypesFactory.GetDocType(Doctypename)
            If DocType.ID = 0 Then
                zamba.core.zclass.raiseerror(New Exception("No existe el tipo de documento " & Doctypename))
                Exit Sub
            End If
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, "Error asignando tipo de documento. " & ex.ToString)
            zamba.core.zclass.raiseerror(New Exception("Error al obtener el id del tipo de documento"))
            Exit Sub
        End Try

        Document.DocType = DocType
        '  Document.DocTypeName = Me.Doctypename
        Trace.WriteLineIf(ZTrace.IsInfo, "DoctypeName: " & Doctypename)
        Results_Business.LoadIndexs(Document)
        ' Dim x As Int32
        Try
            processed += 1

            '    Dim indexPosition As Int32 = -1

            Dim ds As DataSet

            Dim i As Integer
            'Limpio el objeto document
            For i = 0 To Document.Indexs.Count - 1
                Document.Indexs(i).data = ""
                If Document.Indexs(i).id = 13 Then
                    'Nro de orden
                    'indexPosition = i
                    Document.Indexs(i).data = poliza.Orden
                End If
                If Document.Indexs(i).id = 18 Then
                    'Nro de poliza
                    Document.Indexs(i).data = poliza.Nro
                End If
                If Document.Indexs(i).id = 19 Then
                    'Endoso
                    Document.Indexs(i).id = poliza.Endoso
                End If
                If Document.Indexs(i).id = 46 Then
                    Document.Indexs(i).data = poliza.Seccion
                End If
                If Document.Indexs(i).id = 45 Then
                    'aca debería usar un autocomplete
                    ds = PreProcess_Factory.getSlst_s45(poliza.CodeCompany.ToString())
                    'ds = Server.Con.ExecuteDataset(CommandType.Text, "Select Codigo,Descripcion from slst_s45 where item=" & poliza.CodeCompany)
                    Document.Indexs(i).data = ds.Tables(0).Rows(0).Item(0)
                    CiaCode = ds.Tables(0).Rows(0).Item(0)
                    CiaName = ds.Tables(0).Rows(0).Item(1)
                    'Me.CiaSupCode = poliza.CodeCompany
                    ds.Dispose()
                End If
            Next
            'If indexPosition = -1 Then
            '    Trace.WriteLineIf(ZTrace.IsInfo,"El indice ingresado no corresponde al tipo de documento")
            '   zamba.core.zclass.raiseerror(New Exception("El indice ingresado no corresponde al tipo de documento"))
            '    Exit Sub
            'End If

            Trace.WriteLineIf(ZTrace.IsInfo, "OriginalFileName: " & fa.FullName)
            Document.File = fa.FullName

            'TODO Falta decir cual es el la posicion del indice
            'Document.IndexData(indexPosition) = Me.PrimaryIndexdata
            Document.FolderId = CoreData.GetNewID(IdTypes.FOLDERSID)
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, "Error " & Errors & " " & ex.ToString)
            Errors += 1
            RaiseEvent PreprocessError(ex.ToString)
        End Try
    End Sub
    Private Sub Insert()
        Try
            Results_Business.InsertDocument(Document, False, False, True, False)
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, "Error: " & Errors & " " & ex.ToString)
            Errors += 1
            RaiseEvent PreprocessError(ex.ToString)
        End Try
    End Sub
    'Private Sub Log(ByVal File As String)
    '    RightFactory.SaveAction(Zamba.Servers.Server.StationId, ObjectTypes.ModuleMonitor, Zamba.Core.RightsType.Execute, "Se inserto la póliza N: " & poliza.Nro & " de " & Me.CiaName)
    'End Sub

    'Private Sub LogsError(ByVal File As String, ByVal ex As Exception)
    '    RightFactory.SaveAction(Zamba.Servers.Server.StationId, ObjectTypes.ModuleMonitor, Zamba.Core.RightsType.Execute, "Error al insertar la poliza N: " & poliza.Nro & " de " & Me.CiaName & " " & ex.ToString)
    'End Sub
    'Private Function GetDestinatarios() As String
    '    Dim destinatarios As String
    '    Try
    '        Dim sql As String = "Select nombre_gestor from imbroker where orden "
    '        Dim ds As DataSet
    '        ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
    '        Dim i As Int32
    '        For i = 0 To ds.Tables(0).Rows.Count
    '            sql = "Select User_Mail from User_Table where User_Description='" & ds.Tables(0).Rows(i).Item(0) & "'"
    '            destinatarios &= Server.Con.ExecuteScalar(CommandType.Text, sql) & ";"
    '        Next
    '        If destinatarios.EndsWith(";") Then destinatarios = destinatarios.Substring(0, destinatarios.Length - 1)
    '        Return destinatarios
    '    Catch ex As Exception
    '    End Try
    'End Function
    Private Sub showNotify()
        Dim Ntf As New frmMonitorNotify("Se ha insertado la poliza N: " & poliza.Nro & ControlChars.NewLine & " a " & CiaName & "(" & CiaCode & ")")
        Ntf.ShowDialog()
        Ntf.Dispose()
    End Sub
    Private Sub showNotifyError(ByVal ex As Exception)
        Dim Ntf As New frmMonitorNotify("Error al insertar la póliza N: " & poliza.Nro & ControlChars.NewLine & " " & ex.ToString)
        Ntf.ShowDialog()
        Ntf.Dispose()
    End Sub

#End Region

    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property

    Public Shared ReadOnly Property Name() As String
        Get
            Return "Recepcion Via Mail Marsh"
        End Get
    End Property
End Class

Public Class PolizaLiberty
    Implements IDisposable
    'TODO: Leer archivo XML, conseguir uno.
    Dim ds As DataSet
    Public Sub New(ByVal fileXML As String)
        ds = New DataSet
        ds.ReadXml(fileXML)
    End Sub
    Public Shared ReadOnly Property Orden() As Int64
        Get

        End Get
    End Property
    Public Shared ReadOnly Property Endoso() As Int16
        Get

        End Get
    End Property
    Public Shared ReadOnly Property Seccion() As String
        Get
            'TODO:Implementar
            Return String.Empty
        End Get
    End Property
    Public Shared ReadOnly Property File() As String
        Get
            'TODO:Implementar
            Return String.Empty
        End Get
    End Property
    Public Shared ReadOnly Property Extension() As String
        Get
            'TODO:Implementar
            Return String.Empty
        End Get
    End Property
    Public Shared ReadOnly Property Nro() As Int64
        Get
            'TODO:Implementar
            Return Nothing
        End Get
    End Property
    Public Shared ReadOnly Property CodigoSuperIntendencia() As Int64
        Get
            'TODO:Implementar
            Return Nothing

        End Get
    End Property
    Public Shared ReadOnly Property CodeCompany() As Int64
        Get
            'TODO:Implementar
            Return Nothing

        End Get
    End Property
    Public Shared ReadOnly Property Fecha() As Date
        Get
            'TODO:Implementar
            Return Nothing

        End Get
    End Property
    Public Shared ReadOnly Property Hora() As DateTime
        Get
            'TODO:Implementar
            Return Nothing

        End Get
    End Property

    Public Sub Dispose() Implements System.IDisposable.Dispose
        ds.Dispose()
    End Sub
End Class

Public Class Poliza
    Implements IDisposable
    Private Linea As String

    Public Sub New(ByVal line As String)
        Linea = line
    End Sub
    Public Function Orden() As String
        Try
            Return Linea.Split("|")(0)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return String.Empty
    End Function
    Public Function Endoso() As String
        Try
            Return Linea.Split("|")(3)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return String.Empty
    End Function
    Public Function Seccion() As Int32
        Try
            Return Linea.Split("|")(1)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Function
    Public Function File() As String
        Try
            Return Linea.Split("|")(4)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return String.Empty
    End Function
    Public Function Extension() As String
        'la extension del archivo sin .
        Try
            Return Linea.Split("|")(4).Substring(30)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return String.Empty
    End Function
    Public Function Nro() As String  'string por los guiones
        Try
            Return Linea.Split("|")(2)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return String.Empty
    End Function
    Public Function CodigoSuperIntendencia() As Int64
        Try
            Return Linea.Split("|")(4).Substring(0, 3)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return Nothing
    End Function
    Public Shared Function CodeCompany() As Int32
        'TODO: Implentar
        Return Nothing
    End Function
    Public Function Fecha() As String
        Try
            Return Linea.Split("|")(4).Substring(18, 23)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return String.Empty
    End Function
    Public Function Hora() As String
        Return Linea.Split("|")(4).Substring(24, 29)
    End Function
    Public Sub Dispose() Implements System.IDisposable.Dispose
        Linea = Nothing
    End Sub
End Class
