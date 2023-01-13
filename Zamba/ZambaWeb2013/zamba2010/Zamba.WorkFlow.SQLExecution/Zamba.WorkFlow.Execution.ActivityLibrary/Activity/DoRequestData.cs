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
    [RuleDescription("SOLICITAR DATOS"), RuleHelp("Solicita los datos.")]
    public partial class DoRequestData : ZRule, IResultActivity, IDoRequestData
	{
        private Zamba.WFExecution.PlayDoRequestData play;
        public DoRequestData()
		{
			InitializeComponent();
            this.play = new Zamba.WFExecution.PlayDoRequestData(this);
		}
        private Int32 _docTypeId;
        private ArrayList _arrayIds;
        
                
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(Results);
            return ActivityExecutionStatus.Closed;
        }

        public override void SetParams(object[] Params)
        {
            this._docTypeId = Int32.Parse(Params[0].ToString());
            this._arrayIds = (ArrayList)Params[1];
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this._docTypeId);
            Params.Add(this._arrayIds);
            return Params;
        }

        #region Miembros de IDoRequestData

        public ArrayList ArrayIds
        {
            get
            {
                return _arrayIds;
            }
            set
            {
                _arrayIds = value;
            }
        }

        public int DocTypeId
        {
            get
            {
                return _docTypeId;
            }
            set
            {
                _docTypeId = value;
            }
        }

        string IDoRequestData.JoinIds()
        {
            String Str = "";
            foreach (String Item in this.ArrayIds)
            {
                Str += "*" + Item;
            }
            Str = Str.Substring(1);
            return Str;
        }

        #endregion
    }
}
