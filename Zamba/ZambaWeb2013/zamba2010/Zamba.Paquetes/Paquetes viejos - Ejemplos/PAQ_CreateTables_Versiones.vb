Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_CreateTables_Versiones
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim strcreate(5) As String
        Dim errormsjs As New System.Text.StringBuilder()

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate(0) = "CREATE TABLE ZVerConfig (DataId numeric(18,0) PRIMARY KEY NOT NULL, DtId numeric(4,0) NOT NULL, IndexId numeric(10,0) NOT NULL, CONSTRAINT FK_ZVerConfig_DOC_TYPE FOREIGN KEY (DtId) REFERENCES DOC_TYPE (DOC_TYPE_ID), CONSTRAINT FK_ZVerConfig_DOC_INDEX FOREIGN KEY (IndexId) REFERENCES DOC_INDEX (INDEX_ID))"
            strcreate(1) = "CREATE TABLE Z_Publish (PublishId numeric(18,0) NOT NULL, DocId numeric(18,0) NOT NULL, UserId numeric(18,0) NOT NULL, PublishDate datetime NOT NULL,CONSTRAINT PK_Z_Publish PRIMARY KEY (PublishId,DocId))"
            strcreate(2) = "CREATE TABLE ZVerEvents (EventId numeric(18,0) PRIMARY KEY NOT NULL, Event nvarchar(40) NOT NULL )"
            strcreate(3) = "CREATE TABLE ZVerEv (DataId numeric(18,0) NOT NULL, EventId numeric(18,0) NOT NULL, EvValue nvarchar(40) NOT NULL,CONSTRAINT PK_ZVerEv PRIMARY KEY (DataId,EventId), CONSTRAINT FK_ZVerEv FOREIGN KEY (EventId) REFERENCES ZVerEvents (EventId), CONSTRAINT FK_ZVerEv_ZverConfig FOREIGN KEY (DataId) REFERENCES ZVerConfig (DataId))"
            strcreate(4) = "CREATE TABLE ZComment (DOCID numeric(18, 0) NOT NULL, COMMENTS nvarchar(500) NOT NULL, CreateDate datetime NOT NULL)"
        Else
            strcreate(0) = "CREATE TABLE ZVerConfig (DataId NUMBER(18,0) PRIMARY KEY NOT NULL, DtId NUMBER(4,0) NOT NULL, IndexId NUMBER(10,0) NOT NULL, CONSTRAINT FK_ZVerConfig_DOC_TYPE FOREIGN KEY (DtId) REFERENCES DOC_TYPE (DOC_TYPE_ID), CONSTRAINT FK_ZVerConfig_DOC_INDEX FOREIGN KEY (IndexId) REFERENCES DOC_INDEX (INDEX_ID))"
            strcreate(1) = "CREATE TABLE Z_Publish (PublishId NUMBER(18,0) NOT NULL, DocId NUMBER(18,0) NOT NULL, UserId NUMBER(18,0) NOT NULL, PublishDate DATE NOT NULL,CONSTRAINT PK_Z_Publish PRIMARY KEY (PublishId,DocId))"
            strcreate(2) = "CREATE TABLE ZVerEvents (EventId NUMBER(18,0) PRIMARY KEY NOT NULL, Event NVARCHAR2(40) NOT NULL )"
            strcreate(3) = "CREATE TABLE ZVerEv (DataId NUMBER(18,0) NOT NULL, EventId NUMBER(18,0) NOT NULL, EvValue NVARCHAR2(40) NOT NULL,CONSTRAINT PK_ZVerEv PRIMARY KEY (DataId,EventId), CONSTRAINT FK_ZVerEv FOREIGN KEY (EventId) REFERENCES ZVerEvents (EventId), CONSTRAINT FK_ZVerEv_ZverConfig FOREIGN KEY (DataId) REFERENCES ZVerConfig (DataId))"
            strcreate(4) = "CREATE TABLE ZComment (DOCID NUMBER(18,0) NOT NULL, Comments VARCHAR2(500) NOT NULL, CreateDate DATE NOT NULL )"
        End If
        If GenerateScripts = False Then
            Try
                For index As Integer = 0 To 4
                    Try
                        Server.Con.ExecuteNonQuery(CommandType.Text, strcreate(index))
                    Catch ex As Exception
                        errormsjs.Append(" ***ERROR: " & index.ToString & ex.Message)
                    End Try
                Next

                Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO ZVerEvents(EventId, Event) VALUES(1,'Nueva Version')")
                Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO ZVerEvents(EventId, Event) VALUES(2,'Final')")

            Catch ex As Exception
                errormsjs.Append("ERROR | " & ex.ToString)
            End Try

        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(strcreate.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
        If errormsjs.Length > 1 Then
            Throw New Exception(Me.name + errormsjs.ToString)
        End If
        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTables_Versiones"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTables_Versiones
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea Tablas ZVerConfig, ZVerEv, ZPublish, ZVerEvents, ZComment"
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("16/08/2007")
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
            Return 29
        End Get
    End Property
End Class
