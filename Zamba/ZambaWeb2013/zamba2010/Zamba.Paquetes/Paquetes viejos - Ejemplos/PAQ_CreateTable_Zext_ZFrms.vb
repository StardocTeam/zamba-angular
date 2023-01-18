Imports Zamba.Servers

Public Class PAQ_CreateTable_Zext_ZFrms
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
            Return DateTime.Parse("09/03/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la tabla ZEXT_ZFRMS"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sql As String
        sql = "create table Zext_zfrms (zfrms_id numeric NOT NULL , zext_id numeric NOT NULL)"
        If Not ZPaq.ExisteTabla("Zext_zfrms") Then
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Else
            Throw New Exception(Me.name & ": la tabla Zext_zfrms ya existe en la base de datos.")
        End If
        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTable_Zext_ZFrms"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_Zext_ZFrms
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
            Return 11
        End Get
    End Property
End Class
