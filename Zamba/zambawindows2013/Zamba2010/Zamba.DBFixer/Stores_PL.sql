CREATE OR REPLACE PACKAGE  "ACTIONS_PKG" AS
 PROCEDURE Save_Action(AID IN USER_HST.ACTION_ID%TYPE ,
 AUSRID IN USER_HST.USER_ID%TYPE ,
 ADATE IN USER_HST.ACTION_DATE%TYPE ,
 AOBJID IN USER_HST.USER_ID%TYPE ,
 AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE ,
 ATYPE IN USER_HST.ACTION_TYPE%TYPE,
 ACONID IN UCM.CON_ID%TYPE,
 SOBJECTID IN USER_HST.S_OBJECT_ID%TYPE);
END Actions_Pkg;

CREATE OR REPLACE PACKAGE  "ACTUALIZA_ESTREG_PKG" AS
 Procedure Actualiza_Estreg (idd in Estreg.ID%Type, PCName In Estreg.M_NAME%Type);
End Actualiza_Estreg_PKG;

CREATE OR REPLACE PACKAGE  "ADDRESTRICTIONRIGHTS_PKG" AS
 PROCEDURE AddRestrictionRights(userId in doc_restriction_r_user.User_ID%Type,
 restrictionId in doc_restriction_r_user.restriction_Id%Type);
END AddRestrictionRights_pkg;

CREATE OR REPLACE PACKAGE  "BORRARHISTORIAL_PKG" AS 
 PROCEDURE BORRARHISTORIAL(HistoryId IN number); END 
 BORRARHISTORIAL_PKG;
CREATE OR REPLACE PACKAGE  "BORRARINDEX_PKG" AS 
 PROCEDURE BORRARINDEX(IP IN number); END BORRARINDEX_PKG;
CREATE OR REPLACE PACKAGE  "BORRARIPTYPE_PKG" AS
 PROCEDURE BorrarIPType(IP IN IP_TYPE.IP_ID%TYPE);
END Borrariptype_Pkg;

CREATE OR REPLACE PACKAGE  "CALLVW_ASIGNEDDOCSBYUSER_PKG"
 AS 
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE CALLVW_ASIGNEDDOCSBYUSER(io_cursor OUT t_cursor); 
 END CALLVW_ASIGNEDDOCSBYUSER_PKG;
CREATE OR REPLACE PACKAGE  "CALL_VIEWGETEXPIREDDOCS_PKG" 
 AS 
 TYPE t_cursor IS REF CURSOR;
PROCEDURE CALL_VIEWGETEXPIREDDOCS(io_cursor OUT t_cursor); 
END CALL_VIEWGETEXPIREDDOCS_PKG;
CREATE OR REPLACE PACKAGE  "CALL_VIEW_GETEXPIREDDOCS_PKG"
 AS 
TYPE t_cursor IS REF CURSOR;
PROCEDURE CALL_VIEW_GETEXPIREDDOCS(io_cursor OUT t_cursor); 
END CALL_VIEW_GETEXPIREDDOCS_PKG;
CREATE OR REPLACE PACKAGE  "CHECK_BY_TIME_PKG" AS
 PROCEDURE Check_by_time(conid IN UCM.CON_ID%TYPE,salida OUT BOOLEAN);
END Check_By_Time_Pkg;

CREATE OR REPLACE PACKAGE  "CLEARIPINDEX_PKG" AS
 PROCEDURE ClearTInd(IP IN IP_INDEX.IP_ID%TYPE);
END ClearIPIndex_pkg;

CREATE OR REPLACE PACKAGE 
  "CLSACTIONS_GETUSERACTIONS_PKG" 
 AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE getUserActions (UserId IN NUMBER, io_cursor OUT t_cursor);
END Clsactions_Getuseractions_Pkg;
CREATE OR REPLACE PACKAGE  "CLSDOCGROUP_LOADDOCTYPES_PKG"
 AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE LoadDocTypes (DocGroupId IN NUMBER, io_cursor OUT t_cursor);
END CLSDOCGROUP_LOADDOCTYPES_PKG;

CREATE OR REPLACE PACKAGE  "CLSDOCTYPE_GETDOCTYPES_PKG" 
 AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE getDocTypes (CurrentUserId IN NUMBER, io_cursor OUT t_cursor);
END CLSDOCTYPE_GETDOCTYPES_PKG;

CREATE OR REPLACE PACKAGE  "CLSDOC_GENERACIONINDICES_PKG"
 AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE generacionIndices (DocTypeId IN NUMBER, io_cursor OUT t_cursor);
END CLSDOC_GENERACIONINDICES_PKG;

CREATE OR REPLACE PACKAGE 
  "CLSVOLUME_RETRIEVEVOLUMEID_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE retrieveVolumeId (DocTypeId IN NUMBER, io_cursor OUT t_cursor);
END CLSVOLUME_RETRIEVEVOLUMEID_PKG;

CREATE OR REPLACE PACKAGE  "CONTARTABLAS_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 Procedure ContarTablas(io_cursor OUT t_cursor);
End ContarTablas_PKG;

CREATE OR REPLACE PACKAGE  "COPY_DOC_TYPE_PKG" AS
 PROCEDURE Copy_Doc_Type(DocTypeId NUMERIC,NewdocTypeId NUMERIC,NewName VARCHAR2);
END Copy_Doc_Type_Pkg;

CREATE OR REPLACE PACKAGE  "COUNTID_UCM_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE count_id (io_cursor OUT t_cursor);
END COUNTID_UCM_PKG;

CREATE OR REPLACE PACKAGE  "COUNT_NEW_MESSAGES_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE CountNewMessages(userId in numeric,io_cursor OUT t_cursor);
END Count_New_Messages_Pkg;

CREATE OR REPLACE PACKAGE  "DBCONFIG_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE getObject(obj_type IN VARCHAR2 , io_cursor OUT t_cursor);
END DBCONFIG_PKG;

CREATE OR REPLACE PACKAGE  "DELETE_BY_CONID_PKG" AS

PROCEDURE Delete_by_conid(conid IN UCM.CON_ID%TYPE );

END Delete_By_conid_Pkg;

CREATE OR REPLACE PACKAGE  "DELETE_BY_TIME_PKG" AS
 PROCEDURE Delete_by_time;
END Delete_By_Time_Pkg;

CREATE OR REPLACE PACKAGE  "DELETE_MSG_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE deleteMsgSender(m_id IN MESSAGE.msg_id%TYPE);
END;

CREATE OR REPLACE PACKAGE  "DELEXCEPTABLE_PKG" AS

PROCEDURE DelExcepTable;

END DelExcepTable_Pkg;

CREATE OR REPLACE PACKAGE  "DELIPINDEX_PKG" AS
 PROCEDURE Borrarindex(IP IN IP_INDEX.IP_ID%TYPE);
END delIPindex_pkg;

CREATE OR REPLACE PACKAGE  "DELIPTYPE_PKG" AS
 PROCEDURE BorrarType(IP IN IP_TYPE.IP_ID%TYPE);
END delIPtype_pkg;

CREATE OR REPLACE PACKAGE  "DEL_LCK_BYTIME_PKG" AS
 PROCEDURE Del_LCK_Bytime;
END Del_LCK_Bytime_Pkg;

CREATE OR REPLACE PACKAGE  "DEL_LCK_PKG" AS
 PROCEDURE Del_LCK(docid IN LCK.Doc_ID%TYPE,
 userid IN LCK.USER_ID%TYPE,
 Estid IN LCK.EST_ID%TYPE);
END Del_Lck_Pkg;

CREATE OR REPLACE PACKAGE  "DEL_MSG_REV_PKG" AS
 PROCEDURE deleteMSGRecived(m_id IN MESSAGE.msg_id%TYPE, u_id IN
MSG_DEST.user_id%TYPE);
 END;

CREATE OR REPLACE PACKAGE  "DINAMICSEARCH" as
 TYPE t_Refcur is REF Cursor;
 Procedure indexSearch(strsql in Varchar2, io_cursor OUT t_refcur);
End DinamicSearch;
CREATE OR REPLACE PACKAGE  "FA_GETARCHIVOSBLOQUEADOS_PKG"
 AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE getArchivosBloqueados (io_cursor OUT t_cursor);
END FA_GETARCHIVOSBLOQUEADOS_PKG;

CREATE OR REPLACE PACKAGE  "FRMDOCTYPE_LOADINDEX_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE loadIndex (DocTypeId IN NUMBER, io_cursor OUT t_cursor);
END FRMDOCTYPE_LOADINDEX_PKG;

CREATE OR REPLACE PACKAGE  "FRMIMPORT_FILLINDEX_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE fillIndex (IPJOBDocTypeId IN NUMBER, io_cursor OUT t_cursor);
END FRMIMPORT_FILLINDEX_PKG;

CREATE OR REPLACE PACKAGE  "GETADDRESSBOOK_PKG" As
 TYPE t_cursor IS REF CURSOR;
 Procedure GetAddressBook (userID IN USER_TABLE.USER_ID%type, io_cursor OUT t_cursor);
End GetAddressBook_PKG;

CREATE OR REPLACE PACKAGE  "GETANDSETLASTID2_PKG" AS
PROCEDURE GetandSetLastId2(varOBJTYPE IN OBJLASTID.OBJECT_TYPE_ID%TYPE, varID OUT OBJLASTID.OBJECTID%TYPE);
END GETANDSETLASTID2_PKG;
CREATE OR REPLACE PACKAGE  "GETANDSET_LASTID_PKG" AS
 TYPE t_cursor IS REF CURSOR;

 PROCEDURE GetandSetLastId (OBJTYPE IN OBJLASTID.OBJECT_TYPE_ID%TYPE, io_cursor OUT t_cursor);
END Getandset_Lastid_Pkg
;
CREATE OR REPLACE PACKAGE  "GETDOCTYPERIGHTS_PKG" As
 TYPE t_cursor IS REF CURSOR;
 Procedure GetDocTypeRights (userID IN number, io_cursor OUT t_cursor);
End GetDocTypeRights_PKG;

CREATE OR REPLACE PACKAGE  "GETDOCUMENTACTIONS_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE getDocumentActions (DocumentId IN NUMBER, io_cursor OUT t_cursor);
END Getdocumentactions_Pkg;

CREATE OR REPLACE PACKAGE  "GETMYMESSAGESNEW_PKG" AS 
 type t_cursor is ref cursor;PROCEDURE 
 zsp_GetMyMessagesNew(my_id IN MSG_DEST.user_id%type,io_cursor
 OUT t_cursor);END GetMyMessagesNew_Pkg;
CREATE OR REPLACE PACKAGE  "GETPROCESS_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE GetProcess(I IN IP_TYPE.IP_ID%TYPE,io_cursor OUT t_cursor);
END Getprocess_Pkg;

CREATE OR REPLACE PACKAGE  "GETRESTRICTIONS_PKG" as
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE GetRestrictions(UserID IN User_Table.User_ID%TYPE,io_cursor OUT t_cursor);
END GetRestrictions_Pkg;

CREATE OR REPLACE PACKAGE  "GET_DOCTYPESID_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 Procedure Get_DocTypesID(io_cursor OUT t_cursor);
End Get_DocTypesID_Pkg;

CREATE OR REPLACE PACKAGE  "GET_DOCTYPES_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE Get_Doctypes(io_cursor OUT t_cursor);
END Get_Doctypes_Pkg;

CREATE OR REPLACE PACKAGE  "GET_MY_MESSAGES_PKG" AS
TYPE t_cursor IS REF CURSOR;
PROCEDURE getMymessages(my_id IN USRTABLE.id%TYPE ,io_cursor
OUT t_cursor);
END;
CREATE OR REPLACE PACKAGE  "GET_MY_MSG_ATTACHS" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE getMymessagesAttach(my_id IN USER_TABLE.user_id%TYPE,io_cursor OUT t_cursor);
END;

CREATE OR REPLACE PACKAGE  "GLOBALPKG" AS TYPE RCT1 IS 
 REF CURSOR; TRANCOUNT INTEGER := 0; IDENTITY INTEGER; END;
CREATE OR REPLACE PACKAGE  "IMPORTJOB_PKG" AS
 PROCEDURE Import_JobT2(IP_ID IN IP_INDEX.IP_ID%TYPE,
 Array_ID IN IP_INDEX.ARRAY_ID%TYPE,
 Index_ID IN IP_INDEX.INDEX_ID%TYPE,
 Index_Order IN IP_INDEX.INDEX_ORDER%TYPE);
END ImportJob_pkg;

CREATE OR REPLACE PACKAGE  "INCREMENTARDOCTYPE_PKG" AS
 PROCEDURE IncrementarDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,X IN Number);
END IncrementarDocType_Pkg;

CREATE OR REPLACE PACKAGE  "INDEXRDOCTYPE_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE IndexRDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,io_cursor OUT t_cursor);
END IndexRDocType_Pkg;

CREATE OR REPLACE PACKAGE  "INSERTLIC_PKG" as
 PROCEDURE INSERTLIC(X varchar);
END Insertlic_PKG;

CREATE OR REPLACE PACKAGE  "INSERTUPDATESUSTITUCION_PKG" 
 As
 PROCEDURE InsertUpdateSustitucion (Id IN number, Detalle IN varchar2, Tabla In Varchar2);
End InsertUpdateSustitucion_PKG;

CREATE OR REPLACE PACKAGE  "INSERT_ATTACH_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE InsertMSGAttach(m_id IN MSG_ATTACH.msg_id%TYPE,
 m_DOCid IN MSG_ATTACH.doc_id%TYPE,
 m_DOC_TYPE_ID IN MSG_ATTACH.doc_type_id%TYPE,
 fold_id IN MSG_ATTACH.folder_id%TYPE,
 inde_id IN MSG_ATTACH.index_id%TYPE,
 m_name IN MSG_ATTACH.name%TYPE,
 m_icon IN MSG_ATTACH.icon%TYPE,
 m_volumelistid IN MSG_ATTACH.volumelistid%TYPE,
 m_doc_file IN MSG_ATTACH.doc_file%type,
 m_offset IN MSG_ATTACH.offset%type,
 m_disk_vol_path IN MSG_ATTACH.disk_vol_path%type);
END INSERT_attach_PKG;

CREATE OR REPLACE PACKAGE  "INSERT_ESTREG_PKG" AS
 Procedure Insert_Estreg (idd in Estreg.ID%Type, PCName In Estreg.M_NAME%Type,
 WinName In Estreg.W_USER%Type, VERSION IN Estreg.VER%TYPE,
 Actualizado IN ESTREG.UPDATED%Type);
End Insert_estreg_PKG;

CREATE OR REPLACE PACKAGE  "INSERT_INTO_IP_FOLDER_PKG" 
 AS
 PROCEDURE Ins_Into_IPFolder(RowNombre IN IP_FOLDER.Nombre%TYPE,
 RowPath IN IP_FOLDER.PATH%TYPE,
 RowTimer IN IP_FOLDER.TIMER%TYPE,
 i IN IP_FOLDER.SERVICE %TYPE,
 RowUserId IN IP_FOLDER.User_ID%TYPE,
 PcName IN IP_FOLDER.NOMBREMAQUINA%TYPE);
END Insert_Into_Ip_Folder_Pkg;

CREATE OR REPLACE PACKAGE  "INSERT_MSG_DESTINO_PKG" AS
 PROCEDURE InsertMSGDest(m_id IN MSG_DEST.msg_id%TYPE,
 m_userid IN MSG_DEST.USER_ID%TYPE,
 m_Dest_TYPE IN MSG_DEST.DEST_TYPE%TYPE,
 m_user_name IN MSG_DEST.user_name%TYPE);
END Insert_Msg_Destino_Pkg;

CREATE OR REPLACE PACKAGE  "INSERT_MSG_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE InsertMSG(m_id IN MESSAGE.msg_id%TYPE,
 m_from IN MESSAGE.msg_from%TYPE,
 m_Body IN MESSAGE.msg_body%TYPE,
 m_subject IN MESSAGE.subject%TYPE,
 m_resend IN MESSAGE.reenvio%TYPE);
END Insert_MSG_pkg;

CREATE OR REPLACE PACKAGE  "INSERT_PROCESS_HST_PKG" as
procedure InsertProcHst (HID in p_hst.id%type,
 PID in p_hst.Process_id%type,
 PDATE in p_hst.Process_Date%type,
 USrid in p_hst.user_id%type,
 totfiles in p_hst.Totalfiles%type,
 procfiles in p_hst.ProcessedFiles%type,
 skpfiles in p_hst.SkipedFiles%type,
 ErrFiles in p_hst.ErrorFiles%type,
 RID in p_hst.Result_id%type,
 Pth in p_hst.Path%type,
 hsh in p_hst.hash%type,
 tfile in p_hst.tempfile%type,
 efile in p_hst.errorfile%type,
 lfile in p_hst.logfile%type);
end;

CREATE OR REPLACE PACKAGE  "INSERT_USER159_PKG" AS
 PROCEDURE Ins_New_Connection(v_userId IN UCM.USER_ID%TYPE,
 v_win_User IN UCM.WINUSER%TYPE,
 v_win_Pc IN UCM.WINPC%TYPE,
 v_con_Id IN UCM.CON_ID%TYPE,
 v_timeout IN UCM.TIME_OUT%TYPE,
 WF IN UCM."TYPE"%Type);
END Insert_User159_Pkg;
CREATE OR REPLACE PACKAGE  "INSERT_USER_PKG" AS
	PROCEDURE Ins_New_Connection(v_userId IN UCM.USER_ID%TYPE,
			 	 v_win_User 	IN UCM.WINUSER%TYPE,
								 v_win_Pc 	IN UCM.WINPC%TYPE,
								 v_con_Id 	IN UCM.CON_ID%TYPE,
								 v_timeout	IN UCM.TIME_OUT%TYPE);
END Insert_User_Pkg;
CREATE OR REPLACE PACKAGE  "INSERT_VERREG_PKG" AS
PROCEDURE Insert_Verreg(
 IDD IN VERREG.ID%TYPE ,
 Version IN VERREG.VER%TYPE ,
 Path IN VERREG.PATH %TYPE);
END Insert_Verreg_Pkg;

CREATE OR REPLACE PACKAGE  "INSERT_ZBARCODE_PKG" AS
 PROCEDURE Insert_ZBarCode(idbarcode IN ZBarCode.ID%TYPE,
 DocTypeId IN ZBarCode.doc_type_id%TYPE,
 UserId in ZBarCode.Userid%type,
 Doc_id in ZBarCode.Doc_id%type);
END Insert_ZBarCode_PKG;
CREATE OR REPLACE PACKAGE  "INS_LCK_PKG" AS
 PROCEDURE Ins_LCK(
 docid IN LCK.Doc_ID%TYPE ,
 Userid IN LCK.USER_ID%TYPE ,
 Estid IN LCK.Est_Id%TYPE );
END Ins_Lck_Pkg;

CREATE OR REPLACE PACKAGE  "IPTYPEPKG" AS
 PROCEDURE ins_iptypeT1(IPP_ID IN IP_TYPE.IP_ID%TYPE,
 IPP_NAME IN IP_TYPE.IP_NAME%TYPE,
 IPP_PATH IN IP_TYPE.IP_PATH%TYPE,
 IPP_CHR IN IP_TYPE.IP_CHR%TYPE,
 IPP_DOCTYPEID IN IP_TYPE.IP_DOCTYPEID%TYPE,
 IPP_MOVE IN IP_TYPE.IP_MOVE%TYPE,
 IPP_VERIFY IN IP_TYPE.IP_VERIFY%TYPE,
 IPP_ACEPTBLANK IN IP_TYPE.IP_ACEPTBLANK%TYPE,
 IPP_BACKUP IN IP_TYPE.IP_BACKUP%TYPE,
 IPP_DELSOURCE IN IP_TYPE.IP_DELSOURCE%TYPE,
 IPP_SOURCEVARIABLE IN IP_TYPE.IP_SOURCEVARIABLE%TYPE,
 IPP_MULTIPLEFILES IN IP_TYPE.IP_MULTIPLEFILES%TYPE,
 IPP_MULTIPLECHR IN IP_TYPE.IP_MULTIPLECHR%TYPE);
END iptypepkg;

CREATE OR REPLACE PACKAGE  "IP_HSTDELETE" AS
 PROCEDURE BorrarHistorial(HistoryId IN P_HST.ID%TYPE);
END Ip_Hstdelete;
CREATE OR REPLACE PACKAGE  "IP_HST_PKG" AS
 PROCEDURE IP_HST1(HST_ID IN IP_HST.HST_ID%TYPE,
 IP_ID IN IP_HST.IP_ID%TYPE,
 IPDate IN IP_HST.IPDATE%TYPE,
 IPUserId IN IP_HST.IPUSERID%TYPE,
 IPDocCount IN IP_HST.IPDOCCOUNT%TYPE,
 IPIndexCount IN IP_HST.IPINDEXCOUNT%TYPE,
 IPResult IN IP_HST.IPRESULT%TYPE,
 IPLINESCOUNT IN IP_HST.IPLINESCOUNT%TYPE,
 IPERRORCOUNT IN IP_HST.IPERRORCOUNT%TYPE,
 IPPATH IN IP_HST.IPPATH%TYPE,
 IPHASH IN IP_HST.IP_HASH%Type);
END IP_HST_pkg;

CREATE OR REPLACE PACKAGE  "IP_PROCESSTASK_PKG" AS
 PROCEDURE IP_PROCTASK(vdia IN IP_PROCESSTASK.DIA%TYPE,
 vhora IN IP_PROCESSTASK.HORA%TYPE,
 IDprocess IN IP_PROCESSTASK.ID_PROCESS%TYPE);

END Ip_Processtask_Pkg;

CREATE OR REPLACE PACKAGE 
  "LL_WFVIEWASIGNEDDOCSBYUSER_PKG" AS PROCEDURE 
 CALL_WFVIEWASIGNEDDOCSBYUSER; END 
 LL_WFVIEWASIGNEDDOCSBYUSER_PKG;
CREATE OR REPLACE PACKAGE 
  "MODVOLUME_RETRIEVEVOLUMEID_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE retrieveVolumeId (DocTypeId IN NUMBER, io_cursor OUT t_cursor);
END MODVOLUME_RETRIEVEVOLUMEID_PKG;

CREATE OR REPLACE PACKAGE  "MOD_RETRIEVEVOLUMEPATH_PKG" 
 AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE retrieveVolumePath (DocTypeId IN NUMBER, io_cursor OUT t_cursor);
END MOD_RETRIEVEVOLUMEPATH_PKG;

CREATE OR REPLACE PACKAGE  "RETRIEVEVOLUMEPATH_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE retrieveVolumePath (DocTypeId IN NUMBER, io_cursor OUT t_cursor);
END RETRIEVEVOLUMEPATH_PKG;

CREATE OR REPLACE PACKAGE  "SEARCH_FILLMYTREEVIEW_PKG" 
 AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE fillMyTreeView (UserId IN NUMBER, io_cursor OUT t_cursor);
END SEARCH_FILLMYTREEVIEW_PKG;

CREATE OR REPLACE PACKAGE  "SEARCH_GENERACIONINDICES_PKG"
 AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE generacionIndices (UserId IN NUMBER, io_cursor OUT t_cursor);
END SEARCH_GENERACIONINDICES_PKG;

CREATE OR REPLACE PACKAGE 
  "SEARCH_TREEVIEWAFTERSELECT_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE treeViewAfterSelect (GlobalUserId IN NUMBER, DocGroupId IN NUMBER, io_cursor OUT t_cursor);
END SEARCH_TREEVIEWAFTERSELECT_PKG;

CREATE OR REPLACE PACKAGE  "SELALL_LCK_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE Selall_LCK( io_cursor OUT t_cursor);
END Selall_Lck_Pkg;

CREATE OR REPLACE PACKAGE  "SELECCIONAR_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE seleccionar(I IN IP_INDEX.IP_ID%TYPE,io_cursor OUT t_cursor);
END Seleccionar_Pkg;

CREATE OR REPLACE PACKAGE  "SELECTALLINDEX_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE Selectallindex(I IN IP_INDEX.IP_ID%TYPE, io_cursor OUT t_cursor);
END Selectallindex_Pkg;

CREATE OR REPLACE PACKAGE  "SELECTIPHST_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE Selectiphst(I IN IP_HST.HST_ID%TYPE,io_cursor OUT t_cursor);
END Selectiphst_Pkg;

CREATE OR REPLACE PACKAGE  "SELECTIPID_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE Selectipid(io_cursor OUT t_cursor);
END Selectipid_Pkg;

CREATE OR REPLACE PACKAGE  "SELECTLAST_VERREG_PKG" AS
TYPE t_cursor IS REF CURSOR;
PROCEDURE SelectLast_Verreg(idd IN VERREG.ID%TYPE,io_cursor OUT t_cursor);
END SelectLast_Verreg_Pkg;

CREATE OR REPLACE PACKAGE  "SELECTLIC_PKG" AS /* No se
 usa */
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE SelectLic(io_cursor OUT t_cursor);
END SelectLic_PKG;

CREATE OR REPLACE PACKAGE  "SELECT_ESTREG_PKG" AS
TYPE t_cursor IS REF CURSOR;
PROCEDURE Select_Estreg(idd IN ESTREG.ID%TYPE,io_cursor OUT t_cursor);
END Select_Estreg_Pkg;

CREATE OR REPLACE PACKAGE  "SELECT_VERREG_PKG" AS
TYPE t_cursor IS REF CURSOR;
PROCEDURE Select_Verreg(io_cursor OUT t_cursor);
END Select_Verreg_Pkg;

CREATE OR REPLACE PACKAGE  "SEL_LCK_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE Sel_LCK(docid IN LCK.DOC_ID%TYPE,io_cursor OUT t_cursor);
END Sel_Lck_Pkg;

CREATE OR REPLACE PACKAGE  "SEL_NAME_LCK_PKG" AS
TYPE t_cursor IS REF CURSOR;
PROCEDURE Sel_Name_LCK(docid IN LCK.Doc_ID%TYPE,io_cursor OUT t_cursor);
END Sel_Name_Lck_Pkg;

CREATE OR REPLACE PACKAGE  "SETDOC_I58INBROKER_PKG" AS
 PROCEDURE SETDOC_I58INBROKER(NROSIN IN DOC_I58.I22%TYPE);
END SETDOC_I58INBROKER_PKG;

CREATE OR REPLACE PACKAGE  "SETORD_INBROKER_PKG" AS
 PROCEDURE SETORD_INBROKER (NROORDEN IN NUMERIC);
END SETORD_INBROKER_PKG;

CREATE OR REPLACE PACKAGE  "SETPOL_INBROKER_PKG" AS
TYPE t_cursor IS REF CURSOR;
 PROCEDURE SETPOL_INBROKER (IDOP IN DOC_I57.I13%TYPE);
END SETPOL_INBROKER_PKG;
CREATE OR REPLACE PACKAGE  "SETRIGHTS_PKG" AS
 PROCEDURE SetRights(UserID IN User_Rights.User_ID%TYPE,
 ObjectID In User_Rights.Object_ID%Type,
 Userrightstype In User_Rights.User_Rights_Type_Id%Type,
 ObjectType in User_Rights.Object_Type_Id%Type,
 RightValue IN User_Rights.Right_Value%Type);
END SetRights_Pkg;

CREATE OR REPLACE PACKAGE  "SETSIN_INBROKER_PKG" AS
 PROCEDURE SETSIN_INBROKER(NROSINCOMPLETE IN DOC_I58.I22%TYPE, NROSINORIGINAL IN DOC_I58.I22%TYPE, NROPOL IN DOC_I58.I18%TYPE);
