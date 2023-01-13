using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public sealed class RequestActionService : WebService
{
    public RequestActionService()
    {
    }

    ///// <summary>
    ///// Executes a Rule to a Task
    ///// </summary>
    ///// <param name="ruleId"></param>
    ///// <param name="stepId"></param>
    ///// <param name="taskId"></param>
    //[WebMethod()]
    ////public void ExecuteRule(Int64 ruleId, Int64 stepId, Int64 taskId, Int64 userId)
    //{
    //    ExecuteRuleWithIndexs(ruleId, stepId, taskId,userId, null, null);
    //}

    [WebMethod()]
    public void ExecuteRules(Int64 ruleId, Int64 stepId, Int64[] tasksId, Int64 userId)
    {
        List<Int64> TaskIds = new List<Int64>(tasksId.Length);
        TaskIds.AddRange(tasksId);

        //TODO: Verificar los argumentos que se le deben pasar.
        //Se comento por error de argumentos, hay que revisarlo. Martin
        //WFRulesBussines.Execute(ruleId, stepId, TaskIds);

        TaskIds.Clear();
        TaskIds = null;
    }

    private void ExecuteRule(IWFRuleParent rule, List<ITaskResult> tasks)
    {
        tasks = rule.Play(tasks);

        if (null != rule.ChildRules && rule.ChildRules.Count > 0)
        {
            foreach (IWFRuleParent CurrentRule in rule.ChildRules)
                ExecuteRule(CurrentRule, tasks);
        }
    }


    ///// <summary>
    ///// Execute a Rule to a Task with the specified Indexs' values
    ///// </summary>
    ///// <param name="ruleId"></param>
    ///// <param name="taskId"></param>
    ///// <param name="stepId"></param>
    ///// <param name="indexValues"></param>

    //[WebMethod()]
    //public void ExecuteRuleWithIndexs(Int64 ruleId, Int64 stepId, Int64 taskId, Int64 userId, List<Int64> indexIds, List<String> IndexValues)
    //{
    //    using (ITaskResult CurrentTask = Tasks.GetTask(taskId))
    //    {
    //        #region Cargar Indices
    //        if (null != indexIds && null != IndexValues && indexIds.Count == IndexValues.Count)
    //        {
    //            IIndex CurrentIndex = null;

    //            foreach (object CurrentIndexItem in CurrentTask.Indexs)
    //            {
    //                if (CurrentIndexItem is IIndex)
    //                {
    //                    CurrentIndex = (IIndex)CurrentIndexItem;

    //                    if (indexIds.Contains(CurrentIndex.ID))
    //                        CurrentIndex.DataTemp = IndexValues[indexIds.IndexOf(CurrentIndex.ID)];
    //                }
    //            }

    //            if (null != CurrentIndex)
    //            {
    //                CurrentIndex.Dispose();
    //                CurrentIndex = null;
    //            }
    //        }
    //        #endregion

    //        using (IWFRuleParent CurrentRule = Rules.GetRule(ruleId, stepId))
    //            Rules.ExecuteRule(CurrentRule, CurrentTask);

    //    }
    //}
}
