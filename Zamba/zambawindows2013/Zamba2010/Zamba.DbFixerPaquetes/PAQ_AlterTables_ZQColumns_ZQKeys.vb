Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Imports System.Text


Public Class PAQ_AlterTables_ZQColumns_ZQKeys
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AlterTables_ZQColumns_ZQKeys"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AlterTables_ZQColumns_ZQKeys
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Agrega la columna VALUES en las tablas ZQColumns y ZQkeys"
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("24/09/2007")
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("24/09/2007")
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
            Return 62
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim strSql As New System.Text.StringBuilder
        Dim sqlBuilder As New StringBuilder()
        Dim sqlbuilder2 As New StringBuilder()


        sqlBuilder.Append("ALTER TABLE ZQCOLUMNS ")
        sqlbuilder2.Append("ALTER TABLE ZQKEYS ")

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sqlBuilder.Append("ADD VALUES nvarchar(254) NULL")
            sqlbuilder2.Append("ADD VALUES nvarchar(254) NULL")

        Else
            sqlBuilder.Append("ADD VALUES nvarchar2(255)")
            sqlbuilder2.Append("ADD VALUES nvarchar2(255)")

        End If

        If GenerateScripts = False Then
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlbuilder2.ToString)

        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(sqlBuilder.ToString)
            sw.WriteLine("")

            sw.WriteLine("")
            sw.WriteLine(sqlbuilder2.ToString)
            sw.WriteLine("")

            sw.Close()
            sw = Nothing
        End If

        Return True
        strSql = Nothing

    End Function
#End Region

End Class

