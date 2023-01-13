Imports System.IO
Imports Zamba.Servers
Imports Zamba.Core
Imports System.Drawing
Imports ZXing
Imports Spire.Barcode
Imports Ionic.Zip
'Imports zamba.DocTypes.Factory

<Ipreprocess.PreProcessName("Insertar Código de Barras"), Ipreprocess.PreProcessHelp("Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. Lee un archivo CajaYLote.xml, el cual esta ubicado en el directorio de la aplicación")>
Public Class ippTxtProcess
    Inherits ZClass
    Implements Ipreprocess

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Procesa los txt insertados, tomando los datos segun configuracion de caracteres fijos"
    End Function
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub
    Private Shared Sub ReadBoxAndBatch(ByRef caja As Int64, ByRef lote As String, ByRef Reemplaza As Boolean)
        Dim ds As DsCajaLote = Nothing
        Try
            ds = New DsCajaLote
            If File.Exists(Application.StartupPath & "\CajayLote.xml") Then
                ds.ReadXml(Application.StartupPath & "\CajayLote.xml")
                caja = ds.Tables(0).Rows(0).Item(0)
                lote = ds.Tables(0).Rows(0).Item(1)
                Reemplaza = ds.Tables(0).Rows(0).Item("REEMPLAZA")
            Else
                Dim row As DsCajaLote.dsCajaLoteRow = ds.dsCajaLote.NewdsCajaLoteRow
                row.NroCaja = 0
                row.lote = "completar"
                row.REEMPLAZA = False
                ds.Tables(0).Rows.Add(row)
                ds.AcceptChanges()
                ds.WriteXml(Application.StartupPath & "\CajayLote.xml")
                MessageBox.Show("Complete el Nro de Caja y Lote")
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            RaiseNotifyErrors("Error leyendo Caja Y Lote " & ex.ToString)
            Throw New Exception("Error leyendo Caja Y Lote " & ex.ToString)
        Finally
            Try
                If IsNothing(ds) = False Then ds.Dispose()
                ds = Nothing
                GC.Collect()
            Catch
            End Try
        End Try
    End Sub
    Public Overrides Sub Dispose()

    End Sub
    Private Function Movetotemp(ByVal Fi As FileInfo) As FileInfo
        Try
            If Fi.Exists Then
                Dim Dir As IO.DirectoryInfo
                Try
                    Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\temp")
                    If Dir.Exists = False Then
                        Dir.Create()
                    End If
                Catch
                    Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\temp")
                    If Dir.Exists = False Then
                        Dir.Create()
                    End If
                End Try
                Dim Fa As New FileInfo(Dir.FullName & "\" & Fi.Name)
                If Fa.Exists Then
                    GC.Collect(GC.MaxGeneration)
                    Fa.Delete()
                End If
                Fi.MoveTo(Fa.FullName)
                Return Fa
            Else
                Return Nothing
            End If
        Catch ex As Exception
            If ex.Message.IndexOf("obtener acceso") <> -1 Then
                GC.Collect(GC.MaxGeneration)
                Threading.Thread.CurrentThread.Sleep(10000)
                Dim Dir As IO.DirectoryInfo
                Try
                    Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\temp")
                    If Dir.Exists = False Then
                        Dir.Create()
                    End If
                Catch
                    Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\temp")
                    If Dir.Exists = False Then
                        Dir.Create()
                    End If
                End Try
                Dim Fa As New FileInfo(Dir.FullName & "\" & Fi.Name)
                If Fa.Exists Then
                    Fa.Delete()
                End If
                Fi.MoveTo(Fa.FullName)
                Return Fa
            ElseIf ex.Message.IndexOf("encontrar") <> -1 Then
                Dim dir As New IO.DirectoryInfo(Fi.Directory.FullName & "\Temp")
                Dim Fa As New FileInfo(dir.FullName & "\" & Fi.Name)
                If Fa.Exists = True Then
                    Return Fa
                Else
                    Return Nothing
                End If
            Else
                Throw New Exception("Error al mover a temporal " & ex.ToString)
            End If
        End Try
    End Function


    Class IndexDefinition
        Public Sub New(indexId As Long, fromChar As Integer, toChar As Integer)
            Me.IndexId = indexId
            Me.FromChar = fromChar - 1
            Me.ToChar = toChar

        End Sub

        Public Property IndexId As Int64
        Public Property FromChar As Int32
        Public Property ToChar As Int32

    End Class

    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process

        Dim docTypeId As Int64
        Dim docId As New ArrayList
        Dim result As New ArrayList
        Dim Reemplaza As Boolean
        Dim UserId As New ArrayList
        Dim ErrorFile As Boolean
        Dim IndexsDefinitions As New List(Of IndexDefinition)
        Dim fx As FileInfo
        Dim DocFileFrom As Int32
        Dim ftxt As FileInfo
        Dim sr As StreamReader
        Dim DoneDirPath As String
        Dim TempDirPath As DirectoryInfo
        Dim fz As FileInfo
        Dim zf As ZipFile
        Dim sw As StreamWriter

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando Proceso TxtProcess")

        If Not IsNothing(Files(0)) Then

            Dim Fzip As New FileInfo(Files(0))

            Try
                If (Fzip.Extension.ToLower = ".zip") Then

                    TempDirPath = New DirectoryInfo(Path.Combine(Fzip.Directory.FullName, "Temp", Fzip.Name.Split(".")(0)))

                    Dim CheckErrorDirPath As String = Path.Combine(TempDirPath.Parent.Parent.FullName, "Error", Fzip.Name.Split(".")(0))
                    Dim CheckDoneDirPath As String = Path.Combine(TempDirPath.Parent.Parent.FullName, "OK", Fzip.Name.Split(".")(0))

                    If (Directory.Exists(CheckDoneDirPath) = False AndAlso Directory.Exists(CheckErrorDirPath) = False AndAlso Directory.Exists(TempDirPath.FullName) = False) Then

                        fz = MovetoBarcodeFolderTemp(Fzip, TempDirPath)
                        zf = Ionic.Zip.ZipFile.Read(fz.FullName)

                        If Not TempDirPath.Exists Then
                            TempDirPath.Create()
                        End If

                        zf.ExtractAll(TempDirPath.FullName)
                        zf.Dispose()
                        zf = Nothing

                        ftxt = New FileInfo(Path.Combine(TempDirPath.FullName, fz.Name.ToLower().Replace("zip", "txt")))

                        If IsNothing(param) OrElse param.Count = 0 Then
                            Throw New Exception("No se ha configurado ningun parametro")
                        Else

                            Dim paramcount As Int32
                            For Each p As String In param
                                If paramcount = 0 Then
                                    If Int64.TryParse(p, docTypeId) Then
                                        paramcount += 1
                                        Continue For
                                    Else
                                        Throw New Exception("La Entidad es incorrecto")
                                    End If
                                End If
                                If paramcount = param.Count - 1 Then
                                    DocFileFrom = p
                                    paramcount += 1
                                    Continue For
                                End If

                                paramcount += 1

                                Dim IndexId As Int64
                                Dim FromToChar As String
                                Dim FromChar As Int32
                                Dim ToChar As Int32

                                If Int32.TryParse(p.Split("|")(0), IndexId) Then
                                    FromToChar = p.Split("|")(1)
                                    If FromToChar.Length > 0 Then
                                        If Int32.TryParse(FromToChar.Split("-")(0), FromChar) Then
                                            If Int32.TryParse(FromToChar.Split("-")(1), ToChar) Then

                                                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("La configuracion del Procesos es correcta: IndexId {0}, Desde {1}, Caracteres {2}", IndexId, FromChar, ToChar))
                                                IndexsDefinitions.Add(New IndexDefinition(IndexId, FromChar, ToChar))
                                            Else
                                                Throw New Exception("La Cantidad del Valor es incorrecto")
                                            End If
                                        Else
                                            Throw New Exception("El Desde del Valor es incorrecto")
                                        End If
                                    Else
                                        Throw New Exception("El Desde y Cantidad del Valor es incorrecto")
                                    End If
                                Else
                                    Throw New Exception("El IndexId es incorrecto")
                                End If
                            Next
                        End If


                        sr = New StreamReader(ftxt.FullName)

                        Dim DocType As IDocType = DocTypeBusinessExt.GetDocTypeByID(docTypeId)
                        Dim RB As New Results_Business
                        Dim line As String
                        While sr.Peek() > -1
                            Dim r As NewResult

                            Try
                                line = sr.ReadLine
                                If line Is Nothing Then
                                    Continue While
                                End If

                                r = Results_Business.GetNewNewResult(docTypeId)

                                For Each ID As IndexDefinition In IndexsDefinitions
                                    For Each I As IIndex In r.Indexs
                                        If I.ID = ID.IndexId Then
                                            Dim value As String = line.Substring(ID.FromChar, ID.ToChar)
                                            I.Data = value
                                            I.DataTemp = value
                                            Exit For
                                        End If
                                    Next
                                Next
                                Dim docfilepath As String = line.Substring(DocFileFrom - 1)
                                r.File = Path.Combine(ftxt.Directory.FullName, docfilepath)

                                Dim InsertResult As InsertResult = RB.Insert(r, True, False, True, False, False, False, False, False, False, False, 0, Nothing, True, String.Empty, String.Empty)

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando estado de indexacion a: " & IndexedState.IndicesSolamenteIndexado.ToString())
                                Dim exist = Server.Con.ExecuteScalar(CommandType.Text, "select 1 from ZINDEXERSTATE where DOCID = " & r.ID) = 1
                                If exist Then
                                    Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE ZINDEXERSTATE SET STATE = {0} WHERE DOCID = {1}", Convert.ToInt32(IndexedState.Pendiente), r.ID))
                                Else
                                    Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO ZINDEXERSTATE (DOCTYPE, DOCID, ""DATE"", STATE) VALUES ({0}, {1}, {2}, {3})", r.DocTypeId, r.ID, If(Server.isOracle, "SYSDATE", "GETDATE()"), Convert.ToInt32(IndexedState.Pendiente)))
                                End If

                            Catch ex As Exception
                                ErrorFile = True
                                raiseerror(ex)
                                If sw Is Nothing Then
                                    sw = New StreamWriter(Path.Combine(TempDirPath.FullName, ftxt.Name.ToLower().Replace(".txt", "-error.txt")))
                                    sw.AutoFlush = True
                                End If
                                sw.WriteLine(line)
                                sw.WriteLine(ex.Message)
                            Finally
                                If r IsNot Nothing Then
                                    r.Dispose()
                                    r = Nothing
                                End If
                            End Try
                        End While
                        RB = Nothing
                    Else
                        If (Fzip.Extension.ToLower = ".zip") AndAlso Fzip.Exists Then
                            Fzip.Delete()
                        End If
                    End If
                End If
            Catch ex As Exception
                ErrorFile = True
                raiseerror(ex)
            Finally
                If ftxt IsNot Nothing Then
                    ftxt = Nothing
                End If
                If fz IsNot Nothing Then
                    fz = Nothing
                End If
                If zf IsNot Nothing Then
                    zf = Nothing
                End If
                If sr IsNot Nothing Then
                    sr.Close()
                    sr.Dispose()
                End If
                If sw IsNot Nothing Then
                    sw.Close()
                    sw.Dispose()
                    sw = Nothing
                End If

                If ErrorFile Then
                    If (Fzip.Extension.ToLower = ".zip") Then
                        DoneDirPath = Path.Combine(TempDirPath.Parent.Parent.FullName, "Error")
                    ElseIf (Fzip.Extension.ToLower = ".txt") Then
                        DoneDirPath = Path.Combine(TempDirPath.Parent.FullName, "Error")
                    End If
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("No se pudo procesar el archivo [{0}], verificar la carpeta Exceptions", New FileInfo(Files(0)).FullName))
                Else
                    If (Fzip.Extension.ToLower = ".zip") Then
                        DoneDirPath = Path.Combine(TempDirPath.Parent.Parent.FullName, "OK")
                    ElseIf (Fzip.Extension.ToLower = ".txt") Then
                        DoneDirPath = Path.Combine(TempDirPath.Parent.FullName, "OK")
                    End If
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Archivo [{0}] procesado exitosamente ", New FileInfo(Files(0)).FullName))
                End If


                If (Fzip.Extension.ToLower = ".zip") Then
                    If Directory.Exists(TempDirPath.FullName) Then
                        If Not Directory.Exists(DoneDirPath) Then
                            Directory.CreateDirectory(DoneDirPath)
                        End If
                        TempDirPath.MoveTo(Path.Combine(DoneDirPath, TempDirPath.Name))
                    End If
                End If
                If (Fzip.Extension.ToLower = ".txt") AndAlso Fzip.Exists Then
                    If Not Directory.Exists(DoneDirPath) Then
                        Directory.CreateDirectory(DoneDirPath)
                    End If
                    Fzip.MoveTo(DoneDirPath)
                End If

            End Try
        Else
            raiseerror(New Exception("Files(0) esta en nothing"))
        End If

        Return Nothing

    End Function

    Private Function CodaBarExistsInZamba(ByVal IdCaratula As Int64) As Boolean
        Return (Server.Con.ExecuteScalar(CommandType.Text, String.Format("Select (1) from zbarcode where id = {0}", IdCaratula)) = 1)
    End Function

    ''' <summary>
    ''' Genera carpeta
    ''' </summary>
    ''' <param name="Directory"></param>
    Private Sub CreateDir(Directory As DirectoryInfo)
        Try
TryAgain:
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando carpeta: " & Directory.FullName)
            If Directory IsNot Nothing AndAlso Not Directory.Exists Then
                Directory.Create()
            End If
        Catch ex As Exception
            If ex.Message.IndexOf("obtener acceso") <> -1 Then
                GC.Collect(GC.MaxGeneration)
                Threading.Thread.CurrentThread.Sleep(10000)
                GoTo TryAgain
            Else
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al crear directorio " & ex.ToString)
                Throw New Exception("Error al crear directorio " & ex.ToString)
            End If
        End Try
    End Sub

    Private Function MovetoBarcodeFolderTemp(ByVal Fi As FileInfo) As FileInfo
        Try
TryAgain:
            If Fi.Exists Then
                Dim Dir As IO.DirectoryInfo
                Dir = New IO.DirectoryInfo(Path.Combine(Fi.Directory.FullName, "Temp"))
                If Dir.Exists = False Then
                    Dir.Create()
                End If

                Dim Fa As New FileInfo(Dir.FullName & "\" & Fi.Name)
                If Fa.Exists Then
                    Throw New Exception("Error al mover a temporal, ya existe el archivo en el temporal")
                End If
                Fi.MoveTo(Fa.FullName)
                Return Fa
            Else
                Return Nothing
            End If
        Catch ex As Exception
            If ex.Message.IndexOf("obtener acceso") <> -1 Then
                GC.Collect(GC.MaxGeneration)
                Threading.Thread.CurrentThread.Sleep(1000)
                GoTo TryAgain
            Else
                Throw New Exception("Error al mover a temporal " & ex.ToString)
            End If
        End Try
    End Function

    Private Function MovetoBarcodeFolderTemp(ByVal Fi As FileInfo, Dir As DirectoryInfo) As FileInfo
        Try
TryAgain:
            If Fi.Exists Then
                If Dir.Exists = False Then
                    Dir.Create()
                End If

                Dim Fa As New FileInfo(Dir.FullName & "\" & Fi.Name)
                If Fa.Exists Then
                    Throw New Exception("Error al mover a temporal, ya existe el archivo en el temporal")
                End If
                Fi.MoveTo(Fa.FullName)
                Return Fa
            Else
                Return Nothing
            End If
        Catch ex As Exception
            If ex.Message.IndexOf("obtener acceso") <> -1 Then
                GC.Collect(GC.MaxGeneration)
                Threading.Thread.CurrentThread.Sleep(1000)
                GoTo TryAgain
            Else
                Throw New Exception("Error al mover a temporal " & ex.ToString)
            End If
        End Try
    End Function

    Private Function MovetoDirectory(ByVal Source As DirectoryInfo, Dest As DirectoryInfo)
        Dim count As Int32 = 10
        Try
TryAgain:
            If Source.Exists Then
                If Dest.Exists = False Then
                    Dest.Create()
                End If
                Source.MoveTo(Dest.FullName)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            If ex.Message.IndexOf("obtener acceso") <> -1 AndAlso count > 0 Then
                count = count - 1
                GC.Collect(GC.MaxGeneration)
                Threading.Thread.CurrentThread.Sleep(3000)
                GoTo TryAgain
            Else
                Throw New Exception("Error al mover carpeta " & ex.ToString)
            End If
        End Try
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        'TODO:Implementar
        Return String.Empty
    End Function

    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property

    Public Shared ReadOnly Property Name() As String
        Get
            Return "Insertar Codigo de Barras"
        End Get
    End Property

End Class
