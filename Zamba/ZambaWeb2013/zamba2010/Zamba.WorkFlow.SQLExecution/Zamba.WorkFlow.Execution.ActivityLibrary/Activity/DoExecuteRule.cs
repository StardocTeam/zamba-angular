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
    /// Esta Regla ejecuta una regla.
    /// </summary>
   [RuleDescription("EJECUTAR REGLA"), RuleHelp("Ejecuta una regla.")]
    public partial class DOExecuteRule : ZRule, IResultActivity, IDOExecuteRule
	{
        private Int32 _RuleId;
        private Zamba.WFExecution.PlayDOExecuteRule play;
        
        public DOExecuteRule()
		{
			InitializeComponent();
            this.play = new Zamba.WFExecution.PlayDOExecuteRule(this);
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(Results);
            return ActivityExecutionStatus.Closed;
        }

        public override void SetParams(object[] Params)
        {
            this.RuleID = Int32.Parse(Params.GetValue(0).ToString());
        }
        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this.RuleID);
            return Params;
        }

        #region IDOExecuteRule Members

        public int RuleID
        {
            get
            {
                return _RuleId;
            }
            set
            {
                _RuleId = value;
            }

        }

        #endregion

        #region IDOExecuteRule Members

        public bool IsRemote
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
