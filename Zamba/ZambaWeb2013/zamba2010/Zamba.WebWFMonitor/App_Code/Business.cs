using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Zamba;
using Zamba.Core;
using Zamba.Data;
using Zamba.Servers;
using Zamba.Users.Factory;
using Zamba.WFBusiness;

public class Business
{
    
    const string TASK_QUERY = "SELECT WFDocument.Doc_Id as Id, WFDocument.Name , WFDocument.ExpireDate, ISNULL(UsrTable.Name,'Ninguno') as UserName,WFDocument.Task_State_Id as TaskStateId , WfStep.Name as WfStepName, ISNULL(WfStepStates.Name , 'Sin Estado')As State FROM WFDocument LEFT JOIN UsrTable ON WfDocument.User_Asigned = UsrTable.id LEFT JOIN WfStep ON WfDocument.step_id = WfStep.step_id LEFT JOIN WfStepStates ON WfDocument.Do_State_Id = WfStepStates.doc_state_id WHERE ";
    /// <summary>
    /// Devuelve un Dataset con todas las Tareas de un Workflow
    /// </summary>
    /// <param name="wfId"></param>
    /// <returns></returns>
    private DataSet GetTasks(int wfId)
    {
        StringBuilder StrQuery = new StringBuilder();
        StrQuery.Append(TASK_QUERY);
        StrQuery.Append("WfDocument.work_id = ");
        StrQuery.Append(wfId.ToString());

        IConnection con = Server.get_Con(true, true, true);

        DataSet ds = con.ExecuteDataset(CommandType.Text, StrQuery.ToString());
        ds = ChangeValues(ds);
        return ds;
    }
    /// <summary>
    /// Devuelve un Dataset con todas las tareas de una Etapa 
    /// </summary>
    /// <param name="wfId"></param>
    /// <param name="stepId"></param>
    /// <returns></returns>
    private DataSet GetTasks(int wfId, int stepId)
    {
        StringBuilder StrQuery = new StringBuilder();
        StrQuery.Append(TASK_QUERY);
        StrQuery.Append("WfDocument.work_id = ");
        StrQuery.Append(wfId.ToString());
        StrQuery.Append(" and wfdocument.step_id = ");
        StrQuery.Append(stepId.ToString());
        IConnection con = Server.get_Con(true, true, true);

        DataSet ds = con.ExecuteDataset(CommandType.Text, StrQuery.ToString());
        ds = ChangeValues(ds);
        return ds;
    }
    /// <summary>
    /// Le agrega la columna de estado de tareas a un dataset de tareas
    /// </summary>
    /// <param name="ds"></param>
    /// <returns></returns>
    private DataSet ChangeValues(DataSet dsTareas)
    {
        dsTareas.Tables[0].Columns.Add("TaskState", typeof(string));

        foreach (DataRow row in dsTareas.Tables[0].Rows)
        {
            int taskStateId = System.Convert.ToInt32(row["TaskStateId"]);
            row["TaskState"] = ((Zamba.Core.TaskResult.TaskStates)(taskStateId));
        }

        return dsTareas;
    }
    
    #region Compare
    private static int CompareByName(TaskResult.ViewTaskResult x, TaskResult.ViewTaskResult y)
    {
        if (x == null)
        {
            if (y == null)
                return 0;
            else
                return -1;
        }
        else
        {
            if (y == null)
                return 1;
            else
            {
                int retval = x.Name.CompareTo(y.Name);

                if (retval != 0)
                    return retval;
                else
                    return x.Name.CompareTo(y.Name);
            }
        }
    }
    private static int CompareByExpiration(TaskResult.ViewTaskResult x, TaskResult.ViewTaskResult y)
    {
        if (x == null)
        {
            if (y == null)
                return 0;
            else
                return -1;
        }
        else
        {
            if (y == null)
                return 1;
            else
            {
                int retval = x.ExpireDate.CompareTo(y.ExpireDate);

                if (retval != 0)
                    return retval;
                else
                    return x.ExpireDate.CompareTo(y.ExpireDate);
            }
        }
    }
    #endregion
    #region Users

    /// <summary>
    /// Devuelve todos los usuarios en un DataView
    /// </summary>
    /// <returns></returns>
    public DataView GetUsers()
    {
        return UserFactory.GetUsers().Tables[0].DefaultView;
    }

