'listo
Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class PAQ_CreateTable_ZBarCode
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim strcreate As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate = "CREATE TABLE ZBarCode (ID numeric(18, 0) NOT NULL ,FECHA datetime NOT NULL ,DOC_TYPE_ID numeric(18, 0) NOT NULL ,USERID numeric(18, 0) NOT NULL ,SCANNED varchar (2) NULL ,SCANNEDDATE datetime NULL ,	DOC_ID numeric(18, 0) NULL ,BATCH varchar (10) NULL ,BOX numeric(18, 0) NULL) "
        Else
            strcreate = "CREATE TABLE ZBarCode (ID number(18, 0) NOT NULL ,FECHA date NOT NULL ,DOC_TYPE_ID number(18, 0) NOT NULL ,USERID number(18, 0) NOT NULL ,SCANNED varchar2 (2) NULL ,SCANNEDDATE date NULL ,	DOC_ID number(18, 0) NULL ,BATCH varchar2 (10) NULL ,BOX number(18, 0) NULL)"
        End If

        Try
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("ZBarCode") Then
                    Throw New Exception(Me.name & ": La tabla ZBarCode ya existe en la base de datos.")
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
        Catch ex As Exception
            Dim exbuilder As New System.Text.StringBuilder
            exbuilder.AppendLine(strcreate)
            exBuilder.Append("Error: ")
            exBuilder.Append(ex.ToString())
            exBuilder.AppendLine()
            exBuilder.AppendLine()
            Throw New Exception(exbuilder.ToString())
        End Try
        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateTable_ZBarCode"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateTable_ZBarCode
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea la tabla ZBarCode."
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("30/01/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
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
            Return 9
        End Get
    End Property

End Class
