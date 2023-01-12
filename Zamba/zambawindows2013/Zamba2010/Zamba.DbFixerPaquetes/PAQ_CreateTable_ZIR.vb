Imports Zamba.Servers
Imports zamba.Core
Imports Microsoft.Win32
Imports system.Text
Public Class PAQ_CreateTable_ZIR

    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub
#Region "IPAQ Members"
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea la tabla ZIR"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateTable_ZIR"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateTable_ZIR
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute

        Dim sqlBuilder As New StringBuilder
        Dim flagException As Boolean = False
        Dim exBuilder As New StringBuilder

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sqlBuilder.Append("CREATE TABLE ZIR (RightId numeric(4, 0) NOT NULL ,IndexId numeric(10, 0) NOT NULL , ")
            sqlBuilder.Append(" DoctypeId numeric(4, 0) NOT NULL ,	UserId numeric(10, 0) NOT NULL ,	RightType numeric(4, 0) NOT NULL )")
        Else
            sqlBuilder.Append("CREATE TABLE ZIR (RightId numeric(4, 0) NOT NULL ,IndexId numeric(10, 0) NOT NULL , ")
            sqlBuilder.Append(" DoctypeId numeric(4, 0) NOT NULL ,	UserId numeric(10, 0) NOT NULL ,	RightType numeric(4, 0) NOT NULL )")
        End If

        Try
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("ZTaskStatus") Then
                    Throw New Exception(Me.name & ": La tabla ZTaskStatus ya existe en la base de datos.")
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sqlBuilder.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            exBuilder.AppendLine(sqlBuilder.ToString())
            exBuilder.Append("Error: ")
            flagException = True
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try

        If flagException Then
            Throw New Exception(Me.name & exBuilder.ToString())
        End If
        sqlBuilder = Nothing

        Return True
    End Function
#End Region

#Region "ZPaq Members"
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("29/01/2008")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("29/01/2008")
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
            Return 91
        End Get
    End Property
#End Region
End Class