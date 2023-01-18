Imports zamba.servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32
Imports System.IO
' CREADO : 15/11/2006 - 
Public Class PAQ_CreateStores_WorkFlow

    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateStores_WorkFlow"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateStores_WorkFlow
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("15/11/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea Stores varios para workflow, en SQL Server"
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("21/12/2006")
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
            Return 86
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Try
            Dim sql As New System.Text.StringBuilder
            Dim sql2 As New System.Text.StringBuilder
            Dim sw As New IO.StreamWriter(Path.Combine(Application.StartupPath, "Script.sql"), True)
            '----PATH P/GUARDAR TODO EL SCRIPT

            If ZPaq.IfExists("zsp_workflow_100_DeleteStepById", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_DeleteStepById ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_DeleteStepById ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pStepId numeric ")
            sql.Append(ControlChars.NewLine)
            sql.Append("As")
            sql.Append(ControlChars.NewLine)
            sql.Append("DELETE WfSTEP where STEP_id = @pStepId")
            sql.Append(ControlChars.NewLine)

            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            If ZPaq.IfExists("Zsp_workflow_100_SaveIcon", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_SaveIcon ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_SaveIcon ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@locX decimal,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@locY decimal,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@StepId int")
            sql.Append(ControlChars.NewLine)
            sql.Append("AS")
            sql.Append(ControlChars.NewLine)
            sql.Append("UPDATE WFSTEP set LocationX = @locX, LocationY = @locY ")
            sql.Append(ControlChars.NewLine)
            sql.Append("Where step_id = @StepId")
            sql.Append(ControlChars.NewLine)

            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)


            If ZPaq.IfExists("Zsp_workflow_100_UpdateColor", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_UpdateColor ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_UpdateColor ")
            End If
            sql.Append(ControlChars.NewLine)
            'Modifico el store para que lo cree como varchar de 50 - MC
            sql.Append("@color varchar(50),")
            sql.Append(ControlChars.NewLine)
            sql.Append("@stepid int")
            sql.Append(ControlChars.NewLine)
            sql.Append("AS")
            sql.Append(ControlChars.NewLine)
            sql.Append("UPDATE WFSTEP set Color = @color where step_id =@stepid")
            sql.Append(ControlChars.NewLine)


            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            If ZPaq.IfExists("Zsp_workflow_200_InsertWFStep", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_200_InsertWFStep ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_200_InsertWFStep ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@WFID int,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@StepId int,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@Name nvarchar,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@descripcion nvarchar,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@ayuda nvarchar,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@ImageIndex int,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@LocationX decimal,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@LocationY decimal,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@MaxDocs int,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@MaxHours int,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@StartAtOpenDoc int,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@Color nvarchar,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@Height decimal,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@Width decimal")
            sql.Append(ControlChars.NewLine)
            sql.Append("AS")
            sql.Append(ControlChars.NewLine)
            sql.Append("INSERT INTO WFSTEP(work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc,Color,Height,Width) ")
            sql.Append(ControlChars.NewLine)
            sql.Append("VALUES(@WFID,@StepId,@Name,@descripcion,@ayuda,GetDate(),GetDate(),@ImageIndex,@LocationX, @LocationY, @MaxDocs, @MaxHours, @StartAtOpenDoc,@Color,@Height,@Width)")
            sql.Append(ControlChars.NewLine)
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            If ZPaq.IfExists("Zsp_workflow_200_getWorkflows", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_200_getWorkflows")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_200_getWorkflows")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("AS")
            sql.Append(ControlChars.NewLine)
            sql.Append("Select Distinct(Select Name From WfWorkflow Where WfWorkflow.work_id =  t1.work_id) as Workflow, t1.Name as Etapa, t2.DCOUNT as Documentos from Zvw_ZVIEWWFUserSTEPS_100 t1 inner join Zvw_WFDocumentCOUNT_100 t2 On t1.step_id=t2.step_id")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)


            If ZPaq.IfExists("Zsp_workflow_100_documentosAsignados", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_documentosAsignados")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_documentosAsignados")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("AS")
            sql.Append(ControlChars.NewLine)
            sql.Append("Select *, isnull(NombreUsuario,'Sin Asignar') as Usuario from ZWFViewReport")

            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)



            If ZPaq.IfExists("Zsp_workflow_100_FillSteps", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_FillSteps")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_FillSteps")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@work_id int")
            sql.Append(ControlChars.NewLine)
            sql.Append("AS")
            sql.Append(ControlChars.NewLine)
            sql.Append("Select * from wfstep where work_id=@work_id")
            sql.Append(ControlChars.NewLine)

            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)


            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '**************************************
            '*************STORES NUEVOS 20-12-2006
            '*************************************

            If ZPaq.IfExists("zsp_workflow_100_CloseTask ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_CloseTask ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_CloseTask ")
            End If
            sql.Append("@pTaskId numeric AS UPDATE WFDOCUMENT SET USER_ASIGNED = 0 ,CheckIn = NULL,User_Asigned_By = 0 ,Date_Asigned_By = NULL WHERE Task_ID = @pTaskId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------
            If ZPaq.IfExists("zsp_workflow_100_DeleteDocumentByTask ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_DeleteDocumentByTask ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_DeleteDocumentByTask ")
            End If
            sql.Append("@pTaskId numeric AS DELETE WFDOCUMENT WHERE Task_ID = @pTaskId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------
            If ZPaq.IfExists("Zsp_workflow_100_DeleteRule", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  Zsp_workflow_100_DeleteRule ")
            Else
                'Create
                sql.Append("CREATE Procedure  Zsp_workflow_100_DeleteRule ")
            End If
            sql.Append("@RuleID numeric AS DELETE FROM wfrules where id=@RuleID")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------
            If ZPaq.IfExists("zsp_workflow_100_DeleteStepStateById", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  zsp_workflow_100_DeleteStepStateById ")
            Else
                'Create
                sql.Append("CREATE Procedure  zsp_workflow_100_DeleteStepStateById ")
            End If
            sql.Append("@pStateID numeric AS DELETE WFStepStates where doc_state_Id = @pStateID")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------
            If ZPaq.IfExists("zsp_workflow_100_DeleteWorkFlowByWfId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  zsp_workflow_100_DeleteWorkFlowByWfId  ")
            Else
                'Create
                sql.Append("CREATE Procedure  zsp_workflow_100_DeleteWorkFlowByWfId  ")
            End If
            sql.Append("@pWork_ID numeric as DELETE wfworkflow where work_id = @pWork_ID ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            '-------------------------------------------------
            If ZPaq.IfExists("zsp_workflow_100_GetAllWf", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_GetAllWf ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_GetAllWf ")
            End If
            sql.Append(" as SELECT * FROM WFworkflow ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------
            If ZPaq.IfExists("zsp_workflow_100_GetDocCountByStepId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_GetDocCountByStepId ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_GetDocCountByStepId ")
            End If
            sql.Append("@pStepId numeric AS SELECT DCOUNT FROM ZVIEWWFDOCUMENTCOUNT WHERE STEP_ID = @pStepId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_GetGroupIdByStepId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  zsp_workflow_100_GetGroupIdByStepId ")
            Else
                'Create
                sql.Append("CREATE Procedure  zsp_workflow_100_GetGroupIdByStepId ")
            End If
            sql.Append(" @pStepId numeric as Select GROUPID from ZVIEWWFUSERSTEPS where step_id = @pStepId; ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_GetStatesByStepId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_GetStatesByStepId ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_GetStatesByStepId ")
            End If
            sql.Append("@pStepId numeric as Select * from WFStepStates where step_id =@pStepId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_GetStatesByStepOrState", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_GetStatesByStepOrState ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_GetStatesByStepOrState ")
            End If
            sql.Append("@pStateId numeric, @pStepId numeric as Select * from WFStepStates where doc_state_id=@pStateId  or step_id = @pStepId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_GetStepIdByDocId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_GetStepIdByDocId ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_GetStepIdByDocId ")
            End If
            sql.Append("@pDocId numeric  as select step_Id from wfdocument where doc_id=@pDocId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_GetStepsByWork", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_GetStepsByWork ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_GetStepsByWork ")
            End If
            sql.Append(" @pWorkId numeric as Select * from wfstep where work_id= @pWorkId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_GetStepsOfUsrGroupByAdt", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_GetStepsOfUsrGroupByAdt ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_GetStepsOfUsrGroupByAdt ")
            End If
            sql.Append("@pAdt numeric as Select * from zstepuserGroups where aditional= @pAdt ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_GetViewStepsByStep", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_GetViewStepsByStep ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_GetViewStepsByStep ")
            End If
            sql.Append("@pStepId numeric as Select * from ZViewWFSTEPS where step_id =@pStepId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_GetViewStepsByWfId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_GetViewStepsByWfId ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_GetViewStepsByWfId ")
            End If
            sql.Append("@pWFId numeric as Select * from ZViewWFSTEPS where WORK_ID =@pWFId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_InsertRuleHistory", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_InsertRuleHistory ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_InsertRuleHistory ")

            End If
            sql.Append(" @pFolderId numeric , @pStepId numeric ,@pDoc_Id numeric, ")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pDocTypeid numeric ,@pRuleId numeric ,@pResult numeric ,@pUsrId numeric ,@pEjecDate datetime ,@pData ")
            sql.Append(ControlChars.NewLine)
            sql.Append("nvarchar(400) as INSERT INTO WFRULESHST (FOLDER_ID  ,STEP_ID   ,DOC_ID  ,DOC_TYPE_ID,Rule_Id ,Result,")
            sql.Append(ControlChars.NewLine)
            sql.Append("User_Id,Ejecution_Date,Data) VALUES (@pFolderId , @pStepId,@pDoc_Id,@pDocTypeid,@pRuleId,@pResult,@pUsrId,@pEjecDate,@pData )")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("Zsp_workflow_100_InsertRuleParamItem", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_InsertRuleParamItem ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_InsertRuleParamItem ")
            End If
            sql.Append("@RuleId numeric,@Item numeric,@Value varchar(1000) AS Insert into WFRuleParamItems(rule_id,item,value) values(@RuleId ,@Item ,@Value)")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_InsertStep ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_InsertStep  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_InsertStep  ")
            End If
            sql.Append("@pWFId numeric ,@pStepId numeric , @pName varchar(50),@pDesc varchar(100),")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pHelp varchar(100), @pCDate datetime,@pEDate datetime , @pImgInd numeric ,@pLocX decimal ,@pLocY decimal,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pMaxDocs numeric ,@pMaxHours numeric ,@pStartAt numeric as INSERT INTO WFSTEP ")
            sql.Append(ControlChars.NewLine)
            sql.Append("(work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, ")
            sql.Append(ControlChars.NewLine)
            sql.Append("LocationY,Max_Docs,Max_Hours,StartAtOpenDoc) VALUES (@pWFId ,@pStepId,@pName, ")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pDesc,@pHelp,@pCDate,@pEDate,@pImgInd ,@pLocX ,@pLocY ,@pMaxDocs ,@pMaxHours ,@pStartAt)")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_InsertStepHistory ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_InsertStepHistory  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_InsertStepHistory  ")
            End If
            sql.Append("@pDocId numeric , @pDocTypeid numeric , @pFolderId numeric,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pStepId numeric ,@pCiDocStateId numeric ,@pcheckin datetime ,@pChkOut datetime ,@pUChkIn numeric ,@pUChkOut numeric,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pCoDocStateId numeric AS INSERT INTO WFSTEPHST (DOC_ID ,DOC_TYPE_ID ,FOLDER_ID,STEP_ID,")
            sql.Append(ControlChars.NewLine)
            sql.Append("ci_Doc_State_Id ,checkin,checkout ,ucheckin ,ucheckout ,co_doc_state_id) VALUES (@pDocId ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pDocTypeid , @pFolderId ,@pStepId,@pCiDocStateId,@pcheckin ,@pChkOut,@pUChkIn ,@pUChkOut,@pCoDocStateId  )")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_InsertStepStates", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_InsertStepStates ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_InsertStepStates ")
            End If
            sql.Append(" @pStateID numeric, @pDesc nvarchar(250), @pStepId numeric, @pName varchar(50), @pInitial numeric AS INSERT INTO WFStepStates (Doc_State_id,Description,Step_Id,Name,Initial) VALUES ( @pStateID , @pDesc , @pStepId , @pName , @pInitial )")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_InsertWorkFlow ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_InsertWorkFlow  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_InsertWorkFlow  ")
            End If
            sql.Append("@pWork_ID decimal,@pWStat_Id decimal,@pName varchar(50),@pHelp varchar(200),@pDescription varchar(100),")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pDate datetime,@pEditDate datetime,@pRefreshRate numeric,@pInitialStepId numeric AS ")
            sql.Append(ControlChars.NewLine)
            sql.Append("Insert into wfworkflow (work_id,Wstat_id,name,help,description,createdate,editdate,refreshrate,initialstepid) ")
            sql.Append(ControlChars.NewLine)
            sql.Append("VALUES (@pWork_ID ,@pWStat_Id ,@pName ,@pHelp ,@pDescription ,@pDate,@pEditDate,@pRefreshRate ,@pInitialStepId)")

            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("Zsp_workflow_100_InsWF ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_InsWF ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_InsWF ")
            End If
            sql.Append("@pWork_ID decimal,@pWStat_Id decimal ,@pName varchar(50),@pHelp varchar(200),@pDescription varchar(100),")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pDate datetime,@pEditDate datetime,@pRefreshRate numeric,@pInitialStepId numeric AS Insert into wfworkflow ")
            sql.Append(ControlChars.NewLine)
            sql.Append("(work_id,Wstat_id,[name],[help],[description],createdate,editdate,refreshrate,initialstepid) ")
            sql.Append(ControlChars.NewLine)
            sql.Append("VALUES (@pWork_ID ,@pWStat_Id ,@pName ,@pHelp ,@pDescription ,@pDate,@pEditDate,@pRefreshRate ,@pInitialStepId )")

            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("Zsp_workflow_100_InsWFStep", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_InsWFStep  ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_InsWFStep  ")
            End If
            sql.Append("@pWFId numeric,@pStepId numeric, @pName varchar(50),@pDesc varchar(100),@pHelp varchar(100), ")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pCDate datetime,@pEDate datetime, @pImgInd numeric,@pLocX decimal,@pLocY decimal,@pMaxDocs numeric,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pMaxHours numeric,@pStartAt numeric AS")
            sql.Append(ControlChars.NewLine)
            sql.Append("INSERT INTO WFSTEP(work_id,STEP_ID,[Name],[Description],[Help],CreateDate,EditDate,ImageIndex,LocationX, ")
            sql.Append(ControlChars.NewLine)
            sql.Append("LocationY,Max_Docs,Max_Hours,StartAtOpenDoc)")
            sql.Append(ControlChars.NewLine)
            sql.Append("VALUES (@pWFId,@pStepId,@pName,@pDesc,@pHelp,@pCDate,GetDate(),@pImgInd,@pLocX,@pLocY,@pMaxDocs,@pMaxHours,@pStartAt)")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_MoveCompleteFolder", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_MoveCompleteFolder  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_MoveCompleteFolder  ")
            End If
            sql.Append("@pStateId numeric,@pStepId numeric,@pCheckIn datetime,@pAsigned numeric,@pExpDate datetime,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pFolderId numeric as UPDATE WFDOCUMENT SET DO_STATE_ID=@pStateId ,STEP_ID =@pStepId,")
            sql.Append(ControlChars.NewLine)
            sql.Append("CheckIn = @pCheckIn,USER_ASIGNED= @pAsigned ,EXPIREDATE= @pExpDate")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_MoveTaskByDocId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_MoveTaskByDocId  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_MoveTaskByDocId  ")
            End If
            sql.Append("@pStateId numeric,@pStepId numeric,@pCheckIn datetime,@pAsigned numeric,@pExpDate datetime,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pDocId numeric AS UPDATE WFDOCUMENT SET DO_STATE_ID=@pStateId,")
            sql.Append(ControlChars.NewLine)
            sql.Append("STEP_ID = @pStepId,CheckIn=@pCheckIn,USER_ASIGNED=  @pAsigned ,EXPIREDATE= @pExpDate WHERE DOC_ID =@pDocId ")
            sql.Append(ControlChars.NewLine)
            sql2.Append(sql.ToString)
            sql.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdAsignedUserOpenTask", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdAsignedUserOpenTask ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdAsignedUserOpenTask ")
            End If
            sql.Append("@pAsignedTo numeric ,@pCheckIn numeric ,@pAsignedId numeric,@pAsgDate datetime ,@pTaskId numeric as ")
            sql.Append(ControlChars.NewLine)
            sql.Append("UPDATE WFDOCUMENT SET USER_ASIGNED = @pAsignedTo ,CheckIn =@pCheckIn,")
            sql.Append(ControlChars.NewLine)
            sql.Append("User_Asigned_By = @pAsignedId ,Date_Asigned_By = @pAsgDate WHERE Task_ID = @pTaskId ")
            sql.Append(ControlChars.NewLine)
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("Zsp_workflow_100_UpdateExpiredDateTask", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_UpdateExpiredDateTask ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_UpdateExpiredDateTask ")
            End If
            sql.Append("@ResultId numeric,@ExpireDate smalldatetime AS UPDATE WFDOCUMENT SET [EXPIREDATE]=@ExpireDate WHERE DOC_ID =@ResultId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("Zsp_workflow_100_UpdateInitialStep", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_UpdateInitialStep ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_UpdateInitialStep ")
            End If
            sql.Append("@InitialStepId numeric,@WFId numeric AS Update WFWorkflow set InitialStepId=@InitialStepId where work_id= @WFId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("Zsp_workflow_100_UpdateParamItem", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_UpdateParamItem ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_UpdateParamItem ")
            End If
            sql.Append("@Value varchar(1000),@RuleId numeric,@Item numeric AS Update WFRuleParamItems set value=@Value where rule_id=@RuleId  And Item = @Item")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdClearExclusiveTask", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdClearExclusiveTask  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdClearExclusiveTask  ")
            End If
            sql.Append("@pTaskId numeric as UPDATE WFDOCUMENT SET EXCLUSIVE=0 WHERE Task_ID = @pTaskId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdDelegateTaskByTask ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdDelegateTaskByTask  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdDelegateTaskByTask  ")
            End If
            sql.Append("@pStepId numeric, @pAsigned numeric, @pExpDate datetime,@pUserId numeric, @pAsgDate datetime,")
            sql.Append("@pTaskId numeric as UPDATE WFDOCUMENT SET STEP_ID= @pStepId ,USER_ASIGNED= @pAsigned,")
            sql.Append("EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= @pUserId ,DATE_ASIGNED_BY= @pAsgDate ,CheckIn=NULL WHERE ")
            sql.Append("Task_ID = @pTaskId ")

            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdDocTypeLifeCycle", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdDocTypeLifeCycle  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdDocTypeLifeCycle  ")
            End If
            sql.Append("@WfId bit, @DocTypeID numeric as update doc_type set Life_Cycle=@WfID where doc_type_id=@DocTypeID")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdDocumentDelegateTask", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdDocumentDelegateTask  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdDocumentDelegateTask  ")
            End If
            sql.Append("@pStepId numeric, @pExpDate datetime, @pTaskId numeric as UPDATE WFDOCUMENT SET STEP_ID=@pStepId,")
            sql.Append("USER_ASIGNED = 0 ,EXPIREDATE= @pExpDate ,USER_ASIGNED_BY=0,")
            sql.Append("DATE_ASIGNED_BY=NULL,CheckIn=NULL WHERE Task_ID =  @pTaskId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdDoSateByDocIdStateId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdDoSateByDocIdStateId  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdDoSateByDocIdStateId  ")
            End If
            sql.Append("@pStateId numeric,@pDocId numeric,@pStepId numeric as Update WFDocument Set do_state_id= @pStateId ")
            sql.Append("where doc_id= @pDocId  and step_id= @pStepId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdExpireDateByDocId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdExpireDateByDocId  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdExpireDateByDocId  ")
            End If
            sql.Append("@pExpDate datetime, @pDocId numeric AS UPDATE WFDOCUMENT SET EXPIREDATE=@pExpDate WHERE DOC_ID = @pDocId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdInitialStateByStepId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdInitialStateByStepId  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdInitialStateByStepId  ")
            End If
            sql.Append("@pInitial numeric, @pStepId numeric as UPDATE WFStepStates SET Initial = @pInitial where step_id= @pStepId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdInitialStep", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdInitialStep  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdInitialStep  ")
            End If
            sql.Append("@pIStepId numeric , @pWfid numeric as UPDATE wfworkflow SET initialstepId = @pIStepId where work_id = @pWfid ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdInitialStepState", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdInitialStepState ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdInitialStepState ")
            End If
            sql.Append("@pInitial numeric, @pStateId numeric as UPDATE WFStepStates SET Initial = @pInitial where doc_state_id= @pStateId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdRefreshRate", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdRefreshRate  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdRefreshRate  ")
            End If
            sql.Append("@pInterval numeric , @pWfid numeric as UPDATE wfworkflow SET refreshrate = @pInterval where work_id = @pWfid")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdSetExclusiveTask", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdSetExclusiveTask  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdSetExclusiveTask  ")
            End If
            sql.Append("@pTaskId numeric as UPDATE WFDOCUMENT SET EXCLUSIVE=1 WHERE Task_ID = @pTaskId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdState", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdState  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdState  ")
            End If
            sql.Append("@pStepId numeric, @pName varchar(50),@pDesc nvarchar(250), @pInitial numeric,@pStateID numeric as UPDATE ")
            sql.Append(ControlChars.NewLine)
            sql.Append("WFStepStates SET STEP_ID = @pStepId  , Name = @pName , Description = @pDesc, ")
            sql.Append(ControlChars.NewLine)
            sql.Append("Initial = @pInitial where Doc_State_Id = @pStateID ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdStateByDocId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdStateByDocId  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdStateByDocId  ")
            End If
            sql.Append("@pStepId numeric, @pAsigned numeric, @pExclusive numeric,@pExpDate datetime, @pUserId numeric, @pDocID numeric ")
            sql.Append(ControlChars.NewLine)
            sql.Append("as UPDATE WFDOCUMENT SET STEP_ID=@pStepId , USER_ASIGNED= @pAsigned ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("EXCLUSIVE = @pExclusive , EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= @pUserId WHERE DOC_ID =@pDocID ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdStateDescription", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdStateDescription  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdStateDescription  ")
            End If
            sql.Append("@pName varchar(50), @pDesc nvarchar(250), @pStateId numeric as  Update WFStepStates Set name= @pName , description= @pDesc where doc_state_id= @pStateId ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdStepByStepId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdStepByStepId  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdStepByStepId  ")
            End If
            sql.Append("@pName varchar(50) , @pDescription varchar(100) ,@pHelp varchar(100) , @pEditDate datetime , @pImgInd numeric,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pLocX decimal , @pLocY decimal , @pStart numeric,@pMaxHours numeric , @pMaxDocs numeric , @pStepId numeric ")
            sql.Append(ControlChars.NewLine)
            sql.Append("as UPDATE WFSTEP set Name = @pName ,Description=@pDescription ,Help = @pHelp ,EditDate = @pEditDate,")
            sql.Append(ControlChars.NewLine)
            sql.Append("ImageIndex = @pImgInd ,LocationX = @pLocX , LocationY=@pLocY,")
            sql.Append(ControlChars.NewLine)
            sql.Append("StartAtopenDoc=@pStart,Max_Hours=@pMaxHours,Max_Docs = @pMaxDocs where step_id = @pStepId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdStetStates", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdStetStates  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdStetStates  ")
            End If
            sql.Append("@pStepId numeric, @pName varchar(50), @pDesc nvarchar(250),@pInitial numeric, @pStateID numeric AS UPDATE ")
            sql.Append(ControlChars.NewLine)
            sql.Append("WFStepStates SET STEP_ID = @pStepId  , Name = @pName,Description = @pDesc  where Doc_State_Id = @pStateID; ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("Zsp_workflow_100_UpdWfInitialStepByWfId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_UpdWfInitialStepByWfId ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_UpdWfInitialStepByWfId ")
            End If
            sql.Append("@pIStepId numeric,@pWfid numeric AS UPDATE wfworkflow SET  initialstepId = @pIStepId where work_id = @pWfid")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdWfName", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdWfName  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdWfName  ")
            End If
            sql.Append("@pName varchar(50), @pWork_Id numeric as UPDATE wfworkflow SET name=@pName where work_id = @pWork_Id ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("Zsp_workflow_100_UpdWfNameByWfId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_UpdWfNameByWfId ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_UpdWfNameByWfId ")
            End If
            sql.Append("@pName varchar(50),@pWork_Id numeric AS UPDATE wfworkflow SET name = @pName where work_id = @pWork_Id ")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("Zsp_workflow_100_UpdWfRefreshRateByWfId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure Zsp_workflow_100_UpdWfRefreshRateByWfId  ")
            Else
                'Create
                sql.Append("CREATE Procedure Zsp_workflow_100_UpdWfRefreshRateByWfId  ")
            End If
            sql.Append("@pInterval numeric,@pWfid numeric AS UPDATE wfworkflow SET  refreshrate = @pInterval where work_id = @pWfid")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '-------------------------------------------------

            If ZPaq.IfExists("zsp_workflow_100_UpdWorkFlowByWfId", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure zsp_workflow_100_UpdWorkFlowByWfId  ")
            Else
                'Create
                sql.Append("CREATE Procedure zsp_workflow_100_UpdWorkFlowByWfId  ")
            End If
            sql.Append("@pWStat_Id decimal,@pName varchar(50),@pHelp varchar(200),@pDescription varchar(100),@pEditDate datetime,")
            sql.Append(ControlChars.NewLine)
            sql.Append("@pRefreshRate numeric,@pStepId numeric,@pWork_ID numeric AS ")
            sql.Append(ControlChars.NewLine)
            sql.Append("UPDATE wfworkflow SET wstat_id=@pWStat_Id,[name]=@pName,help=@pHelp,description = @pDescription,")
            sql.Append(ControlChars.NewLine)
            sql.Append("editdate = @pEditDate ,refreshrate = @pRefreshRate ,InitialStepId = @pStepId  where work_id = @pWork_ID")
            sql2.Append(sql.ToString)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            sw.WriteLine("")
            sw.WriteLine(sql2.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing


        Catch ex As Exception
            Return False
        End Try


        Return True

    End Function
#End Region
    
    'Private Sub GuardarString()
    '    sw.WriteLine("")
    '    sw.WriteLine(sql2.ToString)
    '    sw.WriteLine("")
    '    sw.Close()
    'End Sub
End Class


