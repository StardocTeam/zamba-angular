Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_AlterTable_ZForum
    Inherits ZPaq
    Implements IPAQ

#Region "Propiedades"

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Altera la tabla ZForum para que las columnas StateId y UserID tengqan el mismo tamaño que los IDs de sus tablas"
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("15/01/2007")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AlterTable_ZForum
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AlterTable_ZForum"
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
            Return 60
        End Get
    End Property

#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim strcreate As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate = "ALTER TABLE ZForum ALTER COLUMN [UserId] [numeric](10, 0) NOT NULL ALTER TABLE ZForum ALTER COLUMN [StateId] [numeric](1, 0) NOT NULL"
        Else
            strcreate = "alter table ZForum modify(UserId( NUMBER(10, 0)); alter table ZForum modify(StateID( NUMBER(10, 0));"
        End If
        If ZPaq.ExisteTabla("ZForum") Then
            Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
        End If
        Return True
    End Function

#End Region

End Class
