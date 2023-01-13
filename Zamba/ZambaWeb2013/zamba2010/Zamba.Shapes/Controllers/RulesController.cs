using System;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using System.Data;
using Diagram = Zamba.Shapes.Views.Diagram;
using Zamba.Core.Enumerators;
using Zamba.Shapes.Interfaces;
using System.Collections.Generic;

namespace Zamba.Shapes.Controllers
{
    class RulesController : IDiagramController, IDiagramFiltereable, IDiagramCategoryFilter, IRefresh
    {
        TreeLayout _treeLayout = null;
        Diagram wfDiagram;

        public IDiagram GetDiagram(Object[] parameters)
        {
            return FillDiagram(parameters, false);
        }

        private IDiagram FillDiagram(Object[] parameters, bool isRefresh)
        {
            int category = 10;
            var ruleShape = (IGenericRuleShape)parameters[0];

            //Diagrama donde se comienzan a agregar los nodos
            wfDiagram = new Diagram();

            //Se completan las etapas
            if (ruleShape.stepShape == null || isRefresh)
                ruleShape.stepShape = new StepShape(wfDiagram, WFStepBusiness.GetStepById(WFStepBusiness.GetStepIdByRuleId(ruleShape.Rule.ID, !isRefresh), isRefresh));

            ////Se completan las reglas
            //if (ruleShape.stepShape.Rules == null || ruleShape.stepShape.Rules.WFRules.Count == 0)
            // ruleShape.stepShape.Rules = WFRulesBusiness.GetCompleteHashTableRulesByStep((Int64)ruleShape.stepShape.Step.ID, isRefresh);

            //Se obtiene la categoría
            category = (int)parameters[1];

            //Se agrega el primer shape. Este seria como un titulo, el inicio de todo
            ZCoreView rootObject = new ZCoreView(ruleShape.Rule.ID, ruleShape.Rule.Name);
            GenericShape shpRoot = new GenericShape(wfDiagram, rootObject);
            shpRoot.IsRoot = true;

            try
            {
                if (ruleShape.Rule.ChildRules.Count == 0)
                {
                   // ruleShape.Rule.ChildRules = WFRulesBusiness.GetInstanceRuleById(ruleShape.Rule.ID,true);
                }
                // 'Agrega las reglas padres que dependen del primer nodo del tipo de reglas
                Boolean IsFirst = true;
                foreach (WFRuleParent r in ruleShape.Rule.ChildRules)
                {
                    switch (r.ParentType)
                    {
                        case TypesofRules.Entrada:
                        case TypesofRules.ValidacionEntrada:
                        case TypesofRules.Salida:
                        case TypesofRules.ValidacionSalida:
                        case TypesofRules.Actualizacion:
                        case TypesofRules.Planificada:
                        case TypesofRules.Eventos:
                        case TypesofRules.AccionUsuario:
                        case TypesofRules.Regla:
                        case TypesofRules.Floating:
                            AddRuleShape(shpRoot, r, ((IGenericRuleShape)ruleShape).stepShape, category);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            //Se organizan los objetos del diagrama
            SetLayout(wfDiagram, (GenericShape)shpRoot);

            //Se devuelve el diagrama
            return wfDiagram;
        }

        public void AddRuleShape(GenericShape parentShape, WFRuleParent rule, StepShape stepshape, int category, Diagram dg = null)
        {
            try
            {
                GenericShape ruleShape;

                Diagram currDiag = wfDiagram;
                if (dg != null)
                {
                    currDiag = dg;
                }

                if (rule.Category <= category)
                {
                    if (rule.Enable == false)
                    {
                        ruleShape = new RuleShape(currDiag, (IRule)rule, stepshape, parentShape);
                    }
                    else if (rule.RuleClass.Contains("IfBranch"))
                    {
                        ruleShape = new ConditionalRuleShape(currDiag, (IRule)rule, stepshape, parentShape);
                    }
                    else if (rule.RuleClass.Contains("If"))
                    {
                        ruleShape = new ConditionalRuleShape(currDiag, (IRule)rule, stepshape, parentShape);
                    }
                    else if (rule.RuleClass.Contains("Design"))
                    {
                        ruleShape = new RuleShape(currDiag, (IRule)rule, stepshape, parentShape);
                    }
                    else if (rule.RuleClass.Contains("DOExecuteRule"))
                    {
                        ruleShape = new RuleShape(currDiag, (IRule)rule, stepshape, parentShape);
                        ruleShape.Image = global::Zamba.Shapes.Properties.Resources.Process_Accept;
                        ruleShape.ImageAlign = MindFusion.Drawing.ImageAlign.BottomRight;
                    }
                    else if (rule.RefreshRule.Value)
                    {
                        ruleShape = new RuleShape(currDiag, (IRule)rule, stepshape, parentShape);
                    }
                    else
                    {
                        ruleShape = new RuleShape(currDiag, (IRule)rule, stepshape, parentShape);
                    }
                }
                else
                {
                    ruleShape = parentShape;
                }

                foreach (WFRuleParent R in rule.ChildRules)
                {
                    AddRuleShape(ruleShape, R, stepshape, category);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
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
                treeLayout2 = new TreeLayout(shpRoot.Childs[0],
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

        public IDiagram ApplyCategoryRuleFilter(int category, DiagramType diagramType, object[] parameters)
        {
            if (parameters.Length < 2)
            {
                Array.Resize<object>(ref parameters, 2);
            }
            parameters[1] = category;

            return GetDiagram(parameters);
        }

        public void AddZvarShape(Diagram dg, IEnumerable<string> zvarNames, GenericShape parentShape)
        {
            foreach (string zvar in zvarNames)
            {
                GenericShape gs = new GenericShape(dg, new ZCoreView(0, zvar), parentShape);
            }
        }

        public IDiagram Refresh(object[] parameters)
        {
            ((RuleShape)parameters[0]).Rule = WFRulesBusiness.GetInstanceRuleById(((RuleShape)parameters[0]).Rule.ID,false);
            return FillDiagram(parameters, true);
        }
    }
}
