Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_CreateTable_USRNOTES
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTable_USRNOTES"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_USRNOTES
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la tabla USRNOTES "
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("05/12/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("05/12/2006")
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
            Return 8
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim Sql As New System.Text.StringBuilder

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            Sql.Append("CREATE TABLE [USRNOTES] (")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[ID] [numeric](10, 0) NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[NOMBRE] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_PATHREMOTOARCH] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_MAILSERVER] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_BASEMAIL] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_PATHARCH] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_VISTAEXPORTACION] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_PAPELERA] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_NOMARCHTXT] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_SEQMSG] [numeric](10, 0) NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_SEQATT] [numeric](10, 0) NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_LOCKEO] [numeric](10, 0) NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_ACUMIMG] [numeric](10, 0) NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_LIMIMG] [numeric](10, 0) NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_DESTEXT] [numeric](10, 0) NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_TEXTOSUBJECT] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_BORRAR] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_ARCHCTRL] [char] (255) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_SCHEDULESEL] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_SCHEDULEFIJO] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_SCHEDULEVAR] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_EJECUTABLE] [char] (255) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_NOMUSERNOTES] [char] (255) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_NOMUSERRED] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_CHARSREEMPSUBJ] [char] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[LASTRUNTIME] [datetime] NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_REINTENTO] [numeric](10, 0) NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[ACTIVO] [numeric](4, 0) NULL ,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	[CONF_SEQIMG] [numeric](10, 0) NULL ")
            Sql.Append(ControlChars.NewLine)
            Sql.Append(") ON [PRIMARY]")

        Else
            '** ORACLE
            Sql.Append("CREATE TABLE USRNOTES2 (")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	ID number(10),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	NOMBRE nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_PATHREMOTOARCH nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_MAILSERVER nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_BASEMAIL nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_PATHARCH nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_VISTAEXPORTACION nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_PAPELERA nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_NOMARCHTXT nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_SEQMSG number(10),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_SEQATT number(10),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_LOCKEO number(10),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_ACUMIMG number(10),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_LIMIMG number(10),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_DESTEXT number(10),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_TEXTOSUBJECT nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_BORRAR nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_ARCHCTRL nvarchar2(255),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_SCHEDULESEL nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_SCHEDULEFIJO nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_SCHEDULEVAR nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_EJECUTABLE nvarchar2(255),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_NOMUSERNOTES nvarchar2(255),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_NOMUSERRED nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_CHARSREEMPSUBJ nvarchar2(100),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	LASTRUNTIME date,")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_REINTENTO number(10),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	ACTIVO number(4),")
            Sql.Append(ControlChars.NewLine)
            Sql.Append("	CONF_SEQIMG number(10) )")

        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("USRNOTES2") = True Or ZPaq.ExisteTabla("USRNOTES") = True Then
                Throw New Exception(Me.name & " La tabla USRNOTES o USRNOTES2 ya existe en la base de datos")
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, Sql.ToString)
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(Sql.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
        Return True
        Sql = Nothing
    End Function
#End Region

End Class
