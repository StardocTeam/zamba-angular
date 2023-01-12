using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Zamba.Core;
using Zamba.Core.DocTypes.DocAsociated;
using Zamba.Core.WF.WF;
using Zamba.Data;
using Zamba.Membership;
using Zamba.Servers;

namespace Zamba.Services
{
    public class STasks : IService
    {

        #region Constantes

        private const string DISKGROUPIDCOLUMNNAME = "DISK_GROUP_ID";
        private const string DOC_ID_COLUMNNAME = "Doc_ID";
        private const string DOCID_COLUMNNAME = "DocId";
        private const string DOC_FILE_COLUMNNAME = "DOC_FILE";
        private const string DOC_TYPE_ID_COLUMNNAME = "DOC_TYPE_ID";
        private const string DISK_VOL_ID_COLUMNNAME = "disk_Vol_id";
        private const string DISK_VOL_PATH_COLUMNNAME = "DISK_VOL_PATH";
        private const string DO_STATE_ID_COLUMNNAME = "Do_State_ID";
        private const string DOC_TYPE_NAME_COLUMNNAME = "doc_type_name";
        private const string IMAGEN_COLUMNNAME = "Imagen";
        private const string PLATTER_ID_COLUMNNAME = "PLATTER_ID";
        private const string VOL_ID_COLUMNNAME = "VOL_ID";
        private const string OFFSET_COLUMNNAME = "OFFSET";

        

        private const string ICON_ID_COLUMNNAME = "ICON_ID";
        private const string SHARED_COLUMNNAME = "SHARED";
        private const string VER_PARENT_ID_COLUMNNAME = "ver_Parent_id";
        private const string VERSION_COLUMNNAME = "version";
        private const string ROOTID_COLUMNNAME = "RootId";
        private const string ORIGINAL_FILENAME_COLUMNNAME = "original_Filename";
        private const string NUMEROVERSION_COLUMNNAME = "NumeroVersion";
        private const string NUMERO_DE_VERSION_COLUMNNAME = "numero de version";
        private const string CRDATE_COLUMNNAME = "crdate";
        private const string NAME1_COLUMNNAME = "NAME1";
        private const string STEP_ID_COLUMNNAME = "Step_Id";
        private const string ICONID_COLUMNNAME = "IconId";
        private const string CHECKIN_COLUMNNAME = "CheckIn";
        private const string TASK_ID_COLUMNNAME = "Task_ID";
        private const string WFSTEPID_COLUMNNAME = "WfStepId";
        private const string ASIGNADO_COLUMNNAME = "Asignado";
        private const string STATE_COLUMNNAME = "State";
        private const string ESTADO_TAREA_COLUMNNAME = "Estado";
        private const string SITUACION_COLUMNNAME = "Situacion";
        private const string EXPIREDATE_COLUMNNAME = "ExpireDate";
        private const string VENCIMIENTO_COLUMNNAME = "Vencimiento";
        private const string NAME_COLUMNNAME = "Name";
        private const string TASK_STATE_ID_COLUMNNAME = "task_state_id";
        private const string INGRESO_COLUMNNAME = "Ingreso";
        private const string USER_ASIGNED_COLUMNNAME = "User_Asigned";
        private const string USER_ASIGNED_BY_COLUMNNAME = "User_Asigned_By";
        private const string DATE_ASIGNED_BY_COLUMNNAME = "Date_asigned_By";
        private const string REMARK_COLUMNNAME = "Remark";
        private const string TAG_COLUMNNAME = "Tag";
        private const string DOCTYPEID_COLUMNNAME = "DoctypeId";
        private const string TASKCOLOR_COLUMNNAME = "TaskColor";
        private const string VER_COLUMNNAME = "Ver";
        private const string WORK_ID_COLUMNNAME = "Work_Id";
        private const string NOMBRE_DOCUMENTO_COLUMNNAME = "Nombre Documento";
        private const string SOLICITUD_COLUMNNAME = "Solicitud";

        #endregion

        #region Variables Privadas

