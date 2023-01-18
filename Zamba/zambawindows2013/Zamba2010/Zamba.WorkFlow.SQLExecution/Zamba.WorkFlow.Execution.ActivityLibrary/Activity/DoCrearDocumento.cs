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
	/// Regla para crar un documento.
	/// </summary>
    [RuleDescription("CREAR DOCUMENTO"), RuleHelp("Crea un documento.")]
    public partial class DOCrearDocumento : ZRule, IResultActivity, IDOCrearDocumento
	{	
		public DOCrearDocumento()
		{
			InitializeComponent();
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDOCrearDocumento play =
                new Zamba.WFExecution.PlayDOCrearDocumento();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }
        public override void SetParams(object[] Params)
        {
        }
        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            return Params;
        }
	}
}