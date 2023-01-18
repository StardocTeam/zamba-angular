Imports ILM.LotusLibrary.LotusLibrary
Imports System.Windows.Forms
Imports Zamba.Servers
Imports Zamba.Tools
Imports ILM.LotusLibrary
Imports Zamba
'Imports ODBC_Administrator

''' <summary>
''' Ejecuta los agentes INITIALDATA y EXPMAILS
''' </summary>
''' <remarks>Fuerza la ejecucion del agente de exportacion programada.</remarks>
Public Class Install

    Public Shared Function Instalar() As Boolean
        Try
            If HavetoInstall() = True Then
                Dim C As New ILMInstall
                SetInstalled()
                Return True
            End If
            Return False
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
            Return False
        End Try
    End Function

    Public Shared Sub ReInstalar()
        Try
            If CheckInstalled() = True AndAlso CheckIsFirstTimeofDay() = True Then
                SetFirstTimeofDay()
                ILM.LotusLibrary.LotusLibrary.ExecuteInitialData()
                ILM.LotusLibrary.LotusLibrary.ExecuteProgramedAgent()
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Shared Function HavetoInstall() As Boolean
        Try
            If Not IO.File.Exists(IO.Path.Combine(Membership.MembershipHelper.AppConfigPath, "ILM.Installed.dat")) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
            Return False
        End Try
    End Function

    Private Shared Sub SetInstalled()
        Try
            IO.File.Create(IO.Path.Combine(Membership.MembershipHelper.AppConfigPath, "ILM.Installed.dat"))
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Shared Function CheckInstalled() As Boolean
        Try
            If IO.File.Exists(IO.Path.Combine(Membership.MembershipHelper.AppConfigPath, "ILM.Installed.dat")) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
            Return False
        End Try
    End Function

    Private Shared Sub SetFirstTimeofDay()
        Try
            Dim sw As New IO.StreamWriter(IO.Path.Combine(Membership.MembershipHelper.AppConfigPath, "ILM.Install.dat"))
            sw.AutoFlush = True
            sw.WriteLine(Now.ToString("dd/MM/yyyy"))
            sw.Close()
            sw.Dispose()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Shared Function CheckIsFirstTimeofDay() As Boolean
        Dim sr As IO.StreamReader
        Try
            If IO.File.Exists(IO.Path.Combine(Membership.MembershipHelper.AppConfigPath, "ILM.Install.dat")) = True Then
                sr = New IO.StreamReader(IO.Path.Combine(Membership.MembershipHelper.AppConfigPath, "ILM.Install.dat"))
                Dim strDate As String = sr.ReadLine()
                If strDate Is Nothing Then Return True
                If String.Compare("NI", strDate.ToUpper(), True) <> 0 AndAlso String.Compare(Now.ToString("dd/MM/yyyy"), strDate, True) <> 0 Then
                    Return True
                Else
                    Return False
                End If
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
            Return False
        Finally
            If Not IsNothing(sr) Then
                sr.Dispose()
                sr = Nothing
            End If
        End Try
    End Function

End Class

