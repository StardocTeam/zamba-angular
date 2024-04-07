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
    [RuleDescription("VALIDAR INDICES"), RuleHelp("Valida los indices")]
    [ToolboxItem(typeof(ParallelToolboxItem))]
    [Designer(typeof(ParallelDesigner), typeof(IDesigner))]
public partial class IfIndex : ZCompositeRule, IResultActivity,Zamba.Core.IIfIndex
	{
        public IfIndex()
		{
			InitializeComponent();
            play = new Zamba.WFExecution.PlayIfIndex((Zamba.Core.IIfIndex)this);
		}
        private Boolean _OperatorAND;
        private Int32 time;
        private Zamba.WFExecution.PlayIfIndex play;
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
                branch.Results = this.Results;

                play.Play(this.Results);

                // Ejecutar la actividad
                executionContext.ExecuteActivity((System.Workflow.ComponentModel.Activity)branch);
            }
            
            return ActivityExecutionStatus.Closed;
        }
        private String _condiciones = String.Empty;
        private String _variable = String.Empty;
        private long _indexId;
        private Zamba.Core.Comparators _comparator;
        //private IndexValue ComparativeValue;

        protected const String SEPARADOR_CONDICION = "*";
        protected const String SEPARADOR_CAMPO = "|";

        #region Miembros de IResultActivity
      
        ///// <summary>
        ///// Devuelve el dise�ador de messageActivity
        ///// </summary>
        ///// <returns></returns>
        //public ZRuleControl GetDesigner()
        //{
        //    Core.IIfIndex rule = (Core.IIfIndex)this;
        //    UCIfIndex designer = new UCIfIndex(ref rule);
        //    return designer;
        //}

        public override void SetParams(object[] Params)
        {
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            return null;
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

        #region Miembros de IIfIndex

        Zamba.Core.Comparators Zamba.Core.IIfIndex.Comparator
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

        string Zamba.Core.IIfIndex.Condiciones
        {
            get
            {
                return _condiciones;
            }
            set
            {
                _condiciones = value;
            }
        }

        long Zamba.Core.IIfIndex.IndexId
        {
            get
            {
                return _indexId;
            }
            set
            {
                _indexId = value;
            }
        }

        //string Zamba.Core.IIfIndex.Valor
        //{
        //    get
        //    {
        //        return ComparativeValue._Valor;
        //    }
        //    set
        //    {
        //        ComparativeValue = new IndexValue(Value);
        //    }
        //}

        string Zamba.Core.IIfIndex.Variable
        {
            get
            {
                return _variable;
            }
            set
            {
                _variable = value;
            }
        }

        #endregion

        #region Miembros de IIfIndex


        public bool OperatorAND
        {
            get
            {
                return this._OperatorAND;
              
            }
            set
            {
                this._OperatorAND = value;
            }
        }

        #endregion

        #region IRule Members
        #endregion

        #region IRule Members


        public string MaskName
        {
            get { throw new NotImplementedException(); }
        }

        public TypesofRules RuleType
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

        public int Version
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

        public long WFStepId
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

        #region IResultActivity Members

        System.Collections.Generic.List<object> IResultActivity.GetParams()
        {
            throw new NotImplementedException();
        }

        string IResultActivity.MaskName
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

        event ItemListChanging IResultActivity.OnItemListChanging
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        System.Collections.Generic.List<ITaskResult> IResultActivity.Results
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

        void IResultActivity.SetParams(object[] Params)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IRule Members

        bool? IRule.AlertExecution
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

        System.Collections.Generic.List<IRule> IRule.ChildRules
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

        bool? IRule.CloseTask
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

        bool? IRule.ContinueWithError
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

        bool? IRule.ExecuteWhenResult
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

        string IRule.Description
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

        string IRule.MaskName
        {
            get { throw new NotImplementedException(); }
        }

        IRule IRule.ParentRule
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

        bool? IRule.RefreshRule
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

        TypesofRules IRule.RuleType
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

        int IRule.Version
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

        long IRule.WFStepId
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

        #region ICore Members

        long ICore.ID
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

        string ICore.Name
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

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}