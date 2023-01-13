using System;
using System.Collections.Generic;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using Diagram = Zamba.Shapes.Views.Diagram;
using System.Data;
using System.Linq;
using MindFusion.Diagramming;

namespace Zamba.Shapes.Controllers
{
    class WorkFlowController : IDiagramController, IDiagramFiltereable,IRefresh
    {
        private GenericShape _ShpRoot;
        DataTable _dtActors;
        Diagram WFDiagram;
        private object[] _params;

        private FlowchartLayout _Layout = null;

        public IDiagram GetDiagram(Object[] parameters)
        {
            ////Se crea el Diagrama principal
            Diagram diagWorkFlow = new Diagram();

            //ML Este podria ser el Nodo Proyecto.

            ////Se crea un objeto ROOT para comenzar el diagrama
            ZCoreView rootObject = new ZCoreView(0, "Zamba");
            diagWorkFlow.DiagramType = DiagramType.Workflows;
            ////Se crea el nodo root (shape)
            _ShpRoot = new GenericShape(diagWorkFlow, rootObject);
            _ShpRoot.IsRoot = true;
            //Se Obtienen los WFs a Utilizar
            ArrayList lstWF = WFlowDiagramBusiness.GetAllWFs();
            if (lstWF.Count == 0)
            {
                ZCoreView rootObject2 = new ZCoreView(0, "El diagrama seleccionado no tiene nodos");
                GenericShape shpRoot2 = new GenericShape(diagWorkFlow, rootObject2);
                shpRoot2.Transparent = true;
                shpRoot2.Expandable = false;
                shpRoot2.TextPadding = new MindFusion.Diagramming.Thickness(60f, 1f, 1f, 1f);
                return diagWorkFlow;
            }
            GenericController.DrawAllWFs(lstWF, diagWorkFlow, _ShpRoot);

            //Se obtienen los grupos
            _dtActors = new WFBusinessExt().GetAllWFActors();
            IEnumerable<IUserGroup> actors;
            //Se itera por cada wfshape hija
            foreach (WFShape childShape in _ShpRoot.Childs)
            {
                //buscamos los grupos que ven ese WF
                actors = (from actorRow in _dtActors.Select("work_id=" + childShape.Id)
                          select new UserGroup() { ID = long.Parse(actorRow["ID"].ToString()), Name = actorRow["NAME"].ToString(), Description = actorRow["DESCRIPCION"].ToString() }).OfType<IUserGroup>();

                //seteamos al WF los actores correspondientes
                ActorsController.SetChildActors(diagWorkFlow, actors, childShape);
            }

            //Se itera por cada wfshape hija
            foreach (WFShape childShape in _ShpRoot.Childs)
            {
                IEnumerable<ICore> entidades = new WFBusinessExt().GetDocTypeByWF(long.Parse(childShape.Id.ToString()));

                //seteamos al WF los actores correspondientes
                DocTypesController.AddEntities(entidades, diagWorkFlow, childShape);
            }


            lstWF.Clear();

            ////Se organizan los objetos del diagrama
            SetLayout(diagWorkFlow, _ShpRoot);
            ////Se devuelve el diagrama

            diagWorkFlow.GridColor = System.Drawing.Color.White;

            diagWorkFlow.DiagramActors = _dtActors;
            return diagWorkFlow;

        }                
        
