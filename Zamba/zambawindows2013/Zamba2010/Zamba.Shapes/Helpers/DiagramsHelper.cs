using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zamba.Shapes.Helpers
{
    class DiagramsHelper
    {

        public static String GetDiagramTypeDescripcion(Core.DiagramType dgmtype)
        {
            switch (dgmtype)
            { 
                case Core.DiagramType.SiteMap:
                    return  "A continuacion se presenta el diagrama general del sitio, con las funciones y procesos principales de la solucion.";
                    break;
                    case Core.DiagramType.Workflows:
return "En el siguiente diagrama se listan todos los procesos definidos para la solucion y los perfiles/actores que pueden actuar en cada proceso.";
break;
                    case Core.DiagramType.Entities:
                    return "Se listan a continuacion las entidades exitentes en la solucion y sus atributos, denotando tambien la relacion de asociacion existe entre las entidades.";
                        break;

                    case Core.DiagramType.StepActions:
  return "En este diagrama se visualizan todas las Acciones y Reglas de Negocio que se pueden invocar en la Etapa del Proceso. Las reglas se muestran a nivel macro, indicando que perfiles pueden actuar sobre las mismas.";
                        break;

                    case Core.DiagramType.Actors:
                    return "este diagrama muestra los perfiles de usuarios y los actores asignados a cada uno.";
                break;
                    case Core.DiagramType.Forms:
                    return "Este diagrama lista los Formularios existentes, para las entidades.";
                break;

                    case Core.DiagramType.Environment:
                    return "";
                        break;

                    case Core.DiagramType.WorkflowSteps:
                    return "En este diagrama, se listan las etapas de un Workflow.";
                        break;

                    case Core.DiagramType.WorkflowEntitiesRelations:
                    return "";
                break;

                    case Core.DiagramType.Search:
                    return "";
                break;

                    case Core.DiagramType.Insert:
                    return "";
                break;
                    case Core.DiagramType.Tasks:
                    return "";
                break;

                    case Core.DiagramType.Home:
                    return "A continuacion se presenta el diagrama general del sitio, con las funciones y procesos principales de la solucion.";
                break;

                    case Core.DiagramType.DiagramType:
                    return "";
                break;

                    case Core.DiagramType.WorkFlowRules:
return "En este diagrama se visualizan todas las reglas que componen un proceso, Accion de Usuario, Evento, o Regla de Negocio.";
break;

                    case Core.DiagramType.Interfaces:
  return "En este diagrama se muestras todas las interfaces externas que el sistema mantiene. Las mismas pueden ser conexiones a bases de datos o consumo de Web Services.";
break;

                    case Core.DiagramType.Reports:
                    return "En este listado se muestran todos los reportes habilitados y configurados para la solucion.";
                break;

                    case Core.DiagramType.DocType:
                    return "Se listan a continuacion las entidades exitentes en la solucion y sus atributos, denotando tambien la relacion de asociacion existe entre las entidades.";
                break;

                    case Core.DiagramType.DER:
                    return "Se listan a continuacion las entidades exitentes en la solucion y las tablas correspondientes en la base de datos.";
                break;
                default:
                return "";
                break;   
            
            }
        
        }
    }
}
