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
    /// Esta Regla exporta una tarea a excel.
    /// </summary>
    [RuleDescription("EXPORTAR A EXCEL"), RuleHelp("Exporta una tarea a excel.")]
    public partial class DoExportToExcel : ZRule, IResultActivity, IDoExportToExcel
	{	

        private String m_sRuta;
        
        public DoExportToExcel()
		{
			InitializeComponent();
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDoExportToExcel play =
                new Zamba.WFExecution.PlayDoExportToExcel();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        #region IDoExportToExcel Members

        public string Ruta
        {
            get
            {
                return m_sRuta;
            }
            set
            {
                m_sRuta = value;
            }
        }

        #endregion

        public override void SetParams(object[] Params)
        {
            this.Ruta = Params.GetValue(0).ToString();
        }
        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this.Ruta);
            return Params;
        }
    }
}