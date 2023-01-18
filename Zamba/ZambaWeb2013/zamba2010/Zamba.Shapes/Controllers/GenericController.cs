using System;
using System.Collections.Generic;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using Zamba.Shapes.Views;
using Diagram = Zamba.Shapes.Views.Diagram;
using System.Windows.Forms;
using MindFusion.Diagramming;

namespace Zamba.Shapes.Controllers
{
    //public static delegate void DOpenRuleDiagram(Int64 ruleId);

    class GenericController: IDiagramFiltereable
    {
        public delegate void OpenDiagram(IDiagram diagramToOpen, object[] parameters);
        public static event OpenDiagram NeedOpenDiagram;

        /// <summary>
        /// Obtiene el diagrama del tipo deseado
        /// </summary>
        /// <param name="type">Tipo de diagrama</param>
        /// <param name="param">Especifica que es lo que se va a mostrar</param>
        /// <returns></returns>
        public static IDiagram GetDiagram(DiagramType type, Object[] param = null)
        {
            IDiagram diagram = null;

            //Verifica que tipo de diagrama debe dibujar
            switch (type)
            {
                case DiagramType.Home:
                    HomeController homeController = new HomeController();
                    diagram = homeController.GetDiagram(param);
                    break;
                case DiagramType.Actors:
                    ActorsController actorController = new ActorsController();
                    diagram = actorController.GetDiagram(param);
                    break;
                case DiagramType.Entities:
                    //diagram = EntitiesController.GetDiagram(param);
                    break;
                case DiagramType.Environment:
                    //diagram = EnvironmentController.GetDiagram(param);
                    break;
                case DiagramType.Forms:
                    RuleFormDetailsController formDetails = new RuleFormDetailsController();
                    diagram = formDetails.GetDiagram(param);
                    break;
                case DiagramType.Insert:
                    //diagram = InsertController.GetDiagram(param);
                    break;
                case DiagramType.Search:
                    //diagram = SearchController.GetDiagram(param);
                    break;
                case DiagramType.SiteMap:
                    //diagram = HomeController.GetDiagram(param);
                    break;
                case DiagramType.StepActions:
                    StepActionsController stepActionsController = new StepActionsController();
                    diagram = stepActionsController.GetDiagram(param);
                    break;
                case DiagramType.Tasks:
                    TasksController tasksController = new TasksController();
                    diagram = tasksController.GetDiagram(param);
                    break;
                case DiagramType.WorkflowEntitiesRelations:
                    //diagram = WorkflowEntitiesRelations.GetDiagram(param);
                    break;
                case DiagramType.Workflows:
                    WorkFlowController workFlowController = new WorkFlowController();
                    diagram = workFlowController.GetDiagram(param);
                    break;
                case DiagramType.WorkflowSteps:
                    WorkFlowStepsController workFlowStepsController = new WorkFlowStepsController();
                    diagram = workFlowStepsController.GetDiagram(param);
                    break;
                case DiagramType.WorkFlowRules:
                    RulesController rulesController = new RulesController();
                    diagram = rulesController.GetDiagram(param);
                    break;
                case DiagramType.Reports:
                    ReportController reportController = new ReportController();
                    diagram = reportController.GetDiagram(param);
                    break;
                case DiagramType.Interfaces:
                    InterfaceController interfaceController = new InterfaceController();
                    diagram = interfaceController.GetDiagram(param);
                    break;
                case DiagramType.DocType:
                    DocTypesController docTypeController = new DocTypesController();
                    diagram = docTypeController.GetDiagram(param);
                    break;
                case DiagramType.DER:
                    DERController derController = new DERController();
                    diagram = derController.GetDiagram(param);
                    break;
            }

            if (diagram != null) diagram.DiagramType = type;

            return diagram;
        }

