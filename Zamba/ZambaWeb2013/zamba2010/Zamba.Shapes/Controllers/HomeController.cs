using System;
using System.Collections.Generic;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using Diagram = Zamba.Shapes.Views.Diagram;

namespace Zamba.Shapes.Controllers
{
    class HomeController : IDiagramController, IRefresh
    {
        private MindFusion.Diagramming.Layout.TreeLayout treeLayout = null;

        public IDiagram GetDiagram(Object[] parameters)
        {
            return FillDiagram(parameters);

        }

        private IDiagram FillDiagram(Object[] parameters)
        {

            ////Se crea el Diagrama principal
            Diagram diagHome = new Diagram();
            ////Se crea un objeto ROOT para comenzar el diagrama
            ZCoreView rootObject = new ZCoreView(0, "Zamba");
            ////Se crea el nodo root (shape)
            GenericShape shpRoot = new GenericShape(diagHome, rootObject);
            shpRoot.IsRoot = true;

            ////Se agrega el shape de busqueda
            ZCoreView ObjectBusqueda = new ZCoreView(1, "Busqueda");
            diagHome.DiagramType = DiagramType.Search;
            GenericShape shpBusqueda = new GenericShape(diagHome, ObjectBusqueda, shpRoot);
            //////Se Obtienen las Secciones a Utilizar
            List<ISectionDiagram> lstSandE = SectionDiagramBusiness.GetSectionAndEntities();
            if (lstSandE.Count == 0)
            {
              
                ZCoreView rootObject2 = new ZCoreView(0, "El diagrama seleccionado no tiene nodos");
                GenericShape shpRoot2 = new GenericShape(diagHome, rootObject2);
                shpRoot2.Transparent = true;
                shpRoot2.Expandable = false;
                shpRoot2.TextPadding = new MindFusion.Diagramming.Thickness(60f, 1f, 1f, 1f);
                return diagHome;
            }
            //////Ahora comienza el agregado automatico de los nodos
            GenericController.DrawSectionsAndEntities(lstSandE, diagHome, shpBusqueda);
            lstSandE.Clear();

            //////Se agrega el shape de Insertar
            ZCoreView ObjectInsertar = new ZCoreView(2, "Insertar");
            diagHome.DiagramType = DiagramType.Insert;
            GenericShape shpInsertar = new GenericShape(diagHome, ObjectInsertar, shpRoot);
            //////Se Obtienen las Entidades a Utilizar
            ArrayList lstE = EntitiesDiagramBusiness.GetEntitiesByRightType();
            GenericController.DrawAllEntities(lstE, diagHome, shpInsertar);
            lstSandE.Clear();

            //////Se agrega el shape de Tareas
            ZCoreView ObjectTareas = new ZCoreView(3, "Tareas");
            diagHome.DiagramType = DiagramType.Tasks;
            GenericShape shpTareas = new GenericShape(diagHome, ObjectTareas, shpRoot);
            //Se Obtienen los WFs a Utilizar
            ArrayList lstWF = WFlowDiagramBusiness.GetAllWFs();
            GenericController.DrawAllWFs(lstWF, diagHome, shpTareas);
            lstWF.Clear();
            //////Se agrega el shape de Novedades (OPCIONAL)
            //ZCoreView ObjectNovedades = new ZCoreView(4, "Novedades");
            //GenericShape shpNovedades = new GenericShape(diagHome, ObjectNovedades, shpRoot);


            //////Se agrega el shape de Novedades (OPCIONAL)
            ZCoreView ObjectBtnDin = new ZCoreView(5, "Botones Dinamicos");
            GenericShape shpBtnDin = new GenericShape(diagHome, ObjectBtnDin, shpRoot);
            List<IButtonDiagram> lstButton = ButtonDiagramBusiness.getButtonsInMTandH();
            GenericController.DrawAllButtons(lstButton, diagHome, shpBtnDin);
            lstButton.Clear();

            ////se agrega el shape de Reportes
            ZCoreView ObjectReport = new ZCoreView(6, "Reportes");
            GenericShape shpReport = new GenericShape(diagHome, ObjectReport, shpRoot);
            //List<IButtonDiagram> lstButton = ButtonDiagramBusiness.getButtonsInMTandH();
            //GenericController.DrawAllButtons(lstButton, diagHome, shpBtnDin);
            //lstButton.Clear();

            ////Se organizan los objetos del diagrama
            SetLayout(diagHome, shpRoot);
            ////Se devuelve el diagrama

            diagHome.GridColor = System.Drawing.Color.White;
            return diagHome;
        }
                


