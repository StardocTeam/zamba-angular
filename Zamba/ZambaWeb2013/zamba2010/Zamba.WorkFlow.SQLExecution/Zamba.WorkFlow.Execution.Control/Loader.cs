namespace Zamba.WorkFlow.Execution.Control
{
	using System;
	using System.IO;
	using System.ComponentModel;
	using System.ComponentModel.Design;
	using System.ComponentModel.Design.Serialization;
	using System.Workflow.ComponentModel;
    using System.Workflow.ComponentModel.Compiler;
    using System.Workflow.ComponentModel.Design;
    using System.Workflow.ComponentModel.Serialization;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing.Design;
    using System.Xml;
    using Zamba.Core;

    #region WorkflowLoader
    internal sealed class WorkflowLoader : WorkflowDesignerLoader
    {
        private string xoml = string.Empty;

        internal WorkflowLoader()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            try
            {
                IDesignerLoaderHost host = LoaderHost;
                if (host != null)
                {
                    host.RemoveService(typeof(IIdentifierCreationService));
                    host.AddService(typeof(IIdentifierCreationService), new IdentifierCreationService(host));

                    //Contextmenu
                    WorkflowMenuCommandService menu = new WorkflowMenuCommandService(host);
                    menu.OnRefreshWF += new RefreshWF(this.RefreshWF);
                    menu.OnOpenRegionWF += new OpenRegionWF(this.OpenRegionWF);
                    host.AddService(typeof(IMenuCommandService), menu);

                    host.AddService(typeof(IToolboxService), new Toolbox(host));
                    TypeProvider typeProvider = new TypeProvider(host);
                    typeProvider.AddAssemblyReference(typeof(string).Assembly.Location);
                    host.AddService(typeof(ITypeProvider), typeProvider, true);
                    //host.AddService(typeof(IWorkflowCompilerOptionsService), new WorkflowCompilerOptionsService());
                    host.AddService(typeof(IEventBindingService), new EventBindingService());
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void RefreshWF()
        {
            if (this.dRefreshWF != null)
                this.dRefreshWF();
        }

        /// <summary>
        /// Refresh workflow
        /// </summary>
        public event RefreshWF OnRefreshWF
        {
            add
            {
                this.dRefreshWF += value;
            }
            remove
            {
                this.dRefreshWF -= value;
            }
        }
        private RefreshWF dRefreshWF = null;

        private void OpenRegionWF(Int64 stepId, Int64 parentId)
        {
            if (this.dOpenRegionWF != null)
                this.dOpenRegionWF(stepId, parentId);
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

        public override void Dispose()
        {
            IDesignerLoaderHost host = LoaderHost;
            if (host != null)
            {
                host.RemoveService(typeof(IIdentifierCreationService));
                host.RemoveService(typeof(IMenuCommandService));
                host.RemoveService(typeof(IToolboxService));
                host.RemoveService(typeof(ITypeProvider), true);
                host.RemoveService(typeof(IWorkflowCompilerOptionsService));
                host.RemoveService(typeof(IEventBindingService));
            }

            base.Dispose();
        }

        public override void ForceReload()
        {
        }

        public string DefaultNamespace
        {
            get
            {
                return "SampleNamespace";
            }
        }

        public string Xoml
        {
            get
            {
                return this.xoml;
            }

            set
            {
                this.xoml = value;
            }
        }

        private Type workflowType = null;

        public Type WorkflowType
        {
            get { return workflowType; }
            set { workflowType = value; }
        }

        private string xomlFile = string.Empty;

        public string XomlFile
        {
            get
            {
                return this.xomlFile;
            }
            set
            {
                this.xomlFile = value;
            }
        }

        public override string FileName
        {
            get { return string.Empty; }
        }

        public override TextReader GetFileReader(string filePath)
        {
            return new StreamReader(new FileStream(filePath, FileMode.OpenOrCreate));
        }

        public override TextWriter GetFileWriter(string filePath)
        {
            // clsoe the handle
            return new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate));
        }

        protected override void PerformLoad(IDesignerSerializationManager serializationManager)
        {
            try
            {
                IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
                Activity rootActivity = null;
                //First see if we have a workflow type
                if (WorkflowType != null)
                {
                    rootActivity = (Activity)Activator.CreateInstance(WorkflowType);
                }
                else
                {
                    // Create a text reader out of the doc data, and ask
                    TextReader reader = new StringReader(this.xoml);
                    try
                    {
                        using (XmlReader xmlReader = XmlReader.Create(reader))
                        {
                            WorkflowMarkupSerializer xomlSerializer = new WorkflowMarkupSerializer();
                            rootActivity = xomlSerializer.Deserialize(xmlReader) as Activity;
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }

                if (rootActivity != null && designerHost != null)
                {
                    AddObjectGraphToDesignerHost(designerHost, rootActivity);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        protected override void PerformFlush(IDesignerSerializationManager manager)
        {
        }

        public void PerformFlush()
        {
            try
            {
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                if (host != null && host.RootComponent != null)
                {
                    Activity service = host.RootComponent as Activity;

                    if (service != null)
                    {
                        using (XmlWriter writer = XmlWriter.Create(this.xomlFile))
                        {
                            WorkflowMarkupSerializer xomlSerializer = new WorkflowMarkupSerializer();
                            xomlSerializer.Serialize(writer, service);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private static void AddObjectGraphToDesignerHost(IDesignerHost designerHost, Activity activity)
        {
            try
            {
                Guid Definitions_Class = new Guid("3FA84B23-B15B-4161-8EB8-37A54EFEEFC7");

                if (designerHost == null)
                    throw new ArgumentNullException("designerHost");
                if (activity == null)
                    throw new ArgumentNullException("activity");

                string rootSiteName = activity.QualifiedName;
                if (activity.Parent == null)
                {
                    string fullClassName = activity.UserData[Definitions_Class] as string;
                    if (fullClassName == null)
                        fullClassName = activity.GetType().FullName;
                    rootSiteName = (fullClassName.LastIndexOf('.') != -1) ? fullClassName.Substring(fullClassName.LastIndexOf('.') + 1) : fullClassName;
                    designerHost.Container.Add(activity, rootSiteName);
                }
                else
                {
                    designerHost.Container.Add(activity, activity.QualifiedName);
                }

                if (activity is CompositeActivity)
                {
                    foreach (Activity activity2 in GetNestedActivities(activity as CompositeActivity))
                        designerHost.Container.Add(activity2, activity2.QualifiedName);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private static Activity[] GetNestedActivities(CompositeActivity compositeActivity)
        {
            ArrayList nestedActivities = new ArrayList();
                
            try
            {
                if (compositeActivity == null)
                    throw new ArgumentNullException("compositeActivity");

                IList<Activity> childActivities = null;
                Queue compositeActivities = new Queue();
                compositeActivities.Enqueue(compositeActivity);
                while (compositeActivities.Count > 0)
                {
                    CompositeActivity compositeActivity2 = (CompositeActivity)compositeActivities.Dequeue();
                    childActivities = compositeActivity2.Activities;

                    foreach (Activity activity in childActivities)
                    {
                        nestedActivities.Add(activity);
                        if (activity is CompositeActivity)
                            compositeActivities.Enqueue(activity);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return (Activity[])nestedActivities.ToArray(typeof(Activity));
        }

        internal static void DestroyObjectGraphFromDesignerHost(IDesignerHost designerHost, Activity activity)
        {
            try
            {
                if (designerHost == null)
                    throw new ArgumentNullException("designerHost");
                if (activity == null)
                    throw new ArgumentNullException("activity");

                designerHost.DestroyComponent(activity);

                if (activity is CompositeActivity)
                {
                    foreach (Activity activity2 in GetNestedActivities(activity as CompositeActivity))
                        designerHost.DestroyComponent(activity2);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }   
        }
    }
    #endregion
}
