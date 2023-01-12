Imports Zamba.Servers
Public Class PAQ_AlterTable_Usrtable_Correo
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
            Return Date.Parse("30/06/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega los datos del mail (campos CONF_BASEMAIL, CONF_MAILSERVER, CONF_MAILTYPE, SMTP) a la tabla USRTABLE."
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTable_Usrtable_Correo"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTable_Usrtable_Correo
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("20/07/2006")
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
            Return 51
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        If Server.ServerType = Server.DBTYPES.OracleClient OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle Then
            Return Me.AlterORACLE(GenerateScripts)
        Else
            Return Me.AlterSQL(GenerateScripts)
        End If
    End Function

    Private Function AlterORACLE(ByVal scripts As Boolean) As Boolean
        Dim sql As String = "Alter table usrtable add(CONF_BASEMAIL nvarchar2(30) null, CONF_MAILSERVER nvarchar2(30) null, CONF_MAILTYPE int null, SMTP nvarchar2(30) null)"
        Try
            If scripts = False Then
                If ZPaq.ExisteTabla("usrtable") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                    sql = "UPDATE USRTABLE SET CONF_MAILTYPE=3"
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                    Return True
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine(sql)
                sw.Close()
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Private Function AlterSQL(ByVal scripts As Boolean) As Boolean
        Dim sql As String = "Alter table usrtable add CONF_BASEMAIL nvarchar(30) null default('Base Sin Asignar'), CONF_MAILSERVER nvarchar(30) null default('Servidor No Asignado'), CONF_MAILTYPE int null default(3), SMTP nvarchar(30) null default('No Asignado')"
        Try
            If scripts = False Then
                If ZPaq.ExisteTabla("usrtable") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                    Return True
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine(sql)
                sw.Close()
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function
#End Region

End Class
