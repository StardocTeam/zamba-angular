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
    /// Esta Regla obtiene los documentos asociados a un documento.
    /// </summary>
   [RuleDescription("DOCUMENTOS ASOCIADOS"), RuleHelp("Obtiene los documentos asociados a un documento.")]
    public partial class DOGetDocAsoc : ZRule, IResultActivity, IDOGetDocAsoc
	{	
        private String _tiposDeDocumento = String.Empty;
        private String _variable = String.Empty;
        private bool _continuarConResultadoObtenido = false;

		public DOGetDocAsoc()
		{
			InitializeComponent();
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDOGetDocAsoc play =
                new Zamba.WFExecution.PlayDOGetDocAsoc();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        #region IDOGetDocAsoc Members

        public string Variable
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

        public string tiposDeDocumento
        {
            get
            {
                return _tiposDeDocumento;
            }
            set
            {
                _tiposDeDocumento = value;
            }
        }

        public bool ContinuarConResultadoObtenido
        {
            get
            {
                return _continuarConResultadoObtenido;
            }
            set
            {
                _continuarConResultadoObtenido = value;
            }
        }

        #endregion
        
       public override void SetParams(object[] Params)
        {
            this.tiposDeDocumento = Params.GetValue(0).ToString();
            this.Variable = Params.GetValue(1).ToString();
            this.ContinuarConResultadoObtenido = bool.Parse(Params.GetValue(2).ToString());
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this.tiposDeDocumento);
            Params.Add(this.Variable);
            Params.Add(this.ContinuarConResultadoObtenido);
            return Params;
        }
   }
}