        private void SetLayout(MindFusion.Diagramming.Diagram diagram, GenericShape shpRoot)
        {
            //Layout del arbol principal
            treeLayout = new MindFusion.Diagramming.Layout.TreeLayout(shpRoot,
                TreeLayoutType.Centered,
                false,
                TreeLayoutLinkType.Rounded,
                TreeLayoutDirections.TopToBottom,
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

        private IDiagram CreateHomeWeb()
        {
            //[Jeremias] 19-12-2012
            ////Se crea el Diagrama principal
            Diagram diagHome = new Diagram();
            ////Se crea un objeto ROOT para comenzar el diagrama
            ZCoreView rootObject = new ZCoreView(0, "Zamba");
            ////Se crea el nodo root (shape)
            GenericShape shpRoot = new GenericShape(diagHome, rootObject);

            ////Se agrega el shape de busqueda
            ZCoreView ObjectBusqueda = new ZCoreView(1, "Búsqueda");
            diagHome.DiagramType = DiagramType.Search;
            GenericShape shpBusqueda = new GenericShape(diagHome, ObjectBusqueda, shpRoot);

            ////Se agrega el shape de Resultados de Búsqueda
            ZCoreView ObjectInsertar = new ZCoreView(2, "Resultados de Búsqueda");
            diagHome.DiagramType = DiagramType.Search;
            GenericShape shpInsertar = new GenericShape(diagHome, ObjectInsertar, shpRoot);

            ////Se agrega el shape de Listados
            ZCoreView ObjectTareas = new ZCoreView(3, "Listados");
            diagHome.DiagramType = DiagramType.Tasks;
            GenericShape shpTareas = new GenericShape(diagHome, ObjectTareas, shpRoot);
     
            //Se agregan los Shapes 'Listado Tareas y Tareas Activas' al Shape 'Listados'
            ZCoreView ObjectListadoTareas = new ZCoreView(6, "Listado Tareas");
            diagHome.DiagramType = DiagramType.Tasks;
            GenericShape shpListadoTareas = new GenericShape(diagHome, ObjectListadoTareas, shpTareas);
            ZCoreView ObjectTareasActivas = new ZCoreView(7, "Tareas Activas");
            diagHome.DiagramType = DiagramType.Tasks;
            GenericShape shpTareasActivas = new GenericShape(diagHome, ObjectTareasActivas, shpTareas);

            //Se agregan los Shapes 'Foro'
             ZCoreView ObjectForo = new ZCoreView(8, "Foro");
            diagHome.DiagramType = DiagramType.Tasks;
            GenericShape shpForo = new GenericShape(diagHome, ObjectForo, shpTareasActivas);

            //Se agregan los Shapes 'Historial de mail'
             ZCoreView ObjectHistorial = new ZCoreView(9, "Historial de mail");
            diagHome.DiagramType = DiagramType.Tasks;
            GenericShape shpHistorial = new GenericShape(diagHome, ObjectHistorial, shpTareasActivas);

            //////Se agrega el shape de 'Reporte General'
            ZCoreView ObjectNovedades = new ZCoreView(4, "Reporte General");
            GenericShape shpNovedades = new GenericShape(diagHome, ObjectNovedades, shpRoot);

            //////Se agrega el shape de 'Botones Dinámicos'
            ZCoreView ObjectBtnDin = new ZCoreView(5, "Botones Dinámicos");
            GenericShape shpBtnDin = new GenericShape(diagHome, ObjectBtnDin, shpRoot);
            List<IButtonDiagram> lstButton = ButtonDiagramBusiness.getButtonsInMTandH();
            GenericController.DrawAllButtons(lstButton, diagHome, shpBtnDin);
            lstButton.Clear();

            ////Se organizan los objetos del diagrama
            SetLayout(diagHome, shpRoot);
            ////Se devuelve el diagrama

            diagHome.GridColor = System.Drawing.Color.White;
            return diagHome;

        }


        public IDiagram Refresh(object[] parameters)
        {
            return FillDiagram(parameters);
        }
    }
}
