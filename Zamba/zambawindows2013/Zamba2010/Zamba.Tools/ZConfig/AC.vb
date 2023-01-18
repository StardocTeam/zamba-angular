Imports System.Windows.Forms
Imports System.Diagnostics



Public Class ApplicationConfig

    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

    Dim Dsconfig1 As New DsConfig


    Private Function ReadWebConfig() As String
        '    Dim ConString As String = DirectCast(System.Web.Configuration.WebConfigurationManager.ConnectionStrings.Item("Zamba").ConnectionString, String)
        Dim WebFile As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings.Item("Zamba").ElementInformation.Source
        Dim fi As New IO.FileInfo(WebFile)
        Dim File As String = fi.Directory.FullName & "\app.ini"
        Return File
    End Function

    Public Sub New(file As String)
        Try
            Read(file)
        Catch ex As Exception
            Throw New Exception("No se encuentra una configuración correcta para la aplicación")
        End Try
    End Sub

    Public Sub Write(File As String)
        Try
            If Dsconfig1.Configuration.Rows.Count = 0 Then
                Dim RowC As DsConfig.ConfigurationRow = Dsconfig1.Configuration.NewConfigurationRow()
                RowC.DB = "Nombre Base de Datos"
                RowC.EMAIL = "eMail"
                RowC.MAILTYPE = 1
                RowC.MAXQUERY = 20
                RowC.PASSWORD = Encryption.EncryptString("Zamba Password", key, iv)
                RowC.SERVER = "Nombre del Servidor"
                RowC.SERVERTYPE = 0
                RowC.SMTP = "Servidor SMTP"
                RowC.STATION = 0
                RowC.TIMEOUT = 60
                RowC.USER = "Usuario"
                RowC.LOGEXTODB = False
                RowC.LOGEXTOFILE = False
                RowC.BASEMAIL = "BASE.NSF"
                RowC.SERVERMAIL = "SERVIDOR LOTUS"
                RowC.LOGEXTOFILEOK = "false"
                RowC.WIN_AUTHENTICATION = "false"
                Dsconfig1.Configuration.AddConfigurationRow(RowC)
                Dsconfig1.Configuration.AcceptChanges()
            End If

            If Dsconfig1.Importacion.Rows.Count = 0 Then
                Dim RowI As DsConfig.ImportacionRow = Dsconfig1.Importacion.NewImportacionRow()
                RowI.Service = 2
                Dsconfig1.Importacion.AddImportacionRow(RowI)
                Dsconfig1.Importacion.AcceptChanges()
            End If

            If Dsconfig1.Monitoreo.Rows.Count = 0 Then
                Dim RowM As DsConfig.MonitoreoRow = Dsconfig1.Monitoreo.NewMonitoreoRow()
                RowM.Service = 2
                RowM.TempPath = Application.StartupPath
                RowM.TempZipPath = Application.StartupPath
                Dsconfig1.Monitoreo.AddMonitoreoRow(RowM)
                Dsconfig1.Monitoreo.AcceptChanges()
            End If

            If Dsconfig1.Ver.Rows.Count = 0 Then
                Dim RowV As DsConfig.VerRow = Dsconfig1.Ver.NewVerRow()
                RowV.Ver = 159
                Dsconfig1.Ver.AddVerRow(RowV)
                Dsconfig1.Ver.AcceptChanges()
            End If

            If Not File Is Nothing Then
                Dsconfig1.WriteXmlSchema(File)
                Dsconfig1.WriteXml(File)
            End If
        Catch
            Try
                Dim appFile As New IO.FileInfo(File)
                appFile.Attributes = IO.FileAttributes.Normal
                Dsconfig1.WriteXmlSchema(File)
                Dsconfig1.WriteXml(File)
            Catch ex As Exception
                Throw ex
            End Try
        End Try
    End Sub
    Public Overridable Sub dispose()
    End Sub

    Public Sub Read(File As String)
        If IsNothing(File) = False Then
            If IO.File.Exists(File) Then
                Trace.WriteLine("Archivo de Configuracion: " & File)
                Dsconfig1.Clear()
                Dsconfig1.AcceptChanges()
                Dsconfig1.ReadXml(File)
            Else
                Trace.WriteLine("Archivo de Configuracion Inexistente: " & File)
                Dsconfig1.Clear()
                Dsconfig1.AcceptChanges()
                Write(File)
            End If
        Else
            Trace.WriteLine("Archivo de Configuracion Error")
            Dsconfig1.Clear()
            Dsconfig1.AcceptChanges()
            Write(File)
        End If
    End Sub



    Public Property USER() As String
        Get
            If Dsconfig1.Configuration(0).USER = "" Then
                USER = "Usuario"
            End If
            Return Dsconfig1.Configuration(0).USER
        End Get
        Set(ByVal Value As String)
            Dsconfig1.Configuration(0).USER = Trim(Value)
        End Set
    End Property
    Public Property PASSWORD() As String
        Get
            If Dsconfig1.Configuration(0).PASSWORD = String.Empty Then
                PASSWORD = String.Empty
                Return String.Empty
            Else
                'CAMBIO: si hay error devuelvo un sring.empty
                Try
                    Return Trim(Encryption.DecryptString(Dsconfig1.Configuration(0).PASSWORD, key, iv))
                Catch ex As Exception
                    Return String.Empty
                End Try
            End If
        End Get
        Set(ByVal Value As String)
            If Value = String.Empty Then
                Dsconfig1.Configuration(0).PASSWORD = String.Empty
            Else
                Dsconfig1.Configuration(0).PASSWORD = Encryption.EncryptString(Trim(Value), key, iv)
            End If
        End Set
    End Property

    Public Property SERVERTYPE() As DBTYPES
        Get
            If IsDBNull(Dsconfig1.Configuration(0).SERVERTYPE()) Then
                SERVERTYPE = 0
            End If
            Return DirectCast(Dsconfig1.Configuration(0).SERVERTYPE(), Integer)
        End Get
        Set(ByVal Value As DBTYPES)
            Dsconfig1.Configuration(0).SERVERTYPE() = DirectCast(Value, Integer)
        End Set
    End Property
    Public Property SERVER() As String
        Get
            If Dsconfig1.Configuration(0).SERVER = "" Then
                SERVER = "Nombre del Servidor"
            End If
            Return Dsconfig1.Configuration(0).SERVER
        End Get
        Set(ByVal Value As String)
            Dsconfig1.Configuration(0).SERVER = Trim(Value)
        End Set
    End Property
    Public Property WIN_AUTHENTICATION() As String
        Get
            Try
                If IsDBNull(Dsconfig1.Configuration(0).WIN_AUTHENTICATION) Then
                    WIN_AUTHENTICATION = "false"
                Else
                    WIN_AUTHENTICATION = Dsconfig1.Configuration(0).WIN_AUTHENTICATION
                End If
            Catch
                WIN_AUTHENTICATION = "false"
                Dsconfig1.Configuration(0).WIN_AUTHENTICATION = "false"
            End Try
            Return WIN_AUTHENTICATION
        End Get
        Set(ByVal Value As String)
            Dsconfig1.Configuration(0).WIN_AUTHENTICATION = Value.ToString()
        End Set
    End Property

    Public Property DB() As String
        Get
            If Dsconfig1.Configuration(0).DB = "" Then
                DB = "Nombre de Base de Datos"
            End If
            Return Dsconfig1.Configuration(0).DB
        End Get
        Set(ByVal Value As String)
            Dsconfig1.Configuration(0).DB = Trim(Value)
        End Set
    End Property


    Public ReadOnly Property TIMEOUT() As Int16
        Get
            If IsDBNull(Dsconfig1.Configuration(0).TIMEOUT) Then
                Return 30
            Else
                Dim db_timeOut As Int16

                If Not Int16.TryParse(Dsconfig1.Configuration(0).TIMEOUT.ToString(), db_timeOut) Then
                    db_timeOut = 30
                End If

                Return db_timeOut
            End If
        End Get
    End Property




    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece la propiedad del archivo Readonly segun el valor recibido por parametro
    ''' </summary>
    ''' <param name="Valor">True,False</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub SetReadOnlyAPP(ByVal file As String, ByVal Valor As Boolean)
        Try
            If IO.File.Exists(file) Then
                Dim f As New IO.FileInfo(file)
                If Valor = True Then
                    f.Attributes = IO.FileAttributes.ReadOnly
                Else
                    f.Attributes = IO.FileAttributes.Normal
                End If
                f = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
