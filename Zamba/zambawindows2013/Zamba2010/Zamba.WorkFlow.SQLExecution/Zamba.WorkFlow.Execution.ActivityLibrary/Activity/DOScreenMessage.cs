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
    [RuleDescription("MOSTRAR MENSAJE EN PANTALLA"), RuleHelp("Muestra un mensaje en pantalla.")]
    public partial class DOSCREENMESSAGE : ZRule, IResultActivity, IDOSCREENMESSAGE
	{

        private Zamba.WFExecution.PlayDOScreenMessage play;

        public DOSCREENMESSAGE()
		{
			InitializeComponent();
            this.play = new Zamba.WFExecution.PlayDOScreenMessage(this);
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(Results);
            return ActivityExecutionStatus.Closed;
        }

        public override void SetParams(object[] Params)
        {
            this.Mensaje = (String)Params[0];
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this.Mensaje);
            return Params;
        }

        #region Miembros de IDOSCREENMESSAGE

        String _message;
        String _NameScreen;

        #endregion

        #region Miembros de IDOSCREENMESSAGE

        public string Mensaje
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }
        public string NameScreen
        {
            get
            {
                return _NameScreen;
            }
            set
            {
                _NameScreen = value;
            }
        }

        #endregion
    }
}
