Imports ZAMBA.Servers
Imports ZAMBA.Core
Imports Zamba.Data
'Imports zamba.DocTypes.Factory
Imports System.Threading


<Ipreprocess.PreProcessName("Enviar Mail"), Ipreprocess.PreProcessHelp("Inserta el documento monitoreado en Zamba y lo envia por Mail. Recibe como parámetro el nombre del tipo de documento, el nombre del índice asociado al nombre del archivo, carpeta de backup, Id de la consulta AutoComplete,nombre de los indices de busqueda separados por |, nombre de los indices de actualizacion separados por |, Id de la consulta de Cias,Nombre Indice Busqueda,Id de la consulta de destinatarios,Nombre Indice Busqueda,(Si no es autenticado el envio de correo, ingresar un espacio en vez de los datos en Usuario, clave, puerto, servidor. )Usuario de Smtp,Clave de Smtp,puerto smtp,Servidor smtp")> _
Public Class ippMarshEmisionViaMail
    Inherits ZClass
    Implements Ipreprocess

#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
#End Region

#Region "Variables Locales"
    Const m_folderTemporal As String = "Temporal"
    Dim processed As Int32
    Dim Errors As Int32
    Dim Doctypename As String = "DocType"
    Dim DocType As DocType
    Dim BackUpFolder As String = ".\"
    Dim Document As NewResult
    Dim IndexName As String = "Index"
    Dim WhereCols1 As New ArrayList
    Dim UpdateCols1 As New ArrayList
    Dim SqlId1 As Int32
    Shared Destinatarios As String
    Dim SqlId2 As Int32
    Dim WhereCols2 As New ArrayList
    Dim CiaCode As Int32
    Dim CiaSupCode As Int32
    Shared CiaName As String
    Dim PrimaryIndexdata As String
    Shared Autor As String
    Shared email As String
    Shared Sector As String
    Dim userSmtp As String
    Dim pwdSmtp As String
    Dim portSmtp As String
    Dim serverSmtp As String
#End Region

#Region "Metodos Principales"
    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process
        Dim hilo As Thread = New Thread(AddressOf processEncolado)
        hilo.Priority = ThreadPriority.BelowNormal
        hilo.Start(New ProcessDto(Files, param, xml, Test))
        Return Nothing
    End Function
    Public Sub processEncolado(ByVal o As Object)
        Dim Files As ArrayList
        Dim param As ArrayList
        Dim xml As String
        Dim Test As Boolean
        Dim ppo As ProcessDto

        ppo = DirectCast(o, ProcessDto)

        Files = ppo.m_Files
        param = ppo.m_param
        xml = ppo.m_xml
        Test = ppo.m_Test

        Trace.WriteLineIf(ZTrace.IsInfo, Files(0))
        If Not IsNothing(Files(0)) Then
            Dim fi As IO.FileInfo
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "GetParams")
                ''1.--
                'Optiene los parametros necesarios, estos parametros se  pasan como un 
                'array, este array se pasa como parametro cuando se llama a este metodo.
                GetParameters(param)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Obteniendo Parametros " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "Mueve el archivo a una ruta temporal.")
                ''2.--
                'Mueve el archivo a una carpeta temporal
                'Retorna un file info.
                fi = MoveToTemp(Files(0))
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error al mover archivo. " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            ''3.--
            'Desparsea por el caracter '.'
            'Toma el primer valor, es decir obtiene el nombre del archivo
            PrimaryIndexdata = fi.Name.Split(".")(0)
            Trace.WriteLineIf(ZTrace.IsInfo, "El nombre del archivo es: " & PrimaryIndexdata)
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "Backupfile( " & fi.FullName & "," & BackUpFolder & ")")
                '' 4. --
                'Crea un back up del archivo.
                BackUpFile(fi.FullName, BackUpFolder)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Realizando BackUp " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "CreateDocument")
                ''5. --
                'Obtiene un nuevo result, a partir del tipo de documento.
                CreateDocument(fi, Document)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Creando Documento " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "Autocomplete")
                ''6.-- 
                'Completa los indices.
                Autocomplete(Document, WhereCols1, UpdateCols1, SqlId1)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Autocompletando " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo Codigo de Cia")
                ''7.--
                'Obtiene los datos de la compania.
                ObtenerNombreCiaDestinatarios(Document)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Obteniendo Codigo de Cia " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "Insert")
                ''8.--
                ''Inserta el documento en Zamba
                Insert()
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Insertando Documento " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "Llamo a actualizar")
                ''9.--
                'Actualiza un documento con los datos de inbroker
                'El documento a actualizar es el Doc_Type_Id = 57
                Actualizar(fi.Name.Substring(0, fi.Name.IndexOf(".")))
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, ex.Message)
            End Try
            Try
                ''9.2.-- 
                'Obtiene el autor de Pdf
                Autor = ObtenerAutorDePdf(fi)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error al enviar Mail " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            Try
                ''9.3.-- 
                'Buscar la direccion de correo del autor del Pdf
                email = ObtenerMailDeAutorPdf(Autor)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error al enviar Mail " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "Mail")
                ''10.--
                'Envia un automail.
                'El envio puede ser autenticado o sin autenticar.
                'Si no es autenticado los parametros del proceso tales como:
                'usuario, clave, puerto y servidor, deben ser estar vacios. 
                Dim smtpAutenticada As SMTP_Validada = Nothing
                If userSmtp.Length > 0 AndAlso pwdSmtp.Length > 0 AndAlso portSmtp.Length > 0 AndAlso serverSmtp.Length > 0 Then
                    smtpAutenticada = New SMTP_Validada(userSmtp, pwdSmtp, portSmtp, serverSmtp)
                End If
                Mail(MailsTypes.SolEmiOrden, Document, fi, smtpAutenticada)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error al enviar Mail " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "Log")
                ''11.--
                'Logea en la base de datos la insercion del documento.
                Log(fi.FullName)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Realizando Log" & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "Elimina archivo.")
                ''12.--
                'Borra el archivo de trabajo.
                'Se fuerza el Garbage collector para que libere las referencias,
                'y asi minimizar el blockeo de el archivo cuando se intenta borrar.
                'Igual este no asegura que el archivo quede liberado.
                GC.Collect()
                fi.Delete()
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error eliminando archivo " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                'Me.showNotifyError(ex)
                'Exit Sub
            End Try
            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "SHOWNOTIFY")
                ''13.--
                'Presenta un cartel notificando el resultado del proceso.
                showNotify()
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Realizando Log" & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                showNotifyError(ex)
                Exit Sub
            End Try
        End If
    End Sub
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        'TODO:Implementar
        Return Nothing
    End Function
