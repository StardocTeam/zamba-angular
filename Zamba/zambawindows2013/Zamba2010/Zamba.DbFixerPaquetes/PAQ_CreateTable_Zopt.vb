'LISTO
Imports Zamba.Servers
Imports Zamba.AppBlock

Public Class PAQ_CreateTable_Zopt
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea la tabla ZOpt, utilizada para almacenar el path donde se guardan las imagenes"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sql = "Create Table Zopt (Item nvarchar(50) PRIMARY KEY, [Value] nvarchar(400) NOT NULL)"
        Else
            ' Oracle
            sql = "CREATE TABLE ZOPT (ITEM NVARCHAR2(50) NOT NULL," & Chr(34) & "VALUE" & Chr(34) & "  NVARCHAR2(400) NOT NULL, CONSTRAINT" & Chr(34) & "PK123456" & Chr(34) & " PRIMARY KEY(ITEM))"
        End If

        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("ZOPT") = True Then
                Throw New Exception(Me.name & ": La tabla ZOPT ya existe en la base de datos.")
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

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateTableZopt"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateTable_Zopt
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("01/01/2006")
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
            Return 13
        End Get
    End Property
End Class
