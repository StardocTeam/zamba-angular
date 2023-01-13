Imports System.Threading
'Imports Zamba.Imports
Imports System.Runtime.InteropServices
Imports ZAMBA.Servers
Imports ZAMBA.Core
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
'Imports Zamba.AppBlock
Imports Zamba.Data

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.ServerImport
''' Class	 : Imports.Service.ZServEngine
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Servidor para el monitoreo de mail exportados a Zamba
''' </summary>
''' <remarks>
''' Inserta los mails que exporta Zamba.LocalImport.exe
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class ZServEngine
    Inherits MarshalByRefObject
    Implements Zamba.Core.IZRemoting

    Public Function ExecuteRule(ByVal RuleId As Long, ByRef lista As List(Of ITaskResult)) As Object Implements IZRemoting.ExecuteRule

    End Function

    Public Function ExecuteRule(ByVal ruleId As Int64, ByVal stepId As Int64, ByVal results As List(Of ITaskResult)) As List(Of ITaskResult) Implements IZRemoting.ExecuteRule

    End Function

    Public Function IsRunning() As Boolean Implements Core.IZRemoting.IsRunning
        Return True
    End Function

    Public Sub Maximizar() Implements Core.IZRemoting.Maximizar

    End Sub

    Public ImportacionLocal As ImportacionLocal
    'TODO ZIMPORTS: El dir debe ser configurable
    Private ImportsDirectory As IO.DirectoryInfo
    Private Importados, Pendientes, Erroneos As Int64

    Public Function Run(ByVal Action As String, ByVal Argument As String, ByVal obs As Hashtable) As Boolean Implements Core.IZRemoting.Run
        SyncLock (Me)
            Try
                If Me.ImportsDirectory.Exists = False Then Me.ImportsDirectory.Create()
                Dim Linea As String = Argument
                Dim es As New IO.StreamWriter(Me.ImportsDirectory.FullName & "\Line.txt", True)
                es.WriteLine(Linea)
                es.Close()
                '   Me.MnuImportados.Text = "Importados: " & Me.Importados.ToString
                '  Me.mnuPendientes.Text = "Pendientes: " & Me.Pendientes.ToString
                '  Me.mnuerrores.Text = "Errores: " & Me.Erroneos
                InsertarLinea(Linea)
                '  Me.MnuImportados.Text = "Importados: " & Me.Importados.ToString
                '   Me.mnuPendientes.Text = "Pendientes: " & Me.Pendientes.ToString
                '  Me.mnuerrores.Text = "Errores: " & Me.Erroneos
                RaiseEvent Status(Pendientes, Importados, Me.Erroneos, obs)
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            Finally
            End Try
        End SyncLock
    End Function


    Dim MachineName, IP, WinUser As String


    Public Function Run1() As String
        Try
            If Me.ImportsDirectory.Exists = False Then Me.ImportsDirectory.Create()
            Dim Linea As New System.Text.StringBuilder

            Dim i As Int16
            For i = 1 To Environment.GetCommandLineArgs.Length - 2
                Linea.Append(Environment.GetCommandLineArgs(i) & " ")
            Next
            'Linea.Append("67262B3E81D4642C032577EB00592731|Javier|Javier|Javier|30/11/2010|13:13:47|javier.colombera@stardoc.com.ar| | |test ||D:\lotusexporta\MSG37.TXT|1023|1129|1".Replace("'", "") & " ")


            'Linea.Append(Environment.GetCommandLineArgs(1))


            Trace.WriteLine(Me.ImportsDirectory.FullName & "\Line.txt")
            Dim es As New IO.StreamWriter(Me.ImportsDirectory.FullName & "\Line.txt", True)
            es.WriteLine(Linea)
            es.Close()
            Trace.WriteLine("Ya escribi Line.txt")
            'todo zimports: falta poner el resultado de cada linea en el archivo
            Trace.AutoFlush = True
            If Linea.Length = 0 Then
                Trace.WriteLine("------------------------------------------------------------------")
                Trace.WriteLine("     ----    Error:   " & Now.ToString & "         ----           ")
                Trace.WriteLine("No se ha recibido la linea, revise la configuracion de Lotus Notes")
                Trace.WriteLine("------------------------------------------------------------------")
            End If
            Trace.WriteLine("Linea: " & Linea.ToString)
            Dim ResultMessage As String = InsertarLinea(Linea.ToString)
            Trace.WriteLine("Finaliza el insertarlinea")
            Return ResultMessage
        Catch ex As Exception
            Trace.WriteLine("__________________________________________")
            Trace.WriteLine("Error: " & ex.ToString)
            Trace.WriteLine("__________________________________________")
            Return "Error al Insertar eMail"
        End Try
    End Function



    Dim NewResult As NewResult

    ''' <summary>
    ''' Inserta el mail pendiente a exportación de Exporta Lotus
    ''' </summary>
    ''' <param name="Linea">Línea con los parámetros enviados pro Lotus</param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  09/12/2010  Modified    Se actualiza llamadas a funciones de librerías de Zamba
    ''' </history>
    Private Function InsertarLinea(ByVal Linea As String) As String
        'INGRESAR INDICES DEL MENSAJE (C,CO,PARA,DE...) EN DSINDEXCONFIG.XSD
        'LEER XML PARA SABER LA CANTIDAD DE PARAMETROS DEL MENSAJE Y SU RESPECTIVO ID

        'If IsNothing(Users.User.CurrentUser) OrElse Users.User.CurrentUser.ID <> Int64.Parse(UserPreferences.getValue("UserId", Sections.UserPreferences, 0)) Then Zamba.Core.RightComponent.ValidateLogIn(Int32.Parse(UserPreferences.getValue("UserId", Sections.UserPreferences, 0))) 'Me.UserId)
        'Dim Ds As New DsIndexConfig

        'If IO.File.Exists(".\DsIndexConfig.xml") = True Then
        '    Trace.WriteLine("Leo el xml de configuracion")
        '    Ds.ReadXml(".\DsIndexConfig.xml")
        'Else
        '    Trace.WriteLine("El archivo de config no existe, creo uno por defecto. DsIndexConfig.xml")
        '    Ds = CargarPorDefecto()
        '    Ds.WriteXml(".\DsIndexConfig.xml")
        'End If

        Dim dsMapping As DataSet = GetMapping()

        'LE ASIGNO A CANTPARMENSAJE LA CANTIDAD DE PARAMETROS QUE TIENE EL MENSAJE
        Dim CantParMensaje As Integer

        CantParMensaje = dsMapping.Tables(0).Rows.Count()
        Trace.WriteLine("Cantidad Paramatros del xml: " & CantParMensaje)

        'GUARDO EN PARTOTALES TODOS LOS PARAMETROS
        'LA CANTIDAD DE PARAMETROS TOTALES RECIBIDOS ES ParTotales.Count
        Dim ParTotales As New ArrayList
        ParTotales.AddRange(Linea.Split("|"))
        Trace.WriteLine("1")
        Trace.WriteLine("Cantidad de Pares " & ParTotales.Count)
        'VALIDO SI LA LINEA ESTA COMPLETA O FUE CORTADA POR LONGITUD
        If ParTotales.Count < 13 Then

            'User
            Dim zambaUser As Zamba.Core.IUser = UserBusiness.GetUserByname(ParTotales(1).ToString)
            Zamba.Membership.MembershipHelper.SetCurrentUser(zambaUser)


            'La linea fue cortada, voy a buscarla en el maestro.
            Trace.WriteLine("Entro en la recuperacion de la linea")
            Dim Path As String
            Path = ParTotales(11).ToString.Split(",")(0).ToString
            Path = Path.Substring(0, Path.LastIndexOf("\") + 1)
            Dim File As String
            File = Path & "Maestro2.txt"
            If IO.File.Exists(File) Then
                Dim sr As IO.StreamReader = Nothing
                Try
                    sr = New IO.StreamReader(File)
                    Dim x As String
                    While sr.Peek <> -1
                        x = sr.ReadLine
                        If x.IndexOf(ParTotales(0).ToString) <> -1 Then
                            Linea = x
                            InsertarLinea(Linea)
                            Return Nothing
                        End If
                    End While
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                Finally
                    sr.Close()
                    sr.Dispose()
                    sr = Nothing
                End Try
            End If
        End If

        'PARINDICES ES LA CANTIDAD DE INDICES A GUARDAR
        Dim ParIndices As Integer
        ParIndices = ((ParTotales.Count - CantParMensaje - 2) / 2)
        Trace.WriteLine("2")

        'SE QUE LOS ADJUNTOS ESTAN EN PARTOTALES, EL INDICE ES CANTPARMENSAJE

        Dim PathFiles As String
        PathFiles = ParTotales(CantParMensaje).ToString()
        Trace.WriteLine("3")
        'GUARDO EN PARFILES LOS PATHS DE LOS FILES
        Dim ParFiles As New ArrayList
        ParFiles.AddRange(PathFiles.Split(","))
        Trace.WriteLine("4")

        'EN NEWRESULT COMPLETO LOS PARAMETROS
        'DOCTYPEID SON LOS ELEMENTOS DE PARTOTALES A PARTIR DEL 
        'INDICE (CANTPARMENSAJE + 2), VIENE ID Y DESPUES VALUE
        'EN TOTAL LA CANTIDAD DE PAREJAS ES PARINDICES
        Dim i As Integer

        'GUARDO EN PARID TODOS LOS PARAMETROS ID
        Dim ParId As New ArrayList
        'AL PRINCIPIO LE AGREGO LOS ID DEL XML
        For i = 0 To CantParMensaje - 1
            'ParId.Add(Ds.Tables(0).Rows(0).ItemArray(i))
            ParId.Add(dsMapping.Tables(0).Rows(i)(0).ToString)
        Next

        Trace.WriteLine("5")
        'DESPUES LE AGREGO LOS ID QUE VIENEN AL FINAL
        For i = (CantParMensaje + 2) To ParTotales.Count - 1 Step +2
            ParId.Add(ParTotales(i))
        Next
        Trace.WriteLine("6")

        'GUARDO EN PARVALUE TODOS LOS PARAMETROS VALUE
        Dim ParValue As New ArrayList
        'AL PRINCIPIO LE AGREGO LOS VALUE QUE VIENEN AL PRINCIPIO
        For i = 0 To CantParMensaje - 1
            ParValue.Add(ParTotales(i))
        Next
        Trace.WriteLine("7")
        'DESPUES LE AGREGO LOS VALUE QUE VIENEN AL FINAL
        For i = (CantParMensaje + 3) To ParTotales.Count - 1 Step +2
            ParValue.Add(ParTotales(i))
        Next

        'INSTANCIO EL USER
        Trace.WriteLine("8")
        Trace.WriteLine("SetCurrentUser")
        'Dim user As Zamba.Core.IUser = Zamba.Core.UserBusiness.GetUserByname(ParTotales(1).ToString)
        'Zamba.Core.UserBusiness.SetCurrentUser(user)

        Trace.WriteLine("8-BIS")
        Trace.WriteLine("Cargo el ZCore")
        ZCore.LoadCore()


        Try
            Trace.WriteLine("Instancio el Result")
            NewResult = New NewResult

            Dim cont As Integer
            For Each par As String In ParTotales
                Trace.WriteLine("param " + cont.ToString + " " + par)
            Next
            Trace.WriteLine("CantparMensaje + 1: " + Convert.ToString(CantParMensaje + 1))
            Trace.WriteLine("Doc_ID: " + ParTotales(CantParMensaje + 1).ToString())

            Dim DocTypeId As Int32 = Int32.Parse(ParTotales(CantParMensaje + 1).ToString())
            NewResult.Parent = DocTypesFactory.GetDocType(DocTypeId)
            Trace.WriteLine("10")
            NewResult.DocumentalId = 0
            'CUANDO ESTE LO DE LOTE, AGRUPAR POR CARPETA
            NewResult.FolderId = CoreData.GetNewID(IdTypes.FOLDERID)

            Trace.WriteLine("12")
            NewResult.Indexs = ZCore.FilterIndex(NewResult.Parent.ID)
            Trace.WriteLine("13")

            'COMPLETO LOS VALUES DE CADA ID
            For Each index As Zamba.Core.Index In NewResult.Indexs
                For i = 0 To (ParId.Count - 1)
                    Trace.WriteLine("ParID= " & ParId(i).ToString())

                    Trace.WriteLine("Verifico la nulidad del parid")

                    If IsDBNull(ParId(i)) = True Then

                    ElseIf CStr(ParId(i)) = "" Then

                    ElseIf IsNumeric(ParId(i)) = False Then

                    ElseIf index.ID = CInt(ParId(i)) Then
                        Trace.WriteLine("El parid coincide con el index.id")

                        Select Case index.Type
                            Case IndexDataType.Fecha
                                index.Data = CDate(ParValue(i)).ToString("dd/MM/yyyy")
                                index.DataTemp = CDate(ParValue(i)).ToString("dd/MM/yyyy")
                            Case IndexDataType.Fecha_Hora
                                index.Data = CDate(ParValue(i)).ToString()
                                index.DataTemp = CDate(ParValue(i)).ToString()
                            Case IndexDataType.Moneda
                                index.Data = CInt(ParValue(i)).ToString()
                                index.DataTemp = CInt(ParValue(i)).ToString()
                            Case IndexDataType.Numerico
                                index.Data = CInt(ParValue(i)).ToString()
                                index.DataTemp = CInt(ParValue(i)).ToString()
                            Case IndexDataType.Numerico_Decimales
                                index.Data = CInt(ParValue(i)).ToString()
                                index.DataTemp = CInt(ParValue(i)).ToString()
                            Case IndexDataType.Numerico_Largo
                                index.Data = Val(ParValue(i)).ToString()
                                index.DataTemp = Val(ParValue(i)).ToString()
                            Case IndexDataType.Moneda
                                index.Data = CInt(ParValue(i)).ToString()
                                index.DataTemp = CInt(ParValue(i)).ToString()
                            Case Else
                                index.Data = ParValue(i).Trim.ToString()
                                index.DataTemp = ParValue(i).Trim.ToString()
                        End Select
                        Exit For


                    End If
                    Trace.WriteLine("No entro en ningun if, el parid es " & ParId(i))
                Next
                Trace.WriteLine("Paso al siguiente indice")
            Next
            Trace.WriteLine("14")

            'Verifico la existencia de todos los archivos, si uno no existe no proceso la linea
            For i = 0 To ParFiles.Count - 1
                If Not IO.File.Exists(ParFiles(i)) Then
                    Throw New Exception("El archivo " & ParFiles(i) & " NO EXISTE")
                End If
            Next

            Dim FlagError As Boolean '
            Dim FlagInserted As Boolean

            'POR CADA FILE EN PARFILES INSERTO UNA VEZ
            For i = 0 To ParFiles.Count - 1
                Trace.WriteLine("Hay " & ParFiles.Count & " archivos")
                Trace.WriteLine(ParFiles(i) & " " & i)
                NewResult.File = ParFiles(i).ToString()
                Trace.WriteLine((15 + i))
                Trace.WriteLine("Voy a insertar el documento")
                NewResult.ID = CoreData.GetNewID(IdTypes.DOCID)
                Trace.WriteLine("Obtengo el DOCID= " & NewResult.ID)
                'Cambiar el Move a True

                Try
                    If Results_Business.InsertDocument(NewResult, False, False, False, False, False) = InsertResult.Insertado Then
                        FlagInserted = CheckResultInserted(NewResult)
                        ' ZServEngine.CheckControl(Linea)
                        'If FlagInserted = True Then SetChecked(Linea.Split("|")(0)) 'Esto quita la linea de la tabla ZexportErrors
                        If FlagInserted = True Then SetChecked(Linea) 'Esto quita la linea de la tabla ZexportErrors
                    Else
                        ZServEngine.LeftAlone(Linea)
                    End If
                    Trace.WriteLine("Inserte el documento")
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                    Trace.WriteLine("ERROR: " & ex.Message)
                    Me.Erroneos += 1
                    FlagError = True
                End Try
            Next

            If FlagError = False AndAlso FlagInserted = True Then
                ZServEngine.CheckControl(Linea)
            End If

            Return NewResult.Name
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Trace.WriteLine("ERROR: " & ex.Message)
            Me.Erroneos += 1
            Return "Error al insertar eMail"
        End Try
        Return Nothing
    End Function
    Private Function CheckResultInserted(ByRef Result As Result) As Boolean
        Dim DocID As Int64 = Result.ID

        Try
            Dim sql As String = "SELECT count(*) FROM DOC" & Result.DocType.ID
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            If (ds.Tables(0).Rows(0).Item(0) > 0) AndAlso System.IO.File.Exists(Result.FullPath) Then

                CheckResultInserted = True
            End If

        Catch ex As Exception

        End Try

    End Function
    Private Shared Sub SetChecked(ByVal line As String)
        Try
            'Dim sql As String = "Delete from ZExportErrors where Codigo='" & Codigo & "'"
            Dim sql As String = "Delete from ZExportErrors where line='" & line & "'"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    '// -----------------------------------------------------------------------------
    '// <summary>
    '// Este metodo guarda en un xml las lineas que el ServerImport no pudo insertar
    '// para ser procesadas despues
    '// </summary>
    '// <remarks></remarks>
    '// '// <history>
    '// 	[Hernan]	24/02/2006	Created
    '// </history>
    '// -----------------------------------------------------------------------------
    Private Shared Sub LeftAlone(ByVal linea As String)
        Try
            Dim ds As New Pendientes
            Dim row As Pendientes.pendientesRow = ds.pendientes.NewpendientesRow
            row.lineas = linea
            ds.Tables(0).Rows.Add(row)
            ds.AcceptChanges()
            ds.WriteXml(System.Windows.Forms.Application.StartupPath & "\Pendientes.xml")
            ds.Dispose()
        Catch
        End Try
    End Sub

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
            Trace.WriteLine("Error al crear el xml por defecto. " & ex.Message)
        End Try
        Return ds
    End Function

    Private Shared Function CargarUserPorDefecto(ByVal StartupPath As String) As UserConfig
        Dim user As New UserConfig
        Try
            Dim row As UserConfig.UserConfigRow = user.UserConfig.NewUserConfigRow
            row.BackupFolder = StartupPath
            row.UserId = 0
            user.UserConfig.Rows.Add(row)
            user.AcceptChanges()
        Catch
        End Try
        Return user
    End Function

#Region "importacion"
    Dim PL As ZProcessLine
    'Public Sub Procesar(ByVal cadena As String)
    '    Lineas.Enqueue(cadena)

    '    While Lineas.Count > 0
    '        Try

    '            Dim linea As String = Quitar()
    '            Try
    '                Trace.WriteLine("linea " & linea)
    '            Catch ex As Exception
    '            End Try

    '            'TODO: Evaluar de usar threads y llamar a un metodo de una instancia unica
    '            Try
    '                Pendientes += 1
    '                '  Me.mnuPendientes.Text = "Pendientes: " & Pendientes
    '                ' Me.Cicon.Icon = Drawing.Icon.FromHandle(New Drawing.Bitmap(Me.Icons.Images(3)).GetHicon)
    '            Catch
    '            End Try

    '            PL = New ZProcessLine(linea)
    '            Dim T1 As New Thread(AddressOf Importar)
    '            T1.Name = "Importacion"
    '            T1.Start()
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '            Trace.WriteLine("Error: " & ex.tostring)
    '        End Try
    '    End While
    'End Sub
    Public Shared Event Status(ByVal Pendientes As Int64, ByVal Importados As Int64, ByVal Erroneos As Int64, ByVal obs As Hashtable)

    'Private Sub Importar()
    '    SyncLock PL
    '        Try
    '            ImportacionLocal = New ImportacionLocal(PL.Cadena)
    '            Importados += 1
    '            Pendientes -= 1
    '            RaiseEvent Status(Pendientes, Importados, Me.Erroneos)
    '            'Me.mnuPendientes.Text = "Pendientes: " & Pendientes
    '            'Me.MnuImportados.Text = "Importados: " & Importados
    '            'Me.Cicon.Icon = Drawing.Icon.FromHandle(New Drawing.Bitmap(Me.Icons.Images(0)).GetHicon)
    '        Catch ex As Threading.ThreadStateException
    '        Catch ex As Threading.ThreadStateException
    '        Catch ex As Threading.ThreadStateException
    '        Catch ex As Threading.ThreadStateException
    '        Catch ex As Exception
    '        Catch
    '        Finally
    '        End Try
    '    End SyncLock
    'End Sub
#End Region
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza el valor a "Insertado"
    ''' </summary>
    ''' <param name="Linea"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Shared Sub CheckControl(ByVal Linea As String)
        Dim sql As String
        Try
            sql = "Update ZexportControl Set Insertado='S' Where Codigo='" & Linea.Split("|")(0).Trim & "'"
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene los indices mapeados de: Para, EnviadoPor, CC, Asunto..
    ''' </summary>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  09/12/2010  Created
    ''' </history>
    Private Shared Function GetMapping() As DataSet
        Try
            Dim sql As String = "SELECT INDEXID FROM LOTUSINDEXMAPPING ORDER BY ORDERP ASC"
            Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, sql)
            Return ds
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function

#Region "Cola de trabajo"
    Private Shared _lineas As New Queue
    Public Shared Property Lineas() As Queue
        Get
            If _lineas Is Nothing Then
                _lineas = New Queue
            End If
            Return _lineas
        End Get
        Set(ByVal Value As Queue)
            _lineas = Value
        End Set
    End Property
    Public Shared Function Quitar() As String
        Return Lineas.Dequeue.ToString()
    End Function
    Public Shared Function AllLines() As Array
        Return Lineas.ToArray()
    End Function
#End Region

    Public Sub New()
        ImportsDirectory = New IO.DirectoryInfo(System.windows.forms.Application.startuppath)
    End Sub
End Class
