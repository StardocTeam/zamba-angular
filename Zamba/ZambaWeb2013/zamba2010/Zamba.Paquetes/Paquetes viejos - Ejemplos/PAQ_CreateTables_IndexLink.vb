'ver
Imports Zamba.Servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32

Public Class PAQ_CreateTables_IndexLink
    Inherits ZPaq
    Implements IPAQ
    Private Const CREATETABLE As String = "CREATE TABLE"
    Private Shared INDEX_LINK_SERVER() As String = {"CREATE TABLE INDEX_LINK (ID NUMeric(10) NOT NULL,NAME VARCHAR(20) NOT NULL,DESCRIPCION VARCHAR(200))" _
                                            , "alter table index_link add primary key(ID)"}
    Private Shared INDEX_LINK_INFO_SERVER() As String = {"CREATE TABLE INDEX_LINK_INFO (ID NUMeric(10) NOT NULL,DATA VARCHAR(100), FLAG NUMeric(1) DEFAULT 0 NOT NULL, DOCTYPE NUMeric(4,0) NOT NULL, DOCINDEX NUMeric(4,0) NOT NULL,NAME varchar(50) NOT NULL)" _
                                                    , "alter table INDEX_LINK_INFO add FOREIGN KEY(DOCINDEX, DOCTYPE) REFERENCES INDEX_R_DOC_TYPE(INDEX_ID,DOC_TYPE_ID) ON DELETE CASCADE" _
                                                    , "alter table index_link_info add foreign key(ID) references index_link(ID) on delete cascade"}

    Private Shared INDEX_LINK_ORACLE() As String = {"CREATE TABLE INDEX_LINK (ID NUMBER(10) NOT NULL, NAME VARCHAR2(20) NOT NULL, DESCRIPCION VARCHAR2(200), CONSTRAINT IDPK PRIMARY KEY(ID), CONSTRAINT UNIQUENAME UNIQUE(NAME)  USING INDEX)"}
    Private Shared INDEX_LINK_INFO_ORACLE() As String = {"CREATE TABLE INDEX_LINK_INFO (ID NUMBER(10) NOT NULL,DATA VARCHAR2(100), FLAG NUMBER(1) DEFAULT 0 NOT NULL, DOCTYPE NUMBER(10) NOT NULL, DOCINDEX NUMBER(10) NOT NULL,NAME varchar2(50), CONSTRAINT INDEX_LINK_FK FOREIGN KEY(DOCINDEX, DOCTYPE) REFERENCES INDEX_R_DOC_TYPE(INDEX_ID,DOC_TYPE_ID) ON DELETE CASCADE )"}

    Public Overrides Sub Dispose()

    End Sub
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return datetime.Parse("03/02/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Si no existen las tablas INDEX_LINK e INDEX_LINK_INFO se crean, también se crean PRIMARY KEYS y FOREIGN KEYS para las tablas, también se crea una PRIMARY KEY en INDEX_R_DOC_TYPE , de existir no hace nada. Es condición que la tabla INDEX_R_DOC_TYPE ya exista."
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            If Not ZPaq.IfExists(obtenerNombreTabla(INDEX_LINK_SERVER(0), CREATETABLE), ZPaq.Tipo.Table, Me.CanDrop) Then
                Dim SQL As String
                For Each SQL In INDEX_LINK_SERVER
                    Try
                        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)
                    Catch ex As Exception
                        Dim exn As New Exception("ABORTO | No se pudo crear la tabla " & obtenerNombreTabla(INDEX_LINK_SERVER(0), CREATETABLE) & " , excepción: " & ex.ToString)
                        'ZException.Log(exn, False)
                        Return False
                    End Try
                Next
            End If

            'Hago que sean primary key para que despues puedan ser foreign key
            Try
                'alter table index_r_doc_type add primary key (INDEX_ID,DOC_TYPE_ID)
                Server.Con.ExecuteNonQuery(CommandType.Text, "alter table index_r_doc_type add primary key (INDEX_ID,DOC_TYPE_ID)")
            Catch ex As Exception
                Dim exn As New Exception("AVISO | No se pudo crear la PRIMARY KEY en Index_r_doc_type, puede que no se pueda crear la tabla INDEX_LINK_INFO. Excepción: " & ex.ToString)
                'ZException.Log(exn, False)
            End Try

            If Not ZPaq.IfExists(obtenerNombreTabla(INDEX_LINK_INFO_SERVER(0), CREATETABLE), ZPaq.Tipo.Table, Me.CanDrop) Then
                Dim SQL As String
                For Each SQL In INDEX_LINK_INFO_SERVER
                    Try
                        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)
                    Catch ex As Exception
                        Dim exn As New Exception("ABORTO | No se pudo crear la tabla " & obtenerNombreTabla(INDEX_LINK_INFO_SERVER(0), CREATETABLE) & " , excepción: " & ex.ToString)
                        Return False
                    End Try
                Next
            End If

        Else

            If Not ZPaq.IfExists(obtenerNombreTabla(INDEX_LINK_ORACLE(0), CREATETABLE), ZPaq.Tipo.Table, Me.CanDrop) Then
                Dim SQL As String
                For Each SQL In INDEX_LINK_ORACLE
                    Try
                        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)
                    Catch ex As Exception
                        Dim exn As New Exception("ABORTO | No se pudo crear la tabla " & obtenerNombreTabla(INDEX_LINK_ORACLE(0), CREATETABLE) & " , excepción: " & ex.ToString)
                        'ZException.Log(exn, False)
                        Return False
                    End Try
                Next
            End If

            'Hago que sean primary key para que despues puedan ser foreign key
            Try
                'alter table index_r_doc_type add primary key (INDEX_ID,DOC_TYPE_ID)
                Server.Con.ExecuteNonQuery(CommandType.Text, "alter table index_r_doc_type add primary key (INDEX_ID,DOC_TYPE_ID)")
            Catch ex As Exception
                Dim exn As New Exception("AVISO | No se pudo crear la PRIMARY KEY en Index_r_doc_type, puede que no se pueda crear la tabla INDEX_LINK_INFO. Excepción: " & ex.ToString)
                'ZException.Log(exn, False)
            End Try

            If Not ZPaq.IfExists(obtenerNombreTabla(INDEX_LINK_INFO_ORACLE(0), CREATETABLE), ZPaq.Tipo.Table, Me.CanDrop) Then
                Dim SQL As String
                For Each SQL In INDEX_LINK_INFO_ORACLE
                    Try
                        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)
                    Catch ex As Exception
                        Dim exn As New Exception("ABORTO | No se pudo crear la tabla " & obtenerNombreTabla(INDEX_LINK_INFO_ORACLE(0), CREATETABLE) & " , excepción: " & ex.ToString)
                        'ZException.Log(exn, False)
                        Return False
                    End Try
                Next
            End If

        End If
        Return True
    End Function
    Private Shared Function obtenerNombreTabla(ByVal sSql As String, ByVal sCreateTable As String) As String
        Dim i As Int32
        Dim sTabla As String

        sTabla = sSql.ToUpper.Replace(sCreateTable, "").Trim
        i = sTabla.IndexOf(" ")
        Return sTabla.Substring(0, i)
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTablesIndexLink"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTables_IndexLink
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
            Return 21
        End Get
    End Property
End Class