        public static IDiagram RefreshDiagram(DiagramType type, Object[] param = null)
        {
            IDiagram diagram = null;

            //Verifica que tipo de diagrama debe dibujar
            switch (type)
            {
                case DiagramType.Home:
                    HomeController homeController = new HomeController();
                    diagram = homeController.Refresh(param);
                    break;
                case DiagramType.Actors:
                    ActorsController actorController = new ActorsController();
                    diagram = actorController.Refresh(param);
                    break;
                case DiagramType.EntityForms:
                    EntityFormsController entityController = new EntityFormsController();
                    diagram = entityController.Refresh(param);
                    break;
                case DiagramType.Environment:
                    //diagram = EnvironmentController.GetDiagram(param);
                    break;
                case DiagramType.Forms:
                    RuleFormDetailsController formDetails = new RuleFormDetailsController();
                    diagram = formDetails.GetDiagram(param);
                    break;
                case DiagramType.Insert:
                    //diagram = InsertController.GetDiagram(param);
                    break;
                case DiagramType.Search:
                    //diagram = SearchController.GetDiagram(param);
                    break;
                case DiagramType.SiteMap:
                    //diagram = HomeController.GetDiagram(param);
                    break;
                case DiagramType.StepActions:
                    StepActionsController stepActionsController = new StepActionsController();
                    diagram = stepActionsController.Refresh(param);
                    break;
                case DiagramType.Tasks:
                    TasksController tasksController = new TasksController();
                    diagram = tasksController.Refresh(param);
                    break;
                case DiagramType.WorkflowEntitiesRelations:
                    //diagram = WorkflowEntitiesRelations.GetDiagram(param);
                    break;
                case DiagramType.Workflows:
                    WorkFlowController workFlowController = new WorkFlowController();
                    diagram = workFlowController.Refresh(param);
                    break;
                case DiagramType.WorkflowSteps:
                    WorkFlowStepsController workFlowStepsController = new WorkFlowStepsController();
                    diagram = workFlowStepsController.Refresh(param);
                    break;
                case DiagramType.WorkFlowRules:
                    RulesController rulesController = new RulesController();
                    diagram = rulesController.Refresh(param);
                    break;
                case DiagramType.Reports:
                    ReportController reportController = new ReportController();
                    diagram = reportController.Refresh(param);
                    break;
                case DiagramType.Interfaces:
                    InterfaceController interfaceController = new InterfaceController();
                    diagram = interfaceController.Refresh(param);
                    break;
                case DiagramType.DocType:
                    DocTypesController docTypeController = new DocTypesController();
                    diagram = docTypeController.Refresh(param);
                    break;
                case DiagramType.DER:
                    DERController derController = new DERController();
                    diagram = derController.Refresh(param);
                    break;
                
                
            }

            if (diagram != null) diagram.DiagramType = type;

            return diagram;
        }



