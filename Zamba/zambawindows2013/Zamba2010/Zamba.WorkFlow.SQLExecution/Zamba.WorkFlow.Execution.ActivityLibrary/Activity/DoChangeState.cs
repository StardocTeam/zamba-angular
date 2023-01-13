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
    /// Regla para modificar el estado de una tarea.
    /// </summary>
    [RuleDescription("CAMBIAR ESTADO"), RuleHelp("Modifica el estado de una tarea.")]
    public partial class DoChangeState : ZRule, IResultActivity, IDoChangeState
	{

        private Int32 m_iStateId, m_iStepId;
        private Zamba.WFExecution.PlayDoChangeState play;
        
		public DoChangeState()
		{
			InitializeComponent();
            play = new Zamba.WFExecution.PlayDoChangeState(this);
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(Results);
            return ActivityExecutionStatus.Closed;
        }


        #region IDoChangeState Members

        public int StateId
        {
            get
            {
                return m_iStateId;
            }
            set
            {
                m_iStateId = value;
            }
        }

        public string StateName
        {
            get
            {
                return Zamba.Data.WFStepStatesFactory.GetStateNameByStateId(this.m_iStateId);
            }
        }

        public int StepId
        {
            get
            {
                return m_iStepId;
            }
            set
            {
                m_iStepId = value;
            }
        }

        #endregion

        public override void SetParams(object[] Params)
        {
            StateId = Int32.Parse(Params.GetValue(0).ToString());
            StepId = Int32.Parse(Params.GetValue(1).ToString());
        }
        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(StateId);
            Params.Add(StepId);
            return Params;
        }
    }
}