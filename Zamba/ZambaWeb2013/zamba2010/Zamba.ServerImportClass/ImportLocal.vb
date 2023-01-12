Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Servers
'Imports Zamba.Imports
'Imports Zamba.DocTypes.Factory

Public Class ImportacionLocal
    Implements IDisposable

#Region "Variables Globales"
    Dim Ds As New DsProceso
    '   Dim _path As String
    Dim _user As String
    'Dim _userid As Int32
    '    Dim CarpetaBackup As String = Application.StartupPath & "\LocalImports"
    ', CarpetaError, Carpeta As String
    ' Dim indices As New DsConfig
    Dim Cadena As String
    '   Dim doctypeIDPrivate As Int32
    '   Dim docTypeIDPublic As Int32
    '  Dim docid As Int64
    '   Dim PublicIndexs As New DsConfig
    Dim UserData As New UserConfig
    Dim Errores As IO.StreamWriter

#End Region
#Region "Tipo Enumerado"
    Public Enum ModulosConError
        GET_ID_Proceso = 0
        CONEXION = 1
        RED = 2
        EjecucionInicial = 3
    End Enum
#End Region
#Region "Metodos Privados"

    Private Shared Function ReplaceChars(ByVal newname As String) As String
        newname = newname.Replace("ç", "")
        newname = newname.Replace("Ç", "")
        newname = newname.Replace("º", "")
        newname = newname.Replace("´", "")
        newname = newname.Replace("`", "")
        newname = newname.Replace("Ã", "")
        newname = newname.Replace("Á", "")
        newname = newname.Replace("à", "")
        newname = newname.Replace("á", "")
        newname = newname.Replace("å", "")
        newname = newname.Replace("æ", "")
        newname = newname.Replace("ñ", "")
        newname = newname.Replace("Ñ", "")
        newname = newname.Replace("€", "")
        newname = newname.Replace("É", "")
        newname = newname.Replace("Ê", "")
        newname = newname.Replace("È", "")
        newname = newname.Replace("è", "")
        newname = newname.Replace("é", "")
        newname = newname.Replace("ë", "")
        newname = newname.Replace("ê", "")
        newname = newname.Replace("Í", "")
        newname = newname.Replace("ì", "")
        newname = newname.Replace("í", "")
        newname = newname.Replace("î", "")
        newname = newname.Replace("ï", "")
        newname = newname.Replace("ÿ", "")
        newname = newname.Replace("Ó", "")
        newname = newname.Replace("Ò", "")
        newname = newname.Replace("ò", "")
        newname = newname.Replace("ó", "")
        newname = newname.Replace("ö", "")
        newname = newname.Replace("Ü", "")
        newname = newname.Replace("Ú", "")
        newname = newname.Replace("û", "")
        newname = newname.Replace("ù", "")
        newname = newname.Replace("ü", "")
        newname = newname.Replace("ú", "")
        newname = newname.Replace("Ù", "")
        newname = newname.Replace("'", "")
        newname = newname.Replace("<", "")
        newname = newname.Replace(">", "")
        newname = newname.Replace(Chr(34), "")
        newname = newname.Replace("ª", "")
        newname = newname.Replace("ä", "")
        newname = newname.Replace("'", "")
        newname = newname.Replace(",", "")
        newname = newname.Replace("~", "")
        newname = newname.Replace("Á", "")
        newname = newname.Replace("É", "")
        newname = newname.Replace("Í", "")
        newname = newname.Replace("Ó", "")
        newname = newname.Replace("Ú", "")
        Return newname
    End Function
    Private Sub RenameFiles(ByVal carpeta As IO.DirectoryInfo)
        Try
            Dim params() As Char = {"ç", "´", "€", "`", "á", "é", "í", "ó", "ú", "ñ", "Ñ", "Á", "É", "Í", "Ó", "Ú", "à", "è", "ì", "ò", "ù", "º", "ÿ", "û", "ö", "ô", "Ö", "Ü", "Ã", "Ê", "'", "<", ">", Chr(34), "º", "ä", "ª", ",", "'", "~", "Á", "É", "Í", "Ó", "Ú"}
            Dim newname As String

            For Each file As IO.FileInfo In carpeta.GetFiles
                If file.Name.IndexOfAny(params) <> -1 Then
                    newname = file.FullName
                    newname = ReplaceChars(newname)
                    IO.File.Copy(file.FullName, newname, True)
                    IO.File.Delete(file.FullName)
                End If
            Next
        Catch ex As Exception
            Trace.WriteLine(ex.ToString)
            zamba.core.zclass.raiseerror(ex)
            Errores.WriteLine("Error: " & ex.ToString)
        End Try
    End Sub
    Private Sub LoadConfig()
        '   Dim doctype As New DocTypesFactory

        '        Dim Fi As New IO.FileInfo(".\Import.ini")
        '       Dim INI As New Zamba.Tools.INIClass
        '  Me.Carpeta_Backup = UserPreferences.BackUpFolder
        ' Me.Carpeta = INI.ReadIni(Fi.FullName, "Folder", "Folder", ".\ImportFolder")
        'UserID = Zamba.controls.UserPreferences.UserId()
        'INI.ReadIni(Fi.FullName, "User", "UserId", 0)


        'Trace.WriteLine("MakeConection")
        'Me.makeConnection()
        'Trace.WriteLine("MakeCOnection OK")

        Try
            Trace.WriteLine("Obteniendo los permisos de usuario")
            'JNC
            'If IsNothing(Users.User.CurrentUser) OrElse Users.User.CurrentUser.ID <> Int64.Parse(UserPreferences.getValue("UserId", Sections.UserPreferences, 0)) Then Zamba.Core.RightComponent.ValidateLogIn(Int32.Parse(UserPreferences.getValue("UserId", Sections.UserPreferences, 0))) 'Me.UserId)
            Trace.WriteLine("Los permisos se obtuvieron correctamente")
        Catch ex As Exception
            Errores.WriteLine("ERROR: NO SE PUDO CARGAR LOS PERMISOS DE USUARIO, VERIFIQUE QUE EL USUARIO ESTE EN LA TABLA USRTABLE Y QUE NO HAYA MODIFICADO SU CONTRASEÑA")
        End Try

        'Trace.WriteLine("Archivo de indices: " & Me.IndexFile)
        'If IO.File.Exists(Me.IndexFile) Then
        '    Trace.WriteLine("Existe y lo leo")
        '    indices.ReadXml(Me.IndexFile)
        'Else
        '    Trace.WriteLine("No existe y lo creo")
        '    '    Me.CrearIndices()
        '    Trace.WriteLine("Se creo el archivo de indices.")
        'End If
        'If IO.File.Exists("C:\Importacion\PublicicIndexes.xml") Then
        '    Trace.WriteLine("El archivo de indices públicos existe y lo leo")
        '    Me.PublicIndexs.ReadXml("C:\Importacion\PublicicIndexes.xml")
        'Else
        '    Trace.WriteLine("El archivo de indices públicos no existe y lo creo")
        '    '   Me.CrearindicesPublicos()
        '    Me.PublicIndexs.ReadXml("C:\Importacion\PublicicIndexes.xml")
        'End If
    End Sub
    'Private Sub CrearIndices()
    '    Try
    '        '    Trace.WriteLine("DocTypeId Privado: " & Me.doctypeIDPrivate)
    '        '    If doctypeIDPrivate <= 0 Then
    '        '     Trace.WriteLine("No existe el tipo de documento " & Me.Documento)
    '        '     Application.Exit()
    '        '     End If

    '        ' Dim docindex As DataSet = doctype.GetIndexSchema(docid)

    '        'docid = utilities.LastId(IdTypes.DOCID)

    '        '  Dim sql As String = "select IP_ID from ip_type where IP_DoctypeId=" & doctypeIDPrivate
    '        '  Trace.WriteLine("Consulta sql: " & sql)
    '        '   Dim IPID As Int32 = Server.Con(True).ExecuteScalar(CommandType.Text, sql)
    '        '   Trace.WriteLine("Resultado de ejecucion SQL")
    '        '  If IPID <= 0 Then
    '        '    
    '        '    Trace.WriteLine("No existe el tipo proceso de importacion " & Me.ProcessName)
    '        '     End If

    '        '     sql = "select Index_Id from ip_index where IP_ID=" & IPID & " order by Index_Order"
    '        '    Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
    '        '    ds.Tables(0).TableName = indices.DsConfig.TableName
    '        '    indices.Merge(ds)
    '        '     Trace.WriteLine("escribo el indice.xml en:  " & Me.IndexFile)
    '        '     indices.WriteXml(Me.IndexFile)

    '    Catch ex As Exception
    '        Trace.WriteLine("Error: " & ex.tostring)
    '        MessageBox.Show(ex.tostring)
    '    End Try
    'End Sub
    'Private Sub CrearindicesPublicos()
    '    Try
    '        'IndicesPublicos
    '        Trace.WriteLine("Creo el archivo de indices publicos")
    '        Trace.WriteLine("DocTypeId Público: " & Me.docTypeIDPublic)
    '        If docTypeIDPublic <= 0 Then
    '            Trace.WriteLine("No existe el tipo de documento " & Me.Documento)
    '            Application.Exit()
    '        End If
    '        Dim sql As String = "select IP_ID from ip_type where IP_DoctypeId=" & docTypeIDPublic
    '        Trace.WriteLine("Consulta sql: " & sql)
    '        Dim IPID As Int32 = Server.Con.ExecuteScalar(CommandType.Text, sql)
    '        Trace.WriteLine("Resultado de ejecucion SQL, Id de Proceso de Mails Publicos: " & IPID.ToString)
    '        If IPID <= 0 Then
    '            Trace.WriteLine("No existe el tipo proceso de importacion " & Me.ProcessName)
    '        End If

    '        sql = "select Index_Id from ip_index where IP_ID=" & IPID & " order by Index_Order"
    '        Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
    '        Ds.Tables(0).TableName = Me.PublicIndexs.DsConfig.TableName
    '        PublicIndexs.Merge(Ds)
    '        Trace.WriteLine("Escribo el Publicindexes.xml en: C:\Importacion\PublicicIndexes.xml")
    '        PublicIndexs.WriteXml("C:\Importacion\PublicicIndexes.xml")
    '        Trace.WriteLine("El archivo de indices publicos se creo OK")
    '    Catch ex As Exception
    '        Trace.WriteLine("Error: " & ex.tostring)
    '    End Try
    'End Sub
    'Private Sub Preprocesos(ByRef cadena As String, ByVal WinUser As String, ByVal UserId As Int32)
    '    Dim i As Int16
    '    '   Dim privado As Boolean = True
    '    Trace.WriteLine("=====================")
    '    Trace.WriteLine("Accede a Preprocesos")
    '    Trace.WriteLine("=====================")

    '    Trace.WriteLine("Reemplazo caracteres inválidos en la linea ' y comillas dobles")
    '    cadena = cadena.Replace(Chr(34), "").Replace("'", "")
    '    'todo zimports: no deberia hacer el mismo reemplazo que de los archivos?

    '    Dim campos As String() = cadena.Split("|")
    '    'verifico la longitud del campo
    '    'Try
    '    '    For i = 0 To campos.Length - 1 'indices.Tables(0).Rows.Count - 1
    '    '        If IsNumeric(indices.DsConfig(i).Index_Id) AndAlso (indices.DsConfig(i).Index_Id = 34 Or indices.DsConfig(i).Index_Id = 31 Or indices.DsConfig(i).Index_Id = 32 Or indices.DsConfig(i).Index_Id = 33) Then
    '    '            ' Para Marsh, campos Para, CC, BCC y Asunto son cortados si superan los 200 caracteres
    '    '            If campos(i) <> "" AndAlso campos(i).Length > 200 Then
    '    '                Trace.WriteLine("Se ha recortado el campo a 200 caracteres")
    '    '                Trace.WriteLine(campos(i).ToString)
    '    '                campos(i) = campos(i).Substring(0, 199)
    '    '            End If
    '    '        End If

    '    '        'Se recortan los campos a 200 caracteres.
    '    '        If IsNumeric(Me.PublicIndexs.DsConfig(i).Index_Id) AndAlso (Me.PublicIndexs.DsConfig(i).Index_Id = 34 Or Me.PublicIndexs.DsConfig(i).Index_Id = 31 Or Me.PublicIndexs.DsConfig(i).Index_Id = 32 Or Me.PublicIndexs.DsConfig(i).Index_Id = 33) Then
    '    '            If campos(i) <> "" AndAlso campos(i).Length > 200 Then
    '    '                Trace.WriteLine("Se ha recortado el campo a 200 caracteres")
    '    '                Trace.WriteLine(campos(i).ToString)
    '    '                campos(i) = campos(i).Substring(0, 199)
    '    '            End If
    '    '            'End If
    '    '    Next
    '    'Catch ex As Exception
    '    '    Errores.WriteLine("ERROR en el preproceso: " & ex.tostring)
    '    '    ErrorsManager.LOG(ex, "Error al querer ")
    '    'End Try
    '    'verifico si es público o privado
    '    'Trace.WriteLine("Verifico si es Público o Privado")
    '    'For i = 11 To campos.Length - 1
    '    '    If campos(i).ToString.Trim <> String.Empty Then
    '    '        privado = False
    '    '    End If
    '    'Next
    '    ''rearmo la cadena
    '    'cadena = String.Empty
    '    'For i = 0 To campos.Length - 1
    '    '    cadena &= campos(i)
    '    '    cadena &= "|"
    '    'Next
    '    Trace.WriteLine("Copio la linea al maestro en Backup")
    '    'todo zimports: para que hago esto?
    '    If cadena.EndsWith("|") Then cadena = cadena.Substring(0, cadena.Length - 1)
    '    Try
    '        Dim sw As New IO.StreamWriter(Me.CarpetaBackup & "\Maestro.txt", True)
    '        sw.WriteLine(cadena)
    '        sw.Close()
    '    Catch ex As Exception
    '        Trace.WriteLine("Error: " & ex.ToString)
    '        Errores.WriteLine("Error: " & ex.ToString)
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try

    '    'Reemplazo los caracteres inválidos en la cadena (solo en el campo archivos)
    '    campos = cadena.Split("|")
    '    cadena = String.Empty
    '    campos(10) = Me.ReplaceChars(campos(10))
    '    For i = 0 To campos.Length - 1
    '        cadena &= campos(i)
    '        cadena &= "|"
    '    Next
    '    If cadena.EndsWith("|") Then cadena = cadena.Substring(0, cadena.Length - 1)
    '    Trace.WriteLine("Reemplazo caracteres inválidos en los archivos")
    '    '        Me.RenameFiles(New IO.DirectoryInfo(Me.Carpeta))
    '    Try
    '        'Cargo el Zcore con la configuracion Global
    '        ZCore.LoadCore()    ' Me.UserId)
    '    Catch ex As Exception
    '    End Try

    '    'INSERTA LA LINEA
    '    InsertarLinea(cadena)


    '    'TODO Martin: Ver cuando pueda ser cualquier DT y no priv o publ
    '    '    If privado = True Then
    '    'Trace.WriteLine("El mail es privado y lo importo como tal")


    '    'ImportarPrivado(cadena, WinUser, UserId)
    '    '    Else
    '    '  Trace.WriteLine("El mail es público y lo importo a Mail")
    '    ' ImportarPublico(cadena, WinUser, UserId)
    '    '    End If
    'End Sub
    ''Private Sub ImportarPrivado(ByVal cadena As String, ByVal WinUser As String, ByVal UserId As Int32)
    '    'TODO: Armar nombre de proceso y buscar con Select de IPTYPE para tomar los datos del proceso, y pasarlos en el new
    '    Dim ProcessName As String = "Mail de " & WinUser
    '    Dim Strselect As String = "Select IP_ID,IP_DOCTYPEID from IP_TYPE where IP_NAME = '" & ProcessName & "'"
    '    Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
    '    Dim ProcessId As Int32 = Ds.Tables(0).Rows(0).Item("IP_ID")
    '    Dim DocTypeId As String = Ds.Tables(0).Rows(0).Item("IP_DOCTYPEID")
    '    'ver martin    Dim importEngine As New Zamba.imports.LineImport(ProcessId, cadena, DocTypeId, UserId)
    'End Sub
    'Private Sub ImportarPublico(ByVal cadena As String, ByVal WinUser As String, ByVal UserId As Int32)
    '    'TODO: Armar nombre de proceso y buscar con Select de IPTYPE para tomar los datos del proceso, y pasarlos en el new
    '    Dim ProcessName As String = "Mail Publicos"
    '    Dim Strselect As String = "Select IP_ID, IP_DOCTYPEID from IP_TYPE where IP_NAME = '" & ProcessName & "'"
    '    Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
    '    Dim ProcessId As Int32 = Ds.Tables(0).Rows(0).Item("IP_ID")
    '    Dim DocTypeId As String = Ds.Tables(0).Rows(0).Item("IP_DOCTYPEID")
    '    'ver martin  Dim importEngine As New Zamba.imports.LineImport(ProcessId, cadena, DocTypeId, UserId)
    'End Sub
    'Private Sub GetPublicIndexes()
    '    If IO.File.Exists("C:\Importacion\PublicIndex.xml") Then
    '        PublicIndexs.ReadXml("C:\Importacion\PublicIndex.xml")
    '    Else

    '    End If

    'End Sub
