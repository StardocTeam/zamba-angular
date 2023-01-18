Imports Zamba.Servers
Imports zamba.AppBlock

Public Class PAQ_CreateTable_ReportBuilder
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
#Region "Propiedades"
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Creacion de las Tabla para ReportBuilder - Version 1.0.0.0 (Crea la tabla ReportBuilder)"
        End Get
    End Property



    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTable_ReportBuilder"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_ReportBuilder
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("08/01/07")
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
            Return 5
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim strcreate As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate = "CREATE TABLE ReportBuilder (Id [numeric](18, 0) NOT NULL, Name nVARCHAR(100) NOT NULL, Tables nVARCHAR(255) NOT NULL,Fields nVARCHAR(2000) NOT NULL,Relations nVARCHAR(1000) NULL,Conditions nVARCHAR(1000) NULL)"
        Else
            strcreate = "CREATE TABLE ReportBuilder (Id number(9) NOT NULL, Name VARCHAR2(100) NOT NULL, Tables VARCHAR2(255) NOT NULL,Fields VARCHAR2(1000) NOT NULL,Relations VARCHAR2(1000) NULL,Conditions VARCHAR2(1000) NULL)"
        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("ReportBuilder") = True Then
                Throw New Exception(Me.name & ": La tabla ReportBuilder ya existe en la base de datos.")
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
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
End Class
