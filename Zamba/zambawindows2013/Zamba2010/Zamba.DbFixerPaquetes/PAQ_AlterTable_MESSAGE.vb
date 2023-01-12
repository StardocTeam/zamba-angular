Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32

Public Class PAQ_AlterTable_MESSAGE
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim strcreate As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate = "ALTER TABLE MESSAGE ALTER COLUMN MSG_BODY NVARCHAR(4000) NULL "
        Else
            strcreate = "ALTER TABLE MESSAGE MODIFY (MSG_BODY NVARCHAR2(4000) NULL)"
        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("MESSAGE") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
            Else
                Throw New Exception(Me.name + " la tabla Message no existe en la base de datos.")
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
#End Region

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AlterTable_MESSAGE"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AlterTable_MESSAGE
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Modifica la Tabla MESSAGE: amplia la longitud de la columna Msg_Body a varchar(4000)"
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("22/01/2007")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "3.0.0"
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("22/01/2007")
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
            Return 47
        End Get
    End Property
#End Region

End Class
