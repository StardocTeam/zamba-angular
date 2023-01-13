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
    ///  modulo de Implementacion para la Rule Action AutoComplete
    /// </summary>
    [RuleDescription("AUTOCOMPLETAR"), RuleHelp("Completa automaticamente los indices de un documento.")]
    public partial class DOAutoComplete : ZRule, IResultActivity, IDOAutoComplete
	{
        private Zamba.WFExecution.PlayDOAutoComplete play;
		public DOAutoComplete()
		{
			InitializeComponent();
            play = new Zamba.WFExecution.PlayDOAutoComplete(this);
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(Results);
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