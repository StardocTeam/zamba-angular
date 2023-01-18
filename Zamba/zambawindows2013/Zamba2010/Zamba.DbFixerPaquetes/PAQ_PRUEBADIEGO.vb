Imports Zamba.Servers

Public Class PAQ_PRUEBADIEGO
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
            Return Date.Parse("30/07/2008")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "1.0.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea la tabla fruta para sql"
        End Get
    End Property
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_PRUEBADIEGO"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_PRUEBADIEGO
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
            Return 100
        End Get
    End Property
#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        If Server.ServerType = Server.DBTYPES.MSSQLServer7Up OrElse Server.ServerType = Server.DBTYPES.MSSQLServer Then
            Return Me.ExecuteSQL(GenerateScripts)
        End If
    End Function

    Public Function ExecuteOracle(ByVal ToScripts As Boolean) As Boolean
        Try
            Dim sql As String = "CREATE TABLE [FRUTA] (	[idfruta] [numeric](18, 0) NOT NULL ,	[frutaname] [varchar] (50) ,	[frutadate] [datetime] NOT NULL ) "
            If ToScripts = False Then
                If ZPaq.ExisteTabla("FRUTA") Then
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
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ExecuteSQL(ByVal ToScripts As Boolean) As Boolean
        Try
            Dim sql As String = "CREATE TABLE [FRUTA] (	[idfruta] [numeric](18, 0) NOT NULL ,	[frutaname] [varchar] (50) ,	[frutadate] [datetime] NOT NULL ) "
            If ToScripts = False Then
                If Not ZPaq.ExisteTabla("FRUTA") Then
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
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region


End Class
