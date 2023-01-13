'ver
Imports Zamba.Servers
Imports Zamba.Core
Public Class PAQ_CreateTable_Foro
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTable_Foro"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_Foro
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la tabla y los store procedures a utilizar en el foro "
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
            Return 2
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        'CREA LA TABLA DEL FORO
        Dim strcreate As String
        If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
            strcreate = "CREATE TABLE ZForum (DOCT number(10) NOT NULL, DocId number(10) NOT NULL, IdMensaje number(10) NOT NULL, ParentId number(10) NOT NULL, LinkName nvarchar2 (60) NOT NULL, Mensaje nvarchar2 (300) NOT NULL, Fecha date  NOT NULL, UserId number(2) NOT NULL, StateId number (1) NOT NULL)"
        Else
            strcreate = "CREATE TABLE ZForum (DOCT numeric(10) NOT NULL, DocId numeric(10) NOT NULL, IdMensaje numeric(10) NOT NULL, ParentId numeric (10) NOT NULL, LinkName nvarchar (60) NOT NULL, Mensaje nvarchar (300) NOT NULL, Fecha datetime  NOT NULL, UserId numeric(2) NOT NULL, StateId numeric (1) NOT NULL)"
        End If

        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("ZForum") = True Then
                Throw New Exception(Me.name & " La tabla ZForum ya existe en la base de datos")
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

        Dim strconstraint As String
        If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
            strconstraint = "ALTER TABLE ZForum ADD CONSTRAINT pk_ZForum PRIMARY KEY (DOCT,DOCID,ParentId,IdMensaje)"
        Else
            strconstraint = "ALTER TABLE ZForum ADD CONSTRAINT pk_ZForum PRIMARY KEY (DOCT,DOCID,ParentId,IdMensaje)"
        End If

        If GenerateScripts = False Then
            Server.Con.ExecuteNonQuery(CommandType.Text, strconstraint.ToString)
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(strconstraint.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If

        'CREA EL STORE PROCEDURE DE INSERT
        Dim sb As New System.Text.StringBuilder

        If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
            'El SP de ORACLE se hace en el siguiente IF junto con el SP siguiente.
        Else
            ZPaq.IfExists("zsp_zforum_100_Insert_Foro", ZPaq.Tipo.StoredProcedure, True)

            sb.Append("CREATE PROCEDURE zsp_zforum_100_Insert_Foro")
            sb.Append(ControlChars.NewLine)
            sb.Append("@f_Doct int,")
            sb.Append(ControlChars.NewLine)
            sb.Append("@f_DocId int,")
            sb.Append(ControlChars.NewLine)
            sb.Append("@f_IdMensaje  int,")
            sb.Append(ControlChars.NewLine)
            sb.Append("@f_ParentId  int,")
            sb.Append(ControlChars.NewLine)
            sb.Append("@f_LinkMensaje nvarchar(50),")
            sb.Append(ControlChars.NewLine)
            sb.Append("@f_Mensaje nvarchar(50),")
            sb.Append(ControlChars.NewLine)
            sb.Append("@f_Fecha datetime,")
            sb.Append(ControlChars.NewLine)
            sb.Append("@f_UserId int,")
            sb.Append(ControlChars.NewLine)
            sb.Append("@f_StateId int")
            sb.Append(ControlChars.NewLine)
            sb.Append("As")
            sb.Append(ControlChars.NewLine)
            sb.Append("INSERT INTO ZForum(DocT,")
            sb.Append(ControlChars.NewLine)
            sb.Append("	     DocId,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     IdMensaje,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     ParentId,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     LinkName,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     Mensaje,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     Fecha,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     UserId,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     StateId) ")
            sb.Append(ControlChars.NewLine)
            sb.Append("		VALUES  (@f_Doct, ")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     @f_DocId,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     @f_IdMensaje,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     @f_ParentId,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     @f_LinkMensaje,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     @f_Mensaje,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     @f_Fecha,")
            sb.Append(ControlChars.NewLine)
            sb.Append("")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     @f_UserId,")
            sb.Append(ControlChars.NewLine)
            sb.Append("			     @f_StateId)")

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
        End If




        'CREA EL STORE PROCEDURE DE DELETE
        Dim sb2 As New System.Text.StringBuilder

        If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
            sb2.Append("create or replace package zsp_zforum_100 as Procedure delete_From_Foro(pdocid in zforum.docid%type , pdoct in zforum.doct%type , pparentid in zforum.parentid%type); Procedure insert_Foro(f_Doct in zforum.Doct%type,f_DocId in zforum.DocId%type ,f_IdMensaje  in zforum.IdMensaje%type,f_ParentId  in zforum.ParentId%type,f_LinkName in zforum.LinkName%type,f_Mensaje in zforum.mensaje%type,f_Fecha in zforum.fecha%type,f_UserId in zforum.userid%type,f_StateId in zforum.stateid%type); end zsp_zforum_100;")
            sb2.Append(Microsoft.VisualBasic.ControlChars.NewLine)
            sb2.Append("/")
            sb2.Append("create or replace package body zsp_zforum_100 as Procedure delete_From_Foro (pdocid in zforum.docid%type,pdoct in zforum.doct%type,pparentid in zforum.parentid%type) is begin Delete from ZForum where DocId=pdocid and DocT=pdoct and ParentId=pparentid; end delete_From_Foro; Procedure insert_Foro(f_Doct in zforum.Doct%type,f_DocId in zforum.DocId%type ,f_IdMensaje  in zforum.IdMensaje%type,f_ParentId  in zforum.ParentId%type,f_LinkName in zforum.LinkName%type,f_Mensaje in zforum.mensaje%type,f_Fecha in zforum.fecha%type,f_UserId in zforum.userid%type,f_StateId in zforum.stateid%type) is begin INSERT INTO ZForum(DocT,DocId,IdMensaje,ParentId,LinkName,Mensaje,Fecha,UserId,StateId) VALUES (f_Doct, f_DocId,f_IdMensaje,f_ParentId,f_LinkName,f_Mensaje,f_Fecha,f_UserId,f_StateId); end insert_Foro; end zsp_zforum_100;")
            sb2.Append(Microsoft.VisualBasic.ControlChars.NewLine)
            sb2.Append("/")
        Else
            ZPaq.IfExists("zsp_zforum_100_delete_From_Foro", ZPaq.Tipo.StoredProcedure, True)

            'sb2.Append("SET QUOTED_IDENTIFIER ON ")
            'sb2.Append(ControlChars.NewLine)
            'sb2.Append("GO")
            'sb2.Append(ControlChars.NewLine)
            'sb2.Append("SET ANSI_NULLS OFF ")
            'sb2.Append(ControlChars.NewLine)
            'sb2.Append("GO")
            'sb2.Append(ControlChars.NewLine)
            sb2.Append("CREATE Proc zsp_zforum_100_delete_From_Foro")
            sb2.Append(ControlChars.NewLine)
            sb2.Append("@docid int,")
            sb2.Append(ControlChars.NewLine)
            sb2.Append("@doct int,")
            sb2.Append(ControlChars.NewLine)
            sb2.Append("@parentid int")
            sb2.Append(ControlChars.NewLine)
            sb2.Append("as")
            sb2.Append(ControlChars.NewLine)
            sb2.Append("Delete from ZForum where DocId=@docid and DocT=@doct and ParentId=@parentid")
            'sb2.Append(ControlChars.NewLine)
            'sb2.Append("GO")
            'sb2.Append(ControlChars.NewLine)
            'sb2.Append("SET QUOTED_IDENTIFIER OFF ")
            'sb2.Append(ControlChars.NewLine)
            'sb2.Append("GO")
            'sb2.Append(ControlChars.NewLine)
            'sb2.Append("SET ANSI_NULLS ON ")
            'sb2.Append(ControlChars.NewLine)
            'sb2.Append("GO")
        End If

        If GenerateScripts = False Then
            Server.Con.ExecuteNonQuery(CommandType.Text, sb2.ToString)
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(sb2.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
        Return True
    End Function

#End Region


End Class