        private string nombreDocumento_currUserConfig;
        private string imagen_currUserConfig;
        private string ver_currUserConfig;
        private string EstadoTarea_currUserConfig;
        private string Asignado_currUserConfig;
        private string Situacion_currUserConfig;
        private string Nombre_Original_currUserConfig;
        private string TipoDocumento_currUserConfig;

        private string version_UserConfig;
        private string NroVersion_UserConfig;

        private string FechaCreacion_currUserConfig;
        private string FechaModificacion_currUserConfig;

        #endregion

        private WFTaskBusiness WFTaskBusiness;
        private WFBusiness WFBusiness;
        private DocAsociatedBusiness DocAsociatedBusiness;
        private Newsfactory Newsfactory;
        private WFTasksFactory WFTasksFactory;
      
        public string NombreDocumento_currUserConfig

        {
            
            get { return nombreDocumento_currUserConfig = GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME; }
            set { nombreDocumento_currUserConfig = value; }
        }

        public STasks()
        {
            this.WFBusiness = new WFBusiness();
            this.DocAsociatedBusiness = new Zamba.Core.DocTypes.DocAsociated.DocAsociatedBusiness();
            this.Newsfactory = new Newsfactory();
            this.WFTaskBusiness = new WFTaskBusiness();
        }

        private void loadPreferences()
        {
            NombreDocumento_currUserConfig = GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME;
            imagen_currUserConfig = GridColumns.IMAGEN_COLUMNNAME;
            ver_currUserConfig = GridColumns.VER_COLUMNNAME;
            TipoDocumento_currUserConfig = GridColumns.DOC_TYPE_NAME_COLUMNNAME;
            EstadoTarea_currUserConfig = GridColumns.STATE_COLUMNNAME;
            Asignado_currUserConfig = GridColumns.ASIGNADO_COLUMNNAME;
            Situacion_currUserConfig = GridColumns.SITUACION_COLUMNNAME;
            Nombre_Original_currUserConfig = GridColumns.ORIGINAL_FILENAME_COLUMNNAME;
            version_UserConfig = GridColumns.VERSION_COLUMNNAME;
            NroVersion_UserConfig = GridColumns.NUMERO_DE_VERSION_COLUMNNAME;
            FechaCreacion_currUserConfig = GridColumns.CRDATE_COLUMNNAME;
            FechaModificacion_currUserConfig = GridColumns.LASTUPDATE_COLUMNNAME;
        }

        #region Get
        /// <summary>
        /// Returns a Task by its Id
        /// </summary>
        /// <param name="taskId"></param>

        public DataSet GetTasksToExpireGroupByStep(Int64 workflowid, Int32 FromHours, Int32 ToHours)
        {
            return WFTaskBusiness.GetTasksToExpireGroupByStep(workflowid, FromHours, ToHours);
        }

        public DataSet GetTasksToExpireGroupByUser(Int64 workflowid, Int32 FromHours, Int32 ToHours)
        {
            return WFTaskBusiness.GetTasksToExpireGroupByUser(workflowid, FromHours, ToHours);
        }

        public DataSet GetExpiredTasksGroupByStep(Int64 workflowid)
        {
            return WFTaskBusiness.GetExpiredTasksGroupByStep(workflowid);
        }

        public DataSet GetExpiredTasksGroupByUser(Int64 workflowid)
        {
            return WFTaskBusiness.GetExpiredTasksGroupByUser(workflowid);
        }

        public DataSet GetTasksBalanceGroupByWorkflow(Int64 workflowid)
        {
            return WFTaskBusiness.GetTasksBalanceGroupByWorkflow(workflowid);
        }

        public DataSet GetTasksBalanceGroupByStep(Int64 stepid)
        {
            return WFTaskBusiness.GetTasksBalanceGroupByStep(stepid);
        }

        public DataSet GetAsignedTasksCountsGroupByStep(Int64 workflowid)
        {
            return WFTaskBusiness.GetAsignedTasksCountsGroupByStep(workflowid);
        }

        public DataSet GetAsignedTasksCountsGroupByUser(Int64 workflowid)
        {
            return WFTaskBusiness.GetAsignedTasksCountsGroupByUser(workflowid);
        }

        public DataSet GetTaskConsumedMinutesByWorkflowGroupByUsers(Int64 workflowid)
        {
            return WFTaskBusiness.GetTaskConsumedMinutesByWorkflowGroupByUsers(workflowid);
        }

