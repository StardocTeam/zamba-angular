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
    /// Esta Regla Valida la existencia del archivo.
    /// </summary>
    [RuleDescription("VALIDA EXISTENCIA DE ARCHIVO"), RuleHelp("Valida la existencia del archivo")]
    [ToolboxItem(typeof(ParallelToolboxItem))]
    [Designer(typeof(ParallelDesigner), typeof(IDesigner))]
public partial class IfFileExist : ZCompositeRule, IResultActivity, Zamba.Core.IIfFileExists
	{
        //Private _searchPath As String
        //Private _searchOption As IO.SearchOption = SearchOption.AllDirectories
        //Private _textoInteligente As String

        private String _searchPath;
        private System.IO.SearchOption _searchOption = System.IO.SearchOption.AllDirectories;
        private String _textoInteligente;

        public IfFileExist()
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
                Zamba.WFExecution.PlayIfFileExists play = new Zamba.WFExecution.PlayIfFileExists();
                play.Play(this.Results, (Zamba.Core.IIfFileExists) this);

                // Ejecutar la actividad
                executionContext.ExecuteActivity((System.Workflow.ComponentModel.Activity)branch);
            }
            
            return ActivityExecutionStatus.Closed;
        }
        #region IIfFileExists Members

        public System.IO.SearchOption SearchOption
        {
            get
            {
                return this._searchOption;
            }
            set
            {
                this._searchOption = (System.IO.SearchOption)value;
            }
        }

        public string SearchPath
        {
            get
            {
                return this._searchPath;
            }
            set
            {
                this._searchPath = value;
            }
        }

        public string TextoInteligente
        {
            get
            {
                return this._textoInteligente;
            }
            set
            {
                this._textoInteligente = value;
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
        //    Zamba.Core.IIfFileExists rule = (Zamba.Core.IIfFileExists)this;
        //    UCIfFileExists cont = new UCIfFileExists(ref rule);
        //    return cont;
        //    return null;
        //}

        public override void SetParams(object[] Params)
        {
            this.SearchPath = Params.GetValue(0).ToString();
            this.SearchOption = (System.IO.SearchOption)((Int32)Params.GetValue(1));
            this.TextoInteligente = Params.GetValue(2).ToString();
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this.SearchPath);
            Params.Add(this.SearchOption);
            Params.Add(this.TextoInteligente);
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

        #region IRule Members
        #endregion
    }
}
