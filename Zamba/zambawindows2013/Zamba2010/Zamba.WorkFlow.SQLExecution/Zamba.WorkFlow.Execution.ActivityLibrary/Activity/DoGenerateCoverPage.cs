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
    /// Esta Regla genera una caratula a partir del documento seleccionado.
    /// </summary>
    [RuleDescription("GENERAR CARATULA"), RuleHelp("Genera una caratula a partir del documento seleccionado.")]
    public partial class DoGenerateCoverPage : ZRule, IResultActivity, IDoGenerateCoverPage
	{	
		public DoGenerateCoverPage()
		{
			InitializeComponent();
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDoGenerateCoverPage play =
                new Zamba.WFExecution.PlayDoGenerateCoverPage();
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

        #region IDoGenerateCoverPage Members

        public long DocTypeId
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

        public string Note
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

        public bool PrintIndexs
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