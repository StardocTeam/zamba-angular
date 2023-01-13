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
//using Zamba.WFUserControl;
using Zamba.Core;

namespace Zamba.WFActivity.Xoml
{
    /// <summary>
    /// This activity show a ArrayList of messages
    /// </summary>
    [RuleDescription("VALIDA USUARIOS EN GRUPOS"), RuleHelp("Valida si el usuario pertenece a un grupo")]
    [ToolboxItem(typeof(ParallelToolboxItem))]
    [Designer(typeof(ParallelDesigner), typeof(IDesigner))]
    public partial class IfGroups : CompositeActivity, IResultActivity, Zamba.Core.IIfGroups
	{
        public IfGroups()
		{
			InitializeComponent();
		}

        private Int32 time;
        /// <summary>
        /// Title of the Form
        /// </summary>
        public Int32 Time
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// Wait the specified time
        /// </summary>
        /// <param name="executionContext"></param>
        /// <returns></returns>
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            for (int child = 0; child < this.EnabledActivities.Count; child++)
            {
                IResultActivity branch = this.EnabledActivities[child] as IResultActivity;
                branch.Results = this.results;

                Zamba.WFExecution.PlayIfGroups play =
                new Zamba.WFExecution.PlayIfGroups();
                play.Play(results, (Zamba.Core.IIfGroups)this);

                // Ejecutar la actividad
                executionContext.ExecuteActivity((System.Workflow.ComponentModel.Activity)branch);
            }
            
            return ActivityExecutionStatus.Closed;
        }

        #region Miembros de IResultActivity
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
        ///// <summary>
        ///// Devuelve el diseñador de messageActivity
        ///// </summary>
        ///// <returns></returns>
        //public ZRuleControl GetDesigner()
        //{
        //    Core.IIfUsers rule = (Core.IIfUsers)this;
        //    UCIfUsers designer = new UCIfUsers(ref rule);
        //    return designer;
        //}

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

            



        }

        public System.Collections.Generic.List<object> GetParams()
        {
            return null;
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

        Zamba.Core.TypesofRules ruletype;
        public Zamba.Core.TypesofRules RuleType
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
            foreach (IResultActivity time in e.AddedItems)
            {
                time.OnItemListChanging += new ItemListChanging(this.passItemListChanging);
            }

            if (this.dItemListChanging != null)
                this.dItemListChanging(e);

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
        private Hashtable _GroupList = new Hashtable(); 
        private Zamba.Core.UserComparators _comparator;

        #region Miembros de IIFGROUPS

        public Zamba.Core.UserComparators Comparator
        {
            get
            {
                return _comparator;
            }
            set
            {
                _comparator = value;
            }
        }

        public Hashtable GroupList
        {
            get
            {
                return _GroupList;
            }
            set
            {
                _GroupList = value;
            }
        }

        #endregion
    }
}
