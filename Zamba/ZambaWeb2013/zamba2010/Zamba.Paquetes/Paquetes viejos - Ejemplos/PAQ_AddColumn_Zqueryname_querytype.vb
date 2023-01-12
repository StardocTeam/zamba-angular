Imports Zamba.Servers

Public Class PAQ_AddColumn_Zqueryname_querytype
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
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega la columna QueryType a la tabla Zqueryname"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AddColumn_Zqueryname_querytype"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AddColumn_Zqueryname_querytype
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
            Return 37
        End Get
    End Property

#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        If Server.ServerType = Server.DBTYPES.OracleClient OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle Then
            Return Me.ExecuteORACLE(GenerateScripts)
        Else
            Return Me.ExecuteSQL(GenerateScripts)
        End If
    End Function

    Private Function ExecuteSQL(ByVal GenerateScripts As Boolean) As Boolean
        Try
            Dim sql As String = "Alter Table Zqueryname add querytype nvarchar(20) null"
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("Zqueryname") Then
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

    Private Function ExecuteORACLE(ByVal GenerateScripts As Boolean) As Boolean
        Try
            Dim sql As String = "Alter Table Zqueryname add querytype nvarchar2(20) null"
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("Zqueryname") Then
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