END SETSIN_INBROKER_PKG;
CREATE OR REPLACE PACKAGE  "SHOWPROCESSHISTORY159_PKG" 
 AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE showProcessHistory(PROCESSID IN NUMBER, IO_CURSOR OUT T_CURSOR);
END;
CREATE OR REPLACE PACKAGE  "SHOWPROCESSHISTORY_PKG" AS
TYPE t_cursor IS REF CURSOR;
PROCEDURE showProcessHistory (ProcessID IN NUMBER, io_cursor OUT t_cursor);
END;

CREATE OR REPLACE PACKAGE  "SHOWPROCESSHISTORY_PKG159" 
 AS

TYPE t_cursor IS REF CURSOR;

PROCEDURE showProcessHistory159 (ClsIpJob1ID IN NUMBER, io_cursor OUT
t_cursor);

END;

CREATE OR REPLACE PACKAGE  "UCM_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE getConID ( conid OUT INT);
END UCM_Pkg;

CREATE OR REPLACE PACKAGE  "UPDATEDATA_PKG" AS
 PROCEDURE UpdateData (VolumeId IN NUMBER, FileSize IN DECIMAL);
END UpdateData_Pkg;

CREATE OR REPLACE PACKAGE  "UPDATEVOLDELFILE_PKG" AS
 PROCEDURE UPDATEVOLDELFILE (VolumeId IN NUMBER, FileSize IN DECIMAL);
END UpdateVoldelFile_Pkg;

CREATE OR REPLACE PACKAGE  "UPDATE_DISKVOLSTATE_PKG" AS
 PROCEDURE Update_diskvolstate(Volid IN DISK_VOLUME.DISK_VOL_ID%TYPE);
END Update_diskvolstate_Pkg;

CREATE OR REPLACE PACKAGE  "UPDATE_DISKVOLUME_PKG" AS
PROCEDURE Update_diskvolume(VSIZE IN DISK_VOLUME.DISK_VOL_SIZE%TYPE,
 Actualfiles IN DISK_VOLUME.DISK_VOL_FILES%TYPE,
 volumeid IN DISK_VOLUME.DISK_VOL_ID%TYPE);
END Update_diskvolume_Pkg;

CREATE OR REPLACE PACKAGE  "UPDATE_DISKVOLUSED_PKG" AS
 PROCEDURE Update_diskvolused(VolId IN DISK_VOLUME.disk_vol_ID%TYPE,
 Lastoffsetused IN DISK_VOLUME.disk_vol_lstoffset%TYPE);

END Update_diskvolused_Pkg;

CREATE OR REPLACE PACKAGE  "UPDATE_DOCNOTES_PKG" AS
 PROCEDURE Update_docnotes(nota IN DOC_NOTES.Note_Text%TYPE,
 posX IN DOC_NOTES.x_position%TYPE,
 posY IN DOC_NOTES.Y_position%TYPE,
 notaid IN DOC_NOTES.Note_ID%TYPE);

END Update_docnotes_Pkg;

CREATE OR REPLACE PACKAGE  "UPDATE_IPFOLDERBACK_PKG" AS
 PROCEDURE Update_ipfolderback(vcarpeta IN IP_FOLDERBACKUP.carpeta_Backup%TYPE,
 v_antes IN IP_FOLDERBACKUP.antes%TYPE,
 idcarpeta IN IP_FOLDERBACKUP.Id_carpeta%TYPE);

END Update_ipfolderback_Pkg;

CREATE OR REPLACE PACKAGE  "UPDATE_IPFOLDER_PKG" AS
 PROCEDURE Update_ipfolder(vnom IN IP_FOLDER.nombre%TYPE,
 vpath IN IP_FOLDER.path%TYPE,
 cod IN IP_FOLDER.ID%TYPE);

END Update_ipfolder_Pkg;

CREATE OR REPLACE PACKAGE  "UPDATE_LASTOBJ_PKG" AS
 PROCEDURE Update_lastobj(idobjeto IN OBJLASTID.OBJECTID%TYPE,
 tipo IN OBJLASTID.OBJECT_TYPE_ID%TYPE);

END Update_lastobj_Pkg;

CREATE OR REPLACE PACKAGE  "UPDATE_PROCESS_HST_PKG" as

procedure updateProcHst ( HID in p_hst.id%type,
 totfiles in p_hst.Totalfiles%type,
 procfiles in p_hst.ProcessedFiles%type,
 skpfiles in p_hst.SkipedFiles%type,
 ErrFiles in p_hst.ErrorFiles%type,
 RID in p_hst.Result_id%type,
 hsh in p_hst.hash%type);
end;

CREATE OR REPLACE PACKAGE  "UPDATE_SCANNEDBARCODE_PKG" 
 AS
 PROCEDURE Update_barcode(caratulaid IN zbarcode.id%TYPE,
 lote IN zbarcode.batch%TYPE,
 caja IN zbarcode.box%TYPE);
END UPDATE_SCANNEDBARCODE_PKG;
CREATE OR REPLACE PACKAGE  "UPDATE_USERRIGHTS_PKG" AS
PROCEDURE Update_User_Right(rightv IN USER_RIGHTS.RIGHT_VALUE%TYPE,
 rightid IN USER_RIGHTS.RIGHT_ID%TYPE);
END UPDATE_UserRights_pkg;

CREATE OR REPLACE PACKAGE  "WFVIEWASIGNEDDOCSBYUSER_PKG" 
 AS PROCEDURE CALL_WFVIEWASIGNEDDOCSBYUSER; END 
 WFVIEWASIGNEDDOCSBYUSER_PKG;
CREATE OR REPLACE PACKAGE  "ZDOCINDGET_PKG" as 
 TYPE t_Refcur is REF Cursor; 
 Procedure ZDIndGetDdownByInd(indexid in doc_Index.index_id%type, io_cursor OUT t_refcur); 
 Procedure ZDIndGetCantByNameId(IndexName in doc_Index.index_name%type, IndexId in doc_Index.index_id%type, io_cursor OUT t_refcur); 
 End ZDocIndGet_pkg;
CREATE OR REPLACE PACKAGE  "ZDTINS_PKG" AS 
PROCEDURE ZDtInsDtAssociated(DoctypeId1 in Doctypes_associated.DoctypeId1%type, Index1 in Doctypes_associated.Index1%type, doctypeid2 in Doctypes_associated.doctypeid2%type, index2 in Doctypes_associated.index2%type); 
END ZDtIns_Pkg;
CREATE OR REPLACE PACKAGE  "ZEXECSQL_PKG" as 
TYPE t_Refcur is REF Cursor; 
Procedure ZExecSql(strsql in Varchar2, io_cursor OUT t_refcur); 
End ZExecSql_pkg;
CREATE OR REPLACE PACKAGE  "ZGETINTEGRIDADINDICES_PKG" 
 as 
 TYPE t_cursor IS REF CURSOR; 
 procedure ZGetColumnsDoc_D(io_cursor out t_cursor); 
 procedure ZGetColumnsDoc_I (io_cursor out t_cursor); 
 procedure ZGetAllDrI (io_cursor out t_cursor); 
 end ZGetIntegridadIndices_pkg;
CREATE OR REPLACE PACKAGE  "ZGETUSERRIGTH_PKG" as 
 type t_cursor is ref cursor; 
 Procedure GetArchivosUserRight(UserId in usr_rights_view.user_id%type, io_cursor out t_cursor); 
 end ZGetUserRigth_pkg;
CREATE OR REPLACE PACKAGE  "ZINDLNKINF_PKG" AS 
 PROCEDURE ZIndLnkInfInsRow(pId IN index_link_info.Id%type ,pData IN index_link_info.Data%type, pFlag IN index_link_info.Flag%type, pDocType IN index_link_info.DocType%type ,pDocIndex IN index_link_info.DocIndex%type, pName IN index_link_info.Name%type); 
 END ZIndLnkInf_Pkg;
CREATE OR REPLACE PACKAGE  "ZINDRDTUPD_PKG" as 
 Procedure ZIndRDtUpdByDtIDIndID(DocTypeId in Index_R_Doc_Type.Doc_Type_ID%type, IndexId in Index_R_Doc_Type.Index_Id%type); 
 Procedure ZIndRDtUpdByDtIDIndID2(DocTypeId in Index_R_Doc_Type.Doc_Type_ID%type, IndexId in Index_R_Doc_Type.Index_Id%type); 
 End ZIndRDtUpd_pkg;
CREATE OR REPLACE PACKAGE  "ZSP_BARCODE_100" as 
 PROCEDURE InsertBarCode(idbarcode IN ZBarCode.ID%TYPE, 
 DocTypeId IN ZBarCode.doc_type_id%TYPE, UserId in 
 ZBarCode.Userid%type,Doc_id in ZBarCode.Doc_Id%type); 
 PROCEDURE UpdBarCode(caratulaid IN zbarcode.id%TYPE,lote IN 
 zbarcode.batch%TYPE,caja IN zbarcode.box%TYPE); end;
CREATE OR REPLACE PACKAGE  "ZSP_CONNECTION_100" as 
 TYPE t_cursor IS REF CURSOR; 
 PROCEDURE CountConnections (io_cursor OUT t_cursor);PROCEDURE DeleteConnection(conid IN UCM.CON_ID%TYPE );
 PROCEDURE InsertNewConecction(v_userId IN UCM.USER_ID%TYPE, v_win_User IN UCM.WINUSER%TYPE, v_win_Pc IN UCM.WINPC%TYPE, v_con_Id IN UCM.CON_ID%TYPE, v_timeout IN UCM.TIME_OUT%TYPE,WF IN UCM.Type%Type);
 end ZSP_CONNECTION_100;
CREATE OR REPLACE PACKAGE  "ZSP_CONNECTION_300" as 
 TYPE t_cursor IS REF CURSOR; 
 PROCEDURE DeleteConnection(conid IN UCM.CON_ID%TYPE, winpc IN UCM.WINPC%TYPE);
 end ZSP_CONNECTION_300;
CREATE OR REPLACE PACKAGE  "ZSP_DOCASSOCIATED_100" as
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE GetDocAssociatedById(pDoctypeId IN DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,io_cursor OUT t_cursor);
 procedure GetDocAssociatedId2ById1(DocTypeId IN DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,io_cursor OUT t_cursor);
 PROCEDURE getDocTypesAsociated(DocTypeId numeric, UserId numeric,io_cursor OUT t_cursor);
 end;
CREATE OR REPLACE PACKAGE 
  "ZSP_DOCINDEX_200_LOADINDEX_PKG" AS
 TYPE t_cursor IS REF CURSOR;
 Procedure zsp_docindex_200_LoadIndex(DocTypeId in Index_R_Doc_Type.Doc_Type_Id%type, io_cursor OUT t_cursor);
END zsp_docindex_200_LoadIndex_PKG;
CREATE OR REPLACE PACKAGE  "ZSP_DOCTYPES_100" as TYPE
 t_cursor IS REF CURSOR;
 PROCEDURE CopyDocType(DocTypeId NUMBER,
 NewdocTypeId NUMBER,NewName VARCHAR2);PROCEDURE
 FillMeTreeView (UserId IN NUMBER, io_cursor OUT
 t_cursor);
 Procedure GetAllDocTypesIdNames(io_cursor OUT
 t_cursor);
 PROCEDURE GetDocumentActions (DocumentId IN NUMBER,
 io_cursor OUT t_cursor);
 PROCEDURE IncrementsDocType(DocID IN
 Doc_Type.Doc_Type_ID%TYPE,X IN Number);
 PROCEDURE
 GetDocTypesByGroupId (DocGroupId IN NUMBER, io_cursor OUT
 t_cursor);
 Procedure GetDiskGroupId(DocTypeId IN
 Doc_type.doc_type_Id%type,io_cursor OUT t_cursor);
 Procedure
 GetAssociatedIndex(DocTypeId11 IN
 DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,DocTypeId21 IN
 DOC_TYPE_R_DOC_TYPE.DoctypeId2%type,io_cursor OUT
 t_cursor);
 PROCEDURE UpdDocCountById(DocCount IN
 DOC_TYPE.DOCCOUNT%type,DocTypeId IN
 DOC_TYPE.DOC_TYPE_ID%type);
 PROCEDURE UpdMbById(TamArch IN
 DOC_TYPE.MB%type,DocTypeId IN
 DOC_TYPE.DOC_TYPE_ID%type);
 PROCEDURE
 GetUserNameDocumentAction (DocumentId IN
 user_hst.Object_Id%type, io_cursor OUT t_cursor);
 end zsp_doctypes_100;
CREATE OR REPLACE PACKAGE  "ZSP_DOCTYPES_200" AS 
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE GetDocTypesByUserRights(userid IN usrtable.id%type, righttype IN Number,io_cursor OUT t_cursor);
 PROCEDURE GetDocTypesByUserRightsAndZI(userid IN usrtable.id%type, righttype IN Number, fldrId IN Number,io_cursor OUT t_cursor);
 End;
CREATE OR REPLACE PACKAGE  "ZSP_EXCEPTION_100" as 
 PROCEDURE DeleteExceptionTable;end;
CREATE OR REPLACE PACKAGE  "ZSP_GENERIC_100" as TYPE 
 t_cursor IS REF CURSOR; Procedure ExecSqlString(strsql in 
 Varchar2,io_cursor OUT t_cursor);end zsp_generic_100;
CREATE OR REPLACE PACKAGE  "ZSP_GETDOCCOUNTTOINDEX_PKG" 
 AS
TYPE t_cursor IS REF CURSOR;
PROCEDURE GetDocCountToIndex(io_cursor OUT t_cursor);
END ZSP_GETDOCCOUNTTOINDEX_PKG;
CREATE OR REPLACE PACKAGE 
  "ZSP_GETDOCIDANDDTIDTOINDEX_PKG" AS
TYPE t_cursor IS REF CURSOR;
PROCEDURE GetDocIdAndDTIDtoIndex(io_cursor OUT t_cursor);
END ZSP_GETDOCIDANDDTIDTOINDEX_PKG;
CREATE OR REPLACE PACKAGE  "ZSP_GETMYMESSAGESNEW_PKG" AS
 type t_cursor is ref cursor;PROCEDURE 
 zsp_GetMyMessagesNew(my_id IN MSG_DEST.user_id%type,io_cursor
 OUT t_cursor);END zsp_GetMyMessagesNew_Pkg;
CREATE OR REPLACE PACKAGE  "ZSP_IMPORTS_100" as 
 TYPE t_cursor IS REF CURSOR;
 Procedure DeleteHystory(HistoryId IN P_HST.ID%TYPE);
 Procedure InsertProcHistory (HID in p_hst.id%type,PID in p_hst.Process_id%type, PDATE in p_hst.Process_Date%type,USrid in p_hst.user_id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type,skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type,RID in p_hst.Result_id%type,Pth in p_hst.Path%type,hsh in p_hst.hash%type,tfile in p_hst.tempfile%type,efile in p_hst.errorfile%type, lfile in p_hst.logfile%type);
 Procedure InsertUserAction(AID IN USER_HST.ACTION_ID%TYPE , AUSRID IN USER_HST.USER_ID%TYPE , AOBJID IN USER_HST.USER_ID%TYPE , AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE , ATYPE IN USER_HST.ACTION_TYPE%TYPE,ACONID IN UCM.CON_ID%TYPE, SOBJECTID IN USER_HST.S_OBJECT_ID%TYPE);
 Procedure GetProcessHistory (ClsIpJob1ID IN NUMBER, io_cursor OUT t_cursor);
 Procedure UpdProcHistory(HID in p_hst.id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type, skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type, RID in p_hst.Result_id%type,hsh in p_hst.hash%type);
 
 end zsp_imports_100;
CREATE OR REPLACE PACKAGE  "ZSP_IMPORTS_200" as
 TYPE t_cursor IS REF CURSOR;
 Procedure InsertUserAction(
 ACONID IN UCM.CON_ID%TYPE ,
 WIN_PC UCM.WINPC%TYPE ,
 AUSRID IN USER_HST.USER_ID%TYPE ,
 AOBJID IN USER_HST.OBJECT_ID%TYPE ,
 AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE ,
 ATYPE IN USER_HST.ACTION_TYPE%TYPE,
 SOBJECTID IN USER_HST.S_OBJECT_ID%TYPE,
 io_cursor out t_cursor);

 end;
CREATE OR REPLACE PACKAGE  "ZSP_INDEX_100" as 
 TYPE t_cursor IS REF CURSOR;
 Procedure FillIndex (IPJOBDocTypeId IN NUMBER, io_cursor OUT t_cursor);
 Procedure IndexGeneration (DocTypeId IN NUMBER, io_cursor OUT t_cursor);
 Procedure GetIndexRDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,io_cursor OUT t_cursor);
 Procedure GetDocTypeIndexs (DocTypeId IN NUMBER, io_cursor OUT t_cursor);
 Procedure GetIndexQtyByNameId (IndexName in doc_Index.index_name%type, IndexId in doc_Index.index_id%type, io_cursor OUT t_cursor);
 Procedure GetIndexDropDown(indexid in doc_Index.index_id%type, io_cursor OUT t_cursor);
 Procedure GetAllIndexRDocType (io_cursor out t_cursor);
 Procedure GetDoc_dColumns(io_cursor out t_cursor);
 Procedure GetDoc_iColumns (io_cursor out t_cursor);
 Procedure InsertLinkInfo(pId IN index_link_info.Id%type ,pData IN index_link_info.Data%type, pFlag IN index_link_info.Flag%type, pDocType IN index_link_info.DocType%type,pDocIndex IN index_link_info.DocIndex%type, pName IN index_link_info.Name%type);
 Procedure UpdIndexRDoctypeByDtInd(DocTypeId in Index_R_Doc_Type.Doc_Type_ID%type, IndexId in Index_R_Doc_Type.Index_Id%type);Procedure UpdIndexRDoctypeByDtInd2 (DocTypeId int, IndexId int );
 Procedure GetIndexRightValues(user_id in USR_RIGHTS.groupid%type, io_cursor OUT t_cursor);
 end zsp_index_100;
CREATE OR REPLACE PACKAGE  "ZSP_INDEX_200" as 
 TYPE t_cursor IS REF CURSOR;
 Procedure GetDocTypeIndexs (DocTypeId IN NUMBER, io_cursor OUT t_cursor);
 end zsp_index_200;
CREATE OR REPLACE PACKAGE  "ZSP_LICENSE_100" as TYPE 
 t_cursor IS REF CURSOR;Procedure GetActiveWFConect (io_cursor
 out t_cursor);Procedure GetDocumentalLicenses(io_cursor OUT 
 t_cursor);end zsp_license_100;
CREATE OR REPLACE PACKAGE  "ZSP_LOCK_100" as TYPE 
 t_cursor IS REF CURSOR;Procedure DeleteLocked(docid IN 
 LCK.Doc_ID%TYPE,userid IN LCK.USER_ID%TYPE,Estid IN 
 LCK.EST_ID%TYPE);Procedure GetBlockeds (io_cursor OUT 
 t_cursor);Procedure LockDocument(docid IN LCK.Doc_ID%TYPE ,
 Userid IN LCK.USER_ID%TYPE , Estid IN LCK.Est_Id%TYPE 
 );Procedure GetDocumentLockedState(docid IN LCK.doc_ID%TYPE,
 io_cursor OUT t_cursor);end zsp_lock_100;
CREATE OR REPLACE PACKAGE  "ZSP_MESSAGES_100" as TYPE 
 t_cursor IS REF CURSOR;PROCEDURE CountNewMessages(userId in 
 numeric,io_cursor OUT t_cursor);PROCEDURE 
 DeleteRecivedMsg(m_id IN MESSAGE.msg_id%TYPE, u_id IN 
 MSG_DEST.user_id%TYPE);PROCEDURE DeleteSenderMsg(m_id IN 
 MESSAGE.msg_id%TYPE);PROCEDURE GetMyMessages(my_id IN 
 USRTABLE.id%TYPE ,io_cursor OUT t_cursor);PROCEDURE 
 GetMyMessagesNew(my_id IN USRTABLE.id%TYPE ,io_cursor OUT 
 t_cursor);PROCEDURE GetMyAttachments(my_id IN 
 USRTABLE.id%TYPE ,io_cursor OUT t_cursor);PROCEDURE 
 InsertMsg(m_id IN MESSAGE.msg_id%TYPE,m_from IN 
 MESSAGE.msg_from%TYPE,m_Body IN MESSAGE.msg_body%TYPE,
 m_subject IN MESSAGE.subject%TYPE,m_resend IN 
 MESSAGE.reenvio%TYPE); PROCEDURE InsertAttach(m_id IN 
 msg_attach.msg_id%TYPE, m_DOCid IN msg_attach.doc_id%TYPE, 
 m_DOC_TYPE_ID IN msg_attach.doc_type_id%TYPE, m_folder_id IN 
 msg_attach.folder_id%TYPE, m_index_id IN 
 msg_attach.index_id%TYPE,m_name IN msg_attach.name%TYPE, 
 m_icon IN msg_attach.icon%TYPE, m_volumelistid IN 
 msg_attach.volumelistid%TYPE, m_doc_file IN 
 msg_attach.doc_file%TYPE, m_offset IN msg_attach.offset%TYPE,
 m_disk_vol_path IN msg_attach.disk_vol_path%TYPE);PROCEDURE 
 InsertMsgDest(m_id IN MSG_DEST.msg_id%TYPE, m_userid IN 
 MSG_DEST.USER_ID%TYPE, m_Dest_TYPE IN MSG_DEST.DEST_TYPE%TYPE,
 m_User_name IN MSG_DEST.user_name%TYPE);end zsp_messages_100;
CREATE OR REPLACE PACKAGE  "ZSP_OBJECTS_100" AS 
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE GetAndSetLastId (OBJTYPE IN OBJLASTID.OBJECT_TYPE_ID%TYPE, io_cursor OUT t_cursor);
 --Se podr?a poner ac? el m?todo del paquete GetAndSetLastId2_PKG
 END zsp_objects_100;
CREATE OR REPLACE PACKAGE  "ZSP_PREPROCESS_100" as 
 TYPE t_cursor IS REF CURSOR;
 Procedure GeTPreprocessByUsrID (userid NUMBER, io_cursor OUT t_cursor);
 end Zsp_Preprocess_100;
CREATE OR REPLACE PACKAGE  "ZSP_SCHEDULE_100" as TYPE 
 t_cursor IS REF CURSOR;PROCEDURE UpdLastTaskExecution(Id IN 
 Schedule.TASK_ID%type,Fecha IN Schedule.FECHA%type, io_cursor
 OUT t_cursor);procedure GetTasks(Fecha IN Schedule.FECHA%type,
 io_cursor OUT t_cursor);PROCEDURE DeleteTask(Id IN 
 Schedule.TASK_ID%type);PROCEDURE GetNewTasks (Id IN 
 Schedule.TASK_ID%type, io_cursor OUT t_cursor);end 
 zsp_schedule_100;
CREATE OR REPLACE PACKAGE  "ZSP_SEARCH_100" AS
 PROCEDURE WordDelete (varResultId IN ZSEARCHVALUES_DT.ResultId%TYPE);
 PROCEDURE WordUpdate (varResultId IN ZSEARCHVALUES_DT.ResultId%TYPE, varIndexId IN ZSEARCHVALUES_DT.IndexId%TYPE);
 PROCEDURE WordInsert (varWord IN ZSEARCHVALUES.Word%TYPE, varDTID IN ZSEARCHVALUES_DT.DTID%TYPE, varResultId IN ZSEARCHVALUES_DT.ResultId%TYPE, varIndexId IN ZSEARCHVALUES_DT.IndexId%TYPE);
END zsp_search_100;
CREATE OR REPLACE PACKAGE  "ZSP_SECURITY_100" as type 
 t_cursor is ref cursor; Procedure 
 GetArchivosUserRight(UserId in 
 Zvw_USR_Rights_100.user_id%type, io_cursor out 
 t_cursor);Procedure GetDocTypeRights(userID IN number, 
 io_cursor OUT t_cursor);PROCEDURE 
 GetUserDocumentsResctrictions(UserID IN 
 User_Table.User_ID%TYPE,io_cursor OUT t_cursor);PROCEDURE 
 InsertStation(idd IN ESTREG.ID%TYPE,io_cursor OUT 
 t_cursor);PROCEDURE UpdUserRight(rightv IN 
 USER_RIGHTS.RIGHT_VALUE%TYPE,rightid IN 
 USER_RIGHTS.RIGHT_ID%TYPE);end zsp_security_100;
CREATE OR REPLACE PACKAGE  "ZSP_USERS_100" as 
 TYPE t_cursor IS REF CURSOR; 
 PROCEDURE GetUserAddressBook (userID IN USRTABLE.ID%type, io_cursor OUT t_cursor); 
 PROCEDURE GetUserActions (UserId IN NUMBER, io_cursor OUT t_cursor);
 end;
CREATE OR REPLACE PACKAGE  "ZSP_USERS_200" AS
TYPE t_cursor IS REF CURSOR;
 PROCEDURE GetUserByName(username IN Usrtable.Name%TYPE, io_cursor OUT t_cursor);
END Zsp_users_200;
CREATE OR REPLACE PACKAGE  "ZSP_VERSION_300" as type 
 t_cursor is ref cursor;Procedure 
 GETVERSIONDETAILS(Param_docId in number,io_cursor out 
 t_cursor);Procedure INSERTVERSIONCOMMENT(Par_docId IN number,
 Par_comment in varchar2);Procedure 
 INSERTPUBLISH(Parm_publishid IN number,Parm_docid IN number, 
 Parm_userid IN number,Par_publishdate in date); end 
 ZSP_VERSION_300;
CREATE OR REPLACE PACKAGE  "ZSP_VOLUME_100" as TYPE 
 t_cursor IS REF CURSOR;PROCEDURE UpdFilesAndSize(VolumeId IN
 NUMBER, FileSize IN DECIMAL); PROCEDURE 
 UpdDeletedFiles(VolumeId IN NUMBER, FileSize IN DECIMAL); 
 procedure GetDocGroupRDocVolByDgId(volgroup in 
 disk_group_r_disk_volume.disk_group_id%type,io_cursor OUT 
 t_cursor); PROCEDURE UpdFilesByVolId(Archs IN 
 disk_volume.Disk_vol_files%type,DiskVolId IN 
 disk_volume.disk_vol_id%type);end;
CREATE OR REPLACE  PACKAGE "ZSP_WORKFLOW_100"  as
 TYPE t_cursor IS REF CURSOR;
 PROCEDURE WFRecotizacion(varI46 IN numeric, io_cursor OUT t_cursor); 
 PROCEDURE GetUserWFIdsAndNames(user_id in USR_RIGHTS.groupid%type, io_cursor OUT t_cursor);
 PROCEDURE GetStepsByWFIdAndUserId(workid numeric, userid numeric, io_cursor OUT t_cursor);
 PROCEDURE GetStepStatesByStepId(stepid in WFSTEPSTATES.step_id%type, io_cursor OUT t_cursor); 
 PROCEDURE GetStepByStepId(stepid in WFSTEP.step_id%type, io_cursor OUT t_cursor);
 PROCEDURE GetTasksByStepAndUserId(stepId in wfdocument.step_id%type, userId in wfdocument.user_asigned%type, selectType in numeric, io_cursor OUT t_cursor); 
 PROCEDURE GetRulesPreferences(rule_id in ZRULEOPT.ruleid%type, io_cursor OUT t_cursor);
 PROCEDURE GetStepStates(stepid in wfstepstates.step_id%type, io_cursor OUT t_cursor);
 PROCEDURE InsertWFStepHst(vDoc_Id WFStepHst.Doc_Id%type, vDoc_Name WFStepHst.Doc_Name%type, vDocTypeId WFStepHst.DocTypeId%type, vDoc_Type_Name WFStepHst.Doc_Type_Name%type, vFOLDER_Id WFStepHst.FOLDER_Id%type, vStepId WFStepHst.StepId%type, vStep_Name WFStepHst.Step_Name%type, vState WFStepHst.State%type, vUserName WFStepHst.UserName%type, vAccion WFStepHst.Accion%type, vFecha WFStepHst.Fecha%type, vWorkflowId WFStepHst.WorkflowId%type, vWorkflowName WFStepHst.WorkflowName%type);
 PROCEDURE GetStepsWithZOpenEvent(usrid numeric, io_cursor OUT t_cursor);
