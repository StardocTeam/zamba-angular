Imports Zamba.Tools
Imports System.Threading
Imports Zamba.Core
Imports System.Web.Configuration
Imports System.IO
Imports Zamba

Public Class Server

    Private Sub New()

    End Sub

    Public Shared ConInitialized As Boolean
    Public Shared ConInitializing As Boolean
    Public Sub InitializeConnection(ByVal DB_CONVERT_DATE_FORMAT As String, ByVal DB_CONVERT_DATE_TIME_FORMAT As String)
        Constant.SQLCon._DB_CONVERT_DATE_FORMAT = DB_CONVERT_DATE_FORMAT
        Constant.SQLCon._DB_CONVERT_DATE_TIME_FORMAT = DB_CONVERT_DATE_TIME_FORMAT
    End Sub

    Public Sub New(connectionfile As String)
        _connectionfile = connectionfile
    End Sub

    Public Enum AUTH_TYPES As Integer
        WindowsAuthentication = 1
        SQLServerAuthentication = 2
    End Enum
    Private Enum APPTYPE As SByte
        windows = 0
        web = 1
        winservice = 2
    End Enum

    Event Connected()
    Friend Shared BrokenCounts As Int32

    Private Shared Function Connection(Optional ByVal FlagClose As Boolean = True) As IConnection
        Try
            Dim _Connection As IConnection
            Select Case Server.ServerType
                Case DBTYPES.SyBase
                    'Debug.WriteLine("Base de Datos: " & ConnectionString)
                    _Connection = New SyBaseCon(ConnectionString, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.Oracle
                    ''Debug.WriteLine("Base de Datos: " & ConnectionString)
                    _Connection = New OraCon(ConnectionString, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.Oracle9
                    ''Debug.WriteLine("Base de Datos: " & ConnectionString)
                    _Connection = New OraCon9(ConnectionString, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.OracleClient, DBTYPES.OracleDirect
                    ''Debug.WriteLine("Base de Datos: " & ConnectionString)
                    _Connection = New OraClientCon(ConnectionString, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.OracleManaged
                    ''Debug.WriteLine("Base de Datos: " & ConnectionString)
                    _Connection = New OraManagedCon(ConnectionString, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.OracleODP
                    ''Debug.WriteLine("Base de Datos: " & ConnectionString)
                    _Connection = New OraODP(ConnectionString, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.MSSQLServer
                    ''Debug.WriteLine("Base de Datos: " & ConnectionString)
                    _Connection = New SQLCon(ConnectionString, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.MSSQLServer7Up, DBTYPES.MSSQLExpress
                    _Connection = New SQLCon7(ConnectionString, dbOwner, FlagClose)
                    Return _Connection

                Case DBTYPES.ODBC
                    _Connection = New ODBCCon(ConnectionString, dbOwner, FlagClose)
                    Return _Connection

            End Select
            Return Nothing
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en Base de Datos: " & ConnectionString)
            Zamba.AppBlock.ZException.Log(ex)
            Throw New Exception("No se puede instanciar el motor de la base de datos")
        End Try
    End Function
    Private Shared Function Connection(ByVal servertype As DBTYPES, ByVal connectionstring As String, Optional ByVal FlagClose As Boolean = True) As IConnection
        Try
            Dim _Connection As IConnection
            Select Case servertype
                Case DBTYPES.SyBase
                    ''Debug.WriteLine("Base de Datos: " & connectionstring)
                    _Connection = New SyBaseCon(connectionstring, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.Oracle
                    ''Debug.WriteLine("Base de Datos: " & connectionstring)
                    _Connection = New OraCon(connectionstring, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.Oracle9
                    ''Debug.WriteLine("Base de Datos: " & connectionstring)
                    _Connection = New OraCon9(connectionstring, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.OracleClient, DBTYPES.OracleDirect
                    'Debug.WriteLine("Base de Datos: " & connectionstring)
                    _Connection = New OraClientCon(connectionstring, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.OracleManaged
                    ''Debug.WriteLine("Base de Datos: " & ConnectionString)
                    _Connection = New OraManagedCon(connectionstring, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.OracleODP
                    ''Debug.WriteLine("Base de Datos: " & ConnectionString)
                    _Connection = New OraODP(connectionstring, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.MSSQLServer
                    'Debug.WriteLine("Base de Datos: " & connectionstring)
                    _Connection = New SQLCon(connectionstring, dbOwner, FlagClose)
                    Return _Connection
                Case DBTYPES.MSSQLServer7Up, DBTYPES.MSSQLExpress
                    'Debug.WriteLine("Base de Datos: " & connectionstring)
                    _Connection = New SQLCon7(connectionstring, dbOwner, FlagClose)
                    Return _Connection

                Case DBTYPES.ODBC
                    'Debug.WriteLine("Base de Datos: " & connectionstring)
                    _Connection = New ODBCCon(connectionstring, dbOwner, FlagClose)
                    Return _Connection
            End Select
            Return Nothing
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en Base de Datos: " & connectionstring)
            Zamba.AppBlock.ZException.Log(ex)
            Throw New Exception("No se puede instanciar el motor de la base de datos")
        End Try
    End Function

    Public Function TestConnection() As Boolean
        Try
            Dim d As Integer = Server.Con.ExecuteScalar(CommandType.Text, "select count(1) from usrtable")
            Return True
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function

    Public Shared Function CreateTables() As CreateTables
        Try
            Select Case Server.ServerType
                Case DBTYPES.SyBase
                    Return New SyBaseCreateTables
                Case DBTYPES.Oracle
                    Return New OraCreateTables
                Case DBTYPES.Oracle9
                    Return New Ora9CreateTables
                Case DBTYPES.OracleClient, DBTYPES.OracleDirect
                    Return New OraClientCreateTables
                Case DBTYPES.OracleManaged
                    Return New OraManagedCreateTables
                Case DBTYPES.OracleODP
                    Return New OraODPCreateTables
                Case DBTYPES.MSSQLServer
                    Return New SQLCreateTables
                Case DBTYPES.MSSQLServer7Up, DBTYPES.MSSQLExpress
                    Return New SQL7CreateTables
            End Select
            Return Nothing
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw New Exception("No se puede instanciar el motor de la base de datos")
        End Try
    End Function

    Public Sub MakeConnection(ByVal ServerType As DBTYPES, ByVal DBServer As String, ByVal DB As String, ByVal User As String, ByVal Password As String)
        Try
            If Server.ConInitialized = False AndAlso Server.ConInitializing = False Then Throw New Exception("Conexion no inicializada correctamente")
            MakeConString(ServerType, DBServer, DB, User, Password)
            '             InitializePool()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw New Exception("No se pudo cargar la configuracion actual " & ex.ToString)
        End Try
    End Sub
    Private Sub MakeConString(ByVal ServerType As DBTYPES, ByVal DBServer As String, ByVal DB As String, ByVal User As String, ByVal Password As String)

        Try
            Zamba.Servers.Server.ServerType = ServerType
            Zamba.Servers.Server.DB = DB
            Zamba.Servers.Server.DBUser = User
            Zamba.Servers.Server.DBServer = DBServer
            Zamba.Servers.Server.DBPassword = Password

            Select Case ServerType
                Case DBTYPES.SyBase
                    Server.DBTYPE = "ASAProv.80"
                    Server.ConnectionString = "Provider=" & DBTYPE & "; dbf=" & DB.Trim & "; uid=" & User.Trim & "; pwd=" & Trim(Password)
                Case DBTYPES.Oracle
                    Server.DBTYPE = "MSDAORA.1"
                    Server.ConnectionString = "Provider=" & DBTYPE & ";Password=" & Password & ";User ID=" & User & ";Data Source=" & DB & ";Persist Security Info=True"
                    Server._dbOwner = User
                Case DBTYPES.Oracle9
                    Server.DBTYPE = "OraOLEDB.Oracle"
                    Server.ConnectionString = "Provider=" & DBTYPE & ";Password=" & Password & ";User ID=" & User & ";Data Source=" & DB & ";Persist Security Info=True; OLEDB.NET=true"
                    Server._dbOwner = User
                Case DBTYPES.OracleClient
                    Server.ConnectionString = "Server=" + DB &
                    ";User Id=" + User &
                    ";Password=" + Password & ";Data Source=" & DB & ";Persist Security Info=True"
                    Server._dbOwner = User
                Case DBTYPES.OracleODP
                    Server.ConnectionString = "Data Source=" + DB &
                    ";User Id=" + User &
                    ";Password=" + Password & ";"
                    Server._dbOwner = User
                Case DBTYPES.OracleManaged
                    If DBServer.ToUpper().Contains("DESCRIPTION") Then
                        Server.ConnectionString = "Data Source=" + DBServer &
                    ";User Id=" + User &
                    ";Password=" + Password & ";" &
                       " Pooling = True; Min Pool Size=2; Max Pool Size=100; Connection Timeout=30;"
                        Server._dbOwner = User
                    Else
                        Server.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=" & DBServer & ") (PORT=1521)) (CONNECT_DATA= (SID=" & DB & ")));User ID=" & User & ";Password=" & Password &
                            " Pooling = True; Min Pool Size=2; Max Pool Size=100; Connection Timeout=30;"
                        Server._dbOwner = User
                    End If
                Case DBTYPES.OracleDirect
                    Server.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=" & DBServer & ") (PORT=1521)) (CONNECT_DATA= (SID=" & DB & ")));User ID=" & User & ";Password=" & Password
                    Server._dbOwner = User
                Case DBTYPES.MSSQLServer
                    Server.DBTYPE = "SQLOLEDB.1"
                    Server.ConnectionString = "Password=" & Password & ";Persist Security Info=True;User ID=" & User & ";In" &
                    "itial Catalog=" & DB & ";Data Source=" & DBServer & ";Use Procedure for Prepare=1;Auto Translate=" &
                    "True;Packet Size=4096;Use Encryption for Data=False;Tag wit" &
                    "h column collation when possible=False;Connection Timeout=" & AppConfig.TIMEOUT
                    If User.ToUpper = "SA" Then
                        Server._dbOwner = "dbo"
                    Else
                        Server._dbOwner = User
                    End If

                    '"True;Packet Size=4096;Workstation ID=" & StationId & ";Use Encryption for Data=False;Tag wit" & _

                Case DBTYPES.MSSQLServer7Up, DBTYPES.MSSQLExpress
                    Server.DBTYPE = "SQLOLEDB.1"
                    Server.ConnectionString = "Password=" & Password & ";User ID=" & User & ";Initial Catalog=" & DB & ";Data Source=" & DBServer & ";Connection Timeout=" & AppConfig.TIMEOUT
                    'Server.ConnectionString = "Password=" & Password & ";User ID=" & User & ";Initial Catalog=" & DB & ";Data Source=" & DBServer & ";Workstation ID=" & StationId
                    If User.ToUpper = "SA" Then
                        Server._dbOwner = "dbo"
                    Else
                        Server._dbOwner = User
                    End If

                Case DBTYPES.ODBC
                    Server.ConnectionString = "DSN=" & DBServer & ";Uid=" & User & ";Pwd=" & Password & ";"
            End Select
            RaiseEvent Connected()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw ex
        End Try
    End Sub
    Private Shared Function MakeConnectionString(ByVal ServerType As DBTYPES, ByVal DBServer As String, ByVal DB As String, ByVal User As String, ByVal Password As String) As String

        Try
            Dim ConnectionString As String = ""

            If User.ToLower = "na" And Password.ToLower = "na" Then
                Select Case ServerType
                    Case DBTYPES.Oracle
                        DBTYPE = "MSDAORA.1"
                        ConnectionString = "Integrated Security=SSPI;" & "Provider=" & DBTYPE & ";Data Source=" & DB & ";Persist Security Info=True"
                        _dbOwner = User
                    Case DBTYPES.Oracle9
                        DBTYPE = "OraOLEDB.Oracle"
                        ConnectionString = "Integrated Security=SSPI;" & "Provider=" & DBTYPE & ";Data Source=" & DB & ";Persist Security Info=True; OLEDB.NET=true;"
                        _dbOwner = User
                    Case DBTYPES.OracleClient
                        ConnectionString = "Integrated Security=SSPI;" & "Server=" + DB &
                        ";Data Source=" & DB & ";Persist Security Info=True;"
                        _dbOwner = User
                    Case DBTYPES.OracleODP
                        ConnectionString = "Data Source=" + DB &
                    ";User Id=" + User &
                    ";Password=" + Password & ";"
                        _dbOwner = User
                    Case DBTYPES.OracleDirect
                        ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=" & DBServer & ") (PORT=1521)) (CONNECT_DATA= (SID=" & DB & ")));User ID=" & User & ";Password=" & Password
                        _dbOwner = User
                    Case DBTYPES.OracleManaged
                        If DBServer.ToUpper().Contains("DESCRIPTION") Then
                            ConnectionString = "Data Source=" + DBServer &
                    ";User Id=" + User &
                    ";Password=" + Password & ";" &
                       " Pooling = True; Min Pool Size=2; Max Pool Size=100; Connection Timeout=30;"
                            _dbOwner = User
                        Else
                            ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=" & DBServer & ") (PORT=1521)) (CONNECT_DATA= (SID=" & DB & ")));User ID=" & User & ";Password=" & Password &
                       " Pooling = True; Min Pool Size=2; Max Pool Size=100; Connection Timeout=30;"
                            _dbOwner = User
                        End If
                    Case DBTYPES.MSSQLServer
                        DBTYPE = "SQLOLEDB.1"
                        ConnectionString = "Integrated Security=SSPI; Initial Catalog=" & DB & ";Data Source=" & DBServer & ";Packet Size=4096;Use Encryption for Data=False;Tag wit" & "h column collation when possible=False"

                        If User.ToUpper = "SA" Then
                            _dbOwner = "dbo"
                        Else
                            _dbOwner = User
                        End If

                    Case DBTYPES.MSSQLServer7Up, DBTYPES.MSSQLExpress
                        DBTYPE = "SQLOLEDB.1"
                        ConnectionString = "Integrated Security=SSPI; Initial Catalog=" & DB & ";Data Source=" & DBServer & ";"

                    Case DBTYPES.ODBC
                        ConnectionString = "DSN=" & DBServer & ";Uid=" & User & ";Pwd=" & Password & ";"
                End Select
            Else
                Select Case ServerType
                    Case DBTYPES.SyBase
                        ConnectionString = "Provider=" & DBTYPE & "; dbf=" & DB.Trim & "; uid=" & User.Trim & "; pwd=" & Trim(Password)
                    Case DBTYPES.Oracle
                        ConnectionString = "Provider=" & DBTYPE & ";Password=" & Password & ";User ID=" & User & ";Data Source=" & DB & ";Persist Security Info=True"
                    Case DBTYPES.OracleODP
                        ConnectionString = "Provider=" & DBTYPE & ";Password=" & Password & ";User ID=" & User & ";Data Source=" & DB & ";"
                    Case DBTYPES.Oracle9
                        ConnectionString = "Provider=" & DBTYPE & ";Password=" & Password & ";User ID=" & User & ";Data Source=" & DB & ";Persist Security Info=True; OLEDB.NET=true"
                    Case DBTYPES.OracleClient
                        ConnectionString = "Server=" + DB &
                        ";User Id=" + User &
                        ";Password=" + Password & ";Data Source=" & DB & ";Persist Security Info=True"
                    Case DBTYPES.OracleDirect
                        ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=" & DBServer & ") (PORT=1521)) (CONNECT_DATA= (SID=" & DB & ")));User ID=" & User & ";Password=" & Password
                    Case DBTYPES.OracleManaged
                        If DBServer.ToUpper().Contains("DESCRIPTION") Then
                            ConnectionString = "Data Source=" + DBServer &
                    ";User Id=" + User &
                    ";Password=" + Password & ";" &
                       " Pooling = True; Min Pool Size=2; Max Pool Size=100; Connection Timeout=30;"
                            _dbOwner = User
                        Else
                            ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=" & DBServer & ") (PORT=1521)) (CONNECT_DATA= (SID=" & DB & ")));User ID=" & User & ";Password=" & Password &
                       " Pooling = True; Min Pool Size=2; Max Pool Size=100; Connection Timeout=30;"
                            _dbOwner = User
                        End If
                    Case DBTYPES.MSSQLServer
                        ConnectionString = "Password=" & Password & ";Persist Security Info=True;User ID=" & User & ";In" &
                        "itial Catalog=" & DB & ";Data Source=" & DBServer & ";Use Procedure for Prepare=1;Auto Translate=" &
                        "True;Packet Size=4096;Use Encryption for Data=False;Tag wit" &
                        "h column collation when possible=False;Connection Timeout=" & AppConfig.TIMEOUT
                    Case DBTYPES.MSSQLServer7Up, DBTYPES.MSSQLExpress
                        ConnectionString = "Password=" & Password & ";User ID=" & User & ";Initial Catalog=" & DB & ";Data Source=" & DBServer & ";Connection Timeout=" & AppConfig.TIMEOUT
                    Case DBTYPES.ODBC
                        ConnectionString = "DSN=" & DBServer & ";Uid=" & User & ";Pwd=" & Password & ";"
                End Select
            End If

            Return ConnectionString
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw ex
        End Try
    End Function

    Public Sub MakeConnection()
        Try
            If Server.ConInitialized = False AndAlso Server.ConInitializing = False Then Throw New Exception("Conexion no Inicializada correctamente")
            'ZTrace.WriteLineIf(ZTrace.IsInfo, "Inicializando Coneccion")
            MakeConString()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw New Exception("No se pudo cargar la configuracion actual " & ex.ToString)
        End Try
    End Sub

    Private Shared _appConfig As ApplicationConfig

    Private Sub MakeConString()
        ' ZTrace.WriteLineIf(ZTrace.IsInfo, "Entro al makeconstring")


        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Instancio el AppConfig")
            If (AppConfig.SERVERTYPE <> DBTYPES.SinDefinir) Then
                Zamba.Servers.Server.ServerType = AppConfig.SERVERTYPE
                ZTrace.WriteLineIf(ZTrace.IsInfo, "ServerType= " & Server.ServerType)

                Zamba.Servers.Server.DB = AppConfig.DB
                Zamba.Servers.Server.DBUser = AppConfig.USER
                Zamba.Servers.Server.DBServer = AppConfig.SERVER
                Zamba.Servers.Server.DBPassword = AppConfig.PASSWORD
                ZTrace.WriteLineIf(ZTrace.IsInfo, "DB= " & Zamba.Servers.Server.DB)

                If AppConfig.WIN_AUTHENTICATION = False Then
                    Select Case AppConfig.SERVERTYPE
                        Case DBTYPES.SyBase
                            Server.DBTYPE = "ASAProv.80"
                            Server.ConnectionString = "Provider=" & DBTYPE & "; dbf=" & AppConfig.DB.Trim & "; uid=" & Trim(AppConfig.USER) & "; pwd=" & Trim(AppConfig.PASSWORD)
                        Case DBTYPES.Oracle
                            Server.DBTYPE = "MSDAORA.1"
                            Server.ConnectionString = "Provider=" & DBTYPE & ";Password=" & AppConfig.PASSWORD & ";User ID=" & AppConfig.USER & ";Data Source=" & AppConfig.DB & ";Persist Security Info=True"
                            Server._dbOwner = AppConfig.USER
                        Case DBTYPES.Oracle9
                            Server.DBTYPE = "OraOLEDB.Oracle"
                            Server.ConnectionString = "Provider=" & DBTYPE & ";Password=" & AppConfig.PASSWORD & ";User ID=" & AppConfig.USER & ";Data Source=" & AppConfig.DB & ";Persist Security Info=True; OLEDB.NET=true"
                            Server._dbOwner = AppConfig.USER
                        Case DBTYPES.OracleClient
                            If DBServer.ToUpper().Contains("DESCRIPTION") Then
                                Server.ConnectionString = "Data Source=" + DBServer & ";User Id=" + AppConfig.USER & ";Password=" + AppConfig.PASSWORD & ";"
                                Server._dbOwner = AppConfig.USER
                            Else
                                Server.ConnectionString = "Server=" + AppConfig.DB & ";User Id=" + AppConfig.USER &
                           ";Password=" + AppConfig.PASSWORD & ";Data Source=" & AppConfig.DB & ";Persist Security Info=True"
                                Server._dbOwner = AppConfig.USER
                            End If
                        Case DBTYPES.OracleODP
                            Server.ConnectionString = "Data Source=" + AppConfig.DB &
                           ";User Id=" + AppConfig.USER &
                           ";Password=" + AppConfig.PASSWORD & ";"
                            Server._dbOwner = AppConfig.USER
                        Case DBTYPES.OracleDirect
                            Server.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=" & AppConfig.SERVER & ") (PORT=1521)) (CONNECT_DATA= (SID=" & AppConfig.DB & ")));User ID=" & AppConfig.USER & ";Password=" & AppConfig.PASSWORD
                            Server._dbOwner = AppConfig.USER
                        Case DBTYPES.OracleManaged
                            If DBServer.ToUpper().Contains("DESCRIPTION") Then
                                Server.ConnectionString = "Data Source=" + DBServer & ";User Id=" + AppConfig.USER & ";Password=" + AppConfig.PASSWORD & ";" &
                       " Pooling = True; Min Pool Size=2; Max Pool Size=100; Connection Timeout=30;"
                                Server._dbOwner = AppConfig.USER
                            Else
                                Server.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=" & DBServer & ") (PORT=1521)) (CONNECT_DATA= (SID=" & DB & ")));User ID=" & AppConfig.USER & ";Password=" & AppConfig.PASSWORD &
                       " Pooling = True; Min Pool Size=2; Max Pool Size=100; Connection Timeout=30;"
                                Server._dbOwner = AppConfig.USER
                            End If
                        Case DBTYPES.MSSQLServer
                            Server.DBTYPE = "SQLOLEDB.1"
                            Server.ConnectionString = "Password=" & AppConfig.PASSWORD & ";Persist Security Info=True;User ID=" & AppConfig.USER & ";In" &
                            "itial Catalog=" & AppConfig.DB & ";Data Source=" & AppConfig.SERVER & ";Use Procedure for Prepare=1;Auto Translate=" &
                            "True;Packet Size=4096;Use Encryption for Data=False;Tag wit" &
                            "h column collation when possible=False;Connection Timeout=" & AppConfig.TIMEOUT

                            If AppConfig.USER.ToUpper = "SA" Then
                                Server._dbOwner = "dbo"
                            Else
                                Server._dbOwner = AppConfig.USER
                            End If

                        Case DBTYPES.MSSQLServer7Up, DBTYPES.MSSQLExpress
                            Server.DBTYPE = "SQLOLEDB.1"
                            Server.ConnectionString = "Password=" & AppConfig.PASSWORD & ";User ID=" & AppConfig.USER & ";Initial Catalog=" & AppConfig.DB & ";Data Source=" & AppConfig.SERVER & ";Connection Timeout=" & AppConfig.TIMEOUT
                            If AppConfig.USER.ToUpper = "SA" Then
                                Server._dbOwner = "dbo"
                            Else
                                Server._dbOwner = AppConfig.USER
                            End If

                        Case DBTYPES.ODBC
                            ConnectionString = "DSN=" & DBServer & ";Uid=" & AppConfig.USER & ";Pwd=" & AppConfig.PASSWORD & ";"
                    End Select
                Else
                    Select Case AppConfig.SERVERTYPE
                        Case DBTYPES.Oracle
                            Server.DBTYPE = "MSDAORA.1"
                            Server.ConnectionString = "Integrated Security=SSPI;" & "Provider=" & DBTYPE + ";Data Source=" & AppConfig.DB & ";Persist Security Info=True"
                            Server._dbOwner = AppConfig.USER
                        Case DBTYPES.Oracle9
                            Server.DBTYPE = "OraOLEDB.Oracle"
                            Server.ConnectionString = "Integrated Security=SSPI;" & "Provider=" & DBTYPE & ";Data Source=" & AppConfig.DB & ";Persist Security Info=True; OLEDB.NET=true;"
                            Server._dbOwner = AppConfig.USER
                        Case DBTYPES.OracleClient
                            Server.ConnectionString = "Integrated Security=SSPI;" & "Server=" + AppConfig.DB & ";Data Source=" & AppConfig.DB & ";Persist Security Info=True;"
                            Server._dbOwner = AppConfig.USER
                        Case DBTYPES.OracleODP
                            Server.ConnectionString = "Integrated Security=SSPI;" & "Data Source=" + AppConfig.DB & ";"
                            Server._dbOwner = AppConfig.USER
                        Case DBTYPES.OracleDirect
                            Server.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=" & AppConfig.SERVER & ") (PORT=1521)) (CONNECT_DATA= (SID=" & AppConfig.DB & ")));User ID=" & AppConfig.USER & ";Password=" & AppConfig.PASSWORD
                            Server._dbOwner = AppConfig.USER
                        Case DBTYPES.OracleManaged
                            If DBServer.ToUpper().Contains("DESCRIPTION") Then
                                Server.ConnectionString = "Data Source=" + DBServer & ";User Id=" + AppConfig.USER & ";Password=" + AppConfig.PASSWORD & ";" &
                       " Pooling = True; Min Pool Size=2; Max Pool Size=100; Connection Timeout=30;"
                                Server._dbOwner = AppConfig.USER
                            Else
                                Server.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=" & DBServer & ") (PORT=1521)) (CONNECT_DATA= (SID=" & DB & ")));User ID=" & AppConfig.USER & ";Password=" & AppConfig.PASSWORD &
                       " Pooling = True; Min Pool Size=2; Max Pool Size=100; Connection Timeout=30;"
                                Server._dbOwner = AppConfig.USER
                            End If
                        Case DBTYPES.MSSQLServer
                            Server.DBTYPE = "SQLOLEDB.1"
                            Server.ConnectionString = "Integrated Security=SSPI; Initial Catalog=" & AppConfig.DB & ";Data Source=" & AppConfig.SERVER & ";Connection Timeout=" & AppConfig.TIMEOUT & "True;Packet Size=4096;Use Encryption for Data=False;Tag wit" & "h column collation when possible=False"

                            If AppConfig.USER.ToUpper = "SA" Then
                                Server._dbOwner = "dbo"
                            Else
                                Server._dbOwner = AppConfig.USER
                            End If

                        Case DBTYPES.MSSQLServer7Up, DBTYPES.MSSQLExpress
                            Server.DBTYPE = "SQLOLEDB.1"
                            Server.ConnectionString = "Integrated Security=SSPI; Initial Catalog=" & AppConfig.DB & ";Data Source=" & AppConfig.SERVER & ";Connection Timeout=" & AppConfig.TIMEOUT

                        Case DBTYPES.ODBC
                            ConnectionString = "DSN=" & DBServer & ";Uid=" & AppConfig.USER & ";Pwd=" & AppConfig.PASSWORD & ";"
                    End Select
                End If

                RaiseEvent Connected()
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw ex
        Finally
            AppConfig.dispose()
            AppConfig = Nothing
        End Try
    End Sub


    Public Shared Event ConnectionTerminated()

    ''' <summary>
    ''' Maneja la ruptura de la conexion
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConnectionIsBroken(ByVal ex As Exception) As IConnection
        Try
            BrokenCounts += 1

            'Si nunca se corto la conexion
            If BrokenCounts <> 1 Then
                If BrokenCounts > 3 AndAlso Membership.MembershipHelper.ClientType <> Core.ClientType.Service Then
                    ConInitialized = False
                    Throw New ZConnectionException(ex, "Err: 101. No se puede conectar con la Base de Datos, por favor contacte a su administrador del sistema")
                Else
                    If Membership.MembershipHelper.ClientType = Core.ClientType.Desktop Or Membership.MembershipHelper.ClientType = Core.ClientType.Undefined AndAlso Server.ConInitialized = True AndAlso Server.ConInitializing = False Then
                        Dim WP As FrmWaitConnection
                        WP = New FrmWaitConnection(ex)

                        If WP.ShowDialog() = DialogResult.Cancel Then
                            ConInitialized = False
                            WP.Dispose()
                            WP = Nothing
                            Throw New ZConnectionException(ex, "Err: 101. No se puede conectar con la Base de Datos, por favor contacte a su administrador del sistema")
                        End If
                        WP.Dispose()
                        WP = Nothing
                    Else
                        If Server.ConInitialized = False AndAlso Server.ConInitializing = True Then
                            ConInitialized = False
                            Throw New ZConnectionException(ex, "Err: 101. No se puede conectar con la Base de Datos, por favor contacte a su administrador del sistema")
                        End If

                        If BrokenCounts < 20 Then
                            Thread.Sleep(BrokenCounts * 30000)
                        Else
                            BrokenCounts = 1
                            Zamba.AppBlock.ZException.Log(ex)
                        End If
                    End If
                End If

            End If
            'Instancio una nueva conexion
            Dim _Server As New Server
            _Server.MakeConnection()
            _Server.dispose()
            Return Server.Con
        Catch ex1 As Exception
            RaiseEvent ConnectionTerminated()
            Throw New ZConnectionException(ex1, "Err: 101. No se puede conectar con la Base de Datos, por favor contacte a su administrador del sistema")

        End Try
        Return Nothing
    End Function
    'falta implementar el dispose
    Public Sub dispose()
    End Sub

