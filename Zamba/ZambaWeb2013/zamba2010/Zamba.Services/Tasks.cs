using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Zamba.Core;
using Zamba.Core.WF.WF;

namespace Zamba.Services
{
    public class Tasks : IService
    {
        #region Singleton
        private static Tasks _task = null;

        private Tasks()
        {
        }

        public static IService GetInstance()
        {
            if (_task == null)
                _task = new Tasks();

            return _task;
        }
        #endregion
        #region Get
        /// <summary>
        /// Returns a Task by its Id
        /// </summary>
        /// <param name="taskId"></param>
   

     

       

       

        public static DataSet GetTasksToExpireGroupByStep(Int64 workflowid, Int32 FromHours, Int32 ToHours)
        {
            return WFTaskBusiness.GetTasksToExpireGroupByStep(workflowid, FromHours, ToHours);
        }

        public static DataSet GetTasksToExpireGroupByUser(Int64 workflowid, Int32 FromHours, Int32 ToHours)
        {
            return WFTaskBusiness.GetTasksToExpireGroupByUser(workflowid, FromHours, ToHours);
        }

        public static DataSet GetExpiredTasksGroupByStep(Int64 workflowid)
        {
            return WFTaskBusiness.GetExpiredTasksGroupByStep(workflowid);
            //return null;
        }

        public static DataSet GetExpiredTasksGroupByUser(Int64 workflowid)
        {
            return WFTaskBusiness.GetExpiredTasksGroupByUser(workflowid);
            //return null;
        }

        public static DataSet GetTasksBalanceGroupByWorkflow(Int64 workflowid)
        {
            return WFTaskBusiness.GetTasksBalanceGroupByWorkflow(workflowid);
        }

        public static DataSet GetTasksBalanceGroupByStep(Int64 stepid)
        {
            return WFTaskBusiness.GetTasksBalanceGroupByStep(stepid);
        }

        public static DataSet GetAsignedTasksCountsGroupByStep(Int64 workflowid)
        {
            return WFTaskBusiness.GetAsignedTasksCountsGroupByStep(workflowid);
        }
        public static DataSet GetAsignedTasksCountsGroupByUser(Int64 workflowid)
        {
            return WFTaskBusiness.GetAsignedTasksCountsGroupByUser(workflowid);
        }

        public static DataSet GetTaskConsumedMinutesByWorkflowGroupByUsers(Int64 workflowid)
        {
            return WFTaskBusiness.GetTaskConsumedMinutesByWorkflowGroupByUsers(workflowid);
        }

        public static DataSet GetTaskConsumedMinutesByStepGroupByUsers(Int64 stepid)
        {
            return WFTaskBusiness.GetTaskConsumedMinutesByStepGroupByUsers(stepid);
        }


        public static Hashtable GetTasksAverageTimeInSteps(Int64 workflowid)
        {
            return WFTaskBusiness.GetTasksAverageTimeInSteps(workflowid);
        }
        
        public static Hashtable GetTasksAverageTimeByStep(Int64 stepid)
        {
            return WFTaskBusiness.GetTasksAverageTimeByStep(stepid);
        }
        #endregion
        #region ABM
        /// <summary>
        /// Removes a Task from a Workflow. It can also delete that document.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="deleteDocument"></param>
        public static void RemoveTask(ref ITaskResult result, Boolean deleteDocument, Int64 CurrentUserId)
        {
            WFBusiness.RemoveTask(ref result, deleteDocument, CurrentUserId, false);
        }

        /// <summary>
        /// Updates a Task
        /// </summary>
        /// <param name="result"></param>
        public static void UpdateTask(ITaskResult result)
        {
            // TODO :
            //WFTaskBusiness.UpdateTask(result);
        }

