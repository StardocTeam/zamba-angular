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
//using Zamba.Controls;

namespace Zamba.WFActivity.Xoml
{
    /// <summary>
    /// This activity show a ArrayList of delays
    /// </summary>
    [RuleDescription("MENSAJE INTERNO"), RuleHelp("Envia un mensaje interno.")]
    public partial class DOInternalMessage : ZRule, IResultActivity, IDOInternalMessage
	{
        private String _CCOStr = String.Empty, _CCStr = String.Empty, _ToStr = String.Empty;
        private Boolean _oneDocPerMail;
        private InternalMessage _msg = new InternalMessage();
        
        public DOInternalMessage()
		{
			InitializeComponent();
		}
        
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Zamba.WFExecution.PlayDOInternalMessage play =
                new Zamba.WFExecution.PlayDOInternalMessage();
            play.Play(Results, this);
            return ActivityExecutionStatus.Closed;
        }

        #region IDOInternalMessage Members

        string IDOInternalMessage.CCOStr
        {
            get
            {
                return this._CCOStr;
            }
            set
            {
                this._CCOStr = value;
            }
        }

        string IDOInternalMessage.CCStr
        {
            get
            {
                return this._CCStr;
            }
            set
            {
                this._CCStr = value;
            }
        }

        public IInternalMessage Msg
        {
            get
            {
                return this._msg;
            }
            set
            {
                this._msg = (InternalMessage)value;
            }
        }

        bool IDOInternalMessage.OneDocPerMail
        {
            get
            {
                return this._oneDocPerMail;
            }
            set
            {
                this._oneDocPerMail = value;
            }
        }

        string IDOInternalMessage.ToStr
        {
            get
            {
                return this._ToStr;
            }
            set
            {
                this._ToStr = value;
            }
        }

        #endregion

        public override void SetParams(object[] Params)
        {
            this._msg = (InternalMessage)Params[0];
            this._oneDocPerMail = (bool)Params[1];
        }
        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this._msg);
            Params.Add(this._oneDocPerMail);
            return Params;
        }

        private void BuildTo(string toStr)
        {
           if (toStr.Length > 0)
           {
               String[] ids = toStr.Split(char.Parse(";"));
               foreach (String str in ids)
               {
                   User user;
                   try
                   {
                       user = (User)UserBusiness.GetUserById(Int32.Parse(str));
                   }
                   catch 
                    {
                        throw new Exception("No se pudo encontrar un usuario con id " + str);
                    }
                    if (user == null)
                    {
                        throw new Exception("No se pudo encontrar un usuario con id " + str);
                    }
                    else
                    {
                        _msg.TOUSER.Add(user);
                        _msg.ToStr += user.Name + " ";
                    }
                    user = null;
                }
            }
        }
        private void BuildCC(string cC)
        { 
            if (cC.Length > 0)
            {
                String[] ids = cC.Split(char.Parse(";"));
                foreach (String str in ids)
                {
                    User user;
                    try
                    {
                        user = (User)UserBusiness.GetUserById(Int32.Parse(str));
                    }
                    catch
                    {
                        throw new Exception ("No se pudo encontrar un usuario con id " + str);
                    }
                    if (user == null)
                    {
                        throw new Exception("No se pudo encontrar un usuario con id " + str);
                    }
                    else
                    {
                        _msg.CC.Add(user);
                        _msg.CCStr += user.Name + " ";
                    
                    }
                    
                user = null;
                }
                _msg.CCStr = cC;
            }
        }
        private void BuildCCO(string cCO)
        { 
            if (cCO.Length > 0)
            {
                String[] ids = cCO.Split(char.Parse(";"));
                foreach (String str in ids)
                {
                    User user;
                    try
                    {
                        user = (User)UserBusiness.GetUserById(Int32.Parse(str));
                    }
                   catch 
                    {
                       ZClass.raiseerror(new Exception("No se pudo encontrar un usuario con id " + str));
                       break;
                    }
                    if (user == null)
                    { 
                        ZClass.raiseerror(new Exception("No se pudo encontrar un usuario con id " + str));
                    }
                    else
                    {
                        _msg.CCO.Add(user);
                        _msg.CCOStr += user.Name + " ";
                    }
                    user = null;
                }
            }
        }
    }
}