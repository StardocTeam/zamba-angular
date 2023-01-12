using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.Activities;
using System.Workflow.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using System.CodeDom.Compiler;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Tracking;
using Zamba.WorkFlow.Execution.WorkFlow;
using Zamba.WorkFlow.Business;
using Zamba.WFActivity.Xoml;
using Zamba.Core;

namespace Zamba.WorkFlow.Execution.Control
{
    public partial class WorkflowControl : UserControl, IDisposable, IServiceProvider
    {
        #region  Contructors
        /// <summary>
        /// Create the workflowControl
        /// </summary>
        public WorkflowControl()
        {
            InitializeComponent();

            try
            {
                WorkflowTheme.CurrentTheme.ReadOnly = false;
                WorkflowTheme.CurrentTheme.AmbientTheme.ShowConfigErrors = false;
                WorkflowTheme.CurrentTheme.ReadOnly = true;
                
                Toolbox toolbox = new Toolbox(this);
                this.workflowViewSplitter.Panel1.Controls.Add(toolbox);
                toolbox.Dock = DockStyle.Fill;
                toolbox.BackColor = BackColor;
                toolbox.Font = WorkflowTheme.CurrentTheme.AmbientTheme.Font;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Create the workflowControl
        /// </summary>
        public WorkflowControl(Int64 stepID)
        {
            InitializeComponent();

            try
            {
                WorkflowTheme.CurrentTheme.ReadOnly = false;
                WorkflowTheme.CurrentTheme.AmbientTheme.ShowConfigErrors = false;
                WorkflowTheme.CurrentTheme.ReadOnly = true;

                //Toolbox toolbox = new Toolbox(this);
                //this.workflowViewSplitter.Panel1.Controls.Add(toolbox);
                //toolbox.Dock = DockStyle.Fill;
                //toolbox.BackColor = BackColor;
                //toolbox.Font = WorkflowTheme.CurrentTheme.AmbientTheme.Font;

                workflowViewSplitter.Panel1.Controls.Clear();
                workflowViewSplitter.Panel2.Controls.Clear();
                this.pnlDesigner.Controls.Clear();

                this.workflow = new Zamba.WorkFlow.Execution.WorkFlow.WorkFlowModel();
                this.workflow.WFStepId = stepID;
                workflow.Name = "ZambaWorkflow";
                this.LoadWorkflow(stepID);
                this.pnlDesigner.Controls.Add(workflowView);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Create the workflowControl
        /// </summary>
        public WorkflowControl(Int64 stepId, Int64 parentId)
        {
            InitializeComponent();

            try
            {
                WorkflowTheme.CurrentTheme.ReadOnly = false;
                WorkflowTheme.CurrentTheme.AmbientTheme.ShowConfigErrors = false;
                WorkflowTheme.CurrentTheme.ReadOnly = true;

                Toolbox toolbox = new Toolbox(this);
                this.workflowViewSplitter.Panel1.Controls.Add(toolbox);
                toolbox.Dock = DockStyle.Fill;
                toolbox.BackColor = BackColor;
                toolbox.Font = WorkflowTheme.CurrentTheme.AmbientTheme.Font;

                this.workflow = new Zamba.WorkFlow.Execution.WorkFlow.WorkFlowModel();
                this.workflow.WFStepId = stepId;
                workflow.Name = "ZambaWorkflow";
                this.LoadWorkflow(stepId, parentId);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        #endregion

        #region  Properties,Variables And Constants
        /// <summary>
        /// Nombre de la assembly que tiene las activities creadas por el usuario
        /// </summary>
        private String[] assemblyNames = {"Zamba.WFActivity.Xoml.dll","Zamba.WFActivity.Regular.dll",
                                            "Zamba.Core.dll","Zamba.ICore.dll",
                                            "Zamba.WorkFlow.Execution.WorkFlow.dll",};

        //Visualizador del workflow
        private WorkflowView workflowView;
        private DesignSurface designSurface;
        private WorkflowLoader loader;
        private WorkflowCompilerResults compilerResults;
        private WorkflowRuntime workflowRuntime;
        //Workflow que esta corriendo
        private WorkFlowModel workflow;

        /// <summary>
        /// WorkFlow path
        /// </summary>
        private string XomlFile
        {
            get
            {
                return this.loader.XomlFile;
            }
            set
            {
                this.loader.XomlFile = value;
            }
        }
        /// <summary>
        /// Workflow coded path
        /// </summary>
        private string Xoml
        {
            get
            {
                string xoml = string.Empty;
                if (this.loader != null)
                {
                    try
                    {
                        this.loader.Flush();
                        xoml = this.loader.Xoml;
                    }
                    catch
                    {
                    }
                }
                return xoml;
            }

            set
            {
                try
                {
                    if (!String.IsNullOrEmpty(value))
                        LoadWorkflow(value);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Nombre del WorkFlow
        /// </summary>
        public string WorkflowName
        {
            get
            {
                return this.workflow == null ? string.Empty : this.workflow.Name;
            }
        }
        new public object GetService(Type serviceType)
        {
            return (this.workflowView != null) ? ((IServiceProvider)this.workflowView).GetService(serviceType) : null;
        }
        /// <summary>
        /// Contiene el numero de version
        /// </summary>
        public Int32 NumVersion = 0;
        #endregion

        #region  ProtectedMetodsAndEvents
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                UnloadWorkflow();
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Carga el workflow en el view
        /// </summary>
        /// <_param name="xoml"></_param>
        private void LoadWorkflow(string xoml)
        {
            try
            {
                SuspendLayout();

                DesignSurface designSurface = new DesignSurface();
                WorkflowLoader loader = new WorkflowLoader();
                loader.Xoml = xoml;
                designSurface.BeginLoad(loader);

                IDesignerHost designerHost = designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (designerHost != null && designerHost.RootComponent != null)
                {
                    IRootDesigner rootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as IRootDesigner;
                    if (rootDesigner != null)
                    {
                        UnloadWorkflow();

                        this.designSurface = designSurface;
                        this.loader = loader;
                        this.loader.OnRefreshWF += new RefreshWF(this.RefreshWF);
                        this.loader.OnOpenRegionWF += new OpenRegionWF(this.OpenRegionWF);
                        this.workflowView = rootDesigner.GetView(ViewTechnology.Default) as WorkflowView;
                        this.workflowViewSplitter.Panel2.Controls.Add(this.workflowView);
                        this.workflowView.Dock = DockStyle.Fill;
                        this.workflowView.TabIndex = 1;
                        this.workflowView.TabStop = true;
                        this.workflowView.HScrollBar.TabStop = false;
                        this.workflowView.VScrollBar.TabStop = false;
                        this.workflowView.Focus();
                        this.workflowView.DoubleClick += new EventHandler(OnDoubleClickView);
                    }
                }

                ResumeLayout(true);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Doble Clik en el visualizador de workflow
        /// </summary>
        /// <_param name="o"></_param>
        /// <_param name="e"></_param>
        private void OnDoubleClickView(object sender, EventArgs e)
        {

        }
        
        private void UnloadWorkflow()
        {
            try
            {
                IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (designerHost != null && designerHost.Container.Components.Count > 0)
                    WorkflowLoader.DestroyObjectGraphFromDesignerHost(designerHost, designerHost.RootComponent as Activity);

                if (this.designSurface != null)
                {
                    this.designSurface.Dispose();
                    this.designSurface = null;
                }

                if (this.workflowView != null)
                {
                    Controls.Remove(this.workflowView);
                    this.workflowView.Dispose();
                    this.workflowView = null;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Obtengo el workflow basico
        /// </summary>
        /// <_param name="stepId"></_param>
        private void LoadWorkflow(Int64 stepId)
        {
            try
            {
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                    {
                        WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                        ActivityBusiness.GetBasicWorkflow(workflow, stepId);
                        serializer.Serialize(xmlWriter, workflow);
                        this.Xoml = stringWriter.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Cargo el workflow de la etapa y la region indicada
        /// </summary>
        /// <_param name="stepId"></_param>
        /// <_param name="parentId"></_param>
        private void LoadWorkflow(Int64 stepId, Int64 parentId)
        {
            try
            {
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                    {
                        WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                        workflow = ActivityBusiness.GetWorkflow(stepId, parentId);
                        serializer.Serialize(xmlWriter, workflow);
                        this.Xoml = stringWriter.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Get the Current Workflow
        /// </summary>
        /// <returns>Current Workflow</returns>
        private WorkFlowModel getWF()
        {
            try
            {
                ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));

                if (selectionService != null)
                {
                    if (selectionService.PrimarySelection is Activity)
                    {
                        Activity activity = (Activity)selectionService.PrimarySelection;

                        while (activity.Name != this.WorkflowName)
                        {
                            activity = activity.Parent;
                        }
                        return (WorkFlowModel)activity;
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return null;
        }

        /// <summary>
        /// Save the Workflow in the DataBase
        /// </summary>
        //public void Save()
        //{
        //    try
        //    {
        //        WorkFlowModel wf = getWF();
        //        ActivityBusiness.SaveWorkFlow(wf);
        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);
        //    }
        //}

        /// <summary>
        /// Recarga el workFlow
        /// </summary>
        private void RefreshWF()
        {
            try
            {
                WorkFlowModel wf = getWF();
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                    {
                        WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                        serializer.Serialize(xmlWriter, wf);
                        this.Xoml = stringWriter.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void OpenRegionWF(Int64 StepId, Int64 ParentId)
        {
            if (this.dOpenRegionWF != null)
                this.dOpenRegionWF(StepId,ParentId);
        }

        /// <summary>
        /// Refresh workflow
        /// </summary>
        public event OpenRegionWF OnOpenRegionWF
        {
            add
            {
                this.dOpenRegionWF += value;
            }
            remove
            {
                this.dOpenRegionWF -= value;
            }
        }
        private OpenRegionWF dOpenRegionWF = null;

        /// <summary>
        /// Set the Zoom
        /// </summary>
        /// <_param name="zoomFactor"></_param>
        public void ProcessZoom(int zoomFactor)
        {
            try
            {
                this.workflowView.Zoom = zoomFactor;
                this.workflowView.Update();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Compila el Workflow
        /// </summary>
        /// <_param name="showMessage"></_param>
        /// <returns></returns>
        public bool Compile()
        {
            try
            {
                if (this.designSurface != null && this.loader != null)
                {
                    this.XomlFile = Membership.MembershipHelper.StartUpPath + "\\TempWorflow" + NumVersion + ".xoml";
                    this.loader.PerformFlush();
                    XmlDocument doc = new XmlDocument();
                    doc.Load(XomlFile);
                    XmlAttribute attrib = doc.CreateAttribute("x", "Class", "http://schemas.microsoft.com/winfx/2006/xaml");
                    attrib.Value = string.Format("{0}.{1}", this.GetType().Namespace, this.WorkflowName);
                    doc.DocumentElement.Attributes.Append(attrib);
                    doc.Save(XomlFile);
                }

                if (File.Exists(XomlFile))
                {
                    //MessageBox.Show(this, "Cannot locate xoml file: " + Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), XomlFile), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    bool compileOK = true;

                    // Compile the workflow
                    WorkflowCompiler compiler = new WorkflowCompiler();
                    WorkflowCompilerParameters parameters = new WorkflowCompilerParameters(assemblyNames);
                    parameters.LibraryPaths.Add(Membership.MembershipHelper.StartUpPath);
                    parameters.OutputAssembly = string.Format("{0}.dll", this.WorkflowName + NumVersion);
                    NumVersion += 1;

                    compilerResults = compiler.Compile(parameters, XomlFile);

                    StringBuilder errors = new StringBuilder();
                    foreach (CompilerError compilerError in compilerResults.Errors)
                    {
                        errors.Append(compilerError.ToString() + '\n');
                    }

                    if (errors.Length != 0)
                    {
                        MessageBox.Show(this, errors.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        compileOK = false;
                    }

                    File.Delete(XomlFile);
                    return compileOK;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }
        /// <summary>
        /// Run the WorkFlow
        /// </summary>
        /// <returns></returns>
        public void Run()
        {
            try
            {
                if (this.Compile() == true)
                {
                    // Start the runtime engine
                    if (this.workflowRuntime == null)
                    {
                        this.workflowRuntime = new WorkflowRuntime();
                    }

                    Dictionary<string, Object> pars = new Dictionary<string, Object>();
                    Zamba.Core.TaskResult o1 = new Zamba.Core.TaskResult();
                    o1.ExpireDate = new DateTime(2001, 01, 01);
                    Zamba.Core.TaskResult o2 = new Zamba.Core.TaskResult();
                    o2.ExpireDate = new DateTime(2007, 12, 01);
                    System.Collections.Generic.List<Zamba.Core.ITaskResult> o3 = new List<Zamba.Core.ITaskResult>();
                    o3.Add(o1);
                    o3.Add(o2);
                    pars.Add("Results", o3);

                    workflowRuntime.CreateWorkflow((compilerResults.CompiledAssembly.GetType(string.Format("{0}.{1}", this.GetType().Namespace, this.WorkflowName))), pars).Start();
                    workflowRuntime.StartRuntime();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        #endregion
    }
}