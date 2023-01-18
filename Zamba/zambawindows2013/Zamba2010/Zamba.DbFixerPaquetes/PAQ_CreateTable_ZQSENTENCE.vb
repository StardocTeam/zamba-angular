Imports Zamba.Servers

Public Class PAQ_CreateTable_ZQSENTENCE
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
#Region "Propiedades"
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("11/05/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "1.6.0"
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea la Tabla ZQSentence"
        End Get
    End Property
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateTable_ZQSENTENCE"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateTable_ZQSENTENCE
        End Get
    End Property

#End Region

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        If Server.ServerType = Server.DBTYPES.OracleClient OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle Then
            Return Me.ExecuteOracle(GenerateScripts)
        Else
            Return Me.ExecuteSQL(GenerateScripts)
        End If
    End Function

    Private Function ExecuteSQL(ByVal GenerateScripts As Boolean) As Boolean
        Dim sql As String = "CREATE TABLE ZQSENTENCE(id int,Value nvarchar(200), type nvarchar(30))"
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("ZQSENTENCE") = True Then
                Throw New Exception(Me.name & ": La tabla ZQSENTENCE ya existe en la base de datos.")
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
    Private Function ExecuteOracle(ByVal GenerateScripts As Boolean) As Boolean
        Try
            Dim sql As String = "CREATE TABLE ZQSENTENCE(id NUMBER(10),Value nvarchar2(200), type nvarchar2(30))"
            If GenerateScripts = False Then
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
        Catch ex As Exception
            Return False
        End Try
    End Function

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
            Return 14
        End Get
    End Property



End Class