#End Region

#Region "XML"
    'Sin implementar.
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function
    'Sin Implementar.
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
#End Region

#Region "Acciones"
    Private Sub GetParameters(ByVal param As ArrayList)
        Try
            'Dim params As String() = param.Split(",")
            Dim params As Array = param.ToArray
            'Dim i As Integer
            'Obtiene el tipo de Documento
            Doctypename = params(0)
            Trace.WriteLineIf(ZTrace.IsInfo, "Doctypename: " & Doctypename)
            'Optiene el nombre del indice
            IndexName = params(1)
            Trace.WriteLineIf(ZTrace.IsInfo, "IndexName: " & IndexName)
            'optiene el path de backup
            BackUpFolder = params(2)
            Trace.WriteLineIf(ZTrace.IsInfo, "BackupFolder: " & BackUpFolder)

            'Extrae los valores para armar la consulta.
            SqlId1 = params(3)
            Trace.WriteLineIf(ZTrace.IsInfo, "SqlId1: " & SqlId1)
            WhereCols1.AddRange(params(4).Split("|"))
            Trace.WriteLineIf(ZTrace.IsInfo, "WhereCols1: " & params(4))
            UpdateCols1.AddRange(params(5).Split("|"))
            Trace.WriteLineIf(ZTrace.IsInfo, "UpdateCols1: " & params(5))
            SqlId2 = params(6)
            WhereCols2.AddRange(params(7).Split("|"))

            userSmtp = params(10)
            pwdSmtp = params(11)
            portSmtp = params(12)
            serverSmtp = params(13)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    ''Mueve el archivo a procesar, a un forlder temporal.
    Private Function MoveToTemp(ByVal archivo As String) As IO.FileInfo
        Try
            Dim pathTemp As String
            Dim fileTemp As String
            pathTemp = IO.Path.Combine(BackUpFolder, m_folderTemporal)
            If Not IO.Directory.Exists(pathTemp) Then
                IO.Directory.CreateDirectory(pathTemp)
            End If
            fileTemp = IO.Path.Combine(pathTemp, IO.Path.GetFileName(archivo))
            'Se fuerza el GC.
            'Si algun proceso trabajo con el archivo, este no libera
            'el manejador a ese archivo, hasta que no se liberen sus recursos.
            'Es decir cualquier otro proceso que quiera trabajar con el 
            'archivo, no podra hacerlo.
            'Forzando el GC se elimima este inconveniente.
            'Este problema no ocurre siempre , sin que es aleatorio.
            If IO.File.Exists(fileTemp) Then
                GC.Collect()
                IO.File.Delete(fileTemp)
                GC.Collect()
            End If
            GC.Collect()
            IO.File.Move(archivo, fileTemp)
            GC.Collect()
            Return New IO.FileInfo(fileTemp)
        Catch ex As Exception
            Throw New ArgumentException(ex.ToString)
        End Try
    End Function
    'Crea un backUp del archivo a procesar.
    Private Shared Sub BackUpFile(ByVal File As String, ByVal Folder As String)
        Try
            Dim BK As New ippBackupFile
            BK.processFile(File, Folder)
            Trace.WriteLineIf(ZTrace.IsInfo, "BackUpFile:" & File.ToString)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    'Completa automaticamente los indices seleccionados.
    Private Shared Sub Autocomplete(ByRef Document As Result, ByVal WhereCols As ArrayList, ByVal UpdateCols As ArrayList, ByVal SqlId As Int32)
        Dim Autocomplete As New ippAutocomplete
        Autocomplete.ProcessDocument(Document, WhereCols, UpdateCols, SqlId)
        Trace.WriteLineIf(ZTrace.IsInfo, "Autocomplete:" & Document.Name)
    End Sub
    'Obtiene el nombre de la compania , superIntendencia y correo de los destinatarios.
    Private Sub ObtenerNombreCiaDestinatarios(ByRef Document As Result)
        Dim WhereColsData As New ArrayList
        Dim i As Int32
        For i = 0 To Document.Indexs.Count - 1
            If String.Compare(Document.Indexs(i).name, DirectCast(WhereCols2(0), String)) = 0 Then
                Trace.WriteLineIf(ZTrace.IsInfo, "CiaCode = " & Document.Indexs(i).data)
                CiaCode = CInt(Document.Indexs(i).data)
                WhereColsData.Add(CiaCode)
                Exit For
            End If
        Next
        Dim dstemp As DataSet = Zamba.Core.DataBaseAccessBusiness.ExecuteDataset(WhereColsData, SqlId2)

        Trace.WriteLineIf(ZTrace.IsInfo, dstemp.Tables(0).Rows(0).Item(0))
        Trace.WriteLineIf(ZTrace.IsInfo, dstemp.Tables(0).Rows(0).Item(1))
        Trace.WriteLineIf(ZTrace.IsInfo, dstemp.Tables(0).Rows(0).Item(2))
        If Not IsDBNull(dstemp.Tables(0).Rows(0).Item(0)) Then
            CiaName = dstemp.Tables(0).Rows(0).Item(0)
            Trace.WriteLineIf(ZTrace.IsInfo, "El nombre de la compañia es: " & CiaName)
        Else
            Trace.WriteLineIf(ZTrace.IsInfo, "No se encontro el nombre de la compañia")
        End If
        If Not IsDBNull(dstemp.Tables(0).Rows(0).Item(1)) Then
            CiaSupCode = dstemp.Tables(0).Rows(0).Item(1)
        Else
            Trace.WriteLineIf(ZTrace.IsInfo, "No se encontro el Codigo de Superintendencia")
        End If
        If Not IsDBNull(dstemp.Tables(0).Rows(0).Item(2)) Then
            Destinatarios = dstemp.Tables(0).Rows(0).Item(2)
        Else
            Trace.WriteLineIf(ZTrace.IsInfo, "No se encontron los destinatarios para notificar por correo")
        End If
    End Sub
    'Tipo de Auto Mail
    Enum MailsTypes
        SolEmiOrden
        SolEndOrden
    End Enum
    'Envia un Mail
    Private Shared Sub Mail(ByVal MailType As MailsTypes, ByVal Document As Result, ByVal Fi As IO.FileInfo, ByVal smptAutenticada As SMTP_Validada)
        Try
            Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo template de Mail SolEmiOrden")
            Dim autoMail As AutoMail = MessagesBusiness.GetAutomailByName(MailType.ToString)
            'Dim x As Int32
            'Dim i As Int32
            Trace.WriteLineIf(ZTrace.IsInfo, "Subject" & Fi.Name.Substring(0, Fi.Name.IndexOf(".")) & " Cia: " & CiaName)
            autoMail.Subject &= Fi.Name.Substring(0, Fi.Name.IndexOf(".")) & " Cia: " & CiaName
            Trace.WriteLineIf(ZTrace.IsInfo, "body" & autoMail.Body)
            autoMail.Body &= ControlChars.NewLine
            Trace.WriteLineIf(ZTrace.IsInfo, "destinatarios" & Destinatarios)
            autoMail.MailTo = Destinatarios
            autoMail.CC = email
            autoMail._Attach.Add(Fi.FullName)
            Trace.WriteLineIf(ZTrace.IsInfo, "Envio")
            MessagesBusiness.AutoMail_SMTP(autoMail, smptAutenticada)

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    'Crea un documento a partir del tipo del mismo 
    Private Sub CreateDocument(ByVal fi As IO.FileInfo, ByRef document As NewResult)

        Dim RB As New Results_Business
        'Agregue el document como parametro porque se usa fuera del metodo
        Try
            'Obtiene el tipo de documento a partir del nombre del del tipo de documento.
            DocType = DocTypesFactory.GetDocType(Doctypename)
            Trace.WriteLineIf(ZTrace.IsInfo, "Obtengo DocTypeID: " & DocType.ID)
            If DocType.ID = 0 Then
                Zamba.Core.ZClass.raiseerror(New Exception("No existe el tipo de documento " & Doctypename))
                Exit Sub
            End If
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, "Error asignado tipo de documento " & ex.ToString)
            Zamba.Core.ZClass.raiseerror(New Exception("Error al obtener el id del tipo de documento"))
            Exit Sub
        End Try
        Try
            'Obtiene un result a partir de un tipo de documento.
            'Solo se especifica el nombre del archivo.
            document = RB.GetNewNewResult(DocType, , fi.FullName)
            '   document.DocTypeName = Me.Doctypename
            Trace.WriteLineIf(ZTrace.IsInfo, "DoctypeName: " & Doctypename)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        RB = Nothing
        Try
            processed += 1
            Dim indexPosition As Int32 = -1
            Dim i As Integer
            'Limpio el objeto document y busco la pos del indice 
            For i = 0 To document.Indexs.Count - 1
                document.Indexs(i).Data = ""
                If document.Indexs(i).Name.Trim.ToUpper = IndexName.Trim.ToUpper Then
                    indexPosition = i
                    Exit For
                End If
            Next
            If indexPosition = -1 Then
                Trace.WriteLineIf(ZTrace.IsInfo, "El indice ingresado no corresponde al tipo de documento")
                Zamba.Core.ZClass.raiseerror(New Exception("El indice ingresado no corresponde al tipo de documento"))
                Exit Sub
            End If

            Trace.WriteLineIf(ZTrace.IsInfo, "OriginalFileName: " & fi.FullName)
            document.File = fi.FullName

            'TODO decir cual es el la posicion del indice
            'Completa el valor del indice seleccionado
            document.Indexs(indexPosition).Data = PrimaryIndexdata
            document.FolderId = CoreData.GetNewID(IdTypes.FOLDERSID)
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, "Error " & Errors & " " & ex.ToString)
            Errors += 1
            RaiseEvent PreprocessError(ex.ToString)
        End Try
    End Sub
    'Inserta el documento en Zamba
    Private Sub Insert()
        Try
            Results_Business.InsertDocument(Document, False, False, True, False)
            'Mover es False, el archivo se elimina al finalizar todo el proceso si errores.
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, "Error " & Errors & " " & ex.ToString)
            Errors += 1
            RaiseEvent PreprocessError(ex.ToString)
        End Try
    End Sub
    'Actualiza el doc_type_id = 57 con datos provenientes
    'de Inbroker.
    Private Shared Sub Actualizar(ByVal OrdenId As Int32)
        Dim strselect As String = "Select codseccion,idasegurado,codpoliza,FecFacturacion,FecVigenciaInicialPoliza, FecVigenciaFinalPoliza from sg_operacionconsulta where Idoperacion=" & OrdenId
        Trace.WriteLineIf(ZTrace.IsInfo, "El 1er Select: " & strselect)
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Trace.WriteLineIf(ZTrace.IsInfo, "El select devolvio: " & ds.Tables(0).Rows.Count & " filas")
        If ds.Tables(0).Rows.Count > 0 Then
            Dim sql As String = "Update doc_i57 set I46=" & ds.Tables(0).Rows(0).Item(0) & ", I16=" & ds.Tables(0).Rows(0).Item(1) & ", I18=" & ds.Tables(0).Rows(0).Item(2) & ", I12=" & Server.Con.ConvertDate(ds.Tables(0).Rows(0).Item(3)) & ", I20=" & Server.Con.ConvertDate(ds.Tables(0).Rows(0).Item(4)) & ", I21=" & Server.Con.ConvertDate(ds.Tables(0).Rows(0).Item(5)) & " where I13=" & OrdenId
            Trace.WriteLineIf(ZTrace.IsInfo, "SQL Actualización= " & sql)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            Trace.WriteLineIf(ZTrace.IsInfo, "La actualización se ejecuto OK")
        Else
            Trace.WriteLineIf(ZTrace.IsInfo, "No se encontro los datos de la orden para actualizar")
        End If
    End Sub
    'Escribe Un log de Insercion
    Private Sub Log(ByVal File As String)
        UserBusiness.Rights.SaveAction(0, ObjectTypes.ModuleMonitor, RightsType.Execute, "se inserto la orden N: " & PrimaryIndexdata & " de " & CiaName)
    End Sub
    'Muestra un cartel de notificacion cuando el proceso
    'fue ejecutado satisfactoriamente.
    Private Sub showNotify()
        Try
            Dim Ntf As New frmMonitorNotify("Se ha enviado la Orden N: " & PrimaryIndexdata & ControlChars.NewLine & " a " & CiaName & "(" & CiaCode & ")")
            Ntf.ShowDialog()
            Ntf.Dispose()
        Catch ex As Exception
            RaiseEvent PreprocessError(ex.ToString)
        End Try
    End Sub
    'Muestra un cartel de notificacion cuando el proceso 
    'fue ejecutado y no se pudo procesar correctamente.
    Private Sub showNotifyError(ByVal ex As Exception)
        Dim Ntf As New frmMonitorNotify("Error al enviar la Orden N: " & PrimaryIndexdata & ControlChars.NewLine & " " & ex.ToString)
        Ntf.ShowDialog()
        Ntf.Dispose()
    End Sub

    'Lee el nombre del autor.
    Private Function ObtenerAutorDePdf(ByVal fi As IO.FileInfo) As String
        Dim autor As String = ""
        ' Adrian:
        ' Por ahora se comenta para compilar ya que 
        ' no encuentro la clase en zamba.
        ' Dim info As Zamba.Tools.CFileInfo
        Try
            ' Adrian - info = New Zamba.Tools.CFileInfo(fi.FullName)
            ' Adrian - autor = info.FileAuthor
        Catch ex As Exception
            Throw New ArgumentException(ex.StackTrace)
        Finally
            ' Adrian - info = Nothing
        End Try
        Return autor
    End Function

    'Busca en los usuarios de zamba , cual es la direccion de correo,
    'Partiendo del nombre del usuario (Autor).
    Private Function ObtenerMailDeAutorPdf(ByVal autor As String) As String
        'Dim usuario As Zamba.Core.User
        Dim email As String = String.Empty
        Try
            email = Server.Con.ExecuteScalar(CommandType.Text, "Select email from MailsDeUsuarios Where UsuarioDeRed='" & autor & "'")
        Catch ex As Exception
            Throw New ArgumentException(ex.StackTrace)
        End Try
        Return email
    End Function

