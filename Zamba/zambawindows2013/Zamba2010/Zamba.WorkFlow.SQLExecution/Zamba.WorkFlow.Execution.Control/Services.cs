using Zamba.Core.Enumerators;
using Zamba.WorkFlow.Execution.Control.Properties;

namespace Zamba.WorkFlow.Execution.Control
{
	using System;
	using System.IO;
	using System.Text;
	using System.CodeDom;
	using System.CodeDom.Compiler;
	using System.Collections;
	using System.ComponentModel;
	using System.Collections.Generic;
	using System.ComponentModel.Design;
	using System.ComponentModel.Design.Serialization;
	using System.Workflow.ComponentModel.Compiler;
	using System.Workflow.ComponentModel;
	using System.Workflow.ComponentModel.Design;
    using System.Windows.Forms;
    using System.Drawing;
    using Zamba.WFActivity.Xoml;
    using Zamba.Core;
    using Zamba.CommonLibrary;

    #region IdentifierCreationService
    internal sealed class IdentifierCreationService : IIdentifierCreationService
	{
		private IServiceProvider serviceProvider = null;

		internal IdentifierCreationService(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		void IIdentifierCreationService.ValidateIdentifier(Activity activity, string identifier)
		{
            try
            {
                if (identifier == null)
                    throw new ArgumentNullException("identifier");
                if (activity == null)
                    throw new ArgumentNullException("activity");

                if (activity.Name.ToLower().Equals(identifier.ToLower()))
                    return;

                ArrayList identifiers = new ArrayList();
                Activity rootActivity = GetRootActivity(activity);
                identifiers.AddRange(GetIdentifiersInCompositeActivity(rootActivity as CompositeActivity));
                identifiers.Sort();
                if (identifiers.BinarySearch(identifier.ToLower(), StringComparer.OrdinalIgnoreCase) >= 0)
                    throw new ArgumentException(string.Format("Duplicate Component Identifier {0}", identifier));
            }
            catch
            {
                MessageBox.Show("Identificador duplicado");
            }
		}

		void IIdentifierCreationService.EnsureUniqueIdentifiers(CompositeActivity parentActivity, ICollection childActivities)
		{
			if (parentActivity == null)
				throw new ArgumentNullException("parentActivity");
			if (childActivities == null)
				throw new ArgumentNullException("childActivities");

			ArrayList allActivities = new ArrayList();

			Queue activities = new Queue(childActivities);
			while (activities.Count > 0)
			{
				Activity activity = (Activity)activities.Dequeue();
				if (activity is CompositeActivity)
				{
					foreach (Activity child in ((CompositeActivity)activity).Activities)
						activities.Enqueue(child);
				}

				//If we are moving activities, we need not regenerate their identifiers
				if (((IComponent)activity).Site != null)
					continue;

				allActivities.Add(activity);
			}

			// get the root activity
			CompositeActivity rootActivity = GetRootActivity(parentActivity) as CompositeActivity;
			ArrayList identifiers = new ArrayList(); // all the identifiers in the workflow
			identifiers.AddRange(GetIdentifiersInCompositeActivity(rootActivity));

			foreach (Activity activity in allActivities)
			{
				string finalIdentifier = activity.Name;

                // now loop until we find a identifier that hasn't been used.
				string baseIdentifier = GetBaseIdentifier(activity);
				int index = 0;

				identifiers.Sort();
				while (finalIdentifier == null || finalIdentifier.Length == 0 || identifiers.BinarySearch(finalIdentifier.ToLower(), StringComparer.OrdinalIgnoreCase) >= 0)
				{
					finalIdentifier = string.Format("{0}{1}", baseIdentifier, ++index);
				}

				// add new identifier to collection 
				identifiers.Add(finalIdentifier);
				activity.Name = finalIdentifier;
			}
		}

        private static IList GetIdentifiersInCompositeActivity(CompositeActivity compositeActivity)
        {
            ArrayList identifiers = new ArrayList();
            if (compositeActivity != null)
            {
                identifiers.Add(compositeActivity.Name);
                IList<Activity> allChildren = GetAllNestedActivities(compositeActivity);
                foreach (Activity activity in allChildren)
                    identifiers.Add(activity.Name);
            }
            return ArrayList.ReadOnly(identifiers);
        }

        private static string GetBaseIdentifier(Activity activity)
        {
            string baseIdentifier = activity.GetType().Name;
            StringBuilder b = new StringBuilder(baseIdentifier.Length);
            for (int i = 0; i < baseIdentifier.Length; i++)
            {
                if (Char.IsUpper(baseIdentifier[i]) && (i == 0 || i == baseIdentifier.Length - 1 || Char.IsUpper(baseIdentifier[i + 1])))
                {
                    b.Append(Char.ToLower(baseIdentifier[i]));
                }
                else
                {
                    b.Append(baseIdentifier.Substring(i));
                    break;
                }
            }
            return b.ToString();
        }

        private static Activity GetRootActivity(Activity activity)
        {
            if (activity == null)
                throw new ArgumentException("activity");

            while (activity.Parent != null)
                activity = activity.Parent;

            return activity;
        }

        private static Activity[] GetAllNestedActivities(CompositeActivity compositeActivity)
        {
            if (compositeActivity == null)
                throw new ArgumentNullException("compositeActivity");

            ArrayList nestedActivities = new ArrayList();
            Queue compositeActivities = new Queue();
            compositeActivities.Enqueue(compositeActivity);
            while (compositeActivities.Count > 0)
            {
                CompositeActivity compositeActivity2 = (CompositeActivity)compositeActivities.Dequeue();

                foreach (Activity activity in compositeActivity2.Activities)
                {
                    nestedActivities.Add(activity);
                    if (activity is CompositeActivity)
                        compositeActivities.Enqueue(activity);
                }

                foreach (Activity activity in compositeActivity2.EnabledActivities)
                {
                    if (!nestedActivities.Contains(activity))
                    {
                        nestedActivities.Add(activity);
                        if (activity is CompositeActivity)
                            compositeActivities.Enqueue(activity);
                    }
                }
            }
            return (Activity[])nestedActivities.ToArray(typeof(Activity));
        }
    }
    #endregion

