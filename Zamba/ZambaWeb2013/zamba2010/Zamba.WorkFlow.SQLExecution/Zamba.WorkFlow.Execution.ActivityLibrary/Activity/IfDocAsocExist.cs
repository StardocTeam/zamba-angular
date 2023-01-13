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
    /// Esta Regla Valida la existencia de documentos asociados.
    /// </summary>
  [RuleDescription("VALIDA EXISTENCIA DOCUMENTO ASOCIADO"), RuleHelp("Valida la existencia de documentos asociados")]
    [ToolboxItem(typeof(ParallelToolboxItem))]
    [Designer(typeof(ParallelDesigner), typeof(IDesigner))]
public partial class IfDocAsocExist : ZCompositeRule, IResultActivity, Zamba.Core.IIfDocAsocExist
	{

        public Int32 _TipoDeDocumento;
        public Boolean _Existencia;

        public IfDocAsocExist()
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
                Zamba.WFExecution.PlayIfDocAsocExist play = new Zamba.WFExecution.PlayIfDocAsocExist();
                play.Play(this.Results,(Zamba.Core.IIfDocAsocExist) this);

                // Ejecutar la actividad
                executionContext.ExecuteActivity((System.Workflow.ComponentModel.Activity)branch);
            }
            
            return ActivityExecutionStatus.Closed;
        }

        #region IIfDocAsocExist Members

        public Boolean Existencia
        {
            get
            {
                return _Existencia;
            }
            set
            {
                _Existencia = value;
            }
        }

        public int TipoDeDocumento
        {
            get
            {
                return _TipoDeDocumento;
            }
            set
            {
                _TipoDeDocumento = value;
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
        //    Zamba.Core.IIfDocAsocExist rule = (Zamba.Core.IIfDocAsocExist)this;
        //    UCIfDocAsocExist cont = new UCIfDocAsocExist(ref rule);
        //    return cont;
        //    return null;
        //}
      
        public override void SetParams(object[] Params)
        {
            TipoDeDocumento = Int32.Parse(Params.GetValue(0).ToString());
            Existencia = Convert.ToBoolean(Int16.Parse(Params.GetValue(1).ToString()));
        }
        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this.TipoDeDocumento);
            Params.Add(this.Existencia);
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
