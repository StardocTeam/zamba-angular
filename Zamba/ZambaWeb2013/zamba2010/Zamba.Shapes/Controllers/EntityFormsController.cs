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
using System.IO;
using MindFusion.Diagramming;
using System.Linq;

namespace Zamba.Shapes.Controllers
{
    class EntityFormsController : IDiagramController, IDiagramFiltereable, IRefresh
    {
        private GenericShape _ShpRoot;
        private FlowchartLayout _Layout = null;
        private object[] _params;
        Diagram wfDiagram;

        public IDiagram GetDiagram(object[] parameters)
        {
            return FillDiagram(parameters);
        }

        private IDiagram FillDiagram(object[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                IDocType docType = (IDocType)parameters[0];

                //Diagrama donde se comienzan a agregar los nodos
                wfDiagram = new Diagram();
                wfDiagram.DiagramType = DiagramType.EntityForms;
                _ShpRoot = new DocTypeShape(wfDiagram, docType);
                _ShpRoot.IsRoot = true;

                List<ZwebForm> forms = FormBusiness.GetAllForms(docType.ID, true);

                if (forms != null)
                {
                    foreach (ZwebForm form in forms)
                    {
                        AddFormChild(wfDiagram, form, _ShpRoot);
                    }
                }

                SetLayout(wfDiagram, _ShpRoot);

                return wfDiagram;
            }
            return null;
        }

        public void AddFormChild(Diagram diagWorkFlow, IZwebForm form, GenericShape childShape)
        {
            FormShape fShape = new FormShape(diagWorkFlow, form, childShape);
        }

        private void SetLayout(Diagram diagram, GenericShape genericShape)
        {
            //Layout del arbol principal
            _Layout = new FlowchartLayout();

            //Se actualiza el diseño
            _Layout.Arrange(diagram);

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
