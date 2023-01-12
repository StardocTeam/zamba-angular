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
    [RuleDescription("ENVIAR MAIL"), RuleHelp("Envía un mail.")]
    public partial class DOMail : ZRule, IResultActivity, IDOMail
    {
        private Zamba.WFExecution.PlayDOMail play;
        public DOMail()
        {
            InitializeComponent();
            play = new Zamba.WFExecution.PlayDOMail(this);
        }


        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            play.Play(Results);
            return ActivityExecutionStatus.Closed;
        }

        public override void SetParams(object[] Params)
        {
            this._Para = Params[0].ToString();
            this._CC = Params[1].ToString();
            this._CCO = Params[2].ToString();
            this._Asunto = Params[3].ToString();
            if (string.Compare(Params[4].ToString(), "1") == 0)
            {
                this._Senddocument = true;
            }
            else
                this._Senddocument = false;
            this._Body = Params[5].ToString();
            if (string.Compare(Params[6].ToString(), "1") == 0)
            {
                this._AttachAssociatedDocuments = true;
            }

            this._imagesNames = Params[7].ToString();
            this._pathImages = Params[8].ToString();

            if (string.Compare(Params[9].ToString(), "1") == 0)
            {
                this._groupMailTo = true;
            }
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this._Para);
            Params.Add(this._CC);
            Params.Add(this._CCO);
            Params.Add(this._Asunto);
            Params.Add(this._Body);
            Params.Add(this._Senddocument);
            Params.Add(this._AttachAssociatedDocuments);
            Params.Add(this._imagesNames);
            Params.Add(this._pathImages);
            Params.Add(this._groupMailTo);

            return Params;
        }

        private String _Para;
        private String _CCO;
        private String _Asunto;
        private String _Body;
        private Boolean _Senddocument;
        private String _CC;
        private Boolean _AttachAssociatedDocuments;
        private string _smtpMail;
        private string _smtpPass;
        private string _smtpPort;
        private string _smtpServer;
        private string _smtpUser;
        private bool _useSMTPConfig;
        private string _imagesNames;
        private string _pathImages;
        private bool _groupMailTo;
        private bool _keepAssociatedDocName;

        #region Miembros de IDOMail

        private IMailConfigDocAsoc.DTTypes mDTType = IMailConfigDocAsoc.DTTypes.AllDT;

        public IMailConfigDocAsoc.DTTypes DTType
        {
            get { return mDTType; }
            set { mDTType = value; }
        }
        private IMailConfigDocAsoc.Selections mSelection = IMailConfigDocAsoc.Selections.First;

        public IMailConfigDocAsoc.Selections Selection
        {
            get { return mSelection; }
            set { mSelection = value; }
        }
        private String mDocTypes;

        public String DocTypes
        {
            get { return mDocTypes; }
            set { mDocTypes = value; }
        }
        private Int32 mIndex;

        public Int32 Index
        {
            get { return mIndex; }
            set { mIndex = value; }
        }
        private Comparadores mOper;

        public Comparadores Oper
        {
            get { return mOper; }
            set { mOper = value; }
        }
        private String mIndexValue;

        public String IndexValue
        {
            get { return mIndexValue; }
            set { mIndexValue = value; }
        }

        public string Asunto
        {
            get
            {
                return _Asunto;
            }
            set
            {
                _Asunto = value;
            }
        }

        public string Body
        {
            get
            {
                return _Body;
            }
            set
            {
                _Body = value;
            }
        }

        public string CC
        {
            get
            {
                return _CC;
            }
            set
            {
                _CC = value;
            }
        }

        public string CCO
        {
            get
            {
                return _CCO;
            }
            set
            {
                _CCO = value;
            }
        }

        public string Para
        {
            get
            {
                return _Para;
            }
            set
            {
                _Para = value;
            }
        }

        public bool SendDocument
        {
            get
            {
                return _Senddocument;
            }
            set
            {
                _Senddocument = value;
            }
        }

        public bool AttachAssociatedDocuments
        {
            get
            {
                return _AttachAssociatedDocuments;
            }
            set
            {
                _AttachAssociatedDocuments = value;
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

        public string ImagesNames
        {
            get
            {
                return _imagesNames;
            }
            set
            {
                _imagesNames = value;
            }
        }

        public string SmtpServer
        {
            get
            {
                return _smtpServer;
            }
            set
            {
                _smtpServer = value;
            }
        }

        public string SmtpPort
        {
            get
            {
                return _smtpPort;
            }
            set
            {
                _smtpPort = value;
            }
        }

        public string SmtpMail
        {
            get
            {
                return _smtpMail;
            }
            set
            {
                _smtpMail = value;
            }
        }

        public string SmtpUser
        {
            get
            {
                return _smtpUser;
            }
            set
            {
                _smtpUser = value;
            }
        }

        public string SmtpPass
        {
            get
            {
                return _smtpPass;
            }
            set
            {
                _smtpPass = value;
            }
        }

        public bool UseSMTPConfig
        {
            get
            {
                return _useSMTPConfig;
            }
            set
            {
                _useSMTPConfig = value;
            }
        }

        public string PathImages
        {
            get
            {
                return _pathImages;
            }
            set
            {
                _pathImages = value;
            }
        }

        public bool KeepAssociatedDocsName
        {
            get
            {
                return _keepAssociatedDocName;
            }
            set
            {
                _keepAssociatedDocName = value;
            }
        }

        #endregion

        #region IDOMail Members


        public bool AttachLink
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

        #region IDOMail Members


        private Boolean mAutomatic;
        public bool Automatic
        {
            get
            {
                return mAutomatic;
            }
            set
            {
                mAutomatic = value;
            }
        }

        #endregion

        #region IDOMail Members

        #endregion
    }
}