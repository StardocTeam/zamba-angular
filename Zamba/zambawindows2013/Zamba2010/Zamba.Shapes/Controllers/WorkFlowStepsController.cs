using System;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using System.Data;
using Diagram = Zamba.Shapes.Views.Diagram;
using System.Collections.Generic;
using System.Linq;

using ColumnStyle = MindFusion.Diagramming.ColumnStyle;
using MindFusion.Diagramming;
using MindFusion.Diagramming.WinForms;
using System.Data;
using Zamba.Core.Enumerators;


namespace Zamba.Shapes.Controllers
{
    class WorkFlowStepsController : IDiagramController, IDiagramFiltereable,IRefresh
    {
        private GenericShape _ShpRoot;
        private LayeredLayout _Layout = null;
        private DataTable _dtActors;
        private object[] _params;
        Diagram WFDiagram;

        public IDiagram GetDiagram(Object[] parameters)
        {
            _params = parameters;

            var wfShape = ((WFShape)parameters[0]);
            var wfSteps = WFStepBusiness.GetStepsByWorkflow((Int64)wfShape.WF.WorkId);

            //Diagrama donde se comienzan a agregar los nodos
            WFDiagram = new Diagram();
            WFDiagram.DiagramType = DiagramType.WorkflowSteps;

            StepShape shpWFs = null;
            IEnumerable<IUserGroup> actors;
            _dtActors = new WFStepBusinessExt().GetAllWFStepActors();

            //recorro los workflows
            foreach (var Step in wfSteps)
            {
                shpWFs = new StepShape(WFDiagram, (IWFStep)Step, null);

                actors = (from actorRow in _dtActors.Select("step_id=" + Step.ID)
                          select new UserGroup() { ID = long.Parse(actorRow["ID"].ToString()), Name = actorRow["NAME"].ToString(), Description = actorRow["DESCRIPCION"].ToString() }).OfType<IUserGroup>();

                //seteamos al WF los actores correspondientes
                ActorsController.SetChildActors(WFDiagram, actors, shpWFs);
            }

            WorkFlow WF = WFBusiness.GetWorkFlow(wfShape.WF.WorkId);
            WFBusiness.GetFullEditWF(WF);
            DibujarFlechaWF(WF);

            //Se organizan los objetos del diagrama
            SetLayout(WFDiagram);
            WFDiagram.DiagramActors = _dtActors;

            //Se devuelve el diagrama
            return WFDiagram;
        }


