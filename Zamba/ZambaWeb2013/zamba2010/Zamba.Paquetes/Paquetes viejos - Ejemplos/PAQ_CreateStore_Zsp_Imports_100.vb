Imports Zamba.Servers
Imports Zamba.AppBlock

Public Class PAQ_CreateStore_Zsp_Imports_100
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("09-05-2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "1.5.9"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea Procedimientos DeleteHystory, InsertProcHistory, InsertUserAction,GetProcessHistory y UpdProcHistory"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateStore_Zsp_Imports_100"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateStore_Zsp_Imports_100
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
            Return 74
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sql As New System.Text.StringBuilder
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            'SQL
            Try
                sql.Append("CREATE PROCEDURE DeleteHystory @HistoryID NUMERIC AS DELETE FROM P_HST WHERE ID =@HistoryId")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
            sql.Remove(0, sql.Length)

            Try
                sql.Append("CREATE PROCEDURE InsertProcHistory @HID NUMERIC, @PID NUMERIC, @PDATE SMALLDATETIME, @USrid NUMERIC, @totfiles NUMERIC,@procfiles NUMERIC, @skpfiles NUMERIC, @ErrFiles NUMERIC, @RID NUMERIC, @Pth VARCHAR, @hsh VARCHAR, @tfile VARCHAR, @efile VARCHAR, @lfile VARCHAR AS INSERT INTO P_HST(ID,Process_ID,Process_Date,User_Id,TotalFiles,ProcessedFiles,SkipedFiles,ErrorFiles,Result_Id,PATH,HASH,errorfile,tempfile,logfile)VALUES(@HID,@PID,@Pdate,@UsrId,@TotFiles,@ProcFiles,@SkpFiles,@ErrFiles,@RID,@Pth,@Hsh,@efile,@tfile,@lfile)")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
            sql.Remove(0, sql.Length)

            Try
                sql.Append("CREATE PROCEDURE InsertUserAction @AID NUMERIC, @AUSRID NUMERIC, @AOBJID NUMERIC, @AOBJTID NUMERIC, @ATYPE NUMERIC,@ACONID NUMERIC, @SOBJECTID NVARCHAR AS INSERT INTO USER_HST(ACTION_ID, USER_ID,ACTION_DATE,OBJECT_ID,OBJECT_TYPE_ID,ACTION_TYPE,S_Object_Id) VALUES(@AID,@AUSRID,")
                sql.Append(DateTime.Now.ToString)
                sql.Append(",@AOBJID,@AOBJTID,@ATYPE,@SOBJECTID)IF @AUSRID <> 9999 THEN UPDATE UCM SET u_time = SYSDATE WHERE con_id= @ACONID END IF")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
            sql.Remove(0, sql.Length)

            Try
                sql.Append("CREATE PROCEDURE GetProcessHistory @ClsIpJob1ID NUMBER AS SELECT [IP_HST].[HST_ID],[IP_HST].[IP_ID],[IP_HST].[IPDate],[IP_HST].[IPUSERID],[IP_HST].[IPDocCount],[IP_HST].[IPIndexCount],[IP_RESULTS].[RESULT],[IP_HST].[IPRESULT],[IP_HST].[IPLINESCOUNT],[IP_HST].[IPERRORCOUNT],[IP_HST].[IPPATH],[USRTABLE].[Name] FROM IP_HST , USRTABLE, IP_RESULTS WHERE [IP_HST].[IPUserId] = [USRTABLE].[Id] AND [IP_HST].[IP_ID] = @ClsIpJob1ID AND [IP_RESULTS].[RESULT_ID] = [IP_HST].[IPRESULT] ORDER BY [IP_HST].[HST_ID] DESC")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
            sql.Remove(0, sql.Length)
            Try
                sql.Append("CREATE PROCEDURE UpdProcHistory @HID NUMERIC, @totfiles NUMERIC, @procfiles NUMERIC, @skpfiles NUMERIC,@ErrFiles NUMERIC, @RID NUMERIC, @hsh VARCHAR AS UPDATE P_HST SET TotalFiles =@TotFiles, ProcessedFiles =@ProcFiles, SkipedFiles =@SkpFiles, Result_ID =@RID, ERRORFiles =@ErrFiles ,HASH=@Hsh where ID = @HId")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            'ORACLE
            Try
                sql.Append("CREATE OR REPLACE  PACKAGE ZSP_IMPORTS_100 AS TYPE t_cursor IS REF CURSOR; PROCEDURE DeleteHystory(HistoryId IN P_HST.ID%TYPE);	PROCEDURE InsertProcHistory(HID in p_hst.id%type,PID in p_hst.Process_id%type, PDATE in p_hst.Process_Date%type,USrid in p_hst.user_id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type,skpfiles in p_hst.SkipedFiles%type, ErrFiles in p_hst.ErrorFiles%type,RID in p_hst.Result_id%type, Pth in p_hst.Path%type,hsh in p_hst.hash%type,tfile in p_hst.tempfile%type,efile in p_hst.errorfile%type, lfile in p_hst.logfile%type);	PROCEDURE InsertUserAction(AID IN USER_HST.ACTION_ID%TYPE , AUSRID IN USER_HST.USER_ID%TYPE,AOBJID IN USER_HST.USER_ID%TYPE , AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE , ATYPE IN USER_HST.ACTION_TYPE%TYPE,ACONID IN UCM.CON_ID%TYPE, SOBJECTID IN USER_HST.S_OBJECT_ID%TYPE);	PROCEDURE GetProcessHistory(ClsIpJob1ID IN NUMBER, io_cursor OUT t_cursor);	PROCEDURE UpdProcHistory(HID in  p_hst.id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type,skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type, RID in p_hst.Result_id%type,hsh in p_hst.hash%type);END ZSP_IMPORTS_100;")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                sql.Remove(0, sql.Length)
                sql.Append("CREATE OR REPLACE  PACKAGE BODY ZSP_IMPORTS_100  AS PROCEDURE DeleteHystory(HistoryId IN P_HST.ID%TYPE) IS  BEGIN DELETE FROM P_HST WHERE ID=HistoryId; COMMIT; END DeleteHystory; PROCEDURE InsertProcHistory (HID in  p_hst.id%type,PID in p_hst.Process_id%type, PDATE in p_hst.Process_Date%type,USrid in p_hst.user_id%type,totfiles in p_hst.Totalfiles%type, procfiles in p_hst.ProcessedFiles%type,skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type,RID in p_hst.Result_id%type,Pth in p_hst.Path%type,hsh in p_hst.hash%type,tfile in p_hst.tempfile%type,efile in p_hst.errorfile%type, lfile in p_hst.logfile%type) IS BEGIN INSERT INTO P_HST(ID,Process_ID,Process_Date,User_Id,TotalFiles,ProcessedFiles,SkipedFiles,ErrorFiles,Result_Id,PATH,HASH,errorfile,tempfile,logfile) VALUES(HID,PID ,Pdate,UsrId,TotFiles,ProcFiles, SkpFiles,ErrFiles,RID,Pth,Hsh,efile,tfile,lfile); END InsertProcHistory; PROCEDURE InsertUserAction(AID IN USER_HST.ACTION_ID%TYPE , AUSRID IN USER_HST.USER_ID%TYPE, AOBJID IN USER_HST.USER_ID%TYPE ,AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE ,ATYPE IN USER_HST.ACTION_TYPE%TYPE, ACONID IN UCM.CON_ID%TYPE, SOBJECTID IN USER_HST.S_OBJECT_ID%TYPE) IS BEGIN INSERT INTO USER_HST(ACTION_ID, USER_ID,ACTION_DATE, OBJECT_ID,OBJECT_TYPE_ID,ACTION_TYPE,S_Object_Id) VALUES (AID, AUSRID,to_char(sysdate,'dd-MM-yyyy hh:mi:ss(am) '),AOBJID, AOBJTID,ATYPE,SOBJECTID); If AUSRID <> 9999 Then UPDATE UCM SET u_time = SYSDATE WHERE con_id= ACONID;")
                sql.Append("END IF; COMMIT;	END InsertUserAction; PROCEDURE GetProcessHistory(ClsIpJob1ID IN NUMBER, io_cursor OUT t_cursor) IS v_cursor t_cursor ;	BEGIN OPEN v_cursor FOR SELECT IP_HST.HST_ID,IP_HST.IP_ID,IP_HST.IPDate, IP_HST.IPUSERID,IP_HST.IPDocCount, IP_HST.IPIndexCount, IP_RESULTS.RESULT , IP_HST.IPRESULT, IP_HST.IPLINESCOUNT, IP_HST.IPERRORCOUNT,IP_HST.IPPATH,USRTABLE.Name FROM IP_HST , USRTABLE, IP_RESULTS WHERE(IP_HST.IPUserId = USRTABLE.Id And IP_HST.IP_ID = ClsIpJob1ID) AND IP_RESULTS.RESULT_ID = IP_HST.IPRESULT ORDER BY IP_HST.HST_ID DESC; io_cursor := v_cursor; END GetProcessHistory;	PROCEDURE UpdProcHistory(HID in p_hst.id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type, skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type, RID in p_hst.Result_id%type,hsh in p_hst.hash%type) IS 	BEGIN 	UPDATE P_HST SET TotalFiles = TotFiles, ProcessedFiles = ProcFiles, SkipedFiles = SkpFiles, Result_ID = RID, ERRORFiles = ErrFiles, HASH = Hsh WHERE ID = HId;	END UpdProcHistory; END zsp_imports_100; ")

                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
        Return True
    End Function
#End Region

End Class
