using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;
using Zamba.Diagrams.Controllers;

namespace Zamba.Diagrams.Controllers
{
    class GenericController
    {
        public static IDiagram GetDiagram(DiagramType type, Object[] param = null)
        {
            IDiagram diagram = null;

            //Verifica que tipo de diagrama debe dibujar
            switch (type)
            {
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
                    //diagram = FormsController.GetDiagram(param);
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
                    //diagram = StepActionsController.GetDiagram(param);
                    break;
                case DiagramType.Tasks:
                    //diagram = TasksController.GetDiagram(param);
                    break;
                case DiagramType.WorkflowEntitiesRelations:
                    //diagram = WorkflowEntitiesRelations.GetDiagram(param);
                    break;
                case DiagramType.Workflows:
                    //diagram = Workflows.GetDiagram(param);
                    break;
                case DiagramType.WorkflowSteps:
                    //diagram = WorkflowsSteps.GetDiagram(param);
                    break;
            }

            return diagram;
        }
    }
}
