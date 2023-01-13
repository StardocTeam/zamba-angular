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
    [RuleDescription("EJECUTAR APLICACION O COMANDO"), RuleHelp("Ejecuta una aplicacion o comando")]
    public partial class DOShell : ZRule, IResultActivity, IDOSHELL
	{
        private Zamba.WFExecution.PlayDOShell play;

        public DOShell()
		{
			InitializeComponent();
            play = new Zamba.WFExecution.PlayDOShell(this);
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

        #region Miembros de IDOSHELL
        string _filePath;
        string _parameter;

        string IDOSHELL.Filepath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
            }
        }

        string IDOSHELL.Parameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                _parameter = value;
            }
        }
        #endregion

    }
    
}