        /// <summary>
        /// Une todos los shapes de un WorkFlow con flechas
        /// </summary>
        /// <param name="Diagrama">Diagrama que contiene los shapes</param>
        public void DibujarFlechaWF(IWorkFlow wf)
        {
            try
            {
                GenericShape shape1 = null;
                GenericShape shape2 = null;

                int i = 0;

                                                 
                    //Obtengo un List con todas las conexiones de los shapes
                     ArrayList linklist = WfShapesBusiness.FillTransitions(wf);
              

                    if (linklist != null && linklist.Count>0)
                    {
                        i = 0;
                        for (i = 0; i < linklist.Count; ++i)
                        {
                            Int64 stepid1 = Int64.Parse(((String[])linklist[i])[0].ToString());
                            Int64 stepid2 = Int64.Parse(((String[])linklist[i])[1].ToString());

                            foreach (GenericShape gs in WFDiagram.Nodes)
                            {
                                if (gs is StepShape && ((StepShape)gs).Step.ID == stepid1) shape1 = gs;
                                if (gs is StepShape && ((StepShape)gs).Step.ID == stepid2) shape2 = gs;
                          }
                            if (shape1 != null && shape2 != null)
                                this.addlink(WFDiagram,shape1, shape2);
                        }
                    }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

        }

        private void addlink(MindFusion.Diagramming.Diagram diagram, GenericShape shape1, GenericShape shape2)
        {
                  
                DiagramLink link = new DiagramLink(diagram,shape1,shape2);
                link.DrawCrossings = false;
                link.AutoRoute = true;
                diagram.Links.Add(link);
          
        }


        private void SetLayout(MindFusion.Diagramming.Diagram diagram)
        {
            //Layout del arbol principal
            _Layout = new LayeredLayout();

            //Se actualiza el diseño
            _Layout.Arrange(diagram);

            //Dibuja el diagrama acomodando todo lo modificado
            diagram.ResizeToFitItems(25, false);
        }

        public IDiagram ApplyActorFilter(string actorName, DiagramType diagramType, object[] parameters)
        {
            Diagram diag = (Diagram)GetDiagram(parameters);

            if (!string.IsNullOrEmpty(actorName))
            {
                DataRow[] rowResults;

                foreach (ActorShape actor in diag.Nodes.OfType<ActorShape>())
                {
                    if (actor.Text != actorName)
                    {
                        actor.Visible = false;
                        foreach (MindFusion.Diagramming.DiagramLink item in actor.GetAllLinks())
                        {
                            item.Visible = false;
                        }

                        //Esto es para que tambien oculte el WF si no tiene actores(deberia ser por un check).
                        // if (childShape.Childs.Count == 0) childShape.Visible = false;
                    }
                }

                SetLayout(diag);
            }

            return diag;
        }
        public IDiagram FillDiagram(Object[] parameters, bool isRefresh)
        {
            _params = parameters;

            var _WFShape = ((WFShape)parameters[0]);
            var WFSteps = WFStepBusiness.GetStepsByWorkflow((Int64)_WFShape.WF.WorkId);

            //Diagrama donde se comienzan a agregar los nodos
            WFDiagram = new Diagram();
            WFDiagram.DiagramType = DiagramType.WorkflowSteps;

            StepShape shpWFs = null;
            IEnumerable<IUserGroup> actors;
            _dtActors = new WFStepBusinessExt().GetAllWFStepActors();

            //recorro los workflows
            foreach (var Step in WFSteps)
            {
                shpWFs = new StepShape(WFDiagram, (IWFStep)Step, null);

                actors = (from actorRow in _dtActors.Select("step_id=" + Step.ID)
                          select new UserGroup() { ID = long.Parse(actorRow["ID"].ToString()), Name = actorRow["NAME"].ToString(), Description = actorRow["DESCRIPCION"].ToString() }).OfType<IUserGroup>();

                //seteamos al WF los actores correspondientes
                ActorsController.SetChildActors(WFDiagram, actors, shpWFs);
            }

            WorkFlow WF = WFBusiness.GetWorkFlow(_WFShape.WF.WorkId);
            WFBusiness.GetFullEditWF(WF);
            DibujarFlechaWF(WF);

            //Se organizan los objetos del diagrama
            SetLayout(WFDiagram);
            WFDiagram.DiagramActors = _dtActors;

            //Se devuelve el diagrama
            return WFDiagram;

        }
        public IDiagram Refresh(Object[] parameters)
        {
            return FillDiagram(parameters, true);
        }
        private GenericShape AddRuleShape(GenericShape parentShape, WFRuleParent rule, StepShape stepshape, Boolean useTestCase)
        {
            try
            {

                GenericShape RuleShape;
                //= new RuleShape(WFDiagram, (IRule)rule, parentShape);

                if (rule.Enable == false)
                {
                    RuleShape = new RuleShape(WFDiagram, (IRule)rule, stepshape, parentShape, AttachToNode.TopLeft, useTestCase);

                }
                else if (rule.RuleClass.Contains("IfBranch"))
                {

                    RuleShape = new ConditionalRuleShape(WFDiagram, (IRule)rule, stepshape, parentShape, AttachToNode.TopLeft);

                }
                else if (rule.RuleClass.Contains("If"))
                {

                    RuleShape = new ConditionalRuleShape(WFDiagram, (IRule)rule, stepshape, parentShape, AttachToNode.TopLeft);

                }
                else if (rule.RuleClass.Contains("Design"))
                {
                    RuleShape = new RuleShape(WFDiagram, (IRule)rule, stepshape, parentShape, AttachToNode.TopLeft, useTestCase);

                }
                else if (rule.RefreshRule.Value)
                {
                    RuleShape = new RuleShape(WFDiagram, (IRule)rule, stepshape, parentShape, AttachToNode.TopLeft, useTestCase);

                }
                else
                {
                    RuleShape = new RuleShape(WFDiagram, (IRule)rule, stepshape, parentShape, AttachToNode.TopLeft, useTestCase);

                }
                if (RuleShape.Text != string.Empty)
                {
                    WFBusiness WFB = new WFBusiness();
                    RuleShape.Text = WFB.GetUserActionName(rule.ID, rule.WFStepId, rule.Name, true);
                    WFB = null;
                }
                else
                {
                    RuleShape.Text = WFRulesBusiness.GetRuleName(rule.ID);
                }


                return RuleShape;

                //Este diagrama no tiene que cargar las hijas, para eso esta el siguiente diagrama.
                //foreach (WFRuleParent R in rule.ChildRules)
                //    {
                //        AddRuleShape(RuleShape, R);
                //    }
            }

            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }
        private void SetLayout(MindFusion.Diagramming.Diagram diagram, GenericShape shpRoot)
        {
            //Layout del arbol principal
            _Layout = new LayeredLayout();

            //Se actualiza el diseño
            _Layout.Arrange(diagram);

            //Verifica si tiene nodos hijos
            if (shpRoot.Childs != null && shpRoot.Childs.Count > 0)
            {
                //Crea un segundo layout para los hijos
                TreeLayout treeLayout2;
                treeLayout2 = new TreeLayout(shpRoot,
                        TreeLayoutType.HorizontalVertical,
                        false,
                        TreeLayoutLinkType.Straight,
                        TreeLayoutDirections.LeftToRight,
                        30, 4, true, new SizeF(10, 10));

                //Aplica el layout
                for (int i = 0; i < shpRoot.Childs.Count; i++)
                {
                    treeLayout2.Root = shpRoot.Childs[i];
                    treeLayout2.Arrange(diagram);
                }
            }

            //Dibuja el diagrama acomodando todo lo modificado
            diagram.ResizeToFitItems(25, false);
        }
    }
}
