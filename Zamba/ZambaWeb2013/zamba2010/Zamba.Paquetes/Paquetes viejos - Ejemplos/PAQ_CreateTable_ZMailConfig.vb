Imports Zamba.Servers
Imports zamba.Core
Imports Microsoft.Win32
Imports system.Text
Public Class PAQ_CreateTable_ZMailConfig

    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub
#Region "IPAQ Members"
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la tabla ZMailConfig; tabla donde se guardan las configuraciones de mail de los usuarios."
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTableZMailConfig"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_ZMailConfig
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute

        Dim sqlBuilder As New StringBuilder
        Dim flagException As Boolean = False
        Dim exBuilder As New StringBuilder

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sqlBuilder.Append("CREATE TABLE [ZMailConfig]([UserName] [varchar](50) NOT NULL,[UserPassword] [varchar](50) NOT NULL,[ProveedorSMTP] [nvarchar](50) NULL,[Puerto] [int] NOT NULL,[Correo] [varchar](50) NOT NULL,[MailServer] [nvarchar](50) NULL,[BaseMail] [nvarchar](30) NULL,[MailType] [int] NOT NULL,[UserID] [numeric](10,0) NOT NULL)")
        Else
            sqlBuilder.Append("CREATE TABLE ZMailConfig (UserName varchar(50) NOT NULL,UserPassword varchar(50) NOT NULL,ProveedorSMTP varchar(50) NULL,Puerto int NOT NULL,Correo varchar(50) NOT NULL,MailServer varchar(50) NULL,BaseMail varchar(30) NULL,MailType int NOT NULL,UserID numeric NOT NULL)")
        End If
        Try
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("ZMailConfig") = True Then
                    Throw New Exception(Me.name & ": La tabla ZMailConfig ya existe en la base de datos.")
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sqlBuilder.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            exBuilder.AppendLine(sqlBuilder.ToString())
            exBuilder.Append("Error: ")
            flagException = True
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try

        sqlBuilder.Remove(0, sqlBuilder.Length)

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sqlBuilder.Append("ALTER TABLE [ZMailConfig] ADD CONSTRAINT [PK_ZMailConfig] PRIMARY KEY CLUSTERED ([UserID] ASC)")
        Else
            sqlBuilder.Append("ALTER TABLE ZMailConfig ADD CONSTRAINT PK_ZMailConfig PRIMARY KEY (UserID)")
        End If

        Try
            If GenerateScripts = False Then
                If Not ZPaq.ExisteTabla("ZMailConfig") = True Then
                    Throw New Exception(Me.name & ": La tabla ZMailConfig ya existe en la base de datos.")
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sqlBuilder.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            exBuilder.AppendLine(sqlBuilder.ToString())
            exBuilder.Append("Error: ")
            flagException = True
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sqlBuilder.Append("ALTER TABLE [ZMailConfig] ADD CONSTRAINT [FK_ZMailConfig] FOREIGN KEY ([UserID]) REFERENCES [USRTABLE] ([ID]))")
        Else
            sqlBuilder.Append("ALTER TABLE ZMailConfig ADD CONSTRAINT FK_ZMailConfig FOREIGN KEY (UserID) REFERENCES USRTABLE (ID)")
        End If

        Try
            If GenerateScripts = False Then
                If Not ZPaq.ExisteTabla("ZMailConfig") = True Then
                    Throw New Exception(Me.name & ": La tabla ZMailConfig ya existe en la base de datos.")
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sqlBuilder.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            exBuilder.AppendLine(sqlBuilder.ToString())
            exBuilder.Append("Error: ")
            flagException = True
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try

        If flagException Then
            Throw New Exception(Me.name & exBuilder.ToString())
        End If

        Return True

    End Function
#End Region

#Region "ZPaq Members"
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("07/08/2007")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("07/08/2007")
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
            Return 28
        End Get
    End Property
#End Region
End Class