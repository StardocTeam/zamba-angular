Imports System
Imports Microsoft.Win32
Public Class ODBC

    Public ClaveNombre As String
    '( ruta donde crear la clave de registro) (predeterminado de Windows)
    ' Private rutaClave As String = "SOFTWARE\ODBC\ODBC.INI\ODBC Data Sources" & "\" & Me.ClaveNombre
    '( ruta con los datos de conexion)

    'Private rutaClavesConexion As String = "SOFTWARE\ODBC\ODBC.INI\" & Me.ClaveNombre
    Private clave As RegistryKey '= Registry.LocalMachine.OpenSubKey(Me.RutaClave, True)
    Private claveConexion As RegistryKey ' = Registry.LocalMachine.OpenSubKey(RutaClavesConexion)

    Private mVarRutaClave As String
    Private mVarRutaClavesConexion As String

    '----Variables miembro para Oracle
    Private mVarApplicationAttributes As String
    Private mVarAttributes As String
    Private mVarBatchAutocommitMode As String
    Private mVarCloseCursor As String
    Private mVarDescriptionOracle As String
    Private mVarDisableMTS As String
    Private mVarDriver As String
    Private mVarDSN As String
    Private mVarEXECSchemaOpt As String
    Private mVarEXECSyntax As String
    Private mVarFailover As String
    Private mVarFailoverDelay As String
    Private mVarFailoverRetryCount As String
    Private mVarForceWCHAR As String
    Private mVarLobs As String
    Private mVarLongs As String
    Private mVarMetadataIdDefault As String
    Private mVarmPassword As String
    Private mVarPrefetchCount As String
    Private mVarQueryTimeout As String
    Private mVarResultSets As String
    Private mVarServerName As String
    Private mVarSQLGetDataExtensions As String
    Private mVarTranslationDLL As String
    Private mVarTransationOption As String
    Private mVarUserID As String
    '----Variables miembro para SQL Server
    Private mVarDatabase As String
    Private mVarDescriptionSQLServer As String
    Private mVarDriverSQL As String
    Private mVarLastUser As String
    Private mVarServerSQL As String

    Public Sub GenerarODBCOracle()
        Try
            ' clave = Registry.LocalMachine.OpenSubKey(Me.RutaClave, True)
            clave = Registry.LocalMachine.CreateSubKey(Me.RutaClave) ' crea la carpeta
            clave.SetValue(Me.ClaveNombre, "Oracle en OraHome92") 'le asigna clave y valor


            claveConexion = Registry.LocalMachine.CreateSubKey(Me.RutaClavesConexion)

            'ingresar todas las claves para la conexion PARA ORACLE
            claveConexion.SetValue("ApplicationAttributes", Me.ApplicationAttributes)
            claveConexion.SetValue("Attributes", Me.Attributes)
            claveConexion.SetValue("BatchAutocommitMode ", Me.BatchAutocommitMode)
            claveConexion.SetValue("CloseCursor", Me.CloseCursor)
            claveConexion.SetValue("Description", Me.DescriptionOracle)
            claveConexion.SetValue("DisableMTS", Me.DisableMTS)
            claveConexion.SetValue("Driver", Me.Driver)
            claveConexion.SetValue("DSN", Me.DSN)
            claveConexion.SetValue("EXECSchemaOpt", Me.EXECSchemaOpt)
            claveConexion.SetValue("EXECSyntax", Me.EXECSyntax)
            claveConexion.SetValue("Failover", Me.Failover)
            claveConexion.SetValue("FailoverDelay", Me.FailoverDelay)
            claveConexion.SetValue("FailoverRetryCount", Me.FailoverRetryCount)
            claveConexion.SetValue("ForceWCHAR", Me.ForceWCHAR)
            claveConexion.SetValue("Lobs", Me.Lobs)
            claveConexion.SetValue("Longs", Me.Longs)
            claveConexion.SetValue("MetadataIdDefault", Me.MetadataIdDefault)
            claveConexion.SetValue("Password", "") '--En el registro se guarda como vacia, luego valida el servidor
            claveConexion.SetValue("PrefetchCount", Me.PrefetchCount)
            claveConexion.SetValue("QueryTimeout", Me.QueryTimeout)
            claveConexion.SetValue("ResultSets", Me.ResultSets)
            claveConexion.SetValue("ServerName", Me.ServerName)
            claveConexion.SetValue("SQLGetData extensions", Me.SQLGetDataExtensions)
            claveConexion.SetValue("Translation DLL", Me.TranslationDLL)
            claveConexion.SetValue("Translation Option", Me.TransationOption)
            claveConexion.SetValue("UserID", Me.UserID)

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub
    Public Sub GenerarODBCSqlServer()
        Try
            'claveConexion = Registry.LocalMachine.CreateSubKey(System.Convert.ToString(RutaClavesConexion))
            clave = Registry.LocalMachine.CreateSubKey(Me.RutaClave) ' crea la carpeta
            clave.SetValue(Me.ClaveNombre, "SQL Server")
            claveConexion = Registry.LocalMachine.CreateSubKey(Me.RutaClavesConexion)
            'claves y valores para SQL SERVER
            claveConexion.SetValue("Database", Me.Database)
            claveConexion.SetValue("Description", Me.DescriptionSQLServer)
            claveConexion.SetValue("Driver", Me.DriverSQL)
            claveConexion.SetValue("LastUser", Me.LastUser)
            claveConexion.SetValue("Server", Me.ServerSQL)

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        
    End Sub

    Public Property ApplicationAttributes() As String
        Get
            Return mVarApplicationAttributes
        End Get
        Set(ByVal value As String)
            mVarApplicationAttributes = value
        End Set
    End Property
    Public Property Attributes() As String
        Get
            Return mVarAttributes
        End Get
        Set(ByVal value As String)
            mVarAttributes = value
        End Set
    End Property
    Public Property BatchAutocommitMode() As String
        Get
            Return mVarBatchAutocommitMode
        End Get
        Set(ByVal value As String)
            mVarBatchAutocommitMode = value
        End Set
    End Property
    Public Property CloseCursor() As String
        Get
            Return mVarCloseCursor
        End Get
        Set(ByVal value As String)
            mVarCloseCursor = value
        End Set
    End Property
    Public Property DescriptionOracle() As String
        Get
            Return mVarDescriptionOracle
        End Get
        Set(ByVal value As String)
            mVarDescriptionOracle = value
        End Set
    End Property
    Public Property DisableMTS() As String
        Get
            Return mVarDisableMTS
        End Get
        Set(ByVal value As String)
            mVarDisableMTS = value
        End Set
    End Property
    Public Property Driver() As String
        Get
            Return mVarDriver
        End Get
        Set(ByVal value As String)
            mVarDriver = value
        End Set
    End Property
    Public Property DSN() As String
        Get
            Return mVarDSN
        End Get
        Set(ByVal value As String)
            mVarDSN = value
        End Set
    End Property
    Public Property EXECSchemaOpt() As String
        Get
            Return mVarEXECSchemaOpt
        End Get
        Set(ByVal value As String)
            mVarEXECSchemaOpt = value
        End Set
    End Property
    Public Property EXECSyntax() As String
        Get
            Return mVarEXECSyntax
        End Get
        Set(ByVal value As String)
            mVarEXECSyntax = value
        End Set
    End Property
    Public Property Failover() As String
        Get
            Return mVarFailover
        End Get
        Set(ByVal value As String)
            mVarFailover = value
        End Set
    End Property
    Public Property FailoverDelay() As String
        Get
            Return mVarFailoverDelay
        End Get
        Set(ByVal value As String)
            mVarFailoverDelay = value
        End Set
    End Property
    Public Property FailoverRetryCount() As String
        Get
            Return mVarFailoverRetryCount
        End Get
        Set(ByVal value As String)
            mVarFailoverRetryCount = value
        End Set
    End Property
    Public Property ForceWCHAR() As String
        Get
            Return mVarForceWCHAR
        End Get
        Set(ByVal value As String)
            mVarForceWCHAR = value
        End Set
    End Property
    Public Property Lobs() As String
        Get
            Return mVarLobs
        End Get
        Set(ByVal value As String)
            mVarLobs = value
        End Set
    End Property
    Public Property Longs() As String
        Get
            Return mVarLongs
        End Get
        Set(ByVal value As String)
            mVarLongs = value
        End Set
    End Property
    Public Property MetadataIdDefault() As String
        Get
            Return mVarMetadataIdDefault
        End Get
        Set(ByVal value As String)
            mVarMetadataIdDefault = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return mVarmPassword
        End Get
        Set(ByVal value As String)
            mVarmPassword = ""
        End Set
    End Property
    Public Property PrefetchCount() As String
        Get
            Return mVarPrefetchCount
        End Get
        Set(ByVal value As String)
            mVarPrefetchCount = value
        End Set
    End Property
    Public Property QueryTimeout() As String
        Get
            Return mVarQueryTimeout
        End Get
        Set(ByVal value As String)
            mVarQueryTimeout = value
        End Set
    End Property
    Public Property ResultSets() As String
        Get
            Return mVarResultSets
        End Get
        Set(ByVal value As String)
            mVarResultSets = value
        End Set
    End Property
    Public Property ServerName() As String
        Get
            Return mVarServerName
        End Get
        Set(ByVal value As String)
            mVarServerName = value
        End Set
    End Property
    Public Property SQLGetDataExtensions() As String
        Get
            Return mVarSQLGetDataExtensions
        End Get
        Set(ByVal value As String)
            mVarSQLGetDataExtensions = value
        End Set
    End Property
    Public Property TranslationDLL() As String
        Get
            Return mVarTranslationDLL
        End Get
        Set(ByVal value As String)
            mVarTranslationDLL = value
        End Set
    End Property
    Public Property TransationOption() As String
        Get
            Return mVarTransationOption
        End Get
        Set(ByVal value As String)
            mVarTransationOption = value
        End Set
    End Property
    Public Property UserID() As String
        Get
            Return mVarUserID
        End Get
        Set(ByVal value As String)
            mVarUserID = value
        End Set
    End Property
    Public Property Database() As String
        Get
            Return mVarDatabase
        End Get
        Set(ByVal value As String)
            mVarDatabase = value
        End Set
    End Property
    Public Property DescriptionSQLServer() As String
        Get
            Return mVarDescriptionSQLServer
        End Get
        Set(ByVal value As String)
            mVarDescriptionSQLServer = value
        End Set
    End Property
    Public Property DriverSQL() As String
        Get
            Return mVarDriverSQL
        End Get
        Set(ByVal value As String)
            mVarDriverSQL = value
        End Set
    End Property
    Public Property LastUser() As String
        Get
            Return mVarLastUser
        End Get
        Set(ByVal value As String)
            mVarLastUser = value
        End Set
    End Property
    Public Property ServerSQL() As String
        Get
            Return mVarServerSQL
        End Get
        Set(ByVal value As String)
            mVarServerSQL = value
        End Set
    End Property
    Public Property RutaClave() As String
        Get
            Return mVarRutaClave
        End Get
        Set(ByVal value As String)
            mVarRutaClave = value
        End Set
    End Property
    Public Property RutaClavesConexion() As String
        Get
            Return mVarRutaClavesConexion
        End Get
        Set(ByVal value As String)
            mVarRutaClavesConexion = value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
