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
    [RuleDescription("GENERAR ID"), RuleHelp("Genera un id y completa un indice determinado.")]
    public partial class DoGetNewId : ZRule, IResultActivity, IDoGetNewId
	{
        public DoGetNewId()
		{
			InitializeComponent();
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDoGetNewId play =
                new Zamba.WFExecution.PlayDoGetNewId();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        Int32 m_iDocTypeId;
        Int32 m_iIndexId;

        public override void SetParams(object[] Params)
        {
            this.m_iDocTypeId = Int32.Parse(Params[0].ToString());
            this.m_iIndexId = Int32.Parse(Params[1].ToString());
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this.m_iDocTypeId);
            Params.Add(this.m_iIndexId);
            return Params;
        }

        #region Miembros de IDoGetNewId

        public int DocTypeId
        {
            get
            {
                return m_iDocTypeId;
            }
            set
            {
                m_iDocTypeId = value;
            }
        }

        public int IndexId
        {
            get
            {
                return m_iIndexId;
            }
            set
            {
                m_iIndexId = value;
            }
        }

        #endregion
    }
}
