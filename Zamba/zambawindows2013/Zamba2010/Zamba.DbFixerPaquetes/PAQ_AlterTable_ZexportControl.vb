Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Imports System.io
Public Class PAQ_AlterTable_ZexportControl
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub


#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AlterZexportControl"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AlterTable_ZexportControl
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Modifica columna LINE de la tabla ZEXPORTCONTROL al tipo long (string largo)"
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("18/12/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("18/12/2006")
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
            Return 59
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim Sql As New System.Text.StringBuilder
        Dim sql2 As New System.Text.StringBuilder
        Try
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

            Else
                Sql.Append("CREATE TABLE ZEXPORTCONTROLbkp (FECHA  DATE,USERID NVARCHAR2(30),LINE NVARCHAR2(2000),INSERTADO CHAR(1 byte),CODIGO NVARCHAR2(50)) ")
                Server.Con.ExecuteNonQuery(CommandType.Text, Sql.ToString)
                sql2.Append(Sql.ToString)
                Sql.Remove(0, Sql.Length)
                Sql.Append("INSERT INTO ZEXPORTCONTROLbkp select ZEXPORTCONTROL.* from ZEXPORTCONTROL ")
                Server.Con.ExecuteNonQuery(CommandType.Text, Sql.ToString)
                sql2.Append(Sql.ToString)
                Sql.Remove(0, Sql.Length)
                Sql.Append("DELETE FROM ZEXPORTCONTROL ")
                Server.Con.ExecuteNonQuery(CommandType.Text, Sql.ToString)
                sql2.Append(Sql.ToString)
                Sql.Remove(0, Sql.Length)
                Sql.Append("ALTER TABLE ZEXPORTCONTROL MODIFY(LINE LONG) ")
                Server.Con.ExecuteNonQuery(CommandType.Text, Sql.ToString)
                sql2.Append(Sql.ToString)
                Sql.Remove(0, Sql.Length)
                Sql.Append("INSERT INTO ZEXPORTCONTROL select ZEXPORTCONTROLbkp.* from ZEXPORTCONTROLbkp")
                Server.Con.ExecuteNonQuery(CommandType.Text, Sql.ToString)
                sql2.Append(Sql.ToString)
                Sql.Remove(0, Sql.Length)


                Dim sw As New IO.StreamWriter(Path.Combine(Application.StartupPath, "ScriptOracle.sql"), True)
                sw.WriteLine("")
                sw.WriteLine(sql2.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If

        Catch ex As Exception

        End Try
        Return True
        Sql = Nothing

    End Function
#End Region


End Class