        /// <summary>
        /// Inserts a new Task
        /// </summary>
        /// <param name="result"></param>
        /// <param name="workflow"></param>
        public static void Insert(ITaskResult result, IWorkFlow workflow)
        {
            ArrayList results = new ArrayList(1);
            results.Add(result);
            WFTaskBusiness.AddResultsToWorkFLow(results, workflow, true, true);
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
        public static void RemoveTask(Int64 taskId, Int64 docTypeId, String fullPath, Boolean deleteDocument, Int64 CurrentUserId)
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
      

        /// <summary>
        /// Derives a Task to a User.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stepId"></param>
        /// <param name="asignedToId"></param>
        /// <param name="asignedToName"></param>
        /// <param name="asignedBy"></param>
        /// <param name="asignedDate"></param>
        //public static void DeriveTask(Int64 taskId, Int64 stepId, Int64 asignedToId, String asignedToName,
        //                              Int64 asignedBy, DateTime asignedDate, Boolean selectCarp, Int64 CurrentUserId)
        //{
        //    WFBusiness.DerivarTarea(taskId, stepId, asignedToId, asignedToName, asignedBy, asignedDate, selectCarp,  CurrentUserId);
        //}
        //#endregion
        #region Asign
        /// <summary>
        /// Removes a Task from a Workflow
        /// </summary>
        /// <param name="result"></param>
        /// <param name="deleteDocument"></param>
        //public static void RemoveTask(ref ITaskResult result, bool deleteDocument)
        //{
        //    Zamba.WFBusiness.WFTaskBusiness.Remove(ref result, deleteDocument);
        //}
        /// <summary>
        /// Assings a TaskResult to an User
        /// </summary>
        /// <param name="result"></param>
        /// <param name="asignedTo">The user who is assigned the TaskResult</param>
        /// <param name="AsignedBy">The user who assigns the TaskResult</param>
        public static void AsignTask(ref ITaskResult result, Int64 asignedToId, Int64 AsignedById)
        {
            WFTaskBusiness.Asign(ref result, asignedToId, AsignedById,true);
        }

        /// <summary>
        /// Assings a TaskResult to an User
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskName"></param>
        /// <param name="docTypeId"></param>
        /// <param name="docTypeName"></param>
        /// <param name="folderId"></param>
        /// <param name="stepId"></param>
        /// <param name="stepName"></param>
        /// <param name="stateId"></param>
        /// <param name="userName"></param>
        /// <param name="workflowId"></param>
        /// <param name="workflowName"></param>
        /// <param name="asignedToUserId"></param>
        /// <param name="asignedByUserId"></param>
        /// <param name="asignedDate"></param>
        public static void AsignTask(Int64 taskID, String taskName, Int64 docTypeId, String docTypeName, Int32 folderId,
                                     Int32 stepId, Int32 stateId, String userName, Int32 workflowId,
                                     long asignedToUserId, long asignedByUserId, DateTime asignedDate)
        {
            WFTaskBusiness.Asign(taskID, taskName, docTypeId, docTypeName, folderId, stepId, 
                                            stateId, userName, workflowId,  asignedToUserId,
                                            asignedByUserId, asignedDate);
        }

        /// <summary>
        /// Assings TaskResults to an User
        /// </summary>
        /// <param name="taskIds"></param>
        /// <param name="asignedTo"></param>
        /// <param name="asignedBy"></param>
        public static void AsignTasks(List<Int64> taskIds, Int64 asignedTo, Int64 asignedBy)
        {
            foreach (Int64 CurrentTaskId in taskIds)
                WFTaskBusiness.Asign(CurrentTaskId, asignedTo, asignedBy,DateTime.Now);
        }
        #endregion
        #region UnAsign
        /// <summary>
        /// Unassigns a Task
        /// </summary>
        /// <param name="result"></param>
        /// <param name="asignedBy"></param>
        public static void UnAssignTask(ref ITaskResult result, IUser asignedBy)
        {
            WFTaskBusiness.UnAssign(ref result, asignedBy);
        }

        /// <summary>
        /// Unassigns a Task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="asignedToUserId"></param>
        /// <param name="asignedToUserName"></param>
        /// <param name="asignedByUserId"></param>
        /// <param name="asignedDate"></param>
        public static void UnAssignTask(Int64 taskId, Int64 asignedToUserId, String asignedToUserName,
                                        Int64 asignedByUserId, DateTime asignedDate)
        {
            WFTaskBusiness.UnAssign(taskId, asignedToUserId, asignedToUserName, asignedByUserId, asignedDate);
        }
        #endregion
        #endregion
        #region Change State
        /// <summary>
        /// Changes a Task state
        /// </summary>
        /// <param name="result"></param>
        /// <param name="state"></param>
        /// /// <history>Marcelo Modified 31/08/2010 Se quita la llamada a la transaccion</history>
        public static void ChangeTaskState(ref ITaskResult result, IWFStepState state)
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
        public static void InitialiceTask(ref ITaskResult result)
        {
            WFTaskBusiness.Iniciar(ref result);
        }

        public static DataSet GetTaskHistory(Int64 taskId)
        {
            return WFTaskBusiness.GetTaskHistory(taskId);
        }
    }
}