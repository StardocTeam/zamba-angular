using System;
using System.Collections;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Core.WF.WF;

/// <summary>
/// Service that handles the logic of Workflows , Steps , Rules  
/// </summary>
public class SWorkflow : IService
{

    private WFStepBusiness WFStepBusiness;
    private WFTaskBusiness WFTaskBusiness;
    private WFBusiness WFBusiness;
    private WFRulesBusiness WFRulesBusiness;

    public SWorkflow()
    {
        WFStepBusiness = new WFStepBusiness();
        WFBusiness = new WFBusiness();
        WFRulesBusiness = new WFRulesBusiness();
        WFTaskBusiness = new WFTaskBusiness();
    }
          
    #region IService Members
    ServicesTypes IService.ServiceType()
    {
        return ServicesTypes.Workflow;
    }

    #region Workflow
    #region Get
    /// <summary>
    /// Returns all avaible Workflows
    /// </summary>
    /// <returns></returns>
    public  List<IWorkFlow> GetWorkflows()
    {
        return WFBusiness.GetWorkflows();
    }

    /// <summary>
    /// Returns every Workflow for which a user has rights 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public  List<IWorkFlow> GetWorkflows(Int64 userId)
    {
        return null;
        //return WFBusiness.GetWorkflows((userId);
    }

    /// <summary>
    /// Returns a Workflow by its ID
    /// </summary>
    /// <param name="workflowId"></param>
    /// <returns></returns>
    public  IWorkFlow GetWorkflow(Int64 workflowId)
    {
        return null;
        //return WFBusiness.GetWorkflow(workflowId);
    }


    /// <summary>
    ///  Return Workflow names and id´s based on User's permissions
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>

    public System.Collections.Generic.List<Zamba.Core.EntityView> GetUserWFIdsAndNamesWithSteps(Int64 userid)
    {
       return WFBusiness.GetUserWFIdsAndNamesWithSteps(userid);
    }
     
    #endregion
    #region ABM
    /// <summary>
    /// Removes a Workflow
    /// </summary>
    /// <param name="workflowId"></param>
    public  void RemoveWorkflow(Int32 workflowId)
    {
        WFBusiness.RemoveWorkFlow(workflowId);
    }

    /// <summary>
    /// Saves selected Workflow 
    /// </summary>
    /// <param name="workflow"></param>
    public  void SaveWorkflow(IWorkFlow workflow)
    {
        ///TODO:
    }

    /// <summary>
    /// Saves selected Workflows
    /// </summary>
    /// <param name="workflowList"></param>
    public  void SaveWorkflows(List<IWorkFlow> workflowList)
    {
        foreach (IWorkFlow currentWorkflow in workflowList)
            SaveWorkflow(currentWorkflow);
    }
    #endregion
    #endregion
    
    #region Steps

    #region Get
    /// <summary>
    /// Returns all steps from a Workflow
    /// </summary>
    /// <param name="workflowId"></param>
    /// <returns></returns>
    public  List<IWFStep> GetSteps(Int64 workflowId)
    {
        var stepsList = new List<IWFStep>();
        // TODO: 
        return stepsList;
    }

    /// <summary>
    /// Return a Step by its ID
    /// </summary>
    /// <param name="stepId"></param>
    /// <returns></returns>
    public  IWFStep GetStep(Int64 stepId)
    {
        IWFStep currentStep = null;

        // TODO: 

        return currentStep;
    }
    #endregion

    /// <summary>
    /// Sets the initial step of a Workflow.
    /// </summary>
    /// <param name="step"></param>
    public void SetInitialStep(IEditStepNode step)
    {

        WFBusiness.SetInitialStep(step);
    }
    #endregion
    #region Rules
 
    /// <summary>
    /// Executes the selected rule
    /// </summary>
    /// <param name="rule"></param>
    public void ExecuteRule(IWFRuleParent rule)
    {
        //TODO:
    }

    /// <summary>
    /// Executes the selected rules
    /// </summary>
    /// <param name="rules"></param>
    public void ExecuteRules(List<IWFRuleParent> rules)
    {
        foreach (IWFRuleParent currentRule in rules)
            ExecuteRule(currentRule);
    }
    #endregion
    #region Tasks
    
    #region ABM
    /// <summary>
    /// Removes a Task from a Workflow. It can also delete that document.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="deleteDocument"></param>
    public void RemoveTask(ref ITaskResult result, Boolean deleteDocument, IUser user)
    {
        WFTaskBusiness.Remove(ref result, deleteDocument, user.ID,false);
    }

    /// <summary>
    /// Updates a Task
    /// </summary>
    /// <param name="result"></param>
    public  void UpdateTask(ITaskResult result)
    {
        //WFTaskBusiness.UpdateTask(result);
    }

    /// <summary>
    /// Inserts a new Task
    /// </summary>
    /// <param name="result"></param>
    /// <param name="workflow"></param>
    public void Insert(ITaskResult result, IWorkFlow workflow)
    {
        var results = new ArrayList(1) {result};

        WFTaskBusiness.AddResultsToWorkFLow(results, workflow, true, false, Zamba.Membership.MembershipHelper.CurrentUser.ID, false);

        results.Clear();
        result = null;
    }
    #endregion
    /// <summary>
    /// Assings a TaskResult to user
    /// </summary>
    /// <param name="result"></param>
    /// <param name="asignedTo">The user who is assigned the TaskResult</param>
    /// <param name="AsignedBy">The user who assigns the TaskResult</param>
    public  void AsignTask(ref ITaskResult result, Int64 asignedToId, Int64 asignedById, IUser currenuser)
    {
        WFTaskBusiness.Asign(ref result, asignedToId, asignedById,  true);
    }

    /// <summary>
    /// Changes a Task state
    /// </summary>
    /// <param name="result"></param>
    /// <param name="state"></param>
    /// <history>Marcelo Modified 31/08/2010 Se quita la llamada a la transaccion</history>
    public  void ChangeTaskState(ref ITaskResult result, IWFStepState state)
    {
        WFTaskBusiness.ChangeState(ref result, state);
    }

   

    /// <summary>
    /// Initializes a TaskResult 
    /// </summary>
    /// <param name="result"></param>
    public  void InitialiceTask(ref ITaskResult result)
    {
        WFTaskBusiness.Iniciar(ref result);
    }

    /// <summary>
    /// Unassigns a Task
    /// </summary>
    /// <param name="result"></param>
    /// <param name="asignedBy"></param>
    public  void UnAssignTask(ref ITaskResult result, IUser asignedBy)
    {
        WFTaskBusiness.UnAssign(ref result, asignedBy);
    }
    #endregion
    #endregion
}