        public DataSet GetTaskConsumedMinutesByStepGroupByUsers(Int64 stepid)
        {
            return WFTaskBusiness.GetTaskConsumedMinutesByStepGroupByUsers(stepid);
        }

        public Hashtable GetTasksAverageTimeInSteps(Int64 workflowid)
        {
            return WFTaskBusiness.GetTasksAverageTimeInSteps(workflowid);
        }

        public Hashtable GetTasksAverageTimeByStep(Int64 stepid)
        {
            return WFTaskBusiness.GetTasksAverageTimeByStep(stepid);
        }

        public Int64 GetDocTypeId(Int64 taskId)
        {
            return WFTaskBusiness.GetDocTypeId(taskId);
        }

        public DataSet GetDocTypeIdByDocid(Int64 docId)
        {
            return WFTaskBusiness.GetDocTypesIdsByDocId(docId);
        }


        #endregion
        #region ABM
        /// <summary>
        /// Removes a Task from a Workflow. It can also delete that document.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="deleteDocument"></param>
        public void RemoveTask(ref ITaskResult result, Boolean deleteDocument, IUser currentuser)
        {
            WFTaskBusiness.Remove(ref result, deleteDocument, currentuser.ID, false);
        }

        /// <summary>
        /// Updates a Task
        /// </summary>
        /// <param name="result"></param>
        public void UpdateTask(ITaskResult result)
        {
            // TODO :
            //WFTaskBusiness.UpdateTask(result);
        }

        /// <summary>
        /// Actualiza el estado de las tareas del usuario en ejecucion a asignadas
        /// </summary>
        /// <param name="userID"></param>
        public void UpdateUserTaskStateToAsign(Int64 userID)
        {
            WFTaskBusiness.UpdateUserTaskStateToAsign(userID);
        }

        public void UpdateTaskState(Int64 taskId, Int64 taskStateId)
        {
            WFTaskBusiness.UpdateTaskState(taskId, taskStateId);
        }

        /// <summary>
        /// Inserts a new Task
        /// </summary>
        /// <param name="result"></param>
        /// <param name="workflow"></param>
        public void Insert(ITaskResult result, IWorkFlow workflow)
        {
            ArrayList results = new ArrayList(1);
            results.Add(result);
            WFTaskBusiness.AddResultsToWorkFLow(results, workflow, true, false, Zamba.Membership.MembershipHelper.CurrentUser.ID, false);
            results.Clear();
            results = null;
        }
        #endregion
        #region Remove
        /// <summary>
        /// Removes a Task from a Workflow
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="docTypeId"></param>
        /// <param name="fullPath"></param>
        /// <param name="deleteDocument"></param>
        public void RemoveTask(Int64 taskId, Int64 docTypeId, String fullPath, Boolean deleteDocument, Int64 CurrentUserId)
        {
            WFTaskBusiness.Remove(taskId, deleteDocument, docTypeId, fullPath, CurrentUserId);
        }

        /// <summary>
        /// Remove a Task from the Workflow
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="deleteDocument"></param>


        /// <summary>
        /// Remove a Task from the Workflow
        /// </summary>
        /// <param name="taskId"></param>

        /// <summary>
        /// Remove the Tasks from the Workflow
        /// </summary>
        /// <param name="taskIds"></param>
        /// <param name="deleteDocument"></param>


        /// <summary>
        /// Remove the Tasks from the Workflow
        /// </summary>
        /// <param name="taskIds"></param>

        #endregion
        #region Derive
        /// <summary>
        /// Derives a Task to a User.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="asignedTo">The User who is assigned the Task</param>
        /// <param name="asignedBy">The User who assigns the Task</param>


        public void DeriveTask(ITaskResult TaskResult, Int64 stepid, Int64 asignedToId, String asignedToName,
                                 Int64 asignedBy, DateTime asignedDate, Boolean selectCarp)

        {
            WFTaskBusiness.Derivar(TaskResult, stepid, asignedToId, asignedToName, asignedBy, asignedDate, selectCarp, Membership.MembershipHelper.CurrentUser.ID);
        }



