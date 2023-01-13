using System;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Core.Enumerators;
using Zamba.Core.WF.WF;

namespace Zamba.Services
{
    public class Rules : IService
    {
        #region Singleton
        private static Rules _rules = null;

        private Rules()
        {
        }

        public static IService GetInstance()
        {
            if (_rules == null)
                _rules = new Rules();

            return _rules;
        }
        #endregion

        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Rules;
        }

        #region ABM
        //public static IWFRuleParent GetRule(Int64 ruleId)
        //{
        //    return WFRulesBusiness.GetRule(ruleId);
        //}

        public static IWFRuleParent GetRule(Int64 ruleId, Int64 stepId)
        {
            using (IWFStep CurrentStep = Steps.GetStep(stepId))
                return WFRulesBusiness.GetInstanceRuleById(ruleId, CurrentStep.ID);
        }

        /// <summary>
        /// Adds a rule to the selected Workflow Node.
        /// </summary>
        /// <param name="ruleName"></param>
        /// <param name="node"></param>
        public static void Insert(String ruleName, IBaseWFNode node,String RuleNameFromUser , TypesofRules ruleType)
        {
            WFBusiness.AddRule(ruleName, node, RuleNameFromUser,ruleType);
        }

        /// <summary>
        /// Removes a Rule
        /// </summary>
        /// <param name="rule"></param>
        public static void Remove(IWFRuleParent rule)
        {
            WFRulesBusiness.DeleteRuleByID((Int32)rule.ID);
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
        public static void ExecuteRule(IWFRuleParent rule, ITaskResult task)
        {
            List<ITaskResult> Tasks = new List<ITaskResult>(1);
            Tasks.Add(task);

            rule.ExecuteRule(Tasks, new WFTaskBusiness(), bool.Parse(UserPreferences.getValue("TraceRuleTime", Sections.WorkFlow, "False")));

            Tasks.Clear();
        }

        /// <summary>
        /// Executes the selected rule
        /// </summary>
        /// <param name="rule"></param>
        public static void ExecuteRule(IWFRuleParent rule, List<ITaskResult> tasks)
        {
            rule.ExecuteRule(tasks, new WFTaskBusiness(), bool.Parse(UserPreferences.getValue("TraceRuleTime", Sections.WorkFlow, "False")));
        }

        /// <summary>
        /// Executes the selected rule
        /// </summary>
        /// <param name="rule"></param>
        public static void ExecuteRules(List<IWFRuleParent> rules, ITaskResult task)
        {
            List<ITaskResult> Tasks = new List<ITaskResult>(1);
            Tasks.Add(task);

            foreach (IWFRuleParent CurrentRule in rules)
                CurrentRule.ExecuteRule(Tasks, new WFTaskBusiness(), bool.Parse(UserPreferences.getValue("TraceRuleTime", Sections.WorkFlow, "False")));

            Tasks.Clear();
            Tasks = null;
        }

        /// <summary>
        /// Executes the selected rule
        /// </summary>
        /// <param name="rule"></param>
        public static void ExecuteRules(List<IWFRuleParent> rules, List<ITaskResult> tasks)
        {
            foreach (IWFRuleParent CurrentRule in rules)
                CurrentRule.ExecuteRule(tasks, new WFTaskBusiness(), bool.Parse(UserPreferences.getValue("TraceRuleTime", Sections.WorkFlow, "False")));
        }
    }
}