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
    /// This activity show a ArrayList of delays
    /// </summary>
    [RuleDescription("DISEÑO DE REGLA"), RuleHelp("Brinda ayuda para diseñar reglas de manera semántica.")]
    public partial class DoDesign : ZRule, IResultActivity, IDoDesign
    {
        public DoDesign()
		{
			InitializeComponent();
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDoDesign play =
                new Zamba.WFExecution.PlayDoDesign();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        public override void SetParams(object[] Params)
        {
            this._help = Params[0].ToString();
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this._help);
            return Params;
        }

        #region Miembros de IDoDesign

        private string _help;

        public string Help
        {
            get
            {
                return _help;
            }
            set
            {
                _help = value;
            }
        }

        #endregion
    }
}