END ZSP_WORKFLOW_100;
CREATE OR REPLACE PACKAGE  "ZSP_ZBARCODE_200_PKG" as 
 procedure getPathForIdTypeIdDoc( doc_id in numeric, 
 doc_type_id in numeric ); end;
CREATE OR REPLACE PACKAGE  "ZSP_ZFORUM_100" as 
 Procedure delete_From_Foro(pdocid in zforum.docid%type , pdoct in zforum.doct%type , pparentid in zforum.parentid%type); 
 Procedure insert_Foro(f_Doct in zforum.Doct%type,f_DocId in zforum.DocId%type ,f_IdMensaje in zforum.IdMensaje%type,f_ParentId in zforum.ParentId%type,f_LinkName in zforum.LinkName%type,f_Mensaje in zforum.mensaje%type,f_Fecha in zforum.fecha%type,f_UserId in zforum.userid%type,f_StateId in zforum.stateid%type); 
 end zsp_zforum_100;
CREATE OR REPLACE PACKAGE  "ZWFSTEPSFACTORY_PKG" as
TYPE t_cursor IS REF CURSOR;
 procedure ZGetViewWFStepsByWfID (pWFId in ZViewWFSTEPS.work_id%type, io_cursor out t_cursor);
end ZWFSTEPSFACTORY_PKG;
CREATE OR REPLACE PACKAGE  "ZWFUPDDT_PKG" AS 
 PROCEDURE ZWfUpdDtLCByDtId(WfId IN doc_type.life_cycle%type , DocTypeID IN doc_type.doc_type_id%type ); 
 END ZWfUpdDt_Pkg;
CREATE OR REPLACE PACKAGE BODY  "ACTIONS_PKG" AS
 PROCEDURE Save_Action(AID IN USER_HST.ACTION_ID%TYPE ,
 AUSRID IN USER_HST.USER_ID%TYPE ,
 ADATE IN USER_HST.ACTION_DATE%TYPE ,
 AOBJID IN USER_HST.USER_ID%TYPE ,
 AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE ,
 ATYPE IN USER_HST.ACTION_TYPE%TYPE,
 ACONID IN UCM.CON_ID%TYPE,
 SOBJECTID IN USER_HST.S_OBJECT_ID%TYPE)IS

 BEGIN
 INSERT INTO USER_HST(ACTION_ID, USER_ID,ACTION_DATE,
 OBJECT_ID,OBJECT_TYPE_ID,ACTION_TYPE,S_Object_Id)
 VALUES (AID,AUSRID,ADATE,AOBJID,AOBJTID,ATYPE,SOBJECTID);

 IF AUSRID <> 9999 THEN
 UPDATE UCM SET u_time = SYSDATE WHERE con_id= ACONID;
 END IF;
 COMMIT;
 END Save_Action;
 END Actions_Pkg;

CREATE OR REPLACE PACKAGE BODY  "ACTUALIZA_ESTREG_PKG" 
 as
 Procedure Actualiza_Estreg (idd in Estreg.ID%Type, PCName In Estreg.M_NAME%Type)IS

Begin

 Update Estreg Set LAST_CHECK=Sysdate
 Where id=idd and M_NAME=PCName;
 if sql%notfound then
 commit;
 End If;
 Commit;
 End Actualiza_Estreg;
End Actualiza_Estreg_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "ADDRESTRICTIONRIGHTS_PKG" AS
 PROCEDURE AddRestrictionRights(userId in doc_restriction_r_user.User_ID%Type,
 restrictionId in doc_restriction_r_user.restriction_Id%Type)is
BEGIN
 insert into doc_restriction_r_user(user_id,restriction_Id) values(userId,RestrictionId);
 commit;
END AddRestrictionRights;
END AddRestrictionRights_pkg;

CREATE OR REPLACE PACKAGE BODY  "BORRARHISTORIAL_PKG" AS
 
PROCEDURE BORRARHISTORIAL(HistoryId in number) is 
BEGIN DELETE FROM IP_HST WHERE HST_ID =HistoryId; 
commit; 
END BORRARHISTORIAL; 
END BORRARHISTORIAL_PKG;
CREATE OR REPLACE PACKAGE BODY  "BORRARINDEX_PKG" AS 
PROCEDURE BORRARINDEX(IP in number) is BEGIN DELETE FROM IP_INDEX WHERE IP_ID=IP; 
commit; 
END BORRARINDEX; 
END BORRARINDEX_PKG;
CREATE OR REPLACE PACKAGE BODY  "BORRARIPTYPE_PKG" AS
 PROCEDURE BorrarIPType(IP IN IP_TYPE.IP_ID%TYPE)IS
 BEGIN
 DELETE FROM IP_TYPE WHERE IP_ID=IP;
 COMMIT;
 END BorrarIPType;
END Borrariptype_Pkg;

CREATE OR REPLACE PACKAGE BODY 
  "CALLVW_ASIGNEDDOCSBYUSER_PKG" AS 
 PROCEDURE CALLVW_ASIGNEDDOCSBYUSER (io_cursor OUT t_cursor)
 is 
 v_cursor t_cursor;
 BEGIN 
OPEN v_cursor FOR 
SELECT * from WFViewAsignedDocsByUser; 
io_cursor := v_cursor;
 END CALLVW_ASIGNEDDOCSBYUSER; 
 END CALLVW_ASIGNEDDOCSBYUSER_PKG;
CREATE OR REPLACE PACKAGE BODY 
  "CALL_VIEWGETEXPIREDDOCS_PKG" AS 
PROCEDURE CALL_VIEWGETEXPIREDDOCS(io_cursor OUT t_cursor) 
is 
 v_cursor t_cursor;
BEGIN 
OPEN v_cursor FOR
Select * from View_GetExpiredDocs; 
io_cursor := v_cursor;
commit; 
END CALL_VIEWGETEXPIREDDOCS; 
END CALL_VIEWGETEXPIREDDOCS_PKG;
CREATE OR REPLACE PACKAGE BODY 
  "CALL_VIEW_GETEXPIREDDOCS_PKG" AS PROCEDURE 
 CALL_VIEW_GETEXPIREDDOCS(io_cursor OUT t_cursor) 
is 
v_cursor t_cursor;
BEGIN 
OPEN v_cursor FOR
Select * from View_GetExpiredDocs; 
io_cursor := v_cursor;
commit; 
END CALL_VIEW_GETEXPIREDDOCS; 
END CALL_VIEW_GETEXPIREDDOCS_PKG;
CREATE OR REPLACE PACKAGE BODY  "CHECK_BY_TIME_PKG" AS
 PROCEDURE Check_by_time (conid IN UCM.CON_ID%TYPE,salida OUT BOOLEAN) IS
 num NUMBER(5);
 BEGIN
 SELECT COUNT(*) INTO num FROM UCM WHERE (SYSDATE - U_TIME)*24*60 >
TIME_OUT AND CON_ID=conid;
 IF num > 0 THEN
 salida := TRUE;
 ELSE
 salida := FALSE;
 END IF;
 END Check_by_time;
END Check_By_Time_Pkg;

CREATE OR REPLACE PACKAGE BODY  "CLEARIPINDEX_PKG" AS
 PROCEDURE ClearTInd(IP IN IP_INDEX.IP_ID%TYPE)IS
 BEGIN
 DELETE FROM IP_INDEX WHERE IP_ID=IP;
 COMMIT;
 END ClearTInd;
END ClearIPIndex_pkg;

