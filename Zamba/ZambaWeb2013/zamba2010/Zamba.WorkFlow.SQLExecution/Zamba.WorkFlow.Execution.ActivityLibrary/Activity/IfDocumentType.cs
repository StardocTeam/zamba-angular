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
    /// Esta Regla Valida el tipo de documento.
    /// </summary>
    [RuleDescription("VALIDA TIPO DE DOCUMENTO"), RuleHelp("Valida el tipo de documento")]
    [ToolboxItem(typeof(ParallelToolboxItem))]
    [Designer(typeof(ParallelDesigner), typeof(IDesigner))]
public partial class IfDocumentType : ZCompositeRule, IResultActivity, Zamba.Core.IIfDocumentType
	{

        private Zamba.Core.Comparators _Comp;
        private Int32 _DocTypeId;
        private Zamba.WFExecution.PlayIfDocumentType play;
        
        public IfDocumentType()
		{
			InitializeComponent();
            play = new Zamba.WFExecution.PlayIfDocumentType(this);
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
                branch.Results = this.Results;
                play.Play(this.Results);

                // Ejecutar la actividad
                executionContext.ExecuteActivity((System.Workflow.ComponentModel.Activity)branch);
            }
            
            return ActivityExecutionStatus.Closed;
        }

        #region Miembros de IResultActivity
        
        ///// <summary>
        ///// Devuelve el diseñador de messageActivity
        ///// </summary>
        ///// <returns></returns>
        //public ZRuleControl GetDesigner()
        //{
        //    Zamba.Core.IIfDocumentType rule = (Zamba.Core.IIfDocumentType)this;
        //    UCIfDocumentType cont = new UCIfDocumentType(ref rule);
        //    return cont;
        //    return null;
        //}

        public override void SetParams(object[] Params)
        {
            this.DocTypeId = Int32.Parse(Params.GetValue(0).ToString());
            this.Comp = (Zamba.Core.Comparators)Params.GetValue(1);            
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this.DocTypeId);
            Params.Add(this.Comp);
            return Params;
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

        #region IIfDocumentType Members

        public Zamba.Core.Comparators Comp
        {
            get
            {
                return this._Comp;
            }
            set
            {
                this._Comp = value;
            }
        }

        public int DocTypeId
        {
            get
            {
                return this._DocTypeId;
            }
            set
            {
                this._DocTypeId = value;
            }
        }

        #endregion

        #region IRule Members
        #endregion
    }
}
