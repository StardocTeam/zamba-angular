using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Zamba.Core;
using Zamba.WFExecution;

namespace Zamba.WFActivity.Xoml
{
    [RuleDescription("Outlook Appointment"), RuleHelp("Permite crear un nuevo appointment en Outlook.")]
	public partial class DoCreateOutlookAppointment: ZRule, IResultActivity, IDOCreateOutlookAppointment 
	{
		public DoCreateOutlookAppointment()
		{
			InitializeComponent();
		}

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            PlayDOCreateOutlookAppointment play = new PlayDOCreateOutlookAppointment(this);
            play.Play(Results);
            return ActivityExecutionStatus.Closed;
        }

        public Hashtable RuleVariablesInterReglas
        {
            get { return WFRuleParent.VariablesInterReglas; }
        }

        public override void SetParams(object[] Params)
        {
            //this._servertype = Int32.Parse(Params[3].ToString());
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            return Params;
        }

        #region IDOCreateOutlookAppointment Members

        public OLAppointmentDateType AppointmentDateType
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

        public string AppointmentEndDate
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

        public string AppointmentEndTime
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

        public string AppointmentStartDate
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

        public string AppointmentStartTime
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

        public string Body
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

        public string Location
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

        public bool ShowAppointmentForm
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

        public string Subject
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
