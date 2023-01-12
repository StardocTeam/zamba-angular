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
    /// Regla que envia un mail automatico.
    /// </summary>
    [RuleDescription("AUTOMAIL"), RuleHelp("Envia un mail automatico.")]
    public partial class DOAutoMail : ZRule, IDOAutoMail
    {
        private Boolean _addDocument, _addLink, _addIndexs, _groupMailTo,_sendOnce;
        private SMTP_Validada _smtp;
        private AutoMail _autoMail;
        private System.Collections.Generic.List<string> _indexsnames;
        private Zamba.WFExecution.PlayDOAutoMail play;

        public DOAutoMail()
        {
            InitializeComponent();
            play = new Zamba.WFExecution.PlayDOAutoMail(this);
            if (this.smtp == null)
            {
                this.smtp = new SMTP_Validada();
            }
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(this.Results);
            return ActivityExecutionStatus.Closed;
        }

        #region IDOAutoMail Members

        public bool AddDocument
        {
            get
            {
                return _addDocument;
            }
            set
            {
                _addDocument = value;
            }
        }

        public bool SendOnce
        {
            get
            {
                return _sendOnce;
            }
            set
            {
                _sendOnce = value;
            }
        }
        public bool GroupMailTo
        {
            get
            {
                return _groupMailTo;
            }
            set
            {
                _groupMailTo = value;
            }
        }
        public bool AddIndexs
        {
            get
            {
                return _addIndexs;
            }
            set
            {
                _addIndexs = value;
            }
        }

        public bool AddLink
        {
            get
            {
                return _addLink;
            }
            set
            {
                _addLink = value;
            }
        }

        public bool groupMailTo
        {
            get
            {
                return _groupMailTo;
            }
            set
            {
                _groupMailTo = value;
            }
        }

        public IAutoMail Automail
        {
            get
            {
                return _autoMail;
            }
            set
            {
                _autoMail = (Zamba.Core.AutoMail)value;
            }
        }

        public System.Collections.Generic.List<string> IndexNames
        {
            get
            {
                return _indexsnames;
            }
            set
            {
                _indexsnames = value;
            }
        }

        public ISMTP_Validada smtp
        {
            get
            {
                return _smtp;
            }
            set
            {
                _smtp = (Zamba.Core.SMTP_Validada)value;
            }
        }

        #endregion

        #region Miembros de IResultActivity

       public override void SetParams(object[] Params)
        {
            Automail = Zamba.Core.MessagesBussines.GetAutomailById(Int32.Parse(Params.GetValue(0).ToString()));
            AddDocument = Convert.ToBoolean(Int16.Parse(Params.GetValue(1).ToString()));
            AddLink = Convert.ToBoolean(Int16.Parse(Params.GetValue(2).ToString()));
            AddIndexs = Convert.ToBoolean(Int16.Parse(Params.GetValue(3).ToString()));
            smtp.User = Params.GetValue(4).ToString();
            smtp.Password = Params.GetValue(5).ToString();
            smtp.Port = Int32.Parse(Params.GetValue(6).ToString());
            smtp.Server = Params.GetValue(7).ToString();

            if (!String.IsNullOrEmpty(Params[8].ToString()))
            {
                foreach (String index in Params.GetValue(8).ToString().Split(char.Parse("|")))
                    IndexNames.Add(index);
            }
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(Automail);
            Params.Add(AddDocument);
            Params.Add(AddLink);
            Params.Add(AddIndexs);
            Params.Add(smtp.User);
            Params.Add(smtp.Password);
            Params.Add(smtp.Port);
            Params.Add(smtp.Server);
            Params.Add(IndexNames);
            return Params;
        }

        #endregion
    }
}