Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32



Public Class PAQ_AlterTable_USRNOTES

    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTablaUSRNOTES"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTable_USRNOTES
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Modifica columnas de la tabla USRNOTES: CONF_PATHREMOTOARCH, CONF_PATHARCH a CHAR/nvarchar(100) y CONF_ARCHCTRL, CONF_EJECUTABLE, CONF_NOMUSERNOTES a CHAR/nvarchar(255)."
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("01/11/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("01/11/2006")
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
            Return 49
        End Get
    End Property

#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute

        Dim strSql As New System.Text.StringBuilder

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN ID NUMERIC(10)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN NOMBRE CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_PATHREMOTOARCH CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_MAILSERVER CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_BASEMAIL CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_PATHARCH CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_VISTAEXPORTACION CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_PAPELERA CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_NOMARCHTXT CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_SEQMSG NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_SEQATT NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_LOCKEO NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_ACUMIMG NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_LIMIMG NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_DESTEXT NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_TEXTOSUBJECT CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_BORRAR CHAR (100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_ARCHCTRL CHAR (255)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_SCHEDULESEL CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_SCHEDULEFIJO CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_SCHEDULEVAR CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_EJECUTABLE CHAR(255)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_NOMUSERNOTES CHAR(255)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_NOMUSERRED CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_CHARSREEMPSUBJ CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN LASTRUNTIME DATETIME")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_REINTENTO NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN ACTIVO NUMERIC(4)")
            strSql.Append("ALTER TABLE USRNOTES ALTER COLUMN CONF_SEQIMG NUMERIC(10)")

        Else
            strSql.Append("ALTER TABLE USRNOTES MODIFY (ID NUMERIC(10)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (NOMBRE CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_PATHREMOTOARCH CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_MAILSERVER CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_BASEMAIL CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_PATHARCH CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_VISTAEXPORTACION CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_PAPELERA CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_NOMARCHTXT CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_SEQMSG NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_SEQATT NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_LOCKEO NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_ACUMIMG NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_LIMIMG NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_DESTEXT NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_TEXTOSUBJECT CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_BORRAR CHAR (100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_ARCHCTRL CHAR (255)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_SCHEDULESEL CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_SCHEDULEFIJO CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_SCHEDULEVAR CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_EJECUTABLE CHAR(255)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_NOMUSERNOTES CHAR(255)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_NOMUSERRED CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_CHARSREEMPSUBJ CHAR(100)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (LASTRUNTIME DATETIME")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_REINTENTO NUMERIC (10)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (ACTIVO NUMERIC(4)")
            strSql.Append("ALTER TABLE USRNOTES MODIFY (CONF_SEQIMG NUMERIC(10)")

        End If

        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("USRNOTES") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, strSql.ToString)
            Else
                Throw New Exception(Me.name & ": la tabla usrnotes no existe en la base de datos.")
            End If
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(strSql.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
        Return True
        strSql = Nothing

    End Function

#End Region

End Class



