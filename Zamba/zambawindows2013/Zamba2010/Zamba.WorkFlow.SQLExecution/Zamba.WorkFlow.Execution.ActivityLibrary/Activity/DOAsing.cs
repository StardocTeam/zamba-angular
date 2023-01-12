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
//using Zamba.WFBusiness;
//using Zamba.WFUserControl;

namespace Zamba.WFActivity.Xoml
{
    /// <summary>
    /// Regla para asignar una tarea a un usuario.
    /// </summary>
    [RuleDescription("ASIGNAR A USUARIO"), RuleHelp("Asigna una tarea a un usuario.")]
    public partial class DoAsign : ZRule, IResultActivity, IDoAsign
	{                    

        private String _AlternateUser = String.Empty;
        private Int32 _UserId = 0;
        private Zamba.WFExecution.PlayDoAsign play;
		public DoAsign()
		{
			InitializeComponent();
            play = new Zamba.WFExecution.PlayDoAsign(this);
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(Results);
            return ActivityExecutionStatus.Closed;
        }


        #region IDoAsign Members

        public string AlternateUser
        {
            get
            {
                return _AlternateUser;
            }
            set
            {
                _AlternateUser = value;
            }
        }

        public Hashtable RuleVariablesInterReglas
        {
            get
            {
                return WFRuleParent.VariablesInterReglas;
            }
        }

        public Int32 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                _UserId = value;
            }
        }

        #endregion

        public override void SetParams(object[] Params)
        {
            SortedList users = WFUserBussines.GetUsersByStepID(WFStepId);
            this._UserId = Int32.Parse(Params.GetValue(0).ToString());
            AlternateUser = Params.GetValue(1).ToString();
        }
        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(UserId);
            Params.Add(AlternateUser);
            return Params;
        }
    }
}