#End Region

#Region "Metodos Públicos"
    Public Sub WriteErrors(ByVal ex As Exception, ByVal origen As ModulosConError)
        Try
            Dim RedError As New IO.FileInfo(Me.CarpetaBackup & "\NetErrors.txt")
            Dim ConErrors As IO.FileInfo
            Select Case origen
                Case ModulosConError.CONEXION
                    ConErrors = New IO.FileInfo(Me.CarpetaBackup & "\ERRORES_CONEXION.txt")
                    If ConErrors.Exists = False Then ConErrors.Create()
                Case ModulosConError.GET_ID_Proceso
                    ConErrors = New IO.FileInfo(Me.CarpetaBackup & "\Error_GetID.txt")
                Case Else
                    ConErrors = New IO.FileInfo(Me.CarpetaBackup & "\Errores.txt")
            End Select

            Dim sw As New IO.StreamWriter(ConErrors.FullName, True, System.Text.Encoding.Default)
            sw.WriteLine("Error:   " & Now.ToString)
            sw.WriteLine("-------------------------")
            sw.WriteLine("Error Origen: " & ex.Source)
            sw.WriteLine("Error: " & ex.ToString)
            sw.Close()
        Catch
        End Try
    End Sub

#End Region

#Region "New"
    'Public Sub New(ByVal linea As String)
    '    Cadena = linea
    '    Trace.WriteLine("6")
    '    LoadConfig()
    '    Trace.WriteLine("9")
    '    '        Me.RenameFiles(New IO.DirectoryInfo(Me.Carpeta))
    '    Trace.WriteLine("10")
    '    Preprocesos(Cadena, Environment.UserName, UserPreferences.UserId)
    '    Trace.WriteLine("11")
    'End Sub
