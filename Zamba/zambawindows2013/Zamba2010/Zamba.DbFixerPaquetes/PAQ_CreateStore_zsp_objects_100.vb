Imports zamba.servers

Public Class PAQ_CreateStore_zsp_objects_100
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("07/03/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.1"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea o Sobreescribe el Procedimiento zsp_objects_100"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateStore_zsp_objects_100"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStore_zsp_objects_100
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
            Return 76
        End Get
    End Property

#End Region
#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
            Try
                ReplaceStoredPKG(GenerateScripts)
                ReplaceStoredBody(GenerateScripts)
            Catch ex As Exception
                Return False
            End Try
            Return True
        End If
    End Function
    Private Shared Sub ReplaceStoredPKG(ByVal GenerateScripts As Boolean)
        Dim sb As New System.Text.StringBuilder
        sb.Append("Create or replace package zsp_objects_100 As ")
        sb.Append("TYPE t_cursor IS REF CURSOR;")
        sb.Append("PROCEDURE GetAndSetLastId (OBJTYPE IN OBJLASTID.OBJECT_TYPE_ID%TYPE,  io_cursor OUT t_cursor);")
        sb.Append("end;")
        If GenerateScripts = False Then
            Server.Con.ExecuteNonQuery(CommandType.Text, sb.ToString)
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(sb.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
    End Sub
    Private Shared Sub ReplaceStoredBody(ByVal GenerateScripts As Boolean)
        Dim sb As New System.Text.StringBuilder
        sb.Append("CREATE OR REPLACE  PACKAGE BODY ZSP_OBJECTS_100 As ")
        sb.Append(" PROCEDURE GetAndSetLastId (OBJTYPE IN OBJLASTID.OBJECT_TYPE_ID%TYPE, io_cursor OUT t_cursor) IS")
        sb.Append(" s_cursor t_cursor;")
        sb.Append(" r_cursor t_cursor;")
        sb.Append(" OBJID OBJLASTID.OBJECTID%TYPE; ")
        sb.Append("BEGIN")
        sb.Append(" OPEN s_cursor FOR SELECT OBJECTID FROM OBJLASTID WHERE OBJECT_TYPE_ID = OBJTYPE;")
        sb.Append(" IF SQL%NotFound then    Insert into Objlastid(Object_type_id,objectid) values(OBJTYPE,0);          open r_cursor for select objectid from objlastid where object_type_id=objtype;          io_cursor:=r_cursor;        else          io_cursor := s_cursor;        End If;        UPDATE OBJLASTID SET OBJECTID = OBJECTID + 1  WHERE OBJECT_TYPE_ID =  OBJTYPE; END;end zsp_objects_100;")
        If GenerateScripts = False Then
            Server.Con.ExecuteNonQuery(CommandType.Text, sb.ToString)
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(sb.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
    End Sub

#End Region
End Class