    #region WorkflowCompilerOptionsService
    internal class WorkflowCompilerOptionsService : IWorkflowCompilerOptionsService
    {
        public WorkflowCompilerOptionsService()
        {
        }

        string IWorkflowCompilerOptionsService.RootNamespace
        {
            get
            {
                return String.Empty;
            }
        }

        string IWorkflowCompilerOptionsService.Language
        {
            get
            {
                return "CSharp";
            }
        }

        #region IWorkflowCompilerOptionsService Members

        public bool CheckTypes
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
    #endregion

    #region Class EventBindingService
    internal class EventBindingService : IEventBindingService
    {
        public EventBindingService()
        {
        }

        public string CreateUniqueMethodName(IComponent component, EventDescriptor e)
        {
            return e.DisplayName;
        }

        public ICollection GetCompatibleMethods(EventDescriptor e)
        {
            return new ArrayList();
        }

        public EventDescriptor GetEvent(PropertyDescriptor property)
        {
            return (property is EventPropertyDescriptor) ? ((EventPropertyDescriptor)property).EventDescriptor : null;
        }

        public PropertyDescriptorCollection GetEventProperties(EventDescriptorCollection events)
        {
            return new PropertyDescriptorCollection(new PropertyDescriptor[] { }, true);
        }

        public PropertyDescriptor GetEventProperty(EventDescriptor e)
        {
            return new EventPropertyDescriptor(e);
        }

        public bool ShowCode()
        {
            return false;
        }

        public bool ShowCode(int lineNumber)
        {
            return false;
        }

        public bool ShowCode(IComponent component, EventDescriptor e)
        {
            return false;
        }

        private class EventPropertyDescriptor : PropertyDescriptor
        {
            private EventDescriptor eventDescriptor;

            public EventDescriptor EventDescriptor
            {
                get
                {
                    return this.eventDescriptor;
                }
            }

            public EventPropertyDescriptor(EventDescriptor eventDescriptor)
                : base(eventDescriptor, null)
            {
                this.eventDescriptor = eventDescriptor;
            }