        private void SetLayout(MindFusion.Diagramming.Diagram diagram, GenericShape shpRoot)
        {
            //Layout del arbol principal
            _Layout = new FlowchartLayout();

            //Se actualiza el diseño
            _Layout.Arrange(diagram);

            //Verifica si tiene nodos hijos
            if (shpRoot.Childs != null && shpRoot.Childs.Count > 0)
            {
                //Crea un segundo layout para los hijos
                MindFusion.Diagramming.Layout.TreeLayout treeLayout2 = null;
                treeLayout2 = new MindFusion.Diagramming.Layout.TreeLayout(shpRoot,
                        TreeLayoutType.Centered,
                        false,
                        TreeLayoutLinkType.Straight,
                        TreeLayoutDirections.TopToBottom,
                        15, 4, true, new SizeF(10, 10));

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

        public IDiagram ApplyActorFilter(string actorName, DiagramType diagramType, object[] parameters)
        {
            IDiagram diag = GetDiagram(null);

            if (!string.IsNullOrEmpty(actorName))
            {
                DataRow[] rowResults;
                foreach (WFShape childShape in _ShpRoot.Childs)
                {
                    if (childShape.Childs != null)
                    {
                        foreach (var actor in childShape.Childs.OfType<ActorShape>())
                        {
                            try
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
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }

            //_ShpRoot.Childs.Clear();

            SetLayout((MindFusion.Diagramming.Diagram)diag, _ShpRoot);

            return diag;
        }
        public IDiagram FillDiagram(Object[] parameters, bool isRefresh)
        {
            ////Se crea el Diagrama principal
            Diagram diagWorkFlow = new Diagram();

            //ML Este podria ser el Nodo Proyecto.

            ////Se crea un objeto ROOT para comenzar el diagrama
            ZCoreView rootObject = new ZCoreView(0, "Zamba");
            diagWorkFlow.DiagramType = DiagramType.Workflows;
            ////Se crea el nodo root (shape)
            _ShpRoot = new GenericShape(diagWorkFlow, rootObject);
            _ShpRoot.IsRoot = true;
            //Se Obtienen los WFs a Utilizar
            ArrayList lstWF = WFlowDiagramBusiness.GetAllWFs();
            GenericController.DrawAllWFs(lstWF, diagWorkFlow, _ShpRoot);

            //Se obtienen los grupos
            _dtActors = new WFBusinessExt().GetAllWFActors();
            IEnumerable<IUserGroup> actors;
            //Se itera por cada wfshape hija
            foreach (WFShape childShape in _ShpRoot.Childs)
            {
                //buscamos los grupos que ven ese WF
                actors = (from actorRow in _dtActors.Select("work_id=" + childShape.Id)
                          select new UserGroup() { ID = long.Parse(actorRow["ID"].ToString()), Name = actorRow["NAME"].ToString(), Description = actorRow["DESCRIPCION"].ToString() }).OfType<IUserGroup>();

                //seteamos al WF los actores correspondientes
                ActorsController.SetChildActors(diagWorkFlow, actors, childShape);
            }

            //Se itera por cada wfshape hija
            foreach (WFShape childShape in _ShpRoot.Childs)
            {
                IEnumerable<ICore> entidades = new WFBusinessExt().GetDocTypeByWF(long.Parse(childShape.Id.ToString()));

                //seteamos al WF los actores correspondientes
                DocTypesController.AddEntities(entidades, diagWorkFlow, childShape);
            }


            lstWF.Clear();

            ////Se organizan los objetos del diagrama
            SetLayout(diagWorkFlow, _ShpRoot);
            ////Se devuelve el diagrama

            diagWorkFlow.GridColor = System.Drawing.Color.White;

            diagWorkFlow.DiagramActors = _dtActors;
            return diagWorkFlow;

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
        //private void SetLayout(MindFusion.Diagramming.Diagram diagram, GenericShape shpRoot)
        //{
        //    //Layout del arbol principal
        //    _Layout = new LayeredLayout();

        //    //Se actualiza el diseño
        //    _Layout.Arrange(diagram);

        //    //Verifica si tiene nodos hijos
        //    if (shpRoot.Childs != null && shpRoot.Childs.Count > 0)
        //    {
        //        //Crea un segundo layout para los hijos
        //        TreeLayout treeLayout2;
        //        treeLayout2 = new TreeLayout(shpRoot,
        //                TreeLayoutType.HorizontalVertical,
        //                false,
        //                TreeLayoutLinkType.Straight,
        //                TreeLayoutDirections.LeftToRight,
        //                30, 4, true, new SizeF(10, 10));

        //        //Aplica el layout
        //        for (int i = 0; i < shpRoot.Childs.Count; i++)
        //        {
        //            treeLayout2.Root = shpRoot.Childs[i];
        //            treeLayout2.Arrange(diagram);
        //        }
        //    }

        //    //Dibuja el diagrama acomodando todo lo modificado
        //    diagram.ResizeToFitItems(25, false);
        //}
        public void DibujarFlechaWF(IWorkFlow wf)
        {
            try
            {
                GenericShape shape1 = null;
                GenericShape shape2 = null;

                int i = 0;


                //Obtengo un List con todas las conexiones de los shapes
                ArrayList linklist = WfShapesBusiness.FillTransitions(wf);


                if (linklist != null && linklist.Count > 0)
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
                            this.addlink(WFDiagram, shape1, shape2);
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

            DiagramLink link = new DiagramLink(diagram, shape1, shape2);
            link.DrawCrossings = false;
            link.AutoRoute = true;
            diagram.Links.Add(link);

        }
        private void SetLayout(MindFusion.Diagramming.Diagram diagram)
        {
            //Layout del arbol principal
            _Layout = new FlowchartLayout();

            //Se actualiza el diseño
            _Layout.Arrange(diagram);

            //Dibuja el diagrama acomodando todo lo modificado
            diagram.ResizeToFitItems(25, false);
        }
    }
}
