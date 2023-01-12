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
    [RuleDescription("GENERAR DOCUMENTO DE EXCEL"), RuleHelp("Genera Documentos en Excel")]
[Designer(typeof(GenerateExcelDesigner), typeof(IDesigner))]
    public partial class DoGenerateExcel : ZRule, IResultActivity, IDoGenerateExcel
	{
        public DoGenerateExcel()
		{
			InitializeComponent();
		}

        private Int32 docTypeId = 0;
        public Int32 DocTypeId
        {
            get { return docTypeId; }
            set { docTypeId = value;}
        }
        private String title = String.Empty;
        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        private String index = String.Empty;
        public String Index
        {
            get { return index; }
            set { index = value; }
        }
        private String footer = String.Empty;
        public String Footer
        {
            get { return footer; }
            set { footer = value; }
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDOGenerateExcel play =
                new Zamba.WFExecution.PlayDOGenerateExcel();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        public override void SetParams(object[] Params)
        {
            docTypeId = Int32.Parse(Params.GetValue(0).ToString());
            title = Params.GetValue(1).ToString();
            index = Params.GetValue(2).ToString();
            footer = Params.GetValue(3).ToString();
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(docTypeId);
            Params.Add(title);
            Params.Add(index);
            Params.Add(footer);
            return Params;
        }
     }
}