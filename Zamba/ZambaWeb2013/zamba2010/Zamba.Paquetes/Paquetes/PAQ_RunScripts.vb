'ver
Imports Zamba.Servers

Public Class PAQ_RunScripts
    Inherits ZPaq
    Implements IPAQ


#Region "Atributos y propiedades"
    Private Const _name As String = "PAQ_RunScripts"
    Private Const _description As String = "Corre todas las consultas de la carpeta Scripts (dependiendo del servidor de SQL o Oracle es la carpeta)"
    Private Const _version As String = "1"
    Private Const fechaCreacion As String = "23/05/2011"
    Private _installed As Boolean

    Public ReadOnly Property Description() As String Implements IPAQ.Description
        Get
            Return _description
        End Get
    End Property
    Public Property Installed() As Boolean Implements IPAQ.Installed
        Get
            Return _installed
        End Get
        Set(ByVal value As Boolean)
            _installed = value
        End Set
    End Property
    Public ReadOnly Property Name() As String Implements IPAQ.Name
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property Number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_RunScripts
        End Get
    End Property
    Public ReadOnly Property Orden() As Long Implements IPAQ.Orden
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse(fechaCreacion)
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return _version
        End Get
    End Property
    Public ReadOnly Property DependenciesIDs() As System.Collections.Generic.List(Of Int64) Implements IPAQ.DependenciesIDs
        Get
            Return New Generic.List(Of Int64)
        End Get
    End Property

#End Region

#Region "Métodos"
    Public Function execute() As Boolean Implements IPAQ.Execute
        Dim sql As String
        Dim errores As String = String.Empty
        'Leer todos los archivos en la carpeta correspondiente
        If Server.isSQLServer Then
            'leer todo lo de SQL
            For Each script As String In System.IO.Directory.GetFiles(Application.StartupPath & "\Scripts\SQL")
                Try
                    Dim strReader As System.IO.StreamReader = New System.IO.StreamReader(script)
                    sql = strReader.ReadToEnd()
                    strReader.Dispose()
                    strReader = Nothing
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                Catch ex As Exception
                    errores &= Name & " " & ex.ToString() & " "
                End Try
            Next
        Else
            'leer todo lo de Oracle
            For Each script As String In System.IO.Directory.GetFiles(Application.StartupPath & "\Scripts\Oracle")
                Try
                    Dim strReader As System.IO.StreamReader = New System.IO.StreamReader(script)
                    sql = strReader.ReadToEnd()
                    strReader.Dispose()
                    strReader = Nothing
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                Catch ex As Exception
                    errores &= Name & " " & ex.ToString() & " "
                End Try
            Next
        End If

        If String.IsNullOrEmpty(errores) Then
            Throw New Exception(errores)
        End If
        Return True
    End Function
#End Region
    Public Overrides Sub Dispose() Implements IDisposable.Dispose

    End Sub

End Class