Public Class ILMInstall

    Public Sub New()
        Trace.Listeners.Add(New TextWriterTraceListener(Membership.MembershipHelper.AppTempPath & "\Exceptions\Trace ILM Setup " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt"))
        Trace.AutoFlush = True
        Try
            CreateODBCConnection()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
        End Try
        Try
            Instalar()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
        End Try
    End Sub



    Private Sub CreateODBCConnection()

        Try

            Dim ODBCFactory As New ODBC

            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                'GENERAR EN SQL SERVER
                ODBCFactory.RutaClave = "SOFTWARE\ODBC\ODBC.INI\ODBC Data Sources"
                ODBCFactory.ClaveNombre = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC SQLSERVER", "OdbcNombre").ToString()
                'Comienzo a generar las claves de registro :
                ODBCFactory.RutaClavesConexion = "SOFTWARE\ODBC\ODBC.INI\" & ODBCFactory.ClaveNombre.ToString

                ODBCFactory.LastUser = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC SQLSERVER", "LastUser", "sa").ToString()
                ODBCFactory.DescriptionSQLServer = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC SQLSERVER", "Descrption", "").ToString()
                ODBCFactory.DriverSQL = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC SQLSERVER", "Driver").ToString()
                ODBCFactory.ServerSQL = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC SQLSERVER", "Server").ToString()
                ODBCFactory.Database = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC SQLSERVER", "Database").ToString()
                ODBCFactory.GenerarODBCSqlServer()
            Else
                'GENERAR EN ORACLE
                ODBCFactory.RutaClave = "SOFTWARE\ODBC\ODBC.INI\ODBC Data Sources"
                '--nombre del ODBC :
                ODBCFactory.ClaveNombre = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "OdbcNombre", "Exporta").ToString()
                'Comienzo a generar las claves de registro :
                ODBCFactory.RutaClavesConexion = "SOFTWARE\ODBC\ODBC.INI\" & ODBCFactory.ClaveNombre.ToString

                ODBCFactory.ApplicationAttributes = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "Application Attributes", "").ToString()
                ODBCFactory.Attributes = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "Attributes", "").ToString()
                ODBCFactory.BatchAutocommitMode = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "BatchAutocommitMode", "").ToString()
                ODBCFactory.CloseCursor = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "CloseCursor", "").ToString()
                '-----------
                ODBCFactory.DescriptionOracle = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "Description", "").ToString()
                ODBCFactory.DisableMTS = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "DisableMTS", "").ToString()
                ODBCFactory.Driver = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "Driver", "").ToString()
                ODBCFactory.DSN = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "DSN", "").ToString()
                ODBCFactory.EXECSchemaOpt = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "EXECSchemaOpt", "").ToString()
                ODBCFactory.EXECSyntax = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "EXECSyntax", "").ToString()
                ODBCFactory.Failover = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "Failover", "").ToString()
                ODBCFactory.FailoverDelay = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "FailoverDelay", "").ToString()
                ODBCFactory.FailoverRetryCount = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "FailoverRetryCount", "").ToString()
                ODBCFactory.ForceWCHAR = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "ForceWCHAR", "").ToString()
                ODBCFactory.Lobs = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "Lobs", "").ToString()
                ODBCFactory.Longs = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "Longs", "").ToString()
                ODBCFactory.MetadataIdDefault = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "MetadataIdDefault", "").ToString()
                ODBCFactory.PrefetchCount = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "PrefetchCount", "").ToString()
                ODBCFactory.QueryTimeout = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "QueryTimeout", "").ToString()
                ODBCFactory.ResultSets = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "ResultSets", "").ToString()
                ODBCFactory.ServerName = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "ServerName", "").ToString()
                ODBCFactory.SQLGetDataExtensions = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "SQLGetData extensions", "").ToString()
                ODBCFactory.TransationOption = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "Translation Option", "").ToString()
                ODBCFactory.TranslationDLL = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "Translation DLL", "").ToString()
                ODBCFactory.UserID = INIClass.ReadIni(Membership.MembershipHelper.AppConfigPath & "\ODBC.ini", "ODBC ORACLE", "UserID", "").ToString()
                ODBCFactory.GenerarODBCOracle()
            End If

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Private Sub Instalar()
        Dim viewnames, docuser, dbnames, NAMES, docnames As Object
        Dim s, mailname As Object
        Dim dbExporta As Object, nAgent As Object

        ' Me conecto a la base de un usuario y ejecuto el initial data,
        Try
            Dim Directorio As String

            Dim Server, User, Location, dataDirectory As String  ',database

            '**Inicializa Lotus
            s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

            '**Obtiene el Server del Notes.ini que usa esta sesion
            dataDirectory = s.GetEnvironmentString("Directory", True)

            If dataDirectory.Trim <> "" Then
                Try
                    CopyDll("C:\Program Files\Lotus\Notes")
                Catch ex As Exception
                    Zamba.AppBlock.ZException.Log(ex)
                End Try
                Try
                    CopyNSF(dataDirectory)
                Catch ex As Exception
                    Zamba.AppBlock.ZException.Log(ex)
                End Try
            End If

            '**Obtiene el Server del Notes.ini que usa esta sesion
            Server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)
            Trace.WriteLine(Server)

            Dim vecServer As String() = Server.Split(",")
            Dim sServer As String = vecServer(0)
            Try
                Trace.WriteLine("Obteniendo Server: " & vecServer(0))
            Catch
            End Try
            Try
                Trace.WriteLine("Obteniendo Server: " & vecServer(1))
            Catch
            End Try

            '**Obtiene la Location del Notes.ini que usa esta sesion.
            '**Esto es para poder sacar el usuario actual debido a que
            '**con el s.UserName a veces viene en blanco.
            Location = ILM.LotusLibrary.LotusLibrary.EnviromentLocation(s)

            Dim vecLocation As String() = Location.Split(",")

            User = vecLocation(2) '**el UserName esta en la 3º posicion.
            Trace.WriteLine("Obteniendo User: " & User)
            '**Obtiene el nombre de la base names desde el Notes.ini que usa esta sesion
            NAMES = ILM.LotusLibrary.LotusLibrary.EnviromentNames(s)
            Trace.WriteLine("Obteniendo NAMES de la base: " & User)
            Dim vecNames As String() = NAMES.Split(",")

            '**Obtiene la base de Names
            dbnames = ILM.LotusLibrary.LotusLibrary.EnviromentServerNames(s, vecNames)

            '**La vista de Users tiene a cada usuario con todas las entradas posibles que pueda tener un usuario
            viewnames = dbnames.GetView("($Users)")
            Trace.WriteLine("Obteniendo view")

            docnames = viewnames.GetDocumentByKey(User, True)

            If Not (dbnames.IsOpen) Then
                dbnames.Open(Server, vecNames(0))
                Trace.WriteLine("Base Abierta")
            End If

            '**La vista de Users tiene a cada usuario con todas las entradas posibles que pueda tener un usuario
            viewnames = dbnames.GetView("($Users)")
            Trace.WriteLine("Obteniendo view")

            docnames = viewnames.GetDocumentByKey(User, True)
            Trace.WriteLine("GetDocumentByKey Ejecutado")

            '**Obtengo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension
            mailname = ILM.LotusLibrary.LotusLibrary.MailName(docnames)

            '**Obtengo la base de mail del usuario agregando la extension
            Dim BaseName As String = mailname(0)
            If BaseName.ToUpper.IndexOf(".NSF") = -1 Then
                BaseName = BaseName + ".nsf"
                Trace.WriteLine("El nombre de la base no fue renombrado")
            End If

            Trace.WriteLine("Nombre de la base= " & BaseName)

            Trace.WriteLine("GetDocumentByKey Ejecutado")

            Directorio = ILM.LotusLibrary.LotusLibrary.EnviromentDirectory(s)

            Trace.WriteLine("Obteniendo User: " & s.CommonUserName)

            Trace.WriteLine("Obteniendo la base de mail")

            'Verifico la existencia del usuario en la USRNOTES, si existe 
            'modifico los datos del registro y si no existe genero
            'un nuevo registro.
            Dim re As business.Registro = New business.Registro()
            Dim userID As Int64 = re.BuscarId(Environment.UserName)
            re.UserId = userID
            re.CONF_PAPELERA = "(TrashExporto)"
            re.NOMBRE = s.commonUserName
            re.ACTIVO = 1
            re.CONF_NOMARCHTXT = "Maestro.TXT"
            re.CONF_BASEMAIL = "Mail\" & BaseName
            re.CONF_EJECUTABLE = "C:\Program Files\zamba software\Zamba.LocalImport.exe"
            re.CONF_PATHARCH = "\\ARBUEAS08\zamba\Mails\" & Environment.UserDomainName & "\"
            re.CONF_MAILSERVER = sServer
            re.CONF_NOMUSERNOTES = s.UserName
            re.CONF_NOMUSERRED = Environment.UserDomainName
            re.CONF_ARCHCTRL = "C:\NotesCtrl.TXT"
            re.CONF_VISTAEXPORTACION = "ExportoNotes"
            re.CONF_SEQMSG = 0
            re.CONF_SEQATT = 0
            re.CONF_LOCKEO = 0
            re.CONF_ACUMIMG = 0
            re.CONF_LIMIMG = 0
            re.CONF_DESTEXT = 10240
            re.CONF_TEXTOSUBJECT = "EXPORTADO A ZAMBA"
            re.CONF_BORRAR = "SI"
            re.CONF_SCHEDULESEL = "1"
            re.CONF_SCHEDULEVAR = "0"
            re.CONF_SEQIMG = 0

            If re.VerificarExistencia(userID) Then
                re.Guardar(re, "Guardar")
            Else
                re.Guardar(re, "Insertar")
            End If

            'Inserto el usuario en la base Exporta
            dbExporta = s.GETDATABASE("", "Exporta.nsf")
            If Not dbExporta Is Nothing Then
                viewnames = dbExporta.GetView("(Registro)")
                If Not viewnames Is Nothing Then
                    docuser = viewnames.GetDocumentByKey(s.CommonUserName, True)
                    If docuser Is Nothing Then
                        docuser = dbExporta.createdocument
                    End If
                    docuser.replaceitemvalue("form", "ConfigFrm")
                    docuser.replaceitemvalue("Conf_NomUserNotes", s.UserName)
                    docuser.replaceitemvalue("Conf_BaseMail", BaseName)
                    docuser.replaceitemvalue("Conf_MailServer", sServer)
                    docuser.save(True, False)
                End If
                'docnames = viewnames.GetDocumentByKey(s.commonusername, True)
                'If Not docnames Is Nothing Then
                nAgent = ILM.LotusLibrary.LotusLibrary.RunAgent(dbExporta, "Initial Data Auto", docuser.noteid)
            End If

            Trace.WriteLine("Creando Documento Para enviar")
            Trace.WriteLine("Mensaje Enviado")
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
            Throw New ArgumentException(ex.ToString)
        Finally

            viewnames = Nothing
            docuser = Nothing
            dbnames = Nothing
            NAMES = Nothing
            docnames = Nothing
            s = Nothing
            mailname = Nothing
            dbExporta = Nothing
            nAgent = Nothing
            Application.Exit()
        End Try
    End Sub

    Private Sub CopyDll(ByVal PathDestino As String)
        Application.DoEvents()
        Try
            If IO.File.Exists(Membership.MembershipHelper.AppConfigPath & "\nAgentIns.dll") Then
                IO.File.Copy(Membership.MembershipHelper.AppConfigPath & "\nAgentIns.dll", PathDestino & "\nAgentIns.dll", True)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine("Error al copiar nAgentIns.dll: " & ex.Message)
        End Try
        Try
            If IO.File.Exists(Membership.MembershipHelper.AppConfigPath & "\lcppn201.dll") Then
                IO.File.Copy(Membership.MembershipHelper.AppConfigPath & "\lcppn201.dll", PathDestino & "\lcppn201.dll", True)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine("Error al copiar lcppn201.dll: " & ex.Message)
        End Try
        Try
            If IO.File.Exists(Membership.MembershipHelper.AppConfigPath & "\rti11al.dll") Then
                IO.File.Copy(Membership.MembershipHelper.AppConfigPath & "\rti11al.dll", PathDestino & "\rti11al.dll", True)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine("Error al copiar rti11al.dll: " & ex.Message)
        End Try
        Try
            If IO.File.Exists(Membership.MembershipHelper.AppConfigPath & "\OdbcLotus.Data") Then
                IO.File.Copy(Membership.MembershipHelper.AppConfigPath & "\OdbcLotus.Data", PathDestino & "\OdbcLotus.Data", True)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine("Error al copiar OdbcLotus.Data: " & ex.Message)
        End Try
    End Sub

    Private Sub CopyNSF(ByVal PathDestino As String)
        Try
            'Do BackUp if old nsf exporta database exist
            If IO.File.Exists(PathDestino & "\Exporta.nsf") Then
                IO.File.Copy(PathDestino & "\Exporta.nsf", PathDestino & "\Exporta" & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".nsf")
            End If

            IO.File.Copy(Membership.MembershipHelper.AppConfigPath & "\Exporta.nsf", PathDestino & "\Exporta.nsf", True)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine("Error al copiar la base Exporta.nsf: " & ex.Message)
        End Try
    End Sub

End Class