    /// <summary>
    /// Devuelve todos los usuarios en un ArrayList
    /// </summary>
    /// <returns></returns>
    public ArrayList GetAllUsers()
    {
        return UserFactory.GetUsersArrayListOrderByName();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stepId"></param>
    /// <param name="wfId"></param>
    /// <returns></returns>
    public List<User.UserView> LoadUsers(Int32 stepId, Int32 wfId)
    {
        if ((stepId == 0) || (wfId == 0))
            return new List<User.UserView>();

        WorkFlow MyWorkflow = null;

        DsWF DsAllWorkflows = WFBusiness.GetAllWorkflows();

        foreach (WorkFlow CurrentWorkflow in WFFactory.GetWFs())
        {
            if (CurrentWorkflow.ID == wfId)
            {
                MyWorkflow = CurrentWorkflow;
                break;
            }
        }

        WFStep CurrentStep = WFStepsFactory.GetStep(MyWorkflow, stepId);
        WFBusiness.FillUsersAndGroups( CurrentStep);

        List<User.UserView> UserList = new List<User.UserView>();

        User.UserView g;
        foreach (User u in CurrentStep.Users.Values)
        {
            g = new User.UserView(u);
            UserList.Add(g);
        }
        return UserList;
    }
    #endregion
    #region Tasks
    public void Asignar(List<TaskResult> tasks, User asignedTo)
    {
        foreach (TaskResult CurrentTaskResult in tasks)
        {
            WFStep Step = CurrentTaskResult.WfStep;
            WFBusiness.FillUsersAndGroups(Step);
            CurrentTaskResult.WfStep = Step;
            WFBusiness.AsignTask(CurrentTaskResult, asignedTo, asignedTo);//TODO:CAMBIAR ULTIMO PARAMETRO
        }
    }
    /// <summary>
    /// Quita las tareas seleccionadas del Workflow
    /// </summary>
    /// <param name="tasks"></param>
    /// <param name="deleteDocument">Borrar el documento ademas de quitarlo</param>
    public void Quitar(List<TaskResult> tasks, Boolean deleteDocument)
    {
        foreach (TaskResult Task in tasks)
            WFBusiness.RemoveTask(Task, deleteDocument);
    }
    /// <summary>
    /// Renueva las tareas seleccionadas
    /// </summary>
    /// <param name="tasks"></param>
    /// <param name="fecha"></param>
    public void Renovar(List<TaskResult> tasks, DateTime fecha)
    {
        foreach (TaskResult Task in tasks)
            WFBusiness.ChangeExpireDateTask(Task, fecha);
    }
    /// <summary>
    /// Desasigna las tareas seleccionadas
    /// </summary>
    /// <param name="tasks"></param>
    /// <param name="asignedTo"></param>
    public void Desasignar(List<TaskResult> tasks, User asignedTo)
    {
        foreach (TaskResult Task in tasks)
            WFBusiness.UnAssignTask(Task, asignedTo);
    }

    /// <summary>
    /// Cambia el estado de las tareas seleccionadas
    /// </summary>
    /// <param name="tasks"></param>
    /// <param name="workflowId"></param>
    /// <param name="stepId"></param>
    /// <param name="stepStateId">Id del estado a cambiar</param>
    public void CambiarEstado(List<TaskResult> tasks, Int32 workflowId, Int32 stepId, Int32 stepStateId)
    {
        foreach (WFStep.State StepState in WFStepBussines.GetStepStates(stepId, workflowId).Values)
        {
            if (StepState.Id == stepStateId)
            {
                foreach (TaskResult taskResult in tasks)
                    WFBusiness.ChangeStateTask(taskResult, StepState);
                break;
            }
        }
    }
    #endregion
    /// <summary>
    /// Devuelve las tareas de acuerdo a un WorkflowId y Etapa. 
    /// </summary>
    /// <param name="wfId"></param>
    /// <param name="stepId"></param>
    /// <returns></returns>
    public DataSet GetTasks(string wfId, string stepId)
    {
        try
        {
            if (String.IsNullOrEmpty(wfId))
            {
                return null;
            }
            else
            {

                if (String.IsNullOrEmpty(stepId))
                    return GetTasks(System.Convert.ToInt32(wfId));
                else
                    return GetTasks(System.Convert.ToInt32(wfId), System.Convert.ToInt32(stepId));
            }
        }
        catch (Exception ex)
        {
            ZCore.RaiseError(ex);
            return null;
        }
    }

    /// <summary>
    /// Devuelve los estados de acuerdo a un workflowId y stepId
    /// </summary>
    /// <param name="wfId"></param>
    /// <param name="stepId"></param>
    /// <returns></returns>
    public DataView GetStates(string wfId, string stepId)
    {

        if (String.IsNullOrEmpty(wfId) || String.IsNullOrEmpty(stepId))
            return null;
        else
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            SortedList StepStates = WFStepBussines.GetStepStates(Convert.ToInt32(stepId), Convert.ToInt32(wfId));

            if (null == StepStates)
                return null;
            else
            {
                foreach (object item in StepStates.Values)
                {
                    WFStep.State State = ((WFStep.State)(item));
                    DataRow Row = dt.NewRow();
                    Row["Id"] = System.Convert.ToInt32(State.Id);
                    Row["Name"] = State.Name;
                    dt.Rows.Add(Row);
                }
                return dt.DefaultView;
            }
        }
    }

    public DataView GetSteps(string wfId)
    {
        if (!String.IsNullOrEmpty(wfId))
            return WFStepBussines.GetDsSteps(int.Parse(wfId)).Tables[0].DefaultView;
        else
            return null;
    }
}