            public override Type ComponentType
            {
                get
                {
                    return this.eventDescriptor.ComponentType;
                }
            }
            public override Type PropertyType
            {
                get
                {
                    return this.eventDescriptor.EventType;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override object GetValue(object component)
            {
                return null;
            }

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object component, object value)
            {
            }

            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }
        }
    }
    #endregion

    #region Class MenuCommandService
    internal sealed class WorkflowMenuCommandService : MenuCommandService
    {
        public WorkflowMenuCommandService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        /// <summary>
        /// Muestra el contextMenu
        /// </summary>
        /// <_param name="menuID"></_param>
        /// <_param name="x"></_param>
        /// <_param name="y"></_param>
        public override void ShowContextMenu(CommandID menuID, int x, int y)
        {
            try
            {
                if (menuID == WorkflowMenuCommands.SelectionMenu)
                {
                    ContextMenu contextMenu = new ContextMenu();

                    MenuItem[] items = GetSelectionMenuItems();
                    if (items.Length > 0)
                    {
                        foreach (MenuItem item in items)
                            contextMenu.MenuItems.Add(item);
                    }

                    WorkflowView workflowView = GetService(typeof(WorkflowView)) as WorkflowView;
                    if (workflowView != null)
                        contextMenu.Show(workflowView, workflowView.PointToClient(new Point(x, y)));
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Cuando un elemento del contextmenu es clickeado
        /// </summary>
        /// <_param name="sender"></_param>
        /// <_param name="e"></_param>
        private void OnMenuClicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem menuItem = sender as MenuItem;
                if (menuItem != null && menuItem.Tag is MenuCommand)
                {
                    MenuCommand command = menuItem.Tag as MenuCommand;
                    Boolean bol = false;

                    ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (selectionService != null)
                    {
                        foreach (object obj in selectionService.GetSelectedComponents())
                        {
                            if (obj is IResultActivity)
                            {
                                //Agregar accion usuario
                                if (command.CommandID.ID == WorkflowMenuCommands.ZoomOut.ID)
                                {
                                    ParallelBranch userAccion = new ParallelBranch();
                                    userAccion.Name = "Accion de Usuario " + ((CompositeActivity)obj).Activities.Count.ToString();
                                    userAccion.RuleType = TypesofRules.AccionUsuario;
                                    ((CompositeActivity)obj).Activities.Add(userAccion);
                                    if (dRefreshWF != null)
                                        dRefreshWF();
                                    bol = true;
                                    break;
                                }
                                else if (command.CommandID.ID == WorkflowMenuCommands.Zoom75Mode.ID)
                                {
                                    if (dOpenRegionWF != null)
                                        dOpenRegionWF(((IResultActivity)obj).WFStepId, (Int64)((IResultActivity)obj).RuleType);
                                    bol = true;
                                    break;
                                }
                                //Cambiar nombre
                                else if (command.CommandID.ID == WorkflowMenuCommands.Zoom50Mode.ID)
                                {
                                    InputBox Dialogo = new InputBox("Asigne un nombre a la regla", "Nombre:", ((IResultActivity)obj).Name.Split(char.Parse("("))[0]);

                                    Dialogo.ShowDialog();
                                    if (string.Compare(Dialogo.Texto, string.Empty) != 0)
                                    {
                                        WFRulesBusiness.UpdateRuleNameByID(((IResultActivity)obj).ruleId, Dialogo.Texto);
                                        ((IResultActivity)obj).Name = Dialogo.Texto + " (" + ((IResultActivity)obj).ruleId + ")";
                                    }
                                    Dialogo.Dispose();
                                    if (dRefreshWF != null)
                                        dRefreshWF();
                                    bol = true;
                                    break;
                                }
                                //Si no es del tipo rule no se la puede eliminar
                                else if (!((TypesofRules)((IResultActivity)obj).RuleType == TypesofRules.Regla))
                                {
                                    if ((TypesofRules)((IResultActivity)obj).RuleType == TypesofRules.AccionUsuario)
                                    {
                                        if (System.Windows.Forms.MessageBox.Show(Resources.WorkflowMenuCommandService_OnMenuClicked_Esta_seguro_que_desea_eliminar_la_regla, Resources.WorkflowMenuCommandService_OnMenuClicked_Eliminar_Regla, MessageBoxButtons.OKCancel) == DialogResult.OK)
                                        {
                                            WFRulesBusiness.DeleteRuleByID(((IResultActivity)((CompositeActivity)obj).Activities[0]).ruleId);
                                            int count = ((CompositeActivity)obj).Activities.Count;
                                            ((CompositeActivity)obj).Parent.Activities.Remove((Activity)obj);
                                        }

                                    }
                                }
                                //Mostrar diseñador
                                else if (command.CommandID.ID == WorkflowMenuCommands.ZoomIn.ID)
                                {
                                    Zamba.WorkFlow.Execution.Control.RulesDesigner design = new Zamba.WorkFlow.Execution.Control.RulesDesigner((IResultActivity)obj);
                                    if (design.isDesigned == true)
                                        design.ShowDialog();
                                    design.Dispose();
                                    bol = true;
                                }
                                else if (command.CommandID.ID == WorkflowMenuCommands.Delete.ID)
                                {
                                    if (System.Windows.Forms.MessageBox.Show(Resources.WorkflowMenuCommandService_OnMenuClicked_Esta_seguro_que_desea_eliminar_la_regla, Resources.WorkflowMenuCommandService_OnMenuClicked_Eliminar_Regla, MessageBoxButtons.OKCancel) == DialogResult.OK)
                                    {
                                        WFRulesBusiness.DeleteRuleByID(((IResultActivity)obj).ruleId);
                                    }
                                    else
                                    {
                                        bol = true;
                                    }
                                }
                            }
                        }

                    }
                    if (bol == false)
                        command.Invoke();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
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

        /// <summary>
        /// Open Region
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
        private RefreshWF dRefreshWF = null;
        private OpenRegionWF dOpenRegionWF = null;


        /// <summary>
        /// Creo los item personalizados del contextmenu
        /// </summary>
        /// <returns></returns>
        private MenuItem[] GetSelectionMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            try
            {

                Dictionary<CommandID, string> selectionCommands = new Dictionary<CommandID, string>();
                ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                
                if (selectionService != null)
                {
                    foreach (object obj in selectionService.GetSelectedComponents())
                    {
                        if (obj is IResultActivity)
                        {
                                //Todo Crear una clase que herede de WorkflowMenuCommands
                                //Agrego los controles del contextmenu de cada regla
                                if ((TypesofRules)((IResultActivity)obj).RuleType == TypesofRules.Regla)
                                {
                                    selectionCommands.Add(WorkflowMenuCommands.Delete, "Borrar Regla");
                                    selectionCommands.Add(WorkflowMenuCommands.ZoomIn, "Diseñar Regla");
                                    selectionCommands.Add(WorkflowMenuCommands.Zoom50Mode, "Cambiar Nombre");
                                }
                                else if (obj is Parallel)
                                    selectionCommands.Add(WorkflowMenuCommands.ZoomOut, "Agregar Accion de Usuario");
                                else if ((TypesofRules)((IResultActivity)obj).RuleType != TypesofRules.Regla)
                                {
                                    if (obj is ParallelBranch)
                                        selectionCommands.Add(WorkflowMenuCommands.Delete, "Borrar Accion de Usuario");
                                    else
                                        selectionCommands.Add(WorkflowMenuCommands.Zoom75Mode, "Diseñar Region");
                                }
                                break;
                        }
                        else
                        {
                            selectionCommands.Add(WorkflowMenuCommands.Print, "Imprimir WorkFlow");
                            selectionCommands.Add(WorkflowMenuCommands.SaveAsImage, "Exportar WorkFlow a Imagen");
                        }
                    }
                }

                foreach (CommandID id in selectionCommands.Keys)
                {
                    MenuCommand command = FindCommand(id);
                    if (command != null)
                    {
                        MenuItem menuItem = new MenuItem(selectionCommands[id], new EventHandler(OnMenuClicked));
                        menuItem.Tag = command;
                        menuItems.Add(menuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return menuItems.ToArray();
        }
    }
    #endregion

    public delegate void RefreshWF();
    public delegate void OpenRegionWF(Int64 stepId,Int64 parentTypeId);
}