        /// <summary>
        /// Derives a Task to a User.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stepId"></param>
        /// <param name="asignedToId"></param>
        /// <param name="asignedToName"></param>
        /// <param name="asignedBy"></param>
        /// <param name="asignedDate"></param>
        //public void DeriveTask(Int64 taskId, Int64 stepId, Int64 asignedToId, String asignedToName,
        //                              Int64 asignedBy, DateTime asignedDate, Boolean selectCarp, Int64 CurrentUserId)
        //{
        //    WFBusiness.DerivarTarea(taskId, stepId, asignedToId, asignedToName, asignedBy, asignedDate, selectCarp,  CurrentUserId);
        //}
        #endregion
        #region Asign
        /// <summary>
        /// Removes a Task from a Workflow
        /// </summary>
        /// <param name="result"></param>
        /// <param name="deleteDocument"></param>
        //public void RemoveTask(ref ITaskResult result, bool deleteDocument)
        //{
        //    Zamba.WFBusiness.WFTaskBusiness.Remove(ref result, deleteDocument);
        //}
        /// <summary>
        /// Assings a TaskResult to an User
        /// </summary>
        /// <param name="result"></param>
        /// <param name="asignedTo">The user who is assigned the TaskResult</param>
        /// <param name="AsignedBy">The user who assigns the TaskResult</param>
        public void AsignTask(ref ITaskResult result, Int64 asignedToId, Int64 AsignedById, Boolean LogAction)
        {
            WFTaskBusiness.Asign(ref result, asignedToId, AsignedById, LogAction);
        }

      

      
        #endregion
     

        #region Change State
        /// <summary>
        /// Changes a Task state
        /// </summary>
        /// <param name="result"></param>
        /// <param name="state"></param>
        /// /// <history>Marcelo Modified 31/08/2010 Se quita la llamada a la transaccion</history>
        public void ChangeTaskState(ref ITaskResult result, IWFStepState state)
        {
            WFTaskBusiness.ChangeState(ref result, state);
        }


        #endregion

        #region IService Members
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Tasks;
        }
        #endregion
        /// <summary>
        /// Initializes a TaskResult 
        /// </summary>
        /// <param name="result"></param>
        public void InitialiceTask(ref ITaskResult result)
        {
            WFTaskBusiness.Iniciar(ref result);
        }

        public void Finalizar(ITaskResult TaskResult)
        {
            WFTaskBusiness.Finalizar(ref TaskResult, Membership.MembershipHelper.CurrentUser.ID);
        }

        public DataSet GetTaskHistory(Int64 task_id)
        {
            return WFTaskBusiness.GetTaskHistory(task_id);
        }

        public DataSet GetOnlyIndexsHistory(Int64 docId)
        {
            return WFTaskBusiness.GetOnlyIndexsHistory(docId);
        }


      

      

        

        /// <summary>
        /// Obtiene la tarea en base al tipo de documento
        /// </summary>
        /// <param name="docid"></param>
        /// <param name="doctypeid"></param>
        /// <returns></returns>
        public ITaskResult GetTaskByDocIdAndDocTypeId(long docid, long doctypeid)
        {
            return WFTaskBusiness.GetTaskByDocIdAndDocTypeId(docid, doctypeid);
        }

        public ITaskResult GetTaskByDocId(Int64 DocId)
        {
            return WFTaskBusiness.GetTaskByDocId(DocId, Membership.MembershipHelper.CurrentUser.ID);
        }

        /// <summary>
        /// Devuelve un objeto ZCoreView con el id y nombre de la tarea
        /// </summary>
        /// <param name="docId">Id del documento a buscar</param>
        /// <returns>ZCoreView con el id y nombre de la tarea</returns>
        /// <remarks>Devuelve la primer tarea encontrada por ese DocId</remarks>
        public ZCoreView GetTaskIdAndNameByDocId(long docId)
        {
            return WFTaskBusiness.GetTaskIdAndNameByDocId(docId);
        }

