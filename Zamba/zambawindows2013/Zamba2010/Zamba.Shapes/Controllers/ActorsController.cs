using System;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using Diagram = Zamba.Shapes.Views.Diagram;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MindFusion.Diagramming;
using System.Linq;

namespace Zamba.Shapes.Controllers
{
    class ActorsController: IDiagramController, IDiagramFiltereable,IRefresh
    {
        private LayeredLayout layout = null;

        public IDiagram GetDiagram(Object[] parameters)
        {
            return FillDiagram(parameters, false);
        }

        private IDiagram FillDiagram(Object[] parameters, bool isRefresh)
        {
            //Se obtienen los datos a manipular
            ArrayList lstGroups = UserGroupBusiness.GetAllGroupsArrayList();
            //Diagrama donde se comienzan a agregar los nodos
            //Diagram diagActors = new Diagram();
            Diagram diagActors = new Diagram();
            if (lstGroups.Count > 0)
            {
               

                //Se crea un objeto ROOT para comenzar el diagrama
                ZCoreView rootObject = new ZCoreView(0, "Actors");
                //GenericShape shpRoot = new GenericShape(diagActors, rootObject);

                //Se agrega el primer shape. Este seria como un titulo, el inicio de todo
                //diagActors.Nodes.Add(shpRoot);

                //Ahora comienza el agregado automatico de los nodos
                for (int i = 0; i < lstGroups.Count; i++)
                {
                    //Se obtiene el grupo del listado
                    IUserGroup group = (IUserGroup)lstGroups[i];

                    //Se completan los usuarios del grupo
                    ArrayList users = UserBusiness.GetGroupUsers(group.ID);
                    group.Users = users;

                    //Se crea el shape especifico por actor
                    ActorShape shpGroup = new ActorShape(diagActors, (IActor)group);
                    //Se reubica el texto del shape.
                    shpGroup.TextPadding = new MindFusion.Diagramming.Thickness(1f, 25f, 1f, 1f);

                    //Busco la existencia de usuarios
                    if (users != null && users.Count > 0)
                    {
                        for (int j = 0; j < users.Count; j++)
                        {
                            //Se obtiene el usuario del listado
                            IUser user = (IUser)group.Users[j];

                            //Se crea el shape del usuario
                            ActorShape shpUser = new ActorShape(diagActors, (IActor)user, shpGroup);
                            shpUser.Expanded = true;
                            shpUser.Expandable = false;

                            shpUser.TextPadding = new MindFusion.Diagramming.Thickness(25f, 1f, 1f, 1f);
                        }
                    }
                }

                //Se organizan los objetos del diagrama
                SetLayout(diagActors);

                //Se devuelve el diagrama
                return diagActors;
            }
            else
            {
                ZCoreView rootObject = new ZCoreView(0, "El diagrama seleccionado no tiene nodos");
                GenericShape shpRoot = new GenericShape(diagActors, rootObject);
                shpRoot.Transparent = true;
                shpRoot.Expandable = false;
                shpRoot.TextPadding = new MindFusion.Diagramming.Thickness(50f, 1f, 1f, 1f);
                return diagActors;
            }
        }

        private void SetLayout(MindFusion.Diagramming.Diagram diagram)
        {
            //Layout del arbol principal

            layout = new LayeredLayout();
            layout.Orientation = MindFusion.Diagramming.Layout.Orientation.Horizontal;
            
            //Setea la distancia entre los distintos grupos
            layout.Margins = new SizeF(20, 0);

            layout.Arrange(diagram);

            //Dibuja el diagrama acomodando todo lo modificado
            diagram.ResizeToFitItems(25, false);
        }

        internal static void SetChildActors(Diagram diagWorkFlow, IEnumerable<IUserGroup> actors, GenericShape childShape)
        {
            foreach (IUserGroup group in actors)
            {    
                    ActorShape act = new ActorShape(diagWorkFlow, (IActor)group, childShape);
                    act.Expanded = true;
                    act.Expandable = false;
                    //Setea el nombre de los actores debajo del Shape.
                    act.TextPadding = new MindFusion.Diagramming.Thickness(1f, 25f, 1f, 1f); 
            }
        }

        public IDiagram ApplyActorFilter(string actorName, DiagramType diagramType, object[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Segun el string de condicion de la relga añadirá los grupos pertinentes
        /// Formato del string: -Condicion- Y -Condicion- ... Y (Grupos: -grupos-) 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="ruleCondition"></param>
        /// <param name="shapeAdded"></param>
        internal static List<UserGroup> AddRuleActors(IWFRuleParent r, string ruleCondition, GenericShape shapeAdded, Diagram diagram)
        {
            //Si hay un shape
            if(shapeAdded != null)
            {
                List<UserGroup> ruleUsrGroups = new List<UserGroup>();

                //Separa por dos puntos(para obtener solo los grupos)
                string[] splittedValues = ruleCondition.Split(':');
                if (splittedValues.Length > 0)
                {
                    //Elimina los parentesis innecesarios
                    string groups = splittedValues[splittedValues.Length - 1].Replace(")", string.Empty);
                    IUserGroup group;
                    //Separa los grupos por ,
                    foreach (string item in groups.Split(','))
                    {
                        //obtiene el grupo y agrega el actor.
                        group = new UserGroup() { ID = UserGroupBusiness.GetGroupIdByName(item.Trim()), Name = item };
                        ruleUsrGroups.Add((UserGroup)group);
                        ActorShape act = new ActorShape(diagram, (IActor)group, shapeAdded);
                        act.TextPadding = new MindFusion.Diagramming.Thickness(1f, 25f, 1f, 1f);
                        act.Expandable = false;
                    }
                }

                return ruleUsrGroups;
            }
            return null;
        }

        public IDiagram Refresh(object[] parameters)
        {
            return FillDiagram(parameters, true);
        }
    }
}
