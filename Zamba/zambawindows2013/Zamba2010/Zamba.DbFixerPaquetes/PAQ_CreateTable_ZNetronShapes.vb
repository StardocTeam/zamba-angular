Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class PAQ_CreateTable_ZNetronShapes
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea la tabla ZNetronShapes, utilizada para almacenar objetos asociados a documentos"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sql = "Create Table ZNetronShapes (Shape_Id numeric(4) PRIMARY KEY not null, Shape_DocT numeric(4), Shape_DocId numeric(4), Shape_Tipo smallint not null, Shape_Height numeric(4), Shape_Color numeric(4), Shape_Opaque numeric(4), Shape_Text nvarchar(255), Shape_Width numeric(4), Shape_X numeric(4), Shape_Y numeric(4), Shape_StartId smallint, Shape_EndId smallint, Shape_StartNum tinyint, Shape_EndNum tinyint)"
        Else
            ' Oracle
            sql = "Create Table ZNetronShapes (Shape_Id NUMBER(4) primary key not null, Shape_DocT NUMBER(4), Shape_DocId NUMBER(4), Shape_Tipo NUMBER(2) not null, Shape_Height NUMBER(4), Shape_Color NUMBER(4), Shape_Opaque NUMBER(4), Shape_Text nvarchar2(255), Shape_Width NUMBER(4), Shape_X NUMBER(4), Shape_Y NUMBER(4), Shape_StartId NUMBER(2), Shape_EndId NUMBER(4), Shape_StartNum NUMBER(1), Shape_EndNum NUMBER(4))"
        End If


        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("ZNetronShapes") = True Then
                Throw New Exception(Me.name & " La tabla ZNetronShapes ya existe en la base de datos")
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
            Return "TABLA ZNETRONSHAPES"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateTable_ZNetronShapes
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("20/02/2005")
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
            Return 12
        End Get
    End Property
End Class
