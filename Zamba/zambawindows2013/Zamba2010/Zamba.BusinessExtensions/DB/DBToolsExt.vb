Imports Zamba.Data
Imports Zamba.Tools

Public Class DBToolsExt
    Inherits DBTools

#Region "Attributes"
    Dim _dbSchema As String = Nothing
    Dim _useWindowsAuthentication As Boolean
    Dim _appConfig As ApplicationConfig = Nothing
#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene la configuración de conexión
    ''' </summary>
    ''' <value>ApplicationConfig</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ApplicationConfig() As ApplicationConfig
        Get
            Return Zamba.Servers.Server.AppConfig
        End Get
    End Property

    ''' <summary>
    ''' Verifica si la conexión se realiza mediante autenticación de Windows
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>True, en caso de que el usuario se este conectando mediante autenticación de windows</returns>
    ''' <remarks>Depende del valor configurado en el archivo app.ini</remarks>
    Public ReadOnly Property UseWindowsAuthentication() As Boolean
        Get
            If Boolean.TryParse(ApplicationConfig.WIN_AUTHENTICATION, _useWindowsAuthentication) Then
            End If
            Return _useWindowsAuthentication
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el esquema de la base conectada
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>El esquema utilizado para conectarse a la base de datos</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DataBaseSchema() As String
        Get
            If _dbSchema Is Nothing Then
                Dim dbTools As New Data.DbToolsFactoryExt
                _dbSchema = dbTools.GetDataBaseSchema(ZOptFactory.GetValue("DBOwner"))
                dbTools = Nothing
            End If
            Return _dbSchema
        End Get
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Verifica si la base de datos sea Oracle
    ''' </summary>
    ''' <returns>True en caso de que sea Oracle</returns>
    ''' <remarks></remarks>
    Public Function IsOracle() As Boolean
        Return DBToolsFactory.IsOracle()
    End Function
#End Region

End Class
