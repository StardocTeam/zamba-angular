Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_CreateStore_ZSP_VERSION_300
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateStore_ZSP_VERSION_300"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStore_ZSP_VERSION_300
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea los Stores PAQ_ZSP_VERSION_300 para SQL y ORACLE, "
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("31/05/2007")
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("31/05/2007")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "3.0.0"
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
            Return 79
        End Get
    End Property

#End Region

#Region "Ejecuición"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Try
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

                ExecSQL(GenerateScripts)
            Else
                ExecOracle(GenerateScripts)

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return True

    End Function
    Private Shared Sub ExecSQL(ByVal generatescripts As Boolean)
        Dim sql As New System.Text.StringBuilder
        Dim sql2 As New System.Text.StringBuilder
        Try

            sql.Append("CREATE PROC ZSP_VERSION_300_INSERTVERSIONCOMMENT @docid int,@comment nvarchar(500) AS insert into ZComment(docid,comments,CreateDate) values (@docid,@comment,getdate())")

            If generatescripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If

            sql.Remove(0, sql.Length)

            sql.Append("CREATE PROC ZSP_VERSION_300_GETVERSIONDETAILS @docId int AS Select comments,CreateDate from ZComment where docid= @docid")
            If generatescripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If

            'sql.Remove(0, sql.Length)

            'sql.Append("CREATE PROC ZSP_VERSION_300_GETVERSIONDETAILS @docId int AS Select comments,CreateDate from ZComment where docid= @docid")
            'If generatescripts = False Then
            '    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            'Else
            '    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            '    sw.WriteLine("")
            '    sw.WriteLine(sql.ToString)
            '    sw.WriteLine("")
            '    sw.Close()
            '    sw = Nothing
            'End If

            '****
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub
    Private Shared Sub ExecOracle(ByVal generatescripts As Boolean)
        Dim sql As New System.Text.StringBuilder
        Try
            '--ENCABEZADO DE PAQUETE ORACLE
            sql.Append("CREATE OR REPLACE PACKAGE " & Chr(34) & "ZSP_VERSION_300" & Chr(34) & " as type ")
            sql.Append(" t_cursor is ref cursor;")
            sql.Append("Procedure GETVERSIONDETAILS(Param_docId in number,io_cursor out t_cursor);")

            sql.Append("Procedure INSERTVERSIONCOMMENT(Par_docId IN number,Par_comment in varchar2);")
            sql.Append("Procedure INSERTPUBLISH(Parm_publishid IN number,Parm_docid IN number,Parm_userid IN number,Par_publishdate in date);")
            sql.Append(" end ZSP_VERSION_300;")
            If generatescripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If

            sql.Remove(0, sql.Length)

            '--CUERPO DEL PAQUETE
            sql.Append("CREATE OR REPLACE PACKAGE BODY " & Chr(34) & "ZSP_VERSION_300" & Chr(34) & " as Procedure GETVERSIONDETAILS(Param_docId in number,io_cursor out t_cursor) is v_cursor t_cursor;")
            sql.Append("BEGIN ")
            sql.Append(" open v_cursor for Select * from ZComment where docid= Param_docId;")
            sql.Append(" io_cursor:=v_cursor;")
            sql.Append(" end GETVERSIONDETAILS;")

            sql.Append(" Procedure INSERTVERSIONCOMMENT(Par_docId in number,Par_comment in varchar2)                      ")
            sql.Append(" IS")
            sql.Append(" v_cursor t_cursor; ")
            sql.Append(" begin ")
            sql.Append("  INSERT INTO ZComment VALUES (Par_docId,Par_comment,sysdate); COMMIT; ")
            sql.Append(" end INSERTVERSIONCOMMENT;")
            sql.Append("procedure INSERTPUBLISH(Parm_publishid IN number,Parm_docid IN number,Parm_userid IN number,Par_publishdate in date) is v_cursor t_cursor;  begin INSERT INTO Z_Publish(PublishId, DocId, UserId, PublishDate) VALUES(Parm_publishid, Parm_docid, Parm_userid, Par_publishdate); COMMIT;  end INSERTPUBLISH;")
            sql.Append("END ZSP_VERSION_300;")
            '---
            If generatescripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

End Class
