Imports Zamba.Servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32

Public Class PAQ_CreateViews_General2
    Inherits ZPaq
    Implements IPAQ
    Private Const CREATEVIEW As String = "CREATE VIEW"
    Private Const CREATEREPLACEVIEW As String = "CREATE OR REPLACE VIEW"
    Public Overrides Sub Dispose()

    End Sub
    Private Sub AgregarFunciones(ByVal GenerateScripts As Boolean)
        Dim StrCreate As String

        Try
            IfExists("ShowState", ZPaq.Tipo.UserFunction, Me.CanDrop)
            StrCreate = "CREATE FUNCTION ShowState(@Id int)RETURNS nvarchar(30) as BEGIN declare @name nvarchar if @id=0 Return 'Sin Estado' Else  Select @name=[Description] from WFStepStates where Doc_state_id=@id Return @name End "
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(StrCreate.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            Dim exn As New Exception("ERROR | No se pudo crear la funcion ShowState, excepción: " & ex.ToString)
            'ZException.Log(exn, False)
            MessageBox.Show(exn.Message)
        End Try


        Try
            IfExists("ShowStep", ZPaq.Tipo.UserFunction, Me.CanDrop)
            StrCreate = "CREATE FUNCTION ShowStep(@Id int)RETURNS nvarchar(30) AS BEGIN declare @name nvarchar(30) Select @name=[Name] from WFStep where Step_id=@id Return @name END"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(StrCreate.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            Dim exn As New Exception("ERROR | No se pudo crear la funcion ShowStep, excepción: " & ex.ToString)
            'ZException.Log(exn, False)
            MessageBox.Show(exn.Message)
        End Try

        Try
            IfExists("ShowUser", ZPaq.Tipo.UserFunction, Me.CanDrop)
            StrCreate = "CREATE FUNCTION ShowUser(@Id int) RETURNS nvarchar(30) AS BEGIN declare @name nvarchar(30) Select @name=[Name] from usrtable where [id]=@id Return @name END"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(StrCreate.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            Dim exn As New Exception("ERROR | No se pudo crear la funcion ShowUser, excepción: " & ex.ToString)
            'ZException.Log(exn, False)
            MessageBox.Show(exn.Message)
        End Try

    End Sub

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        AgregarFunciones(GenerateScripts)
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            'SQL 

            Dim StrCreate As String = ""

            Try
                IfExists("Zvw_WFHistory_200", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_WFHistory_200 AS SELECT Fecha AS Fecha, Step_Name AS Etapa, State AS Estado, UserName AS Usuario, Accion AS Accion, Doc_Name AS Tarea, Doc_Type_Name AS [Tipo Documento], Doc_Id FROM WFStepHst"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_BESTDOCUMENTS_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "create View Zvw_BESTDOCUMENTS_100 as Select top 90 S_OBJECT_ID as Documento,Count(S_Object_ID)as [Cantidad de Consultas] from user_hst group by S_OBJECT_ID order by [Cantidad de Consultas] DESC"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_DOCDELETED_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "Create View Zvw_DOCDELETED_100 as select [name] as Usuario,Action_Date as Fecha, S_Object_Id as Documento from user_hst,usrtable where Action_ID=4 and user_hst.user_id=usrtable.id"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_DOCSEND_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "Create View Zvw_DOCSEND_100 As select name as Usuario,Action_Date as Fecha, S_Object_Id as Documento from user_hst,usrtable where Action_ID=11 and user_hst.user_id=usrtable.id"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            ''error a partir de aca !!!!!! no existe la tabla WFWorkFlow_State
            'Try
            '    If IfExists("Zvw_ZViewWF_100", ZPaq.Tipo.View, Me.CanDrop) Then
            '        StrCreate = "CREATE VIEW Zvw_ZViewWF_100 AS SELECT WFWorkflow.work_id, WFWorkflow.Wstat_id, WFWorkflow.Name, WFWorkflow.Description, WFWorkflow.Help, WFWorkflow.CreateDate, WFWorkflow.EditDate, WFWorkFlow_State.wstat_Description, WFWorkflow.Refreshrate, WFWorkflow.InitialStepId FROM WFWorkflow INNER JOIN WFWorkFlow_State ON WFWorkflow.Wstat_id = WFWorkFlow_State.Wstat_id"
            '        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            '    End If

            'Catch ex As Exception
            '    Dim exn As New Exception("ERROR | No se pudo crear la vista " & Me.obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
            '    'ZException.Log(exn, False)
            '    MessageBox.Show(exn.Message)
            'End Try

            Try
                IfExists("Zvw_ESTREG_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "Create View Zvw_ESTREG_100 AS Select ID, M_NAME PC, W_USER [Usuario Windows],VER Version, UPDATED Actualizado, LAST_CHECK [Ultima verificacion] from estreg"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_HISTORYVIEW_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_HISTORYVIEW_100 AS SELECT top 100 percent USER_HST.ACTION_DATE as Fecha, USRTABLE.NAME as Usuario, USER_HST.ACTION_ID as Accion FROM USER_HST, USRTABLE WHERE USER_HST.USER_ID =usrtable.id and USER_HST.ACTION_DATE> getdate()-30 order by usuario"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("zvw_USUARIOS_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW zvw_USUARIOS_100 AS SELECT USRTABLE.ID, USRTABLE.DESCRIPTION, USRTABLE.NAME FROM USRTABLE"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_INACTIVEUSERS_100", ZPaq.Tipo.View, Me.CanDrop)
                'StrCreate = "CREATE VIEW Zvw_INACTIVEUSERS_100 AS Select Top 100 [Description] from usrtable where [name] not in (Select distinct Usuario  from Zvw_HISTORYVIEW_100) order by [Description]"
                StrCreate = "CREATE VIEW Zvw_INACTIVEUSERS_100 AS Select Top 100 percent ([Apellido] + ', ' + [Nombres]) as [Description] from usrtable where [name] not in (Select distinct Usuario from Zvw_HISTORYVIEW_100) order by [Apellido]"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_INSTALACIONES_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE View Zvw_INSTALACIONES_100 as Select top 100 percent ID as [Nro de PC], W_User as [Usuario Windows], M_Name as PC, VER as [Version Zamba] from estreg order by W_User"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_LoginsFailed_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "Create View Zvw_LoginsFailed_100 As Select top 100 percent User_hst.Action_date as Fecha,(UsrTable.Apellido + ' ' + usrtable.nombres) as Usuario from User_hst, Usrtable where user_hst.ACTION_TYPE=27 and user_hst.[User_ID]=UsrTable.[Id] order by UsrTable.[apellido]"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_ONLINEUSERS_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_ONLINEUSERS_100 (USER_DESCRIPTION, Hora_de_Conexion, Ultima_Actividad, Usuario_de_Windows, PC_Nro, PC, timeout) AS select UsrTable.Description, UCM.C_Time as Hora_de_Conexion, UCM.U_Time as Ultima_Actividad, UCM.WinUser as Usuario_de_Windows, Estreg.ID as PC_Nro,EStreg.M_Name as PC,UCM.Time_Out as timeout from UCM, Estreg, UsrTable Where UCM.USER_ID=UsrTable.ID and UCM.WinUser=Estreg.W_User"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_RESTRICTION_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_RESTRICTION_100 (DOC_TYPE_ID, INDEX_ID,STRING_VALUE,RESTRICTION_ID,RESTRICTION_NAME, USER_ID) AS select DOC_TYPE_ID, INDEX_ID, STRING_VALUE, r.RESTRICTION_ID,  RESTRICTION_NAME,USER_ID from doc_restrictions r,doc_restriction_r_user u where u.restriction_id=r.restriction_id"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_UsersPrint_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE View Zvw_UsersPrint_100 As Select top 100 percent User_hst.Action_date as Fecha,(UsrTable.Apellido + '  ' + usrtable.nombres) as Usuario,user_hst.S_OBJECT_ID as Documento from User_hst, Usrtable where user_hst.ACTION_TYPE=9 and user_hst.[User_ID]=UsrTable.[Id] order by UsrTable.[apellido]"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_USR_Rights_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE View Zvw_USR_Rights_100 As select u.ID [USER_ID], u.[name] [USER_NAME], r.rtype RIGHT_TYPE, r.objid OBJECTID,Aditional from usrtable u, usr_r_group urr, usr_rights r where (u.id=urr.usrid and urr.groupid=r.groupid) union select u.ID [USER_ID], u.[name] [USER_NAME],r.rtype RIGHT_TYPE, r.objid OBJECTID,Aditional from usrtable u, usr_rights r where u.id=r.groupid"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_docgroups_rights_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_docgroups_rights_100 AS SELECT     USR_RIGHTS.GROUPID AS ID, DOC_TYPE_GROUP.DOC_TYPE_GROUP_NAME, RIGHTSTYPE.RIGHTSTYPE FROM USR_RIGHTS INNER JOIN  OBJECTTYPES ON USR_RIGHTS.OBJID = OBJECTTYPES.OBJECTTYPESID INNER JOIN  RIGHTSTYPE ON USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID INNER JOIN DOC_TYPE_GROUP ON USR_RIGHTS.ADITIONAL = DOC_TYPE_GROUP.DOC_TYPE_GROUP_ID"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_doctypes_rights_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_doctypes_rights_100 AS SELECT USR_RIGHTS.GROUPID AS ID, USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, USR_RIGHTS.ADITIONAL,  OBJECTTYPES.OBJECTTYPES, RIGHTSTYPE.RIGHTSTYPE, DOC_TYPE.DOC_TYPE_NAME FROM USR_RIGHTS, OBJECTTYPES, RIGHTSTYPE, DOC_TYPE WHERE USR_RIGHTS.OBJID = OBJECTTYPES.OBJECTTYPESID AND USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID AND USR_RIGHTS.ADITIONAL = DOC_TYPE.DOC_TYPE_ID"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_objects_rights_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_objects_rights_100 AS SELECT USR_RIGHTS.GROUPID AS ID, USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, USR_RIGHTS.ADITIONAL, OBJECTTYPES.OBJECTTYPES, RIGHTSTYPE.RIGHTSTYPE FROM USR_RIGHTS, OBJECTTYPES, RIGHTSTYPE WHERE USR_RIGHTS.OBJID = OBJECTTYPES.OBJECTTYPESID AND USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_restrictions_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_restrictions_100 AS SELECT  DISTINCT USRGROUP.ID,DOC_TYPE.DOC_TYPE_NAME, DOC_RESTRICTIONS.RESTRICTION_NAME FROM    USRGROUP,DOC_RESTRICTIONS,DOC_TYPE,DOC_RESTRICTION_R_USER WHERE DOC_RESTRICTIONS.DOC_TYPE_ID = DOC_TYPE.DOC_TYPE_ID AND DOC_RESTRICTIONS.RESTRICTION_ID = DOC_RESTRICTION_R_USER.RESTRICTION_ID AND USRGROUP.ID = DOC_RESTRICTION_R_USER.USER_ID UNION SELECT  DISTINCT USRTABLE.ID,DOC_TYPE.DOC_TYPE_NAME, DOC_RESTRICTIONS.RESTRICTION_NAME FROM USRTABLE,DOC_RESTRICTIONS,DOC_TYPE,DOC_RESTRICTION_R_USER WHERE DOC_RESTRICTIONS.DOC_TYPE_ID = DOC_TYPE.DOC_TYPE_ID AND DOC_RESTRICTIONS.RESTRICTION_ID = DOC_RESTRICTION_R_USER.RESTRICTION_ID AND  USRTABLE.ID = DOC_RESTRICTION_R_USER.USER_ID"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_permisos_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_permisos_100 AS SELECT TOP 100 PERCENT DOC_TYPE.DOC_TYPE_NAME AS Documento, RIGHTSTYPE.RIGHTSTYPE AS Permiso, USRTABLE.NAME AS Usuario FROM USR_RIGHTS INNER JOIN RIGHTSTYPE ON USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID INNER JOIN USRTABLE ON USR_RIGHTS.GROUPID = USRTABLE.ID INNER JOIN DOC_TYPE ON USR_RIGHTS.ADITIONAL = DOC_TYPE.DOC_TYPE_ID WHERE (USR_RIGHTS.ADITIONAL <> - 1) AND (USR_RIGHTS.OBJID = 2) ORDER BY USRTABLE.NAME, DOC_TYPE.DOC_TYPE_NAME, RIGHTSTYPE.RIGHTSTYPE"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_volumenes_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "Create View Zvw_volumenes_100 As Select top 100 percent  dg.disk_group_name Lista, dv.disk_vol_name Volumen, Disk_vol_path Ruta, Disk_vol_files Archivos,disk_vol_size MB, (disk_vol_size - (disk_vol_size_len/1024)) Libre from disk_group dg inner join Disk_group_R_Disk_volume on Disk_group_R_Disk_volume.DISK_GROUP_ID=dg.DISK_GROUP_ID inner join Disk_volume dv on dv.DISK_VOL_ID=Disk_group_R_Disk_volume.Disk_VOLUME_ID order by dg.disk_group_name,dv.disk_vol_name"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            'Error La tabla WFDocumentState no existe!
            'Try
            '    IfExists("Zvw_WFTASKHISTORY_100", ZPaq.Tipo.View, Me.CanDrop)
            '    StrCreate = "CREATE VIEW Zvw_WFTASKHISTORY_100 AS SELECT     WFStepHst.Doc_Id, WFStep.Name AS etapa, WFStepHst.CheckIn AS checkin,(SELECT usrtable.name FROM usrtable WHERE wfstephst.ucheckin = usrtable.id) AS usercheckin,(SELECT wfdocumentstate.name FROM wfdocumentstate WHERE wfstephst.ci_doc_state_id = wfdocumentstate.doc_state_id) AS statecheckin, WFStepHst.CheckOut AS checkout,(SELECT usrtable.name FROM usrtable WHERE wfstephst.ucheckout = usrtable.id) AS usercheckout,(SELECT wfdocumentstate.name FROM wfdocumentstate WHERE wfstephst.co_doc_state_id = wfdocumentstate.doc_state_id) AS statecheckout  FROM WFStepHst INNER JOIN WFStep ON WFStepHst.Step_Id = WFStep.step_Id"
            '    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            'Catch ex As Exception
            '    Dim exn As New Exception("ERROR | No se pudo crear la vista " & Me.obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
            '    'ZException.Log(exn, False)
            '    MessageBox.Show(exn.Message)
            'End Try

            'Try
            '    IfExists("Zvw_WFViewdocsstates_100", ZPaq.Tipo.View, Me.CanDrop)
            '    StrCreate = "CREATE VIEW Zvw_WFViewdocsstates_100 AS SELECT Step_Id, Doc_State_ID, Description, Name, Initial FROM WFDocumentState DOCSTATE WHERE Initial = 1"
            '    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            'Catch ex As Exception
            '    Dim exn As New Exception("ERROR | No se pudo crear la vista " & Me.obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
            '    'ZException.Log(exn, False)
            '    MessageBox.Show(exn.Message)
            'End Try

            'Try
            '    IfExists("Zvw_WFViewDocStepsStates_100", ZPaq.Tipo.View, Me.CanDrop)
            '    StrCreate = "CREATE VIEW Zvw_WFViewDocStepsStates_100 AS SELECT WFDocumentState.Name, WFDocumentState.Description, WFDocumentState.Doc_State_ID, WFDocumentState.Step_Id, WFDocumentState.Initial FROM WFDocumentState INNER JOIN WFStep ON WFDocumentState.Step_Id = WFStep.step_Id"
            '    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            'Catch ex As Exception
            '    Dim exn As New Exception("ERROR | No se pudo crear la vista " & Me.obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
            '    'ZException.Log(exn, False)
            '    MessageBox.Show(exn.Message)
            'End Try

            Try
                IfExists("Zvw_WFViewForms_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_WFViewForms_100 AS SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type, ZFrms.ObjectTypeId, ZFrms.Path, ZFrms.Description, Ztype_Zfrms.Form_Id, Ztype_Zfrms.DocType_Id, WF_Frms.Step_Id, WF_Frms.Sort FROM ZFrms INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id INNER JOIN WF_Frms ON Ztype_Zfrms.Form_Id = WF_Frms.Form_Id"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_ZstepUserGroups_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_ZstepUserGroups_100 AS SELECT USRGROUP.ID, USRGROUP.NAME, USRGROUP.DESCRIPCION, USR_RIGHTS.ADITIONAL, USR_RIGHTS.RTYPE, USR_RIGHTS.OBJID FROM USR_RIGHTS INNER JOIN USRGROUP ON USR_RIGHTS.GROUPID = USRGROUP.ID WHERE USR_RIGHTS.RTYPE = 1  AND USR_RIGHTS.OBJID = 42"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_ZViewForms_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_ZViewForms_100 AS SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type, ZFrms.ObjectTypeId, ZFrms.Path, ZFrms.Description, Ztype_Zfrms.Form_Id, Ztype_Zfrms.DocType_Id FROM ZFrms INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            'Error no se puede crear la vista por que no existe la vista Zvw_ZViewWF_100 (no se pudo crear por que no existe la tabla WFWorkFlow_State
            'Try
            '    IfExists("Zvw_DocTypeInWF_100", ZPaq.Tipo.View, Me.CanDrop)
            '    StrCreate = "Create View Zvw_DocTypeInWF_100 as Select wf.[name] as [Workflow],zdoctype.[doc_type_name] as [Tipo de Documento] from Zvw_ZViewWF_100 wf inner join ZWFViewWfDoctypes zdoctype on wf.[work_id]=zdoctype.WFID"
            '    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            'Catch ex As Exception
            '    Dim exn As New Exception("ERROR | No se pudo crear la vista " & Me.obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
            '    MessageBox.Show(exn.Message)
            'End Try

            Try
                IfExists("Zvw_WFDocumentCOUNT_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_WFDocumentCOUNT_100 AS SELECT COUNT(Doc_ID) AS DCOUNT, step_Id FROM WFDocument GROUP BY step_Id"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("ZVIEWWFSTEPS", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW ZVIEWWFSTEPS AS SELECT step_Id, work_id, Name, Description, Help, CreateDate, ImageIndex, EditDate, LocationX, LocationY, max_docs, max_Hours, StartAtOpenDoc FROM WFStep"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            Try
                IfExists("Zvw_ZVIEWWFUserSTEPS_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_ZVIEWWFUserSTEPS_100 AS SELECT WFStep.step_Id, WFStep.work_id, WFStep.Name, WFStep.Description, WFStep.Help, WFStep.CreateDate, WFStep.ImageIndex, WFStep.EditDate, WFStep.LocationX, WFStep.LocationY, WFStep.max_docs, USR_RIGHTS.GROUPID, USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, WFStep.max_Hours, WFStep.StartAtOpenDoc FROM WFStep INNER JOIN USR_RIGHTS ON WFStep.step_Id = USR_RIGHTS.ADITIONAL"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            'Error no existe la tabla WFStepRules
            'Try
            '    IfExists("Zvw_ZWFRulesByStep_100", ZPaq.Tipo.View, Me.CanDrop)
            '    StrCreate = "Create View Zvw_ZWFRulesByStep_100 as SELECT DISTINCT TOP 100 PERCENT WFWorkflow.Name AS Workflow, WFStep.Name AS Etapas, ZWFViewRules.Description AS Regla, ZWFViewRules.RuleResult AS Resultado FROM WFStep INNER JOIN WFStepRules ON WFStep.step_Id = WFStepRules.step_Id INNER JOIN WFWorkflow ON WFStep.work_id = WFWorkflow.work_id INNER JOIN ZWFViewRules ON WFStep.step_Id = ZWFViewRules.step_Id WHERE (ZWFViewRules.Enabled IS NOT NULL) ORDER BY WFWorkflow.Name, WFStep.Name, ZWFViewRules.Description"
            '    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            'Catch ex As Exception
            '    Dim exn As New Exception("ERROR | No se pudo crear la vista " & Me.obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
            '    'ZException.Log(exn, False)
            '    MessageBox.Show(exn.Message)
            'End Try

            'Error no exite las tablas WFDocumentState WFStep_State
            'Try
            '    IfExists("Zvw_ZWfStepsStates_100", ZPaq.Tipo.View, Me.CanDrop)
            '    StrCreate = "create view Zvw_ZWfStepsStates_100 as SELECT distinct  WFWorkflow.Name AS Workflow,WFStep.Name AS Etapa, WFDocumentState.Name AS Estado, WFDocument.Name AS Documento FROM WFStep INNER JOIN WFDocumentState ON WFStep.step_Id = WFDocumentState.Step_Id INNER JOIN WFStep_State ON WFDocumentState.Doc_State_ID = WFStep_State.SStat_ID INNER JOIN WFDocument ON WFStep.step_Id = WFDocument.step_Id INNER JOIN WFWorkflow ON WFStep.work_id = WFWorkflow.work_id CROSS JOIN WF_DT"
            '    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            'Catch ex As Exception
            '    Dim exn As New Exception("ERROR | No se pudo crear la vista " & Me.obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
            '    'ZException.Log(exn, False)
            '    MessageBox.Show(exn.Message)
            'End Try

            Try
                IfExists("Zvw_WFDocStepsXUser_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_WFDocStepsXUser_100 AS SELECT WFDocument.Doc_ID, WFDocument.DOC_TYPE_ID, WFDocument.Folder_ID, WFDocument.step_Id, WFDocument.Do_State_ID, WFDocument.Name, WFDocument.IconId, WFDocument.CheckIn, WFStepHst.UCheckIn AS ucheckinid, USRTABLE.NAME AS ucheckinname, USRTABLE.DESCRIPTION, USRTABLE.ADDRESS_BOOK, USRTABLE.NOMBRES, USRTABLE.APELLIDO, USRTABLE.CORREO, USRTABLE.TELEFONO, USRTABLE.PUESTO, USRTABLE.FIRMA, USRTABLE.FOTO  FROM WFDocument INNER JOIN WFStepHst ON WFDocument.Doc_ID = WFStepHst.Doc_Id AND WFDocument.CheckIn = WFStepHst.CheckIn  INNER JOIN USRTABLE ON WFStepHst.UCheckIn = USRTABLE.ID"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try

            'Try
            '    IfExists("ZViewForms", ZPaq.Tipo.View, Me.CanDrop)
            '    StrCreate = "CREATE VIEW ZViewForms AS SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type, ZFrms.ObjectTypeId,ZFrms.Path, ZFrms.Description, Ztype_Zfrms.Form_Id,Ztype_Zfrms.DocType_Id FROM ZFrms INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id"
            '    Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            'Catch ex As Exception
            '    Dim exn As New Exception("ERROR | No se pudo crear la vista " & Me.obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
            '    'ZException.Log(exn, False)
            '    MessageBox.Show(exn.Message)
            'End Try
            Try
                IfExists("Zvw_ZViewForms_100", ZPaq.Tipo.View, Me.CanDrop)
                StrCreate = "CREATE VIEW Zvw_ZViewForms_100 AS SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type, ZFrms.ObjectTypeId, ZFrms.Path, ZFrms.Description, Ztype_Zfrms.Form_Id, Ztype_Zfrms.DocType_Id FROM ZFrms INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id"
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, StrCreate)
            Catch ex As Exception
                Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(StrCreate, CREATEVIEW) & ", excepción: " & ex.ToString)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try




            'Dim StrSql() As Object = {"create View Zvw_BESTDOCUMENTS_100 as Select top 90 S_OBJECT_ID as Documento,Count(S_Object_ID)as [Cantidad de Consultas] from user_hst group by S_OBJECT_ID order by [Cantidad de Consultas] DESC", "Create View Zvw_DOCDELETED_100 as select [name] as Usuario,Action_Date as Fecha, S_Object_Id as Documento from user_hst,usrtable where Action_ID=4 and user_hst.user_id=usrtable.id" _
            '                        , "Create View Zvw_DOCSEND_100 As select name as Usuario,Action_Date as Fecha, S_Object_Id as Documento from user_hst,usrtable where Action_ID=11 and user_hst.user_id=usrtable.id" _
            '                        , "CREATE VIEW Zvw_ZViewWF_100 AS SELECT WFWorkflow.work_id, WFWorkflow.Wstat_id, WFWorkflow.Name, WFWorkflow.Description, WFWorkflow.Help, WFWorkflow.CreateDate, WFWorkflow.EditDate, WFWorkFlow_State.wstat_Description, WFWorkflow.Refreshrate, WFWorkflow.InitialStepId FROM WFWorkflow INNER JOIN WFWorkFlow_State ON WFWorkflow.Wstat_id = WFWorkFlow_State.Wstat_id" _
            '                        , "Create View Zvw_ESTREG_100 AS Select ID, M_NAME PC, W_USER [Usuario Windows],VER Version, UPDATED Actualizado, LAST_CHECK Ultima verificacion from estreg" _
            '                        , "CREATE VIEW Zvw_HISTORYVIEW_100 AS SELECT top 100 percent USER_HST.ACTION_DATE as Fecha, USRTABLE.NAME as Usuario, USER_HST.ACTION_ID as Accion FROM USER_HST, USRTABLE WHERE USER_HST.USER_ID =usrtable.id and USER_HST.ACTION_DATE> getdate()-30 order by usuario" _
            '                        , "CREATE VIEW zvw_USUARIOS_100 AS SELECT USRTABLE.ID, USRTABLE.DESCRIPTION, USRTABLE.NAME FROM USRTABLE" _
            '                        , "CREATE VIEW Zvw_INACTIVEUSERS_100 AS Select Top 100 [Description] from usrtable where [name] not in (Select distinct Usuario  from Zvw_HISTORYVIEW_100) order by [Description]" _
            '                        , "CREATE View Zvw_INSTALACIONES_100 as Select top 100 ID as [Nro de PC], W_User as [Usuario Windows], M_Name as PC, VER as [Version Zamba] from estreg order by W_User" _
            '                        , "Create View Zvw_LoginsFailed_100 As Select top 100 User_hst.Action_date as Fecha,(UsrTable.Apellido + ' ' + usrtable.nombres) as Usuario from User_hst, Usrtable where user_hst.ACTION_TYPE=27 and user_hst.[User_ID]=UsrTable.[Id] order by UsrTable.[apellido]" _
            '                        , "CREATE VIEW Zvw_ONLINEUSERS_100 (USER_DESCRIPTION, Hora_de_Conexion, Ultima_Actividad, Usuario_de_Windows, PC_Nro, PC, timeout) AS select UsrTable.Description, UCM.C_Time as Hora_de_Conexion, UCM.U_Time as Ultima_Actividad, UCM.WinUser as Usuario_de_Windows, Estreg.ID as PC_Nro,EStreg.M_Name as PC,UCM.Time_Out as timeout from UCM, Estreg, UsrTable Where UCM.USER_ID=UsrTable.ID and UCM.WinUser=Estreg.W_User" _
            '                        , "CREATE VIEW Zvw_RESTRICTION_100 (DOC_TYPE_ID, INDEX_ID,STRING_VALUE,RESTRICTION_ID,RESTRICTION_NAME, USER_ID) AS select DOC_TYPE_ID, INDEX_ID, STRING_VALUE, r.RESTRICTION_ID,  RESTRICTION_NAME,USER_ID from doc_restrictions r,doc_restriction_r_user u where u.restriction_id=r.restriction_id" _
            '                        , "CREATE View Zvw_UsersPrint_100 As Select top 100 User_hst.Action_date as Fecha,(UsrTable.Apellido + '  ' + usrtable.nombres) as Usuario,user_hst.S_OBJECT_ID as Documento from User_hst, Usrtable where user_hst.ACTION_TYPE=9 and user_hst.[User_ID]=UsrTable.[Id] order by UsrTable.[apellido]" _
            '                        , "CREATE View Zvw_USR_Rights_100 As select u.ID [USER_ID], u.[name] [USER_NAME], r.rtype RIGHT_TYPE, r.objid OBJECTID,Aditional from usrtable u, usr_r_group urr, usr_rights r where (u.id=urr.usrid and urr.groupid=r.groupid) union select u.ID [USER_ID], u.[name] [USER_NAME],r.rtype RIGHT_TYPE, r.objid OBJECTID,Aditional from usrtable u, usr_rights r where u.id=r.groupid" _
            '                        , "CREATE VIEW Zvw_docgroups_rights_100 AS SELECT     USR_RIGHTS.GROUPID AS ID, DOC_TYPE_GROUP.DOC_TYPE_GROUP_NAME, RIGHTSTYPE.RIGHTSTYPE FROM USR_RIGHTS INNER JOIN  OBJECTTYPES ON USR_RIGHTS.OBJID = OBJECTTYPES.OBJECTTYPESID INNER JOIN  RIGHTSTYPE ON USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID INNER JOIN DOC_TYPE_GROUP ON USR_RIGHTS.ADITIONAL = DOC_TYPE_GROUP.DOC_TYPE_GROUP_ID" _
            '                        , "CREATE VIEW Zvw_doctypes_rights_100 AS SELECT USR_RIGHTS.GROUPID AS ID, USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, USR_RIGHTS.ADITIONAL,  OBJECTTYPES.OBJECTTYPES, RIGHTSTYPE.RIGHTSTYPE, DOC_TYPE.DOC_TYPE_NAME FROM USR_RIGHTS, OBJECTTYPES, RIGHTSTYPE, DOC_TYPE WHERE USR_RIGHTS.OBJID = OBJECTTYPES.OBJECTTYPESID AND USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID AND USR_RIGHTS.ADITIONAL = DOC_TYPE.DOC_TYPE_ID" _
            '                        , "CREATE VIEW Zvw_objects_rights_100 AS SELECT USR_RIGHTS.GROUPID AS ID, USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, USR_RIGHTS.ADITIONAL, OBJECTTYPES.OBJECTTYPES, RIGHTSTYPE.RIGHTSTYPE FROM USR_RIGHTS, OBJECTTYPES, RIGHTSTYPE WHERE USR_RIGHTS.OBJID = OBJECTTYPES.OBJECTTYPESID AND USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID" _
            '                        , "CREATE VIEW Zvw_restrictions_100 AS SELECT  DISTINCT USRGROUP.ID,DOC_TYPE.DOC_TYPE_NAME, DOC_RESTRICTIONS.RESTRICTION_NAME FROM    USRGROUP,DOC_RESTRICTIONS,DOC_TYPE,DOC_RESTRICTION_R_USER WHERE DOC_RESTRICTIONS.DOC_TYPE_ID = DOC_TYPE.DOC_TYPE_ID AND DOC_RESTRICTIONS.RESTRICTION_ID = DOC_RESTRICTION_R_USER.RESTRICTION_ID AND USRGROUP.ID = DOC_RESTRICTION_R_USER.USER_ID UNION SELECT  DISTINCT USRTABLE.ID,DOC_TYPE.DOC_TYPE_NAME, DOC_RESTRICTIONS.RESTRICTION_NAME FROM USRTABLE,DOC_RESTRICTIONS,DOC_TYPE,DOC_RESTRICTION_R_USER WHERE DOC_RESTRICTIONS.DOC_TYPE_ID = DOC_TYPE.DOC_TYPE_ID AND DOC_RESTRICTIONS.RESTRICTION_ID = DOC_RESTRICTION_R_USER.RESTRICTION_ID AND  USRTABLE.ID = DOC_RESTRICTION_R_USER.USER_ID" _
            '                        , "CREATE VIEW Zvw_permisos_100 AS SELECT TOP 100 PERCENT DOC_TYPE.DOC_TYPE_NAME AS Documento, RIGHTSTYPE.RIGHTSTYPE AS Permiso, USRTABLE.NAME AS Usuario FROM USR_RIGHTS INNER JOIN RIGHTSTYPE ON USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID INNER JOIN USRTABLE ON USR_RIGHTS.GROUPID = USRTABLE.ID INNER JOIN DOC_TYPE ON USR_RIGHTS.ADITIONAL = DOC_TYPE.DOC_TYPE_ID WHERE (USR_RIGHTS.ADITIONAL <> - 1) AND (USR_RIGHTS.OBJID = 2) ORDER BY USRTABLE.NAME, DOC_TYPE.DOC_TYPE_NAME, RIGHTSTYPE.RIGHTSTYPE" _
            '                        , "Create View Zvw_volumenes_100 As Select top 100 percent  dg.disk_group_name Lista, dv.disk_vol_name Volumen, Disk_vol_path Ruta, Disk_vol_files Archivos,disk_vol_size MB, (disk_vol_size - (disk_vol_size_len/1024)) Libre from disk_group dg inner join Disk_group_R_Disk_volume on Disk_group_R_Disk_volume.DISK_GROUP_ID=dg.DISK_GROUP_ID inner join Disk_volume dv on dv.DISK_VOL_ID=Disk_group_R_Disk_volume.Disk_VOLUME_ID order by dg.disk_group_name,dv.disk_vol_name" _
            '                        , "CREATE VIEW Zvw_WFTASKHISTORY_100 AS SELECT     WFStepHst.Doc_Id, WFStep.Name AS etapa, WFStepHst.CheckIn AS checkin,(SELECT usrtable.name FROM usrtable WHERE wfstephst.ucheckin = usrtable.id) AS usercheckin,(SELECT wfdocumentstate.name FROM wfdocumentstate WHERE wfstephst.ci_doc_state_id = wfdocumentstate.doc_state_id) AS statecheckin, WFStepHst.CheckOut AS checkout,(SELECT usrtable.name FROM usrtable WHERE wfstephst.ucheckout = usrtable.id) AS usercheckout,(SELECT wfdocumentstate.name FROM wfdocumentstate WHERE wfstephst.co_doc_state_id = wfdocumentstate.doc_state_id) AS statecheckout  FROM WFStepHst INNER JOIN WFStep ON WFStepHst.Step_Id = WFStep.step_Id" _
            '                        , "CREATE VIEW Zvw_WFViewdocsstates_100 AS SELECT Step_Id, Doc_State_ID, Description, Name, Initial FROM WFDocumentState DOCSTATE WHERE Initial = 1" _
            '                        , "CREATE VIEW Zvw_WFViewDocStepsStates_100 AS SELECT WFDocumentState.Name, WFDocumentState.Description, WFDocumentState.Doc_State_ID, WFDocumentState.Step_Id, WFDocumentState.Initial FROM WFDocumentState INNER JOIN WFStep ON WFDocumentState.Step_Id = WFStep.step_Id" _
            '                        , "CREATE VIEW Zvw_WFViewForms_100 AS SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type, ZFrms.ObjectTypeId, ZFrms.Path, ZFrms.Description, Ztype_Zfrms.Form_Id, Ztype_Zfrms.DocType_Id, WF_Frms.Step_Id, WF_Frms.Sort FROM ZFrms INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id INNER JOIN WF_Frms ON Ztype_Zfrms.Form_Id = WF_Frms.Form_Id" _
            '                        , "CREATE VIEW Zvw_ZstepUserGroups_100 AS SELECT USRGROUP.ID, USRGROUP.NAME, USRGROUP.DESCRIPCION, USR_RIGHTS.ADITIONAL, USR_RIGHTS.RTYPE, USR_RIGHTS.OBJID FROM USR_RIGHTS INNER JOIN USRGROUP ON USR_RIGHTS.GROUPID = USRGROUP.ID WHERE USR_RIGHTS.RTYPE = 1  AND USR_RIGHTS.OBJID = 42" _
            '                        , "CREATE VIEW Zvw_ZViewForms_100 AS SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type, ZFrms.ObjectTypeId, ZFrms.Path, ZFrms.Description, Ztype_Zfrms.Form_Id, Ztype_Zfrms.DocType_Id FROM ZFrms INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id" _
            '                        , "Create View Zvw_DocTypeInWF_100 as Select wf.[name] as [Workflow],zdoctype.[doc_type_name] as [Tipo de Documento] from Zvw_ZViewWF_100 wf inner join ZWFViewWfDoctypes zdoctype on wf.[work_id]=zdoctype.WFID" _
            '                        , "CREATE VIEW Zvw_WFDocumentCOUNT_100 AS SELECT COUNT(Doc_ID) AS DCOUNT, step_Id FROM WFDocument GROUP BY step_Id" _
            '                        , "CREATE VIEW ZVIEWWFSTEPS AS SELECT step_Id, work_id, Name, Description, Help, CreateDate, ImageIndex, EditDate, LocationX, LocationY, max_docs, max_Hours, StartAtOpenDoc FROM WFStep" _
            '                        , "CREATE VIEW Zvw_ZVIEWWFUserSTEPS_100 AS SELECT WFStep.step_Id, WFStep.work_id, WFStep.Name, WFStep.Description, WFStep.Help, WFStep.CreateDate, WFStep.ImageIndex, WFStep.EditDate, WFStep.LocationX, WFStep.LocationY, WFStep.max_docs, USR_RIGHTS.GROUPID, USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, WFStep.max_Hours, WFStep.StartAtOpenDoc FROM WFStep INNER JOIN USR_RIGHTS ON WFStep.step_Id = USR_RIGHTS.ADITIONAL" _
            '                        , "Create View Zvw_ZWFRulesByStep_100 as SELECT DISTINCT TOP 100 PERCENT WFWorkflow.Name AS Workflow, WFStep.Name AS Etapas, ZWFViewRules.Description AS Regla, ZWFViewRules.RuleResult AS Resultado FROM WFStep INNER JOIN WFStepRules ON WFStep.step_Id = WFStepRules.step_Id INNER JOIN WFWorkflow ON WFStep.work_id = WFWorkflow.work_id INNER JOIN ZWFViewRules ON WFStep.step_Id = ZWFViewRules.step_Id WHERE (ZWFViewRules.Enabled IS NOT NULL) ORDER BY WFWorkflow.Name, WFStep.Name, ZWFViewRules.Description" _
            '                        , "create view Zvw_ZWfStepsStates_100 as SELECT distinct  WFWorkflow.Name AS Workflow,WFStep.Name AS Etapa, WFDocumentState.Name AS Estado, WFDocument.Name AS Documento FROM WFStep INNER JOIN WFDocumentState ON WFStep.step_Id = WFDocumentState.Step_Id INNER JOIN WFStep_State ON WFDocumentState.Doc_State_ID = WFStep_State.SStat_ID INNER JOIN WFDocument ON WFStep.step_Id = WFDocument.step_Id INNER JOIN WFWorkflow ON WFStep.work_id = WFWorkflow.work_id CROSS JOIN WF_DT" _
            '                        , "CREATE VIEW Zvw_WFDocStepsXUser_100 AS SELECT WFDocument.Doc_ID, WFDocument.DOC_TYPE_ID, WFDocument.Folder_ID, WFDocument.step_Id, WFDocument.Do_State_ID, WFDocument.Name, WFDocument.IconId, WFDocument.CheckIn, WFStepHst.UCheckIn AS ucheckinid, USRTABLE.NAME AS ucheckinname, USRTABLE.DESCRIPTION, USRTABLE.ADDRESS_BOOK, USRTABLE.NOMBRES, USRTABLE.APELLIDO, USRTABLE.CORREO, USRTABLE.TELEFONO, USRTABLE.PUESTO, USRTABLE.FIRMA, USRTABLE.FOTO  FROM WFDocument INNER JOIN WFStepHst ON WFDocument.Doc_ID = WFStepHst.Doc_Id AND WFDocument.CheckIn = WFStepHst.CheckIn  INNER JOIN USRTABLE ON WFStepHst.UCheckIn = USRTABLE.ID"}

            'Dim SQL As String





            'For Each SQL In StrSql
            '    Try
            '        Zpaq.IfExists(Me.obtenerNombreVista(SQL, CREATEREPLACEVIEW), ZPaq.Tipo.View, Me.CanDrop)
            '        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)
            '    Catch ex As Exception
            '        Dim exn As New Exception("ERROR | No se pudo crear la vista " & Me.obtenerNombreVista(SQL, CREATEREPLACEVIEW) & ", excepción: " & ex.ToString)
            '        'ZException.Log(exn, False)
            '    End Try
            'Next

        Else
            'ORACLE
            Dim StrSql() As Object = {"create or replace view Zvw_BESTDOCUMENTS_100 as SELECT UCM.WINUSER as Usuario,USRTABLE.DESCRIPTION as " & Chr(34) & " Nombre de Usuario " & Chr(34) & ",UCM.WINPC as PC,UCM.C_TIME as " & Chr(34) & " Hora de Conexion" & Chr(34) & ",UCM.U_TIME as " & Chr(34) & "Ultima accion" & Chr(34) & ",UCM.TIME_OUT as Timeout FROM UCM, USRTABLE WHERE UCM.USER_ID = UsrTable.ID Order by UCM.WINUSER" _
                                        , "create or replace view Zvw_WFHistory_200 AS SELECT     Doc_Id AS [Id Documento], CheckIn AS [Check In], CheckOut AS [Check Out], Step_Id AS Etapa, CI_Doc_state_id AS [Estado Inicial], CO_Doc_state_id AS [Estado Final], UCheckIn AS [Usuario Check In], UCheckOut AS [Usuario Check Out] FROM(WFStepHst) ORDER BY Doc_Id" _
                                        , "create or replace view Zvw_DOCDELETED_100 as select name as Usuario,Action_Date as Fecha, S_Object_Id as Documento from user_hst,usrtable where Action_ID=4 and user_hst.user_id=usrtable.id" _
                                        , "create or replace view Zvw_DOCSEND_100 as select name as Usuario,Action_Date as Fecha, S_Object_Id as Documento from user_hst,usrtable where Action_ID=11 and user_hst.user_id=usrtable.id" _
                                        , "create or replace view Zvw_ESTREG_100 as Select ID, M_NAME PC, W_USER " & Chr(34) & "Usuario Windows" & Chr(34) & ",VER Version, UPDATED Actualizado, LAST_CHECK" & Chr(34) & "Ultima verificacion" & Chr(34) & "from estreg" _
                                        , "create or replace VIEW Zvw_HISTORYVIEW_100 AS SELECT USER_HST.ACTION_DATE as Fecha, USRTABLE.NAME as Usuario, USER_HST.ACTION_ID as Accion FROM USER_HST, USRTABLE WHERE USER_HST.USER_ID =usrtable.id and USER_HST.ACTION_DATE > sysdate -30 order by usuario" _
                                        , "create or replace view zvw_USUARIOS_100 AS SELECT USRTABLE.ID, USRTABLE.DESCRIPTION, USRTABLE.NAME FROM USRTABLE" _
                                        , "create or replace view Zvw_INACTIVEUSERS_100 as Select Description from usrtable where name not in (Select distinct(Usuario)  from Zvw_HISTORYVIEW_100) order by Description" _
                                        , "create or replace view Zvw_INSTALACIONES_100 as Select ID as" & Chr(34) & "Nro de PC" & Chr(34) & ", W_User as" & Chr(34) & "Usuario Windows" & Chr(34) & ", M_Name as PC, VER as " & Chr(34) & " Version Zamba " & Chr(34) & " from estreg order by W_User" _
                                        , "create or replace view Zvw_LoginsFailed_100 as Select User_hst.Action_date as Fecha,(UsrTable.Apellido + ' ' + usrtable.nombres) as Usuario from User_hst, Usrtable where user_hst.ACTION_TYPE=27 and user_hst.User_ID=UsrTable.Id order by UsrTable.apellido" _
                                        , "create or replace view Zvw_ONLINEUSERS_100 as SELECT UCM.WINUSER as Usuario,usrtable.DESCRIPTION as" & Chr(34) & "Nombre de Usuario" & Chr(34) & ",UCM.WINPC as PC,UCM.C_TIME as" & Chr(34) & "Hora de Conexion" & Chr(34) & ",UCM.U_TIME as" & Chr(34) & "Ultima accion" & Chr(34) & ",UCM.TIME_OUT as Timeout FROM UCM, usrtable WHERE ( UCM.USER_ID = usrtable.ID) Order by UCM.WINUSER" _
                                        , "create or replace view Zvw_RESTRICTION_100 as select DOC_TYPE_ID, INDEX_ID, STRING_VALUE, r.RESTRICTION_ID,RESTRICTION_NAME,USER_ID from doc_restrictions r,doc_restriction_r_user u where u.restriction_id=r.restriction_id" _
                                        , "create or replace view Zvw_UsersPrint_100 as Select User_hst.Action_date as Fecha, UsrTable.Description as Usuario,user_hst.S_OBJECT_ID as Documento from User_hst, Usrtable where user_hst.ACTION_TYPE=9 and user_hst.User_ID=UsrTable.Id and (TO_DATE(Substr(ACTION_DATE,1,10),'yyyy-mm-dd') > Sysdate -30) order by UsrTable.Description" _
                                        , "create or replace view Zvw_USR_Rights_100 as select u.ID USER_ID,u.name USER_NAME,r.rtype RIGHT_TYPE,r.objid OBJECTID,Aditional from usrtable u,usr_r_group urr,usr_rights r where u.id=urr.usrid and urr.groupid=r.groupid union select u.ID USER_ID,u.name USER_NAME,r.rtype RIGHT_TYPE,r.objid OBJECTID,Aditional from usrtable u,usr_rights r where u.id=r.groupid" _
                                        , "create or replace view Zvw_docgroups_rights_100 as SELECT USR_RIGHTS.GROUPID AS ID, DOC_TYPE_GROUP.DOC_TYPE_GROUP_NAME, RIGHTSTYPE.RIGHTSTYPE FROM USR_RIGHTS, OBJECTTYPES, RIGHTSTYPE, DOC_TYPE_GROUP WHERE USR_RIGHTS.OBJID = OBJECTTYPES.OBJECTTYPESID AND USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID AND USR_RIGHTS.ADITIONAL = DOC_TYPE_GROUP.DOC_TYPE_GROUP_ID" _
                                        , "create or replace view Zvw_doctypes_rights_100 as SELECT USR_RIGHTS.GROUPID AS ID, USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, USR_RIGHTS.ADITIONAL,OBJECTTYPES.OBJECTTYPES, RIGHTSTYPE.RIGHTSTYPE, DOC_TYPE.DOC_TYPE_NAME FROM USR_RIGHTS, OBJECTTYPES, RIGHTSTYPE, DOC_TYPE WHERE USR_RIGHTS.OBJID = OBJECTTYPES.OBJECTTYPESID AND USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID AND USR_RIGHTS.ADITIONAL = DOC_TYPE.DOC_TYPE_ID" _
                                        , "create or replace view Zvw_objects_rights_100 as SELECT USR_RIGHTS.GROUPID AS ID, USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, USR_RIGHTS.ADITIONAL,OBJECTTYPES.OBJECTTYPES, RIGHTSTYPE.RIGHTSTYPE FROM USR_RIGHTS, OBJECTTYPES, RIGHTSTYPE WHERE USR_RIGHTS.OBJID = OBJECTTYPES.OBJECTTYPESID AND USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID" _
                                        , "create or replace view Zvw_restrictions_100 as SELECT  DISTINCT USRGROUP.ID,DOC_TYPE.DOC_TYPE_NAME, DOC_RESTRICTIONS.RESTRICTION_NAME FROM USRGROUP,DOC_RESTRICTIONS,DOC_TYPE,DOC_RESTRICTION_R_USER WHERE DOC_RESTRICTIONS.DOC_TYPE_ID = DOC_TYPE.DOC_TYPE_ID AND DOC_RESTRICTIONS.RESTRICTION_ID = DOC_RESTRICTION_R_USER.RESTRICTION_ID AND USRGROUP.ID = DOC_RESTRICTION_R_USER.USER_ID UNION SELECT  DISTINCT USRTABLE.ID,DOC_TYPE.DOC_TYPE_NAME, DOC_RESTRICTIONS.RESTRICTION_NAME FROM USRTABLE,DOC_RESTRICTIONS,DOC_TYPE,DOC_RESTRICTION_R_USER WHERE DOC_RESTRICTIONS.DOC_TYPE_ID = DOC_TYPE.DOC_TYPE_ID AND DOC_RESTRICTIONS.RESTRICTION_ID = DOC_RESTRICTION_R_USER.RESTRICTION_ID AND USRTABLE.ID = DOC_RESTRICTION_R_USER.USER_ID" _
                                        , "create or replace view Zvw_permisos_100 as SELECT DOC_TYPE.DOC_TYPE_NAME AS Documento, RIGHTSTYPE.RIGHTSTYPE AS Permiso, USRTABLE.NAME AS Usuario FROM USR_RIGHTS INNER JOIN RIGHTSTYPE ON USR_RIGHTS.RTYPE = RIGHTSTYPE.RIGHTSTYPEID INNER JOIN USRTABLE ON USR_RIGHTS.GROUPID = USRTABLE.ID INNER JOIN DOC_TYPE ON USR_RIGHTS.ADITIONAL = DOC_TYPE.DOC_TYPE_ID WHERE (USR_RIGHTS.ADITIONAL <> - 1) AND (USR_RIGHTS.OBJID = 2) ORDER BY USRTABLE.NAME, DOC_TYPE.DOC_TYPE_NAME, RIGHTSTYPE.RIGHTSTYPE" _
                                        , "create or replace view Zvw_volumenes_100 as Select dg.disk_group_name Lista, dv.disk_vol_name Volumen, Disk_vol_path Ruta, Disk_vol_files Archivos,disk_vol_size MB, (disk_vol_size - (disk_vol_size_len/1024)) Libre from disk_group dg inner join Disk_group_R_Disk_volume on Disk_group_R_Disk_volume.DISK_GROUP_ID=dg.DISK_GROUP_ID inner join Disk_volume dv on dv.DISK_VOL_ID=Disk_group_R_Disk_volume.Disk_VOLUME_ID order by dg.disk_group_name,dv.disk_vol_name" _
                                        , "create or replace view Zvw_ZstepUserGroups_100 as SELECT USRGROUP.ID, USRGROUP.NAME, USRGROUP.DESCRIPCION, USR_RIGHTS.ADITIONAL, USR_RIGHTS.RTYPE, USR_RIGHTS.OBJID FROM USR_RIGHTS INNER JOIN USRGROUP ON USR_RIGHTS.GROUPID = USRGROUP.ID WHERE (USR_RIGHTS.RTYPE = 1) AND (USR_RIGHTS.OBJID = 42)" _
                                        , "create or replace view Zvw_ZViewForms_100 as SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type,ZFrms.ObjectTypeId, ZFrms.Path, ZFrms.Description,Ztype_Zfrms.Form_Id,Ztype_Zfrms.DocType_Id FROM ZFrms INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id" _
                                        , "create or replace view Zvw_WFDocumentCOUNT_100 as SELECT COUNT(Doc_ID) AS DCOUNT, step_Id FROM WFDocument GROUP BY step_Id" _
                                        , "create or replace view Zvw_WFSTEPS_100 as SELECT step_Id, work_id, Name, Description, Help, CreateDate, ImageIndex, EditDate, LocationX, LocationY, max_docs, max_Hours, StartAtOpenDoc FROM WFStep" _
                                        , "create or replace view Zvw_WFUserSTEPS_100 as SELECT WFStep.step_Id, WFStep.work_id, WFStep.Name, WFStep.Description, WFStep.Help, WFStep.CreateDate, WFStep.ImageIndex, WFStep.EditDate, WFStep.LocationX, WFStep.LocationY, WFStep.max_docs, USR_RIGHTS.GROUPID,USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, WFStep.max_Hours, WFStep.StartAtOpenDoc FROM WFStep INNER JOIN USR_RIGHTS ON WFStep.step_Id = USR_RIGHTS.ADITIONAL" _
                                        , "CREATE OR REPLACE VIEW ZVIEWFORMS (NAME,PARENTID,TYPE,OBJECTTYPEID,PATH,DESCRIPTION,FORM_ID,DOCTYPE_ID) AS SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type,ZFrms.ObjectTypeId, ZFrms.Path, ZFrms.Description,Ztype_Zfrms.Form_Id,Ztype_Zfrms.DocType_Id FROM ZFrms INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id WITH READ ONLY" _
                                        , "create or replace view Zvw_DocTypeInWF_100 as Select wf.name as Workflow,zdoctype.doc_type_name as " & Chr(34) & "Tipo de Documento" & Chr(34) & " from Zvw_ZViewWF_100 wf inner join ZWFViewWfDoctypes zdoctype on wf.work_id=zdoctype.WFID"}


            Dim SQL As String
            Dim Cantidad As Int32 = 0
            Dim Cantidad2 As Int32 = 0

            For Each SQL In StrSql
                Try
                    Cantidad2 += 1
                    ZPaq.IfExists(obtenerNombreVista(SQL, CREATEVIEW), ZPaq.Tipo.View, Me.CanDrop)
                    Server.Con.ExecuteNonQuery(CommandType.Text, SQL)
                    Cantidad += 1
                Catch ex As Exception
                    Dim exn As New Exception("ERROR | No se pudo crear la vista " & obtenerNombreVista(SQL, CREATEREPLACEVIEW) & " (  " & SQL & "  ) , excepción: " & ex.ToString)
                    'ZException.Log(exn, False)
                    MessageBox.Show(exn.Message)
                End Try
            Next

            'Dim strcreate As String
            ''package zsp_barcode_100
            'strcreate = "create or replace package zsp_barcode_100 as PROCEDURE InsertBarCode(idbarcode IN  ZBarCode.ID%TYPE, DocTypeId IN ZBarCode.doc_type_id%TYPE, UserId in ZBarCode.Userid%type,Doc_id in ZBarCode.Doc_Id%type); PROCEDURE  UpdBarCode(caratulaid IN zbarcode.id%TYPE,lote IN zbarcode.batch%TYPE,caja IN zbarcode.box%TYPE); end;"
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            'strcreate = "create or replace package body zsp_barcode_100 as PROCEDURE InsertBarCode(idbarcode IN  ZBarCode.ID%TYPE, DocTypeId IN ZBarCode.doc_type_id%TYPE, UserId in ZBarCode.Userid%type,Doc_id in ZBarCode.Doc_Id%type) IS BEGIN Insert into ZBarCode(Id,Fecha,Doc_Type_ID,UserId,Doc_Id) Values(idbarcode,Sysdate,DocTypeId,Userid,Doc_id); COMMIT; END InsertBarCode; PROCEDURE  UpdBarCode(caratulaid IN zbarcode.id%TYPE,lote IN zbarcode.batch%TYPE,caja IN zbarcode.box%TYPE) IS BEGIN UPDATE zbarcode SET scanned='SI', scanneddate=sysdate, batch=lote, box=caja WHERE id = caratulaid; COMMIT; END UpdBarCode; end zsp_barcode_100;"
            'Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
            MessageBox.Show("SE CREARON " & Cantidad & " VISTAS DE " & Cantidad2)
        End If
        Return True
    End Function


    'Private Sub IfExistsDrop(ByVal Vista As String)
    '    Try
    '        Dim strexists As String = "IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = '" & Vista & "') DROP VIEW " & Vista
    '        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strexists)
    '    Catch ex As Exception
    '        Dim exn As New Exception("ERROR | ERROR AL CHECKEAR SI LA VISTA " & Vista & " EXISTE. " & ex.ToString)
    '        'ZException.Log(exn, False)
    '    End Try
    'End Sub

    Private Shared Function obtenerNombreVista(ByVal sSql As String, ByVal sSqlCreateVista As String) As String
        Dim i As Int32
        Dim sVista As String
        Try
            sVista = sSql.ToUpper.Replace(sSqlCreateVista, "").Trim
            i = sVista.IndexOf(" ")
            Return sVista.Substring(0, i)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "Vistas"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateViews_General2
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea todas las vista"
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("03/02/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "1.1.0"
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
            Return 70
        End Get
    End Property

End Class
