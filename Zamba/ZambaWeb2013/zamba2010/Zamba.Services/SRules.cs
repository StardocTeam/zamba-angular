using System;
using System.Collections.Generic;
using System.Data;
using Zamba.Core;
using Zamba.Core.Enumerators;
using Zamba.Core.WF.WF;

namespace Zamba.Services
{
    public class SRules : IService
    {

        private WFBusiness WFBusiness;
        private WFRulesBusiness WFRulesBusiness;
        private SSteps Steps;

        public SRules()
        {
            this.WFBusiness = new WFBusiness();
            this.WFRulesBusiness = new WFRulesBusiness();
            this.Steps = new SSteps();
        }
        
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Rules;
        }


       

        /// <summary>
        /// Executes the selected rule
        /// </summary>
        /// <param name="rule"></param>
        public void ExecuteRule(IWFRuleParent rule, List<ITaskResult> tasks,Boolean IsAsync)
        {
            rule.ExecuteRule(tasks,  new WFTaskBusiness(), new WFRulesBusiness(), IsAsync);
        }

      

     

        public List<ITaskResult> ExecuteWebRule(Int64 ruleID, List<ITaskResult> results, ref RulePendingEvents RulePendingEvent, ref RuleExecutionResult ExecutionResult, ref List<Int64> ExecutedIDs, ref System.Collections.Hashtable Params, ref List<Int64> PendingChildRules, ref  Boolean RefreshRule, ref List<long> TaskIdsToRefresh, Boolean IsAsync)
        {
            return WFRulesBusiness.ExecuteWebRule(ruleID, results, ref RulePendingEvent, ref ExecutionResult, ref ExecutedIDs, ref Params,  ref PendingChildRules, ref   RefreshRule, TaskIdsToRefresh,  IsAsync);
        }

        public List<IWFRuleParent> GetCompleteHashTableRulesByStep(Int64 stepid)
        {
            return WFRulesBusiness.GetCompleteHashTableRulesByStep(stepid);
        }

        public string GetUserActionName(IRule rule)
        {
            return WFBusiness.GetUserActionName(rule);
        }

    }
}
