using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using Diagram = Zamba.Shapes.Views.Diagram;
using System.Data;

namespace Zamba.Shapes.Controllers
{
    class ReportController: IDiagramFiltereable, IRefresh
    {
        ReportBuilder.Business.ReportBuilderComponent RB = new ReportBuilder.Business.ReportBuilderComponent();

        private MindFusion.Diagramming.Layout.TreeLayout treeLayout = null;

        public IDiagram GetDiagram(Object[] parameters)
        {
            return FillDiagram(parameters);

        }

        private IDiagram FillDiagram(Object[] parameters)
        {
            ////Se crea el Diagrama principal
            Diagram diagWorkFlow = new Diagram();

            //ML Este podria ser el Nodo Proyecto.

            ////Se crea un objeto ROOT para comenzar el diagrama
            ZCoreView rootObject = new ZCoreView(0, "Reportes");
            diagWorkFlow.DiagramType = DiagramType.Reports;
            ////Se crea el nodo root (shape)
            GenericShape shpRoot = new GenericShape(diagWorkFlow, rootObject);
            shpRoot.IsRoot = true;

            //Se cargan los reportes Customizados.
            DataSet dsReports = new DataSet();
            dsReports = RB.GetAllQueryIdsAndNames();
            if (dsReports.Tables[0].Rows.Count == 0)
            {
          
                ZCoreView rootObject2 = new ZCoreView(0, "El diagrama seleccionado no tiene nodos");
                GenericShape shpRoot2 = new GenericShape(diagWorkFlow, rootObject2);
                shpRoot2.Transparent = true;
                shpRoot2.Expandable = false;
                shpRoot2.TextPadding = new MindFusion.Diagramming.Thickness(60f, 1f, 1f, 1f);
                return diagWorkFlow;
            }
    
            for (int i = 0; i < dsReports.Tables[0].Rows.Count; i++)
            {

                string ReportName = dsReports.Tables[0].Rows[i][1].ToString();

                ZCoreView ObjectBusqueda = new ZCoreView(1, ReportName);
                diagWorkFlow.DiagramType = DiagramType.Reports;
                GenericShape shpBusqueda = new GenericShape(diagWorkFlow, ObjectBusqueda, shpRoot);

            }
            //Se cargan los reportes Generales.
            DataSet dsGeneralReports = new DataSet();
            dsGeneralReports = RB.GetAllQueryIdsAndNamesReporteGeneral();

            for (int i = 0; i < dsGeneralReports.Tables[0].Rows.Count; i++)
            {

                string ReportName = dsGeneralReports.Tables[0].Rows[i][1].ToString();

                ZCoreView ObjectBusqueda = new ZCoreView(1, ReportName);
                diagWorkFlow.DiagramType = DiagramType.Reports;
                GenericShape shpBusqueda = new GenericShape(diagWorkFlow, ObjectBusqueda, shpRoot);

            }

            ////Se organizan los objetos del diagrama
            SetLayout(diagWorkFlow, shpRoot);
            ////Se devuelve el diagrama

            diagWorkFlow.GridColor = System.Drawing.Color.White;
            return diagWorkFlow;
        }

        private void SetLayout(MindFusion.Diagramming.Diagram diagram, GenericShape shpRoot)
        {
            //Layout del arbol principal
            treeLayout = new MindFusion.Diagramming.Layout.TreeLayout(shpRoot,
                TreeLayoutType.Cascading,
                false,
                TreeLayoutLinkType.Rounded,
                TreeLayoutDirections.LeftToRight,
                10, 5, true, new SizeF(10, 10));

            //Se actualiza el diseño
            treeLayout.Arrange(diagram);

            //Verifica si tiene nodos hijos
            if (shpRoot.Childs != null && shpRoot.Childs.Count > 0)
            {
                //Crea un segundo layout para los hijos
                MindFusion.Diagramming.Layout.TreeLayout treeLayout2 = null;
                treeLayout2 = new MindFusion.Diagramming.Layout.TreeLayout(shpRoot,
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
