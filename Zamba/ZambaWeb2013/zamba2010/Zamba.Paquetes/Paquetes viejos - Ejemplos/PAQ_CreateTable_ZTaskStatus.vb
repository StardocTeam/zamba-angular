Imports Zamba.Servers
Imports zamba.Core
Imports Microsoft.Win32
Imports system.Text
Public Class PAQ_CreateTable_ZTaskStatus

    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub
#Region "IPAQ Members"
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la tabla ZtaskStatus; tabla donde se muestran los estados posibles que pueden tener una tarea luego de la verificacion de Una persona de Gerarquia."
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTable_ZTaskStatus"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_ZTaskStatus
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute

        Dim sqlBuilder As New StringBuilder
        Dim flagException As Boolean = False
        Dim exBuilder As New StringBuilder

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sqlBuilder.Append("CREATE TABLE ZTaskStatus ( StatusId numeric(1, 0) NULL , TaskStatus nvarchar (30) COLLATE Modern_Spanish_CI_AS NULL )")
        Else
            sqlBuilder.Append("CREATE TABLE ZTaskStatus ( StatusId numeric(1, 0) NULL , TaskStatus nvarchar2 (30)  NULL )")
        End If

        Try
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("ZTaskStatus") Then
                    Throw New Exception(Me.name & ": La tabla ZTaskStatus ya existe en la base de datos.")
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
                sqlBuilder.Remove(0, sqlBuilder.Length - 1)
                sqlBuilder.Append("INSERT INTO ZTaskStatus(StatusId, TaskStatus) VALUES(0,'Pendiente de Autorizaci�n')")
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
                sqlBuilder.Remove(0, sqlBuilder.Length - 1)
                sqlBuilder.Append("INSERT INTO ZTaskStatus(StatusId, TaskStatus) VALUES(1,'Autorizada')")
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
                sqlBuilder.Remove(0, sqlBuilder.Length - 1)
                sqlBuilder.Append("INSERT INTO ZTaskStatus(StatusId, TaskStatus) VALUES(2,'Rechazada')")
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
            Return 90
        End Get
    End Property
#End Region
End Class