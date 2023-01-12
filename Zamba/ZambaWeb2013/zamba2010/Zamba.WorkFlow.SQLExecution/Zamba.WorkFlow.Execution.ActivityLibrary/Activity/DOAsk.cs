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
    [RuleDescription("REALIZAR PREGUNTA AL USUARIO"), RuleHelp("Realiza una pregunta al usuario.")]
    public partial class DoAsk : ZRule, IResultActivity, IDOAsk
	{

        private String _Variable;
        private String _Mensaje;
        private Zamba.WFExecution.PlayDOAsk play;
        
        public DoAsk()
		{
			InitializeComponent();
            play = new Zamba.WFExecution.PlayDOAsk(this);
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

        #region IDOAsk Members

        string IDOAsk.Variable
        {
            get
            {
                return _Variable;
            }
            set
            {
                _Variable = value;
            }
        }

        string IDOAsk.Mensaje
        {
            get
            {
                return _Mensaje;
            }
            set
            {
                _Mensaje = value;
            }
        }
        
        #endregion
    }
}
