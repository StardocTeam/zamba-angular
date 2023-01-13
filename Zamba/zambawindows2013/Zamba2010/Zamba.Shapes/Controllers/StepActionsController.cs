using System;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using System.Data;
using Diagram = Zamba.Shapes.Views.Diagram;
using Zamba.Core.Enumerators;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MindFusion.Diagramming;
using MindFusion.Diagramming.WinForms;
using Zamba.Shapes.Views;

namespace Zamba.Shapes.Controllers
{
    class StepActionsController : IDiagramController, IDiagramFiltereable, IDiagramCategoryFilter,IRefresh
    {
        private GenericShape _ShpRoot;
        private FlowchartLayout _Layout = null;
        
        Diagram WFDiagram;
        DataTable _dtActors = new DataTable();

        public StepActionsController()
        { 
            _dtActors.Columns.Add("NAME", typeof(string));
        }

        public IDiagram GetDiagram(Object[] parameters)
        {
           return FillDiagram(parameters,false,ref WFDiagram, _ShpRoot,_dtActors, _Layout);
        }

        public static IDiagram FillDiagram(Object[] parameters, bool isRefresh, ref Diagram WFDiagram, GenericShape _ShpRoot, DataTable _dtActors, FlowchartLayout _Layout)
        {
            RuleShape shpWFs = null;
            var stepShape = ((StepShape)parameters[0]);

            ////Falta Cambiar para solo traer las reglas de accion de usuario y las generales, solo la primer regla.
            //StepShape.Rules = WFRulesBusiness.GetCompleteHashTableRulesByStep((Int64)StepShape.Step.ID,isRefresh);

            //Diagrama donde se comienzan a agregar los nodos
            WFDiagram = new Diagram();
            WFDiagram.DiagramType = DiagramType.StepActions;

            //Se agrega el primer shape. Este seria como un titulo, el inicio de todo
            ZCoreView rootObject = new ZCoreView(stepShape.Step.ID, stepShape.Step.Name);
            _ShpRoot = new GenericShape(WFDiagram, rootObject);
            _ShpRoot.IsRoot = true;

            // 'Agrega las reglas padres que dependen del primer nodo del tipo de reglas
            Boolean isFirst = true;
            GenericShape shapeAdded = null;
            string ruleCondition;
            WFRulesBusinessExt rBusExt;
            List<UserGroup> usrg = null;
            TestCaseBusiness tcb;
            Boolean hasTestCase;
            DataTable dtTestCase = null;
             
            try
            {
                rBusExt = new WFRulesBusinessExt();
                tcb = new TestCaseBusiness();

                if (stepShape.Rules == null)
                {
                    stepShape.Rules = WFRulesBusiness.GetRulesDSByStepID(stepShape.Step.ID, false);
                }

                foreach (DsRules.WFRulesRow r in stepShape.Rules.WFRules.Rows)
                {
                    dtTestCase = tcb.GetCases(43, (Int64)r.Id);
                    hasTestCase = (dtTestCase.Rows.Count > 0);

                    //ML: Lo ideal no seria instanciar la regla. Deberia el DS tener la categoria
                    IWFRuleParent rule = Zamba.Core.WFRulesBusiness.GetInstanceRuleById((Int64)r.Id,stepShape.Step.ID,true);
                    if (r.ParentId == 0 || rule.Category == 0)
                    {
                        switch ((TypesofRules)r.ParentType)
                        {
                            case TypesofRules.Entrada:
                            case TypesofRules.ValidacionEntrada:
                            case TypesofRules.Salida:
                            case TypesofRules.ValidacionSalida:
                            case TypesofRules.Actualizacion:
                            case TypesofRules.Planificada:
                            case TypesofRules.Eventos:
                            case TypesofRules.AccionUsuario:
                            case TypesofRules.Floating:
                            case TypesofRules.Regla:
                                shapeAdded = AddRuleShape(_ShpRoot, rule, stepShape, hasTestCase,ref WFDiagram);
                                break;
                        }

                        //Se obtiene la condicion de habilitacion de la regla
                        ruleCondition = rBusExt.GetRuleCondition((Int64)r.Id);
                        //Se agregan los actores que se involucran en la habilitacion
                        usrg = ActorsController.AddRuleActors(rule, ruleCondition, shapeAdded, WFDiagram);

                        foreach (UserGroup item in usrg)
                        {
                            if (_dtActors.Select("NAME='" + item.Name + "'").Length == 0)
                            {
                                _dtActors.Rows.Add(new object[] { item.Name });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                rBusExt = null;
                tcb = null;
                if(usrg != null)
                {
                    usrg.Clear();
                    usrg = null;
                }
                if(dtTestCase != null)
                {
                    dtTestCase.Dispose();
                    dtTestCase = null;
                }
            }

            //Se organizan los objetos del diagrama
            SetLayout(WFDiagram, _ShpRoot,_Layout);
            WFDiagram.DiagramActors = _dtActors;
            
            //Se devuelve el diagrama
            return WFDiagram;
        }

        private static GenericShape AddRuleShape(GenericShape parentShape, IWFRuleParent rule, StepShape stepshape, Boolean useTestCase, ref Diagram WFDiagram)
        {
            try
            {

                GenericShape RuleShape;
                //= new RuleShape(WFDiagram, (IRule)rule, parentShape);

                if (rule.Enable == false)
                {
                    RuleShape = new RuleShape(WFDiagram, (IRule)rule, stepshape, parentShape,AttachToNode.TopLeft, useTestCase);

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
                    if (rule.ParentType == TypesofRules.AccionUsuario)
                    {
                        WFBusiness WFB = new WFBusiness();
                        RuleShape.Text = WFB.GetUserActionName(rule.ID,rule.WFStepId,rule.Name, true);
                        WFB = null;
                    }
                    else
                    {
                        RuleShape.Text = WFRulesBusiness.GetRuleName(rule.ID);
                    }
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

        private static  void SetLayout(MindFusion.Diagramming.Diagram diagram, GenericShape shpRoot, FlowchartLayout _Layout)
        {
            //Layout del arbol principal
            _Layout = new FlowchartLayout();
            
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

        public IDiagram ApplyActorFilter(string actorName, DiagramType diagramType, object[] parameters)
        {
            IDiagram diag = GetDiagram(parameters);

            if (!string.IsNullOrEmpty(actorName))
            {
                DataRow[] rowResults;
                foreach (GenericShape childShape in _ShpRoot.Childs)
                {
                    if (childShape.Childs != null)
                    {
                        foreach (ActorShape actor in childShape.Childs)
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
                    }
                }
            }

            //_ShpRoot.Childs.Clear();

            SetLayout((MindFusion.Diagramming.Diagram)diag, (StepShape)parameters[0], _Layout);

            return diag;
        }

        public IDiagram ApplyCategoryRuleFilter(int category, DiagramType diagramType, object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IDiagram Refresh(Object[] parameters)
        {
            return FillDiagram(parameters, true,ref WFDiagram, _ShpRoot,_dtActors, _Layout);
        }
    }
}