#End Region

#Region "Propiedades de Identidad del Proceso"
    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property
    Public Shared ReadOnly Property Name() As String
        Get
            Return "Emision Via Mail Marsh"
        End Get
    End Property
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Inserta el documento monitoreado en Zamba y lo envia por Mail. Recibe como parámetro el nombre del tipo de documento, el nombre del índice asociado al nombre del archivo, carpeta de backup, Id de la consulta AutoComplete,nombre de los indices de busqueda separados por |, nombre de los indices de actualizacion separados por |, Id de la consulta de Cias,Nombre Indice Busqueda,Id de la consulta de destinatarios,Nombre Indice Busqueda,(Si no es autenticado el envio de correo, ingresar un espacio en vez de los datos en Usuario, clave, puerto, servidor. )Usuario de Smtp,Clave de Smtp,puerto smtp,Servidor smtp"
    End Function
#End Region
    Public Overrides Sub Dispose()

    End Sub
End Class

#Region "Clase Auxiliar"
''Esta clase es utilizada, para pasarle los valores de configuracion, al hilo,
''que va a correr el proceso.
''Dado que los hilos soportan solo el objeto object y no es muy prolijo pasar 
''un array.
''Lo correcto es utilizar un objeto de transporte como es este Dto (Data Transfer Object).
Public Class ProcessDto
    Public m_Files As ArrayList
    Public m_param As ArrayList
    Public m_xml As String
    Public m_Test As Boolean

    Public Sub New(ByVal Files As ArrayList, _
                    ByVal param As ArrayList, _
                    ByVal xml As String, _
                    ByVal Test As Boolean)
        m_Files = Files
        m_param = param
        m_xml = xml
        m_Test = Test
    End Sub
End Class
#End Region


