Imports Zamba.Servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32

Public Class PAQ_CreateStores_General
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub


#Region "Propiedades"

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateStores_General"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStores_General
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea todos los procedimientos almacenados"
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("20/04/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("14/07/2006 17:35")
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
            Return 85
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim strcreate As String
        Dim bResultado As Boolean = True

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

            Try
                ZPaq.IfExists("zsp_users_100_GetDocumentAction", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_users_100_GetDocumentAction') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_users_100_GetDocumentAction"
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_users_100_GetDocumentAction @DocumentId int AS SELECT UsrTable.[Name] as Usuario, User_Hst.Action_Date as Fecha, RightsType.RightsType as [Accion], ObjectTypes.ObjectTypes as " & Chr(34) & "Tipo de Objeto" & Chr(34) & ",  User_Hst.[s_Object_Id] as [Objeto] FROM User_Hst INNER JOIN UsrTable ON User_Hst.[User_Id] = UsrTable.[Id] INNER JOIN ObjectTypes ON User_Hst.Object_Type_Id = ObjectTypes.ObjectTypesId INNER JOIN RightsType ON User_Hst.Action_Type = RightsType.RightsTypeId where ([Object_Id] = @DocumentId) AND (Object_Type_Id = 6)"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try
            Try
                ZPaq.IfExists("zsp_imports_100_GetProcessHistory", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
            Catch
            End Try
            Try
                ZPaq.IfExists("zsp_users_100_GetUserAction", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_users_100_GetUserAction') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_users_100_GetUserAction"
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_users_100_GetUserAction @UserId numeric (9) AS SELECT DISTINCT  User_Hst.Action_Date as Fecha, ObjectTypes.ObjectTypes as Herramienta, RightsType.RightsType as Accion, User_HST.[s_Object_Id] as En FROM User_HST INNER JOIN UsrTable ON User_HST.[User_Id] = UsrTable.[Id] INNER JOIN ObjectTypes ON User_HST.Object_Type_Id = ObjectTypes.ObjectTypesId INNER JOIN RightsType ON User_HST.Action_Type = RightsType.RightsTypeId WHERE(User_HST.[User_Id] = @UserId) order By User_Hst.Action_Date desc "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try



            Try
                ZPaq.IfExists("zsp_doctypes_100_LoadDocTypes", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_doctypes_100_LoadDocTypes') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_doctypes_100_LoadDocTypes"
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_doctypes_100_LoadDocTypes @DocGroupId int AS SELECT Doc_Type.Doc_Type_Id, Doc_Type.Doc_Type_Name, Doc_Type.Object_Type_Id, Doc_Type_R_Doc_Type_Group.Doc_Order,Doc_Type_R_Doc_Type_Group.Doc_Type_Group FROM Doc_Type INNER JOIN Doc_Type_R_Doc_Type_Group ON Doc_Type.Doc_Type_Id = Doc_Type_R_Doc_Type_Group.Doc_Type_Id WHERE(Doc_Type_R_Doc_Type_Group.Doc_Type_Group = @DocGroupId) ORDER BY Doc_Type_R_Doc_Type_Group.Doc_Order"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_doctypes_100_CopyDocType", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_doctypes_100_CopyDocType') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_doctypes_100_CopyDocType "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE Proc zsp_doctypes_100_CopyDocType @Doc_Type_ID numeric, @New_Doc_Type_Id numeric, @New_Name char(30) As Declare @FILE_FORMAT_ID numeric,@DISK_GROUP_ID numeric,@THUMBNAILS numeric,@ICON_ID numeric,@CROSS_REFERENCE numeric,@LIFE_CYCLE numeric,@OBJECT_TYPE_ID numeric,@AUTONAME char(255) Begin Transaction Select @FILE_FORMAT_ID=FILE_FORMAT_ID,@DISK_GROUP_ID=Disk_Group_ID,@THUMBNAILS=THUMBNAILS,@ICON_ID=ICON_ID,@CROSS_REFERENCE=CROSS_REFERENCE,@LIFE_CYCLE=LIFE_CYCLE,@OBJECT_TYPE_ID=OBJECT_TYPE_ID,@AUTONAME=AUTONAME From DOC_TYPE Where DOC_TYPE_ID=@DOC_TYPE_ID Insert into DOC_TYPE(DOC_TYPE_ID,DOC_TYPE_NAME,FILE_FORMAT_ID, DISK_GROUP_ID,THUMBNAILS,ICON_ID,CROSS_REFERENCE,LIFE_CYCLE,OBJECT_TYPE_ID,AUTONAME, Documentalid) Values (@New_Doc_Type_id,@New_Name,@FILE_FORMAT_ID,@DISK_GROUP_ID,@THUMBNAILS,@ICON_ID,@CROSS_REFERENCE,@LIFE_CYCLE,@OBJECT_TYPE_ID,@AUTONAME,0) Commit Tran Delete from TablaTemp Insert into TablaTemp(INDEX_ID,DOC_TYPE_ID,ORDEN,MUSTCOMPLETE,LoadLotus,ShowLotus) Select INDEX_ID,DOC_TYPE_ID,ORDEN,MUSTCOMPLETE,LoadLotus,ShowLotus from INDEX_R_DOC_TYPE Where DOC_TYPE_ID=@Doc_Type_ID Update TablaTemp Set Doc_Type_ID=@New_Doc_Type_Id Insert into Index_R_Doc_Type(INDEX_ID,DOC_TYPE_ID,ORDEN,MUSTCOMPLETE,LoadLotus,ShowLotus) Select INDEX_ID,DOC_TYPE_ID,ORDEN,MUSTCOMPLETE,LoadLotus,ShowLotus from TablaTemp where Doc_Type_ID=@New_Doc_Type_ID "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_messages_100_CountNewMessages", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_messages_100_CountNewMessages') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_messages_100_CountNewMessages"
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create procedure zsp_messages_100_CountNewMessages @UserId integer as SELECT count(*) FROM MSG_DEST WHERE MSG_DEST.user_id=@userid AND MSG_DEST.deleted=0 and [read]=0"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_connection_100_DeleteConnection", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_connection_100_DeleteConnection') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_connection_100_DeleteConnection "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "Create Proc zsp_connection_100_DeleteConnection @conid int as Delete from UCM where Con_ID=@conid "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try
            Try
                ZPaq.IfExists("zsp_messages_100_DeleteRecivedMsg", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_messages_100_DeleteRecivedMsg') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_messages_100_DeleteRecivedMsg "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_messages_100_DeleteRecivedMsg @m_id int, @u_id int as UPDATE MSG_DEST SET  deleted=1 WHERE msg_id=@m_id AND user_id=@u_id "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_messages_100_DeleteSenderMsg", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_messages_100_DeleteSenderMsg') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_messages_100_DeleteSenderMsg "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE procedure zsp_messages_100_DeleteSenderMsg @m_id int AS Declare @recived numeric select @recived=count(*) from msg_dest where msg_id=@m_id and deleted=0 if @recived=0 Begin delete from msg_dest where msg_id=@m_id delete from msg_attach where msg_id=@m_id delete from message where msg_id=@m_id End else update message set deleted=1 where msg_id=@m_id "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try
            Try
                ZPaq.IfExists("zsp_exception_100_DeleteExceptionTable", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_exception_100_DeleteExceptionTable') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_exception_100_DeleteExceptionTable "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_exception_100_DeleteExceptionTable AS Delete from Excep Where Fecha>(GetDate()-30) "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try
            Try
                ZPaq.IfExists("zsp_index_100_FillIndex", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_FillIndex') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_FillIndex "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_index_100_FillIndex @IPJOBDocTypeId numeric (9) AS SELECT DOC_INDEX.INDEX_ID,DOC_INDEX.INDEX_NAME, DOC_INDEX.INDEX_TYPE, DOC_INDEX.INDEX_LEN, DOC_INDEX.AUTOFILL, DOC_INDEX.NOINDEX, DOC_INDEX.DROPDOWN, DOC_INDEX.AUTODISPLAY, DOC_INDEX.INVISIBLE, DOC_INDEX.OBJECT_TYPE_ID FROM INDEX_R_DOC_TYPE INNER JOIN DOC_INDEX ON INDEX_R_DOC_TYPE.INDEX_ID = DOC_INDEX.INDEX_ID WHERE INDEX_R_DOC_TYPE.DOC_TYPE_ID = @IPJOBDOCTYPEID "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_doctypes_100_FillMeTreeView", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_doctypes_100_FillMeTreeView') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_doctypes_100_FillMeTreeView "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_doctypes_100_FillMeTreeView @UserId numeric AS SELECT DOC_TYPE_GROUP.DOC_TYPE_GROUP_ID, DOC_TYPE_GROUP.DOC_TYPE_GROUP_NAME, DOC_TYPE_GROUP.ICON, DOC_TYPE_GROUP.PARENT_ID,DOC_TYPE_GROUP.OBJECT_TYPE_ID, USR_RIGHTS.GROUPID, USR_RIGHTS.RTYPE FROM DOC_TYPE_GROUP INNER JOIN USR_RIGHTS ON DOC_TYPE_GROUP.DOC_TYPE_GROUP_ID = USR_RIGHTS.ADITIONAL AND DOC_TYPE_GROUP.OBJECT_TYPE_ID = USR_RIGHTS.OBJID WHERE (USR_RIGHTS.GROUPID = @UserID) AND (USR_RIGHTS.RTYPE = 1) ORDER BY DOC_TYPE_GROUP.DOC_TYPE_GROUP_ID"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_docindex_100_LoadIndex", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_docindex_100_LoadIndex') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_docindex_100_LoadIndex "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_docindex_100_LoadIndex @DocTypeId int AS SELECT Doc_Index.Index_Id, Doc_Index.Index_Name, Doc_Index.Index_Type, Doc_Index.Index_Len, Doc_Index.Object_Type_Id, Index_R_Doc_Type.Orden, Index_R_Doc_Type.Doc_Type_Id FROM Doc_Index INNER JOIN Index_R_Doc_Type ON Doc_Index.Index_Id = Index_R_Doc_Type.Index_Id WHERE(Index_R_Doc_Type.Doc_Type_Id = @DocTypeId) ORDER BY Index_R_Doc_Type.Orden "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_imports_100_GetProcessHistory", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_imports_100_GetProcessHistory') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_imports_100_GetProcessHistory "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_imports_100_GetProcessHistory @ProcessID INT AS SELECT P_HST.ID, P_HST.Process_Date, P_HST.User_Id, P_HST.TotalFiles, P_HST.ProcessedFiles, P_HST.Result_Id, P_HST.SkipedFiles, P_HST.ErrorFiles, P_HST.Path, USRTABLE.NAME, P_HST.Process_id, ip_results.Result, P_HST.Hash, P_HST.logfile, P_HST.errorfile, P_HST.tempfile FROM P_HST, USRTABLE, IP_RESULTS WHERE P_HST.User_Id = USRTABLE.ID AND P_HST.process_ID = @ProcessID AND IP_RESULTS.RESULT_ID = P_HST.RESULT_ID ORDER BY P_HST.ID DESC "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_index_100_IndexGeneration", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_IndexGeneration ') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_IndexGeneration "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_index_100_IndexGeneration @DocTypeId  int AS SELECT DOC_INDEX.Index_Id, DOC_INDEX.Index_Name, DOC_INDEX.INDEX_TYPE,DOC_INDEX.Index_Len, DOC_INDEX.AutoFill, DOC_INDEX.[NoIndex],DOC_INDEX.DropDown, DOC_INDEX.AutoDisplay,DOC_INDEX.Invisible, DOC_INDEX.Object_Type_Id,INDEX_R_DOC_TYPE.Doc_Type_Id,INDEX_R_DOC_TYPE.Orden FROM DOC_INDEX inner join INDEX_R_DOC_TYPE ON DOC_INDEX.Index_Id = INDEX_R_DOC_TYPE.Index_Id WHERE Doc_Type_Id = @DocTypeId ORDER BY ORDEN "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_license_100_GetActiveWFConect", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_license_100_GetActiveWFConect') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_license_100_GetActiveWFConect "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "Create Proc zsp_license_100_GetActiveWFConect AS Select Used from LIC where Type=1 "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_objects_100_GetAndSetLastId", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_objects_100_GetAndSetLastId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_objects_100_GetAndSetLastId "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

                strcreate = "CREATE PROCEDURE zsp_objects_100_GetAndSetLastId @OBJTYPE int AS Declare @OBJID numeric Select @objid=count(*) from Objlastid WHERE OBJECT_TYPE_ID =@OBJTYPE If @objid=0   Begin  Insert Into Objlastid(Object_type_ID,ObjectID) values(@objtype,0) End UPDATE OBJLASTID SET OBJECTID = OBJECTID + 1  WHERE OBJECT_TYPE_ID =@OBJTYPE SELECT OBJECTID FROM OBJLASTID WHERE OBJECT_TYPE_ID = @OBJTYPE "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_security_100_GetArchivosUserRight", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_security_100_GetArchivosUserRight') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_security_100_GetArchivosUserRight "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                'strcreate = "Create Proc zsp_security_100_GetArchivosUserRight @UserId int As SELECT distinct(dtg.Doc_Type_Group_ID),dtg.Doc_Type_Group_Name,dtg.Icon,dtg.Parent_Id,dtg.Object_Type_Id,urv.User_Id,urv.Right_Type FROM  DOC_TYPE_GROUP dtg, USR_RIGHTS_VIEW urv WHERE dtg.Doc_Type_Group_ID = urv.Aditional AND dtg.Object_Type_Id = urv.ObjectID and urv.User_Id =@Userid ORDER BY dtg.Doc_Type_Group_ID "
                strcreate = "Create Proc zsp_security_100_GetArchivosUserRight @UserId int As SELECT distinct(dtg.Doc_Type_Group_ID),dtg.Doc_Type_Group_Name,dtg.Icon,dtg.Parent_Id,dtg.Object_Type_Id,urv.User_Id,urv.Right_Type FROM  DOC_TYPE_GROUP dtg, Zvw_USR_Rights_100 urv WHERE dtg.Doc_Type_Group_ID = urv.Aditional AND dtg.Object_Type_Id = urv.ObjectID and urv.User_Id =@Userid ORDER BY dtg.Doc_Type_Group_ID "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_messages_100_GetMyMessages", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_messages_100_GetMyMessages') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_messages_100_GetMyMessages "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE procedure zsp_messages_100_GetMyMessages @my_id INT AS Select message.msg_id,message.msg_from,usrtable.description as User_Name,message.subject,message.msg_date,message.reenvio,message.deleted,msg_dest.User_id as DEST,msg_dest.dest_type,[msg_dest].[read],message.msg_body,msg_dest.User_NAME as dest_name from message,msg_dest,usrtable      where message.msg_id = msg_dest.msg_id and usrtable.id= message.msg_from and message.msg_id in (select msg.msg_id from message msg,msg_dest where  msg.msg_id = msg_dest.msg_id and msg_dest.User_id=@my_id and msg_dest.deleted=0)"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_messages_100_GetMyAttachments", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_messages_100_GetMyAttachments') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_messages_100_GetMyAttachments "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE procedure zsp_messages_100_GetMyAttachments @my_id INT AS Select message.msg_id,msg_attach.doc_id,msg_attach.doc_type_id,msg_attach.folder_id,msg_attach.index_id ,msg_attach.name,msg_attach.icon,volumelistid,doc_file,offset,disk_vol_path from message,msg_attach     WHERE MESSAGE.msg_id = MSG_ATTACH.msg_id AND  MESSAGE.msg_id IN (SELECT msg.msg_id FROM MESSAGE msg,MSG_DEST WHERE  msg.msg_id = MSG_DEST.msg_id AND MSG_DEST.user_id=@my_id AND MSG_DEST.deleted=0) Order by message.msg_ID"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_doctypes_100_IncrementsDocType", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_doctypes_100_IncrementsDocType') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_doctypes_100_IncrementsDocType "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_doctypes_100_IncrementsDocType @DocID numeric,@X numeric AS Update Doc_Type Set DocCount=DocCount + @X where Doc_Type_Id= @DocID "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_index_100_GetIndexRDocType", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_GetIndexRDocType') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_GetIndexRDocType "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE procedure zsp_index_100_GetIndexRDocType @DocID Int AS Select Index_Id, Doc_Type_ID From Index_R_Doc_Type where Doc_Type_ID=@DocId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_lock_100_LockDocument", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_lock_100_LockDocument') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_lock_100_LockDocument "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "Create Procedure zsp_lock_100_LockDocument @Doc_Id decimal,@User_Id int,@Est_Id int As Insert into LCK(Doc_ID,[User_Id],LCK_Date,Est_Id) values(@Doc_ID,@User_Id,GetDate(),@Est_Id) "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_connection_100_InsertNewConecction", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_connection_100_InsertNewConecction') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_connection_100_InsertNewConecction "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE  zsp_connection_100_InsertNewConecction @v_userId int,@v_win_User nvarchar(50),@v_win_Pc nvarchar(50),@v_con_Id int,@Time_out int,@Type int AS INSERT INTO UCM(USER_ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,Time_out,Type) VALUES (@v_UserId,GetDate(),GetDate(),@v_Win_User,@v_Win_PC,@v_con_Id,@Time_out,@Type) "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_barcode_100_InsertBarCode", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_barcode_100_InsertBarCode') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_barcode_100_InsertBarCode "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE Procedure zsp_barcode_100_InsertBarCode @id int,@DocTypeId int, @UserId int, @DocId bigint As Insert into ZBarCode([Id],Fecha,Doc_Type_ID,UserId,Scanned,Doc_Id) Values(@id,GetDate(),@DocTypeId,@Userid,'No',@DocId) "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_messages_100_InsertMsg", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_messages_100_InsertMsg') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_messages_100_InsertMsg "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_messages_100_InsertMsg @m_id int,@m_from int,@m_Body nvarchar(4000),@m_subject nvarchar(100),@m_resend int As INSERT INTO message(msg_id,msg_from,msg_body,subject,msg_date,reenvio,deleted) VALUES (@m_id, @m_from,@m_body,@m_subject,Getdate(),@m_resend,0)"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_messages_100_InsertAttach", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_messages_100_InsertAttach') and OBJECTPROPERTY(id, N'IsProcedure') = 1)  drop procedure zsp_messages_100_InsertAttach "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE procedure zsp_messages_100_InsertAttach @m_id INT,@m_DOCid int,@m_DOC_TYPE_ID int,@fold_id int,@inde_id int,@m_name nVarchar(100),@m_icon int,@m_Disk_Group int,@m_doc_file nvarchar(50),@m_offset int,@m_disk_Vol_Path nvarchar(250) As INSERT INTO MSG_ATTACH(MSG_ID,DOC_ID,DOC_TYPE_ID,FOLDER_ID,INDEX_ID,[NAME],ICON,volumelistid,doc_file,offset,disk_vol_path) VALUES (@m_id,@m_DOCid,@m_DOC_TYPE_ID,@FOLD_ID,@inde_id,@m_name,@m_icon,@m_Disk_Group,@m_doc_file,@m_offset,@m_disk_vol_path) "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_imports_100_InsertProcHistory", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_imports_100_InsertProcHistory') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_imports_100_InsertProcHistory "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create  proc zsp_imports_100_InsertProcHistory @HID numeric,@PID numeric,@PDATE smalldatetime,@USrid numeric,@totfiles numeric,@procfiles numeric,@skpfiles numeric,@ErrFiles numeric,@RID numeric,@Pth varchar(250),@hsh varchar(50),@tfile varchar(250),@efile varchar(250),@lfile varchar(250) As INSERT INTO P_HST(ID,Process_ID,Process_Date,User_Id,TotalFiles,ProcessedFiles,SkipedFiles,ErrorFiles,Result_Id,PATH,HASH,errorfile,tempfile,logfile) VALUES (@HID,@PID,@Pdate,@UsrId ,@TotFiles ,@ProcFiles,@SkpFiles,@ErrFiles,@RID     ,@Pth,@Hsh,@efile,@tfile,@lfile) "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_imports_100_InsertUserAction", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_imports_100_InsertUserAction') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_imports_100_InsertUserAction"
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE  zsp_imports_100_InsertUserAction @AID int,@AUSRID int,@AOBJID int,@AOBJTID int,@ATYPE int, @ACONID int, @S_OBJECT_ID nvarchar(255) As BEGIN Transaction INSERT INTO USER_HST(ACTION_ID,[USER_ID],ACTION_DATE,[OBJECT_ID],OBJECT_TYPE_ID,ACTION_TYPE,S_OBJECT_ID) VALUES (@AID,@AUSRID,getdate(),@AOBJID,@AOBJTID,@ATYPE,@S_OBJECT_ID) IF @AUSRID <> 9999 UPDATE UCM SET U_TIME = GetDATE() WHERE con_id= @ACONID       COMMIT TRANSACTION "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_security_100_InsertStation", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_security_100_InsertStation') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_security_100_InsertStation"
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE Proc zsp_security_100_InsertStation @Pc nvarchar(50),@WinUser nvarchar(50),@ver  nvarchar(5) As Declare @stationid numeric IF (SELECT count(*) FROM ESTREG  WHERE M_Name=@PC)=0 Begin   select @stationid=objectid  from objlastid where object_type_id=35   Insert into Estreg(ID,M_Name,W_User,Ver,updated,last_check) values(@stationid,@PC,@Winuser,@Ver,GetDate(),GetDate())   SELECT * FROM ESTREG  WHERE M_Name=@PC End Else  SELECT * FROM ESTREG  WHERE M_Name=@PC "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_license_100_GetDocumentalLicenses", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_license_100_GetDocumentalLicenses') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_license_100_GetDocumentalLicenses "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE Proc zsp_license_100_GetDocumentalLicenses as Select [Numero_Licencias] from LIC Where Type=0 "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_volume_100_UpdFilesAndSize", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_volume_100_UpdFilesAndSize') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_volume_100_UpdFilesAndSize "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_volume_100_UpdFilesAndSize @VolumeId NUMERIC, @FileSize DECIMAL  AS Declare @totalfiles numeric Declare @totalsize DECIMAL Begin Transaction SET @totalfiles =(SELECT DISK_VOL_FILES FROM DISK_VOLUME WHERE DISK_VOL_ID = @VolumeId) SET @totalsize=(SELECT DISK_VOL_SIZE_LEN  FROM DISK_VOLUME WHERE DISK_VOL_ID=@VolumeId) UPDATE DISK_VOLUME SET DISK_VOL_FILES =@totalfiles + 1, DISK_VOL_SIZE_LEN =@totalsize + @FileSize WHERE DISK_VOL_ID = @VolumeId Commit Transaction "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_imports_100_UpdProcHistory", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_imports_100_UpdProcHistory') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_imports_100_UpdProcHistory "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_imports_100_UpdProcHistory @HID numeric, @totfiles numeric,@procfiles numeric,@skpfiles numeric,@ErrFiles numeric,@RID numeric,@hsh varchar(50) as UPDATE P_HST SET TotalFiles = @TotFiles,ProcessedFiles = @ProcFiles,SkipedFiles = @SkpFiles,Result_ID = @RID,ERRORFiles = @ErrFiles ,HASH= @Hsh where ID = @HId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_volume_100_UpdDeletedFiles", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_volume_100_UpdDeletedFiles') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_volume_100_UpdDeletedFiles "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_volume_100_UpdDeletedFiles (@VolumeId NUMERIC, @FileSize DECIMAL) AS Declare @totalfiles NUMERIC, @totalsize DECIMAL Begin Transaction SELECT @totalfiles=DISK_VOL_FILES FROM DISK_VOLUME WHERE DISK_VOL_ID = @VolumeId SELECT @totalsize=DISK_VOL_SIZE_LEN  FROM DISK_VOLUME WHERE DISK_VOL_ID =@VolumeId UPDATE DISK_VOLUME SET  DISK_VOL_FILES =@totalfiles - 1, DISK_VOL_SIZE_LEN =@totalsize - @FileSize WHERE DISK_VOL_ID = @VolumeId Commit Transaction"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try



            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_DeleteWorkFlowByWfId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_DeleteWorkFlowByWfId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_DeleteWorkFlowByWfId @pWork_ID numeric as DELETE wfworkflow where work_id = @pWork_ID "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_DeleteDocumentByTask') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_DeleteDocumentByTask "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_DeleteDocumentByTask @pTaskId numeric as DELETE WFDOCUMENT WHERE Task_ID = @pTaskId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_DeleteStepById') and OBJECTPROPERTY(id, N'IsProcedure') = 1)  drop procedure zsp_workflow_100_DeleteStepById "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_DeleteStepById @pStepId numeric as DELETE wfSTEP where STEP_id = @pStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_DeleteStepStateById') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_DeleteStepStateById "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_DeleteStepStateById @pStateID numeric AS DELETE WFStepStates where doc_state_Id = @pStateID "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            Try
                ZPaq.IfExists("zsp_volume_100_GetDocGroupRDocVolByDgId", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_volume_100_GetDocGroupRDocVolByDgId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_volume_100_GetDocGroupRDocVolByDgId "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_volume_100_GetDocGroupRDocVolByDgId @volgroup numeric as Select disk_volume_id from disk_group_r_disk_volume where disk_group_id=@volgroup "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_index_100_GetIndexQtyByNameId", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_GetIndexQtyByNameId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_GetIndexQtyByNameId "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_index_100_GetIndexQtyByNameId @IndexName char(30), @IndexId numeric As Select count(*) from Doc_index where Index_name=@IndexName and Index_id <>@IndexId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_docassociated_100_GetDocAssociatedById", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_docassociated_100_GetDocAssociatedById') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_docassociated_100_GetDocAssociatedById "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                'strcreate = "create proc zsp_docassociated_100_GetDocAssociatedById @DoctypeId int AS Select count(*) from Doctypes_associated where DoctypeId1=@DocTypeId or doctypeid2=@DocTypeId "
                strcreate = "create proc zsp_docassociated_100_GetDocAssociatedById @DoctypeId int AS Select count(*) from doc_type_r_doc_type where DoctypeId1=@DocTypeId or doctypeid2=@DocTypeId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try



            Try
                ZPaq.IfExists("zsp_docassociated_100_getDocAssociatedId2ById1", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_docassociated_100_getDocAssociatedId2ById1') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_docassociated_100_getDocAssociatedId2ById1 "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                'strcreate = "create proc zsp_docassociated_100_getDocAssociatedId2ById1 @DocTypeId int as Select DoctypeId2 from doctypes_associated where doctypeid1= @DocTypeId "
                strcreate = "create proc zsp_docassociated_100_getDocAssociatedId2ById1 @DocTypeId int as Select DoctypeId2 from DOC_TYPE_r_DOC_TYPE where doctypeid1= @DocTypeId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_doctypes_100_UpdDocCountById", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_doctypes_100_UpdDocCountById') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_doctypes_100_UpdDocCountById"
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_doctypes_100_UpdDocCountById @DocCount numeric, @DocTypeId numeric As Update doc_type set Doccount=@DocCount where doc_type_id=@DocTypeId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_doctypes_100_UpdMbById", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_doctypes_100_UpdMbById') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_doctypes_100_UpdMbById "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "Create proc zsp_doctypes_100_UpdMbById @TamArch decimal, @DocTypeId numeric As Update doc_type set MB=(MB + @TamArch) where Doc_type_Id= @DocTypeId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_generic_100_ExecSqlString", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_generic_100_ExecSqlString') and OBJECTPROPERTY(id, N'IsProcedure') = 1)  drop procedure zsp_generic_100_ExecSqlString "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_generic_100_ExecSqlString @Sql varchar(255) as exec(@Sql) "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False

            End Try


            Try
                ZPaq.IfExists("zsp_index_100_GetAllIndexRDocType", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_GetAllIndexRDocType') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_GetAllIndexRDocType "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_index_100_GetAllIndexRDocType as select t.index_id, t.doc_type_id from INDEX_R_DOC_TYPE t order by doc_type_id, index_id "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_GetAllWf') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_GetAllWf "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_GetAllWf as SELECT * FROM WFworkflow "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            Try
                ZPaq.IfExists("zsp_index_100_GetDoc_dColumns", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_GetDoc_dColumns') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_GetDoc_dColumns "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_index_100_GetDoc_dColumns as select replace(c.name,'D','') 'Columna', replace(t.name,'DOC_D','') 'DOC_D' from syscolumns c inner join sysobjects t on c.id=t.id where t.type='u' and t.name like 'doc_d%' order by t.name ,c.name "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_index_100_GetDoc_iColumns", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_GetDoc_iColumns') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_GetDoc_iColumns"
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_index_100_GetDoc_iColumns as select replace(c.name,'I','') 'Columna', replace(t.name,'DOC_I','') 'DOC_I' from syscolumns c inner join sysobjects t on c.id=t.id where t.type='u' and t.name like 'doc_i%' order by t.name ,c.name "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try



            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_GetDocCountByStepId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_GetDocCountByStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_GetDocCountByStepId @pStepId numeric AS SELECT DCOUNT FROM ZVIEWWFDOCUMENTCOUNT WHERE STEP_ID = @pStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_GetStepsOfUsrGroupByAdt') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_GetStepsOfUsrGroupByAdt "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_GetStepsOfUsrGroupByAdt @pAdt numeric as Select * from zstepuserGroups where aditional= @pAdt "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            Try
                ZPaq.IfExists("zsp_zschedule_100_GetNewTasks", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_zschedule_100_GetNewTasks') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_zschedule_100_GetNewTasks "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_zschedule_100_GetNewTasks @Id as int as select * from schedule where task_id > @Id "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try



            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_GetViewStepsByWfId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_GetViewStepsByWfId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_GetViewStepsByWfId @pWFId numeric as Select * from ZViewWFSTEPS where WORK_ID = @pWFId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_GetViewStepsByStep') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_GetViewStepsByStep "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_GetViewStepsByStep @pStepId numeric as Select * from ZViewWFSTEPS where step_id = @pStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_GetStepIdByDocId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_GetStepIdByDocId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_GetStepIdByDocId @pDocId numeric  as select step_Id from wfdocument where doc_id= @pDocId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_GetStatesByStepId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_GetStatesByStepId"
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_GetStatesByStepId @pStepId numeric as Select * from WFStepStates where step_id = @pStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_GetStatesByStepOrState') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_GetStatesByStepOrState "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_GetStatesByStepOrState @pStateId numeric, @pStepId numeric as Select * from WFStepStates where doc_state_id=@pStateId  or step_id = @pStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_GetStepsByWork') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_GetStepsByWork "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_GetStepsByWork @pWorkId numeric as Select * from wfstep where work_id= @pWorkId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            Try
                ZPaq.IfExists("zsp_index_100_InsertLinkInfo", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_InsertLinkInfo') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_InsertLinkInfo"
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create  proc zsp_index_100_InsertLinkInfo @Id numeric,@Data varchar(100), @Flag numeric,@DocType numeric,@DocIndex numeric, @Name varchar(50) as insert into index_link_info(id,data,flag,doctype,docindex,name ) values(@Id,@Data,@Flag,@DocType,@DocIndex, @Name) "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_index_100_UpdIndexRDoctypeByDtInd", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_UpdIndexRDoctypeByDtInd') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_UpdIndexRDoctypeByDtInd "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_index_100_UpdIndexRDoctypeByDtInd @DocTypeId int, @IndexId int As Update Index_R_DocType set Mustcomplete=1, ShowLotus=1, LoadLotus=1 where Doc_Type_ID=@DocTypeId and Index_Id=@IndexId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_index_100_UpdIndexRDoctypeByDtInd2", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_UpdIndexRDoctypeByDtInd2') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_UpdIndexRDoctypeByDtInd2 "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_index_100_UpdIndexRDoctypeByDtInd2 @DocTypeId int, @IndexId int As Update Index_R_DocType set Mustcomplete=1, ShowLotus=1 where Doc_Type_ID=@DocTypeId and Index_Id=@IndexId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try



            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_InsertWorkFlow') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_InsertWorkFlow "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_InsertWorkFlow @pWork_ID decimal, @pWStat_Id decimal ,@pName varchar(50),@pHelp varchar(200), @pDescription varchar(100),@pDate datetime, @pEditDate datetime, @pRefreshRate numeric, @pInitialStepId numeric AS Insert into wfworkflow (work_id,Wstat_id,name,help,description,createdate,editdate,refreshrate,initialstepid) VALUES (@pWork_ID ,@pWStat_Id ,@pName ,@pHelp ,@pDescription ,@pDate,@pEditDate,@pRefreshRate ,@pInitialStepId )"
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_InsertRuleHistory') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_InsertRuleHistory "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_InsertRuleHistory @pFolderId numeric , @pStepId numeric ,@pDoc_Id numeric ,@pDocTypeid numeric ,@pRuleId numeric ,@pResult numeric ,@pUsrId numeric ,@pEjecDate datetime ,@pData nvarchar(400) as INSERT INTO WFRULESHST (FOLDER_ID  ,STEP_ID   ,DOC_ID  ,DOC_TYPE_ID,Rule_Id ,Result  ,User_Id,Ejecution_Date,Data) VALUES (@pFolderId , @pStepId ,@pDoc_Id,@pDocTypeid,@pRuleId,@pResult,@pUsrId,@pEjecDate,@pData) "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_InsertStep') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_InsertStep "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_InsertStep @pWFId numeric ,@pStepId numeric , @pName varchar(50),@pDesc varchar(100),@pHelp varchar(100), @pCDate datetime,@pEDate datetime , @pImgInd numeric ,@pLocX decimal ,@pLocY decimal ,@pMaxDocs numeric ,@pMaxHours numeric ,@pStartAt numeric as INSERT INTO WFSTEP (work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc) VALUES (@pWFId ,@pStepId,@pName ,@pDesc,@pHelp,@pCDate,@pEDate,@pImgInd ,@pLocX ,@pLocY ,@pMaxDocs ,@pMaxHours ,@pStartAt) "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_InsertStepHistory') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_InsertStepHistory "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_InsertStepHistory @pDocId numeric , @pDocTypeid numeric , @pFolderId numeric ,@pStepId numeric ,@pCiDocStateId numeric ,@pcheckin datetime ,@pChkOut datetime ,@pUChkIn numeric ,@pUChkOut numeric ,@pCoDocStateId numeric AS INSERT INTO WFSTEPHST (DOC_ID ,DOC_TYPE_ID ,FOLDER_ID,STEP_ID ,ci_Doc_State_Id   ,checkin,checkout ,ucheckin ,ucheckout ,co_doc_state_id) VALUES         (@pDocId , @pDocTypeid , @pFolderId ,@pStepId   ,@pCiDocStateId    ,@pcheckin ,@pChkOut ,@pUChkIn  ,@pUChkOut ,@pCoDocStateId  ) "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            Try
                ZPaq.IfExists("zsp_schedule_100_GetTasks", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_schedule_100_GetTasks') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_schedule_100_GetTasks "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_schedule_100_GetTasks as select * from schedule"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_schedule_100_DeleteTask", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_schedule_100_DeleteTask') and OBJECTPROPERTY(id, N'IsProcedure') = 1)drop procedure zsp_schedule_100_DeleteTask "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_schedule_100_DeleteTask @Id int As DELETE Schedule WHERE TASK_ID=@Id "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_schedule_100_UpdLastTaskExecution", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_schedule_100_UpdLastTaskExecution') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_schedule_100_UpdLastTaskExecution "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_schedule_100_UpdLastTaskExecution @Id int, @Fecha datetime ,@Ultima datetime As UPDATE Schedule SET FECHA=@Fecha, ULTIMA=@Ultima WHERE TASK_ID=@Id "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try



            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdState') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdState "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdState @pStepId numeric, @pName varchar(50),@pDesc nvarchar(250), @pInitial numeric,@pStateID numeric as UPDATE WFStepStates SET STEP_ID = @pStepId  , Name = @pName , Description = @pDesc , Initial = @pInitial where Doc_State_Id = @pStateID "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdWorkFlowByWfId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdWorkFlowByWfId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdWorkFlowByWfId @pWStat_Id decimal ,@pName varchar(50) ,@pHelp varchar(200),@pDescription varchar(100) ,@pEditDate datetime ,@pRefreshRate numeric ,@pStepId numeric , @pWork_ID numeric as UPDATE wfworkflow SET wstat_id = @pWStat_Id ,name = @pName ,help = @pHelp ,description = @pDescription ,editdate = @pEditDate ,refreshrate = @pRefreshRate ,InitialStepId = @pStepId  where work_id = @pWork_ID "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdStateByDocId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdStateByDocId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdStateByDocId @pStepId numeric, @pAsigned numeric, @pExclusive numeric, @pExpDate datetime, @pUserId numeric, @pDocID numeric as UPDATE WFDOCUMENT SET STEP_ID=@pStepId , USER_ASIGNED = @pAsigned ,EXCLUSIVE = @pExclusive , EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= @pUserId WHERE DOC_ID =@pDocID "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdStateByDocId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdStateByDocId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdStateByDocId @pStepId numeric, @pAsigned numeric, @pExclusive numeric, @pExpDate datetime, @pUserId numeric, @pDocID numeric as UPDATE WFDOCUMENT SET STEP_ID=@pStepId , USER_ASIGNED = @pAsigned ,EXCLUSIVE = @pExclusive , EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= @pUserId WHERE DOC_ID =@pDocID "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_CloseTask') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_CloseTask "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_CloseTask @pTaskId numeric as UPDATE WFDOCUMENT SET USER_ASIGNED = 0 ,CheckIn = NULL,User_Asigned_By = 0 ,Date_Asigned_By = NULL WHERE Task_ID = @pTaskId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdDelegateTaskByTask') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdDelegateTaskByTask"
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdDelegateTaskByTask @pStepId numeric, @pAsigned numeric, @pExpDate datetime, @pUserId numeric, @pAsgDate datetime,@pTaskId numeric as UPDATE WFDOCUMENT SET STEP_ID= @pStepId ,USER_ASIGNED = @pAsigned ,EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= @pUserId ,DATE_ASIGNED_BY= @pAsgDate ,CheckIn=NULL WHERE Task_ID = @pTaskId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdSetExclusiveTask') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdSetExclusiveTask "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdSetExclusiveTask @pTaskId numeric as UPDATE WFDOCUMENT SET EXCLUSIVE=1 WHERE Task_ID = @pTaskId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdClearExclusiveTask') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdClearExclusiveTask "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdClearExclusiveTask @pTaskId numeric as UPDATE WFDOCUMENT SET EXCLUSIVE=0 WHERE Task_ID = @pTaskId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)


            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_MoveCompleteFolder') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_MoveCompleteFolder "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create procedure zsp_workflow_100_MoveCompleteFolder @pStateId numeric,@pStepId numeric,@pCheckIn datetime,@pAsigned numeric,@pExpDate datetime,@pFolderId numeric as UPDATE WFDOCUMENT SET DO_STATE_ID= @pStateId ,STEP_ID =@pStepId ,CheckIn = @pCheckIn ,USER_ASIGNED= @pAsigned ,EXPIREDATE= @pExpDate "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_MoveTaskByDocId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_MoveTaskByDocId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create procedure zsp_workflow_100_MoveTaskByDocId @pStateId numeric,@pStepId numeric,@pCheckIn datetime,@pAsigned numeric,@pExpDate datetime,@pDocId numeric AS UPDATE WFDOCUMENT SET DO_STATE_ID=  @pStateId ,STEP_ID = @pStepId ,CheckIn =  @pCheckIn ,USER_ASIGNED=  @pAsigned ,EXPIREDATE= @pExpDate WHERE DOC_ID = @pDocId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdExpireDateByDocId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdExpireDateByDocId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdExpireDateByDocId @pExpDate datetime, @pDocId numeric AS UPDATE WFDOCUMENT SET EXPIREDATE=@pExpDate WHERE DOC_ID = @pDocId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdDoSateByDocIdStateId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdDoSateByDocIdStateId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdDoSateByDocIdStateId @pStateId numeric,@pDocId numeric,@pStepId numeric as Update WFDocument Set do_state_id= @pStateId  where doc_id= @pDocId  and step_id= @pStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdInitialStep') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdInitialStep "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdInitialStep @pIStepId numeric , @pWfid numeric as UPDATE wfworkflow SET  initialstepId = @pIStepId where work_id = @pWfid "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdWfName') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdWfName "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdWfName @pName varchar(50), @pWork_Id numeric as UPDATE wfworkflow SET name = @pName where work_id = @pWork_Id "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdRefreshRate') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdRefreshRate "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdRefreshRate @pInterval numeric , @pWfid numeric as UPDATE wfworkflow SET  refreshrate = @pInterval where work_id = @pWfid "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdInitialStateByStepId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdInitialStateByStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdInitialStateByStepId @pInitial numeric, @pStepId numeric as UPDATE WFStepStates SET Initial = @pInitial where step_id= @pStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdStepByStepId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdStepByStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdStepByStepId  @pName varchar(50) , @pDescription varchar(100) ,@pHelp varchar(100) , @pEditDate datetime , @pImgInd numeric , @pLocX decimal , @pLocY decimal , @pStart numeric ,@pMaxHours numeric , @pMaxDocs numeric , @pStepId numeric as UPDATE WFSTEP set Name = @pName ,Description = @pDescription ,Help = @pHelp ,EditDate = @pEditDate ,ImageIndex = @pImgInd ,LocationX = @pLocX , LocationY = @pLocY ,StartAtopenDoc = @pStart ,Max_Hours = @pMaxHours ,Max_Docs = @pMaxDocs where step_id = @pStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdStateDescription') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdStateDescription "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdStateDescription @pName varchar(50), @pDesc nvarchar(250), @pStateId numeric as Update WFStepStates Set name= @pName , description= @pDesc where doc_state_id= @pStateId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdDocTypeLifeCycle') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdDocTypeLifeCycle "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdDocTypeLifeCycle @WfId bit, @DocTypeID numeric as update doc_type set Life_Cycle=@WfID where doc_type_id=@DocTypeID"
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdDocumentDelegateTask') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdDocumentDelegateTask "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdDocumentDelegateTask @pStepId numeric, @pExpDate datetime, @pTaskId numeric as UPDATE WFDOCUMENT SET STEP_ID= @pStepId  ,USER_ASIGNED = 0 ,EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= 0 ,DATE_ASIGNED_BY=NULL,CheckIn=NULL WHERE Task_ID =  @pTaskId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdAsignedUserOpenTask') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdAsignedUserOpenTask "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdAsignedUserOpenTask @pAsignedTo numeric ,@pCheckIn numeric ,@pAsignedId numeric ,@pAsgDate datetime ,@pTaskId numeric as UPDATE WFDOCUMENT SET USER_ASIGNED = @pAsignedTo ,CheckIn = @pCheckIn,User_Asigned_By = @pAsignedId ,Date_Asigned_By = @pAsgDate WHERE Task_ID = @pTaskId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)


            Try
                ZPaq.IfExists("zsp_imports_100_DeleteHystory", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_imports_100_DeleteHystory') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_imports_100_DeleteHystory "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create PROC zsp_imports_100_DeleteHystory @HistoryId numeric As DELETE FROM P_HST WHERE ID =@HistoryId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_connection_100_CountConnections", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_connection_100_CountConnections') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_connection_100_CountConnections "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_connection_100_CountConnections AS SELECT count(CON_ID) FROM UCM "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_lock_100_DeleteLocked", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_lock_100_DeleteLocked') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_lock_100_DeleteLocked "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE Proc zsp_lock_100_DeleteLocked @Doc_Id decimal As Delete from LCK where Doc_ID=@Doc_Id "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_doctypes_100_GetAllDocTypesIdNames", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_doctypes_100_GetAllDocTypesIdNames') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_doctypes_100_GetAllDocTypesIdNames "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "Create Procedure zsp_doctypes_100_GetAllDocTypesIdNames as Select Doc_Type_ID,Doc_Type_Name from Doc_Type "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_users_100_GetUserAddressBook", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_users_100_GetUserAddressBook') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_users_100_GetUserAddressBook "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE procedure zsp_users_100_GetUserAddressBook  @userID Int AS SELECT ADDRESS_BOOK FROM USRTABLE WHERE ID=@useRID "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try



            Try
                ZPaq.IfExists("zsp_lock_100_GetBlockeds", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_lock_100_GetBlockeds') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_lock_100_GetBlockeds "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_lock_100_GetBlockeds AS SELECT IP_Task.Id AS ID, IP_Task.File_Path AS Ruta, IP_Task.Zip_Origen AS Archivo_Zip FROM IP_Task INNER JOIN IP_Folder ON IP_Task.Id_Configuracion = IP_Folder.Id  WHERE IP_Task.Bloqueado = 1 "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_security_100_GetDocTypeRights", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_security_100_GetDocTypeRights') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_security_100_GetDocTypeRights "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE Proc zsp_security_100_GetDocTypeRights @UserId numeric As SELECT DOC_TYPE.DOC_TYPE_ID, DOC_TYPE.DOC_TYPE_NAME, DOC_TYPE.FILE_FORMAT_ID, DOC_TYPE.DISK_GROUP_ID, DOC_TYPE.THUMBNAILS, DOC_TYPE.ICON_ID, DOC_TYPE.CROSS_REFERENCE, DOC_TYPE.LIFE_CYCLE, DOC_TYPE.OBJECT_TYPE_ID, DOC_TYPE.AUTONAME, DOC_TYPE.DocumentalId, DOC_TYPE.DOCCOUNT FROM DOC_TYPE INNER JOIN USR_RIGHTS ON DOC_TYPE.DOC_TYPE_ID = USR_RIGHTS.ADITIONAL WHERE (USR_RIGHTS.GROUPID = @userID) AND (USR_RIGHTS.RTYPE = 3) AND (USR_RIGHTS.OBJID = 2) ORDER BY DOC_TYPE.DOC_TYPE_NAME; "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_doctypes_100_GetDocumentActions", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_doctypes_100_GetDocumentActions') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_doctypes_100_GetDocumentActions "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_doctypes_100_GetDocumentActions (@DocumentId numeric) AS SELECT  USRTABLE.[Name] as " & Chr(34) & "User_Name" & Chr(34) & ", USER_HST.Action_Date, OBJECTTYPES.OBJECTTYPES, RIGHTSTYPE.RIGHTSTYPE,USER_HST.[s_Object_Id] FROM USER_HST, USRTABLE , OBJECTTYPES, RIGHTSTYPE WHERE USER_HST.[User_Id] = USRTABLE.[Id] AND USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId AND USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId AND [Object_Id] = @DocumentId AND Object_Type_Id = 6 "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_security_100_GetUserDocumentsResctrictions", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_security_100_GetUserDocumentsResctrictions') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_security_100_GetUserDocumentsResctrictions "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_security_100_GetUserDocumentsResctrictions @UserID int AS select RESTRICTION_ID from doc_restriction_r_user where [user_id]=@UserId"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_users_100_GetUserActions", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_users_100_GetUserActions') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_users_100_GetUserActions "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create PROC zsp_users_100_GetUserActions @UserId numeric as SELECT USER_HST.Action_Date AS Fecha, OBJECTTYPES.OBJECTTYPES AS Herramienta, RIGHTSTYPE.RIGHTSTYPE AS Accion, user_hst.s_object_id AS En FROM USER_HST,USRTABLE,OBJECTTYPES,RIGHTSTYPE WHERE USER_HST.User_Id = USRTABLE.Id AND USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId AND USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId AND USER_HST.User_Id = @UserId ORDER BY USER_HST.Action_Date DESC "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try





            Try
                ZPaq.IfExists("zsp_messages_100_InsertMsgDest", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_messages_100_InsertMsgDest') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_messages_100_InsertMsgDest "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE procedure zsp_messages_100_InsertMsgDest @m_id INT,@m_userid INT,@m_Dest_TYPE Char,@m_User_Name varchar(100) AS INSERT INTO msg_dest(msg_id, [user_id],dest_type,[read],deleted,[User_Name]) VALUES (@m_id, @m_userid, @m_Dest_TYPE,  0,  0,  @m_User_Name) "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_doctypes_100_GetDocTypesByGroupId", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_doctypes_100_GetDocTypesByGroupId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_doctypes_100_GetDocTypesByGroupId "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create PROC zsp_doctypes_100_GetDocTypesByGroupId @DocGroupId numeric as SELECT DOC_TYPE.Doc_Type_Id, DOC_TYPE.Doc_Type_Name, DOC_TYPE.Object_Type_Id, DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Order,DOC_TYPE_R_DOC_TYPE_GROUP.DOC_TYPE_GROUP FROM DOC_TYPE ,DOC_TYPE_R_DOC_TYPE_GROUP WHERE DOC_TYPE.Doc_Type_Id = DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Type_Id AND DOC_TYPE_R_DOC_TYPE_GROUP.DOC_TYPE_GROUP = @DocGroupId ORDER BY DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Order "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try



            Try
                ZPaq.IfExists("zsp_index_100_GetDocTypeIndexs", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_GetDocTypeIndexs') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_GetDocTypeIndexs "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_index_100_GetDocTypeIndexs @DocTypeId int AS SELECT Doc_Index.Index_Id, Doc_Index.Index_Name, Doc_Index.Index_Type, Doc_Index.Index_Len, Doc_Index.Object_Type_Id, Index_R_Doc_Type.Orden, Index_R_Doc_Type.Doc_Type_Id FROM Doc_Index INNER JOIN Index_R_Doc_Type ON Doc_Index.Index_Id = Index_R_Doc_Type.Index_Id WHERE(Index_R_Doc_Type.Doc_Type_Id = @DocTypeId) ORDER BY Index_R_Doc_Type.Orden "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_lock_100_GetDocumentLockedState", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_lock_100_GetDocumentLockedState') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_lock_100_GetDocumentLockedState "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "Create proc zsp_lock_100_GetDocumentLockedState (@Doc_ID decimal) As IF ((Select Count(*) from LCK Where Doc_ID=@Doc_ID) > 0)   Select 1 Else  Select 0 "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_imports_100_GetProcessHistory", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_imports_100_GetProcessHistory') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_imports_100_GetProcessHistory "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "CREATE PROCEDURE zsp_imports_100_GetProcessHistory (@ProcessID NUMERIC) AS SELECT P_HST.ID,P_HST.Process_Date,P_HST.[User_Id], P_HST.TotalFiles,P_HST.ProcessedFiles,P_HST.Result_Id ,P_HST.SkipedFiles,P_HST.ErrorFiles,P_HST.Path,USRTABLE.Name,P_HST.Process_id,ip_results.Result ,P_HST.Hash FROM P_HST,USRTABLE,IP_RESULTS WHERE P_HST.[User_Id] = USRTABLE.Id  AND P_HST.process_ID = @ProcessID AND IP_RESULTS.RESULT_ID = P_HST.RESULT_ID ORDER BY P_HST.ID DESC "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_barcode_100_UpdBarCode", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_barcode_100_UpdBarCode') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_barcode_100_UpdBarCode "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create PROC zsp_barcode_100_UpdBarCode @caratulaid numeric,@lote varchar(10),@caja numeric as UPDATE zbarcode SET scanned='SI', scanneddate=getdate(), batch=@lote, box=@caja WHERE id = @caratulaid "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            'Try
            '    Zpaq.IfExists("zsp_security_100_UpdUserRight", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
            '    'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_security_100_UpdUserRight') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_security_100_UpdUserRight "
            '    'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            '    strcreate = "CREATE PROC zsp_security_100_UpdUserRight @Rightv  numeric, @Rightid  numeric AS UPDATE USR_RIGHTS SET Right_Value =@Rightv  WHERE Right_Id =@Rightid"
            '    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'Catch ex As Exception
            '    ZException.Log(ex, False)
            '    bResultado = False
            'End Try

            Try
                ZPaq.IfExists("zsp_index_100_GetIndexDropDown", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_index_100_GetIndexDropDown') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_index_100_GetIndexDropDown "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create proc zsp_index_100_GetIndexDropDown @indexid int as Select Dropdown from doc_Index where index_id=@indexid "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            'Try
            '    Zpaq.IfExists("", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
            '    'strcreate = "if exists (select * from sysobjects where id = object_id(N'ZDtGetDgIdByDtId') and OBJECTPROPERTY(id, N'IsProcedure') = 1)drop procedure ZDtGetDgIdByDtId "
            '    'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            '    strcreate = "create proc ZDtGetDgIdByDtId @DocTypeId numeric as Select disk_group_id from doc_type where doc_type_id=@DoctypeId "
            '    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'Catch ex As Exception
            '    ZException.Log(ex, False)
            '    bResultado = False
            'End Try
            Try
                ZPaq.IfExists("zsp_doctype_100_GetAssociatedIndex", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_doctype_100_GetAssociatedIndex') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_doctype_100_GetAssociatedIndex "
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                'strcreate = "create proc zsp_doctype_100_GetAssociatedIndex @DocTypeId1 numeric,@DocTypeId2 numeric as Select Index1,index2 from doctypes_associated where doctypeid1=@DocTypeId1 and doctypeId2=@DocTypeId2 "
                strcreate = "create proc zsp_doctype_100_GetAssociatedIndex @DocTypeId1 numeric,@DocTypeId2 numeric as Select Index1,index2 from DOC_TYPE_R_DOC_TYPE where doctypeid1=@DocTypeId1 and doctypeId2=@DocTypeId2 "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_volume_100_UpdFilesByVolId", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_volume_100_UpdFilesByVolId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_volume_100_UpdFilesByVolId"
                'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create PROC zsp_volume_100_UpdFilesByVolId @Archs decimal,@DiskVolId int as Update disk_volume set Disk_vol_files=@Archs where disk_vol_id=@DiskVolId "
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_GetGroupIdByStepId') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_GetGroupIdByStepId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_GetGroupIdByStepId @pStepId numeric as Select GROUPID from ZVIEWWFUSERSTEPS where step_id = @pStepId; "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_InsertStepStates') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_InsertStepStates "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_InsertStepStates @pStateID numeric, @pDesc nvarchar(250), @pStepId numeric, @pName varchar(50), @pInitial numeric AS INSERT INTO WFStepStates (Doc_State_id,Description,Step_Id,Name,Initial) VALUES ( @pStateID , @pDesc , @pStepId , @pName , @pInitial )"
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdStetStates') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdStetStates "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create procedure zsp_workflow_100_UpdStetStates @pStepId numeric, @pName varchar(50), @pDesc nvarchar(250), @pInitial numeric, @pStateID numeric AS UPDATE WFStepStates SET STEP_ID = @pStepId  , Name = @pName , Description = @pDesc  where Doc_State_Id = @pStateID; "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)

            'strcreate = "if exists (select * from sysobjects where id = object_id(N'zsp_workflow_100_UpdInitialStepState') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure zsp_workflow_100_UpdInitialStepState "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create proc zsp_workflow_100_UpdInitialStepState @pInitial numeric, @pStateId numeric as UPDATE WFStepStates SET Initial = @pInitial where doc_state_id= @pStateId "
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)



            'CREATE PROCEDURE zsp_users_100_GetUserNameDocumentAction @DocumentId int as
            'SELECT (UsrTable.Nombres + ' ' + UsrTable.apellido) as Usuario, User_Hst.Action_Date as Fecha, RightsType.RightsType as [Accion], ObjectTypes.ObjectTypes as "Tipo de Objeto",  User_Hst.[s_Object_Id] as [Objeto] 
            'FROM User_Hst INNER JOIN UsrTable ON User_Hst.[User_Id] = UsrTable.[Id] INNER JOIN ObjectTypes ON User_Hst.Object_Type_Id = ObjectTypes.ObjectTypesId INNER JOIN RightsType ON User_Hst.Action_Type = RightsType.RightsTypeId 
            'where ([Object_Id] = @DocumentId) AND (Object_Type_Id = 6)
            Try
                ZPaq.IfExists("zsp_users_100_GetUserNameDocumentAction", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                strcreate = "CREATE PROCEDURE zsp_users_100_GetUserNameDocumentAction @DocumentId int as SELECT (UsrTable.Nombres + ' ' + UsrTable.apellido) as Usuario, User_Hst.Action_Date as Fecha, RightsType.RightsType as [Accion], ObjectTypes.ObjectTypes as [Tipo de Objeto],  User_Hst.[s_Object_Id] as [Objeto] FROM User_Hst INNER JOIN UsrTable ON User_Hst.[User_Id] = UsrTable.[Id] INNER JOIN ObjectTypes ON User_Hst.Object_Type_Id = ObjectTypes.ObjectTypesId INNER JOIN RightsType ON User_Hst.Action_Type = RightsType.RightsTypeId where ([Object_Id] = @DocumentId) AND (Object_Type_Id = 6)"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


        Else    'ORACLE
            Try
                ZPaq.IfExists("zsp_barcode_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_barcode_100
                strcreate = "create or replace package zsp_barcode_100 as PROCEDURE InsertBarCode(idbarcode IN  ZBarCode.ID%TYPE, DocTypeId IN ZBarCode.doc_type_id%TYPE, UserId in ZBarCode.Userid%type,Doc_id in ZBarCode.Doc_Id%type); PROCEDURE  UpdBarCode(caratulaid IN zbarcode.id%TYPE,lote IN zbarcode.batch%TYPE,caja IN zbarcode.box%TYPE); end;"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create or replace package body zsp_barcode_100 as PROCEDURE InsertBarCode(idbarcode IN  ZBarCode.ID%TYPE, DocTypeId IN ZBarCode.doc_type_id%TYPE, UserId in ZBarCode.Userid%type,Doc_id in ZBarCode.Doc_Id%type) IS BEGIN Insert into ZBarCode(Id,Fecha,Doc_Type_ID,UserId,Doc_Id) Values(idbarcode,Sysdate,DocTypeId,Userid,Doc_id); COMMIT; END InsertBarCode; PROCEDURE  UpdBarCode(caratulaid IN zbarcode.id%TYPE,lote IN zbarcode.batch%TYPE,caja IN zbarcode.box%TYPE) IS BEGIN UPDATE zbarcode SET scanned='SI', scanneddate=sysdate, batch=lote, box=caja WHERE id = caratulaid; COMMIT; END UpdBarCode; end zsp_barcode_100;"

                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_connection_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_connection_100
                strcreate = "create or replace package zsp_connection_100 as TYPE t_cursor IS REF CURSOR; PROCEDURE CountConnections (io_cursor OUT t_cursor);PROCEDURE DeleteConnection(conid IN UCM.CON_ID%TYPE );PROCEDURE InsertNewConecction(v_userId IN UCM.USER_ID%TYPE, v_win_User IN UCM.WINUSER%TYPE, v_win_Pc IN UCM.WINPC%TYPE, v_con_Id IN UCM.CON_ID%TYPE, v_timeout IN UCM.TIME_OUT%TYPE,WF IN UCM.Type%Type);end;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
                strcreate = "create or replace package body zsp_connection_100 as PROCEDURE CountConnections (io_cursor OUT t_cursor) IS  v_cursor t_cursor; BEGIN  OPEN v_cursor FOR SELECT count(CON_ID) FROM UCM;  io_cursor := v_cursor; END CountConnections;PROCEDURE DeleteConnection(conid IN UCM.CON_ID%TYPE ) IS BEGIN DELETE FROM UCM WHERE CON_ID = conid;COMMIT;END DeleteConnection;PROCEDURE InsertNewConecction(v_userId        IN UCM.USER_ID%TYPE, v_win_User    IN UCM.WINUSER%TYPE, v_win_Pc IN UCM.WINPC%TYPE, v_con_Id IN UCM.CON_ID%TYPE, v_timeout IN UCM.TIME_OUT%TYPE,WF IN UCM.Type%Type) IS BEGIN INSERT INTO UCM(USER_ID,C_TIME, U_TIME, WINUSER,WINPC,CON_ID,TIME_OUT,Type) VALUES (v_UserId,SYSDATE, SYSDATE,v_Win_User,v_Win_PC,v_con_Id,v_timeout,WF);COMMIT;END InsertNewConecction;end zsp_connection_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_docassociated_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_docassociated_100
                strcreate = "create or replace package zsp_docassociated_100 as TYPE t_cursor IS REF CURSOR;PROCEDURE GetDocAssociatedById(pDoctypeId IN DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,io_cursor OUT t_cursor);procedure GetDocAssociatedId2ById1(DocTypeId IN DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,io_cursor OUT t_cursor);end;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
                strcreate = "create or replace package body zsp_docassociated_100 as PROCEDURE GetDocAssociatedById(pDoctypeId IN DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,io_cursor OUT t_cursor) IS v_cursor t_cursor;   BEGIN   OPEN v_cursor FOR Select count(*) from DOC_TYPE_R_DOC_TYPE where DoctypeId1=pDocTypeId or doctypeid2=pDocTypeId;   io_cursor := v_cursor;   END GetDocAssociatedById; procedure GetDocAssociatedId2ById1(DocTypeId IN DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,io_cursor OUT t_cursor) IS   v_cursor t_cursor;   BEGIN   OPEN v_cursor FOR Select DoctypeId2 from DOC_TYPE_R_DOC_TYPE where doctypeid1= DocTypeId;   io_cursor:=v_cursor;   END GetDocAssociatedId2ById1;end zsp_docassociated_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try



            Try
                ZPaq.IfExists("zsp_doctypes_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_doctypes_100
                Dim str As New System.Text.StringBuilder
                str.Append("create or replace package zsp_doctypes_100 as ")
                str.Append("TYPE t_cursor IS REF CURSOR;")
                str.Append("PROCEDURE CopyDocType(DocTypeId NUMBER,NewdocTypeId NUMBER,NewName VARCHAR2);")
                str.Append("PROCEDURE FillMeTreeView (UserId IN NUMBER, io_cursor OUT t_cursor);")
                str.Append("Procedure GetAllDocTypesIdNames(io_cursor OUT t_cursor);")
                str.Append("PROCEDURE GetDocumentActions (DocumentId IN NUMBER, io_cursor OUT t_cursor);")
                str.Append("PROCEDURE IncrementsDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,X IN Number);")
                str.Append("PROCEDURE GetDocTypesByGroupId (DocGroupId IN NUMBER, io_cursor OUT t_cursor);")
                str.Append("Procedure GetDiskGroupId(DocTypeId IN Doc_type.doc_type_Id%type,io_cursor OUT t_cursor);")
                str.Append("Procedure GetAssociatedIndex(DocTypeId1 IN DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,DocTypeId2 IN DOC_TYPE_R_DOC_TYPE.DoctypeId2%type,io_cursor OUT t_cursor);")
                str.Append("PROCEDURE UpdDocCountById(DocCount IN DOC_TYPE.DOCCOUNT%type,DocTypeId IN DOC_TYPE.DOC_TYPE_ID%type);")
                str.Append("PROCEDURE UpdMbById(TamArch IN DOC_TYPE.MB%type,DocTypeId IN DOC_TYPE.DOC_TYPE_ID%type);")
                str.Append("PROCEDURE GetUserNameDocumentAction (DocumentId IN user_hst.Object_Id%type, io_cursor OUT t_cursor);")
                str.Append("end zsp_doctypes_100;")
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, str.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(str.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
                str = Nothing
                str = New System.Text.StringBuilder
                str.Append("Create or Replace Package body zsp_doctypes_100 as")
                str.Append(" PROCEDURE CopyDocType(DocTypeId NUMBER,NewdocTypeId NUMBER,NewName VARCHAR2) IS ")
                str.Append("BEGIN ")
                str.Append("DECLARE FILEFORMATID NUMBER;")
                str.Append("DISKGROUPID NUMBER;THUMB NUMBER;ICONID NUMBER;CROSSREFERENCE NUMBER;LIFECYCLE NUMBER;OBJECTTYPEID NUMBER;ANAME VARCHAR2(255);")
                str.Append("BEGIN SELECT FILE_FORMAT_ID , Disk_Group_ID, THUMBNAILS, ICON_ID, CROSS_REFERENCE, LIFE_CYCLE, OBJECT_TYPE_ID,AUTONAME INTO FILEFORMATID,  DISKGROUPID, THUMB, ICONID, CROSSREFERENCE, LIFECYCLE, OBJECTTYPEID, ANAME FROM DOC_TYPE WHERE DOC_TYPE_ID=DOCTYPEID;")
                str.Append("INSERT INTO DOC_TYPE(DOC_TYPE_ID,DOC_TYPE_NAME,FILE_FORMAT_ID,DISK_GROUP_ID,THUMBNAILS,ICON_ID,CROSS_REFERENCE,LIFE_CYCLE,OBJECT_TYPE_ID,AUTONAME,DOCUMENTALID) VALUES(NewDocTypeid,NewName,FILEFORMATID,DISKGROUPID,THUMB,ICONID,CROSSREFERENCE,LIFECYCLE,OBJECTTYPEID,ANAME,0);")
                str.Append("COMMIT;")
                str.Append("DELETE FROM TABLATEMP;")
                str.Append("INSERT INTO TABLATEMP(Index_ID,Doc_Type_ID,Orden,MUSTCOMPLETE,SHOWLOTUS,LOADLOTUS ) SELECT Index_ID,Doc_Type_ID,Orden,MUSTCOMPLETE,SHOWLOTUS,LOADLOTUS FROM INDEX_R_DOC_TYPE WHERE DOC_TYPE_ID=DocTypeID;")
                str.Append("UPDATE TABLATEMP SET Doc_Type_ID=NewDocTypeId;")
                str.Append("COMMIT;")
                str.Append("INSERT INTO INDEX_R_DOC_TYPE(Index_ID,Doc_Type_ID,Orden,MUSTCOMPLETE,SHOWLOTUS,LOADLOTUS ) SELECT Index_ID,Doc_Type_ID,Orden,MUSTCOMPLETE,SHOWLOTUS,LOADLOTUS FROM TABLATEMP WHERE Doc_Type_ID=NewDocTypeID;")
                str.Append("COMMIT;")
                str.Append("DELETE FROM TABLATEMP;")
                str.Append("Commit;")
                str.Append("END;")
                str.Append("END CopyDocType;")
                str.Append("PROCEDURE FillMeTreeView (UserId IN NUMBER, io_cursor OUT t_cursor) IS  v_cursor t_cursor;")
                str.Append("BEGIN ")
                str.Append("OPEN v_cursor FOR SELECT DOC_TYPE_GROUP.Doc_Type_Group_ID, DOC_TYPE_GROUP.Doc_Type_Group_Name, DOC_TYPE_GROUP.Icon, DOC_TYPE_GROUP.Parent_Id, DOC_TYPE_GROUP.Object_Type_Id, USR_RIGHTS.groupId, USR_RIGHTS.RType FROM DOC_TYPE_GROUP,USR_RIGHTS WHERE DOC_TYPE_GROUP.Doc_Type_Group_ID = USR_RIGHTS.aditional AND DOC_TYPE_GROUP.Object_Type_Id = USR_RIGHTS.ObjID and USR_RIGHTS.groupId = UserID AND USR_RIGHTS.RType = 1 ORDER BY DOC_TYPE_GROUP.Doc_Type_Group_ID;")
                str.Append("io_cursor := v_cursor;")
                str.Append("END FillMeTreeView;")
                str.Append("Procedure GetAllDocTypesIdNames(io_cursor OUT t_cursor)IS v_cursor t_cursor;")
                str.Append("Begin OPEN v_cursor FOR Select Doc_Type_ID,Doc_Type_Name from Doc_Type;")
                str.Append("io_cursor := v_cursor;")
                str.Append("End GetAllDocTypesIdNames;")
                str.Append("PROCEDURE GetDocumentActions (DocumentId IN NUMBER, io_cursor OUT t_cursor) IS  v_cursor t_cursor;")
                str.Append("BEGIN OPEN v_cursor FOR SELECT USRTABLE.Name User_Name, USER_HST.Action_Date, OBJECTTYPES.OBJECTTYPES, RIGHTSTYPE.RIGHTSTYPE, USER_HST.Object_Id, User_hst.s_object_id FROM USER_HST , USRTABLE, OBJECTTYPES, RIGHTSTYPE WHERE USER_HST.User_Id = USRTABLE.Id AND USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId AND USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId AND Object_Id = DocumentId AND Object_Type_Id = 6  ORDER BY USER_HST.ACTION_DATE DESC, USER_HST.OBJECT_ID, USER_HST.S_OBJECT_ID;")
                str.Append("io_cursor := v_cursor;")
                str.Append("END GetDocumentActions;")
                str.Append("PROCEDURE IncrementsDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,X IN Number) IS ")
                str.Append("BEGIN ")
                str.Append("Update Doc_Type Set DocCount=DocCount + X where Doc_Type_Id= DocID;")
                str.Append("END IncrementsDocType;")
                str.Append("PROCEDURE GetDocTypesByGroupId (DocGroupId IN NUMBER, io_cursor OUT t_cursor) IS v_cursor t_cursor;")
                str.Append("BEGIN ")
                str.Append("OPEN v_cursor FOR SELECT DOC_TYPE.Doc_Type_Id, DOC_TYPE.Doc_Type_Name, DOC_TYPE.Object_Type_Id, DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Order,DOC_TYPE_R_DOC_TYPE_GROUP.DOC_TYPE_GROUP FROM DOC_TYPE ,DOC_TYPE_R_DOC_TYPE_GROUP WHERE DOC_TYPE.Doc_Type_Id = DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Type_Id AND DOC_TYPE_R_DOC_TYPE_GROUP.DOC_TYPE_GROUP = DocGroupId ORDER BY DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Order;")
                str.Append("io_cursor := v_cursor;")
                str.Append("END GetDocTypesByGroupId;")
                str.Append("procedure GetDiskGroupId(DocTypeId IN Doc_type.doc_type_Id%type,io_cursor OUT t_cursor) IS ")
                str.Append("v_cursor t_cursor;")
                str.Append("BEGIN ")
                str.Append("OPEN v_cursor FOR Select disk_group_id from doc_type where doc_type_id=DoctypeId;")
                str.Append("io_cursor:=v_cursor;")
                str.Append("END GetDiskGroupId;")
                str.Append("procedure GetAssociatedIndex(DocTypeId1 IN DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,DocTypeId2 IN DOC_TYPE_R_DOC_TYPE.DoctypeId2%type,io_cursor OUT t_cursor) IS ")
                str.Append("v_cursor t_cursor; ")
                str.Append("BEGIN OPEN v_cursor FOR Select Index1,index2 from DOC_TYPE_R_DOC_TYPE where doctypeid1=DocTypeId1 and doctypeId2=DocTypeId2;")
                str.Append("io_cursor:=v_cursor;")
                str.Append("END GetAssociatedIndex;")
                str.Append("PROCEDURE UpdDocCountById(DocCount IN DOC_TYPE.DOCCOUNT%type,DocTypeId IN DOC_TYPE.DOC_TYPE_ID%type) IS ")
                str.Append("BEGIN ")
                str.Append("Update DOC_TYPE set Doccount=DocCount where DOC_TYPE_ID=DocTypeId;")
                str.Append("END UpdDocCountById;")
                str.Append("PROCEDURE UpdMbById(TamArch IN DOC_TYPE.MB%type,DocTypeId IN DOC_TYPE.DOC_TYPE_ID%type) IS ")
                str.Append("BEGIN Update doc_type set MB=(MB + TamArch) where Doc_type_Id= DocTypeId;")
                str.Append("END UpdMbById;")
                str.Append("PROCEDURE GetUserNameDocumentAction (DocumentId IN user_hst.Object_Id%type, io_cursor OUT t_cursor) IS ")
                str.Append("v_cursor t_cursor;")
                str.Append("BEGIN ")
                str.Append("OPEN v_cursor FOR SELECT  USRTABLE.Nombres + ' ' + USRTABLE.apellido Usuario, USER_HST.Action_Date, OBJECTTYPES.OBJECTTYPES, RIGHTSTYPE.RIGHTSTYPE, USER_HST.Object_Id, User_hst.s_object_id FROM USER_HST, USRTABLE, OBJECTTYPES, RIGHTSTYPE WHERE USER_HST.User_Id = USRTABLE.Id AND USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId AND USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId AND Object_Id = DocumentId AND Object_Type_Id = 6 ORDER BY USER_HST.ACTION_DATE DESC, USER_HST.OBJECT_ID, USER_HST.S_OBJECT_ID;")
                str.Append("io_cursor := v_cursor;")
                str.Append("END GetUserNameDocumentAction;")
                str.Append("END zsp_doctypes_100;")
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, str.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(str.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_exception_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_exception_100
                strcreate = "create or replace package zsp_exception_100 as PROCEDURE DeleteExceptionTable;end;"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create or replace package body zsp_exception_100 as PROCEDURE DeleteExceptionTable IS BEGIN Delete from Excep where Fecha >(Sysdate-30);END DeleteExceptionTable;end zsp_exception_100;"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_generic_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_generic_100
                strcreate = "create or replace package zsp_generic_100 as TYPE t_cursor IS REF CURSOR; Procedure ExecSqlString(strsql in Varchar2,io_cursor OUT t_cursor);end zsp_generic_100;"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create or replace package body zsp_generic_100 as Procedure ExecSqlString(strsql in Varchar2,io_cursor OUT t_cursor) is    v_ReturnCursor t_cursor;Begin Open v_ReturnCursor  FOR strsql; io_cursor := v_Returncursor;End ExecSqlString;end zsp_generic_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_imports_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_imports_100
                strcreate = "create or replace package zsp_imports_100 as TYPE t_cursor IS REF CURSOR;Procedure DeleteHystory(HistoryId IN P_HST.ID%TYPE);Procedure InsertProcHistory (HID in  p_hst.id%type,PID in p_hst.Process_id%type, PDATE in p_hst.Process_Date%type,USrid in p_hst.user_id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type,skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type,RID in p_hst.Result_id%type,Pth in p_hst.Path%type,hsh in p_hst.hash%type,tfile in p_hst.tempfile%type,efile in  p_hst.errorfile%type, lfile in p_hst.logfile%type);Procedure InsertUserAction(AID IN USER_HST.ACTION_ID%TYPE , AUSRID IN USER_HST.USER_ID%TYPE , AOBJID IN USER_HST.USER_ID%TYPE , AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE , ATYPE IN USER_HST.ACTION_TYPE%TYPE,ACONID IN UCM.CON_ID%TYPE, SOBJECTID IN USER_HST.S_OBJECT_ID%TYPE);Procedure GetProcessHistory (ClsIpJob1ID IN NUMBER, io_cursor OUT t_cursor);Procedure UpdProcHistory(HID in  p_hst.id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type, skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type, RID in p_hst.Result_id%type,hsh in p_hst.hash%type);end zsp_imports_100;"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create or replace package body zsp_imports_100 as PROCEDURE DeleteHystory(HistoryId IN P_HST.ID%TYPE)IS  BEGIN     DELETE FROM P_HST WHERE ID =HistoryId;  COMMIT;  END DeleteHystory;  procedure InsertProcHistory (HID in  p_hst.id%type,PID in p_hst.Process_id%type, PDATE in p_hst.Process_Date%type,USrid in p_hst.user_id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type,skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type,RID in p_hst.Result_id%type,Pth in p_hst.Path%type,hsh in p_hst.hash%type,tfile in p_hst.tempfile%type,efile in  p_hst.errorfile%type, lfile in p_hst.logfile%type) is begin INSERT INTO P_HST(ID,Process_ID,Process_Date,User_Id,TotalFiles,ProcessedFiles,SkipedFiles,ErrorFiles,Result_Id,PATH, HASH,errorfile,tempfile,logfile)VALUES(HID,PID ,Pdate,UsrId,TotFiles,ProcFiles,SkpFiles,ErrFiles,RID,Pth,Hsh,efile,tfile,lfile);end InsertProcHistory;PROCEDURE  InsertUserAction(AID IN USER_HST.ACTION_ID%TYPE , AUSRID IN USER_HST.USER_ID%TYPE , AOBJID IN USER_HST.USER_ID%TYPE , AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE , ATYPE IN USER_HST.ACTION_TYPE%TYPE,ACONID IN UCM.CON_ID%TYPE, SOBJECTID IN USER_HST.S_OBJECT_ID%TYPE) IS BEGIN INSERT INTO USER_HST(ACTION_ID, USER_ID,ACTION_DATE, OBJECT_ID,OBJECT_TYPE_ID,ACTION_TYPE,S_Object_Id)VALUES (AID,AUSRID,sysdate,AOBJID,AOBJTID,ATYPE,SOBJECTID);IF AUSRID <> 9999 THEN   UPDATE UCM SET u_time = SYSDATE WHERE con_id= ACONID;END IF;COMMIT;END InsertUserAction;PROCEDURE GetProcessHistory (ClsIpJob1ID IN NUMBER, io_cursor OUT t_cursor) IS  v_cursor t_cursor; BEGIN  OPEN v_cursor FOR   SELECT IP_HST.HST_ID,IP_HST.IP_ID,IP_HST.IPDate, IP_HST.IPUSERID,IP_HST.IPDocCount, IP_HST.IPIndexCount,   IP_RESULTS.RESULT , IP_HST.IPRESULT, IP_HST.IPLINESCOUNT,IP_HST.IPERRORCOUNT,IP_HST.IPPATH,USRTABLE.Name   FROM IP_HST , USRTABLE, IP_RESULTS   WHERE   IP_HST.IPUserId = USRTABLE.Id AND   IP_HST.IP_ID = ClsIpJob1ID AND IP_RESULTS.RESULT_ID = IP_HST.IPRESULT   ORDER BY IP_HST.HST_ID DESC;  io_cursor := v_cursor; END GetProcessHistory;  procedure UpdProcHistory(HID in  p_hst.id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type, skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type, RID in p_hst.Result_id%type,hsh in p_hst.hash%type) is begin UPDATE P_HST SET TotalFiles = TotFiles,ProcessedFiles = ProcFiles,SkipedFiles = SkpFiles,Result_ID = RID,ERRORFiles = ErrFiles , HASH= Hsh where ID = HId;end UpdProcHistory;end zsp_imports_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_index_100", ZPaq.Tipo.Package, Me.CanDrop)
                ''package zsp_index_100
                strcreate = "create or replace package zsp_index_100 as TYPE t_cursor IS REF CURSOR;Procedure FillIndex (IPJOBDocTypeId IN NUMBER, io_cursor OUT t_cursor);Procedure IndexGeneration (DocTypeId IN NUMBER, io_cursor OUT t_cursor);Procedure GetIndexRDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,io_cursor OUT t_cursor);Procedure GetDocTypeIndexs (DocTypeId IN NUMBER, io_cursor OUT t_cursor);Procedure GetIndexQtyByNameId (IndexName in doc_Index.index_name%type, IndexId in doc_Index.index_id%type, io_cursor OUT t_cursor);Procedure GetIndexDropDown(indexid in doc_Index.index_id%type, io_cursor OUT t_cursor);Procedure GetAllIndexRDocType (io_cursor out t_cursor);Procedure GetDoc_dColumns(io_cursor out t_cursor);Procedure GetDoc_iColumns (io_cursor out t_cursor);Procedure InsertLinkInfo(pId IN index_link_info.Id%type ,pData IN index_link_info.Data%type, pFlag IN index_link_info.Flag%type, pDocType IN index_link_info.DocType%type,pDocIndex IN index_link_info.DocIndex%type, pName IN index_link_info.Name%type);Procedure UpdIndexRDoctypeByDtInd(DocTypeId in Index_R_Doc_Type.Doc_Type_ID%type, IndexId in Index_R_Doc_Type.Index_Id%type);Procedure UpdIndexRDoctypeByDtInd2 (DocTypeId int, IndexId int );end zsp_index_100;"
                If GenerateScripts = False Then
                    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
                strcreate = "create or replace package body zsp_index_100 as PROCEDURE FillIndex (IPJOBDocTypeId IN NUMBER, io_cursor OUT t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor FOR SELECT DOC_INDEX.Index_Id, DOC_INDEX.Index_Name, DOC_INDEX.INDEX_TYPE, DOC_INDEX.Index_Len, DOC_INDEX.AutoFill,   DOC_INDEX.NoIndex,DOC_INDEX.DropDown,DOC_INDEX.AutoDisplay,DOC_INDEX.Invisible,DOC_INDEX.Object_Type_Id FROM  INDEX_R_DOC_TYPE, DOC_INDEX WHERE  INDEX_R_DOC_TYPE.Index_Id = DOC_INDEX.Index_Id AND INDEX_R_DOC_TYPE.Doc_Type_Id = IPJOBDocTypeId;io_cursor := v_cursor;END FillIndex; PROCEDURE IndexGeneration (DocTypeId IN NUMBER, io_cursor OUT t_cursor) IS  v_cursor t_cursor; BEGIN  OPEN v_cursor FOR   SELECT DOC_INDEX.Index_Id, DOC_INDEX.Index_Name, DOC_INDEX.INDEX_TYPE, DOC_INDEX.Index_Len, DOC_INDEX.AutoFill, DOC_INDEX.NoIndex,   DOC_INDEX.DropDown, DOC_INDEX.AutoDisplay, DOC_INDEX.Invisible, DOC_INDEX.Object_Type_Id, INDEX_R_DOC_TYPE.Doc_Type_Id, INDEX_R_DOC_TYPE.Orden   FROM DOC_INDEX ,INDEX_R_DOC_TYPE WHERE   DOC_INDEX.Index_Id = INDEX_R_DOC_TYPE.Index_Id AND Doc_Type_Id = DocTypeId ORDER BY ORDEN;  io_cursor := v_cursor; END IndexGeneration;PROCEDURE GetIndexRDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,io_cursor OUT t_cursor)IS v_cursor t_cursor;BEGIN OPEN v_cursor FOR Select Index_Id, Doc_Type_ID From Index_R_Doc_Type where Doc_Type_ID=DocId;io_cursor := v_cursor;END GetIndexRDocType;PROCEDURE GetDocTypeIndexs (DocTypeId IN NUMBER, io_cursor OUT t_cursor) IS  v_cursor t_cursor; BEGIN OPEN v_cursor FOR SELECT DOC_INDEX.INDEX_ID,DOC_INDEX.INDEX_NAME,DOC_INDEX.INDEX_TYPE,DOC_INDEX.INDEX_LEN, DOC_INDEX.Object_Type_Id,   INDEX_R_DOC_TYPE.Orden,INDEX_R_DOC_TYPE.Doc_Type_Id  FROM DOC_INDEX, INDEX_R_DOC_TYPE  WHERE INDEX_R_DOC_TYPE.INDEX_ID = DOC_INDEX.Index_Id AND   INDEX_R_DOC_TYPE.Doc_Type_Id = DocTypeId  ORDER BY INDEX_R_DOC_TYPE.Orden;io_cursor := v_cursor; END GetDocTypeIndexs;procedure GetIndexQtyByNameId (IndexName in doc_Index.index_name%type, IndexId in doc_Index.index_id%type, io_cursor OUT t_cursor)IS v_cursor t_cursor;begin open v_cursor for select count(*) from Doc_index where Index_name=IndexName and Index_id <>IndexId;io_cursor:=v_cursor;end GetIndexQtyByNameId;Procedure GetIndexDropDown(indexid in doc_Index.index_id%type, io_cursor OUT t_cursor) is v_cursor t_cursor;Begin Open v_cursor  FOR Select Dropdown from doc_Index where index_id=indexid;io_cursor := v_cursor;End GetIndexDropDown;procedure GetAllIndexRDocType (io_cursor out t_cursor) IS v_cursor t_cursor;begin open v_cursor for select index_id, doc_type_id from INDEX_R_DOC_TYPE order by doc_type_id, index_id;io_cursor:=v_cursor;end GetAllIndexRDocType;procedure GetDoc_dColumns(io_cursor out t_cursor) IS v_cursor t_cursor;begin open v_cursor for select replace(COLUMN_NAME,'D',''),replace(TABLE_NAME,'DOC_D','')from user_tab_columns where TABLE_NAME like'DOC_D%' and COLUMN_NAME like 'D%' order by TABLE_NAME,COLUMN_NAME;io_cursor:=v_cursor;end GetDoc_dColumns;procedure GetDoc_iColumns (io_cursor out t_cursor) IS v_cursor t_cursor;begin open v_cursor for select replace(COLUMN_NAME,'I',''),replace(TABLE_NAME,'DOC_I','')from user_tab_columns where TABLE_NAME like'DOC_I%' and COLUMN_NAME like 'I%' order by TABLE_NAME,COLUMN_NAME;io_cursor:=v_cursor;end GetDoc_iColumns;PROCEDURE InsertLinkInfo(pId IN index_link_info.Id%type ,pData IN index_link_info.Data%type, pFlag IN index_link_info.Flag%type, pDocType IN index_link_info.DocType%type,pDocIndex IN index_link_info.DocIndex%type, pName IN index_link_info.Name%type) IS BEGIN insert into index_link_info(id,data,flag,doctype,docindex,name ) values(pId,pData,pFlag,pDocType,pDocIndex, pName);END InsertLinkInfo;Procedure UpdIndexRDoctypeByDtInd(DocTypeId in Index_R_Doc_Type.Doc_Type_ID%type, IndexId in Index_R_Doc_Type.Index_Id%type) is Begin Update Index_R_Doc_Type set Mustcomplete=1, ShowLotus=1, LoadLotus=1 where Doc_Type_ID=DocTypeId and Index_Id=IndexId;End UpdIndexRDoctypeByDtInd;procedure UpdIndexRDoctypeByDtInd2 (DocTypeId int, IndexId int )IS begin Update Index_R_Doc_Type set Mustcomplete=1, ShowLotus=1 where Doc_Type_ID=DocTypeId and Index_Id=IndexId;end UpdIndexRDoctypeByDtInd2;end zsp_index_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_license_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_license_100
                strcreate = "create or replace package zsp_license_100 as TYPE t_cursor IS REF CURSOR;Procedure GetActiveWFConect (io_cursor out t_cursor);Procedure GetDocumentalLicenses(io_cursor OUT t_cursor);end zsp_license_100;"
                If GenerateScripts = False Then
                    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
                strcreate = "create or replace package body zsp_license_100 as procedure GetActiveWFConect (io_cursor out t_cursor) IS v_cursor t_cursor;BEGIN open v_cursor for Select Used from LIC where Type=1;end GetActiveWFConect;PROCEDURE GetDocumentalLicenses(io_cursor OUT t_cursor)IS v_cursor t_cursor;BEGIN OPEN v_cursor FOR SELECT Numero_Licencias FROM LIC;io_cursor := v_cursor;END GetDocumentalLicenses;end zsp_license_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_lock_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_lock_100
                strcreate = "create or replace package zsp_lock_100 as TYPE t_cursor IS REF CURSOR;Procedure DeleteLocked(docid IN LCK.Doc_ID%TYPE,userid IN LCK.USER_ID%TYPE,Estid IN LCK.EST_ID%TYPE);Procedure GetBlockeds (io_cursor OUT t_cursor);Procedure LockDocument(docid IN LCK.Doc_ID%TYPE ,Userid IN LCK.USER_ID%TYPE , Estid IN LCK.Est_Id%TYPE );Procedure GetDocumentLockedState(docid IN LCK.doc_ID%TYPE,io_cursor OUT t_cursor);end zsp_lock_100;"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create or replace package body zsp_lock_100 as PROCEDURE  DeleteLocked(docid IN LCK.Doc_ID%TYPE,userid IN LCK.USER_ID%TYPE,Estid IN LCK.EST_ID%TYPE) IS BEGIN DELETE FROM LCK WHERE Doc_ID=docid AND User_ID=userid AND Est_Id=estid;COMMIT;END DeleteLocked;PROCEDURE GetBlockeds (io_cursor OUT t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor FOR SELECT IP_TASK.Id AS ID, IP_TASK.File_Path AS Ruta, IP_TASK.Zip_Origen AS Archivo_Zip FROM IP_TASK ,IP_FOLDER WHERE IP_TASK.Id_Configuracion = IP_FOLDER.Id AND IP_TASK.Bloqueado = 1;io_cursor := v_cursor;END GetBlockeds;PROCEDURE  LockDocument(docid IN LCK.Doc_ID%TYPE ,Userid IN LCK.USER_ID%TYPE , Estid IN LCK.Est_Id%TYPE ) IS BEGIN INSERT INTO LCK(doc_ID,USER_ID,LCK_Date,Est_Id)VALUES (docid,userid,SYSDATE,Estid);COMMIT;END LockDocument;PROCEDURE  GetDocumentLockedState(docid IN LCK.doc_ID%TYPE,io_cursor OUT t_cursor) IS s_cursor t_cursor;BEGIN OPEN s_cursor FOR SELECT doc_Id FROM LCK WHERE Doc_ID=docid;io_cursor := s_cursor;END GetDocumentLockedState;end zsp_lock_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_messages_100", ZPaq.Tipo.StoredProcedure, Me.CanDrop)
                strcreate = "create or replace package zsp_messages_100 as TYPE t_cursor IS REF CURSOR;PROCEDURE CountNewMessages(userId in numeric,io_cursor OUT t_cursor);PROCEDURE DeleteRecivedMsg(m_id IN MESSAGE.msg_id%TYPE, u_id IN MSG_DEST.user_id%TYPE);PROCEDURE DeleteSenderMsg(m_id IN MESSAGE.msg_id%TYPE);PROCEDURE GetMyMessages(my_id IN USRTABLE.id%TYPE ,io_cursor OUT t_cursor);PROCEDURE GetMyMessagesNew(my_id IN USRTABLE.id%TYPE ,io_cursor OUT t_cursor);PROCEDURE GetMyAttachments(my_id IN USRTABLE.id%TYPE ,io_cursor OUT t_cursor);PROCEDURE InsertMsg(m_id IN MESSAGE.msg_id%TYPE,m_from IN MESSAGE.msg_from%TYPE,m_Body IN MESSAGE.msg_body%TYPE,m_subject IN MESSAGE.subject%TYPE,m_resend IN MESSAGE.reenvio%TYPE); PROCEDURE InsertAttach(m_id IN msg_attach.msg_id%TYPE, m_DOCid IN msg_attach.doc_id%TYPE, m_DOC_TYPE_ID IN msg_attach.doc_type_id%TYPE, m_folder_id IN msg_attach.folder_id%TYPE, m_index_id IN msg_attach.index_id%TYPE,m_name IN msg_attach.name%TYPE, m_icon IN msg_attach.icon%TYPE,  m_volumelistid IN msg_attach.volumelistid%TYPE, m_doc_file IN msg_attach.doc_file%TYPE, m_offset IN msg_attach.offset%TYPE, m_disk_vol_path IN msg_attach.disk_vol_path%TYPE);PROCEDURE InsertMsgDest(m_id IN MSG_DEST.msg_id%TYPE, m_userid IN MSG_DEST.USER_ID%TYPE, m_Dest_TYPE IN MSG_DEST.DEST_TYPE%TYPE, m_User_name IN MSG_DEST.user_name%TYPE);end zsp_messages_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
                strcreate = "create or replace package body zsp_messages_100 as PROCEDURE CountNewMessages(userId in numeric,io_cursor OUT t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor FOR SELECT count(*) FROM MSG_DEST WHERE MSG_DEST.user_id=userid AND MSG_DEST.deleted=0 and read=0;io_cursor := v_cursor;END CountNewMessages;PROCEDURE DeleteRecivedMsg(m_id IN MESSAGE.msg_id%TYPE, u_id IN MSG_DEST.user_id%TYPE) IS BEGIN UPDATE MSG_DEST SET  deleted=1 WHERE msg_id=m_id AND user_id=u_id;END DeleteRecivedMsg;PROCEDURE DeleteSenderMsg(m_id IN MESSAGE.msg_id%TYPE) IS recived NUMERIC;BEGIN SELECT COUNT(*)INTO recived FROM MSG_DEST WHERE msg_id=m_id AND deleted=0;IF (recived=0)THEN DELETE MSG_DEST WHERE msg_id=m_id;DELETE MSG_ATTACH WHERE msg_id=m_id;DELETE MESSAGE WHERE msg_id=m_id;ELSE UPDATE MESSAGE SET deleted=1 WHERE msg_id=m_id;END IF;END DeleteSenderMsg;PROCEDURE GetMyMessages(my_id IN USRTABLE.id%TYPE ,io_cursor OUT t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor FOR SELECT MESSAGE.msg_id, MESSAGE.msg_from, USRTABLE.name User_Name, MESSAGE.subject, MESSAGE.msg_date, MESSAGE.reenvio, MESSAGE.deleted, MSG_DEST.user_id DEST, MSG_DEST.user_name DEST_NAME,MSG_DEST.dest_type, MSG_DEST.READ, MESSAGE.msg_body FROM MESSAGE,MSG_DEST,USRTABLE WHERE message.msg_id=msg_dest.msg_id and message.msg_from=usrtable.id and message.msg_id in(SELECT msg_id FROM MSG_DEST where user_id=my_id AND deleted=0);io_cursor := v_cursor;END GetMyMessages;PROCEDURE GetMyMessagesNew(my_id IN USRTABLE.id%TYPE ,io_cursor OUT t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor FOR SELECT MESSAGE.msg_id, MESSAGE.msg_from, USRTABLE.name User_Name, MESSAGE.subject, MESSAGE.msg_date, MESSAGE.reenvio, MESSAGE.deleted, MSG_DEST.user_id DEST, MSG_DEST.user_name DEST_NAME,MSG_DEST.dest_type, MSG_DEST.READ, MESSAGE.msg_body FROM MESSAGE,MSG_DEST,USRTABLE WHERE message.msg_id=msg_dest.msg_id and message.msg_from=usrtable.id and message.msg_id in(SELECT msg_id FROM MSG_DEST where user_id=my_id AND deleted=0);io_cursor := v_cursor;END GetMyMessagesNew;PROCEDURE GetMyAttachments(my_id IN USRTABLE.id%TYPE ,io_cursor OUT t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor FOR SELECT MESSAGE.msg_id, MSG_ATTACH.msg_id, MSG_ATTACH.doc_id, MSG_ATTACH.doc_type_id, MSG_ATTACH.folder_id, MSG_ATTACH.index_id, MSG_ATTACH.name, MSG_ATTACH.icon, volumelistid, doc_file, offset, disk_vol_path FROM MESSAGE,MSG_ATTACH WHERE MESSAGE.msg_id = MSG_ATTACH.msg_id AND MESSAGE.msg_id IN (SELECT msg.msg_id FROM MESSAGE msg,MSG_DEST WHERE  msg.msg_id = MSG_DEST.msg_id AND MSG_DEST.user_id=my_id AND MSG_DEST.deleted=0); io_cursor := v_cursor;END GetMyAttachments;PROCEDURE InsertMsg(m_id IN MESSAGE.msg_id%TYPE,m_from IN MESSAGE.msg_from%TYPE,m_Body IN MESSAGE.msg_body%TYPE,m_subject IN MESSAGE.subject%TYPE,m_resend IN MESSAGE.reenvio%TYPE) IS BEGIN INSERT INTO MESSAGE(msg_id,msg_from,msg_body,subject,msg_date,reenvio,deleted)VALUES (m_id,m_from,m_body,m_subject,SYSDATE,m_resend, 0);COMMIT;END InsertMSG; PROCEDURE InsertAttach(m_id IN msg_attach.msg_id%TYPE, m_DOCid IN msg_attach.doc_id%TYPE, m_DOC_TYPE_ID IN msg_attach.doc_type_id%TYPE, m_folder_id IN msg_attach.folder_id%TYPE, m_index_id IN msg_attach.index_id%TYPE,m_name IN msg_attach.name%TYPE, m_icon IN msg_attach.icon%TYPE,  m_volumelistid IN msg_attach.volumelistid%TYPE, m_doc_file IN msg_attach.doc_file%TYPE, m_offset IN msg_attach.offset%TYPE, m_disk_vol_path IN msg_attach.disk_vol_path%TYPE) IS BEGIN INSERT INTO msg_attach(MSG_ID,DOC_ID,DOC_TYPE_ID,folder_id,index_id,name,icon,volumelistid,doc_file,offset,disk_vol_path)VALUES (m_id,m_DOCid,m_DOC_TYPE_ID,m_folder_id,m_index_id,m_name,m_icon,m_volumelistid,m_doc_file,m_offset,m_disk_vol_path);COMMIT;END InsertAttach;PROCEDURE InsertMsgDest(m_id IN MSG_DEST.msg_id%TYPE, m_userid IN MSG_DEST.USER_ID%TYPE, m_Dest_TYPE IN MSG_DEST.DEST_TYPE%TYPE, m_User_name IN MSG_DEST.user_name%TYPE) IS BEGIN INSERT INTO MSG_DEST(msg_id,user_id ,dest_type, READ,User_name)VALUES (m_id  ,m_userid,m_Dest_Type,0,m_user_name);COMMIT;END InsertMsgDest;end zsp_messages_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_objects_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_objects_100                
                strcreate = "CREATE OR REPLACE PACKAGE zsp_objects_100 AS TYPE t_cursor IS REF CURSOR;	PROCEDURE GetAndSetLastId (OBJTYPE IN OBJLASTID.OBJECT_TYPE_ID%TYPE, io_cursor OUT t_cursor);END zsp_objects_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
                strcreate = "CREATE OR REPLACE PACKAGE BODY zsp_objects_100 AS  PROCEDURE GetAndSetLastId (OBJTYPE IN OBJLASTID.OBJECT_TYPE_ID%TYPE, io_cursor OUT t_cursor) IS     s_cursor t_cursor;     r_cursor t_cursor;     OBJID OBJLASTID.OBJECTID%TYPE;     BEGIN          OPEN s_cursor FOR SELECT OBJECTID           FROM OBJLASTID        WHERE OBJECT_TYPE_ID = OBJTYPE;              IF SQL%NotFound then                  Insert into Objlastid(Object_type_id,objectid) values(OBJTYPE,0);                  open r_cursor for select objectid from objlastid where object_type_id=objtype;                  io_cursor:=r_cursor;              else                  io_cursor := s_cursor;              End If;              UPDATE OBJLASTID SET OBJECTID = OBJECTID + 1  WHERE OBJECT_TYPE_ID =  OBJTYPE;   END GetAndSetLastId;END zsp_objects_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_schedule_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_schedule_100
                strcreate = "create or replace package zsp_schedule_100 as TYPE t_cursor IS REF CURSOR;PROCEDURE UpdLastTaskExecution(Id IN Schedule.TASK_ID%type,Fecha IN Schedule.FECHA%type, io_cursor OUT t_cursor);procedure GetTasks(Fecha IN Schedule.FECHA%type,io_cursor OUT t_cursor);PROCEDURE DeleteTask(Id IN Schedule.TASK_ID%type);PROCEDURE GetNewTasks (Id IN Schedule.TASK_ID%type, io_cursor OUT t_cursor);end zsp_schedule_100;"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
                strcreate = "create or replace package body zsp_schedule_100 as PROCEDURE UpdLastTaskExecution(Id IN Schedule.TASK_ID%type,Fecha IN Schedule.FECHA%type, io_cursor OUT t_cursor) IS	 BEGIN     Update Schedule set FECHA=Fecha where TASK_ID=Id; END UpdLastTaskExecution;  PROCEDURE ZScdGetTareasHoy(Fecha IN Schedule.FECHA%type,io_cursor OUT t_cursor) IS  	v_cursor t_cursor;   BEGIN   OPEN v_cursor FOR    select * from schedule where Fecha = SYSDATE;   	io_cursor := v_cursor;END ZScdGetTareasHoy;PROCEDURE GetTasks(Fecha IN Schedule.FECHA%type,io_cursor OUT t_cursor) IS  	v_cursor t_cursor;   BEGIN   OPEN v_cursor FOR    select * from schedule where Fecha = SYSDATE;  	io_cursor := v_cursor;END GetTasks;PROCEDURE DeleteTask(Id IN Schedule.TASK_ID%type) IS	 begin	 DELETE from Schedule WHERE TASK_ID=Id;END DeleteTask;procedure GetNewTasks (Id IN Schedule.TASK_ID%type, io_cursor OUT t_cursor) IS v_cursor t_cursor;BEGIN open v_cursor for select * from schedule where task_id > Id;END GetNewTasks;end zsp_schedule_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try


            Try
                ZPaq.IfExists("zsp_users_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_users_100
                strcreate = "create or replace package zsp_users_100 as  TYPE t_cursor IS REF CURSOR;  Procedure GetUserAddressBook (userID IN USRTABLE.ID%type, io_cursor OUT t_cursor);  PROCEDURE GetUserActions (UserId IN NUMBER, io_cursor OUT t_cursor);end;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
                strcreate = "create or replace package body zsp_users_100 as Procedure GetUserAddressBook (userID IN USRTABLE.ID%type, io_cursor OUT t_cursor)  IS v_cursor t_cursor;BEGIN OPEN v_cursor FOR SELECT ADDRESS_BOOK           FROM USRTABLE           WHERE ID=USeRID;io_cursor := v_cursor; END GetUserAddressBook;PROCEDURE GetUserActions (UserId IN NUMBER, io_cursor OUT t_cursor) IS  v_cursor t_cursor; BEGIN  OPEN v_cursor FOR   SELECT USER_HST.Action_Date AS Fecha, OBJECTTYPES.OBJECTTYPES AS Herramienta, RIGHTSTYPE.RIGHTSTYPE AS Accion, user_hst.s_object_id AS En   FROM USER_HST,USRTABLE,OBJECTTYPES,RIGHTSTYPE   WHERE USER_HST.User_Id = USRTABLE.Id AND         USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId AND         USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId AND         USER_HST.User_Id = UserId         ORDER BY USER_HST.Action_Date DESC; io_cursor := v_cursor; END GetUserActions;end zsp_users_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

            Try
                ZPaq.IfExists("zsp_volume_100", ZPaq.Tipo.Package, Me.CanDrop)
                'package zsp_volume_100
                strcreate = "create or replace package zsp_volume_100 as TYPE t_cursor IS REF CURSOR;PROCEDURE  UpdFilesAndSize(VolumeId IN NUMBER, FileSize IN DECIMAL); PROCEDURE  UpdDeletedFiles(VolumeId IN NUMBER, FileSize IN DECIMAL); procedure GetDocGroupRDocVolByDgId(volgroup in disk_group_r_disk_volume.disk_group_id%type,io_cursor OUT t_cursor); PROCEDURE UpdFilesByVolId(Archs IN disk_volume.Disk_vol_files%type,DiskVolId IN disk_volume.disk_vol_id%type);end;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
                strcreate = "create or replace package body zsp_volume_100 as PROCEDURE UpdFilesAndSize(VolumeId IN NUMBER, FileSize IN DECIMAL)IS  totalfiles NUMBER;  totalsize DECIMAL;BEGIN  SELECT DISK_VOL_FILES INTO totalfiles FROM DISK_VOLUME WHERE DISK_VOL_ID = VolumeId;  SELECT DISK_VOL_SIZE_LEN INTO totalsize FROM DISK_VOLUME WHERE DISK_VOL_ID = VolumeId;  UPDATE DISK_VOLUME SET  DISK_VOL_FILES = totalfiles + 1, DISK_VOL_SIZE_LEN = totalsize + FileSize WHERE DISK_VOL_ID = VolumeId;END UpdFilesAndSize;PROCEDURE UpdDeletedFiles(VolumeId IN NUMBER, FileSize IN DECIMAL)IS  totalfiles NUMBER;  totalsize DECIMAL;BEGIN  SELECT DISK_VOL_FILES INTO totalfiles FROM DISK_VOLUME WHERE DISK_VOL_ID = VolumeId;  SELECT DISK_VOL_SIZE_LEN INTO totalsize FROM DISK_VOLUME WHERE DISK_VOL_ID = VolumeId;  UPDATE DISK_VOLUME SET  DISK_VOL_FILES = totalfiles - 1, DISK_VOL_SIZE_LEN = totalsize - FileSize WHERE DISK_VOL_ID = VolumeId;END UpdDeletedFiles;  procedure GetDocGroupRDocVolByDgId(volgroup in disk_group_r_disk_volume.disk_group_id%type,io_cursor OUT t_cursor) IS   v_cursor t_cursor;   BEGIN     OPEN v_cursor FOR Select disk_volume_id from disk_group_r_disk_volume where disk_group_id=volgroup;     io_cursor := v_cursor;   END GetDocGroupRDocVolByDgId; PROCEDURE UpdFilesByVolId(Archs IN disk_volume.Disk_vol_files%type,DiskVolId IN disk_volume.disk_vol_id%type) IS  BEGIN     Update disk_volume set Disk_vol_files=Archs where disk_vol_id=DiskVolId;   END UpdFilesByVolId;end zsp_volume_100;"
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strcreate.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try

        End If
        Return bResultado
    End Function

#End Region

End Class
