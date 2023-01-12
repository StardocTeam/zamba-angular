'listo
Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class PAQ_CreateTable_ZServidores
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea la tabla Zservidores, utilizada por ZServerImport / ZLocalImport"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sql = "Create Table ZServidores (Id numeric(18,0) PRIMARY KEY NOT NULL, NombreServidor char(20), Puerto numeric(18,0), IP char(15), Objeto char(15), Interfaz char(15), Descripcion char(50))"
        Else
            ' Oracle
            sql = "Create Table ZServidores (Id NUMBER(9) PRIMARY KEY NOT NULL, NombreServidor nvarchar2(20), Puerto NUMBER(9), IP nvarchar2(15) , Objeto nvarchar2(15), Interfaz nvarchar2(15), Descripcion nvarchar2(50))"
        End If

        Try
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("ZServidores") = True Then
                    Throw New Exception(Me.name & " La tabla ZServidores ya existe en la base de datos")
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
        Catch ex As Exception
            Dim exbuilder As New System.Text.StringBuilder
            exbuilder.AppendLine(sql)
            exbuilder.Append("Error: ")
            exbuilder.Append(ex.ToString())
            exbuilder.AppendLine()
            exbuilder.AppendLine()
            Throw New Exception(exbuilder.ToString())
        End Try
        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "Tabla Zservidores"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateTable_ZServidores
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
            Return 15
        End Get
    End Property
End Class
