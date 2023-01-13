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
using Zamba.AppBlock;

namespace Zamba.WorkFlow.Designer
{
    /// <summary>
    /// Clase que es el WebBrowser de los Xoml's
    /// </summary>
   partial class WorkflowControl : ZControl, IDisposable, IServiceProvider
    {
        //Visualizador del workflow
        private WorkflowView            workflowView;
        private DesignSurface           designSurface;
        private WorkflowLoader          loader;
        SequentialWorkflowActivity workflow;

        #region  Contructors
        /// <summary>
        /// Create the control, load an workflow and set the controls to client or administrator
        /// </summary>
        /// <param name="path"></param>
        /// <param name="showAdmin"></param>
        public WorkflowControl(Boolean Enabled)
        {
            viewEnabled = Enabled;
            InitializeComponent();

            WorkflowTheme.CurrentTheme.ReadOnly = false;
            WorkflowTheme.CurrentTheme.AmbientTheme.ShowConfigErrors = false;
            WorkflowTheme.CurrentTheme.ReadOnly = true;

            Toolbox toolbox = new Toolbox(this);
            this.workflowViewSplitter.Panel1.Controls.Add(toolbox);
            toolbox.Dock = DockStyle.Fill;
            toolbox.BackColor = BackColor;
            toolbox.Font = WorkflowTheme.CurrentTheme.AmbientTheme.Font;
            
        }
        
        /// <summary>
        /// Create the control, load an workflow and set the controls to client or administrator
        /// </summary>
        /// <param name="path"></param>
        /// <param name="showAdmin"></param>
        public WorkflowControl(string path, Boolean Enabled)
        {
            viewEnabled = Enabled;
            InitializeComponent();

            WorkflowTheme.CurrentTheme.ReadOnly = false;
            WorkflowTheme.CurrentTheme.AmbientTheme.ShowConfigErrors = false;
            WorkflowTheme.CurrentTheme.ReadOnly = true;

            Toolbox toolbox = new Toolbox(this);
            this.workflowViewSplitter.Panel1.Controls.Add(toolbox);
            toolbox.Dock = DockStyle.Fill;
            toolbox.BackColor = BackColor;
            toolbox.Font = WorkflowTheme.CurrentTheme.AmbientTheme.Font;
            if (path != "")
                LoadExistingWorkflow(path);
        }

        public WorkflowControl()
        {
            InitializeComponent();

            WorkflowTheme.CurrentTheme.ReadOnly = false;
            WorkflowTheme.CurrentTheme.AmbientTheme.ShowConfigErrors = false;
            WorkflowTheme.CurrentTheme.ReadOnly = true;

            Toolbox toolbox = new Toolbox(this);
            this.workflowViewSplitter.Panel1.Controls.Add(toolbox);
            toolbox.Dock = DockStyle.Fill;
            toolbox.BackColor = BackColor;
            toolbox.Font = WorkflowTheme.CurrentTheme.AmbientTheme.Font;
        }

        #endregion

       private Boolean viewEnabled = true;

        public string XomlFile
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

        public string Xoml
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                UnloadWorkflow();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        new public object GetService(Type serviceType)
        {
            return (this.workflowView != null) ? ((IServiceProvider)this.workflowView).GetService(serviceType) : null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.Xoml == "")
                ShowDefaultWorkflow();
        }

        /// <summary>
        /// Carga el workflow en el view
        /// </summary>
        /// <param name="xoml"></param>
        private void LoadWorkflow(string xoml)
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
                    this.workflowView = rootDesigner.GetView(ViewTechnology.Default) as WorkflowView;
                    this.workflowViewSplitter.Panel2.Controls.Add(this.workflowView);
                    this.workflowView.Dock = DockStyle.Fill;
                    this.workflowView.TabIndex = 1;
                    this.workflowView.TabStop = true;
                    this.workflowView.HScrollBar.TabStop = false;
                    this.workflowView.VScrollBar.TabStop = false;
                    this.workflowView.Focus();
                    this.workflowView.Enabled = viewEnabled;
                       
                    ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;