#End Region

#Region "Propiedades"
    Public ReadOnly Property CarpetaBackup() As String
        Get
            Dim Dir As String = Me.StartUpPath & "\LocalImports"
            If IO.Directory.Exists(Dir) = False Then
                IO.Directory.CreateDirectory(Dir)
            End If
            Return Dir
        End Get
    End Property
    'Public Property Path() As String
    '    Get
    '        Return _path
    '    End Get
    '    Set(ByVal Value As String)
    '        _path = Value
    '    End Set
    'End Property
    Public Property Usuario() As String
        Get
            Return _user
        End Get
        Set(ByVal Value As String)
            _user = Value
        End Set
    End Property
    'Public Property Carpeta_Errores() As String
    '    Get
    '        Return CarpetaError
    '    End Get
    '    Set(ByVal Value As String)
    '        CarpetaError = Value
    '    End Set
    'End Property
    'Public Property UserId() As Int32
    '    Get
    '        Return _userid
    '    End Get
    '    Set(ByVal Value As Int32)
    '        _userid = Value
    '    End Set
    'End Property
    Public ReadOnly Property ProcessName() As String
        Get
            Return "Mail de " & Me.Usuario
        End Get
    End Property
    Public Shared ReadOnly Property Documento() As String
        Get
            Return "Mail de " & Environment.UserName
        End Get
    End Property
    'Private ReadOnly Property IndexFile()
    '    Get
    '        Return "C:\Importacion\" & "indices.xml"
    '    End Get
    'End Property
    ''Private ReadOnly Property PrivateMailFile() As String
    ''    Get
    ''        Return "C:\Importacion\" & "Private.osl"
    ''    End Get
    ''End Property
    'Private ReadOnly Property PublicMailFile() As String
    '    Get
    '        Return "C:\Importacion\" & "Public.osl"
    '    End Get
    'End Property
