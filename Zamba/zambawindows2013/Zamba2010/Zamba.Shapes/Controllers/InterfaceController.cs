using System;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using System.Data;
using Diagram = Zamba.Shapes.Views.Diagram;
using Zamba.Core.Enumerators;

namespace Zamba.Shapes.Controllers
{
    class InterfaceController : IDiagramController, IDiagramFiltereable, IRefresh
    {
        private TreeLayout _treeLayout = null;

        Diagram WFDiagram;
        public IDiagram GetDiagram(Object[] parameters)
        {
            return FillDiagram(parameters);
        }

        private IDiagram FillDiagram(Object[] parameters)
        {
            ArrayList Rules = WFlowDiagramBusiness.GetInterfaceRules();
            //Diagrama donde se comienzan a agregar los nodos
            WFDiagram = new Diagram();

            WFDiagram.DiagramType = DiagramType.Interfaces;

            //Se agrega el primer shape. Este seria como un titulo, el inicio de todo
            ZCoreView rootObject = new ZCoreView(0, "Interfaces");
            GenericShape shpRoot = new GenericShape(WFDiagram, rootObject);
            shpRoot.IsRoot = true;
            if (Rules.Count == 0)
            {
                ZCoreView rootObject2 = new ZCoreView(0, "El diagrama seleccionado no tiene nodos");
                GenericShape shpRoot2 = new GenericShape(WFDiagram, rootObject2);
                shpRoot2.Transparent = true;
                shpRoot2.Expandable = false;
                shpRoot2.TextPadding = new MindFusion.Diagramming.Thickness(60f, 1f, 1f, 1f);
                return WFDiagram;
            }
            DrawAllInterfaces(Rules, WFDiagram, shpRoot);

            //Se organizan los objetos del diagrama
            SetLayout(WFDiagram, shpRoot);
            //Se devuelve el diagrama
            return WFDiagram;
        }

        public static void DrawAllInterfaces(ArrayList lst, Diagram diagHome, GenericShape shpTareas)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                InterfaceShape shpWf = new InterfaceShape(diagHome, (IInterfaceDiagram)lst[i], shpTareas);
            };
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

        public IDiagram Refresh(object[] parameters)
        {
            return FillDiagram(parameters);
        }
    }
}