                    if (selectionService != null)
                    {
                        selectionService.SelectionChanged += new EventHandler(OnSelectionChanged);
                    }
                }
            }

            ResumeLayout(true);
        }

        private void UnloadWorkflow()
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

        private void ShowDefaultWorkflow()
        {
            this.workflow = new SequentialWorkflowActivity();
            workflow.Name = "CustomWorkflow";
            this.LoadWorkflow();
        }

        private void LoadWorkflow()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                {
                    WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                    serializer.Serialize(xmlWriter, workflow);
                    this.Xoml = stringWriter.ToString();
                }
            }
        }
        
        /// <summary>
        /// Cambia el Zoom
        /// </summary>
        /// <param name="zoomFactor"></param>
        public void ProcessZoom(int zoomFactor)
        {
            this.workflowView.Zoom = zoomFactor;
            this.workflowView.Update();
        }
        
        private void OnSelectionChanged(object sender, EventArgs e)
        {
            ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
        }
        
        public void DeleteSelected()
        {
            ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));

            if (selectionService != null)
            {
                if (selectionService.PrimarySelection is Activity)
                {
                    Activity activity = (Activity)selectionService.PrimarySelection;

                    if (activity.Name != this.WorkflowName)
                    {
                        activity.Parent.Activities.Remove(activity);
                        this.workflowView.Update();
                    }
                }
            }
        }

       public void ChangeName()
       {
           ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));

           if (selectionService != null)
           {
               if (selectionService.PrimarySelection is Activity)
               {
                   Activity activity = (Activity)selectionService.PrimarySelection;
                   ChangeText nombre = new ChangeText(activity.Name);
                   nombre.ShowDialog();
                   if (nombre.name != "")
                      activity.Name = nombre.name;
                  this.workflowView.Update();
                  this.workflowView.Refresh();
               }
           }
       }

       public void GetAsociatedDocs()
       {
           ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));

           if (selectionService != null)
           {
               if (selectionService.PrimarySelection is Activity)
               {
                   Activity activity = (Activity)selectionService.PrimarySelection;
                 //  ChangeText nombre = new ChangeText(activity.Name);
                 //  nombre.ShowDialog();
                 //  if (nombre.name != "")
                 //      activity.Name = nombre.name;
               //    this.workflowView.Update();
               //    this.workflowView.Refresh();

                   try
                   {
                       if (this.dShowDocAsociated != null)
                           dShowDocAsociated(0);
                   }
                   catch 
                   { }
               }
           }
       }
        public delegate void DShowDocAsociated(int ID); 
       private DShowDocAsociated dShowDocAsociated;
    
       public event DShowDocAsociated  ShowDocAsociated
       {
           add
           {
            this.dShowDocAsociated += value;
           }
       remove{ this.dShowDocAsociated -= value;}
       }


        /// <summary>
        /// Ask and Load an existing Workflow 
        /// </summary>
        public void LoadExistingWorkflow()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xoml files (*.xoml)|*.xoml|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (XmlReader xmlReader = XmlReader.Create(openFileDialog.FileName))
                {
                    WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                    this.workflow = (SequentialWorkflowActivity)serializer.Deserialize(xmlReader);
                    this.LoadWorkflow();

                    this.XomlFile = openFileDialog.FileName;
                    this.Text = "Workflow: [" + openFileDialog.FileName + "]";
                }
            }
        }
        /// <summary>
        /// Load an existing Workflow
        /// </summary>
        /// <param name="path">Path of the workflow</param>
        public void LoadExistingWorkflow(String path)
        {
            using (XmlReader xmlReader = XmlReader.Create(path))
            {
                WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                this.workflow = (SequentialWorkflowActivity)serializer.Deserialize(xmlReader);
                this.LoadWorkflow();

                this.XomlFile = path;
                this.Text = "Workflow: [" + path + "]";
            }
        }
        /// <summary>
        /// Save the workflow to a Xoml file
        /// </summary>
        private void SaveFile()
        {
            if (this.XomlFile.Length != 0)
            {
                this.SaveExistingWorkflow(this.XomlFile);
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "xoml files (*.xoml)|*.xoml|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.SaveExistingWorkflow(saveFileDialog.FileName);
                    this.Text = "Workflow: [" + saveFileDialog.FileName + "]";
                }
            }
        }

       public void Export()
       {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                workflowView.SaveWorkflowImage(saveFileDialog.FileName,System.Drawing.Imaging.ImageFormat.Jpeg);
       }

       public void Print()
       {
           System.Drawing.Printing.PrintDocument doc = this.workflowView.PrintDocument;
           PrintDialog dlg = new PrintDialog();
           dlg.UseEXDialog = true;
           if (dlg.ShowDialog() == DialogResult.OK)
           {
               doc.PrinterSettings = dlg.PrinterSettings;
               doc.Print();
           }  
       }

        internal void SaveExistingWorkflow(string filePath)
        {
            if (this.designSurface != null && this.loader != null)
            {
                this.XomlFile = filePath;
                this.loader.PerformFlush();
            }
        }
        /// <summary>
        /// Save the Workflow
        /// </summary>
        /// <param name="showMessage">Show or not the filedialog</param>
        public void Save(bool showMessage)
        {
            try
            {
                // Save the workflow first, and capture the filePath of the workflow
                this.SaveFile();

                XmlDocument doc = new XmlDocument();
                doc.Load(this.XomlFile);
                XmlAttribute attrib = doc.CreateAttribute("x", "Class", "http://schemas.microsoft.com/winfx/2006/xaml");
                attrib.Value = string.Format("{0}.{1}", this.GetType().Namespace, this.WorkflowName);
                doc.DocumentElement.Attributes.Append(attrib);
                doc.Save(this.XomlFile);

                if (showMessage)
                {
                    MessageBox.Show(this, this.Text, "Workflow Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