CREATE OR REPLACE PACKAGE BODY 
  "CLSACTIONS_GETUSERACTIONS_PKG" 
 AS
 PROCEDURE getUserActions (UserId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT USER_HST.Action_Date AS Fecha, OBJECTTYPES.OBJECTTYPES AS
Herramienta, RIGHTSTYPE.RIGHTSTYPE AS Accion, user_hst.s_object_id AS En
 FROM USER_HST,USER_TABLE,OBJECTTYPES,RIGHTSTYPE
 WHERE USER_HST.User_Id = USER_TABLE.User_Id AND
 USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId AND
 USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId AND
 USER_HST.User_Id = UserId
 ORDER BY USER_HST.Action_Date DESC;
 io_cursor := v_cursor;
 END getUserActions;
END Clsactions_Getuseractions_Pkg;
CREATE OR REPLACE PACKAGE BODY 
  "CLSDOCGROUP_LOADDOCTYPES_PKG" AS
 PROCEDURE LoadDocTypes (DocGroupId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DOC_TYPE.Doc_Type_Id, DOC_TYPE.Doc_Type_Name, DOC_TYPE.Object_Type_Id, DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Order,
 DOC_TYPE_R_DOC_TYPE_GROUP.DOC_TYPE_GROUP

 FROM DOC_TYPE ,DOC_TYPE_R_DOC_TYPE_GROUP
 WHERE DOC_TYPE.Doc_Type_Id = DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Type_Id AND
 DOC_TYPE_R_DOC_TYPE_GROUP.DOC_TYPE_GROUP = DocGroupId
 ORDER BY DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Order;
 io_cursor := v_cursor;
 END LoadDocTypes;
END CLSDOCGROUP_LOADDOCTYPES_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "CLSDOCTYPE_GETDOCTYPES_PKG" AS
 PROCEDURE getDocTypes (CurrentUserId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DOC_TYPE.Doc_Type_Id, DOC_TYPE.Doc_Type_Name, DOC_TYPE.File_Format_ID, DOC_TYPE.Disk_Group_ID, DOC_TYPE.Thumbnails, DOC_TYPE.Icon_Id,
 DOC_TYPE.Object_Type_Id, DOC_TYPE.AutoName
 FROM DOC_TYPE,USER_RIGHTS
 WHERE DOC_TYPE.Doc_Type_Id = USER_RIGHTS.Object_Id AND
 DOC_TYPE.Object_Type_Id = USER_RIGHTS.Object_Type_ID AND
 USER_RIGHTS.Right_Value = 1 AND
 USER_RIGHTS.User_Rights_Type_Id = 3 AND
 USER_RIGHTS.User_Id = CurrentUserId
 ORDER BY DOC_TYPE.Doc_Type_Name;
 io_cursor := v_cursor;
 END getDocTypes;
END CLSDOCTYPE_GETDOCTYPES_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "CLSDOC_GENERACIONINDICES_PKG" AS
 PROCEDURE generacionIndices (DocTypeId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DOC_INDEX.Index_Id, DOC_INDEX.Index_Name, DOC_INDEX.INDEX_TYPE, DOC_INDEX.Index_Len, DOC_INDEX.AutoFill, DOC_INDEX.NoIndex,
 DOC_INDEX.DropDown, DOC_INDEX.AutoDisplay, DOC_INDEX.Invisible, DOC_INDEX.Object_Type_Id, INDEX_R_DOC_TYPE.Doc_Type_Id, INDEX_R_DOC_TYPE.Orden
 FROM DOC_INDEX ,INDEX_R_DOC_TYPE WHERE
 DOC_INDEX.Index_Id = INDEX_R_DOC_TYPE.Index_Id AND Doc_Type_Id = DocTypeId ORDER BY ORDEN;
 io_cursor := v_cursor;
 END generacionIndices;
END CLSDOC_GENERACIONINDICES_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "CLSVOLUME_RETRIEVEVOLUMEID_PKG" AS
 PROCEDURE retrieveVolumeId (DocTypeId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DISK_VOLUME.Disk_Vol_Id, DISK_VOLUME.Disk_Vol_Name, DISK_VOLUME.Disk_Vol_Size, DISK_VOLUME.Disk_Vol_Type, DISK_VOLUME.Disk_Vol_Copy,
 DISK_VOLUME.Disk_Vol_Path, DISK_VOLUME.Disk_Vol_Size_Len, DISK_VOLUME.Disk_Vol_State, DISK_VOLUME.Disk_Vol_LstOffset
 FROM DOC_TYPE, DISK_VOLUME where DOC_TYPE.Disk_Group_ID = DISK_VOLUME.Disk_Vol_Id and DOC_TYPE.Doc_Type_Id = DocTypeId;
 io_cursor := v_cursor;
 END retrieveVolumeId;
END CLSVOLUME_RETRIEVEVOLUMEID_PKG;

CREATE OR REPLACE PACKAGE BODY  "CONTARTABLAS_PKG" AS
Procedure ContarTablas(io_cursor OUT t_cursor)IS
v_cursor t_cursor;
Begin
OPEN v_cursor FOR
select * from tabs where Table_Name Like 'DOC_%';
io_cursor := v_cursor;
End ContarTablas;
End ContarTablas_PKG;

CREATE OR REPLACE PACKAGE BODY  "COPY_DOC_TYPE_PKG" AS
 PROCEDURE Copy_Doc_Type(DocTypeId NUMERIC,NewdocTypeId NUMERIC,NewName VARCHAR2)IS
BEGIN
DECLARE FILEFORMATID NUMERIC;
DISKGROUPID NUMERIC;
THUMB NUMERIC;
ICONID NUMERIC;
CROSSREFERENCE NUMERIC;
LIFECYCLE NUMERIC;
OBJECTTYPEID NUMERIC;
ANAME VARCHAR2(255);
BEGIN
SELECT FILE_FORMAT_ID , Disk_Group_ID, THUMBNAILS, ICON_ID, CROSS_REFERENCE, LIFE_CYCLE, OBJECT_TYPE_ID,AUTONAME INTO
FILEFORMATID, DISKGROUPID, THUMB, ICONID, CROSSREFERENCE, LIFECYCLE, OBJECTTYPEID, ANAME
FROM DOC_TYPE WHERE DOC_TYPE_ID=DOCTYPEID;

INSERT INTO DOC_TYPE(DOC_TYPE_ID,DOC_TYPE_NAME,FILE_FORMAT_ID, DISK_GROUP_ID,THUMBNAILS,ICON_ID,CROSS_REFERENCE,LIFE_CYCLE,OBJECT_TYPE_ID,AUTONAME,DOCUMENTALID) VALUES (NewDocTypeid,NewName,FILEFORMATID,DISKGROUPID,THUMB,ICONID,CROSSREFERENCE,LIFECYCLE,OBJECTTYPEID,ANAME,0);
COMMIT;
DELETE FROM TABLATEMP;
INSERT INTO TABLATEMP(Index_ID,Doc_Type_ID,Orden) SELECT Index_ID,Doc_Type_ID,Orden FROM INDEX_R_DOC_TYPE WHERE DOC_TYPE_ID=DocTypeID;
UPDATE TABLATEMP SET Doc_Type_ID=NewDocTypeId;
COMMIT;
INSERT INTO INDEX_R_DOC_TYPE(Index_ID,Doc_type_Id,Orden) SELECT Index_Id,Doc_Type_ID,Orden FROM TABLATEMP WHERE Doc_Type_ID=NewDocTypeID;
COMMIT;
DELETE FROM TABLATEMP;
COMMIT;
END;
END Copy_Doc_Type;
END Copy_Doc_Type_Pkg;
CREATE OR REPLACE PACKAGE BODY  "COUNTID_UCM_PKG" AS
 PROCEDURE count_id (io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR SELECT count(CON_ID) FROM UCM;
 io_cursor := v_cursor;
 END count_id;
END COUNTID_UCM_PKG;

CREATE OR REPLACE PACKAGE BODY 
 "COUNT_NEW_MESSAGES_PKG" AS
 PROCEDURE CountNewMessages(userId in numeric,io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR SELECT count(*) FROM MSG_DEST WHERE MSG_DEST.user_id=userid AND MSG_DEST.deleted=0 and read=0;
 io_cursor := v_cursor;
 END;
END Count_New_Messages_Pkg;
CREATE OR REPLACE PACKAGE BODY  "DBCONFIG_PKG" AS
PROCEDURE getObject(obj_type IN VARCHAR2 , io_cursor OUT t_cursor) IS
v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT object_name FROM user_objects
 WHERE object_id >30000 AND OBJECT_TYPE = obj_type AND status ='VALID';
 io_cursor := v_cursor;
 END getObject;
END DBCONFIG_PKG;

CREATE OR REPLACE PACKAGE BODY  "DELETE_BY_CONID_PKG" AS

PROCEDURE Delete_by_conid(conid IN UCM.CON_ID%TYPE ) IS

BEGIN

DELETE FROM UCM

WHERE CON_ID = conid;

COMMIT;

END Delete_by_conid;

END Delete_By_conid_Pkg;
CREATE OR REPLACE PACKAGE BODY  "DELETE_BY_TIME_PKG" AS
 PROCEDURE Delete_by_time IS
 BEGIN
 DELETE FROM UCM
 WHERE TIME_OUT < TO_NUMBER(SYSDATE - U_TIME)*(24*60) ;
 COMMIT;
 END Delete_by_time;
 END Delete_By_Time_Pkg;

CREATE OR REPLACE PACKAGE BODY  "DELETE_MSG_PKG" AS

 PROCEDURE deleteMsgSender(m_id IN MESSAGE.msg_id%TYPE)IS
 recived NUMERIC;
 BEGIN
 SELECT COUNT(*)INTO recived FROM MSG_DEST WHERE msg_id=m_id AND
deleted=0;
 IF (recived=0)THEN
 DELETE MSG_DEST WHERE msg_id=m_id;
 DELETE MSG_ATTACH WHERE msg_id=m_id;
 DELETE MESSAGE WHERE msg_id=m_id;
 ELSE
 UPDATE MESSAGE SET deleted=1 WHERE msg_id=m_id;
 END IF;
 END;

END;

CREATE OR REPLACE PACKAGE BODY  "DELEXCEPTABLE_PKG" AS

PROCEDURE DelExcepTable IS

BEGIN

Delete from Excep where Fecha >(Sysdate-30);

END DelExcepTable;

END DelExcepTable_Pkg;

CREATE OR REPLACE PACKAGE BODY  "DELIPINDEX_PKG" AS
 PROCEDURE Borrarindex(IP IN IP_INDEX.IP_ID%TYPE)IS
 BEGIN
 DELETE FROM IP_INDEX WHERE IP_ID=IP;
 COMMIT;
 END Borrarindex;
END delIPindex_pkg;

CREATE OR REPLACE PACKAGE BODY  "DELIPTYPE_PKG" AS
 PROCEDURE BorrarType(IP IN IP_TYPE.IP_ID%TYPE)IS
 BEGIN
 DELETE FROM IP_TYPE WHERE IP_ID=IP;
 COMMIT;
 END BorrarType;
END delIPtype_pkg;

CREATE OR REPLACE PACKAGE BODY  "DEL_LCK_BYTIME_PKG" AS
 PROCEDURE Del_LCK_Bytime IS
BEGIN
 DELETE FROM LCK WHERE (SYSDATE - LCK_Date) > 1;
 COMMIT;
END Del_LCK_Bytime;
END Del_Lck_Bytime_Pkg;

CREATE OR REPLACE PACKAGE BODY  "DEL_LCK_PKG" AS
 PROCEDURE Del_LCK(docid IN LCK.Doc_ID%TYPE,
 userid IN LCK.USER_ID%TYPE,
 Estid IN LCK.EST_ID%TYPE)IS
BEGIN
 DELETE FROM LCK WHERE Doc_ID=docid AND User_ID=userid AND Est_Id=estid;
 COMMIT;
END Del_LCK;
END Del_Lck_Pkg;

CREATE OR REPLACE PACKAGE BODY  "DEL_MSG_REV_PKG" AS
 PROCEDURE deleteMSGRecived(m_id IN MESSAGE.msg_id%TYPE, u_id IN
MSG_DEST.user_id%TYPE) IS
 BEGIN
 UPDATE MSG_DEST SET deleted=1 WHERE msg_id=m_id AND
user_id=u_id;
 END;
END;

CREATE OR REPLACE PACKAGE BODY  "DINAMICSEARCH" as
 Procedure indexSearch(strsql in Varchar2,io_cursor OUT t_refcur) is
 v_ReturnCursor t_RefCur;
Begin
 Open v_ReturnCursor FOR strsql;
 io_cursor := v_Returncursor;
End IndexSearch;
End DinamicSearch;
CREATE OR REPLACE PACKAGE BODY 
  "FA_GETARCHIVOSBLOQUEADOS_PKG" AS
 PROCEDURE getArchivosBloqueados (io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT IP_TASK.Id AS ID, IP_TASK.File_Path AS Ruta, IP_TASK.Zip_Origen AS Archivo_Zip
 FROM IP_TASK ,IP_FOLDER WHERE
 IP_TASK.Id_Configuracion = IP_FOLDER.Id AND IP_TASK.Bloqueado = 1;
 io_cursor := v_cursor;
 END getArchivosBloqueados;
END FA_GETARCHIVOSBLOQUEADOS_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "FRMDOCTYPE_LOADINDEX_PKG" AS
 PROCEDURE loadIndex (DocTypeId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
OPEN v_cursor FOR

SELECT DOC_INDEX.INDEX_ID,
 DOC_INDEX.INDEX_NAME,
 DOC_INDEX.INDEX_TYPE,
 DOC_INDEX.INDEX_LEN,
 DOC_INDEX.Object_Type_Id,
 INDEX_R_DOC_TYPE.Orden,
 INDEX_R_DOC_TYPE.Doc_Type_Id
 FROM DOC_INDEX, INDEX_R_DOC_TYPE
 WHERE INDEX_R_DOC_TYPE.INDEX_ID = DOC_INDEX.Index_Id AND
 INDEX_R_DOC_TYPE.Doc_Type_Id = DocTypeId
 ORDER BY INDEX_R_DOC_TYPE.Orden;
io_cursor := v_cursor;
 END loadIndex;
END FRMDOCTYPE_LOADINDEX_PKG;

CREATE OR REPLACE PACKAGE BODY  "FRMIMPORT_FILLINDEX_PKG"
 AS
 PROCEDURE fillIndex (IPJOBDocTypeId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DOC_INDEX.Index_Id,
 DOC_INDEX.Index_Name,
 DOC_INDEX.INDEX_TYPE,
 DOC_INDEX.Index_Len,
 DOC_INDEX.AutoFill,
 DOC_INDEX.NoIndex,
 DOC_INDEX.DropDown,
 DOC_INDEX.AutoDisplay,
 DOC_INDEX.Invisible,
 DOC_INDEX.Object_Type_Id

 FROM INDEX_R_DOC_TYPE, DOC_INDEX
 WHERE INDEX_R_DOC_TYPE.Index_Id = DOC_INDEX.Index_Id AND
 INDEX_R_DOC_TYPE.Doc_Type_Id = IPJOBDocTypeId;
 io_cursor := v_cursor;
 END fillIndex;
END FRMIMPORT_FILLINDEX_PKG;

CREATE OR REPLACE PACKAGE BODY  "GETADDRESSBOOK_PKG" 
 AS
Procedure GetAddressBook (userID IN USER_TABLE.USER_ID%type, io_cursor OUT t_cursor) IS
v_cursor t_cursor;
BEGIN
 OPEN v_cursor FOR SELECT USER_ADDRESS_BOOK
 FROM USER_TABLE
 WHERE USER_ID=USeRID;
io_cursor := v_cursor;

 END GETADDRESSBOOK;
END GETADDRESSBOOK_Pkg;

CREATE OR REPLACE PACKAGE BODY  "GETANDSETLASTID2_PKG" 
 AS
PROCEDURE GetandSetLastId2(varOBJTYPE IN OBJLASTID.OBJECT_TYPE_ID%TYPE, varID OUT OBJLASTID.OBJECTID%TYPE) IS
 tempId OBJLASTID.OBJECTID%TYPE;
 BEGIN
 SELECT Count(*) into tempId FROM OBJLASTID WHERE OBJECT_TYPE_ID = varOBJTYPE;

 IF tempId=0 then
 Insert into Objlastid(Object_type_id,objectid) values(varOBJTYPE, 0);
 End If;

 UPDATE OBJLASTID SET OBJECTID = OBJECTID + 1 WHERE OBJECT_TYPE_ID = varOBJTYPE;
 SELECT OBJECTID into tempId FROM OBJLASTID WHERE OBJECT_TYPE_ID = varOBJTYPE;

 varID:=tempId;
 END GetandSetLastId2;
END GETANDSETLASTID2_PKG;
CREATE OR REPLACE PACKAGE BODY  "GETANDSET_LASTID_PKG" 
 AS
 PROCEDURE GetandSetLastId (OBJTYPE IN OBJLASTID.OBJECT_TYPE_ID%TYPE, io_cursor OUT t_cursor) IS
 s_cursor t_cursor;
 r_cursor t_cursor;
 OBJID OBJLASTID.OBJECTID%TYPE;
 BEGIN
 
 OPEN s_cursor FOR SELECT OBJECTID
 FROM OBJLASTID
 WHERE OBJECT_TYPE_ID = OBJTYPE;

 IF SQL%NotFound then
 Insert into Objlastid(Object_type_id,objectid) values(OBJTYPE,0);
 open r_cursor for select objectid from objlastid where object_type_id=objtype;
 io_cursor:=r_cursor;
 else
 io_cursor := s_cursor;
 End If;
 UPDATE OBJLASTID SET OBJECTID = OBJECTID + 1 WHERE OBJECT_TYPE_ID = OBJTYPE;
 END;
END Getandset_Lastid_Pkg;
CREATE OR REPLACE PACKAGE BODY  "GETDOCTYPERIGHTS_PKG" 
 As
 Procedure GetDocTypeRights (userID IN number,io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
BEGIN
 OPEN v_cursor FOR
 Select Doc_Type.Doc_Type_Id, Doc_Type.Doc_Type_Name, Doc_Type.File_Format_ID, Doc_Type.Disk_Group_ID, Doc_Type.Thumbnails, Doc_Type.Icon_Id, Doc_Type.Cross_Reference, Doc_Type.Life_Cycle, Doc_Type.Object_Type_Id, Doc_Type.AutoName, doc_type.documentalid from Doc_Type, User_Rights Where Doc_Type.Doc_Type_Id=User_Rights.Object_ID and User_Rights.User_ID=userID and User_Rights.User_Rights_Type_Id=3 and User_Rights.Right_value=1 and user_rights.object_type_id=2 ORDER BY doc_type.Doc_Type_Name;
 io_cursor := v_cursor;
 End GetDocTypeRights;
 End GetDocTypeRights_PKG;

CREATE OR REPLACE PACKAGE BODY  "GETDOCUMENTACTIONS_PKG" 
 AS
 PROCEDURE getDocumentActions (DocumentId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT USER_TABLE.User_Name,
 USER_HST.Action_Date,
 OBJECTTYPES.OBJECTTYPES,
 RIGHTSTYPE.RIGHTSTYPE,
 USER_HST.Object_Id,
 User_hst.s_object_id
 FROM USER_HST , USER_TABLE , OBJECTTYPES, RIGHTSTYPE

 WHERE USER_HST.User_Id = USER_TABLE.User_Id AND
 USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId AND
 USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId AND
 Object_Id = DocumentId AND
 Object_Type_Id = 6
 ORDER BY USER_HST.ACTION_DATE DESC, USER_HST.OBJECT_ID, USER_HST.S_OBJECT_ID;
 io_cursor := v_cursor;
 END getDocumentActions;
END Getdocumentactions_Pkg;

CREATE OR REPLACE PACKAGE BODY  "GETMYMESSAGESNEW_PKG" 
 AS PROCEDURE zsp_GetMyMessagesNew(my_id IN 
 MSG_DEST.user_id%type,io_cursor OUT t_cursor) IS v_cursor 
 t_cursor;BEGIN OPEN v_cursor 
 for 
 Select msg_id 
 from MSG_DEST 
 where MSG_DEST.READ='0' 
 and user_id = my_id 
 and MSG_DEST.deleted='0';io_cursor := v_cursor; END 
 zsp_GetMyMessagesNew;	End GetMyMessagesNew_Pkg;
CREATE OR REPLACE PACKAGE BODY  "GETPROCESS_PKG" AS
 PROCEDURE GetProcess(I IN IP_TYPE.IP_ID%TYPE,io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT IP_NAME, IP_PATH, IP_CHR, IP_DOCTYPEID, IP_ID, IP_MOVE, IP_VERIFY, IP_ACEPTBLANK, IP_BACKUP, IP_DELSOURCE, IP_SOURCEVARIABLE, IP_MULTIPLEFILES,IP_MULTIPLECHR FROM IP_TYPE WHERE (IP_ID = I);
 io_cursor := v_cursor;
 END GetProcess;
END Getprocess_Pkg;

CREATE OR REPLACE PACKAGE BODY  "GETRESTRICTIONS_PKG" as
PROCEDURE GetRestrictions(UserID IN User_Table.User_ID%TYPE,io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 select RESTRICTION_ID from doc_restriction_r_user where user_id=UserId;
 io_cursor := v_cursor;
 END GetRestrictions;
END GetRestrictions_Pkg;

CREATE OR REPLACE PACKAGE BODY  "GET_DOCTYPESID_PKG" AS
Procedure Get_DocTypesID(io_cursor OUT t_cursor)IS
v_cursor t_cursor;
Begin
OPEN v_cursor FOR
Select Doc_Type_ID,Doc_Type_Name from Doc_Type;
io_cursor := v_cursor;
End Get_DocTypesID;
End Get_DocTypesID_PKG;

CREATE OR REPLACE PACKAGE BODY  "GET_DOCTYPES_PKG" AS
 PROCEDURE Get_Doctypes(io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT Doc_Type_Id, Doc_Type_Name, File_Format_ID, Disk_Group_ID, Thumbnails, Icon_Id, Cross_Reference, Life_Cycle, Object_Type_Id FROM DOC_TYPE;
 io_cursor := v_cursor;
 END Get_DocTypes;
END Get_Doctypes_Pkg;

CREATE OR REPLACE PACKAGE BODY  "GET_MY_MESSAGES_PKG" AS
PROCEDURE getMymessages(my_id IN USRTABLE.id%TYPE ,io_cursor
OUT t_cursor)IS
v_cursor t_cursor;
BEGIN
OPEN v_cursor FOR SELECT MESSAGE.msg_id,
MESSAGE.msg_from,
USRTABLE.name User_Name,
MESSAGE.subject,
MESSAGE.msg_date,
MESSAGE.reenvio,
MESSAGE.deleted,
MSG_DEST.user_id DEST,
MSG_DEST.user_name DEST_NAME,
MSG_DEST.dest_type,
MSG_DEST.READ,
MESSAGE.msg_body
FROM MESSAGE,MSG_DEST,USRTABLE
WHERE message.msg_id=msg_dest.msg_id and
message.msg_from=usrtable.id and
message.msg_id in(SELECT msg_id FROM MSG_DEST where user_id=my_id AND
deleted=0);
io_cursor := v_cursor;
END getmymessages;
END Get_My_Messages_Pkg;
CREATE OR REPLACE PACKAGE BODY  "GET_MY_MSG_ATTACHS" AS
 PROCEDURE getmymessagesattach(my_id IN USER_TABLE.user_id%TYPE
,io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR SELECT MESSAGE.msg_id,
 MSG_ATTACH.msg_id,
 MSG_ATTACH.doc_id,
 MSG_ATTACH.doc_type_id,
 MSG_ATTACH.folder_id,
 MSG_ATTACH.index_id ,
 MSG_ATTACH.name,
 MSG_ATTACH.icon,
volumelistid,
 doc_file,
 offset,
 disk_vol_path
 FROM MESSAGE,MSG_ATTACH
 WHERE MESSAGE.msg_id = MSG_ATTACH.msg_id AND
 /*user_table.user_id= message.msg_from and */
 MESSAGE.msg_id IN (SELECT msg.msg_id FROM MESSAGE msg,MSG_DEST
WHERE msg.msg_id = MSG_DEST.msg_id AND MSG_DEST.user_id=my_id AND
MSG_DEST.deleted=0);
 io_cursor := v_cursor;
 END getmymessagesattach;
END get_my_msg_attachs;

CREATE OR REPLACE PACKAGE BODY  "IMPORTJOB_PKG" AS
 PROCEDURE Import_JobT2(IP_ID IN IP_INDEX.IP_ID%TYPE,
 Array_ID IN IP_INDEX.ARRAY_ID%TYPE,
 Index_ID IN IP_INDEX.INDEX_ID%TYPE,
 Index_Order IN IP_INDEX.INDEX_ORDER%TYPE)IS
 BEGIN
 INSERT INTO IP_INDEX VALUES(IP_ID, Array_ID, Index_ID, Index_Order);
 COMMIT;
END import_jobT2;
END ImportJob_pkg;

CREATE OR REPLACE PACKAGE BODY  "INCREMENTARDOCTYPE_PKG" 
 AS
 PROCEDURE IncrementarDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,X IN Number) IS
 BEGIN
 Update Doc_Type Set DocCount=DocCount + X where Doc_Type_Id= DocID;
 END IncrementarDocType;
END IncrementarDocType_Pkg;

CREATE OR REPLACE PACKAGE BODY  "INDEXRDOCTYPE_PKG" AS
 PROCEDURE IndexRDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR Select Index_Id, Doc_Type_ID From Index_R_Doc_Type where Doc_Type_ID=DocId;
 io_cursor := v_cursor;
 END IndexRDocType;
END IndexRDocType_Pkg;

CREATE OR REPLACE PACKAGE BODY  "INSERTLIC_PKG" as
 PROCEDURE INSERTLIC(X varchar) IS
 BEGIN
 UPDATE LIC SET NUMERO_LICENCIAS=X;
 COMMIT;
 END INSERTLIC;
END INSERTLIC_pkg;

CREATE OR REPLACE PACKAGE BODY 
  "INSERTUPDATESUSTITUCION_PKG" As
 PROCEDURE InsertUpdateSustitucion (Id IN number, Detalle IN varchar2, Tabla In Varchar2)IS
Begin
dbms_utility.exec_ddl_statement('Insert into '||Tabla||' values ('||Id||',''||Detalle||'')');
dbms_utility.exec_ddl_statement('Update '||Tabla||' Set Descripcion='''||detalle||''' Where id='||id);
Commit;
 End InsertUpdateSustitucion;
End InsertUpdateSustitucion_PKG;

CREATE OR REPLACE PACKAGE BODY  "INSERT_ATTACH_PKG" AS
 PROCEDURE InsertMSGAttach(m_id IN MSG_ATTACH.msg_id%TYPE,
 m_DOCid IN MSG_ATTACH.doc_id%TYPE,
 m_DOC_TYPE_ID IN MSG_ATTACH.doc_type_id%TYPE,
 fold_id IN MSG_ATTACH.folder_id%TYPE,
 inde_id IN MSG_ATTACH.index_id%TYPE,
 m_name IN MSG_ATTACH.name%TYPE,
 m_icon IN MSG_ATTACH.icon%TYPE,
 m_volumelistid IN MSG_ATTACH.volumelistid%TYPE,
 m_doc_file IN MSG_ATTACH.doc_file%TYPE,
 m_offset IN MSG_ATTACH.offset%TYPE,
 m_disk_vol_path IN MSG_ATTACH.disk_vol_path%TYPE)
IS
BEGIN
 INSERT INTO MSG_ATTACH(MSG_ID,
 DOC_ID,
 DOC_TYPE_ID,
 FOLDER_ID,
 INDEX_ID,
 NAME,
 ICON,
volumelistid,
 doc_file,
 offset,
 disk_vol_path)
 VALUES (m_id,
 m_DOCid,
 m_DOC_TYPE_ID,
 FOLD_ID,
 inde_id,
 m_name,
 m_icon,
 m_volumelistid,
 m_doc_file,
 m_offset,
 m_disk_vol_path);
 COMMIT;
END InsertMSGAttach;
END Insert_attach_pkg;

CREATE OR REPLACE PACKAGE BODY  "INSERT_ESTREG_PKG" as
 Procedure Insert_Estreg (idd in Estreg.ID%Type, PCName In Estreg.M_NAME%Type,
 WinName In Estreg.W_USER%Type, VERSION IN Estreg.VER%TYPE,
 Actualizado IN ESTREG.UPDATED%Type)IS

Begin

 Update Estreg Set M_NAME=PCName, W_User=WinName, Ver=VERSION, UPDATED=Actualizado, LAST_CHECK=Sysdate
 Where id=idd;
 if sql%notfound then
 Insert into ESTREG Values(idd,PCName,WinName,VERSION,Actualizado,SysDate);
 End If;
 Commit;
 End Insert_Estreg;
End Insert_Estreg_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "INSERT_INTO_IP_FOLDER_PKG" AS
 PROCEDURE Ins_Into_IPFolder(RowNombre IN IP_FOLDER.Nombre%TYPE,
 RowPath IN IP_FOLDER.PATH%TYPE,
 RowTimer IN IP_FOLDER.TIMER%TYPE,
 i IN IP_FOLDER.SERVICE%TYPE,
 RowUserId IN IP_FOLDER.User_ID%TYPE,
 PcName IN IP_FOLDER.NOMBREMAQUINA%TYPE) IS

 BEGIN
 INSERT INTO IP_FOLDER(Nombre,Path,NombreMaquina,Service,User_Id,Timer)
 VALUES(RowNombre,RowPath,RowUserId,PcName,I,RowTimer);
 COMMIT;
 END Ins_Into_IPFolder; /*fin del procedimiento*/
 END Insert_Into_Ip_Folder_Pkg; /*fin del paquete*/

CREATE OR REPLACE PACKAGE BODY 
 "INSERT_MSG_DESTINO_PKG" AS
 PROCEDURE InsertMSGDest(m_id IN MSG_DEST.msg_id%TYPE,
 m_userid IN MSG_DEST.USER_ID%TYPE,
 m_Dest_TYPE IN MSG_DEST.DEST_TYPE%TYPE,
 m_User_name IN MSG_DEST.user_name%TYPE)
 IS
 BEGIN
 INSERT INTO MSG_DEST(msg_id,user_id ,dest_type, READ,User_name)
 VALUES (m_id ,m_userid,m_Dest_Type,0,m_user_name);
 COMMIT;
END InsertMSGDest;

END Insert_Msg_Destino_Pkg;
CREATE OR REPLACE PACKAGE BODY  "INSERT_MSG_PKG" AS
 PROCEDURE InsertMSG(
 m_id IN MESSAGE.msg_id%TYPE,
 m_from IN MESSAGE.msg_from%TYPE,
 m_Body IN MESSAGE.msg_body%TYPE,
 m_subject IN MESSAGE.subject%TYPE,
 m_resend IN MESSAGE.reenvio%TYPE)
IS
BEGIN

 INSERT INTO MESSAGE(msg_id,
 msg_from,
 msg_body,
 subject,
 msg_date,
 reenvio,
 deleted)
 VALUES (
 m_id,
 m_from,
 m_body,
 m_subject,
 SYSDATE,
 m_resend,
 0);
 COMMIT;
END InsertMSG;
END Insert_MsG_pkg;

CREATE OR REPLACE PACKAGE BODY  "INSERT_PROCESS_HST_PKG" 
 as
 procedure InsertProcHst
 (HID in p_hst.id%type,
 PID in p_hst.Process_id%type,
 PDATE in p_hst.Process_Date%type,
 USrid in p_hst.user_id%type,
 totfiles in p_hst.Totalfiles%type,
 procfiles in p_hst.ProcessedFiles%type,
 skpfiles in p_hst.SkipedFiles%type,
 ErrFiles in p_hst.ErrorFiles%type,
 RID in p_hst.Result_id%type,
 Pth in p_hst.Path%type,
 hsh in p_hst.hash%type,
 tfile in p_hst.tempfile%type,
 efile in p_hst.errorfile%type,
 lfile in p_hst.logfile%type)
 is
 begin

 INSERT INTO P_HST(ID,
 Process_ID,
 Process_Date,
 User_Id,
 TotalFiles,
 ProcessedFiles,
 SkipedFiles,
 ErrorFiles,
 Result_Id,
 PATH,
 HASH,
 errorfile,
 tempfile,
 logfile)
 VALUES(HID,
 PID ,
 Pdate,
/* to_date(Pdate,'DD-MM-YYYY HH24-MI-SS'),*/
 UsrId,
 TotFiles,
 ProcFiles,
 SkpFiles,
 ErrFiles,
 RID,
 Pth,
 Hsh,
 efile,
 tfile,
 lfile);
 end;
end;

CREATE OR REPLACE PACKAGE BODY  "INSERT_USER159_PKG" AS
PROCEDURE Ins_New_Connection(v_userId IN UCM.USER_ID%TYPE,
 v_win_User IN UCM.WINUSER%TYPE,
 v_win_Pc IN UCM.WINPC%TYPE,
 v_con_Id IN UCM.CON_ID%TYPE,
 v_timeout IN UCM.TIME_OUT%TYPE,
 WF IN UCM."TYPE"%Type) IS
BEGIN
 INSERT INTO UCM(USER_ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,TIME_OUT,"TYPE")
 VALUES (v_UserId,SYSDATE,
 SYSDATE,v_Win_User,v_Win_PC,v_con_Id,v_timeout,WF);
 COMMIT;
END Ins_New_Connection;
END Insert_User159_Pkg;
CREATE OR REPLACE PACKAGE BODY  "INSERT_USER_PKG" AS
PROCEDURE Ins_New_Connection(v_userId 	 	 IN UCM.USER_ID%TYPE,
			 					 v_win_User 	 IN UCM.WINUSER%TYPE,
								 v_win_Pc IN UCM.WINPC%TYPE,
								 v_con_Id IN UCM.CON_ID%TYPE,
								 v_timeout IN UCM.TIME_OUT%TYPE) IS
	 BEGIN
	 		INSERT INTO UCM(USER_ID,C_TIME,	U_TIME,	WINUSER,WINPC,CON_ID,TIME_OUT)
			VALUES (v_UserId,SYSDATE, SYSDATE,v_Win_User,v_Win_PC,v_con_Id,v_timeout);
			COMMIT;
	 END Ins_New_Connection;
END Insert_User_Pkg;

CREATE OR REPLACE PACKAGE BODY  "INSERT_VERREG_PKG" AS
PROCEDURE Insert_Verreg(
 IDD IN VERREG.ID%TYPE ,
 Version IN VERREG.VER%TYPE ,
 Path IN VERREG.Path%TYPE)IS

BEGIN
 INSERT INTO VERREG(Id,Ver,Path,Updated)
 VALUES(IDD, Version,Path,SYSDATE);
 COMMIT;
END Insert_Verreg;
END Insert_Verreg_Pkg;

CREATE OR REPLACE PACKAGE BODY  "INSERT_ZBARCODE_PKG" AS
 PROCEDURE Insert_ZBarCode(idbarcode IN ZBarCode.ID%TYPE,
 DocTypeId IN ZBarCode.doc_type_id%TYPE,
 UserId in ZBarCode.Userid%type,
 Doc_id in ZBarCode.Doc_Id%type)
IS
BEGIN
 Insert into ZBarCode(Id,Fecha,Doc_Type_ID,UserId,Doc_Id)
Values(idbarcode,Sysdate,DocTypeId,Userid,Doc_id);
 COMMIT;
END Insert_ZBarCode;
END INSERT_ZBarcode_PKG;
CREATE OR REPLACE PACKAGE BODY  "INS_LCK_PKG" AS
 PROCEDURE Ins_LCK(docid IN LCK.Doc_ID%TYPE ,
 Userid IN LCK.USER_ID%TYPE ,
 Estid IN LCK.Est_Id%TYPE )IS
 BEGIN
 INSERT INTO LCK(doc_ID,USER_ID,LCK_Date,Est_Id)
 VALUES (docid,userid,SYSDATE,Estid);
 COMMIT;
 END Ins_LCK;
 END Ins_Lck_Pkg;

CREATE OR REPLACE PACKAGE BODY  "IPTYPEPKG" AS
 PROCEDURE ins_iptypeT1(IPP_ID IN IP_TYPE.IP_ID%TYPE,
 IPP_NAME IN IP_TYPE.IP_NAME%TYPE,
 IPP_PATH IN IP_TYPE.IP_PATH%TYPE,
 IPP_CHR IN IP_TYPE.IP_CHR%TYPE,
 IPP_DOCTYPEID IN IP_TYPE.IP_DOCTYPEID%TYPE,
 IPP_MOVE IN IP_TYPE.IP_MOVE%TYPE,
 IPP_VERIFY IN IP_TYPE.IP_VERIFY%TYPE,
 IPP_ACEPTBLANK IN IP_TYPE.IP_ACEPTBLANK%TYPE,
 IPP_BACKUP IN IP_TYPE.IP_BACKUP%TYPE,
 IPP_DELSOURCE IN IP_TYPE.IP_DELSOURCE%TYPE,
 IPP_SOURCEVARIABLE IN IP_TYPE.IP_SOURCEVARIABLE%TYPE,
 IPP_MULTIPLEFILES IN IP_TYPE.IP_MULTIPLEFILES%TYPE,
 IPP_MULTIPLECHR IN IP_TYPE.IP_MULTIPLECHR%TYPE)IS

 BEGIN
 INSERT INTO IP_TYPE(IP_ID,
 IP_NAME,
 IP_PATH,
 IP_CHR,
 IP_DOCTYPEID,
 IP_MOVE,
 IP_VERIFY,
 IP_ACEPTBLANK,
 IP_BACKUP,
 IP_DELSOURCE,
 IP_SOURCEVARIABLE,
 IP_MULTIPLEFILES,
 IP_MULTIPLECHR)
 VALUES(IPP_ID,
 IPP_NAME,
 IPP_PATH,
 IPP_CHR,
 IPP_DOCTYPEID,
 IPP_MOVE,
 IPP_VERIFY,
 IPP_ACEPTBLANK,
 IPP_BACKUP,
 IPP_DELSOURCE,
 IPP_SOURCEVARIABLE,
 IPP_MULTIPLEFILES,
 IPP_MULTIPLECHR);
 COMMIT;
END ins_iptypeT1;
END iptypepkg;

CREATE OR REPLACE PACKAGE BODY  "IP_HSTDELETE" AS
 PROCEDURE BorrarHistorial(HistoryId IN P_HST.ID%TYPE)IS
 BEGIN
 DELETE FROM P_HST WHERE ID =HistoryId;
 COMMIT;
 END Borrarhistorial;
END Ip_Hstdelete;
CREATE OR REPLACE PACKAGE BODY  "IP_HST_PKG" AS
 PROCEDURE IP_HST1(HST_ID IN IP_HST.HST_ID%TYPE,
 IP_ID IN IP_HST.IP_ID%TYPE,
 IPDate IN IP_HST.IPDATE%TYPE,
 IPUserId IN IP_HST.IPUSERID%TYPE,
 IPDocCount IN IP_HST.IPDOCCOUNT%TYPE,
 IPIndexCount IN IP_HST.IPINDEXCOUNT%TYPE,
 IPResult IN IP_HST.IPRESULT%TYPE,
 IPLINESCOUNT IN IP_HST.IPLINESCOUNT%TYPE,
 IPERRORCOUNT IN IP_HST.IPERRORCOUNT%TYPE,
 IPPATH IN IP_HST.IPPATH%TYPE,
 IPHASH IN IP_HST.IP_HASH%Type) IS
 BEGIN
 INSERT INTO IP_HST
 VALUES(HST_ID,IP_ID, IPDate, IPUserId, IPDocCount, IPIndexCount, IPResult,IPLINESCOUNT,IPERRORCOUNT,IPPATH,IPHASH);
 COMMIT;
 END IP_HST1;
END IP_HST_pkg;

CREATE OR REPLACE PACKAGE BODY  "IP_PROCESSTASK_PKG" AS
 PROCEDURE IP_PROCTASK(vdia IN IP_PROCESSTASK.DIA%TYPE,
 vhora IN IP_PROCESSTASK.HORA%TYPE,
 IDprocess IN IP_PROCESSTASK.ID_PROCESS%TYPE)IS
 BEGIN
 UPDATE IP_PROCESSTASK SET ID_PROCESS = IDProcess, DIA = vdia, HORA = vhora;
 COMMIT;
 END IP_PROCTASK;
 END Ip_Processtask_Pkg;

CREATE OR REPLACE PACKAGE BODY 
  "MODVOLUME_RETRIEVEVOLUMEID_PKG" AS
 PROCEDURE retrieveVolumeId (DocTypeId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DISK_VOLUME.Disk_Vol_Id,
 DISK_VOLUME.Disk_Vol_Name,
 DISK_VOLUME.Disk_Vol_Size,
 DISK_VOLUME.Disk_Vol_Type,
 DISK_VOLUME.Disk_Vol_Copy,
 DISK_VOLUME.Disk_Vol_Path,
 DISK_VOLUME.Disk_Vol_Size_Len,
 DISK_VOLUME.Disk_Vol_State,
 DISK_VOLUME.Disk_Vol_LstOffset
 FROM DOC_TYPE, DISK_VOLUME
 WHERE DOC_TYPE.Disk_Group_ID = DISK_VOLUME.Disk_Vol_Id AND
 DOC_TYPE.Doc_Type_Id = DocTypeId;

 io_cursor := v_cursor;
 END retrieveVolumeId;
END MODVOLUME_RETRIEVEVOLUMEID_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "MOD_RETRIEVEVOLUMEPATH_PKG" AS
 PROCEDURE retrieveVolumePath (DocTypeId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DISK_VOLUME.Disk_Vol_Id,
 DISK_VOLUME.Disk_Vol_Name,
 DISK_VOLUME.Disk_Vol_Size,
 DISK_VOLUME.Disk_Vol_Type,
 DISK_VOLUME.Disk_Vol_Copy,
 DISK_VOLUME.Disk_Vol_Path,
 DISK_VOLUME.Disk_Vol_Size_Len,
 DISK_VOLUME.Disk_Vol_State,
 DISK_VOLUME.Disk_Vol_LstOffset

 FROM DOC_TYPE , DISK_VOLUME
 WHERE DOC_TYPE.Disk_Group_ID = DISK_VOLUME.Disk_Vol_Id AND
 DOC_TYPE.Doc_Type_Id = DocTypeId;
 io_cursor := v_cursor;
 END retrieveVolumePath;
END MOD_RETRIEVEVOLUMEPATH_PKG;

CREATE OR REPLACE PACKAGE BODY  "RETRIEVEVOLUMEPATH_PKG" 
 AS
 PROCEDURE retrieveVolumePath (DocTypeId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DISK_VOLUME.Disk_Vol_Id,
 DISK_VOLUME.Disk_Vol_Name,
 DISK_VOLUME.Disk_Vol_Size,
 DISK_VOLUME.Disk_Vol_Type,
 DISK_VOLUME.Disk_Vol_Copy,
 DISK_VOLUME.Disk_Vol_Path,
 DISK_VOLUME.Disk_Vol_Size_Len,
 DISK_VOLUME.Disk_Vol_State,
 DISK_VOLUME.Disk_Vol_LstOffset
 FROM DOC_TYPE ,DISK_VOLUME
 WHERE DOC_TYPE.Disk_Group_ID = DISK_VOLUME.Disk_Vol_Id AND
 DOC_TYPE.Doc_Type_Id = DocTypeId;
 io_cursor := v_cursor;
 END retrieveVolumePath;
END RETRIEVEVOLUMEPATH_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "SEARCH_FILLMYTREEVIEW_PKG" AS
 PROCEDURE fillMyTreeView (UserId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DOC_TYPE_GROUP.Doc_Type_Group_ID,
 DOC_TYPE_GROUP.Doc_Type_Group_Name,
 DOC_TYPE_GROUP.Icon,
 DOC_TYPE_GROUP.Parent_Id,
 DOC_TYPE_GROUP.Object_Type_Id,
 USER_RIGHTS.User_Id,
 USER_RIGHTS.User_Rights_Type_Id,
 USER_RIGHTS.Right_Value
 FROM DOC_TYPE_GROUP,USER_RIGHTS
 WHERE DOC_TYPE_GROUP.Doc_Type_Group_ID = USER_RIGHTS.Object_Id AND
 DOC_TYPE_GROUP.Object_Type_Id = USER_RIGHTS.Object_Type_ID and
 USER_RIGHTS.User_Id = UserID AND
 USER_RIGHTS.User_Rights_Type_Id = 1 AND
 USER_RIGHTS.Right_Value = 1
 ORDER BY DOC_TYPE_GROUP.Doc_Type_Group_ID;
 io_cursor := v_cursor;
 END fillMyTreeView;
END SEARCH_FILLMYTREEVIEW_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "SEARCH_GENERACIONINDICES_PKG" AS
 PROCEDURE generacionIndices (UserId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT
 DOC_TYPE_GROUP.Doc_Type_Group_ID,
 DOC_TYPE_GROUP.Doc_Type_Group_Name,
 DOC_TYPE_GROUP.Icon,
 DOC_TYPE_GROUP.Parent_Id,
 DOC_TYPE_GROUP.Object_Type_Id,
 USER_RIGHTS.User_Id,
 USER_RIGHTS.User_Rights_Type_Id,
 USER_RIGHTS.Right_Value
 FROM DOC_TYPE_GROUP,USER_RIGHTS
 WHERE DOC_TYPE_GROUP.Doc_Type_Group_ID = USER_RIGHTS.Object_Id AND
 DOC_TYPE_GROUP.Object_Type_Id = USER_RIGHTS.Object_Type_ID AND
 USER_RIGHTS.User_Id = UserID AND USER_RIGHTS.User_Rights_Type_Id = 1 AND
 USER_RIGHTS.Right_Value = 1
 ORDER BY DOC_TYPE_GROUP.Doc_Type_Group_ID;
 io_cursor := v_cursor;
 END generacionIndices;
END SEARCH_GENERACIONINDICES_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "SEARCH_TREEVIEWAFTERSELECT_PKG" AS
 PROCEDURE treeViewAfterSelect (GlobalUserId IN NUMBER, DocGroupId IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DISTINCT DOC_TYPE.Doc_Type_Name, DOC_TYPE.LIFE_CYCLE, DOC_TYPE_R_DOC_TYPE_GROUP.DOC_TYPE_GROUP, DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Type_Id, DOC_TYPE.Icon_Id

 FROM DOC_TYPE, DOC_TYPE_R_DOC_TYPE_GROUP, USER_RIGHTS
 WHERE DOC_TYPE.Doc_Type_Id = DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Type_Id AND
 DOC_TYPE.Doc_Type_Id = USER_RIGHTS.Object_Id AND
 DOC_TYPE.Object_Type_Id = USER_RIGHTS.Object_Type_ID AND
 DOC_TYPE_R_DOC_TYPE_GROUP.DOC_TYPE_GROUP = DocGroupId AND USER_RIGHTS.User_Rights_Type_Id = 1 AND
 USER_RIGHTS.Right_Value = 1 AND
 USER_RIGHTS.User_Id = GlobalUserID
 ORDER BY DOC_TYPE.Doc_Type_Name;
 io_cursor := v_cursor;
 END treeViewAfterSelect;
END Search_Treeviewafterselect_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SELALL_LCK_PKG" AS
PROCEDURE Selall_LCK(io_cursor OUT t_cursor) IS
 s_cursor t_cursor;
BEGIN
 OPEN s_cursor FOR SELECT * FROM LCK;
 io_cursor := s_cursor;
END Selall_LCK;
END Selall_Lck_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SELECCIONAR_PKG" AS
 PROCEDURE seleccionar(I IN IP_INDEX.IP_ID%TYPE,io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT IP_ID, ARRAY_ID, INDEX_ID, INDEX_ORDER FROM IP_INDEX WHERE IP_ID = I ORDER BY INDEX_ORDER ASC;
 io_cursor := v_cursor;
 END seleccionar;
END Seleccionar_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SELECTALLINDEX_PKG" AS
 PROCEDURE Selectallindex(I IN IP_INDEX.IP_ID%TYPE, io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT * FROM IP_INDEX WHERE IP_ID =I ORDER BY INDEX_ORDER ASC;
 io_cursor := v_cursor;
 END Selectallindex;
END Selectallindex_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SELECTIPHST_PKG" AS
 PROCEDURE Selectiphst(I IN IP_HST.HST_ID%TYPE,io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT HST_ID,IP_ID, IPDate, IPUserId, IPDocCount, IPIndexCount, IPResult,IPLINESCOUNT,IPERRORCOUNT,IPPATH,RESULT
 FROM IP_HST, IP_RESULTS WHERE IP_HST.IPRESULT = IP_RESULTS.RESULT_ID AND HST_ID =I;
 io_cursor := v_cursor;
 END Selectiphst;
END Selectiphst_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SELECTIPID_PKG" AS
 PROCEDURE Selectipid(io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT IP_ID FROM IP_TYPE ORDER BY IP_ID DESC;
 io_cursor := v_cursor;
 END Selectipid;
END Selectipid_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SELECTLAST_VERREG_PKG" 
 AS
 PROCEDURE SelectLast_Verreg(idd IN VERREG.ID%TYPE,io_cursor OUT t_cursor)IS
 s_cursor t_cursor;
BEGIN
 OPEN s_cursor FOR
 SELECT * FROM
 (SELECT * FROM VERREG
 ORDER BY Id DESC)WHERE ROWNUM=1;
 io_cursor := s_cursor;
 COMMIT;
END SelectLast_Verreg;
END SelectLast_Verreg_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SELECTLIC_PKG" AS
 PROCEDURE SelectLic(io_cursor OUT t_cursor)IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT Numero_Licencias FROM LIC;
 io_cursor := v_cursor;
 END SelectLic;
END SelectLic_PKG;

CREATE OR REPLACE PACKAGE BODY  "SELECT_ESTREG_PKG" AS
 PROCEDURE Select_Estreg(idd IN ESTREG.ID%TYPE,io_cursor OUT t_cursor)IS
 s_cursor t_cursor;
BEGIN
 OPEN s_cursor FOR
 SELECT * FROM ESTREG
 WHERE ID=idd and ID>0;
 io_cursor := s_cursor;
 COMMIT;
END Select_Estreg;
END Select_Estreg_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SELECT_VERREG_PKG" AS
 PROCEDURE Select_Verreg(io_cursor OUT t_cursor)IS
 s_cursor t_cursor;
BEGIN
 OPEN s_cursor FOR
 SELECT * FROM VERREG;
 io_cursor := s_cursor;
 COMMIT;
END Select_Verreg;
END Select_Verreg_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SEL_LCK_PKG" AS
 PROCEDURE Sel_LCK(docid IN LCK.doc_ID%TYPE,io_cursor OUT t_cursor)IS
 s_cursor t_cursor;
 BEGIN
 OPEN s_cursor FOR
 SELECT doc_Id FROM LCK WHERE Doc_ID=docid;
 io_cursor := s_cursor;
 END Sel_Lck;
END Sel_Lck_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SEL_NAME_LCK_PKG" AS
 PROCEDURE Sel_Name_Lck(docid IN LCK.Doc_ID%TYPE,io_cursor OUT t_cursor)IS
 s_cursor t_cursor;
BEGIN
 OPEN s_cursor FOR
 SELECT USER_TABLE.User_Name FROM USER_TABLE,LCK WHERE LCK.Doc_ID=docid;
 io_cursor := s_cursor;
 COMMIT;
END Sel_Name_Lck;
END Sel_Name_Lck_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SETDOC_I58INBROKER_PKG" 
 AS
PROCEDURE SETDOC_I58INBROKER(NROSIN IN DOC_I58.I22%TYPE)IS
FecCierre1 DATE;
CodCliente1 NUMBER;
CodSeccion1 NUMBER;
CodPoliza1 NUMBER;
FecSiniestro1 DATE;
IdAseguradora1 NUMBER;

 BEGIN
 SELECT FECCIERRE,CODCLIENTE,CODSECCION,CODPOLIZA,FECSINIESTRO,IDASEGURADORA
 INTO FecCierre1,CodCliente1,CodSeccion1,CodPoliza1,FecSiniestro1,IdAseguradora1
 FROM SGSINIESTROSCONSULTA WHERE NROSINIESTRO=NROSIN;

 UPDATE DOC_I58 SET I23 = FecCierre1, I16 = CodCliente1, I46 = CodSeccion1,I18 = CodPoliza1, I27 = FecSiniestro1, I45 = IdAseguradora1
 WHERE I22 = NROSIN;
 COMMIT;
 END SETDOC_I58INBROKER;
END SETDOC_I58INBROKER_PKG;
CREATE OR REPLACE PACKAGE BODY  "SETORD_INBROKER_PKG" AS
 PROCEDURE SETORD_INBROKER(NROORDEN IN NUMERIC)IS
CLIENTE1 NUMERIC;
SECCION1 NUMERIC;
COMPANIA1 NUMERIC;
FECEMISION1 DATE;
USUARIO1 varchar(30);
 BEGIN
 SELECT IDCLIENTE,CODSECCION,CODASEGURADORA,FECEMISION
 INTO CLIENTE1,SECCION1,COMPANIA1,FECEMISION1
 FROM SGOPERACIONCONSULTA WHERE NROORDEN=IDOPERACION;

 SELECT USUARIOPROCESO
 INTO USUARIO1
 FROM SGOPERACIONOUT WHERE NROORDEN=IDOPERACION AND CODIGOCONTROL = 'ALT';

 UPDATE DOC_I233 SET I16 = CLIENTE1,I46 = SECCION1, I45 = COMPANIA1, I97 = FECEMISION1,
		 I173 = USUARIO1 WHERE I13 = NROORDEN;

 COMMIT;
 END SETORD_INBROKER;
END SETORD_INBROKER_PKG;
CREATE OR REPLACE PACKAGE BODY  "SETPOL_INBROKER_PKG" AS
PROCEDURE SETPOL_INBROKER(IDOP IN DOC_I57.I13%TYPE)IS
CODTIPOORDEN1 varchar(30);
CODSEGMENTACION11 varchar(30);
CODCONTROLINTERNOTRANSACCION1 varchar(30);
USUARIOPROCESO1 varchar(30);
FECEMISION1 DATE;
FECFACTURACION1 DATE;
CODASEGURADORA1 VARCHAR2(6);
CODSECCION1 VARCHAR2(3);
IDCLIENTE1 NUMBER(14);
CODPOLIZA1 VARCHAR2(25);
CODENDOSO1 VARCHAR2(15);
FECVIGENCIAINICIALOPERACION1 DATE;
FECVIGENCIAFINALOPERACION1 DATE;
IDPOLIZA1 NUMBER(14);
CANTREG NUMBER(14);

 BEGIN
 	SELECT COUNT(IDCLIENTE)
 	 into CANTREG
 FROM SG_OPERACIONTOTAL WHERE IDOPERACION=IDOP AND trim(CODCONTROLINTERNOTRANSACCION) ='001';
 IF CANTREG > 0 THEN

	 SELECT CODTIPOORDEN,USUARIOPROCESO,CODSEGMENTACION1,CODCONTROLINTERNOTRANSACCION,FECEMISION,FECFACTURACION,CODASEGURADORA,CODSECCION,IDCLIENTE,CODPOLIZA,CODENDOSO,FECVIGENCIAINICIALOPERACION,FECVIGENCIAFINALOPERACION,IDPOLIZA
 	INTO CODTIPOORDEN1,USUARIOPROCESO1,CODSEGMENTACION11,CODCONTROLINTERNOTRANSACCION1,FECEMISION1,FECFACTURACION1,CODASEGURADORA1,CODSECCION1,IDCLIENTE1,CODPOLIZA1,CODENDOSO1,FECVIGENCIAINICIALOPERACION1,FECVIGENCIAFINALOPERACION1,IDPOLIZA1
		FROM SG_OPERACIONTOTAL WHERE IDOPERACION=IDOP AND CODCONTROLINTERNOTRANSACCION = '001' AND rownum <= 1;

	 UPDATE DOC_I57 SET I172 = CODTIPOORDEN1, I173 = USUARIOPROCESO1, I151 = CODSEGMENTACION11, I174 = '001', I97 = FECEMISION1, I12 = FECFACTURACION1, I45 = CODASEGURADORA1, I46 = CODSECCION1, I16 = IDCLIENTE1, I18 = CODPOLIZA1 , I19 = CODENDOSO1 , I20 = FECVIGENCIAINICIALOPERACION1, I21 = FECVIGENCIAFINALOPERACION1, I17 = IDPOLIZA1
 	WHERE I13 = IDOP;

		COMMIT;
 ELSE
		SELECT COUNT(IDCLIENTE)
 	INTO CANTREG
 	FROM SG_OPERACIONTOTAL WHERE IDOPERACION=IDOP AND trim(CODCONTROLINTERNOTRANSACCION)='ALT';

	 IF CANTREG > 0 THEN

		 SELECT CODTIPOORDEN,USUARIOPROCESO,CODSEGMENTACION1,CODCONTROLINTERNOTRANSACCION,FECEMISION,FECFACTURACION,CODASEGURADORA,CODSECCION,IDCLIENTE,CODPOLIZA,CODENDOSO,FECVIGENCIAINICIALOPERACION,FECVIGENCIAFINALOPERACION,IDPOLIZA
 	 INTO CODTIPOORDEN1,USUARIOPROCESO1,CODSEGMENTACION11,CODCONTROLINTERNOTRANSACCION1,FECEMISION1,FECFACTURACION1,CODASEGURADORA1,CODSECCION1,IDCLIENTE1,CODPOLIZA1,CODENDOSO1,FECVIGENCIAINICIALOPERACION1,FECVIGENCIAFINALOPERACION1,IDPOLIZA1
		 FROM SG_OPERACIONTOTAL WHERE IDOPERACION=IDOP AND CODCONTROLINTERNOTRANSACCION = 'ALT' AND rownum <= 1;


		 UPDATE DOC_I57 SET I172 = CODTIPOORDEN1, I173 = USUARIOPROCESO1, I151 = CODSEGMENTACION11, I174 = 'ALT', I97 = FECEMISION1, I12 = FECFACTURACION1, I45 = CODASEGURADORA1, I46 = CODSECCION1, I16 = IDCLIENTE1, I18 = CODPOLIZA1 , I19 = CODENDOSO1 , I20 = FECVIGENCIAINICIALOPERACION1, I21 = FECVIGENCIAFINALOPERACION1, I17 = IDPOLIZA1
 		WHERE I13 = IDOP;

			COMMIT;

		ELSE
		 SELECT COUNT(IDCLIENTE)
		 INTO CANTREG
		 FROM SG_OPERACIONTOTAL WHERE IDOPERACION=IDOP AND (trim(CODCONTROLINTERNOTRANSACCION) <>'ALT' and trim(CODCONTROLINTERNOTRANSACCION) <>'001');

		 IF CANTREG > 0 THEN

			 SELECT CODTIPOORDEN,USUARIOPROCESO,CODSEGMENTACION1,CODCONTROLINTERNOTRANSACCION,FECEMISION,FECFACTURACION,CODASEGURADORA,CODSECCION,IDCLIENTE,CODPOLIZA,CODENDOSO,FECVIGENCIAINICIALOPERACION,FECVIGENCIAFINALOPERACION,IDPOLIZA
 	 INTO CODTIPOORDEN1,USUARIOPROCESO1,CODSEGMENTACION11,CODCONTROLINTERNOTRANSACCION1,FECEMISION1,FECFACTURACION1,CODASEGURADORA1,CODSECCION1,IDCLIENTE1,CODPOLIZA1,CODENDOSO1,FECVIGENCIAINICIALOPERACION1,FECVIGENCIAFINALOPERACION1,IDPOLIZA1
		 FROM SG_OPERACIONTOTAL WHERE IDOPERACION=IDOP AND rownum <= 1;


			 UPDATE DOC_I57 SET I172 = CODTIPOORDEN1, I173 = USUARIOPROCESO1, I151 = CODSEGMENTACION11, I174 = CODCONTROLINTERNOTRANSACCION1, I97 = FECEMISION1, I12 = FECFACTURACION1, I45 = CODASEGURADORA1, I46 = CODSECCION1, I16 = IDCLIENTE1, I18 = CODPOLIZA1 , I19 = CODENDOSO1 , I20 = FECVIGENCIAINICIALOPERACION1, I21 = FECVIGENCIAFINALOPERACION1, I17 = IDPOLIZA1
			 WHERE I13 = IDOP;

			 COMMIT;

			 ELSE
			 UPDATE DOC_I57 SET I174 = 'N/A' WHERE I13 = IDOP;
			 COMMIT;
		 END IF;
		 END IF;
	 END IF;

EXCEPTION
 WHEN INVALID_NUMBER THEN
 UPDATE DOC_I57 SET I19 = CODENDOSO1 WHERE I13 = IDOP;
 when OTHERS THEN
 UPDATE DOC_I57 SET I19 = CODENDOSO1 WHERE I13 = IDOP;
 END SETPOL_INBROKER;
END SETPOL_INBROKER_PKG;
CREATE OR REPLACE PACKAGE BODY  "SETRIGHTS_PKG" AS
PROCEDURE SetRights(UserID IN User_Rights.User_ID%TYPE,
 ObjectID In User_Rights.Object_ID%Type,
 Userrightstype In User_Rights.User_Rights_Type_Id%Type,
 ObjectType in User_Rights.Object_Type_Id%Type,
 RightValue in User_Rights.Right_Value%Type)IS
UltimoID NUMBER;
Cantidad NUMBER;
RightId NUMBER;
Begin
Select Count(*) into Cantidad from User_Rights where User_ID=UserID and Object_Id=ObjectId and User_Rights_Type_ID=Userrightstype and
 Object_type_Id=ObjectType;
 If Cantidad=0 then
 UPDATE OBJLASTID SET OBJECTID = OBJECTID + 1 WHERE OBJECT_TYPE_ID = 7;
 SELECT OBJECTID into UltimoID FROM OBJLASTID WHERE OBJECT_TYPE_ID = 7;
 Insert into User_Rights Values(UserID,ObjectID,Userrightstype,ObjectType,UltimoID,RightValue);
 Else
 Select right_id into rightid from User_Rights where User_ID=UserID and Object_Id=ObjectId and User_Rights_Type_ID=Userrightstype and
 Object_type_Id=ObjectType;
 Update User_Rights Set Right_Value=RightValue where Right_ID= RightID;
 End if;
Commit;
END SetRights;
END SetRights_Pkg;

CREATE OR REPLACE PACKAGE BODY  "SETSIN_INBROKER_PKG" 
 AS
PROCEDURE SETSIN_INBROKER(NROSINCOMPLETE IN DOC_I58.I22%TYPE, NROSINORIGINAL IN DOC_I58.I22%TYPE, NROPOL IN DOC_I58.I18%TYPE)IS
FecCierre1 DATE;
CodCliente1 NUMBER;
CodSeccion1 NUMBER;
CodPoliza1 VARCHAR2(25);
FecSiniestro1 DATE;
IdAseguradora1 NUMBER;
CANTREG NUMBER(14);

 BEGIN
 UPDATE DOC_I58 SET I22 = NROSINCOMPLETE, I18= NROPOL
 WHERE I22 = NROSINORIGINAL;

 COMMIT;

 	 SELECT COUNT(CODCLIENTE)
 INTO CANTREG
 FROM SGSINIESTROSCONSULTA WHERE NROSINIESTRO=NROSINCOMPLETE;

 IF CANTREG > 0 THEN
 SELECT FECCIERRE,CODCLIENTE,CODSECCION,CODPOLIZA,FECSINIESTRO,IDASEGURADORA
 INTO FecCierre1,CodCliente1,CodSeccion1,CodPoliza1,FecSiniestro1,IdAseguradora1
 FROM SGSINIESTROSCONSULTA WHERE NROSINIESTRO=NROSINCOMPLETE;

 UPDATE DOC_I58 SET I23 = FecCierre1, I16 = CodCliente1, I46 = CodSeccion1,I18 = CodPoliza1, I27 = FecSiniestro1, I45 = IdAseguradora1
 WHERE I22 = NROSINCOMPLETE;
 END IF;
 COMMIT;
 END SETSIN_INBROKER;
END SETSIN_INBROKER_PKG;
CREATE OR REPLACE PACKAGE BODY 
  "SHOWPROCESSHISTORY159_PKG" AS
 PROCEDURE showProcessHistory (ProcessID IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR SELECT P_HST.ID,
 P_HST.Process_Date ,
 P_HST.User_Id ,
 P_HST.TotalFiles ,
 P_HST.ProcessedFiles ,
 P_HST.Result_Id ,
 P_HST.SkipedFiles,
 P_HST.ErrorFiles,
 P_HST.Path,
 USRTABLE.Name,
 P_HST.Process_id,
 ip_results.Result ,
 P_HST.Hash,
 P_HST.logfile,
 P_HST.errorfile,
 P_HST.tempfile
 FROM P_HST,
 USRTABLE,
 IP_RESULTS
 WHERE
 P_HST.User_Id = USRTABLE.Id AND
 P_HST.process_ID = ProcessID AND
 IP_RESULTS.RESULT_ID = P_HST.RESULT_ID
 ORDER BY P_HST.ID DESC;
 io_cursor := v_cursor;
 END showProcessHistory;
END showprocesshistory159_pkg;
CREATE OR REPLACE PACKAGE BODY  "SHOWPROCESSHISTORY_PKG" 
 AS
 PROCEDURE showProcessHistory (ProcessID IN NUMBER, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR SELECT P_HST.ID,
 P_HST.Process_Date ,
 P_HST.User_Id ,
 P_HST.TotalFiles ,
 P_HST.ProcessedFiles ,
 P_HST.Result_Id ,
 P_HST.SkipedFiles,
 P_HST.ErrorFiles,
 P_HST.Path,
 USER_TABLE.User_Name,
 P_HST.Process_id,
 ip_results.Result ,
 P_HST.Hash,
 P_HST.logfile,
 P_HST.errorfile,
 P_HST.tempfile
 FROM P_HST,
 USER_TABLE,
 IP_RESULTS
 WHERE
 P_HST.User_Id = USER_TABLE.User_Id AND
 P_HST.process_ID = ProcessID AND
 IP_RESULTS.RESULT_ID = P_HST.RESULT_ID
 ORDER BY P_HST.ID DESC;
 io_cursor := v_cursor;
 END showProcessHistory;
END SHOWPROCESSHISTORY_PKG;

CREATE OR REPLACE PACKAGE BODY 
  "SHOWPROCESSHISTORY_PKG159" AS

PROCEDURE showProcessHistory159 (ClsIpJob1ID IN NUMBER,
io_cursor OUT t_cursor) IS

v_cursor t_cursor;

BEGIN

OPEN v_cursor FOR

SELECT
P_HST.ID,P_HST.Process_ID,P_HST.Process_Date,
P_HST.USER_ID,P_HST.TotalFiles, P_HST.Processedfiles,

IP_RESULTS.RESULT , P_HST.RESULT_Id,
P_HST.TotalFiles,P_HST.SkipedFiles,P_HST.PATH,USRTABLE.Name

FROM P_HST , USRTABLE, IP_RESULTS

WHERE

P_HST.User_Id = USRTABLE.Id AND

P_HST.ID = ClsIpJob1ID AND
IP_RESULTS.RESULT_ID = P_HST.RESULT_Id

ORDER BY P_HST.ID DESC;

io_cursor := v_cursor;

END showProcessHistory159;

END SHOWPROCESSHISTORY_PKG159;

CREATE OR REPLACE PACKAGE BODY  "UCM_PKG" AS
 PROCEDURE getConID (conid OUT INT)
 IS
 CURSOR v_cursor IS SELECT con_id FROM UCM ORDER BY con_id;
 v_conid INT;
 i INT:=1;
 flag INT;
 BEGIN
 OPEN v_cursor;
 LOOP
 FETCH v_cursor INTO v_conid;
 IF v_conid <> i THEN
 conid:=i;
 flag:=1;
 END IF;
 EXIT WHEN v_cursor%NOTFOUND OR flag = 1;
 i:=i+1;
 END LOOP;

 IF v_conid=i THEN
 conid:=i+1;
 END IF;
 CLOSE v_cursor;
 END getConID; /*fin del procedimiento*/
 END Ucm_Pkg; /*fin del paquete*/

CREATE OR REPLACE PACKAGE BODY  "UPDATEDATA_PKG" AS
PROCEDURE UpdateData (VolumeId IN NUMBER, FileSize IN DECIMAL)
IS
 totalfiles NUMBER;
 totalsize DECIMAL;
BEGIN
 SELECT DISK_VOL_FILES INTO totalfiles FROM DISK_VOLUME WHERE DISK_VOL_ID = VolumeId;
 SELECT DISK_VOL_SIZE_LEN INTO totalsize FROM DISK_VOLUME WHERE DISK_VOL_ID = VolumeId;
 UPDATE DISK_VOLUME SET DISK_VOL_FILES = totalfiles + 1, DISK_VOL_SIZE_LEN = totalsize + FileSize WHERE DISK_VOL_ID = VolumeId;
END UpdateData;
END UpdateData_Pkg;

CREATE OR REPLACE PACKAGE BODY  "UPDATEVOLDELFILE_PKG" 
 AS
PROCEDURE UPDATEVOLDELFILE (VolumeId IN NUMBER, FileSize IN DECIMAL)
IS
 totalfiles NUMBER;
 totalsize DECIMAL;
BEGIN
 SELECT DISK_VOL_FILES INTO totalfiles FROM DISK_VOLUME WHERE DISK_VOL_ID = VolumeId;
 SELECT DISK_VOL_SIZE_LEN INTO totalsize FROM DISK_VOLUME WHERE DISK_VOL_ID = VolumeId;
 UPDATE DISK_VOLUME SET DISK_VOL_FILES = totalfiles - 1, DISK_VOL_SIZE_LEN = totalsize - FileSize WHERE DISK_VOL_ID = VolumeId;
END UPDATEVOLDELFILE;
END UpdateVoldelFile_Pkg;

CREATE OR REPLACE PACKAGE BODY  "UPDATE_DISKVOLSTATE_PKG"
 AS
 PROCEDURE Update_diskvolstate(Volid IN DISK_VOLUME.DISK_VOL_ID%TYPE)IS
 BEGIN
 UPDATE DISK_VOLUME SET DISK_VOL_STATE = 1 WHERE DISK_VOL_ID = VolId;
 COMMIT;
 END Update_diskvolstate;
 END Update_diskvolstate_Pkg;

CREATE OR REPLACE PACKAGE BODY  "UPDATE_DISKVOLUME_PKG" 
 AS
 PROCEDURE Update_diskvolume(VSIZE IN DISK_VOLUME.DISK_VOL_SIZE%TYPE,
 Actualfiles IN DISK_VOLUME.DISK_VOL_FILES%TYPE,
 volumeid IN DISK_VOLUME.DISK_VOL_ID%TYPE)IS
 BEGIN
 UPDATE DISK_VOLUME SET Disk_Vol_Size_Len = VSIZE, Disk_Vol_Files =ActualFiles WHERE Disk_Vol_Id =volumeid;
 COMMIT;
 END Update_diskvolume;
 END Update_diskvolume_Pkg;

CREATE OR REPLACE PACKAGE BODY  "UPDATE_DISKVOLUSED_PKG" 
 AS
 PROCEDURE Update_diskvolused(VolId IN DISK_VOLUME.disk_vol_ID%TYPE,
 Lastoffsetused IN DISK_VOLUME.disk_vol_lstoffset%TYPE)IS
 BEGIN
 UPDATE DISK_VOLUME SET DISK_VOL_LSTOFFSET =LastOffsetUsed WHERE DISK_VOL_ID =VolId;
 COMMIT;
 END Update_diskvolused;
 END Update_diskvolused_Pkg;

CREATE OR REPLACE PACKAGE BODY  "UPDATE_DOCNOTES_PKG" AS
 PROCEDURE Update_docnotes(nota IN DOC_NOTES.Note_Text%TYPE,
 posX IN DOC_NOTES.x_position%TYPE,
 posY IN DOC_NOTES.Y_position%TYPE,
 notaid IN DOC_NOTES.Note_ID%TYPE)IS


 BEGIN
 UPDATE DOC_NOTES SET NOTE_TEXT=nota, X_Position=posX, Y_Position=posY WHERE Note_Id=notaid;
 COMMIT;
 END Update_docnotes;
 END Update_docnotes_Pkg;

CREATE OR REPLACE PACKAGE BODY  "UPDATE_IPFOLDERBACK_PKG"
 AS
 PROCEDURE Update_ipfolderback(vcarpeta IN IP_FOLDERBACKUP.carpeta_Backup%TYPE,
 v_antes IN IP_FOLDERBACKUP.antes%TYPE,
 idcarpeta IN IP_FOLDERBACKUP.Id_carpeta%TYPE)IS
 BEGIN
 UPDATE IP_FOLDERBACKUP SET Carpeta_BackUp=vcarpeta, Antes = v_antes WHERE Id_Carpeta = idcarpeta;
 COMMIT;
 END Update_ipfolderback;
 END Update_ipfolderback_Pkg;

CREATE OR REPLACE PACKAGE BODY  "UPDATE_IPFOLDER_PKG" AS
 PROCEDURE Update_ipfolder(vnom IN IP_FOLDER.nombre%TYPE,
 vpath IN IP_FOLDER.path%TYPE,
 cod IN IP_FOLDER.ID%TYPE)IS
 BEGIN
 UPDATE IP_FOLDER SET Nombre=vnom, Path=vpath WHERE ID=cod;
 COMMIT;
 END Update_ipfolder;
 END Update_ipfolder_Pkg;

CREATE OR REPLACE PACKAGE BODY  "UPDATE_LASTOBJ_PKG" AS
 PROCEDURE Update_lastobj(idobjeto IN OBJLASTID.OBJECTID%TYPE,
 tipo IN OBJLASTID.OBJECT_TYPE_ID%TYPE)IS

 BEGIN
 UPDATE OBJLASTID SET OBJECTID = idobjeto WHERE OBJECT_TYPE_ID = tipo;
 COMMIT;
 END Update_lastobj;
 END Update_lastobj_Pkg;

CREATE OR REPLACE PACKAGE BODY  "UPDATE_PROCESS_HST_PKG" 
 as
 procedure UpdateProcHst(HID in p_hst.id%type,
 totfiles in p_hst.Totalfiles%type,
 procfiles in p_hst.ProcessedFiles%type,
 skpfiles in p_hst.SkipedFiles%type,
 ErrFiles in p_hst.ErrorFiles%type,
 RID in p_hst.Result_id%type,
 hsh in p_hst.hash%type)
 is
 begin
 UPDATE P_HST SET
 TotalFiles = TotFiles,
 ProcessedFiles = ProcFiles,
 SkipedFiles = SkpFiles,
 Result_ID = RID,
 ERRORFiles = ErrFiles ,
 HASH= Hsh
 where ID = HId;
 end;
end;

CREATE OR REPLACE PACKAGE BODY 
 "UPDATE_SCANNEDBARCODE_PKG" AS
 PROCEDURE Update_barcode(caratulaid IN zbarcode.id%TYPE,
 lote IN zbarcode.batch%TYPE,
 caja IN zbarcode.box%TYPE) IS
 BEGIN
 UPDATE zbarcode SET scanned='SI', scanneddate=sysdate, batch=lote, box=caja WHERE
id = caratulaid;
 COMMIT;
END Update_barcode;
END UPDATE_SCANNEDBARCODE_PKG;
CREATE OR REPLACE PACKAGE BODY  "UPDATE_USERRIGHTS_PKG" 
 AS
 PROCEDURE Update_User_Right(Rightv IN USER_RIGHTS.Right_Value%TYPE,
 Rightid IN USER_RIGHTS.Right_Id%TYPE)IS
 BEGIN
 UPDATE USER_RIGHTS SET Right_Value =Rightv WHERE Right_Id =Rightid;
 COMMIT;
 END Update_User_right;
 END Update_Userrights_Pkg;

CREATE OR REPLACE PACKAGE BODY  "ZDOCINDGET_PKG" as 
 Procedure ZDIndGetDdownByInd(indexid in doc_Index.index_id%type, io_cursor OUT t_refcur) is v_cursor t_RefCur; 
 Begin 
 Open v_cursor 
 FOR Select Dropdown from doc_Index where index_id=indexid; 
 io_cursor := v_cursor; 
 End ZDIndGetDdownByInd; 
 Procedure ZDIndGetCantByNameId(IndexName in Doc_Index.index_name%type, IndexId in Doc_Index.index_id%type, io_cursor OUT t_refcur) is v_cursor t_RefCur; 
 Begin 
 Open v_cursor 
 FOR Select count(*) from Doc_index where Index_name=IndexName and Index_id <>IndexId; 
 io_cursor := v_cursor; 
 End ZDIndGetCantByNameId; 
 End ZDocIndGet_pkg;
CREATE OR REPLACE PACKAGE BODY  "ZDTINS_PKG" AS 
PROCEDURE ZDtInsDtAssociated(DoctypeId1 in Doctypes_associated.DoctypeId1%type, Index1 in Doctypes_associated.Index1%type, doctypeid2 in Doctypes_associated.doctypeid2%type, index2 in Doctypes_associated.index2%type) 
IS BEGIN 
Insert into Doctypes_associated(doctypeid1,doctypeid2,index1,Index2) 
values(doctypeid1,doctypeid2,index1,Index2); 
END ZDtInsDtAssociated; 
END ZDtIns_Pkg;
CREATE OR REPLACE PACKAGE BODY 
  "ZGETINTEGRIDADINDICES_PKG" as procedure 
 ZGetColumnsDoc_D(io_cursor out t_cursor) IS v_cursor 
 t_cursor; begin open v_cursor 
 for 
 select replace(COLUMN_NAME,'D',''),replace(TABLE_NAME,'DOC_D',
 '') 
 from user_tab_columns 
 where TABLE_NAME like'DOC_D%' 
 and COLUMN_NAME like 'D%' order by TABLE_NAME,COLUMN_NAME; 
 io_cursor:=v_cursor; end ZGetColumnsDoc_D; procedure 
 ZGetColumnsDoc_I (io_cursor out t_cursor) IS v_cursor 
 t_cursor; begin open v_cursor 
 for 
 select replace(COLUMN_NAME,'I',''),replace(TABLE_NAME,'DOC_I',
 '') 
 from user_tab_columns 
 where TABLE_NAME like'DOC_I%' 
 and COLUMN_NAME like 'I%' order by TABLE_NAME,COLUMN_NAME; 
 io_cursor:=v_cursor; end ZGetColumnsDoc_I; procedure 
 ZGetAllDrI (io_cursor out t_cursor) IS v_cursor t_cursor; 
 begin open v_cursor 
 for 
 select index_id, doc_type_id 
 from INDEX_R_DOC_TYPE order by doc_type_id, index_id; 
 io_cursor:=v_cursor; end ZGetAllDrI; end 
 ZGetIntegridadIndices_pkg;
CREATE OR REPLACE PACKAGE BODY  "ZGETUSERRIGTH_PKG" as 
 Procedure GetArchivosUserRight(UserId in 
 usr_rights_view.user_id%type, io_cursor out t_cursor) is 
 v_cursor t_cursor; begin open v_cursor 
 for 
 SELECT distinct(dtg.Doc_Type_Group_ID),
 dtg.Doc_Type_Group_Name,dtg.Icon,dtg.Parent_Id,
 dtg.Object_Type_Id,urv.User_Id,urv.Right_Type 
 
 FROM DOC_TYPE_GROUP dtg, USR_RIGHTS_VIEW urv 
 
 WHERE dtg.Doc_Type_Group_ID = urv.Aditional 
 AND dtg.Object_Type_Id = urv.ObjectID 
 and urv.User_Id =Userid ORDER BY 
 dtg.Doc_Type_Group_ID; io_cursor:=v_cursor; end 
 GetArchivosUserRight; end ZGetUserRigth_pkg;
CREATE OR REPLACE PACKAGE BODY  "ZINDLNKINF_PKG" AS 
 PROCEDURE ZIndLnkInfInsRow(pId IN index_link_info.Id%type ,
 pData IN index_link_info.Data%type, pFlag IN 
 index_link_info.Flag%type, pDocType IN 
 index_link_info.DocType%type ,
 pDocIndex IN index_link_info.DocIndex%type, pName IN 
 index_link_info.Name%type) IS BEGIN 
 insert into index_link_info(id,data,flag,doctype,docindex,
 name ) values(pId,pData,pFlag,pDocType,pDocIndex, pName); 
 END ZIndLnkInfInsRow; END ZIndLnkInf_Pkg;
CREATE OR REPLACE PACKAGE BODY  "ZINDRDTUPD_PKG" as 
 Procedure ZIndRDtUpdByDtIDIndID(DocTypeId in Index_R_Doc_Type.Doc_Type_ID%type, IndexId in Index_R_Doc_Type.Index_Id%type) 
 is Begin 
 Update Index_R_Doc_Type set Mustcomplete=1, ShowLotus=1, LoadLotus=1 
 where Doc_Type_ID=DocTypeId and Index_Id=IndexId; 
 End ZIndRDtUpdByDtIDIndID; 
 
 Procedure ZIndRDtUpdByDtIDIndID2(DocTypeId in Index_R_Doc_Type.Doc_Type_ID%type, IndexId in Index_R_Doc_Type.Index_Id%type) is 
 Begin 
 Update Index_R_Doc_Type set Mustcomplete=1, ShowLotus=1 where Doc_Type_ID=DocTypeId and Index_Id=IndexId; 
 End ZIndRDtUpdByDtIDIndID2; 
 
 End ZIndRDtUpd_pkg;
CREATE OR REPLACE PACKAGE BODY  "ZSP_BARCODE_100" as 
 PROCEDURE InsertBarCode(idbarcode IN ZBarCode.ID%TYPE, 
 DocTypeId IN ZBarCode.doc_type_id%TYPE, UserId in 
 ZBarCode.Userid%type,Doc_id in ZBarCode.Doc_Id%type) IS BEGIN
 Insert into ZBarCode(Id,Fecha,Doc_Type_ID,UserId,Doc_Id) 
 Values(idbarcode,Sysdate,DocTypeId,Userid,Doc_id); COMMIT; 
 END InsertBarCode; PROCEDURE UpdBarCode(caratulaid IN 
 zbarcode.id%TYPE,lote IN zbarcode.batch%TYPE,caja IN 
 zbarcode.box%TYPE) IS BEGIN 
 UPDATE zbarcode 
 SET scanned='SI', scanneddate=sysdate, batch=lote, box=caja 
 WHERE id = caratulaid; COMMIT; END UpdBarCode; end 
 zsp_barcode_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_CONNECTION_100" as
 PROCEDURE CountConnections (io_cursor OUT t_cursor) IS 
 v_cursor t_cursor; BEGIN OPEN v_cursor 
 FOR 
 SELECT count(CON_ID) 
 FROM UCM; io_cursor := v_cursor; END 
 CountConnections;PROCEDURE DeleteConnection(conid IN 
 UCM.CON_ID%TYPE ) IS BEGIN 
 DELETE FROM UCM 
 WHERE CON_ID = conid;COMMIT;END DeleteConnection;PROCEDURE 
 InsertNewConecction(v_userId IN UCM.USER_ID%TYPE, 
 v_win_User IN UCM.WINUSER%TYPE, v_win_Pc IN UCM.WINPC%TYPE,
 v_con_Id IN UCM.CON_ID%TYPE, v_timeout IN UCM.TIME_OUT%TYPE,
 WF IN UCM.Type%Type) IS BEGIN 
 INSERT INTO UCM(USER_ID,C_TIME, U_TIME, WINUSER,WINPC,CON_ID,
 TIME_OUT,Type) 
 VALUES (v_UserId,SYSDATE, SYSDATE,v_Win_User,v_Win_PC,
 v_con_Id,v_timeout,WF);COMMIT;END InsertNewConecction;end 
 zsp_connection_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_CONNECTION_300" 
 as PROCEDURE DeleteConnection(conid IN UCM.CON_ID%TYPE, winpc
 IN UCM.WINPC%TYPE) IS BEGIN 
 DELETE FROM UCM 
 WHERE CON_ID = conid 
 AND WINPC = winpc ;COMMIT; END DeleteConnection;end 
 zsp_connection_300;
CREATE OR REPLACE PACKAGE BODY  "ZSP_DOCASSOCIATED_100" 
 as
 PROCEDURE GetDocAssociatedById(pDoctypeId IN DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,io_cursor OUT t_cursor)
 IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor
 FOR
 Select count(*)
 from DOC_TYPE_R_DOC_TYPE 
 where DoctypeId1=pDocTypeId 
 or doctypeid2=pDocTypeId;
 io_cursor := v_cursor;
 END GetDocAssociatedById;
 procedure GetDocAssociatedId2ById1(DocTypeId IN DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,io_cursor OUT t_cursor)
 IS 
 v_cursor t_cursor; 
 BEGIN 
 OPEN v_cursor
 FOR 
 Select DoctypeId2 
 from DOC_TYPE_R_DOC_TYPE 
 where doctypeid1= DocTypeId;
 io_cursor:=v_cursor;
 END GetDocAssociatedId2ById1;
 PROCEDURE getDocTypesAsociated(DocTypeId numeric, UserId numeric,io_cursor OUT t_cursor)
 IS 
 v_cursor t_cursor; 
 BEGIN 
 OPEN v_cursor
 FOR 
 SELECT *
 FROM DOC_TYPE_R_DOC_TYPE
 WHERE DocTypeId1= DocTypeId AND DocTypeID2 IN (SELECT aditional FROM USR_RIGHTS WHERE objid=2 and rtype=1 and (groupid= UserId OR groupid IN (SELECT groupid FROM usr_r_group WHERE usrid= UserId)));
 io_cursor:=v_cursor;
 END getDocTypesAsociated;
end zsp_docassociated_100;
CREATE OR REPLACE PACKAGE BODY 
  "ZSP_DOCINDEX_200_LOADINDEX_PKG" AS
 Procedure zsp_docindex_200_LoadIndex(DocTypeId in Index_R_Doc_Type.Doc_Type_Id%type, io_cursor out t_cursor) is
 v_cursor t_cursor;
 begin
 open v_cursor for
 SELECT Doc_Index.Index_Id, Doc_Index.Index_Name, Doc_Index.Index_Type, Doc_Index.Index_Len,
 Doc_Index.Object_Type_Id, Index_R_Doc_Type.Orden, Index_R_Doc_Type.Doc_Type_Id, Doc_Index.DropDown
 FROM Doc_Index
 INNER JOIN Index_R_Doc_Type ON Doc_Index.Index_Id = Index_R_Doc_Type.Index_Id
 WHERE(Index_R_Doc_Type.Doc_Type_Id = DocTypeId) 
 ORDER BY Index_R_Doc_Type.Orden;
 io_cursor := v_cursor;
 
 End zsp_docindex_200_LoadIndex;
 End zsp_docindex_200_LoadIndex_pkg;
CREATE OR REPLACE PACKAGE BODY  "ZSP_DOCTYPES_100" as
 PROCEDURE CopyDocType(DocTypeId NUMBER,NewdocTypeId NUMBER, NewName VARCHAR2) IS BEGIN
 DECLARE 
 FILEFORMATID NUMBER;
 DISKGROUPID NUMBER;
 THUMB NUMBER;
 ICONID NUMBER;
 CROSSREFERENCE NUMBER;
 LIFECYCLE NUMBER;
 OBJECTTYPEID NUMBER;
 ANAME VARCHAR2(255);
 BEGIN
 SELECT FILE_FORMAT_ID , Disk_Group_ID, THUMBNAILS, ICON_ID,
 CROSS_REFERENCE, LIFE_CYCLE, OBJECT_TYPE_ID,AUTONAME
 INTO FILEFORMATID, DISKGROUPID, THUMB, ICONID,
 CROSSREFERENCE, LIFECYCLE, OBJECTTYPEID, ANAME
 FROM DOC_TYPE
 WHERE DOC_TYPE_ID=DOCTYPEID;INSERT
 INTO DOC_TYPE(DOC_TYPE_ID,DOC_TYPE_NAME,FILE_FORMAT_ID,
 DISK_GROUP_ID,THUMBNAILS,ICON_ID,CROSS_REFERENCE,LIFE_CYCLE,
 OBJECT_TYPE_ID,AUTONAME,DOCUMENTALID) VALUES(NewDocTypeid,
 NewName,FILEFORMATID,DISKGROUPID,THUMB,ICONID,CROSSREFERENCE,
 LIFECYCLE,OBJECTTYPEID,ANAME,0);COMMIT;DELETE
 FROM TABLATEMP;INSERT
 INTO TABLATEMP(Index_ID,Doc_Type_ID,Orden,MUSTCOMPLETE,
 SHOWLOTUS,LOADLOTUS )
 SELECT Index_ID,Doc_Type_ID,Orden,MUSTCOMPLETE,SHOWLOTUS,
 LOADLOTUS
 FROM INDEX_R_DOC_TYPE
 WHERE DOC_TYPE_ID=DocTypeID;UPDATE TABLATEMP
 SET Doc_Type_ID=NewDocTypeId;COMMIT;INSERT
 INTO INDEX_R_DOC_TYPE(Index_ID,Doc_Type_ID,Orden,MUSTCOMPLETE,
 SHOWLOTUS,LOADLOTUS )
 SELECT Index_ID,Doc_Type_ID,Orden,MUSTCOMPLETE,SHOWLOTUS,
 LOADLOTUS
 FROM TABLATEMP
 WHERE Doc_Type_ID=NewDocTypeID;COMMIT;DELETE
 FROM TABLATEMP;Commit;END;
 END CopyDocType;
 
 
 PROCEDURE FillMeTreeView (UserId IN NUMBER, io_cursor OUT t_cursor) 
 IS
 v_cursor t_cursor;BEGIN OPEN v_cursor
 FOR
 SELECT DOC_TYPE_GROUP.Doc_Type_Group_ID,
 DOC_TYPE_GROUP.Doc_Type_Group_Name, DOC_TYPE_GROUP.Icon,
 DOC_TYPE_GROUP.Parent_Id, DOC_TYPE_GROUP.Object_Type_Id,
 USR_RIGHTS.groupId, USR_RIGHTS.RType
 FROM DOC_TYPE_GROUP,USR_RIGHTS
 WHERE DOC_TYPE_GROUP.Doc_Type_Group_ID = USR_RIGHTS.aditional
 AND DOC_TYPE_GROUP.Object_Type_Id = USR_RIGHTS.ObjID
 and USR_RIGHTS.groupId = UserID
 AND USR_RIGHTS.RType = 1 ORDER BY
 DOC_TYPE_GROUP.Doc_Type_Group_ID;io_cursor := v_cursor;END
 FillMeTreeView;
 
 Procedure GetAllDocTypesIdNames(io_cursor OUT
 t_cursor)IS v_cursor t_cursor;Begin OPEN v_cursor
 FOR
 Select Doc_Type_ID,Doc_Type_Name
 from Doc_Type;io_cursor := v_cursor;End
 GetAllDocTypesIdNames;
 
 PROCEDURE GetDocumentActions
 (DocumentId IN NUMBER, io_cursor OUT t_cursor) IS v_cursor
 t_cursor;BEGIN OPEN v_cursor
 FOR
 SELECT USRTABLE.Name User_Name, USER_HST.Action_Date,
 OBJECTTYPES.OBJECTTYPES, RIGHTSTYPE.RIGHTSTYPE,
 USER_HST.Object_Id, User_hst.s_object_id
 FROM USER_HST , USRTABLE, OBJECTTYPES, RIGHTSTYPE
 WHERE USER_HST.User_Id = USRTABLE.Id
 AND USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId
 AND USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId
 AND Object_Id = DocumentId
 AND Object_Type_Id = 6 ORDER BY USER_HST.ACTION_DATE DESC,
 USER_HST.OBJECT_ID, USER_HST.S_OBJECT_ID;io_cursor :=
 v_cursor;END GetDocumentActions;
 
 PROCEDURE IncrementsDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,X IN
 Number) IS BEGIN
 Update Doc_Type
 Set DocCount=DocCount + X
 where Doc_Type_Id= DocID;END IncrementsDocType;
 
 PROCEDURE GetDocTypesByGroupId (DocGroupId IN NUMBER, io_cursor OUT
 t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor
 FOR
 SELECT DOC_TYPE.Doc_Type_Id, DOC_TYPE.Doc_Type_Name,
 DOC_TYPE.Object_Type_Id, DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Order,
 DOC_TYPE_R_DOC_TYPE_GROUP.DOC_TYPE_GROUP
 FROM DOC_TYPE ,DOC_TYPE_R_DOC_TYPE_GROUP
 WHERE DOC_TYPE.Doc_Type_Id =
 DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Type_Id
 AND DOC_TYPE_R_DOC_TYPE_GROUP.DOC_TYPE_GROUP = DocGroupId
 ORDER BY DOC_TYPE_R_DOC_TYPE_GROUP.Doc_Order;io_cursor :=
 v_cursor;END GetDocTypesByGroupId;
 
 procedure GetDiskGroupId(DocTypeId IN Doc_type.doc_type_Id%type,
 io_cursor OUT t_cursor) IS v_cursor t_cursor;BEGIN OPEN
 v_cursor
 FOR
 Select disk_group_id
 from doc_type
 where doc_type_id=DoctypeId;io_cursor:=v_cursor;END
 GetDiskGroupId;
 
 procedure GetAssociatedIndex(DocTypeId11 IN
 DOC_TYPE_R_DOC_TYPE.DoctypeId1%type,DocTypeId21 IN
 DOC_TYPE_R_DOC_TYPE.DoctypeId2%type,io_cursor OUT t_cursor)
 IS v_cursor t_cursor; BEGIN OPEN v_cursor
 FOR
 Select Index1,index2
 from DOC_TYPE_R_DOC_TYPE
 where doctypeid1=DocTypeId11
 and doctypeId2=DocTypeId21;io_cursor:=v_cursor;END
 GetAssociatedIndex;
 
 PROCEDURE UpdDocCountById(DocCount IN DOC_TYPE.DOCCOUNT%type,
 DocTypeId IN DOC_TYPE.DOC_TYPE_ID%type) 
 IS BEGIN
 Update DOC_TYPE
 set Doccount=DocCount
 where DOC_TYPE_ID=DocTypeId;END UpdDocCountById;
 
 PROCEDURE
 UpdMbById(TamArch IN DOC_TYPE.MB%type,DocTypeId IN
 DOC_TYPE.DOC_TYPE_ID%type) IS BEGIN
 Update doc_type
 set MB=(MB + TamArch)
 where Doc_type_Id= DocTypeId;END UpdMbById;
 
 PROCEDURE GetUserNameDocumentAction (DocumentId IN
 user_hst.Object_Id%type, io_cursor OUT t_cursor) IS v_cursor
 t_cursor;BEGIN OPEN v_cursor
 FOR
 SELECT USRTABLE.Nombres + ' ' + USRTABLE.apellido Usuario,
 USER_HST.Action_Date, OBJECTTYPES.OBJECTTYPES,
 RIGHTSTYPE.RIGHTSTYPE, USER_HST.Object_Id,
 User_hst.s_object_id
 FROM USER_HST, USRTABLE, OBJECTTYPES, RIGHTSTYPE
 WHERE USER_HST.User_Id = USRTABLE.Id
 AND USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId
 AND USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId
 AND Object_Id = DocumentId
 AND Object_Type_Id = 6 ORDER BY USER_HST.ACTION_DATE DESC,
 USER_HST.OBJECT_ID, USER_HST.S_OBJECT_ID;io_cursor :=
 v_cursor;END GetUserNameDocumentAction;END zsp_doctypes_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_DOCTYPES_200" AS
 PROCEDURE GetDocTypesByUserRights(userid IN usrtable.id%type,
 righttype IN Number, io_cursor OUT t_cursor)IS v_cursor 
 t_cursor; BEGIN OPEN v_cursor 
 FOR 
 Select * 
 from doc_type 
 where Doc_type_id in (Select distinct(aditional) 
 from usr_rights 
 where (GROUPID in (Select groupid 
 from usr_r_group 
 where usrid=userid) or groupId=userid) 
 And (objid=2 
 and rtype=righttype)) order by doc_type_name; io_cursor := 
 v_cursor; END GetDocTypesByUserRights; 
 
 PROCEDURE GetDocTypesByUserRightsAndZI(userid IN usrtable.id%type, righttype IN Number,fldrId IN Number, io_cursor OUT t_cursor)
 IS v_cursor t_cursor; 
 BEGIN 
 OPEN v_cursor FOR 
 Select * from doc_type where Doc_type_id in (
 Select distinct(aditional) from usr_rights where (GROUPID in (
 Select groupid from usr_r_group where usrid=userid) or groupId=userid) 
 And (objid=2 and rtype=righttype)) 
 and doc_type_id in (
 select DTID from zi where folderid = fldrId) 
 order by doc_type_name; 
 io_cursor := v_cursor; 
 
 END GetDocTypesByUserRightsAndZI; 
END zsp_doctypes_200;
CREATE OR REPLACE PACKAGE BODY  "ZSP_EXCEPTION_100" as 
 PROCEDURE DeleteExceptionTable IS BEGIN 
 Delete from Excep 
 where Fecha >(Sysdate-30);END DeleteExceptionTable;end 
 zsp_exception_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_GENERIC_100" as 
 Procedure ExecSqlString(strsql in Varchar2,io_cursor OUT 
 t_cursor) is v_ReturnCursor t_cursor;Begin Open 
 v_ReturnCursor 
 FOR strsql; io_cursor := v_Returncursor;End ExecSqlString;end
 zsp_generic_100;
CREATE OR REPLACE PACKAGE BODY 
  "ZSP_GETDOCCOUNTTOINDEX_PKG" AS
PROCEDURE GetDocCountToIndex(io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
	OPEN v_cursor FOR 
	SELECT count(*) from ZI inner join doc_type on ZI.DTID = doc_type.doc_type_id
	WHERE ZI.DocID not in (SELECT ResultId from ZSearchValues_DT);
io_cursor := v_cursor;
 END GetDocCountToIndex;
END ZSP_GETDOCCOUNTTOINDEX_PKG;
CREATE OR REPLACE PACKAGE BODY 
  "ZSP_GETDOCIDANDDTIDTOINDEX_PKG" AS
PROCEDURE GetDocIdAndDTIDtoIndex(io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
	SELECT ZI.DocId, ZI.DTID from ZI inner join doc_type on ZI.DTID = doc_type.doc_type_id
	where ZI.DocID not in 
		(SELECT ResultId from ZSearchValues_DT);
 io_cursor := v_cursor;
 END GetDocIdAndDTIDtoIndex;
END ZSP_GETDOCIDANDDTIDTOINDEX_PKG;
CREATE OR REPLACE PACKAGE BODY 
  "ZSP_GETMYMESSAGESNEW_PKG" AS PROCEDURE 
 zsp_GetMyMessagesNew(my_id IN MSG_DEST.user_id%type,io_cursor
 OUT t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor 
 for 
 Select msg_id 
 from MSG_DEST 
 where MSG_DEST.READ='0' 
 and user_id = my_id 
 and MSG_DEST.deleted='0';io_cursor := v_cursor; END 
 zsp_GetMyMessagesNew;	End zsp_GetMyMessagesNew_Pkg;
CREATE OR REPLACE PACKAGE BODY  "ZSP_IMPORTS_100" as 
 PROCEDURE DeleteHystory(HistoryId IN P_HST.ID%TYPE)IS BEGIN 
 
 DELETE FROM P_HST 
 WHERE ID =HistoryId; COMMIT; END DeleteHystory; 
 procedure InsertProcHistory (HID in p_hst.id%type,PID in p_hst.Process_id%type, PDATE in p_hst.Process_Date%type,USrid in p_hst.user_id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type,skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type,RID in p_hst.Result_id%type,Pth in p_hst.Path%type,hsh in p_hst.hash%type,tfile in p_hst.tempfile%type,efile in p_hst.errorfile%type, lfile in p_hst.logfile%type) is begin INSERT INTO P_HST(ID,Process_ID,Process_Date,User_Id,TotalFiles,ProcessedFiles,SkipedFiles,ErrorFiles,Result_Id,PATH, HASH,errorfile,tempfile,logfile)VALUES(HID,PID ,Pdate,UsrId,TotFiles,ProcFiles,SkpFiles,ErrFiles,RID,Pth,Hsh,efile,tfile,lfile);end InsertProcHistory;
 
 PROCEDURE InsertUserAction(
 AID IN USER_HST.ACTION_ID%TYPE , 
 AUSRID IN USER_HST.USER_ID%TYPE , 
 AOBJID IN USER_HST.USER_ID%TYPE , 
 AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE ,
 ATYPE IN USER_HST.ACTION_TYPE%TYPE,
 ACONID IN UCM.CON_ID%TYPE, 
 SOBJECTID IN USER_HST.S_OBJECT_ID%TYPE) 
 IS BEGIN 
 INSERT INTO USER_HST(ACTION_ID, USER_ID,ACTION_DATE, OBJECT_ID,OBJECT_TYPE_ID,ACTION_TYPE,S_Object_Id)
 VALUES (AID,AUSRID,sysdate,AOBJID,AOBJTID,ATYPE,SOBJECTID);
 IF AUSRID <> 9999 THEN 
 UPDATE UCM SET u_time = SYSDATE 
 WHERE con_id= ACONID;
 END IF;
 
 COMMIT;
 END InsertUserAction;
 
 
 
 
 
 PROCEDURE GetProcessHistory (ClsIpJob1ID IN NUMBER, io_cursor OUT t_cursor) IS v_cursor t_cursor;
 BEGIN OPEN v_cursor FOR 
 SELECT IP_HST.HST_ID,IP_HST.IP_ID,IP_HST.IPDate, IP_HST.IPUSERID,IP_HST.IPDocCount, IP_HST.IPIndexCount, IP_RESULTS.RESULT , IP_HST.IPRESULT, IP_HST.IPLINESCOUNT,IP_HST.IPERRORCOUNT,IP_HST.IPPATH,USRTABLE.Name FROM IP_HST , USRTABLE, IP_RESULTS WHERE IP_HST.IPUserId = USRTABLE.Id AND IP_HST.IP_ID = ClsIpJob1ID AND IP_RESULTS.RESULT_ID = IP_HST.IPRESULT ORDER BY IP_HST.HST_ID DESC; io_cursor := v_cursor; END GetProcessHistory; 
 
 procedure UpdProcHistory(HID in p_hst.id%type,totfiles in p_hst.Totalfiles%type,procfiles in p_hst.ProcessedFiles%type, skpfiles in p_hst.SkipedFiles%type,ErrFiles in p_hst.ErrorFiles%type, RID in p_hst.Result_id%type,hsh in p_hst.hash%type) is begin UPDATE P_HST SET TotalFiles = TotFiles,ProcessedFiles = ProcFiles,SkipedFiles = SkpFiles,Result_ID = RID,ERRORFiles = ErrFiles , HASH= Hsh where ID = HId;end UpdProcHistory;
 
 
 end zsp_imports_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_IMPORTS_200" as
 Procedure InsertUserAction(
 ACONID IN UCM.CON_ID%TYPE ,
 WIN_PC UCM.WINPC%TYPE ,
 AUSRID IN USER_HST.USER_ID%TYPE ,
 AOBJID IN USER_HST.OBJECT_ID%TYPE ,
 AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE ,
 ATYPE IN USER_HST.ACTION_TYPE%TYPE,
 SOBJECTID IN USER_HST.S_OBJECT_ID%TYPE,
 io_cursor out t_cursor)
 is
 v_cursor t_cursor;
 res number;
 begin
 res := fInsertUserAction(ACONID, WIN_PC, AUSRID , AOBJID, AOBJTID, ATYPE, SOBJECTID);

 if res=1 then
 open v_cursor for
 select 1 as resultado into res from usrtable where rownum < 2;
 elsif res=0 then
 open v_cursor for
 select 0 as resultado into res from usrtable where rownum < 2;
 elsif res=2 then
 open v_cursor for
 select 2 as resultado into res from usrtable where rownum < 2;
 elsif res=3 then
 open v_cursor for
 select 3 as resultado into res from usrtable where rownum < 2;
 end if;
 io_cursor:=v_cursor;

 end InsertUserAction;

 end zsp_imports_200;
CREATE OR REPLACE PACKAGE BODY  "ZSP_INDEX_100" as 
 PROCEDURE FillIndex (IPJOBDocTypeId IN NUMBER, io_cursor OUT 
 t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor 
 FOR 
 SELECT DOC_INDEX.Index_Id, DOC_INDEX.Index_Name, 
 DOC_INDEX.INDEX_TYPE, DOC_INDEX.Index_Len, DOC_INDEX.AutoFill,
 DOC_INDEX.NoIndex,DOC_INDEX.DropDown,DOC_INDEX.AutoDisplay,
 DOC_INDEX.Invisible,DOC_INDEX.Object_Type_Id 
 FROM INDEX_R_DOC_TYPE, DOC_INDEX 
 WHERE INDEX_R_DOC_TYPE.Index_Id = DOC_INDEX.Index_Id 
 AND INDEX_R_DOC_TYPE.Doc_Type_Id = IPJOBDocTypeId;io_cursor 
 := v_cursor;END FillIndex; PROCEDURE IndexGeneration 
 (DocTypeId IN NUMBER, io_cursor OUT t_cursor) IS v_cursor 
 t_cursor; BEGIN OPEN v_cursor 
 FOR 
 SELECT DOC_INDEX.Index_Id, DOC_INDEX.Index_Name, 
 DOC_INDEX.INDEX_TYPE, DOC_INDEX.Index_Len, DOC_INDEX.AutoFill,
 DOC_INDEX.NoIndex, DOC_INDEX.DropDown, 
 DOC_INDEX.AutoDisplay, DOC_INDEX.Invisible, 
 DOC_INDEX.Object_Type_Id, INDEX_R_DOC_TYPE.Doc_Type_Id, 
 INDEX_R_DOC_TYPE.Orden 
 FROM DOC_INDEX ,INDEX_R_DOC_TYPE 
 WHERE DOC_INDEX.Index_Id = INDEX_R_DOC_TYPE.Index_Id 
 AND Doc_Type_Id = DocTypeId ORDER BY ORDEN; io_cursor := 
 v_cursor; END IndexGeneration;PROCEDURE 
 GetIndexRDocType(DocID IN Doc_Type.Doc_Type_ID%TYPE,io_cursor
 OUT t_cursor)IS v_cursor t_cursor;BEGIN OPEN v_cursor 
 FOR 
 Select Index_Id, Doc_Type_ID 
 From Index_R_Doc_Type 
 where Doc_Type_ID=DocId;io_cursor := v_cursor;END 
 GetIndexRDocType;PROCEDURE GetDocTypeIndexs (DocTypeId IN 
 NUMBER, io_cursor OUT t_cursor) IS v_cursor t_cursor; BEGIN 
 OPEN v_cursor 
 FOR 
 SELECT DOC_INDEX.INDEX_ID,DOC_INDEX.INDEX_NAME,
 DOC_INDEX.INDEX_TYPE,DOC_INDEX.INDEX_LEN, 
 DOC_INDEX.Object_Type_Id, INDEX_R_DOC_TYPE.Orden,
 INDEX_R_DOC_TYPE.Doc_Type_Id 
 FROM DOC_INDEX, INDEX_R_DOC_TYPE 
 WHERE INDEX_R_DOC_TYPE.INDEX_ID = DOC_INDEX.Index_Id 
 AND INDEX_R_DOC_TYPE.Doc_Type_Id = DocTypeId ORDER BY 
 INDEX_R_DOC_TYPE.Orden;io_cursor := v_cursor; END 
 GetDocTypeIndexs;procedure GetIndexQtyByNameId (IndexName in 
 doc_Index.index_name%type, IndexId in doc_Index.index_id%type,
 io_cursor OUT t_cursor)IS v_cursor t_cursor;begin open 
 v_cursor 
 for 
 select count(*) 
 from Doc_index 
 where Index_name=IndexName 
 and Index_id <>IndexId;io_cursor:=v_cursor;end 
 GetIndexQtyByNameId;Procedure GetIndexDropDown(indexid in 
 doc_Index.index_id%type, io_cursor OUT t_cursor) is v_cursor 
 t_cursor;Begin Open v_cursor 
 FOR 
 Select Dropdown 
 from doc_Index 
 where index_id=indexid;io_cursor := v_cursor;End 
 GetIndexDropDown;procedure GetAllIndexRDocType (io_cursor out
 t_cursor) IS v_cursor t_cursor;begin open v_cursor 
 for 
 select index_id, doc_type_id 
 from INDEX_R_DOC_TYPE order by doc_type_id, 
 index_id;io_cursor:=v_cursor;end 
 GetAllIndexRDocType;procedure GetDoc_dColumns(io_cursor out 
 t_cursor) IS v_cursor t_cursor;begin open v_cursor 
 for 
 select replace(COLUMN_NAME,'D',''),replace(TABLE_NAME,'DOC_D',
 '')from user_tab_columns 
 where TABLE_NAME like'DOC_D%' 
 and COLUMN_NAME like 'D%' order by TABLE_NAME,
 COLUMN_NAME;io_cursor:=v_cursor;end GetDoc_dColumns;procedure
 GetDoc_iColumns (io_cursor out t_cursor) IS v_cursor 
 t_cursor;begin open v_cursor 
 for 
 select replace(COLUMN_NAME,'I',''),replace(TABLE_NAME,'DOC_I',
 '')from user_tab_columns 
 where TABLE_NAME like'DOC_I%' 
 and COLUMN_NAME like 'I%' order by TABLE_NAME,
 COLUMN_NAME;io_cursor:=v_cursor;end GetDoc_iColumns;PROCEDURE
 InsertLinkInfo(pId IN index_link_info.Id%type ,pData IN 
 index_link_info.Data%type, pFlag IN index_link_info.Flag%type,
 pDocType IN index_link_info.DocType%type,pDocIndex IN 
 index_link_info.DocIndex%type, pName IN 
 index_link_info.Name%type) IS BEGIN 
 insert into index_link_info(id,data,flag,doctype,docindex,
 name ) values(pId,pData,pFlag,pDocType,pDocIndex, pName);END 
 InsertLinkInfo;Procedure UpdIndexRDoctypeByDtInd(DocTypeId in
 Index_R_Doc_Type.Doc_Type_ID%type, IndexId in 
 Index_R_Doc_Type.Index_Id%type) is Begin 
 Update Index_R_Doc_Type 
 set Mustcomplete=1, ShowLotus=1, LoadLotus=1 
 where Doc_Type_ID=DocTypeId 
 and Index_Id=IndexId;End UpdIndexRDoctypeByDtInd;procedure 
 UpdIndexRDoctypeByDtInd2 (DocTypeId int, IndexId int )IS 
 begin 
 Update Index_R_Doc_Type 
 set Mustcomplete=1, ShowLotus=1 
 where Doc_Type_ID=DocTypeId 
 and Index_Id=IndexId;end UpdIndexRDoctypeByDtInd2;
 
 PROCEDURE GetIndexRightValues(user_id in USR_RIGHTS.groupid%type, io_cursor OUT t_cursor)
 IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 	SELECT * FROM ZIR WHERE UserId IN (
		SELECT DISTINCT GROUPID from USR_R_GROUP WHERE USRID = user_id);
 io_cursor:=v_cursor;
 END GetIndexRightValues;
 
 end zsp_index_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_INDEX_200" as 
 PROCEDURE GetDocTypeIndexs (DocTypeId IN NUMBER, io_cursor 
 OUT t_cursor) IS v_cursor t_cursor; BEGIN OPEN v_cursor 
 FOR 
 SELECT DOC_INDEX.INDEX_ID,DOC_INDEX.INDEX_NAME,
 DOC_INDEX.INDEX_TYPE,DOC_INDEX.INDEX_LEN, 
 DOC_INDEX.Object_Type_Id,INDEX_R_DOC_TYPE.Orden,
 INDEX_R_DOC_TYPE.Doc_Type_Id, Doc_Index.DropDown 
 FROM Doc_Index INNER JOIN Index_R_Doc_Type 
 ON Doc_Index.Index_Id = Index_R_Doc_Type.Index_Id 
 WHERE INDEX_R_DOC_TYPE.Doc_Type_Id = DocTypeId ORDER BY 
 INDEX_R_DOC_TYPE.Orden;io_cursor := v_cursor; END 
 GetDocTypeIndexs;end zsp_index_200;
CREATE OR REPLACE PACKAGE BODY  "ZSP_LICENSE_100" as 
 procedure GetActiveWFConect (io_cursor out t_cursor) IS 
 v_cursor t_cursor;BEGIN open v_cursor 
 for 
 Select Used 
 from LIC 
 where Type=1;end GetActiveWFConect;PROCEDURE 
 GetDocumentalLicenses(io_cursor OUT t_cursor)IS v_cursor 
 t_cursor;BEGIN OPEN v_cursor 
 FOR 
 SELECT Numero_Licencias 
 FROM LIC;io_cursor := v_cursor;END GetDocumentalLicenses;end 
 zsp_license_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_LOCK_100" as 
 PROCEDURE DeleteLocked(docid IN LCK.Doc_ID%TYPE,userid IN 
 LCK.USER_ID%TYPE,Estid IN LCK.EST_ID%TYPE) IS BEGIN 
 DELETE FROM LCK 
 WHERE Doc_ID=docid 
 AND User_ID=userid 
 AND Est_Id=estid;COMMIT;END DeleteLocked;PROCEDURE 
 GetBlockeds (io_cursor OUT t_cursor) IS v_cursor 
 t_cursor;BEGIN OPEN v_cursor 
 FOR 
 SELECT IP_TASK.Id AS ID, IP_TASK.File_Path AS Ruta, 
 IP_TASK.Zip_Origen AS Archivo_Zip 
 FROM IP_TASK ,IP_FOLDER 
 WHERE IP_TASK.Id_Configuracion = IP_FOLDER.Id 
 AND IP_TASK.Bloqueado = 1;io_cursor := v_cursor;END 
 GetBlockeds;PROCEDURE LockDocument(docid IN LCK.Doc_ID%TYPE ,
 Userid IN LCK.USER_ID%TYPE , Estid IN LCK.Est_Id%TYPE ) IS 
 BEGIN 
 INSERT INTO LCK(doc_ID,USER_ID,LCK_Date,Est_Id)VALUES (docid,
 userid,SYSDATE,Estid);COMMIT;END LockDocument;PROCEDURE 
 GetDocumentLockedState(docid IN LCK.doc_ID%TYPE,io_cursor OUT
 t_cursor) IS s_cursor t_cursor;BEGIN OPEN s_cursor 
 FOR 
 SELECT doc_Id 
 FROM LCK 
 WHERE Doc_ID=docid;io_cursor := s_cursor;END 
 GetDocumentLockedState;end zsp_lock_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_MESSAGES_100" as 
 PROCEDURE CountNewMessages(userId in numeric,io_cursor OUT 
 t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor 
 FOR 
 SELECT count(*) 
 FROM MSG_DEST 
 WHERE MSG_DEST.user_id=userid 
 AND MSG_DEST.deleted=0 
 and read=0;io_cursor := v_cursor;END 
 CountNewMessages;PROCEDURE DeleteRecivedMsg(m_id IN 
 MESSAGE.msg_id%TYPE, u_id IN MSG_DEST.user_id%TYPE) IS BEGIN 
 UPDATE MSG_DEST 
 SET deleted=1 
 WHERE msg_id=m_id 
 AND user_id=u_id;END DeleteRecivedMsg;PROCEDURE 
 DeleteSenderMsg(m_id IN MESSAGE.msg_id%TYPE) IS recived 
 NUMERIC;BEGIN 
 SELECT COUNT(*)INTO recived 
 FROM MSG_DEST 
 WHERE msg_id=m_id 
 AND deleted=0;IF (recived=0)THEN 
 DELETE MSG_DEST 
 WHERE msg_id=m_id;DELETE MSG_ATTACH 
 WHERE msg_id=m_id;DELETE MESSAGE 
 WHERE msg_id=m_id;ELSE 
 UPDATE MESSAGE 
 SET deleted=1 
 WHERE msg_id=m_id;END IF;END DeleteSenderMsg;PROCEDURE 
 GetMyMessages(my_id IN USRTABLE.id%TYPE ,io_cursor OUT 
 t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor 
 FOR 
 SELECT MESSAGE.msg_id, MESSAGE.msg_from, USRTABLE.name 
 User_Name, MESSAGE.subject, MESSAGE.msg_date, MESSAGE.reenvio,
 MESSAGE.deleted, MSG_DEST.user_id DEST, MSG_DEST.user_name 
 DEST_NAME,MSG_DEST.dest_type, MSG_DEST.READ, MESSAGE.msg_body
 FROM MESSAGE,MSG_DEST,USRTABLE 
 WHERE message.msg_id=msg_dest.msg_id 
 and message.msg_from=usrtable.id 
 and message.msg_id in(SELECT msg_id 
 FROM MSG_DEST 
 where user_id=my_id 
 AND deleted=0);io_cursor := v_cursor;END 
 GetMyMessages;PROCEDURE GetMyMessagesNew(my_id IN 
 USRTABLE.id%TYPE ,io_cursor OUT t_cursor) IS v_cursor 
 t_cursor;BEGIN OPEN v_cursor 
 FOR 
 SELECT MESSAGE.msg_id, MESSAGE.msg_from, USRTABLE.name 
 User_Name, MESSAGE.subject, MESSAGE.msg_date, MESSAGE.reenvio,
 MESSAGE.deleted, MSG_DEST.user_id DEST, MSG_DEST.user_name 
 DEST_NAME,MSG_DEST.dest_type, MSG_DEST.READ, MESSAGE.msg_body
 FROM MESSAGE,MSG_DEST,USRTABLE 
 WHERE message.msg_id=msg_dest.msg_id 
 and message.msg_from=usrtable.id 
 and message.msg_id in(SELECT msg_id 
 FROM MSG_DEST 
 where user_id=my_id 
 AND deleted=0);io_cursor := v_cursor;END 
 GetMyMessagesNew;PROCEDURE GetMyAttachments(my_id IN 
 USRTABLE.id%TYPE ,io_cursor OUT t_cursor) IS v_cursor 
 t_cursor;BEGIN OPEN v_cursor 
 FOR 
 SELECT MESSAGE.msg_id, MSG_ATTACH.msg_id, MSG_ATTACH.doc_id, 
 MSG_ATTACH.doc_type_id, MSG_ATTACH.folder_id, 
 MSG_ATTACH.index_id, MSG_ATTACH.name, MSG_ATTACH.icon, 
 volumelistid, doc_file, offset, disk_vol_path 
 FROM MESSAGE,MSG_ATTACH 
 WHERE MESSAGE.msg_id = MSG_ATTACH.msg_id 
 AND MESSAGE.msg_id IN (SELECT msg.msg_id 
 FROM MESSAGE msg,MSG_DEST 
 WHERE msg.msg_id = MSG_DEST.msg_id 
 AND MSG_DEST.user_id=my_id 
 AND MSG_DEST.deleted=0); io_cursor := v_cursor;END 
 GetMyAttachments;PROCEDURE InsertMsg(m_id IN 
 MESSAGE.msg_id%TYPE,m_from IN MESSAGE.msg_from%TYPE,m_Body IN
 MESSAGE.msg_body%TYPE,m_subject IN MESSAGE.subject%TYPE,
 m_resend IN MESSAGE.reenvio%TYPE) IS BEGIN 
 INSERT INTO MESSAGE(msg_id,msg_from,msg_body,subject,msg_date,
 reenvio,deleted)VALUES (m_id,m_from,m_body,m_subject,SYSDATE,
 m_resend, 0);COMMIT;END InsertMSG; PROCEDURE 
 InsertAttach(m_id IN msg_attach.msg_id%TYPE, m_DOCid IN 
 msg_attach.doc_id%TYPE, m_DOC_TYPE_ID IN 
 msg_attach.doc_type_id%TYPE, m_folder_id IN 
 msg_attach.folder_id%TYPE, m_index_id IN 
 msg_attach.index_id%TYPE,m_name IN msg_attach.name%TYPE, 
 m_icon IN msg_attach.icon%TYPE, m_volumelistid IN 
 msg_attach.volumelistid%TYPE, m_doc_file IN 
 msg_attach.doc_file%TYPE, m_offset IN msg_attach.offset%TYPE,
 m_disk_vol_path IN msg_attach.disk_vol_path%TYPE) IS BEGIN 
 INSERT INTO msg_attach(MSG_ID,DOC_ID,DOC_TYPE_ID,folder_id,
 index_id,name,icon,volumelistid,doc_file,offset,
 disk_vol_path)VALUES (m_id,m_DOCid,m_DOC_TYPE_ID,m_folder_id,
 m_index_id,m_name,m_icon,m_volumelistid,m_doc_file,m_offset,
 m_disk_vol_path);COMMIT;END InsertAttach;PROCEDURE 
 InsertMsgDest(m_id IN MSG_DEST.msg_id%TYPE, m_userid IN 
 MSG_DEST.USER_ID%TYPE, m_Dest_TYPE IN MSG_DEST.DEST_TYPE%TYPE,
 m_User_name IN MSG_DEST.user_name%TYPE) IS BEGIN 
 INSERT INTO MSG_DEST(msg_id,user_id ,dest_type, READ,
 User_name)VALUES (m_id ,m_userid,m_Dest_Type,0,
 m_user_name);COMMIT;END InsertMsgDest;end zsp_messages_100;
CREATE OR REPLACE PACKAGE BODY "ZSP_OBJECTS_100"
 AS
 PROCEDURE GetAndSetLastId (OBJTYPE IN OBJLASTID.OBJECT_TYPE_ID%TYPE, io_cursor OUT t_cursor)
 IS
 s_cursor t_cursor;
 tempId int;
 BEGIN
 
 SELECT Count(*) into tempId FROM OBJLASTID WHERE OBJECT_TYPE_ID = OBJTYPE;

 IF tempId=0 then
 Insert into Objlastid(Object_type_id,objectid) values(OBJTYPE, 1);
 else
 UPDATE OBJLASTID SET OBJECTID = OBJECTID + 1
 WHERE OBJECT_TYPE_ID = OBJTYPE;
 End If;
 
 open s_cursor for
 select objectid from objlastid
 where object_type_id=objtype;
 
 io_cursor := s_cursor;
 
 END GetAndSetLastId;
 END zsp_objects_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_PREPROCESS_100" as 
 Procedure GeTPreprocessByUsrID(userid NUMBER, io_cursor OUT t_cursor)
 IS 
 v_cursor t_cursor; 
 BEGIN 
 OPEN v_cursor
 FOR 
 SELECT PreprocessID as "ID", PreprocessName as "Name"
 FROM PreprocessIDs
 WHERE PreprocessID IN (SELECT aditional FROM USR_RIGHTS WHERE objid=70 and rtype=19 and (groupid= userid OR groupid IN (SELECT groupid FROM usr_r_group WHERE usrid= userid)));
 io_cursor:=v_cursor;
 END GeTPreprocessByUsrID;
 end Zsp_Preprocess_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_SCHEDULE_100" as 
 PROCEDURE UpdLastTaskExecution(Id IN Schedule.TASK_ID%type,
 Fecha IN Schedule.FECHA%type, io_cursor OUT t_cursor) IS	 
 BEGIN 
 Update Schedule 
 set FECHA=Fecha 
 where TASK_ID=Id; END UpdLastTaskExecution; PROCEDURE 
 ZScdGetTareasHoy(Fecha IN Schedule.FECHA%type,io_cursor OUT 
 t_cursor) IS 	v_cursor t_cursor; BEGIN OPEN v_cursor 
 FOR 
 select * 
 from schedule 
 where Fecha = SYSDATE; 	io_cursor := v_cursor;END 
 ZScdGetTareasHoy;PROCEDURE GetTasks(Fecha IN 
 Schedule.FECHA%type,io_cursor OUT t_cursor) IS 	v_cursor 
 t_cursor; BEGIN OPEN v_cursor 
 FOR 
 select * 
 from schedule 
 where Fecha = SYSDATE; 	io_cursor := v_cursor;END 
 GetTasks;PROCEDURE DeleteTask(Id IN Schedule.TASK_ID%type) 
 IS	 begin	 
 DELETE from Schedule 
 WHERE TASK_ID=Id;END DeleteTask;procedure GetNewTasks (Id IN 
 Schedule.TASK_ID%type, io_cursor OUT t_cursor) IS v_cursor 
 t_cursor;BEGIN open v_cursor 
 for 
 select * 
 from schedule 
 where task_id > Id;END GetNewTasks;end zsp_schedule_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_SEARCH_100" AS
 PROCEDURE WordDelete(varResultId IN ZSEARCHVALUES_DT.ResultId%TYPE) IS BEGIN
 DELETE FROM ZSEARCHVALUES_DT
 WHERE ResultId = varResultId;
 END WordDelete;

 PROCEDURE WordUpdate(varResultId IN ZSEARCHVALUES_DT.ResultId%TYPE, varIndexId IN ZSEARCHVALUES_DT.IndexId%TYPE) IS BEGIN
 DELETE FROM ZSEARCHVALUES_DT
	 WHERE IndexId = varIndexId And ResultId = varResultId;
 END WordUpdate;

 PROCEDURE WordInsert(varWord IN ZSEARCHVALUES.Word%TYPE, varDTID IN ZSEARCHVALUES_DT.DTID%TYPE, varResultId IN ZSEARCHVALUES_DT.ResultId%TYPE, varIndexId IN ZSEARCHVALUES_DT.IndexId%TYPE) IS
 varId numeric(18);
 varWordId numeric(18);
 BEGIN
 SELECT count(*) into varId FROM ZSEARCHDICTIONARY WHERE Word = varWord;

 IF varId = 0 then
 SELECT count(*) into varId FROM ZSEARCHVALUES WHERE Word = varWord;

 IF varId > 0 then
 SELECT Id into varWordId FROM ZSEARCHVALUES WHERE Word = varWord;
 SELECT count(*) into varId FROM ZSEARCHVALUES_DT WHERE WordId = varWordId AND IndexId = varIndexId AND ResultId = varResultId AND DTID = varDTID;

 IF varId = 0 then
 INSERT INTO ZSEARCHVALUES_DT (WordId,IndexId,ResultId,DTID) VALUES (varWordId,varIndexId,varResultId,varDTID);
 END IF;
 ELSE
 GETANDSETLASTID2_PKG.GetandSetLastId2 (91, varWordId);

 INSERT INTO ZSEARCHVALUES (Id, Word) VALUES(varWordId, varWord);
 INSERT INTO ZSEARCHVALUES_DT (WordId, IndexId, ResultId, DTID) VALUES (varWordId, varIndexId, varResultId, varDTID);
 END IF;
 END IF;
 END WordInsert;
END zsp_search_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_SECURITY_100" as 
 Procedure GetArchivosUserRight(UserId in 
 Zvw_USR_Rights_100.user_id%type, io_cursor out t_cursor) is 
 v_cursor t_cursor; 
 begin 
 open v_cursor for SELECT distinct(dtg.Doc_Type_Group_ID),dtg.Doc_Type_Group_Name,dtg.Icon,dtg.Parent_Id,dtg.Object_Type_Id,urv.User_Id,urv.Right_Type 
 FROM DOC_TYPE_GROUP dtg, Zvw_USR_Rights_100 urv 
 WHERE dtg.Doc_Type_Group_ID = urv.Aditional AND dtg.Object_Type_Id = urv.ObjectID and urv.User_Id =Userid ORDER BY dtg.Doc_Type_Group_ID; io_cursor:=v_cursor; end GetArchivosUserRight;Procedure GetDocTypeRights(userID IN number,io_cursor OUT t_cursor) IS v_cursor t_cursor; BEGIN OPEN v_cursor FOR Select Doc_Type.Doc_Type_Id, Doc_Type.Doc_Type_Name, Doc_Type.File_Format_ID, Doc_Type.Disk_Group_ID,Doc_Type.Thumbnails, Doc_Type.Icon_Id, Doc_Type.Cross_Reference, Doc_Type.Life_Cycle, Doc_Type.Object_Type_Id, Doc_Type.AutoName, doc_type.documentalid from Doc_Type, User_Rights Where Doc_Type.Doc_Type_Id=User_Rights.Object_ID and User_Rights.User_ID=userID and User_Rights.User_Rights_Type_Id=3 and User_Rights.Right_value=1 and user_rights.object_type_id=2 ORDER BY doc_type.Doc_Type_Name; io_cursor := v_cursor; End GetDocTypeRights;PROCEDURE GetUserDocumentsResctrictions(UserID IN User_Table.User_ID%TYPE,io_cursor OUT t_cursor)IS v_cursor t_cursor; BEGIN OPEN v_cursor FOR select RESTRICTION_ID from doc_restriction_r_user where user_id=UserId; io_cursor := v_cursor;END GetUserDocumentsResctrictions; PROCEDURE InsertStation(idd IN ESTREG.ID%TYPE,io_cursor OUT t_cursor)IS s_cursor t_cursor;BEGIN OPEN s_cursor FOR SELECT * FROM ESTREG WHERE ID=idd and ID>0; io_cursor := s_cursor; COMMIT;END InsertStation; PROCEDURE UpdUserRight(Rightv IN USER_RIGHTS.Right_Value%TYPE, Rightid IN USER_RIGHTS.Right_Id%TYPE)IS BEGIN UPDATE USER_RIGHTS SET Right_Value =Rightv WHERE Right_Id =Rightid; COMMIT; END UpdUserRight;end zsp_security_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_USERS_100" as 
 PROCEDURE GetUserAddressBook (userID IN USRTABLE.ID%type, io_cursor OUT t_cursor) 
 IS 
 v_cursor t_cursor;
 BEGIN 
 OPEN v_cursor FOR 
 SELECT ADDRESS_BOOK 
 FROM USRTABLE 
 WHERE ID=USeRID;io_cursor := v_cursor; 
 END GetUserAddressBook;
 
 PROCEDURE GetUserActions (UserId IN NUMBER, io_cursor OUT t_cursor) 
 IS 
 v_cursor t_cursor; 
 BEGIN 
 OPEN v_cursor FOR 
 SELECT USER_HST.Action_Date AS Fecha, OBJECTTYPES.OBJECTTYPES AS Herramienta, RIGHTSTYPE.RIGHTSTYPE AS Accion, user_hst.s_object_id AS En 
 FROM USER_HST,USRTABLE,OBJECTTYPES,RIGHTSTYPE 
 WHERE USER_HST.User_Id = USRTABLE.Id AND USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId AND USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId AND USER_HST.User_Id = UserId 
 ORDER BY USER_HST.Action_Date DESC; 
 io_cursor := v_cursor; 
 END GetUserActions;
 
 
 end ZSP_USERS_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_USERS_200" AS
PROCEDURE GetUserByName(username IN Usrtable.Name%TYPE, io_cursor OUT t_cursor) IS
v_cursor t_cursor;
BEGIN
 OPEN v_cursor FOR SELECT * FROM usrtable where name =username;io_cursor := v_cursor;
END GetUserByName;
END Zsp_users_200;
CREATE OR REPLACE PACKAGE BODY  "ZSP_VERSION_300" as 
 Procedure GETVERSIONDETAILS(Param_docId in number,io_cursor 
 out t_cursor) is v_cursor t_cursor;BEGIN open v_cursor 
 for 
 Select * 
 from ZComment 
 where docid= Param_docId; io_cursor:=v_cursor; end 
 GETVERSIONDETAILS; Procedure INSERTVERSIONCOMMENT(Par_docId 
 in number,Par_comment in varchar2) IS 
 v_cursor t_cursor; begin 
 INSERT INTO ZComment 
 VALUES (Par_docId,Par_comment,sysdate); COMMIT; end 
 INSERTVERSIONCOMMENT;procedure INSERTPUBLISH(Parm_publishid 
 IN number,Parm_docid IN number,Parm_userid IN number, 
 Par_publishdate in date) is v_cursor t_cursor; begin 
 INSERT INTO Z_Publish(PublishId, DocId, UserId, PublishDate) 
 VALUES(Parm_publishid, Parm_docid, Parm_userid, 
 Par_publishdate); COMMIT; end INSERTPUBLISH;END 
 ZSP_VERSION_300;
CREATE OR REPLACE PACKAGE BODY  "ZSP_VOLUME_100" as 
 PROCEDURE UpdFilesAndSize(VolumeId IN NUMBER, FileSize IN 
 DECIMAL)IS totalfiles NUMBER; totalsize DECIMAL;BEGIN 
 SELECT DISK_VOL_FILES 
 INTO totalfiles 
 FROM DISK_VOLUME 
 WHERE DISK_VOL_ID = VolumeId; 
 SELECT DISK_VOL_SIZE_LEN 
 INTO totalsize 
 FROM DISK_VOLUME 
 WHERE DISK_VOL_ID = VolumeId; 
 UPDATE DISK_VOLUME 
 SET DISK_VOL_FILES = totalfiles + 1, DISK_VOL_SIZE_LEN = 
 totalsize + FileSize 
 WHERE DISK_VOL_ID = VolumeId;END UpdFilesAndSize;PROCEDURE 
 UpdDeletedFiles(VolumeId IN NUMBER, FileSize IN DECIMAL)IS 
 totalfiles NUMBER; totalsize DECIMAL;BEGIN 
 SELECT DISK_VOL_FILES 
 INTO totalfiles 
 FROM DISK_VOLUME 
 WHERE DISK_VOL_ID = VolumeId; 
 SELECT DISK_VOL_SIZE_LEN 
 INTO totalsize 
 FROM DISK_VOLUME 
 WHERE DISK_VOL_ID = VolumeId; 
 UPDATE DISK_VOLUME 
 SET DISK_VOL_FILES = totalfiles - 1, DISK_VOL_SIZE_LEN = 
 totalsize - FileSize 
 WHERE DISK_VOL_ID = VolumeId;END UpdDeletedFiles; procedure 
 GetDocGroupRDocVolByDgId(volgroup in 
 disk_group_r_disk_volume.disk_group_id%type,io_cursor OUT 
 t_cursor) IS v_cursor t_cursor; BEGIN OPEN v_cursor 
 FOR 
 Select disk_volume_id 
 from disk_group_r_disk_volume 
 where disk_group_id=volgroup; io_cursor := v_cursor; 
 END GetDocGroupRDocVolByDgId; PROCEDURE UpdFilesByVolId(Archs
 IN disk_volume.Disk_vol_files%type,DiskVolId IN 
 disk_volume.disk_vol_id%type) IS BEGIN 
 Update disk_volume 
 set Disk_vol_files=Archs 
 where disk_vol_id=DiskVolId; END UpdFilesByVolId;end 
 zsp_volume_100;
CREATE OR REPLACE  PACKAGE BODY 
    "ZSP_WORKFLOW_100"  AS 
 
PROCEDURE WFRecotizacion(varI46 IN numeric, io_cursor OUT t_cursor) IS 
v_cursor t_cursor;
varValue varchar(400);

BEGIN 

select value into varValue from ZOPT where ITEM = 'FECHAANTACT';

OPEN v_cursor FOR
SELECT LPAD(i46,'3',0) as "Codigo Seccion",
i108 as "Codigo Grupo Economico", 
i16 as "Codigo Cliente", 
i45 as "Codigo Aseguradora", 
i18 as "Numero Poliza", 
crdate as "Fecha de ultima cotizacion" 
FROM doc_i422 WHERE doc_id IN (
	SELECT MAX(doc_id) FROM doc_i422 
	WHERE rtrim(ltrim(i209)) = 'Presentacion Cotizacion' and 
	(i46<>25 and i46 is not null and i45 is not null) and 
	(i16 is not null) and 
	i46 = varI46 and 
	crdate > (varValue) GROUP BY i16)

UNION

SELECT LPAD(i46,'3',0) as "Codigo Seccion", 
i108 as "Codigo Grupo Economico", 
i16 as "Codigo Cliente", 
i45 as "Codigo Aseguradora", 
i18 as "Numero Poliza", 
crdate as "Fecha de ultima cotizacion" 
FROM doc_i422 WHERE doc_id IN (
	SELECT MAX(doc_id) FROM doc_i422 
	WHERE rtrim(ltrim(i209)) = 'Presentacion Cotizacion' and 
	(i46<>25 and i46 is not null and i45 is not null) and 
	(i16 is null) and 
	i46 = varI46 and 
	crdate > (varValue) GROUP BY i108)

UNION

(SELECT LPAD(i46,'3',0) as "Codigo Seccion", 
i108 as "Codigo Grupo Economico", 
i16 as "Codigo Cliente", 
i45 as "Codigo Aseguradora", 
i18 as "Numero Poliza", 
crdate as "Fecha de ultima cotizacion" 
FROM doc_i225 WHERE doc_id IN 
	(SELECT MAX(doc_id) FROM doc_i225 
	where i46 = 25 and 
	i45 is not null and 
	(i16 is not null) and 
	i46 = varI46 and 
	crdate > (varValue) GROUP BY i16)

UNION

SELECT LPAD(i46,'3',0) as "Codigo Seccion", 
i108 as "Codigo Grupo Economico", 
i16 as "Codigo Cliente", 
i45 as "Codigo Aseguradora", 
i18 as "Numero Poliza", 
crdate as "Fecha de ultima cotizacion" 
FROM doc_i225 WHERE doc_id IN (
	SELECT MAX(doc_id) FROM doc_i225 
	GROUP BY i108) and 
	(i46=25 and i46 is not null and i45 is not null) and 
	(i16 is null) and 
	i46 = varI46 and 
	crdate > (varValue));
	
	/* Salida de datos */
	io_cursor := v_cursor;
END WFRecotizacion; 

 PROCEDURE GetUserWFIdsAndNames(user_id in USR_RIGHTS.groupid%type, io_cursor OUT t_cursor)
 IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT WFworkflow.WORK_ID, WFworkflow.name FROM WFworkflow 
	WHERE work_id IN (
		SELECT work_id FROM wfstep 
		WHERE step_id IN (
			SELECT aditional FROM USR_RIGHTS 
			WHERE objid=42 and rtype=19 and (groupid=user_id OR groupid IN (
				SELECT groupid FROM usr_r_group 
				WHERE usrid=user_id))));
 io_cursor:=v_cursor;
 END GetUserWFIdsAndNames;

 PROCEDURE GetStepsByWFIdAndUserId(workid numeric, userid numeric, io_cursor OUT t_cursor)
 IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT STEP_ID, WORK_ID, NAME, DESCRIPTION, HELP, CREATEDATE, IMAGEINDEX, EDITDATE, LOCATIONX, LOCATIONY, MAX_DOCS, MAX_HOURS, STARTATOPENDOC, TASKCOUNT(step_id,userid) as TaskCount
 FROM WFSTEP
 WHERE WORK_ID = workid AND step_id IN (SELECT aditional FROM USR_RIGHTS WHERE objid=42 and rtype=19 and (groupid= userid OR groupid IN (SELECT groupid FROM usr_r_group WHERE usrid= userid)));
 io_cursor:=v_cursor;
 END GetStepsByWFIdAndUserId;
 
 PROCEDURE GetStepStatesByStepId(stepid in WFSTEPSTATES.step_id%type, io_cursor OUT t_cursor)
 IS
 v_cursor t_cursor;
 BEGIN 
 OPEN v_cursor FOR
 	 SELECT * FROM WFStepStates
	 WHERE step_id = stepid;
 io_cursor:=v_cursor;
 END GetStepStatesByStepId;
 
 PROCEDURE GetStepByStepId(stepid in WFSTEP.step_id%type, io_cursor OUT t_cursor)
 IS
 v_cursor t_cursor;
 BEGIN 
 OPEN v_cursor FOR
 	 SELECT * FROM WFStep
	 WHERE step_id = stepid;
 io_cursor:=v_cursor;
 END GetStepByStepId;
 
 PROCEDURE GetTasksByStepAndUserId(stepId in wfdocument.step_id%type, userId in 
wfdocument.user_asigned%type, selectType in numeric, io_cursor OUT t_cursor)
 IS
 v_cursor t_cursor;
 BEGIN 
	IF selectType=0 THEN
		OPEN v_cursor FOR
		SELECT wfdocument.*, wfstepstates.name as state, uag.name as Asignado 
		FROM wfdocument left join wfstepstates on do_state_id = doc_state_id 
		left join zvw_UserAndGroups uag on wfdocument.user_asigned = uag.id
		WHERE wfdocument.step_id = stepId and (user_asigned=userId or user_asigned=0);

	ELSIF selectType=1 THEN
		OPEN v_cursor FOR
		SELECT wfdocument.*, wfstepstates.name as state, uag.name as Asignado 
		FROM wfdocument left join wfstepstates on do_state_id = doc_state_id 
		left join zvw_UserAndGroups uag on wfdocument.user_asigned = uag.id
		WHERE wfdocument.step_id=stepId and user_asigned<>0;
	ELSIF selectType=2 THEN
		OPEN v_cursor FOR
		SELECT wfdocument.*, wfstepstates.name as state, uag.name as Asignado 
		FROM wfdocument left join wfstepstates on do_state_id = doc_state_id 
		left join zvw_UserAndGroups uag on wfdocument.user_asigned = uag.id
		WHERE wfdocument.step_id=stepId and user_asigned=userId;
 ELSIF selectType=3 THEN
		OPEN v_cursor FOR
		SELECT wfdocument.*, wfstepstates.name as state, uag.name as Asignado 
		FROM wfdocument left join wfstepstates on do_state_id = doc_state_id 
		left join zvw_UserAndGroups uag on wfdocument.user_asigned = uag.id
		WHERE wfdocument.step_id=stepId;
 END IF;
	io_cursor:=v_cursor;

 END GetTasksByStepAndUserId;
 
PROCEDURE GetRulesPreferences(rule_id in ZRULEOPT.ruleid%type, io_cursor OUT t_cursor)
 IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
 SELECT DISTINCT ObjValue,ObjExtraData,ObjectId,SectionId 
	FROM ZRuleOpt where Ruleid= rule_id;
 io_cursor:=v_cursor;
 END GetRulesPreferences;
 
PROCEDURE GetStepStates(stepid in wfstepstates.step_id%type, io_cursor OUT t_cursor)
 IS
 v_cursor t_cursor;
 BEGIN
 if stepid=0 then
 OPEN v_cursor FOR
		 SELECT * FROM wfstepstates;	
	 else 
	 OPEN v_cursor FOR
		 SELECT * FROM wfstepstates WHERE step_id = stepid;
	 end if;
 io_cursor:=v_cursor;
 END GetStepStates;

 PROCEDURE GetStepsWithZOpenEvent(usrid numeric, io_cursor OUT t_cursor)
  IS
 v_cursor t_cursor;
 BEGIN
 	 OPEN v_cursor FOR
        SELECT distinct WFRules.step_Id, WFStep.name
        FROM WFRules
        INNER JOIN WFStep
        ON WFRules.step_Id = WFStep.step_Id
        WHERE type = 40
        AND WFRules.step_id IN (SELECT aditional FROM USR_RIGHTS WHERE objid=42 and rtype=19 and (groupid= usrid OR groupid IN (SELECT groupid FROM usr_r_group WHERE usrid= usrid)))
        AND WFRules.step_id IN (SELECT aditional FROM USR_RIGHTS WHERE objid=42 and rtype=35 and (groupid= usrid OR groupid IN (SELECT groupid FROM usr_r_group WHERE usrid= usrid)));
   io_cursor:=v_cursor;
 END GetStepsWithZOpenEvent;

PROCEDURE InsertWFStepHst( 
		vDoc_Id WFStepHst.Doc_Id%type, 
		vDoc_Name WFStepHst.Doc_Name%type, 
		vDocTypeId WFStepHst.DocTypeId%type, 
		vDoc_Type_Name WFStepHst.Doc_Type_Name%type, 
		vFOLDER_Id WFStepHst.FOLDER_Id%type, 
		vStepId WFStepHst.StepId%type, 
		vStep_Name WFStepHst.Step_Name%type, 
		vState WFStepHst.State%type, 
		vUserName WFStepHst.UserName%type, 
		vAccion WFStepHst.Accion%type, 
		vFecha WFStepHst.Fecha%type, 
		vWorkflowId WFStepHst.WorkflowId%type, 
		vWorkflowName WFStepHst.WorkflowName%type)
IS
BEGIN
	INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion, Fecha, WorkflowId, WorkflowName) 
	VALUES (vDoc_Id, vDoc_Name, vDocTypeId, vDoc_Type_Name, vFOLDER_Id, vStepId, vStep_Name, vState, vUserName, vAccion, vFecha, vWorkflowId, vWorkflowName);
 END InsertWFStepHst;

END ZSP_WORKFLOW_100;
CREATE OR REPLACE PACKAGE BODY  "ZSP_ZFORUM_100" as 
 Procedure delete_From_Foro (pdocid in zforum.docid%type,pdoct
 in zforum.doct%type,pparentid in zforum.parentid%type) is 
 begin 
 Delete from ZForum 
 where DocId=pdocid 
 and DocT=pdoct 
 and ParentId=pparentid; end delete_From_Foro; Procedure 
 insert_Foro(f_Doct in zforum.Doct%type,f_DocId in 
 zforum.DocId%type ,f_IdMensaje in zforum.IdMensaje%type,
 f_ParentId in zforum.ParentId%type,f_LinkName in 
 zforum.LinkName%type,f_Mensaje in zforum.mensaje%type,f_Fecha
 in zforum.fecha%type,f_UserId in zforum.userid%type,f_StateId
 in zforum.stateid%type) is begin 
 INSERT INTO ZForum(DocT,DocId,IdMensaje,ParentId,LinkName,
 Mensaje,Fecha,UserId,StateId) 
 VALUES (f_Doct, f_DocId,f_IdMensaje,f_ParentId,f_LinkName,
 f_Mensaje,f_Fecha,f_UserId,f_StateId); end insert_Foro; end 
 zsp_zforum_100;
CREATE OR REPLACE PACKAGE BODY  "ZWFSTEPSFACTORY_PKG" 
 as PROCEDURE ZGetViewWFStepsByWfID (pWFId in 
 ZViewWFSTEPS.work_id%type, io_cursor OUT t_cursor) IS
 v_cursor t_cursor;
 BEGIN
 OPEN v_cursor FOR
Select * from ZViewWFSTEPS where WORK_ID = pWFId;
io_cursor := v_cursor;
end ZGetViewWFStepsByWfID;
end ZWFSTEPSFACTORY_PKG;
CREATE OR REPLACE PACKAGE BODY  "ZWFUPDDT_PKG" AS 
 PROCEDURE ZWfUpdDtLCByDtId(WfId IN doc_type.life_cycle%type ,
 DocTypeID IN doc_type.doc_type_id%type ) IS BEGIN 
 UPDATE doc_type 
 SET life_cycle=WfID 
 WHERE doc_type_id=DocTypeID; END ZWfUpdDtLCByDtId; END 
 ZWfUpdDt_Pkg;

CREATE OR REPLACE  PACKAGE "ZSP_DOCTOZI_100"   as
  type t_cursor is ref cursor;
  procedure GetDocTypes(orderByDesc in int ,io_cursor out t_cursor);
  procedure AddDocToZI(INSERT_ID in zi.InsertID%type,
                       DOCTYPEID in zi.DTID%type,
                       DOC_ID in zi.DocID%type,
                       FOLDER_ID in zi.FolderID%type,
                       I_DATE in zi.IDate%type);
end;

CREATE OR REPLACE  PACKAGE BODY "ZSP_DOCTOZI_100"   as
  procedure GetDocTypes(orderByDesc in int, io_cursor out t_cursor) is
      v_cursor t_cursor;
  begin
      if orderByDesc = 0 then
          open v_cursor for
              select doc_type_id from doc_Type order by doc_type_id;
      else
          open v_cursor for
              select doc_type_id from doc_Type order by doc_type_id desc;
      end if;
      io_cursor := v_cursor;
  end GetDocTypes;

  procedure AddDocToZI(INSERT_ID in zi.InsertID%type,
                       DOCTYPEID in zi.DTID%type,
                       DOC_ID in zi.DocID%type,
                       FOLDER_ID in zi.FolderID%type,
                       I_DATE in zi.IDate%type) is
  begin
      INSERT INTO ZI(InsertID, DTID, DocID, FolderID, IDate)
      VALUES(INSERT_ID, DOCTYPEID, DOC_ID, FOLDER_ID, I_DATE);
  end AddDocToZI;

end ZSP_DOCTOZI_100;
CREATE OR REPLACE FUNCTION  "FINSERTUSERACTION" (ACONID
 IN UCM.CON_ID%TYPE , WIN_PC UCM.WINPC%TYPE , AUSRID IN
 USER_HST.USER_ID%TYPE , AOBJID IN USER_HST.OBJECT_ID%TYPE ,
 AOBJTID IN USER_HST.OBJECT_TYPE_ID%TYPE , ATYPE IN
 USER_HST.ACTION_TYPE%TYPE, SOBJECTID IN
 USER_HST.S_OBJECT_ID%TYPE)return number
 is
 AID number;
 cantLicencias int;

 begin
		-- Valida si el usuario tiene una licencia activa
		SELECT count(*) into cantLicencias FROM UCM Where CON_ID = ACONID AND WINPC = WIN_PC;

		IF cantLicencias = 1 then
			-- En caso de tenerla, se inserta el saveAction
			GetandSetLastId2_PKG.GetandSetLastId2(6, AID);
			INSERT INTO USER_HST(ACTION_ID, USER_ID, ACTION_DATE, OBJECT_ID, OBJECT_TYPE_ID, ACTION_TYPE, S_OBJECT_ID)
			VALUES (AID, AUSRID, sysdate, AOBJID, AOBJTID, ATYPE, SOBJECTID);

			IF AUSRID <> 9999 then
				UPDATE UCM SET U_TIME = sysdate WHERE con_id= ACONID;
			END IF;
			-- Retorna 1 para indicar que las acciones se insertaron con ?xito
			return 1;

		ELSE
			-- En caso de no tener la licencia, se valida si la acci?n a insertar es
			-- la de m?ximo de licencias conectadas
			IF (AUSRID = 87) AND (AOBJID = 27) then

				GetandSetLastId2_PKG.GetandSetLastId2(6, AID);
				INSERT INTO USER_HST(ACTION_ID, USER_ID, ACTION_DATE, OBJECT_ID, OBJECT_TYPE_ID, ACTION_TYPE, S_OBJECT_ID)
				VALUES (AID, AUSRID, sysdate, AOBJID, AOBJTID, ATYPE, SOBJECTID);
				-- Retorna 1 para indicar que la acci?n se inserto con ?xito
			return 2;

			ELSE
				-- Retorna 0 para indicar que no existieron licencias disponibles
			return 0;

			END IF;
		END IF;
	 COMMIT;

	EXCEPTION WHEN OTHERS then
	 ROLLBACK;
			return 3;

 end fInsertUserAction;
CREATE OR REPLACE FUNCTION  "ISNUMERIC" (MyNumber 
 Varchar) Return Number

Is

x int;

Begin

Select To_Number(MyNumber) INTO X from Dual;

Return 0;

Exception

When Others Then

Return 1;

End;

CREATE OR REPLACE FUNCTION  "TASKCOUNT" (stepid int, 
 userid int) RETURN number
 IS
 nadie number;
 otros number;
 counte number;
BEGIN
 SELECT COUNT(*) INTO nadie FROM USR_RIGHTS WHERE aditional = stepid AND objid=42 and rtype=33 and (groupid= userid OR groupid IN (SELECT groupid FROM usr_r_group WHERE usrid= userid));
 SELECT COUNT(*) INTO otros FROM USR_RIGHTS WHERE aditional = stepid AND objid=42 and rtype=32 and (groupid= userid OR groupid IN (SELECT groupid FROM usr_r_group WHERE usrid= userid));
 
 IF nadie > 0 AND otros = 0 THEN
 SELECT COUNT(Task_ID) INTO counte FROM wfdocument WHERE step_id = stepid AND (user_asigned = userid or user_asigned = 0); 
 ELSIF nadie = 0 AND otros > 0 THEN
 SELECT COUNT(Task_ID) INTO counte FROM wfdocument WHERE step_id = stepid AND user_asigned <> 0;
 ELSIF nadie = 0 AND otros = 0 THEN
 SELECT COUNT(Task_ID) INTO counte FROM wfdocument WHERE step_id = stepid AND user_asigned = userid;
 ELSE
 SELECT COUNT(Task_ID) INTO counte FROM wfdocument WHERE step_id = stepid;
 
 END IF;
 
 RETURN counte;

END;

update wfstep set color='white' where color is null;

ALTER TABLE WFStep MODIFY ( color varchar2(50) not null );

ALTER TABLE WFStep MODIFY color Varchar2(50) DEFAULT 'white';

ALTER TABLE ZI ADD CONSTRAINT PK_ZI PRIMARY KEY (InsertID);
ALTER TABLE ZI ADD CONSTRAINT IX_ZI UNIQUE (DocID);
CREATE INDEX IX_Default_Search ON Default_Search (DocTypeId,UserId);
CREATE INDEX XIF1WFStep ON WFStep (work_id) PCTFREE = 90;
ALTER TABLE WFStep ADD CONSTRAINT PK_WFStep_1 PRIMARY KEY  (step_Id) ;
ALTER TABLE WFStep ADD CONSTRAINT FK_WFStep_WFWorkflow 
FOREIGN KEY (work_id) REFERENCES WFWorkflow (work_id);
ALTER TABLE WFRules ADD CONSTRAINT FK_WFRules_WFStep 
FOREIGN KEY (step_Id) REFERENCES WFStep (step_Id);
ALTER TABLE WFStepStates ADD CONSTRAINT FK_WFStepStates_WFStep 
FOREIGN KEY (Step_Id) REFERENCES WFStep(step_Id);
CREATE  INDEX IX_Security ON Security (userid) ;
CREATE  INDEX IX_WFStepStates ON WFStepStates (Step_Id) ;
CREATE  INDEX IX_WFRules ON WFRules (step_Id,Id,ParentId) ;
CREATE  INDEX IX_WFDocument ON WFDocument (step_Id,User_Asigned) ;
CREATE  INDEX XIF1WFStep ON WFStep (work_id,step_Id) ;
ALTER TABLE ZIR ADD CONSTRAINT PK_ZIR PRIMARY KEY  (IndexId,DoctypeId,UserId,RightType) ;
CREATE  INDEX IX_WFStepOpt ON WFStepOpt (StepId) ;
ALTER TABLE LCK ADD CONSTRAINT PK_LCK PRIMARY KEY  (DOC_ID) ;
CREATE  INDEX IX_ZFrmIndexsDesc ON ZFrmIndexsDesc (FId,IId,Type,C_Value) ;
CREATE  INDEX IX_ZRuleOpt ON ZRuleOpt (RuleId,SectionId,ObjectId) ;
CREATE  INDEX IX_DOC_TYPE_R_DOC_TYPE ON DOC_TYPE_R_DOC_TYPE (DocTypeID1) ;
CREATE  INDEX IX_INDEX_R_DOC_TYPE ON INDEX_R_DOC_TYPE (DOC_TYPE_ID) :
CREATE  INDEX IX_ZI_DTID ON ZI (DTID) ;
CREATE TABLE "UCM_HST" ("FECHA_Y_HORA" DATE, 
    "COUNTCONEX" NUMBER(10), "TYPE" NUMBER(10), "USERID" 
    NUMBER(18))
CREATE OR REPLACE  PACKAGE 
    "UCM_TRIGGER_INSERT_PKG"  as
  recent_user_id ucm.user_id%TYPE;
  recent_type_con ucm.type%TYPE;
END;
CREATE OR REPLACE TRIGGER 
    "UCM_HST_INSERT_TRIGGER" AFTER
INSERT
OR DELETE ON "UCM" declare
d number(10);
userid number(18);
typecon number(10);
begin
  select count(*) into d from ucm;
  typecon := ucm_trigger_insert_pkg.recent_type_con;
  userid := ucm_trigger_insert_pkg.recent_user_id;


  insert into ucm_hst (fecha_y_hora, countconex, type, userid) values (sysdate, d, typecon, userid);
  end;
CREATE OR REPLACE TRIGGER 
    "UCM_HST_DELETE_TRIGGER" BEFORE
DELETE ON "UCM" declare
d number(10);
userid number(18);
typecon number(10);
begin

  typecon := ucm_trigger_insert_pkg.recent_type_con;
  userid := ucm_trigger_insert_pkg.recent_user_id;
  select count(*)-1 into d from ucm where type=typecon;

  insert into ucm_hst (fecha_y_hora, countconex, type, userid) values (sysdate, d, typecon, userid);
  end;
CREATE OR REPLACE TRIGGER 
    "UCM_global_variable_TRIGGER" AFTER
INSERT
OR DELETE ON "UCM" FOR EACH ROW begin
    ucm_trigger_insert_pkg.recent_user_id := :new.user_id;
    ucm_trigger_insert_pkg.recent_type_con := :new.type;
  end;

/************************************************************************
Marcelo Created 13/08/09
Script para la creacion de forms sin tipo de documento
*************************************************************************/
ALTER TABLE Ztype_Zfrms DROP constraint FK_ZTYPE_ZFRMS_DOC_TYPE_DO35;
