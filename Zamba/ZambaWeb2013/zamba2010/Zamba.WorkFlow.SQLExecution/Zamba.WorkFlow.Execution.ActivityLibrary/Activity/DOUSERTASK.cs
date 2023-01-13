using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Zamba.Core;
//using Zamba.WFUserControl;
using System.Workflow.ComponentModel;
using System.Workflow.Activities;

namespace Zamba.WFActivity.Xoml
{
	[RuleDescription("EJECUTA REGLA DE USUARIO"), RuleHelp("Ejecutar Regla de Usuario")]
    public partial class DOUSERTASK : ZRule, IResultActivity, IDOUSERTASK
	{	
		public DOUSERTASK()
		{
			InitializeComponent();
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDOUSERTASK play =
                new Zamba.WFExecution.PlayDOUSERTASK();
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