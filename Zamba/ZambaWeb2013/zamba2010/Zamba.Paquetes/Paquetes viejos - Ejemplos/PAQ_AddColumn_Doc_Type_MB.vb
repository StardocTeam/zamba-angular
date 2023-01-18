'ver
Imports Zamba.Servers

Public Class PAQ_AddColumn_Doc_Type_MB
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sql As String
        If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
            sql = "ALTER TABLE DOC_TYPE ADD (MB NUMBER(10, 10) DEFAULT 0)"
        Else
            sql = "Alter Table DOC_TYPE Add MB Decimal Null default 0"
        End If

        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("DOC_TYPE") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            End If
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
#End Region

#Region "Propiedades"
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega la columna 'MB' a la tabla Doc_Type. En dicha columna se almacena la cantidad de Mb por tipo de documento."
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AddColumn_Doc_Type_MB"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AddColumn_Doc_Type_MB
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
            Return 31
        End Get
    End Property
#End Region

End Class
