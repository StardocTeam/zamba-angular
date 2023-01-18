using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Reflection;
using System.Diagnostics;
using Zamba.Core;
//using Zamba.WFUserControl;

namespace Zamba.WFActivity.Xoml
{
    /// <summary>
    /// This activity show a ArrayList of delays
    /// </summary>
    [RuleDescription(""), RuleHelp("")]
    public partial class IfBranch : SequenceActivity, IResultActivity, IIfBranch
    {
        private Zamba.WFExecution.PlayIfBranch play;
        public IfBranch()
		{
			InitializeComponent();
            play = new Zamba.WFExecution.PlayIfBranch(this);
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(Results);
            return ActivityExecutionStatus.Closed;
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
            this._ifType = Boolean.Parse(Params[0].ToString());
        }

        public System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this._ifType);
            return Params;
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
                return this.Name.Split(Char.Parse("-"))[0];
            }
            set
            {
                this.Name = value + "-" + this.id.ToString();
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
        #endregion
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

        ///// <summary>
        ///// Se da cuando se modifica la lista de items
        ///// </summary>
        ///// <param name="e"></param>
        protected override void OnListChanging(ActivityCollectionChangeEventArgs e)
        {
            foreach (Activity time in e.AddedItems)
            {
                if (time is IResultActivity)
                    {
                    ((IResultActivity)time).OnItemListChanging += new ItemListChanging(this.passItemListChanging);
                if (((IResultActivity)time).RuleType == 0 && ((IResultActivity)time).GetType() == typeof(ParallelBranch) && this.dItemListChanging == null)
                {
                    ((IResultActivity)time).RuleType = TypesofRules.Regla;
                }
                    }
            }

            if (this.dItemListChanging != null)
            {
                this.dItemListChanging(e);
            }

            base.OnListChanging(e);
        }
        private ItemListChanging dItemListChanging = null;

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

        #region Miembros de IIfBranch

        bool _ifType;
        public bool ifType
        {
            get
            {
                return _ifType;
            }
            set
            {
                _ifType =value;
            }
        }

        #endregion
        #region IRule Members

        public bool? AlertExecution
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool? ExecuteWhenResult
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public System.Diagnostics.TextWriterTraceListener TraceTime
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public bool? RefreshRule
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool? ContinueWithError
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public DateTime StartTime
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public bool? CloseTask
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Help
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool? CleanRule
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
