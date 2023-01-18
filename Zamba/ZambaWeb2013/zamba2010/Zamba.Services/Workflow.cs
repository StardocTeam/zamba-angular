using System;
using System.Collections;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Core.WF.WF;

/// <summary>
/// Service that handles the logic of Workflows , Steps , Rules  
/// </summary>
public class Workflow : IService
{
    #region Singleton
    private static Workflow _workflow = null;

    private Workflow()
    {
    }

    public static IService GetInstance()
    {
        if (_workflow == null)
            _workflow = new Workflow();

        return _workflow;
    }
    #endregion
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
    public static List<IWorkFlow> GetWorkflows()
    {
        return WFBusiness.GetWorkflows();
    }

    /// <summary>
    /// Returns every Workflow for which a user has rights 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static List<IWorkFlow> GetWorkflows(Int64 userId)
    {
        return null;
        //return WFBusiness.GetWorkflows((userId);
    }

    /// <summary>
    /// Returns a Workflow by its ID
    /// </summary>
    /// <param name="workflowId"></param>
    /// <returns></returns>
    public static IWorkFlow GetWorkflow(Int64 workflowId)
    {
        return null;
        //return WFBusiness.GetWorkflow(workflowId);
    }
    #endregion
    #region ABM
    /// <summary>
    /// Removes a Workflow
    /// </summary>
    /// <param name="workflowId"></param>
    public static void RemoveWorkflow(Int32 workflowId)
    {
        WFBusiness.RemoveWorkFlow(workflowId);
    }

    /// <summary>
    /// Saves selected Workflow 
    /// </summary>
    /// <param name="workflow"></param>
    public static void SaveWorkflow(IWorkFlow workflow)
    {
        ///TODO:
    }

    /// <summary>
    /// Saves selected Workflows
    /// </summary>
    /// <param name="workflowList"></param>
    public static void SaveWorkflows(List<IWorkFlow> workflowList)
    {
        foreach (IWorkFlow CurrentWorkflow in workflowList)
            SaveWorkflow(CurrentWorkflow);
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
    public static List<IWFStep> GetSteps(Int64 workflowId)
    {
        List<IWFStep> StepsList = new List<IWFStep>();
        // TODO: 
        return StepsList;
    }

    /// <summary>
    /// Return a Step by its ID
    /// </summary>
    /// <param name="stepId"></param>
    /// <returns></returns>
    public static IWFStep GetStep(Int64 stepId)
    {
        IWFStep CurrentStep = null;

        // TODO: 

        return CurrentStep;
    }
    #endregion
    #region ABM
    /// <summary>
    /// Removes a Step
    /// </summary>
    /// <param name="step"></param>
    public static void RemoveStep(ref IWFStep step)
    {
        WFStepBusiness.DelStep(ref step);
    }

    /// <summary>
    /// Removes a Step
    /// </summary>
    /// <param name="stepId"></param>
    public static void RemoveStep(Int64 stepId)
    {
        IWFStep CurrentStep = GetStep(stepId);
        RemoveStep(ref CurrentStep);
        CurrentStep.Dispose();
    }

    /// <summary>
    /// Updates a Step
    /// </summary>
    /// <param name="step"></param>
    public static void UpdateStep(ref IWFStep step)
    {
        WFStepBusiness.UpdateStep(ref step);
    }

    /// <summary>
    /// Inserts a Step to a Workflow
    /// </summary>
    /// <param name="step"></param>
    /// <param name="workflow"></param>
    public static void InsertStep(IWFStep step, IWorkFlow workflow)
    {
        WFStepBusiness.AddStep(step, workflow);
    }
    #endregion
    /// <summary>
    /// Sets the initial step of a Workflow.
    /// </summary>
    /// <param name="step"></param>
    public static void SetInitialStep(IEditStepNode step)
    {
        WFBusiness.SetInitialStep(step);
    }
    #endregion
    #region Rules
    #region ABM
    /// <summary>
    /// Adds a rule to the selected Workflow Node.
    /// </summary>
    /// <param name="ruleName"></param>
    /// <param name="node"></param>
    public static void Insert(String ruleName, IBaseWFNode node)
    {
        //WFBusiness.AddRule(ruleName, node);
    }

    /// <summary>
    /// Removes a Rule
    /// </summary>
    /// <param name="rule"></param>
    public static void Remove(IWFRuleParent rule)
    {
        WFRulesBusiness.DeleteRuleByID((Int32) rule.ID);
    }

    /// <summary>
    /// Updates a Rule name
    /// </summary>
    /// <param name="ruleNode"></param>
    public static void UpdateRuleName(IRuleNode ruleNode)
    {
        WFBusiness.ChangeNameRule(ruleNode);
    }
    #endregion
    /// <summary>
    /// Executes the selected rule
    /// </summary>
    /// <param name="rule"></param>
    public static void ExecuteRule(IWFRuleParent rule)
    {
        //TODO:
    }

    /// <summary>
    /// Executes the selected rules
    /// </summary>
    /// <param name="rules"></param>
    public static void ExecuteRules(List<IWFRuleParent> rules)
    {
        foreach (IWFRuleParent CurrentRule in rules)
            ExecuteRule(CurrentRule);
    }
    #endregion
    #region Tasks
    
    #region ABM
    /// <summary>
    /// Removes a Task from a Workflow. It can also delete that document.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="deleteDocument"></param>
    public static void RemoveTask(ref ITaskResult result, Boolean deleteDocument,Int64 currentUserId )
    {
        WFBusiness.RemoveTask(ref result, deleteDocument,currentUserId, false);
    }

    /// <summary>
    /// Updates a Task
    /// </summary>
    /// <param name="result"></param>
    public static void UpdateTask(ITaskResult result)
    {
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
        result = null;
    }
    #endregion
    /// <summary>
    /// Assings a TaskResult to user
    /// </summary>
    /// <param name="result"></param>
    /// <param name="asignedTo">The user who is assigned the TaskResult</param>
    /// <param name="AsignedBy">The user who assigns the TaskResult</param>
    public static void AsignTask(ref ITaskResult result, Int64 asignedToId, Int64 asignedById)
    {
        WFTaskBusiness.Asign(ref result, asignedToId, asignedById,true);
    }

    /// <summary>
    /// Changes a Task state
    /// </summary>
    /// <param name="result"></param>
    /// <param name="state"></param>
    /// <history>Marcelo Modified 31/08/2010 Se quita la llamada a la transaccion</history>
    public static void ChangeTaskState(ref ITaskResult result, IWFStepState state)
    {
        WFTaskBusiness.ChangeState(ref result, state);
    }

    /// <summary>
    /// Derives a Task to a User.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="asignedTo">The User who is assigned the Task</param>
    /// <param name="asignedBy">The User who assigns the Task</param>
    public static void DeriveTask(ITaskResult result, IUser asignedTo, IUser asignedBy)
    {
        //WFBusiness.DerivarTarea(ref result, asignedTo, asignedBy);
    }

    /// <summary>
    /// Initializes a TaskResult 
    /// </summary>
    /// <param name="result"></param>
    public static void InitialiceTask(ref ITaskResult result)
    {
        WFTaskBusiness.Iniciar(ref result);
    }

    /// <summary>
    /// Unassigns a Task
    /// </summary>
    /// <param name="result"></param>
    /// <param name="asignedBy"></param>
    public static void UnAssignTask(ref ITaskResult result, IUser asignedBy)
    {
        WFTaskBusiness.UnAssign(ref result, asignedBy);
    }
    #endregion
    #endregion
}