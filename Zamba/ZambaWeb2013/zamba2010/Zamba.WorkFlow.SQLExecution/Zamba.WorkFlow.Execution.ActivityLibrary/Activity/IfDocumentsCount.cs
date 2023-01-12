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
    /// Esta regla Valida la cantidad de tareas.
    /// </summary>
    [RuleDescription("VALIDA CANTIDAD DE TAREAS"), RuleHelp("Valida la cantidad de tareas")]
    [ToolboxItem(typeof(ParallelToolboxItem))]
    [Designer(typeof(ParallelDesigner), typeof(IDesigner))]
    public partial class IfDOCUMENTSCOUNT : ZCompositeRule, IResultActivity, Zamba.Core.IIfDOCUMENTSCOUNT
    {
        private Core.Comparadores _comparacion;
        private short _cantidadTareas;

        public IfDOCUMENTSCOUNT()
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
                branch.Results = this.Results;
                Zamba.WFExecution.PlayIfDocumentsCount play = new Zamba.WFExecution.PlayIfDocumentsCount();
                play.Play(this.Results, (Zamba.Core.IIfDOCUMENTSCOUNT)this);

                // Ejecutar la actividad
                executionContext.ExecuteActivity((System.Workflow.ComponentModel.Activity)branch);
            }

            return ActivityExecutionStatus.Closed;
        }

        #region IIfDOCUMENTSCOUNT Members

        short Zamba.Core.IIfDOCUMENTSCOUNT.CantidadTareas
        {
            get
            {
                return this._cantidadTareas;
            }
            set
            {
                this._cantidadTareas = value;
            }
        }

        Zamba.Core.Comparadores Zamba.Core.IIfDOCUMENTSCOUNT.Comparacion
        {
            get
            {
                return this._comparacion;
            }
            set
            {
                this._comparacion = value;
            }
        }

        #endregion
        #region Miembros de IResultActivity
        
        ///// <summary>
        ///// Devuelve el diseñador de messageActivity
        ///// </summary>
        ///// <returns></returns>
        //public ZRuleControl GetDesigner()
        //{
        //    Zamba.Core.IIfDOCUMENTSCOUNT rule = (Zamba.Core.IIfDOCUMENTSCOUNT)this;
        //    UCIfDocumentsCount cont = new UCIfDocumentsCount(ref rule);
        //    return cont;
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

        #region IRule Members
        #endregion
    }
}
