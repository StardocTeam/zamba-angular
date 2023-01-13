'ver
Imports Zamba.Servers

Public Class PAQ_CreateTable_DOC_TYPE_R_DOC_TYPE
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la tabla DOC_TYPE_R_DOC_TYPE"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute

        Dim sql As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sql = "Create Table DOC_TYPE_R_DOC_TYPE(DocTypeID1 int, DocTypeID2 int, Index1 int, Index2 int)"
        Else
            sql = "Create Table DOC_TYPE_R_DOC_TYPE(DocTypeID1 Number(10), DocTypeID2 Number(10), Index1 Number(10), Index2 Number(10))"
        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("DOC_TYPE_R_DOC_TYPE") = True Then
                Throw New Exception(Me.name & " La tabla DOC_TYPE_R_DOC_TYPE ya existe en la base de datos")
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

        Me.Migrar()
        Return True
    End Function
    Private Sub Migrar()
        Dim sql As String = "Insert into DOC_TYPE_R_DOC_TYPE Select * from doctypes_associated"
        If ZPaq.ExisteTabla("doctypes_associated") Then
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            Try
                ZPaq.IfExists("doctypes_associated", ZPaq.Tipo.Table, True)
            Catch ex As Exception
                Throw New Exception(Me.name & ": error al migrar los datos de la tabla doctypes_associated (" & ex.ToString() & ")")
            End Try
        Else
            Throw New Exception(Me.name & ": error al migrar los datos de la tabla doctypes_associated")
        End If
    End Sub

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTableDOC_TYPE_R_DOC_TYPE"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_DOC_TYPE_R_DOC_TYPE
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("14/02/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
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
            Return 1
        End Get
    End Property
End Class
