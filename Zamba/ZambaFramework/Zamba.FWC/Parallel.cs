using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Reflection;
using Zamba.Core;
using Zamba.Core.Enumerators;

namespace Zamba.WFActivity.Xoml
{
    [ToolboxItem(typeof(ParallelToolboxItem))]
    [Designer(typeof(ParallelDesigner), typeof(IDesigner))]
    public partial class Parallel : CompositeActivity, IResultActivity
	{
        public Parallel()
		{
			InitializeComponent();
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
    }
}