        /// <summary>
        /// Devuelve un objeto ZCoreView con el id y nombre de la tarea
        /// </summary>
        /// <param name="docId">Id del documento a buscar</param>
        /// <param name="docTypeId">Tipo de documento</param>
        /// <returns>ZCoreView con el id y nombre de la tarea</returns>
        /// <remarks>Devuelve la primer tarea encontrada por ese DocId</remarks>
        public ZCoreView GetTaskViewByDocIdAndDocTypeId(long id, long docTypeId)
        {
            return WFTaskBusiness.GetTaskViewByDocIdAndDocTypeId(id, docTypeId);
        }

        public Int64 GetDocId(Int64 taskId)
        {
            return WFTaskBusiness.GetDocId(taskId);
        }

        public Int64 GetStepId(Int64 taskId)
        {
            return WFTaskBusiness.GetStepIDByTaskId(taskId);
        }

        public DataTable GetResultExtraData(Int64 taskId)
        {
            return WFTaskBusiness.GetResultExtraData(taskId);
        }

        public ITaskResult GetTaskByTaskIdAndDocTypeIdAndStepId(Int64 TaskId, Int64 DocTypeId, Int64 WFStepId, Int32 PageSize)
        {
            return WFTaskBusiness.GetTaskByTaskIdAndDocTypeIdAndStepId(TaskId, DocTypeId, WFStepId, PageSize);
        }

       

        public DataTable GetTasksByStepandDocTypeId(long stepId, long DocTypeId, Boolean WithGridRights, IFiltersComponent FC, Int64 LastDocId, Int32 PageSize, ref long totalCount)
        {
            return WFTaskBusiness.GetTasksByStepandDocTypeId(stepId, DocTypeId, WithGridRights, Membership.MembershipHelper.CurrentUser.ID, ref FC, LastDocId, PageSize, ref totalCount, false);
        }

        public DataSet GetTaskDs(Int64 taskId)
        {
            return WFTaskBusiness.GetTaskDs(taskId);
        }

        public List<Int64> GetTaskIDsByDocId(Int64 docId)
        {
            return WFTaskBusiness.GetTaskIDsByDocId(docId);
        }

        public DataSet getHistory(long DocId)
        {
            return EmailBusiness.getHistory(DocId);
        }

        //public DataTable getAsociatedDTResultsFromResult(IResult Result, Int64 LastDocId, Boolean blnOpen, IUser currentUser, Boolean GetTaskId)
        //{
        //    return DocAsociatedBusiness.getAsociatedDTResultsFromResult(Result, LastDocId, blnOpen, GetTaskId, currentUser.ID);
        //}


        public DataTable getAsociatedDTResultsFromResult(IResult Result, Int64 LastDocId, Boolean blnOpen, Int64 currentUser, Boolean GetTaskId)
        {
            return DocAsociatedBusiness.getAsociatedDTResultsFromResult(Result, LastDocId, blnOpen, GetTaskId, currentUser);
        }

