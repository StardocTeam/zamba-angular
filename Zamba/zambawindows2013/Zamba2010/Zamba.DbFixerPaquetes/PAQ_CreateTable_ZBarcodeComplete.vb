'ver
Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
'Imports System.Text

Public Class PAQ_CreateTable_ZBarcodeComplete
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute

        Dim strcreate As New System.Text.StringBuilder
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate.Append("CREATE TABLE [dbo].[ZBarcodeComplete] (")
            strcreate.Append("[Doctypeid] [numeric](18, 0) NOT NULL ,")
            strcreate.Append("[Indexid] [numeric](18, 0) NOT NULL ,")
            strcreate.Append("[Tabla] [varchar] (50) COLLATE Modern_Spanish_CI_AS NULL ,")
            strcreate.Append("[Columna] [varchar] (50) COLLATE Modern_Spanish_CI_AS NULL ,")
            strcreate.Append("[Clave] [numeric](18, 0) NULL ,")
            strcreate.Append("[Orden] [numeric](18, 0) NULL ,")
            strcreate.Append(")")
        Else
            strcreate.Append("CREATE TABLE ZBarcodeComplete (Doctypeid number(10) NOT NULL ,Indexid number(10) NOT NULL ,Tabla nvarchar2 (50),Columna nvarchar2 (50),Clave number(1) NULL ,Orden number(1) NULL, Conditions nvarchar2 (255) DEFAULT '=' NOT NULL)")
        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("ZBarcodeComplete") = True Then
                Throw New Exception(Me.name & ": La tabla ZBarcodeComplete ya existe en la base de datos.")
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString())
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(strcreate.ToString())
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "ZBarcodeComplete"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateTable_ZBarcodeComplete
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea la Tabla Autocompletar"
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("01/01/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "1.6.0"
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
            Return 10
        End Get
    End Property

End Class
