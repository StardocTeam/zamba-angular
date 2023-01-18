Imports Zamba.Servers
Public Class PAQ_AlterTable_Usrtable_NameIndex
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTable_Usrtable_NameIndex"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTable_Usrtable_NameIndex
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("03/07/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega un indice en la tabla USRTABLE por la columna NAME. Esto mejora el tiempo en el inicio de sesión."
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
            Return 52
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            Return Me.executeSQL(GenerateScripts)
        Else
            Return Me.executeOracle(GenerateScripts)
        End If
    End Function

    Private Function executeOracle(ByVal generatescripts As Boolean) As Boolean

    End Function

    Private Function executeSQL(ByVal generatescripts As Boolean) As Boolean
        Dim sql As String = "CREATE UNIQUE NONCLUSTERED INDEX IName ON usrtable(name)"
        Try
            If generatescripts = True Then
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine(sql)
                sw.Close()
            Else
                If ZPaq.ExisteTabla("usrtable") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                Return True
                End If
            End If
        Catch
            Return False
        End Try
        Return False
    End Function
#End Region

    
End Class
