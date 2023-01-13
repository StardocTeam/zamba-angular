using System;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using System.Data;
using Diagram = Zamba.Shapes.Views.Diagram;
using Zamba.Core.Enumerators;
using MindFusion.Diagramming;
using System.Collections.Generic;

namespace Zamba.Shapes.Controllers
{
    class TasksController : IDiagramController, IDiagramFiltereable,IRefresh
    {
        private TreeLayout _treeLayout = null;
        private GenericShape _ShpRoot;
        Diagram WFDiagram;
        DataTable _dtActors = new DataTable();
        public IDiagram GetDiagram(Object[] parameters)
        {
            //zsp_WorkFlow_100_GetWFRelations
            DataTable dtWFRelations = WFBusiness.GetWorkflowRelations();

            if (dtWFRelations.Rows.Count > 0)
            {
                //Diagrama donde se comienzan a agregar los nodos
                Diagram WFDiagram = new Diagram();
                
                //Se agrega el primer shape. Este seria como un titulo, el inicio de todo
                ZCoreView rootObject = new ZCoreView(0, "Workflows");
                GenericShape shpRoot = new GenericShape(WFDiagram, rootObject);
                shpRoot.IsRoot = true;
                //
                ArrayList WFIDs = new ArrayList();
                ArrayList WFRelationIDs = new ArrayList();
                WorkflowShape shpWFs = null;

                //recorro los workflows
                foreach (DataRow WFs in dtWFRelations.Rows)
                {
                    if (!(WFIDs.Contains(Convert.ToInt64(WFs.ItemArray[0]))))
                    {
                        //verifico si la relacion existe
                        if (!(WFRelationIDs.Contains(WFIDs)))
                        {
                            WorkFlow WF = WFBusiness.GetWFbyId(Convert.ToInt64(WFs.ItemArray[0]));
                            shpWFs = new WorkflowShape(WFDiagram, (IWorkFlow)WF, shpRoot);

                            //cargo los Nodos WF que estan relacionados al WF padre
                            foreach (DataRow WFRelations in dtWFRelations.Rows)
                            {
                                if (WF.ID == Convert.ToInt64(WFRelations.ItemArray[0]))
                                {
                                    if (!(WFRelationIDs.Contains(Convert.ToInt64(WFRelations.ItemArray[1]))))
                                    {
                                        //obtengo wf
                                        IWorkFlow WFRelation = (IWorkFlow)WFBusiness.GetWFbyId(Convert.ToInt64(WFRelations.ItemArray[1]));
                                        //agrego nodo hijo
                                        WorkflowShape shpWFs2 = new WorkflowShape(WFDiagram, (IWorkFlow)WFRelation, shpWFs);
                                        //agrego id al array
                                        WFRelationIDs.Add(Convert.ToInt64(WFRelations.ItemArray[1]));
                                    }
                                }
                            }
                            //-----------------------------------------------------------------------------------

                            WFRelationIDs.Clear();
                            WFIDs.Add(Convert.ToInt64(WFs.ItemArray[0]));
                        }
                    }
                    //Se organizan los objetos del diagrama
                    SetLayout(WFDiagram, shpRoot);
                }
                    //Se devuelve el diagrama
                    return WFDiagram;                
            }
            return null;
        }

        private void SetLayout(MindFusion.Diagramming.Diagram diagram, GenericShape shpRoot)
        {
            //Layout del arbol principal
            _treeLayout = new TreeLayout(shpRoot,
                TreeLayoutType.Cascading,
                false,
                TreeLayoutLinkType.Rounded,
                TreeLayoutDirections.LeftToRight,
                10, 5, true, new SizeF(10, 10));

            //Se actualiza el diseño
            _treeLayout.Arrange(diagram);

            //Verifica si tiene nodos hijos
            if (shpRoot.Childs != null && shpRoot.Childs.Count > 0)
            {
                //Crea un segundo layout para los hijos
                TreeLayout treeLayout2;
                treeLayout2 = new TreeLayout(shpRoot,
                        TreeLayoutType.Centered,
                        false,
                        TreeLayoutLinkType.Straight,
                        TreeLayoutDirections.LeftToRight,
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
            throw new NotImplementedException();
        }
        public IDiagram FillDiagram(Object[] parameters, bool isRefresh)
        {
            //zsp_WorkFlow_100_GetWFRelations
            DataTable dtWFRelations = WFBusiness.GetWorkflowRelations();

            if (dtWFRelations.Rows.Count > 0)
            {
                //Diagrama donde se comienzan a agregar los nodos
                Diagram WFDiagram = new Diagram();

                //Se agrega el primer shape. Este seria como un titulo, el inicio de todo
                ZCoreView rootObject = new ZCoreView(0, "Workflows");
                GenericShape shpRoot = new GenericShape(WFDiagram, rootObject);
                shpRoot.IsRoot = true;
                //
                ArrayList WFIDs = new ArrayList();
                ArrayList WFRelationIDs = new ArrayList();
                WorkflowShape shpWFs = null;

                //recorro los workflows
                foreach (DataRow WFs in dtWFRelations.Rows)
                {
                    if (!(WFIDs.Contains(Convert.ToInt64(WFs.ItemArray[0]))))
                    {
                        //verifico si la relacion existe
                        if (!(WFRelationIDs.Contains(WFIDs)))
                        {
                            WorkFlow WF = WFBusiness.GetWFbyId(Convert.ToInt64(WFs.ItemArray[0]));
                            shpWFs = new WorkflowShape(WFDiagram, (IWorkFlow)WF, shpRoot);

                            //cargo los Nodos WF que estan relacionados al WF padre
                            foreach (DataRow WFRelations in dtWFRelations.Rows)
                            {
                                if (WF.ID == Convert.ToInt64(WFRelations.ItemArray[0]))
                                {
                                    if (!(WFRelationIDs.Contains(Convert.ToInt64(WFRelations.ItemArray[1]))))
                                    {
                                        //obtengo wf
                                        IWorkFlow WFRelation = (IWorkFlow)WFBusiness.GetWFbyId(Convert.ToInt64(WFRelations.ItemArray[1]));
                                        //agrego nodo hijo
                                        WorkflowShape shpWFs2 = new WorkflowShape(WFDiagram, (IWorkFlow)WFRelation, shpWFs);
                                        //agrego id al array
                                        WFRelationIDs.Add(Convert.ToInt64(WFRelations.ItemArray[1]));
                                    }
                                }
                            }
                            //-----------------------------------------------------------------------------------

                            WFRelationIDs.Clear();
                            WFIDs.Add(Convert.ToInt64(WFs.ItemArray[0]));
                        }
                    }
                    //Se organizan los objetos del diagrama
                    SetLayout(WFDiagram, shpRoot);
                }
                //Se devuelve el diagrama
                return WFDiagram;
            }
            return null;

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
    }
}