        public static void DrawSectionsAndEntities(List<ISectionDiagram> lst, Diagram diagHome, GenericShape shpBusqueda)
        {
            if (lst != null)
            {
                int i;
                for (i = 0; i < lst.Count; i++)
                {
                    //Agrego las secciones que no tienen hijos
                    ISectionDiagram section = (ISectionDiagram)lst[i];
                    if (section.ChildSection.Count == 0)
                    {
                        SectionShape shpGroup = new SectionShape(diagHome, section, shpBusqueda);
                        //ArrayList lstEntities = EntitiesBusiness.GetEntitiesBySectionId(section.ID);
                         for (int a = 0; a < section.EntitiesSection.Count; a++)
                        {
                           IEntityDiagram entity = (IEntityDiagram)section.EntitiesSection[a];
                          EntityShape shpEntity = new EntityShape(diagHome, entity, shpGroup);
                         }
                    }
                    //Agrego las secciones con hijos
                    else
                    {
                        SectionShape shpGroupChilds = new SectionShape(diagHome, section, shpBusqueda);

                        DrawSectionsAndEntities(section.ChildSection, diagHome, shpGroupChilds);
                    }
                }
            }
        }
        public static void DrawAllEntities(ArrayList lst, Diagram diagHome, GenericShape shpInsertar)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                //Agrego las Entidades
                IEntityDiagram entity = (IEntityDiagram)lst[i];
                EntityShape shpEnt = new EntityShape(diagHome, entity, shpInsertar);
            };
        }
        public static void DrawAllWFs(ArrayList lst, Diagram diagHome, GenericShape shpTareas)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                //Agrego las Entidades
                IWF wf = (IWF)lst[i];
                WFShape shpWf = new WFShape(diagHome, wf, shpTareas);
            };
        }

        public static void DrawAllButtons(List<IButtonDiagram> lst, Diagram diagHome, GenericShape shpBtnDin) {
            if (lst != null)
            {
                int i;
                for (i = 0; i < lst.Count; i++)
                {
                    //Agrego las secciones que no tienen hijos
                    IButtonDiagram Button = (IButtonDiagram)lst[i];
                    ButtonShape shpbtn = new ButtonShape(diagHome, Button, shpBtnDin);                   
                }
            }

        }
        
        public IDiagram ApplyActorFilter(string actorName, DiagramType diagramType, object[] parameters)
        {
            IDiagram diagram = null;
            switch (diagramType)
            {
                case DiagramType.Actors:
                    break;
                case DiagramType.DiagramType:
                    break;
                case DiagramType.Entities:
                    break;
                case DiagramType.Environment:
                    break;
                case DiagramType.Forms:
                    break;
                case DiagramType.Home:
                    break;
                case DiagramType.Insert:
                    break;
                case DiagramType.Search:
                    break;
                case DiagramType.SiteMap:
                    break;
                case DiagramType.StepActions:
                    diagram = new StepActionsController().ApplyActorFilter(actorName, diagramType, parameters);
                    break;
                case DiagramType.Tasks:
                    break;
                case DiagramType.WorkFlowRules:
                    break;
                case DiagramType.WorkflowEntitiesRelations:
                    break;
                case DiagramType.WorkflowSteps:
                    diagram = new WorkFlowStepsController().ApplyActorFilter(actorName, diagramType, parameters);
                    break;
                case DiagramType.Workflows:
                    diagram = new WorkFlowController().ApplyActorFilter(actorName, diagramType, null);
                    break;
                default:
                    break;
            }
            if (diagram != null) diagram.DiagramType = diagramType;
            return diagram;
        }

        public IDiagram ApplyRuleCategoryFilter(int category, DiagramType diagramType, object[] parameters)
        {
            IDiagram diagram = null;
            switch (diagramType)
            {
                case DiagramType.WorkFlowRules:
                    diagram = new RulesController().ApplyCategoryRuleFilter(category, diagramType, parameters);
                    break;
                case DiagramType.StepActions:
                    diagram = new StepActionsController().ApplyCategoryRuleFilter(category, diagramType, parameters);
                    break;
                default:
                    break;
            }
            if (diagram != null) diagram.DiagramType = diagramType;
            return diagram;
        }

        /// <summary>
        /// Abre un menú contextual con las opciones implementadas por cada shape
        /// </summary>
        /// <param name="shpGeneric">Shape genérico</param>
        public static void OpenShapeContextMenu(DiagramNode shpGeneric, DOpenRuleDiagram openRuleDiagram, DiagramTab previousTab)
        {
            List<ToolStripItem> mnuItems = new List<ToolStripItem>();

            if (shpGeneric is GenericShape)
            {
                ShapeType shapeType = ((GenericShape)shpGeneric).ShapeType;

                if (shpGeneric is RuleShape || shpGeneric is ConditionalRuleShape || shpGeneric is InterfaceShape)
                {
                    ToolStripButton mnuProperties = new ToolStripButton("Propiedades", null, new System.EventHandler(PropertiesContextMenu_OnClick));
                    mnuProperties.Tag = openRuleDiagram;
                    mnuItems.Add(mnuProperties);

                    if (!(shpGeneric is ConditionalRuleShape))
                    {
                        ToolStripButton mnuUseCase = new ToolStripButton("Casos de Uso", null, new System.EventHandler(UseCaseContextMenu_OnClick));
                        mnuItems.Add(mnuUseCase);
                    }

                    if (shpGeneric is RuleShape)
                    {
                        String RuleClass = ((RuleShape)shpGeneric).Rule.RuleClass.ToLower();
                        if (RuleClass == "doshowform" || RuleClass == "doaddasociatedform")
                        {
                            ToolStripButton mnuForm = new ToolStripButton("Visualizar formulario", null, new System.EventHandler(FormContextMenu_OnClick));
                            mnuItems.Add(mnuForm);

                            ToolStripButton mnuDetailForm = new ToolStripButton("Detalles del formulario", null, new System.EventHandler(FormDetailsContextMenu_OnClick));
                            mnuDetailForm.Tag = previousTab;
                            mnuItems.Add(mnuDetailForm);
                        }
                    }
                }
                else if(shpGeneric is FormShape)
                {
                    ToolStripButton mnuForm = new ToolStripButton("Visualizar formulario", null, new System.EventHandler(FormContextMenu_OnClick));
                    mnuItems.Add(mnuForm);
                }
            }
            else if (shpGeneric is TableNode && ((IDiagram)shpGeneric.Parent).DiagramType != DiagramType.Forms)
            {
                ToolStripButton mnuForm = new ToolStripButton("Formularios asociados", null, new System.EventHandler(FormContextDocForms_OnClick));
                mnuForm.Tag = previousTab;
                mnuItems.Add(mnuForm);
            }

            if (mnuItems.Count > 0)
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip();
                contextMenu.Items.AddRange(mnuItems.ToArray());
                contextMenu.Tag = shpGeneric;
                contextMenu.Width = 150;
                contextMenu.Show(Control.MousePosition);
            }
        }

        public static void PropertiesContextMenu_OnClick(object sender, EventArgs e)
        {
            ToolStripButton button = (ToolStripButton)sender;
            OpenShapeProperties((GenericShape)(button).GetCurrentParent().Tag, (DOpenRuleDiagram)(button).Tag);
            button = null;
        }

        public static void UseCaseContextMenu_OnClick(object sender, EventArgs e)
        {
            ToolStripButton button = (ToolStripButton)sender;
            OpenUseCase((GenericShape)((ToolStripButton)sender).GetCurrentParent().Tag);
            button = null;
        }

        public static void FormContextMenu_OnClick(object sender, EventArgs e)
        {
            OpenFormVisualization((GenericShape)((ToolStripButton)sender).GetCurrentParent().Tag);
        }

        public static void FormDetailsContextMenu_OnClick(object sender, EventArgs e)
        {
            ToolStripButton button = (ToolStripButton)sender;

            //Otenemos la regla
            RuleShape ruleShape = (RuleShape)button.GetCurrentParent().Tag;

            IZwebForm form;
            //Evaluamos como obtener el formulario, segun si es doshowform o doaddasociatedform
            if (ruleShape.Rule.RuleClass.ToLower() == "doshowform")
            {
                form = FormBusiness.GetForm(((IDoShowForm)ruleShape.Rule).FormID);
            }
            else
            {
                form = FormBusiness.GetForm(((IDoAddAsociatedForm)ruleShape.Rule).FormID);
            }

            object[] parameters = new object[] { ruleShape, form };
            Diagram diagram = (Diagram)(new RuleFormDetailsController().GetDiagram(parameters));
            diagram.PreviousDiagramTab = (DiagramTab)button.Tag;

            //Elevamos el evento para generar la nueva tab
            NeedOpenDiagram(diagram, parameters);
        }

        public static void FormContextDocForms_OnClick(object sender, EventArgs e)
        {
            ToolStripButton button = (ToolStripButton)sender;
            TableNode dn = (TableNode)button.GetCurrentParent().Tag;
            int docTypeId = int.Parse(dn.Id.ToString());
            string docTypeName = dn.Caption;

            object[] parameters = new object[] { new DocType(docTypeId, docTypeName, -1) };
            Diagram diagram = (Diagram)(new EntityFormsController().GetDiagram(parameters));
            diagram.PreviousDiagramTab = (DiagramTab)button.Tag;

            NeedOpenDiagram(diagram, parameters);
        }

        /// <summary>
        /// Abre la solapa propiedades de un Shape determinado
        /// </summary>
        /// <param name="shpGeneric">Shape genérico</param>
        public static void OpenShapeProperties(GenericShape shpGeneric, DOpenRuleDiagram openRuleDiagram)
        {
            try
            {
                ShapeType shapeType = shpGeneric.ShapeType;

                if (shpGeneric is RuleShape || shpGeneric is ConditionalRuleShape || shpGeneric is InterfaceShape)
                {
                    WFRuleParent rule;
                    if (shpGeneric is RuleShape)
                        rule = (WFRuleParent)((RuleShape)shpGeneric).Rule;
                    else if (shpGeneric is ConditionalRuleShape)
                        rule = (WFRuleParent)((ConditionalRuleShape)shpGeneric).Rule;
                    else
                        rule = (WFRuleParent)WFRulesBusiness.GetInstanceRuleById(Int64.Parse(shpGeneric.Id.ToString()), false);

                    Views.FrmRuleProperties frmRuleProperties = new Views.FrmRuleProperties(ref rule, new DOpenRuleDiagram(openRuleDiagram));
                    if (frmRuleProperties.InitializationError)
                        frmRuleProperties.Dispose();
                    else
                        frmRuleProperties.Show();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Muestra los casos de uso de una regla determinada
        /// </summary>
        /// <param name="shpGeneric">Shape genérico</param>
        public static void OpenUseCase(GenericShape shpGeneric)
        {
            try
            {
                if (shpGeneric is RuleShape)
                {
                    IRule rule = ((RuleShape)shpGeneric).Rule;

                    Form frm = new Form();
                    UCUseCase uc = null;

                    uc = new UCUseCase(rule);

                    uc.Dock = DockStyle.Fill;
                    frm.Name = "Casos de Uso";
                    frm.Text = "Caso de Uso: " + rule.Name;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Controls.Add(uc);

                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Previsualiza un formulario de un Shape determinado
        /// </summary>
        /// <param name="shpGeneric">Shape genérico</param>
        public static void OpenFormVisualization(GenericShape shpGeneric)
        {
            if (shpGeneric is RuleShape || shpGeneric is FormShape)
            {
                Form frm = new Form();
                UCForm uc = null;

                try
                {
                    if (shpGeneric is RuleShape)
                    {
                        IRule rule = ((RuleShape)shpGeneric).Rule;

                        if (rule.RuleClass.ToLower() == "doshowform")
                        {
                            uc = new UCForm(((IDoShowForm)rule).FormID);
                        }
                        else if (rule.RuleClass.ToLower() == "doaddasociatedform")
                        {
                            uc = new UCForm(((IDoAddAsociatedForm)rule).FormID);
                        }
                    }
                    else if (shpGeneric is FormShape)
                    {
                        uc = new UCForm(((FormShape)shpGeneric).ZForm.ID);
                    }

                    uc.Dock = DockStyle.Fill;
                    frm.Name = "Previsualizacion del Formulario";
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Controls.Add(uc);
                    frm.ShowDialog();
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
                finally
                {
                    if (uc != null)
                    {
                        uc.Dispose();
                        uc = null;
                    }
                    frm.Dispose();
                    frm = null;
                }
            }
        }

        /// <summary>
        /// Genera la reglacion entre dos nodos
        /// </summary>
        /// <param name="diag"></param>
        /// <param name="shpOrigin"></param>
        /// <param name="shpDestiny"></param>
        internal void SetRelation(Diagram diag, DiagramNode shpOrigin, DiagramNode shpDestiny)
        {
            DiagramLink link = new DiagramLink(diag, shpOrigin, shpDestiny);
            link.DrawCrossings = false;

            link.AutoRoute = true;
            diag.Links.Add(link);
        }
    }
    
}