#End Region


#Region "HELP"
    'Toma la linea que genera Lotus Notes para grabar en el maestro y 
    'la importa a Zamba
    'Corre local en cada PC.
    '
    'Carpeta: Ubicacion de los archivos cuyos nombres se van a reemplazar
    'Carpeta_backup: Ubicacion donde se copian los archivos anteriores al proceso

#End Region
    'Private Sub ProcessNewLine()
    '    Try
    '        Dim nr As NewResult = NewResult.GetResultFromFile(Me.PrivateMailFile)
    '    Catch ex As Exception
    '        Trace.WriteLine("Error en ProcessNewLine")
    '    End Try
    'End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose
        Try
            Trace.WriteLine("Llamada a Dispose de Imports Local")
            UserData.Dispose()
            Ds.Dispose()
            'Me.Errores.Close()
            '  indices.Dispose()
            'Me.CarpetaBackup = String.Empty
        Catch
        End Try
    End Sub

    Dim NewResult As NewResult
    'Private Sub InsertarLinea(ByVal Linea As String)

    '    'INGRESAR INDICES DEL MENSAJE (C,CO,PARA,DE...) EN DSINDEXCONFIG.XSD
    '    'DESPUES HACER ABM DE INDICES DEL MENSAJE PARA QUE SEAN MODIFICABLES
    '    'LEER XML PARA SABER LA CANTIDAD DE PARAMETROS DEL MENSAJE Y SU RESPECTIVO ID

    '    Dim Ds As New DsIndexConfig

    '    If IO.File.Exists(Application.ExecutablePath & "\DsIndexConfig.xml") = True Then
    '        Ds.ReadXml(Application.ExecutablePath & "\DsIndexConfig.xml")
    '    Else
    '        Ds = CargarPorDefecto()
    '        Ds.WriteXml(Application.ExecutablePath & "\DsIndexConfig.xml")
    '    End If

    '    'LE ASIGNO A CANTPARMENSAJE LA CANTIDAD DE PARAMETROS QUE TIENE EL MENSAJE
    '    Dim CantParMensaje As Integer
    '    CantParMensaje = Ds.Tables(0).Rows.Count()

    '    'GUARDO EN PARTOTALES TODOS LOS PARAMETROS
    '    'LA CANTIDAD DE PARAMETROS TOTALES RECIBIDOS ES ParTotales.Count
    '    Dim ParTotales As ArrayList = New ArrayList
    '    ParTotales.AddRange(Linea.Split("|"))

    '    'VALIDO SI LA LINEA ESTA COMPLETA O FUE CORTADA POR LONGITUD
    '    If ParTotales.Count < 13 Then
    '        'La linea fue cortada, voy a buscarla en el maestro.
    '        Dim Path As String
    '        Path = ParTotales(11).ToString.Split(",")(0).ToString
    '        Path = Path.Substring(0, Path.LastIndexOf("/"))
    '        Dim File As String
    '        File = Path & "Maestro2.txt"
    '        If IO.File.Exists(File) Then
    '            Dim sr As New IO.StreamReader(File)
    '            Dim x As String
    '            While sr.Peek <> -1
    '                x = sr.ReadLine
    '                If x.IndexOf(ParTotales(0).ToString) <> -1 Then
    '                    Linea = x
    '                    InsertarLinea(Linea)
    '                    Exit Sub
    '                End If
    '            End While
    '        End If
    '    End If
    '    'PARINDICES ES LA CANTIDAD DE INDICES A GUARDAR
    '    Dim ParIndices As Integer
    '    ParIndices = (ParTotales.Count - CantParMensaje - 2 / 2)

    '    'SE QUE LOS ADJUNTOS ESTAN EN PARTOTALES, EL INDICE ES CANTPARMENSAJE

    '    Dim PathFiles As String
    '    PathFiles = ParTotales(CantParMensaje)
    '    'GUARDO EN PARFILES LOS PATHS DE LOS FILES
    '    Dim ParFiles As ArrayList = New ArrayList
    '    ParFiles.AddRange(PathFiles.Split(","))

    '    'EN NEWRESULT COMPLETO LOS PARAMETROS
    '    'DOCTYPEID SON LOS ELEMENTOS DE PARTOTALES A PARTIR DEL 
    '    'INDICE (CANTPARMENSAJE + 2), VIENE ID Y DESPUES VALUE
    '    'EN TOTAL LA CANTIDAD DE PAREJAS ES PARINDICES
    '    Dim i As Integer

    '    'GUARDO EN PARID TODOS LOS PARAMETROS ID
    '    Dim ParId As ArrayList = New ArrayList
    '    'AL PRINCIPIO LE AGREGO LOS ID DEL XML
    '    For i = 0 To CantParMensaje - 1
    '        ParId.Add(Ds.Tables(0).Rows(i))
    '    Next
    '    'DESPUES LE AGREGO LOS ID QUE VIENEN AL FINAL
    '    For i = (CantParMensaje + 2) To ParTotales.Count - 1 Step +2
    '        ParId.Add(ParTotales(i))
    '    Next

    '    'GUARDO EN PARVALUE TODOS LOS PARAMETROS VALUE
    '    Dim ParValue As New ArrayList
    '    'AL PRINCIPIO LE AGREGO LOS VALUE QUE VIENEN AL PRINCIPIO
    '    For i = 0 To CantParMensaje - 1
    '        ParValue.Add(ParTotales(i))
    '    Next
    '    'DESPUES LE AGREGO LOS VALUE QUE VIENEN AL FINAL
    '    For i = (CantParMensaje + 3) To ParTotales.Count - 1 Step +2
    '        ParValue.Add(ParTotales(i))
    '    Next

    '    Try

    '        NewResult = New NewResult
    '        Dim DocTypeId As Int32 = ParTotales(CantParMensaje + 1)
    '        NewResult.Parent = DocTypesFactory.GetDocType(DocTypeId)
    '        NewResult.DocumentalId = 0
    '        NewResult.FolderId = CoreData.GetNewID(IdTypes.FOLDERID)
    '        NewResult.Indexs = ZCore.FilterIndex(NewResult.Parent.ID)

    '        'COMPLETO LOS VALUES DE CADA ID
    '        For Each index As Zamba.Core.Index In NewResult.Indexs
    '            For i = 0 To ParId.Count - 1
    '                If index.ID = ParId(i) Then
    '                    Select Case index.Type
    '                        Case IndexDataType.Fecha
    '                            index.Data = CDate(ParValue(i))
    '                        Case IndexDataType.Fecha_Hora
    '                            index.Data = CDate(ParValue(i))
    '                        Case IndexDataType.Moneda
    '                            index.Data = CInt(ParValue(i))
    '                        Case IndexDataType.Numerico
    '                            index.Data = CInt(ParValue(i))
    '                        Case IndexDataType.Numerico_Decimales
    '                            index.Data = CInt(ParValue(i))
    '                        Case IndexDataType.Numerico_Largo
    '                            index.Data = CInt(ParValue(i))
    '                        Case IndexDataType.Moneda
    '                            index.Data = CInt(ParValue(i))
    '                        Case Else
    '                            index.Data = ParValue(i)
    '                    End Select
    '                End If
    '            Next
    '        Next

    '        'POR CADA FILE EN PARFILES INSERTO UNA VEZ
    '        For i = 0 To ParFiles.Count - 1
    '            Me.RenameFiles(New IO.FileInfo(ParFiles(i)).Directory)
    '            NewResult.File = ParFiles(i)
    '            Results_Factory.InsertDocument(NewResult, False, False, False, False)
    '        Next
    '    Catch
    '    End Try
    'End Sub

    Private Shared Function CargarPorDefecto() As DsIndexConfig
        Dim ds As New DsIndexConfig
        Try
            Dim row As DsIndexConfig.IndicesRow = ds.Indices.NewIndicesRow
            row.Codigo = 110
            row.Asunto = 34
            row.BCC = 33
            row.Hora = 70
            row.Fecha = 30
            row.Categoria = 53
            row.Para = 31
            row.EnviadoPor = 29
            row.UsuarioNotes = 51
            row.UsuarioWindows = 52
            row.CC = 32
            ds.Indices.Rows.Add(row)
            ds.AcceptChanges()
        Catch ex As Exception
        End Try
        Return ds
    End Function
    Private StartUpPath As String
    Public Sub New(ByVal startUpPath As String)
        Me.StartUpPath = startUpPath
    End Sub
End Class
