Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_AlterTable_Usrtable
    Inherits ZPaq
    Implements IPAQ

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim strcreate As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate = "alter table usrtable alter column FOTO VARCHAR(255) NULL  " & _
                        "alter table usrtable alter column FIRMA VARCHAR(255) NULL " & _
                        "alter table usrtable alter column ADDRESS_BOOK VARCHAR(255) NULL "
        Else
            strcreate = "alter table usrtable MODIFY ( FOTO VARCHAR2(255), FIRMA VARCHAR2(255), ADDRESS_BOOK VARCHAR2(255));"
        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("usrtable") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
            End If
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(strcreate.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
        Return True
    End Function

    Public Function existenColumnas() As Boolean
        'TODO Store: SPExistenColumnas
        Dim str As String
        str = "select name from syscolumns where id = (select id from sysobjects where name = 'Nombre_Tabla') and name = 'Nombre_Columna'"

    End Function
#End Region

#Region "Propiedades"
    Public Overrides Sub Dispose()

    End Sub

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AlterTableUsrtable Modifica la tabla usrtable."
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Modifica las columnas FOTO, ADDRESS_BOOK y FIRMA ampliando su rango a varchar(255)."
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("20/06/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AlterTable_Usrtable
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
            Return 50
        End Get
    End Property

#End Region

End Class
