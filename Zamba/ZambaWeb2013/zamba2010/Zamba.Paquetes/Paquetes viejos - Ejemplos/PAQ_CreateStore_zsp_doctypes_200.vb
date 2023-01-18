Imports zamba.servers
Public Class PAQ_CreateStore_zsp_doctypes_200
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_GETRIGHTS"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateStore_zsp_doctypes_200
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("14/06/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Genera el Stored Procedure que dado un UserId devuelve el listado de la Tabla Doc_Type"
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("14/07/2006")
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
            Return 73
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        If Server.ServerType = Server.DBTYPES.OracleClient OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle Then
            Return Me.ExecOracle(GenerateScripts)
        Else
            Return Me.ExecSQL(GenerateScripts)
        End If
    End Function
    Private Function ExecOracle(ByVal GenerateScripts As Boolean) As Boolean
        Try
            Dim sql As System.Text.StringBuilder
            sql = New System.Text.StringBuilder
            sql.Append("CREATE OR REPLACE PACKAGE zsp_doctypes_200 AS ")
            sql.Append("TYPE t_cursor IS REF CURSOR;")
            sql.Append("PROCEDURE GetDocTypesByUserRights(userid IN usrtable.id%type, righttype IN Number, io_cursor OUT t_cursor);")
            sql.Append("End;")
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = Nothing
            sql = New System.Text.StringBuilder
            sql.Append("CREATE OR REPLACE PACKAGE BODY zsp_doctypes_200")
            sql.Append(ControlChars.NewLine)
            sql.Append("AS")
            sql.Append(ControlChars.NewLine)
            sql.Append("PROCEDURE GetDocTypesByUserRights(userid IN usrtable.id%type, righttype IN Number, io_cursor OUT t_cursor)IS ")
            sql.Append(ControlChars.NewLine)
            sql.Append("v_cursor t_cursor;")
            sql.Append(ControlChars.NewLine)
            sql.Append("BEGIN")
            sql.Append(ControlChars.NewLine)
            sql.Append("OPEN v_cursor FOR Select * from doc_type where Doc_type_id in (Select distinct(aditional) from usr_rights")
            sql.Append(ControlChars.NewLine)
            sql.Append("where (GROUPID in (Select groupid from usr_r_group where usrid=userid) or groupId=userid) And (objid=2 and rtype=righttype))")
            sql.Append(ControlChars.NewLine)
            sql.Append("order by doc_type_name;")
            sql.Append(ControlChars.NewLine)
            sql.Append("io_cursor := v_cursor;")
            sql.Append(ControlChars.NewLine)
            sql.Append("END GetDocTypesByUserRights;")
            sql.Append(ControlChars.NewLine)
            sql.Append("END zsp_doctypes_200;")
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = Nothing
            Return True
        Catch
            Return False
        End Try
    End Function
    Private Function ExecSQL(ByVal GenerateScripts As Boolean) As Boolean
        'TODO store: zsp_doctypes_200_GetDocTypesByUserRights
        Try
            Dim sql As System.Text.StringBuilder
            sql = New System.Text.StringBuilder
            sql.Append("Create Procedure zsp_doctypes_200_GetDocTypesByUserRights")
            sql.Append(ControlChars.NewLine)
            sql.Append("@usrid int,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@rtype int")
            sql.Append(ControlChars.NewLine)
            sql.Append("As")
            sql.Append(ControlChars.NewLine)
            sql.Append("Select * from doc_type where Doc_type_id in (Select distinct(aditional) from usr_rights")
            sql.Append(ControlChars.NewLine)
            sql.Append("where (GROUPID in (Select groupid from usr_r_group where usrid=@usrid) or groupId=@usrid) And (objid=2 and rtype=@rtype))")
            sql.Append(ControlChars.NewLine)
            sql.Append("order by doc_type_name")
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = Nothing
            Return True
        Catch
            Return False
        End Try
        Return False
    End Function
#End Region

End Class
