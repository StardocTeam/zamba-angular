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
    /// Regla que agrega un documento al workflow.
    /// </summary>
    [RuleDescription("AGREGAR A WORKFLOW"), RuleHelp("Agrega un documento al workflow.")]
    public partial class DOADDTOWF : ZRule, IResultActivity, IDOADDTOWF
	{
        private Int32 m_iWorkId;
        
        public DOADDTOWF()
		{
			InitializeComponent();
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDOADDTOWF play =
                new Zamba.WFExecution.PlayDOADDTOWF();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        #region IDOADDTOWF Members

        public string WorkFlowName()
        {
            return Zamba.Data.WFFactory.GetWfNameById(this.m_iWorkId);
        }

        public int WorkId
        {
            get
            {
                return m_iWorkId;
            }
            set
            {
                m_iWorkId = value;
            }
        }

        #endregion

        #region Miembros de IResultActivity

        public override void SetParams(object[] Params)
        {
             WorkId = Int32.Parse(Params.GetValue(0).ToString());
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(WorkId);
            return Params;
        }

        #endregion
    }
}