#Region "Properties"
    Private Shared _ServerType As DBTYPES = DBTYPES.SinDefinir
    Public Shared Property ServerType() As DBTYPES
        Get
            If _ServerType = DBTYPES.SinDefinir Then
                Dim s As New Zamba.Servers.Server
                s.MakeConnection()
                s.dispose()
            End If
            Return _ServerType
        End Get
        Set(ByVal Value As DBTYPES)
            _ServerType = Value
        End Set
    End Property
    Public Shared DB As String
    Public Shared DBServer As String
    Public Shared DBUser As String
    Public Shared DBPassword As String
    Private Shared DBTYPE As String
    Private Shared ConnectionString As String
    Public Shared U_Time As Date = Now

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna True si el servidor al que se est� conectado es del tipo Oracle
    ''' </summary>
    ''' -----------------------------------------------------------------------------
    Public Shared ReadOnly Property isOracle() As Boolean
        Get
            If Not ServerType = DBTYPES.SinDefinir AndAlso (ServerType = DBTYPES.Oracle OrElse ServerType = DBTYPES.Oracle9 OrElse ServerType = DBTYPES.OracleClient OrElse ServerType = DBTYPES.OracleDirect OrElse ServerType = DBTYPES.OracleManaged OrElse ServerType = DBTYPES.OracleODP) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna True si el servidor al que se est� conectado es del tipo SQL Server
    ''' </summary>
    ''' -----------------------------------------------------------------------------
    Public Shared ReadOnly Property isSQLServer() As Boolean
        Get
            If ServerType = DBTYPES.MSSQLServer OrElse ServerType = DBTYPES.MSSQLServer7Up OrElse ServerType = DBTYPES.MSSQLExpress Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna True si el servidor al que se est� conectado es del tipo ODBC
    ''' </summary>
    ''' -----------------------------------------------------------------------------
    Public Shared ReadOnly Property isODBC() As Boolean
        Get
            If ServerType = DBTYPES.ODBC Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Private Shared _dbOwner As String
    Public Shared ReadOnly Property dbOwner() As String
        Get
            Return _dbOwner
        End Get
    End Property
    Public Enum SqlConnectionOwnership
        'Connection is owned and managed by SqlHelper
        Internal
        'Connection is owned and managed by the caller
        [External]
    End Enum 'SqlConnectionOwnership

    Private Shared _conn As IConnection
    Private Shared _connections As New ArrayList
    Private Shared ConIndex As Int16
    Private Shared MaxCons As Int16 = 1


    Public Shared ReadOnly Property Con(ByVal ServerType As DBTYPES, ByVal DBServer As String, ByVal DB As String, ByVal User As String, ByVal Password As String, Optional ByVal Forced As Boolean = False, Optional ByVal FlagClose As Boolean = True) As IConnection
        Get
            If ServerType = DBTYPES.ODBC Then
                Dim ConString As String = Server.MakeConnectionString(ServerType, DBServer, DB, User, Password)
                Dim connection As IConnection

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando nueva conexion ODBC")
                connection = Server.Connection(ServerType, ConString, False)
                connection.Open()


                Return connection
            Else
                Dim ConString As String = Server.MakeConnectionString(ServerType, DBServer, DB, User, Password)
                Dim Connection As IConnection = Server.Connection(ServerType, ConString, FlagClose)
                Connection.Open()
                Return Connection
            End If
        End Get
    End Property



    Public Shared ReadOnly Property Con(Optional ByVal FlagClose As Boolean = True) As IConnection
        Get
            If ConInitialized OrElse ConInitializing Then
                Return Server.Connection(FlagClose)
            Else
                Throw New Exception("Conexion no inicializada correctamente")
            End If

        End Get
    End Property

    Public Shared _isAppConfigLoaded As Boolean

    Private Shared _connectionfile As String
    Public Shared Property AppConfig() As ApplicationConfig
        Get
            If _appConfig Is Nothing Then _appConfig = New ApplicationConfig(_connectionfile)
            Return _appConfig
        End Get
        Set(ByVal value As ApplicationConfig)
            _appConfig = value
        End Set
    End Property

    Public Shared Event SessionTimeOut()
