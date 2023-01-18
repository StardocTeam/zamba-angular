using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Zamba.Core;
using MindFusion.Diagramming;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;

namespace Zamba.Diagrams.Controllers
{
    class ActorsController: IDiagramController
    {
        private MindFusion.Diagramming.Layout.TreeLayout treeLayout = null;

        public IDiagram GetDiagram(Object[] parameters)
        {
            //Se obtienen los datos a manipular
            ArrayList lstGroups = UserGroupBusiness.GetAllGroupsArrayList();

            if (lstGroups.Count > 0)
            {
                //Diagrama donde se comienzan a agregar los nodos
                Diagrams.Diagram diagActors = new Diagrams.Diagram();

                //Se crea un objeto ROOT para comenzar el diagrama
                ZCoreView rootObject = new ZCoreView(0, "Actors");
                GenericShape shpRoot = new GenericShape(diagActors, rootObject);

                //Se agrega el primer shape. Este seria como un titulo, el inicio de todo
                diagActors.Nodes.Add(shpRoot);

                //Ahora comienza el agregado automatico de los nodos
                for (int i = 0; i < lstGroups.Count; i++)
                {
                    //Se obtiene el grupo del listado
                    IUserGroup group = (IUserGroup)lstGroups[i];

                    //Se completan los usuarios del grupo
                    ArrayList users = UserBusiness.GetGroupUsers(group.ID);
                    group.Users = users;

                    //Se crea el shape especifico por actor
                    ActorShape shpGroup = new ActorShape(diagActors, (IActor)group, shpRoot);

                    //Busco la existencia de usuarios
                    if ( users != null && users.Count > 0)
                    {
                        for (int j = 0; j < users.Count; j++)
                        {
                            //Se obtiene el usuario del listado
                            IUser user = (IUser)group.Users[j];

                            //Se crea el shape del usuario
                            ActorShape shpUser = new ActorShape(diagActors, (IActor)user, shpGroup);
                        }
                    }
                }

                //Se organizan los objetos del diagrama
                SetLayout(diagActors, shpRoot);

                //Se devuelve el diagrama
                return diagActors;
            }
            else
            {
                return null;
            }
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
    }
}
