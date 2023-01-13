'ver
Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_AddColumns_TABLATEMP
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim strcreate As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate = "ALTER TABLE TABLATEMP ADD MUSTCOMPLETE numeric, SHOWLOTUS numeric, LOADLOTUS numeric"
        Else
            strcreate = "ALTER TABLE TABLATEMP ADD (MUSTCOMPLETE NUMBER(4), SHOWLOTUS NUMBER(1), LOADLOTUS NUMBER(1))"
        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("TABLATEMP") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
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

    Public Function existenColumnas() As Boolean
        '     Dim existe As Boolean, i As Int16
        'TODO Store: SPExistenColumnas

        Dim str As String
        str = "select name from syscolumns where id = (select id from sysobjects where name = 'Nombre_Tabla') and name = 'Nombre_Columna'"
    End Function
#End Region

#Region "Propiedades"

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AddColumns_TABLATEMP"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AddColumns_TABLATEMP
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega las columnas MUSTCOMPLETE,SHOWLOTUS,LOADLOTUS a la tabla TablaTemp."
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("31/01/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
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
            Return 41
        End Get
    End Property

#End Region

End Class
