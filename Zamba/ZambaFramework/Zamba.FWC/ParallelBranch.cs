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
    /// Defines the ParallelBranch activity
    /// </summary>
    /// <remarks>
    /// This activity is added as a child of the ParallelIf activity. You need to set the condition
    /// property which will be evaluated at runtime, and if this returns true then the subordinate
    /// activities will be executed.
    /// </remarks>
    //[ToolboxItem(false)]
    //[Designer(typeof(MCIfBranchDesigner))]
    //[ActivityValidator(typeof(MCIfBranchValidator))]
    public class ParallelBranch : SequenceActivity, IResultActivity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParallelBranch(Boolean IsFirst)
	    {
            isFirst = IsFirst;
	    }

        public ParallelBranch()
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
        
        private Boolean isFirst;
        public Boolean IsFirst
        {
            get { return isFirst; }
            set { isFirst = value;}
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
        //public ZRuleControl GetDesigner()
        //{
        //    return null;
        //}

        public System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(isFirst);
            return Params;
        }

        Int64 id;
        public Int64 ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public void SetParams(object[] Params)
        {
            this.isFirst = Convert.ToBoolean(Int16.Parse(Params.GetValue(0).ToString()));
        }

        Int32 version;
        public Int32 Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        public string MaskName
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
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
            for (int child = 0; child < this.EnabledActivities.Count; child++)
            {
                IResultActivity childActivity = this.EnabledActivities[child] as IResultActivity;

                if (null != childActivity && null != this.results)
                {
                    childActivity.Results = this.results;
                }
                    // Ejecutar la actividad
                    executionContext.ExecuteActivity((System.Workflow.ComponentModel.Activity)childActivity);
            }
            return ActivityExecutionStatus.Closed;
        }

        #region Copiar Y Pegar en cada regla que herede de sequential o composite
        /// <summary>
        /// Manejo del ItemChanging
        /// </summary>
        public event ItemListChanging OnItemListChanging
        {
            add
            {
                this.dItemListChanging += value;
            }
            remove
            {
                this.dItemListChanging -= value;
            }
        }
        private ItemListChanging dItemListChanging = null;

        protected override void OnListChanged(ActivityCollectionChangeEventArgs e)
        {
            base.OnListChanged(e);
            for (int i = 0; i <= e.AddedItems.Count - 1; i++)
            {
                //e.AddedItems[i].Parent = this;
                ((IResultActivity)e.AddedItems[i]).OnItemListChanging += new ItemListChanging(this.passItemListChanging);
            }

            if (this.dItemListChanging != null)
                this.dItemListChanging(e);
        }

        /// <summary>
        /// Dispara el evento ListChanging para que lo capture el padre
        /// </summary>
        /// <param name="e"></param>
        private void passItemListChanging(ActivityCollectionChangeEventArgs e)
        {
            if (this.dItemListChanging != null)
                dItemListChanging(e);
        }
        #endregion
        #region IRule Members

        public bool? AlertExecution { get; set; }

        public bool? ExecuteWhenResult { get; set; }
        public DateTime StartTime { get; set; }
        public bool? RefreshRule { get; set; } 
       

        public bool? ContinueWithError { get; set; }
        public System.Diagnostics.TextWriterTraceListener TraceTime { get; set; }
        public bool? CloseTask { get; set; }

        public string Help { get; set; }

        public bool? CleanRule { get; set; }

        #endregion
    }

}