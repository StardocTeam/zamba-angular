Imports Zamba.Data
Imports Zamba.Tools
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DBTools
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para trabajar con bases de datos
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	30/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class DBTools
    Inherits ZClass
    Implements IDisposable






    Public Shared Sub ReEnumerarColumna(ByVal Tabla As String, ByVal Columna As String)
        DBToolsFactory.reenumerarcolumna(Tabla, Columna)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza la cantidad de documentos insertados para cada Entidad
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub ContarDocTypes()
        DBToolsFactory.contardoctypes()
    End Sub
    Public Shared Function GetDocCount(ByVal ID As Int32) As Int32
        Return DBToolsFactory.getdoccount(ID)
    End Function
    Public Shared Function GetDocCount() As DataSet
        Return DBToolsFactory.getdoccount
    End Function

    Public Shared Function GetActiveDatabase() As String
        Return DBToolsFactory.GetActiveDatabase
    End Function

    Public Shared Function GetServerType() As String
        Return DBToolsFactory.GetServerType
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Lee el archivo App.ini y completa los valores de la ultima conexion configurada
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetActualConfig() As ArrayList

        Dim array As ArrayList = New ArrayList()
        array.Add(CStr(Zamba.Servers.Server.AppConfig.SERVER))
        array.Add(Zamba.Servers.Server.AppConfig.DB)
        array.Add(Zamba.Servers.Server.AppConfig.USER)
        array.Add(Zamba.Servers.Server.AppConfig.PASSWORD)
        array.Add(Zamba.Servers.Server.AppConfig.WIN_AUTHENTICATION)
        Return array
    End Function


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
    Private Shared Sub SetReadOnlyAPP(ByVal Valor As Boolean)
        Dim f As IO.FileInfo
        Try
            If IO.File.Exists(".\app.ini") Then
                f = New IO.FileInfo(".\app.ini")

                If Valor = True Then
                    f.Attributes = IO.FileAttributes.ReadOnly
                Else
                    f.Attributes = IO.FileAttributes.Normal
                End If
                f = Nothing
            End If
        Finally
            f = Nothing
        End Try
    End Sub

    Public Overrides Sub Dispose()

    End Sub
End Class