        public DataTable getTasksAsDT(long StepId, ArrayList docTypeIds, int LastPage, int PageSize, IFiltersComponent FC, ref long totalCount)
        {
            loadPreferences();

            DataTable dtAux = new DataTable();
            DataTable table = new DataTable();

            if (LastPage > 0)
                LastPage = LastPage - 1;

            table = new DataTable();
            table.MinimumCapacity = 0;

            DataTable tabletemp;

            totalCount = 0;
            long partialCount = 0;

            foreach (long docTypeId in docTypeIds)
            {

                if (docTypeId > 0)
                {
                    try
                    {
                        tabletemp = GetTasksByStepandDocTypeId(StepId, docTypeId, true, FC, (long)LastPage, PageSize, ref partialCount);
                        totalCount += partialCount;
                        table.Merge(tabletemp);
                        table.MinimumCapacity = table.MinimumCapacity + tabletemp.MinimumCapacity;
                    }
                    catch (Exception ex)
                    {
                        Zamba.AppBlock.ZException.Log(ex);
                    }
                }
            }

            dtAux.MinimumCapacity = table.MinimumCapacity;

            lock (dtAux)
            {
                dtAux.Rows.Clear();
                dtAux.Columns.Clear();
                dtAux.AcceptChanges();

                if (table.Rows.Count > 0)
                {
                    table.Columns.Remove(DISKGROUPIDCOLUMNNAME);
                    table.Columns.Remove(PLATTER_ID_COLUMNNAME);
                    table.Columns.Remove(VOL_ID_COLUMNNAME);
                    table.Columns.Remove(DOC_FILE_COLUMNNAME);
                    table.Columns.Remove(OFFSET_COLUMNNAME);
                    table.Columns.Remove(ICON_ID_COLUMNNAME);
                    table.Columns.Remove(SHARED_COLUMNNAME);
                    table.Columns.Remove(VER_PARENT_ID_COLUMNNAME);
                    table.Columns.Remove(VERSION_COLUMNNAME);
                    table.Columns.Remove(ROOTID_COLUMNNAME);
                    table.Columns.Remove(NUMEROVERSION_COLUMNNAME);
                    table.Columns.Remove(DISK_VOL_ID_COLUMNNAME);
                    table.Columns.Remove(DISK_VOL_PATH_COLUMNNAME);
                    table.Columns.Remove(CRDATE_COLUMNNAME);

                    table.Columns.Remove(USER_ASIGNED_COLUMNNAME);
                    table.Columns.Remove(USER_ASIGNED_BY_COLUMNNAME);


                    table.Columns.Remove(DATE_ASIGNED_BY_COLUMNNAME);

                    table.Columns.Remove(TASK_STATE_ID_COLUMNNAME);
                    table.Columns.Remove(REMARK_COLUMNNAME);
                    table.Columns.Remove(TAG_COLUMNNAME);
                    table.Columns.Remove(WORK_ID_COLUMNNAME);

                    dtAux.Columns.Add(NombreDocumento_currUserConfig, typeof(string));
                    dtAux.Columns.Add(imagen_currUserConfig, typeof(object));
                    dtAux.Columns.Add(EstadoTarea_currUserConfig, typeof(string));
                    // dtAux.Columns.Add(Situacion_currUserConfig, typeof(string));
                    dtAux.Columns.Add(Asignado_currUserConfig, typeof(string));

                    if (table.Columns.Contains(EXPIREDATE_COLUMNNAME))
                        dtAux.Columns.Add(VENCIMIENTO_COLUMNNAME, table.Columns[EXPIREDATE_COLUMNNAME].DataType);
                    else
                        dtAux.Columns.Add(VENCIMIENTO_COLUMNNAME, table.Columns[VENCIMIENTO_COLUMNNAME].DataType);

                    if (table.Columns.Contains(ver_currUserConfig) == false)
                    {
                        table.Columns.Add(imagen_currUserConfig, typeof(object));

                        table.Columns[ASIGNADO_COLUMNNAME].ColumnName = Asignado_currUserConfig;
                        table.Columns[NAME_COLUMNNAME].ColumnName = NombreDocumento_currUserConfig;
                        table.Columns[DOC_ID_COLUMNNAME].ColumnName = "DocId";
                        table.Columns[STEP_ID_COLUMNNAME].ColumnName = "WfStepId";
                        table.Columns[DOC_TYPE_ID_COLUMNNAME].ColumnName = "DoctypeId";
                        table.Columns[EXPIREDATE_COLUMNNAME].ColumnName = "Vencimiento";
                        table.Columns[STATE_COLUMNNAME].ColumnName = EstadoTarea_currUserConfig;
                        table.Columns[CHECKIN_COLUMNNAME].ColumnName = "Ingreso";
                        table.Columns[SITUACION_COLUMNNAME].ColumnName = Situacion_currUserConfig;
                    }

                    if (table != null && dtAux != null)
                    {
                        try
                        {
                            if (table.Rows.Count > 0 && dtAux.Columns.Count > 0)
                            {
                                dtAux.AcceptChanges();
                                table.AcceptChanges();
                                dtAux.Merge(table);
                            }
                        }
                        catch (Exception ex)
                        {
                            Zamba.AppBlock.ZException.Log(ex);
                            dtAux.Rows.Clear();
                            dtAux.Merge(table);
                        }
                    }
                }
            }

            return dtAux;
        }

        public ITaskResult GetTask(Int64 TaskId, long userId)
        {
            return WFTaskBusiness.GetTask(TaskId, userId);
        }
    }
}