#End Region


    Public Shared _currentfile As String
    Public Shared Function currentfile(ByVal Optional IsService As Boolean = False) As String
        Dim File As IO.FileInfo

        If Not IsNothing(_currentfile) AndAlso _currentfile.Length > 0 Then
            Try
                File = New FileInfo(_currentfile)
            Catch
            End Try
        End If

        If IsService And IsNothing(File) Then
            ''Release
            File = GetConexionFileFromRelease(File)

            If IsNothing(File) OrElse Not File.Exists Then
                ''AppData
                File = GetConexionFileFromAppData(File)
                ''LastUsed
                If IsNothing(File) OrElse Not File.Exists Then
                    File = GetConexionFileFromLastUsed(File)
                End If
            End If
        ElseIf IsNothing(File) Then
            ''LastUsed
            File = GetConexionFileFromLastUsed(File)
            ''AppData
            If IsNothing(File) OrElse Not File.Exists Then
                File = GetConexionFileFromAppData(File)
                ''Release
                If IsNothing(File) OrElse Not File.Exists Then
                    File = GetConexionFileFromRelease(File)
                End If
            End If
        End If


        If IsNothing(File) OrElse Not File.Exists Then
            Try
                Dim path As String = ReadWebConfig()
            Catch
                File = New FileInfo(".\app.ini")
            End Try

            If IsNothing(File) OrElse Not File.Exists Then
                Throw New Exception("No se pudo encontrar un archivo de configuracion de conexion valido (app.ini)")
            End If
        End If

        _currentfile = File.FullName
        Return _currentfile

    End Function

    Private Shared Function GetConexionFileFromLastUsed(File As FileInfo) As FileInfo
        If IsNothing(File) OrElse Not File.Exists Then
            Try
                Dim LastUsedConfigFile As String = GetLastUsedConfigFile()
                If String.IsNullOrEmpty(LastUsedConfigFile) = False Then
                    File = New FileInfo(LastUsedConfigFile)
                    If File.Exists = False Then
                        File = Nothing
                    End If
                End If
            Catch ex As Exception
            End Try
        End If

        Return File
    End Function

    Private Shared Function GetConexionFileFromAppData(File As FileInfo) As FileInfo
        If IsNothing(File) OrElse Not File.Exists Then
            Try
                File = New FileInfo(Membership.MembershipHelper.AppConfigPath & "\app.ini")
            Catch
            End Try

            Try
                File = New FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\app.ini")
            Catch
            End Try
            If IsNothing(File) OrElse Not File.Exists Then
                Try
                    Dim FileInis As String() = Directory.GetFiles(Membership.MembershipHelper.AppConfigPath, "*app*.ini")
                    For Each Fi As String In FileInis
                        If Fi.ToLower.Contains("appiniserverpath.ini") = False Then
                            File = New FileInfo(Fi)
                            Exit For
                        End If
                    Next
                Catch ex As Exception
                End Try
            End If
        End If

        Return File
    End Function

    Private Shared Function GetConexionFileFromRelease(File As FileInfo) As FileInfo
        If IsNothing(File) OrElse Not File.Exists Then
            Try
                File = New FileInfo(Application.StartupPath & "\app.ini")
            Catch
                File = New FileInfo(".\app.ini")
            End Try
            If IsNothing(File) OrElse Not File.Exists Then
                Try
                    Dim path As String = ReadWebConfig()
                Catch
                    File = New FileInfo(".\app.ini")
                End Try

                If IsNothing(File) OrElse Not File.Exists Then
                    Try
                        Dim FileInis As String() = Directory.GetFiles(Application.StartupPath, "*app*.ini")
                        For Each Fi As String In FileInis
                            If Fi.ToLower.Contains("appiniserverpath.ini") = False Then
                                File = New FileInfo(Fi)
                                Exit For
                            End If
                        Next
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If
        Return File
    End Function

    Public Shared Function GetLastUsedConfigFile() As String
        Try
            Dim File As New FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\LastUsedConfigFile.ini")
            If (File.Exists) Then
                Dim sr As New StreamReader(File.FullName)
                Dim LastUsedFileName As String = sr.ReadToEnd
                sr.Close()
                sr.Dispose()
                Return LastUsedFileName
            End If
        Catch ex As Exception

        End Try
        Return String.Empty
    End Function

    Private Shared Function ReadWebConfig() As String
        Dim file As String

        If WebConfigurationManager.ConnectionStrings.Item("Zamba") IsNot Nothing Then
            Dim webFile As String = WebConfigurationManager.ConnectionStrings.Item("Zamba").ElementInformation.Source
            Dim fi As New FileInfo(webFile)
            file = fi.Directory.FullName & "\app.ini"
        End If

        Return file
    End Function




End Class