'ver
Imports Zamba.Servers

Public Class PAQ_RunScripts
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Corre todas las consultas de la carpeta Scripts (dependiendo del servidor de SQL o Oracle es la carpeta)"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As String
        Dim errores As String = String.Empty
        'Leer todos los archivos en la carpeta correspondiente
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            'leer todo lo de SQL
            For Each script As String In System.IO.Directory.GetFiles(Application.StartupPath & "\Scripts\SQL")
                Try
                    Dim strReader As System.IO.StreamReader = New System.IO.StreamReader(script)
                    sql = strReader.ReadToEnd()
                    strReader.Dispose()
                    strReader = Nothing
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                Catch ex As Exception
                    errores &= Me.name & " " & ex.ToString() & " "
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
                    errores &= Me.name & " " & ex.ToString() & " "
                End Try
            Next
        End If

        If String.IsNullOrEmpty(errores) Then
            Throw New Exception(errores)
        End If
        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_RunScripts"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_RunScripts
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("18/12/2007")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property
    Public ReadOnly Property orden() As Int64 Implements IPAQ.orden
        Get
            Return 88
        End Get
    End Property
End Class
