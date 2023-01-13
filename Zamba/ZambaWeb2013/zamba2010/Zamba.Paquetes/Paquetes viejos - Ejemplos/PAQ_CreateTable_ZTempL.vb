Imports Zamba.Servers
Public Class PAQ_CreateTable_ZTempL
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return DateTime.Parse("14/02/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Se crea la tabla ZTEMPL que es usada en el modulo INSERT"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sql As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sql = "create table ZTempl (id NUMERIC NOT NULL, name NVARCHAR(150) NOT NULL, description NVARCHAR(250) NOT NULL,type NUMERIC NOT NULL, path NVARCHAR(260) NOT NULL)"
        Else
            ' Oracle
            sql = "create table ZTempl (id NUMERIC NOT NULL, name VARCHAR2(150) NOT NULL, description VARCHAR2(250) NOT NULL,type NUMERIC NOT NULL, path VARCHAR2(260) NOT NULL)"
        End If

        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("ztempl") = True Then
                Throw New Exception(Me.name & ": La tabla ztempl ya existe en la base de datos.")
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(sql.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTableZTempL"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_ZTempL
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.Installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property
    Public ReadOnly Property orden() As Int64 Implements IPAQ.Orden
        Get
            Return 17
        End Get
    End Property
End Class