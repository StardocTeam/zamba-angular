Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_CreateStore_ZSP_SECURITY_100
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

    
#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_StoresZSP_SECURITY_100"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStore_ZSP_SECURITY_100
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea los Stores ZSP_SECURITY_100_xxx para SQL y ORACLE, "
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("14/12/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("14/12/2006")
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
            Return 77
        End Get
    End Property


#End Region

#Region "Ejecución"
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
        Try

            sql.Append("CREATE PROC zsp_security_100_UpdUserRight @Rightv  numeric, @Rightid  numeric AS UPDATE USER_RIGHTS SET Right_Value =@Rightv  WHERE Right_Id =@Rightid")
            If generatescripts = False Then
                ExecSQL(generatescripts)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If

            'Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            sql.Append("CREATE Proc zsp_security_100_InsertStation @Pc nvarchar(50),@WinUser nvarchar(50),@ver  nvarchar(5) As Declare @stationid numeric IF (SELECT count(*) FROM ESTREG  WHERE M_Name=@PC)=0 Begin   select @stationid=objectid  from objlastid where object_type_id=35   Insert into Estreg(ID,M_Name,W_User,Ver,updated,last_check) values(@stationid,@PC,@Winuser,@Ver,GetDate(),GetDate())   SELECT * FROM ESTREG  WHERE M_Name=@PC End Else  SELECT * FROM ESTREG  WHERE M_Name=@PC ")
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

            ' Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            sql.Append("CREATE PROCEDURE zsp_security_100_GetUserDocumentsResctrictions @UserID int AS select RESTRICTION_ID from doc_restriction_r_user where [user_id]=@UserId")
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

            'Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            sql.Append("CREATE Proc zsp_security_100_GetDocTypeRights @UserId numeric As SELECT DOC_TYPE.DOC_TYPE_ID, DOC_TYPE.DOC_TYPE_NAME, DOC_TYPE.FILE_FORMAT_ID, DOC_TYPE.DISK_GROUP_ID, DOC_TYPE.THUMBNAILS, DOC_TYPE.ICON_ID, DOC_TYPE.CROSS_REFERENCE, DOC_TYPE.LIFE_CYCLE, DOC_TYPE.OBJECT_TYPE_ID, DOC_TYPE.AUTONAME, DOC_TYPE.DocumentalId, DOC_TYPE.DOCCOUNT FROM DOC_TYPE INNER JOIN USR_RIGHTS ON DOC_TYPE.DOC_TYPE_ID = USR_RIGHTS.ADITIONAL WHERE (USR_RIGHTS.GROUPID = @userID) AND (USR_RIGHTS.RTYPE = 3) AND (USR_RIGHTS.OBJID = 2) ORDER BY DOC_TYPE.DOC_TYPE_NAME; ")
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
            'Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            sql.Append("Create Proc zsp_security_100_GetArchivosUserRight @UserId int As SELECT distinct(dtg.Doc_Type_Group_ID),dtg.Doc_Type_Group_Name,dtg.Icon,dtg.Parent_Id,dtg.Object_Type_Id,urv.User_Id,urv.Right_Type FROM  DOC_TYPE_GROUP dtg, Zvw_USR_Rights_100 urv WHERE dtg.Doc_Type_Group_ID = urv.Aditional AND dtg.Object_Type_Id = urv.ObjectID and urv.User_Id =@Userid ORDER BY dtg.Doc_Type_Group_ID ")
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
            'Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            '*****New SP Added January 22,2007
            sql.Append("Create procedure zsp_Security_100_GetNewRights @userid int,@objid int,@rtype int,@aditional int AS ")
            sql.Append(ControlChars.NewLine)
            sql.Append("declare @cant int")
            sql.Append(ControlChars.NewLine)
            sql.Append("Select @cant=count(*) from usr_rights Where Aditional=@aditional and rtype=@rtype and objid=@objid and (Groupid = @userid or Groupid in (Select Groupid from usr_R_group Where USRID=@userid))")
            sql.Append(ControlChars.NewLine)
            sql.Append("if @cant>1")
            sql.Append(ControlChars.NewLine)
            sql.Append("    Select 1")
            sql.Append(ControlChars.NewLine)
            sql.Append("Else")
            sql.Append(ControlChars.NewLine)
            sql.Append("Select 0")


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

            '****
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub
    Private Shared Sub ExecOracle(ByVal generatescripts As Boolean)
        Dim sql As New System.Text.StringBuilder
        Try
            '--ENCABEZADO DE PAQUETE ORACLE
            sql.Append("CREATE OR REPLACE  PACKAGE " & Chr(34) & "ZSP_SECURITY_100" & Chr(34) & " as type ")
            sql.Append(" t_cursor is ref cursor;  Procedure ")
            sql.Append(" GetArchivosUserRight(UserId in ")
            sql.Append(" Zvw_USR_Rights_100.user_id%type, io_cursor out ")
            sql.Append(" t_cursor);Procedure GetDocTypeRights(userID IN number,")
            sql.Append(" io_cursor OUT t_cursor);PROCEDURE ")
            sql.Append(" GetUserDocumentsResctrictions(UserID IN ")
            sql.Append(" User_Table.User_ID%TYPE,io_cursor OUT t_cursor);PROCEDURE ")
            sql.Append(" InsertStation(idd IN ESTREG.ID%TYPE,io_cursor OUT ")
            sql.Append(" t_cursor);PROCEDURE UpdUserRight(rightv IN ")
            sql.Append(" USER_RIGHTS.RIGHT_VALUE%TYPE,rightid IN ")
            sql.Append(" USER_RIGHTS.RIGHT_ID%TYPE);")
            '---02/03/2007
            sql.Append(" PROCEDURE GETNEWRIGHTS(P_userid numeric,P_objid numeric,P_rtype numeric,P_aditional numeric);")
            sql.Append(" end zsp_security_100;")
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
            sql.Append("CREATE OR REPLACE PACKAGE BODY " & Chr(34) & "ZSP_SECURITY_100" & Chr(34) & " as Procedure GetArchivosUserRight(UserId in ")
            sql.Append(" Zvw_USR_Rights_100.user_id%type, io_cursor out t_cursor) is  v_cursor t_cursor;  ")
            sql.Append(" begin ")
            sql.Append(" open v_cursor for SELECT ")
            sql.Append(" distinct(dtg.Doc_Type_Group_ID),dtg.Doc_Type_Group_Name,dtg.Icon,dtg.Parent_Id,dtg.Object_Type_Id,urv.User_Id,urv.Right_Type ")
            sql.Append(" FROM DOC_TYPE_GROUP dtg, Zvw_USR_Rights_100 urv  WHERE dtg.Doc_Type_Group_ID = urv.Aditional AND dtg.Object_Type_Id = urv.ObjectID and urv.User_Id =Userid")
            sql.Append(" ORDER BY dtg.Doc_Type_Group_ID;    io_cursor:=v_cursor;")
            sql.Append(" end ")
            sql.Append("GetArchivosUserRight;Procedure GetDocTypeRights(userID IN number,io_cursor OUT t_cursor) IS   v_cursor t_cursor;   ")
            sql.Append("BEGIN  ")
            sql.Append(" OPEN v_cursor FOR Select")
            sql.Append(" Doc_Type.Doc_Type_Id, Doc_Type.Doc_Type_Name, Doc_Type.File_Format_ID, Doc_Type.Disk_Group_ID,Doc_Type.Thumbnails, Doc_Type.Icon_Id, ")
            sql.Append(" Doc_Type.Cross_Reference, Doc_Type.Life_Cycle, Doc_Type.Object_Type_Id,")
            sql.Append(" Doc_Type.AutoName, doc_type.documentalid     from Doc_Type, User_Rights")
            sql.Append(" Where Doc_Type.Doc_Type_Id=User_Rights.Object_ID and User_Rights.User_ID=userID and User_Rights.User_Rights_Type_Id=3 and")
            sql.Append(" User_Rights.Right_value=1 and user_rights.object_type_id=2 ORDER BY")
            sql.Append(" doc_type.Doc_Type_Name;   io_cursor := v_cursor; ")
            sql.Append(" End ")
            sql.Append(" GetDocTypeRights;PROCEDURE GetUserDocumentsResctrictions(UserID IN User_Table.User_ID%TYPE,io_cursor OUT t_cursor)IS        v_cursor t_cursor;")
            sql.Append(" BEGIN ")
            sql.Append("   OPEN v_cursor FOR      select RESTRICTION_ID from ")
            sql.Append(" doc_restriction_r_user where user_id=UserId;      io_cursor := v_cursor; ")
            sql.Append(" END ")
            sql.Append(" GetUserDocumentsResctrictions; PROCEDURE InsertStation(idd IN ")
            sql.Append(" ESTREG.ID%TYPE,io_cursor OUT t_cursor)IS   s_cursor t_cursor; ")
            sql.Append(" BEGIN ")
            sql.Append("  OPEN ")
            sql.Append(" s_cursor FOR   SELECT * FROM ESTREG   WHERE ID=idd and ID>0;   io_cursor := s_cursor; ")
            sql.Append(" COMMIT; ")
            sql.Append(" END InsertStation; PROCEDURE  UpdUserRight(Rightv  IN USER_RIGHTS.Right_Value%TYPE, Rightid IN USER_RIGHTS.Right_Id%TYPE)IS ")
            sql.Append(" BEGIN ")
            sql.Append("      UPDATE USER_RIGHTS SET Right_Value =Rightv  WHERE Right_Id =Rightid;   COMMIT; ")
            sql.Append(" END UpdUserRight; ")
            '---02/03/2007
            sql.Append("PROCEDURE GETNEWRIGHTS(P_userid numeric,P_objid numeric,P_rtype numeric,P_aditional numeric) IS ")
            sql.Append(" cant numeric; ")
            sql.Append("   BEGIN ")
            sql.Append("    Select count(*) into cant from usr_rights Where Aditional=P_aditional and rtype=P_rtype and objid=P_objid and (Groupid=P_userid or Groupid in (Select Groupid from usr_R_group Where USRID=P_userid));")
            sql.Append("    if cant > 1 then ")
            sql.Append("      Select 1 into cant from dual;")
            sql.Append("    else ")
            sql.Append("      Select 0 into cant from dual;")
            sql.Append("    end if;")
            sql.Append("   COMMIT;")
            sql.Append("  END GETNEWRIGHTS;")
            sql.Append(" end zsp_security_100;")
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
