using System;
using System.ComponentModel;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using System.Reflection;
//using Zamba.WFUserControl;
using Zamba.Core;
using Zamba.Core.Enumerators;

namespace Zamba.WFActivity.Xoml
{
    /// <summary>
    /// Defines the ParallelIfBranch activity
    /// </summary>
    /// <remarks>
    /// This activity is added as a child of the ParallelIf activity. You need to set the condition
    /// property which will be evaluated at runtime, and if this returns true then the subordinate
    /// activities will be executed.
    /// </remarks>
    //[ToolboxItem(false)]
    //[Designer(typeof(MCIfBranchDesigner))]
    //[ActivityValidator(typeof(MCIfBranchValidator))]
    public class TypeofRule : Activity, IResultActivity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TypeofRule()
	    {
	    }

        //[Marcelo] 18/01/10 Created
        private Int64 _ruleId;

        /// <summary>
        /// Regla de WF de Zamba
        /// </summary>
        public Int64 ruleId
        {
            get
            {
                return _ruleId;
            }
            set
            {
                _ruleId = value;
            }
        }

        private string _ruleClass;
        public string RuleClass
        {
            get
            {
                return _ruleClass;
            }
            set
            {
                _ruleClass = value;
            }
        }

        #region Miembros de IResultActivity

        System.Collections.Generic.List<Zamba.Core.ITaskResult> results;
        public System.Collections.Generic.List<Zamba.Core.ITaskResult> Results
        {
            get
            {
                return results;
            }
            set
            {
                results = value;
            }
        }

        Int64 wfstepId;
        public Int64 WFStepId
        {
            get
            {
                return wfstepId;
            }
            set
            {
                wfstepId = value;
            }
        }

        TypesofRules ruletype;
        public TypesofRules RuleType
        {
            get
            {
                return ruletype;
            }
            set
            {
                ruletype = value;
            }
        }

        System.Collections.Generic.List<Zamba.Core.IRule> childRules = new System.Collections.Generic.List<Zamba.Core.IRule>();
        public System.Collections.Generic.List<Zamba.Core.IRule> ChildRules
        {
            get
            {
                return childRules;
            }
            set
            {
                childRules = value;
            }
        }

        Zamba.Core.IRule parentRule;
        public Zamba.Core.IRule ParentRule
        {
            get
            {
                return parentRule;
            }
            set
            {
                parentRule = value;
            }
        }
        #endregion

        /// <summary>
        /// Get all the childs and give them the results
        /// </summary>
        /// <param name="executionContext"></param>
        /// <returns></returns>
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            //for (int child = 0; child < this.EnabledActivities.Count; child++)
            //{
            //    IResultActivity childActivity = this.EnabledActivities[child] as IResultActivity;

            //    if (null != childActivity && null != this.results)
            //    {
            //        childActivity.Results = this.results;

            //        // Ejecutar la actividad
            //        executionContext.ExecuteActivity((System.Workflow.ComponentModel.Activity)childActivity);
            //    }

            //}
            return ActivityExecutionStatus.Closed;
        }


        /// <summary>
        /// Evento que se dispara al agrega activities
        /// </summary>
        public event ItemListChanging OnItemListChanging;
    }
}