using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode genérico
    /// </summary>
    class GenericShape : ShapeNode, IShape,  IDisposable
    {
        #region Properties

        /// <summary>
        /// Id del ShapeNode padre
        /// </summary>
        public object ParentId { get; set; }

        /// <summary>
        /// Tipo de Shape
        /// </summary>
        public ShapeType ShapeType
        {
            get
            {
                return Core.ShapeType.Generic;
            }
        }

        /// <summary>
        /// Nodos hijos
        /// </summary>
        public List<ShapeNode> Childs { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode generico
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="generic">Objeto de Zamba genérico a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public GenericShape(Diagram diagram, ICore generic, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram)
        {
            //Generic attributes
            this.Id = generic.ID;
            this.Text = generic.Name;
            diagram.Nodes.Add(this);

            if (parentNode != null)
            {
                //set the parent id node
                this.ParentId = parentNode.Id;

                // create the new node and add it to the parent group
                // so its position is updated when the parent is moved
                this.AttachTo(parentNode, attachToNodeType);

                //Agrega el nodo actual como hijo del padre
                if (parentNode.Childs == null) parentNode.Childs = new List<ShapeNode>();
                parentNode.Childs.Add(this);

                // link the parent node with the child
                DiagramLink link = new DiagramLink(diagram, parentNode, this);
                diagram.Links.Add(link);
            }
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.Childs != null)
                {
                    for (int i = 0; i < this.Childs.Count; i++)
                        if (this.Childs[i] != null) this.Childs[i].Dispose();
                    this.Childs.Clear();
                }

                this.Childs = null;
                this.ParentId = null;
                base.Dispose();
            }
        }

        #endregion
    }
}
